using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using BMC.CoreLib.Diagnostics;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace BMC.CoreLib.Net
{
    public abstract class NetSocketHandler : DisposableObject, INetSocketHandler, INetSocketHandlerSsl
    {
        protected object _lockSocket = new object();
        protected Socket _socket = null;
        protected EndPoint _localEndPoint = null;
        protected EndPoint _remoteEndPoint = null;

        protected const int SEND_BUFFER_SIZE = 8096;
        protected const int RECEIVE_BUFFER_SIZE = 8096;

        protected BMC.CoreLib.NativeMethods.WinSock.TimeValue _ioWait = new NativeMethods.WinSock.TimeValue();

        protected NetSocketHandler()
        {
            this.PollTimeout = 10000000; // 10 seconds
        }

        #region ISocketHandler Members

        public Socket InternalSocket
        {
            get { return _socket; }
        }

        public Encoding Encoding { get; set; }

        public abstract bool IsConnected { get; }

        public System.Net.EndPoint LocalEndPoint { get { return _localEndPoint; } }

        public System.Net.EndPoint RemoteEndPoint { get { return _remoteEndPoint; } }

        private int _sendTimeout = 0;
        public int SendTimeout
        {
            get
            {
                return _sendTimeout;
            }
            set
            {
                if (value < 0) value = 0;
                _sendTimeout = value;
            }
        }

        private int _receiveTimeout = 0;
        public int ReceiveTimeout
        {
            get
            {
                return _receiveTimeout;
            }
            set
            {
                if (value < 0) value = 0;
                _receiveTimeout = value;
            }
        }

        protected int _pollTimeout = 0;
        public virtual int PollTimeout
        {
            get
            {
                return _pollTimeout;
            }
            set
            {
                if (value < 0) value = 1;
                _pollTimeout = (value * 1000);
                BMC.CoreLib.NativeMethods.WinSock.MicrosecondsToTimeValue(_pollTimeout, ref _ioWait);
            }
        }

        public new INetSocketHandler Clone()
        {
            throw new NotImplementedException();
        }

        public bool Start()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Start");
            bool result = default(bool);
            this.Stop();

            try
            {
                result = this.StartInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                this.Stop();
            }

            return result;
        }

        protected abstract bool StartInternal();

        public bool Stop()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Start");
            bool result = default(bool);

            try
            {
                result = this.StopInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            _socket = null;
            return result;
        }

        protected abstract bool StopInternal();

        protected bool EnsureConnectionEstablished()
        {
            return this.EnsureInternalConnectionEstablished();
        }

        protected virtual bool EnsureInternalConnectionEstablished() { return false; }

        public bool Bind()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Bind");
            bool result = default(bool);

            try
            {
                result = this.BindInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract bool BindInternal();

        public bool BindMulticastIP(string multicastAddress, string localAddress)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Bind");
            bool result = default(bool);

            try
            {
                if (!this.Bind())
                {
                    return false;
                }

                IPAddress multicastAddressIp = null;
                if (!IPAddress.TryParse(multicastAddress, out multicastAddressIp))
                {
                    return false;
                }

                IPAddress localAddressIp = null;
                if (!IPAddress.TryParse(localAddress, out localAddressIp))
                {
                    return false;
                }

                result = this.BindMulticastIPInternal(multicastAddressIp, localAddressIp);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract bool BindMulticastIPInternal(IPAddress multicastAddressIp, IPAddress localAddressIp);

        protected bool Connect(EndPoint remoteEndpoint)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Connect");
            bool result = default(bool);

            try
            {
                _remoteEndPoint = remoteEndpoint;
                result = this.ConnectInternal(PROC, remoteEndpoint);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract bool ConnectInternal(ModuleProc PROC, EndPoint remoteEndpoint);

        protected bool Disconnect()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Disconnect");
            bool result = default(bool);

            try
            {
                result = this.DisconnectInternal(PROC);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected abstract bool DisconnectInternal(ModuleProc PROC);

        public int Write(string data)
        {
            if (data.IsEmpty()) return -1;
            return this.Write(this.Encoding.GetBytes(data));
        }

        public int Write(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0) return -1;
            return this.Write(buffer, 0, buffer.Length);
        }

        public int Write(byte[] buffer, int offset, int size)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Write");
            int result = default(int);

            try
            {
                result = this.WriteInternal(PROC, buffer, offset, size);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                result = -1;
            }

            return result;
        }

        protected virtual int WriteInternal(ModuleProc PROC, byte[] buffer, int offset, int size)
        {
            return this._WriteInternal(buffer, offset, size);
        }

        protected virtual int _WriteInternal(byte[] buffer, int offset, int size)
        {
            SocketError errorCode = SocketError.SocketError;
            int bytesSend = _socket.Send(buffer, offset, size, SocketFlags.None, out errorCode);
            return bytesSend;
        }

        public int WriteTo(string data, ref EndPoint remoteEndPoint)
        {
            if (data.IsEmpty()) return -1;
            return this.WriteTo(this.Encoding.GetBytes(data), ref remoteEndPoint);
        }

        public int WriteTo(byte[] buffer, ref EndPoint remoteEndPoint)
        {
            if (buffer == null || buffer.Length == 0) return -1;
            return this.WriteTo(buffer, 0, buffer.Length, ref remoteEndPoint);
        }

        public int WriteTo(byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "WriteTo");
            int result = default(int);

            try
            {
                result = this.WriteToInternal(PROC, buffer, offset, size, ref remoteEndPoint);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                result = -1;
            }

            return result;
        }

        protected virtual int WriteToInternal(ModuleProc PROC, byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint)
        {
            return this._WriteToInternal(buffer, offset, size, ref remoteEndPoint);
        }

        protected virtual int _WriteToInternal(byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint)
        {
            return _socket.SendTo(buffer, offset, size, SocketFlags.None, remoteEndPoint);
        }

        #region Stream Sync Read
        public string Read()
        {
            return string.Empty;
        }

        public int Read(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0) return -1;
            return this.Read(buffer, 0, buffer.Length);
        }

        public int Read(byte[] buffer, int offset, int size)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Read");
            int result = default(int);

            try
            {
                result = this.ReadInternal(PROC, buffer, offset, size);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                result = -1;
            }

            return result;
        }

        protected virtual int ReadInternal(ModuleProc PROC, byte[] buffer, int offset, int size)
        {
            return this._ReadInternal(buffer, offset, size);
        }

        protected virtual int _ReadInternal(byte[] buffer, int offset, int size)
        {
            SocketError errorCode = SocketError.SocketError;
            return _socket.Receive(buffer, offset, size, SocketFlags.None, out errorCode);
        }
        #endregion

        #region Datagram Sync Read
        public string ReadFrom(ref EndPoint remoteEndPoint)
        {
            return string.Empty;
        }

        public int ReadFrom(byte[] buffer, ref EndPoint remoteEndPoint)
        {
            if (buffer == null || buffer.Length == 0) return -1;
            return this.ReadFrom(buffer, 0, buffer.Length, ref remoteEndPoint);
        }

        public int ReadFrom(byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Read");
            int result = default(int);

            try
            {
                result = this.ReadFromInternal(PROC, buffer, offset, size, ref remoteEndPoint);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                result = -1;
            }

            return result;
        }

        protected virtual int ReadFromInternal(ModuleProc PROC, byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint)
        {
            return this._ReadFromInternal(buffer, offset, size, ref remoteEndPoint);
        }

        protected virtual int _ReadFromInternal(byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint)
        {
            return _socket.ReceiveFrom(buffer, offset, size, SocketFlags.None, ref remoteEndPoint);
        }
        #endregion

        #region Datagram Async Read

        public IAsyncResult BeginReadFrom(ref EndPoint remoteEndPoint)
        {
            return null;
        }

        public IAsyncResult BeginReadFrom(byte[] buffer, ref EndPoint remoteEndPoint)
        {
            if (buffer == null || buffer.Length == 0) return null;
            return this.BeginReadFrom(buffer, 0, buffer.Length, ref remoteEndPoint);
        }

        public IAsyncResult BeginReadFrom(byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint)
        {

            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Read");
            IAsyncResult result = default(IAsyncResult);

            try
            {
                result = this.BeginReadFromInternal(PROC, buffer, offset, size, ref remoteEndPoint);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                result = null;
            }

            return result;
        }

        protected virtual IAsyncResult BeginReadFromInternal(ModuleProc PROC, byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint)
        {
            return _socket.BeginReceiveFrom(buffer, offset, size, SocketFlags.None, ref remoteEndPoint, null, this);
        }

        public int EndReadFrom(IAsyncResult asyncResult, ref EndPoint remoteEndPoint)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Read");
            int result = default(int);

            try
            {
                result = this.EndReadFromInternal(PROC, asyncResult, ref remoteEndPoint);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                result = -1;
            }

            return result;
        }

        protected virtual int EndReadFromInternal(ModuleProc PROC, IAsyncResult asyncResult, ref EndPoint remoteEndPoint)
        {
            return _socket.EndReceiveFrom(asyncResult, ref remoteEndPoint);
        }

        #endregion

        public void WriteSocketLog(string status)
        {
            this.WriteSocketLog(null, status);
        }

        public void WriteSocketLogV(string status, params object[] values)
        {
            this.WriteSocketLog(null, string.Format(status, values));
        }

        public void WriteSocketLog(ModuleProc PROC, string status)
        {
            string log = string.Empty;

            if (this.LocalEndPoint != null)
            {
                log = this.LocalEndPoint.ToString();
            }
            if (this.RemoteEndPoint != null)
            {
                if (!log.IsEmpty()) log += " / ";
                log += this.RemoteEndPoint.ToString();
            }
            if (!log.IsEmpty()) log += " => ";
            log += status;

            if (PROC == null)
                Log.Info(log);
            else
                Log.Info(PROC, log);
        }

        public void WriteSocketLogV(ModuleProc PROC, string status, params object[] values)
        {
            this.WriteSocketLog(PROC, string.Format(status, values));
        }

        IPEndPoint INetSocketHandler.LocalEndPoint
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        public event SocketReceivedBytesHandler ReceivedBytes = null;

        protected bool OnReceivedBytes(INetSocketHandler socket, byte[] data)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnClientReadBytes");

            try
            {
                if (this.ReceivedBytes != null)
                {
                    return this.ReceivedBytes(socket, data);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                return false;
            }
            return true;
        }

        protected bool HasReceivedBytesEvent
        {
            get { return this.ReceivedBytes != null; }
        }

        #region MyRegion

        private SslStream _sslStream = null;
        private SslProtocols _sslProtocol = SslProtocols.Default;

        public virtual void CloseSslStream()
        {
            BMC.CoreLib.Diagnostics.ModuleProc PROC = new BMC.CoreLib.Diagnostics.ModuleProc(this.DYN_MODULE_NAME, "CloseSslStream");

            try
            {
                if (_sslStream != null)
                {
                    _sslStream.Close();
                    _sslStream = null;
                }
            }
            catch (Exception ex)
            {
                BMC.CoreLib.Log.Exception(PROC, ex);
            }
        }

        public event System.Net.Security.RemoteCertificateValidationCallback RemoteCertificateValidation = null;

        public SslProtocols SslProtocol
        {
            get { return _sslProtocol; }
            set { _sslProtocol = value; }
        }

        public System.Net.Security.SslStream GetSslStream(string targetHost, X509CertificateCollection clientCertificates)
        {
            BMC.CoreLib.Diagnostics.ModuleProc PROC = new BMC.CoreLib.Diagnostics.ModuleProc(this.DYN_MODULE_NAME, "GetSslStream");

            try
            {
                if (!this.EnsureConnectionEstablished())
                {
                    this.WriteSocketLog("Unable to establish the connection.");
                    return null;
                }
                else
                {
                    if (_sslStream != null) return _sslStream;
                }

                NetworkStream ns = new NetworkStream(this.InternalSocket, true);
                if (this.RemoteCertificateValidation != null)
                {
                    _sslStream = new SslStream(ns, false, this.RemoteCertificateValidation, null);
                }
                else
                {
                    _sslStream = new SslStream(ns, false);
                }

                _sslStream.AuthenticateAsClient(targetHost, clientCertificates, this.SslProtocol, false);
            }
            catch (Exception ex)
            {
                BMC.CoreLib.Log.Exception(PROC, ex);
                this.Stop();
            }

            return _sslStream;
        }

        #endregion
    }
}
