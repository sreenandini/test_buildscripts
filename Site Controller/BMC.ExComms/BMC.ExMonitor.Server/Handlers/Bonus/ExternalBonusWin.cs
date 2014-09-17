using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.Bonus
{
    internal abstract class MonitorHandler_ExternalBonusWin_Base
        : MonitorHandlerBase_Meter
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnExecuteInternal"))
            {
                MonMsg_G2H request = context.G2HMessage;
                MonTgt_G2H_Status_DescriptionBase tgtSrc = target as MonTgt_G2H_Status_DescriptionBase;

                try
                {
                    DateTime faultDate = request.FaultDate;

                    // add the fault event
                    this.AddFaultEvent(context, target, tgtSrc.Description, false);

                    // create the ticket exception
                    this.OnCreateTicketException(request, target);

                    // add the meters
                    this.ForceMeterRead(context, target);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }

                return true;
            }
        }

        protected virtual void OnCreateTicketException(MonMsg_G2H request, MonitorEntity_MsgTgt target) { }
    }

    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.MachinePaidExternalBonusWin)]
    internal class MonitorHandler_MachinePaidExternalBonusWin
        : MonitorHandler_ExternalBonusWin_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }

    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.AttendantPaidExternalBonusWin)]
    internal class MonitorHandler_AttendantPaidExternalBonusWin
        : MonitorHandler_ExternalBonusWin_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }

        protected override void OnCreateTicketException(MonMsg_G2H request, MonitorEntity_MsgTgt target)
        {
            InstallationDetailsForMSMQ dbData = request.Extra as InstallationDetailsForMSMQ;
            ExCommsDataContext.Current.CreateTickeException_Handpay(request.InstallationNo, 0, dbData.Bar_Pos_Name, true, "MYSTERY", request.FaultDate);
        }
    }
}
