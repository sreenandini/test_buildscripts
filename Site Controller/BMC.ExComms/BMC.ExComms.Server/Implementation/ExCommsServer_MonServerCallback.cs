using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Interfaces;
using BMC.ExComms.Server.Executor;

namespace BMC.ExComms.Server
{
    internal partial class ExCommsServerImpl
        : IExMonServer4CommsServerCallback
    {
        bool IExMonServer4CommsServerCallback.ProcessH2GMessage(MonMsg_H2G request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessH2GMessage"))
            {
                bool result = default(bool);

                try
                {
                    if (request == null)
                    {
                        method.Info("Invalid method received from monitor server");
                        return false;
                    }

                    FFMsg_H2G h2gMessage = MonitorEntityFactory.CreateEntity(request);
                    if (h2gMessage == null)
                    {
                        method.Info("Unable to convert the freeform message from monitor message");
                        return false;
                    }

                    method.Info("Processing H2G Message for : " + h2gMessage.IpAddress);
                    ExCommsExecutorFactory.ProcessMessage(h2gMessage);
                    result = true;
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
