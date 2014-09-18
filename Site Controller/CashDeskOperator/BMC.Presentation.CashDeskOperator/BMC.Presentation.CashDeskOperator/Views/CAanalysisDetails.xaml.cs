using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator.BusinessObjects;
using System.Windows.Media;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;


namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CAanalysisDetails.xaml
    /// </summary>
    public partial class CAanalysisDetails : Window, IDisposable
    {

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DataTable dtAnalysisDetails;
        private int _iWeekID = 0;
        private int _iType;
        public string HeaderText{ get; set; }
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private DataTable dtPrintDetails = null;
        private Decimal TotalAvgBet = 0;
        private DateTime _dteStartDate;
        private DateTime _DteEndDate;

        public CAanalysisDetails(int iType,  DateTime dteStartDate, DateTime DteEndDate, int iWeekID)
        {
            InitializeComponent();
            try
            {
                _dteStartDate = dteStartDate;
                _DteEndDate = DteEndDate;
                if (iType ==-99)
                {
                    lblStatus.Visibility = Visibility.Visible;
                    progressBar1.Visibility = Visibility.Visible;
                    //txtHeader.Text = "Drop Week BreakDown";
                    txtHeader1.Visibility = Visibility.Visible;
                    txtHeader.Visibility = Visibility.Hidden;
                    btnPrint.Visibility = Visibility.Hidden;
                    btnExport.Visibility = Visibility.Hidden;
                    btnRefresh.Visibility = Visibility.Hidden;
                  
                    _iType = -99;
                    _iWeekID = iWeekID;

                    backgroundWorker1 = new BackgroundWorker();
                    backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
                    backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
                }
                else
                {
                    Refresh(iType,dteStartDate,DteEndDate);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_iType==-99)
            {
                lstAnalysisDetails.Visibility = Visibility.Hidden;
                lstCollectionDetails.Visibility = Visibility.Visible;
                progressBar1.IsIndeterminate = true;
                this.Cursor = Cursors.Wait;
                backgroundWorker1.RunWorkerAsync();
            }
           
        }

        private void backgroundWorker1_DoWork(object sender,DoWorkEventArgs e)
        { 
            //System.Threading.Thread.Sleep(10000);
            dtAnalysisDetails = GetCollectionDetails();
        }

        private DataTable GetCollectionDetails()
        { 
            IAnalysis objCashdesk = AnalysisBusinessObject.CreateInstance();
            return objCashdesk.GetWeeklyCollectionDetails(_iWeekID);
        }

        private void btn_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SortClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            String field = column.Tag as String;
            if (_CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(_CurSortCol).Remove(_CurAdorner);
                lstAnalysisDetails.Items.SortDescriptions.Clear();
            }
            ListSortDirection newDir = ListSortDirection.Ascending;
            if (_CurSortCol == column && _CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;
            _CurSortCol = column;
            _CurAdorner = new SortAdorner(_CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(_CurSortCol).Add(_CurAdorner);
            lstAnalysisDetails.Items.SortDescriptions.Add(new SortDescription(field, newDir));
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
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                lblStatus.Visibility = Visibility.Hidden;
                progressBar1.Visibility = Visibility.Hidden;
                this.Cursor = Cursors.Arrow;
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintOrExportDetails(true);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            PrintOrExportDetails(false);
        }

        private void PrintOrExportDetails(bool Print)
        {
            string sType = string.Empty;
            switch (_iType)
            {
                case 1: sType = "(Day)"; break;
                case 2: sType = "(Drop)"; break;
                case 3: sType = "(Week)"; break;
                case 4: sType = "(Month)"; break;
                default: sType = ""; break;
            }

            string sReportName = txtHeader.Text.Trim() + sType;

            using (CReportViewer cReportViewer = new CReportViewer())
            {
                if (dtPrintDetails.Rows.Count > 0)
                {
                    cReportViewer.ShowReportsAnalysisDetails(dtPrintDetails, sReportName, Print, TotalAvgBet,string.Empty);
                }
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
                        ((BMC.Presentation.POS.Views.CAanalysisDetails)(this)).Loaded -= (this.Window_Loaded);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_2)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_4)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_5)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_6)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_7)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_8)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_9)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_10)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_11)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_12)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_13)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_14)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_15)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_16)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_17)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_18)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_19)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_20)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_21)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_22)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_23)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_24)).Click -= (this.SortClick);
                        ((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_25)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_26)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_27)).Click -= (this.SortClick);
                        //((System.Windows.Controls.GridViewColumnHeader)(GridViewColumnHeader_28)).Click -= (this.SortClick);
                        this.btnExit.Click -= (this.btn_Exit);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CAanalysisDetails objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CAanalysisDetails"/> is reclaimed by garbage collection.
        /// </summary>
        ~CAanalysisDetails()
        {
            Dispose(false);
        }

        #endregion      

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh(_iType, _dteStartDate, _DteEndDate);
        }

        public void Refresh(int iType, DateTime dteStartDate, DateTime DteEndDate)
        {
            _iType = iType;
            lblStatus.Visibility = Visibility.Hidden;
            progressBar1.Visibility = Visibility.Hidden;
            lstAnalysisDetails.Visibility = Visibility.Visible;
            lstCollectionDetails.Visibility = Visibility.Hidden;
            IAnalysis objCashdesk = AnalysisBusinessObject.CreateInstance();
            DataTable dtAnalysisDetails = objCashdesk.GetAnalysisDetails(iType, dteStartDate, DteEndDate,  BMC.Business.CashDeskOperator.AnalysisView.Position, 0);
            dtPrintDetails = null;
            dtPrintDetails = dtAnalysisDetails.Copy();
            if (dtPrintDetails.Rows.Count > 0)
            {
                TotalAvgBet = Convert.ToDecimal(dtPrintDetails.Rows[0]["AvgBet"]);
                dtPrintDetails.Rows[0].Delete();
                dtPrintDetails.AcceptChanges();
            }
            lstAnalysisDetails.DataContext = dtAnalysisDetails.DefaultView;
            txtHeader.Visibility = Visibility.Visible;
            txtHeader1.Visibility = Visibility.Hidden;
            btnPrint.Visibility = Visibility.Visible;
            btnExport.Visibility = Visibility.Visible;

            if (!Settings.IsAFTEnabledForSite)
            {

                this.gvAnalysisDetails.Columns.Remove(gvColCashableEFTIn);
                this.gvAnalysisDetails.Columns.Remove(gvColCashableEFTOut);
                this.gvAnalysisDetails.Columns.Remove(gvColNonCashableEFTIn);
                this.gvAnalysisDetails.Columns.Remove(gvColNonCashableEFTOut);
                this.gvAnalysisDetails.Columns.Remove(gvColWATIn);
                this.gvAnalysisDetails.Columns.Remove(gvColWATOut);

                dtPrintDetails.Columns.Add("CashableEFTIn", typeof(decimal), "0.00");
                dtPrintDetails.Columns.Add("CashableEFTOut", typeof(decimal), "0.00");
                dtPrintDetails.Columns.Add("NonCashableEFTIn", typeof(decimal), "0.00");
                dtPrintDetails.Columns.Add("NonCashableEFTOut", typeof(decimal), "0.00");
                dtPrintDetails.Columns.Add("WATIn", typeof(decimal), "0.00");
                dtPrintDetails.Columns.Add("WATOut", typeof(decimal), "0.00");
            }
        }
    }

    public class SortAdorner : Adorner
    {
        private readonly static Geometry _AscGeometry =
            Geometry.Parse("M 0,0 L 10,0 L 5,5 Z");
        private readonly static Geometry _DescGeometry =
            Geometry.Parse("M 0,5 L 10,5 L 5,0 Z");
        public ListSortDirection Direction { get; private set; }
        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        { Direction = dir; }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (AdornedElement.RenderSize.Width < 20)
                return;
            drawingContext.PushTransform(
                new TranslateTransform(
                  AdornedElement.RenderSize.Width - 15,
                  (AdornedElement.RenderSize.Height - 5) / 2));
            drawingContext.DrawGeometry(Brushes.Black, null,
                Direction == ListSortDirection.Ascending ?
                  _AscGeometry : _DescGeometry);
            drawingContext.Pop();
        }
    }
}
