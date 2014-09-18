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
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Helper_classes;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator;
using System.IO;
using BMC.Common.ExceptionManagement;
using System.ComponentModel;
using System.Configuration;
using BMC.Transport;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for cHourly.xaml
    /// </summary>
    public partial class cHourly : UserControl, IDisposable
    {
        private bool IsMainScreen = true;
        private bool IsFirstTime = true;
        Int32 StartHour = 6;
        ICashDeskManager objCashDeskManager = null;
        public cHourly()
        {
            InitializeComponent();
            objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();
            try
            {
                StartHour = BMC.Transport.Settings.Gaming_Day_Start_Hour;
            }
            catch
            { }
            btnSummary.Visibility = Visibility.Hidden;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ////BMC.Presentation.POS.Views.BatchHistoryBreakdown bd = new BMC.Presentation.POS.Views.BatchHistoryBreakdown(3,2);
            //BMC.Presentation.POS.BatchBreakdown  bd = new BMC.Presentation.POS.BatchBreakdown(2);
            ////BMC.Presentation.POS.BatchBreakdown bd = new BMC.Presentation.POS.BatchBreakdown(677,true);
            //bd.ShowDialog();

            IHourly objVTPHourly = new HourlyBusinessObject();
            DataTable dtHSTypes = objVTPHourly.GetHSTypes();
            cmbGroupBy.ItemsSource = ((System.ComponentModel.IListSource)dtHSTypes).GetList();
            cmbGroupBy.DataContext = dtHSTypes.DefaultView;
            cmbGroupBy.DisplayMemberPath = "HS_TypeName";
            cmbGroupBy.SelectedValuePath = "HS_Type";
            //cmbGroupBy.SelectedIndex = 0;
            try
            {
                string sHourlydefault = string.Empty;
                if (dtHSTypes.Select("HS_TypeName='" + Settings.Hourly_DefaultItem + "'").Count() > 0)
                    cmbGroupBy.Text = Settings.Hourly_DefaultItem;
                else
                    cmbGroupBy.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                cmbGroupBy.SelectedIndex = 0;
            }

            if (cmbLast.SelectedIndex < 0)
            {
                cmbLast.SelectedIndex = 0;
            }
            cmbOptions.SelectedIndex = 0;
            IsFirstTime = false;
            FillData();
        }


        private GridViewColumn AddGridViewColumn(string ColumnName, string DisplayMemberBinding)
        {
            DataTemplate dt = new DataTemplate();
            GridViewColumn gvc = new GridViewColumn();
            gvc.Header = ColumnName;
            gvc.DisplayMemberBinding = new Binding(DisplayMemberBinding);
            return gvc;
        }

        private void lstHourly_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    ListView view = sender as ListView;

            //    if (view.SelectedIndex >= 0)
            //    {
            //        IsMainScreen = !IsMainScreen;
            //        FillData();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("lstHourly_MouseDoubleClick::" + ex.Message, LogManager.enumLogLevel.Error);
            //}
        }

        private void btnDeails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstHourly.SelectedIndex >= 0 || btnDeails.Content.ToString().ToUpper() == "SUMMARY")
                {
                    IsMainScreen = !IsMainScreen;
                    FillData();
                    btnSummary.Visibility = Visibility.Visible;
                    btnDeails.Visibility = Visibility.Hidden;
                    //btnDeails.Content = btnDeails.Content.ToString().ToUpper() == "DETAILS" ? "Summary" : "Details";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnDetails_Click::" + ex.Message, LogManager.enumLogLevel.Error);

            }
        }

        private void btnSummary_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnSummary.IsEnabled = false;
                IsMainScreen = !IsMainScreen;
                FillData();
                btnSummary.Visibility = Visibility.Hidden;
                btnDeails.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnSummary_Click::" + ex.Message, LogManager.enumLogLevel.Error);

            }
            finally
            {
                btnSummary.IsEnabled = true;
            }
        }

        private void FillData()
        {

            try
            {
                this.Cursor = Cursors.Wait;
                //DataRowView dr;

                HourlyDetailEntity entity = null;
                IHourly objVTPHourly = new HourlyBusinessObject();
                Transport.CashDeskOperatorEntity.HourlyDetails objHourly = new BMC.Transport.CashDeskOperatorEntity.HourlyDetails();
                objHourly.StartHour = StartHour;
                
                objHourly.Datatype = GetValue<String>(cmbGroupBy.SelectedValue);
                objHourly.Rows = GetStaticComboValue<Int32>(cmbLast);
                if (objHourly.Rows == 0)
                {
                    objHourly.Rows = BMC.Transport.Settings.HourlyScreenMaxRecords == 0 ? 90 : BMC.Transport.Settings.HourlyScreenMaxRecords;
                }
                if (!IsMainScreen)
                {
                    entity = (HourlyDetailEntity)lstHourly.SelectedItem;
                    objHourly.Date = entity.Date;
                }

                string SelectedOption = ((ComboBoxItem)cmbOptions.SelectedItem).Tag.ToString();//GetStaticComboValue<string>(cmbOptions);//

                int nSelectedItem = cmbOptions.SelectedIndex;
                //
                switch (nSelectedItem)
                {
                    case 0: //Category
                        objHourly.Category = GetValue<Int32>(cmbOptions2.SelectedValue);
                        if (objHourly.Category == 0)
                        {
                            lstHourly.ItemsSource = null;
                            return;
                        }
                        break;
                    case 1://POSITION
                        objHourly.Position = GetValue<Int32>(cmbOptions2.SelectedValue);
                        if (objHourly.Position == 0)
                        {
                            lstHourly.ItemsSource = null;
                            return;
                        }
                        break;
                    case 2://SITE
                        break;
                    case 3://ZONE
                        objHourly.Zone = GetValue<Int32>(cmbOptions2.SelectedValue);
                        if (objHourly.Zone == 0)
                        {
                            lstHourly.ItemsSource = null;
                            return;
                        }
                        break;

                    default:
                        return;
                }
                //                HourlyDetailsEntity dtHourly = objVTPHourly.GetHourlyStatistics(objHourly., (objHourly.Datatype.ToUpper() == "AVG_BET" ? false : !IsMainScreen));
                HourlyDetailsEntity dtHourly = objVTPHourly.GetHourlyStatistics(objHourly.StartHour, objHourly.Rows, objHourly.Datatype,
                    objHourly.Category, objHourly.Zone, objHourly.Position, objHourly.Date, BMC.Business.CashDeskOperator.HourlyDetails.HourlyBasedOnCalendarDay);

                switch (objHourly.Datatype.ToUpper())
                {
                    case "NON_CASHABLE_VOUCHERS_IN_QTY":
                    case "NON_CASHABLE_VOUCHERS_OUT_QTY":
                    case "TICKETS_INSERTED_QTY":
                    case "TICKETS_PRINTED_QTY":
                    case "GAMES_LOST":
                    case "GAMES_WON":
                    case "GAMES_BET":
                        ((HourlyCurrencyPriceConverter)Resources["PriceConverter"]).ShowCurrencySymbol = false;
                        ((HourlyCurrencyPriceConverter)Resources["PriceConverter"]).IsOccupancy = false;
                        break;
                    case "OCCUPANCY(%)":
                        ((HourlyCurrencyPriceConverter)Resources["PriceConverter"]).ShowCurrencySymbol = false;
                        ((HourlyCurrencyPriceConverter)Resources["PriceConverter"]).IsOccupancy = true;
                        if (IsMainScreen)
                        {
                            dtHourly.ForEach((x) => { x.ID = -1; });
                        }
                        break;
                    default:
                        ((HourlyCurrencyPriceConverter)Resources["PriceConverter"]).ShowCurrencySymbol = true;
                        ((HourlyCurrencyPriceConverter)Resources["PriceConverter"]).IsOccupancy = false;
                        break;
                }

                lstHourly.ItemsSource = dtHourly;
                
                FormatListView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { this.Cursor = Cursors.Arrow; }
        }

        private void cmbOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            try
            {
                cmbOptions2.ItemsSource = null;
                //string SelectedOption = GetStaticComboValue<string>(cmbOptions);
                int nSelectedItem = cmbOptions.SelectedIndex;
                if (nSelectedItem > -1)
                {
                    IHourly objVTPHourly = new HourlyBusinessObject();
                    DataTable dtOptions = new DataTable();

                    switch (nSelectedItem)
                    {
                        case 0://"CATEGORY":
                            dtOptions = objVTPHourly.GetMachineTypeDetails();
                            cmbOptions2.Visibility = Visibility.Visible;
                            break;
                        case 1://"POSITION":
                            dtOptions = objVTPHourly.GetPositions();
                            cmbOptions2.Visibility = Visibility.Visible;
                            break;
                        case 2://"SITE":
                            dtOptions = objVTPHourly.GetSiteName();
                            cmbOptions2.Visibility = Visibility.Hidden;
                            break;

                        case 3://"ZONE":
                            dtOptions = objVTPHourly.GetZones();
                            cmbOptions2.Visibility = Visibility.Visible;
                            break;

                        default:
                            break;
                    }

                    if (dtOptions != null)
                    {
                        dtOptions.DefaultView.Sort = "Name";
                        cmbOptions2.ItemsSource = ((System.ComponentModel.IListSource)dtOptions).GetList();
                        cmbOptions2.DataContext = dtOptions.DefaultView;
                        cmbOptions2.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("cmbOptions_SelectionChanged::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            finally { this.Cursor = Cursors.Arrow; }
        }


        private void cmbCombos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                IsMainScreen = true;
                //When selection is changed then Details button should be visible
                //as the selection change event shows the Summary data.
                btnDeails.Visibility = Visibility.Visible;
                btnSummary.Visibility = Visibility.Hidden;

                if (!IsFirstTime)
                {
                    FillData();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("cmbCombos_SelectionChanged::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            finally { this.Cursor = Cursors.Arrow; }
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

        private void FormatListView()
        {
            try
            {
               // GridView GvHourly = (GridView)lstHourly.View;
                int i = 0;
                int startHour = (BMC.Business.CashDeskOperator.HourlyDetails.HourlyBasedOnCalendarDay ? 0 : StartHour);
                foreach (Microsoft.Windows.Controls.DataGridColumn dc in lstHourly.Columns)
                {
                    if (dc.Header.ToString().StartsWith("HS_Hour"))
                    {
                        dc.Header = ((i + startHour) % 24).ToString() + " -> " + ((i + 1 + startHour) % 24).ToString();
                        i++;
                    }
                }

                if (IsMainScreen)
                {
                    lstHourly.Columns[0].Visibility = Visibility.Visible;
                    lstHourly.Columns[1].Visibility = Visibility.Hidden;
                    lstHourly.Columns[2].Visibility = Visibility.Visible;
                    lstHourly.Columns[3].Visibility = Visibility.Hidden;
                    lstHourly.Columns[4].Visibility = Visibility.Hidden;
                }
                else
                {
                    lstHourly.Columns[0].Visibility = Visibility.Hidden;
                    lstHourly.Columns[1].Visibility = Visibility.Visible;
                    lstHourly.Columns[2].Visibility = Visibility.Hidden;
                    lstHourly.Columns[3].Visibility = Visibility.Visible;
                    lstHourly.Columns[4].Visibility = Visibility.Visible;
                }

                //for (i = 5; i < 30; i++)
                //{
                //    ResizeGridViewColumn(lstHourly.Columns[i]);
                //}
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ResizeGridViewColumn(Microsoft.Windows.Controls.DataGridColumn dataGridColumn)
        {
            if (double.IsNaN(dataGridColumn.Width.DisplayValue))
            {
                dataGridColumn.Width = new Microsoft.Windows.Controls.DataGridLength(dataGridColumn.ActualWidth);
               
            }

           // dataGridColumn.Width = double.NaN;
        }

        //private void ResizeGridViewColumn(DataGridColumn column)
        //{
            
        //}

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
                        ((BMC.Presentation.POS.Views.cHourly)(this)).Loaded -= (this.UserControl_Loaded);
                        this.cmbGroupBy.SelectionChanged -= (this.cmbCombos_SelectionChanged);
                        this.cmbLast.SelectionChanged -= (this.cmbCombos_SelectionChanged);
                        this.cmbOptions.SelectionChanged -= (this.cmbOptions_SelectionChanged);
                        this.cmbOptions2.SelectionChanged -= (this.cmbCombos_SelectionChanged);
                        this.lstHourly.MouseDoubleClick -= (this.lstHourly_MouseDoubleClick);
                        this.btnDeails.Click -= (this.btnDeails_Click);
                        this.btnSummary.Click -= (this.btnSummary_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> cHourly objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="cHourly"/> is reclaimed by garbage collection.
        /// </summary>
        ~cHourly()
        {
            Dispose(false);
        }

        #endregion


        private void lstHourly_Sorting(object sender, Microsoft.Windows.Controls.DataGridSortingEventArgs e)
        {
            try
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(lstHourly.ItemsSource);
                e.Column.SortDirection = (e.Column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
                e.Handled = true;
                view.SortDescriptions.Clear();
                view.SortDescriptions.Insert(0, new SortDescription() { PropertyName = "isTotalRow", Direction = ListSortDirection.Descending });
                if(e.Column.SortMemberPath=="Day")
                    view.SortDescriptions.Insert(1, new SortDescription() { PropertyName = "Date", Direction = e.Column.SortDirection.Value });
                else
                    view.SortDescriptions.Insert(1, new SortDescription() { PropertyName = e.Column.SortMemberPath, Direction = e.Column.SortDirection.Value });
                view.Refresh();

            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }


        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog fileDialog = null;
                string filepath = string.Empty;
                if (lstHourly != null)
                {
                    fileDialog = new System.Windows.Forms.SaveFileDialog();
                    fileDialog.Filter = "Excel Files (*.csv)|*.csv|All Files (*.*) |*.*";
                    fileDialog.ShowDialog();
                    filepath = fileDialog.FileName;
                    if (filepath != "")
                    {

                        bool bResult = objCashDeskManager.HourlyExportToExcel(lstHourly, filepath, IsMainScreen);
                        if (bResult)
                        {
                            MessageBox.ShowBox("MessageID559", BMC_Icon.Information);
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID560");
                        }
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID561");
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnExport_Click::" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }


    }
}
