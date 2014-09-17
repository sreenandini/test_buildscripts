using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace BMC.CoreLib.Net
{
    public class TcpSocketServerClient : NetSocketHandler
    {
        public TcpSocketServerClient(Socket socket)
        {
            _socket = socket;
            _remoteEndPoint = _socket.LocalEndPoint;
        }

        public override bool IsConnected
        {
            get { throw new NotImplementedException(); }
        }

        protected override bool StartInternal()
        {
            return true;
        }

        protected override bool StopInternal()
        {
            return true;
        }

        protected override bool BindInternal()
        {
            throw new NotImplementedException();
        }

        protected override bool BindMulticastIPInternal(System.Net.IPAddress multicastAddressIp, System.Net.IPAddress localAddressIp)
        {
            throw new NotImplementedException();
        }

        protected override bool ConnectInternal(BMC.CoreLib.Diagnostics.ModuleProc PROC, System.Net.EndPoint remoteEndpoint)
        {
            throw new NotImplementedException();
        }

        protected override bool DisconnectInternal(BMC.CoreLib.Diagnostics.ModuleProc PROC)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            if (_socket != null)
            {
                return _socket.RemoteEndPoint.GetHashCode();
            }
            return base.GetHashCode();
        }

        public override string ToString()
        {
            if (_socket != null)
            {
                return _socket.RemoteEndPoint.ToString();
            }
            return base.ToString();
        }
    }
}
