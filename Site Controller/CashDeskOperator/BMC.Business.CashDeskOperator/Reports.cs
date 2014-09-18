using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.DBInterface.CashDeskOperator;
using System.Data;
using System.Data.SqlClient;
using BMC.Transport;
using BMC.CommonLiquidation.Utilities;

namespace BMC.Business.CashDeskOperator
{
    public class Reports
    {
        #region Private Variables

        private ReportsDataAccess reportsDataAccess = null;

        #endregion Private Variables

        #region Constructor

        public Reports()
        {
            reportsDataAccess = new ReportsDataAccess();
        }

        public Reports(string ExchangeConnectionString, string TicketingConnectionString)
        {
            reportsDataAccess = new ReportsDataAccess(CommonUtilities.SiteConnectionString(ExchangeConnectionString), CommonUtilities.TicketingConnectionString(TicketingConnectionString));
        }
        #endregion Constructor

        #region Public Methods

        public DataSet GetJackpotSlipSummaryDetails(DateTime reportStartDateTime, DateTime reportEndDateTime, 
            bool? ShowHandpay,bool? ShowJackpot)
        {
            return reportsDataAccess.GetJackpotSlipSummaryDetails(reportStartDateTime, reportEndDateTime,
                ShowHandpay,ShowJackpot);
        }


        public DataSet GetVoucherCouponLiabilityReport(DateTime issueDate, string sDeviceType, string sVoucherStatus)
        {
            return reportsDataAccess.GetVoucherCouponLiabilityReport(issueDate, sDeviceType, sVoucherStatus);
        }

        public DataSet GetExceptionVoucherDetails(DateTime reportStartDateTime, DateTime reportEndDateTime, bool? IsDrop, Int32 BatchNumber)
        {
            return reportsDataAccess.GetExceptionVoucherDetails(reportStartDateTime, reportEndDateTime, IsDrop, BatchNumber);
        }

        public DataSet GetRedeemedTicketByDevice(DateTime fromDate, DateTime toDate, string DeviceType)
        {
            return reportsDataAccess.GetRedeemedTicketByDevice(fromDate, toDate, DeviceType);
        }

        public DataSet GetExpiredVoucherCouponReport(DateTime startDate, DateTime endDate, string sDeviceType)
        {
            return reportsDataAccess.GetExpiredVoucherCouponReport(startDate, endDate, sDeviceType);
        }

        public DataSet GetExpenseDetails(DateTime reportDate, string reportPeriod,bool IsGamingDayBasedReport)
        {
            return reportsDataAccess.GetExpenseDetails(reportDate, reportPeriod, IsGamingDayBasedReport);
        }
        public DataSet GetStackerDetails(int StackerLevel)
        {
            return reportsDataAccess.GetStackerDetails(StackerLevel);
        }


        public void GetVersion_SiteName(out string sVersion, out string sSiteName)
        {
            sVersion = string.Empty;
            sSiteName = string.Empty;

            SqlDataReader sDR = reportsDataAccess.GetVersion_SiteName();

            if (sDR != null && sDR.HasRows)
            {

                while (sDR.Read())
                {
                    sVersion = sDR.GetString(0);
                    sSiteName = sDR.GetString(1);
                }
                sDR.Close();
            }

        }

