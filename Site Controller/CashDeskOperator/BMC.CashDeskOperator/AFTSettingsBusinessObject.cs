using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using System.Windows.Input;
using System.Windows.Controls;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System.Data;
using System.Threading;

namespace BMC.CashDeskOperator
{
    public class AFTSettingsBusinessObject : IAFTSettingsDetails
    {
         #region Private Variables

        AFTSettings settings = new AFTSettings();

        #endregion

       #region Constructor

        private AFTSettingsBusinessObject() { }

        #endregion

       #region Public Static Function
        public static IAFTSettingsDetails CreateInstance()
        {
            return new AFTSettingsBusinessObject();
        }
        #endregion

        #region IAFTSettingsDetails Members

        public List<BMC.Transport.AFTSetting> GetAFTSettings(int iDenom)
        {
            return settings.GetAFTSettingsDetails(iDenom);
        }

        public bool SaveAFTSettings(List<Transport.AFTSetting> lstSettings)
        {
            return settings.SaveAFTSettings(lstSettings);
        }
        public DataTable GetDenoms()
        {
            return settings.GetDenoms(); 
        }

        #endregion

       
    }

    public class AftAssetDetails
    {
        ICommand process;
        ICommand selectAll;
        ICommand deSelectAll;
        AftAssets aftAssets = new AftAssets();

        public ICommand Process
        {
            get
            {
                if (process == null)
                {
                    process = new DelegateCommandParam<ListView>(ExchangeAftParam);
                }

                return process;
            }
        }

        public ICommand SelectALL
        {
            get
            {
                if (selectAll == null)
                {
                    selectAll = new DelegateCommandParam<ListView>(SelectAssets);
                }

                return selectAll;
            }
        }

        public ICommand DeSelectALL
        {
            get
            {
                if (deSelectAll == null)
                {
                    deSelectAll = new DelegateCommandParam<ListView>(DeSelectAssets);
                }

                return deSelectAll;
            }
        }
           
        public List<BMC.Transport.AftAssets> GetAFTAssets()
        {
            return aftAssets.GetAFTAssets().ToList();
        }

        public void ExchangeAftParam(ListView aftAssets)
        {
            try
            {
                foreach (Transport.AftAssets aftAsset in ((List<Transport.AftAssets>)aftAssets.ItemsSource).Where(m => m.IsChecked == 1 && m.Status == 1).Select(m => m))
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ExchangeAft), aftAsset);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExchangeAftParam: " + ex.Message, LogManager.enumLogLevel.Info); 
                ExceptionManager.Publish(ex);
            }
            //
        }

        public void ExchangeAft(object _aftAsset)
        {
            try
            {
                Transport.AftAssets aftAsset = _aftAsset as Transport.AftAssets;
                AftAssets aftEnableDisable = new AftAssets();
                //
                MachineManagerInterface machineManagerInterface = new MachineManagerInterface();
                int nRet = 0;
                nRet = machineManagerInterface.EnableDisableAFT(aftAsset.InstallationNo, aftAsset.IsChecked);
                if (nRet == 0)
                {
                    aftAsset.Status = aftAsset.IsChecked;
                    aftEnableDisable.UpdateAftStatus(aftAsset.InstallationNo, aftAsset.IsChecked);
                }

                aftAsset.Message = nRet == 0 ? "Success" : nRet == -1 ? "Timed Out" : nRet == -2 ? "NACK" : "UnKnown Error";
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExchangeAft: " + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            finally
            {
            }
        }

        public void SelectAssets(ListView aftAssets)
        {
            foreach (Transport.AftAssets aftAsset in (List<Transport.AftAssets>)aftAssets.ItemsSource)
            {
                aftAsset.IsChecked = 1;
            }
            
        }

        public void DeSelectAssets(ListView aftAssets)
        {
            foreach (Transport.AftAssets aftAsset in (List<Transport.AftAssets>)aftAssets.ItemsSource)
            {
                aftAsset.IsChecked = 0;
            }
        }

        private bool IsAnyOneSelected(List<Transport.AftAssets> aftAssets)
        {
            foreach (Transport.AftAssets aftAsset in aftAssets)
            {
                if (Convert.ToBoolean(aftAsset.IsChecked))
                {
                    return true;
                }
            }
            return false;
        }
    }

    class DelegateCommandParam<T> : ICommand
        {

            Action<T> method = null;
            public DelegateCommandParam(Action<T> action)
            {
                method = action;
            }

            #region ICommand Members

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                if (method != null)
                {
                    method((T)parameter);
                }
            }

            #endregion
        }

}
