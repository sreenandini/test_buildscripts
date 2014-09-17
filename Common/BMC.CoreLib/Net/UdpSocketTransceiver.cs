using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Services;

namespace BMC.CoreLib.Net
{
    /// <summary>
    /// Socket data received handler
    /// </summary>
    /// <param name="remoteEndPoint">The remote end point.</param>
    /// <param name="buffer">The buffer.</param>
    /// <param name="length">The length.</param>
    public delegate void UdpSocketReceivedDataHandler(UdpSocketReceiveData receivedData);

    /// <summary>
    /// Socket Receive Data
    /// </summary>
    public class UdpSocketReceiveData : DisposableObject
    {
        public EndPoint RemoteEndpoint { get; internal set; }
        public byte[] Buffer { get; internal set; }
        public int BufferLength { get; internal set; }
    }

    /// <summary>
    /// Socket Send Data
    /// </summary>
    public class UdpSocketSendData : DisposableObject
    {
        public IPAddress IPAddress { get; set; }
        public int PortNo { get; set; }

        public EndPoint RemoteEndpoint { get; set; }
        public byte[] Buffer { get; set; }
        public int BufferOffset { get; set; }
        public int BufferLength { get; set; }
    }

    /// <summary>
    /// Socket Parameter
    /// </summary>
    public class UdpSocketTransceiverParameter : DisposableObject
    {
        public UdpSocketTransceiverParameter()
        {
            this.ReadBufferSize = 8096;
            this.WriteBufferSize = 8096;
            this.ListenWaitTime = 100;
            this.TransceiverType = UdpSocketTransceiverTypes.Udp;
            this.ClientThreads = 1;
        }

        public string IPAddress { get; set; }
        public int PortNo { get; set; }
        public string MulticastIP { get; set; }
        public string InterfaceIP { get; set; }

        public int ReadBufferSize { get; set; }
        public int WriteBufferSize { get; set; }

        public int ListenWaitTime { get; set; }
        public bool UseInternalQueue { get; set; }
        public UdpSocketTransceiverTypes TransceiverType { get; set; }
        public int ClientThreads { get; set; }
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IUdpSocketReceiver : IListener
    {
        event UdpSocketReceivedDataHandler DataReceived;
    }

    public interface IUdpSocketTransmitter : IListener
    {
        bool Transmit(UdpSocketSendData sendData);
    }

    public interface IUdpSocketTransceiver : IUdpSocketReceiver, IUdpSocketTransmitter { }

    public class IPAddressComparer : IComparer<IPAddress>
    {
        public int Compare(IPAddress x, IPAddress y)
        {
            return String.Compare(x.ToString(), y.ToString(), true);
        }
    }

    public enum UdpSocketTransceiverTypes
    {
        Udp = 0,
        Tcp = 1,
    }

    public static class UdpSocketTransceiverFactory
    {
        public static IUdpSocketTransceiver Create(IExecutorService executorService, UdpSocketTransceiverParameter parameter)
        {
            if (parameter.TransceiverType == UdpSocketTransceiverTypes.Tcp)
                return new UdpSocketTransceiverImpl(executorService, parameter);
            return new UdpSocketTransceiverImpl(executorService, parameter);
        }
    }