        public void GetVersion_SiteName(out string sVersion, out string sSiteName,out string SiteCode)
        {
            sVersion = string.Empty;
            sSiteName = string.Empty;
            SiteCode = string.Empty;

            SqlDataReader sDR = reportsDataAccess.GetVersion_SiteName();

            if (sDR != null && sDR.HasRows)
            {

                while (sDR.Read())
                {
                    sVersion = sDR.GetString(0);
                    sSiteName = sDR.GetString(1);
                    SiteCode = sDR.GetString(2);
                }
                sDR.Close();
            }

        }
        //
        public void GetSplashDetails(out string sCopyRight, out string sDescription, out string sCompanyName, out string sProductName, out string sProductVersion)
        {
            sCopyRight = string.Empty;
            sDescription = string.Empty;
            sCompanyName = string.Empty;
            sProductName = string.Empty;
            sProductVersion = string.Empty;
            //
            SqlDataReader sDR = reportsDataAccess.GetSplashDetails();

            if (sDR != null && sDR.HasRows)
            {

                while (sDR.Read())
                {
                    sCopyRight = sDR.GetString(0);
                    sDescription = sDR.GetString(2);
                    sCompanyName = sDR.GetString(3);
                    sProductName = sDR.GetString(4);
                    sProductVersion = sDR.GetString(1);
                }
                sDR.Close();
            }

        }
        //
        public DataSet GetVoucherListingReport(DateTime startDate, DateTime endDate, string sStatus, string sSlot)
        {
            return reportsDataAccess.GetVoucherListingReport(startDate, endDate, sStatus, sSlot);
        }


        public void GetSlots(System.Windows.Controls.ComboBox combo)
        {

            SqlDataReader sDR = reportsDataAccess.GetSlots();

            if (sDR.HasRows)
            {
                while (sDR.Read())
                {
                    combo.Items.Add(sDR["Stock_No"]);
                }
            }
            sDR.Close();
        }

        public DataTable GetAssetNumberforActiveInstallations()
        {
            return reportsDataAccess.GetAssetNumberforActiveInstallations();
        }

        public DataTable GetBatchNumber(DateTime StartDate, DateTime EndDate, bool isdeclared)
        {
            return reportsDataAccess.GetBatchNumber(StartDate,EndDate, isdeclared);
        }


        public DataSet GetMeterDetails(int installationNo)
        {
            return reportsDataAccess.GetMeterDetails(installationNo);
        }

        public DataSet GetCashDeskReconcilationDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No)
        {
            return reportsDataAccess.GetCashDeskReconcilationDetails(StartDate, EndDate, UserNo, iRoute_No);
        }

