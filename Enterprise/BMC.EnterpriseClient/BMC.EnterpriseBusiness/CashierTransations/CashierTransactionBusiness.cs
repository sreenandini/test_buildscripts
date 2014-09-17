using System;
using System.Collections.Generic;
using BMC.Common.Utilities;
using Microsoft.Win32;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;
using System.Data;
using BMC.DataAccess;
using BMC.Common.LogManagement;
using System.Linq;
using System.Data.Linq;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseDataAccess.CashierTransations;


namespace BMC.EnterpriseBusiness.Business.CashierTransations
{
    public class BMCCashierTreasuryTransactions
    {
        #region "Declarations"
        
        CashDeskManagerDataAccess cashdeskmanagerDataAccess = new CashDeskManagerDataAccess();
        EnterpriseDataContext oDataContext = EnterpriseDataContextHelper.GetDataContext();
        #endregion


        #region FillListOfFilteredPositions
        public List<string> FillListOfFilteredPositions(string RouteNumber)
        {
            return cashdeskmanagerDataAccess.GetFilteredPositions(RouteNumber);
        }

        public List<CRMGetRoutesBySiteID> GetRoutes(int? SiteID)
        {
            return cashdeskmanagerDataAccess.GetRoutes(SiteID);
        }

        public List<UserDetailsBySiteResult> GetUserDetails(int SiteId)
        {
            List<UserDetailsBySiteResult> lstUserDetails = cashdeskmanagerDataAccess.GetUserDetails(SiteId);
            lstUserDetails.Insert(0, GetNoneItemUseDetails());
            return lstUserDetails;
        }

        #endregion

