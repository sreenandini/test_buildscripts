using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;
using System.Configuration;

namespace BMC.EnterpriseReportsDataAccess
{
    public class ReportServerSetting
    {
        ReportDataContext oReportDataContext = new ReportDataContext();       
        public  string ReportPathURL()
        {
            string strResult = string.Empty;
            try
            {

               oReportDataContext.GetSetting(Convert.ToInt32("0"), "ReportServerURL", string.Empty, ref strResult);
            }
            catch (NullReferenceException nux)
            {
                ExceptionManager.Publish(nux);
            }
            catch (ObjectDisposedException obx)
            {
                ExceptionManager.Publish(obx);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return strResult;
        }

        public  string ReportFolder()
        {
            string strResult = string.Empty;
            try
            {

                oReportDataContext.GetSetting(Convert.ToInt32("0"), "ReportFolder", string.Empty, ref strResult);
            }
            catch (NullReferenceException nux)
            {
                ExceptionManager.Publish(nux);
            }
            catch (ObjectDisposedException obx)
            {
                ExceptionManager.Publish(obx);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return strResult;
        }

        public string GenerateURL(string strURL)
        {
            string strReturn = string.Empty;
            strReturn = ConfigurationManager.AppSettings["protocol"].Trim() + strURL.Trim() + ConfigurationManager.AppSettings["ReportServerInstance"].Trim();
            return strReturn;
        }

        public string GetReportMessageException()
        {
            string sMessage = string.Empty;
            oReportDataContext.GetSetting(Convert.ToInt32("0"), "EmptyReportMessage", string.Empty, ref sMessage);
            return sMessage;
        }

        #region Generate Report server URL

        public string GetRegulatory()
        {
            string strResult = string.Empty;
            try
            {

                oReportDataContext.GetSetting(Convert.ToInt32("0"), "RegulatoryType", string.Empty, ref strResult);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return strResult;
        }
        public string GetCurrentCurrenyCulture()
        {
            string strResult = string.Empty;
            try
            {

                oReportDataContext.GetSetting(Convert.ToInt32("0"), "BMC_Reports_Language", "en-US", ref strResult);
            }

            catch (Exception ex)
            {
                strResult = "en-US";
                ExceptionManager.Publish(ex);
            }
            return strResult;
        }

        #endregion 
    }
}
