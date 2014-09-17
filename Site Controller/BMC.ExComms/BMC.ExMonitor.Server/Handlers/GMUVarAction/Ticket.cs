using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.GMUVarAction
{
    [MonitorHandlerMapping((int)FaultSource.GMUVarAction, (int)FaultType_GMUVarAction.TicketNumberRequest)]
    internal class MonitorHandler_GVA_TN_Request :
        MonitorHandler_GVA_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            int ticketNumber = Math.Max(1, ExCommsDataContext.Current.GetLastTicketNumber(context.G2HMessage.InstallationNo));
            context.H2GTargets.Add(new MonTgt_H2G_GVA_TN_Response()
            {
                TicketNumber = ticketNumber,
            });
            return true;
        }
    }

    [MonitorHandlerMapping((int)FaultSource.GMUVarAction, (int)FaultType_GMUVarAction.TicketExpirationDateRequest)]
    internal class MonitorHandler_GVA_TED_Request :
        MonitorHandler_GVA_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            short expiryDays = (short)Math.Max(1, this.GetExpireDays());
            DateTime expiryDate = DateTime.Now.AddDays(expiryDays);
            context.H2GTargets.Add(new MonTgt_H2G_GVA_TED_Response()
            {
                Date = expiryDate,
                ExipreDays = expiryDays,
            });
            return true;
        }

        private int GetExpireDays()
        {
            int? exprDays = 0;
            try
            {
                ExCommsDataContext.Current.GetTicketExpire(ref exprDays);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return exprDays.SafeValue();
        }
    }

    [MonitorHandlerMapping((int)FaultSource.GMUVarAction, (int)FaultType_GMUVarAction.TicketSystemSlotIDRequest)]
    internal class MonitorHandler_GVA_TSSlotID_Request :
        MonitorHandler_GVA_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            int slotID = context.G2HMessage.InstallationNo;
            context.H2GTargets.Add(new MonTgt_H2G_GVA_TSSlotID_Response()
            {
                SlotID = slotID,
            });
            return true;
        }
    }
}
