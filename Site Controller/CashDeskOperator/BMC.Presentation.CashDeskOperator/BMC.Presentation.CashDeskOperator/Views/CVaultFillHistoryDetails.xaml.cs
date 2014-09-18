using BMC.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport;
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

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CCassettesDetails.xaml
    /// </summary>
    public partial class CVaultFillHistoryDetails : Window
    {
        private long _TransactionID = 0;
       
        public CVaultFillHistoryDetails(long TransactionID,string VaultName,string Transactiontype)
        {
            InitializeComponent();
            _TransactionID = TransactionID;
            txtVault.Text = VaultName;
            txtTransactionType.Text = Transactiontype;
            txtTransactionID.Text = TransactionID.ToString();
            clm_TotalAmountOnFill.Width = 0;
            clm_vault_Balance.Width = 0;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTransactionDetails();
        }

        private void LoadTransactionDetails()
        {
            try
            {
                LogManager.WriteLog("CVaultFillHistoryDetails->LoadTransactionDetails()", LogManager.enumLogLevel.Debug);

                List<rsp_Vault_GetFillHistoryDetailsResult> lst_details = Vault.CreateInstance().GetFillHistoryDetails(_TransactionID);
                lvw_FillHistoryDetails.ItemsSource = lst_details;
                this.DataContext = lst_details;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

    }
}
