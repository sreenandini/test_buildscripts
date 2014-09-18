using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.Utilities;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
using System.Data;
using BMC.Common.Utilities;
using System.Data.Linq;

namespace BMC.Business.CashDeskOperator
{
    /// <summary>
    /// Author:     Anuradha
    /// Purpose:    It helps you to view all cash desk transactions (Validated Tickets, Refills and Refunds) entered in the BMC Cash Desk Operator application for a specific period of time
    /// Created:   01 May 2009
    /// </summary>
    /// 


    public class TreasuryTransactions 
    {
        #region "Declarations"
        CashDeskManagerDataAccess cashdeskmanagerDataAccess = new CashDeskManagerDataAccess();
        LinqDataAccessDataContext linqDB = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
        string CurrencySymbol = string.Empty;
        #endregion

        public TreasuryTransactions()
        {
            CurrencySymbol = CurrencySymbol.GetCurrencySymbol();
        }
        #region FillRouteFilter
        public Dictionary<string, string> FillRouteFilter()
        {
            //Routes

            Dictionary<string, string> dRoutes = cashdeskmanagerDataAccess.GetRoutes();

            return dRoutes;
        }


        #endregion


        #region FillListOfFilteredPositions
        public List<string> FillListOfFilteredPositions(string RouteNumber)
        {
            return cashdeskmanagerDataAccess.GetFilteredPositions(RouteNumber);
        }
        #endregion

        #region Tickets
        public List<TicketExceptions> TITOTicketInExceptions(Tickets oTickets, List<string> lstPositions)
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
                DataTable dtTickets=cashdeskmanagerDataAccess.GetTickets(oTickets);
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
                        excep.Machine = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;

                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                        DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);

                        if (!string.IsNullOrEmpty(excep.SEGM) && DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.bExceptionRecordFound = true;
                            excep.Type = "IN";
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());

                           

                          //  excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();
                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();
                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                           // excep.Amount = excep.Value.ToString("###0.#0");
                            excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();

                            excep.Asset = row["Asset"].ToString();
                            excep.PayDevice = row["PayDevice"].ToString();
                            excep.CreateCompleted = string.Empty;
                            excep.DeviceID = row["DeviceID"].ToString();

                            excep.cTicketTotal += excep.currValue;
                            excep.cExceptionsTotal += excep.currValue;
                        }
                        else if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString()))
                        {
                            excep.bExceptionRecordFound = true;
                            excep.Type = "IN";
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();
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
                            excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();

                            //excep.Amount = excep.Value.ToString("###0.#0");
                            excep.Asset = row["Asset"].ToString();
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
                        excep.Machine = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (!string.IsNullOrEmpty(excep.SEGM) && DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());

                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                            DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);

                         //   excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();
                            excep.PrintDate = row["dtPrinted"].ToString().ToShortDateTimeString();


                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = CurrencySymbol + " " + Convert.ToDecimal(excep.Value).GetUniversalCurrencyFormat();

