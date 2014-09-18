using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using BMC.Common.Utilities;
using BMC.CashDeskOperator.BusinessObjects;
using System.Globalization;
using BMC.Common.ExceptionManagement;
using BMC.Common.ConfigurationManagement;
using System.Data;
using BMC.Common.LogManagement;
using CageBMCInterface;
using BMC.Security;
using BMC.Security.Manager;

namespace CageBMCService
{
    // NOTE: If you change the class name "Ticketing" here, you must also update the reference to "Ticketing" in Web.config.
    public class Voucher : VoucherEndPoint
    {
        string CurrentCurrenyCulture = ConfigManager.Read("GetDefaultCultureForCurrency");
        IssueTicketEntity issueTicketEntity = new IssueTicketEntity();
        IIssueTicket objCashDeskOperator = IssueTicketBusinessObject.CreateInstance();
        private long Custid = 0;
        public Voucher()
        {
            SettingInitializer.Initialize(); 
        
        }

        #region VoucherEndPoint Members

        public voucherDTO createVoucher(voucherDTO ticketFormDTO)
        {
            if (!CheckCageEnabled())
                return null;

            //string strValue = request.amount; 
            long lValue = 0;
            try
            {

                //if (strValue != null && strValue != string.Empty)
                //    //lValue = Convert.ToInt64(Convert.ToSingle(strValue, new CultureInfo(CurrentCurrenyCulture)) * 100);
                lValue = ticketFormDTO.amount;
                //else
                //    lValue = 0;

                //if (lValue <= 0)
                //{
                //    MessageBox.ShowBox("MessageID101");
                //    return;
                //}
                if (!string.IsNullOrEmpty(Settings.IssueTicketMaxValue))
                {
                    long lSettingValue = Convert.ToInt64(Settings.IssueTicketMaxValue.GetSingleFromString() * 100);
                    if (lValue > lSettingValue)
                    {
                       // string sMessage = Application.Current.FindResource("MessageID247") as string;
                        //MessageBox.ShowBox(sMessage + ": " +
                        //   ExtensionMethods.GetUniversalCurrencyFormat(Convert.ToInt64(Settings.IssueTicketMaxValue.GetSingleFromString())),
                        //                                                                                            BMC_Icon.Error, true);
                        //return;
                    }
                }
                if ((lValue <= 99999999) && (lValue > 0))
                {
                    issueTicketEntity.lnglValue = lValue;
                    issueTicketEntity.Type = "1";
                    issueTicketEntity.dblValue = ticketFormDTO.amount;
                    issueTicketEntity.lnglValue = ticketFormDTO.amount * 100;
                    issueTicketEntity.Date = DateTime.Today;
                    BMC.Transport.voucherDTO oVoucherDTO = objCashDeskOperator.IssueTicketToCage(issueTicketEntity, ticketFormDTO);
                }
                else
                {
                    //MessageBox.ShowBox("MessageID104", BMC_Icon.Warning);
                    //return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ticketFormDTO;
        }

        public createBulkVoucherResponse createBulkVoucher(createBulkVoucherRequest createBulkVoucher)
        {
            if (!CheckCageEnabled())
                return null;
            this.SetUser(createBulkVoucher.createBulkVoucher.arg0.employeeId);
            createBulkVoucherResponse oResponse = new createBulkVoucherResponse();
            voucherDTO ticketFormDTO = createBulkVoucher.createBulkVoucher.arg0;
            int noOfTickets = createBulkVoucher.createBulkVoucher.arg1;
            long lValue = 0;

            try
            {
                lValue = ticketFormDTO.amount;
                oResponse.createBulkVoucherResponse1 = new voucherDTO[noOfTickets];
                for (int i = 0; i < noOfTickets; i++)
                {
                    issueTicketEntity.lnglValue = lValue;
                    issueTicketEntity.Type = "1";
                    issueTicketEntity.lnglValue = ticketFormDTO.amount;
                    issueTicketEntity.dblValue = ticketFormDTO.amount;
                    issueTicketEntity.Date = DateTime.Today;
                    BMC.Transport.voucherDTO oVoucherDTCopy = ticketFormDTO.Clone();
                    if (!string.IsNullOrEmpty(Settings.IssueTicketMaxValue))
                    {
                        long lSettingValue = Convert.ToInt64(Settings.IssueTicketMaxValue.GetSingleFromString() * 100);
                        if (lValue > lSettingValue || (lValue < 0))
                        {
                            oVoucherDTCopy.errorCodeId = -1001;
                        }
                        else
                        {
                            oVoucherDTCopy = objCashDeskOperator.IssueTicketToCage(issueTicketEntity, oVoucherDTCopy);
                        }
                    }
                    else
                    {
                        oVoucherDTCopy = objCashDeskOperator.IssueTicketToCage(issueTicketEntity, oVoucherDTCopy);
                    }
                    oResponse.createBulkVoucherResponse1[i] = oVoucherDTCopy;

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return oResponse;
        }

        private void SetUser(string User)
        {
            UserManager userObect = new UserManager(oCommonUtilities.CreateInstance().GetConnectionString());
            SecurityHelper.CurrentUser = userObect.GetUserObject(User, User, User);
            SecurityHelper.CurrentUser.UserName = User;

        }

        public searchVouchersResponse searchVouchers(searchVouchersRequest Requset)
        {
            if (!CheckCageEnabled())
                return null;

            searchVouchersResponse oResponse = new searchVouchersResponse();
            try
            {
                oResponse.searchVouchersResponse1 = objCashDeskOperator.searchVouchers(Requset.searchVouchers.arg0, Requset.searchVouchers.arg1, Requset.searchVouchers.arg2, Requset.searchVouchers.arg3);
                oResponse.searchVouchersResponse1[0].allowOverride = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return oResponse;
        }

        public redeemRequestVoucherResponse1 redeemRequestVoucher(redeemRequestVoucherRequest Request)
        {
            if (!CheckCageEnabled())
                return null;
            redeemRequestVoucherResponse1 Response = new redeemRequestVoucherResponse1();
            Response.redeemRequestVoucherResponse = new redeemRequestVoucherResponse();
            try
            {
                Response.redeemRequestVoucherResponse.@return = objCashDeskOperator.redeemRequestVoucher(Request.redeemRequestVoucher.arg0);
                Response.redeemRequestVoucherResponse.@return.allowOverride = false;
                if (Response.redeemRequestVoucherResponse.@return.tktStatus == "Cancelled")
                {
                    Response.redeemRequestVoucherResponse.@return.tktStatusId = -1031; 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return Response;
        }   

        public redeemVoucherResponse1 redeemVoucher(redeemVoucherRequest request)
        {
            if (!CheckCageEnabled())
                return null;
            this.SetUser(request.redeemVoucher.arg0.employeeId);
            bool isValid = false;
            RTOnlineTicketDetail objTicketDetail = null;
            double redeemTicketAmount = 0;
            redeemVoucherResponse1 oReponse = new redeemVoucherResponse1();
            oReponse.redeemVoucherResponse = new redeemVoucherResponse();
            oReponse.redeemVoucherResponse.@return = request.redeemVoucher.arg0;
            try
            {
                IRedeemOnlineTicket objCashDeskOper = RedeemOnlineTicketBusinessObject.CreateInstance();
                objTicketDetail = new RTOnlineTicketDetail();
                objTicketDetail.TicketString = request.redeemVoucher.arg0.barcode;
                //int ticketStatus = 0;
                objTicketDetail = objCashDeskOper.GetRedeemTicketAmountCage(objTicketDetail);
                
                redeemTicketAmount = double.Parse ( objTicketDetail.RedeemedAmount.ToString()) / 100;
                objTicketDetail.CustomerId = Custid;

                int iResult = objCashDeskOper.CheckSDGTicketCage(objTicketDetail.TicketString);
                LogManager.WriteLog("redeemVoucher->CheckSDGTicketCage Return Value :" + iResult.ToString() , LogManager.enumLogLevel.Info);
                oReponse.redeemVoucherResponse.@return = objCashDeskOperator.redeemRequestVoucher(request.redeemVoucher.arg0);
                if (iResult == 0)
                {

                    isValid = objCashDeskOper.RedeemOnlineTicketCage(objTicketDetail);
                    if (isValid)
                    {
                        oReponse.redeemVoucherResponse.@return.tktStatus = "REDEEMED";
                        oReponse.redeemVoucherResponse.@return.tktStatusId = 3;
                        oReponse.redeemVoucherResponse.@return.errorCodeId = 0;
                    }
                    else
                    {
                        oReponse.redeemVoucherResponse.@return.tktStatus = objTicketDetail.TicketStatus;
                        oReponse.redeemVoucherResponse.@return.tktStatusId = -98;
                        oReponse.redeemVoucherResponse.@return.errorCodeId = -98;
                    }
                    
                }
                else
                {
                    if (iResult == -1)
                    {
                        oReponse.redeemVoucherResponse.@return.tktStatus = "VOUCHER_ALREADY_REDEEEMED";
                        oReponse.redeemVoucherResponse.@return.tktStatusId = -1001;
                        oReponse.redeemVoucherResponse.@return.errorCodeId = -1001;
                    }
                    else if (iResult == -2)
                    {
                        oReponse.redeemVoucherResponse.@return.tktStatus = "VOUCHER_BARCODE_NOT_EXIST";
                        oReponse.redeemVoucherResponse.@return.tktStatusId = -3;
                        oReponse.redeemVoucherResponse.@return.errorCodeId = -3;
                    }
                    else if (iResult == -3)
                    {
                        if (Settings.RedeemExpiredTicket)
                        {
                            objTicketDetail = new RTOnlineTicketDetail();
                            objTicketDetail.TicketString = request.redeemVoucher.arg0.barcode;
                            objTicketDetail.CustomerId = Custid;
                           
                            isValid = objCashDeskOper.RedeemOnlineTicketCage(objTicketDetail);
                            LogManager.WriteLog("Redeem Expired Voucher response:" + objTicketDetail.ValidTicket.ToString() + " " + ((objTicketDetail.TicketStatus!= null)?objTicketDetail.TicketStatus:string.Empty), LogManager.enumLogLevel.Info);
                            if (objTicketDetail.ValidTicket == true)
                            {
                                oReponse.redeemVoucherResponse.@return.tktStatus = "REDEEMED";
                                oReponse.redeemVoucherResponse.@return.tktStatusId = 3;
                                oReponse.redeemVoucherResponse.@return.errorCodeId = 0;
                            }
                            else
                            {
                                
                                oReponse.redeemVoucherResponse.@return.tktStatus = "GENERAL_VOUCHER_ERROR";
                                oReponse.redeemVoucherResponse.@return.tktStatusId = -2;
                                oReponse.redeemVoucherResponse.@return.errorCodeId = -2;
                            }
                        
                        }
                        else
                        {
                            oReponse.redeemVoucherResponse.@return.tktStatus = "VOUCHER_EXPIRED";
                            oReponse.redeemVoucherResponse.@return.tktStatusId = -1005;
                            oReponse.redeemVoucherResponse.@return.errorCodeId = -1005;
                        }

                    }
                    else if (iResult == -12)
                    {
                        oReponse.redeemVoucherResponse.@return.tktStatus = "REDEEM_REQ_ALREADY_VOIDED";
                        oReponse.redeemVoucherResponse.@return.tktStatusId = -1031;
                        oReponse.redeemVoucherResponse.@return.errorCodeId = -1031;
                    }
                    else if (iResult == -99)
                    {
                        oReponse.redeemVoucherResponse.@return.tktStatus = "SITE CODE MISMATCH";
                        oReponse.redeemVoucherResponse.@return.tktStatusId = -99;
                        oReponse.redeemVoucherResponse.@return.errorCodeId = -99;
                    }
                    else
                    {
                        oReponse.redeemVoucherResponse.@return.tktStatus = "GENERAL_VOUCHER_ERROR";
                        oReponse.redeemVoucherResponse.@return.tktStatusId = -1;
                        oReponse.redeemVoucherResponse.@return.errorCodeId = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);
                oReponse.redeemVoucherResponse.@return.tktStatusId = -1;
                oReponse.redeemVoucherResponse.@return.errorCodeId = -1;
            }
            return oReponse;
        }

        bool CheckCageEnabled()
        {
            if (!Settings.CAGE_ENABLED)
            {
                LogManager.WriteLog("Voucher Service: Cage Not Enabled",LogManager.enumLogLevel.Info);
                
            }
            return Settings.CAGE_ENABLED;
        }
        #endregion

        #region "Not Implemented"

        public cancelRedeemVoucherResponse1 cancelRedeemVoucher(cancelRedeemVoucherRequest request)
        {
            cancelRedeemVoucherResponse1 oResponse = new cancelRedeemVoucherResponse1();
            try
            {
                oResponse.cancelRedeemVoucherResponse = new cancelRedeemVoucherResponse();
                oResponse.cancelRedeemVoucherResponse.@return = objCashDeskOperator.redeemRequestVoucher(request.cancelRedeemVoucher.arg0);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }//objCashDeskOperator.CancelTicketCage(request.cancelRedeemVoucher.arg0.barcode);
            return oResponse;
        }

        public createVoucherResponse1 createVoucher(createVoucherRequest request)
        {
            throw new NotImplementedException();
        }

        public getTransactionReasonsForCashDeskResponse getTransactionReasonsForCashDesk(getTransactionReasonsForCashDeskRequest request)
        {

            getTransactionReasonsForCashDeskResponse o = new getTransactionReasonsForCashDeskResponse();
            try
            {
                o.getTransactionReasonsForCashDeskResponse1 = new transactionReasonInfoDTO[1];
                o.getTransactionReasonsForCashDeskResponse1[0] = new transactionReasonInfoDTO() { description = "SUCCESS", errorPresent = false, id = 12, idSpecified = false };

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            } 
            return o;
        }

        public getVoucherParametersResponse1 getVoucherParameters(getVoucherParametersRequest request)
        {
            return new getVoucherParametersResponse1()
            {
                getVoucherParametersResponse = new getVoucherParametersResponse() { @return = new voucherParameterDTO() { allowCashierLocOnTkts = Settings.CAGE_ALLOWCASHIERLOCONTKTS, allowPrintTktOverride = Settings.CAGE_ALLOWPRINTTKTOVERRIDE, errorCodeId = 0, errorPresent = false, tktPrinterEnabled = Settings.CAGE_TKTPRINTERENABLED, minTktPrintAmtForEmp = Settings.CAGE_MINTKTPRINTAMTFOREMP, maxTktRedemptionAmtForEmp = Settings.CAGE_MAXTKTREDEMPTIONAMTFOREMP, maxTktRedemptionAmt = Settings.CAGE_MAXTKTREDEMPTIONAMT, maxNoOfTktPrintLimit = Settings.CAGE_MAXNOOFTKTPRINTLIMIT, maxDailyCashierGenTktAmt = Settings.CAGE_MAXDAILYCASHIERGENTKTAMT, maxTktPrintAmtForEmp = Convert.ToInt64(Settings.IssueTicketMaxValue.GetSingleFromString() * 100) } }
            };
        }

        public getVoucherTransactionHistoryResponse getVoucherTransactionHistory(getVoucherTransactionHistoryRequest request)
        {
            return null;
        }

        public getVouchersResponse getVouchers(getVouchersRequest request)
        {
            throw new NotImplementedException();
        }

        public inquireVoucherResponse1 inquireVoucher(inquireVoucherRequest request)
        {
            throw new NotImplementedException();
        }

        public overrideRedeemVoucherResponse1 overrideRedeemVoucher(overrideRedeemVoucherRequest request)
        {
            throw new NotImplementedException();
        }

        public redeemVoucherWithoutStatusCheckResponse1 redeemVoucherWithoutStatusCheck(redeemVoucherWithoutStatusCheckRequest request)
        {
            throw new NotImplementedException();
        }

        public voidBulkVoucherResponse voidBulkVoucher(voidBulkVoucherRequest request)
        {
            voidBulkVoucherResponse oResponse = new voidBulkVoucherResponse();
            oResponse.voidBulkVoucherResponse1 = new voucherDTO[request.voidBulkVoucher.Length];
            try
            {
                for (int iCount = 0; iCount < request.voidBulkVoucher.Length; iCount++)
                {
                    cancelRedeemVoucherRequest oCancelVoucher = new cancelRedeemVoucherRequest() { cancelRedeemVoucher = new cancelRedeemVoucher() { arg0 = request.voidBulkVoucher[iCount] } };
                    oResponse.voidBulkVoucherResponse1[iCount] = this.VoidVoucher(oCancelVoucher).cancelRedeemVoucherResponse.@return;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return oResponse;
        }
        private cancelRedeemVoucherResponse1 VoidVoucher(cancelRedeemVoucherRequest request)
        {
            string strCancelStatus;
            cancelRedeemVoucherResponse1 oResponse = new cancelRedeemVoucherResponse1();
            oResponse.cancelRedeemVoucherResponse = new cancelRedeemVoucherResponse();
            oResponse.cancelRedeemVoucherResponse.@return = objCashDeskOperator.redeemRequestVoucher(request.cancelRedeemVoucher.arg0);
            strCancelStatus=objCashDeskOperator.CancelTicketCage(request.cancelRedeemVoucher.arg0.barcode);
            if (strCancelStatus == "-1")
            {
                oResponse.cancelRedeemVoucherResponse.@return.errorCodeId = -1022;
                oResponse.cancelRedeemVoucherResponse.@return.tktStatusId = -1022;
                oResponse.cancelRedeemVoucherResponse.@return.tktStatus = "UNVOIDABLE";
            }
            return oResponse;
        }
        public voidVoucherResponse1 voidVoucher(voidVoucherRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
