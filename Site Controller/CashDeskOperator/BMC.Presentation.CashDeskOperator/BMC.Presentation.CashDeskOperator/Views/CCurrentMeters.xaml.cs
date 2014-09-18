using System;
using System.Collections.Generic;
using System.Data;
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
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.CashDeskOperator.BusinessObjects;
using System.Threading;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CCurrentMeters.xaml
    /// </summary>
    /// 
   
    public partial class CCurrentMeters : UserControl, IDisposable
    {
        public int InstallationNo { get; set; }
        DateTime readDate;
        private UpdateProgressBarDelegate _updatePbDelegate = null;
        private bool _isLoaded = false;
        private Action<double> _showMeterProcess = null;
        private Action _enableControls = null;
        private Action<DataSet> _bindGrid = null;

        public CCurrentMeters()
        {
            InitializeComponent();
            this.Initialize();
        }

        public CCurrentMeters(int nInstallationNo, DateTime dtInstallationStartDate)
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
                        LogManager.WriteLog("CCurrentMeters screen was disposed already. Skipped loading.", LogManager.enumLogLevel.Info);
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
                LogManager.WriteLog("Error Occured in CurrentMeters: Get Current day Meters for Installation No " + InstallationNo.ToString() + " : " + ex.Message, LogManager.enumLogLevel.Error);
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
                LogManager.WriteLog("CurrentMeters: Get Current Meters for Installation No: " + InstallationNo.ToString() + "<START>", LogManager.enumLogLevel.Debug);
                meterLife.GetCurrentMeters(InstallationNo);

                LogManager.WriteLog("CurrentMeters: Database Fetching <START>", LogManager.enumLogLevel.Debug);
                DataSet dsCurrentDayMeter = meterLife.GetCurrentDayMeters(InstallationNo);
                LogManager.WriteLog("CurrentMeters: Database Fetching <END>", LogManager.enumLogLevel.Debug);

                if (_bindGrid != null)
                {
                    this.Dispatcher.Invoke(_bindGrid, dsCurrentDayMeter);
                }
                else
                {
                    LogManager.WriteLog("CCurrentMeters screen was disposed already. Skipped loading.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error Occured in CurrentMeters: Get Current Day Meters for Installation No " + InstallationNo.ToString() + " : " + ex.Message, LogManager.enumLogLevel.Error);
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
                    LogManager.WriteLog("CCurrentMeters screen was disposed already. Skipped loading.", LogManager.enumLogLevel.Info);
                }
            }
        }

        private void BindGrid(DataSet dsCurrentDayMeter)
        {
            if (dsCurrentDayMeter != null && dsCurrentDayMeter.Tables.Count > 0 && dsCurrentDayMeter.Tables[0].Rows.Count > 0)
            {
                if (dsCurrentDayMeter.Tables[0].Rows[0]["ReadDate"].ToString() != String.Empty)
                {
                    readDate = Convert.ToDateTime(dsCurrentDayMeter.Tables[0].Rows[0]["ReadDate"]);
                    lblReadDate.Content = readDate.GetUniversalDateTimeFormat();
                    lblReadDate.Foreground = new SolidColorBrush(Colors.White);
                    lblReadDate.FontWeight = FontWeights.Normal;
                }
                else
                {
                    lblReadDate.Foreground = new SolidColorBrush(Colors.Red);
                    lblReadDate.FontWeight = FontWeights.Bold;
                }
                //
                Binding bind = new Binding();
                lstView.DataContext = dsCurrentDayMeter.Tables[0];
                lstView.SetBinding(ListView.ItemsSourceProperty, bind);
                LogManager.WriteLog("CurrentMeters: Current Day Meters for Installation No: " + InstallationNo.ToString(), LogManager.enumLogLevel.Debug);
            }
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
                LogManager.WriteLog("CCurrentMeters screen was disposed already. Skipped loading.", LogManager.enumLogLevel.Info);
            }
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

        private void MeterLife_Loaded(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            pbMeters.Visibility = Visibility.Hidden;
            btnRefresh.IsEnabled = false;
            this.WaitFillListView();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string SiteCode = "";
                string AssetNumber = "";
                string PosNumber = "";
                this.Cursor = Cursors.Wait;

                LogManager.WriteLog("Inside btnPrint_Click", LogManager.enumLogLevel.Info);

                IMeterLife meterLife = MeterLifeBusinessObject.CreateInstance();
                meterLife.GetAssetDetails(this.InstallationNo, ref SiteCode, ref AssetNumber, ref PosNumber);

                if (lstView != null)
                {
                    if (lstView.Items.Count > 0)
                    {
                        DataTable meterData = new DataTable();

                        meterData.TableName = "CurrentMeters";

                        meterData.Columns.Add("Meter");
                        meterData.Columns.Add("Start");
                        meterData.Columns.Add("Current");
                        meterData.Columns.Add("Difference");
                        meterData.Columns.Add("Value");
                        meterData.Columns.Add("SiteCode");
                        meterData.Columns.Add("PosNumber");
                        meterData.Columns.Add("AssetNumber");
                        meterData.Columns.Add("ReadDate");

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
                            meterRow["ReadDate"] = readDate.ToString("dd-MMM-yyyy HH:mm:ss");
                            meterData.Rows.Add(meterRow);
                        }

                        LogManager.WriteLog("Meter Datatable filled successfully with Print Report Data.", LogManager.enumLogLevel.Info);

                        CReportViewer cReportViewer = new CReportViewer();

                        cReportViewer.PrintCurrentDayMeterReport(meterData);

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
            }
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
                        this.CurrentMeters.Loaded -= (this.MeterLife_Loaded);
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
                    LogManager.WriteLog("|=> CCurrentMeters objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        ~CCurrentMeters()
        {
            Dispose(false);
        }

        #endregion

        private void lstView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