        #region Tickets
        public List<TicketExceptions> TITOTicketInExceptions(Tickets oTickets)
        {
            //            '
            //'
            //Dim oRs             As adodb.Recordset
            //Dim myItem          As ListItem
            //Dim cTicketTotal    As Currency
            //Dim currValue       As Currency
            string strTicketInException = string.Empty;
            List<TicketExceptions> lstTickets = null;

            try
            {
                DataTable dtTickets = cashdeskmanagerDataAccess.GetTickets(oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                }
                else
                {

                    lstTickets = new List<TicketExceptions>();
                    TicketExceptions excep = null;
                    // DataTable dtTickets = cashdeskmanagerDataAccess.GetTickets(oTickets);
                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.SEGM = row["PrintDevice"].ToString();
                        excep.Machine = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;

                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                        DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);
                        if (!string.IsNullOrEmpty(excep.SEGM))
                        {
                            excep.bExceptionRecordFound = true;
                            excep.Type = "IN";
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());



                            //  excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();
                            excep.PrintDate = dt.ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();
                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = excep.Value.ToString("###0.#0");
                            excep.Asset = row["PrintDevice"].ToString();
                            excep.PayDevice = row["PayDevice"].ToString();
                            excep.CreateCompleted = string.Empty;

                            excep.cTicketTotal += excep.currValue;
                            excep.cExceptionsTotal += excep.currValue;
                        }
                        else if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString(), oTickets.SITE.ToString()))
                        {
                            excep.bExceptionRecordFound = true;
                            excep.Type = "IN";
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                            excep.PrintDate = dt.ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();
                            //  excep.PrintDate = dt.ToString().ReadDateTimeWithSeconds().ToString();

                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = excep.Value.ToString("###0.#0");
                            excep.Asset = row["PrintDevice"].ToString();
                            excep.PayDevice = row["PayDevice"].ToString();
                            excep.CreateCompleted = string.Empty;

                            excep.cTicketTotal += excep.currValue;
                            excep.cExceptionsTotal += excep.currValue;
                        }
                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;

        }
        #endregion Tickets

        #region Tickets
        public List<TicketExceptions> TitoTicketsAll(Tickets oTickets, List<string> lstPositions)
        {
            //            '
            //'
            //Dim oRs             As adodb.Recordset
            //Dim myItem          As ListItem
            //Dim cTicketTotal    As Currency
            //Dim currValue       As Currency
            string strTicketInException = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;


            try
            {
                DataTable dtTickets = cashdeskmanagerDataAccess.GetTickets(oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.SEGM = row["PrintDevice"].ToString();
                        excep.Machine = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (!string.IsNullOrEmpty(excep.SEGM) && DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());

                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                            DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);

                            //   excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();
                            excep.PrintDate = dt.ToString().ReadDateTimeWithSeconds().ToString();


                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = excep.Value.ToString("###0.#0");
                            //
                            if (row["dtPaid"] == null)
                            {
                                excep.PayDate = DateTime.Now.GetUniversalDateTimeFormat();
                            }
                            else
                            {
                                excep.PayDate = row["dtPaid"].ToString().ReadDateTimeWithSeconds().ToString();
                            }
                            //  excep.PayDate = (row["dtPaid"] == null ? string.Empty : string.Empty);


                            excep.Asset = row["PrintDevice"].ToString();

                            excep.PayDevice = (row["PayDevice"] != null ? row["PayDevice"].ToString() : string.Empty);
                            excep.CreateCompleted = string.Empty;
                            excep.Status = (row["StrVoucherStatus"] != null ? row["StrVoucherStatus"].ToString() : string.Empty);
                        }

                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;

        }



        #endregion

        public List<TicketExceptions> TitoTicketOutExceptions(Tickets oTickets)
        {
            return cashdeskmanagerDataAccess.TitoTicketOutExceptions(oTickets);
        }

        public List<TicketExceptions> TitoTicketsClaimed(Tickets oTickets)
        {
            return cashdeskmanagerDataAccess.TitoTicketsClaimed(oTickets);
        }

        public List<TicketExceptions> TitoTicketsClaimedLiability(Tickets oTickets)
        {
            return cashdeskmanagerDataAccess.TitoTicketsClaimedLiability(oTickets);
        }
        public List<TicketExceptions> TicketsClaimed(TicketsClaimed oTickets)
        {
            return cashdeskmanagerDataAccess.TicketsClaimed(oTickets);
        }

        public List<TicketExceptions> TicketsPrinted(TicketsClaimed oTickets)
        {
            return cashdeskmanagerDataAccess.TicketsPrinted(oTickets);
        }

        public List<TicketExceptions> TitoTicketsPrinted(Tickets oTickets)
        {
            return cashdeskmanagerDataAccess.TitoTicketsPrinted(oTickets);
        }

        public List<TicketExceptions> TitoTicketsPrintedLiability(Tickets oTickets)
        {
            return cashdeskmanagerDataAccess.TitoTicketsPrintedLiability(oTickets);
        }

        #region "Void/Expired Tickets"

        public List<TicketExceptions> GetTicket_VoidnExpired(Tickets oTickets)
        {

            string strTicketInException = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;


            try
            {
                DataTable dtTickets = cashdeskmanagerDataAccess.GetTickets(oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.SEGM = row["PrintDevice"].ToString();
                        excep.Machine = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;

                        excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                        excep.TransactionType = "Voucher";
                        excep.Zone = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Zone, "", oTickets.SITE.ToString());//"n/a";

                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                        DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);


                        //  excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();
                        excep.PrintDate = dt.ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();

                        excep.PayDate = dt.ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();
                        excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                        //excep.Amount = (Convert.ToDouble(row["iAmount"]) / 100).ToString("###0.#0") +")";
                        excep.Amount = (Convert.ToDouble(row["iAmount"]) / 100).ToString("###0.#0");

                        excep.Status = (row["StrVoucherStatus"].ToString().Trim().ToUpper() == "NA" ? "Auto Cancelled" :
                            row["StrVoucherStatus"].ToString().Trim().ToUpper() == "VD" ? "Void" : row["StrVoucherStatus"].ToString().Trim().ToUpper() == "EXP" ? "Expired" : "Expired");
                        excep.cExceptionsTotal += excep.currValue;


                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }
        #endregion

        #region "Active Tickets"

        #region TitoTicketsUnclaimed
        public List<TicketExceptions> TitoTicketsUnclaimed(Tickets oTickets)
        {

            string strTicketInException = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;


            try
            {
                oTickets.Type = "U";
                DataTable dtTickets = cashdeskmanagerDataAccess.GetTickets(oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.SEGM = row["PrintDevice"].ToString();
                        excep.Machine = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());

                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;


                        excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                        excep.TransactionType = "Voucher";
                        excep.Zone = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Zone, "", oTickets.SITE.ToString());//"n/a";

                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                        DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);

                        //excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();

                        excep.PayDate = dt.ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();
                        excep.PrintDate = dt.ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();


                        //excep.PayDate = dt.ToString().ReadDateTimeWithSeconds().ToString();


                        excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                        excep.Amount = (Convert.ToDouble(row["iAmount"]) / 100).ToString("###0.#0");

                        excep.Status = (row["StrVoucherStatus"] == "NA" ? "Auto Cancelled" : "Void");
                        excep.cExceptionsTotal += excep.currValue;

                        if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString(), oTickets.SITE.ToString()))
                        {
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                            excep.TransactionType = "Voucher";
                            excep.Zone = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Zone, "", oTickets.SITE.ToString());//"n/a";

                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                            DateTime dtP = DateTime.Parse(row["dtPrinted"].ToString(),
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);


                            //excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();

                            //excep.PayDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();

                            excep.PrintDate = dtP.ToString().ReadDateTimeWithSeconds().ToString();

                            excep.PayDate = dtP.ToString().ReadDateTimeWithSeconds().ToString();

                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = (Convert.ToDouble(row["iAmount"]) / 100).ToString("###0.#0");

                            excep.Status = (row["StrVoucherStatus"] == "NA" ? "Auto Cancelled" : "Void");
                            excep.cExceptionsTotal += excep.currValue;
                        }

                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }
        #endregion //Tito Tickets Unclaimed

        #region TicketsUnclaimed
        public List<TicketExceptions> TicketsUnclaimed(TicketsClaimed oTickets)
        {
            return cashdeskmanagerDataAccess.TicketsUnClaimed(oTickets);
        }
        #endregion TicketsUnclaimed
        #endregion //Active Tickets

        #region "PromoCashable Tickets"
        public List<TicketExceptions> GetPromoCashableTickets(TicketsClaimed oTickets)
        {

            string strTicketInException = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;


            try
            {
                DataTable dtTickets = cashdeskmanagerDataAccess.GetPromoTickets(oTickets);
                if (dtTickets == null && dtTickets.Rows.Count < 0)
                {
                }
                else
                {
                    lstTickets = new List<TicketExceptions>();

                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.SEGM = row["PrintDevice"].ToString();
                        excep.Machine = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());

                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;

                        excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                        excep.TransactionType = "PROMO";
                        excep.Zone = row["GameTitle"].ToString();

                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                        DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);


                        //excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();

                        //excep.PayDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();

                        excep.PrintDate = dt.ToString().ReadDateTimeWithSeconds().ToString();

                        excep.PayDate = dt.ToString().ReadDateTimeWithSeconds().ToString();

                        excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                        excep.Amount = (Convert.ToDouble(row["iAmount"]) / 100).ToString("###0.#0");

                        excep.Status = (row["StrVoucherStatus"] == "NA" ? "Auto Cancelled" : "Void");
                        excep.cExceptionsTotal += excep.currValue;

                        if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString(), oTickets.SITE.ToString()))
                        {
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(CashDeskManagerDataAccess.ReqType.Machine, row["PrintDevice"].ToString(), oTickets.SITE.ToString());
                            excep.TransactionType = "PROMO";
                            excep.Zone = row["GameTitle"].ToString();

                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                            DateTime dtP = DateTime.Parse(row["dtPrinted"].ToString(),
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);

                            excep.PrintDate = dtP.ToString().ReadDateTimeWithSeconds().ToString();

                            excep.PayDate = dtP.ToString().ReadDateTimeWithSeconds().ToString();

                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = (Convert.ToDouble(row["iAmount"]) / 100).ToString("###0.#0");

                            excep.Status = (row["StrVoucherStatus"] == "NA" ? "Auto Cancelled" : "Void");
                            excep.cExceptionsTotal += excep.currValue;
                        }

                        lstTickets.Add(excep);
                    }
                }

            }

            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return lstTickets;
        }
        #endregion "PromoCashable Tickets"

        #region "Ticket Anomalies"
        public List<TicketExceptions> GetTicketAnomalies(TicketsClaimed oTickets, List<string> lstPositions)
        {
            return cashdeskmanagerDataAccess.RetrieveTicketAnomalies(oTickets, lstPositions);
        }
        #endregion

        #region TicketExceptions
        public List<TicketExceptions> GetTreasuryItems(Tickets oTickets)
        {
            return cashdeskmanagerDataAccess.GetTreasuryItems(oTickets);
        }
        #endregion TicketExceptions


        public string GetRegionFromSite()
        {
            return cashdeskmanagerDataAccess.GetRegionFromSite();
        }

        public bool GetHopperSetting()
        {
            //return false;
            return (Convert.ToBoolean(cashdeskmanagerDataAccess.GetHopperSetting().ToLower()));
        }

        public bool ClearTicketStatus(string Ticket, string DeviceID)
        {
            return cashdeskmanagerDataAccess.ClearTicketStatus(Ticket, DeviceID);
        }


        public List<Company> GetCompanyDetails(int SecurityUserID)
        {
            return oDataContext.GetCompanyDetails(SecurityUserID).ToList();

        }
        public List<SubCompany> GetSubCompany(int CompanyID)
        {
            return oDataContext.GetSubCompanyDetails(CompanyID).ToList();
        }

        public List<Site> GetSites(int subcmp, int region, int area)
        {
            return oDataContext.GetSiteDetails(subcmp, region, area).ToList();
        }

        public string GetBMCVersion()
        {
            foreach (var result in oDataContext.GetBMCVersion())
                return result.Result;

            return "12.1";
        }

        public void GetSite_Code_Name(int SITEID, out string SITECODE, out string SITENAME)
        {
            SITECODE = string.Empty;
            SITENAME = string.Empty;

            foreach (var Result in oDataContext.GetSite_Code_Name(SITEID))
            {
                SITECODE = Result.Site_Code;
                SITENAME = Result.Site_Name;
            }

        }


        public DataSet GetCashDeskReconcilationDetails(DateTime StartDate, DateTime EndDate,int SiteID,int RouteNo, int UserNo)
        {
            return cashdeskmanagerDataAccess.GetCashDeskReconcilationDetails(StartDate, EndDate, SiteID, RouteNo, UserNo);
            return cashdeskmanagerDataAccess.GetCashDeskReconcilationDetails(StartDate, EndDate, SiteID, RouteNo, UserNo);


        }


        public DataSet GetSystemBalancingDetails(DateTime StartDate, DateTime EndDate, int _SITEID, int RouteNo, int UserNo)
        {
            return cashdeskmanagerDataAccess.GetSystemBalancingDetails(StartDate, EndDate, _SITEID, RouteNo, UserNo);
        }

        public string GetRegion_Site(int SITEID)
        {
            
            foreach (var Result in oDataContext.GetRegion_Site(SITEID))
            {
                return Result.REGION;
            }

            return "";

        }

        public DataSet GetCashDeskMovementDetails(DateTime StartDate, DateTime EndDate, int _SITEID,string sRegion,int RouteNo, int UserNo)
        {
            return cashdeskmanagerDataAccess.GetCashDeskMovementDetails(StartDate, EndDate, _SITEID, sRegion, RouteNo, UserNo);
        }

        internal SiteCultureInfo GetSiteCulture(int userID)
        {
            foreach (var result in oDataContext.GetSiteCulture(userID))
                return result;

            return null;

            //return new SiteCultureInfo(string.Empty, "en-US", "en-US", "en-US");
        }

        public bool CheckViewTicketAccess(int UserID)
        {
            var result = cashdeskmanagerDataAccess.CheckAccessForViewTicketNumber(UserID);
            return result;
        }

        internal ISingleResult<CashierTransactionsDA> GetCashierTransactions(DateTime startdate,DateTime enddate,int siteid,int RouteNo, int UserNo)
        {
            return oDataContext.GetCashierTransactions(startdate, enddate, siteid, RouteNo, UserNo);

        }

        public ISingleResult<rsp_GetCashierTransactionSettingsResult> GetInitialSettings()
        {
            return oDataContext.GetInitialSettings2();
        }

        internal DataSet GetCashierTransactionsDetails(bool? isCDMPaid, bool? isCDMPrinted, bool? isHandPay, bool? isShortPay,bool? isVoidVoucher, bool? isJackpot,
            bool? isProgressive, bool? isVoid, bool? isMachinePaid, bool? isMachinePrinted,
             bool? isActive, bool? isVoidCancel, bool? isExpired, bool? isException, bool? isLiability,bool? isPromo,
             bool? isNonCashableIN, bool? isNonCashableOut, DateTime startDate, DateTime endDate, int isiteid,int Route_No,bool? isOffline)
        {
            return cashdeskmanagerDataAccess.GetCashierTransactionsDetails(isCDMPaid, isCDMPrinted, isHandPay, isShortPay, isVoidVoucher, isJackpot,
                                            isProgressive,  isVoid,  isMachinePaid,  isMachinePrinted, 
                                             isActive,  isVoidCancel,  isExpired,  isException,  isLiability,
                                            isPromo, isNonCashableIN, isNonCashableOut, startDate, endDate, isiteid, Route_No, isOffline);
        }

        public DataSet GetCashierHistory( DateTime startDate, DateTime endDate, int nSiteID, int Route_No, int User_No)
        {
            return cashdeskmanagerDataAccess.GetCashierHistory(startDate, endDate, nSiteID, Route_No, User_No);
        }
        public DataSet GetCashierHistory_Details(
                   DateTime startDate, DateTime endDate, int nSiteID, int Route_No, int User_No, bool isCDMPaid, bool isCDMPrinted, bool isHandPay, bool isShortpay
                , bool isVoidVoucher, bool isJackpot, bool isProgressive, bool isVoid, bool isMachinePaid, bool isMachinePrinted, bool isActive, bool isVoidCancel, bool isExpired
                , bool isException, bool isLiability, bool isPromo, bool isNonCashableIN, bool isNonCashableOut, bool isOffline

            )
        {
            return cashdeskmanagerDataAccess.GetCashierHistory_Details(startDate, endDate, nSiteID, Route_No, User_No,isCDMPaid, isCDMPrinted, isHandPay, isShortpay
                , isVoidVoucher, isJackpot, isProgressive, isVoid, isMachinePaid, isMachinePrinted, isActive, isVoidCancel, isExpired
                , isException, isLiability, isPromo, isNonCashableIN, isNonCashableOut, isOffline);
        }
  

        internal UserDetailsBySiteResult GetNoneItemUseDetails()
        {
            try
            {
                UserDetailsBySiteResult objUserDetailsBySiteResult = new UserDetailsBySiteResult
                {
                    SecurityUserID = 0,
                    UserName = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_AllCriteria")//"-ALL-"
                };
                return objUserDetailsBySiteResult;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new UserDetailsBySiteResult();
            }
        }
    }
}
