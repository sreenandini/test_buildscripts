using BMC.Common.ExceptionManagement;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExMonitor.Server.Handlers.Tickets
{
    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.HandPaidJackpot)]
    internal class MonitorHandler_Ticket_HandPaidJackpot 
        : MonitorHandler_Ticket_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }

    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.JackpotReset)]
    internal class MonitorHandler_Ticket_21_19 : MonitorHandler_Ticket_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnExecuteInternal"))
            {
                bool result = default(bool);
                MonMsg_G2H request = context.G2HMessage;

                try
                {
                    DateTime faultDate = request.FaultDate;
                    ExCommsDataContext.Current.CreateTickeException_Handpay(request.InstallationNo, 0, request.BarPositionNo, true, "", faultDate);
                    ExCommsDataContext.Current.UpdateFloorFinancialSession(request.InstallationNo, "HP", "");
                    ExCommsDataContext.Current.UpdateFloorFinancialSession(request.InstallationNo, "INIT", "");
                    this.ForceMeterRead(context, target);
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
