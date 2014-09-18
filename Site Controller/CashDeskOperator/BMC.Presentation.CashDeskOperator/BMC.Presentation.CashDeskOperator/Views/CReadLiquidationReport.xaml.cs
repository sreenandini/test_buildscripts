using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Views;
using BMC.CommonLiquidation.Utilities;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CReadLiquidationReport.xaml
    /// </summary>
    public partial class CReadLiquidationReport : UserControl
    {
        public CReadLiquidationReport()
        {
            InitializeComponent();
        }

        #region Events

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetReadLiquidationReportRecords();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstReadReport.Items.Count <= 0)
                {
                    MessageBox.ShowBox("MessageID526", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    ReadLiquidationReportRecords lstReadLiqRecords = lstReadReport.SelectedItem as ReadLiquidationReportRecords;
                    if (lstReadLiqRecords == null)
                    {
                        MessageBox.ShowBox("MessageID527", BMC_Icon.Information);
                        return;
                    }

                    cReportViewer.ShowLiquidationReportForRead(null, lstReadLiqRecords.ReadId);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.Show();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkLast20_Checked(object sender, RoutedEventArgs e)
        {
            GetReadLiquidationReportRecords();
        }

        private void chkLast20_Unchecked(object sender, RoutedEventArgs e)
        {
            GetReadLiquidationReportRecords();
        }

        #endregion //Events

        #region Methods

        private void GetReadLiquidationReportRecords()
        {
            try
            {
                IReports objReports = ReportsBusinessObject.CreateInstance();
                lstReadReport.DataContext = objReports.GetReadLiquidationReportRecords(Convert.ToBoolean(chkLast20.IsChecked));
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Methods
    }
}
