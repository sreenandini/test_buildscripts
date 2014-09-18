using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Business.CashDeskOperator;
using System.Data;
using BMC.Transport;
using BMC.CommonLiquidation.Utilities;
using BMC.Common.ExceptionManagement;

namespace BMC.CashDeskOperator
{
    public class ReportsBusinessObject : IReports
    {
        #region Private Variables

        BMC.Business.CashDeskOperator.Reports reports = null;

        #endregion

        #region Constructor

        public ReportsBusinessObject()
        {
            reports = new BMC.Business.CashDeskOperator.Reports();
        }

        public ReportsBusinessObject(string ExchangeConnectionString, string TicketingConnectionString)
        {
            reports = new BMC.Business.CashDeskOperator.Reports(ExchangeConnectionString, TicketingConnectionString);
        }
        #endregion Constructor

        #region Public Static Function
        public static IReports CreateInstance()
        {
            return new ReportsBusinessObject();
        }

        public static IReports CreateInstance(string ExchangeConnectionString, string TicketingConnectionString)
        {
            return new ReportsBusinessObject(ExchangeConnectionString, TicketingConnectionString);
        }
        #endregion

        #region IReports Members

        public DataSet GetJackpotSlipSummaryDetails(DateTime reportStartDateTime, DateTime reportEndDateTime,
            bool? ShowHandpay,bool? ShowJackpot)
        {
            return reports.GetJackpotSlipSummaryDetails(reportStartDateTime, reportEndDateTime, 
                ShowHandpay,ShowJackpot);
        }

        public DataSet GetVoucherCouponLiabilityReport(DateTime issueDate, string sDeviceType, string sVoucherStatus)
        {
            return reports.GetVoucherCouponLiabilityReport(issueDate, sDeviceType, sVoucherStatus);
        }

        public DataSet GetRedeemedTicketByDevice(DateTime fromDate, DateTime toDate, string DeviceType)
        {
            return reports.GetRedeemedTicketByDevice(fromDate, toDate, DeviceType);
        }

        public DataSet GetExpiredVoucherCouponReport(DateTime startDate, DateTime endDate, string sDeviceType)
        {
            return reports.GetExpiredVoucherCouponReport(startDate, endDate, sDeviceType);
        }

        public DataSet GetExpenseDetails(DateTime reportDate, string reportPeriod, bool IsGamingDayBasedReport)
        {
            return reports.GetExpenseDetails(reportDate, reportPeriod,IsGamingDayBasedReport);
        }

        public DataSet GetStackerDetails(int StackerLevel)
        {
            return reports.GetStackerDetails(StackerLevel);
        }

        public DataSet GetAuditTrailReport(DateTime fromDate, DateTime toDate, string sModuleName)
        {
            AuditBusiness AB = new AuditBusiness();
            return AB.GetAuditTrailReport(fromDate, toDate, sModuleName);
        }

        public DataSet GetAFTAuditTrailReport(DateTime fromDate, DateTime toDate)
        {
            AuditBusiness AB = new AuditBusiness();
            return AB.GetAFTAuditTrailReport(fromDate, toDate);
        }

        public void GetVersion_SiteName(out string sVersion, out string sSiteName)
        {
            reports.GetVersion_SiteName(out sVersion, out sSiteName);
        }

        public string GetSetting(string SettingName, string DefaultValue)
        {
            return reports.GetSetting(SettingName, DefaultValue);
        }

        //
        public void GetSplashDetails(out string sCopyRight, out string sDescription, out string sCompanyName, out string sProductName, out string sProductVersion)
        {
            reports.GetSplashDetails(out sCopyRight, out sDescription, out sCompanyName, out sProductName, out sProductVersion);
        }
        //
        public DataSet GetVoucherListingReport(DateTime startDate, DateTime endDate, string Status, string Slot)
        {
            return reports.GetVoucherListingReport(startDate, endDate, Status, Slot);
        }
        public void GetSlots(System.Windows.Controls.ComboBox combo)
        {
            reports.GetSlots(combo);
        }

        public DataTable GetAssetNumberforActiveInstallations()
        {
            return reports.GetAssetNumberforActiveInstallations();
        }

        public DataTable GetBatchNumber(DateTime StartDate, DateTime EndDate, bool isdeclared)
        {
            return reports.GetBatchNumber(StartDate,EndDate, isdeclared);
        }

        public DataSet GetMeterDetails(int installationNo)
        {
            return reports.GetMeterDetails(installationNo);
        }

        public List<ReadLiquidationReportRecords> GetReadLiquidationReportRecords(bool bOnlyLast20Records)
        {
            return reports.GetReadLiquidationReportRecords(bOnlyLast20Records);
        }

        public List<LiquidationDetailForReport> GetLiquidationDetailForReport(int? iBatchId, int? iReadId)
        {
            return reports.GetLiquidationDetailForReport(iBatchId, iReadId);
        }

