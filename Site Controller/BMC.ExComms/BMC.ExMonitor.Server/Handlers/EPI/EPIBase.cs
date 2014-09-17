using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.CoreLib;

namespace BMC.ExMonitor.Server.Handlers.EPI
{
    internal class MonitorHandler_EPI_Base
        : MonitorHandlerBase_Meter
    {
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            return false;
        }

        protected bool CheckCardInSession(MonMsg_G2H request, Action<MonMsg_G2H> doWork)
        {
            // check if the card in exists in the local dictionary
            if (!CurrentEPIManager.EPIProcessExists(request.InstallationNo))
            {
                // no card in, ok check in the database
                if (!CurrentDataContext.IsCardSessionExists(request.InstallationNo, request.CardNumber.ConvertToInt32()))
                {
                    // still no card in, so exit now
                    if (doWork != null) doWork(request);
                    Log.Info("No card in .. Exiting");
                    return false;
                }
            }
            return true;
        }
    }
}
