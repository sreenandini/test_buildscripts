using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using System.ComponentModel;
using System.Threading;
using BMC.Business.CashDeskOperator;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using ComExchangeLib;


namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for EmployeeCardSink.xaml
    /// </summary>
    public partial class EmployeeCardSync : UserControl
    {
        public static List<string> ProcessedAssets = new List<string>();
        BackgroundWorker bgWorker = new BackgroundWorker();
        ManualResetEvent _Reset = new ManualResetEvent(false);
        private AutoResetEvent _Autoreset = new AutoResetEvent(false);
        private ExchangeClient _exchangeClient;
        public EmployeeCardSync()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lvEmpCardSync.ItemsSource = ActiveInstallations();
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
                if (MessageBox.ShowBox("MessageID500", BMC_Icon.Information, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No) return;
                List<ActiveMachine> _activeMachine = (List<ActiveMachine>)lvEmpCardSync.ItemsSource;
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
                            ProcessToEmployeeCardSync(Machine);
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
                        LogManager.WriteLog("ProcessEmployeeCardSync: " + Ex.Message, LogManager.enumLogLevel.Info);
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
                LogManager.WriteLog("ProcessEmployeeCardSync: " + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
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

        private void ProcessToEmployeeCardSync(ActiveMachine Machine)
        {

            MachineManagerLazyInitializer manager = null;
            ActiveMachine _activeMachine = null;
           

             List<Employeecarddata> getEmpcards = oCommonUtilities.CreateInstance().GetEmployeeCardPollingData();


             try
             {
                 _activeMachine = Machine as ActiveMachine;
                 List<Employeecarddata> cards = getEmpcards.Where(emp => emp.Installation_No == _activeMachine.Installation_No).ToList();

                 EmployeeMasterCard empcard = new EmployeeMasterCard();
                 List<Employeecarddata> lstEmpPolling = empcard._emppollingCollection;


                 foreach (Employeecarddata data in lstEmpPolling)
                 {
                     if (data.Installation_No == _activeMachine.Installation_No)
                     {
                         if (data.PollingStatus)
                         {
                             _activeMachine.Status = "Success";
                             oCommonUtilities.CreateInstance().UpdateEmployeecardPolling(cards[0].EmployeeCard, _activeMachine.Installation_No);
                         }
                         else
                         {
                             _activeMachine.Status = "Failed";
                         }
                     }
                 }

                 if (lstEmpPolling.Count == 0) _activeMachine.Status = "Failed";
             }
             catch (Exception Ex)
             {

                 LogManager.WriteLog("ProcessToEmployeeCardSync: " + Ex.Message, LogManager.enumLogLevel.Info);
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
                         LogManager.WriteLog("ProcessToEmployeeCardSync:ReleaseMachineManager " + Ex.Message, LogManager.enumLogLevel.Info); ;
                     }
                 }
             }
        }

        public bool EmployeecardSend(string EmpCardNo, string EmployeeFlags, int InstallationNo)
        {
            try
            {
                string EmpFlags = EmployeeFlags.Substring(2);
                List<byte> enumver = Enumerable.Range(0, EmpFlags.Length)
                       .Where(x => x % 2 == 0)
                       .Select(x => Convert.ToByte(EmpFlags.Substring(x, 2), 16)).ToList();

                List<byte> cardno = Enumerable.Range(0, EmpCardNo.PadLeft(10, '0').Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(EmpCardNo.PadLeft(10, '0').Substring(x, 2), 16)).ToList();

                enumver.Insert(0, Convert.ToByte(EmployeeFlags.Substring(0, 1)));
                enumver.Insert(1, Convert.ToByte(EmployeeFlags.Substring(1, 1)));
                enumver.InsertRange(0, cardno);

                byte[] bData = enumver.ToArray();

                bool status = new MachineManagerLazyInitializer().GetMachineManager().SendEmpCard(InstallationNo, 84, bData);
                return status;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }

        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            List<ActiveMachine> _activeMachine = (List<ActiveMachine>)lvEmpCardSync.ItemsSource;
            foreach (ActiveMachine Machine in _activeMachine)
            {
                Machine.IsChecked = 1;
            }
        }

        private void btnDeSelectAll_Click(object sender, RoutedEventArgs e)
        {
            List<ActiveMachine> _activeMachine = (List<ActiveMachine>)lvEmpCardSync.ItemsSource;
            foreach (ActiveMachine Machine in _activeMachine)
            {
                Machine.IsChecked = 0;
            }
        }
        private bool IsAnyOneSelected()
        {
            List<ActiveMachine> _activeMachine = (List<ActiveMachine>)lvEmpCardSync.ItemsSource;
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
