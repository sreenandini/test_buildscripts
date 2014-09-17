using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.DataLayer.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketingCOM;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.Common.ExceptionManagement;

namespace BMC.ExMonitor.Server.Handlers.Tickets
{
    [MonitorHandlerMapping((int)FaultSource.TicketEvent, (int)FaultType_TicketEvent.TicketRedemptionComplete)]
    internal class MonitorHandler_Ticket_Redeem_Complete
        : MonitorHandler_Ticket_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnExecuteInternal"))
            {
                MonMsg_G2H msgSrc = context.G2HMessage;
                MonTgt_G2H_Ticket_Redemption_Close tgtSrc = target as MonTgt_G2H_Ticket_Redemption_Close;
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
                    int installationNo = msgSrc.InstallationNo;
                    int? voucherID = 0;
                    long retCode = 0;
                    bool isLocalTicket = false;
                    bool sendAck = false;
                    method.InfoV("TICKET_REDEEM_CLOSE: Local Site Code : {0}, Ticket Site Code : {1}", siteCode, ticketSiteCode);

                    if (tgtSrc.Amount > 0 &&
                        tgtSrc.Status == FF_AppId_TicketRedemption_Close_Status.Success)
                    {
                        // is TIS printed ticket
                        if (ticketService.IsTISPrintedTicketPrefix(barcode))
                        {
                            method.InfoV("TICKET_REDEEM_CLOSE (TIS): TIS Printed Ticket ({0})", barcode);

                            if (ExCommsDataContext.Current.RedeemTicketComplete(barcode, stockNo, ref voucherID, siteCode, playerCardNo))
                            {
                                method.Info("TICKET_REDEEM_CLOSE (TIS): Send ticket: " + barcode + " redeem information to TIS");
                                ticketService.TisTicketRedeemComplete(barcode);
                                method.Info("TICKET_REDEEM_CLOSE (TIS): Redeem information send to TIS for ticket: " + barcode);
                            }
                            else
                            {
                                method.Info("TICKET_REDEEM_CLOSE: Problem while sending the Tkt Redeem complete msg to DB!");
                                tgtSrc.Status = FF_AppId_TicketRedemption_Close_Status.CouponRejectedbySystem;
                            }
                        }
                        // cross ticketing enabled and ticket site code not matched
                        else if (_configStore.IsCrossTicketingEnabled)//CrossTickeing From Setting
                        {
                            method.InfoV("TICKET_REDEEM_CLOSE (CROSS SITE): Ticket Printed in Site {1} ({0})", barcode, ticketSiteCode);

                            // local site
                            if (!siteCode.IgnoreCaseCompare(ticketSiteCode))
                            {
                                isLocalTicket = true;
                            }
                            else
                            {
                                //foreign site
                                if (installationNo == 0 ||
                                    string.IsNullOrEmpty(stockNo))
                                {
                                    method.Info("TICKET_REDEEM_CLOSE (CROSS SITE): Installation detail for ticket " + barcode + " in " + installationNo.ToString() + " for DeviceID " + stockNo);
                                    if (ticketService.TicketRedeemComplete(barcode, stockNo, out retCode) != 0)
                                    {
                                        method.Info("TICKET_REDEEM_CLOSE (CROSS SITE): Problem while sending the Tkt Redeem complete msg to WS!");
                                        tgtSrc.Status = FF_AppId_TicketRedemption_Close_Status.CouponRejectedbySystem;
                                    }
                                }
                                else
                                {
                                    method.Info("TICKET_REDEEM_CLOSE (CROSS SITE): Stored Installation detail for ticket " + barcode + " in " + installationNo.ToString() + " for DeviceID" + stockNo);
                                    if (ticketService.TicketRedeemComplete(barcode, stockNo, out retCode) != 0)
                                    {
                                        method.Info("TICKET_REDEEM_CLOSE (CROSS SITE): Stored Problem while sending the Tkt Redeem complete msg to WS!");
                                        tgtSrc.Status = FF_AppId_TicketRedemption_Close_Status.CouponRejectedbySystem;
                                    }
                                }
                            }
                        }
                        // local site ticket
                        else
                        {
                            isLocalTicket = true;
                        }
                        sendAck = true;
                    }

                    // local site ticket
                    if (isLocalTicket)
                    {
                        method.Info("TICKET_REDEEM_CLOSE (LOCAL): Ticket: " + barcode + " Sitecode : " + siteCode + " Inst  : " + installationNo.ToString());
                        if (!ExCommsDataContext.Current.RedeemTicketComplete(barcode, stockNo, ref voucherID, siteCode, playerCardNo))
                        {
                            method.Info("TICKET_REDEEM_CLOSE (LOCAL):Problem while sending the Tkt Redeem complete msg to DB!");
                        }
                        else
                        {
                            method.InfoV("TICKET_REDEEM_CLOSE (LOCAL): Success while redeeming amount for Barcode : {0}", barcode);
                        }
                    }
                    else
                    {
                        //else nota a valid amount or redeem status
                        method.Info("TICKET_REDEEM_CLOSE: No proper Ticket Redeemption!");
                        this.TicketReedeemFail(tgtSrc, msgSrc, ticketService, ref voucherID);
                        sendAck = true;
                    }

                    // send the acknowledgement
                    if (sendAck)
                    {
                        method.Info("TICKET_REDEEM_CLOSE: Sending redeem complete acknowledement");
                        MonTgt_H2G_Ticket_Redemption_Close response = new MonTgt_H2G_Ticket_Redemption_Close()
                        {
                            Status = (tgtSrc.Status == FF_AppId_TicketRedemption_Close_Status.Success) ? FF_AppId_ResponseStatus_Types.Success : FF_AppId_ResponseStatus_Types.Fail
                        };
                        context.H2GTargets.Add(response);
                        method.Info("TICKET_REDEEM_CLOSE: Successfully sent redeem complete acknowledement");
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return true;
            }
        }

