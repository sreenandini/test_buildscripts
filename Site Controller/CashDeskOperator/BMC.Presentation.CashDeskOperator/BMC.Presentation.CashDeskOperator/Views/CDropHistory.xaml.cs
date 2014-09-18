using System;
using System.Windows;
using System.Windows.Controls;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;
using System.Data;
using System.Collections.Generic;
using BMC.CashDeskOperator;
using BMC.Security;
using BMC.DBInterface.CashDeskOperator;
using BMC.Presentation.POS;
using BMC.Common.ExceptionManagement;
using System.Linq;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Views;
using BMC.Common.Utilities;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CDropHistory.xaml
    /// </summary>
    public partial class CDropHistory : IDisposable
    {
        private bool disposed = false;
        List<SiteConfig> _siteconfig = new List<SiteConfig>();
        CollectionHelper _collectionHelper;
        private bool isExceptionOccured = false;
        private bool isCommonCDO = false;
        private bool Restricted = false;
        int iWeekCount = 0;
        public CDropHistory()
        {
            try
            {
                InitializeComponent();
                cboWeek.Visibility = Visibility.Hidden;
                iWeekCount = Convert.ToInt32(cboWeek.Text);
                _collectionHelper = new CollectionHelper();
                if (Login._siteconfig != null && Login._siteconfig.Count > 0)
                {
                    this.cboSiteCode.SelectionChanged -= cboSiteCode_SelectionChanged;
                    cboSiteCode.ItemsSource = Login._siteconfig;
                    this.cboSiteCode.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cboSiteCode_SelectionChanged);
                    cboSiteCode.Visibility = Visibility.Visible;
                    cboSiteCode.SelectedItem = Login._siteconfig.Find(x => x.SiteCode == Settings.SiteCode);                    
                    isCommonCDO = true;
                }
                else
                {
                    cboSiteCode.Visibility = Visibility.Collapsed;
                    isCommonCDO = false;
                }
                chkFullCount.IsChecked = true;
                this.chkBatch.Checked -= new System.Windows.RoutedEventHandler(this.chkBatch_Checked);
                chkLast20.IsChecked = true;
                chkBatch.IsChecked = true;
                this.chkBatch.Checked += new System.Windows.RoutedEventHandler(this.chkBatch_Checked);

                if ((!Settings.CentralizedDeclaration) && Security.SecurityHelper.HasAccess("BMC.Presentation.CPerformDrop.PartCollectionDrop") && (Settings.IsPartCollectionEnabled))
                {
                    chkPartCount.Visibility = Visibility.Visible;
                }
                else
                {
                    chkPartCount.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.ShowBox("MessageID425", BMC_Icon.Error);
                BMC.Common.LogManagement.LogManager.WriteLog("Error Invoking CDropHistory", Ex.Message, BMC.Common.LogManagement.LogManager.enumLogLevel.Error);
                isExceptionOccured = true;
                dgFullWeekHistory.ItemsSource = null;
                dgFullBatchHistory.ItemsSource = null;
            }
        }
        private SiteConfig GetSiteItem(string sitecode)
        {
            try
            {
                return Login._siteconfig.Find(m => m.SiteCode == sitecode);
            }
            catch (Exception ex) { return null; }
        }

        //void lstFullBatchHistory_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgFullBatchHistory.Sort(headerClicked);
        //}

        //void lstFullWeekHistory_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgFullWeekHistory.Sort(headerClicked);
        //}

        //void lstPartCountHistory_GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        //{
        //    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        //    dgPartCountHistory.Sort(headerClicked);
        //}

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnDetails.IsEnabled = false;
                bool ChkWeek = (bool)chkWeek.IsChecked;
                if (((bool)chkWeek.IsChecked && dgFullWeekHistory.SelectedIndex >= 0) || ((bool)chkBatch.IsChecked && dgFullBatchHistory.SelectedIndex >= 0))
                {
                    BatchBreakdown bd =
                        new POS.BatchBreakdown(((bool)chkBatch.IsChecked && dgFullBatchHistory.SelectedIndex >= 0) ? ((FullBatchCollectionData)dgFullBatchHistory.SelectedItem).Number : 0,
                            ((bool)chkWeek.IsChecked && dgFullWeekHistory.SelectedIndex >= 0) ? ((FullWeekCollectionData)dgFullWeekHistory.SelectedItem).WeekId : 0,
                            ((bool)chkWeek.IsChecked && dgFullWeekHistory.SelectedIndex >= 0) ? ((FullWeekCollectionData)dgFullWeekHistory.SelectedItem).WeekNumber : 0,
                            cboSiteCode.SelectedItem as SiteConfig, ChkWeek);

                    try { bd.Owner = MessageBox.parentOwner; }
                    catch (Exception ex)
                    {
                        BMC.Common.LogManagement.LogManager.WriteLog("Error Invoking btnDetails_Click", ex.Message + "\r\n" + ex.StackTrace, BMC.Common.LogManagement.LogManager.enumLogLevel.Error);
                    }
                    bd.ShowDialog();
                }                
            }
            catch (Exception ex)
            {

                BMC.Common.LogManagement.LogManager.WriteLog(ex.Message, BMC.Common.LogManagement.LogManager.enumLogLevel.Error);
            }
            finally
            {
                btnDetails.IsEnabled = true;
            }

        }



        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                    dgPartCountHistory.DataContext = null;
                    dgFullWeekHistory.DataContext = null;
                    dgFullBatchHistory.DataContext = null;
                }
            }
            disposed = true;
        }

        ~CDropHistory()
        {
            Dispose(false);
        }
        #endregion

        private void cboSiteCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                isExceptionOccured = false;
                if (!CollectionHelper.IsServerConnected((cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString))
                {
                    SetDefaultSiteCode(false, e);
                    return;
                }
                _collectionHelper = new CollectionHelper((cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString);

                if (!_collectionHelper.IsAuthorized(SecurityHelper.CurrentUser.SecurityUserID, "BMC.Presentation.CommonCDOforDeclaration"))
                {
                    SetDefaultSiteCode(true, e);
                    return;
                }

                if (Convert.ToBoolean(chkBatch.IsChecked))
                {
                    dgFullBatchHistory.ItemsSource = _collectionHelper.GetCollectionBatchData(Restricted);
                }
                if (Convert.ToBoolean(chkWeek.IsChecked))
                {
                    dgFullWeekHistory.ItemsSource = _collectionHelper.GetCollectionWeekData(iWeekCount);
                }
                if (Convert.ToBoolean(chkPartCount.IsChecked))
                {
                    ShowPartCollectionHistory();
                }
            }
            catch (Exception Ex)
            {
                BMC.Common.LogManagement.LogManager.WriteLog("Error Invoking CDropHistory::cboSiteCode_SelectionChanged", Ex.Message, BMC.Common.LogManagement.LogManager.enumLogLevel.Error);
                isExceptionOccured = true;
                dgFullWeekHistory.ItemsSource = null;
                dgFullBatchHistory.ItemsSource = null;
            }
        }

        private void SetDefaultSiteCode(bool bUserAuthenticationFailed, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
            {
                MessageBox.ShowBox(bUserAuthenticationFailed ? "MessageID405" : "MessageID404", BMC_Icon.Warning,
                    (e.AddedItems[0] as SiteConfig).SiteCode);
                e.Handled = false;
                cboSiteCode.SelectedItem = Login._siteconfig.Find(x => x.SiteCode == Settings.SiteCode);                
            }
        }

        private void chkWeek_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isExceptionOccured)
                {
                    chkLast20.Visibility = Visibility.Hidden;
                    cboWeek.Visibility = Visibility.Visible;                   
                    cboWeek.SelectedIndex =6 ;
                    dgFullWeekHistory.ItemsSource = _collectionHelper.GetCollectionWeekData(iWeekCount);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkBatch_Checked(object sender, RoutedEventArgs e)
        {
            chkLast20.Visibility = Visibility.Visible;
            cboWeek.Visibility = Visibility.Hidden;
            if (!isExceptionOccured)
            {
                dgFullBatchHistory.ItemsSource = _collectionHelper.GetCollectionBatchData(Restricted);
            }
        }

        private void chkLast20_Checked(object sender, RoutedEventArgs e)
        {
            Restricted = true;
            if (!isExceptionOccured)
            {
                dgFullBatchHistory.ItemsSource = _collectionHelper.GetCollectionBatchData(true);
                if (chkPartCount.Visibility == Visibility.Visible)
                {
                    ShowPartCollectionHistory();            
                }
            }
        }

        private void chkLast20_Unchecked(object sender, RoutedEventArgs e)
        {
            Restricted = false;
            if (!isExceptionOccured)
            {
                dgFullBatchHistory.ItemsSource = _collectionHelper.GetCollectionBatchData(false);
                if (chkPartCount.Visibility == Visibility.Visible)
                {
                    ShowPartCollectionHistory();
                }
            }
        }   
     
        private void chkPartCount_Checked(object sender, RoutedEventArgs e)
        {
            ShowPartCollectionHistory();            
        }

        private void ShowPartCollectionHistory()
        {
            try
            {
                chkLast20.Visibility = Visibility.Visible;
                cboWeek.Visibility = Visibility.Hidden;
                if (chkPartCount.IsChecked == true)
                    btnDetails.Visibility = Visibility.Collapsed;
                List<PartBatchCollectionData> lstGridData = _collectionHelper.GetPartCollectionBatchData(chkLast20.IsChecked.Value);
                dgPartCountHistory.ItemsSource = lstGridData;                
                
                List<PartBatchCollectionData> Calc = _collectionHelper.GetPartCollectionBatchData(false); //Since total should not depend on selection records
                decimal dTodayCashSum = Calc.Where(d => (d.DateTime.Date == DateTime.Today.Date)).Sum(x => x.dCash);
                int TodayQuantity = Calc.Where(d => (d.DateTime.Date == DateTime.Today.Date)).Count();
                decimal dCashSum = Calc.Sum(x => x.dCash);
                int TotalQuantity = Calc.Count;

                txtTodayTotal.Text = Convert.ToString(dTodayCashSum.GetUniversalCurrencyFormatWithSymbol());
                txtTodayTotalQty.Text = Convert.ToString(TodayQuantity);

                txtTotalCash.Text = Convert.ToString(dCashSum.GetUniversalCurrencyFormatWithSymbol());
                txtQuantity.Text = Convert.ToString(TotalQuantity);

                if (lstGridData.Count > 0)
                {
                    btnPartCollReport.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkPartCount_UnChecked(object sender, RoutedEventArgs e)
        {
            btnDetails.Visibility = Visibility.Visible;            
        }

        private void btnPartCollReport_Click(object sender, RoutedEventArgs e)
        {
            int NoofRecords = 0;
            if(chkLast20.IsChecked == true)
            {
                NoofRecords = 20;
            }
            else
            {
                NoofRecords = 2000;
            }
            CreatePartCollectionReport(NoofRecords);
        }
        private void CreatePartCollectionReport(int Noofrecords)
        {
            try
            {
                IReports objReports = null;
                DataSet dsPartCollDetails = null;
                if (isCommonCDO)
                {
                    if (!CollectionHelper.IsServerConnected((cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString))
                    {
                        return;
                    }
                    if (!_collectionHelper.IsAuthorized(SecurityHelper.CurrentUser.SecurityUserID, "BMC.Presentation.CommonCDOforDeclaration"))
                    {
                        return;
                    }
                    objReports = ReportsBusinessObject.CreateInstance((cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString, (cboSiteCode.SelectedItem as SiteConfig).TicketConnectionString);
                    
                }
                else
                {
                    objReports = ReportsBusinessObject.CreateInstance(); 
                }

                dsPartCollDetails = objReports.GetPartCollectionDetails(Noofrecords);
                if (dsPartCollDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }
                if (isCommonCDO)
                {
                    using (CReportViewer cReportViewer = new CReportViewer((cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString, (cboSiteCode.SelectedItem as SiteConfig).TicketConnectionString))
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                        cReportViewer.ShowPartCollection(Noofrecords, (cboSiteCode.SelectedItem as SiteConfig).ExchangeConnectionString, (cboSiteCode.SelectedItem as SiteConfig).TicketConnectionString);
                        cReportViewer.SetOwner(Window.GetWindow(this));
                        cReportViewer.Show();
                    }
                }
                else
                {
                    using (CReportViewer cReportViewer = new CReportViewer())
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                        cReportViewer.ShowPartCollection(Noofrecords);
                        cReportViewer.SetOwner(Window.GetWindow(this));
                        cReportViewer.Show();
                    }
                }

                LogManager.WriteLog("Show Part Collection Report Successfull", LogManager.enumLogLevel.Info);
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private T GetValue<T>(Object objValue)
        {
            TypeCode tpCode = Type.GetTypeCode(typeof(T));
            try
            {
                switch (tpCode)
                {
                    case TypeCode.Int32:
                        if (objValue != null)
                            return (T)(object)Convert.ToInt32(objValue);
                        else return (T)(object)0;
                    case TypeCode.String:
                        if (objValue != null)
                            return (T)(object)objValue.ToString();
                        else return (T)(object)"";
                }
                return (T)(object)0;
            }
            catch
            {
                switch (tpCode)
                {
                    case TypeCode.Int32: return (T)(object)0;
                    case TypeCode.String: return (T)(object)"";
                }
                return (T)(object)0;
            }
        }
        private T GetStaticComboValue<T>(ComboBox cmbCombo)
        {
            TypeCode tpCode = Type.GetTypeCode(typeof(T));
            try
            {
                ComboBoxItem ci = (ComboBoxItem)cmbCombo.SelectedValue;
                return GetValue<T>(ci.Content);
            }
            catch
            {
                return (T)(object)0;
            }
        }

        private void cboWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                iWeekCount = GetStaticComboValue<Int32>(cboWeek);
                dgFullWeekHistory.ItemsSource = _collectionHelper.GetCollectionWeekData(iWeekCount);
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
            
        }
    }
}
