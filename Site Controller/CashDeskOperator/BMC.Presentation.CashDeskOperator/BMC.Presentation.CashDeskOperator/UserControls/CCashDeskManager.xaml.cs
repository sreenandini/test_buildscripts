using System;
using System.Windows;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.Transport;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CashDeskOperator;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using System.Data;
using BMC.Presentation.POS.Views;
using BMC.Presentation.CashDeskManager.UserControls;
using System.Globalization;
using BMC.Presentation.POS.Helper_classes;
using BMC.Business.CashDeskOperator;
using System.Data.Linq;
using System.Linq;
using BMC.Security;
using System.Collections.Generic;
using System.Windows.Data;



namespace BMC.Presentation
{
    public partial class CCashDeskManager : IDisposable
    {
        #region "Declarations"
        ICashDeskManager objCashDeskManager = null;
        string RouteNumber = string.Empty;
        string dtFrom = string.Empty;
        string dtTo = string.Empty;
        string TimeFrom = string.Empty;
        string TimeTo = string.Empty;

        string TREASURY_REFILL = "Refill";
        string TREASURY_REFUND = "Refund";
        string TREASURY_HANDPAY_CREDIT = "AttendantPay Credit";
        string TREASURY_HANDPAY_CREDIT_Handpay = "Attendent Pay";
        string TREASURY_SHORTPAY = "Shortpay";
        string TREASURY_CASH_DESK_FLOAT = "Cash Desk Float";
        string TREASURY_PROGRESSIVE = "Progressive";
        string TREASURY_JACKPOT = "AttendantPay Jackpot";
        string TREASURY_JACKPOT_Cash = "Jackpot";
        string MYFORMAT = "#,##0.00";
        bool flag = false;
        BackgroundWorker _worker = null;
        List<TicketExceptions> lstTicketExcep = null;
        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        Grid pnlContent = null;
        Microsoft.Windows.Controls.DataGrid lvView = null;
        List<TicketExceptions> lstTicketsClaimed = null;
        int UserNo = 0;
        string SecurityRole = string.Empty;
        List<User> UserRoles, URoleBased = null;
        int iRoute_No = 0;
        string sRoute_Name = "ALL";
        CashierTransactions objResult = new CashierTransactions();
        CashierHistory _CashierHistory = null;
        bool isDateChanged = false;
        int _iNoofRecords = 1000;
        #endregion