        private void TicketReedeemFail(MonTgt_G2H_Ticket_Redemption_Close tgtSrc, MonMsg_G2H msgSrc, ICallWebService ticketService, ref int? voucherID)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "TicketReedeemFail"))
            {
                try
                {
                    long retCode = 0;
                    if (tgtSrc.Status != FF_AppId_TicketRedemption_Close_Status.CouponRejectedbySystem)
                    {
                        if (_configStore.IsCrossTicketingEnabled)
                        {
                            if ((string.Equals(HandlerHelper.Current.LocalSiteCode, msgSrc.SiteCode)) || 
                                ticketService.IsTISPrintedTicketPrefix(tgtSrc.Barcode))
                            {
                                if (!ExCommsDataContext.Current.RedeemTicketComplete(tgtSrc.Barcode, msgSrc.Asset, ref voucherID, msgSrc.SiteCode, msgSrc.CardNumber))
                                {
                                    Log.Info("TICKET_REDEEM_CLOSE_FAIL (TIS or LOCAL SITE): No proper Ticket Redeemption!!");
                                }
                            }
                            else
                            {
                                Log.Info("TICKET_REDEEM_CLOSE_FAIL (CROSS SITE): Foreign Ticket Rdm Cmpt cancel");
                                if (msgSrc.InstallationNo == 0 || 
                                    string.IsNullOrEmpty(msgSrc.Asset))
                                {
                                    Log.Info("TICKET_REDEEM_CLOSE_FAIL (CROSS SITE): Installation detail in " + msgSrc.InstallationNo.ToString() + " lID DeviceID " + msgSrc.Asset);
                                    if (ticketService.TicketRedeemCancel(tgtSrc.Barcode, msgSrc.Asset, 0, out retCode) != 0)
                                    {
                                        Log.Info("TICKET_REDEEM_CLOSE_FAIL (CROSS SITE): Problem while sending the Ticket Redeem complete msg to DB!");
                                    }
                                }
                                else
                                {
                                    Log.Info("TICKET_REDEEM_CLOSE_FAIL (CROSS SITE): Stored Installation detail in " + msgSrc.InstallationNo.ToString() + " lID DeviceID " + msgSrc.Asset);
                                    if (ticketService.TicketRedeemCancel(tgtSrc.Barcode, msgSrc.Asset, 0, out retCode) != 0)
                                    {
                                        Log.Info("TICKET_REDEEM_CLOSE_FAIL (CROSS SITE): Problem while sending the Ticket Redeem complete msg to DB!");
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!ExCommsDataContext.Current.RedeemTicketComplete(tgtSrc.Barcode, msgSrc.Asset, ref voucherID, msgSrc.SiteCode, msgSrc.CardNumber))
                            {
                                Log.Info("TICKET_REDEEM_CLOSE_FAIL (LOCAL SITE): No proper Ticket Redeemption!!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }
}