    internal class UdpSocketTransceiverImpl
        : ListenerBase, IUdpSocketTransceiver
    {
        private readonly IDictionary<string, EndPoint> _remoteEndPoints = null;
        protected UdpSocketTransceiverParameter _parameter = null;
        protected UdpSocketClientServer _socketReceiver = null;
        protected UdpSocketClientClient _socketTransmitter = null;
        private readonly IProducerConsumerQueue<UdpSocketReceiveData> _queueReceived = null;
        private readonly bool _useInternalQueue = false;

        protected byte[] _readBuffer = null;
        protected byte[] _writeBuffer = null;

        internal UdpSocketTransceiverImpl(IExecutorService executorService, UdpSocketTransceiverParameter parameter)
            : base(executorService)
        {
            _parameter = parameter;
            _readBuffer = new byte[Math.Max(256, parameter.ReadBufferSize)];
            _writeBuffer = new byte[Math.Max(256, parameter.WriteBufferSize)];

            _useInternalQueue = parameter.UseInternalQueue;
            if (_useInternalQueue)
            {
                _queueReceived = ProducerConsumerQueueFactory.Create<UdpSocketReceiveData>(executorService, -1,
                    parameter.ListenWaitTime, false);
                _queueReceived.Dequeue += this.OnDataReceived;
            }
            _remoteEndPoints = new SortedDictionary<string, EndPoint>();
        }

        protected override bool StartInternal()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "StartInternal"))
            {
                bool result = default(bool);

                try
                {
                    // receiver socket
                    if (_parameter.IPAddress.IsEmpty())
                        _parameter.IPAddress = Extensions.GetIpAddressString(-1);
                    _socketReceiver = new UdpSocketClientServer(_parameter.IPAddress, _parameter.PortNo);
                    string boundAddress = _socketReceiver.LocalEndPoint.ToString();
                    method.InfoV("Socket bind address : {0}", boundAddress);

                    if (!_parameter.MulticastIP.IsEmpty() &&
                        !_parameter.InterfaceIP.IsEmpty())
                    {
                        result = _socketReceiver.BindMulticastIP(_parameter.MulticastIP, _parameter.InterfaceIP);
                        method.InfoV("Socket multicast address : {0}, {1}", _parameter.MulticastIP, _parameter.InterfaceIP);
                    }
                    else
                    {
                        result = _socketReceiver.Bind();
                    }

                    if (result)
                    {
                        method.InfoV("Socket was successfully bounded to : {0}", boundAddress);
                        if (Extensions.UseTaskInsteadOfThread)
                            Extensions.CreateLongRunningTask(this.OnListen);
                        else
                            Extensions.CreateThreadAndStart(new System.Threading.ThreadStart(this.OnListen));
                    }
                    else
                    {
                        method.InfoV("!!! Unable to bind the socket on : {0}", boundAddress);
                    }

                    // sender socket
                    _socketTransmitter = new UdpSocketClientClient();
                    result = _socketTransmitter.Bind();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected override bool StopInternal()
        {
            return true;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            this.Stop();
        }

        private void OnListen()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnListen");

            try
            {
                IExecutorService executor = this.Executor;
                int waitTime = _parameter.ListenWaitTime;
                IPEndPoint ep = _socketReceiver.LocalEndPoint as IPEndPoint;

                while (!executor.WaitForShutdown(waitTime))
                {
                    try
                    {
                        EndPoint remoteEndPoint = new IPEndPoint(ep.Address, ep.Port);
                        IAsyncResult readAsync = _socketReceiver.BeginReadFrom(_readBuffer, ref remoteEndPoint);

                        int waitIndex = executor.WaitAny(readAsync.AsyncWaitHandle, -1);
                        if (waitIndex == 0) break;

                        int bytesRead = _socketReceiver.EndReadFrom(readAsync, ref remoteEndPoint);
                        if (bytesRead <= 0)
                        {
                            _socketReceiver.WriteSocketLog("Invalid data received from : " + remoteEndPoint.ToString());
                        }
                        else
                        {
                            byte[] receivedBuffer = new byte[bytesRead];
                            Buffer.BlockCopy(_readBuffer, 0, receivedBuffer, 0, bytesRead);
                            IPEndPoint rep = remoteEndPoint as IPEndPoint;

                            UdpSocketReceiveData receivedData = new UdpSocketReceiveData()
                            {
                                Buffer = receivedBuffer,
                                BufferLength = receivedBuffer.Length,
                                RemoteEndpoint = new IPEndPoint(rep.Address, rep.Port),
                            };
                            if (_useInternalQueue)
                            {
                                _queueReceived.Enqueue(receivedData);
                            }
                            else
                            {
                                this.OnDataReceived(receivedData);
                            }
                        }
                        remoteEndPoint = null;
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

        public event UdpSocketReceivedDataHandler DataReceived = null;

        private void OnDataReceived(UdpSocketReceiveData receivedData)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnDataReceived");

            try
            {
                if (this.DataReceived != null)
                {
                    this.DataReceived(receivedData);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private bool Transmit(IPAddress remoteAddress, int portNo, byte[] buffer, int offset, int length)
        {
            EndPoint ep = null;
            string key = string.Format("{0}_{1:D}", remoteAddress.ToString(), portNo);
            if (_remoteEndPoints.ContainsKey(key))
            {
                ep = _remoteEndPoints[key];
            }
            else
            {
                ep = new IPEndPoint(remoteAddress, portNo);
                _remoteEndPoints.Add(key, ep);
            }
            return this.Transmit(ep, buffer, offset, length);
        }

        private bool Transmit(EndPoint remoteEndPoint, byte[] buffer, int offset, int length)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Transmit");
            bool result = default(bool);
            string address = string.Empty;
            int sent = 0;

            try
            {
                address = remoteEndPoint.ToString();
                if (length == 0) length = buffer.Length;
                sent = _socketTransmitter.WriteTo(buffer, offset, length, ref remoteEndPoint);
                result = (sent > 0);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                Log.InfoV(PROC, "TRANSMIT : {0:D} bytes of data was sent to {1} ({2}).", sent, address, (result ? "Success" : "Failure"));
            }

            return result;
        }

        public bool Transmit(UdpSocketSendData sendData)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Transmit");
            bool result = default(bool);

            try
            {
                if (sendData.RemoteEndpoint != null)
                {
                    result = this.Transmit(sendData.RemoteEndpoint, sendData.Buffer, sendData.BufferOffset, sendData.BufferLength);
                }
                else
                {
                    result = this.Transmit(sendData.IPAddress, sendData.PortNo, sendData.Buffer, sendData.BufferOffset, sendData.BufferLength);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }

    internal class TcpSocketTransceiverImpl
        : ListenerBase, IUdpSocketTransceiver
    {
        private readonly IDictionary<IPAddress, EndPoint> _remoteEndPoints = null;
        protected UdpSocketTransceiverParameter _parameter = null;
        protected TcpSocketServer _socket = null;
        private readonly IProducerConsumerQueue<UdpSocketReceiveData> _queueReceived = null;
        private readonly bool _useInternalQueue = false;

        protected byte[] _readBuffer = null;
        protected byte[] _writeBuffer = null;

        internal TcpSocketTransceiverImpl(IExecutorService executorService, UdpSocketTransceiverParameter parameter)
            : base(executorService)
        {
            _parameter = parameter;
            _readBuffer = new byte[Math.Max(256, parameter.ReadBufferSize)];
            _writeBuffer = new byte[Math.Max(256, parameter.WriteBufferSize)];

            _useInternalQueue = parameter.UseInternalQueue;
            if (_useInternalQueue)
            {
                _queueReceived = ProducerConsumerQueueFactory.Create<UdpSocketReceiveData>(executorService, -1,
                    parameter.ListenWaitTime, false);
                _queueReceived.Dequeue += this.OnDataReceived;
            }
            _remoteEndPoints = new SortedDictionary<IPAddress, EndPoint>(new IPAddressComparer());
        }

        protected override bool StartInternal()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "StartInternal"))
            {
                bool result = default(bool);

                try
                {
                    _socket = new TcpSocketServer(_parameter.PortNo, _parameter.ClientThreads);
                    string boundAddress = _socket.LocalEndPoint.ToString();
                    method.InfoV("Socket bind address : {0}", boundAddress);

                    // bind the socket
                    result = _socket.Bind();
                    if (result)
                    {
                        method.InfoV("Socket was successfully bounded to : {0}", boundAddress);
                        _socket.ReceivedBytes += OnSocket_ReceivedBytes;
                    }
                    else
                    {
                        method.InfoV("!!! Unable to bind the socket on : {0}", boundAddress);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected override bool StopInternal()
        {
            return true;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            this.Stop();
        }

        private void OnListen()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnListen");

            try
            {
                IExecutorService executor = this.Executor;
                int waitTime = _parameter.ListenWaitTime;
                IPEndPoint ep = _socket.LocalEndPoint as IPEndPoint;

                while (!executor.WaitForShutdown(waitTime))
                {
                    try
                    {
                        EndPoint remoteEndPoint = new IPEndPoint(ep.Address, ep.Port);
                        IAsyncResult readAsync = _socket.BeginReadFrom(_readBuffer, ref remoteEndPoint);

                        int waitIndex = executor.WaitAny(readAsync.AsyncWaitHandle, -1);
                        if (waitIndex == 0) break;

                        int bytesRead = _socket.EndReadFrom(readAsync, ref remoteEndPoint);
                        if (bytesRead <= 0)
                        {
                            _socket.WriteSocketLog("Invalid data received from : " + remoteEndPoint.ToString());
                        }
                        else
                        {
                            byte[] receivedBuffer = new byte[bytesRead];
                            Buffer.BlockCopy(_readBuffer, 0, receivedBuffer, 0, bytesRead);
                            IPEndPoint rep = remoteEndPoint as IPEndPoint;

                            UdpSocketReceiveData receivedData = new UdpSocketReceiveData()
                            {
                                Buffer = receivedBuffer,
                                BufferLength = receivedBuffer.Length,
                                RemoteEndpoint = new IPEndPoint(rep.Address, rep.Port),
                            };
                            if (_useInternalQueue)
                            {
                                _queueReceived.Enqueue(receivedData);
                            }
                            else
                            {
                                this.OnDataReceived(receivedData);
                            }
                        }
                        remoteEndPoint = null;
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

        public event UdpSocketReceivedDataHandler DataReceived = null;

        private void OnDataReceived(UdpSocketReceiveData receivedData)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnDataReceived");

            try
            {
                if (this.DataReceived != null)
                {
                    this.DataReceived(receivedData);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        bool OnSocket_ReceivedBytes(INetSocketHandler client, byte[] data)
        {
            this.OnDataReceived(new UdpSocketReceiveData()
            {
                Buffer = data,
                BufferLength = data.Length,
                RemoteEndpoint = client.RemoteEndPoint,
            });
            return true;
        }

        private bool Transmit(IPAddress remoteAddress, int portNo, byte[] buffer, int offset, int length)
        {
            EndPoint ep = null;
            if (_remoteEndPoints.ContainsKey(remoteAddress))
            {
                ep = _remoteEndPoints[remoteAddress];
            }
            else
            {
                ep = new IPEndPoint(remoteAddress, portNo);
                _remoteEndPoints.Add(remoteAddress, ep);
            }
            return this.Transmit(ep, buffer, offset, length);
        }

        private bool Transmit(EndPoint remoteEndPoint, byte[] buffer, int offset, int length)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Transmit");
            bool result = default(bool);

            try
            {
                int sent = _socket.WriteTo(buffer, offset, length, ref remoteEndPoint);
                result = (sent > 0);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public bool Transmit(UdpSocketSendData sendData)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Transmit");
            bool result = default(bool);

            try
            {
                if (sendData.BufferLength == 0)
                {
                    sendData.BufferLength = sendData.Buffer.Length;
                }
                if (sendData.RemoteEndpoint != null)
                {
                    result = this.Transmit(sendData.RemoteEndpoint, sendData.Buffer, sendData.BufferOffset, sendData.BufferLength);
                }
                else
                {
                    result = this.Transmit(sendData.IPAddress, sendData.PortNo, sendData.Buffer, sendData.BufferOffset, sendData.BufferLength);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
