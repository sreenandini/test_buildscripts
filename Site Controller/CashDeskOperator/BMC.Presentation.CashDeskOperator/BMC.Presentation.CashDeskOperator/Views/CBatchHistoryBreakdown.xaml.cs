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
using System.Windows.Shapes;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.Utilities;
using System.Configuration;
using System.ComponentModel;
using BMC.Common.ExceptionManagement;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for BatchHistoryBreakdown.xaml
    /// </summary>
    public partial class BatchHistoryBreakdown : Window, IDisposable
    {
        Batch batch;
        int _CollectionID;
        int _InstallationNo;
        string ExchangeConnection;
        List<Transport.AllEvents> _lstEvents;
        BMC.Transport.CollectionView _collectionview;
        //
        public BatchHistoryBreakdown(int CollectionID, int InstallationNo, string ExchangeConst, List<Transport.AllEvents> events, BMC.Transport.CollectionView collection)
        {
            InitializeComponent();
            _CollectionID = CollectionID;
            _InstallationNo = InstallationNo;
            _collectionview = collection;
            _lstEvents = events;
            ExchangeConnection = ExchangeConst;
            txtBatchDate.Content = _collectionview.Collection_Date_Performed.GetUniversalDateTimeFormat();
            txtGame.Content = ((_collectionview.MachineName).Length > 15) ? (_collectionview.MachineName).Substring(0, 15) : _collectionview.MachineName;
            txtGame.ToolTip = _collectionview.MachineName;
            txtGamingDay.Content = _collectionview.CollectionDate.GetUniversalDateTimeFormat();
            txtPos.Content = _collectionview.PosName;
            txtUser.Content = _collectionview.UserName.Split(',').First();
            txtUser.ToolTip = _collectionview.UserName;
            LogManager.WriteLog("Declared User Name : " + _collectionview.DeclaredUserName, LogManager.enumLogLevel.Info);
            txtDeclareBy.Content = _collectionview.DeclaredUserName;
            txtDeclareBy.ToolTip = _collectionview.DeclaredUserName;
            txtZone.Content = _collectionview.ZoneName;
            txtAsset.Content = _collectionview.Stock_No;
            ChangeHeader();
            if (!Settings.IsAFTEnabledForSite ? true : false)
            {
                RemoveHeader("EFTIn");
                RemoveHeader("EFTOut");
                RemoveHeader("EFT");
            }
            MessageBox.childOwner = this;
            dgEvents.ItemsSource = _lstEvents;
            BackgroundWorker _bgLoadPositionScreen = new BackgroundWorker();
            _bgLoadPositionScreen.DoWork += OnPositionScreenInitialize;
            _bgLoadPositionScreen.RunWorkerCompleted += OnPositionScreenComplete;
            _bgLoadPositionScreen.RunWorkerAsync();
            _bgLoadPositionScreen.WorkerSupportsCancellation = true;            
        }
        //
        void OnPositionScreenInitialize(object sender, DoWorkEventArgs e)
        {
            try
            {
                LogInfo("START Loading Batch Break Down Screen");
            }
            catch (Exception ex) { LogError("OnPositionScreenInitialize", ex); }
        }
        //
        void OnPositionScreenComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                List<TreasuryUser> Treasury = null;
                batch = new Batch(_CollectionID, _InstallationNo, 0, ExchangeConnection, 0);
                dgCashCollected.ItemsSource = batch.GetCollectionUser(_collectionview);
                dgCashBreakdown.ItemsSource = batch.GetCollectionDetailsforListView(_collectionview);
                Treasury = batch.GetTreasuryTable(_collectionview);
                if (Treasury != null && Treasury.Count > 0)
                    dgTresuryEntries.ItemsSource = Treasury;               
            }
            catch (Exception ex) { LogError("OnPositionScreenComplete", ex); }
        }
        //
        private void LogError(string methodName, Exception ex)
        {
            LogManager.WriteLog("BatchHistoryBreakdown Error in " + methodName + " : " + ex.Message, LogManager.enumLogLevel.Error);
            ExceptionManager.Publish(ex);
        }
        //
        private void LogInfo(string content)
        {
            LogManager.WriteLog("BatchHistoryBreakdown Info - " + content + " : " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff tt"), LogManager.enumLogLevel.Info);
        }

        //private void ChangeHeaderOld()
        //{
        //    try
        //    {
        //        GridView GvBreakDown;
        //        GvBreakDown = (GridView)dgCashBreakdown.View;
        //        int columnCount = GvBreakDown.Columns.Count;

        //        for (int i = columnCount-1; i >= 0 ; i--)
        //        {
        //            int currency;
        //            var k = int.TryParse(((GridViewColumnHeader)CashGridView.Columns[i].Header).Content.ToString(), out currency);
        //            if (k)
        //                if(!DenomVisibility(currency.ToString()))
        //                    CashGridView.Columns.Remove(CashGridView.Columns[i]);
        //                else
        //                ((GridViewColumnHeader)CashGridView.Columns[i].Header).Content = ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol() + " " + ((GridViewColumnHeader)CashGridView.Columns[i].Header).Content;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("ChangeHeader:" + ex.Message, LogManager.enumLogLevel.Error);
        //    }
        //}


        private void ChangeHeader()
        {
            try
            {
                int columnCount = dgCashBreakdown.Columns.Count;

                for (int i = columnCount - 1; i >= 0; i--)
                {
                    int currency;
                    var k = int.TryParse((dgCashBreakdown.Columns[i].Header).ToString(), out currency);
                    if (k)
                        if (!DenomVisibility(currency.ToString()))
                            dgCashBreakdown.Columns.Remove(dgCashBreakdown.Columns[i]);
                        else
                            dgCashBreakdown.Columns[i].Header = ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol() + " " + (dgCashBreakdown.Columns[i].Header.ToString());
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ChangeHeader:" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void chkVar_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                dgCashBreakdown.Visibility = Visibility.Hidden;
                dgVarianceHistory.Visibility = Visibility.Visible;
                lblRecords.Visibility = Visibility.Visible;
                cmbLast.Visibility = Visibility.Visible;
                dgVarianceHistory.ItemsSource = batch.GetAssetVarianceHistory(_InstallationNo, GetRecordsComboValue());
            }
            catch (Exception ex)
            {

                LogManager.WriteLog("chkVar_Checked::" + ex.Message, LogManager.enumLogLevel.Error);
            }

        }

        private void chkBreakdown_Checked(object sender, RoutedEventArgs e)
        {
            if (dgCashBreakdown == null) return;
            dgCashBreakdown.Visibility = Visibility.Visible;
            dgVarianceHistory.Visibility = Visibility.Hidden;
            lblRecords.Visibility = Visibility.Hidden;
            cmbLast.Visibility = Visibility.Hidden;

        }

        private void btn_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbLast_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                dgCashBreakdown.Visibility = Visibility.Hidden;
                dgVarianceHistory.Visibility = Visibility.Visible;
                lblRecords.Visibility = Visibility.Visible;
                cmbLast.Visibility = Visibility.Visible;
                dgVarianceHistory.ItemsSource = batch.GetAssetVarianceHistory(_InstallationNo, GetRecordsComboValue());
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("cmbLast_SelectionChanged::" + ex.Message, LogManager.enumLogLevel.Error);

            }
        }

        //void lstVarianceHistory_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgVarianceHistory.Sort(headerClicked);
        //}

        //void lstTresuryEntries_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgTresuryEntries.Sort(headerClicked);
        //}

        //void dgCashBreakdown_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgCashBreakdown.Sort(headerClicked);
        //}

        //void lstEvents_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgEvents.Sort(headerClicked);
        //}

        //void lstCashCollected_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    lstCashCollected.Sort(headerClicked);
        //}

        private static bool DenomVisibility(string denom)
        {


            var listUS = new List<string> { "1", "5", "10", "20", "50", "100" };
            var listItaly = new List<string> { "5", "10", "20", "50", "100", "200", "500" };
            var lsOther = new List<string> { "1", "2", "5", "10", "20", "50", "100", "200", "500" };
            var lisUK = new List<string> { "5", "10", "20", "50" };
            var lstAR = new List<string> {"2", "5", "10", "20", "50", "100"};

            //bool returnValue;

            switch (Settings.Region.ToUpper())
            {
                case "US":
                    return listUS.Contains(denom);
                case "IT":
                    return listItaly.Contains(denom);
                case "AR":
                    return lstAR.Contains(denom);
                case "UK":
                    return lisUK.Contains(denom);
                default:
                    return lsOther.Contains(denom);
            }

            //if (Common.Utilities.ExtensionMethods.CurrentSiteCulture.ToUpper().Contains("US"))
            //if (Common.Utilities.ExtensionMethods.CurrentSiteCulture.ToUpper().Contains("IT"))
            //    returnValue = 
            //else
            //    returnValue = 
            //return returnValue;
        }

        private int GetRecordsComboValue()
        {
            try
            {
                if ((((ComboBoxItem)cmbLast.SelectedItem).Content.ToString().ToUpper()) != "ALL")
                {
                    return Convert.ToInt16(((ComboBoxItem)cmbLast.SelectedItem).Content);
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetRecordsComboValue::" + ex.Message, LogManager.enumLogLevel.Error);
                return 0;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void RemoveHeaderOld(string sHeaderName)
        //{
        //    try
        //    {
        //        GridView GvBreakDown;
        //        GvBreakDown = (GridView)dgCashBreakdown.View;
        //        int columnCount = GvBreakDown.Columns.Count;

        //        for (int i = columnCount - 1; i >= 0; i--)
        //        {
        //            if (sHeaderName.ToUpper() == Convert.ToString(((GridViewColumnHeader)CashGridView.Columns[i].Header).Tag).ToUpper())
        //            {
        //                CashGridView.Columns.Remove(CashGridView.Columns[i]);
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("RemoveHeader:" + ex.Message, LogManager.enumLogLevel.Error);
        //    }
        //}

        private void RemoveHeader(string sHeaderName)
        {
            try
            {
                int columnCount = dgCashBreakdown.Columns.Count;

                for (int i = columnCount - 1; i >= 0; i--)
                {
                    if (sHeaderName.ToUpper() == dgCashBreakdown.Columns[i].Header.ToString().ToUpper())
                    {
                        dgCashBreakdown.Columns.Remove(dgCashBreakdown.Columns[i]);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("RemoveHeader:" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }


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
                        this.chkBreakdown.Checked -= (this.chkBreakdown_Checked);
                        this.chkVar.Checked -= (this.chkVar_Checked);
                        this.cmbLast.SelectionChanged -= (this.cmbLast_SelectionChanged);
                        this.btnClose.Click -= (this.btnClose_Click);
                        this.btnExit.Click -= (this.btnExit_Click);                      
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("BatchHistoryBreakdown objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="BatchHistoryBreakdown"/> is reclaimed by garbage collection.
        /// </summary>
        ~BatchHistoryBreakdown()
        {
            Dispose(false);
        }

        #endregion
    }
}
