/* Modification Block
 * Name             Track#-CR#          Date            Description
 * Selva Kumar S    S001-133128         24 May 2012     Signature order is modified to receive values for
 *                                                      coresponding parameters
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using Microsoft.Win32;
using BMC.Transport;
using BMC.CommonLiquidation.Utilities;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;

namespace BMC.DBInterface.CashDeskOperator
{
    public class ReportsDataAccess
    {
        private string _ExchangeConnectionString = string.Empty;
        private string _TicketingConnectionString = string.Empty;
        public ReportsDataAccess()
        {
            _ExchangeConnectionString = CommonDataAccess.ExchangeConnectionString;
            _TicketingConnectionString = CommonDataAccess.TicketingConnectionString;
        }
        public ReportsDataAccess(string ExchangeConnectionString, string TicketingConnectionString)
           
        {
            _ExchangeConnectionString = ExchangeConnectionString;
            _TicketingConnectionString = TicketingConnectionString;
        }

        public List<ServerDetails> GetDataBaseConnectionString()
        {
            string regKeyConnectionString = GetExchangeConnectionString();
            List<ServerDetails> databaseCredentials = null;
            string[] DBCredentials = null;
            if (!string.IsNullOrEmpty(regKeyConnectionString))
                DBCredentials = regKeyConnectionString.Split(';');

            ServerDetails dbInfo = new ServerDetails();
            if (DBCredentials != null)
            {
                dbInfo.ServerName = DBCredentials[0].Split('=')[1].ToString();
                dbInfo.Username = DBCredentials[1].Split('=')[1].ToString();
                dbInfo.Password = DBCredentials[2].Split('=')[1].ToString();
                dbInfo.DataBase = DBCredentials[3].Split('=')[1].ToString();
                dbInfo.ConnectionTimeout = DBCredentials[4].Split('=')[1].ToString();

                databaseCredentials = new List<ServerDetails>();
                databaseCredentials.Add(dbInfo);
            }
            return databaseCredentials;
        }

        public List<ServerDetails> GetDataBaseConnectionString(string ExchangeConnectionString)
        {
            string regKeyConnectionString =string.Empty;
            if (ExchangeConnectionString.ToUpper().Contains("SERVER"))
            {
                regKeyConnectionString = ExchangeConnectionString;
            }
            else
            {
                regKeyConnectionString =BMC.Common.Security.CryptEncode.Decrypt(ExchangeConnectionString);
            }
            List<ServerDetails> databaseCredentials = null;
            string[] DBCredentials = null;
            if (!string.IsNullOrEmpty(regKeyConnectionString))
                DBCredentials = regKeyConnectionString.Split(';');

            ServerDetails dbInfo = new ServerDetails();
            if (DBCredentials != null)
            {
                dbInfo.ServerName = DBCredentials[0].Split('=')[1].ToString();
                dbInfo.Username = DBCredentials[1].Split('=')[1].ToString();
                dbInfo.Password = DBCredentials[2].Split('=')[1].ToString();
                dbInfo.DataBase = DBCredentials[3].Split('=')[1].ToString();
                dbInfo.ConnectionTimeout = DBCredentials[4].Split('=')[1].ToString();

                databaseCredentials = new List<ServerDetails>();
                databaseCredentials.Add(dbInfo);
            }
            return databaseCredentials;
        }

        private string GetExchangeConnectionString()
        {
            string strConnectionString = "";

            try
            {
                //bool bUseHex = true;
                //RegistryKey regKeyConnectionString = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\Cashmaster");
                //strConnectionString = regKeyConnectionString.GetValue("SQLConnect").ToString();
                //regKeyConnectionString.Close();

                strConnectionString = DatabaseHelper.GetExchangeConnectionString();

                //if (!strConnectionString.ToUpper().Contains("SERVER"))
                //{
                //    strConnectionString = BMC.Common.Security.CryptEncode.Decrypt(strConnectionString);
                //}

                return strConnectionString;
            }
            catch (Exception ex)
            {
                strConnectionString = "";
                return strConnectionString;
            }
        }
        public DataSet GetJackpotSlipSummaryDetails(DateTime reportStartDateTime, DateTime reportEndDateTime,
            bool? ShowHandpay,bool? ShowJackpot)
        {
            DataSet dsJackpotSlipSummartDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter(DBConstants.SP_PARAM_JACKPOT_SUMMARY_REPORT_START_DATE_TIME, reportStartDateTime);
                objParams[1] = new SqlParameter(DBConstants.SP_PARAM_JACKPOT_SUMMARY_REPORT_END_DATE_TIME, reportEndDateTime);
                objParams[2] = new SqlParameter(DBConstants.SP_PARAM_JACKPOT_SUMMARY_REPORT_INCLUDEHANDPAY, ShowHandpay);
                objParams[3] = new SqlParameter(DBConstants.SP_PARAM_JACKPOT_SUMMARY_REPORT_SHOWJACKPOT, ShowJackpot);

                SqlHelper.FillDataset(_ExchangeConnectionString, DBConstants.SP_RSP_GETJACKPOTSLIPSUMMARYDETAILSFORREPORTS, dsJackpotSlipSummartDetails, new string[] { "JackpotSlipSummaryDetails" }, objParams);

                return dsJackpotSlipSummartDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetVoucherCouponLiabilityReport(DateTime issueDate, string sDeviceType, string sVoucherStatus)
        {
            DataSet dsVoucherCouponLiability = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@ISSUEDATE", issueDate);
                objParams[1] = new SqlParameter("@Status", sVoucherStatus);
                objParams[2] = new SqlParameter("@DeviceType", sDeviceType);



                SqlHelper.FillDataset(CommonDataAccess.TicketingConnectionString, DBConstants.SP_RSP_GETVOUCHERCOUPONLIABILITYREPORT, dsVoucherCouponLiability, new string[] { "VoucherCouponLiability" }, objParams);

                return dsVoucherCouponLiability;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public SqlDataReader GetVersion_SiteName()
        {
            try
            {
                return SqlHelper.ExecuteReader(_ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_RSP_RSP_GETVERSION_SITENAME);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public SqlDataReader GetSplashDetails()
        {
            try
            {
                return SqlHelper.ExecuteReader(_ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.SP_RSP_GETSPLASH_DETAILS);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }




        public DataSet GetRedeemedTicketByDevice(DateTime fromDate, DateTime toDate, string DeviceType)
        {
            DataSet dsRedeem = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@startDate", fromDate);
                objParams[1] = new SqlParameter("@endDate", toDate);
                objParams[2] = new SqlParameter("@DeviceType", DeviceType);


                SqlHelper.FillDataset(_TicketingConnectionString, DBConstants.SP_RSP_GETREDEEMEDTICKETBYDEVICE, dsRedeem, new string[] { "RedeemedTicketByDevice" }, objParams);

                return dsRedeem;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }


        public DataSet GetExpiredVoucherCouponReport(DateTime startDate, DateTime endDate, string sDeviceType)
        {
            DataSet dsExpiredVoucherCoupon = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@startDate", startDate);
                objParams[1] = new SqlParameter("@EndDate", endDate);
                objParams[2] = new SqlParameter("@DeviceType", sDeviceType);


                SqlHelper.FillDataset(_TicketingConnectionString, DBConstants.SP_RSP_GETEXPIREDVOUCHERCOUPONREPORT, dsExpiredVoucherCoupon, new string[] { "ExpiredVoucherCoupon" }, objParams);

                return dsExpiredVoucherCoupon;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }


        public DataSet GetExpenseDetails(DateTime reportDate, string reportPeriod,bool IsGamingDayBasedReport)
        {
            DataSet dsExpenseDetails = new DataSet();
            int errorCode = 0;
            string errorMessage = string.Empty;

            try
            {
                LogManager.WriteLog("Inside GetExpenseDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter(DBConstants.SP_PARAM_EXPENSE_DETAIL_REPORT_DATE, reportDate);
                objParams[1] = new SqlParameter(DBConstants.SP_PARAM_EXPENSE_DETAIL_REPORT_PERIOD, reportPeriod);
                objParams[2] = new SqlParameter(DBConstants.SP_PARAM_EXPENSE_DETAIL_REPORT_ERROR_CODE, errorCode);
                objParams[2].Direction = ParameterDirection.Output;
                objParams[3] = new SqlParameter(DBConstants.SP_PARAM_EXPENSE_DETAIL_REPORT_ERROR_MSG, errorMessage);
                objParams[3].Direction = ParameterDirection.Output;
                objParams[4] = new SqlParameter(DBConstants.SP_PARAM_EXPENSE_DETAIL_GamingDay_BasedReport, IsGamingDayBasedReport);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString, DBConstants.SP_RSP_GETEXPENSEDETAILSFORREPORTS, dsExpenseDetails, new string[] { "ExpenseDetails", "SummarizedExpenseDetails", "SummExpenseDetails" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                if (errorCode != 0)
                {
                    throw new Exception(errorMessage);
                }

                return dsExpenseDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetStackerDetails(int StackerLevel)
        {
            DataSet dsStackerLevelDetails = new DataSet();
            int errorCode = 0;
            string errorMessage = string.Empty;

            try
            {
                LogManager.WriteLog("Inside GetStackerDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter(DBConstants.SP_PARAM_STACKER_LEVEL, StackerLevel);

                SqlHelper.FillDataset(_ExchangeConnectionString, DBConstants.SP_RSP_GETSTACKERLEVLDETAILSFORREPORTS, dsStackerLevelDetails, new string[] { "StackerLevelDetailsReport" }, objParams);

                return dsStackerLevelDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetVoucherListingReport(DateTime startDate, DateTime endDate, string sStatus, string sSlot)
        {
            DataSet dsListing = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@startDate", startDate);
                objParams[1] = new SqlParameter("@endDate", endDate);
                objParams[2] = new SqlParameter("@Status", sStatus);
                objParams[3] = new SqlParameter("@Slot", sSlot);


                SqlHelper.FillDataset(_TicketingConnectionString, DBConstants.SP_RSP_GETVOUCHERLISTINGREPORT, dsListing, new string[] { "VoucherListingReport" }, objParams);

                return dsListing;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }

        }

        public SqlDataReader GetSlots()
        {
            try
            {
                return SqlHelper.ExecuteReader(_ExchangeConnectionString, CommandType.Text, "Select Stock_No from Machine order by Stock_No");

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataTable GetAssetNumberforActiveInstallations()
        {
            DataSet dsAssetNumbers = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetAssetNumberforActiveInstallations method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString, DBConstants.SP_RSP_GETASSETNUMBERFORACTIVEINSTALLATIONS, dsAssetNumbers, new string[] { "AssetNumber" }, null);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsAssetNumbers.Tables["AssetNumber"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetBatchNumber(DateTime StartDate, DateTime EndDate, bool isdeclared)
        {
            DataSet dsBatchNumbers = new DataSet();

            try
            {

                LogManager.WriteLog("Inside GetBatchNumber method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@startDate", StartDate);
                objParams[1] = new SqlParameter("@endDate", EndDate);
                objParams[2] = new SqlParameter("@IsDrop", isdeclared);

                SqlHelper.FillDataset(_TicketingConnectionString, DBConstants.SP_GETBATCHNO, dsBatchNumbers, new string[] { "BatchNumber" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsBatchNumbers.Tables["BatchNumber"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataSet GetMeterDetails(int installationNo)
        {
            DataSet dsMeterDetails = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetMeterDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter(DBConstants.SP_PARAM_METER_DETAIL_INSTALLATION_ID, installationNo);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString, DBConstants.SP_RSP_GETMETERDETAILSFORREPORTS, dsMeterDetails, new string[] { "MeterDetails" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsMeterDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCashDeskReconcilationDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No)
        {
            DataSet dsCashDeskReconDetails = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetCashDeskReconcilationDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, StartDate);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, EndDate);
                objParams[2] = new SqlParameter("@User", UserNo);
                objParams[3] = new SqlParameter(DBConstants.CONST_PARAM_RouteNo, iRoute_No);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString, DBConstants.SP_RSP_GETCASHDESKRECONDETAILS, dsCashDeskReconDetails, new string[] { "CashDeskReconcilation" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsCashDeskReconDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetCashDeskMovementDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No)
        {
            DataSet dsCashDeskMovementDetails = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetCashDeskMovementDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, StartDate);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, EndDate);
                objParams[2] = new SqlParameter("@User", UserNo);
                objParams[3] = new SqlParameter(DBConstants.CONST_PARAM_RouteNo, iRoute_No);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString, DBConstants.SP_RSP_GETCASHDESKMOVEMENTDETAILS, dsCashDeskMovementDetails, new string[] { "CashDeskMovement" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsCashDeskMovementDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetSystemBalancingDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No)
        {
            DataSet dsSystemBalancingDetails = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetSystemBalancingDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, StartDate);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, EndDate);
                objParams[2] = new SqlParameter("@User", UserNo);
                objParams[3] = new SqlParameter(DBConstants.CONST_PARAM_RouteNo, iRoute_No);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString, DBConstants.SP_RSP_GETSYSTEMBALANCINGDETAILS, dsSystemBalancingDetails, new string[] { "SystemBalancing" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsSystemBalancingDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetLiquidationDetails(int BatchNo)
        {
            DataSet dsLiquidation = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetLiquidationDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_BATCH, BatchNo);


                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString,
                        DBConstants.RSP_REPORT_SGVI_LIQUIDATIONDETAIL, dsLiquidation, new string[] { "LiquidationDetail" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsLiquidation;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetExceptionSummary(int BatchNo)
        {
            DataSet dsExceptionSummary = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetExceptionSummary method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_BATCH, BatchNo);


                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString,
                        DBConstants.RSP_REPORT_SGVI_EXCEPTIONSUMMARY, dsExceptionSummary, new string[] { "ExceptionSummary" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsExceptionSummary;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }


        public DataSet GetBatchWinLoss(int BatchNo, int WeekNo)
        {
            DataSet dsBatchWinLoss = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetBatchWinLoss method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[2];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_BATCH, BatchNo);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_WEEK, WeekNo > 0 ? true : false);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString,
                        DBConstants.RSP_REPORT_BATCHWINLOSS, dsBatchWinLoss, new string[] { "BatchWinLossReport" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsBatchWinLoss;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetTicketAnomalies(DateTime StartDate, DateTime EndDate)
        {
            DataSet dsTicketAnomalies = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetTicketAnomalies method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[2];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, StartDate);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, EndDate);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_TicketingConnectionString,
                        DBConstants.RSP_REPORT_TICKETANOMALIES, dsTicketAnomalies, new string[] { "TicketAnomaliesReport" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsTicketAnomalies;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        public DataSet GetMachineDrop(int BatchNo, int WeekNo)
        {
            DataSet dsBatchWinLoss = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetBatchWinLoss method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[2];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_BATCH, BatchNo);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_WEEK, WeekNo > 0 ? true : false);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString,
                        DBConstants.RSP_REPORT_MACHINEDROP, dsBatchWinLoss, new string[] { "BatchWinLossReport" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsBatchWinLoss;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        
        public DataSet GetLiquidationSummaryDetails(int BatchNo)
        {
            DataSet dsLiquidationSummary = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetLiquidationDetails method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_BATCH, BatchNo);


                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_ExchangeConnectionString,
                        DBConstants.RSP_REPORT_SGVI_LIQUIDATIONSUMMARY, dsLiquidationSummary, new string[] { "LiquidationSummary" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsLiquidationSummary;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        #region +S001 START
        /* public DataSet GetCashierTransactionsDetails(bool? isCDMPaid, bool? isCDMPrinted, bool? isHandPay, bool? isShortpay,bool? isJackpot, 
                                                     bool? isProgressive, bool? isVoid, bool? isMachinePaid, bool? isMachinePrinted,
                                                    bool? isNonCashableIN, bool? isNonCashableOut,
                                                     bool? isActive, bool? isVoidCancel, bool? isExpired, bool? isException,
                                                    bool? isLiability,bool? isPromo,DateTime startDate, DateTime endDate,int user)*/

        public DataSet GetCashierTransactionsDetails(bool? isCDMPaid, bool? isCDMPrinted, bool? isHandPay, bool? isShortpay,bool? isVoidVoucher, bool? isJackpot,
                                                     bool? isProgressive, bool? isVoid, bool? isMachinePaid, bool? isMachinePrinted,
                                                     bool? isActive, bool? isVoidCancel, bool? isExpired, bool? isException,
                                                    bool? isLiability, bool? isPromo, bool? isNonCashableIN, bool? isNonCashableOut,
                                                    DateTime startDate, DateTime endDate, int user, int iRoute_No)
        #endregion +S001 END
        {
            DataSet dsCashierTransactionsDetails = new DataSet();

            try
            {                SqlParameter[] objParams = new SqlParameter[23];

                objParams[0] = new SqlParameter("@isCDMPaid", isCDMPaid);
                objParams[1] = new SqlParameter("@isCDMPrinted", isCDMPrinted);
                objParams[2] = new SqlParameter("@isHandPay", isHandPay);
                objParams[3] = new SqlParameter("@isShortpay", isShortpay);
                objParams[4] = new SqlParameter("@isVoidVoucher", isVoidVoucher);
                objParams[5] = new SqlParameter("@isJackpot", isJackpot);
                objParams[6] = new SqlParameter("@isProgressive", isProgressive);
                objParams[7] = new SqlParameter("@isVoid", isVoid);
                objParams[8] = new SqlParameter("@isMachinePaid", isMachinePaid);
                objParams[9] = new SqlParameter("@isMachinePrinted", isMachinePrinted);
                objParams[10] = new SqlParameter("@isActive", isActive);
                objParams[11] = new SqlParameter("@isVoidCancel", isVoidCancel);
                objParams[12] = new SqlParameter("@isExpired", isExpired);
                objParams[13] = new SqlParameter("@isException", isException);
                objParams[14] = new SqlParameter("@isLiability", isLiability);
                objParams[15] = new SqlParameter("@isPromo", isPromo);
                objParams[16] = new SqlParameter("@isNonCashableIN", isNonCashableIN);
                objParams[17] = new SqlParameter("@isNonCashableOut", isNonCashableOut);
                objParams[18] = new SqlParameter("@StartDate", startDate);
                objParams[19] = new SqlParameter("@EndDate", endDate);
                objParams[20] = new SqlParameter("@User", user);
                objParams[21] = new SqlParameter("@Route_No", iRoute_No);
                objParams[22] = new SqlParameter("@isOffline", true );
                SqlHelper.FillDataset(_TicketingConnectionString, "rsp_GetCashierTransactionsDetails", dsCashierTransactionsDetails, new string[] { "DetailsView" }, objParams);

                return dsCashierTransactionsDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetExceptionVoucherDetails(DateTime reportStartDateTime, DateTime reportEndDateTime, bool? IsDrop,Int32 BatchNumber)
        {
            DataSet dsExceptionVoucherDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@Startdate", reportStartDateTime);
                objParams[1] = new SqlParameter("@Enddate", reportEndDateTime);
                objParams[2] = new SqlParameter("@IsDrop", IsDrop);
                objParams[3] = new SqlParameter("@BatchNo", BatchNumber);

                SqlHelper.FillDataset(_TicketingConnectionString, DBConstants.RSP_GETEXCEPTION_VOUCHER_FOR_REPORTS, dsExceptionVoucherDetails, new string[] { "ExceptionVoucher" }, objParams);

                return dsExceptionVoucherDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        public DataSet GetCrossPropertyTicketAnalysisReport(DateTime StartDate, DateTime EndDate)
        {
            DataSet dsCrProp = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@STARTDATE", StartDate);
                objParams[1] = new SqlParameter("@ENDDATE", EndDate);

                SqlHelper.FillDataset(_TicketingConnectionString, "rsp_getCrossPropertyTicketAnalysis", dsCrProp, new string[] { "CrossPropertyTicketAnalysis" }, objParams);
                return dsCrProp;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetCrossPropertyLiabilityTransferDetailsReport(DateTime StartDate, DateTime EndDate)
        {
            DataSet dsCrProp = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@STARTDATE", StartDate);
                objParams[1] = new SqlParameter("@ENDDATE", EndDate);

                SqlHelper.FillDataset(_TicketingConnectionString, "rsp_getreportliabilitytransfersdetails", dsCrProp, new string[] { "CrossPropertyTicketAnalysis" }, objParams);
                return dsCrProp;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetCrossPropertyLiabilityTransferSummaryReport(DateTime StartDate, DateTime EndDate)
        {
            DataSet dsCrProp = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@STARTDATE", StartDate);
                objParams[1] = new SqlParameter("@ENDDATE", EndDate);

                SqlHelper.FillDataset(_TicketingConnectionString, "rsp_getCrossPropertyLiabilityTransferSummary", dsCrProp, new string[] { "CrossPropertyTicketAnalysis" }, objParams);
                return dsCrProp;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        public DataSet GetCashierTransactions(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No)
        {
            DataSet dsCashierTransactionsDetails = new DataSet();

            try
            {
                LogManager.WriteLog("Inside GetCashierTransactions method", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Stored procedure parameters...", LogManager.enumLogLevel.Info);

                SqlParameter[] objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, StartDate);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, EndDate);
                objParams[2] = new SqlParameter("@User", UserNo);
                objParams[3] = new SqlParameter(DBConstants.CONST_PARAM_RouteNo, iRoute_No);

                LogManager.WriteLog("Stored Procedure parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Executing SQLHelper FillDataset method...", LogManager.enumLogLevel.Info);

                SqlHelper.FillDataset(_TicketingConnectionString, "rsp_GetCashierTransactions", dsCashierTransactionsDetails, new string[] { "CashDeskReconcilation" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsCashierTransactionsDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetAccountingWinLossReport(int ZoneNo, int MachineCategoryNo, DateTime StartDate, DateTime EndDate, bool IncludeNonCashable)
        {
            DataSet dsAccWinLoss = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@Zone", ZoneNo);
                objParams[1] = new SqlParameter("@Category", MachineCategoryNo);
                objParams[2] = new SqlParameter("@StartDate", StartDate);
                objParams[3] = new SqlParameter("@EndDate", EndDate);
                objParams[4] = new SqlParameter("@IncludeNonCashable", IncludeNonCashable);

                SqlHelper.FillDataset(_ExchangeConnectionString, "rsp_REPORT_AccountingWinLossReport", dsAccWinLoss, new string[] { "AccountingWinLoss" }, objParams);
                return dsAccWinLoss;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public List<ReadLiquidationReportRecords> GetReadLiquidationReportRecords(int iNoOfRecords)
        {
            try
            {
                CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(CommonDataAccess.ExchangeConnectionString);
                return commonLiquidationDataContext.GetReadLiquidationReportRecords(null, iNoOfRecords).ToList();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<ReadLiquidationReportRecords>();
            }
        }

        public List<LiquidationDetailForReport> GetLiquidationDetailForReport(int? iBatchId, int? iReadId)
        {
            try
            {
                CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(CommonDataAccess.ExchangeConnectionString);
                return commonLiquidationDataContext.GetLiquidationDetailForReport(iBatchId, iReadId).ToList();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<LiquidationDetailForReport>();
            }
        }


        public int CheckLiquidationPerformed(int BatchID,ref int? LiquidationPerformed)
        {
            try
            {
                CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(CommonDataAccess.ExchangeConnectionString);
                return commonLiquidationDataContext.CheckLiquidationPerformed(BatchID,ref LiquidationPerformed);
            }
            catch (Exception ex)
            {
                
                 ExceptionManager.Publish(ex);
                 return 0;
            }
        }

        public DataSet GetPartCollection(int NoofRecords)
        {
            DataSet dsPartColl = new DataSet();
            try
            {
                LogManager.WriteLog("Inside GetPartCollection method", LogManager.enumLogLevel.Info);
                
                SqlParameter[] objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAN_NUMBEROFPARTCOUNTRECORDS, NoofRecords);
                

                SqlHelper.FillDataset(_ExchangeConnectionString,
                        DBConstants.RSP_REPORT_GETPARTCOLLECTIONBATCHDATA, dsPartColl, new string[] { "PartCollectionReport" }, objParams);

                LogManager.WriteLog("SQLHelper FillDataset executed successfully", LogManager.enumLogLevel.Info);

                return dsPartColl;
    		}
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
			}
        }

        public SqlDataReader IsResetOccuredAndCompleted()
        {
            try
            {
                return SqlHelper.ExecuteReader(_ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetFactoryResetStatus");
    }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
}
        } 
            //Promotional Ticket
        public DataSet GetPromotionalTicketHistory()
        {
            DataSet dsResult = new DataSet();

            try
            {
                SqlHelper.FillDataset(CommonDataAccess.TicketingConnectionString, DBConstants.RSP_SELECTPROMOTIONHISTORYREPORT, dsResult, new string[] { "PromotionalTicketHistory" });
                return dsResult;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetTISDetails(DateTime StartDate,DateTime EndDate,int NoOfRecords)
        {
            DataSet dsResult = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter(DBConstants.CONST_PARAM_STARTDATE, StartDate);
                objParams[1] = new SqlParameter(DBConstants.CONST_PARAM_ENDDATE, EndDate);
                objParams[2] = new SqlParameter(DBConstants.CONST_PARAM_RECORDSINPAGE, NoOfRecords);
                SqlHelper.FillDataset(CommonDataAccess.TicketingConnectionString, DBConstants.RSP_SELECTTISDETAILSREPORT, dsResult, new string[] { "PromotionalTISDetails" },objParams);
                return dsResult;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public string GetSetting(string strSettingName, string defaultValue)
        {
            return CommonDataAccess.GetSettingValue(strSettingName, defaultValue);
        }

        
    }
}