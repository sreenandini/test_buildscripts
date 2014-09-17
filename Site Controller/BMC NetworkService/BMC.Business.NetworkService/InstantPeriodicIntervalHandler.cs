using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComExchangeLib;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Runtime.InteropServices;
using System.Data;
using BMC.DBInterface.NetworkService;
using System.Timers;
using BMC.Common.ConfigurationManagement;
using BMC.Transport.NetworkService;
using System.Threading;
#if NEW_EXCOMMS
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Proxies;
#endif
//using System.Threading;

namespace BMC.Business.NetworkService
{
    public class InstantPeriodicIntervalHandler : ObjectStateObserver, IDisposable
    {
        private ExchangeClient _exchangeClient;
        private IExchangeAdmin _iExchangeAdmin;
        Sector203Data m_SectorData = new Sector203Data();
        private System.Timers.Timer _tmrRequest = null;
        System.Threading.ManualResetEvent mEvent = new System.Threading.ManualResetEvent(false); 

        public InstantPeriodicIntervalHandler()
        {
            _exchangeClient = new ExchangeClient();
            _exchangeClient.InitialiseExchange(0);

            _tmrRequest = new System.Timers.Timer(Int32.Parse(ConfigManager.Read("InstantPeriodicInterval")) * 1000);
            _tmrRequest.Elapsed += new ElapsedEventHandler(ProcessRequest);

            _iExchangeAdmin = (IExchangeAdmin)_exchangeClient;
            ObjectStateNotifier.AddObserver(this);

            _tmrRequest.Start();
        }

        public override void NotifyState(ObjectState state)
        {
            base.NotifyState(state);
        }

        private void ProcessRequest(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = sender as System.Timers.Timer;

            try
            {
                timer.Stop();

                // Instant Periodic Interval Modified
                bool isModified = DBBuilder.IsInstantPeriodicIntervalModified();

                // Modified ?
                if (isModified)
                {
                    LogManager.WriteLog("|##> +YES+ Change in Instant Periodic Interval found.", LogManager.enumLogLevel.Info);
                    this.SendSectorCommand();
                }
                else
                {
                    LogManager.WriteLog("|##> -NO- No change in Instant Periodic Interval found.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (!this.IsObjectInactive)
                {
                    timer.Start();
                }
            }
        }

        internal int SendSectorCommand()
        {
            if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
            {
                return -1;
            }



            DataTable dt = DBBuilder.GetInstallationsForInstantPeriodicInterval();
            if (dt == null || dt.Rows.Count == 0) return -1;

            byte[] bData = { };

            m_SectorData.Command = Convert.ToByte(0x71);
            m_SectorData.PutCommandDataVB(bData);

            foreach (DataRow dr in dt.Rows)
            {
                int installationNo = 0;
                try
                {
                    if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
                    {
                        break;
                    }

                    if (Int32.TryParse(dr["Installation_No"].ToString(), out installationNo))
                    {
                        #if !NEW_EXCOMMS
                        _exchangeClient.RequestExWriteSector(installationNo, 203, m_SectorData);
                        #else
                                                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                        {
                            InstallationNo = installationNo,
                        };
                        monMsg_H2G.Targets.Add(new MonTgt_H2G_InstantPeriodicNW { });
                        return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
                        #endif

                        LogManager.WriteLog("|##> Successfully sent the details for installation : " + installationNo.ToString(), LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog("|##> Unable to get the installation from database for installation : " + installationNo.ToString(), LogManager.enumLogLevel.Error);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("|##> Unable to send command for Installation : " + installationNo.ToString(), LogManager.enumLogLevel.Error);
                    ExceptionManager.Publish(ex);
                }
            }
            
            return 0;
        }

        #region IDisposable Members

        private bool _disposed;
        
        public void Dispose()
        {
            if (_disposed) return;
            ObjectStateNotifier.RemoveObserver(this);

            try
            {
                var i = Marshal.ReleaseComObject(_exchangeClient);
                Thread.Sleep(10);

                while (i > 0)
                {
                    LogManager.WriteLog("Number of objects in _exchangeClient = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_exchangeClient);
                }
                LogManager.WriteLog("|=> _exchangeClient was released successfully.", LogManager.enumLogLevel.Info);

                i = Marshal.ReleaseComObject(_iExchangeAdmin);
                Thread.Sleep(10);
                while (i > 0)
                {
                    LogManager.WriteLog("Number of objects in _iExchangeAdmin = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_iExchangeAdmin);
                }
                LogManager.WriteLog("|=> _iExchangeAdmin was released successfully.", LogManager.enumLogLevel.Info);
            }
            catch
            { }

            _iExchangeAdmin = null;
            _exchangeClient = null;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="InstantPeriodicIntervalHandler"/> is reclaimed by garbage collection.
        /// </summary>
        ~InstantPeriodicIntervalHandler()
        {
            Dispose();
            _disposed = true;
        }

        #endregion



    }
  
}