//excep.Amount = excep.Value.ToString("###0.#0");
                            if (row["dtPaid"] == null)
                            {
                                excep.PayDate = DateTime.Now.GetUniversalDateTimeFormat();
                            }
                            else 
                            {
                                excep.PayDate = row["dtPaid"].ToString().ToShortDateTimeString();
                            }
                          //  excep.PayDate = (row["dtPaid"] == null ? string.Empty : string.Empty);


                            excep.Asset = row["Asset"].ToString();

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
        #region "user"
        public List<User> GetListOfUsers(int UserNo)
        {
            return cashdeskmanagerDataAccess.GetListOfUsers(UserNo);
        }

        public List<User> GetListOfUsersRoles(int UserNo)
        {
            return cashdeskmanagerDataAccess.GetListOfUsersRoles(UserNo);
        }
        #endregion

        #region Route

        public List<RouteCollection> GetRouteCollection()
        {
            return linqDB.GetRouteCollection().ToList();
        }

        #endregion //Route

        public List<TicketExceptions> TitoTicketOutExceptions(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return cashdeskmanagerDataAccess.TitoTicketOutExceptions(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsClaimed(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return cashdeskmanagerDataAccess.TitoTicketsClaimed(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsClaimedLiability(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return cashdeskmanagerDataAccess.TitoTicketsClaimedLiability(oTickets, lstPositionstoDisplay);
        }
        public List<TicketExceptions> TicketsClaimed(TicketsClaimed oTickets,List<string> lstPositionstoDisplay)
        {
            return cashdeskmanagerDataAccess.TicketsClaimed(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TicketsPrinted(TicketsClaimed oTickets, List<string> lstPositionstoDisplay)
        {
            return cashdeskmanagerDataAccess.TicketsPrinted(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsPrinted(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return cashdeskmanagerDataAccess.TitoTicketsPrinted(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsPrintedLiability(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return cashdeskmanagerDataAccess.TitoTicketsPrintedLiability(oTickets, lstPositionstoDisplay);
        }

        #region "Void/Expired Tickets"

        public List<TicketExceptions> GetTicket_VoidnExpired(Tickets oTickets,List<string> lstPositions)
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
                        excep.Machine = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "Voucher";
                            excep.Zone = "n/a";

                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                            DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);
                            excep.Asset = row["Asset"].ToString();

                          //  excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();
                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();

                            excep.PayDate = Convert.ToDateTime(row["dtExpire"]).ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            //excep.Amount = (Convert.ToDouble().ToString("###0.#0") +")";
                            excep.Amount = CurrencySymbol + " " + (Convert.ToDecimal(row["iAmount"]) / 100).GetUniversalCurrencyFormat();

                            excep.Status = (row["StrVoucherStatus"].ToString().Trim().ToUpper() == "NA" ? "Auto Cancelled" :
                                row["StrVoucherStatus"].ToString().Trim().ToUpper() == "VD" ? "Void" : row["StrVoucherStatus"].ToString().Trim().ToUpper() == "EXP" ? "Expired" : "Expired");
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
        #endregion

        #region "Active Tickets"

        #region TitoTicketsUnclaimed
        public List<TicketExceptions> TitoTicketsUnclaimed(Tickets oTickets, List<string> lstPositions)
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
                        excep.Machine = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());

                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;

                        if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "Voucher";
                            excep.Zone = "n/a";

                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                            DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);

                            //excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();

                            excep.PayDate = row["dtPrinted"].ToString().ToShortDateTimeString();
                            excep.PrintDate = row["dtPrinted"].ToString().ToShortDateTimeString();
                          

                            //excep.PayDate = dt.ToString().ReadDateTimeWithSeconds().ToString();


                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = CurrencySymbol + " " + (Convert.ToDecimal(row["iAmount"]) / 100).GetUniversalCurrencyFormat();

                            excep.Status = (row["StrVoucherStatus"] == "NA" ? "Auto Cancelled" : "Void");
                            excep.cExceptionsTotal += excep.currValue;
                        }
                        else if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString()))
                        {
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "Voucher";
                            excep.Zone = "n/a";

                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                            DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);


                            //excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();

                            //excep.PayDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();

                            excep.PrintDate = row["dtPrinted"].ToString().ToShortDateTimeString();

                            excep.PayDate = row["dtPrinted"].ToString().ToShortDateTimeString();

                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = CurrencySymbol + " " + (Convert.ToDecimal(row["iAmount"]) / 100).GetUniversalCurrencyFormat();

                            excep.Status = (row["StrVoucherStatus"] == "NA" ? "Auto Cancelled" : "Void");
                            excep.cExceptionsTotal += excep.currValue;
                            excep.Asset = row["Asset"].ToString();
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
        public List<TicketExceptions> TicketsUnclaimed(TicketsClaimed oTickets, List<string> lstPositions)
        {
            return cashdeskmanagerDataAccess.TicketsUnClaimed(oTickets, lstPositions);
        }
        #endregion TicketsUnclaimed
        #endregion //Active Tickets

        #region "PromoCashable Tickets"
        public List<TicketExceptions> GetPromoCashableTickets(TicketsClaimed oTickets, List<string> lstPositions)
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
                        excep.Machine = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());

                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "PROMO";
                            excep.Zone = row["GameTitle"].ToString();

                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                            DateTime dt = DateTime.Parse(row["dtPrinted"].ToString(),
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);


                            //excep.PrintDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();

                            //excep.PayDate = row["dtPrinted"].ToString().ReadDateTimeWithSeconds().ToString();

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();

                            excep.PayDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();

                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = CurrencySymbol + " " + (Convert.ToDecimal(row["iAmount"]) / 100).GetUniversalCurrencyFormat();

                            excep.Status = (row["StrVoucherStatus"] == "NA" ? "Auto Cancelled" : "Void");
                            excep.cExceptionsTotal += excep.currValue;
                        }
                        else if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString()))
                        {
                            excep.Position = cashdeskmanagerDataAccess.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "PROMO";
                            excep.Zone = row["GameTitle"].ToString();

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();

                            excep.PayDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy HH:mm:ss").ReadDateTimeWithSeconds().ToString();

                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            //excep.Amount = (Convert.ToDouble(row["iAmount"]) / 100).ToString("###0.#0") ;
                            excep.Amount = CurrencySymbol + " " + (Convert.ToDecimal(row["iAmount"]) / 100).GetUniversalCurrencyFormat();

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
        public List<TicketExceptions> GetTicketAnomalies(TicketsClaimed oTickets,List<string> lstPositions)
        {
            return cashdeskmanagerDataAccess.RetrieveTicketAnomalies(oTickets,lstPositions);
        }
        #endregion

        #region TicketExceptions
        public List<TicketExceptions> GetTreasuryItems(Tickets oTickets, List<string> lstPositions)
        {
            return cashdeskmanagerDataAccess.GetTreasuryItems(oTickets, lstPositions);
        }
        #endregion TicketExceptions

  #region Activate SDG Ticket
        public Dictionary<string,bool> ActivateSDGTicket(string Ticket, string DeviceID,bool iStatus)
                {
                    return cashdeskmanagerDataAccess.ActivateSDGTicket(Ticket, DeviceID, iStatus);
                }
  #endregion Activate SDG Ticket



        public string GetRegionFromSite()
        {
            return cashdeskmanagerDataAccess.GetRegionFromSite();
        }

        public bool GetHopperSetting()
        {
            //return false;
            return (Convert.ToBoolean(cashdeskmanagerDataAccess.IsHopperSetting()));
        }

        public bool IsRegulatoryEnabled()
        {
            //return false;
            return (Convert.ToBoolean(cashdeskmanagerDataAccess.IsRegulatoryEnabled()));
        }


        public bool ClearTicketStatus(string Ticket,string DeviceID)
        {
            return cashdeskmanagerDataAccess.ClearTicketStatus(Ticket, DeviceID);
        }


        public ISingleResult<CashierTransactions> GetCashierTransactions(DateTime startDate, DateTime endDate, int UserNo, int iRoute_No)
        {
            LinqDataAccessDataContext objLinq = new LinqDataAccessDataContext(CommonDataAccess.TicketingConnectionString);
            return objLinq.GetCashierTransactions(startDate, endDate, UserNo, iRoute_No);
        }

        public ISingleResult<CashierTransactionsDetails> GetCashierTransactionsDetails(
           bool? isCDMPaid, bool? isCDMPrinted, bool? isHandPay, bool? isShortpay, bool? isVoidVoucher, bool? isJackpot, bool? isProgressive, bool? isVoid,
           bool? isMachinePaid, bool? isMachinePrinted,
            bool? isActive, bool? isVoidCancel, bool? isExpired, bool? isException, bool? isLiability, bool? isPromo,
            bool? isNonCashableIN, bool? isNonCashableOut,
           DateTime startDate, DateTime endDate, int UserNo, int iRoute_No,bool? chkOfflinevoucher)
        {
            LinqDataAccessDataContext objLinq = new LinqDataAccessDataContext(CommonDataAccess.TicketingConnectionString);

            return objLinq.GetCashierTransactionsDetails(isCDMPaid, isCDMPrinted, isHandPay, isShortpay, isVoidVoucher,isJackpot, isProgressive, isVoid,
                                                          isMachinePaid, isMachinePrinted, 
                                                           isActive, isVoidCancel, isExpired, isException, isLiability, isPromo,
                                                          isNonCashableIN, isNonCashableOut,
                                                          startDate, endDate, UserNo, iRoute_No,chkOfflinevoucher);
        }

        public CashierHistory GetCashierTransactionsData(DateTime startDate, DateTime endDate, int UserNo, int iRoute_No)
        {
            LinqDataAccessDataContext objLinq = new LinqDataAccessDataContext(CommonDataAccess.TicketingConnectionString);
            CashierHistory cdo = new CashierHistory();
            var Result = objLinq.GetCashierTransactionsSummary_New(startDate, endDate, UserNo, iRoute_No);
            //cdo.Details = Result.GetResult<rsp_CDM_GetCashierTransactionsDetails_Details>().ToList();
            cdo.Summary = Result.ToList();
            return cdo;
        }

        public List<rsp_CDM_GetCashierTransactionsDetails_New> rsp_CDM_GetCashierTransactionsDetails_New(DateTime startDate, DateTime endDate, int UserNo, int iRoute_No,
           bool isCDMPaid,
           bool isCDMPrinted,
           bool isHandPay,
           bool isShortpay,
           bool isVoidVoucher,
           bool isJackpot,
           bool isProgressive,
           bool isVoid,
           bool isMachinePaid,
           bool isMachinePrinted,
           bool isActive,
           bool isVoidCancel,
           bool isExpired,
           bool isException,
           bool isLiability,
           bool isPromo,
           bool isNonCashableIN,
           bool isNonCashableOut,
           bool isOffline,
           bool isOutstandingHandpays)
        {
            LinqDataAccessDataContext objLinq = new LinqDataAccessDataContext(CommonDataAccess.TicketingConnectionString);
        
            var Result = objLinq.rsp_CDM_GetCashierTransactionsDetails_New(startDate, endDate, UserNo, iRoute_No,
                 isCDMPaid,
                 isCDMPrinted,
                 isHandPay,
                 isShortpay,
                 isVoidVoucher,
                 isJackpot,
                 isProgressive,
                 isVoid,
                 isMachinePaid,
                 isMachinePrinted,
                 isActive,
                 isVoidCancel,
                 isExpired,
                 isException,
                 isLiability,
                 isPromo,
                 isNonCashableIN,
                 isNonCashableOut,isOffline,
                 isOutstandingHandpays);
                //cdo.Details = Result.GetResult<rsp_CDM_GetCashierTransactionsDetails_Details>().ToList();
            return  Result.ToList();
        }


    }
}