        public DataSet GetCashDeskMovementDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No)
        {
            return reportsDataAccess.GetCashDeskMovementDetails(StartDate, EndDate, UserNo, iRoute_No);
        }
       
        public DataSet GetSystemBalancingDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No)
        {
            return reportsDataAccess.GetSystemBalancingDetails(StartDate, EndDate, UserNo, iRoute_No);
        }

        public DataSet GetCashierTransactions(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No)
        {
            return reportsDataAccess.GetCashierTransactions(StartDate, EndDate, UserNo, iRoute_No);
        }
        
        #endregion Public Methods

        public DataSet GetLiquidationDetails(int BatchNo)
        {
            return reportsDataAccess.GetLiquidationDetails(BatchNo);
        }

        public DataSet GetExceptionSummary(int BatchNo)
        {
            return reportsDataAccess.GetExceptionSummary(BatchNo);
        }

        public DataSet GetBatchWinLoss(int BatchNo, int WeekNo)
        {
            return reportsDataAccess.GetBatchWinLoss(BatchNo, WeekNo);
        }

        public DataSet GetTicketAnomalies(DateTime StartDate,DateTime EndDate)
        {
            return reportsDataAccess.GetTicketAnomalies(StartDate, EndDate);
        }
        public DataSet GetMachineDrop(int BatchNo, int WeekNo)
        {
            return reportsDataAccess.GetMachineDrop(BatchNo, WeekNo);
        }
        
        public DataSet GetLiquidationSummaryDetails(int BatchNo)
        {
            return reportsDataAccess.GetLiquidationSummaryDetails(BatchNo);
        }

        public List<ServerDetails> GetDataBaseConnectionString()
        {
            return reportsDataAccess.GetDataBaseConnectionString();
        }
        public List<ServerDetails> GetDataBaseConnectionString(string ExchangeConnectionString)
        {
            return reportsDataAccess.GetDataBaseConnectionString(ExchangeConnectionString);        
        }

        public DataSet GetCashierTransactionsDetails(
          bool? isCDMPaid, bool? isCDMPrinted, bool? isHandPay, bool? isShortPay, bool? isVoidVoucher, bool? isJackpot, bool? isProgressive, bool? isVoid,
          bool? isMachinePaid, bool? isMachinePrinted,
            bool? isActive, bool? isVoidCancel, bool? isExpired, bool? isException, bool? isLiability, bool? isPromo,
            bool? isNonCashableIN, bool? isNonCashableOut,
          DateTime startDate, DateTime endDate, int user, int iRoute_No)
        {


            return reportsDataAccess.GetCashierTransactionsDetails(isCDMPaid, isCDMPrinted, isHandPay, isShortPay, isVoidVoucher,isJackpot, isProgressive, isVoid,
                                                          isMachinePaid, isMachinePrinted,
                                                         isActive, isVoidCancel, isExpired, isException, isLiability, isPromo,
                                                          isNonCashableIN, isNonCashableOut,
                                                          startDate, endDate, user, iRoute_No);
        }
        public DataSet GetCrossPropertyTicketAnalysisReport(DateTime StartDate, DateTime EndDate)
        {
            return reportsDataAccess.GetCrossPropertyTicketAnalysisReport(StartDate, EndDate);
        }

        public DataSet GetCrossPropertyLiabilityTransferDetailsReport(DateTime StartDate, DateTime EndDate)
        {
            return reportsDataAccess.GetCrossPropertyLiabilityTransferDetailsReport(StartDate, EndDate);
        }

        public DataSet GetCrossPropertyLiabilityTransferSummaryReport(DateTime StartDate, DateTime EndDate)
        {
            return reportsDataAccess.GetCrossPropertyLiabilityTransferSummaryReport(StartDate, EndDate);
        }

        public List<ReadLiquidationReportRecords> GetReadLiquidationReportRecords(bool bOnlyLast20Records)
        {
            return reportsDataAccess.GetReadLiquidationReportRecords(bOnlyLast20Records ? 20 : 3000);
        }

        public List<LiquidationDetailForReport> GetLiquidationDetailForReport(int? iBatchId, int? iReadId)
        {
            return reportsDataAccess.GetLiquidationDetailForReport(iBatchId, iReadId);
        }

        public int CheckLiquidationPerformed(int BatchID,ref int? LiquidationPerformed)
        {
            return reportsDataAccess.CheckLiquidationPerformed(BatchID,ref LiquidationPerformed);
        }

        public DataSet GetPartCollection(int NoofRecords)
        {
            return reportsDataAccess.GetPartCollection(NoofRecords);
    	}

        public DataSet GetAccountingWinLossReport(int ZoneNo, int MachineCategoryNo, DateTime StartDate, DateTime EndDate, bool IncludeNonCashable)
        {
            return reportsDataAccess.GetAccountingWinLossReport(ZoneNo,MachineCategoryNo,StartDate, EndDate, IncludeNonCashable);
        }

        public bool IsResetOccuredAndCompleted()
        {
            bool nResult = false;

            SqlDataReader sDR = reportsDataAccess.IsResetOccuredAndCompleted();

            if (sDR != null && sDR.HasRows)
            {
                while (sDR.Read())
                {
                    nResult = Convert.ToBoolean(sDR.GetString(0));                    
                }
                sDR.Close();
            }

            return nResult;
            
        }
        public DataSet GetPromotionalTicketHistory()
        {
            return reportsDataAccess.GetPromotionalTicketHistory();
        }

        public DataSet GetTISDetails(DateTime StartDate, DateTime EndDate,int NoOfRecordsInPage)
        {
            return reportsDataAccess.GetTISDetails(StartDate, EndDate,NoOfRecordsInPage);
        }

        public string GetSetting(string SettingName, string DefaultValue)
        {
            return reportsDataAccess.GetSetting(SettingName, DefaultValue);
        }
	}
}
