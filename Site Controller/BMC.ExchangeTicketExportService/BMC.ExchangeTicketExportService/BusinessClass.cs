using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using BMC.Business.CashDeskOperator.WebServices;
using System.Threading;
using System.Web.Services.Protocols;
using System.Configuration;

namespace BMC.ExchangeTicketExportService
{
    public static class BusinessClass
    {
        public static ManualResetEvent m_reset = new ManualResetEvent(false);
        private static Int32 perItemInterval = 0;
        
		static BusinessClass()
        {
            try
            {
                perItemInterval = Convert.ToInt32(ConfigurationSettings.AppSettings["PerItemProcessMilliseconds"]);
            }
            catch (Exception)
            {
                perItemInterval = 100;
            }
        }

        public static bool ExportTicketDetails()
        {
            DataAccess objDataAccess = new DataAccess();
            DataTable dtRecords = new DataTable();
            bool bStatus = true;

            try
            {
                //Step 1 - Check the Ticket_Export_History to check if there are any records to be processed.
                if (objDataAccess.CheckDataToExport())
                {
                    //Step 2 - Get records to be Exported.
                    dtRecords = objDataAccess.GetDataToExport();

                    //Step 3 - Export the records.
                    foreach (DataRow drRecord in dtRecords.Rows)
                    {
                        //Process the record and Update the status of the export.
                        try
                        {
                           ExportTicketData(Convert.ToInt32(drRecord["TEH_ID"]), Convert.ToInt32(drRecord["TEH_ReferenceID"]), drRecord["TEH_Type"].ToString());
                            LogManager.WriteLog("Processed record in the Ticket_Export table with id - " + drRecord["TEH_ID"].ToString(), LogManager.enumLogLevel.Info);
                        }
                        catch (Exception exExport)
                        {
                            //Dont do anything. Skip this record and continue with the next one.
                            LogManager.WriteLog("Processing failed for record in the Ticket_Export table with id - " + drRecord["TEH_ID"].ToString(), LogManager.enumLogLevel.Info);
                        }
                        if (m_reset.WaitOne(perItemInterval))
                                break;
                    }
                }
                else
                {
                    LogManager.WriteLog(" No record in Ticket_Export table to export.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception exExportTicketDetails)
            {
                ExceptionManager.Publish(exExportTicketDetails);
                bStatus = false;
            }

            return bStatus;
        }

        private static void ExportTicketData(int iTEHID, int iRefID, string strExportType)
        {
            string strXMLData = string.Empty;
            bool bStatus = false;
            string strErrorMessage = string.Empty;
            string strSiteCode = string.Empty;

            try
            {
                strSiteCode = DataAccess.GetSiteCode();
                int iSiteCode = Convert.ToInt32(strSiteCode.Trim());

                Proxy webProxy = new Proxy(strSiteCode);
                strErrorMessage = "Export Failed.";

                switch (strExportType.ToLower().Trim())
                {
                    case "voucher":
                        strXMLData = DataAccess.GetVoucherDetailsToExport(iRefID);
                        //LogManager.WriteLog("Voucher data - " + strXMLData, LogManager.enumLogLevel.Info);
                        bStatus = webProxy.ImportVoucherDetails(strXMLData);
                        LogManager.WriteLog("Voucher Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    case "ticket_exception":
                        strXMLData = DataAccess.GetTicketExceptionDetailsToExport(iRefID);
                        //LogManager.WriteLog("Ticket_Exception data - " + strXMLData, LogManager.enumLogLevel.Info);
                        bStatus = webProxy.ImportTicketExceptionDetails(strXMLData, iSiteCode);
                        LogManager.WriteLog("Ticket Exception Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    case "device":
                        strXMLData = DataAccess.GetDeviceDetailsToExport(iRefID);
                        //LogManager.WriteLog("Device data - " + strXMLData, LogManager.enumLogLevel.Info);
                        bStatus = webProxy.ImportDeviceDetails(strXMLData);
                        LogManager.WriteLog("Device Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    case "promotions":
                        strXMLData = DataAccess.GetPromotionsDetailsToExport(iRefID);
                       
                        bStatus = webProxy.ImportPromotionsDetails(strXMLData,iSiteCode);
                        LogManager.WriteLog("Promotions Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    case "promotionaltickets":
                        strXMLData = DataAccess.GetPromotionalTicketsDetailsToExport(iRefID);
                       
                        bStatus = webProxy.ImportPromotionalTicketsDetails(strXMLData,iSiteCode);
                        LogManager.WriteLog("Promotional Tickets Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    case "extsyspromoticket":
                        strXMLData = DataAccess.GetTISPromotionalTicketsDetailsToExport(iRefID);

                        bStatus = webProxy.ImportPromotionalTicketsDetails(strXMLData, iSiteCode);
                        LogManager.WriteLog("TIS Promotional Tickets Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    case "extvoucherdetails":
                        strXMLData = DataAccess.GetExternalVoucherDetailsToExport(iRefID);

                        bStatus = webProxy.ImportExternalVoucherDetails(strXMLData, iSiteCode);
                        LogManager.WriteLog("External Voucher Details Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;

                }
            }
            catch (Exception exExportData)
            {
                bStatus = false;
                LogManager.WriteLog("Import Process Failed id - " + iTEHID.ToString(), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(exExportData);
            }
            
            //Call UpdateExportStatus to update the status of the Export.
            UpdateTicketExportStatus(iTEHID, bStatus);
       }

        private static void UpdateTicketExportStatus(int iTEHID, bool bStatus)
        {
            try
            {
                if (bStatus)
                {
                    //Update the status as 100(Completely successfully.)
                    DataAccess.UpdateExportStatus(iTEHID, 100);
                }
                else
                {
                    //Update the status as 100(Completely successfully.)
                    DataAccess.UpdateExportStatus(iTEHID, 1);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static bool ExportCVDetails()
        {
            DataAccess objDataAccess = new DataAccess();
            DataTable dtRecords = new DataTable();
            bool bStatus = true;

            try
            {
                //Step 1 - Check the Ticket_Export_History to check if there are any records to be processed.
                if (objDataAccess.CheckCVDataToExport())
                {
                    //Step 2 - Get records to be Exported.
                    dtRecords = objDataAccess.GetCVDataToExport();

                    //Step 3 - Export the records.
                    foreach (DataRow drRecord in dtRecords.Rows)
                    {
                        //Process the record and Update the status of the export.
                        try
                        {
                            ExportCVData(Convert.ToInt32(drRecord["CVEH_ID"]), Convert.ToInt32(drRecord["CVEH_ReferenceID"]), drRecord["CVEH_Type"].ToString());
                            LogManager.WriteLog("Processed record in the CV_Export table with id - " + drRecord["CVEH_ID"].ToString(), LogManager.enumLogLevel.Info);
                        }
                        catch (Exception exExport)
                        {
                            //Dont do anything. Skip this record and continue with the next one.
                            LogManager.WriteLog("Processing failed for record in the CV_Export table with id - " + drRecord["CVEH_ID"].ToString(), LogManager.enumLogLevel.Info);
                            break;
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog(" No record in CV_Export table to export.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception exExportCVDetails)
            {
                LogManager.WriteLog("CV_Export Failed.", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(exExportCVDetails);
                bStatus = false;
            }

            return bStatus;
        }

        private static void ExportCVData(int iCVEHID, int iRefID, string strExportType)
        {
            string strXMLData = string.Empty;
            bool bStatus = false;
            string strErrorMessage = string.Empty;
            string strSiteCode = string.Empty;

            try
            {
                strSiteCode = DataAccess.GetSiteCode();
                int iSiteCode = Convert.ToInt32(strSiteCode.Trim());

                Proxy webProxy = new Proxy(strSiteCode);
                strErrorMessage = "Export Failed.";

                switch (strExportType.Trim())
                {
                    case "COMPONENTDETAILS":
                        strXMLData = DataAccess.ExportComponentDetails(iRefID);
                        bStatus = webProxy.UpdateDetailsFromXML("COMPONENTDETAILS", strXMLData);
                        LogManager.WriteLog("COMPONENTDETAILS Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    case "MACHINECOMPONENTDETAILS":
                        strXMLData = DataAccess.ExportMachineComponentDetails(iRefID);
                        bStatus = webProxy.UpdateDetailsFromXML("MACHINECOMPONENTDETAILS", strXMLData);
                        LogManager.WriteLog("MACHINECOMPONENTDETAILS Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    case "COMPVERIFICATIONRECORD":
                        strXMLData = DataAccess.ExportComponentVerificationDetails(iRefID);
                        bStatus = webProxy.UpdateDetailsFromXML("COMPVERIFICATIONRECORD", strXMLData);
                        LogManager.WriteLog("COMPVERIFICATIONRECORD Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    case "AUTHENTICATECOMPONENT":
                        strXMLData = DataAccess.ExportAuthenticateComponent(iRefID);
                        bStatus = webProxy.UpdateDetailsFromXML("AUTHENTICATECOMPONENT", strXMLData);
                        LogManager.WriteLog("AUTHENTICATECOMPONENT Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    case "COMPONENTCOUNT":
                        strXMLData = DataAccess.ExportComponentCount(iRefID);
                        bStatus = webProxy.UpdateDetailsFromXML("COMPONENTCOUNT", strXMLData);
                        LogManager.WriteLog("COMPONENTCOUNT Import Process Completed - " + bStatus.ToString(), LogManager.enumLogLevel.Info);
                        break;
                }
            }
            catch (Exception exExportData)
            {
                bStatus = false;
                LogManager.WriteLog("Import Process Failed id - " + iCVEHID.ToString(), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(exExportData);
            }

            //Call UpdateExportStatus to update the status of the Export.
            UpdateCVExportStatus(iCVEHID, bStatus);
        }

        private static void UpdateCVExportStatus(int iTEHID, bool bStatus)
        {
            if (bStatus)
            {
                //Update the status as 100(Completely successfully.)
                DataAccess.UpdateCVExportStatus(iTEHID, 100);
            }
            else
            {
                //Update the status as 100(Completely successfully.)
                DataAccess.UpdateCVExportStatus(iTEHID, 1);
            }
        }

        public static bool IsRegulatoryEnabled()
        {
            try
            {
                string IsRegulatoryEnabled = DataAccess.GetSettingFromDB("IsRegulatoryEnabled", "False");
                string IsRegulatoryAAMS = DataAccess.GetSettingFromDB("RegulatoryType", "A");

                LogManager.WriteLog(string.Format("{0} - {1}, {2} - {3}", "Regulatory Enabled", IsRegulatoryEnabled.ToUpper(),
                                    "Regulatory Type", IsRegulatoryAAMS.ToUpper()), LogManager.enumLogLevel.Info);

                if (IsRegulatoryEnabled.ToUpper() == "TRUE" && IsRegulatoryAAMS.ToUpper() == "AAMS")
                    return true;

                return false;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool CheckSiteStatus()
        {
            LogManager.WriteLog("Inside CheckSiteStatus method", LogManager.enumLogLevel.Info);

            return DataAccess.GetSiteStatus();
        }
    }
}