        public CCashDeskManager()
        {
            try
            {
                InitializeComponent();
                objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();
                _worker = new BackgroundWorker();
                _worker.WorkerReportsProgress = true;
                _worker.WorkerSupportsCancellation = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public CCashDeskManager(Grid pnlContent)
        {
            try
            {
                InitializeComponent();
                objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();
                _worker = new BackgroundWorker();
                _worker.WorkerReportsProgress = true;
                _worker.WorkerSupportsCancellation = true;
                UserRoles = objCashDeskManager.GetListOfUsersRoles(SecurityHelper.CurrentUser.User_No);
                URoleBased = UserRoles.Where(RoleName => RoleName.RoleAccessName == UserRole.AccessOtherUsers.ToString()).ToList();

                Thread.Sleep(5);
                List<User> users;
                if (URoleBased.Count == 0)
                    users = objCashDeskManager.GetListOfUsers(SecurityHelper.CurrentUser.User_No);
                else
                {
                    users = objCashDeskManager.GetListOfUsers(0);
                    users.Insert(0, new User()
                    {
                        UserNo = 0,
                        UserName = "ALL",
                        RoleAccessName = string.Empty,
                        RoleaccessID = 0,
                        RoleName = string.Empty,
                        SecurityUserID = 0
                    });

                }

                if (users.Count > 0)
                    UserNo = (users[0] as User).UserNo;

                cboUser.ItemsSource = users.Distinct();
                cboUser.DisplayMemberPath = "UserName";
                cboUser.SelectedIndex = 0;

                cboUser.SelectionChanged += new SelectionChangedEventHandler(cboUser_SelectionChanged);
                LoadRoute();

                this.pnlContent = pnlContent;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

                void cboUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<User> users;
            User lstUser = cboUser.SelectedItem as User;
            UserNo = lstUser.UserNo;
            if (URoleBased.Count == 0 || UserNo != 0)
                users = objCashDeskManager.GetListOfUsers(SecurityHelper.CurrentUser.User_No);
            else
                users = objCashDeskManager.GetListOfUsers(0);


        }

        private void cboRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                iRoute_No = Convert.ToInt32(cboRoute.SelectedValue);
                sRoute_Name = (iRoute_No > 0) ? ((RouteCollection)cboRoute.SelectedItem).Route_Name : "ALL";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExcepDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CExceptions objExceptions = null;
                objExceptions = new CExceptions("0", StartDate, EndDate, 0);


                objExceptions.Owner = MessageBox.parentOwner;
                objExceptions.ShowDialogEx(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                //btnExcepDetails.IsEnabled = true;
            }
        }


        private void btnDebug_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CDebug objDebug = null;
                objDebug = new CDebug("0", StartDate, EndDate, 0);


                pnlContent.Children.Add(objDebug);
                objDebug.Margin = new Thickness(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnLiabilityDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CLiability objLiability = null;
                objLiability = new CLiability("0", StartDate, EndDate, 0);

                objLiability.Owner = MessageBox.parentOwner;
                objLiability.ShowDialogEx(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnVoidDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CVoidAutoCancelledTickets objVoidCancelled = null;
                objVoidCancelled = new CVoidAutoCancelledTickets("0", StartDate, EndDate, 0);


                objVoidCancelled.Owner = MessageBox.parentOwner;
                objVoidCancelled.ShowDialogEx(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnActiveDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CActiveTicketsxaml objActiveTickets = null;
                objActiveTickets = new CActiveTicketsxaml("0", StartDate, EndDate, 0);


                objActiveTickets.Owner = MessageBox.parentOwner;
                objActiveTickets.ShowDialogEx(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnPromoCashableDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CPromoCashableDetails objPromo = null;
                objPromo = new CPromoCashableDetails("0", StartDate, EndDate, 0);


                objPromo.Owner = MessageBox.parentOwner;
                objPromo.ShowDialogEx(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnAnomalies_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                CTicketAnomalies objTicketAnomalies = null;
                objTicketAnomalies = new CTicketAnomalies("0", StartDate, EndDate, 0);


                objTicketAnomalies.Owner = MessageBox.parentOwner;
                objTicketAnomalies.ShowDialogEx(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExpiredDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CVoidExpired objVoidExpired = null;
                objVoidExpired = new CVoidExpired("0", StartDate, EndDate, 0);


                objVoidExpired.Owner = MessageBox.parentOwner;
                objVoidExpired.ShowDialogEx(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnProcess.IsEnabled = false;
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID282", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID283", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID284", BMC_Icon.Information);
                    return;
                }


                if (dtpStartDate.SelectedDate != null && dtpEndDate.SelectedDate != null)
                {
                    StartDate = new DateTime(((DateTime)dtpStartDate.SelectedDate).Year,
                        ((DateTime)dtpStartDate.SelectedDate).Month,
                        ((DateTime)dtpStartDate.SelectedDate).Day,
                        tmpStartTime.SelectedHour, tmpStartTime.SelectedMinute,
                        tmpStartTime.SelectedSecond);

                    EndDate = new DateTime(((DateTime)dtpEndDate.SelectedDate).Year,
                        ((DateTime)dtpEndDate.SelectedDate).Month,
                        ((DateTime)dtpEndDate.SelectedDate).Day,
                        dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute,
                        dtpEndtime.SelectedSecond);

                    Settings.dtCashierTransStartTime = StartDate;

                    //LoadData(UserNo, SecurityRole);
                    showModel(LoadCashierTransactionData);
                }
                else
                {
                    MessageBox.ShowBox("MessageID37");
                }
                isDateChanged = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnProcess.IsEnabled = true;
            }
        }

        private void LoadCashierTransactionData()
        {

            try
            {

                TreasuryTransactions objTT = new TreasuryTransactions();
                _CashierHistory = objTT.GetCashierTransactionsData(StartDate, EndDate, UserNo, iRoute_No);

                foreach (rsp_CDM_GetCashierTransactionsSummary data in _CashierHistory.Summary)
                {
                    switch (data.Summary_Type.ToUpper())
                    {
                        case "CDPAIDAMOUNT":
                            objResult.CDPaidAmount = data.Amount;
                            objResult.CDPaidCount = data.Count_Summary;

                            break;
                        case "CDPRINTEDAMOUNT":
                            objResult.CDPrintedAmount = data.Amount;
                            objResult.CDPrintedCount = data.Count_Summary;
                            break;
                        case "HANDPAYAMOUNT":
                            objResult.HandPayAmount = data.Amount;
                            objResult.HandPayCount = data.Count_Summary;

                            break;
                        case "SHORTPAYAMOUNT":
                            objResult.ShortPayAmount = data.Amount;
                            objResult.ShortPayCount = data.Count_Summary;
                            break;
                        case "JACKPOTAMOUNT":
                            objResult.JackpotAmount = data.Amount;
                            objResult.JackpotCount = data.Count_Summary;
                            //chkjackpot
                            break;
                        case "PROGAMOUNT":
                            objResult.ProgAmount = data.Amount;
                            objResult.ProgCount = data.Count_Summary;
                            //chkProghandpays
                            break;
                        case "VOIDAMOUNT":
                            objResult.VoidAmount = data.Amount;
                            objResult.VoidCount = data.Count_Summary;
                            break;
                        case "MCPAIDAMOUNT":
                            objResult.MCPaidAmount = data.Amount;
                            objResult.MCPaidCount = data.Count_Summary;
                            break;
                        case "MCPRINTAMOUNT":
                            objResult.MCPrintAmount = data.Amount;
                            objResult.MCPrintCount = data.Count_Summary;
                            break;
                        case "ACTIVECASHABLEVOUCHERAMOUNT":
                            objResult.ActiveCashableVoucherAmount = data.Amount;
                            objResult.ActiveCashableVoucherCount = data.Count_Summary;
                            break;
                        case "VOIDTICKETSAMOUNT":
                            objResult.VoidTicketsAmount = data.Amount;
                            objResult.VoidTicketsCount = data.Count_Summary;
                            break;
                        case "VOIDVOUCHERAMOUNT":
                            objResult.VoidVoucherAmount = data.Amount;
                            objResult.VoidVoucherCount = data.Count_Summary;
                            break;
                        case "CANCELLEDAMOUNT":
                            objResult.CancelledAmount = data.Amount;
                            objResult.CancelledCount = data.Count_Summary;
                            break;
                        case "EXPIREDAMOUNT":
                            objResult.ExpiredAmount = data.Amount;
                            objResult.ExpiredCount = data.Count_Summary;
                            break;
                        case "TICKETINEXCEPTIONAMOUNT":
                            objResult.TicketInExceptionAmount = data.Amount;
                            objResult.TicketInExceptionCount = data.Count_Summary;
                            break;
                        case "TICKETOUTEXCEPTIONAMOUNT":
                            objResult.TicketOutExceptionAmount = data.Amount;
                            objResult.TicketOutExceptionCount = data.Count_Summary;
                            break;
                        case "CASHABLEVOUCHERLIABILITYAMOUNT":
                            objResult.CashableVoucherLiabilityAmount = data.Amount;
                            objResult.CashableVoucherLiabilityCount = data.Count_Summary;
                            break;
                        case "PROMOCASHABLEAMOUNT":
                            objResult.PromoCashableAmount = data.Amount;
                            objResult.PromoCashableCount = data.Count_Summary;
                            break;
                        case "NONCASHABLEINAMOUNT":
                            objResult.NonCashableINAmount = data.Amount;
                            objResult.NonCashableINCount = data.Count_Summary;
                            break;
                        case "NONCASHABLEOUTAMOUNT":
                            objResult.NonCashableOutAmount = data.Amount;
                            objResult.NonCashableOutCount = data.Count_Summary;
                            break;
                        case "OFFLINEVOUCHERAMOUNT":
                            objResult.OfflineVoucherAmount = data.Amount;
                            objResult.OfflineVoucherCount = data.Count_Summary;
                            break;
                        case "OUTSTANDINGHANDPAYSAMOUNT":
                            objResult.OutstandingHandpaysAmount = data.Amount;
                            objResult.OutstandingHandpaysCount = data.Count_Summary;
                            break;
                        default:
                            break;
                    }

                }
                Dispatcher.Invoke(new System.Action(
                                () =>
                                {
                                    base.DataContext = null;
                                    base.DataContext = objResult;
                                })
                        );

            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");


                this.dtpStartDate.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dtpStartDate_SelectedDateChanged);

                dtpStartDate.SelectedDate = DateTime.Now.Date;
                dtpEndDate.SelectedDate = DateTime.Now.Date;

                if (Settings.dtCashierTransStartTime.HasValue)
                {
                    tmpStartTime.SelectedHour = Settings.dtCashierTransStartTime.Value.Hour;
                    tmpStartTime.SelectedMinute = Settings.dtCashierTransStartTime.Value.Minute;
                    tmpStartTime.SelectedSecond = Settings.dtCashierTransStartTime.Value.Second;
                }
                else
                {
                    tmpStartTime.SelectedHour = DateTime.Parse(Settings.DailyAutoReadTime).Hour;
                    tmpStartTime.SelectedMinute = DateTime.Parse(Settings.DailyAutoReadTime).Minute;
                    tmpStartTime.SelectedSecond = DateTime.Parse(Settings.DailyAutoReadTime).Second;
                }

                //grpCoinHopper.Visibility = Settings.CD_Not_Use_Hoppers ? Visibility.Visible : Visibility.Collapsed;


                txtCashDeskShortPaysVal.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                txtCashDeskShortPayQty.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;

                lblNoncashableVoucherIn.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                txtNCInQty.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                txtNCInVal.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                txtNCOutQty.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                txtNCOutVal.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                chkNCTicketIn.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                chkNCTicketOut.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                chkShortPays.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                chkVoidVoucher.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                lblNoncashableVoucherOut.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                //lblPromocashableVouchers.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                //txtPromoCashableVal.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                //txtPromoQty.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                //
                txtOfflinevoucher.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                txtOfflinevoucher1.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                lblOfflinevoucher.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                chkOfflinevoucher.Visibility = !Settings.RegulatoryEnabled ? Visibility.Visible : Visibility.Hidden;
                //

                btnReconciliation.Visibility = Settings.EnableCashdeskReconciliation ? Visibility.Visible : Visibility.Hidden;
                btnMovement.Visibility = Settings.EnableCashdeskMovement ? Visibility.Visible : Visibility.Hidden;
                btnBalancing.Visibility = Settings.EnableSystemBalancing ? Visibility.Visible : Visibility.Hidden;
                btnTicketAnomalies.Visibility = Settings.IsTicketAnomaliesEnabled ? Visibility.Visible : Visibility.Hidden;

                //set start date 
                if (!Settings.dtCashierTransStartTime.HasValue)
                {
                    if (DateTime.Now < DateTime.Parse(string.Format("{0} {1}", DateTime.Now.ToShortDateString(), Settings.DailyAutoReadTime)))
                    {
                        dtpStartDate.Text = DateTime.Now.Date.AddDays(-1).ToString();
                    }
                    else
                    {
                        dtpStartDate.Text = DateTime.Now.Date.ToString();
                    }
                }
                else
                {
                    dtpStartDate.Text = Settings.dtCashierTransStartTime.Value.Date.ToString();
                }

                dtpEndDate.Text = DateTime.Now.Date.ToString();

                _iNoofRecords = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CDM_RecordsCount"]);

                if (_iNoofRecords < 1)
                    _iNoofRecords = 1000;
            }
            catch (Exception ex)
            {
                _iNoofRecords = 1000;
                ExceptionManager.Publish(ex);
            }
        }

        private void btnPrint_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnPrint_Copy.IsEnabled = false;
                if (_worker.IsBusy)
                {
                    MessageBox.ShowBox("MessageID368", BMC_Icon.Information);
                    return;
                }
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID282", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID283", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID284", BMC_Icon.Information);
                    return;
                }
                if (isDateChanged)
                {
                    btnProcess_Click(sender, e);
                }
                decimal TotalCount = 0;

                if (chkCashDeskTicketIn.IsChecked.Value)
                    TotalCount += objResult.CDPaidCount.Value;
                if (chkCashDeskTicketOut.IsChecked.Value)
                    TotalCount += objResult.CDPrintedCount.Value;
                if (chkHandpays.IsChecked.Value)
                    TotalCount += objResult.HandPayCount.Value;
                if (chkShortPays.IsChecked.Value)
                    TotalCount += objResult.ShortPayCount.Value;
                if (chkVoidVoucher.IsChecked.Value)
                    TotalCount += objResult.VoidVoucherCount.Value;
                if (chkjackpot.IsChecked.Value)
                    TotalCount += objResult.JackpotCount.Value;
                if (chkProghandpays.IsChecked.Value)
                    TotalCount += objResult.ProgCount.Value;
                if (chkVoidTransactions.IsChecked.Value)
                    TotalCount += objResult.VoidCount.Value;
                if (chkTicketIn.IsChecked.Value)
                    TotalCount += objResult.MCPaidCount.Value;
                if (chkTicketOut.IsChecked.Value)
                    TotalCount += objResult.MCPrintCount.Value;
                if (chkActive.IsChecked.Value)
                    TotalCount += objResult.ActiveCashableVoucherCount.Value;
                if (chkVoidCancel.IsChecked.Value)
                {
                    TotalCount += objResult.VoidTicketsCount.Value;
                    TotalCount += objResult.CancelledCount.Value;
                }
                if (chkExpired.IsChecked.Value)
                    TotalCount += objResult.ExpiredCount.Value;
                if (chkException.IsChecked.Value)
                {
                    TotalCount += objResult.TicketInExceptionCount.Value;
                    TotalCount += objResult.TicketOutExceptionCount.Value;
                }
                if (chkLiability.IsChecked.Value)
                    TotalCount += objResult.CashableVoucherLiabilityCount.Value;
                //if (chkPromo.IsChecked.Value)
                //    TotalCount += objResult.PromoCashableCount.Value;
                if (chkNCTicketIn.IsChecked.Value)
                    TotalCount += objResult.NonCashableINCount.Value;
                if (chkNCTicketOut.IsChecked.Value)
                    TotalCount += objResult.NonCashableOutCount.Value;
                if (chkOfflinevoucher.IsChecked.Value)
                    TotalCount += objResult.OfflineVoucherCount.Value;
                if (chkUnprocessedHandpay.IsChecked.Value)
                    TotalCount += objResult.OutstandingHandpaysCount.Value;

                if (TotalCount == 0)
                {
                    //No data found.
                    MessageBox.ShowBox("MessageID893", BMC_Icon.Warning, BMC_Button.OK);
                    return;
                }
                else if (TotalCount > _iNoofRecords)
                {
                    //Too many data to show, Do you want to continue ?
                    if (MessageBox.ShowBox("MessageID895", BMC_Icon.Warning, BMC_Button.YesNo, "") == DialogResult.No)
                        return;
                }

                getdata();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnPrint_Copy.IsEnabled = true;
            }
        }
        void showModel(System.Action Method)
        {
            ModalProgressBar dig = null;
            try
            {

                dig = new ModalProgressBar(Method);
                dig.Owner = System.Windows.Window.GetWindow(this);
                dig.ShowDialogEx(this);

            }
            catch (Exception Ex)
            {
                if (dig != null)
                    dig.Close();
                ExceptionManager.Publish(Ex);
            }
        }

        private void getdata()
        {

            TreasuryTransactions objTT = new TreasuryTransactions();


            List<rsp_CDM_GetCashierTransactionsDetails_New> detailslist = objTT.rsp_CDM_GetCashierTransactionsDetails_New(StartDate, EndDate, UserNo, iRoute_No,
                chkCashDeskTicketIn.IsChecked.Value,
                chkCashDeskTicketOut.IsChecked.Value,
                chkHandpays.IsChecked.Value,
                chkShortPays.IsChecked.Value,
                chkVoidVoucher.IsChecked.Value,
                chkjackpot.IsChecked.Value,
                chkProghandpays.IsChecked.Value,
                chkVoidTransactions.IsChecked.Value,
                chkTicketIn.IsChecked.Value,
                chkTicketOut.IsChecked.Value,
                chkActive.IsChecked.Value,
                chkVoidCancel.IsChecked.Value,
                chkExpired.IsChecked.Value,
                chkException.IsChecked.Value,
                chkLiability.IsChecked.Value,
                false,
                chkNCTicketIn.IsChecked.Value,
                chkNCTicketOut.IsChecked.Value,
                chkOfflinevoucher.IsChecked.Value,
                chkUnprocessedHandpay.IsChecked.Value
                );

            string sFooter = GetFooterText();
            var objViewAll = new CashDeskManagerAllDetails(detailslist, StartDate, EndDate, sFooter,
                                                           UserNo, iRoute_No);
            objViewAll.btnExit.Click += ObjPosDetailsExitClicked;
            objViewAll.Owner = MessageBox.parentOwner;
            // pnlContent.Children.Add(objViewAll);
            objViewAll.ShowDialogEx(this);
            lvView = objViewAll.lvViewAll;
        }

        private string GetFooterText()
        {
            string sFooterText = string.Empty;

            sFooterText += chkCashDeskTicketIn.IsChecked == true ? "[Vouchers Claimed]" : "";
            sFooterText += chkCashDeskTicketOut.IsChecked == true ? " [Vouchers Issued]" : "";
            sFooterText += chkHandpays.IsChecked == true ? " [Attendant pay]" : "";
            sFooterText += chkShortPays.IsChecked == true ? " [Shortpay]" : "";
            sFooterText += chkjackpot.IsChecked == true ? " [Jackpot pays]" : "";
            sFooterText += chkProghandpays.IsChecked == true ? " [Progressive pays]" : "";
            sFooterText += chkVoidTransactions.IsChecked == true ? " [Void Transactions]" : "";
            sFooterText += chkTicketIn.IsChecked == true ? " [Cashable Voucher In]" : "";
            sFooterText += chkTicketOut.IsChecked == true ? " [Cashable Voucher Out]" : "";

            sFooterText += chkActive.IsChecked == true ? " [Active Vouchers]" : "";
            sFooterText += chkVoidCancel.IsChecked == true ? " [Void/Cancelled Vouchers]" : "";
            sFooterText += chkException.IsChecked == true ? " [Exception Vouchers]" : "";
            sFooterText += chkExpired.IsChecked == true ? " [Expired Vouchers]" : "";
            sFooterText += chkLiability.IsChecked == true ? " [Liability Vouchers]" : "";
            //sFooterText += chkPromo.IsChecked == true ? " [Promo Cashable Vouchers]" : "";
            sFooterText += chkNCTicketIn.IsChecked == true ? " [Non Cashable Voucher In]" : "";
            sFooterText += chkNCTicketOut.IsChecked == true ? " [Non Cashable Voucher Out]" : "";
            sFooterText += chkVoidVoucher.IsChecked == true ? " [Void Vouchers]" : "";
            sFooterText += chkUnprocessedHandpay.IsChecked == true ? " [Outstanding Att’pays and JP]" : "";
            return sFooterText;
        }

        private void btnReconciliation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnReconciliation.IsEnabled = false;
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID282", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID283", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID284", BMC_Icon.Information);
                    return;
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                DataSet CashdeskDetails = null;
                //= objReports.GetCashDeskReconcilationDetails(StartDate, EndDate);
                switch (URoleBased.Count)
                {
                    case 0:
                        {
                            if (Security.SecurityHelper.CurrentUser.User_No == UserNo)
                            {
                                CashdeskDetails = objReports.GetCashDeskReconcilationDetails(StartDate, EndDate, UserNo, iRoute_No);
                            }
                            break;
                        }
                    case 1:
                        {
                            if (UserNo == 0)
                            {
                                CashdeskDetails = objReports.GetCashDeskReconcilationDetails(StartDate, EndDate, 0, iRoute_No);
                            }
                            else
                            {
                                CashdeskDetails = objReports.GetCashDeskReconcilationDetails(StartDate, EndDate, UserNo, iRoute_No);
                            }
                            break;
                        }
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    cReportViewer.ShowCashDeskReconcilationReport(CashdeskDetails, StartDate, EndDate);
                    cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                    cReportViewer.Show();
                }

                LogManager.WriteLog("ShowCashDeskReconcilationReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnReconciliation.IsEnabled = true;
            }
        }

        private void btnMovement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnMovement.IsEnabled = false;
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID282", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID283", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID284", BMC_Icon.Information);
                    return;
                }


                IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                //DataSet CashdeskDetails = objReports.GetCashDeskMovementDetails(StartDate, EndDate);

                DataSet CashdeskDetails = null;
                switch (URoleBased.Count)
                {
                    case 0:
                        {
                            if (Security.SecurityHelper.CurrentUser.User_No == UserNo)

                                CashdeskDetails = objReports.GetCashDeskMovementDetails(StartDate, EndDate, UserNo, iRoute_No);

                            break;
                        }
                    case 1:
                        {
                            if (UserNo == 0)
                            {
                                CashdeskDetails = objReports.GetCashDeskMovementDetails(StartDate, EndDate, 0, iRoute_No);
                            }
                            else
                            {
                                CashdeskDetails = objReports.GetCashDeskMovementDetails(StartDate, EndDate, UserNo, iRoute_No);
                            }
                            break;
                        }
                }

                if (CashdeskDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                if (objCashDeskManager.GetRegionFromSite().Equals("US") || Settings.Region == "AR")
                {
                    using (CReportViewer cReportViewer = new CReportViewer())
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                        cReportViewer.ShowCashDeskMovementUSReport(CashdeskDetails, StartDate, EndDate);
                        cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                        cReportViewer.Show();
                    }
                }
                else
                //if (objCashDeskManager.GetRegionFromSite().Equals("UK"))
                {
                    using (CReportViewer cReportViewer = new CReportViewer())
                    {
                        LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                        cReportViewer.ShowCashDeskMovementReport(CashdeskDetails, StartDate, EndDate);
                        cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                        cReportViewer.Show();
                    }
                }

