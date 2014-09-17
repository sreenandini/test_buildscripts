using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using BMC.CoreLib.Diagnostics;
using System.Threading;

namespace BMC.CoreLib.Net
{
    public class TcpSocketClient : NetSocketHandler
    {
        protected TcpClient _handler = null;

        public TcpSocketClient(int portNo)
        {
            _remoteEndPoint = new IPEndPoint(IPAddress.Loopback, portNo);
        }

        public TcpSocketClient(int portNo, string remoteAddress)
        {
            IPAddress remoteAddressIp = null;
            if (!IPAddress.TryParse(remoteAddress, out remoteAddressIp))
            {
                _remoteEndPoint = new IPEndPoint(remoteAddressIp, portNo);
            }
        }

        public TcpSocketClient(EndPoint remoteEndpoint)
        {
            _remoteEndPoint = remoteEndpoint;
        }

        public override bool IsConnected
        {
            get { throw new NotImplementedException(); }
        }

        protected override bool StartInternal()
        {
            return this.Connect(_remoteEndPoint);
        }

        protected override bool StopInternal()
        {
            return this.Disconnect();
        }

        protected override bool EnsureInternalConnectionEstablished()
        {
            bool result = false;
            if (_handler == null)
            {
                result = this.Start();
            }
            else
            {
                if (!this.IsConnected)
                {
                    result = this.Start();
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }

        protected override bool BindInternal()
        {
            throw new NotImplementedException();
        }

        protected override bool BindMulticastIPInternal(System.Net.IPAddress multicastAddressIp, System.Net.IPAddress localAddressIp)
        {
            throw new NotImplementedException();
        }

        protected override bool ConnectInternal(ModuleProc PROC, System.Net.EndPoint remoteEndpoint)
        {
            _handler = new TcpClient(AddressFamily.InterNetwork);
            _handler.SendTimeout = this.SendTimeout;
            _handler.ReceiveTimeout = this.ReceiveTimeout;
            _handler.Connect(_remoteEndPoint as IPEndPoint);
            _localEndPoint = _handler.Client.LocalEndPoint;
            _socket = _handler.Client;
            this.WriteSocketLog("has been connected successfully.");
            return true;
        }

        protected override bool DisconnectInternal(ModuleProc PROC)
        {
            if (_socket != null)
            {
                try
                {
                    _socket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
                finally
                {
                    Thread.Sleep(10);
                    _socket.Close(5 * 1000);
                }

                this.WriteSocketLog("has been closed successfully.");
                _handler = null;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            if (_localEndPoint != null) return _localEndPoint.ToString();
            else if (_remoteEndPoint != null) return _remoteEndPoint.ToString();
            return base.ToString();
        }
    }
}
