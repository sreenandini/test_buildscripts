using System;
using System.Windows;
using System.Windows.Controls;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator;
using BMC.Presentation.POS.Views;
using System.Data;
using Microsoft.Windows.Controls;
using AC.AvalonControlsLibrary.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using BMC.Security;
using BMC.Presentation.POS.Helper_classes;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CCustomReports : UserControl, IDisposable
    {
        #region Private Declarations

        private DateTime StartDate = DateTime.Now;
        private DateTime EndDate = DateTime.Now;
        private int cmbReportsSelectedIndex = 0;
        private string SiteCode = string.Empty;
        #endregion

        #region Constructor

        public CCustomReports()
        {
            InitializeComponent();
            SiteCode = Settings.SiteCode;
        }

        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
              var choices = new SortedDictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.ExpenseDetailReport"))
                choices["ExpenseDetailReport"] = "Expense Detail Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.ExpiredVoucherCouponReport"))
                choices["ExpiredVoucherCouponReport"] = "Expired Voucher/Coupon Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.JackpotSlipSummaryReport"))
                choices["JackpotSlipSummaryReport"] = "Jackpot Slip Summary Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.MeterListReport"))
                choices["MeterListReport"] = "Meter List Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.RedeemedTicketByDeviceReport"))
                choices["RedeemedTicketByDeviceReport"] = "Redeemed Voucher by Device Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.TicketIssuedReport"))
                choices["TicketIssuedReport"] = "Voucher Listing Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.VoucherCouponLiabilityReport"))
                choices["VoucherCouponLiabilityReport"] = "Voucher/Coupon Liability Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.ExceptionVoucherDetails"))
                choices["ExceptionVoucherDetailReport"] = "Exception Voucher Details Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferSummaryReport"))
                choices["CrossPropertyLiabilityTransferSummaryReport"] = "Cross Property Liability Transfer Summary Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyLiabilityTransferDetailsReport"))
                choices["CrossPropertyLiabilityTransferDetailsReport"] = "Cross Property Liability Transfer Details Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.CrossPropertyTicketAnalysisReport"))
                choices["CrossPropertyTicketAnalysisReport"] = "Cross Property Ticket Analysis Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.StackerLevelDetailsReport"))
                choices["StackerLevelDetailsReport"] = "StackerLevel Details Report";
            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports.AccountingWinLossReport"))
                choices["AccountingWinLossReport"] = "Accounting Machine Win/Loss Report";

            if (choices.Count != 0)
            {
                cmbReports.ItemsSource = new System.Windows.Forms.BindingSource(choices, null);
                cmbReports.DisplayMemberPath = "Value";
                cmbReports.SelectedValuePath = "Key";
                cmbReports.SelectedIndex = 0;
            }            
        }

        private void cmbReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                switch (cmbReports.SelectedValue.ToString())
                {
                    case "ExpiredVoucherCouponReport":
                        {
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Visible;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;

                            dtpFromDate_Expired.Text = DateTime.Now.AddDays(-1).ToString();
                            dtpFromDate_Expired.DisplayDate = DateTime.Now.AddDays(-1);
                            tmpFromDate_Expired.SelectedTime = new TimeSpan(00, 00, 00);

                            dtpToDate_Expired.Text = DateTime.Now.ToString();
                            dtpToDate_Expired.DisplayDate = DateTime.Now;
                            tmpToDate_Expired.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                            var choices = new Dictionary<string, string>();
                            choices["ALL"] = "ALL";
                            choices["CASHDESK"] = "CASH DESK";
                            choices["SLOT"] = "SLOT";

                            cmbDeviceType_Expired.ItemsSource = new System.Windows.Forms.BindingSource(choices, null);
                            cmbDeviceType_Expired.DisplayMemberPath = "Value";
                            cmbDeviceType_Expired.SelectedValuePath = "Key";
                            cmbDeviceType_Expired.SelectedIndex = 0;


                            break;
                        }
                    case "VoucherCouponLiabilityReport":
                        {
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Visible;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;

                            dtpIssueDate_liability.Text = DateTime.Now.ToString();
                            dtpIssueDate_liability.DisplayDate = DateTime.Now;
                            tmpIssueDate_liability.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                            var choices = new Dictionary<string, string>();
                            choices["ALL"] = "ALL";
                            choices["CASHDESK"] = "CASH DESK";
                            choices["SLOT"] = "SLOT";

                            cmbDeviceType_liability.ItemsSource = new System.Windows.Forms.BindingSource(choices, null);
                            cmbDeviceType_liability.DisplayMemberPath = "Value";
                            cmbDeviceType_liability.SelectedValuePath = "Key";
                            cmbDeviceType_liability.SelectedIndex = 0;

                            var choices2 = new Dictionary<string, string>();
                            choices2["ALL"] = "ALL";
                            choices2["ACTIVE"] = "ACTIVE";
                            choices2["CANCELLED"] = "CANCELLED";
                            choices2["EXPIRED"] = "EXPIRED";
                            choices2["VOID"] = "VOID";

                            cmbVoucherStatus_liability.ItemsSource = new System.Windows.Forms.BindingSource(choices2, null);
                            cmbVoucherStatus_liability.DisplayMemberPath = "Value";
                            cmbVoucherStatus_liability.SelectedValuePath = "Key";
                            cmbVoucherStatus_liability.SelectedIndex = 0;

                            break;
                        }
                    case "RedeemedTicketByDeviceReport":
                        {
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Visible;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;

                            dtpFromDate_Redeem.Text = DateTime.Now.AddDays(-1).ToString();
                            dtpFromDate_Redeem.DisplayDate = DateTime.Now.AddDays(-1);
                            tmpFromDate_Redeem.SelectedTime = new TimeSpan(00, 00, 00);

                            dtpToDate_Redeem.Text = DateTime.Now.ToString();
                            dtpToDate_Redeem.DisplayDate = DateTime.Now;
                            tmpToDate_Redeem.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                            var choices = new Dictionary<string, string>();
                            choices["ALL"] = "ALL";
                            choices["CASHDESK"] = "CASH DESK";
                            choices["SLOT"] = "SLOT";

                            cmbDeviceType_Redeem.ItemsSource = new System.Windows.Forms.BindingSource(choices, null);
                            cmbDeviceType_Redeem.DisplayMemberPath = "Value";
                            cmbDeviceType_Redeem.SelectedValuePath = "Key";
                            cmbDeviceType_Redeem.SelectedIndex = 0;


                            break;
                        }
                    case "ExpenseDetailReport":
                        {
                            ExpenseDetailsReportGrid.Visibility = Visibility.Visible;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;

                            dtpReportDate_ExpenseDetail.Text = DateTime.Now.ToString();
                            dtpReportDate_ExpenseDetail.DisplayDate = DateTime.Now;

                            var choices = new Dictionary<string, string>();
                            choices["ALL"] = "ALL";
                            choices["DAY"] = "DAY";
                            choices["PTD"] = "PTD";
                            choices["MTD"] = "MTD";
                            choices["QTD"] = "QTD";
                            choices["YTD"] = "YTD";
                            choices["LTD"] = "LTD";

                            cmbExpensePeriod.ItemsSource = new System.Windows.Forms.BindingSource(choices, null);
                            cmbExpensePeriod.DisplayMemberPath = "Value";
                            cmbExpensePeriod.SelectedValuePath = "Key";

                            cmbExpensePeriod.SelectedIndex = 0;

                            break;
                        }

                    case "JackpotSlipSummaryReport":
                        {
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Visible;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;

                            dtpReportStartDate_JackpotSlipSummary.Text = DateTime.Now.AddDays(-1).ToString();
                            dtpReportStartDate_JackpotSlipSummary.DisplayDate = DateTime.Now.AddDays(-1);
                            tpReportStartTime.SelectedTime = new TimeSpan(00, 00, 00);

                            dtpReportEndDate_JackpotSlipSummary.Text = DateTime.Now.ToString();
                            dtpReportEndDate_JackpotSlipSummary.DisplayDate = DateTime.Now;
                            tpReportEndTime.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                            break;
                        }

                    case "TicketIssuedReport":
                        {
                            TicketIssuedReportGrid.Visibility = Visibility.Visible;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;

                            dtpFromDate_Issued.Text = DateTime.Now.AddDays(-1).ToString();
                            dtpFromDate_Issued.DisplayDate = DateTime.Now.AddDays(-1);
                            tmpFromDate_Issued.SelectedTime = new TimeSpan(00, 00, 00);

                            dtpToDate_Issued.Text = DateTime.Now.ToString();
                            dtpToDate_Issued.DisplayDate = DateTime.Now;
                            tmpToDate_Issued.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                            cmbVoucherStatus_Issued.SelectedIndex = 0;

                            if (cmbSlot_Issued.Items.Count == 1)
                            {
                                GetSlots(cmbSlot_Issued);
                            }
                            cmbSlot_Issued.SelectedIndex = 0;

                            break;
                        }

                    case "MeterListReport":
                        {
                            MeterListReportGrid.Visibility = Visibility.Visible;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;

                            FillAssetNumberCombo();

                            break;
                        }
                    case "ExceptionVoucherDetailReport":
                        {
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Visible;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;
                            dtpFromDate_Exception.Text = DateTime.Now.AddDays(-1).ToString();
                            dtpFromDate_Exception.DisplayDate = DateTime.Now.AddDays(-1);
                            tmpFromDate_Exception.SelectedTime = new TimeSpan(00, 00, 00);

                            dtpToDate_Exception.Text = DateTime.Now.ToString();
                            dtpToDate_Exception.DisplayDate = DateTime.Now;
                            tmpToDate_Exception.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                            FillBatchNumberCombo(DateTime.Now.Date.AddDays(-1), DateTime.Now,Convert.ToBoolean(chkDropBased.IsChecked));
                            break;
                        }
                    case "CrossPropertyLiabilityTransferSummaryReport":
                        {
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Visible;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;

                            dtpFromDate_TSR.Text = DateTime.Now.AddDays(-1).ToString();
                            dtpFromDate_TSR.DisplayDate = DateTime.Now.AddDays(-1);
                            tmpFromDate_TSR.SelectedTime = new TimeSpan(00, 00, 00);

                            dtpToDate_TSR.Text = DateTime.Now.ToString();
                            dtpToDate_TSR.DisplayDate = DateTime.Now;
                            tmpToDate_TSR.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                          
                            break;
                        } 
                    case "CrossPropertyLiabilityTransferDetailsReport":
                        {
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Visible;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;


                            dtpFromDate_TDR.Text = DateTime.Now.AddDays(-1).ToString();
                            dtpFromDate_TDR.DisplayDate = DateTime.Now.AddDays(-1);
                            tmpFromDate_TDR.SelectedTime = new TimeSpan(00, 00, 00);

                            dtpToDate_TDR.Text = DateTime.Now.ToString();
                            dtpToDate_TDR.DisplayDate = DateTime.Now;
                            tmpToDate_TDR.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                            
                            break;
                        }
                    case "CrossPropertyTicketAnalysisReport":
                        {
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Visible;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;

                            dtpFromDate_TAR.Text = DateTime.Now.AddDays(-1).ToString();
                            dtpFromDate_TAR.DisplayDate = DateTime.Now.AddDays(-1);
                            tmpFromDate_TAR.SelectedTime = new TimeSpan(00, 00, 00);

                            dtpToDate_TAR.Text = DateTime.Now.ToString();
                            dtpToDate_TAR.DisplayDate = DateTime.Now;
                            tmpToDate_TAR.SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                            
                            break;
                        }

                    case "StackerLevelDetailsReport":                    
                        {
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Visible;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            AccountingWinLossReportGrid.Visibility = Visibility.Hidden;

                            cmbStackerLevel.Items.Clear();
                            for (int i = 0; i <= 100; i++)
                                cmbStackerLevel.Items.Add(i);
                            cmbStackerLevel.SelectedIndex = 0;
                            break;
                        }
                    case "AccountingWinLossReport":
                        {
                            AccountingWinLossReportGrid.Visibility = Visibility.Visible;
                            CrossPropertyTicketAnalysisReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferDetailsReportGrid.Visibility = Visibility.Hidden;
                            CrossPropertyLiabilityTransferSummaryReportGrid.Visibility = Visibility.Hidden;
                            ExceptionVoucherDetailReportGrid.Visibility = Visibility.Hidden;
                            ExpiredVoucherCouponReportGrid.Visibility = Visibility.Hidden;
                            RedeemedTicketByDeviceReportGrid.Visibility = Visibility.Hidden;
                            VoucherCouponLiabilityReportGrid.Visibility = Visibility.Hidden;
                            ExpenseDetailsReportGrid.Visibility = Visibility.Hidden;
                            TicketIssuedReportGrid.Visibility = Visibility.Hidden;
                            JackpotSlipSummaryReportGrid.Visibility = Visibility.Hidden;
                            MeterListReportGrid.Visibility = Visibility.Hidden;
                            StackerLevelDetailsReportGrid.Visibility = Visibility.Hidden;

                            dtpFromDate_AWLR.Text = DateTime.Now.AddDays(-1).ToString();
                            dtpFromDate_AWLR.DisplayDate = DateTime.Now.AddDays(-1);

                            dtpToDate_AWLR.Text = DateTime.Now.ToString();
                            dtpToDate_AWLR.DisplayDate = DateTime.Now;

                            LoadAccWinLossComboBoxes();

                            break;
                        }
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

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;

                switch (cmbReports.SelectedValue.ToString())
                {
                    case "ExpiredVoucherCouponReport":
                        {

                            if (dtpFromDate_Expired.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpFromDate_Expired.Focus();
                                return;
                            }

                            if (dtpToDate_Expired.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpToDate_Expired.Focus();
                                return;
                            }


                            GetExpiredVoucherCouponReport(GetDateTime(dtpFromDate_Expired, tmpFromDate_Expired),
                                GetDateTime(dtpToDate_Expired, tmpToDate_Expired), (cmbDeviceType_Expired.SelectedValue.ToString()));

                            break;
                        }
                    case "VoucherCouponLiabilityReport":
                        {
                            if (dtpIssueDate_liability.SelectedDate != null)
                            {
                                GetVoucherCouponLiabilityReport(GetDateTime(dtpIssueDate_liability, tmpIssueDate_liability), cmbDeviceType_liability.SelectedValue.ToString(),
                                    cmbVoucherStatus_liability.SelectedValue.ToString());
                            }
                            else
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpIssueDate_liability.Focus();
                                return;
                            }
                            break;
                        }
                    case "RedeemedTicketByDeviceReport":
                        {
                            if (dtpFromDate_Redeem.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpFromDate_Redeem.Focus();
                                return;
                            }
                            if (dtpToDate_Redeem.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpToDate_Redeem.Focus();
                                return;
                            }


                            GetRedeemedTicketByDeviceReport(GetDateTime(dtpFromDate_Redeem, tmpFromDate_Redeem),
                                GetDateTime(dtpToDate_Redeem, tmpToDate_Redeem), (cmbDeviceType_Redeem.SelectedValue.ToString()));

                            break;
                        }
                    case "ExpenseDetailReport":
                        {
                            if (dtpReportDate_ExpenseDetail.SelectedDate != null)
                            {
                                ShowExpenseDetailsReport(dtpReportDate_ExpenseDetail.SelectedDate.Value, cmbExpensePeriod.SelectedValue.ToString(),chkIsGamingDay.IsChecked.Value,SiteCode);
                            }
                            else
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpReportDate_ExpenseDetail.Focus();
                                return;
                            }
                            break;
                        }
                    case "TicketIssuedReport":
                        {
                            if (dtpFromDate_Issued.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpFromDate_Redeem.Focus();
                                return;
                            }
                            if (dtpToDate_Issued.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpToDate_Redeem.Focus();
                                return;
                            }

                            //GetIssuedTicketReport(GetDateTime(dtpFromDate_Issued, tmpFromDate_Issued), GetDateTime(dtpToDate_Issued, tmpToDate_Issued),
                            //        cmbVoucherStatus_Issued.SelectedValue.ToString(), cmbSlot_Issued.SelectedItem.ToString());
                            GetIssuedTicketReport(GetDateTime(dtpFromDate_Issued, tmpFromDate_Issued), GetDateTime(dtpToDate_Issued, tmpToDate_Issued),
                                     ((System.Windows.Controls.ComboBoxItem)cmbVoucherStatus_Issued.SelectedValue).Content.ToString(), cmbSlot_Issued.SelectedItem.ToString());

                            break;
                        }
                    case "JackpotSlipSummaryReport":
                        {
                            if (dtpReportStartDate_JackpotSlipSummary.SelectedDate != null && dtpReportEndDate_JackpotSlipSummary.SelectedDate != null)
                            {
                                ShowJackpotSlipSummaryReport(GetDateTime(dtpReportStartDate_JackpotSlipSummary, tpReportStartTime), GetDateTime(dtpReportEndDate_JackpotSlipSummary, tpReportEndTime));
                            }
                            else
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpReportStartDate_JackpotSlipSummary.Focus();
                                return;
                            }
                            break;
                        }
                    case "MeterListReport":
                        {
                            ShowMeterListReport(cmbAssetNumber.SelectedValue.ToString(), cmbAssetNumber.Text);
                            break;
                        }
                    case "ExceptionVoucherDetailReport":
                        {

                            if (dtpFromDate_Exception.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpFromDate_Exception.Focus();
                                return;
                            }

                            if (dtpToDate_Exception.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpToDate_Exception.Focus();
                                return;
                            }

                            Int32 BatchNumber =(cmbBatchNumber.SelectedValue == null 
                                || Convert.ToString(cmbBatchNumber.SelectedValue) == "ALL") ? 0 : Convert.ToInt32(cmbBatchNumber.SelectedValue);
                            ShowExceptionVoucherDetailReport(GetDateTime(dtpFromDate_Exception, tmpFromDate_Exception),
                                GetDateTime(dtpToDate_Exception, tmpToDate_Exception), BatchNumber, SiteCode);

                            break;
                        }
                    case "CrossPropertyLiabilityTransferSummaryReport":
                        {
                            if (dtpFromDate_TSR.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpFromDate_TSR.Focus();
                                return;
                            }

                            if (dtpToDate_TSR.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpToDate_TSR.Focus();
                                return;
                            }

                            ShowCrossPropertyLiabilityTransferSummaryReport(GetDateTime(dtpFromDate_TSR, tmpFromDate_TSR),
                                GetDateTime(dtpToDate_TSR, tmpToDate_TSR));

                            break;
                        }
                    case "CrossPropertyLiabilityTransferDetailsReport":
                        {
                            if (dtpFromDate_TDR.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpFromDate_TDR.Focus();
                                return;
                            }

                            if (dtpToDate_TDR.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpToDate_TDR.Focus();
                                return;
                            }

                            ShowCrossPropertyLiabilityTransferDetailsReport(GetDateTime(dtpFromDate_TDR, tmpFromDate_TDR),
                                GetDateTime(dtpToDate_TDR, tmpToDate_TDR));

                            break;
                        }
                    case "CrossPropertyTicketAnalysisReport":
                        {
                            if (dtpFromDate_TAR.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpFromDate_TAR.Focus();
                                return;
                            }

                            if (dtpToDate_TAR.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpToDate_TAR.Focus();
                                return;
                            }

                            ShowCrossPropertyTicketAnalysisReport(GetDateTime(dtpFromDate_TAR, tmpFromDate_TAR),
                                GetDateTime(dtpToDate_TAR, tmpToDate_TAR));
                            break;
                        }
                    case "StackerLevelDetailsReport":
                        {
                            ShowStackerLevelDetailReport(Convert.ToInt32(cmbStackerLevel.SelectedValue.ToString()));
                            break;
                        }
                    case "AccountingWinLossReport":
                        {
                            if (dtpFromDate_AWLR.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpFromDate_AWLR.Focus();
                                return;
                            }

                            if (dtpToDate_AWLR.SelectedDate == null)
                            {
                                MessageBox.ShowBox("Please enter a valid date.", BMC_Icon.Error, true);
                                dtpToDate_AWLR.Focus();
                                return;
                            }

                            ShowAccountingWinLossReport(Convert.ToInt32(cmbZone_AWLR.SelectedValue.ToString()),Convert.ToInt32(cmbCategory_AWLR.SelectedValue.ToString()),dtpFromDate_AWLR.SelectedDate.Value,
                                dtpToDate_AWLR.SelectedDate.Value, cmbZone_AWLR.Text, cmbCategory_AWLR.Text, chkNonCashable_AWLR.IsChecked.GetValueOrDefault());
                            break;
                        }
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

        private void ShowCrossPropertyTicketAnalysisReport(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                LogManager.WriteLog("Inside ShowCrossPropertyTicketAnalysisReport method", LogManager.enumLogLevel.Info);

                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID286", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID287", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet dtDataset = objReports.GetCrossPropertyTicketAnalysisReport(StartDate, EndDate);

                if (dtDataset.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowCrossPropertyTicketAnalysisReport("CrossPropertyTicketAnalysisReport", dtDataset, StartDate, EndDate);
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }

        private void ShowCrossPropertyLiabilityTransferDetailsReport(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                LogManager.WriteLog("Inside ShowCrossPropertyLiabilityTransferDetailsReport method", LogManager.enumLogLevel.Info);

                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID286", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID287", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet dtDataset = objReports.GetCrossPropertyLiabilityTransferDetailsReport(StartDate, EndDate);

                if (dtDataset.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowCrossPropertyLiabilityTransferDetailsReport("CrossPropertyLiabilityTransferDetailsReport", dtDataset, StartDate, EndDate);
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }

        private void ShowCrossPropertyLiabilityTransferSummaryReport(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                LogManager.WriteLog("Inside ShowCrossPropertyLiabilityTransferSummaryReport method", LogManager.enumLogLevel.Info);

                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID286", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID287", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet dtDataset = objReports.GetCrossPropertyLiabilityTransferSummaryReport(StartDate, EndDate);

                if (dtDataset.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowCrossPropertyLiabilityTransferSummaryReport("CrossPropertyLiabilityTransferDetailsReport", dtDataset, StartDate, EndDate);
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }

        private void ShowAccountingWinLossReport(int ZoneNo, int MachineCategoryNo, DateTime StartDate, DateTime EndDate, string ZoneName, string Category, bool IncludeNonCashable)
        {
            try
            {
                LogManager.WriteLog("Inside ShowAccountingWinLossReport method", LogManager.enumLogLevel.Info);

                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID286", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID287", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet dtDataset = objReports.GetAccountingWinLossReport(ZoneNo,MachineCategoryNo,StartDate, EndDate, IncludeNonCashable);

                if (dtDataset.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowAccountingWinLossReport("AccountingWinLossReport", dtDataset, ZoneNo, MachineCategoryNo, StartDate, EndDate, ZoneName, Category, IncludeNonCashable);
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }

        #endregion

        #region private methods

        private void GetSlots(ComboBox combo)
        {
            IReports objReports = ReportsBusinessObject.CreateInstance();
            objReports.GetSlots(combo);
        }

        private void FillBatchNumberCombo(DateTime StartDate, DateTime EndDate, bool isdeclared)
        {
            try
            {
                LogManager.WriteLog("Inside FillAssetNumberCombo method", LogManager.enumLogLevel.Info);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching Asset Numbers from database....", LogManager.enumLogLevel.Info);

                DataTable dtBatchNumbers = objReports.GetBatchNumber(StartDate, EndDate, isdeclared);

                if (dtBatchNumbers.Rows.Count > 0)
                {
                    LogManager.WriteLog("Batch Number fetched successfully from database", LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog("No Batch Numbers available to be fetched from database", LogManager.enumLogLevel.Error);
                }
                List<String> batches = new List<string>();
                batches.Add("ALL");
                foreach (DataRow dr in dtBatchNumbers.Rows)
                {
                    batches.Add(Convert.ToString(dr[0]));
                }
               LogManager.WriteLog("Setting Batch number combo box Item Source property", LogManager.enumLogLevel.Info);

                cmbBatchNumber.ItemsSource = batches;// ((System.ComponentModel.IListSource)dtBatchNumbers).GetList();
                cmbBatchNumber.DataContext = batches;//dtBatchNumbers.DefaultView;
                cmbBatchNumber.SelectedIndex = 0;

                LogManager.WriteLog("Batch number combo box Item Source property set successfully", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillAssetNumberCombo()
        {
            try
            {
                LogManager.WriteLog("Inside FillAssetNumberCombo method", LogManager.enumLogLevel.Info);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching Asset Numbers from database....", LogManager.enumLogLevel.Info);

                DataTable dtAssetNumbers = objReports.GetAssetNumberforActiveInstallations();

                if (dtAssetNumbers.Rows.Count > 0)
                {
                    LogManager.WriteLog("Asset numbers fetched successfully from database", LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog("No Asset numbers available to be fetched from database", LogManager.enumLogLevel.Error);
                }

                DataRow dataRow = dtAssetNumbers.NewRow();
                dataRow["InstallationNo"] = "ALL";
                dataRow["StockNo"] = "ALL";

                dtAssetNumbers.Rows.InsertAt(dataRow, 0);

                LogManager.WriteLog("Setting Asset number combo box Item Source property", LogManager.enumLogLevel.Info);

                cmbAssetNumber.ItemsSource = ((System.ComponentModel.IListSource)dtAssetNumbers).GetList();
                cmbAssetNumber.DataContext = dtAssetNumbers.DefaultView;
                cmbAssetNumber.SelectedIndex = 0;

                LogManager.WriteLog("Asset number combo box Item Source property set successfully", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void GetVoucherCouponLiabilityReport(DateTime dtIssueDate,string sDeviceType,string sVoucherStatus )
        {
            try
            {
                LogManager.WriteLog("Inside GetVoucherCouponLiabilityReport method", LogManager.enumLogLevel.Info);

                if (dtIssueDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID285", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet dtDetails = objReports.GetVoucherCouponLiabilityReport(dtIssueDate, sDeviceType, sVoucherStatus);

                if (dtDetails.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowVoucherCouponLiabilityReport("VoucherCouponLiabilityReport", dtDetails, dtIssueDate, sDeviceType, sVoucherStatus);
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }
        private void GetRedeemedTicketByDeviceReport(DateTime dtFromDate,DateTime dtToDate, string sDeviceType)
        {
            try
            {
                LogManager.WriteLog("Inside GetRedeemedTicketByDeviceReport method", LogManager.enumLogLevel.Info);

                if (dtFromDate > dtToDate)
                {
                    MessageBox.ShowBox("MessageID286", BMC_Icon.Information);
                    return;
                }

                if (dtFromDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID287", BMC_Icon.Information);
                    return;
                }

                if (dtToDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet dtDataset = objReports.GetRedeemedTicketByDevice(dtFromDate, dtToDate, sDeviceType);

                //if (dtDataset.Tables[0].Rows.Count == 0)
                //{
                //    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                //    return;
                //}

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowRedeemedTicketByDevice("RedeemedTicketByDevice", dtDataset, dtFromDate, dtToDate, sDeviceType,SiteCode);
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }

        private void GetExpiredVoucherCouponReport(DateTime dtFromDate, DateTime dtToDate, string sDeviceType)
        {
            try
            {
                LogManager.WriteLog("Inside GetExpiredVoucherCouponReport method", LogManager.enumLogLevel.Info);

                if (dtFromDate > dtToDate)
                {
                    MessageBox.ShowBox("MessageID286", BMC_Icon.Information);
                    return;
                }

                if (dtFromDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID287", BMC_Icon.Information);
                    return;
                }

                if (dtToDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet dtDataset = objReports.GetExpiredVoucherCouponReport(dtFromDate, dtToDate, sDeviceType);

                if (dtDataset.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowExpiredVoucherCoupon("ExpiredVoucherCoupon", dtDataset, dtFromDate, dtToDate, sDeviceType);
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }
        private void ShowStackerLevelDetailReport(int StackerLevel)
        {
            try
            {
                IReports objReports = ReportsBusinessObject.CreateInstance();
                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                DataSet dsStackerDetails = objReports.GetStackerDetails(StackerLevel);

                //if (dsStackerDetails.Tables[0].Rows.Count == 0)
                //{
                //    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);
                //    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                //    return;
                //}
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    cReportViewer.ShowStackerLevelDetailReport(dsStackerDetails, StackerLevel,SiteCode);
                    cReportViewer.ShowDialog();
                }

                LogManager.WriteLog("ShowStackerLevelDetailReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
				MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }

        }

        private void ShowExpenseDetailsReport(DateTime reportDate, string reportPeriod,bool IsGamingDayBasedReport,string SiteCode)
        {
            try
            {
                LogManager.WriteLog("Inside ShowExpenseDetailsReport method", LogManager.enumLogLevel.Info);

                if (reportDate > System.DateTime.Now)
                {   
                    MessageBox.ShowBox("MessageID281", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                DataSet expenseDetails = objReports.GetExpenseDetails(reportDate, reportPeriod, IsGamingDayBasedReport);

                if (expenseDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    cReportViewer.ShowExpenseDetailsReport(expenseDetails, reportDate, reportPeriod, IsGamingDayBasedReport,SiteCode);
                    cReportViewer.ShowDialog();                    
                }

				LogManager.WriteLog("ShowExpenseDetailsReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
				MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }

        private void ShowJackpotSlipSummaryReport(DateTime reportStartDateTime, DateTime reportEndDateTime)
        {
            try
            {
                LogManager.WriteLog("Inside ShowJackportSlipSummaryReport method", LogManager.enumLogLevel.Info);

                if (reportStartDateTime > reportEndDateTime)
                {
                    MessageBox.ShowBox("MessageID282", BMC_Icon.Information);
                    return;
                }

                if (reportStartDateTime > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID283", BMC_Icon.Information);
                    return;
                }

                if (reportEndDateTime > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                DataSet jackpotSlipSummaryDetails = objReports.GetJackpotSlipSummaryDetails(reportStartDateTime, 
                    reportEndDateTime,chkIncludeHandpay.IsChecked,chkIncludeJackpot.IsChecked);

                if (jackpotSlipSummaryDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    cReportViewer.ShowJackpotSlipSummaryReport(jackpotSlipSummaryDetails, reportStartDateTime, reportEndDateTime,
                        chkIncludeHandpay.IsChecked,chkIncludeJackpot.IsChecked);
                    cReportViewer.ShowDialog();
                }

                LogManager.WriteLog("ShowJackportSlipSummaryReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }

        private void ShowMeterListReport(string installationNo, string assetNo)
        {
            DataSet dsMeterDetails = null;
            DataSet dsMeterDetail = null;

            try
            {
                LogManager.WriteLog("Inside ShowMeterListReport method", LogManager.enumLogLevel.Info);

                dsMeterDetails = new DataSet();

                IReports objReports = ReportsBusinessObject.CreateInstance();

                if (installationNo == "ALL")
                {
                    LogManager.WriteLog("Fetching Asset Numbers from database....", LogManager.enumLogLevel.Info);

                    DataTable dtAssetNumbers = objReports.GetAssetNumberforActiveInstallations();

                    if (dtAssetNumbers.Rows.Count > 0)
                    {
                        LogManager.WriteLog("Asset Numbers fetched successfully from database", LogManager.enumLogLevel.Info);

                        foreach (DataRow dataRow in dtAssetNumbers.Rows)
                        {
                            LogManager.WriteLog(string.Format("{0} - {1}", "Fetching report data from database for Installation No", 
                                dataRow["InstallationNo"].ToString()), LogManager.enumLogLevel.Info);

                            dsMeterDetail = objReports.GetMeterDetails(Convert.ToInt32(dataRow["InstallationNo"]));

                            LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                            LogManager.WriteLog("Merging dataset...", LogManager.enumLogLevel.Info);

                            dsMeterDetails.Merge(dsMeterDetail);

                            LogManager.WriteLog("Merge dataset Successfull", LogManager.enumLogLevel.Info);                            

                            if (dsMeterDetail != null) { dsMeterDetail = null; }
                        }
                    }
                    else
                    {
                        LogManager.WriteLog("Asset Number not available - Return", LogManager.enumLogLevel.Info);

                        MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                        return;
                    }
                }
                else
                {
                    LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                    dsMeterDetails = objReports.GetMeterDetails(Convert.ToInt32(installationNo));

                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                }

                if (dsMeterDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowMeterListReport(dsMeterDetails, assetNo);
                    cReportViewer.ShowDialog();
                }

                LogManager.WriteLog("ShowMeterListReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }            
        }

        private void GetIssuedTicketReport(DateTime dtFromDate, DateTime dtToDate, string sStatus, string sSlot)
        {
            try
            {
                LogManager.WriteLog("Inside GetIssuedTicketReport method", LogManager.enumLogLevel.Info);

                if (dtFromDate > dtToDate)
                {
                    MessageBox.ShowBox("MessageID286", BMC_Icon.Information);
                    return;
                }

                if (dtFromDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID287", BMC_Icon.Information);
                    return;
                }

                if (dtToDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet dtDataset = objReports.GetVoucherListingReport(dtFromDate, dtToDate, sStatus,sSlot);

				if (dtDataset.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowVoucherListingReport("VoucherListingReport", dtDataset, dtFromDate, dtToDate, sStatus,sSlot);
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }

        private void ShowExceptionVoucherDetailReport(DateTime dtFromDate, DateTime dtToDate, Int32 BatchNumber,string SiteCode)
        {
            try
            {
                LogManager.WriteLog("Inside GetExceptionVoucherDetailReport method", LogManager.enumLogLevel.Info);

                if (dtFromDate > dtToDate)
                {
                    MessageBox.ShowBox("MessageID286", BMC_Icon.Information);
                    return;
                }

                if (dtFromDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID287", BMC_Icon.Information);
                    return;
                }

                if (dtToDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID288", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();

                DataSet dtDataset = objReports.GetExceptionVoucherDetails(dtFromDate, dtToDate, chkDropBased.IsChecked, BatchNumber);

                if (dtDataset.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowExceptionVoucherDetail("ExceptionVoucherDetails", dtDataset, dtFromDate, dtToDate, chkDropBased.IsChecked, BatchNumber,SiteCode);
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }
        }


        private DateTime GetDateTime(Microsoft.Windows.Controls.DatePicker datePicker, TimePicker timePicker)
        {
            
                 DateTime dtDateTime = new DateTime(datePicker.SelectedDate.Value.Year,
                                                    datePicker.SelectedDate.Value.Month,
                                                    datePicker.SelectedDate.Value.Day,
                                                    timePicker.SelectedHour, 
                                                    timePicker.SelectedMinute,
                                                    timePicker.SelectedSecond);

                return dtDateTime;            
          
        }
        
        #endregion

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
                        ((BMC.Presentation.CCustomReports)(this)).Loaded -= (this.Window_Loaded);
                        this.cmbReports.SelectionChanged -= (this.cmbReports_SelectionChanged);
                        this.btnOK.Click -= (this.btnOK_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CCustomReports objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CCustomReports"/> is reclaimed by garbage collection.
        /// </summary>
        ~CCustomReports()
        {
            Dispose(false);
        }

        #endregion

        private void dtpFromDate_Exception_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpFromDate_Exception.SelectedDate == null || tmpFromDate_Exception == null || dtpToDate_Exception.SelectedDate == null || tmpToDate_Exception == null) return;
            DateTime dtDateTime = new DateTime(dtpFromDate_Exception.SelectedDate.Value.Year,
                                                    dtpFromDate_Exception.SelectedDate.Value.Month,
                                                    dtpFromDate_Exception.SelectedDate.Value.Day,
                                                    tmpFromDate_Exception.SelectedHour,
                                                    tmpFromDate_Exception.SelectedMinute,
                                                    tmpFromDate_Exception.SelectedSecond);
            DateTime dtToDate = new DateTime(dtpToDate_Exception.SelectedDate.Value.Year,
                                                    dtpToDate_Exception.SelectedDate.Value.Month,
                                                    dtpToDate_Exception.SelectedDate.Value.Day,
                                                    tmpToDate_Exception.SelectedHour,
                                                    tmpToDate_Exception.SelectedMinute,
                                                    tmpToDate_Exception.SelectedSecond);
            FillBatchNumberCombo(dtDateTime, dtToDate, Convert.ToBoolean(chkDropBased.IsChecked));

        }

        private void dtpFromDate_Exception_SelectedDateChanged(object sender, TimeSelectedChangedRoutedEventArgs e)
        {
            if (dtpFromDate_Exception.SelectedDate == null || tmpFromDate_Exception == null || dtpToDate_Exception.SelectedDate == null || tmpToDate_Exception == null) return;
            DateTime dtDateTime = new DateTime(dtpFromDate_Exception.SelectedDate.Value.Year,
                                                   dtpFromDate_Exception.SelectedDate.Value.Month,
                                                   dtpFromDate_Exception.SelectedDate.Value.Day,
                                                   tmpFromDate_Exception.SelectedHour,
                                                   tmpFromDate_Exception.SelectedMinute,
                                                   tmpFromDate_Exception.SelectedSecond);
            DateTime dtToDate = new DateTime(dtpToDate_Exception.SelectedDate.Value.Year,
                                                    dtpToDate_Exception.SelectedDate.Value.Month,
                                                    dtpToDate_Exception.SelectedDate.Value.Day,
                                                    tmpToDate_Exception.SelectedHour,
                                                    tmpToDate_Exception.SelectedMinute,
                                                    tmpToDate_Exception.SelectedSecond);
            FillBatchNumberCombo(dtDateTime, dtToDate, Convert.ToBoolean(chkDropBased.IsChecked));
        }

        private void chkDropBased_Checked(object sender, RoutedEventArgs e)
        {
            if (dtpFromDate_Exception.SelectedDate == null || tmpFromDate_Exception == null || dtpToDate_Exception.SelectedDate == null || tmpToDate_Exception == null) return;
            DateTime dtDateTime = new DateTime(dtpFromDate_Exception.SelectedDate.Value.Year,
                                                   dtpFromDate_Exception.SelectedDate.Value.Month,
                                                   dtpFromDate_Exception.SelectedDate.Value.Day,
                                                   tmpFromDate_Exception.SelectedHour,
                                                   tmpFromDate_Exception.SelectedMinute,
                                                   tmpFromDate_Exception.SelectedSecond);
            DateTime dtToDate = new DateTime(dtpToDate_Exception.SelectedDate.Value.Year,
                                                    dtpToDate_Exception.SelectedDate.Value.Month,
                                                    dtpToDate_Exception.SelectedDate.Value.Day,
                                                    tmpToDate_Exception.SelectedHour,
                                                    tmpToDate_Exception.SelectedMinute,
                                                    tmpToDate_Exception.SelectedSecond);
            FillBatchNumberCombo(dtDateTime, dtToDate, Convert.ToBoolean(chkDropBased.IsChecked));
        }

        private void chkDropBased_Unchecked(object sender, RoutedEventArgs e)
        {
            if (dtpFromDate_Exception.SelectedDate == null || tmpFromDate_Exception == null || dtpToDate_Exception.SelectedDate == null || tmpToDate_Exception == null) return;
            DateTime dtDateTime = new DateTime(dtpFromDate_Exception.SelectedDate.Value.Year,
                                                   dtpFromDate_Exception.SelectedDate.Value.Month,
                                                   dtpFromDate_Exception.SelectedDate.Value.Day,
                                                   tmpFromDate_Exception.SelectedHour,
                                                   tmpFromDate_Exception.SelectedMinute,
                                                   tmpFromDate_Exception.SelectedSecond);
            DateTime dtToDate = new DateTime(dtpToDate_Exception.SelectedDate.Value.Year,
                                                    dtpToDate_Exception.SelectedDate.Value.Month,
                                                    dtpToDate_Exception.SelectedDate.Value.Day,
                                                    tmpToDate_Exception.SelectedHour,
                                                    tmpToDate_Exception.SelectedMinute,
                                                    tmpToDate_Exception.SelectedSecond);
            FillBatchNumberCombo(dtDateTime, dtToDate, Convert.ToBoolean(chkDropBased.IsChecked));
        }

        private void LoadAccWinLossComboBoxes()
        {
            try
            {
                DataTable dt_zones = new CommonDataAccess().GetAllZones();

                DataRow dataRow = dt_zones.NewRow();
                dataRow["Zone_No"] = 0;
                dataRow["Zone_Name"] = "ALL";

                dt_zones.Rows.InsertAt(dataRow, 0);

                LogManager.WriteLog("Setting Asset number combo box Item Source property", LogManager.enumLogLevel.Info);

                cmbZone_AWLR.ItemsSource = ((System.ComponentModel.IListSource)dt_zones).GetList();
                cmbZone_AWLR.DataContext = dt_zones.DefaultView;
                cmbZone_AWLR.DisplayMemberPath = "Zone_Name";
                cmbZone_AWLR.SelectedValuePath = "Zone_No";
                cmbZone_AWLR.SelectedIndex = 0;

                DataTable dt_machine_category = new CommonDataAccess().GetMachineCategory();

                DataRow dataRow1 = dt_machine_category.NewRow();
                dataRow1["Machine_Type_ID"] = 0;
                dataRow1["Machine_Type_Code"] = "ALL";

                dt_machine_category.Rows.InsertAt(dataRow1, 0);

                LogManager.WriteLog("Setting Asset number combo box Item Source property", LogManager.enumLogLevel.Info);

                cmbCategory_AWLR.ItemsSource = ((System.ComponentModel.IListSource)dt_machine_category).GetList();
                cmbCategory_AWLR.DataContext = dt_machine_category.DefaultView;
                cmbCategory_AWLR.DisplayMemberPath = "Machine_Type_Code";
                cmbCategory_AWLR.SelectedValuePath = "Machine_Type_ID";
                cmbCategory_AWLR.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }     

    }
}
