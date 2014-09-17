using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.PlayerGateway.Gateway;

namespace BMC.ExMonitor.Server.Handlers.EPI
{
    /// <summary>
    /// Balance Request Monitor Handler
    /// </summary>
    [MonitorHandlerMapping((int)FaultSource.ECash, (int)FaultType_ECash.AFTAccountList)]
    internal class MonitorHandler_EPI_Balance_Request :
        MonitorHandler_EPI_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            MonMsg_G2H request = context.G2HMessage;
            Log.Info("Balance Request Started for : " + request.CardNumber);

            // delete the epi message
            CurrentEPIMsgProcessor.DeleteEPIMessage(request.InstallationNo);

            // check if the card in exists in the local dictionary
            if (!this.CheckCardInSession(request, null)) return false;

            // process the card in
            return SDTMessages.Instance.ProcessBalanceRequest(context.G2HMessage, target as MonTgt_G2H_EFT_BalanceRequest);
        }
    }
}
