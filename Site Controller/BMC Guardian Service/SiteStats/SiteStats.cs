using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using BMC.Common;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Monitoring;
using System.Xml;
using BMC.Business.CashDeskOperator.WebServices;
using System.Data.SqlClient;
using SiteStatusService.DBBuilder;
using System.Configuration;
using BMC.DataAccess;
using System.Threading;

namespace SiteStatusService
{
    public partial class SiteStats : ServiceBase
    {
        public SiteStats()
        {
            InitializeComponent();
        }
        private static int SiteStatusCheckInterval = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SiteStatusCheckInterval")) * 60 * 1000;
        private static bool isProcessing = false;
        private static int SiteEnabledStatus = 1;
        private const string ProcGetSiteEnabledStatus = "rsp_GetSiteEnabledStatus";
        private const string ProcGetSiteStatus = "rsp_GetSiteStatus";
        private const string BMCGuardianService = "BMCGUARDIANSERVICE";
        private static bool isServiceRunning = false;
        System.Timers.Timer testTimer = new System.Timers.Timer();
        System.Timers.Timer ServiceRestartTimer = new System.Timers.Timer();
        System.Timers.Timer serviceStatusTimer = new System.Timers.Timer();
        System.Timers.Timer CheckSiteStatusTimer = new System.Timers.Timer();

        private AutoResetEvent _onstopexc = new AutoResetEvent(true);

        protected override void OnStart(string[] args)
        {
            LogManager.WriteLog("Launching Thread to check database connectivity...", LogManager.enumLogLevel.Info);

            Thread dbConnectivityThread = new Thread(new ThreadStart(CheckDBConnectivity));
            dbConnectivityThread.IsBackground = true;
            dbConnectivityThread.Start();

            LogManager.WriteLog("Thread to check database connectivity launched successfully.", LogManager.enumLogLevel.Info);
        }

        private void CheckDBConnectivity()
        {
            LogManager.WriteLog("Starting Guardian Service...", LogManager.enumLogLevel.Info);

            SqlConnection sqlConnection = null;
            bool isDBConnectionUp = false;

            while (!isDBConnectionUp)
            {
                try
                {
                    LogManager.WriteLog("Checking DB Connectivity...", LogManager.enumLogLevel.Info);

                    sqlConnection = new SqlConnection(DBCalls.GetConnRegSettings());
                    sqlConnection.Open();
                    isDBConnectionUp = true;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);

                    LogManager.WriteLog("DB Connection is down.", LogManager.enumLogLevel.Error);

                    LogManager.WriteLog("Putting the Thread to Sleep for 10 Seconds...", LogManager.enumLogLevel.Info);

                    Thread.Sleep(10 * 1000);
                }
                finally
                {
                    if (sqlConnection != null)
                        sqlConnection = null;
                }
            }

            LogManager.WriteLog("DB Connection is Up.", LogManager.enumLogLevel.Info);

            StartService();
        }

