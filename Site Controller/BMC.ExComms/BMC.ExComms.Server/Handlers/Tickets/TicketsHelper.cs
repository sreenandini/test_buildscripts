using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.Native;

namespace BMC.ExComms.Server.Handlers.Tickets
{
    internal static class TicketsHelper
    {
        private const string DYN_MODULE_NAME = "TicketsHelper";

        private static FFTgt_B2B_TicketInfo WrapTargetAndReturn(FFTgt_B2B_TicketInfoData data)
        {
            return new FFTgt_B2B_TicketInfo()
            {
                SubTargetData = data,
                IsResponseRequired = true,
                IsSecured = true,
            };
        }

        private static FFMsg_G2H WrapMessageAndReturn(string ipAddress, FFTgt_B2B_TicketInfoData data)
        {
            return WrapMessageAndReturn(ipAddress, FF_FlowInitiation.Any, data);
        }

        private static FFMsg_G2H WrapMessageAndReturn(string ipAddress, FF_FlowInitiation flowInitiation, FFTgt_B2B_TicketInfoData data)
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateG2HMessage(ipAddress, FF_AppId_G2H_Commands.ResponseRequest,
                FF_AppId_G2H_MessageTypes.FreeForm, FF_AppId_SessionIds.Tickets, true,
                WrapTargetAndReturn(data));
            msg.FlowInitiation = flowInitiation;
            return msg;
        }

        private static FFMsg_H2G WrapMessageAndReturnH2G(string ipAddress, FF_AppId_H2G_PollCodes pollCode, FFTgt_B2B_TicketInfoData data)
        {
            return WrapMessageAndReturnH2G(ipAddress, pollCode, FF_FlowInitiation.Any, data);
        }

        private static FFMsg_H2G WrapMessageAndReturnH2G(string ipAddress, FF_AppId_H2G_PollCodes pollCode, FF_FlowInitiation flowInitiation, FFTgt_B2B_TicketInfoData data)
        {
            FFMsg_H2G msg = FreeformEntityFactory.CreateH2GMessage(ipAddress, pollCode,
                FF_AppId_SessionIds.Tickets, true,
                WrapTargetAndReturn(data));
            msg.FlowInitiation = flowInitiation;
            return msg;
        }

        internal static FFMsg_G2H PrintTicket(string ipAddress, double amount, FF_AppId_TicketTypes type)
        {
            TicketIDInfo ticketID = GenerateTicket(ipAddress, amount);
            if (ticketID == null) return null;

            return WrapMessageAndReturn(ipAddress, FF_FlowInitiation.Gmu, new FFTgt_G2H_Ticket_Printed_Request()
            {
                BarCode = ticketID.Barcode,
                Amount = amount,
                Type = type,
            });
        }

        internal static FFMsg_H2G PrintTicketAck(string ipAddress, IFreeformEntity_Msg request)
        {
            return WrapMessageAndReturnH2G(ipAddress, FF_AppId_H2G_PollCodes.FreeformNoResponse, 
                FF_FlowInitiation.Host, new FFTgt_H2G_Ticket_Printed_Response()
            {
                Status = FF_AppId_ResponseStatus_Types.Success,
            });
        }

        internal static FFMsg_G2H VoidTicket(string ipAddress, string barcode)
        {
            return WrapMessageAndReturn(ipAddress, FF_FlowInitiation.Gmu, new FFTgt_G2H_Ticket_Void()
            {
                Barcode = barcode,
                Error = FF_AppId_TicketPrintStatus.Unknown,
            });
        }

        internal static FFMsg_G2H RedeemTicketRequest(string ipAddress, string barcode)
        {
            return WrapMessageAndReturn(ipAddress, FF_FlowInitiation.Gmu, new FFTgt_G2H_Ticket_Redemption_Request()
            {
                Barcode = barcode,
            });
        }

        internal static FFMsg_H2G RedeemTicketResponse(string ipAddress, string barcode, double amount, FF_AppId_TicketTypes type)
        {
            return WrapMessageAndReturnH2G(ipAddress, FF_AppId_H2G_PollCodes.FreeformResponse,
               FF_FlowInitiation.Host, new FFTgt_H2G_Ticket_Redemption_Response()
               {
                   Barcode = barcode,
                   Amount = amount,
                   Type = type,
               });
        }

        internal static FFMsg_G2H RedeemTicketComplete(string ipAddress, string barcode, double amount, FF_AppId_TicketTypes type, FF_AppId_TicketRedemption_Close_Status status)
        {
            return WrapMessageAndReturn(ipAddress, FF_FlowInitiation.Gmu, new FFTgt_G2H_Ticket_Redemption_Close()
            {
                Barcode = barcode,
                Amount = amount,
                Type = type,
                Status = status,
            });
        }

        internal static FFMsg_H2G RedeemTicketCompleteResponse(string ipAddress, FF_AppId_ResponseStatus_Types status)
        {
            return WrapMessageAndReturnH2G(ipAddress, FF_AppId_H2G_PollCodes.FreeformNoResponse,
               FF_FlowInitiation.Host, new FFTgt_H2G_Ticket_Redemption_Close()
               {
                   Status = status,
               });
        }

        private static TicketIDInfo GenerateTicket(string key, double Amount)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "GenerateTicket"))
            {
                TicketIDInfo result = default(TicketIDInfo);
                TicketGlobal tk = ModuleHelper.Current.GetTicketGlobal(key);
                if (tk == null) return null;

                try
                {
                    result = new TicketIDInfo();

                    // ticket number
                    tk.TicketNumber += 1;
                    result.UnpackedID.CopyToBufferASCII(tk.TicketNumber.GetStringToBytes(5), 9, 5);

                    // casino id
                    result.UnpackedID.CopyToBufferASCII(ModuleHelper.Current.TicketLocationCode.GetASCIIBytesValue(4), 0, 4);

                    // slot id
                    result.SlotID = tk.SlotID;
                    result.UnpackedID.CopyToBufferASCII(result.SlotID.GetStringToBytes(4), 4, 5);

                    // crc
                    uint crc = SDGTicketGenerator.GenerateCRC(result.UnpackedID, (long)Amount, tk.TicketKey);
                    result.UnpackedID.CopyToBufferASCII(crc.GetStringToBytes(3), 14, 3);

                    // check digit
                    result.UnpackedID[17] = SDGTicketGenerator.CheckDigit(result.UnpackedID, 17);

                    // Amount
                    result.Amount = Amount;
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
