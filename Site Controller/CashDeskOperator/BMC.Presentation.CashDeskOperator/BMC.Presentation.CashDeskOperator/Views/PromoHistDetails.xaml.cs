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
using System.Data.Linq;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.ExceptionManagement;


namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for PromoHistDetails.xaml
    /// </summary>
    public partial class PromoHistDetails : Window
    {
        BMC.Business.CashDeskOperator.Promotional objPromotioal = new BMC.Business.CashDeskOperator.Promotional();
        Promotional objprom = new Promotional();
        public int RowSelectedTicketId;
        public PromoHistDetails(int TicketId,string strPromoName)
        {
            try
            {
                InitializeComponent();
                RowSelectedTicketId = TicketId;
                ISingleResult<PromotionalClassHistoryDetails> lstPromotionalHistoryDetails = objprom.BGetPromoHistoryDetails(RowSelectedTicketId);
                dgPromoHistDetGridView.DataContext = lstPromotionalHistoryDetails as ISingleResult<PromotionalClassHistoryDetails>;
                txtHeader.Text = strPromoName;
                if (lstPromotionalHistoryDetails == null) return;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
     
    }
}
