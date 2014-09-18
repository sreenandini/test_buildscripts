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
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;
using System.Data;
using System.ComponentModel;
using BMC.DBInterface.CashDeskOperator;
using BMC.Business.CashDeskOperator;
using BMC.Common.LogManagement;
using System.Collections;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for UCAnalysisDetails.xaml
    /// </summary>
    public partial class UCAnalysisDetails : IDisposable
    {
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DataTable dtAnalysisDetails;
        private int _iWeekID = 0;
        private int _iType;
        public string HeaderText { get; set; }
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private DataTable dtPrintDetails = null;
        private Decimal TotalAvgBet = 0;
        private DateTime _dteStartDate;
        private DateTime _DteEndDate;

        private AnalysisView _viewType = AnalysisView.Position;
        private int _zoneId = -1;



        public bool RefreshVisibility
        {
            get { return (bool)GetValue(RefreshVisibilityProperty); }
            set
            {
                SetValue(RefreshVisibilityProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for RefreshVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RefreshVisibilityProperty =
            DependencyProperty.Register("RefreshVisibility", typeof(bool), typeof(UCAnalysisDetails), new UIPropertyMetadata(true));


        public UCAnalysisDetails()
        {
            InitializeComponent();
        }

        public string Caption { get; set; }

        public AnalysisDetailsParent ParentElement { get; set; }

        public void Initialize(AnalysisDetailsParent parent, string caption, int iType, AnalysisView viewType, int zoneId, DateTime dteStartDate, DateTime DteEndDate, int iWeekID)
        {
            try
            {
                this.ParentElement = parent;
                this.Caption = caption;

                _dteStartDate = dteStartDate;
                _DteEndDate = DteEndDate;
                _viewType = viewType;
                _zoneId = zoneId;
                btnRefresh.Visibility = (this.RefreshVisibility ? Visibility.Visible : Visibility.Collapsed);
                LogManager.WriteLog("UCAnalysisDetails:Initialize Started", LogManager.enumLogLevel.Info);
                this.UpdateColumnCaptions();
                LoadFilterData();
                if (iType == -99)
                {
                    //lblStatus.Visibility = Visibility.Visible;
                    //progressBar1.Visibility = Visibility.Visible;
                    //txtHeader.Text = "Drop Week BreakDown";
                    //txtHeader1.Visibility = Visibility.Visible;
                    //txtHeader.Visibility = Visibility.Hidden;
                    btnPrint.Visibility = Visibility.Hidden;
                    btnExport.Visibility = Visibility.Hidden;

                    _iType = -99;
                    _iWeekID = iWeekID;

                    backgroundWorker1 = new BackgroundWorker();
                    backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
                    backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
                }
                else
                {
                    Refresh(iType, _viewType, _zoneId, dteStartDate, DteEndDate);
                }
                //lstAnalysisDetails.ClipboardCopyMode = (Microsoft.Windows.Controls.DataGridClipboardCopyMode)DataGridClipboardCopyMode.IncludeHeader;
                GetRefreshedData();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadFilterData()
        {
            LogManager.WriteLog("UCAnalysisDetails:GetActiveZones", LogManager.enumLogLevel.Info);
            DataTable dt_zones = new CommonDataAccess().GetActiveZones();
            DataRow drMc = dt_zones.NewRow();
            drMc["Zone_No"] = -1;
            drMc["Zone_Name"] = "-- All Machines --";
            dt_zones.Rows.InsertAt(drMc, 0);
            if (dt_zones.Rows.Count > 0)
            {
                dt_zones.DefaultView.Sort = "Zone_Name";
                cmbFilter.ItemsSource = ((System.ComponentModel.IListSource)dt_zones).GetList();
            }
            cmbFilter.DataContext = dt_zones;
            cmbFilter.DisplayMemberPath = "Zone_Name";
            cmbFilter.SelectedValuePath = "Zone_No";
            cmbFilter.SelectedIndex = 0;
            cmbView.Items.Add("Position");
            cmbView.Items.Add("Zone");
            cmbView.SelectedIndex = 0;
        }
        public void GetRefreshedData()
        {
            try
            {
                if (_iType == -99)
                {
                    lstAnalysisDetails.Visibility = Visibility.Hidden;
                    lstCollectionDetails.Visibility = Visibility.Visible;
                    // progressBar1.IsIndeterminate = true;
                    this.Cursor = Cursors.Wait;
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ResizeGridViewColumn(Microsoft.Windows.Controls.DataGridColumn column)
        {
            if (double.IsNaN(column.Width.DisplayValue))
            {
                column.Width = new Microsoft.Windows.Controls.DataGridLength(column.ActualWidth);
            }
            //  column.Width = new Microsoft.Windows.Controls.DataGridLength(double.NaN);
        }

        private void UpdateColumnCaptions()
        {
            switch (this.ParentElement)
            {
                case AnalysisDetailsParent.ReportDropDetails:
                case AnalysisDetailsParent.SiteInterrogration:
                    {
                        lstAnalysisDetails.Columns[6].Header = this.FindResource("CAanalysisDetails_xaml_GridViewColumnHeader_GMU_Server");
                        lstAnalysisDetails.Columns[7].Header = this.FindResource("CAanalysisDetails_xaml_GridViewColumnHeader_GMU_Machine");

                    }
                    break;
                default:
                    break;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //System.Threading.Thread.Sleep(10000);
                dtAnalysisDetails = GetCollectionDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private DataTable GetCollectionDetails()
        {
            DataTable dtColl = new DataTable();
            try
            {
                IAnalysis objCashdesk = AnalysisBusinessObject.CreateInstance();
                dtColl = objCashdesk.GetWeeklyCollectionDetails(_iWeekID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtColl;
        }



        private void SortClick(object sender, RoutedEventArgs e)
        {
            try
            {
                GridViewColumnHeader column = sender as GridViewColumnHeader;
                String field = column.Tag as String;
                DataView dv = (DataView)lstAnalysisDetails.DataContext;
                if (_CurSortCol != null)
                {
                    AdornerLayer.GetAdornerLayer(_CurSortCol).Remove(_CurAdorner);
                    lstAnalysisDetails.Items.SortDescriptions.Clear();
                }
                ListSortDirection newDir = ListSortDirection.Ascending;
                if (_CurSortCol == column && _CurAdorner.Direction == newDir)
                    newDir = ListSortDirection.Descending;
                dv.Sort = "SortColumn desc, " + field + (newDir.Equals(ListSortDirection.Ascending) ? "  asc" : " desc");
                lstAnalysisDetails.DataContext = dv;
                _CurSortCol = column;
                _CurAdorner = new SortAdorner(_CurSortCol, newDir);
                AdornerLayer.GetAdornerLayer(_CurSortCol).Add(_CurAdorner);
                //lstAnalysisDetails.Items.SortDescriptions.Add(new SortDescription(field, newDir));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // First, handle the case where an exception was thrown.
                if (e.Error != null)
                {
                    MessageBox.ShowBox(e.Error.Message, BMC_Icon.Error);
                }
                else if (e.Cancelled)
                {
                    MessageBox.ShowBox("MessageID153", BMC_Icon.Error);
                }
                else
                {
                    lstCollectionDetails.DataContext = dtAnalysisDetails.DefaultView;
                    foreach (Microsoft.Windows.Controls.DataGridColumn gvCol in lstCollectionDetails.Columns)
                        ResizeGridViewColumn(gvCol);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                //lblStatus.Visibility = Visibility.Hidden;
                //progressBar1.Visibility = Visibility.Hidden;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintOrExportDetails(true);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //PrintOrExportDetails(false);
            try
            {
                PrintOrExportDetails(false);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void PrintOrExportDetails(bool Print)
        {
            try
            {
                string sType = string.Empty;
                if (ParentElement != AnalysisDetailsParent.SiteInterrogration)
                {
                    switch (_iType)
                    {
                        case 1: sType = "(Day)"; break;
                        case 2: sType = "(Drop)"; break;
                        case 3: sType = "(Week)"; break;
                        case 4: sType = "(Month)"; break;
                        default: sType = ""; break;
                    }
                }

                string sReportName = this.Caption.Trim() + sType;

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    if (dtPrintDetails.Rows.Count > 0)
                    {
                        string sAnalysisDetails = string.Empty;
                        lstAnalysisDetails.SelectAllCells();
                        lstAnalysisDetails.ClipboardCopyMode = (Microsoft.Windows.Controls.DataGridClipboardCopyMode)DataGridClipboardCopyMode.IncludeHeader;
                        ApplicationCommands.Copy.Execute(null, lstAnalysisDetails);
                        lstAnalysisDetails.UnselectAllCells();
                        try
                        {
                            sAnalysisDetails = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                            Clipboard.Clear();
                        }
                        catch
                        {
                            MessageBox.ShowBox("MessageID56");
                        }

                        cReportViewer.ShowReportsAnalysisDetails(dtPrintDetails, sReportName, Print, TotalAvgBet, sAnalysisDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
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

                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_2)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_4)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_5)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_6)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_7)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_8)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_9)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_10)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_11)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_12)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_13)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_14)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_15)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_16)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_17)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_18)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_19)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_20)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_21)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_22)).Click -= (this.SortClick);
                        ////((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_23)).Click -= (this.SortClick);
                        ////((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_24)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_25)).Click -= (this.SortClick);
                        ////((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_26)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_27)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_28)).Click -= (this.SortClick);

                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("UCAnalysisDetails objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="UCAnalysisDetails"/> is reclaimed by garbage collection.
        /// </summary>
        ~UCAnalysisDetails()
        {
            Dispose(false);
        }

        #endregion

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            AnalysisView viewType = (cmbView.SelectedIndex == 0 ? AnalysisView.Position : AnalysisView.Zone);
            Refresh(_iType, viewType, (int)cmbFilter.SelectedValue, _dteStartDate, _DteEndDate);
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnalysisView viewType = (cmbView.SelectedIndex == 0 ? AnalysisView.Position : AnalysisView.Zone);
                int zoneId = (int)cmbFilter.SelectedValue;
                LogManager.WriteLog("UCSiteIntegorration:btnGo_Click ZoneID:" + zoneId + " Type:" + viewType.ToString(), LogManager.enumLogLevel.Info);
                Refresh(viewType, zoneId);
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("Unable to Refresh Site Interrogation details", BMC_Icon.Information, true);
                ExceptionManager.Publish(ex);
            }
        }

        public void Refresh(AnalysisView viewType, int zoneId)
        {
            Refresh(_iType, viewType, zoneId, _dteStartDate, _DteEndDate);
        }

        public void Refresh(int iType, AnalysisView viewType, int zoneId, DateTime dteStartDate, DateTime DteEndDate)
        {
            try
            {
                _iType = iType;
                //lblStatus.Visibility = Visibility.Hidden;
                //progressBar1.Visibility = Visibility.Hidden;
                lstAnalysisDetails.Visibility = Visibility.Visible;
                lstCollectionDetails.Visibility = Visibility.Hidden;
                //GridViewColumn_NoOfMC.Width = (viewType == AnalysisView.Zone) ? 78 : 0;
                string reportType = string.Empty;
                switch (iType.ToString())
                {
                    case "1":
                        reportType = "DAY";
                        break;
                    case "2":
                        reportType = "DROP";
                        break;
                    case "3":
                        reportType = "WEEK";
                        break;
                    case "4":
                        reportType = "MONTH";
                        break;
                }
                LogManager.WriteLog("UCAnalysisDetails:Refresh ZoneID:" + zoneId + " ReportType:" + reportType, LogManager.enumLogLevel.Info);
                Action doAnalysis = () =>
                {
                    IAnalysis objCashdesk = AnalysisBusinessObject.CreateInstance();
                    dtAnalysisDetails = objCashdesk.GetAnalysisDetails(iType, dteStartDate, DteEndDate, viewType, zoneId);
                };


                if (iType == 2)
                {
                    //Getting Installation No for grabbing current meters
                    IList<int> inst = (new CommonDataAccess()).GetInstallationDetailsForReports(reportType)
                         .AsEnumerable()
                         .Where(r => (zoneId == -1 ? true : (r["Zone_No"] != DBNull.Value ? (r.Field<int>("Zone_No") == zoneId) : false)))
                         .Select(row => row.Field<int>("Installation_No"))
                         .ToList<int>();

                    //Grabbing current meters from VLT
                    LoadingWindow ld_analysis = new LoadingWindow(this, Audit.Transport.ModuleName.AnalysisDetails, inst, doAnalysis);

                    ld_analysis.ShowDialog();
                }
                else
                {

                    LoadingWindow ld_analysis = new LoadingWindow(this, Audit.Transport.ModuleName.AnalysisDetails, doAnalysis, true);

                    ld_analysis.ShowDialog();
                }

                dtPrintDetails = null;
                dtPrintDetails = dtAnalysisDetails.Copy();
                if (dtPrintDetails.Rows.Count > 0)
                {
                    TotalAvgBet = Convert.ToDecimal(dtPrintDetails.Rows[0]["AvgBet"]);
                    dtPrintDetails.Rows[0].Delete();
                    dtPrintDetails.AcceptChanges();
                }
                lstAnalysisDetails.DataContext = dtAnalysisDetails.DefaultView;
                foreach (Microsoft.Windows.Controls.DataGridColumn gvCol in lstAnalysisDetails.Columns)
                {
                    ResizeGridViewColumn(gvCol);
                    lstAnalysisDetails.Columns[0].Width = 0;
                }
                //txtHeader.Visibility = Visibility.Visible;
                //txtHeader1.Visibility = Visibility.Hidden;
                btnPrint.Visibility = Visibility.Visible;
                btnExport.Visibility = Visibility.Visible;

                if (!Settings.IsAFTEnabledForSite)
                {
                    this.lstAnalysisDetails.Columns.Remove(gvColCashableEFTIn);
                    this.lstAnalysisDetails.Columns.Remove(gvColCashableEFTOut);
                    this.lstAnalysisDetails.Columns.Remove(gvColNonCashableEFTIn);
                    this.lstAnalysisDetails.Columns.Remove(gvColNonCashableEFTOut);
                    this.lstAnalysisDetails.Columns.Remove(gvColWATIn);
                    this.lstAnalysisDetails.Columns.Remove(gvColWATOut);

                    dtPrintDetails.Columns.Add("CashableEFTIn", typeof(decimal), "0.00");
                    dtPrintDetails.Columns.Add("CashableEFTOut", typeof(decimal), "0.00");
                    dtPrintDetails.Columns.Add("NonCashableEFTIn", typeof(decimal), "0.00");
                    dtPrintDetails.Columns.Add("NonCashableEFTOut", typeof(decimal), "0.00");
                    dtPrintDetails.Columns.Add("WATIn", typeof(decimal), "0.00");
                    dtPrintDetails.Columns.Add("WATOut", typeof(decimal), "0.00");
                }

                GetRefreshedData();
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox(ex.Message, true);
                ExceptionManager.Publish(ex);
            }
        }

       // private void btnExport1_Click(object sender, RoutedEventArgs e)
       // {
         //   DataTable dtSite = new DataTable();
         //   DataSet dtsiteinter = new DataSet("table");
         //   dtsiteinter.Tables.Add(dtAnalysisDetails.Copy());
         //   using (CReportViewer cReportViewer = new CReportViewer())
         //   {
         //       cReportViewer.ShowSiteIntegorration("SiteIntegorrationReport", dtAnalysisDetails.Copy());
          //      cReportViewer.ShowDialog();
         //   }
       // }

        private void lstAnalysisDetails_Sorting_1(object sender, Microsoft.Windows.Controls.DataGridSortingEventArgs e)
        {
            try
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(lstAnalysisDetails.ItemsSource);
                ListSortDirection direction = (e.Column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
                e.Column.SortDirection = direction;
                e.Handled = true;
                view.SortDescriptions.Clear();
                view.SortDescriptions.Insert(0, new SortDescription() { PropertyName = "IsTotalRow", Direction = ListSortDirection.Descending });
                view.SortDescriptions.Insert(1, new SortDescription() { PropertyName = e.Column.SortMemberPath, Direction = direction });
                view.Refresh();

            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }


        }

        public enum AnalysisDetailsParent
        {
            ReportDetails = 0,
            ReportDropDetails = 1,
            SiteInterrogration = 2
        }
    }
}