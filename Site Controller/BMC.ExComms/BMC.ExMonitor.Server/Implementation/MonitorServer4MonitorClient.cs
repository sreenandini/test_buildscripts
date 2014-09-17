using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExMonitor.Server
{
    internal partial class ExMonitorServerImpl
        : IExMonServer4MonClient
    {
        void IExMonServer4MonClient.Subscribe(ExComms.Contracts.DTO.Monitor.ExMonServer4MonClientCallbackTypes callbackType, ExComms.Contracts.DTO.SubscribeRequestEntity request)
        {
            throw new NotImplementedException();
        }

        void IExMonServer4MonClient.Unsubscribe(ExComms.Contracts.DTO.Monitor.ExMonServer4MonClientCallbackTypes callbackType, ExComms.Contracts.DTO.UnsubscribeRequestEntity request)
        {
            throw new NotImplementedException();
        }

        bool IExMonServer4MonClient.ProcessG2HMessage(ExComms.Contracts.DTO.Monitor.MonMsg_G2H request)
        {
            throw new NotImplementedException();
        }

        bool IExMonServer4MonClient.ProcessH2GMessage(ExComms.Contracts.DTO.Monitor.MonMsg_H2G request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessH2GMessage"))
            {
                bool result = default(bool);

                try
                {
                    result = (this as IExMonServer4CommsServer2).ProcessH2GMessage(request);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        ExComms.Contracts.DTO.Monitor.MonMsg_G2H IExMonServer4MonClient.ProcessH2GMessageSync(ExComms.Contracts.DTO.Monitor.MonMsg_H2G request)
        {
            throw new NotImplementedException();
        }
    }
}
