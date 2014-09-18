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
using BMC.Business.CashDeskManager;
using BMC.Transport;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CashDeskManagerAllDetails.xaml
    /// </summary>
    public partial class CashDeskManagerAllDetails : UserControl
    {
        #region "Declarations"
      
        string TREASURY_REFILL = "Refill";
        string TREASURY_REFUND = "Refund";
        string TREASURY_PRIZE_CARD_REFILL = "Prize Card Refill";
        string TREASURY_HANDPAY_JACKPOT = "Handpay Jackpot";
        string TREASURY_HANDPAY_CREDIT = "Handpay Credit";
        string TREASURY_SHORTPAY = "Shortpay";
        string TREASURY_CASH_DESK_FLOAT = "Cash Desk Float";
        string TREASURY_FLOAT = "Float";
        string TREASURY_PROGRESSIVE = "Prog";
        string TREASURY_JACKPOT = "Handpay Jackpot";
        string MYFORMAT = "#,##0.00";
        TreasuryTransactions busTreasury;
        string RouteNumber = string.Empty;
        string StartDate = string.Empty;
        string EndDate = string.Empty;
        string StartTime = string.Empty;
        string EndTime = string.Empty;
        bool isChkHandpays = false;
        bool isChkRefills = false;
        bool ischkRefunds = false;
        bool ischkCashDeskFloat = false;
        bool ischkJackpot = false;
        bool ischkProgressive = false;
        bool ischkShortpays = false;
        List<TicketExceptions> lstTickets = null;

        #endregion
        public CashDeskManagerAllDetails()
        {
            InitializeComponent();
        }

        public CashDeskManagerAllDetails(List<TicketExceptions> lstTickets)
        {
            InitializeComponent();
            this.lstTickets = lstTickets;
        }

        public CashDeskManagerAllDetails(string RouteNumber, string StartDate, string EndDate, string StartTime, string EndTime,
             bool isChkRefills,
            bool ischkRefunds,
            bool ischkCashDeskFloat,
            bool ischkJackpot,
            bool ischkProgressive,
            bool ischkShortpays)
        {
            InitializeComponent();


            this.RouteNumber = RouteNumber;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.isChkRefills = isChkRefills;
            this.ischkRefunds = ischkRefunds;
            this.ischkCashDeskFloat = ischkCashDeskFloat;
            this.ischkJackpot = ischkJackpot;
            this.ischkProgressive = ischkProgressive;
            this.ischkShortpays = ischkShortpays;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (lstTickets != null)
            {
                lvViewAll.ItemsSource = lstTickets;
            }
        }
    }
}