        public void StartService()
        {
            //SiteStats test = new SiteStats();
            // TODO: Add code here to start your service.
            testTimer.Enabled = true;
            ServiceRestartTimer.Enabled = true;
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            LogManager.WriteLog("Guardian service started", LogManager.enumLogLevel.Info);

            ServiceRestartTimer.Interval = Convert.ToInt32(ConfigManager.Read("ServiceCheckTimeInterval")) * 1000 * 60;    // Execute timer every
            testTimer.Interval = Convert.ToInt32(ConfigManager.Read("TimeInterval")) * 1000;    // Execute timer every TimeInterval seconds
            testTimer.Elapsed += new System.Timers.ElapsedEventHandler(testTimer_Elapsed);
            ServiceRestartTimer.Elapsed += new System.Timers.ElapsedEventHandler(ServiceTimer_Elapsed);

            try
            {
                serviceStatusTimer.Enabled = true;
                serviceStatusTimer.Interval = Convert.ToInt32(ConfigManager.Read("ServiceStatusInterval")) * 1000 * 60;
                serviceStatusTimer.Elapsed += new System.Timers.ElapsedEventHandler(serviceStatusTimer_Elapsed);
                serviceStatusTimer.Start();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            try
            {
                // For Enable/Disable Site
                string regulatoryType = string.Empty;
                bool isRegulatoryEnabled = false;

                regulatoryType = GetSettingFromDB("RegulatoryType").ToUpper();
                isRegulatoryEnabled = Convert.ToBoolean(GetSettingFromDB("IsRegulatoryEnabled"));

                LogManager.WriteLog(string.Format("{0} - {1}, {2} - {3}", "Regulatory Enabled", isRegulatoryEnabled.ToString().ToUpper(),
                    "Regulatory Type", regulatoryType.ToUpper()), LogManager.enumLogLevel.Info);

                if (isRegulatoryEnabled == true && regulatoryType == "AAMS")
                {
                    LogManager.WriteLog("Inside StartMonitoringSiteStatus", LogManager.enumLogLevel.Info);
                    StartMonitoringSiteStatus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }


            isServiceRunning = true;
            Thread Th_Exch = new Thread(GetServiceStatusForExchange);
            Th_Exch.Start();
            LogManager.WriteLog("Site Service Status For Exchange Started Successfully.", LogManager.enumLogLevel.Info);
            // Sit and wait so we can see some output
            //Console.ReadLine();
            UpdateStatusInEnterprise();
            RestartServices();
        }

        void serviceStatusTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string strServiceName = "BMCNetworkService";

            try
            {
                DataSet dsData = DBBuilder.DBCalls.GetServiceStatusDetails();
                bool bRequired = false;

                LogManager.WriteLog("Service Status Details - Timer Triggered.", LogManager.enumLogLevel.Error);

                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        DateTime dtNextRun;

                        try
                        {
                            dtNextRun = Convert.ToDateTime(dsData.Tables[0].Rows[0]["SS_NextRunTime"]);
                        }
                        catch (Exception ex)
                        {
                            dtNextRun = DateTime.Now;
                            ExceptionManager.Publish(ex);
                        }

                        int iStatus = DateTime.Compare(DateTime.Now, dtNextRun);

                        LogManager.WriteLog("Service Status Details - Current Date " + DateTime.Now.ToString() + " NextRunDate " +
                            dtNextRun.ToString() + " Status " + iStatus.ToString(), LogManager.enumLogLevel.Error);

                        if (iStatus > 0)
                        {
                            bRequired = true;
                        }
                    }
                    else
                    {
                        bRequired = true;
                    }
                }
                else
                {
                    bRequired = true;
                }

                if (bRequired)
                {
                    BMCMonitoring objMonitoring = new BMCMonitoring();

                    LogManager.WriteLog("Service Status Details - Starting Process.", LogManager.enumLogLevel.Error);

                    if (objMonitoring.RestartService(strServiceName))
                    {
                        DBBuilder.DBCalls.UpdateServiceStatusDetails();
                        LogManager.WriteLog(strServiceName + " Restarted", LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog("Error while restarting " + strServiceName, LogManager.enumLogLevel.Info);
                    }
                }
            }
            catch (Exception exserviceStatusTimer_Elapsed)
            {
                LogManager.WriteLog("Error while restarting " + strServiceName, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(exserviceStatusTimer_Elapsed);
            }
        }

        private void testTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LogManager.WriteLog("Timer elapsed for Guardian -- starting to update status to enterprise.", LogManager.enumLogLevel.Info);
            UpdateStatusInEnterprise();
        }