                LogManager.WriteLog("ShowCashDeskMovementReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnMovement.IsEnabled = true;
            }
        }

        private void btnBalancing_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnBalancing.IsEnabled = false;
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID282", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID283", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID284", BMC_Icon.Information);
                    return;
                }


                IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                DataSet SystemBalancingDetails = null;

                switch (URoleBased.Count)
                {
                    case 0:
                        {
                            if (Security.SecurityHelper.CurrentUser.User_No == UserNo)
                            {
                                SystemBalancingDetails = objReports.GetSystemBalancingDetails(StartDate, EndDate, UserNo, iRoute_No);
                            }
                            break;
                        }
                    case 1:
                        {
                            if (UserNo == 0)
                            {
                                SystemBalancingDetails = objReports.GetSystemBalancingDetails(StartDate, EndDate, 0, iRoute_No);
                            }
                            else
                            {
                                SystemBalancingDetails = objReports.GetSystemBalancingDetails(StartDate, EndDate, UserNo, iRoute_No);
                            }
                            break;
                        }
                }
                if (SystemBalancingDetails.Tables[0].Rows.Count == 0)
                {
                    LogManager.WriteLog("No Records found for the selected criteria - Return", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }


                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    cReportViewer.ShowSystemBalancingReport(SystemBalancingDetails, StartDate, EndDate);
                    cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                    cReportViewer.Show();
                }


                LogManager.WriteLog("ShowSystemBalancingReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnBalancing.IsEnabled = true;
            }
        }



        private void dtpStartDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                isDateChanged = true;
                DateTime dt = Convert.ToDateTime(dtpStartDate.SelectedDate);
                StartDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpStartTime.SelectedHour, tmpStartTime.SelectedMinute, tmpStartTime.SelectedSecond);
                txtStartDate.Text = Convert.ToDateTime(StartDate).ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            }
            catch (StackOverflowException ex)
            {
                ExceptionManager.Publish(ex);
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }
        private void dtpEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                isDateChanged = true;
                DateTime dt = Convert.ToDateTime(dtpEndDate.SelectedDate);
                EndDate = new DateTime(dt.Year, dt.Month, dt.Day, dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute, dtpEndtime.SelectedSecond);
                txtEndDate.Text = Convert.ToDateTime(EndDate).ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            }
            catch (StackOverflowException ex)
            {
                ExceptionManager.Publish(ex);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dtpEndtime_SelectedTimeChanged(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            try
            {
                isDateChanged = true;
                DateTime dt = Convert.ToDateTime(dtpEndDate.SelectedDate);
                EndDate = new DateTime(dt.Year, dt.Month, dt.Day, dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute, dtpEndtime.SelectedSecond);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void tmpStartTime_SelectedTimeChanged(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            try
            {
                isDateChanged = true;
                DateTime dt = Convert.ToDateTime(dtpStartDate.SelectedDate);
                StartDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpStartTime.SelectedHour, tmpStartTime.SelectedMinute, tmpStartTime.SelectedSecond);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                objCashDeskManager.CloseExcel();
                _worker.CancelAsync();
                this.dtpStartDate.SelectedDateChanged -= new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dtpStartDate_SelectedDateChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private void ObjPosDetailsExitClicked(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture.DateTimeFormat = new System.Globalization.CultureInfo(ExtensionMethods.CurrentDateCulture).DateTimeFormat;
        }

        enum UserRole
        {
            AccessOtherUsers = 1
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
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(ExtensionMethods.CurrentSiteCulture);
            Thread.CurrentThread.CurrentCulture.DateTimeFormat = new System.Globalization.CultureInfo(ExtensionMethods.CurrentDateCulture).DateTimeFormat;

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
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.UserControl.Unloaded -= (this.UserControl_Unloaded);
                        //this.btnActiveDetails.Click -= (this.btnActiveDetails_Click);
                        //this.btnVoidDetails.Click -= (this.btnVoidDetails_Click);
                        //this.btnExpiredDetails.Click -= (this.btnExpiredDetails_Click);
                        //this.btnPromoCashableDetails.Click -= (this.btnPromoCashableDetails_Click);
                        //this.btnExcepDetails.Click -= (this.btnExcepDetails_Click);
                        //this.btnLiabilityDetails.Click -= (this.btnLiabilityDetails_Click);
                        this.dtpStartDate.SelectedDateChanged -= (this.dtpStartDate_SelectedDateChanged);
                        this.tmpStartTime.SelectedTimeChanged -= (this.tmpStartTime_SelectedTimeChanged);
                        this.dtpEndDate.SelectedDateChanged -= (this.dtpEndDate_SelectedDateChanged);
                        this.dtpEndtime.SelectedTimeChanged -= (this.dtpEndtime_SelectedTimeChanged);
                        this.btnAnomalies.Click -= (this.btnAnomalies_Click);
                        this.btnPrint_Copy.Click -= (this.btnPrint_Copy_Click);
                        this.btnProcess.Click -= (this.btnProcess_Click);
                        this.btnReconciliation.Click -= (this.btnReconciliation_Click);
                        this.btnMovement.Click -= (this.btnMovement_Click);
                        this.btnBalancing.Click -= (this.btnBalancing_Click);
                        this.chkALLCD.Checked -= (this.chkALLCD_Checked);
                        this.chkALLMC.Checked -= (this.chkALLMC_Checked);
                        this.chkALLCD.Unchecked -= (this.chkALLCD_Unchecked);
                        this.chkALLMC.Unchecked -= (this.chkALLMC_Unchecked);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CCashDeskManager objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CCashDeskManager"/> is reclaimed by garbage collection.
        /// </summary>
        ~CCashDeskManager()
        {
            Dispose(false);
        }

        #endregion

        private void chkALLCD_Checked(object sender, RoutedEventArgs e)
        {
            bool bChecked = (bool)chkALLCD.IsChecked;

            if (chkCashDeskTicketIn != null)
            {
                chkCashDeskTicketIn.IsChecked = bChecked;
                chkCashDeskTicketOut.IsChecked = bChecked;
                chkHandpays.IsChecked = bChecked;
                chkShortPays.IsChecked = bChecked;
                chkjackpot.IsChecked = bChecked;
                chkProghandpays.IsChecked = bChecked;
                chkVoidTransactions.IsChecked = bChecked;
                chkVoidVoucher.IsChecked = bChecked;
                chkOfflinevoucher.IsChecked = bChecked;
            }

        }

        private void chkALLCD_Unchecked(object sender, RoutedEventArgs e)
        {
            bool bChecked = (bool)chkALLCD.IsChecked;

            if (chkCashDeskTicketIn != null)
            {
                chkCashDeskTicketIn.IsChecked = bChecked;
                chkCashDeskTicketOut.IsChecked = bChecked;
                chkHandpays.IsChecked = bChecked;
                chkShortPays.IsChecked = bChecked;
                chkjackpot.IsChecked = bChecked;
                chkProghandpays.IsChecked = bChecked;
                chkVoidTransactions.IsChecked = bChecked;
                chkVoidVoucher.IsChecked = bChecked;
                chkOfflinevoucher.IsChecked = bChecked;
            }
        }

        private void chkALLMC_Checked(object sender, RoutedEventArgs e)
        {
            bool bChecked = (bool)chkALLMC.IsChecked;


            if (chkTicketIn != null)
            {
                chkTicketIn.IsChecked = bChecked;
                chkTicketOut.IsChecked = bChecked;
                chkActive.IsChecked = bChecked;
                chkVoidCancel.IsChecked = bChecked;
                chkException.IsChecked = bChecked;
                chkExpired.IsChecked = bChecked;
                chkLiability.IsChecked = bChecked;
                //chkPromo.IsChecked = bChecked;
                chkNCTicketIn.IsChecked = bChecked;
                chkNCTicketOut.IsChecked = bChecked;
                chkUnprocessedHandpay.IsChecked = bChecked;
            }
        }

        private void chkALLMC_Unchecked(object sender, RoutedEventArgs e)
        {
            bool bChecked = (bool)chkALLMC.IsChecked;


            if (chkTicketIn != null)
            {
                chkTicketIn.IsChecked = bChecked;
                chkTicketOut.IsChecked = bChecked;
                chkActive.IsChecked = bChecked;
                chkVoidCancel.IsChecked = bChecked;
                chkException.IsChecked = bChecked;
                chkExpired.IsChecked = bChecked;
                chkLiability.IsChecked = bChecked;
                //chkPromo.IsChecked = bChecked;
                chkNCTicketIn.IsChecked = bChecked;
                chkNCTicketOut.IsChecked = bChecked;
                chkUnprocessedHandpay.IsChecked = bChecked;

            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                btnPrint.IsEnabled = false;



                if (_worker.IsBusy)
                {
                    MessageBox.ShowBox("MessageID368", BMC_Icon.Information);
                    return;
                }
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID282", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID283", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID284", BMC_Icon.Information);
                    return;
                }

                if (isDateChanged)
                {
                    btnProcess_Click(sender, e);
                }

                IReports objReports = ReportsBusinessObject.CreateInstance();

                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);

                DataSet CashdeskDetails = null;
                String UserName = string.Empty;
                User lstUser = cboUser.SelectedItem as User;
                if (lstUser == null || String.IsNullOrEmpty(lstUser.UserName))
                {
                    UserName = SecurityHelper.CurrentUser.UserName;
                }
                else
                {
                    UserName = lstUser.UserName;
                }

                CashdeskDetails = new DataSet();

                //Add this list in order amount/count 
                CashdeskDetails.Tables.Add("CDSummary");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("CDPaidAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("CDPaidCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("CDPrintedAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("CDPrintedCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("HandPayAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("HandPayCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("JackpotAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("JackpotCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("ProgAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("ProgCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("ShortPayAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("ShortPayCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("OfflineVoucherAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("OfflineVoucherCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("VoidAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("VoidCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("MCPaidAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("MCPaidCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("MCPrintAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("MCPrintCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("ActiveCashableVoucherAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("ActiveCashableVoucherCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("VoidTicketsAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("VoidTicketsCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("VoidVoucherAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("VoidVoucherCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("CancelledAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("CancelledCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("ExpiredAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("ExpiredCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("TicketInExceptionAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("TicketInExceptionCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("TicketOutExceptionAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("TicketOutExceptionCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("CashableVoucherLiabilityAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("CashableVoucherLiabilityCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("PromoCashableAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("PromoCashableCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("NonCashableINAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("NonCashableINCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("NonCashableOutAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("NonCashableOutCount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("OutstandingHandpaysAmount");
                CashdeskDetails.Tables["CDSummary"].Columns.Add("OutstandingHandpaysCount");



                CashdeskDetails.Tables["CDSummary"].Rows.Add(CashdeskDetails.Tables["CDSummary"].NewRow());


                foreach (rsp_CDM_GetCashierTransactionsSummary item in _CashierHistory.Summary)
                {
                    if (CashdeskDetails.Tables["CDSummary"].Columns.IndexOf(item.Summary_Type) < 0)
                        continue;
                    CashdeskDetails.Tables["CDSummary"].Rows[0][item.Summary_Type] = item.Amount.ToString();
                    CashdeskDetails.Tables["CDSummary"].Rows[0][CashdeskDetails.Tables["CDSummary"].Columns.IndexOf(item.Summary_Type) + 1] = item.Count_Summary.ToString();
                }


                //= objReports.GetCashDeskReconcilationDetails(StartDate, EndDate);
                // CashdeskDetails = objReports.GetCashierTransactions(StartDate, EndDate, UserNo, iRoute_No);

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);

                    cReportViewer.ShowCashierTransactionsHistoryReport(CashdeskDetails, StartDate, EndDate, UserNo, UserName, sRoute_Name, iRoute_No, CashdeskDetails.GetXml());
                    cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                    cReportViewer.Show();
                }

                LogManager.WriteLog("ShowCashDeskReconcilationReport Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnPrint.IsEnabled = true;
            }
        }

        private void LoadRoute()
        {
            cboRoute.ItemsSource = objCashDeskManager.GetRouteCollection();
            cboRoute.DisplayMemberPath = "Route_Name";
            cboRoute.SelectedValuePath = "Route_No";
            cboRoute.SelectedValue = 0;
        }

        private void btnRefreshTime_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                dtpEndtime.SelectedTime = DateTime.Now.TimeOfDay;
                dtpEndDate.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTicketAnomalies_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID282", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID283", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID284", BMC_Icon.Information);
                    return;
                }
                IReports objReports = ReportsBusinessObject.CreateInstance();
                DataSet TicketAnomalies = null;
                TicketAnomalies = objReports.GetTicketAnomalies(StartDate, EndDate);
                LogManager.WriteLog("Fetching report data from database...", LogManager.enumLogLevel.Info);
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database Ticket Anomalies Report", LogManager.enumLogLevel.Info);
                    cReportViewer.ShowTicketAnomaliesReport(StartDate, EndDate);
                    cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));

                    cReportViewer.Show();
                }

                LogManager.WriteLog("Ticket Anomalies Report Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID282", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID283", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID284", BMC_Icon.Information);
                    return;
                }




            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

    }

}
