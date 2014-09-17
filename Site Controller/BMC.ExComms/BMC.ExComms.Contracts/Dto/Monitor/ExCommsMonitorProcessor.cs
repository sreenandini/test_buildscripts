using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using BMC.Common.ExceptionManagement;
using BMC.ExComms.Contracts.Interfaces;
using BMC.ExComms.Contracts.Proxies;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    public interface IExCommsMonitorProcessor
    {
        bool ProcessG2HMessage(MonMsg_G2H request);
    }

    public class ExCommsMonitorProcessor : IExCommsMonitorProcessor
    {
        public ExCommsMonitorProcessor()
        {
        }

        public bool ProcessG2HMessage(MonMsg_G2H request)
        {
            try
            {
                ExMonServer4CommsServerProxy exMonProxy = ExMonServer4CommsServerProxyFactory.Get();
                return exMonProxy.ProcessG2HMessage(request);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
    }
}
