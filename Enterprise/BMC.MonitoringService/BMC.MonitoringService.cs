namespace BMC.MonitoringService
{
    #region Namespaces

    using System.ServiceProcess;
    using BMC.Common.LogManagement;
    using System.Threading;
    using BMC.Common.ExceptionManagement;
    using System;
    using System.Diagnostics;

    #endregion Namespaces

    #region Pubic Class

    public partial class BMCMonitoringService : ServiceBase
    {
        #region Private Variables

        BMCExchangeMonitoring _bmcExchangeMonitoring        =   new BMCExchangeMonitoring();
        BMCEnterpriseMonitoring _bMCEnterpriseMonitoring    =   new BMCEnterpriseMonitoring();
        BMCMoniterSite _BMCMoniterSite = new BMCMoniterSite();
        private object _lockObject = new object();
        private int _killProcessInitiated = 0;
        private Thread _thMonitorProcess = null;

        #endregion Private Variables

        #region Constructor

        public BMCMonitoringService()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Service Events

        protected override void OnStart(string[] args)
        {
            try
            {
                LogManager.WriteLog("Starting BMC Monitoring Service...", LogManager.enumLogLevel.Info);

                _bmcExchangeMonitoring.StartExchangeMonitoring();
                _bMCEnterpriseMonitoring.StartEnterpriseMonitoring();
                _BMCMoniterSite.Start();
                LogManager.WriteLog(string.Format("BMC Monitoring Service ({0:D}) was started successfully.", Process.GetCurrentProcess().Id), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                LogManager.WriteLog("Stopping BMC Monitoring Service...", LogManager.enumLogLevel.Info);
                this.MonitorAndKillProcess(120);
                
                _bmcExchangeMonitoring.StopExchangeMonitoring();
                _bMCEnterpriseMonitoring.StopEnterpriseMonitoring();
                _bMCEnterpriseMonitoring.mre_ShutDown.Set();
                _bmcExchangeMonitoring.mre_ShutDown.Set();
                _BMCMoniterSite.Stop();
                this.DisposeObject(ref _bMCEnterpriseMonitoring, "_bmcExchangeMonitoring");
                this.DisposeObject(ref _bmcExchangeMonitoring, "_bmcExchangeMonitoring");

                LogManager.WriteLog(string.Format("BMC Monitoring Service ({0:D}) was stopped successfully.", Process.GetCurrentProcess().Id), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.AbortProcessMonitorThread();
                Thread.Sleep(3000);
            }
        }
        
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="objectName">Name of the object.</param>
        private void DisposeObject<T>(ref T obj, string objectName)
            where T : class, IDisposable
        {
            this.DisposeObject<T>(ref obj, objectName, string.Empty);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="objectName">Name of the object.</param>
        /// <param name="extra">The extra.</param>
        private void DisposeObject<T>(ref T obj, string objectName, string extra)
            where T : class, IDisposable
        {
            try
            {
                if (obj != null)
                {
                    obj.Dispose();
                    obj = null;
                    LogManager.WriteLog("|%%> " + objectName +
                        (!string.IsNullOrEmpty(extra) ? "(" + extra + ")" : "") +
                        " is disposed successfully.", LogManager.enumLogLevel.Info);
                }
            }
            catch { }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void MonitorAndKillProcess(int secondsToWait)
        {
            try
            {
                secondsToWait = (secondsToWait <= 0 ? 120 : secondsToWait); // default 2 minutes
                if (_killProcessInitiated == 0)
                {
                    lock (_lockObject)
                    {
                        if (_killProcessInitiated == 0)
                        {
                            _killProcessInitiated = 1;
                            LogManager.WriteLog("|=> MonitorAndKillProcess() : Monitor Thread initiated.", LogManager.enumLogLevel.Info);
                            _thMonitorProcess = new Thread(new ParameterizedThreadStart(this.MonitorAndKillProcessInternal));
                            _thMonitorProcess.Name = "TH_MonitorProcess";
                            _thMonitorProcess.Start(secondsToWait);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Monitors the and kill process internal.
        /// </summary>
        /// <param name="state">The state.</param>
        private void MonitorAndKillProcessInternal(object state)
        {
            try
            {
                Thread.Sleep(((int)state) * 1000);
                try
                {
                    LogManager.WriteLog("|=> MonitorAndKillProcessInternal() : Monitor Thread activated. Killing current process.", LogManager.enumLogLevel.Info);
                    Thread.Sleep(3000);
                    Process.GetCurrentProcess().Kill();
                }
                catch { }
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog(string.Format("|=> MonitorAndKillProcessInternal() : Monitor Thread ({0:D}) aborted.", Thread.CurrentThread.ManagedThreadId), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Aborts the process monitor thread.
        /// </summary>
        private void AbortProcessMonitorThread()
        {
            try
            {
                if (_thMonitorProcess != null &&
                    _thMonitorProcess.IsAlive)
                {
                    _thMonitorProcess.Abort();
                }                
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog("|=> AbortProcessMonitorThread() : Monitor Thread aborted.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Service Events
    }

    #endregion Pubic Class
}
