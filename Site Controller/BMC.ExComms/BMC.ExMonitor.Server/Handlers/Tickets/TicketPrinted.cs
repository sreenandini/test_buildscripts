using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExMonitor.Server.Handlers.Tickets
{
    [MonitorHandlerMapping((int)FaultSource.TicketEvent, (int)FaultType_TicketEvent.TicketPrinted)]
    internal class MonitorHandler_Ticket_Printed_Request
        : MonitorHandler_Ticket_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            MonTgt_G2H_Ticket_Printed_Request tgtSrc = target as MonTgt_G2H_Ticket_Printed_Request;
            bool result = ExCommsDataContext.Current.CreateTicketCompleteMC300(
                                                        context.G2HMessage.InstallationNo,
                                                        tgtSrc.BarCode,
                                                        (int)tgtSrc.Amount,
                                                        context.G2HMessage.FaultDate,
                                                        tgtSrc.SequenceNo,
                                                        tgtSrc.Type.GetGmuIdInt8(),
                                                        DateTime.Now);
            context.H2GTargets.Add(new MonTgt_H2G_Ticket_Printed_Response()
            {
                Status = (result ? FF_AppId_ResponseStatus_Types.Success : FF_AppId_ResponseStatus_Types.Fail),
            });
            return true;
        }
    }

    [MonitorHandlerMapping((int)FaultSource.PriorityEvent, (int)FaultType_PriorityEvent.TicketPrinted)]
    internal class MonitorHandler_Ticket_22_9
        : MonitorHandlerBase_Meter
    {
        //protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        //{
        //    using (ILogMethod method = Log.LogMethod("MonitorHandler_Ticket_22_9", "ProcessG2HMessageInternal"))
        //    {
        //        try
        //        {
        //            EPIMeterValueDictionary ePIMeterValueDictionary = ForceMeterReadAndGetLatest(request);
        //            ExCommsDataContext.Current.UpdateTicketExceptionResponseMeters(request.InstallationNo,
        //                (int)ePIMeterValueDictionary.GetMeterValue(EPIMeterTypes.VouchersOut),
        //            (int)ePIMeterValueDictionary.GetMeterValue(EPIMeterTypes.Handpay),
        //            (int)ePIMeterValueDictionary.GetMeterValue(EPIMeterTypes.Jackpot));

        //            ExCommsDataContext.Current.UpdateFloorFinancialSession(request.InstallationNo, "OUT", "");
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            method.Exception(ex);
        //        }
        //        return false;
        //    }
        //}
    }

    //[MonitorHandlerMapping((int)FaultSource.TicketEvent, (int)FaultType_TicketEvent.TicketPrintComplete)]
    //internal class MonitorHandler_Ticket_101_5
    //    : MonitorHandler_Ticket_Base
    //{
    //    protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
    //    {
    //        using (ILogMethod method = Log.LogMethod("MonitorHandler_Ticket_101_5", "ProcessG2HMessageInternal"))
    //        {
    //            try
    //            {
    //                MonTgt_G2H_TicketPrinted_Request printRequest = request.Targets[0] as MonTgt_G2H_TicketPrinted_Request;
    //                if (printRequest == null) return false;

    //                InstallationDetailsForCommsResult instDetails = ExCommsDataContext.Current.GetInstallationDetailsForComms(request.InstallationNo);
    //                if (instDetails == null) return false;

    //                //ExCommsDataContext.Current.esp_CreateTicketComplete(printRequest.BarCode, printRequest.
    //            }
    //            catch (Exception ex)
    //            {
    //                method.Exception(ex);
    //            }
    //            return false;
    //        }
    //    }
    //}
}
