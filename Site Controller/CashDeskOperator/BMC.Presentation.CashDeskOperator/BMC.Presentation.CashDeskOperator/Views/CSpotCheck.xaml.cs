using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using Audit.Transport;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Helper_classes;
using BMC.Presentation.POS.Views;
using BMC.Security;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CSpotCheck.xaml
    /// </summary>
    public partial class CSpotCheck : UserControl, IDisposable
    {
        #region DataMembers

        private SpotCheckConfiguration oSpotCheckConfiguration = SpotCheckConfiguration.SpotCheckConfigurationInstance;
        private List<Installations> lstInstallationDetails = new List<Installations>();
        private List<ZoneByMachine> lstInstallationDetailsByZone = new List<ZoneByMachine>();
        private List<SpotCheck> lstSpotCheckSummaryDetails = new List<SpotCheck>();

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion //DataMembers

        #region Constructor

        public CSpotCheck()
        {
            try
            {
                InitializeComponent();
                LoadInstallationDetails();
                //BeforeProcessStart();
                //RunProcess();
                if (IsMachineSelected()) AssignInstallationValues();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Constructor

        #region Events

        private void btnPerformSpotCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckMachineSelected()) return;
                this.IsEnabled = false;
                
                Installations objInstallationDetails = tvMachineList.SelectedItem as Installations;
                if (objInstallationDetails == null) return;

                int iLstIndx = -1;
                iLstIndx = (from SpotCheck objSpotCheck in lstSpotCheckSummaryDetails
                            where objSpotCheck.InstallationNo == objInstallationDetails.Installation_No
                            select lstSpotCheckSummaryDetails.LastIndexOf(objSpotCheck)).DefaultIfEmpty(-1).FirstOrDefault();

                Action doAnalysis = () =>
                {
                    if (iLstIndx >= 0)
                    {
                        lstSpotCheckSummaryDetails.RemoveAt(iLstIndx);
                    }
                        lstSpotCheckSummaryDetails.Add(oSpotCheckConfiguration.GetSpotCheckSummaryDetails(objInstallationDetails.Installation_No, objInstallationDetails.POP)[0]);
                };

                List<int> lstInatallation = new List<int>();
                lstInatallation.Add(objInstallationDetails.Installation_No);
                LoadingWindow ld_analysis = new LoadingWindow(this, ModuleName.SpotCheck, lstInatallation, doAnalysis);
                ld_analysis.ShowDialog();

                AfterProcessCompleted();
                //txtblkMessage.Text = (Application.Current.FindResource("MessageID504") as string).Replace("@@@@@@", objInstallationDetails.Bar_Position_Name);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                //txtblkMessage.Text = Application.Current.FindResource("MessageID505") as string;
            }

            finally
            {
                this.IsEnabled = true;
            }
        }

        private void btnPrintReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearMessageText();
                if (!IsMachineSelected())
                {
                    txtblkMessage.Text = Application.Current.FindResource("MessageID503") as string;
                    return;
                }

                PrintSpotCheckReport();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkZone_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearMessageText();
                DisplayByZone();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkZone_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearMessageText();
                DisplayByMachine();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void tvMachineList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                ClearMessageText();
                if (!IsMachineSelected()) return;

                AssignInstallationValues();
                AssignSummaryValues();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Events

        #region Methods

        //private void BeforeProcessStart()
        //{
        //    this.IsEnabled = false;
        //    ClearMessageText();
        //}

        //private void RunProcess()
        //{
        //    try
        //    {
        //        if (lstSpotCheckSummaryDetails == null) return;
        //        lstSpotCheckSummaryDetails.Clear();

        //        Action doAnalysis = () =>
        //        {
        //            lstInstallationDetails.ForEach(item =>
        //            {
        //                lstSpotCheckSummaryDetails.Add(oSpotCheckConfiguration.GetSpotCheckSummaryDetails(item.Installation_No, item.POP)[0]);
        //            });
        //        };

        //        List<int> lstInatallation = lstInstallationDetails.Select(item => item.Installation_No).ToList();
        //        LoadingWindow ld_analysis = new LoadingWindow(this, ModuleName.SpotCheck, lstInatallation, doAnalysis);
        //        ld_analysis.ShowDialog();

        //        AfterProcessCompleted();
        //    }

        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        private void AfterProcessCompleted()
        {
            try
            {
                AssignSummaryValues();
                this.IsEnabled = true;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadInstallationDetails()
        {
            var displayByMachine = (FindResource("DisplayMachines") as HierarchicalDataTemplate);
            lstInstallationDetails = oSpotCheckConfiguration.GetInstallationDetails();
            if (lstInstallationDetails == null) return;
            lstInstallationDetails[0].IsSelected = true;

            lstInstallationDetails.ForEach(item => 
                            {
                                if (lstInstallationDetailsByZone.Where(zone => zone.ZoneName == item.Zone_Name).Count() != 1)
                                {
                                    lstInstallationDetailsByZone.Add(new ZoneByMachine()
                                        {
                                            ZoneName = item.Zone_Name,
                                            Machines = new List<Installations>()
                                        });
                                }

                                lstInstallationDetailsByZone.Where(zone => zone.ZoneName == item.Zone_Name).SingleOrDefault().Machines.Add(item);
                            });

            lstInstallationDetailsByZone = lstInstallationDetailsByZone.OrderBy(item => item.ZoneName).ToList();
            tvMachineList.SelectedValuePath = "Installation_No";
            tvMachineList.ItemsSource = lstInstallationDetails;
            tvMachineList.ItemTemplate = displayByMachine;
        }

        private bool IsMachineSelected()
        {
            if (tvMachineList.SelectedItem == null) return false;
            return true;
        }

        private void AssignInstallationValues()
        {
            Installations objInstallationDetails = tvMachineList.SelectedItem as Installations;
            if (objInstallationDetails == null) return;

            txtDate.Text = DateTime.Now.GetUniversalDateFormat();
            txtTime.Text = DateTime.Now.GetUniversalTimeFormat();
            txtZone.Text = objInstallationDetails.Zone_Name;
            txtPos.Text = objInstallationDetails.Bar_Position_Name;
            txtUser.Text = SecurityHelper.CurrentUser.UserName;
            txtGameTitle.Text = objInstallationDetails.GameTitle;
        }

        private void AssignSummaryValues()
        {
            ClearSummaryValues();
            Installations objInstallationDetails = tvMachineList.SelectedItem as Installations;
            if (objInstallationDetails == null) return;

            List<SpotCheck> lstSpotCheck = new List<SpotCheck>();
            lstSpotCheck = lstSpotCheckSummaryDetails.Where(item => item.InstallationNo == objInstallationDetails.Installation_No).ToList();
            if (lstSpotCheck == null || lstSpotCheck.Count <= 0) return;

            txtLastMeterUpdate.Text = lstSpotCheck[0].DateTimeStamp.GetUniversalDateTimeFormat();
            if (lstSpotCheck[0].Date == DateTime.MinValue)
            {
                txtLastDropDate.Text = "N/A";
            }
            else
            {
                txtLastDropDate.Text = lstSpotCheck[0].Date.GetUniversalDateTimeFormat();
            }
            txtNetWinLoss.Text = (lstSpotCheck[0].CashIn - lstSpotCheck[0].CashOut).GetUniversalCurrencyFormatWithSymbol();
            txtHandle.Text = lstSpotCheck[0].CashIn.GetUniversalCurrencyFormatWithSymbol();
            txtPercentagePayout.Text = lstSpotCheck[0].Payout.ToString() + " %";
            txtDrop.Text = Convert.ToDecimal(lstSpotCheck[0].CoinsDrop).GetUniversalCurrencyFormatWithSymbol();
            txtHandpay.Text = lstSpotCheck[0].HandPay.GetUniversalCurrencyFormatWithSymbol();
        }

        private void DisplayByZone()
        {
            var displayByZone = (FindResource("DisplayByZone") as HierarchicalDataTemplate);
            tvMachineList.ItemsSource = lstInstallationDetailsByZone;
            tvMachineList.ItemTemplate = displayByZone;
        }

        private void DisplayByMachine()
        {
            var displayByMachine = (FindResource("DisplayMachines") as HierarchicalDataTemplate);
            tvMachineList.ItemsSource = lstInstallationDetails;
            tvMachineList.ItemTemplate = displayByMachine;
        }

        private void PrintSpotCheckReport()
        {
            Installations objInstallationDetails = tvMachineList.SelectedItem as Installations;
            if (objInstallationDetails == null) return;

            List<SpotCheck> lstSpotCheck = new List<SpotCheck>();
            lstSpotCheck = lstSpotCheckSummaryDetails.Where(item => item.InstallationNo == objInstallationDetails.Installation_No).ToList();
            if (lstSpotCheck == null || lstSpotCheck.Count <= 0) return;

            using (CReportViewer cReportViewer = new CReportViewer())
            {
                cReportViewer.ShowSpotCheckReport(
                                                    objInstallationDetails.Bar_Position_Name,
                                                    objInstallationDetails.Zone_Name.IsNullOrEmpty() ? string.Empty : objInstallationDetails.Zone_Name,
                                                    objInstallationDetails.GameTitle,//(Convert.ToDecimal(objInstallationDetails.POP) / 100).ToString(),
                                                    (Convert.ToDecimal(objInstallationDetails.POP)).ToString(),
                                                    lstSpotCheck[0].DateTimeStamp,
                                                    (lstSpotCheck[0].CashIn - lstSpotCheck[0].CashOut),
                                                    lstSpotCheck[0].CashIn,
                                                    Convert.ToDecimal(lstSpotCheck[0].Payout),
                                                    Convert.ToDecimal(lstSpotCheck[0].CoinsDrop.ToString("#,##0.00")),
                                                    lstSpotCheck[0].HandPay,
                                                    lstSpotCheck[0].Date,
                                                    Settings.SiteCode
                                                 );
                cReportViewer.ShowDialog();
            }
        }

        private void ClearMessageText()
        {
            txtblkMessage.Text = string.Empty;
        }

        private void ClearSummaryValues()
        {
            txtLastMeterUpdate.Text = string.Empty;
            txtLastDropDate.Text = string.Empty;
            txtNetWinLoss.Text = string.Empty;
            txtHandle.Text = string.Empty;
            txtPercentagePayout.Text = string.Empty;
            txtDrop.Text = string.Empty;
            txtHandpay.Text = string.Empty;
        }

        private bool CheckMachineSelected()
        {
            ClearMessageText();
            if (!IsMachineSelected())
            {
                txtblkMessage.Text = Application.Current.FindResource("MessageID503") as string;
                return false;
            }
            return true;
        }

        #endregion //Methods

        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.btnPerformSpotCheck.Click -= (this.btnPerformSpotCheck_Click);
                        this.btnPrintReport.Click -= (this.btnPrintReport_Click);
                        this.chkZone.Checked -= (this.chkZone_Checked);
                        this.chkZone.Unchecked -= (this.chkZone_UnChecked);
                        this.tvMachineList.SelectedItemChanged -= (this.tvMachineList_SelectedItemChanged);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("CSpotCheck objects are released successfully.", LogManager.enumLogLevel.Info);

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CAanalysisDetails"/> is reclaimed by garbage collection.
        /// </summary>
        ~CSpotCheck()
        {
            Dispose(false);
        }

        #endregion      

    }

    #region Zone data collection

    public class ZoneByMachine : INotifyPropertyChanged
    {
        private string _zoneName;
        private bool _isSelected;
        private List<Installations> _machines;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }

        public string ZoneName
        {
            get { return _zoneName; }
            set
            {
                _zoneName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ZoneName"));
                }
            }
        }

        public List<Installations> Machines
        {
            get { return _machines; }
            set
            {
                _machines = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Machines"));
                }
            }
        }
    }

    #endregion //Zone data collection
}
