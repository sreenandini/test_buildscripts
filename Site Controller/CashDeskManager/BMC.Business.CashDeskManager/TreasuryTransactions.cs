using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.DBInterface.CashDeskManager;
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
using System.Data;

namespace BMC.Business.CashDeskManager
{
    /// <summary>
    /// Author:     Anuradha
    /// Purpose:    It helps you to view all cash desk transactions (Validated Tickets, Refills and Refunds) entered in the BMC Cash Desk Operator application for a specific period of time
    /// Created:   01 May 2009
    /// </summary>
    /// 


    public class TreasuryTransactions
    {
        #region FillRouteFilter
        public Dictionary<string, string> FillRouteFilter()
        {
            //Routes

            Dictionary<string, string> dRoutes = DBBuilder.GetRoutes();

            return dRoutes;
        }


        #endregion


        #region FillListOfFilteredPositions
        public List<string> FillListOfFilteredPositions(string RouteNumber)
        {
            return DBBuilder.GetFilteredPositions(RouteNumber);
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
                if (DBBuilder.GetTickets(oTickets) == null && DBBuilder.GetTickets(oTickets).Rows.Count < 0)
                {
                }
                else
                {
                
                    lstTickets = new List<TicketExceptions>();
                    TicketExceptions excep = null;
                    DataTable dtTickets = DBBuilder.GetTickets(oTickets);
                    foreach (DataRow row in dtTickets.Rows)
                    {
                        excep = new TicketExceptions();
                        excep.SEGM = row["PrintDevice"].ToString();
                        excep.Machine = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (!string.IsNullOrEmpty(excep.SEGM) && DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.bExceptionRecordFound = true;
                            excep.Type = "IN";
                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");
                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Asset = row["PrintDevice"].ToString();
                            excep.PayDevice = row["PayDevice"].ToString();
                            excep.CreateCompleted = string.Empty;

                            excep.cTicketTotal += excep.currValue;
                            excep.cExceptionsTotal += excep.currValue;
                        }
                        else if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString()))
                        {
                            excep.bExceptionRecordFound = true;
                            excep.Type = "IN";
                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");
                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
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
                DataTable dtTickets = DBBuilder.GetTickets(oTickets);
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
                        excep.Machine = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (!string.IsNullOrEmpty(excep.SEGM) && DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            if (!string.IsNullOrEmpty(row["strBarCode"].ToString()))
                            {
                                excep.Ticket = row["ActualBarcode"].ToString();
                            }
                            else
                            {
                                excep.Ticket = row["strBarcode"].ToString();
                            }
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;

                            if (!(row["dtPaid"] == null))
                            {
                                excep.PayDate = Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("dd MMM yyyy") + " " +
                                    Convert.ToDateTime(row["dtPaid"] != null ? row["dtPaid"] : string.Empty).ToString("HH:mm");
                            }

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

        public List<TicketExceptions> TitoTicketOutExceptions(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return DBBuilder.TitoTicketOutExceptions(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsClaimed(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return DBBuilder.TitoTicketsClaimed(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsClaimedLiability(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return DBBuilder.TitoTicketsClaimedLiability(oTickets, lstPositionstoDisplay);
        }
        public List<TicketExceptions> TicketsClaimed(TicketsClaimed oTickets,List<string> lstPositionstoDisplay)
        {
            return DBBuilder.TicketsClaimed(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TicketsPrinted(TicketsClaimed oTickets, List<string> lstPositionstoDisplay)
        {
            return DBBuilder.TicketsPrinted(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsPrinted(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return DBBuilder.TitoTicketsPrinted(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsPrintedLiability(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return DBBuilder.TitoTicketsPrintedLiability(oTickets, lstPositionstoDisplay);
        }

        #region "Void/Expired Tickets"

        public List<TicketExceptions> GetTicket_VoidnExpired(Tickets oTickets,List<string> lstPositions)
        {

            string strTicketInException = string.Empty;
            List<TicketExceptions> lstTickets = null;
            TicketExceptions excep = null;


            try
            {
                DataTable dtTickets = DBBuilder.GetTickets(oTickets);
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
                        excep.Machine = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "TITO";
                            excep.Zone = "n/a";

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            excep.PayDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                              Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");
                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = "(" + Convert.ToDouble(row["iAmount"]) / 100 + ")";
   
                            excep.Status = (row["StrVoucherStatus"].ToString()== "NA" ? "Auto Cancelled" : "Void");
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
                DataTable dtTickets = DBBuilder.GetTickets(oTickets);
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
                        excep.Machine = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());

                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;

                        if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "TITO";
                            excep.Zone = "n/a";

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            excep.PayDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                               Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = "(" + Convert.ToDouble(row["iAmount"]) / 100 + ")";

                            excep.Status = (row["StrVoucherStatus"] == "NA" ? "Auto Cancelled" : "Void");
                            excep.cExceptionsTotal += excep.currValue;
                        }
                        else if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString()))
                        {
                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "TITO";
                            excep.Zone = "n/a";

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            excep.PayDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                              Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = "(" + Convert.ToDouble(row["iAmount"]) / 100 + ")";

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
        public List<TicketExceptions> TicketsUnclaimed(TicketsClaimed oTickets, List<string> lstPositions)
        {
            return DBBuilder.TicketsUnClaimed(oTickets, lstPositions);
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
                DataTable dtTickets = DBBuilder.GetPromoTickets(oTickets);
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
                        excep.Machine = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());

                        excep.currValue = (float)Convert.ToDouble(row["iAmount"]) / 100;
                        if (DBCommon.CheckPositionToDisplay(excep.Machine, lstPositions))
                        {
                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "PROMO";
                            excep.Zone = row["GameTitle"].ToString();

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            excep.PayDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                             Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = "(" + Convert.ToDouble(row["iAmount"]) / 100 + ")";

                            excep.Status = (row["StrVoucherStatus"] == "NA" ? "Auto Cancelled" : "Void");
                            excep.cExceptionsTotal += excep.currValue;
                        }
                        else if (DBCommon.IsMachineATicketWorkstation(row["PrintDevice"].ToString()))
                        {
                            excep.Position = DBBuilder.GetBarPositionFromAsset(row["PrintDevice"].ToString());
                            excep.TransactionType = "PROMO";
                            excep.Zone = row["GameTitle"].ToString();

                            excep.PrintDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                                Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            excep.PayDate = Convert.ToDateTime(row["dtPrinted"]).ToString("dd MMM yyyy") + " " +
                              Convert.ToDateTime(row["dtPrinted"]).ToString("HH:mm");

                            excep.Value = Convert.ToDouble(row["iAmount"]) / 100;
                            excep.Amount = "(" + Convert.ToDouble(row["iAmount"]) / 100 + ")";

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
            return DBBuilder.RetrieveTicketAnomalies(oTickets,lstPositions);
        }
        #endregion

        #region TicketExceptions
        public List<TicketExceptions> GetTreasuryItems(Tickets oTickets, List<string> lstPositions)
        {
            return DBBuilder.GetTreasuryItems(oTickets, lstPositions);
        }
        #endregion TicketExceptions


        public string GetRegionFromSite()
        {
            return DBBuilder.GetRegionFromSite();
        }
    }
}
