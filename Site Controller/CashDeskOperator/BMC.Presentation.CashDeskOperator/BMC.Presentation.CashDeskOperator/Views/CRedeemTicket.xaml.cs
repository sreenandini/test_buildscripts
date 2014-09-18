using System;
using System.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using BMC.Common.Utilities;
using BMC.Transport;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using System.Globalization;
using BMC.Presentation.POS.Helper_classes;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.UserControls;
using BMC.Business.CashDeskOperator;
using BMC.Presentation.POS.Views;
using System.Windows.Navigation;
using BMC.Presentation.POS;
using System.Collections.Generic;using BMC.DBInterface.CashDeskOperator;
namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for RedeemTicketOnline.xaml
    /// </summary>
    public partial class CRedeemTicket
    {
        DispatcherTimer disptimerRedeem;
        private POS.Views.CRedeemOfflineTicket frmRedeemOffline;
        public delegate void ScannerClick(object sender, RoutedEventArgs e);
        public delegate void ClickClear(object sender);
        private BMC.Presentation.POS.Views.CustomerDetails customerDetails;
        private bool ProcessCancelled = false;
        private long Custid = 0;
        //private int redeemTicketAmount = 0;
        private bool bisTicketExpired = false;
        private bool isScannerFired = false;
        private CashDispenserWorker _worker = null;
        private System.Windows.Forms.DialogResult _diagResult;

        public CRedeemTicket()
        {
            this.InitializeComponent();
            this.txtStatus.GotFocus += new RoutedEventHandler(txtStatus_GotFocus);
            this.txtPrintedMachine.GotFocus += new RoutedEventHandler(txtPrintedMachine_GotFocus);
            this.txtClaimedDevice.GotFocus += new RoutedEventHandler(txtClaimedDevice_GotFocus);
            this.txtClaimedDate.GotFocus += new RoutedEventHandler(txtClaimedDate_GotFocus);
            this.txtTickAmount.GotFocus += new RoutedEventHandler(txtTickAmount_GotFocus);
            _worker = new CashDispenserWorker(this, ModuleName.Voucher);

            this.dispenserStatus.Visibility = Settings.IsGloryCDEnabled ? Visibility.Hidden : CashDispenserWorker.Visibliity;

        }

        void txtTickAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ucValueCalc.txtDisplay.Focus();
        }

        void txtClaimedDate_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ucValueCalc.txtDisplay.Focus();
        }

        void txtClaimedDevice_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ucValueCalc.txtDisplay.Focus();
        }

        void txtPrintedMachine_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ucValueCalc.txtDisplay.Focus();
        }

        void txtStatus_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ucValueCalc.txtDisplay.Focus();
        }

        void disptimerRedeem_Tick(object sender, EventArgs e)
        {
            if (!bisTicketExpired)
            {
                ClearAll(false);
            }
            bisTicketExpired = false;
            disptimerRedeem.Stop();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = false;
            RTOnlineTicketDetail objTicketDetail = null;
           
            double redeemTicketAmount = 0;

            bool IsCashDispenseError = false;
            try
            {

                if (isScannerFired) //check done not to fire the verify event twice while verifying a ticket using scanner
                {
                    isScannerFired = false;
                    return;
                }
                ClearAll(true);
                TextBlock_11.Text = string.Empty;
                txtAmount.Text = string.Empty;               
                btnVerify.IsEnabled = false;
                if ((sender is System.Windows.Controls.TextBox))
                    isScannerFired = true;
                else
                    isScannerFired = false;

                if (this.ucValueCalc.txtDisplay.Text.Trim().Length > 0)
                {
                    if (this.ucValueCalc.txtDisplay.Text.Trim().Length < 4 )
                    {
                        MessageBox.ShowBox("MessageID403", BMC_Icon.Error);
                        this.txtStatus.Visibility = Visibility.Hidden;
                        this.ucValueCalc.txtDisplay.Text = String.Empty;
                        return;
                    }

                    IRedeemOnlineTicket objCashDeskOper = RedeemOnlineTicketBusinessObject.CreateInstance();
                    objTicketDetail = new RTOnlineTicketDetail();

                    objTicketDetail.ClientSiteCode = Settings.SiteCode;
                    objTicketDetail.TicketString = this.ucValueCalc.txtDisplay.Text.Trim();
                    objTicketDetail.HostSiteCode = objTicketDetail.TicketString.Substring(0, 4);
                    objTicketDetail.RedeemedMachine = System.Environment.MachineName;

                    objTicketDetail = objCashDeskOper.GetRedeemTicketAmount(objTicketDetail);

                    if (objTicketDetail.TicketStatusCode == -990) //TIS Error (If any)
                    {
                        MessageBox.ShowBox(objTicketDetail.TicketErrorMessage, BMC_Icon.Error, true);
                        disptimerRedeem.Stop();
                        this.txtStatus.Visibility = Visibility.Hidden;
                        return;
                    }

                    if (objTicketDetail.TicketStatusCode == -234) //Exception Occured
                    {
                        MessageBox.ShowBox("MessageID425", BMC_Icon.Error);
                        disptimerRedeem.Stop();
                        this.txtStatus.Visibility = Visibility.Hidden;
                        return;
                    }

                    if (objTicketDetail.TicketStatusCode == -99) //Included for Cross Ticketing
                    {
                        MessageBox.ShowBox("MessageID403", BMC_Icon.Error);
                        disptimerRedeem.Stop();
                        this.txtStatus.Visibility = Visibility.Hidden;
                        this.ucValueCalc.txtDisplay.Text = String.Empty;
                        return;
                    }

                   
                   
                        if (objTicketDetail.TicketStatusCode == 250) //Any/specific player card required for TIS
                        {
                            bool ValidCard = false;
                            int PlayerCardClose = 0;
                            


                            if (objTicketDetail.CardRequired == 2)
                            {
                                PlayerCardValidation objPlayerCardValidation = new PlayerCardValidation(objTicketDetail.CardRequired);
                                objPlayerCardValidation.ShowDialogEx(this);
                                ValidCard = objPlayerCardValidation.valid;
                                PlayerCardClose = objPlayerCardValidation.Close;

                                if (PlayerCardClose == 1)
                                {
                                    this.ucValueCalc.txtDisplay.Text = String.Empty;
                                    this.ucValueCalc.txtDisplay.Focus();
                                    return;
                                }

                                else if (!ValidCard)
                                {
                                    MessageBox.ShowBox("PlayerCardRedeem6", BMC_Icon.Error);
                                    this.ucValueCalc.txtDisplay.Text = String.Empty;
                                    this.ucValueCalc.txtDisplay.Focus();
                                    return;
                                }

                            }
                            else if (objTicketDetail.CardRequired == 1)
                            {
                                PlayerCardValidation objPlayerCardValidation = new PlayerCardValidation(objTicketDetail.CardRequired, objTicketDetail.PlayerCardNumber);
                                objPlayerCardValidation.ShowDialogEx(this);
                                ValidCard = objPlayerCardValidation.valid;
                                PlayerCardClose = objPlayerCardValidation.Close;
                                if (PlayerCardClose == 1)
                                {
                                    this.ucValueCalc.txtDisplay.Text = String.Empty;
                                    this.ucValueCalc.txtDisplay.Focus();
                                    return;
                                }
                                else if (!ValidCard)
                                {
                                    MessageBox.ShowBox("PlayerCardRedeem6", BMC_Icon.Error);
                                    this.ucValueCalc.txtDisplay.Text = String.Empty;
                                    this.ucValueCalc.txtDisplay.Focus();
                                    return;
                                }
                            }

                            objTicketDetail.TicketStatusCode = 0;
                        }
                    

                    int ticketAmount = Convert.ToInt32(objTicketDetail.RedeemedAmount);

                    redeemTicketAmount = ticketAmount / 100;

                    objTicketDetail.CustomerId = Custid;

                    this.txtStatus.Visibility = Visibility.Visible;
                    this.txtStatus.Text = "VALIDATING.";
                    disptimerRedeem.IsEnabled = true;
                    disptimerRedeem.Start();


                    //if ((objCashDeskOper.CheckSDGTicket(objTicketDetail.TicketString) == 0) && (Settings.RedeemConfirm))
                    if (objTicketDetail.TicketStatusCode == 0 && (Settings.RedeemConfirm))
                    {
                        disptimerRedeem.Stop();
                        if (Convert.ToBoolean(AppSettings.REDEEM_TICKET_POP_UP_ALERT_VISIBILITY))
                        {
                            _diagResult = MessageBox.ShowBox("MessageID225", BMC_Icon.Question, BMC_Button.YesNo);
                        }
                        else
                        {
                            _diagResult = System.Windows.Forms.DialogResult.Yes;
                        }
                        if (_diagResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            #region ITALY CODE COMMENTED
                            //if (Settings.RegulatoryEnabled == true && Settings.RegulatoryType == "AAMS")
                            //{
                            //    ProcessCancelled = false;
                            //    if (ticketStatus == 0)
                            //    {
                            //        if (redeemTicketAmount >= Settings.RedeemTicketCustomer_Min && redeemTicketAmount <= Settings.RedeemTicketCustomer_Max)
                            //        {
                            //            customerDetails = new BMC.Presentation.POS.Views.CustomerDetails();
                            //            customerDetails.delCustomerUpdated += new BMC.Presentation.POS.Views.CustomerDetails.CustomerUpdateHandler(delCustomerUpdated);
                            //            customerDetails.delCustomerCancelled += new BMC.Presentation.POS.Views.CustomerDetails.CustomerCancelHandler(delCustomerCancelled);
                            //            Owner = Window.GetWindow(this);
                            //            customerDetails.ShowDialog();
                            //        }
                            //        else if (redeemTicketAmount >= Settings.RedeemTicketCustomer_BankAcctNo)
                            //        {
                            //            customerDetails = new BMC.Presentation.POS.Views.CustomerDetails(true);
                            //            customerDetails.delCustomerUpdated += new BMC.Presentation.POS.Views.CustomerDetails.CustomerUpdateHandler(delCustomerUpdated);
                            //            customerDetails.delCustomerCancelled += new BMC.Presentation.POS.Views.CustomerDetails.CustomerCancelHandler(delCustomerCancelled);
                            //            Owner = Window.GetWindow(this);
                            //            customerDetails.ShowDialog();
                            //        }

                            //        if (ProcessCancelled)
                            //        {
                            //            MessageBox.ShowBox("MessageID299", BMC_Icon.Information);
                            //            this.ucValueCalc.txtDisplay.Text = string.Empty;
                            //            return;
                            //        }
                            //    }
                            //} 
                            #endregion

                            objTicketDetail = objCashDeskOper.RedeemOnlineTicket(objTicketDetail);
                            isValid = objTicketDetail.ValidTicket;
                        }
                        else
                        {
                            disptimerRedeem.Start();
                            this.txtStatus.Visibility = Visibility.Hidden;
                            return;
                        }
                        disptimerRedeem.Start();
                    }
                    //else if ((objCashDeskOper.CheckSDGTicket(objTicketDetail.TicketString) == -3) && (Settings.RedeemConfirm)
                    else if (objTicketDetail.TicketStatusCode == -3 && (Settings.RedeemConfirm) && Settings.RedeemExpiredTicket)
                    {
                        disptimerRedeem.Stop();

                        CAuthorize objAuthorize = null;
                        //Manoj 26th Aug 2010. CR383384
                        //RedeemExpiredTicket functionality has been implmented for Winchells.
                        //So, in settings RedeemExpiredTicket will be True for Winchells, for rest it will be False.
                        //So we dont need the following if condition.
                        //if (Settings.Client != null && Settings.Client.ToLower() == "winchells")
                        //{
                        objAuthorize = new CAuthorize("CashdeskOperator.Authorize.cs.ReedemExpiredTicket");
                        objAuthorize.User = Security.SecurityHelper.CurrentUser;
                        string Cur_User_Name = Security.SecurityHelper.CurrentUser.Last_Name + ", " + Security.SecurityHelper.CurrentUser.First_Name;
                        objTicketDetail.RedeemedUser = Cur_User_Name;
                        
                        if (!Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.ReedemExpiredTicket"))
                        {
                            objAuthorize.ShowDialogEx(this);
                            
                            string Auth_User_Name = objAuthorize.User.UserName;
                            if (objAuthorize.User.Last_Name != null && objAuthorize.User.First_Name != null)
                            {
                                Auth_User_Name = objAuthorize.User.Last_Name + ", " + objAuthorize.User.First_Name;
                            }
                            else
                            {
                                Auth_User_Name = objAuthorize.User.UserName;
                            }

                            objTicketDetail.RedeemedUser = Auth_User_Name;
                            
                            if (!objAuthorize.IsAuthorized)
                            {
                                ClearAll(false);
                                return;
                            }
                        }
                        else
                        {
                            objAuthorize.IsAuthorized = true;
                        }
                        //}

                        if (objAuthorize != null && objAuthorize.IsAuthorized)
                        {
                            objTicketDetail.AuthorizedUser_No = objAuthorize.User.SecurityUserID;
                            objTicketDetail.Authorized_Date = DateTime.Now;

                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.Voucher,
                                Audit_Screen_Name = "Vouchers|RedeemVoucher",
                                Audit_Desc = "Voucher Number-" + objTicketDetail.TicketString,
                                AuditOperationType = OperationType.MODIFY,
                                Audit_Field = "AuthorizedUser_No",
                                Audit_New_Vl = Security.SecurityHelper.CurrentUser.SecurityUserID.ToString()
                            });

                        }
                        if (Convert.ToBoolean(AppSettings.REDEEM_TICKET_POP_UP_ALERT_VISIBILITY))
                        {
                            _diagResult = MessageBox.ShowBox("MessageID225", BMC_Icon.Question, BMC_Button.YesNo);
                        }
                        else
                        {
                            _diagResult = System.Windows.Forms.DialogResult.Yes;
                        }
                        if (_diagResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            #region ITALY CODE COMMENTED
                            //if (Settings.RegulatoryEnabled == true && Settings.RegulatoryType == "AAMS")
                            //{
                            //    ProcessCancelled = false;
                            //    if (ticketStatus == 0)
                            //    {
                            //        if (redeemTicketAmount >= Settings.RedeemTicketCustomer_Min && redeemTicketAmount <= Settings.RedeemTicketCustomer_Max)
                            //        {
                            //            customerDetails = new BMC.Presentation.POS.Views.CustomerDetails();
                            //            customerDetails.delCustomerUpdated += new BMC.Presentation.POS.Views.CustomerDetails.CustomerUpdateHandler(delCustomerUpdated);
                            //            customerDetails.delCustomerCancelled += new BMC.Presentation.POS.Views.CustomerDetails.CustomerCancelHandler(delCustomerCancelled);
                            //            Owner = Window.GetWindow(this);
                            //            customerDetails.ShowDialog();
                            //        }
                            //        else if (redeemTicketAmount >= Settings.RedeemTicketCustomer_BankAcctNo)
                            //        {
                            //            customerDetails = new BMC.Presentation.POS.Views.CustomerDetails(true);
                            //            customerDetails.delCustomerUpdated += new BMC.Presentation.POS.Views.CustomerDetails.CustomerUpdateHandler(delCustomerUpdated);
                            //            customerDetails.delCustomerCancelled += new BMC.Presentation.POS.Views.CustomerDetails.CustomerCancelHandler(delCustomerCancelled);
                            //            Owner = Window.GetWindow(this);
                            //            customerDetails.ShowDialog();
                            //        }

                            //        if (ProcessCancelled)
                            //        {
                            //            MessageBox.ShowBox("MessageID299", BMC_Icon.Information);
                            //            this.ucValueCalc.txtDisplay.Text = string.Empty;
                            //            return;
                            //        }
                            //    }
                            //}

                            #endregion
                            objTicketDetail = objCashDeskOper.RedeemOnlineTicket(objTicketDetail);
                            isValid = objTicketDetail.ValidTicket;
                        }
                        else
                        {
                            disptimerRedeem.Start();
                            this.txtStatus.Visibility = Visibility.Hidden;
                            return;
                        }
                        disptimerRedeem.Start();
                    }
                    else
                    {
                        objTicketDetail = objCashDeskOper.RedeemOnlineTicket(objTicketDetail);
                        isValid = objTicketDetail.ValidTicket;
                    }

                    if (objTicketDetail.TicketStatus == "MessageID210")
                        objTicketDetail.TicketStatus = Application.Current.FindResource(objTicketDetail.TicketStatus).ToString() +
                            "(" + CommonUtilities.GetCurrency(Convert.ToDouble(objTicketDetail.TicketValue / 100)) + ")";
                    else
                        objTicketDetail.TicketStatus = Application.Current.FindResource(objTicketDetail.TicketStatus).ToString();

                    IsCashDispenseError = true;
                    if (isValid && objTicketDetail.RedeemedMachine != null && objTicketDetail.RedeemedMachine != string.Empty)
                    {
                        try
                        {
                            //DateTime PrintDate;
                            //string strbar_pos = objCashDeskOper.GetTicketPrintDevice(objTicketDetail.TicketString, out PrintDate);
                            DateTime PrintDate = DateTime.Now;
                            string strbar_pos = objCashDeskOper.GetTicketPrintDevice(objTicketDetail.TicketString, out PrintDate);
                            //TextBlock_11.Text = "#" + strbar_pos + PrintDate.ToString().Replace("/", "").Replace(":", "").Replace("AM", "0").Replace("PM", "1").Replace(" ", "");
                            //TextBlock_11.Text = "#" + objTicketDetail.PrintedDevice + objTicketDetail.PrintedDate.ToString().Replace("/", "").Replace(":", "").Replace("AM", "0").Replace("PM", "1").Replace(" ", "");
                            if (objTicketDetail.RedeemedDate == null || objTicketDetail.RedeemedDate.ToString().Trim().Equals(string.Empty))
                            {
                                TextBlock_11.Text = string.Empty;
                            }
                            else
                            {
                                TextBlock_11.Text = "#" + strbar_pos + objTicketDetail.RedeemedDate.ToString("ddMMyyyyHHmmss");
                            }

                            txtAmount.Text = objTicketDetail.RedeemedAmount.GetUniversalCurrencyFormat();
                            if (!objTicketDetail.TicketStatus.Trim().ToUpper().Equals("ALREADY CLAIMED"))
                            {
                                #region GCD
                                if (Settings.IsGloryCDEnabled && Settings.CashDispenserEnabled)
                                {
                                    LogManager.WriteLog(string.Format("Process Redeem Voucher: {2} for Bar Postion: {0} - Amount: {1} in cents", strbar_pos, ticketAmount, objTicketDetail.TicketString), LogManager.enumLogLevel.Info);

                                    //implement Cash Dispenser
                                    LogManager.WriteLog(string.Format("Amount: {0:0.00} Sending to Cash Dispenser ", ticketAmount), LogManager.enumLogLevel.Info);
                                    LoadingWindow ld = new LoadingWindow(Window.GetWindow(this), ModuleName.Voucher, TextBlock_11.Text , strbar_pos, ticketAmount);
                                    ld.Topmost = true;
                                    ld.ShowDialogEx(this);
                                    Result res = ld.Result;
                                    if (!res.IsSuccess)
                                    {
                                        IsCashDispenseError = false;
                                        this.txtStatus.Text = res.error.Message;
                                        LogManager.WriteLog(string.Format("Unable to Dispense Cash - Amount: {0}", ticketAmount), LogManager.enumLogLevel.Info);
                                        LogManager.WriteLog("Rollback Redeem Voucher Process", LogManager.enumLogLevel.Info);
                                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                        {
                                            AuditModuleName = ModuleName.Voucher,
                                            Audit_Screen_Name = "Vouchers|RedeemVoucher",
                                            Audit_Desc = "Rollback redeem voucher:" + objTicketDetail.TicketString + " due to cash dispenser error",
                                            AuditOperationType = OperationType.MODIFY,
                                            Audit_Old_Vl = "iPayDeviceid:" + objTicketDetail.RedeemedDevice + ";dtPaid:" + objTicketDetail.RedeemedDate.GetUniversalDateTimeFormat() + ";Customerid:" + objTicketDetail.CustomerId.ToString()
                                        });
                                        objCashDeskOper.RollbackRedeemTicket(objTicketDetail.TicketString);
                                        if (Convert.ToBoolean(AppSettings.REDEEM_TICKET_POP_UP_ALERT_VISIBILITY))
                                        {
                                            BMC.Presentation.MessageBox.ShowBox(res.error.Message, res.error.MessageType.Equals("Error") ? BMC_Icon.Error : BMC_Icon.Information, true);
                                        }
                                    }
                                    else
                                    {
                                        LogManager.WriteLog(string.Format("Cash Dispensed Successfully - Amount: {0}", ticketAmount), LogManager.enumLogLevel.Info);

                                        IsCashDispenseError = true;
                                        if (Convert.ToBoolean(AppSettings.REDEEM_TICKET_POP_UP_ALERT_VISIBILITY))
                                        {
                                            BMC.Presentation.MessageBox.ShowBox(res.error.Message, res.error.MessageType.Equals("Error") ? BMC_Icon.Error : BMC_Icon.Information, true);
                                        }

                                    }
                                }
                                #endregion
                            }

                        }
                        catch (Exception Ex)
                        {
                            LogManager.WriteLog("Error showing Voucher Info :" + Ex.Message, LogManager.enumLogLevel.Error);
                        }
                    }

                    if (objTicketDetail.ShowOfflineTicketScreen)
                    {
                        int result;

                        if (objTicketDetail.HostSiteCode == Settings.SiteCode) // Offline Tickets redemption is valid only for local site code
                            result = objCashDeskOper.CheckSDGOfflineTicket(objTicketDetail.TicketString);
                        else
                            result = -14;

                        if (result == -14)// Site Code Mismatch
                        {
                            this.txtStatus.Visibility = Visibility.Visible;
                            this.txtStatus.Text = Application.Current.FindResource("MessageID312") as string;
                            this.txtStatus.Background = System.Windows.Media.Brushes.Red;
                            return;
                        }
                        else
                        {
                            frmRedeemOffline = new BMC.Presentation.POS.Views.CRedeemOfflineTicket();
                            frmRedeemOffline.TicketNumber = ucValueCalc.txtDisplay.Text.Trim();
                            frmRedeemOffline.ShowDialogEx(this);
                            if(frmRedeemOffline.IsSuccessfull)
                            {
                                this.ucValueCalc.txtDisplay.Text = frmRedeemOffline.TicketNumber;
                                button_Click(sender, e);
                            }
                            else
                            {
                                this.ucValueCalc.txtDisplay.Text = string.Empty;
                                this.txtStatus.Clear();
                            }
                        }
                    }
                    else
                    {
                        if (Settings.IsGloryCDEnabled && (!IsCashDispenseError))
                        {
                            disptimerRedeem.Stop();
                            this.txtStatus.Visibility = Visibility.Visible;
                            this.txtStatus.Background = System.Windows.Media.Brushes.Red;
                            TextBlock_11.Text = string.Empty;
                            txtAmount.Text = string.Empty;
                            System.Threading.Thread.Sleep(100);
                            //System.Threading.Thread.CurrentThread
                            disptimerRedeem.Start();
                        }
                        else
                        {
                            this.txtStatus.Text = objTicketDetail.TicketStatus;
                            //"(" + CommonUtilities.GetCurrency(Convert.ToDouble(TicketDetail.TicketValue / 100)) + ")";
                            if (Application.Current.FindResource("MessageID219").ToString() == objTicketDetail.TicketStatus)
                            {
                                bisTicketExpired = true;
                            }
                            this.txtWarning.Text = objTicketDetail.TicketWarning;
                            if (!objTicketDetail.ValidTicket)
                            {
                                this.txtStatus.Background = System.Windows.Media.Brushes.Red;

                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {

                                    AuditModuleName = ModuleName.Voucher,
                                    Audit_Screen_Name = "Vouchers|RedeemVoucher",
                                    Audit_Desc = "Invalid Voucher Redemption Attempt",
                                    AuditOperationType = OperationType.MODIFY,
                                    Audit_Field = "Voucher Number",
                                    Audit_New_Vl = objTicketDetail.TicketString
                                });

                            }
                            else
                            {
                                this.txtStatus.Background = System.Windows.Media.Brushes.White;

                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {

                                    AuditModuleName = ModuleName.Voucher,
                                    Audit_Screen_Name = "Vouchers|RedeemVoucher",
                                    Audit_Desc = "Voucher Number-" + objTicketDetail.TicketString,
                                    AuditOperationType = OperationType.ADD,
                                    Audit_Field = "Voucher Status",
                                    Audit_New_Vl = objTicketDetail.TicketStatus
                                });

                                //Cross Ticketing- Insert Local Record 
                                if (!string.IsNullOrEmpty(objTicketDetail.VoucherXMLData))
                                {
                                    objTicketDetail.RedeemedUser = Security.SecurityHelper.CurrentUser.UserName;
                                    objCashDeskOper.ImportVoucherDetails(objTicketDetail);
                                }

                                if (objTicketDetail.TicketStatus.Contains(Application.Current.FindResource("MessageID210").ToString()))
                                {
                                    //disptimerRedeem.Stop();

                                    Action act = new Action(() =>
                                    {
                                        if (Convert.ToBoolean(AppSettings.REDEEM_TICKET_POP_UP_ALERT_VISIBILITY))
                                        {
                                            if (IsCashDispenseError)
                                            {
                                                if (Settings.IsGloryCDEnabled)
                                                {
                                                    disptimerRedeem.Stop();
                                                    this.txtStatus.Visibility = Visibility.Visible;
                                                }
                                                else
                                                {
                                                    this.ClearAll(false);
                                                }
                                                MessageBox.ShowBox("MessageID377", BMC_Icon.Information);
                                            }


                                        }
                                        else
                                        {
                                            this.txtStatus.Text = Application.Current.FindResource("MessageID377").ToString();
                                            txtAmount.Text = Convert.ToDecimal(objTicketDetail.TicketValue / 100).GetUniversalCurrencyFormat();
                                            bisTicketExpired = false;
                                            this.txtStatus.Background = System.Windows.Media.Brushes.GreenYellow;
                                        }
                                        if (!Settings.IsGloryCDEnabled)
                                        {
                                            this.dispenserStatus.LoadItemsAysnc();
                                        }
                                    });

                                    if (!Settings.IsGloryCDEnabled)
                                    {
                                        _worker.Dispense("Voucher Number", objTicketDetail.TicketString, objTicketDetail.RedeemedAmount, act);

                                    }
                                    else
                                    {
                                        act();
                                        disptimerRedeem.Start();
                                    }

                                }
                            }
                            if (objTicketDetail.EnableTickerPrintDetails)
                            {
                                this.gridRedeemedTicket.Visibility = Visibility.Visible;
                                this.txtPrintedMachine.Text = objTicketDetail.RedeemedMachine;
                                this.txtClaimedDevice.Text = objTicketDetail.RedeemedDevice;
                                this.txtTickAmount.Text = objTicketDetail.RedeemedAmount.GetUniversalCurrencyFormatWithSymbol();
                                this.txtClaimedDate.Text = objTicketDetail.RedeemedDate.GetUniversalDateTimeFormat();
                            }
                            else
                            {
                                this.gridRedeemedTicket.Visibility = Visibility.Hidden;
                            }
                            this.ucValueCalc.txtDisplay.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID105", BMC_Icon.Warning);
                    this.ucValueCalc.txtDisplay.Focus();
                }
            }
            catch (Exception ex)
            {
                BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                btnVerify.IsEnabled = true;
            }
        }

        /// <summary>
        /// Clears all the contents of the form.
        /// </summary>
        public void ClearAll(bool onLoad)
        {
            this.gridRedeemedTicket.Visibility = Visibility.Hidden;
            this.txtPrintedMachine.Text = "";
            this.txtClaimedDevice.Text = "";
            this.txtClaimedDate.Text = "";
            this.txtTickAmount.Text = "";
            this.txtStatus.Text = "";
            this.txtStatus.Visibility = Visibility.Hidden;
            this.txtStatus.Background = Brushes.White;

            if (!onLoad)
            {
                this.ucValueCalc.txtDisplay.Text = "";
                this.ucValueCalc.Focus();
            }

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ucValueCalc.txtDisplay.Clear();
            this.txtStatus.Visibility = Visibility.Hidden;
            int disp_time = 0;
            try
            {
                disp_time = Convert.ToInt32(ConfigManager.Read("ReedeemScreenDisplayTime").ToString());
                if (disp_time <= 0)
                {
                    disp_time = 10;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                disp_time = 10;
            }
            disptimerRedeem = new DispatcherTimer();
            disptimerRedeem.Interval = new TimeSpan(0, 0, disp_time);
            disptimerRedeem.Tick += new EventHandler(disptimerRedeem_Tick);
            disptimerRedeem.Stop();
            disptimerRedeem.IsEnabled = false;

            this.ucValueCalc.ClickClearEvent += new ClickClear(ucValueCalc_ClickClearEvent);

            this.ucValueCalc.ClickEvent += new ScannerClick(button_Click);

            IRedeemOnlineTicket RedeemObject = RedeemOnlineTicketBusinessObject.CreateInstance();
            if (RedeemObject.CheckLaunderingEnabled())
            {
                this.txtWarning.Visibility = Visibility.Visible;
                this.lblWarning.Visibility = Visibility.Visible;
            }
            else
            {
                this.txtWarning.Visibility = Visibility.Hidden;
                this.lblWarning.Visibility = Visibility.Hidden;
            }

            this.ucValueCalc.txtDisplay.Focus();
        }

        void ucValueCalc_ClickClearEvent(object sender)
        {
            ClearAll(false);
            TextBlock_11.Text = "";
            txtAmount.Text = "";
        }

       

        //Delegates to identify  cancel initiated in the customer screen
        void delCustomerCancelled(object sender, RoutedEventArgs e)
        {
            ProcessCancelled = true;
        }

        //Delegates to identify save  initiated in the customer screen
        void delCustomerUpdated(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Custid = Convert.ToInt64(customerDetails.txtCustID.Text.ToString());
            LogManager.WriteLog("Customer id:" + Custid.ToString(), LogManager.enumLogLevel.Info);
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
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.btnVerify.Click -= (this.button_Click);
                        if (dispenserStatus != null)
                        {
                            dispenserStatus.KillLoadItemsThread();
                        }
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CRedeemTicket objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CRedeemTicket"/> is reclaimed by garbage collection.
        /// </summary>
        ~CRedeemTicket()
        {
            Dispose(false);
        }

        #endregion

        private void btnGetVoucherInfo_Click(object sender, RoutedEventArgs e)
        {
            string strBarCode = ucValueCalc.txtDisplay.Text.Trim();
            int InstallationNumber = 0;
            int ValidationLength = 0;
            int CurrLength = strBarCode.Trim().Length;
            try
            {
                if (this.ucValueCalc.txtDisplay.Text.Trim().Length > 0)
                {
                    if (strBarCode.Length < 4)
                    {
                        MessageBox.ShowBox("MessageID403", BMC_Icon.Error);
                        return;
                    }
                    if (strBarCode.Length != 18)
                    {
                        LinqDataAccessDataContext linqDBExchange = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
                        IEnumerable<InstallationFromTicket> InstallationTicket = linqDBExchange.GetInstallationNumber(strBarCode);

                        if (InstallationTicket != null)
                        {
                            foreach (var item in InstallationTicket)
                            {
                                InstallationNumber = item.installation_no.Value;
                                strBarCode = item.strbarcode;
                               
                            }

                            DataTable InstallationDetails = (new CommonDataAccess()).GetInstallationDetails(0, InstallationNumber, false, false);
                            if (InstallationDetails.Rows.Count > 0)
                            {
                               
                                    int.TryParse(InstallationDetails.Rows[0]["Validation_length"].ToString(), out ValidationLength);
                                    if (ValidationLength != CurrLength)
                                    {
                                        MessageBox.ShowBox("MessageID403", BMC_Icon.Error);
                                        return;
                                    }

                            }
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID403", BMC_Icon.Error);
                            return;
                        }
                        
                    }
                   
                    VoucherDetails objVoucherDetails = new VoucherDetails(strBarCode);
                    
                    if (objVoucherDetails.IsVoucherFound)
                    {
                       // Window owner = Window.GetWindow(this);
                        objVoucherDetails.ShowDialogEx(this);
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID213", BMC_Icon.Information);  //Voucher Not Found
                    }
                }
                else
                {
                     MessageBox.ShowBox("MessageID264", BMC_Icon.Information);  //Enter Valid Voucher Bar code
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

       
    }
}