using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Data;
using System.Globalization;
using BMC.CashDeskOperator.BusinessObjects;
using System.Windows.Input;
using System.Threading;
using BMC.Common.LogManagement;
using System.Windows.Media.Animation;
using CrystalDecisions.Shared;
using BMC.Presentation.POS.Reports;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CMeterLife.xaml
    /// </summary>

    public delegate void UpdateProgressBarDelegate(
        System.Windows.DependencyProperty dp, Object value);
    public delegate void FillListViewDelegate();

    public partial class CMeterLife : UserControl, IDisposable
    {
        public int InstallationNo { get; set; }
        private UpdateProgressBarDelegate _updatePbDelegate = null;
        private bool _isLoaded = false;
        private Action<double> _showMeterProcess = null;
        private Action _enableControls = null;
        private Action<DataSet> _bindGrid = null;

        public CMeterLife()
        {
            InitializeComponent();
            this.Initialize();
        }

        //public CMeterLife(int InstallationNo)
        //{
        //    InitializeComponent();
        //    this.InstallationNo = InstallationNo;
        //}

        public CMeterLife(int nInstallationNo, DateTime dtInstallationStartDate)
        {
            try
            {
                InitializeComponent();
                this.InstallationNo = nInstallationNo;
                lblStartDate.Content = dtInstallationStartDate.GetUniversalDateTimeFormat();
                this.Initialize();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void Initialize()
        {
            _updatePbDelegate += new UpdateProgressBarDelegate(pbMeters.SetValue);
            _showMeterProcess += new Action<double>(this.ShowMeterProgress);
            _enableControls += new Action(this.EnableControls);
            _bindGrid += new Action<DataSet>(this.BindGrid);
        }

        private void FillListView()
        {
            try
            {
                btnRefresh.IsEnabled = false;
                _isLoaded = false;
                pbMeters.Visibility = Visibility.Visible;

                pbMeters.Minimum = 0;
                pbMeters.Maximum = short.MaxValue;
                pbMeters.Value = 0;

                // Progress bar
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    if (_showMeterProcess != null)
                    {
                        this.Dispatcher.Invoke(_showMeterProcess,
                            System.Windows.Threading.DispatcherPriority.Render, new object[] { o });
                    }
                    else
                    {
                        LogManager.WriteLog("CMeterLife screen was disposed already. Skipped loading.", LogManager.enumLogLevel.Info);
                    }
                }, pbMeters.Maximum);

                // Actual data
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    this.FetchDataAndBindGrid();
                }, null);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error Occured in MeterLife: Get Current Meters for Installation No " + InstallationNo.ToString() + " : " + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                Thread.Sleep(2000);
                btnRefresh.IsEnabled = true;
            }
            finally
            {
            }
        }

        private void FetchDataAndBindGrid()
        {
            try
            {
                IMeterLife meterLife = MeterLifeBusinessObject.CreateInstance();
                LogManager.WriteLog("MeterLife: Get Current Meters for Installation No: " + InstallationNo.ToString() + "<START>", LogManager.enumLogLevel.Debug);
                meterLife.GetCurrentMeters(InstallationNo);

                LogManager.WriteLog("MeterLife: Database Fetching <START>", LogManager.enumLogLevel.Debug);
                DataSet dsMeterLife = meterLife.GetMeterLife(InstallationNo);
                LogManager.WriteLog("MeterLife: Database Fetching <END>", LogManager.enumLogLevel.Debug);

                if (_bindGrid != null)
                {
                    this.Dispatcher.Invoke(_bindGrid, dsMeterLife);
                }
                else
                {
                    LogManager.WriteLog("CMeterLife screen was disposed already. Skipped loading.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error Occured in MeterLife: Get Current Meters for Installation No " + InstallationNo.ToString() + " : " + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (_enableControls != null)
                {
                    this.Dispatcher.Invoke(_enableControls, null);
                }
                else
                {
                    LogManager.WriteLog("CMeterLife screen was disposed already. Skipped loading.", LogManager.enumLogLevel.Info);
                }
            }
        }

        private void BindGrid(DataSet dsMeterLife)
        {
            Binding bind = new Binding();
            lstView.DataContext = dsMeterLife.Tables[0];
            lstView.SetBinding(ListView.ItemsSourceProperty, bind);
            LogManager.WriteLog("MeterLife: Current Meters for Installation No: " + InstallationNo.ToString() + "<END>", LogManager.enumLogLevel.Debug);
        }

        private void EnableControls()
        {
            btnRefresh.IsEnabled = true;
            _isLoaded = true;
            pbMeters.Visibility = Visibility.Hidden;
            this.Cursor = Cursors.Arrow;
        }

        private void ShowMeterProgress(double maximum)
        {
            //Stores the value of the ProgressBar
            double value = 0;

            do
            {
                value += 70;
                this.ShowMeterProgressValue(value);
            }
            while (!_isLoaded && (value < maximum));
            ShowMeterProgressValue(maximum - 1);
        }

        private void ShowMeterProgressValue(double value)
        {
            if (_updatePbDelegate != null)
            {
                Dispatcher.Invoke(_updatePbDelegate,
                           System.Windows.Threading.DispatcherPriority.Render,
                           new object[] { ProgressBar.ValueProperty, value });
            }
            else
            {
                LogManager.WriteLog("CMeterLife screen was disposed already. Skipped loading.", LogManager.enumLogLevel.Info);
            }
        }

        private void lstView_Loaded(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    FillListView();
            //}

            //catch (Exception ex)
            //{
            //    ExceptionManager.Publish(ex);
            //}
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.WaitFillListView();
        }

        public void WaitFillListView()
        {
            try
            {
                this.Cursor = Cursors.Wait;
                FillListView();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string SiteCode = "";
                string AssetNumber = "";
                string PosNumber = "";
                this.Cursor = Cursors.Wait;
                btnPrint.IsEnabled = false;
                LogManager.WriteLog("Inside btnPrint_Click", LogManager.enumLogLevel.Info);

                IMeterLife meterLife = MeterLifeBusinessObject.CreateInstance();
                meterLife.GetAssetDetails(this.InstallationNo, ref SiteCode, ref AssetNumber, ref PosNumber);

                if (lstView != null)
                {
                    if (lstView.Items.Count > 0)
                    {
                        DataTable meterData = new DataTable();

                        meterData.TableName = "MachineMeters";

                        meterData.Columns.Add("Meter");
                        meterData.Columns.Add("Start");
                        meterData.Columns.Add("Current");
                        meterData.Columns.Add("Difference");
                        meterData.Columns.Add("Value");
                        meterData.Columns.Add("SiteCode");
                        meterData.Columns.Add("PosNumber");
                        meterData.Columns.Add("AssetNumber");

                        LogManager.WriteLog("Filling the Meter Datatable for Print Report...", LogManager.enumLogLevel.Info);

                        foreach (DataRowView dvItem in lstView.Items)
                        {
                            DataRow meterRow = meterData.NewRow();

                            meterRow["Meter"] = dvItem["Meter"].ToString();
                            meterRow["Start"] = dvItem["Start"].ToString();
                            meterRow["Current"] = dvItem["Current"].ToString();
                            meterRow["Difference"] = dvItem["Difference"].ToString();
                            meterRow["Value"] = dvItem["Value"].ToString() != string.Empty ? Convert.ToDecimal(dvItem["Value"]).GetUniversalCurrencyFormatWithSymbol() : "NA";
                            meterRow["SiteCode"] = SiteCode;
                            meterRow["PosNumber"] = PosNumber;
                            meterRow["AssetNumber"] = AssetNumber;
                            meterData.Rows.Add(meterRow);
                        }

                        LogManager.WriteLog("Meter Datatable filled successfully with Print Report Data.", LogManager.enumLogLevel.Info);

                        CReportViewer cReportViewer = new CReportViewer();

                        cReportViewer.PrintMeterLifeReport(meterData);

                        cReportViewer = null;
                        meterData = null;
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID47");
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID47");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
                btnPrint.IsEnabled = true;
            }
        }

        private void MeterLife_Loaded(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            pbMeters.Visibility = Visibility.Hidden;
            btnRefresh.IsEnabled = false;
            this.WaitFillListView();
        }

        #region IDisposable Members

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.MeterLife.Loaded -= (this.MeterLife_Loaded);
                        this.lstView.Loaded -= (this.lstView_Loaded);
                        this.btnRefresh.Click -= (this.btnRefresh_Click);
                        this.btnPrint.Click -= (this.btnPrint_Click);

                        _updatePbDelegate -= (pbMeters.SetValue);
                        _showMeterProcess -= (this.ShowMeterProgress);
                        _enableControls -= (this.EnableControls);
                        _bindGrid -= (this.BindGrid);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CMeterLife objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        ~CMeterLife()
        {
            Dispose(false);
        }

        #endregion
    }

    [ValueConversion(typeof(decimal), typeof(string))]
    public class PriceConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.ToString() != String.Empty)
            {
                decimal price = System.Convert.ToDecimal(value);
                return price.ToString("C");
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string price = value.ToString();

            decimal result;
            if (Decimal.TryParse(price, NumberStyles.Any, null, out result))
            {
                return result;
            }
            return value;
        }

        #endregion
    }

}
