using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.DataAccess;
using System.Threading;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using System.Diagnostics.Eventing.Reader;

namespace BMC.Monitoring
{
    public class BMCMonitoring
    {
        #region Enum

        public enum ServiceTypes
        {
            All,
            Running,
            NotRunning
        }

        #endregion

        #region Fields

        private static readonly string ConnectionString = string.Empty;
        private static readonly string ServiceErrorString = "Service not found";
        private static readonly string ProcGetSiteStats = "rsp_GetSiteStats";
        private static readonly string ProcSiteInterrogationInfo = "rsp_GetSiteInterrogationInfo";
        private static readonly string Proc24HourStatisticsByType3 = "rsp_24HourStatisticsByType3";
        private static readonly object obj_lock=new object ();
        #endregion

        #region Construction

        static BMCMonitoring()
        {
            ConnectionString = GetConnectionString();
        }

        #endregion

        #region Public Methods

        private DataTable GetStartupShutdownLogs(int NoofEventstoGet)
        {

            DataTable dtLogs = new DataTable("SystemLogs");
            dtLogs.Columns.Add("Message");
            dtLogs.Columns.Add("TimeGenerated");
            dtLogs.Columns.Add("ID");
            DataRow dr;
            try
            {
                string query = "*[System/EventID=6005 or System/EventID=6006]";
                EventLogQuery eventsQuery = new EventLogQuery("System", PathType.LogName, query);

                try
                {
                    EventLogReader logReader = new EventLogReader(eventsQuery);
                    int i = 1;
                    for (EventRecord eventdetail = logReader.ReadEvent(); eventdetail != null; eventdetail = logReader.ReadEvent())
                    {
                        dr = dtLogs.NewRow();
                        dr["Message"] = eventdetail.Id == 6005 ? "System Startup" : "System Shutdown";
                        dr["TimeGenerated"] = eventdetail.TimeCreated.ToString();
                        dr["ID"] = i++;
                        dtLogs.Rows.Add(dr);

                    }
                    if (dtLogs.Rows.Count > NoofEventstoGet)
                    {

                        DataView dview = dtLogs.DefaultView;
                        dview.RowFilter = "ID <=" + i + " AND ID >=" + (i - NoofEventstoGet);
                        dview.Sort = "ID Desc";
                        dtLogs = dview.ToTable();

                    }

                }
                catch (EventLogNotFoundException ex)
                {
                    LogManager.WriteLog("Error while reading the event logs" + ex.Message, LogManager.enumLogLevel.Error);
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetSystemLogs:" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return dtLogs;
        }



        public DataTable GetEventLogs(string serviceName)
        {
            try
            {
                DataTable dataTableEvents = new DataTable("EventLogs");
                dataTableEvents.Columns.Add("ServiceName");
                dataTableEvents.Columns.Add("Message");
                dataTableEvents.Columns.Add("TimeGenerated");
                EventLog[] eventLogs = EventLog.GetEventLogs();

                foreach (EventLog eventLog in eventLogs)
                {
                    EventLogEntryCollection entries = eventLog.Entries;
                    for (int i = entries.Count - 1; i >= 0; i--)
                    {
                        if (entries[i].Source.Trim().ToUpper() == serviceName.Trim().ToUpper())
                        {
                            DataRow dataRow = dataTableEvents.NewRow();
                            dataRow["ServiceName"] = serviceName;
                            dataRow["Message"] = entries[i].Message;
                            dataRow["TimeGenerated"] = entries[i].TimeGenerated;
                            dataTableEvents.Rows.Add(dataRow);
                        }
                    }
                }
                return dataTableEvents;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable("Error");
            }

        }

        public bool StartService(string serviceName)
        {
            try
            {
                return RestartService(serviceName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RestartService(string serviceName)
        {
            try
            {
                using (ServiceController serviceController = new ServiceController())
                {
                    serviceController.ServiceName = serviceName;
                    try
                    {
                        if ((serviceController.Status == ServiceControllerStatus.Running) ||
                            (serviceController.Status == ServiceControllerStatus.Paused))
                        {
                            serviceController.Stop();
                            serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                        }
                        else if ((serviceController.Status == ServiceControllerStatus.StartPending) ||
                                 (serviceController.Status == ServiceControllerStatus.PausePending) ||
                                 (serviceController.Status == ServiceControllerStatus.StopPending))
                        {
                            serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                        }

                        serviceController.Start();
                        return true;
                    }
                    catch (InvalidOperationException ex)
                    {
                        
                        ExceptionManager.Publish(ex);
                        //  throw new Exception("Invalid Operation - Please make sure the service exists in the machine or try again later.");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

		public bool StartService(string serviceName, TimeSpan span)
        {
            try
            {
                return RestartService(serviceName, span);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RestartService(string serviceName, TimeSpan span)
        {
            try
            {
                using (ServiceController serviceController = new ServiceController())
                {
                    serviceController.ServiceName = serviceName;
                    try
                    {
                        if ((serviceController.Status == ServiceControllerStatus.Running) ||
                            (serviceController.Status == ServiceControllerStatus.Paused))
                        {
                            serviceController.Stop();
                            serviceController.WaitForStatus(ServiceControllerStatus.Stopped,span);
                        }
                        else if ((serviceController.Status == ServiceControllerStatus.PausePending) ||
                                 (serviceController.Status == ServiceControllerStatus.StopPending))
                        {
                            serviceController.WaitForStatus(ServiceControllerStatus.Stopped,span);
                        }
                        else if(serviceController.Status == ServiceControllerStatus.StartPending)
                        {
                            serviceController.WaitForStatus(ServiceControllerStatus.Running, span);
                            serviceController.Stop();
                            serviceController.WaitForStatus(ServiceControllerStatus.Stopped, span);
                        }
                        serviceController.Start();
                        serviceController.WaitForStatus(ServiceControllerStatus.Running, span);
                        return true;
                    }
                    catch (InvalidOperationException ex)
                    {
                        ExceptionManager.Publish(ex);
                        //  throw new Exception("Invalid Operation - Please make sure the service exists in the machine or try again later.");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public DataTable GetServiceStatus(string allServiceNames, ServiceTypes status)
        {
            try
            {
                lock (obj_lock)
                {
                DataTable dataTableServices = new DataTable("Services");
                dataTableServices.Columns.Add("ServiceName");
                dataTableServices.Columns.Add("Status");
                string[] serviceNames = allServiceNames.Split(',');

                foreach (string serviceName in serviceNames)
                {
                    if (!string.IsNullOrEmpty(serviceName))
                    {
                        using (ServiceController serviceController = new ServiceController())
                        {
                            serviceController.ServiceName = serviceName.Trim();

                            switch (status)
                            {
                                case ServiceTypes.All:
                                    PopulateServiceData(dataTableServices, serviceName, serviceController);
                                    break;
                                case ServiceTypes.Running:
                                    try
                                    {
                                        if (serviceController.Status == ServiceControllerStatus.Running)
                                            PopulateServiceData(dataTableServices, serviceName, serviceController);
                                    }
                                    catch (InvalidOperationException)
                                    { }
                                    break;
                                case ServiceTypes.NotRunning:
                                    try
                                    {
                                        if (serviceController.Status != ServiceControllerStatus.Running)
                                            PopulateServiceData(dataTableServices, serviceName, serviceController);
                                    }
                                    catch (InvalidOperationException)
                                    {
                                        PopulateServiceData(dataTableServices, serviceName, serviceController);
                                    }
                                    break;
                            }
                        }
                    }
                }
                return dataTableServices;
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable("Error");
            }
        }

        private string GetSiteStats()
        {
            try
            {
                SqlParameter[] objparams = new SqlParameter[2];
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "HourlyReadHour";
                param1.Direction = ParameterDirection.Input;
                param1.Value = GetHourlyReadHour();
                objparams[0] = param1;
                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "ReadDate";
                param2.Direction = ParameterDirection.Input;
                param2.Value = GetReadDate();
                objparams[1] = param2;
                
                return SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, ProcGetSiteStats, objparams).ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }

        public string GetSiteStatus(string serviceNames, int NoOfSystemLogs)
        {
            string siteStatus = "<Site>";
            StringWriter stringWriter = new StringWriter();
            StringWriter EventsWriter = new StringWriter(); 
            try
            {
                LogManager.WriteLog("Try to Get Site Status.. ", LogManager.enumLogLevel.Info);
                GetServiceStatus(serviceNames, ServiceTypes.All).WriteXml(stringWriter);
                GetStartupShutdownLogs(NoOfSystemLogs).WriteXml(EventsWriter);
                siteStatus += stringWriter.ToString();
                siteStatus += GetSiteStats();
                siteStatus += GetDriveSpace();
                siteStatus += GetSiteInterrogationInfo();
                siteStatus += GetVLTStatusInformation();
                siteStatus += "<SystemLogs>" + EventsWriter.ToString() + "</SystemLogs>";
                //siteStatus += GetHourlyStatistics();
                // siteStatus += "</Site>";
                //    return siteStatus;
                LogManager.WriteLog("Site Status got successfully", LogManager.enumLogLevel.Error);    
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                siteStatus = "<Site>";
            }
            

            return siteStatus + "</Site>";
        }

        public string GetSiteStatusForExchange(string serviceNames)
        {
            string siteStatus = "<Site>";
            StringWriter stringWriter = new StringWriter();
            StringWriter EventsWriter = new StringWriter();
            try
            {
                LogManager.WriteLog("Try to Get Site Status.. ", LogManager.enumLogLevel.Info);
                GetServiceStatus(serviceNames, ServiceTypes.All).WriteXml(stringWriter);

                siteStatus += stringWriter.ToString();
                siteStatus += "\n<Status>\n<DateTime>" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt") + "</DateTime>\n</Status>\n";
                LogManager.WriteLog("Site Status got successfully", LogManager.enumLogLevel.Error);    

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                siteStatus = "<Site>";
            }
            
            return siteStatus + "</Site>";
        }

        public string GetDriveSpace()
        {
            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                string driveInfo = "<DiskSpace>";
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Fixed && drive.IsReady)
                    {
                        driveInfo += "<Drive><DriveName>" + drive.Name + "</DriveName>";
                        long freeSpace = drive.TotalFreeSpace / (1024 * 1024);
                        driveInfo += "<DriveSpace>" + freeSpace + "</DriveSpace></Drive>";
                    }
                }
                driveInfo += "</DiskSpace>";
                return driveInfo;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "<DiskSpace>Error</DiskSpace>";
            }
        }

        #endregion

        #region Private Methods

        private static string GetConnectionString()
        {
            try
            {
                //bool useHex = true;
                //RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\Cashmaster");
                //string connectionString = registryKey.GetValue("SQLConnect").ToString();
                //registryKey.Close();
                string connectionString = DatabaseHelper.GetExchangeConnectionString();
                //if (!connectionString.ToUpper().Contains("SERVER"))
                //{
                //    connectionString = BMC.Common.Security.CryptEncode.Decrypt(connectionString);
                //}
                return connectionString;
            }
            catch
            {
                LogManager.WriteLog("Unable to get the value for (SQLConnect).", LogManager.enumLogLevel.Error);
            }
            return string.Empty;
        }

        private string GetSiteInterrogationInfo()
        {
            string siteInterrogationInfo = "<SiteInterrogationInfo>";

            try
            {
                using (SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, ProcSiteInterrogationInfo))
                {
                    while (sqlDataReader.Read())
                    {
                        string Position = sqlDataReader["Bar_Pos_Name"].ToString();
                        string Asset = sqlDataReader["Stock_No"].ToString();
                        string GMUToServer = sqlDataReader["Datapak_PollingStatus"].ToString();
                        string GMUToMachine = sqlDataReader["GMU_Machine_Status"].ToString();
                        string MachineStatus = sqlDataReader["Machine_Status"].ToString();
                        string GMU_Version = sqlDataReader["GMU_Version"].ToString();
                        siteInterrogationInfo += "<GMU>";
                        siteInterrogationInfo += "<Position>" + Position + "</Position>";
                        siteInterrogationInfo += "<Asset>" + Asset + "</Asset>";
                        siteInterrogationInfo += "<GMUVersion>" + GMU_Version + "</GMUVersion>";
                        siteInterrogationInfo += "<GMUToServer>" + GMUToServer + "</GMUToServer>";
                        siteInterrogationInfo += "<GMUToMachine>" + GMUToMachine + "</GMUToMachine>";
                        siteInterrogationInfo += "<MachineStatus>" + MachineStatus + "</MachineStatus>";                        
                        siteInterrogationInfo += "</GMU>";
                    }
                }
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                siteInterrogationInfo = "<SiteInterrogationInfo>";
            }


            return siteInterrogationInfo += "</SiteInterrogationInfo>";
        }

        private string GetVLTStatusInformation()
        {
            DataTable dtVLTStatus;
            string strVLTStatus = string.Empty;

            try
            {
                SqlParameter[] objparams = new SqlParameter[5];

                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "AllPosition";
                param1.Direction = ParameterDirection.Input;
                param1.Value = 0;
                objparams[0] = param1;

                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "VLTAAMS";
                param2.Direction = ParameterDirection.Input;
                param2.Value = 0;
                objparams[1] = param2;

                SqlParameter param3 = new SqlParameter();
                param3.ParameterName = "VLTVerification";
                param3.Direction = ParameterDirection.Input;
                param3.Value = 0;
                objparams[2] = param3;

                SqlParameter param4 = new SqlParameter();
                param4.ParameterName = "GameAAMS";
                param4.Direction = ParameterDirection.Input;
                param4.Value = 0;
                objparams[3] = param4;

                SqlParameter param5 = new SqlParameter();
                param5.ParameterName = "GameVerification";
                param5.Direction = ParameterDirection.Input;
                param5.Value = 0;
                objparams[4] = param5;

                string strRegulatory = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, "SELECT Setting_Value FROM dbo.Setting WHERE Setting_Name = 'RegulatoryType'").ToString();

                if (strRegulatory == "" || strRegulatory == null)
                    return "";

                dtVLTStatus = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "rsp_GetPositionCurrentStatus", objparams).Tables[0];

                if (dtVLTStatus.Rows.Count > 0)
                {
                    strVLTStatus = "<VLTStatus>";

                    foreach (DataRow row in dtVLTStatus.Rows)
                    {

                        strVLTStatus += "<VLT>";
                        strVLTStatus += "<Position>" + row["Bar_Pos_Name"].ToString() + "</Position>";
                        strVLTStatus += "<BAD_AAMS_Status>" + row["BAD_AAMS_Status"].ToString() + "</BAD_AAMS_Status>";
                        strVLTStatus += "<BAD_Verification_Status>" + row["BAD_Verification_Status"].ToString() + "</BAD_Verification_Status>";
                        strVLTStatus += "<Game_Verification>" + row["Game_Verification"].ToString() + "</Game_Verification>";
                        strVLTStatus += "<Game_Install_AAMS_Status>" + row["Game_Install_AAMS_Status"].ToString() + "</Game_Install_AAMS_Status>";
                        strVLTStatus += "<Game_Enable_AAMS_Status>" + row["Game_Enable_AAMS_Status"].ToString() + "</Game_Enable_AAMS_Status>";
                        strVLTStatus += "<BAD_AAMS_EnableDisable>" + row["BAD_AAMS_EnableDisable"].ToString() + "</BAD_AAMS_EnableDisable>";
                        strVLTStatus += "</VLT>";
                    }

                    strVLTStatus += "</VLTStatus>";
                }

                return strVLTStatus;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return string.Empty;
            }
        }

        private string GetHourlyStatistics()
        {
            string hourlyStatistics = "<HourlyStatistics>";
            try
            {

                hourlyStatistics += "<Drop>" + GetHourlyStatisticsByType("DROP") + "</Drop>";
                hourlyStatistics += "<HandPay>" + GetHourlyStatisticsByType("HANDPAY") + "</HandPay>";
                hourlyStatistics += "<Jackpot>" + GetHourlyStatisticsByType("JACKPOT") + "</Jackpot>";
                hourlyStatistics += "<TicketsPrinted>" + GetHourlyStatisticsByType("TICKETS_PRINTED_QTY") + "</TicketsPrinted>";


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            hourlyStatistics += "</HourlyStatistics>";

            return hourlyStatistics;
        }

        private double GetHourlyStatisticsByType(string statisticsType)
        {
            double total = 0.0;
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@DataType", statisticsType);
            sqlParameters[1] = new SqlParameter("@Rows", 1);
            sqlParameters[2] = new SqlParameter("@StartHour", 6);
            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            sqlParameters[3] = new SqlParameter("@date", dateTime);

            try
            {
                using (SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, 
                    Proc24HourStatisticsByType3, sqlParameters))
                {
                    while (sqlDataReader.Read())
                    {
                        for (int i = 5; i < sqlDataReader.VisibleFieldCount; i++)
                            total += sqlDataReader.GetDouble(i);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return total;
        }

        private void PopulateServiceData(DataTable dataTableServices, string serviceName, ServiceController serviceController)
        {
            try
            {
                DataRow dataRow = dataTableServices.NewRow();

                dataRow["ServiceName"] = serviceName;
                try
                {
                    dataRow["Status"] = serviceController.Status.ToString();
                }
                catch
                {
                    dataRow["Status"] = ServiceErrorString;
                }

                dataTableServices.Rows.Add(dataRow);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        public bool EndService(string strServiceName)
        {
            bool bCheckStatus = false;
            try
            {
                ServiceController ObjServiceController = new ServiceController();
                ObjServiceController.ServiceName = strServiceName;
                if (ObjServiceController.Status == ServiceControllerStatus.Stopped)
                {
                    bCheckStatus = true;
                }
                else if (ObjServiceController.Status == ServiceControllerStatus.StartPending || ObjServiceController.Status == ServiceControllerStatus.PausePending || ObjServiceController.Status == ServiceControllerStatus.StopPending||ObjServiceController.Status == ServiceControllerStatus.Running || ObjServiceController.Status == ServiceControllerStatus.Paused)
                {
                    ObjServiceController.Stop();
                    Thread.Sleep(5000);
                    ObjServiceController.Refresh();
                    bCheckStatus = ObjServiceController.Status == ServiceControllerStatus.Stopped ? true : false;
                }

            }
            catch (InvalidOperationException ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return bCheckStatus;
        }

        public bool EndService(string strServiceName, TimeSpan span)
        {
            bool bCheckStatus = false;
            try
            {
                ServiceController ObjServiceController = new ServiceController();
                ObjServiceController.ServiceName = strServiceName;
                if (ObjServiceController.Status == ServiceControllerStatus.Stopped)
                {
                    bCheckStatus = true;
                }
                else
                {
                    switch(ObjServiceController.Status)
                    {
                        case ServiceControllerStatus.StartPending:
                            ObjServiceController.WaitForStatus(ServiceControllerStatus.Running, span);
                            break;
                        case ServiceControllerStatus.PausePending:
                            ObjServiceController.WaitForStatus(ServiceControllerStatus.Paused, span);
                            break;
                        case ServiceControllerStatus.StopPending:
                            ObjServiceController.WaitForStatus(ServiceControllerStatus.Stopped, span);
                            break;
                    }
                    ObjServiceController.Stop();
                    ObjServiceController.WaitForStatus(ServiceControllerStatus.Stopped, span);
                    ObjServiceController.Refresh();
                    bCheckStatus = ObjServiceController.Status == ServiceControllerStatus.Stopped ? true : false;
                }

            }
            catch (InvalidOperationException ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return bCheckStatus;
        }


        public void EnableService(string serviceName)
        {
            string name = string.Empty;

            try
            {
                using (ServiceController serviceController = new ServiceController())
                {
                    serviceController.ServiceName = serviceName;
                    try
                    {
                        name = string.Format("{0}\\{1}", "SYSTEM\\CurrentControlSet\\Services", serviceName);

                        RegistryKey key = Registry.LocalMachine.OpenSubKey(name, true);
                        key.SetValue("Start", 2);

                        //name = string.Format("{0}\\{1}", "SYSTEM\\CurrentControlSet\\Services", serviceName);
                        
                        //RegistryKey key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(name, true);
                        //key.SetValue("Start", 2);
                        //BMC.CoreLib.Services.ServiceHelper.ChangeStartMode(serviceController, ServiceStartMode.Automatic);

                        LogManager.WriteLog(string.Format("{0} - {1}", serviceName, "service enabled successfully"), LogManager.enumLogLevel.Info);

                        if (serviceController.Status == ServiceControllerStatus.StartPending)
                        {
                            serviceController.WaitForStatus(ServiceControllerStatus.Running);
                        }
                        else if (serviceController.Status == ServiceControllerStatus.PausePending)
                        {
                            serviceController.WaitForStatus(ServiceControllerStatus.Paused);
                        }
                        else if (serviceController.Status == ServiceControllerStatus.StopPending)
                        {
                            serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                        }

                        serviceController.Start();

                        serviceController.WaitForStatus(ServiceControllerStatus.Running);

                        LogManager.WriteLog(string.Format("{0} - {1}", serviceName, "service started successfully"), LogManager.enumLogLevel.Info);
                    }
                    catch (InvalidOperationException ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void DisableService(string serviceName)
        {
            string name = string.Empty;

            try
            {
                using (ServiceController serviceController = new ServiceController())
                {
                    serviceController.ServiceName = serviceName;
                    try
                    {
                        if (serviceController.Status == ServiceControllerStatus.StartPending)
                        {
                            serviceController.WaitForStatus(ServiceControllerStatus.Running);
                        }
                        else if (serviceController.Status == ServiceControllerStatus.PausePending)
                        {
                            serviceController.WaitForStatus(ServiceControllerStatus.Paused);
                        }
                        else if (serviceController.Status == ServiceControllerStatus.StopPending)
                        {
                            serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                        }

                        serviceController.Stop();
                        serviceController.WaitForStatus(ServiceControllerStatus.Stopped);

                        LogManager.WriteLog(string.Format("{0} - {1}", serviceName, "service stopped successfully"), LogManager.enumLogLevel.Info);

                        name = string.Format("{0}\\{1}", "SYSTEM\\CurrentControlSet\\Services", serviceName);

                        RegistryKey key = Registry.LocalMachine.OpenSubKey(name, true);
                        key.SetValue("Start", 4);

                        //name = string.Format("{0}\\{1}", "SYSTEM\\CurrentControlSet\\Services", serviceName);

                        //RegistryKey key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(name, true);
                        //key.SetValue("Start", 4);
                        //BMC.CoreLib.Services.ServiceHelper.ChangeStartMode(serviceController, ServiceStartMode.Disabled);


                        LogManager.WriteLog(string.Format("{0} - {1}", serviceName, "service disabled successfully"), LogManager.enumLogLevel.Info);
                    }
                    catch (InvalidOperationException ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private string GetHourlyReadHour()
        {
            try 
	        {
                //string value = "";
                //if (ReadFromRegistry("Cashmaster", "HourlyReadHour", ref value))
                //    return value;
                return BMCRegistryHelper.GetRegKeyValue("Cashmaster", "HourlyReadHour");
            }
	        catch (Exception ex)
	        {
		        ExceptionManager.Publish(ex);
	        }
            return null;
        }

        private string GetReadDate()
        {
            try 
	        {
                //string value = "";
                return BMCRegistryHelper.GetRegKeyValue("Cashmaster", "LastAutoRead");
                //if (ReadFromRegistry("Cashmaster", "LastAutoRead", ref value))
                //    return value;
            }
	        catch (Exception ex)
	        {
		        ExceptionManager.Publish(ex);
	        }
            return null;
          
        }

        public bool ReadFromRegistry(string registryPath, string key, ref string value)
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryPath);
                value = registryKey.GetValue(key).ToString();
                registryKey.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        #endregion
    }
}
