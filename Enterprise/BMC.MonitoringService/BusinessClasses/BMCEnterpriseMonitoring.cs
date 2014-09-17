namespace BMC.MonitoringService
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BMC.Common.LogManagement;
    using BMC.Common.ExceptionManagement;
    using System.ServiceProcess;
    using System.Threading;
    using System.Reflection;
    using System.IO;

    #endregion Namespaces

    #region Public Class

    public class BMCEnterpriseMonitoring : IDisposable
    {
        #region Private Variables

        private List<string> enterpriseServices     =   new List<string>();
        private ServiceDetails serviceDetails       =   null;
        private DataHelper dataHelper               =   null;
        private Thread EnterpriseMonitorThread      =   null;
        private bool _isListening = true;

       public  ManualResetEvent mre_ShutDown = new ManualResetEvent(false);

        #endregion Private Variables

        #region Public Methods

        public void StartEnterpriseMonitoring()
        {
            try
            {
                //lock (this)
                {
                    LogManager.WriteLog("Starting Enterprise Monitoring...", LogManager.enumLogLevel.Info);
                }
                
                dataHelper              =   new DataHelper();

                serviceDetails          =   dataHelper.GetAllServiceDetails().ToList<ServiceDetails>().SingleOrDefault();
                enterpriseServices      =   serviceDetails.Setting_Value.Split(',').ToList();

                // Removing the self starting service
                string selfExeName = "BMC.MonitoringService";
                enterpriseServices.Remove(selfExeName);

                foreach (string service in enterpriseServices)
                {
                    //lock (this)
                    {
                        LogManager.WriteLog(string.Format("Started Monitoring Service - [{0}]", service), LogManager.enumLogLevel.Info);
                    }
                }

                //lock (this)
                {
                    LogManager.WriteLog("Spawning MonitorEnterpriseServices Thread...", LogManager.enumLogLevel.Info);
                }

                EnterpriseMonitorThread =   new Thread(new ThreadStart(MonitorEnterpriseServices));
                EnterpriseMonitorThread.Start();

                //lock (this)
                {
                    LogManager.WriteLog("Enterprise Monitoting started successfully", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void StopEnterpriseMonitoring()
        {
            try
            {
                _isListening = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void MonitorEnterpriseServices()
        {
            try
            {
                //lock (this)
                {
                    LogManager.WriteLog("MonitorEnterpriseServices Thread spawned successfully.", LogManager.enumLogLevel.Info);
                }

                while (!mre_ShutDown.WaitOne(StaticMembers.EnterpriseMonitorInterval))
                {
                    try
                    {
                        foreach (string service in enterpriseServices)
                        {
                            if (mre_ShutDown.WaitOne(1))
                            {
                                LogManager.WriteLog("MonitorEnterpriseServices was instruced to close.", LogManager.enumLogLevel.Info);
                                break;
                            }
                            StartWindowsService(service);
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
            }
            catch (Exception ex)
            {
                //lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        private void StartWindowsService(string serviceName)
        {
            try
            {
                using (ServiceController serviceController = new ServiceController())
                {
                    serviceController.ServiceName = serviceName;

                    try
                    {
                        if (serviceController.Status != ServiceControllerStatus.Running)                        
                        {
                            if (serviceController.Status == ServiceControllerStatus.Paused)
                            {
                                serviceController.Stop();
                                serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                            }
                            else if ((serviceController.Status == ServiceControllerStatus.StartPending) ||
                                     (serviceController.Status == ServiceControllerStatus.PausePending) ||
                                     (serviceController.Status == ServiceControllerStatus.StopPending))
                            {
                                serviceController.Stop();
                                serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                            }

                            //lock (this)
                            {
                                LogManager.WriteLog(string.Format("Starting Service - [{0}]...", serviceName), 
                                    LogManager.enumLogLevel.Info);
                            }

                            serviceController.Start();

                            //lock (this)
                            {
                                LogManager.WriteLog(string.Format("Service - [{0}] started  successfully ", serviceName), 
                                    LogManager.enumLogLevel.Info);
                            }                            
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        //lock (this)
                        {
                            LogManager.WriteLog(string.Format("Invalid Operation - Service [{0}] does not exist", serviceName),
                                LogManager.enumLogLevel.Error);
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
                    _isListening = false;
                    try
                    {
                        if (EnterpriseMonitorThread != null &&
                            EnterpriseMonitorThread.IsAlive)
                        {
                            EnterpriseMonitorThread.Abort();
                        }
                    }
                    catch { LogManager.WriteLog("EnterpriseMonitorThread was aborted.", LogManager.enumLogLevel.Info); }
                }
                _isDisposed = true;
            }
        }

        #endregion
    }

    #endregion Public Class
}
