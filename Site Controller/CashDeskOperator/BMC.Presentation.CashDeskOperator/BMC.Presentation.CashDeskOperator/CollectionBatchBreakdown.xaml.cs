using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.Utilities;
using BMC.Transport;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Helper_classes;
using BMC.CashDeskOperator;
using System.Data;
using BMC.Presentation.POS.Views;
using BMC.Business.CashDeskOperator;
using System.ComponentModel;
using BMC.Common.ExceptionManagement;
using System.Collections.Generic;
using System.Threading;
using System.Configuration;
using System.Collections.ObjectModel;

namespace BMC.Presentation.POS
{
    /// <summary>
    /// Interaction logic for CollectionBatchBreakdown.xaml
    /// </summary>
    public partial class BatchBreakdown : IDisposable
    {
        Batch _batch;
        int _BatchID;
        int _WeekID;
        int _WeekNumber;
        private bool isCommonCDOforDeclaration;
        string ExchangeConst = "";
        string TicketConst = "";      
        private bool _ChkWeek;
        private bool _Undeclared;
        //
        public BatchBreakdown(int batchID, int WeekID, int WeekNumber, SiteConfig siteConfig, bool ChkWeek)
        {
            try
            {
                InitializeComponent();
                isCommonCDOforDeclaration = Security.SecurityHelper.HasAccess("BMC.Presentation.CommonCDOforDeclaration");
                _SiteConfig = (isCommonCDOforDeclaration ? siteConfig : null);
                _BatchID = batchID;
                _WeekID = WeekID;
                _ChkWeek = ChkWeek;
                _WeekNumber = WeekNumber;
                 ExchangeConst = (siteConfig != null) ? siteConfig.ExchangeConnectionString : "";
                TicketConst = (siteConfig != null) ? siteConfig.TicketConnectionString : "";
                DisplayButtonVisibility();
                
                //As we dont get Batch ID in Weeks tab hiding expport batch button when week is selected 
                if (ChkWeek) //CR# 193734 
                    btnExport.Visibility = Visibility.Collapsed;

                BackgroundWorker _bgLoadHistoryScreen = new BackgroundWorker();
                _bgLoadHistoryScreen.DoWork += OnHistoryScreenInitialize;
                _bgLoadHistoryScreen.RunWorkerCompleted += OnHistoryScreenComplete;
                _bgLoadHistoryScreen.RunWorkerAsync(new BatchBreakDownParams()
                {
                    _BatchID = batchID,
                    _WeekId = WeekID
                });
                _bgLoadHistoryScreen.WorkerSupportsCancellation = true;
            }
            catch (Exception ex)
            {
                LogError("BatchBreakdown", ex);
            }
        }
        //
        private SiteConfig _SiteConfig = null;
        //
        private void LogError(string methodName, Exception ex)
        {
            LogManager.WriteLog("BatchBreakdown Error in " + methodName + " : " + ex.Message, LogManager.enumLogLevel.Error);
            ExceptionManager.Publish(ex);
        }
        //
        private void LogInfo(string content)
        {
            LogManager.WriteLog("BatchBreakdown Info - " + content + " : " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff tt"), LogManager.enumLogLevel.Info);
        }
        //
        void OnHistoryScreenInitialize(object sender, DoWorkEventArgs e)
        {
            try
            {
                LogInfo("START Loading Batch Break Down Screen");
                _batch = new Batch(0, 0, (e.Argument as BatchBreakDownParams)._BatchID, ExchangeConst,(e.Argument as BatchBreakDownParams)._WeekId);
                _batch.GetBatchBreakdownhistory();
                (e.Argument as BatchBreakDownParams)._BatchBreakDown = _batch;
                e.Result = e.Argument;
            }
            catch (Exception ex) { LogError("OnHistoryScreenInitialize", ex); }
        }
        //
        private void ResizeGridViewColumn(GridViewColumn column)
        {
            if (double.IsNaN(column.Width))
            {
                column.Width = column.ActualWidth;
            }
            column.Width = double.NaN;
        }
        void OnHistoryScreenComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BatchBreakDownParams _params = e.Result as BatchBreakDownParams;
                BatchDetails details;
                dgBatchBreakdown.ItemsSource = _params._BatchBreakDown.GetBatchDetails(out details);
                
                //foreach (GridViewColumn gvCol in gvWeek.Columns)
                //{
                //    ResizeGridViewColumn(gvCol);
                //    gvWeek.Columns[0].Width = 0;
                //}

                txtUser.Content = details.UserName.Split(',').First();
                txtUser.ToolTip = details.UserName;
                txtDate.Content = details.CollectionDate.GetUniversalDateTimeFormat();
                txtNo.Content = details.BatchNo;
                txtRoute.Content = details.BatchName;
                txtGamingDay.Content = details.BatchDate.GetUniversalDateTimeFormat();
                txtDate.Content = details.BatchDate.GetUniversalDateFormat();
                this.DataContext = details;
                if (CustomizeForWeek())

                    txtDate.Content = details.BatchDate.GetUniversalDateFormat() + " - " + details.WeekEndDate.Value.GetUniversalDateFormat();
                //txtDate.Content=details.BatchDate.GetUniversalDateFormat() + " - " + details.BatchEndDate;
                else
                    txtDate.Content = details.CollectionDate.GetUniversalDateTimeFormat();
                if (dgBatchBreakdown.Items.Count > 0)
                    dgBatchBreakdown.SelectedIndex = 0;
                UpdateLoadingBar();
            }
            catch (Exception ex) { LogError("OnHistoryScreenComplete", ex); }
        }
        //
        private void DisplayButtonVisibility()
        {
            lblBatchNo.Content = FindResource("CollectionBatchBreakdown_xaml_lblBatchNo");
            if (_WeekID > 0)
            {
                lblBatchNo.Content = FindResource("CollectionBatchBreakdown_xaml_lblWeekNo");
                lblBatchNo.Width = 150;
            }
            txtPGStatus.Text = "Loading...";
            pgLoading.Visibility = Visibility.Visible;
            txtPGStatus.Visibility = Visibility.Visible;
            btnExport.Visibility = Visibility.Collapsed;
            btnDetails.Visibility = Visibility.Collapsed;
            //btnWinLossReport.Visibility = Visibility.Collapsed;
            btnExport.Visibility = 
                Security.SecurityHelper.HasAccess("BMC.Presentation.BatchBreakdown.btnExport")? Visibility.Visible : Visibility.Collapsed;
            btnDetails.Visibility = Visibility.Visible;
            btnWinLossReport.Visibility = Visibility.Visible;
            //btnWinLossReport.Visibility = (isCommonCDOforDeclaration) ? Visibility.Collapsed : Visibility.Visible;
        }
        //
        private void UpdateLoadingBar()
        {
            Dispatcher.Invoke((ThreadStart)delegate
            {
                txtPGStatus.Visibility = Visibility.Collapsed;
                pgLoading.Visibility = Visibility.Collapsed;
            });
        }
        //
        private bool CustomizeForWeek()
        {
            bool isweekexists = false;
            if (_WeekID > 0)
            {
                txtNo.Content = _WeekNumber;
                txtGamingDay.Visibility = Visibility.Hidden;
                lblUser.Visibility = Visibility.Hidden;
                txtUser.Visibility = Visibility.Hidden;
                lblGamingDay.Visibility = Visibility.Hidden;
                isweekexists = true;
            }
            return isweekexists;
        }
        //
        public void ResetColumnWidths(GridView gridView)
        {
            if (gridView != null)
            {
                foreach (var col in gridView.Columns)
                {
                    if (double.IsNaN(col.Width)) col.Width = col.ActualWidth;
                    col.Width = double.NaN;
                }
            }
        }
        //
        private void dgBatchBreakdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BatchHistoryListView dr = (BatchHistoryListView)dgBatchBreakdown.SelectedItem;
                int CollectionNo = Convert.ToInt32(dr.CollectionKey.Split('#')[0]);
                int InstallationNo = Convert.ToInt32(dr.CollectionKey.Split('#')[1]);
                int Top = Convert.ToInt32(ConfigurationManager.AppSettings.Get("DisplayEventsCount"));
                if (InstallationNo > 0)
                {
                    dgEvents.ItemsSource = _batch.GetAllEvents(CollectionNo, InstallationNo, Top);
                    //ResetColumnWidths(GridViewEvents);
                }
            }
            catch (Exception ex)
            {
                LogError("dgBatchBreakdown_SelectionChanged", ex);
            }
        }
        //
        private void btn_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnExport.IsEnabled = false;
                BMC.CashDeskOperator.CollectionHelper collectionHelper = (isCommonCDOforDeclaration && !string.IsNullOrEmpty(ExchangeConst)) ? new BMC.CashDeskOperator.CollectionHelper(CommonUtilities.SiteConnectionString(ExchangeConst)) : new BMC.CashDeskOperator.CollectionHelper();
                collectionHelper.InsertIntoExportHistory(_BatchID);
                MessageBox.ShowBox("MessageID204");
            }
            catch (Exception ex)
            {
                LogError("btnExport_Click", ex);
            }
            finally
            {
                btnExport.IsEnabled = true;
            }
        }
        //
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgBatchBreakdown.SelectedIndex < 0 || dgBatchBreakdown.SelectedItem == null) return;
                btnDetails.IsEnabled = false;
                BatchHistoryListView dr = (BatchHistoryListView)dgBatchBreakdown.SelectedItem;
                int CollectionNo = Convert.ToInt32(dr.CollectionKey.Split('#')[0]);
                int installationNo = Convert.ToInt32(dr.CollectionKey.Split('#')[1]);
                string zone = dr.Zone ?? "";
                if (CollectionNo > 0 && installationNo > 0 && zone.ToUpper() != "UNDECLARED")
                {
                    var History = new Views.BatchHistoryBreakdown(CollectionNo, installationNo, ExchangeConst, (List<Transport.AllEvents>)dgEvents.ItemsSource, _batch.GetCollectionData(CollectionNo));
                    History.Owner = GetWindow(this);
                    History.ShowDialogEx(this);
                }
            }
            catch (Exception ex)
            {
                LogError("btnDetails_Click", ex);

            }
            finally
            {
                btnDetails.IsEnabled = true;
            }
        }
        //
        //void listBatchBreakdown_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgBatchBreakdown.Sort(headerClicked);
        //}
        //
        //void listEvents_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgEvents.Sort(headerClicked);
        //}
        //
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //
        private void btnWinLossReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                IReports objReports = (isCommonCDOforDeclaration && !string.IsNullOrEmpty(ExchangeConst)) ? ReportsBusinessObject.CreateInstance(ExchangeConst, TicketConst) : ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                BatchHistoryListView dr = (BatchHistoryListView)dgBatchBreakdown.SelectedItem;
                _Undeclared = (dr.Zone ?? "") == "UNDECLARED" ? true : false;//checking whether Zone is Undeclared and setting the _Undeclared 
                var oReports = new CollectionBatchDetailReports(_BatchID, _WeekID, _SiteConfig, Window.GetWindow(this), _ChkWeek,_Undeclared);
                oReports.ShowDialogEx(this);
            }
            catch (Exception ex)
            {
                LogError("btnWinLossReport_Click", ex); 
            }
        }
        //
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
                        //((BMC.Presentation.POS.BatchBreakdown)(this)).Loaded -= (this.Window_Loaded);
                        this.btnExit.Click -= (this.btn_Exit);
                        this.dgBatchBreakdown.SelectionChanged -= (this.dgBatchBreakdown_SelectionChanged);
                        this.btnReturn.Click -= (this.btnReturn_Click);
                        this.btnExport.Click -= (this.btnExport_Click);
                        this.btnDetails.Click -= (this.btnDetails_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("BatchBreakdown objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="BatchBreakdown"/> is reclaimed by garbage collection.
        /// </summary>
        ~BatchBreakdown()
        {
            Dispose(false);
        }

        #endregion

    }

    public class BatchBreakDownParams
    {
        public int _BatchID { get; set; }
        public int _UniqueId { get; set; }
        public Batch _BatchBreakDown { get; set; }
        public int _MaxThreads { get; set; }
        public int _WeekId { get; set; }
    }
}
