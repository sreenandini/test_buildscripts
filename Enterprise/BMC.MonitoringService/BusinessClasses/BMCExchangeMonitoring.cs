namespace BMC.MonitoringService
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BMC.Common.LogManagement;
    using BMC.Common.ExceptionManagement;
    using System.Threading;
    using BMC.BusinessClasses.Proxy;
    using System.Data.SqlClient;
    using System.Data;   
    
    #endregion Namespaces

    #region public Class

    public class BMCExchangeMonitoring : IDisposable
    {
        #region Private Variables

        private System.Timers.Timer newSiteMonitorTimer                 =   null;
        private static bool isProcessing                                =   false;
        private Dictionary<string, SiteDetails> siteDetailsCollection   =   new Dictionary<string, SiteDetails>();
        private DataHelper dataHelper                                   =   null;        
        private Thread ExchangeMonitorThread                            =   null;
        private string strConnectionString = string.Empty;

        private object _lockObject = new object();
        private bool _isListening = true;
        private WaitHandle[] _waitHandles = new WaitHandle[0];
        public ManualResetEvent mre_ShutDown = new ManualResetEvent(false);
        #endregion Private Variables

        #region Public Methods

        public void StartExchangeMonitoring()
        {
            try
            {
                LogManager.WriteLog("Starting Exchange Monitoring...", LogManager.enumLogLevel.Info);

                //Get the Enterprise connection string
                strConnectionString = Common.Utilities.DatabaseHelper.GetConnectionString();

                //Remove any existing dependency connection, then create a new one.
                try
                {
                    SqlDependency.Stop(strConnectionString);
                    SqlDependency.Start(strConnectionString);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }

                // Check for Sites configured in the Enterprise
                CheckNewSite();

                //Register SQL Dependency to trigger change Notifications to Site table
                RegisterSQLDependency();

                //Start Timer to monitor change Notifications to Site table, as backup if SQL Dependency fails
                StartNewSiteMonitorTimer();

                //lock (this)
                {
                    LogManager.WriteLog("Exchange Monitoring started successfully", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                //lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        public void StopExchangeMonitoring()
        {
            try
            {
                //lock (this)
                {
                    LogManager.WriteLog("Stopping Exchange Monitoring...", LogManager.enumLogLevel.Info);
                }

                SqlDependency.Stop(strConnectionString);
                _isListening = false;
                newSiteMonitorTimer.Stop();

                //lock (this)
                {
                    LogManager.WriteLog("Exchange Monitoring stopped successfully", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                //lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void RegisterSQLDependency()
        {
            try
            {
                //lock (this)
                {
                    LogManager.WriteLog("Inside RegisterSQLDependency", LogManager.enumLogLevel.Info);
                }

                SqlConnection sqlConnection =   new SqlConnection(strConnectionString);

                SqlCommand sqlCommand       =   new SqlCommand();
                sqlCommand.Connection       =   sqlConnection;
                sqlCommand.CommandType      =   CommandType.StoredProcedure;
                sqlCommand.CommandText      =   Constants.CONST_RSP_GETSITEWEBURL;
                
                sqlCommand.Notification     =   null;

                //Set the SQLDepencdency to the SQLCommand object.
                SqlDependency sqlDependency =   new SqlDependency(sqlCommand);
                sqlDependency.OnChange      +=  new OnChangeEventHandler(sqlDependency_OnChange);

                //Execute the command.
                sqlConnection.Open();
                sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                //lock (this)
                {
                    LogManager.WriteLog("SQLDependency registered Successfully", LogManager.enumLogLevel.Info);
                }
            }   
            catch (Exception ex)
            {
                //lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        private void StartNewSiteMonitorTimer()
        {
            try
            {
                //lock (this)
                {
                    LogManager.WriteLog("Inside StartNewSiteMonitorTimer", LogManager.enumLogLevel.Info);
                }

                newSiteMonitorTimer             =   new System.Timers.Timer();

                newSiteMonitorTimer.Interval    =   Convert.ToDouble(StaticMembers.NewSiteMonitorInterval);
                newSiteMonitorTimer.Elapsed     +=  new System.Timers.ElapsedEventHandler(newSiteMonitorTimer_Elapsed);
                newSiteMonitorTimer.Enabled     =   true;                

                newSiteMonitorTimer.Start();
                
                //lock (this)
                {
                    LogManager.WriteLog("NewSiteMonitorTimer started successfully", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                //lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }       

        private void CheckNewSite()
        {
            try
            {
                //lock (this)
                {
                    LogManager.WriteLog("Inside CheckNewSite method.", LogManager.enumLogLevel.Info);
                }

                dataHelper = new DataHelper();
                
                Dictionary<string, SiteDetails> siteDetails = dataHelper.GetAllSiteDetails().Where(x => x.WebURL != string.Empty).ToDictionary(x => x.Site_Code);

                foreach (KeyValuePair<string, SiteDetails> eachSite in siteDetails)
                {
                    if (siteDetailsCollection.ContainsKey(eachSite.Key))
                    {
                        lock (_lockObject)
                        {
                            siteDetailsCollection[eachSite.Key] = eachSite.Value;
                        }
                    }
                    else
                    {
                        //lock (this)
                        {
                            LogManager.WriteLog(string.Format("New Site - [{0}] Configured in Enterprise. Addding Site to Monitoring Collection", eachSite.Key),
                                LogManager.enumLogLevel.Info);
                        }

                        lock (_lockObject)
                        {
                            siteDetailsCollection.Add(eachSite.Key, eachSite.Value);
                        }

                        //lock (this)
                        {
                            LogManager.WriteLog(string.Format("Spawning ExchangeMonitorThread for Site - [{0}]", eachSite.Key),
                                LogManager.enumLogLevel.Info);
                        }

                        ExchangeMonitorThread = new Thread(new ParameterizedThreadStart(MonitorExchangeConnectivity)) { Name = eachSite.Key };
                        ExchangeMonitorThread.Start(eachSite.Key);
                    }
                }
            }
            catch (Exception ex)
            {
                //lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        private void MonitorExchangeConnectivity(object siteData)
        {
            Proxy _proxy            =   null;
            SiteDetails siteDetail  =   new SiteDetails();
            int retryAttempt        =   0;
            bool siteCommsUp        =   true;
            ManualResetEvent waitHandle = new ManualResetEvent(false);

            try
            {
                //lock (this)
                {
                    LogManager.WriteLog(string.Format("Thread spawned successfully for Site - [{0}]", siteData.ToString()),
                            LogManager.enumLogLevel.Info);
                }

                try
                {
                    int length = _waitHandles.Length;
                    Array.Resize<WaitHandle>(ref _waitHandles, length + 1);
                    _waitHandles[length] = waitHandle;

                    LogManager.WriteLog(string.Format("Wait handle was successfully added for thread [{0:D}]", Thread.CurrentThread.ManagedThreadId),
                        LogManager.enumLogLevel.Info);
                }
                catch
                {
                    LogManager.WriteLog("Unable to add the wait handles",
                        LogManager.enumLogLevel.Info);
                }

                while (!mre_ShutDown.WaitOne(StaticMembers.ExchangeMonitorInterval))
                {
                    try
                    {
                        lock (_lockObject)
                        {
                            siteDetailsCollection.TryGetValue(siteData.ToString(), out siteDetail);
                        }

                        LogManager.WriteLog(string.Format("{0} - {1}", "Thread Invoked for Site", siteDetail.Site_Code), LogManager.enumLogLevel.Info);

                        _proxy = new Proxy(siteDetail.Site_Code);
                        _proxy.Timeout = StaticMembers.ExchangeProxyTimeOut;
                        _proxy.WebURL = siteDetail.WebURL;

                        int result = _proxy.HelloWebService(100);

                        retryAttempt = 0;

                        if (!siteCommsUp)
                        {
                            InsertSiteCommsEvents(siteDetail.Site_ID, StaticMembers.SiteCommsFaultSource, StaticMembers.SiteCommsResumedFaultType);
                            //lock (this)
                            {
                                LogManager.WriteLog(string.Format("Site Comms Up for Site - [{0}]", siteDetail.Site_Code),
                                    LogManager.enumLogLevel.Info);
                            }
                            siteCommsUp = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog(string.Format("{0} - [{1}]. {2} {3} - [{4}]", "Site", siteDetail.Site_Code, "Exchange Connectivity is Down.", "Exception Message", ex.Message), LogManager.enumLogLevel.Error);
                        retryAttempt++;
                        if (retryAttempt >= StaticMembers.ExchangeProxyMaxRetries)
                        {
                            //lock (this)
                            {
                                LogManager.WriteLog(string.Format("Site Comms Down for Site - [{0}]", siteDetail.Site_Code),
                                    LogManager.enumLogLevel.Error);
                            }
                            InsertSiteCommsEvents(siteDetail.Site_ID, StaticMembers.SiteCommsFaultSource, StaticMembers.SiteCommsFailureFaultType);
                            retryAttempt = 0;
                            siteCommsUp = false;
                        }
                    }
                    finally
                    {
                        if (_proxy != null)
                        {
                            _proxy = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
            finally
            {
                try
                {
                    waitHandle.Set();
                    LogManager.WriteLog(string.Format("Thread [{0:D}] has given signal to close.", Thread.CurrentThread.ManagedThreadId),
                                        LogManager.enumLogLevel.Error);
                }
                catch { }
            }
        }

        private void InsertSiteCommsEvents(int siteID, int faultSource, int FaultType)
        {
            try
            {
                //lock (this)
                {
                    LogManager.WriteLog("Inside InsertSiteCommsEvents method", LogManager.enumLogLevel.Info);
                }

                dataHelper = new DataHelper();
                bool result = dataHelper.InsertEnterpriseEvents(siteID, faultSource, FaultType);

                if (result == true)
                {
                    //lock (this)
                    {
                        LogManager.WriteLog("Site Comms Event inserted successfully into the database", LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    //lock (this)
                    {
                        LogManager.WriteLog("Site Comms Event insert into the database failed", LogManager.enumLogLevel.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                //lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        #endregion Private Methods

        #region Events

        void sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            try
            {
                //lock (this)
                {
                    LogManager.WriteLog("SQLDependency Trigerred.", LogManager.enumLogLevel.Info);
                }

                SqlDependency objSqlDependency = (SqlDependency)sender;

                // Remove the handler, since it is only good for a single notification.
                objSqlDependency.OnChange -= sqlDependency_OnChange;

                if (!isProcessing)
                {
                    isProcessing = true;
                    //lock (this)
                    {
                        LogManager.WriteLog("Processing Started.", LogManager.enumLogLevel.Info);
                    }
                    CheckNewSite();
                    //lock (this)
                    {
                        LogManager.WriteLog("Processing Completed.", LogManager.enumLogLevel.Info);
                    }                    
                }

                //Register the SQLDependency again to trigger change Notifications to Site table
                RegisterSQLDependency();
            }
            catch (Exception ex)
            {
                //lock (this)
                {
                    ExceptionManager.Publish(ex);
                }                
            }
            finally
            {
                isProcessing = false;
            }
        }

        void newSiteMonitorTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //lock (this)
            {
                LogManager.WriteLog("New Site Monitor Timer triggered", LogManager.enumLogLevel.Info);
            }

            try
            {                
                if (!isProcessing)
                {
                    isProcessing = true;
                    //lock (this)
                    {
                        LogManager.WriteLog("Processing Started.", LogManager.enumLogLevel.Info);
                    }
                    CheckNewSite();
                    //lock (this)
                    {
                        LogManager.WriteLog("Processing Completed.", LogManager.enumLogLevel.Info);
                    }                    
                }
            }
            catch (Exception ex)
            {
                //lock (this)
                {
                    ExceptionManager.Publish(ex);
                }                
            }
            finally
            {
                isProcessing = false;
            }
        }

        #endregion Events

        #region IDisposable Members

        private bool _isDisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    try
                    {
                        _isListening = false;
                        if (_waitHandles.Length > 0)
                        {
                            if (!EventWaitHandle.WaitAll(_waitHandles, new TimeSpan(0, 0, 30))) // 30 seconds grace time
                            {
                                LogManager.WriteLog("|=> Unable to cleanup the wait handles.", LogManager.enumLogLevel.Info);
                            }
                            else
                            {
                                LogManager.WriteLog("|=> All the wait handles are released.", LogManager.enumLogLevel.Info);
                            }
                        }
                       
                    }
                    catch { LogManager.WriteLog("ExchangeMonitorThreads were aborted.", LogManager.enumLogLevel.Info); }
                }
                _isDisposed = true;
            }
        }

        #endregion
    }

    #endregion public Class
}
