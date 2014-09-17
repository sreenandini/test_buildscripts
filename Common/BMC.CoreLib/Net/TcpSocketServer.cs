using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Collections;
using System.Threading;
using System.Collections;

namespace BMC.CoreLib.Net
{
    public class TcpSocketServer : NetSocketHandler
    {
        protected TcpListener _handler = null;
        private IExecutorService _executorService = null;
        private IThreadPoolExecutor<ClientsInfo> _pool = null;
        private IDictionary<int, ClientsInfo> _clients = new ConcurrentDictionary<int, ClientsInfo>();

        private readonly int _totalThreads = -1;
        private readonly bool _hasThreads = false;

        #region ClientsInfo
        private class ClientsInfo : DisposableObject, IExecutorKeyThread
        {
            private IDictionary<int, INetSocketHandler> _clients = null;
            private IDictionary<Socket, INetSocketHandler> _clients2 = null;
            private object _clientsLock = new object();
            private Socket[] _sockets = null;

            public ClientsInfo(int threadIndex)
            {
                this.ThreadIndex = threadIndex;
                _clients = new ConcurrentDictionary<int, INetSocketHandler>();
                _clients2 = new ConcurrentDictionary<Socket, INetSocketHandler>();
                this.PrepareHandles();
            }

            public int ThreadIndex { get; set; }

            public void AddClient(INetSocketHandler client)
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddClient");

