using BMC.Guardian.Transport;
using System;
using System.Data;
using System.Xml;
using BMC.Guardian.DBHelper;
using BMC.Security.Interfaces;
using BMC.Security;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.ConfigurationManagement;

namespace BMC.Guardian.Business
{
    

    public static class GuardianBL
    {
        public static string GetAllServiceStatus(string siteName)
        {
            try
            {
                string siteStatus = GuardianDBHelper.GetSiteStatus(siteName);
                if (!string.IsNullOrEmpty(siteStatus))
                {
                    return GetServiceStatus(siteStatus); 
                }
                return "Not able to get status. ";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetAllServiceStatus::" + ex.Message, LogManager.enumLogLevel.Error);
                return "Not able to get status. ";
            }
        }

        public static string GetAllServiceStatus(string siteName, string siteStatus)
        {
            try
            {
                if (!string.IsNullOrEmpty(siteStatus))
                {
                    return GetServiceStatus(siteStatus);
                }
                return "Not able to get status. ";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetAllServiceStatus::" + ex.Message, LogManager.enumLogLevel.Error);
                return "Not able to get status. ";
            }
        }

        public static GuardianDataSet.DiskDetailsDataTable GetDiskSpace(XmlDocument xmlDocument)
        {
            try
            {
                if (xmlDocument.DocumentElement.HasChildNodes)
                {
                    XmlNode node = xmlDocument.SelectSingleNode("Site/DiskSpace");
                    if ((node != null) && node.HasChildNodes)
                    {
                        GuardianDataSet.DiskDetailsDataTable table = new GuardianDataSet.DiskDetailsDataTable();
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            GuardianDataSet.DiskDetailsRow row = table.NewDiskDetailsRow();
                            row.DriveName = node2["DriveName"].InnerText.ToString();
                            row.DriveSpace = node2["DriveSpace"].InnerText.ToString();
                            table.AddDiskDetailsRow(row);
                        }
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetDiskSpace::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return null;
        }

        public static GuardianDataSet.SystemLogsDataTable GetSystemLogs(XmlDocument xmlDocument)
        {
            try
            {
                if (xmlDocument.DocumentElement.HasChildNodes)
                {
                    XmlNode node = xmlDocument.SelectSingleNode("Site/SystemLogs/DocumentElement");
                    if ((node != null) && node.HasChildNodes)
                    {
                        GuardianDataSet.SystemLogsDataTable table = new GuardianDataSet.SystemLogsDataTable();
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            GuardianDataSet.SystemLogsRow row = table.NewSystemLogsRow();
                            row.Message = node2["Message"].InnerText.ToString();
                            row.TimeGenerated= node2["TimeGenerated"].InnerText.ToString();
                            table.AddSystemLogsRow(row);
                        }
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetSystemLogs::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return null;
        }

        public static FIFOStatus GetFIFOStatus()
        {
            return GuardianDBHelper.GetFIFOStatus();
        }

        public static GuardianDataSet.HourlyStatisticsDataTable GetHourlyStatistics(XmlDocument xmlDocument)
        {
            try
            {
                if (xmlDocument.DocumentElement.HasChildNodes)
                {
                    GuardianDataSet.HourlyStatisticsDataTable table = new GuardianDataSet.HourlyStatisticsDataTable();
                    GuardianDataSet.HourlyStatisticsRow row = table.NewHourlyStatisticsRow();
                    XmlNode node = xmlDocument.SelectSingleNode("Site/HourlyStatistics/Drop");
                    if (node != null)
                    {
                        row.Drop = node.InnerText;
                    }
                    node = xmlDocument.SelectSingleNode("Site/HourlyStatistics/HandPay");
                    if (node != null)
                    {
                        row.HandPay = node.InnerText;
                    }
                    node = xmlDocument.SelectSingleNode("Site/HourlyStatistics/Jackpot");
                    if (node != null)
                    {
                        row.Jackpot = node.InnerText;
                    }
                    node = xmlDocument.SelectSingleNode("Site/HourlyStatistics/TicketsPrinted");
                    if (node != null)
                    {
                        row.TicketsPrinted = node.InnerText;
                    }
                    table.AddHourlyStatisticsRow(row);
                    return table;
                }
            }
            catch (Exception exception)
            {
                LogManager.WriteLog("GuardianBL.GetHourly" + exception.Message, LogManager.enumLogLevel.Error);
            }
            return null;
        }

        private static LastProcessedDetails GetLastProcessed(string XMLDoc)
        {
            LastProcessedDetails details = new LastProcessedDetails();
            XmlDocument document = new XmlDocument();
            try
            {
                document.LoadXml(XMLDoc);
                if (document.DocumentElement.HasChildNodes)
                {
                    XmlNode node = document.SelectSingleNode("Site/Status");
                    if ((node != null) && node.HasChildNodes)
                    {
                        if (node["DateTime"] != null)
                        {
                            details.DateTime = node["DateTime"].InnerText;
                        }
                        if (node["Last_Read_Created"] != null)
                        {
                            details.LastReadCreated = node["Last_Read_Created"].InnerText;
                        }
                        if (node["Last_Hour_Created"] != null)
                        {
                            details.LastHourCreated = node["Last_Hour_Created"].InnerText;
                        }
                        if (node["Last_Drop_Created"] != null)
                        {
                            details.LastDropCreated = node["Last_Drop_Created"].InnerText;
                        }
                        if (node["Last_Record_Exported"] != null)
                        {
                            details.LastRecordExported = node["Last_Record_Exported"].InnerText;
                        }
                        if (node["Export_Records_To_Process"] != null)
                        {
                            details.ExportRecordsToProcess = node["Export_Records_To_Process"].InnerText;
                        }
                        //if (node["DB_Version"] != null)
                        //{
                        //    details.DBVersion = node["DB_Version"].InnerText;
                        //}
                        if (node["BMC_Version"] != null)
                        {
                            details.BMCVersion = node["BMC_Version"].InnerText;
                            details.DBVersion = node["BMC_Version"].InnerText;
                        }
                        if (node["HourlyReadHour"] != null)
                        {
                            details.HourlyReadHour = node["HourlyReadHour"].InnerText;
                        }
                        if (node["SiteName"] != null)
                        {
                            details.Site_Code = node["SiteName"].InnerText;
                        }
                        if (node["ReadDate"] != null)
                        {
                            details.ReadDate = node["ReadDate"].InnerText;
                        }
                        if (node["ReadTime"] != null)
                        {
                            details.ReadTime = node["ReadTime"].InnerText;
                        }
                        if (node["LastHourlyDate"] != null)
                        {
                            details.LastHourlyDate = node["LastHourlyDate"].InnerText;
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetLastProcessed::" + ex.Message, LogManager.enumLogLevel.Error);    
            }
            return details;
        }

        public static LastProcessedDetails GetLastProcessedDetails(string siteStatus)
        {
            LastProcessedDetails details2 = null;
            try
            {
                //string siteStatus = GuardianDBHelper.GetSiteStatus(siteName);
                if (!string.IsNullOrEmpty(siteStatus))
                {
                    return GetLastProcessed(siteStatus);
                  
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetLastProcessed::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return details2;
        }

        public static string GetServiceStatus(string siteStatus)
        {
            try
            {
                GuardianDataSet.ServiceDetailsDataTable table;
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(siteStatus);
                if (xmlDocument.DocumentElement.HasChildNodes)
                {
                    XmlNode node = xmlDocument.SelectSingleNode("Site/DocumentElement");
                    if ((node != null) && node.HasChildNodes)
                    {
                         table = new GuardianDataSet.ServiceDetailsDataTable();
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            GuardianDataSet.ServiceDetailsRow row = table.NewServiceDetailsRow();
                            row.ServiceName = node2["ServiceName"].InnerText.ToString();
                            row.ServiceStatus = node2["Status"].InnerText.ToString();
                            table.AddServiceDetailsRow(row);
                        }
                        if (table != null)
                        {
                            if (table.Where<GuardianDataSet.ServiceDetailsRow>(delegate(GuardianDataSet.ServiceDetailsRow dataRow)
                            {
                                return (dataRow.ServiceStatus.ToUpper() != "RUNNING");
                            }).GetEnumerator().MoveNext())
                            {
                                return "One or More Services are not running";
                            }
                            return "All Services Running";
                        }
                        return "Not able to get status. ";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetServiceStatus::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return "Not able to get status. ";
        }

        public static GuardianDataSet.ServiceDetailsDataTable GetServiceStatusasTable(string siteStatus)
        {
            GuardianDataSet.ServiceDetailsDataTable table = new GuardianDataSet.ServiceDetailsDataTable();
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(siteStatus);
                if (xmlDocument.DocumentElement.HasChildNodes)
                {
                    XmlNode node = xmlDocument.SelectSingleNode("Site/DocumentElement");
                    if ((node != null) && node.HasChildNodes)
                    {
                        table = new GuardianDataSet.ServiceDetailsDataTable();
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            GuardianDataSet.ServiceDetailsRow row = table.NewServiceDetailsRow();
                            row.ServiceName = node2["ServiceName"].InnerText.ToString();
                            row.ServiceStatus = node2["Status"].InnerText.ToString();
                            table.AddServiceDetailsRow(row);
                        }
                        if (table != null)
                        {
                            return table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetServiceStatus::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return table;
        }

        public static GuardianDataSet.SiteInterrogationDataTable GetSiteInterrogationDetails(XmlDocument xmlDocument)
        {
            try
            {
                if (xmlDocument.DocumentElement.HasChildNodes)
                {
                    XmlNode node = xmlDocument.SelectSingleNode("Site/SiteInterrogationInfo");
                    if ((node != null) && node.HasChildNodes)
                    {
                        GuardianDataSet.SiteInterrogationDataTable table = new GuardianDataSet.SiteInterrogationDataTable();
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            GuardianDataSet.SiteInterrogationRow row = table.NewSiteInterrogationRow();
                            row.Position = node2["Position"].InnerText;
                            row.Asset = node2["Asset"].InnerText;
                            row.GMUToServer = node2["GMUToServer"].InnerText;
                            row.GMUToMachine = node2["GMUToMachine"].InnerText;
                            row.MachineStatus = node2["MachineStatus"].InnerText;
                            row.GMUVersion = node2["GMUVersion"].InnerText;
                            //row.BAD_AAMS_Status = node2["BAD_AAMS_Status"].InnerText;
                            //row.BAD_Verification_Status = node2["BAD_Verification_Status"].InnerText;
                            //row.Game_AAMS_Status = node2["Game_AAMS_Status"].InnerText;
                            //row.Game_Verification = node2["Game_Verification"].InnerText;
                            table.AddSiteInterrogationRow(row);
                        }
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetSiteInterrogation::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return null;
        }

        public static GuardianDataSet.SiteVLTStatusDataTable GetSiteVLTStatusDetails(XmlDocument xmlDocument)
        {
            try
            {
                           
                if (xmlDocument.DocumentElement.HasChildNodes)
                {
                    XmlNode node = xmlDocument.SelectSingleNode("Site/VLTStatus");
                    if ((node != null) && node.HasChildNodes)
                    {
                        GuardianDataSet.SiteVLTStatusDataTable table = new GuardianDataSet.SiteVLTStatusDataTable();
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            GuardianDataSet.SiteVLTStatusRow row = table.NewSiteVLTStatusRow();
                            row.Position = node2["Position"].InnerText;
                            row.BAD_AAMS_Status = node2["BAD_AAMS_Status"].InnerText;
                            row.BAD_Verification_Status = node2["BAD_Verification_Status"].InnerText;
                            row.Game_AAMS_Status = node2["Game_Install_AAMS_Status"].InnerText;
                            row.Game_Verification = node2["Game_Verification"].InnerText;
                            row.Game_Enable_AAMS_Status = node2["Game_Enable_AAMS_Status"].InnerText;
                            row.BAD_AAMS_EnableDisable = node2["BAD_AAMS_EnableDisable"].InnerText;
                            row.BMC_Enterprise_Status = node2["BMC_Enterprise_Status"].InnerText;
                            table.AddSiteVLTStatusRow(row);
                        }
                        return table;
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionManager.Publish(exception);
            }
            return null;
        }

        public static GuardianDataSet.SiteDetailsDataTable GetSiteList(bool isActiveChecked, Int32 securityUserID, Int32 status, Int32 statusInterval)
        {
            return GuardianDBHelper.GetSiteList(isActiveChecked, securityUserID, status, statusInterval);
        }

        public static SecurityHelper.LoginResults DoLogin(string userName, string password, out IUser User)
        {
            try
            {
                SecurityHelper.CreateInstance(DBHelper.GuardianDBHelper.EnterpriseConnectionString,true);
                //var loginResult =
                  return  SecurityHelper.Login(userName, password, out User);
                //if ( loginResult == SecurityHelper.LoginResults.LoginSuccesful || loginResult == SecurityHelper.LoginResults.PasswordExpired)
                //{
                //    return true;
                //}
                //return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.DoLogin::" + ex.Message, LogManager.enumLogLevel.Error);
                User = null;
                //return false;
                return SecurityHelper.LoginResults.LoginFailed;
            }
        }

        public static GuardianDataSet.StatusHistoryDataTable GetStatusHistory(string SiteCode)
        {
            GuardianDataSet.StatusHistoryDataTable HistoryTable = new GuardianDataSet.StatusHistoryDataTable();
            GuardianDataSet.StatusHistoryRow historyRow;
            DataTable SiteHistoryTable = DBHelper.GuardianDBHelper.GetSiteStatusHistory(SiteCode);
            try
            {
                foreach ( DataRow dr in SiteHistoryTable.Rows)
                {
                    historyRow = HistoryTable.NewStatusHistoryRow();
                    LastProcessedDetails Details = GetLastProcessed(dr["SiteStatus"].ToString());
                    if (!String.IsNullOrEmpty(Details.DateTime))
                    {
                        historyRow.Fifo = Details.ExportRecordsToProcess;
                        historyRow.ServiceStatus = GetServiceStatus(dr["SiteStatus"].ToString());
                        historyRow.TimeStamp = Details.DateTime;
                        historyRow.RowNumber = dr["RowNumber"].ToString();
                        historyRow.UpdateTimeStamp = ((DateTime)dr["UpdateTimeStamp"]).ToString("yyyy-MM-dd HH:mm:ss.fff");
                        HistoryTable.Rows.Add(historyRow);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetStatusHistory::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return HistoryTable;
        }

        public static string GetStatusForGivenTime(string SiteName, int RowNumber)
        {
            try
            {
                DataTable SiteHistoryTable = DBHelper.GuardianDBHelper.GetSiteStatusHistory(DBHelper.GuardianDBHelper.GetSiteCode(SiteName));
                DataRow dr = SiteHistoryTable.Rows[RowNumber - 1];
                return dr["SiteStatus"].ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetStatusForGivenTime::" + ex.Message, LogManager.enumLogLevel.Error);
                return string.Empty;
            }
        }

        public static string GetStatusForGivenTime(string SiteName, DateTime timestamp)
        {
            try
            {
                DataTable SiteHistoryTable = DBHelper.GuardianDBHelper.GetSiteStatusHistoryByTimeStamp(DBHelper.GuardianDBHelper.GetSiteCode(SiteName), timestamp);
                if (SiteHistoryTable != null && SiteHistoryTable.Rows.Count > 0)
                {
                    return SiteHistoryTable.Rows[0]["SiteStatus"].ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.GetStatusForGivenTime::" + ex.Message, LogManager.enumLogLevel.Error);
                return string.Empty;
            }
        }

        public static void UpdateCurrentStatus(string siteName)
        {
            try
            {
                LogManager.WriteLog("UpdateCurrentStatus:: Entry" , LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Arguments:: siteName : " + siteName, LogManager.enumLogLevel.Debug);
                String siteCode = DBHelper.GuardianDBHelper.GetSiteCode(siteName);
                var WebProxy = new Proxy.Proxy(siteCode);
                if (WebProxy != null)
                {
                    String xml = WebProxy.GetSiteStatus();
                    LogManager.WriteLog("xml :\r\n" + xml, LogManager.enumLogLevel.Debug);
                    if (string.IsNullOrEmpty(xml))
                        return;
                    GuardianDBHelper.UpdateSiteStatus(xml, siteCode);
                    LogManager.WriteLog("siteCode : " + siteCode, LogManager.enumLogLevel.Info);
                }
                DataTable SiteHistoryTable = DBHelper.GuardianDBHelper.GetSiteStatusHistory(DBHelper.GuardianDBHelper.GetSiteCode(siteName));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GuardianBL.UpdateCurrentStatus::" + ex.Message, LogManager.enumLogLevel.Error);                
            }

    }

        public static bool CheckConnectivity(string siteCode, Int32 proxyTimeOut)
        {
            try
            {
                LogManager.WriteLog("GuardianBL.CheckConnectivity:: Entry", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Arguments:: siteCode : " + siteCode, LogManager.enumLogLevel.Debug);
                if (string.IsNullOrEmpty(siteCode))
                {
                    return false;
                }
                var WebProxy = new Proxy.Proxy(siteCode, false);
                if (WebProxy != null)
                {
                    WebProxy.Timeout = proxyTimeOut;
                    Int32 sample = WebProxy.HelloWebService(100);
                    LogManager.WriteLog("sample : " + sample, LogManager.enumLogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GuardianBL.CheckConnectivity::" + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        public static SiteStatusDetails GetSiteStatusDetails(LastProcessedDetails lastProcessedRecord, string userName, string missedHourlies, string region, Int32 proxyTimeout, double intervalFromExchange)
        {
            SiteStatusDetails _SiteStatus = new SiteStatusDetails();
            try
            {
                if (lastProcessedRecord == null) return _SiteStatus;
                GetSiteStatus(lastProcessedRecord, _SiteStatus, proxyTimeout, intervalFromExchange);
                _SiteStatus.MissedHourlies = missedHourlies;
                _SiteStatus.Region = region;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return _SiteStatus;
        }

        public static SiteStatusDetails GetSiteStatusDetails(LastProcessedDetails lastProcessedRecord, string userName, Int32 proxyTimeout, double intervalFromExchange)
        {
            SiteStatusDetails _SiteStatus = new SiteStatusDetails();
            try
            {
                if (lastProcessedRecord == null) return _SiteStatus;
                GetSiteStatus(lastProcessedRecord, _SiteStatus, proxyTimeout, intervalFromExchange);
                string Region = "";
                string MissedHourlies = "";
                new Guardian_LINQBL().GetViewSiteStatusInfo(lastProcessedRecord.Site_Code, userName, ref Region, ref MissedHourlies);
                _SiteStatus.MissedHourlies = MissedHourlies;
                _SiteStatus.Region = Region;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return _SiteStatus;
        }

        private static void GetSiteStatus(LastProcessedDetails lastProcessedRecord, SiteStatusDetails _SiteStatus, Int32 proxyTimeout, double intervalFromExchange)
        {
            _SiteStatus.BMCVersion = lastProcessedRecord.BMCVersion;
            _SiteStatus.DateTime = lastProcessedRecord.DateTime;
            _SiteStatus.DBVersion = lastProcessedRecord.DBVersion;
            _SiteStatus.ExportRecordsToProcess = lastProcessedRecord.ExportRecordsToProcess;
            _SiteStatus.HourlyReadHour = lastProcessedRecord.HourlyReadHour;
            _SiteStatus.LastHourCreated = lastProcessedRecord.LastHourCreated;
            _SiteStatus.LastReadCreated = lastProcessedRecord.LastReadCreated;
            _SiteStatus.LastRecordExported = lastProcessedRecord.LastRecordExported;
            _SiteStatus.Site_Code = lastProcessedRecord.Site_Code;
            _SiteStatus.ReadTime = lastProcessedRecord.ReadTime;
            _SiteStatus.ReadDate = lastProcessedRecord.ReadDate;
            _SiteStatus.LastHourlyDate = lastProcessedRecord.LastHourlyDate;
            if (intervalFromExchange > 0)
            {
                DateTime _tempTime = DateTime.MinValue;
                _SiteStatus.IsSiteDown = false;
                if (DateTime.TryParse(_SiteStatus.DateTime, out _tempTime))
                    _SiteStatus.IsSiteDown = (DateTime.Now.Subtract(_tempTime).TotalSeconds > intervalFromExchange);
            }
            else
            {
                _SiteStatus.IsSiteDown = !CheckConnectivity(_SiteStatus.Site_Code, proxyTimeout);
            }
        }

        public static BMC.Guardian.Business.Guardian_LINQBL.AuthenticateAndGetUserResult AuthenticateAndGetUser(string userName, string password, ref UserEntity entity)
        {
            return new Guardian_LINQBL().AuthenticateAndGetUser(userName, password, ref entity);
        }

        public static int usp_LockByUserName(string userName)
        {
            try
            {
                return new Guardian_LINQBL().usp_LockByUserName(userName);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return 0;
        }

    }
}


