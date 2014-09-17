using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Contracts.Interfaces;
using BMC.ExComms.Contracts.Proxies;
using BMC.ExMonitor.Server.Handlers;

namespace BMC.ExMonitor.Server
{
    internal partial class ExMonitorServerImpl
        : IExMonServer4MonProcessorCallback
    {
        bool IExMonServer4MonProcessorCallback.ProcessG2HMessage(MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessG2HMessage"))
            {
                bool result = default(bool);

                try
                {
                    result = MonitorHandlerFactory.Current.Execute(new MonitorExecutionContext(request));
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
    }
}
