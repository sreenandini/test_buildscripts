using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport;
using BMC.Transport.CashDeskOperatorEntity;
using System.Collections.Generic;
using BMC.Common.Utilities;
using System.Windows;
using TicketingClient.TicketingServiceReference;
using System.ServiceModel;
using BMC.Common.LogManagement;
using BMC.PlayerGateway.Gateway;
using System.Threading;
using BMC.CoreLib.WPF;
using BMC.CoreLib.Win32;
using BMC.PlayerGateway.DataLayer.Entity;
using BMC.PlayerGateway.DataLayer.TIS;
using BMC.PlayerGateway.SDT;
using BMC.PlayerGateway.SDT.Messages.Bmc2TIS;
using BMC.PlayerGateway.SDT.Messages.TIS2Bmc;
using BMC.PlayerGateway.SDT.TISGateway;
namespace BMC.Business.CashDeskOperator
{
    public partial class RedeemTicket
    {
        private int CONST_DEFAULT_TICKET_LENGTH = 18;
        public RTOnlineTicketDetail TicketDetail = new RTOnlineTicketDetail();
        public RTOnlineReceiptDetail ReceiptDetails = new RTOnlineReceiptDetail();
        public RTOnlineWageredDropDetail WageredDropDetails = new RTOnlineWageredDropDetail();
        public RedeemTicketDataAccess redeemTicketDB = new RedeemTicketDataAccess();

        public string GetCurrencySymbol()
        {
            if (!string.IsNullOrEmpty(Settings.Region) && Settings.Region.ToUpper() == "US")
                return "$";
            else
                return "£";
        }

        public static bool CheckLaunderingEnabled()
        {
            return Settings.EnableLaundering;
        }

        public bool CheckOfflineRedeemEnabled()
        {
            return Settings.AllowOffLineRedeem;
        }


        public bool CheckTicketValidateVoucherEnabled()
        {
            return Settings.EnableVoucher;
        }

        public double GetAmberCreditsWageredtoCashIn()
        {
            return redeemTicketDB.GetAmberCreditsWageredtoCashIn();
        }

        public bool GetWageredAndDrop(ref RTOnlineWageredDropDetail WageredDropDetail)
        {
            bool IsSuccessfull = false;
            DataSet TicketHistory = new DataSet();
            TicketHistory = redeemTicketDB.GetTicketHistory(WageredDropDetail);

            foreach (DataRow drTicketHistory in TicketHistory.Tables[0].Rows)
            {
                WageredDropDetail.WageredAmount = WageredDropDetail.WageredAmount + Convert.ToDouble(drTicketHistory["Credits_Wagered"]) / 100;
                WageredDropDetail.DropAmount = WageredDropDetail.DropAmount + (Convert.ToDouble(drTicketHistory["COINS_IN"]) + Convert.ToDouble(drTicketHistory["Notes_IN"]));
                IsSuccessfull = true;
            }
            return IsSuccessfull;
        }

        public bool TicketRedemptionReceiptCreate(double CurrentTicketValue, string User, string TicketNumber, double PayoutID, ref RTOnlineReceiptDetail ReceiptDetail)
        {
            bool isSuccess = false;
            //RTOnlineReceiptDetail ReceiptDetail = new RTOnlineReceiptDetail();
            ReceiptDetail.TicketString = TicketNumber.Trim();
            ReceiptDetail.TickerAmount = CurrentTicketValue;

            if (ReceiptDetail.TicketString.Length == 18)
            {
                if (redeemTicketDB.GetTITOTicketInformation(ReceiptDetail))
                    isSuccess = true;
            }
            else
            {
                ReceiptDetail.Payout = PayoutID;
                if (redeemTicketDB.GetMachineDetailsViaTBRPayout(ref ReceiptDetail))
                    isSuccess = true;
            }
            return isSuccess;
        }

