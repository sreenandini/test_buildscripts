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
using Microsoft.Office.Interop.Excel;
using BMC.Business.CashDeskOperator;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using System.Globalization;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CVaultEvents.xaml
    /// </summary>
    public partial class CVaultEvents : UserControl
    {
        VaultBiz objVaultBiz = null;
        private int _RecordCount = 0;
        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        private string strKeyText = "";
        public CVaultEvents()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("CVaultEvents:UserControl_Loaded", LogManager.enumLogLevel.Debug);
                dtpStartDate.SelectedDate = DateTime.Now.Date.AddDays(-1);
                dtpEndDate.SelectedDate = DateTime.Now.Date;
                dtpStartTime.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                dtpEndtime.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                objVaultBiz = new VaultBiz();
                string[] str = new string[] { "20", "30", "40", "50", "60", "100", "200", "300", "All" };
                string[] strEvents = new string[] { "All", "Voucher", "Handpay", "Jackpot", "Mystery", "Prog", "Event" };
                cmb_SelectTop.ItemsSource = str;
                cmb_Type.ItemsSource = strEvents;
                cmb_SelectTop.SelectedIndex = 0;
                cmb_Type.SelectedIndex = 0;
                //refreshEvents(0);
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);

            }

        }

        private void btn_ResetVault_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_SelectTop.SelectedValue.ToString() == "All")
            {
                this.refreshEvents(0);
            }
            else
            {
                this.refreshEvents(int.Parse(cmb_SelectTop.SelectedValue.ToString()));
            }

        }

        void refreshEvents(int iRecordCount)
        {
            try
            {
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("Vault_MessageID26", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("Vault_MessageID27", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("Vault_MessageID28", BMC_Icon.Information);
                    return;
                }
                LogManager.WriteLog("CVaultEvents:refreshEvents", LogManager.enumLogLevel.Debug);

                if (dtpStartDate.SelectedDate != null && dtpEndDate.SelectedDate != null)
                {
                    StartDate = new DateTime(((DateTime)dtpStartDate.SelectedDate).Year,
                        ((DateTime)dtpStartDate.SelectedDate).Month,
                        ((DateTime)dtpStartDate.SelectedDate).Day,
                        dtpStartTime.SelectedHour, dtpStartTime.SelectedMinute,
                        dtpStartTime.SelectedSecond);

                    EndDate = new DateTime(((DateTime)dtpEndDate.SelectedDate).Year,
                        ((DateTime)dtpEndDate.SelectedDate).Month,
                        ((DateTime)dtpEndDate.SelectedDate).Day,
                        dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute,
                        dtpEndtime.SelectedSecond);


                    DataSet dtHistory = objVaultBiz.GetVaultTransactionEvents(0, cmb_Type.SelectedValue.ToString(), iRecordCount, txt_description.Text, StartDate, EndDate);
                    if (dtHistory.Tables[0].Rows.Count > 0)
                    {
                        this.DataContext = dtHistory.Tables[0].DefaultView;
                        lst_Events.ItemsSource = dtHistory.Tables[0].DefaultView;

                    }
                    else
                    {

                        this.DataContext = null;
                        lst_Events.ItemsSource = null;
                        MessageBox.ShowBox("MessageID37", BMC_Icon.Information);
                        LogManager.WriteLog("CVaultEvents:No records found", LogManager.enumLogLevel.Debug);
                    }


                    LogManager.WriteLog("CVaultEvents:Complete", LogManager.enumLogLevel.Debug);
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btn_PrintEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("CVaultEvent->btnPrintEvent_Click()", LogManager.enumLogLevel.Debug);

                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("Vault_MessageID26", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("Vault_MessageID27", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("Vault_MessageID28", BMC_Icon.Information);
                    return;
                }
                if (cmb_SelectTop.SelectedValue.ToString() == "All")
                {
                    _RecordCount = 0;
                }
                else
                {
                    _RecordCount = int.Parse(cmb_SelectTop.SelectedValue.ToString());
                }
                if (dtpStartDate.SelectedDate != null && dtpEndDate.SelectedDate != null)
                {
                    StartDate = new DateTime(((DateTime)dtpStartDate.SelectedDate).Year,
                        ((DateTime)dtpStartDate.SelectedDate).Month,
                        ((DateTime)dtpStartDate.SelectedDate).Day,
                        dtpStartTime.SelectedHour, dtpStartTime.SelectedMinute,
                        dtpStartTime.SelectedSecond);

                    EndDate = new DateTime(((DateTime)dtpEndDate.SelectedDate).Year,
                        ((DateTime)dtpEndDate.SelectedDate).Month,
                        ((DateTime)dtpEndDate.SelectedDate).Day,
                        dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute,
                        dtpEndtime.SelectedSecond);
                    CreateVaultEventDetailsReport(0, cmb_Type.SelectedValue.ToString(), _RecordCount, txt_description.Text, StartDate, EndDate);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CreateVaultEventDetailsReport(int Vault_Id, string Type, int No_Of_Records, string SearchKey, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                    cReportViewer.ShowVaultEventDetailReport(Vault_Id, Type, No_Of_Records, SearchKey, StartDate, EndDate);
                    cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));
                    cReportViewer.Show();
                }
                LogManager.WriteLog("Show Undeclared Drop Report Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dtpStartDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpStartDate.SelectedDate);
                StartDate = new DateTime(dt.Year, dt.Month, dt.Day, dtpStartTime.SelectedHour, dtpStartTime.SelectedMinute, dtpStartTime.SelectedSecond);
                txtStartDate.Text = Convert.ToDateTime(StartDate).ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            }
            catch (StackOverflowException ex)
            {
                ExceptionManager.Publish(ex);
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        private void dtpEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpEndDate.SelectedDate);
                EndDate = new DateTime(dt.Year, dt.Month, dt.Day, dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute, dtpEndtime.SelectedSecond);
                txtEndDate.Text = Convert.ToDateTime(EndDate).ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            }
            catch (StackOverflowException ex)
            {
                ExceptionManager.Publish(ex);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dtpEndtime_SelectedTimeChanged(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpEndDate.SelectedDate);
                EndDate = new DateTime(dt.Year, dt.Month, dt.Day, dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute, dtpEndtime.SelectedSecond);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dtpStartTime_SelectedTimeChanged(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpStartDate.SelectedDate);
                StartDate = new DateTime(dt.Year, dt.Month, dt.Day, dtpStartTime.SelectedHour, dtpStartTime.SelectedMinute, dtpStartTime.SelectedSecond);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txt_description_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!Settings.OnScreenKeyboard)
                    return;
                System.Windows.Controls.TextBox txtMouseUp = sender as System.Windows.Controls.TextBox;
                txtMouseUp.Text = DisplayKeyboard(txtMouseUp.Text);
                txtMouseUp.SelectionStart = txtMouseUp.Text.Length;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public string DisplayKeyboard(string keyText)
        {
            strKeyText = "";
            KeyboardInterface objKeyboard = null;

            try
            {
                System.Windows.Window w = System.Windows.Window.GetWindow(this);
                System.Windows.Point pt = default(System.Windows.Point);
                Size sz = default(Size);
                if (w != null)
                {
                    pt = new System.Windows.Point(w.Left, w.Top);
                    sz = new Size(w.Width, w.Height);
                }

                objKeyboard = new KeyboardInterface();
                objKeyboard.Owner = w;
                objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
                objKeyboard.KeyString = keyText;
                objKeyboard.Top = pt.Y + (sz.Height - objKeyboard.Height);
                objKeyboard.Left = pt.X + (sz.Width / 2) - (objKeyboard.Width / 2);
                objKeyboard.ShowInTaskbar = false;
                objKeyboard.ShowDialog();

                if (objKeyboard != null)
                {
                    objKeyboard.Closing -= this.objKeyboard_Closing;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
          
            return strKeyText;
        }

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside objKeyboard_Closing", LogManager.enumLogLevel.Info);

                if (((KeyboardInterface)sender).DialogResult == true)
                {
                    strKeyText = ((KeyboardInterface)sender).KeyString;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
