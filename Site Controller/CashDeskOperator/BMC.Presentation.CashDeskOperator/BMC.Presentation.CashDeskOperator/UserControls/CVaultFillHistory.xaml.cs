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
using BMC.Business.CashDeskOperator;
using System.Data;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Views;
using BMC.Presentation.POS.Helper_classes;


namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CVaultFillHistory.xaml
    /// </summary>
    public partial class CVaultFillHistory : UserControl
    {
        VaultBiz objVaultBiz = null;

        public CVaultFillHistory()
        {
            InitializeComponent();
        }

    
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                objVaultBiz = new VaultBiz();
                DataTable dtHistory = objVaultBiz.GetFillHistory(0, 20, 0);
                lst_FillHistory.ItemsSource = dtHistory.DefaultView;
                string[] str = new string[] { "20", "30", "40", "50", "60", "100", "All" };
                cmb_SelectTop.ItemsSource = str;
                cmb_SelectTop.SelectedIndex = 0;
                LoadTransactionTypes();
                btnDetails.Visibility = Visibility.Hidden;
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }

        }


        private void btn_ResetVault_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lst_FillHistory.ItemsSource = null;
                DataTable dtHistory = null;
                if (cmbTransType.SelectedItem != null && cmb_SelectTop.SelectedValue != null)
                {

                    if (cmb_SelectTop.SelectedValue.ToString() == "All")
                    {
                        dtHistory = objVaultBiz.GetFillHistory(0, 0, ((rsp_Vault_GetTransactionTypesResult)cmbTransType.SelectedItem).TYPE_ID);
                    }
                    else
                    {
                        dtHistory = objVaultBiz.GetFillHistory(0, int.Parse(cmb_SelectTop.SelectedValue.ToString()), ((rsp_Vault_GetTransactionTypesResult)cmbTransType.SelectedItem).TYPE_ID);
                    }
                    lst_FillHistory.ItemsSource = dtHistory.DefaultView;
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dtHistory = null;
                if (cmb_SelectTop.SelectedValue.ToString() == "All")
                {
                    dtHistory = objVaultBiz.GetFillHistory(0, 0, ((rsp_Vault_GetTransactionTypesResult)cmbTransType.SelectedItem).TYPE_ID);
                }
                else
                {
                    dtHistory = objVaultBiz.GetFillHistory(0, int.Parse(cmb_SelectTop.SelectedValue.ToString()), ((rsp_Vault_GetTransactionTypesResult)cmbTransType.SelectedItem).TYPE_ID);
                }

                if (dtHistory.Rows.Count == 0 || dtHistory.Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                    if (cmb_SelectTop.SelectedValue.ToString() == "All")
                    {
                        cReportViewer.ShowVaultFillHistory(dtHistory, 0, 0);
                    }
                    else
                        cReportViewer.ShowVaultFillHistory(dtHistory, 0, int.Parse(cmb_SelectTop.SelectedValue.ToString()));
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.Show();
                }
                LogManager.WriteLog("ShowLiquidationReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lst_FillHistory.SelectedItem != null)
                {

                    DataRowView dr_select = (System.Data.DataRowView)lst_FillHistory.SelectedItem;
                    long Transaction_ID = Convert.ToInt64(dr_select.Row["Fill_ID"]);

                    CVaultFillHistoryDetails c_details = new CVaultFillHistoryDetails(Transaction_ID,
                        dr_select.Row["Name"].ToString(),
                        dr_select.Row["Type"].ToString());
                    c_details.Owner = Window.GetWindow(this);
                    c_details.ShowDialog();
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void lst_FillHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView dr_select = (System.Data.DataRowView)lst_FillHistory.SelectedItem;
                bool details_enable = Convert.ToBoolean(dr_select.Row["IsWebserviceEnabled"]);
                btnDetails.Visibility = details_enable ? Visibility.Visible : Visibility.Hidden;
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        public void LoadTransactionTypes()
        {
            try
            {
                LogManager.WriteLog("CVaultFillHistory--> LoadTransactionTypes()", LogManager.enumLogLevel.Debug);
                List<rsp_Vault_GetTransactionTypesResult> lst_trans = objVaultBiz.GetTransactionTypes();
                if (lst_trans.Count > 0)
                {
                    lst_trans.Insert(0, new rsp_Vault_GetTransactionTypesResult
                    {
                        TYPE_ID = 0,
                        Type_Description = "ALL"

                    });
                }
                cmbTransType.ItemsSource = lst_trans;
                cmbTransType.DisplayMemberPath = "Type_Description";
                if (lst_trans.Count > 0)
                {
                    cmbTransType.SelectedIndex = 0;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmb_SelectTop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btn_ResetVault_Click(this, null);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbTransType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btn_ResetVault_Click(this, null);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


    }
}
