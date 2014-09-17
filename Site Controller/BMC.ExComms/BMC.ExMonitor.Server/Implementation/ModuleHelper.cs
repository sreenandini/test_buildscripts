using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Configuration;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.PlayerGateway.Gateway;
using System.Threading.Tasks;
using System.Threading;

namespace BMC.ExMonitor.Server
{
    public sealed class ModuleHelper
        : DisposableObject
    {
        private readonly IExMonitorServerConfigStore _configStore = ExMonitorServerConfigStoreFactory.Store;
        ManualResetEvent manualResetEvt = new ManualResetEvent(false);

        #region Single Thread Helper (Current)

        private readonly static SingletonHelper<ModuleHelper> _moduleHelper = new SingletonHelper<ModuleHelper>(
            new Lazy<ModuleHelper>(() => new ModuleHelper()));

        public static ModuleHelper Current
        {
            get { return _moduleHelper.Current; }
        }

        #endregion

        private readonly IDictionary<string, InstallationDetailsForMSMQ> _installationDetailsCache =
            new StringConcurrentDictionary<InstallationDetailsForMSMQ>();

        private ModuleHelper()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Initialize"))
            {
                try
                {
                    Task taskEpiCheck = Task.Factory.StartNew(() => { CheckEPITimeouts(); }, TaskCreationOptions.LongRunning);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    Environment.FailFast("Unable to intialize Module Helper");
                }
            }
        }

        public IDictionary<string, InstallationDetailsForMSMQ> InstallationDetailsCache
        {
            get { return _installationDetailsCache; }
        }

        public InstallationDetailsForMSMQ GetInstallationFromCache(string key, int installationNo)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetInstallationFromCache"))
            {
                InstallationDetailsForMSMQ data = null;

                try
                {
                    var cache = this.InstallationDetailsCache;
                    bool force = (key.IsEmpty() ||
                                  !cache.ContainsKey(key) ||
                                  _configStore.ForceInstallations.ContainsKey(key));

                    data = (force ?
                        cache.AddOrUpdate2(key, ExCommsDataContext.Current.GetInstallationDetailsByDatapak(installationNo)) :
                        cache.GetIfExists(key));
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return data;
            }
        }

        public void CopyTo(AFTInformation source, ExCommsPlayerFlags destination, bool isSpecialPlayer = false, bool offer = false)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CopyTo"))
            {
                try
                {
                    destination.Flag2.Flag_OfferOpt = true;
                    destination.Flag2.Flag_WithdrawCash = source.CanWithdrawCash;

                    destination.Flag3.Flag_WithdrawPoints = (offer ? source.RedeemPoints : source.CanWithdrawPoints);
                    destination.Flag3.Flag_WithdrawPromo = (offer ? source.WithdrawOffers : source.CanWithdrawPromo);
                    destination.Flag3.Flag_DepositPromo = source.CanDepositNonCashable;
                    destination.Flag3.Flag_IsVIP = (isSpecialPlayer ? source.SpecialPlayer : source.VIPFlag);
                    destination.Flag3.Flag_DepositCash = source.CanDepositCashable;
                    destination.Flag3.Flag_QueryAmount = source.CanEnterAmount;
                    destination.Flag3.Flag_IsPINRequired = source.PinRequired;
                    destination.Flag3.Flag_IsECashPlayer = source.EcashPlayer;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public void StopCheckEPITimeouts()
        {
            manualResetEvt.Set();
        }

        private void CheckEPITimeouts()
        {

            List<InstallationDetailsForMSMQ> installationDetailsForMSMQ = null;

            while (!manualResetEvt.WaitOne(100))
            {
                installationDetailsForMSMQ = ExCommsDataContext.Current.GetInstallationDetailsByDatapak();

                Parallel.ForEach<InstallationDetailsForMSMQ>(installationDetailsForMSMQ, (installationDetails) =>
                {
                    if (InstallationDetailsCache.ContainsKey(installationDetails.Installation_No.ToString()))
                    {
                        if (EPIManager.Current.AnyEPITimeouts(installationDetails.Installation_No))
                            EPIManager.Current.CheckEPITimeouts(installationDetails.Installation_No);
                    }
                });
            }
        }
    }
}