        public bool IsResetOccuredAndCompleted()
        {
            return reports.IsResetOccuredAndCompleted();
        }

        #endregion

        #region IReports Members


        public DataSet GetCashDeskReconcilationDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRouteNo)
        {
            return reports.GetCashDeskReconcilationDetails(StartDate, EndDate, UserNo, iRouteNo);
        }

        public DataSet GetCashDeskMovementDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No)
        {
            return reports.GetCashDeskMovementDetails(StartDate, EndDate, UserNo, iRoute_No);
        }

        public DataSet GetCashierTransactions(DateTime StartDate, DateTime EndDate, int UserNo, int iRouteNo)
        {
            return reports.GetCashierTransactions(StartDate, EndDate, UserNo, iRouteNo);
        }

        #endregion



        #region IReports Members


        public DataSet GetSystemBalancingDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No)
        {
            return reports.GetSystemBalancingDetails(StartDate, EndDate, UserNo, iRoute_No);
        }

        #endregion

        #region IReports Members


        public DataSet GetLiquidationDetails(int BatchNo)
        {
            return reports.GetLiquidationDetails(BatchNo);
        }

        public DataSet GetExceptionSummary(int BatchNo)
        {
            return reports.GetExceptionSummary(BatchNo);
        }

        #endregion

        #region IReports Members


        public DataSet GetLiquidationSummaryDetails(int _BatchID)
        {
            return reports.GetLiquidationSummaryDetails(_BatchID);
        }

        #endregion

        #region IReports Members


        public List<ServerDetails> GetDataBaseConnectionString()
        {
            return reports.GetDataBaseConnectionString();
        }
        public List<ServerDetails> GetDataBaseConnectionString(string ExchangeConnectionString)
        {
            return reports.GetDataBaseConnectionString(ExchangeConnectionString);
        }
        #endregion


        #region IReports Members


        public DataSet GetBatchWinLoss(int BatchNo, int WeekNo)
        {
            return reports.GetBatchWinLoss(BatchNo, WeekNo);
        }


        public DataSet GetTicketAnomalies(DateTime StartDate, DateTime EndDate)
        {
            return reports.GetTicketAnomalies(StartDate, EndDate);
        }


        public DataSet GetMachineDrop(int BatchNo, int WeekNo)
        {
            return reports.GetMachineDrop(BatchNo, WeekNo);
        }
        
        public DataSet GetExceptionVoucherDetails(DateTime reportStartDateTime, DateTime reportEndDateTime, bool? IsDrop, Int32 BatchNumber)
        {
            return reports.GetExceptionVoucherDetails(reportStartDateTime, reportEndDateTime, IsDrop, BatchNumber);
        }
        #endregion

        #region IReports Members

        public DataSet GetPartCollectionDetails(int NoofRecords)
        {
            return reports.GetPartCollection(NoofRecords);
        }

        #endregion

        #region IReports Members

        public DataSet GetCrossPropertyTicketAnalysisReport(DateTime StartDate, DateTime EndDate)
        {
            return reports.GetCrossPropertyTicketAnalysisReport(StartDate, EndDate);
        }

        public DataSet GetAccountingWinLossReport(int ZoneNo, int CategoryNo, DateTime StartDate, DateTime EndDate, bool IncludeNonCashable)
        {
            return reports.GetAccountingWinLossReport(ZoneNo,CategoryNo,StartDate, EndDate, IncludeNonCashable);
        }
        #endregion

        #region IReports Members


        public DataSet GetCrossPropertyLiabilityTransferDetailsReport(DateTime StartDate, DateTime EndDate)
        {
            return reports.GetCrossPropertyLiabilityTransferDetailsReport(StartDate, EndDate);
        }

        #endregion

        #region IReports Members


        public DataSet GetCrossPropertyLiabilityTransferSummaryReport(DateTime StartDate, DateTime EndDate)
        {
            return reports.GetCrossPropertyLiabilityTransferSummaryReport(StartDate, EndDate);
        }

        public int CheckLiquidationPerformed(int BatchID,ref int? iLiquidationPerformed)
        {
            try
            {
             return   reports.CheckLiquidationPerformed(BatchID,ref iLiquidationPerformed);
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public DataSet GetPromotionalTicketHistory()
        {
            try
            {
                return reports.GetPromotionalTicketHistory();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
                

            }
        }


        public DataSet GetTISPromotionalDetails(DateTime StartDate, DateTime EndDate,int NoOfRecordsInPage)
        {
            try
            {
                return reports.GetTISDetails(StartDate,EndDate,NoOfRecordsInPage);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
                

            }
        }

        
        
        #endregion


        public void GetVersion_SiteName(out string sVersion, out string sSiteName, out string SiteCode)
        {
            reports.GetVersion_SiteName(out sVersion, out sSiteName, out SiteCode);
        }
    }
}