        private void ServiceTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (ConfigManager.Read("AutomaticServiceRestart").ToString().ToUpper() == "YES")
                {
                    LogManager.WriteLog("Timer elapsed for Services Restart ", LogManager.enumLogLevel.Info);

                    if (SiteEnabledStatus == 1)
                    {
                        RestartServices();
                    }
                    else
                    {
                        LogManager.WriteLog("Site is disabled, hence not starting any stopped Services.", LogManager.enumLogLevel.Info);
                    }
                }
            }
            catch
            { }
        }

        protected override void OnStop()
        {
            testTimer.Enabled = false;
            ServiceRestartTimer.Enabled = false;
            isServiceRunning = false;

            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        private void UpdateStatusInEnterprise()
        {
            try
            {
                int SystemEventsToGet = Convert.ToInt16(ConfigManager.Read("NoofSystemLogsToGet"));
                string SiteCode = DBBuilder.DBCalls.GetSiteName();
                Proxy objService = new Proxy(SiteCode);
                BMCMonitoring objMonitoring = new BMCMonitoring();
                string strServiceNames = DBBuilder.DBCalls.GetServiceNames();
                string strSiteStatus = string.Empty;
                if (!string.IsNullOrEmpty(strServiceNames))
                {
                    strSiteStatus = objMonitoring.GetSiteStatus(strServiceNames, SystemEventsToGet);
                }
                if (!string.IsNullOrEmpty(strSiteStatus))
                {
                    //UpdateSiteServiceDetails(SiteCode, strSiteStatus);
                    //LogManager.WriteLog("Site status updated in exchange.", LogManager.enumLogLevel.Info);

                    objService.UpdateSiteStatsInEnterprise(SiteCode, strSiteStatus);
                    LogManager.WriteLog("Site status updated in enterrprise.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void RestartServices()
        {
            string strServiceName = string.Empty;
            try
            {
                BMCMonitoring objMonitoring = new BMCMonitoring();
                string strServiceNames = DBBuilder.DBCalls.GetServiceNames();
                LogManager.WriteLog("RestartServices: Service to restart " + strServiceNames, LogManager.enumLogLevel.Info);
                string strSiteStatus = string.Empty;
                int SystemEventsToGet = Convert.ToInt16(ConfigManager.Read("NoofSystemLogsToGet"));
                if (!string.IsNullOrEmpty(strServiceNames))
                {
                    strSiteStatus = objMonitoring.GetSiteStatus(strServiceNames, SystemEventsToGet);
                    DataTable dtServiceStatus = GetServiceStatus(strSiteStatus);
                    foreach (DataRow dr in dtServiceStatus.Rows)
                    {
                        if (dr["ServiceStatus"].ToString().ToUpper() != "RUNNING")
                        {
                            strServiceName = dr["ServiceName"].ToString();

                            if (strServiceName.ToUpper() != "BMCGUARDIANSERVICE")
                            {
                                try
                                {
                                    if (objMonitoring.RestartService(strServiceName))
                                    {
                                        LogManager.WriteLog(strServiceName + " Restarted", LogManager.enumLogLevel.Info);
                                    }
                                    else
                                    { LogManager.WriteLog("Error while restarting " + strServiceName, LogManager.enumLogLevel.Info); }
                                }
                                catch (Exception ex1)
                                {
                                    LogManager.WriteLog("Error while restarting " + strServiceName, LogManager.enumLogLevel.Error);
                                    ExceptionManager.Publish(ex1);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private DataTable GetServiceStatus(string strXML)
        {
            try
            {
                XmlDocument xDom = new XmlDocument();
                xDom.LoadXml(strXML);
                DataTable dtSiteStatus = new DataTable();
                dtSiteStatus.Columns.Add("ServiceName");
                dtSiteStatus.Columns.Add("ServiceStatus");
                DataRow drRow;
                if (xDom.DocumentElement.HasChildNodes)
                {
                    XmlNode myNode = xDom.SelectSingleNode("Site/DocumentElement");
                    if (myNode != null && myNode.HasChildNodes)
                    {
                        foreach (XmlNode oNode in myNode.ChildNodes)
                        {
                            drRow = dtSiteStatus.NewRow();
                            drRow[0] = oNode["ServiceName"].InnerText.ToString();
                            drRow[1] = oNode["Status"].InnerText.ToString();
                            dtSiteStatus.Rows.Add(drRow);
                        }
                    }
                }
                return dtSiteStatus;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// checks whether the local webservice is running or not
        /// </summary>
        //private void CheckLocalWebService()
        //{
        //    string strExchangeURL = string.Empty;
        //    try
        //    {
        //        ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);

        //        if (ConfigManager.Read("AutomaticIISRestart").ToString().Trim().ToUpper()=="YES")
        //        {
        //            BGSWSService objWS = new BGSWSService();
        //            objWS.Url = ConfigManager.Read("ExchangeWebServiceURL").ToString().Trim();
        //            try
        //            {
        //                if (objWS.HelloWebService(675) != 675)
        //                {
        //                    RestartIIS();
        //                } 
        //            }
        //            catch 
        //            {
        //                RestartIIS();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        /// <summary>
        /// Restarts the IIS if it is down
        /// </summary>
        private void RestartIIS()
        {
            try
            {
                // BMCMonitoring objMonitoring = new BMCMonitoring();

                ProcessStartInfo psi = new ProcessStartInfo("iisreset");

                Process process = Process.Start(psi);
                LogManager.WriteLog("IIS restarted ", LogManager.enumLogLevel.Info);
                //if (objMonitoring.RestartService("IISAdmin"))
                //{
                //    LogManager.WriteLog("IIS restarted ", LogManager.enumLogLevel.Info);
                //}
                //else
                //{
                //    LogManager.WriteLog("Unable to restart IIS at the moment.", LogManager.enumLogLevel.Info);
                //}

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Unable to restart IIS at the moment.", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Get the settings for Client
        /// </summary>
        /// <param name="sqlparams"></param>
        /// <param name="strConnect"></param>
        /// <returns >string</returns>
        private string GetSettingFromDB(string strSetting)
        {
            string strReturnValue = string.Empty;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB(strSetting);
                SqlHelper.ExecuteNonQuery(DBCalls.GetConnRegSettings(), System.Data.CommandType.StoredProcedure, "rsp_getSetting", sqlparams);
                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    strReturnValue = Convert.ToString(sqlparams[3].Value);
                }
                else
                {
                    strReturnValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return strReturnValue;
        }

        /// <summary>
        /// To set parameters for Get Setting SP
        /// </summary>
        /// <param name="strSettingName">string</param>
        /// <returns type=SqlParameter[] >sp_parames</returns>
        private SqlParameter[] GetSettingParameterDB(string SettingName)
        {
            SqlParameter[] sp_parames = null;
            try
            {

                if (SettingName != null)
                {
                    sp_parames = new SqlParameter[5];

                    sp_parames[0] = new SqlParameter("@Setting_ID", 0);
                    sp_parames[1] = new SqlParameter("@Setting_Name", SettingName.Trim());
                    sp_parames[2] = new SqlParameter("@Setting_Default", string.Empty);

                    sp_parames[3] = new SqlParameter();
                    sp_parames[3].ParameterName = "Setting_Value";
                    sp_parames[3].Direction = ParameterDirection.Output;
                    sp_parames[3].Value = string.Empty;
                    sp_parames[3].SqlDbType = SqlDbType.VarChar;
                    sp_parames[3].Size = 100;

                    SqlParameter ReturnValue = new SqlParameter();
                    ReturnValue.ParameterName = "RETURN_VALUE";
                    ReturnValue.Direction = ParameterDirection.ReturnValue;
                    sp_parames[4] = ReturnValue;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sp_parames;
        }

        private void StartMonitoringSiteStatus()
        {
            try
            {
                LogManager.WriteLog("Starting Monitoring Site Status...", LogManager.enumLogLevel.Info);

                //Get the Exchange connection string
                string strConnectionString = DBCalls.GetConnRegSettings();

                // Get Current Status of Site
                GetSiteEnabledStatus();

                //Remove any existing dependency connection, then create a new one.
                SqlDependency.Stop(strConnectionString);
                SqlDependency.Start(strConnectionString);

                //Register SQL Dependency to trigger change Notifications to Site table
                RegisterSQLDependency();

                //Start Timer to monitor change Notifications to Site table, as backup if SQL Dependency fails
                StartCheckSiteStatusTimer();

                LogManager.WriteLog("Site Status Monitoring started successfully", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void GetSiteEnabledStatus()
        {
            try
            {
                LogManager.WriteLog("Inside GetCurrentSiteStatus", LogManager.enumLogLevel.Info);

                SqlConnection sqlConnection = new SqlConnection(DBCalls.GetConnRegSettings());

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = ProcGetSiteStatus;

                //Execute the command.
                sqlConnection.Open();
                SiteEnabledStatus = Convert.ToInt32(sqlCommand.ExecuteScalar());
                sqlConnection.Close();

                LogManager.WriteLog(string.Format("{0} - {1}", "Current Status of Site", SiteEnabledStatus.ToString()), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void RegisterSQLDependency()
        {
            try
            {
                LogManager.WriteLog("Inside RegisterSQLDependency", LogManager.enumLogLevel.Info);

                SqlConnection sqlConnection = new SqlConnection(DBCalls.GetConnRegSettings());

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = ProcGetSiteEnabledStatus;

                sqlCommand.Notification = null;

                //Set the SQLDepencdency to the SQLCommand object.
                SqlDependency sqlDependency = new SqlDependency(sqlCommand);
                sqlDependency.OnChange += new OnChangeEventHandler(sqlDependency_OnChange);

                //Execute the command.
                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                LogManager.WriteLog("SQLDependency registered Successfully", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {
                LogManager.WriteLog("SQLDependency Trigerred.", LogManager.enumLogLevel.Info);

                SqlDependency objSqlDependency = (SqlDependency)sender;

                // Remove the handler, since it is only good for a single notification.
                objSqlDependency.OnChange -= sqlDependency_OnChange;

                if (!isProcessing)
                {
                    isProcessing = true;
                    LogManager.WriteLog("Processing Started.", LogManager.enumLogLevel.Info);
                    CheckSiteStatus();
                    LogManager.WriteLog("Processing Completed.", LogManager.enumLogLevel.Info);
                }

                //Register the SQLDependency again to trigger change Notifications to Site table
                RegisterSQLDependency();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                isProcessing = false;
            }
        }

        private void StartCheckSiteStatusTimer()
        {
            try
            {
                LogManager.WriteLog("Inside StartCheckSiteStatusTimer", LogManager.enumLogLevel.Info);

                CheckSiteStatusTimer = new System.Timers.Timer();

                CheckSiteStatusTimer.Interval = Convert.ToDouble(SiteStatusCheckInterval);
                CheckSiteStatusTimer.Elapsed += new System.Timers.ElapsedEventHandler(CheckSiteStatusTimer_Elapsed);
                CheckSiteStatusTimer.Enabled = true;

                CheckSiteStatusTimer.Start();

                LogManager.WriteLog("CheckSiteStatusTimer started successfully", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void CheckSiteStatusTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside CheckSiteStatusTimer_Elapsed event.", LogManager.enumLogLevel.Info);

                if (!isProcessing)
                {
                    isProcessing = true;
                    LogManager.WriteLog("Processing Started.", LogManager.enumLogLevel.Info);
                    CheckSiteStatus();
                    LogManager.WriteLog("Processing Completed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                isProcessing = false;
            }
        }

        private void CheckSiteStatus()
        {
            try
            {
                LogManager.WriteLog("Inside CheckSiteStatus", LogManager.enumLogLevel.Info);

                SqlConnection sqlConnection = new SqlConnection(DBCalls.GetConnRegSettings());

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = ProcGetSiteStatus;

                //Execute the command.
                sqlConnection.Open();
                int siteStatus = Convert.ToInt32(sqlCommand.ExecuteScalar());
                sqlConnection.Close();

                if (SiteEnabledStatus != siteStatus)
                {
                    BMCMonitoring objMonitoring = new BMCMonitoring();

                    string strServiceNames = DBBuilder.DBCalls.GetServiceNames();
                    List<string> serviceNames = new List<string>(strServiceNames.Split(','));

                    foreach (string service in serviceNames)
                    {
                        if (service.Trim().ToUpper() == BMCGuardianService) { continue; }

                        if (siteStatus == 1)
                        {
                            objMonitoring.EnableService(service.Trim());
                        }
                        else
                        {
                            objMonitoring.DisableService(service.Trim());
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("Site Status not changed, hence skipping the Site Enable/Disable process.", LogManager.enumLogLevel.Info);
                }

                SiteEnabledStatus = siteStatus;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private void UpdateSiteServiceDetails(string sStatus)
        {
            try
            {
                SqlParameter[] sp_parames = null;

                sp_parames = new SqlParameter[1];

                sp_parames[0] = new SqlParameter("@Status", sStatus);

                SqlHelper.ExecuteScalar(DBCalls.GetConnRegSettings(), "usp_UpdateServiceStatus", sp_parames);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void GetServiceStatusForExchange()
        {
            string strServiceNames = string.Empty;
            int interval = Convert.ToInt32("0" + ConfigManager.Read("Exchange_ServicesStatusInterval"));
            interval = interval == 0 ? 30 : interval;
            while (isServiceRunning)
            {
                try
                {
                    BMCMonitoring objMonitoring = new BMCMonitoring();

                    if (strServiceNames == string.Empty)
                        strServiceNames = DBBuilder.DBCalls.GetServiceNames();
                    string strSiteStatus = string.Empty;
                    if (!string.IsNullOrEmpty(strServiceNames))
                    {
                        strSiteStatus = objMonitoring.GetSiteStatusForExchange(strServiceNames);
                    }
                    if (!string.IsNullOrEmpty(strSiteStatus))
                    {
                        UpdateSiteServiceDetails(strSiteStatus);
                        LogManager.WriteLog("Site status updated in exchange.", LogManager.enumLogLevel.Info);
                    }
                    _onstopexc.WaitOne(interval * 1000);//waits current thread in finite time(i.e 30 sec default)
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    strServiceNames = string.Empty;
                }
            }
        }



    }
}