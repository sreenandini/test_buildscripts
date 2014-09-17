using BMC.ExComms.Contracts.DTO.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.CoreLib;

namespace BMC.ExMonitor.Server.Handlers.Tickets
{
    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.NOVOTicketOut)]
    internal class MonitorHandler_Ticket_NOVOTicketOut 
        : MonitorHandler_Ticket_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnExecuteInternal"))
            {
                bool result = default(bool);
                MonMsg_G2H request = context.G2HMessage;
                MonTgt_G2H_Status_NovoTicketCreate tgtSrc=target as MonTgt_G2H_Status_NovoTicketCreate;

                try
                {
                    InstallationDetailsForMSMQ dbData = request.Extra as InstallationDetailsForMSMQ;
                    int installationNo = request.InstallationNo;

                    method.InfoV("Creating novo ticket for {0:D} Ticket value {1:D} Ticket Number {2:D}", installationNo, tgtSrc.TicketAmount, tgtSrc.TicketNumber);
                    result = ExCommsDataContext.Current.InsertTicket(installationNo, dbData.Machine_No,
                        installationNo, tgtSrc.TicketAmount, tgtSrc.TicketNumber, tgtSrc.PrintDate);
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
