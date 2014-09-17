using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;

namespace BMC.ExMonitor.Server.Handlers.Tickets
{
    [MonitorHandlerMapping((int)FaultSource.TicketEvent, (int)FaultType_TicketEvent.TicketVoid)]
    internal class MonitorHandler_Ticket_Void
        : MonitorHandler_Ticket_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            MonTgt_G2H_Ticket_Void tgtSrc = target as MonTgt_G2H_Ticket_Void;
            int result = TicketPrintCancel(context.G2HMessage.InstallationNo, tgtSrc.Barcode);

            MonTgt_H2G_AckNack tgtDest = new MonTgt_H2G_AckNack()
            {
                Nack = (result == 0),
            };
            context.H2GTargets.Add(tgtDest);
            return true;
        }

        private int TicketPrintCancel(int installationNo, string barCode)
        {
            int returnCode = -1;

            if (ExCommsDataContext.Current.CheckInstallationDetailsForComms(installationNo))
            {
                if (ExCommsDataContext.Current.CancelTicketCompleteMC300(installationNo, barCode))
                {
                    returnCode = 0;
                }
                else
                {
                    returnCode = -2;
                }
            }
            else
            {
                returnCode = -1;
            }

            return returnCode;
        }
    }
}
