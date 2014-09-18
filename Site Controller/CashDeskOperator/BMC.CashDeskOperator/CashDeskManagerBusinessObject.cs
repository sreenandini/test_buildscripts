using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Business.CashDeskOperator;
using System.Windows.Controls;
using BMC.Transport;
using BMC.Common.ExceptionManagement;

namespace BMC.CashDeskOperator
{
    public class CashDeskManagerBusinessObject : ICashDeskManager
    {
        #region Declarations
        TreasuryTransactions objCashDesk = new TreasuryTransactions();
        CashDeskmanagerCommon objCommon = new CashDeskmanagerCommon();
        PrintUtility objPrint = new PrintUtility();
        #endregion

        public CashDeskManagerBusinessObject()
        {
        }

        #region ICashDeskManager Members


        public List<TicketExceptions> TITOTicketInExceptions(Tickets oTickets, List<string> lstPositions)
        {
            return objCashDesk.TITOTicketInExceptions(oTickets, lstPositions);
        }

        public Dictionary<string, string> FillRouteFilter()
        {
            return objCashDesk.FillRouteFilter();
        }

        public List<TicketExceptions> TitoTicketsAll(Tickets oTickets, List<string> lstPositions)
        {
            return objCashDesk.TitoTicketsAll(oTickets, lstPositions);
        }

        public List<TicketExceptions> TitoTicketOutExceptions(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return objCashDesk.TitoTicketOutExceptions(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsClaimed(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return objCashDesk.TitoTicketsClaimed(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsClaimedLiability(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return objCashDesk.TitoTicketsClaimedLiability(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TicketsClaimed(TicketsClaimed oTickets, List<string> lstPositionstoDisplay)
        {
            return objCashDesk.TicketsClaimed(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TicketsPrinted(TicketsClaimed oTickets, List<string> lstPositionstoDisplay)
        {
            return objCashDesk.TicketsPrinted(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsPrinted(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return objCashDesk.TitoTicketsPrinted(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> TitoTicketsPrintedLiability(Tickets oTickets, List<string> lstPositionstoDisplay)
        {
            return objCashDesk.TitoTicketsPrintedLiability(oTickets, lstPositionstoDisplay);
        }

        public List<TicketExceptions> GetTicket_VoidnExpired(Tickets oTickets, List<string> lstPositions)
        {
            return objCashDesk.GetTicket_VoidnExpired(oTickets, lstPositions);
        }

        public List<TicketExceptions> TitoTicketsUnclaimed(Tickets oTickets, List<string> lstPositions)
        {
            return objCashDesk.TitoTicketsUnclaimed(oTickets, lstPositions);
        }

        public List<TicketExceptions> TicketsUnclaimed(TicketsClaimed oTickets, List<string> lstPositions)
        {
            return objCashDesk.TicketsUnclaimed(oTickets, lstPositions);
        }

        public List<TicketExceptions> GetPromoCashableTickets(TicketsClaimed oTickets, List<string> lstPositions)
        {
            return objCashDesk.GetPromoCashableTickets(oTickets, lstPositions);
        }

        public List<TicketExceptions> GetTicketAnomalies(TicketsClaimed oTickets, List<string> lstPositions)
        {
            return objCashDesk.GetTicketAnomalies(oTickets, lstPositions);
        }

        public List<TicketExceptions> GetTreasuryItems(Tickets oTickets, List<string> lstPositions)
        {
            return objCashDesk.GetTreasuryItems(oTickets, lstPositions);
        }

        public string GetRegionFromSite()
        {
            return objCashDesk.GetRegionFromSite();
        }

        public List<RouteCollection> GetRouteCollection()
        {
            List<RouteCollection> lstRouteCollection = objCashDesk.GetRouteCollection();
            lstRouteCollection.Insert(0, GetNoneItemRouteCollection());
            return lstRouteCollection;
        }

        #endregion

        #region Public Static Function
        public static ICashDeskManager CreateInstance()
        {
            return new CashDeskManagerBusinessObject();
        }
        #endregion

        #region ICashDeskManager Members

        public List<string> FillListOfFilteredPositions(string RouteNumber)
        {
            return objCashDesk.FillListOfFilteredPositions(RouteNumber);
        }

        #endregion

        #region ICashDeskManager Members


        public bool isValidDateRange(DateTime dStartdate, DateTime dEndDate)
        {
            return objCommon.isValidDateRange(dStartdate, dEndDate);
        }

        public bool ExportToExcel(ListView lvView, string path)
        {
            return objCommon.ExportToExcel(lvView, path);
        }

        public bool HourlyExportToExcel(ListView lvView, string path, bool IsMainScreen)
        {
            return objCommon.HourlyExportToExcel(lvView, path, IsMainScreen);
        }

        public bool HourlyExportToExcel(Microsoft.Windows.Controls.DataGrid lvView, string path, bool IsMainScreen)
        {
            return objCommon.HourlyExportToExcel(lvView, path, IsMainScreen);
        }
        #endregion

        #region ICashDeskManager Members


        public void CloseExcel()
        {
            objCommon.CloseExcel();
        }

        #endregion

        #region ICashDeskManager Members


        public void PrintFunction(System.Windows.Controls.ListView lvView,DateTime StartDate,DateTime EndDate)
        {
            objPrint.PrintFunction(lvView, StartDate, EndDate);
        }

         public void PrintFunction(System.Windows.Controls.ListView lvView,DateTime StartDate,DateTime EndDate, bool isPrintDate, bool isTransactionType, bool isZone,
            bool  isPos,bool  isMachine,bool  isAsset,bool  isAmount,bool  isTicketPrintedDate,bool  isDetails,string screenName)
        {
            objPrint.PrintFunction(lvView, StartDate, EndDate,  isPrintDate,  isTransactionType,  isZone,
              isPos,  isMachine,  isAsset,  isAmount,  isTicketPrintedDate,  isDetails, screenName);
        }

        #endregion

        #region ICashDeskManager Members


        public bool ShowHopper()
        {
            return (objCashDesk.GetHopperSetting());
        }

        public bool IsRegulatoryEnabled()
        {
            return (objCashDesk.IsRegulatoryEnabled());
        }


        public bool ClearTicketStatus(string Ticket, string DeviceID)
        {
            return (objCashDesk.ClearTicketStatus(Ticket, DeviceID));
        }
        #endregion

        #region ICashDeskManager Members


        public Dictionary<string, bool> ActivateSDGTicket(string Ticket, string DeviceID, bool iStatus)
        {
            return objCashDesk.ActivateSDGTicket(Ticket, DeviceID, iStatus);
        }

        #endregion

        #region ICashDeskManager Members


        public List<User> GetListOfUsers(int UserNo)
        {
            return objCashDesk.GetListOfUsers(UserNo).OrderBy(usr => usr.UserName).ToList();
        }

        #endregion

        #region ICashDeskManager Members


        public List<User> GetListOfUsersRoles(int UserNo)
        {
            return objCashDesk.GetListOfUsersRoles(UserNo);
        }

        #endregion

        private RouteCollection GetNoneItemRouteCollection()
        {
            try
            {
                RouteCollection objRouteCollection = new RouteCollection
                {
                    Route_No = 0,
                    Route_Name = "ALL",
                    Route_Default = false
                };
                return objRouteCollection;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new RouteCollection();
            }
        }
    }
}