        public bool OfflineTicket(ref RTOnlineTicketDetail TicketDetail)
        {
            bool isSuccess = false;

            try
            {
                if (redeemTicketDB.DoesOfflineTicketExist(ref TicketDetail))
                {
                    TicketDetail.EnableTickerPrintDetails = true;
                    redeemTicketDB.GetExceptionDetails(ref TicketDetail);
                    isSuccess = true;
                }
                else
                    isSuccess = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return isSuccess;
        }
        /// <summary>
        /// For offline Ticket Check-Durga
        /// </summary>
        /// <param name="TicketDetail"></param>
        /// <returns></returns>
        public RTOnlineTicketDetail CheckTicket(RTOnlineTicketDetail TicketDetail)
        {

            string LocalTicketorSiteCode;
            string sURL;
            int Installation_No = 0;
            int ValidationLength = 0;
            bool OfflineTicketRedemption = false;
            bool isTISPrintedTicket = VoucherHelper.IsTISPrintedTicket(TicketDetail.TicketString);

            if (!isTISPrintedTicket)
            {
                LinqDataAccessDataContext linqDBExchange = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
                IEnumerable<InstallationFromTicket> InstallationTicket = linqDBExchange.GetInstallationNumber(TicketDetail.TicketString);
                foreach (var item in InstallationTicket)
                {
                    Installation_No = item.installation_no.Value;
                }
                DataTable InstallationDetails = (new CommonDataAccess()).GetInstallationDetails(0, Installation_No, false, false);
                if (InstallationDetails.Rows.Count > 0)
                {
                    try
                    {
                        int.TryParse(InstallationDetails.Rows[0]["Validation_length"].ToString(), out ValidationLength);
                        if (ValidationLength == 0) ValidationLength = CONST_DEFAULT_TICKET_LENGTH;
                    }
                    catch { ValidationLength = CONST_DEFAULT_TICKET_LENGTH; }
                }
                if (ValidationLength == CONST_DEFAULT_TICKET_LENGTH)
                {
                    ValidateSiteCode(TicketDetail.TicketString, out LocalTicketorSiteCode, out sURL);

                    if (sURL.IsNullOrEmpty() || sURL == "INVALID") //Invalid Site Code or No rights to access other Site
                    {
                        TicketDetail.TicketStatus = "MessageID312";
                        TicketDetail.TicketStatusCode = -99;
                        TicketDetail.ValidTicket = false;
                        return TicketDetail;
                    }
                    else if (sURL.StartsWith("http")) // WebService Call in case of Different Site Code 
                    {
                        EndpointAddress objEndpoint = new EndpointAddress(sURL);
                        TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, LocalTicketorSiteCode);

                        //EndpointAddress objEndpoint = new EndpointAddress("http://10.2.108.29/TicketingWCFService/TicketingService.svc"); //sURL
                        //TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, "1001"); //LocalTicketorSiteCode);
                        return objClient.RedeemOnlineTicket(TicketDetail);

                    }
                }
            }
            else
            {
                if (!VoucherHelper.IsEffectiveDateActivated(TicketDetail.TicketString))
                {
                    TicketDetail.TicketStatus = "MessageID876";
                    TicketDetail.ValidTicket = false;
                    return TicketDetail;
                }
                //else if (VoucherHelper.IsCardRequired(TicketDetail.TicketString))
                //{
                //    TicketDetail.TicketStatus = "MessageID878";
                //    TicketDetail.ValidTicket = false;
                //    return TicketDetail;
                //}
            }

            // Local Site Code (Default)

            int Return = 0;

            bool isValidTicket = false;

            string TicketStatus = "";
            string TicketString = TicketDetail.TicketString;

            //HFXVH.Ticket HFXVHTicket = new HFXVH.Ticket();
            RTOnlineWageredDropDetail WageredDropDetail = new RTOnlineWageredDropDetail();


            TicketDetail.ShowOfflineTicketScreen = false;
            try
            {

                //Return = HFXVHTicket.TicketIsValid(ref TicketString, ref ReturnValue);
                Return = TicketIsValid(TicketString, isTISPrintedTicket, ref TicketDetail);
                //TicketDetail.TicketValue = ReturnValue;

                if (Return > -1)
                {
                    TicketStatus = "MessageID210";

                    //                        "VALID VOUCHER"+
                    //Application.Current.FindResource("MessageID210") as string 
                    //                  "(" + CommonUtilities.GetCurrency(Convert.ToDouble(TicketDetail.TicketValue / 100)) + ")";
                    isValidTicket = true;
                }
                else
                {
                    switch (Return)
                    {
                        case -1:
                            TicketStatus = "MessageID211";// Application.Current.FindResource("MessageID211") as string;
                            break;
                        case -2:
                            TicketStatus = "MessageID212";// Application.Current.FindResource("MessageID212") as string;  
                            break;
                        case -3:
                            TicketStatus = "MessageID213";// Application.Current.FindResource("MessageID213") as string; 

                            if (CheckOfflineRedeemEnabled())
                            {
                                if (OfflineTicket(ref TicketDetail))
                                {
                                    TicketStatus = "MessageID214";// Application.Current.FindResource("MessageID214") as string;  
                                    TicketDetail.EnableTickerPrintDetails = true;
                                }
                                else
                                {
                                    TicketDetail.ShowOfflineTicketScreen = true;
                                }
                            }
                            break;
                        case -4:
                            TicketStatus = "MessageID215";// Application.Current.FindResource("MessageID215") as string;  
                            TicketDetail.EnableTickerPrintDetails = true;

                            GetTicketDetails(ref TicketDetail);

                            //if (HFXVHTicket.GetTicketDetails(ref TicketString))
                            //{
                            //    TicketDetail.RedeemedMachine = HFXVHTicket.PrintedMachine;
                            //    TicketDetail.RedeemedDevice = HFXVHTicket.RedeemedMachine;
                            //    TicketDetail.RedeemedDate = HFXVHTicket.RedeemedDate;                              
                            //    TicketDetail.RedeemedAmount = CommonUtilities.GetCurrency((Convert.ToDouble(HFXVHTicket.Value) / 100));
                            //}
                            //else
                            //{
                            //    TicketDetail.RedeemedMachine = "";
                            //    TicketDetail.RedeemedDevice = "";
                            //    TicketDetail.RedeemedDate = "";
                            //    TicketDetail.RedeemedAmount = "";
                            //}

                            break;
                        case -5:
                            TicketStatus = "MessageID216";// Application.Current.FindResource("MessageID216") as string;  
                            break;
                        case -6:
                            TicketStatus = "MessageID217";// Application.Current.FindResource("MessageID217") as string;  
                            break;
                        case -7:
                            TicketStatus = "MessageID218";// Application.Current.FindResource("MessageID218") as string; 
                            break;
                        case -8:
                            TicketStatus = "MessageID219";// Application.Current.FindResource("MessageID219") as string; 
                            break;
                        case -9:
                            TicketStatus = "MessageID220";// Application.Current.FindResource("MessageID220") as string; 
                            break;
                        case -10:
                            TicketStatus = "MessageID221";// Application.Current.FindResource("MessageID221") as string;  
                            break;
                        case -11:
                            TicketStatus = "MessageID222";// Application.Current.FindResource("MessageID222") as string; 
                            break;
                        case -12:
                            TicketStatus = "MessageID223"; //Application.Current.FindResource("MessageID223") as string;   
                            break;
                        case -13:
                            TicketStatus = "MessageID306";// Application.Current.FindResource("MessageID306") as string;
                            break;
                        case -14:
                            TicketStatus = "MessageID312";// Application.Current.FindResource("MessageID312") as string;
                            break;
                        case -98:
                            CheckTicket(TicketDetail);
                            TicketStatus = "MessageID214";// Application.Current.FindResource("MessageID214") as string;
                            break;
                        default:
                            TicketStatus = "MessageID224";// Application.Current.FindResource("MessageID224") as string; 
                            break;
                    }
                }
                TicketDetail.TicketStatus = TicketStatus;

                if (isValidTicket || Return == -4)
                {
                    TicketDetail.ValidTicket = true;

                    GetTicketDetails(ref TicketDetail);

                    //if (HFXVHTicket.GetTicketDetails(ref TicketString))
                    //{
                    //    TicketDetail.RedeemedMachine = HFXVHTicket.PrintedMachine;
                    //    TicketDetail.RedeemedAmount = CommonUtilities.GetCurrency((Convert.ToDouble(HFXVHTicket.Value) / 100));
                    //}
                    //else
                    //{
                    //    TicketDetail.RedeemedMachine = "";
                    //    TicketDetail.RedeemedAmount = "";
                    //}

                    // Check laundering limits, if amount wagered is not enough of credits in, show warning
                    if (Settings.EnableLaundering)
                    {
                        double Wagered = 0;
                        double Drop = 0;
                        WageredDropDetail.WageredAmount = 0;
                        WageredDropDetail.DropAmount = 0;
                        WageredDropDetail.TicketString = TicketString;

                        bool isSuccess = GetWageredAndDrop(ref WageredDropDetail);

                        if (isSuccess)
                        {
                            Wagered = WageredDropDetail.WageredAmount;
                            Drop = WageredDropDetail.DropAmount;
                        }

                        if (Drop > 0)
                        {
                            if ((100 - (((Drop - Wagered) * 100) / Drop)) <= GetAmberCreditsWageredtoCashIn())
                                TicketDetail.TicketWarning = "Suspect Voucher..";
                        }
                    }

                    if (CheckTicketValidateVoucherEnabled() && isValidTicket)
                    {
                        //Check for Offline
                        if (OfflineTicket(ref TicketDetail))
                            OfflineTicketRedemption = true;


                        BMC.Common.LogManagement.LogManager.WriteLog("Started RTOnlineReceiptDetail", BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
                        RTOnlineReceiptDetail ReceiptDetail = new RTOnlineReceiptDetail();
                        TicketRedemptionReceiptCreate((TicketDetail.TicketValue / 100), "", TicketString, 0, ref ReceiptDetail);//HFXVHTicket.CurrentPayoutID);                        
                        CommonUtilities oCommonUtilities = new CommonUtilities();
                        if (OfflineTicketRedemption)
                            oCommonUtilities.GetCommonValues(TicketDetail, ReceiptDetail, "OFFLINE VOUCHER REDEMPTION");
                        else
                            oCommonUtilities.GetCommonValues(TicketDetail, ReceiptDetail);
                        BMC.Common.LogManagement.LogManager.WriteLog("Done RTOnlineReceiptDetail", BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
                    }

                }
                return TicketDetail;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return TicketDetail;
            }
        }
        public bool CheckTicketCage(RTOnlineTicketDetail TicketDetail)
        {

            string LocalTicketorSiteCode;
            string sURL;
            ValidateSiteCode(TicketDetail.TicketString, out LocalTicketorSiteCode, out sURL);

            if (sURL.IsNullOrEmpty() || sURL == "INVALID") //Invalid Site Code or No rights to access other Site
            {
                LogManager.WriteLog("CheckTicketCage:Invalid URL(Site Code Mismatch) " + TicketDetail.TicketString, LogManager.enumLogLevel.Debug);
                TicketDetail.TicketStatus = "Site Code Mismatch";
                TicketDetail.TicketStatusCode = -99;
                TicketDetail.ValidTicket = false;
                //return TicketDetail;
            }
            else if (sURL.StartsWith("http")) // WebService Call in case of Different Site Code 
            {
                LogManager.WriteLog(string.Format("CheckVoucherCage:URL {0} : Voucher {1}  ", sURL, TicketDetail.TicketString), LogManager.enumLogLevel.Debug);
                EndpointAddress objEndpoint = new EndpointAddress(sURL);
                TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, LocalTicketorSiteCode);

                //EndpointAddress objEndpoint = new EndpointAddress("http://10.2.108.29/TicketingWCFService/TicketingService.svc"); //sURL
                //TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, "1001"); //LocalTicketorSiteCode);
                TicketDetail.ClientSiteCode = LocalTicketorSiteCode;
                TicketDetail.RedeemedMachine = Environment.MachineName + "_Cage";
                TicketDetail = objClient.RedeemOnlineTicket(TicketDetail);

                if (TicketDetail.ValidTicket)
                {
                    //Cross Ticketing- Insert Local Record 
                    if (!string.IsNullOrEmpty(TicketDetail.VoucherXMLData))
                    {
                        TicketDetail.RedeemedUser = Security.SecurityHelper.CurrentUser.UserName;
                        ImportVoucherDetails(TicketDetail);
                    }
                }
                else
                {
                    LogManager.WriteLog(string.Format("CheckVoucherCage:URL {0} : Voucher {1} STATUS {2}  ", sURL, TicketDetail.TicketString, TicketDetail.TicketStatus), LogManager.enumLogLevel.Debug);

                    string strStatus = string.Empty;
                    switch (TicketDetail.TicketStatus)
                    {
                        case "MessageID211":
                            strStatus = "BLANK VOUCHER";
                            break;
                        case "MessageID212":// Application.Current.FindResource("MessageID212") as string;  
                            strStatus = "NO DB CONNECTION";
                            break;
                        case "MessageID213":
                            strStatus = "VOUCHER NOT FOUND";// Application.Current.FindResource("MessageID213") as string; 

                            if (CheckOfflineRedeemEnabled())
                            {
                                if (OfflineTicket(ref TicketDetail))
                                {
                                    strStatus = "OFFILNE VOUCHER CLAIMED";// Application.Current.FindResource("MessageID214") as string;  
                                    TicketDetail.EnableTickerPrintDetails = true;
                                }
                                else
                                {
                                    TicketDetail.ShowOfflineTicketScreen = true;
                                }
                            }
                            break;
                        case "MessageID215":// Application.Current.FindResource("MessageID215") as string;  
                            strStatus = "ALREADY CLAIMED";
                            TicketDetail.EnableTickerPrintDetails = true;

                            GetTicketDetails(ref TicketDetail);

                            //if (HFXVHTicket.GetTicketDetails(ref TicketString))
                            //{
                            //    TicketDetail.RedeemedMachine = HFXVHTicket.PrintedMachine;
                            //    TicketDetail.RedeemedDevice = HFXVHTicket.RedeemedMachine;
                            //    TicketDetail.RedeemedDate = HFXVHTicket.RedeemedDate;                              
                            //    TicketDetail.RedeemedAmount = CommonUtilities.GetCurrency((Convert.ToDouble(HFXVHTicket.Value) / 100));
                            //}
                            //else
                            //{
                            //    TicketDetail.RedeemedMachine = "";
                            //    TicketDetail.RedeemedDevice = "";
                            //    TicketDetail.RedeemedDate = "";
                            //    TicketDetail.RedeemedAmount = "";
                            //}

                            break;
                        case "MessageID216":// Application.Current.FindResource("MessageID216") as string;  
                            strStatus = "NON PAYOUT TYPE";
                            break;
                        case "MessageID217":// Application.Current.FindResource("MessageID217") as string;  
                            strStatus = "INVALID VOUCHER TYPE";
                            break;
                        case "MessageID218":// Application.Current.FindResource("MessageID218") as string; 
                            strStatus = "INVALID VOUCHER";
                            break;
                        case "MessageID219":// Application.Current.FindResource("MessageID219") as string; 
                            strStatus = "VOUCHER EXPIRED";
                            break;
                        case "MessageID220":// Application.Current.FindResource("MessageID220") as string; 
                            strStatus = "VOUCHER VOIDED";
                            break;
                        case "MessageID221":// Application.Current.FindResource("MessageID221") as string;  
                            strStatus = "VOUCHER EXCEPTION";
                            break;
                        case "MessageID222":// Application.Current.FindResource("MessageID222") as string; 
                            strStatus = "PROMO VOUCHER";
                            break;
                        case "MessageID223": //Application.Current.FindResource("MessageID223") as string;   
                            strStatus = "INVALID - CANCELLED VOUCHER";
                            break;
                        case "MessageID306":// Application.Current.FindResource("MessageID306") as string;
                            strStatus = "NON CASHABLE VOUCHER";
                            break;
                        case "MessageID312":// Application.Current.FindResource("MessageID312") as string;
                            strStatus = "Site Code Mismatch";
                            break;
                        case "MessageID214":
                            CheckTicket(TicketDetail);
                            strStatus = "OFFILNE VOUCHER CLAIMED";// Application.Current.FindResource("MessageID214") as string;
                            break;
                        default:
                            strStatus = "UNSPECIFIED ERROR";// Application.Current.FindResource("MessageID224") as string; 
                            break;

                    }
                    TicketDetail.TicketStatus = strStatus;
                }
                return TicketDetail.ValidTicket;
            }

            int Return = 0;

            bool isValidTicket = false;

            string TicketStatus = "";
            string TicketString = TicketDetail.TicketString;

            //HFXVH.Ticket HFXVHTicket = new HFXVH.Ticket();
            RTOnlineWageredDropDetail WageredDropDetail = new RTOnlineWageredDropDetail();


            TicketDetail.ShowOfflineTicketScreen = false;
            try
            {

                //Return = HFXVHTicket.TicketIsValid(ref TicketString, ref ReturnValue);
                Return = TicketIsValidCage(TicketString, ref TicketDetail);
                //TicketDetail.TicketValue = ReturnValue;

                if (Return > -1)
                {
                    TicketStatus = "VALID VOUCHER" + "(" + CommonUtilities.GetCurrency(Convert.ToDouble(TicketDetail.TicketValue / 100)) + ")";
                    isValidTicket = true;
                }
                else
                {
                    switch (Return)
                    {
                        case -1:
                            TicketStatus = "BLANK VOUCHER";
                            break;
                        case -2:
                            TicketStatus = "NO DB CONNECTION";
                            break;
                        case -3:
                            TicketStatus = "VOUCHER NOT FOUND";

                            if (CheckOfflineRedeemEnabled())
                            {
                                if (OfflineTicket(ref TicketDetail))
                                {
                                    TicketStatus = "OFFILNE VOUCHER CLAIMED";
                                    TicketDetail.EnableTickerPrintDetails = true;
                                }
                                else
                                {
                                    TicketDetail.ShowOfflineTicketScreen = true;
                                }
                            }
                            break;
                        case -4:
                            TicketStatus = "ALREADY CLAIMED";
                            TicketDetail.EnableTickerPrintDetails = true;

                            GetTicketDetails(ref TicketDetail);

                            //if (HFXVHTicket.GetTicketDetails(ref TicketString))
                            //{
                            //    TicketDetail.RedeemedMachine = HFXVHTicket.PrintedMachine;
                            //    TicketDetail.RedeemedDevice = HFXVHTicket.RedeemedMachine;
                            //    TicketDetail.RedeemedDate = HFXVHTicket.RedeemedDate;                              
                            //    TicketDetail.RedeemedAmount = CommonUtilities.GetCurrency((Convert.ToDouble(HFXVHTicket.Value) / 100));
                            //}
                            //else
                            //{
                            //    TicketDetail.RedeemedMachine = "";
                            //    TicketDetail.RedeemedDevice = "";
                            //    TicketDetail.RedeemedDate = "";
                            //    TicketDetail.RedeemedAmount = "";
                            //}

                            break;
                        case -5:
                            TicketStatus = "NON PAYOUT TYPE";
                            break;
                        case -6:
                            TicketStatus = "INVALID VOUCHER TYPE";
                            break;
                        case -7:
                            TicketStatus = "INVALID VOUCHER";
                            break;
                        case -8:
                            TicketStatus = "VOUCHER EXPIRED";
                            break;
                        case -9:
                            TicketStatus = "VOUCHER VOIDED";
                            break;
                        case -10:
                            TicketStatus = "VOUCHER EXCEPTION";
                            break;
                        case -11:
                            TicketStatus = "PROMO VOUCHER";
                            break;
                        case -12:
                            TicketStatus = "INVALID - CANCELLED VOUCHER";
                            break;
                        case -13:
                            TicketStatus = "NON CASHABLE VOUCHER";
                            break;
                        case -14:
                            TicketStatus = "Site Code Mismatch";
                            break;
                        case -98:
                            CheckTicketCage(TicketDetail);
                            TicketStatus = "OFFILNE VOUCHER CLAIMED";
                            break;
                        default:
                            TicketStatus = "UNSPECIFIED ERROR";
                            break;
                    }
                }
                TicketDetail.TicketStatus = TicketStatus;

                if (isValidTicket || Return == -4)
                {
                    TicketDetail.ValidTicket = true;

                    GetTicketDetails(ref TicketDetail);

                    //if (HFXVHTicket.GetTicketDetails(ref TicketString))
                    //{
                    //    TicketDetail.RedeemedMachine = HFXVHTicket.PrintedMachine;
                    //    TicketDetail.RedeemedAmount = CommonUtilities.GetCurrency((Convert.ToDouble(HFXVHTicket.Value) / 100));
                    //}
                    //else
                    //{
                    //    TicketDetail.RedeemedMachine = "";
                    //    TicketDetail.RedeemedAmount = "";
                    //}

                    // Check laundering limits, if amount wagered is not enough of credits in, show warning
                    if (Settings.EnableLaundering)
                    {
                        double Wagered = 0;
                        double Drop = 0;
                        WageredDropDetail.WageredAmount = 0;
                        WageredDropDetail.DropAmount = 0;
                        WageredDropDetail.TicketString = TicketString;

                        bool isSuccess = GetWageredAndDrop(ref WageredDropDetail);

                        if (isSuccess)
                        {
                            Wagered = WageredDropDetail.WageredAmount;
                            Drop = WageredDropDetail.DropAmount;
                        }

                        if (Drop > 0)
                        {
                            if ((100 - (((Drop - Wagered) * 100) / Drop)) <= GetAmberCreditsWageredtoCashIn())
                                TicketDetail.TicketWarning = "Suspect Voucher..";
                        }
                    }
                    //Commecnted for cage
                    //if (CheckTicketValidateVoucherEnabled() && isValidTicket)
                    //{
                    //    BMC.Common.LogManagement.LogManager.WriteLog("Started RTOnlineReceiptDetail", BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
                    //    RTOnlineReceiptDetail ReceiptDetail = new RTOnlineReceiptDetail();
                    //    TicketRedemptionReceiptCreate((TicketDetail.TicketValue / 100), "", TicketString, 0, ref ReceiptDetail);//HFXVHTicket.CurrentPayoutID);                        
                    //    CommonUtilities oCommonUtilities = new CommonUtilities();
                    //    oCommonUtilities.GetCommonValues(TicketDetail, ReceiptDetail);
                    //    BMC.Common.LogManagement.LogManager.WriteLog("Done RTOnlineReceiptDetail", BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
                    //}

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return TicketDetail.ValidTicket;
        }


        public int TicketIsValid(string TicketString, bool isTISPrintedTicket, ref RTOnlineTicketDetail TicketDetail)
        {
            int Return = 0;
            int Installation_No = 0;
            int? Validation_Length = 0;
            int? IsPromotional = 0;
            int PayTicket = 0;
            string BarCode = string.Empty;
            LinqDataAccessDataContext linqDBExchange = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);

            try
            {
                IEnumerable<InstallationFromTicket> InstallationTicket = linqDBExchange.GetInstallationNumber(TicketDetail.TicketString);

                foreach (var item in InstallationTicket)
                {
                    Installation_No = item.installation_no.Value;
                    BarCode = item.strbarcode.Trim();
                    //TicketString = item.strbarcode.Trim();
                }

                if (Installation_No > 0)
                {
                    linqDBExchange.GetValidationLength(Installation_No, ref Validation_Length);

                    if (Validation_Length.Value != 0)
                    {
                        if (TicketString.Length != Validation_Length.Value)
                            return -3;
                        else
                            TicketString = BarCode;
                    }
                }

                linqDBExchange.CheckPromotionalTicket(TicketString, ref IsPromotional);

                if (IsPromotional.Value == -1)
                    IsPromotional = 1;

                if (IsPromotional.Value == 0)
                    return -11;
                else if (IsPromotional.Value < 0)
                    return -99;

                PayTicket = redeemTicketDB.PaySDGTicket(TicketString, Settings.RedeemExpiredTicket, ref TicketDetail);

                switch (PayTicket)
                {
                    case 0:
                        redeemTicketDB.pCloseSDGTicket(TicketString, TicketDetail);//  TicketString,TicketDetail.ClientSiteCode);
                        if (isTISPrintedTicket)
                        {
                            VoucherHelper.SendTISRedeemTicket(TicketString, TicketDetail.AuthorizedUser_No);
                        }
                        Return = 0;
                        break;
                    case -1:
                        if (redeemTicketDB.CheckforTicketException(TicketString))
                            Return = -10;
                        else
                            Return = -4;
                        break;
                    case -2:
                        if (redeemTicketDB.CheckforTicketException(TicketString))
                        {
                            bool IsFailed = false;
                            int? result = 0;

                            try
                            {
                                linqDBExchange.CreateTicketComplete(TicketString, Security.SecurityHelper.CurrentUser.User_No.ToString(), Security.SecurityHelper.CurrentUser.User_No.ToString(), ref result);

                                if (result.Value != 0)
                                    IsFailed = true;

                                if (!IsFailed)
                                    linqDBExchange.InsertException(Installation_No, 205,
                                        "Voucher (" + TicketString + ") Activated via Ticketing application when claiming",
                                        TicketString, Security.SecurityHelper.CurrentUser.UserName);

                                //Return = -10;

                                TicketIsValid(TicketString, isTISPrintedTicket, ref TicketDetail);
                            }
                            catch (System.Data.SqlClient.SqlException sqlEx)
                            {
                                ExceptionManager.Publish(sqlEx);
                                Return = -98;
                            }
                        }
                        else
                            Return = -3;
                        break;
                    case -3:
                        Return = -8;
                        break;
                    case -4:
                        Return = -9;
                        break;
                    case -5:
                        Return = -9;
                        break;
                    //case -12:
                    //    Return = -12;
                    //    break;
                    case -15:
                        redeemTicketDB.pCloseSDGTicket(TicketString, TicketDetail);
                        Return = 0;
                        break;
                    default:
                        Return = PayTicket;
                        break;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                Return = -99;
            }
            return Return;
        }
        public int TicketIsValidCage(string TicketString, ref RTOnlineTicketDetail TicketDetail)
        {
            int Return = 0;
            int Installation_No = 0;
            int? Validation_Length = 0;
            int? IsPromotional = 0;
            int PayTicket = 0;
            string BarCode = string.Empty;
            LinqDataAccessDataContext linqDBExchange = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);

            try
            {
                IEnumerable<InstallationFromTicket> InstallationTicket = linqDBExchange.GetInstallationNumber(TicketDetail.TicketString);

                foreach (var item in InstallationTicket)
                {
                    Installation_No = item.installation_no.Value;
                    BarCode = item.strbarcode.Trim();
                    //TicketString = item.strbarcode.Trim();
                }

                if (Installation_No > 0)
                {
                    linqDBExchange.GetValidationLength(Installation_No, ref Validation_Length);

                    if (Validation_Length.Value != 0)
                    {
                        if (TicketString.Length != Validation_Length.Value)
                            return -3;
                        else
                            TicketString = BarCode;
                    }
                }

                linqDBExchange.CheckPromotionalTicket(TicketString, ref IsPromotional);

                if (IsPromotional.Value == -1)
                    IsPromotional = 1;

                if (IsPromotional.Value == 0)
                    return -11;
                else if (IsPromotional.Value < 0)
                    return -99;

                PayTicket = redeemTicketDB.PaySDGTicketCage(TicketString, Settings.RedeemExpiredTicket, ref TicketDetail);

                switch (PayTicket)
                {
                    case 0:
                        redeemTicketDB.pCloseSDGTicketCage(TicketString);
                        Return = 0;
                        break;
                    case -1:
                        if (redeemTicketDB.CheckforTicketException(TicketString))
                            Return = -10;
                        else
                            Return = -4;
                        break;
                    case -2:
                        if (redeemTicketDB.CheckforTicketException(TicketString))
                        {
                            bool IsFailed = false;
                            int? result = 0;

                            try
                            {
                                linqDBExchange.CreateTicketComplete(TicketString, Security.SecurityHelper.CurrentUser.User_No.ToString(), Security.SecurityHelper.CurrentUser.User_No.ToString(), ref result);

                                if (result.Value != 0)
                                    IsFailed = true;

                                if (!IsFailed)
                                    linqDBExchange.InsertException(Installation_No, 205,
                                        "Voucher (" + TicketString + ") Activated via Ticketing application when claiming",
                                        TicketString, Security.SecurityHelper.CurrentUser.UserName);

                                //Return = -10;

                                TicketIsValid(TicketString, false, ref TicketDetail);
                            }
                            catch (System.Data.SqlClient.SqlException sqlEx)
                            {
                                ExceptionManager.Publish(sqlEx);
                                Return = -98;
                            }
                        }
                        else
                            Return = -3;
                        break;
                    case -3:
                        Return = -8;
                        break;
                    case -4:
                        Return = -9;
                        break;
                    case -5:
                        Return = -9;
                        break;
                    //case -12:
                    //    Return = -12;
                    //    break;
                    case -15:
                        redeemTicketDB.pCloseSDGTicketCage(TicketString);
                        Return = 0;
                        break;
                    default:
                        Return = PayTicket;
                        break;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                Return = -99;
            }
            return Return;
        }

        public void GetTicketDetails(ref RTOnlineTicketDetail TicketDetail)
        {
            LinqDataAccessDataContext linqDB = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
            DateTime? dtPaid = DateTime.Now;
            int? Amount = 0;
            string payDevice = string.Empty, printDevice = string.Empty;

            try
            {
                linqDB.GetTicketDetails(TicketDetail.TicketString, ref printDevice, ref payDevice, ref dtPaid, ref Amount);

                TicketDetail.RedeemedDate = (DateTime)dtPaid;
                TicketDetail.RedeemedDevice = payDevice;
                TicketDetail.RedeemedMachine = printDevice;
                TicketDetail.RedeemedAmount = (Convert.ToDecimal(Amount.Value) / 100);


            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public int CheckSDGTicket(string TicketString)
        {
            int? ReturnValue = 99;
            int Installation_No = 0;
            int? Validation_Length = 0;
            string BarCode = string.Empty;

            try
            {
                LinqDataAccessDataContext linqDB = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
                IEnumerable<InstallationFromTicket> InstallationTicket = linqDB.GetInstallationNumber(TicketString.Trim());

                foreach (var item in InstallationTicket)
                {
                    Installation_No = item.installation_no.Value;
                    BarCode = item.strbarcode.Trim();
                }

                if (Installation_No > 0)
                {
                    linqDB.GetValidationLength(Installation_No, ref Validation_Length);

                    if (Validation_Length.Value != 0)
                    {
                        if (TicketString.Length != Validation_Length.Value)
                            return -2;
                        else
                            TicketString = BarCode;
                    }
                }

                LinqDataAccessDataContext linqTic = new LinqDataAccessDataContext(CommonDataAccess.TicketingConnectionString);
                linqTic.CheckSDGTicket(TicketString, System.Environment.MachineName, ref ReturnValue);
                return ReturnValue.Value;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -99;
            }
        }

        public int CheckSDGTicketCage(string TicketString)
        {
            int? ReturnValue = 99;
            int Installation_No = 0;
            int? Validation_Length = 0;
            string BarCode = string.Empty;

            try
            {


                string LocalTicketorSiteCode;
                string sURL;
                ValidateSiteCode(TicketString, out LocalTicketorSiteCode, out sURL);

                if (sURL.IsNullOrEmpty() || sURL == "INVALID") //Invalid Site Code or No rights to access other Site
                {
                    return -99;
                    //return TicketDetail;
                }
                else if (sURL.StartsWith("http")) // WebService Call in case of Different Site Code 
                {
                    EndpointAddress objEndpoint = new EndpointAddress(sURL);
                    TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, LocalTicketorSiteCode);

                    //EndpointAddress objEndpoint = new EndpointAddress("http://10.2.108.29/TicketingWCFService/TicketingService.svc"); //sURL
                    //TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, "1001"); //LocalTicketorSiteCode);
                    return objClient.CheckSDGTicket(TicketString);
                }

                // Current site--
                LinqDataAccessDataContext linqDB = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
                IEnumerable<InstallationFromTicket> InstallationTicket = linqDB.GetInstallationNumber(TicketString.Trim());

                foreach (var item in InstallationTicket)
                {
                    Installation_No = item.installation_no.Value;
                    BarCode = item.strbarcode.Trim();
                }

                if (Installation_No > 0)
                {
                    linqDB.GetValidationLength(Installation_No, ref Validation_Length);

                    if (Validation_Length.Value != 0)
                    {
                        if (TicketString.Length != Validation_Length.Value)
                            return -2;
                        else
                            TicketString = BarCode;
                    }
                }

                LinqDataAccessDataContext linqTic = new LinqDataAccessDataContext(CommonDataAccess.TicketingConnectionString);
                linqTic.CheckSDGTicket(TicketString, System.Environment.MachineName, ref ReturnValue);
                return ReturnValue.Value;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -99;
            }
        }



        public RTOnlineTicketDetail GetRedeemTicketAmount(RTOnlineTicketDetail TicketDetailEntity)
        {
            try
            {
                string LocalTicketorSiteCode;
                string sURL;
                int Installation_No = 0;
                int ValidationLength = 0;
                bool isTISPrintedTicket = VoucherHelper.IsTISPrintedTicketPrefix(TicketDetailEntity.TicketString);
                bool hasException = false;
                int PlayerCardValidation = 0;
                string PlayerCardID = string.Empty;
                int TicketType = -1;
                int CurrentTickStatus = 0;
                if (!isTISPrintedTicket)
                {
                    LinqDataAccessDataContext linqDBExchange = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
                    IEnumerable<InstallationFromTicket> InstallationTicket = linqDBExchange.GetInstallationNumber(TicketDetailEntity.TicketString);
                    foreach (var item in InstallationTicket)
                    {
                        Installation_No = item.installation_no.Value;
                    }
                    DataTable InstallationDetails = (new CommonDataAccess()).GetInstallationDetails(0, Installation_No, false, false);
                    if (InstallationDetails.Rows.Count > 0)
                    {
                        try
                        {
                            int.TryParse(InstallationDetails.Rows[0]["Validation_length"].ToString(), out ValidationLength);
                            if (ValidationLength == 0) ValidationLength = CONST_DEFAULT_TICKET_LENGTH;
                        }
                        catch { ValidationLength = CONST_DEFAULT_TICKET_LENGTH; }
                    }
                    if (ValidationLength == CONST_DEFAULT_TICKET_LENGTH)
                    {
                        ValidateSiteCode(TicketDetailEntity.TicketString, out LocalTicketorSiteCode, out sURL);
                        if (sURL.IsNullOrEmpty() || sURL == "INVALID") //Invalid Site Code or No rights to access other Site
                        {
                            TicketDetailEntity.TicketStatus = "MessageID312";
                            TicketDetailEntity.TicketStatusCode = -99;
                            TicketDetailEntity.ValidTicket = false;
                            return TicketDetailEntity;
                        }
                        else if (sURL.StartsWith("http")) // WebService Call in case of Different Site Code 
                        {
                            EndpointAddress objEndpoint = new EndpointAddress(sURL);
                            TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, LocalTicketorSiteCode);
                            //EndpointAddress objEndpoint = new EndpointAddress("http://10.2.108.29/TicketingWCFService/TicketingService.svc"); //sURL
                            //TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, "1001"); //LocalTicketorSiteCode);

                            return objClient.GetRedeemTicketAmount(TicketDetailEntity);

                        }
                    }
                }
                else
                {
                    // if the tis printed ticket available in local db
                    bool isTISTicketAvailable = VoucherHelper.IsTISPrintedTicket(TicketDetailEntity.TicketString);
                    if (isTISTicketAvailable)
                    {
                        // Success case - ok proceed with redeeming
                    }
                    else
                    {
                        // wait worst case 10 secs to get the response from TIS
                        int count = 10;
                        string message = "Waiting for receiving data from TIS...";
                        WPFExtensions.ShowAsyncDialog(null, message, null, 1, count,
                            (o) =>
                            {
                                IAsyncProgress2 o2 = o as IAsyncProgress2;

                                // failure case - hit the tis communication interface and get the ticket
                                try
                                {
                                    o2.UpdateStatusProgress(5, message);
                                    var resp = VoucherHelper.SendTISRedeemTicketQuery(TicketDetailEntity.TicketString, TicketDetailEntity.AuthorizedUser_No);
                                    o2.UpdateStatusProgress(10, message);
                                    if (resp != null &&
                                        !resp.ErrorMessage.IsNullOrEmpty())
                                    {
                                        // TIS Command Service not found or any service error
                                        TicketDetail.TicketStatusCode = -990;
                                        TicketDetail.TicketErrorMessage = resp.ErrorMessage;
                                        hasException = true;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Publish(ex);
                                    TicketDetail.TicketStatusCode = -234;//Exception Case
                                    hasException = true;
                                }
                            });
                    }


                    TicketType = VoucherHelper.TicketType(TicketDetailEntity.TicketString);
                    
                    CurrentTickStatus = VoucherHelper.CurrentTicketStatus(TicketDetailEntity.TicketString);
                    if (VoucherHelper.IsCardRequired(TicketDetailEntity.TicketString))
                    {
                        PlayerCardValidation = 2;

                       

                    }

                    else if (VoucherHelper.IsSpecificCardRequired(TicketDetailEntity.TicketString))
                    {
                        PlayerCardValidation = 1;

                        PlayerCardID = VoucherHelper.PlayerCardNumber(TicketDetailEntity.TicketString);

                    }

                }

                if (!hasException)
                {
                    //Local Site Code(default)
                    TicketDetail = redeemTicketDB.GetRedeemTicketAmount(TicketDetailEntity);
                    TicketDetail.TicketType = TicketType;
                    TicketDetail.CurrentTicketStatus = CurrentTickStatus;

                    if (CurrentTickStatus == 1)
                    {
                        if (TicketType == 0)
                        {
                            if (PlayerCardValidation != 0)
                            {
                                TicketDetail.TicketStatusCode = 250;
                                TicketDetail.CardRequired = PlayerCardValidation;
                                if (PlayerCardValidation == 1)
                                    TicketDetail.PlayerCardNumber = PlayerCardID;

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                TicketDetail.TicketStatusCode = -234;//Exception Case
            }

            return TicketDetail;
        }
        public RTOnlineTicketDetail GetMultiRedeemTicketAmount(RTOnlineTicketDetail TicketDetailEntity)
        {
            try
            {
                string LocalTicketorSiteCode;
                string sURL;
                int Installation_No = 0;
                int ValidationLength = 0;
                bool isTISPrintedTicket = true; ;
                bool hasException = false;
                int PlayerCardValidation = 0;
                string PlayerCardID = string.Empty;
                int TicketType = -1;
                int CurrentTickStatus = 0;
                if (!isTISPrintedTicket)
                {
                    LinqDataAccessDataContext linqDBExchange = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
                    IEnumerable<InstallationFromTicket> InstallationTicket = linqDBExchange.GetInstallationNumber(TicketDetailEntity.TicketString);
                    foreach (var item in InstallationTicket)
                    {
                        Installation_No = item.installation_no.Value;
                    }
                    DataTable InstallationDetails = (new CommonDataAccess()).GetInstallationDetails(0, Installation_No, false, false);
                    if (InstallationDetails.Rows.Count > 0)
                    {
                        try
                        {
                            int.TryParse(InstallationDetails.Rows[0]["Validation_length"].ToString(), out ValidationLength);
                            if (ValidationLength == 0) ValidationLength = CONST_DEFAULT_TICKET_LENGTH;
                        }
                        catch { ValidationLength = CONST_DEFAULT_TICKET_LENGTH; }
                    }
                    if (ValidationLength == CONST_DEFAULT_TICKET_LENGTH)
                    {
                        ValidateSiteCode(TicketDetailEntity.TicketString, out LocalTicketorSiteCode, out sURL);
                        if (sURL.IsNullOrEmpty() || sURL == "INVALID") //Invalid Site Code or No rights to access other Site
                        {
                            TicketDetailEntity.TicketStatus = "MessageID312";
                            TicketDetailEntity.TicketStatusCode = -99;
                            TicketDetailEntity.ValidTicket = false;
                            return TicketDetailEntity;
                        }
                        else if (sURL.StartsWith("http")) // WebService Call in case of Different Site Code 
                        {
                            EndpointAddress objEndpoint = new EndpointAddress(sURL);
                            TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, LocalTicketorSiteCode);
                            //EndpointAddress objEndpoint = new EndpointAddress("http://10.2.108.29/TicketingWCFService/TicketingService.svc"); //sURL
                            //TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, "1001"); //LocalTicketorSiteCode);

                            return objClient.GetRedeemTicketAmount(TicketDetailEntity);

                        }
                    }
                }
                else
                {
                    // if the tis printed ticket available in local db
                    bool isTISTicketAvailable = VoucherHelper.IsTISPrintedTicket(TicketDetailEntity.TicketString);
                    if (isTISTicketAvailable)
                    {
                        // Success case - ok proceed with redeeming
                    }
                    else
                    {
                        // wait worst case 10 secs to get the response from TIS
                        int count = 10;
                        string message = "Waiting for receiving data from TIS...";
                        WPFExtensions.ShowAsyncDialog(null, message, null, 1, count,
                            (o) =>
                            {
                                IAsyncProgress2 o2 = o as IAsyncProgress2;

                                // failure case - hit the tis communication interface and get the ticket
                                try
                                {
                                    o2.UpdateStatusProgress(5, message);
                                    var resp = VoucherHelper.SendTISRedeemTicketQuery(TicketDetailEntity.TicketString, TicketDetailEntity.AuthorizedUser_No);
                                    o2.UpdateStatusProgress(10, message);
                                    if (resp != null &&
                                        !resp.ErrorMessage.IsNullOrEmpty())
                                    {

                                        var resp2 = VoucherHelper.GetInquiryResponse(TicketDetailEntity.TicketString); 
                                        // TIS Command Service not found or any service error
                                        TicketDetail.TicketStatusCode = -990;
                                        TicketDetail.TicketErrorMessage = resp.ErrorMessage;
                                        TicketDetail.RedeemedAmount = resp2.Amount;                                       
                                        hasException = true;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Publish(ex);
                                    TicketDetail.TicketStatusCode = -234;//Exception Case
                                    hasException = true;
                                }
                            });
                    }


                    TicketType = VoucherHelper.TicketType(TicketDetailEntity.TicketString);

                    CurrentTickStatus = VoucherHelper.CurrentTicketStatus(TicketDetailEntity.TicketString);
                    if (VoucherHelper.IsCardRequired(TicketDetailEntity.TicketString))
                    {
                        PlayerCardValidation = 2;



                    }

                    else if (VoucherHelper.IsSpecificCardRequired(TicketDetailEntity.TicketString))
                    {
                        PlayerCardValidation = 1;

                        PlayerCardID = VoucherHelper.PlayerCardNumber(TicketDetailEntity.TicketString);

                    }

                }

                if (!hasException)
                {
                    //Local Site Code(default)
                    TicketDetail = redeemTicketDB.GetRedeemTicketAmount(TicketDetailEntity);
                    TicketDetail.TicketType = TicketType;
                    TicketDetail.CurrentTicketStatus = CurrentTickStatus;

                    if (CurrentTickStatus == 1)
                    {
                        if (TicketType == 0)
                        {
                            if (PlayerCardValidation != 0)
                            {
                                TicketDetail.TicketStatusCode = 250;
                                TicketDetail.CardRequired = PlayerCardValidation;
                                if (PlayerCardValidation == 1)
                                    TicketDetail.PlayerCardNumber = PlayerCardID;

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                TicketDetail.TicketStatusCode = -234;//Exception Case
            }

            return TicketDetail;
        }

        public RTOnlineTicketDetail GetRedeemTicketAmountCage(RTOnlineTicketDetail TicketDetailEntity)
        {
            try
            {
                string LocalTicketorSiteCode;
                string sURL;
                ValidateSiteCode(TicketDetailEntity.TicketString, out LocalTicketorSiteCode, out sURL);

                if (sURL.IsNullOrEmpty() || sURL == "INVALID") //Invalid Site Code or No rights to access other Site
                {
                    TicketDetailEntity.TicketStatus = "Site Code Mismatch";
                    TicketDetailEntity.TicketStatusCode = -99;
                    TicketDetailEntity.ValidTicket = false;
                    return TicketDetailEntity;
                }
                else if (sURL.StartsWith("http")) // WebService Call in case of Different Site Code 
                {
                    EndpointAddress objEndpoint = new EndpointAddress(sURL);
                    TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, LocalTicketorSiteCode);
                    //EndpointAddress objEndpoint = new EndpointAddress("http://10.2.108.29/TicketingWCFService/TicketingService.svc"); //sURL
                    //TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, "1001"); //LocalTicketorSiteCode);

                    return objClient.GetRedeemTicketAmount(TicketDetailEntity);

                }
                //Local Site Code(default)
                return redeemTicketDB.GetRedeemTicketAmount(TicketDetailEntity);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                TicketDetail.TicketStatusCode = -234;//Exception Case
                return TicketDetail;
            }
        }

        public void ValidateSiteCode(string ticketString, out string LocalTicketorSiteCode, out string sURL)
        {
            LocalTicketorSiteCode = string.Empty;
            sURL = string.Empty;

            string sTicketSiteCode = ticketString.Substring(0, 4);
            LinqDataAccessDataContext objLinq = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);

            foreach (var item in objLinq.ValidateSiteCode(sTicketSiteCode))
            {
                LocalTicketorSiteCode = item.SiteCode;
                sURL = item.URL;
            }
        }

        public int CheckSDGOfflineTicket(string ticketString)
        {
            return redeemTicketDB.CheckSDGOfflineTicket(ticketString);
        }
        public string GetTicketPrintDevice(string strbarcode, out DateTime PrintDate)
        {
            return redeemTicketDB.GetTicketPrintDevice(strbarcode, out PrintDate);
        }

        public bool ValidateClientSiteCode(string sClientSiteCode)
        {
            LinqDataAccessDataContext objLinq = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
            return Convert.ToBoolean(objLinq.ValidateClientSiteCode(sClientSiteCode));
        }

        public string GetVoucherDetailsToExport(int iVoucherID)
        {
            return redeemTicketDB.GetVoucherDetailsToExport(iVoucherID);
        }

        public string GetVoucherDetailsForCrossTicketing(string Barcode)
        {
            return redeemTicketDB.GetVoucherDetailsForCrossTicketing(Barcode);
        }

        public bool ImportVoucherDetails(RTOnlineTicketDetail TicketDetail)
        {
            return redeemTicketDB.ImportVoucherDetails(TicketDetail);
        }


        public ReedeemTicketDetailsComms RedeemTicketStartComms(ReedeemTicketDetailsComms TicketDetailComms)
        {
            return redeemTicketDB.RedeemTicketStartComms(TicketDetailComms);
        }

        public void CreatePayDeviceID(string stockNo)
        {
            redeemTicketDB.CreatePayDeviceID(stockNo);
        }

        public void UpdateLiabilityStatus(string Barcode, string SiteCode, string Status)
        {
            redeemTicketDB.UpdateLiabilityStatus(Barcode, SiteCode, Status);
        }

        public ReedeemTicketDetailsComms RedeemTicketCompleteComms(ReedeemTicketDetailsComms TicketDetailComms)
        {
            return redeemTicketDB.RedeemTicketCompleteComms(TicketDetailComms);
        }

        public ReedeemTicketDetailsComms RedeemTicketCancelComms(ReedeemTicketDetailsComms TicketDetailComms)
        {
            return redeemTicketDB.RedeemTicketCancelComms(TicketDetailComms);
        }

        public bool ImportVoucherDetailsComms(ReedeemTicketDetailsComms objReedeemTicketDetailsComms)
        {
            return redeemTicketDB.ImportVoucherDetailsComms(objReedeemTicketDetailsComms);
        }

        public RTOnlineTicketDetail GetVoucherDetailForMultipleTicketRedeem(RTOnlineTicketDetail TicketDetailEntity)
        {
            return redeemTicketDB.GetVoucherAmountAndStatusForMultipleTicket(TicketDetailEntity);
        }

        #region GCD
        public bool CancelRedeemTicket(string TicketString)
        {
            return redeemTicketDB.CancelRedeemTicket(TicketString);
        }
        #endregion
    }

    public static class VoucherHelper
    {
        public static bool IsTISPrintedTicketPrefix(string barcode)
        {
            return TISVoucherHelper.IsTISPrintedTicketPrefix(barcode);
        }

        public static bool IsCardRequired(string barcode)
        {
            return TISVoucherHelper.IsCardRequired(barcode);
        }

        public static int CurrentTicketStatus(string barcode)
        {
            return TISVoucherHelper.CurrentTicketStatus(barcode);
        }

        public static bool IsSpecificCardRequired(string barcode)
        {
            return TISVoucherHelper.IsSpecificCardRequired(barcode);
        }

        public static int TicketType(string barcode)
        {
            return TISVoucherHelper.VoucherType(barcode);
        }

        public static string PlayerCardNumber(string barcode)
        {
            return TISVoucherHelper.PlayerCardNumber(barcode);
        }
        public static bool IsTISPrintedTicket(string barcode)
        {
            return TISVoucherHelper.IsTISPrintedTicket(barcode);
        }

        public static bool IsEffectiveDateActivated(string barcode)
        {
            return TISVoucherHelper.IsEffectiveDateActivated(barcode);
        }

        public static TISRedeemTicketResponse SendTISRedeemTicket(string barcode, int userNo)
        {
            return TISVoucherHelper.SendTISRedeemTicket(barcode, userNo);
        }

        public static TISRedeemTicketQueryResponse SendTISRedeemTicketQuery(string barcode, int userNo)
        {
            return TISVoucherHelper.SendTISRedeemTicketQuery(barcode, userNo);
        }

        public static void SendTISVoidTicket(string barcode, int userNo)
        {
            TISVoucherHelper.SendTISVoidTicket(barcode, userNo);
        }

        public static void ForceReceiveDataFromTIS() { TISVoucherHelper.ForceReceiveDataFromTIS(); }

        public static TISInquiryResponseEntity GetInquiryResponse(string barcode)
        {
            return TISVoucherHelper.GetInquiryResponse(barcode);
        }  
    }
}