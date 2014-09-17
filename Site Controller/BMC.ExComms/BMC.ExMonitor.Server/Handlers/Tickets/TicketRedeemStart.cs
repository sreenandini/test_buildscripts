using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.ExComms.Contracts.Configuration;
using TicketingCOM;
using BMC.ExComms.Contracts.DTO.Freeform;


namespace BMC.ExMonitor.Server.Handlers.Tickets
{
    [MonitorHandlerMapping((int)FaultSource.TicketEvent, (int)FaultType_TicketEvent.TicketRedemptionRequest)]
    internal class MonitorHandler_Ticket_Redeem_Start : MonitorHandler_Ticket_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnExecuteInternal"))
            {
                MonMsg_G2H msgSrc = context.G2HMessage;
                MonTgt_G2H_Ticket_Redemption_Request tgtSrc = target as MonTgt_G2H_Ticket_Redemption_Request;
                ICallWebService ticketService = CallWebServiceFactory.Current;

                try
                {
                    long ticketAmount = 0;
                    long ticketType = 0;
                    string barcode = tgtSrc.Barcode;
                    string stockNo = msgSrc.Asset;
                    string playerCardNo = msgSrc.CardNumber;
                    string siteCode = msgSrc.SiteCode;
                    string ticketSiteCode = barcode.Substring(0, 4);
                    method.InfoV("TICKET_REDEEM_START: Local Site Code : {0}, Ticket Site Code : {1}", siteCode, ticketSiteCode);

                    // is TIS printed ticket
                    if (ticketService.IsTISPrintedTicketPrefix(barcode))
                    {
                        method.InfoV("TICKET_REDEEM_START (TIS): TIS Printed Ticket ({0})", barcode);
                        short tisTicketType = 0;
                        short tisRetCode = 0;
                        
                        if (ticketService.TisTicketRedeemStart(barcode, stockNo, playerCardNo, out ticketAmount, out tisTicketType, out tisRetCode) != 0)
                        {
                            /*Rejecting the Ticket*/
                            method.InfoV("TICKET_REDEEM_START (TIS): Error while redeeming amount for Barcode : {0}", barcode);
                            ticketAmount = 0;
                            ticketType = 0;
                        }
                        else
                        {
                            method.InfoV("TICKET_REDEEM_START (TIS): Success while redeeming amount for Barcode : {0}", barcode);
                            ticketType = tisTicketType;
                        }
                    }
                    // cross ticketing enabled and ticket site code not matched
                    else if (_configStore.IsCrossTicketingEnabled &&
                        !siteCode.IgnoreCaseCompare(ticketSiteCode))//CrossTickeing From Setting
                    {
                        method.InfoV("TICKET_REDEEM_START (CROSS SITE): Ticket Printed in Site {1} ({0})", barcode, ticketSiteCode);
                        long siteTicketType = 0;
                        long siteRetCode = 0;

                        if (ticketService.TicketRedeemStart(barcode, stockNo, out ticketAmount, out siteTicketType, out siteRetCode) != 0)
                        {
                            /*Rejecting the Ticket*/
                            method.InfoV("TICKET_REDEEM_START (CROSS SITE): Error while redeeming amount for Barcode : {0}", barcode);
                            ticketAmount = 0;
                            ticketType = 0;
                        }
                        else
                        {
                            method.InfoV("TICKET_REDEEM_START (TIS): Success while redeeming amount for Barcode : {0}", barcode);
                            ticketType = siteTicketType;
                        }
                    }
                    // ticket printed in local site
                    else
                    {
                        int? localAmount = 0;
                        int? localRetCode = 0;
                        int? localTicketType = 0;

                        string barCodeTemp = barcode;
                        if (!ExCommsDataContext.Current.RedeemTicketStart(ref barCodeTemp, stockNo, siteCode, 0, playerCardNo,
                            ref localAmount, ref localRetCode, ref localTicketType))
                        {
                            /*Rejecting the Ticket*/
                            method.InfoV("TICKET_REDEEM_START (LOCAL SITE): Error while redeeming amount for Barcode : {0}, Error Code : {1:D}", barcode, localRetCode.SafeValue());
                            ticketAmount = 0;
                            ticketType = 0;
                        }
                        else
                        {
                            method.InfoV("TICKET_REDEEM_START (LOCAL): Success while redeeming amount for Barcode : {0}", barcode);
                            ticketAmount = localAmount.SafeValue();
                            ticketType = localTicketType.SafeValue();
                        }
                    }

                    MonTgt_H2G_Ticket_Redemption_Response response = new MonTgt_H2G_Ticket_Redemption_Response()
                    {
                        Amount = ticketAmount,
                        Barcode = barcode,
                        Type = (FF_AppId_TicketTypes)ticketType
                    };
                    context.H2GTargets.Add(response);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return true;
            }
        }
    }
}
