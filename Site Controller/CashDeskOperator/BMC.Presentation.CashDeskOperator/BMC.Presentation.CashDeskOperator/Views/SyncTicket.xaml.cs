using System;
using System.Collections.Generic;
using System.Linq;
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BMC.Common.ExceptionManagement;
using BMC.Business.CashDeskOperator;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using BMC.Common.LogManagement;
using System.Windows.Threading;


namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for SyncTicket.xaml
    /// </summary>
    /// 
    public partial class SyncTicket : UserControl
    {
        public static List<string> ProcessedAssets = new List<string>();
        BackgroundWorker bgWorker = new BackgroundWorker();
        ManualResetEvent _Reset = new ManualResetEvent(false);
        private AutoResetEvent _Autoreset = new AutoResetEvent(false);

        public SyncTicket()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lvSyncTicket.ItemsSource = ActiveInstallations();
        }



        private List<ActiveMachine> ActiveInstallations()
        {
            var oDataContext = new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
            List<ActiveMachine> _activeMachine = new List<ActiveMachine>();
            foreach (var machine in oDataContext.GetActiveInstallations())
            {
                _activeMachine.Add(machine);
            }
            return _activeMachine;
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                if (!IsAnyOneSelected())
                {
                    MessageBox.ShowBox("MessageID369", BMC_Icon.Information, BMC_Button.OK);
                    return;
                }
                bgWorker = new BackgroundWorker();
                if (MessageBox.ShowBox("MessageID365", BMC_Icon.Information, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No) return;
                List<ActiveMachine> _activeMachine = (List<ActiveMachine>)lvSyncTicket.ItemsSource;
                if (_activeMachine.Count > 0)
                {
                    btnProcess.IsEnabled = false;
                }               
                bgWorker.DoWork += (s, e1) =>
                    {
                        try
                        {
                            foreach (ActiveMachine Machine in _activeMachine.Where(m => m.IsChecked == 1).Select(m => m))
                            {
                                if (_Reset.WaitOne(50))
                                {
                                    return;
                                }
                                ProcessToSyncTicket(Machine);
                                if (bgWorker.CancellationPending)
                                {                                    
                                    break;
                                }
                            }
                            _Autoreset.Set();
                           
                        }
                        catch (Exception Ex)
                        {
                            Application.Current.Dispatcher.Invoke((ThreadStart)delegate
                            {
                                btnProcess.IsEnabled = true;
                            });
                            LogManager.WriteLog("ProcessToSyncTicket: " + Ex.Message, LogManager.enumLogLevel.Info);
                        }
                    };
                bgWorker.WorkerSupportsCancellation = true;
                bgWorker.RunWorkerCompleted += (s, e1) =>
                    {
                        Application.Current.Dispatcher.Invoke((ThreadStart)delegate
                        {
                            btnProcess.IsEnabled = true;
                        });
                    };
                bgWorker.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                btnProcess.IsEnabled = true;
                LogManager.WriteLog("ProcessToSyncTicket: " + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
            
        }

        private void ProcessToSyncTicket(ActiveMachine Machine)
        {

            MachineManagerLazyInitializer manager = null;
            ActiveMachine _activeMachine = null;


            try
            {
                _activeMachine = Machine as ActiveMachine;
                oCommonUtilities.CreateInstance().UpdateTicketExpire(Settings.Ticket_Expire);
                //MachineManagerInterface machineManagerInterface = new MachineManagerInterface();
                manager = new MachineManagerLazyInitializer();

                int nSuccess = manager.GetMachineManager().UpdateTicketConfig(_activeMachine.Installation_No, Settings.Ticket_Expire);
                if (nSuccess == 0)
                {
                    _activeMachine.Status = "Success";
                    oCommonUtilities.CreateInstance().UpdateGMUSiteCodeStatus(_activeMachine.Installation_No,1);
                }
                else
                {
                    _activeMachine.Status = "Failed";
                }
            }
            catch (Exception Ex)
            {

                LogManager.WriteLog("ProcessToSyncTicket: " + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                if (manager != null)
                {
                    try
                    {
                        manager.ReleaseMachineManager();
                        manager = null;
                    }
                    catch (Exception Ex)
                    {
                        LogManager.WriteLog("ProcessToSyncTicket:ReleaseMachineManager " + Ex.Message, LogManager.enumLogLevel.Info); ;
                    }
                }

            }
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            List<ActiveMachine> _activeMachine = (List<ActiveMachine>)lvSyncTicket.ItemsSource;
            foreach (ActiveMachine Machine in _activeMachine)
            {
                Machine.IsChecked = 1;
            }
        }

        private void btnDeSelectAll_Click(object sender, RoutedEventArgs e)
        {
            List<ActiveMachine> _activeMachine = (List<ActiveMachine>)lvSyncTicket.ItemsSource;
            foreach (ActiveMachine Machine in _activeMachine)
            {
                Machine.IsChecked = 0;
            }
        }
        private bool IsAnyOneSelected()
        {
            List<ActiveMachine> _activeMachine = (List<ActiveMachine>)lvSyncTicket.ItemsSource;
            foreach (ActiveMachine Machine in _activeMachine)
            {
                if (Convert.ToBoolean(Machine.IsChecked))
                {
                    return true;
                }
            }
            return false;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (bgWorker.IsBusy)
            {

                bgWorker.CancelAsync();
                _Autoreset.WaitOne(); 
                
            }
        }


    }
}