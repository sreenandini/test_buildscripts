using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using System.Net.Sockets;

namespace BMC.CoreLib.Net
{
    public class TcpSocketClient3 : TcpSocketClient2
    {
        protected ManualResetEvent _mreShutdown = null;
        protected Thread _thReadBytesAsync = null;
        protected bool _isReadAsync = true;
        protected int _readBytesAsyncTimeout = 100;

        public TcpSocketClient3(int portNo)
            : base(portNo) { }

        public TcpSocketClient3(int portNo, string remoteAddress)
            : base(portNo, remoteAddress) { }

        public TcpSocketClient3(EndPoint remoteEndpoint)
            : base(remoteEndpoint) { }

        public int ReadBytesAsyncTimeout
        {
            get { return _readBytesAsyncTimeout; }
            set { _readBytesAsyncTimeout = Math.Min(1, value); }
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            _mreShutdown.Set();
            _isReadAsync = false;
            this.CloseReadBytesAsyncThread();
        }

        private void CloseReadBytesAsyncThread()
        {
            if (_thReadBytesAsync != null)
            {
                _thReadBytesAsync.WaitForThreadFinish();
                _thReadBytesAsync = null;
            }
        }

        public void RunReadBytesAsync()
        {
            this.CloseReadBytesAsyncThread();
            _thReadBytesAsync = Extensions.CreateThread(new ThreadStart(this.RunReadBytesAsync));
            _thReadBytesAsync.Start();
        }

        protected virtual void ReadBytesAsync()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ReadBytesAsync");

            try
            {
                byte[] buffer = new byte[RECEIVE_BUFFER_SIZE];
                this.WriteSocketLog(PROC, "Receive buffer size : " + RECEIVE_BUFFER_SIZE.ToString());

                while (!_mreShutdown.WaitOne(_readBytesAsyncTimeout))
                {
                    try
                    {
                        // Reconnect only if not created
                        if (_handler == null)
                        {
                            if (this.Start())
                            {
                                this.WriteSocketLog(PROC, "Socket connected freshly.");
                            }
                            else
                            {
                                if (this.Stop())
                                {
                                    this.WriteSocketLog(PROC, "Socket.Stop called.");
                                }
                                _mreShutdown.WaitOne(this.ReceiveTimeout); // 10 seconds
                                continue;
                            }
                        }

                        while (_isReadAsync)
                        {
                            int bytesRead = 0;
                            //this.WriteSocketLog(PROC, "Waiting to receive the data from server...");

                            // detect the socket disconnection
                            if (_socket.Poll(this.PollTimeout * 1000, SelectMode.SelectRead))
                            {
                                if ((_socket.Available == 0))
                                {
                                    this.WriteSocketLog(PROC, "Socket.Poll : Connection was closed.");
                                    this.Stop();
                                    _mreShutdown.WaitOne(_readBytesAsyncTimeout); // 2 seconds
                                    break;
                                }

                                // valid connection
                                SocketError socketErrorCode = SocketError.Success;
                                bytesRead = _socket.Receive(buffer, 0, buffer.Length, SocketFlags.None, out socketErrorCode);
                                if (!(socketErrorCode == SocketError.Success ||
                                    socketErrorCode == SocketError.TimedOut))
                                {
                                    this.WriteSocketLog(PROC, "Socket Error Occured : " + socketErrorCode.ToString() +
                                                                        ", IsConnected : " + _socket.Connected +
                                                                        ", Data Available : " + _socket.Available.ToString());
                                    this.Stop();
                                    _mreShutdown.WaitOne(_readBytesAsyncTimeout); // 10 seconds
                                    break;
                                }
#if DEBUG
                                else
                                {
                                    this.WriteSocketLog(PROC, "Positive Socket Error Occured : " + socketErrorCode.ToString() +
                                                                        ", IsConnected : " + _socket.Connected +
                                                                        ", Data Available : " + _socket.Available.ToString());
                                }
#endif

                                if (bytesRead > 0)
                                {
                                    byte[] buffer2 = new byte[bytesRead];
                                    Buffer.BlockCopy(buffer, 0, buffer2, 0, bytesRead);
                                    this.WriteSocketLog(PROC, "Received data from server length : " + bytesRead);
                                    ThreadPool.QueueUserWorkItem((o) =>
                                    {
                                        this.OnReceivedBytes(this, (byte[])o);
                                    }, buffer2);
                                }
                            }
                            else
                            {
#if DEBUG
                                this.WriteSocketLog(PROC, "Socket.Poll : Failed " +
                                                            ", IsConnected : " + _socket.Connected +
                                                            ", Data Available : " + _socket.Available.ToString());
#endif
                                if (!_socket.Connected)
                                {
                                    this.Stop();
                                    _mreShutdown.WaitOne(_readBytesAsyncTimeout); // 2 seconds
                                    break;
                                }
                            }

                            if (_mreShutdown.WaitOne(_readBytesAsyncTimeout)) break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                        this.Stop();
                        _mreShutdown.WaitOne(this.ReceiveTimeout); // 2 seconds
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.Stop();
            }
        }
    }
}
