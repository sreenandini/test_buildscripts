using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using BMC.Common.LogManagement;
using BMC.Common;
namespace CustomReports
{
    public class CustomReportsBusiness
    {
        public CustomReportsBusiness()
        {

        }

        internal Dictionary<string, string> GetReports(string strReportPath)
        {
            Dictionary<string, string> dictReport = new Dictionary<string, string>();
            try
            {
                dictReport.Add(BMC.Common.ResourceExtensions.GetResourceTextByKey(null, 0, "Key_SelectReport"), BMC.Common.ResourceExtensions.GetResourceTextByKey(null, 0, "Key_SelectReport")); //"Select Report");

                XDocument xmlDoc = XDocument.Load(strReportPath);
                foreach (XElement c in xmlDoc.Descendants("Report"))
                {
                    if (c.Attribute("Value").Value=="rsp_Report_PerUnitPerDayPerformanceReport")
                    {
                        dictReport.Add(BMC.Common.ResourceExtensions.GetResourceTextByKey(null, 0, "Key_PerUnitPerDayPerformanceReport"), c.Attribute("Value").Value);
                    }
                    else
                    {
                        dictReport.Add(c.Attribute("Name").Value, c.Attribute("Value").Value);
                    }
                }

                //XDocument xmlDoc = XDocument.Load(strReportPath);
                //var q = from c in xmlDoc.Descendants("Report")
                //        select new
                //        {
                //            Value = c.Attribute("Value").Value,
                //            Name = c.Attribute("Name").Value
                //        };

                //dictReport = dictReport.Concat(q.ToDictionary(item => item.Name, item => item.Value)).ToDictionary(e => e.Key, e => e.Value);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsBusiness.GetReports " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return dictReport;
        }

        internal Dictionary<int, string> GetBasedOnFilter()
        {
            Dictionary<int, string> dictBasedOn = new Dictionary<int, string>();
            try
            {
                dictBasedOn.Add(1, ResourceExtensions.GetResourceTextByKey(null, "Key_DS_MGMDSession"));
                dictBasedOn.Add(2, ResourceExtensions.GetResourceTextByKey(null, "Key_DS_Read"));
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsBusiness.GetBasedOnFilter " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return dictBasedOn;
        }

        internal DataSet PopulateCompany(int UserID)
        {
            try
            {
                CustomReportsDAC objDAC = new CustomReportsDAC();
                return objDAC.PopulateCompany(UserID);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsBusiness.PopulateCompany " + ex.Message, LogManager.enumLogLevel.Error);
                return new DataSet();
            }
        }

        internal DataSet PopulateSubCompany(int nCompanyID, int UserId)
        {
            try
            {
                CustomReportsDAC objDAC = new CustomReportsDAC();
                DataSet objData = objDAC.PopulateSubCompany(nCompanyID,UserId);

                DataRow row = objData.Tables[0].NewRow();
                row["Sub_Company_ID"] = 0;
                row["Sub_Company_Name"] = BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_All");//"All";
                objData.Tables[0].Rows.InsertAt(row, 0);
                return objData;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsBusiness.PopulateSubCompany " + ex.Message, LogManager.enumLogLevel.Error);
                return new DataSet();
            }
        }

        internal DataSet PopulateSites(int nSubCompanyID, int nCompanyID,int UserId)
        {
            try
            {
                CustomReportsDAC objDAC = new CustomReportsDAC();
                DataSet objData = objDAC.PopulateSites(nSubCompanyID, nCompanyID,UserId);

                DataRow row = objData.Tables[0].NewRow();
                row["Site_ID"] = 0;
                row["Site_Name"] = BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_All"); //"All";
                objData.Tables[0].Rows.InsertAt(row, 0);
                return objData;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsBusiness.PopulateSites " + ex.Message, LogManager.enumLogLevel.Error);
                return new DataSet();
            }
        }

        internal DataSet PopulateZones(int CompanyId, int nSubCompanyId, int nSiteID)
        {
            try
            {
                CustomReportsDAC objDAC = new CustomReportsDAC();
                DataSet objData = objDAC.PopulateZones(CompanyId, nSubCompanyId, nSiteID);

                DataRow row = objData.Tables[0].NewRow();
                row["Zone_ID"] = 0;
                row["Zone_Name"] = "All";
                objData.Tables[0].Rows.InsertAt(row, 0);
                return objData;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsBusiness.PopulateZones " + ex.Message, LogManager.enumLogLevel.Error);
                return new DataSet();
            }
        }

        internal DataSet GetPUPDPerformanceReport(int nBasedOn, int nCompanyID, int nSubCompanyID, int nSiteID, int nZoneID, DateTime dtStartDate, DateTime dtEndDate)
        {
            try
            {
                CustomReportsDAC objDAC = new CustomReportsDAC();
                return objDAC.GetPUPDPerformanceReport(nBasedOn, nCompanyID, nSubCompanyID, nSiteID, nZoneID, dtStartDate, dtEndDate);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in the CustomReportsBusiness.GetPUPDPerformanceReport " + ex.Message, LogManager.enumLogLevel.Error);
                return new DataSet();
            }
        }
    }
}
