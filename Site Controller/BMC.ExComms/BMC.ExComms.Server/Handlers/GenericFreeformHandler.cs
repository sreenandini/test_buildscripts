using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExComms.Server.Handlers
{
    internal class FreeformHandler_Generic
        : FreeformHandlerBase
    {
        protected override bool ProcessMessageInternal(ILogMethod method, FFMsg_G2H message)
        {
            if (message == null)
            {
                method.Info("Freeform message (G2H) was null.");
                return false;

            }

            this.OnModifyMessage(method, message);
            
            // convert the monitor message from freeform message
            MonMsg_G2H monMsg = MonitorEntityFactory.CreateEntity(message);
            if (monMsg == null)
            {
                method.Info("Unable to convert the monitor message from freeform message.");
                return false;
            }

            // post the monitor message into monitor processor
            if (!this.MonitorProcessor.ProcessG2HMessage(monMsg))
            {
                method.Info("Unable to post the message to monitor processor.");
                return false;
            }

            return true;
        }

        protected virtual void OnModifyMessage(ILogMethod method, FFMsg_G2H message) { }

        protected override bool ProcessMessageInternal(ILogMethod method, FFMsg_H2G message)
        {
            if (message == null)
            {
                method.Info("Freeform message (H2G) was null.");
                return false;
            }

            this.OnModifyMessage(method, message);
            return _serverInstance.PostMessageToTransceiver(message);
        }

        protected virtual void OnModifyMessage(ILogMethod method, FFMsg_H2G message) { }
    }
}
