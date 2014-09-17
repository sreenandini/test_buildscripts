using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExMonitor.Server.Transceiver;

namespace BMC.ExMonitor.Server
{
    internal partial class ExMonitorServerImpl
    {
        internal IExMonitorTransceiver _transceiver = null;

        private void InitTransceiver()
        {
            _transceiver = ExMonitorTransceiverFactory.RegisterAndGet(this.Executor, this);
        }
    }
}
