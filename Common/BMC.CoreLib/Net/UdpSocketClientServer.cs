// -----------------------------------------------------------------------
// <copyright file="UdpSocketServer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net.Sockets;
    using System.Net;
    using BMC.CoreLib.Diagnostics;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UdpSocketClientServer : NetSocketHandler
    {
        private UdpClient _handler = null;

        public UdpSocketClientServer(int portNo)
            : base()
        {
            _localEndPoint = new IPEndPoint(IPAddress.Any, portNo);
        }
        public UdpSocketClientServer(string ipAddress, int portNo)
            : base()
        {
            IPAddress addr = IPAddress.Any;
            if (!ipAddress.IsEmpty() &&
                !IPAddress.TryParse(ipAddress, out addr))
            {
                addr = IPAddress.Any;
            }
            _localEndPoint = new IPEndPoint(addr, portNo);
        }

        public override bool IsConnected
        {
            get { return true; }
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
            if (_handler == null)
            {
                _handler = new UdpClient(_localEndPoint as IPEndPoint);
                _socket = _handler.Client;
            }
            return _handler != null;
        }

        protected override bool BindMulticastIPInternal(System.Net.IPAddress multicastAddressIp, System.Net.IPAddress localAddressIp)
        {
            bool result = this.BindInternal();
            if (result && _handler != null)
            {
                _handler.JoinMulticastGroup(multicastAddressIp, localAddressIp);
            }
            return true;
        }

        protected override bool ConnectInternal(Diagnostics.ModuleProc PROC, System.Net.EndPoint remoteEndpoint)
        {
            return false;
        }

        protected override bool DisconnectInternal(Diagnostics.ModuleProc PROC)
        {
            return false;
        }

        protected override int WriteInternal(Diagnostics.ModuleProc PROC, byte[] buffer, int offset, int size)
        {
            return 0;
        }

        protected override int ReadInternal(Diagnostics.ModuleProc PROC, byte[] buffer, int offset, int size)
        {
            EndPoint remoteEndPoint = null;
            return this.ReadFromInternal(PROC, buffer, offset, size, ref remoteEndPoint);
        }
    }

    public class UdpSocketClientClient : NetSocketHandler
    {
        private UdpClient _handler = null;

        public UdpSocketClientClient()
            : base() { }

        public override bool IsConnected
        {
            get { return true; }
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
            if (_handler == null)
            {
                _handler = new UdpClient();
                _socket = _handler.Client;
            }
            return _handler != null;
        }

        protected override bool BindMulticastIPInternal(System.Net.IPAddress multicastAddressIp, System.Net.IPAddress localAddressIp)
        {
            return false;
        }

        protected override bool ConnectInternal(Diagnostics.ModuleProc PROC, System.Net.EndPoint remoteEndpoint)
        {
            return false;
        }

        protected override bool DisconnectInternal(Diagnostics.ModuleProc PROC)
        {
            return false;
        }

        protected override int WriteInternal(Diagnostics.ModuleProc PROC, byte[] buffer, int offset, int size)
        {
            return 0;
        }

        protected override int ReadInternal(Diagnostics.ModuleProc PROC, byte[] buffer, int offset, int size)
        {
            return 0;
        }
    }
}
