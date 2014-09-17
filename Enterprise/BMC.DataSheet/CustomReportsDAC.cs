using System;
using System.Data;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
using BMC.Common.LogManagement;

namespace CustomReports
{
    public class CustomReportsDAC
    {
        public CustomReportsDAC()
        {

        }

        internal DataSet PopulateCompany(int UserId)
        {
            DataSet objDataSet = null;
            SqlParameter[] oParams = new SqlParameter[1];
            try
            {
                oParams[0] = new SqlParameter("@SecurityUserID", UserId);
                objDataSet = SqlHelper.ExecuteDataset(ConnectionHelper.EnterpriseConnectionString, CommandType.StoredProcedure, CustomReportsConstants.RSP_GETCOMPANYDETAILS, oParams);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsDAC.PopulateCompany " + ex.Message, LogManager.enumLogLevel.Error);
            }

            return objDataSet;
        }

        internal DataSet PopulateSubCompany(int nCompanyID,int UserId)
        {
            DataSet objDataSet = null;
            SqlParameter[] oParams = new SqlParameter[2];            
            try
            {
                oParams[0] = new SqlParameter("@Company", nCompanyID);
                oParams[1] = new SqlParameter("@SecurityUserID", UserId);
                objDataSet = SqlHelper.ExecuteDataset(ConnectionHelper.EnterpriseConnectionString, CommandType.StoredProcedure, CustomReportsConstants.RSP_GETSUBCOMPANYDETAILS, oParams);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsDAC.PopulateSubCompany " + ex.Message, LogManager.enumLogLevel.Error);
            }

            return objDataSet;
        }

        internal DataSet PopulateSites(int nSubCompanyID, int nCompanyID,int UserId)
        {
            DataSet objDataSet = null;
            SqlParameter[] oParams = new SqlParameter[3];
            try
            {
                oParams[0] = new SqlParameter("@SubCompanyId", nSubCompanyID);
                oParams[1] = new SqlParameter("@CompanyId", nCompanyID);
                oParams[2] = new SqlParameter("@SecurityUserID", UserId);


                objDataSet = SqlHelper.ExecuteDataset(ConnectionHelper.EnterpriseConnectionString, CommandType.StoredProcedure, CustomReportsConstants.RSP_GETSITEINFOFORDATASHEET, oParams);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsDAC.PopulateSites " + ex.Message, LogManager.enumLogLevel.Error);
            }

            return objDataSet;
        }

        internal DataSet PopulateZones(int nCompanyId, int nSubCompanyId, int nSiteID)
        {
            DataSet objDataSet = null;
            SqlParameter[] oParams = new SqlParameter[3];
            try
            {
                oParams[0] = new SqlParameter("@CompanyId", nCompanyId);
                oParams[1] = new SqlParameter("@SubCompanyId", nSubCompanyId);
                oParams[2] = new SqlParameter("@Site", nSiteID);
                objDataSet = SqlHelper.ExecuteDataset(ConnectionHelper.EnterpriseConnectionString, CommandType.StoredProcedure, CustomReportsConstants.RSP_GETZONEINFOFORDATASHEET, oParams);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsDAC.PopulateZones " + ex.Message, LogManager.enumLogLevel.Error);
            }

            return objDataSet;
        }

        internal DataSet GetPUPDPerformanceReport(int nBasedOn, int nCompanyID, int nSubCompanyID, int nSiteID, int nZoneID, DateTime dtStartDate, DateTime dtEndDate)
        {
            DataSet objDataSet = new DataSet();
            try
            {
                SqlParameter[] objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@BasedOn", nBasedOn);
                objParams[1] = new SqlParameter("@Company", nCompanyID);
                objParams[2] = new SqlParameter("@SubCompany", nSubCompanyID);
                objParams[3] = new SqlParameter("@Site", nSiteID);
                objParams[4] = new SqlParameter("@Zone", nZoneID);
                objParams[5] = new SqlParameter("@StartDate", dtStartDate);
                objParams[6] = new SqlParameter("@EndDate", dtEndDate);

                SqlHelper.FillDataset(ConnectionHelper.EnterpriseConnectionString, CustomReportsConstants.RSP_REPORT_PERUNITPERDAYPERFORMANCEREPORT, objDataSet, new string[] { "PerUnitPerDayPerformanceReport" }, objParams);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsDAC.GetPUPDPerformanceReport " + ex.Message, LogManager.enumLogLevel.Error);
            }

            return objDataSet;
        }
    }
}
