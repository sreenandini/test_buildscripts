using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Interfaces;
using BMC.ExComms.Contracts.Proxies;

namespace BMC.ExComms.Server
{
    public interface IExCommsMonitorProcessor : IDisposable
    {
        bool ProcessG2HMessage(MonMsg_G2H request);
    }

    public class ExCommsMonitorProcessor :
        DisposableObject,
        IExCommsMonitorProcessor
    {
        private readonly ExMonServer4CommsServerProxy exMonProxy = null;

        public ExCommsMonitorProcessor(IExMonServer4CommsServerCallback callback)
        {
            exMonProxy = ExMonServer4CommsServerProxyFactory.Get(callback);
        }

        public bool ProcessG2HMessage(MonMsg_G2H request)
        {
            try
            {
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
