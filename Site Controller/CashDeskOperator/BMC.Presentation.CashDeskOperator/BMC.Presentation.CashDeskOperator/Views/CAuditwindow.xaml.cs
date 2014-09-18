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
using System.Data.SqlClient;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.CashDeskOperator;
using BMC.Presentation.POS.Views;
using System.Data;
using System.ComponentModel;
using BMC.Presentation.POS.Helper_classes;
using BMC.Transport;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CAuditView : UserControl, IDisposable
    {
        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        BMC.CashDeskOperator.BusinessObjects.Audit oAudit_History = null;

        public CAuditView()
        {
            InitializeComponent();

            if (Settings.IsAFTEnabledForSite)
                btnToggle.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dtpStartDate.Text = DateTime.Now.ToString();
                dtpEndDate.Text = DateTime.Now.ToString();

                dtpStartDate.DisplayDate = DateTime.Now;
                dtpEndDate.DisplayDate = DateTime.Now;


                tmpStartTime.SelectedHour = 0;
                tmpStartTime.SelectedMinute = 0;
                tmpStartTime.SelectedSecond = 0;

                tmpEndTime.SelectedHour = DateTime.Now.Hour;
                tmpEndTime.SelectedMinute = DateTime.Now.Minute;
                tmpEndTime.SelectedSecond = DateTime.Now.Second;

                cmbRows.Items.Add("ALL");
                cmbRows.Items.Add("10");
                cmbRows.Items.Add("20");
                cmbRows.Items.Add("50");
                cmbRows.Items.Add("100");
                cmbRows.SelectedIndex = 0;

                LoadData();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void LoadData()
        {
            try
            {
                StartDate = new DateTime(((DateTime)dtpStartDate.SelectedDate).Year,
                              ((DateTime)dtpStartDate.SelectedDate).Month,
                              ((DateTime)dtpStartDate.SelectedDate).Day,
                              tmpStartTime.SelectedHour, tmpStartTime.SelectedMinute,
                              tmpStartTime.SelectedSecond);

                EndDate = new DateTime(((DateTime)dtpEndDate.SelectedDate).Year,
                    ((DateTime)dtpEndDate.SelectedDate).Month,
                    ((DateTime)dtpEndDate.SelectedDate).Day,
                    tmpEndTime.SelectedHour, tmpEndTime.SelectedMinute,
                    tmpEndTime.SelectedSecond);

                oAudit_History = new BMC.CashDeskOperator.BusinessObjects.Audit();

                var listAudit_History = oAudit_History.GetModulesList();
                if (listAudit_History == null)
                    return;
                cmbModules.ItemsSource = listAudit_History;
                cmbModules.DisplayMemberPath = "Audit_Module_Name";
                cmbModules.SelectedValuePath = "Audit_Module_ID";
                cmbModules.SelectedIndex = 0;

                // For resizing columns automatically.
                //GridView gv = lvAuditView.View as GridView;
                //if (gv != null)
                //{
                //    foreach (GridViewColumn gvc in gv.Columns)
                //    {
                //        gvc.Width = gvc.ActualWidth;
                //        if (gvc.Width == 0.0 || gvc.Width == 0)
                //            gvc.Width = 0;
                //        else
                //            gvc.Width = Double.NaN;
                //    }
                //}               
                //GridView gvAFT = lvAFTView.View as GridView;
                //if (gvAFT != null)
                //{
                //    foreach (GridViewColumn gvc in gvAFT.Columns)
                //    {
                //        gvc.Width = gvc.ActualWidth;
                //        if (gvc.Width == 0.0 || gvc.Width == 0)
                //            gvc.Width = 0;
                //        else
                //            gvc.Width = Double.NaN;
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnToggle_Click(object sender, RoutedEventArgs e)
        {
            if (grdAudit.IsVisible)
            {
                grdAudit.Visibility = Visibility.Hidden;
                GridAFT.Visibility = Visibility.Visible;
                cmbModules.Visibility = Visibility.Hidden;
                txtModule.Visibility = Visibility.Hidden;
                btnToggle.Content = FindResource("CAuditwindow_xaml_button_Toggle2").ToString();

            }
            else
            {
                grdAudit.Visibility = Visibility.Visible;
                GridAFT.Visibility = Visibility.Hidden;
                cmbModules.Visibility = Visibility.Visible;
                txtModule.Visibility = Visibility.Visible;
                btnToggle.Content = FindResource("CAuditwindow_xaml_button_Toggle1").ToString();
            }
        }

        private void btnViewAudit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnViewAudit.IsEnabled = false;

                #region CR_#93510 (A.Vinod Kumar - 12/11/2011)
                /* <CR_#93510>                  
                 * 1. Exact records between from and to date
                 */
                // since we don't have the milliseconds in the UI, remove it from the date
                StartDate = StartDate.AddMilliseconds(-1 * StartDate.Millisecond);
                EndDate = EndDate.AddMilliseconds(-1 * EndDate.Millisecond);
                /* </CR_#93510> */
                #endregion

                if (EndDate > DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    lvAuditView.ItemsSource = null;
                    return;
                }
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID99", BMC_Icon.Information);
                    lvAuditView.ItemsSource = null;
                    return;
                }
                oAudit_History = new BMC.CashDeskOperator.BusinessObjects.Audit();

                #region CR_#93510 (A.Vinod Kumar - 12/11/2011)
                /* <CR_#93510>                  
                 * 1. Exact records between from and to date
                 */
                // after date comparision add 999 milliseconds to todate,
                // otherwise will not get the records between milliseconds
                EndDate = EndDate.AddMilliseconds(999);
                /* </CR_#93510> */
                #endregion

                if (grdAudit.IsVisible)
                {
                    if (cmbModules.SelectedValue != null)
                    {
                        var orsp_GetAuditDetailsResult = oAudit_History.GetAuditDetails((DateTime)StartDate, (DateTime)EndDate,
                            (int.Parse(cmbModules.SelectedValue.ToString()) != 0) ? cmbModules.SelectedValue.ToString() : string.Empty, (cmbRows.SelectedItem == null || cmbRows.SelectedItem.ToString() == "ALL") ? 0 : int.Parse(cmbRows.SelectedItem.ToString()));
                        if (orsp_GetAuditDetailsResult == null)
                        {
                            lvAuditView.ItemsSource = null;
                            return;
                        }
                        if (orsp_GetAuditDetailsResult.Count > 0)
                        {
                            lvAuditView.ItemsSource = orsp_GetAuditDetailsResult;
                            //foreach (GridViewColumn gvCol in gvAudit.Columns)
                            //    ResizeGridViewColumn(gvCol);
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID100", BMC_Icon.Information);
                            lvAuditView.ItemsSource = null;
                        }
                    }
                    else
                    {
                        var orsp_GetAuditDetailsResult = oAudit_History.GetAuditDetails((DateTime)StartDate, (DateTime)EndDate,
                            string.Empty, (cmbRows.SelectedItem == null || cmbRows.SelectedItem.ToString() == "ALL") ? 0 : int.Parse(cmbRows.SelectedItem.ToString()));
                        if (orsp_GetAuditDetailsResult == null)
                        {
                            lvAuditView.ItemsSource = null;
                            return;
                        }
                        if (orsp_GetAuditDetailsResult.Count > 0)
                        {
                            lvAuditView.ItemsSource = orsp_GetAuditDetailsResult;
                            //foreach (GridViewColumn gvCol in gvAudit.Columns)
                            //    ResizeGridViewColumn(gvCol);
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID100", BMC_Icon.Information);
                            lvAuditView.ItemsSource = null;
                        }
                    }
                }
                else
                {
                    var orsp_GetAFTAuditDetailsResult = oAudit_History.GetAFTAuditData((DateTime)StartDate, (DateTime)EndDate,
                             (cmbRows.SelectedItem == null || cmbRows.SelectedItem.ToString() == "ALL") ? 0 : int.Parse(cmbRows.SelectedItem.ToString()));

                    if (orsp_GetAFTAuditDetailsResult == null)
                    {
                        lvAFTView.ItemsSource = null;
                        return;
                    }
                    if (orsp_GetAFTAuditDetailsResult.Count > 0)
                        lvAFTView.ItemsSource = orsp_GetAFTAuditDetailsResult;
                    else
                    {
                        MessageBox.ShowBox("MessageID100", BMC_Icon.Information);
                        lvAFTView.ItemsSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnViewAudit.IsEnabled = true;
            }
        }
        private void ResizeGridViewColumn(GridViewColumn column)
        {
            if (double.IsNaN(column.Width))
            {
                column.Width = column.ActualWidth;
            }
            column.Width = double.NaN;
        }
        private void dtpEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dt = Convert.ToDateTime(dtpEndDate.SelectedDate);
            EndDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpEndTime.SelectedHour, tmpEndTime.SelectedMinute, tmpEndTime.SelectedSecond);
        }

        private void dtpStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dt = Convert.ToDateTime(dtpStartDate.SelectedDate);
            StartDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpStartTime.SelectedHour, tmpStartTime.SelectedMinute, tmpStartTime.SelectedSecond);
        }

        private void tmpEndTime_SelectedTimeChanged(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            DateTime dt = Convert.ToDateTime(dtpEndDate.SelectedDate);
            EndDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpEndTime.SelectedHour, tmpEndTime.SelectedMinute, tmpEndTime.SelectedSecond);
        }

        private void tmpStartTime_SelectedTimeChanged(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            DateTime dt = Convert.ToDateTime(dtpStartDate.SelectedDate);
            StartDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpStartTime.SelectedHour, tmpStartTime.SelectedMinute, tmpStartTime.SelectedSecond);
        }

        private void GetAuditReport(DateTime dtFromDate, DateTime dtToDate, string sModuleID)
        {
            try
            {
                LogManager.WriteLog("Inside GetAuditReport method", LogManager.enumLogLevel.Info);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                DataSet dtDataset = objReports.GetAuditTrailReport(dtFromDate, dtToDate, sModuleID);

                if (dtDataset.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowAuditTrailReport("AuditTrail", dtDataset, dtFromDate, dtToDate, ((BMC.Transport.FillModules)(cmbModules.SelectedItem)).Audit_Module_Name);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.Show();
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnShowReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnShowReport.IsEnabled = false;
                #region CR_#93510 (A.Vinod Kumar - 12/11/2011)
                /* <CR_#93510>                  
                 * 1. Exact records between from and to date
                 */
                // since we don't have the milliseconds in the UI, remove it from the date
                StartDate = StartDate.AddMilliseconds(-1 * StartDate.Millisecond);
                EndDate = EndDate.AddMilliseconds(-1 * EndDate.Millisecond);
                /* </CR_#93510> */
                #endregion

                if (EndDate > DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    return;
                }
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID286", BMC_Icon.Information);
                    return;
                }

                #region CR_#93510 (A.Vinod Kumar - 12/11/2011)
                /* <CR_#93510>                  
                 * 1. Exact records between from and to date
                 */
                // after date comparision add 999 milliseconds to todate,
                // otherwise will not get the records between milliseconds
                EndDate = EndDate.AddMilliseconds(999);
                /* </CR_#93510> */
                #endregion

                if (grdAudit.IsVisible)
                {
                    GetAuditReport(StartDate, EndDate, (cmbModules.SelectedItem == null ||
                        cmbModules.SelectedValue.ToString() == "0") ? string.Empty : cmbModules.SelectedValue.ToString());
                }
                else
                {
                    GetAFTAuditReport(StartDate, EndDate);
                }
            }
            finally
            {
                btnShowReport.IsEnabled = true;
            }
        }

        private void GetAFTAuditReport(DateTime dtFromDate, DateTime dtToDate)
        {
            try
            {
                LogManager.WriteLog("Inside GetAFTAuditReport method", LogManager.enumLogLevel.Info);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                DataSet dtDataset = objReports.GetAFTAuditTrailReport(dtFromDate, dtToDate);

                if (dtDataset.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowAFTAuditTrailReport("AFTAuditTrail", dtDataset, dtFromDate, dtToDate);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.Show();
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
                        ((BMC.Presentation.CAuditView)(this)).Loaded -= (this.Window_Loaded);
                        this.btnExit.Click -= (this.btnExit_Click);
                        this.dtpStartDate.SelectedDateChanged -= (this.dtpStartDate_SelectedDateChanged);
                        this.tmpStartTime.SelectedTimeChanged -= (this.tmpStartTime_SelectedTimeChanged);
                        this.dtpEndDate.SelectedDateChanged -= (this.dtpEndDate_SelectedDateChanged);
                        this.tmpEndTime.SelectedTimeChanged -= (this.tmpEndTime_SelectedTimeChanged);
                        this.btnViewAudit.Click -= (this.btnViewAudit_Click);
                        this.btnShowReport.Click -= (this.btnShowReport_Click);
                        this.btnToggle.Click -= (this.btnToggle_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CAuditView objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CAuditView"/> is reclaimed by garbage collection.
        /// </summary>
        ~CAuditView()
        {
            Dispose(false);
        }

        #endregion

    }

    public class AuditDetailsComparer : ListViewCustomComparer<GetAuditDetailsResult>
    {
        /// Compares the specified x to y.        
        public override int Compare(GetAuditDetailsResult x, GetAuditDetailsResult y)
        {
            String valueX = String.Empty, valueY = String.Empty;
            try
            {
                switch (SortBy)
                {
                    default:
                        valueX = x.Audit_ID.ToString();
                        valueY = y.Audit_ID.ToString();
                        break;
                    case "Audit_ID":
                        valueX = x.Audit_ID.ToString();
                        valueY = y.Audit_ID.ToString();
                        break;
                    case "Verificare_l'operatore_l'identità":
                    case "Audit_User_ID":
                        valueX = x.Audit_User_ID.ToString();
                        valueY = y.Audit_User_ID.ToString();
                        break;
                    case "Nome di operatore":
                    case "User Name":
                        valueX = x.Audit_User_Name;
                        valueY = y.Audit_User_Name;
                        break;
                    case "Data":
                    case "Date":
                        valueX = x.Audit_Date.ToString();
                        valueY = y.Audit_Date.ToString();
                        break;
                    case "Module Name":
                    case "Nome di modulo":
                        valueX = x.Audit_Module_Name;
                        valueY = y.Audit_Module_Name;
                        break;
                    case "Screen Name":
                    case "Schermare il Nome":
                        valueX = x.Audit_Screen_Name;
                        valueY = y.Audit_Screen_Name;
                        break;
                    case "Descrizione":
                    case "Description":
                        valueX = x.Audit_Desc;
                        valueY = y.Audit_Desc;
                        break;
                    case "Slot":
                        valueX = x.Audit_Slot;
                        valueY = y.Audit_Slot;
                        break;
                    case "Campo":
                    case "Field":
                        valueX = x.Audit_Field;
                        valueY = y.Audit_Field;
                        break;
                    case "Vecchio Valore":
                    case "Old Value":
                        valueX = x.Audit_Old_Vl;
                        valueY = y.Audit_Old_Vl;
                        break;
                    case "Nuovo Valore":
                    case "New Value":
                        valueX = x.Audit_New_Vl;
                        valueY = y.Audit_New_Vl;
                        break;
                    case "Tipo di operazione":
                    case "Operation Type":
                        valueX = x.Audit_Operation_Type;
                        valueY = y.Audit_Operation_Type;
                        break;
                    //case "Old Value":
                    //    if (SortDirection.Equals(ListSortDirection.Ascending)) return x.Audit_Operation_Type.CompareTo(y.Audit_Operation_Type);
                    //    else return (-1) * x.Audit_Operation_Type.CompareTo(y.Audit_Operation_Type);
                }

                if (SortDirection.Equals(ListSortDirection.Ascending)) return String.Compare(valueX, valueY);
                else return (-1) * String.Compare(valueX, valueY);

            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
    public class AFTAuditDetailsComparer : ListViewCustomComparer<GetAFTAuditDetailsResult>
    {
        public override int Compare(GetAFTAuditDetailsResult x, GetAFTAuditDetailsResult y)
        {
            String valueX = String.Empty, valueY = String.Empty;
            try
            {
                switch (SortBy)
                {
                    default:
                        valueX = x.AFT_Audit_ID.ToString();
                        valueY = y.AFT_Audit_ID.ToString();
                        break;
                    case "Audit_ID":
                        valueX = x.AFT_Audit_ID.ToString();
                        valueY = y.AFT_Audit_ID.ToString();
                        break;
                    case "Data":
                    case "Date":
                        valueX = x.AFT_TransactionDate.ToString();
                        valueY = y.AFT_TransactionDate.ToString();
                        break;
                    case "Transaction Type":
                    case "Tipo di transazione":
                        valueX = x.AFT_TransactionType;
                        valueY = y.AFT_TransactionType;
                        break;
                    case "CashableAmount":
                        // case "Importo":
                        valueX = x.CashableAmount.ToString();
                        valueY = y.CashableAmount.ToString();
                        break;
                    case "NonCashableAmount":
                        // case "Importo":
                        valueX = x.NonCashableAmount.ToString();
                        valueY = y.NonCashableAmount.ToString();
                        break;
                    case "WATAmount":
                        // case "Importo":
                        valueX = x.WATAmount.ToString();
                        valueY = y.WATAmount.ToString();
                        break;
                    case "Player_ID":
                        valueX = x.AFT_PlayerID.ToString();
                        valueY = y.AFT_PlayerID.ToString();
                        break;
                    case "Nome del giocatore":
                    case "Player Name":
                        valueX = x.AFT_PlayerName;
                        valueY = y.AFT_PlayerName;
                        break;
                    case "Errore Messaggio":
                    case "Error Message":
                        valueX = x.AFT_Error_Message;
                        valueY = y.AFT_Error_Message;
                        break;

                }

                if (SortDirection.Equals(ListSortDirection.Ascending)) return String.Compare(valueX, valueY);
                else return (-1) * String.Compare(valueX, valueY);

            }
            catch (Exception)
            {
                return 0;
            }
        }
    }

}
