using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.PlayerGateway.SDT;
using BMC.PlayerGateway.Gateway;

namespace BMC.ExMonitor.Server
{
    internal partial class ExMonitorServerImpl
    {
        IPlayerGateway _playerGatewayInstance = null;

        private void InitPlayerGateway()
        {
            if (_playerGatewayInstance == null)
            {
                lock (_playerGatewayInstance)
                {
                    if (_playerGatewayInstance == null)
                    {
                        _playerGatewayInstance = new SDTGateway();
                    }
                }
            }
        }

    }
}
