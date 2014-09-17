using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.CoreLib;
using BMC.PlayerGateway.Gateway;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.PlayerGateway;

namespace BMC.ExMonitor.Server.Handlers.EPI
{
    #region Withdrawal Request

    /// <summary>
    /// /// Withdrawal Request Monitor Handler
    /// </summary>
    [MonitorHandlerMapping((int)FaultSource.ECash, (int)FaultType_ECash.WithdrawalRequest)]
    internal class MonitorHandler_EPI_Withdrawal_Request :
        MonitorHandler_EPI_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            MonMsg_G2H request = context.G2HMessage;
            Log.Info("Withdrawwal Request Started for : " + request.CardNumber);

            // delete the epi message
            CurrentEPIMsgProcessor.DeleteEPIMessage(request.InstallationNo);

            // check if the card in exists in the local dictionary
            if (!this.CheckCardInSession(request, null)) return false;

            // process the card in
            return SDTMessages.Instance.ProcessWithdrawRequest(context.G2HMessage, target as MonTgt_G2H_EFT_WithdrawalRequest);
        }
    }

    #endregion //Withdrawal Request

    #region Withdrawal Complete

    /// <summary>
    /// Withdrawal Complete Monitor Handler
    /// </summary>
    [MonitorHandlerMapping((int)FaultSource.ECash, (int)FaultType_ECash.WithdrawalComplete)]
    internal class MonitorHandler_EPI_Withdrawal_Complete :
        MonitorHandler_EPI_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            MonMsg_G2H request = context.G2HMessage;
            Log.Info("Withdrawwal Request Started for : " + request.CardNumber);

            // delete the epi message
            CurrentEPIMsgProcessor.DeleteEPIMessage(request.InstallationNo);

            // check if the card in exists in the local dictionary
            if (!this.CheckCardInSession(request, null)) return false;

            // process the card in
            return SDTMessages.Instance.ProcessWithdrawComplete(context.G2HMessage, target as MonTgt_G2H_EFT_WithdrawalComplete);
        }
    }

    #endregion //Withdrawal Complete
}