                try
                {
                    int hashcode = client.GetHashCode();
                    if (!_clients.ContainsKey(hashcode))
                    {
                        _clients.Add(hashcode, client);
                        _clients2.Add(client.InternalSocket, client);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
                finally
                {
                    this.PrepareHandles();
                }
            }

            public void RemoveClient(INetSocketHandler client)
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddClient");

                try
                {
                    int hashcode = client.GetHashCode();

                    if (_clients.ContainsKey(hashcode))
                    {
                        _clients.Remove(hashcode);
                        _clients2.Remove(client.InternalSocket);
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
                finally
                {
                    this.PrepareHandles();
                }
            }

            private void PrepareHandles()
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "PrepareHandles");

                try
                {
                    if (_clients.Count > 0)
                    {
                        _sockets = (from h in _clients.Values
                                    select h.InternalSocket).ToArray();
                    }
                    else
                    {
                        _sockets = new Socket[0];
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }

            public Socket[] InternalSockets
            {
                get { return _sockets; }
            }

            public INetSocketHandler this[Socket socket]
            {
                get
                {
                    if (_clients2.ContainsKey(socket))
                    {
                        return _clients2[socket];
                    }
                    return null;
                }
            }

            #region IExecutorKeyThread Members

            public int GetThreadIndex(int capacity)
            {
                return this.ThreadIndex;
            }

            #endregion

            #region IExecutorKey Members

            public string UniqueKey
            {
                get { return this.ThreadIndex.ToString(); }
            }

            #endregion
        }
        #endregion

        public TcpSocketServer(int portNo)
            : this(portNo, 0) { }

        public TcpSocketServer(int portNo, int totalThreads)
        {
            _localEndPoint = new IPEndPoint(IPAddress.Any, portNo);
            _totalThreads = (totalThreads <= 0 ? -1 : totalThreads);
            _hasThreads = (totalThreads != 0);
        }

        public override bool IsConnected
        {
            get { return true; }
        }

        protected override bool StartInternal()
        {
            return this.Bind();
        }

        protected override bool StopInternal()
        {
            if (_executorService != null)
            {
                _executorService.AwaitTermination(new TimeSpan(0, 5, 0));
                _executorService = null;

                if (_hasThreads)
                {
                    _pool.Dispose();
                    _pool = null;
                }
            }
            return true;
        }

        protected override bool BindInternal()
        {
            if (_handler == null)
            {
                _executorService = ExecutorServiceFactory.CreateExecutorService();
                if (_hasThreads)
                {
                    _pool = ThreadPoolExecutorFactory.CreateThreadPool<ClientsInfo>(
                        new ThreadPoolExecutorArg()
                        {
                            ExecutorService = _executorService,
                            PoolType = (_totalThreads == -1 ? ThreadPoolType.NonBlockDynamic : ThreadPoolType.AsyncTaskQueue),
                            ThreadCount = _totalThreads,

                        });
                    _pool.ProcessItem += new ExecutorProcessItemHandler<ClientsInfo>(OnPool_ProcessItem);
                }

                _handler = new TcpListener(_localEndPoint as IPEndPoint);
                _socket = _handler.Server;
                _handler.Start();

                if (Extensions.UseTaskInsteadOfThread)
                    Extensions.CreateLongRunningTask(this.OnListen);
                else
                    Extensions.CreateThreadAndStart(new System.Threading.ThreadStart(this.OnListen));
            }
            return (_handler != null);
        }

        private int _clientWaitTimeout = 100;

        public int ClientWaitTimeout
        {
            get { return _clientWaitTimeout; }
            set { _clientWaitTimeout = Math.Max(100, value); }
        }


        private void OnPool_ProcessItem(TcpSocketServer.ClientsInfo item)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnPool_ProcessItem");

            try
            {
                while (!_executorService.WaitForShutdown(_clientWaitTimeout))
                {
                    try
                    {
                        if (item.InternalSockets.Length > 0)
                        {
                            IList temp = new List<Socket>(item.InternalSockets);

                            try
                            {
                                Socket.Select(temp, null, null, this.PollTimeout);
                                for (int i = 0; i < temp.Count; i++)
                                {
                                    Socket clientSocket = (Socket)temp[i];
                                    if (clientSocket != null)
                                    {
                                        INetSocketHandler client = item[clientSocket];
                                        if (client != null)
                                        {
                                            if (clientSocket.Available > 0)
                                            {
                                                byte[] buffer = new byte[clientSocket.Available];
                                                client.Read(buffer, 0, buffer.Length);
                                                this.OnReceivedBytes(client, buffer);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (SocketException)
                            {
                                // no need to log
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override bool BindMulticastIPInternal(IPAddress multicastAddressIp, IPAddress localAddressIp)
        {
            throw new NotImplementedException();
        }

        protected override bool ConnectInternal(BMC.CoreLib.Diagnostics.ModuleProc PROC, EndPoint remoteEndpoint)
        {
            throw new NotImplementedException();
        }

        protected override bool DisconnectInternal(BMC.CoreLib.Diagnostics.ModuleProc PROC)
        {
            throw new NotImplementedException();
        }

        private void OnListen()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnListen");

            try
            {
                while (!_executorService.WaitForShutdown(100))
                {
                    try
                    {
                        IAsyncResult acceptAsync = _socket.BeginAccept(null, null);

                        int waitIndex = _executorService.WaitAny(acceptAsync.AsyncWaitHandle, -1);
                        if (waitIndex == 0) break;

                        Socket clientSocket = _socket.EndAccept(acceptAsync);
                        INetSocketHandler client = new TcpSocketServerClient(clientSocket);
                        int hashcode = client.GetHashCode();
                        if (_hasThreads)
                        {
                            if (_totalThreads != -1)
                            {
                                int threadIndex = (hashcode % _totalThreads);
                                ClientsInfo ci = null;

                                if (_clients.ContainsKey(threadIndex))
                                {
                                    ci = _clients[threadIndex];
                                }
                                else
                                {
                                    ci = new ClientsInfo(threadIndex);
                                    _clients.Add(threadIndex, ci);
                                    _pool.QueueWorkerItem(ci);
                                }

                                Log.InfoV(PROC, "Client [{0}] was connected at Thread {1:D}", client.ToString(), threadIndex);
                                ci.AddClient(client);
                            }
                            else
                            {
                            }
                        }

                        this.OnClientConnected(client);
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event SocketClientConnectedHandler ClientConnected = null;

        private void OnClientConnected(INetSocketHandler client)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnClientConnected");

            try
            {
                if (this.ClientConnected != null)
                {
                    this.ClientConnected(this, client);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override int WriteInternal(BMC.CoreLib.Diagnostics.ModuleProc PROC, byte[] buffer, int offset, int size)
        {
            return 0;
        }

        protected override int _WriteToInternal(byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint)
        {
            return base._WriteToInternal(buffer, offset, size, ref remoteEndPoint);
        }

        protected override int ReadInternal(BMC.CoreLib.Diagnostics.ModuleProc PROC, byte[] buffer, int offset, int size)
        {
            return 0;
        }

        public override string ToString()
        {
            return _localEndPoint.ToString();
        }
    }
}
