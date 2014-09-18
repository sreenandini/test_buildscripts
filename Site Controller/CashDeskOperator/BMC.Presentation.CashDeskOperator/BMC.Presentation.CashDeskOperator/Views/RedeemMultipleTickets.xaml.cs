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
using BMC.Transport;
using BMC.Common;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ConfigurationManagement;
using System.Windows.Threading;
using BMC.Business.CashDeskOperator;
using BMC.Presentation.POS.Helper_classes;
using BMC.DBInterface.CashDeskOperator;
using System.Data;
using BMC.CoreLib.WPF;
using BMC.CoreLib.Win32;
using Audit.BusinessClasses;
using Audit.Transport;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for RedeemMultipleTickets.xaml
    /// </summary>
    public partial class RedeemMultipleTickets 
    {
       
        #region Variables

        private bool disposed = false;
        private string strBarcode = string.Empty;
        private int iVoucherLength = 18;
        List<ReedemTicketInfo> _objTickets = new List<ReedemTicketInfo>();
        Thread _RedeemThread = null;
        public AutoResetEvent _AutoReset = new AutoResetEvent(false);
        //RTOnlineTicketDetail _objOnlineTicketDetail = null;
        RTOnlineTicketDetail _objOnlineTicketDetail = new RTOnlineTicketDetail();
        private System.Windows.Forms.DialogResult _diagResult;
        private int _idisplayTime = 0;
        DispatcherTimer dispTimer;
        Action<string> _ProcessingStatus = null;
        string ProcessState = string.Empty;
        public int iTISPrefix = 0;
        #endregion Variables

        public RedeemMultipleTickets()
        {            
            InitializeComponent();
            
        }

       
        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadControls();
            try
            {
                LogManager.WriteLog("getting the Auto clear time from the config file", LogManager.enumLogLevel.Info);
                _idisplayTime = Convert.ToInt32(ConfigManager.Read("MultipleTicketRedeemAutoClearGrid"));                
                iTISPrefix = Settings.TISTicketPrefix;
                LogManager.WriteLog("The TIS voucher prefix is " + iTISPrefix, LogManager.enumLogLevel.Info);
            }
            catch (Exception)
            {
                _idisplayTime = 10;

            }
            dispTimer = new DispatcherTimer();
            if (_idisplayTime > 0)
            {
                dispTimer.Interval = new TimeSpan(0, 0, _idisplayTime);
                dispTimer.Tick += new EventHandler(dispTimer_Tick);
            }
        }

        private void dispTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside dispTimer_Tick", LogManager.enumLogLevel.Info);
                Dispatcher.Invoke(new Action(() =>
                {
                  
                    ClearAll();
                    txtBarcodeVal.Focus();
                    dispTimer.Stop();
                }
                    ));
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside Window_Unloaded()", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.Close();
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }

        }

        private void txtBarcodeVal_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                LogManager.WriteLog("Inside txtBarcodeVal_KeyUp", LogManager.enumLogLevel.Info);              
                if (e.Key == Key.Enter && txtBarcodeVal.Text.Trim() != string.Empty)
                {
                    AddingToGrid(txtBarcodeVal.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtBarcodeVal_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            try
            {

                LogManager.WriteLog("Inside txtBarcodeVal_PreviewMouseUp() Method", LogManager.enumLogLevel.Info);
                txtErrorStatus.Clear();
                strBarcode = DisplayNumberPad(txtBarcodeVal.Text);
                txtBarcodeVal.Text = strBarcode;
                //if (txtBarcodeVal.Text.Trim().Length == this.iVoucherLength)
                //{
                //    AddingToGrid(strBarcode);
                //}
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void btnStopRedeem_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                LogManager.WriteLog("Inside btnStopRedeem_Click", LogManager.enumLogLevel.Info);
                txtErrorStatus.Text = string.Empty;               
                _AutoReset.Set();
                btnStopRedeem.Visibility = System.Windows.Visibility.Hidden;
                dispTimer.Stop(); 
			}                  
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClearRedeem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnClearRedeem_Click, Clearing all controls", LogManager.enumLogLevel.Info);
               
                ClearAll();
                btnRedeemTickets.Visibility = System.Windows.Visibility.Visible;
                btnStopRedeem.IsEnabled = false;
                dispTimer.Stop();

            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                LogManager.WriteLog("Inside btnRemove_Click,removing the item from the list", LogManager.enumLogLevel.Info);
                txtErrorStatus.Clear();
                var item = (sender as FrameworkElement).DataContext;
                _objTickets.Remove((ReedemTicketInfo)item);
                decimal dTotalAmount = 0;
                int iTicketcount = 0;
                _objTickets.ForEach((x) =>
                {
                    if (x.TicketStatus == 0)
                    {
                        dTotalAmount += x.Amount; iTicketcount++;
                    }
                    else if (x.TicketStatus == -14 && Settings.RedeemConfirm && Settings.RedeemExpiredTicket && Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket"))
                    {
                        dTotalAmount += x.Amount; iTicketcount++;
                    }
                    else if (x.TicketStatus == -15 && Settings.RedeemConfirm && Settings.RedeemExpiredTicket && Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket"))
                    {
                        dTotalAmount += x.Amount; iTicketcount++;
                    }
                    else if (x.TicketStatus == 1)
                    {
                        dTotalAmount += x.Amount; iTicketcount++;
                    }
                });
                txtAmountVal.Text = dTotalAmount.ToString();
                txtCountVal.Text = iTicketcount.ToString();
                LogManager.WriteLog("The Item Removed from the list is:" + item, LogManager.enumLogLevel.Info);
                lvTickets.ItemsSource = null;
                lvTickets.ItemsSource = _objTickets;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRedeemTickets_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnRedeemTickets_Click", LogManager.enumLogLevel.Info);
                txtAmount.Visibility = System.Windows.Visibility.Visible;
                txtCount.Visibility = System.Windows.Visibility.Visible;
                if (lvTickets.Items.Count > 0)
                {

                    txtErrorStatus.Clear();

                    btnRedeemTickets.Visibility = System.Windows.Visibility.Hidden;

                    btnClearRedeem.Visibility = System.Windows.Visibility.Hidden;



                    if (Convert.ToBoolean(AppSettings.REDEEM_TICKET_POP_UP_ALERT_VISIBILITY))
                    {
                        _diagResult = MessageBox.ShowBox("RedeemMultipleTicketConfirmationID1", BMC_Icon.Information, BMC_Button.YesNo);
                    }
                    else
                    {
                        _diagResult = System.Windows.Forms.DialogResult.Yes;
                    }

                    if (_diagResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            LogManager.WriteLog("Initializing the Redeem Thread", LogManager.enumLogLevel.Info);
                            _AutoReset.Reset();
                            DisableRemoveVoucher(false);
                            //btnStopRedeem.Visibility = System.Windows.Visibility.Visible;
                            //btnStopRedeem.IsEnabled = true;
                            txtFinalStatus.Visibility = System.Windows.Visibility.Visible;
                            txtErrorStatus.Visibility = System.Windows.Visibility.Visible;
                            txtErrorStatus.Clear();
                            btnClearRedeem.Visibility = System.Windows.Visibility.Hidden;
                            btnRedeemTickets.Visibility = System.Windows.Visibility.Hidden;
                            //to open the redeem status window when the redemption starts
                            showModel(RedeemTickets);
                            LogManager.WriteLog("Redemptiom completed", LogManager.enumLogLevel.Info);
                        }

                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                    }
                    else
                    {

                        txtErrorStatus.Text = Application.Current.Resources["CRedeemMultipleTicketCancellation"].ToString();
                        btnClearRedeem.Visibility = System.Windows.Visibility.Visible;
                        btnRedeemTickets.Visibility = System.Windows.Visibility.Visible;
                        txtAmount.Visibility = System.Windows.Visibility.Visible;
                        txtCount.Visibility = System.Windows.Visibility.Visible;
                        dispTimer.Stop();
                    }

                }
                else
                {
                   txtErrorStatus.Text = Application.Current.Resources["CRedeemMultipleVoucherNoTicketFound"].ToString();
                   txtBarcodeVal.Focus();
                }
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }

        }

        private void txtBarcodeVal_TextChanged(object sender, TextChangedEventArgs e)
        {
           

            try
            {
            txtErrorStatus.Clear();
            // Code is commented to disable adding the voucher to the grid automatically when it reaches 18 digits
            #region Voucher18digits
            //    if (txtBarcodeVal.Text.Trim().Length == this.iVoucherLength)
            //    {
            //        AddingToGrid(txtBarcodeVal.Text.Trim());

            //    }
            #endregion Voucher18digits

            btnClearRedeem.IsEnabled = true;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                LogManager.WriteLog("Inside Image_MouseDown,removing the item from the list", LogManager.enumLogLevel.Info);
                txtErrorStatus.Clear();
                var item = (sender as FrameworkElement).DataContext;
                _objTickets.Remove((ReedemTicketInfo)item);
                decimal dTotalAmount = 0;
                int iTicketcount = 0;
                _objTickets.ForEach((x) =>
                {
                    if (x.TicketStatus == 0)
                    {
                        dTotalAmount += x.Amount; iTicketcount++;
                    }
                    else if (x.TicketStatus == -14 && Settings.RedeemConfirm && Settings.RedeemExpiredTicket && Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket"))
                    {
                        dTotalAmount += x.Amount; iTicketcount++;
                    }
                    else if (x.TicketStatus == -15 && Settings.RedeemConfirm && Settings.RedeemExpiredTicket && Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket"))
                    {
                        dTotalAmount += x.Amount; iTicketcount++;
                    }
                    else if (x.TicketStatus == 1)
                    {
                        dTotalAmount += x.Amount; iTicketcount++;
                    }
                });
                txtAmountVal.Text = dTotalAmount.ToString();
                txtCountVal.Text = iTicketcount.ToString();
                LogManager.WriteLog("The Item Removed from the list is:" + item, LogManager.enumLogLevel.Info);
                lvTickets.ItemsSource = null;
                lvTickets.ItemsSource = _objTickets;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Event fires when text is entered in to the barcode value textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcodeVal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            try
            {
                CheckIsNumericValue(e);
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        #endregion Events

        #region Methods

        private void LoadControls()
        {
            try
            {
                LogManager.WriteLog("Inside LoadControls", LogManager.enumLogLevel.Info);
                txtBarcodeVal.Focus();
                iVoucherLength = Int32.Parse(ConfigurationManager.AppSettings["VoucherAutoAddOnLength"].ToString());
                txtBarcodeVal.MaxLength = iVoucherLength;
                btnStopRedeem.Visibility = System.Windows.Visibility.Hidden;
                btnRedeemTickets.IsEnabled = true;
                txtAmount.Visibility = System.Windows.Visibility.Visible;
                txtCount.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }

        }

        /// <summary>
        /// To open the voucher redeem status window
        /// </summary>
        /// <param name="Method"></param>
        void showModel(Action Method)
        {
            MultipleVoucherRedeemStatus dig = null;
            try
            {
                LogManager.WriteLog("Inside showModel()", LogManager.enumLogLevel.Info);
                dig = new MultipleVoucherRedeemStatus(this,Method);
                dig.Owner = Window.GetWindow(this);
                dig.ShowDialog();
            }
            catch
            {
                if (dig != null)
                    dig.Close();
                throw;
            }
        }

        private void RedeemTickets()
        {
            try
            {
                LogManager.WriteLog("Inside RedeemTickets() Method", LogManager.enumLogLevel.Info);
                List<ReedemTicketInfo> _lstToProcess = new List<ReedemTicketInfo>();
                _lstToProcess.AddRange(_objTickets.Where(Tck => (Tck.TicketStatus == 0) && (Tck.IsVoucherRedeemed == false) || (Tck.TicketStatus == 1) && (Tck.IsVoucherRedeemed == false) || (Tck.TicketStatus == -14 && Settings.RedeemExpiredTicket && Tck.IsVoucherRedeemed == false && Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket")) || (Tck.TicketStatus == -15 && Settings.RedeemExpiredTicket && Tck.IsVoucherRedeemed == false && Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket"))).ToList());
                LogManager.WriteLog("The number of unprocessed tickets:" + _lstToProcess.Count, LogManager.enumLogLevel.Info);

                if (_lstToProcess.Count == 0)
                {
                    Dispatcher.Invoke
                           (new Action(() =>
                           {
                               txtErrorStatus.Text = Application.Current.Resources["CRedeemMultipleTicketsNoValidTickets"].ToString();
                               btnStopRedeem.Visibility = System.Windows.Visibility.Hidden;
                               btnClearRedeem.Visibility = System.Windows.Visibility.Visible;
                               btnRedeemTickets.Visibility = System.Windows.Visibility.Visible;                              
                               DisableRemoveVoucher(true);

                           }));
                    return;
                }
                foreach (ReedemTicketInfo ticket in _lstToProcess)
                {

                    if (_AutoReset.WaitOne(300))
                    {

                        Dispatcher.Invoke
                            (new Action(() =>
                            {

                                txtErrorStatus.Text = Application.Current.Resources["CRedeemMultipleTicketStopRedeeming"].ToString();
                                //btnExit.IsEnabled = true;
                                btnRedeemTickets.Visibility = System.Windows.Visibility.Visible;
                                btnClearRedeem.Visibility = System.Windows.Visibility.Visible;
                                DisableRemoveVoucher(true);
                            }));

                        return;
                    }
                    Dispatcher.BeginInvoke((Action)(() =>
                                     {

                                         lvTickets.ScrollIntoView(ticket);
                                         lvTickets.SelectedItem = ticket;
                                         txtErrorStatus.Text = string.Format(Application.Current.Resources["RedeemMultipleTicketProcessingMessage"].ToString() + "({0}/{1})...{2}", (_lstToProcess.IndexOf(ticket) + 1).ToString(), _lstToProcess.Count.ToString(), ticket.Barcode);

                                     }), null);

                    if (!ticket.IsVoucherRedeemed)
                    {
                        string RedeemStatus = string.Empty;

                        //Db hit to redeem the ticket
                        RedeemStatus = RedeemTicket(ticket);                   

                            Dispatcher.Invoke(new Action(() =>
                            {                              
                                ticket.Status = RedeemStatus;
                            }));                       

                    }

                }
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    txtErrorStatus.Foreground = new SolidColorBrush(Colors.Green);
                    txtErrorStatus.Text = Application.Current.Resources["CRedeemMultipleTicketRedemptionCompletion"].ToString();                                         
                    btnClearRedeem.Visibility = System.Windows.Visibility.Visible;
                    LogManager.WriteLog("All the tickets are redeemed successfully", LogManager.enumLogLevel.Info);
                    btnRedeemTickets.Visibility = System.Windows.Visibility.Visible;
                    DisableRemoveVoucher(true);
                    btnStopRedeem.Visibility = System.Windows.Visibility.Hidden;                   
                    dispTimer.Start();
                }
                      ), null);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
          
        }

        private string RedeemTicket(ReedemTicketInfo ticket)
        {
            LogManager.WriteLog("Inside RedeemTicket() Method", LogManager.enumLogLevel.Info);
            bool _isValidTicket = false;
            double RedeemTicketAmt = 0;
            int iCustomerID = 0;
            IRedeemOnlineTicket _objRedeemOnlineTicket = RedeemOnlineTicketBusinessObject.CreateInstance();
            RTOnlineTicketDetail _objOnlineTicketDetail = new RTOnlineTicketDetail();

            try
            {
                _objOnlineTicketDetail.RedeemedMachine = System.Environment.MachineName;
                _objOnlineTicketDetail.ClientSiteCode = Settings.SiteCode;               

                LogManager.WriteLog("The Voucher Redeem is Validating", LogManager.enumLogLevel.Info);
                _objOnlineTicketDetail.TicketString = ticket.Barcode;
                _objOnlineTicketDetail.HostSiteCode = _objOnlineTicketDetail.TicketString.Substring(0, 4);

                LogManager.WriteLog("Redeeming the Voucher :" + _objOnlineTicketDetail.TicketString, LogManager.enumLogLevel.Info);
                Dispatcher.BeginInvoke((Action)(() =>
                     {
                         ticket.Status = Application.Current.Resources["CRedeemMultipleTicketRedemptionValidating"].ToString();

                     }), null);

                validateVoucher(ticket);

                _objOnlineTicketDetail = ticket.oRTOnlineTicketDetail;

                ticket.TicketStatus = _objOnlineTicketDetail.TicketStatusCode;
                ticket.oRTOnlineTicketDetail = _objOnlineTicketDetail;


                if (_objOnlineTicketDetail.TicketStatusCode != 0 && _objOnlineTicketDetail.TicketStatusCode != -14 && _objOnlineTicketDetail.TicketStatusCode != -15 && _objOnlineTicketDetail.TicketStatusCode != 1) //Exception Occured
                {
                    return Application.Current.Resources["MessageID425"].ToString();
                }


                int iTicketAmount = Convert.ToInt32(_objOnlineTicketDetail.RedeemedAmount);
                RedeemTicketAmt = iTicketAmount;
                _objOnlineTicketDetail.CustomerId = iCustomerID;
                _objOnlineTicketDetail.AuthorizedUser_No = Security.SecurityHelper.CurrentUser.User_No;
                _objOnlineTicketDetail.RedeemedUser = Security.SecurityHelper.CurrentUser.UserName;

               
                #region ValidVoucher
                //For active voucher
                if ((_objOnlineTicketDetail.TicketStatusCode == 0) && (Settings.RedeemConfirm))
                {
                      LogManager.WriteLog("Processing valid Voucher with status (0) with Barcode" + _objOnlineTicketDetail.TicketString, LogManager.enumLogLevel.Info);
                        Dispatcher.BeginInvoke((Action)(() =>
                        {

                            ticket.Status = Application.Current.Resources["RedeemMultipleTicketProcessingMessage"].ToString();


                        }), null);

                        _objOnlineTicketDetail.TicketString = ticket.Barcode; // to pass the exact barcode entered in the UI to redeem since validation length check is done during redemption also]
                        _objRedeemOnlineTicket.RedeemOnlineTicket(_objOnlineTicketDetail);
                        _isValidTicket = _objOnlineTicketDetail.ValidTicket;
                        //Thread.Sleep(300);
                        LogManager.WriteLog("The Voucher Redeem is Processed", LogManager.enumLogLevel.Info);


                        ticket.Status = Application.Current.Resources[_objOnlineTicketDetail.TicketStatus].ToString();
                        string AuditTicketStatus = ticket.Status;

                        //DB will return as VALID VOUCHER(MessageID210) if the voucher is redeemed successfully, So if the 
                        //ticket is redeemed successfully,by checking the ticket status, the redemption is displayed in the grid
                        if (ticket.Status == Application.Current.Resources["MessageID210"].ToString())
                        {
                            ticket.Status = Application.Current.Resources["CRedeemMultipleTicketRedemptionSuccess"].ToString();

                            AuditTicketStatus = AuditTicketStatus + "(" + CommonUtilities.GetCurrency(Convert.ToDouble(_objOnlineTicketDetail.TicketValue)) + ")";
                            //Audit the redeemed voucher
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.MultipleVoucherRedeem,
                                Audit_Screen_Name = "Multiple Redeem Voucher",
                                Audit_Desc = "Voucher Number -" + _objOnlineTicketDetail.TicketString,
                                AuditOperationType = OperationType.ADD,
                                Audit_Field = "Voucher Status",
                                Audit_New_Vl = AuditTicketStatus,
                                Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                                Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                            });

                        }
                        else
                        {
                            ticket.Status = Application.Current.Resources["CRedeemMultipleVoucherRedemptionFailed"].ToString();
                            ticket.TicketStatus = -1;

                            //Audit the redemption failed voucher
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.MultipleVoucherRedeem,
                                Audit_Screen_Name = "Multiple Redeem Voucher",
                                Audit_Desc = "Voucher redemption failed for :-" + _objOnlineTicketDetail.TicketString,
                                AuditOperationType = OperationType.MODIFY,
                                Audit_Field = "Voucher status",
                                Audit_New_Vl = AuditTicketStatus,
                                Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                                Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                            });


                        }
                        ticket.IsVoucherRedeemed = true;
                        return ticket.Status;
                    
                   
                }

                #endregion ValidVoucher

                #region TISVALIDVOUCHER

                //For active voucher
                else if ((_objOnlineTicketDetail.TicketStatusCode == 1) && (Settings.RedeemConfirm))
                {
                    LogManager.WriteLog("Processing valid TIS Voucher with status (1) with Barcode" + _objOnlineTicketDetail.TicketString, LogManager.enumLogLevel.Info);
                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                      ticket.Status = Application.Current.Resources["RedeemMultipleTicketProcessingMessage"].ToString();
                    }), null);

                    _objRedeemOnlineTicket.RedeemOnlineTicket(_objOnlineTicketDetail);
                    _isValidTicket = _objOnlineTicketDetail.ValidTicket;
                    // Thread.Sleep(300);
                    LogManager.WriteLog("The Voucher Redeem is Processed", LogManager.enumLogLevel.Info);
                    
                    ticket.Status = Application.Current.Resources[_objOnlineTicketDetail.TicketStatus].ToString();
                    string TISAuditStatus = ticket.Status;
                    //DB will return as VALID VOUCHER(MessageID210) if the voucher is redeemed successfully, So if the 
                    //ticket is redeemed successfully,by checking the ticket status, the redemption is displayed in the grid
                    if (ticket.Status == Application.Current.Resources["MessageID210"].ToString())
                    {
                        ticket.Status = Application.Current.Resources["CRedeemTISVoucherRedeemSuccess"].ToString();
                        //Audit the redeemed voucher
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = ModuleName.MultipleVoucherRedeem,
                            Audit_Screen_Name = "Multiple Redeem Voucher",
                            Audit_Desc = "TIS Voucher Number -" + _objOnlineTicketDetail.TicketString,
                            AuditOperationType = OperationType.ADD,
                            Audit_Field = "Voucher Status",
                            Audit_New_Vl = TISAuditStatus+"["+ ticket.Amount + "]",
                            Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                            Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                        });
                    }
                    else if (ticket.Status == Application.Current.Resources["MessageID876"].ToString()) // for TIS Effective date mismatch
                    {
                        ticket.Status = ticket.Status;
                        ticket.TicketStatus = -1;
                        //Audit the redemption failed voucher
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = ModuleName.MultipleVoucherRedeem,
                            Audit_Screen_Name = "Multiple Redeem Voucher",
                            Audit_Desc = "TIS Voucher redemption failed for :-" + _objOnlineTicketDetail.TicketString,
                            AuditOperationType = OperationType.MODIFY,
                            Audit_Field = "Voucher status",
                            Audit_New_Vl = TISAuditStatus,
                            Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                            Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                        });
                    }
                    else
                    {
                        ticket.Status = Application.Current.Resources["CRedeemTISVoucherRedeemFailed"].ToString();
                        ticket.TicketStatus = -1;
                        //Audit the redemption failed voucher
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = ModuleName.MultipleVoucherRedeem,
                            Audit_Screen_Name = "Multiple Redeem Voucher",
                            Audit_Desc = "TIS Voucher redemption failed for :-" + _objOnlineTicketDetail.TicketString,
                            AuditOperationType = OperationType.MODIFY,
                            Audit_Field = "Voucher status",
                            Audit_New_Vl = TISAuditStatus,
                            Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                            Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                        });

                    }
                    ticket.IsVoucherRedeemed = true;
                    return ticket.Status;
                }


                #endregion TISVALIDVOUCHER

                #region forExpiredVoucher
                //Expired Voucher

               
                else if (_objOnlineTicketDetail.TicketStatusCode == -14 && (Settings.RedeemConfirm) && Settings.RedeemExpiredTicket)
                {

                    
                        LogManager.WriteLog("Processing expired Voucher with status -14 with Barcode" + _objOnlineTicketDetail.TicketString, LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("The expired Voucher Redeem is Processing", LogManager.enumLogLevel.Info);
                        Dispatcher.BeginInvoke((Action)(() =>
                        {
                            ticket.Status = Application.Current.Resources["RedeemMultipleTicketProcessingMessage"].ToString();
                        }), null);
                        _objOnlineTicketDetail = _objRedeemOnlineTicket.RedeemOnlineTicket(_objOnlineTicketDetail);
                        _isValidTicket = _objOnlineTicketDetail.ValidTicket;
                        Thread.Sleep(300);

                        LogManager.WriteLog("The Voucher:" + ticket.Barcode + "is Redeemed successfully", LogManager.enumLogLevel.Info);

                        ticket.Status = Application.Current.Resources[_objOnlineTicketDetail.TicketStatus].ToString();
                        string AuditExpiredStatus = ticket.Status;
                        //DB will return as VALID VOUCHER(MessageID210) if the voucher is redeemed successfully, So if the 
                        //ticket is redeemed successfully,by checking the ticket status, the redemption is displayed in the grid

                        if (ticket.Status == Application.Current.Resources["MessageID210"].ToString())
                        {
                            ticket.Status = Application.Current.Resources["CRedeemMultipleTicketRedemptionSuccess"].ToString();

                            
                            //Audit the redeemed voucher
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.MultipleVoucherRedeem,
                                Audit_Screen_Name = "Multiple Redeem Voucher",
                                Audit_Desc = "Expired Voucher Number -" + _objOnlineTicketDetail.TicketString,
                                AuditOperationType = OperationType.ADD,
                                Audit_Field = "Voucher Status",
                                Audit_New_Vl = AuditExpiredStatus + "[" + ticket.Amount + "]",
                                Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                                Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                            });
                        }
                        else
                        {
                            ticket.Status = Application.Current.Resources["CRedeemMultipleVoucherRedemptionFailed"].ToString();
                            ticket.TicketStatus = -1;

                            //Audit the redemption failed voucher
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.MultipleVoucherRedeem,
                                Audit_Screen_Name = "Multiple Redeem Voucher",
                                Audit_Desc = "Expired Voucher redemption failed for :-" + _objOnlineTicketDetail.TicketString,
                                AuditOperationType = OperationType.MODIFY,
                                Audit_Field = "Voucher status",
                                Audit_New_Vl = AuditExpiredStatus,
                                Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                                Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                            });
                        }

                        ticket.IsVoucherRedeemed = true;

                    }

                   

              
                
                #endregion forExpiredVoucher

                #region forTISExpiredVoucher
                //Expired Voucher
                else if (_objOnlineTicketDetail.TicketStatusCode == -15 && (Settings.RedeemConfirm) && Settings.RedeemExpiredTicket)
                {
                   
                    LogManager.WriteLog("Processing TIS expired Voucher with status -15 with Barcode" + _objOnlineTicketDetail.TicketString, LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("The expired TIS Voucher Redeem is Processing", LogManager.enumLogLevel.Info);
                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        ticket.Status = Application.Current.Resources["RedeemMultipleTicketProcessingMessage"].ToString();
                    }), null);
                    _objOnlineTicketDetail = _objRedeemOnlineTicket.RedeemOnlineTicket(_objOnlineTicketDetail);
                    _isValidTicket = _objOnlineTicketDetail.ValidTicket;
                    Thread.Sleep(300);                    
                    LogManager.WriteLog("The Voucher:" + ticket.Barcode + "is Redeemed successfully", LogManager.enumLogLevel.Info);

                    ticket.Status = Application.Current.Resources[_objOnlineTicketDetail.TicketStatus].ToString();
                    string TISExpiredAuditStatus = ticket.Status;
                    //DB will return as VALID VOUCHER(MessageID210) if the voucher is redeemed successfully, So if the 
                    //ticket is redeemed successfully,by checking the ticket status, the redemption is displayed in the grid

                    if (ticket.Status == Application.Current.Resources["MessageID210"].ToString())
                    {
                        ticket.Status = Application.Current.Resources["CRedeemTISVoucherRedeemSuccess"].ToString();

                        //Audit the redeemed voucher
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.MultipleVoucherRedeem,
                            Audit_Screen_Name = "Multiple Redeem Voucher",
                            Audit_Desc = "TIS Voucher Number -" + _objOnlineTicketDetail.TicketString,
                            AuditOperationType = OperationType.ADD,
                            Audit_Field = "Voucher Status",
                            Audit_New_Vl = TISExpiredAuditStatus+ "["+ ticket.Amount + "]",
                            Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                            Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                        });
                    }
                    else if (ticket.Status == Application.Current.Resources["MessageID876"].ToString()) // for TIS Effective date mismatch
                    {
                        ticket.Status = ticket.Status;
                        ticket.TicketStatus = -1;

                        //Audit the redemption failed voucher
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.MultipleVoucherRedeem,
                            Audit_Screen_Name = "Multiple Redeem Voucher",
                            Audit_Desc = "TIS Voucher redemption failed for :-" + _objOnlineTicketDetail.TicketString,
                            AuditOperationType = OperationType.MODIFY,
                            Audit_Field = "Voucher status",
                            Audit_New_Vl = TISExpiredAuditStatus,
                            Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                            Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                        });
                    }

                    else
                    {
                        ticket.Status = Application.Current.Resources["CRedeemTISVoucherRedeemFailed"].ToString();
                        ticket.TicketStatus = -1;

                        //Audit the redemption failed voucher
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.MultipleVoucherRedeem,
                            Audit_Screen_Name = "Multiple Redeem Voucher",
                            Audit_Desc = "TIS Voucher redemption failed for :-" + _objOnlineTicketDetail.TicketString,
                            AuditOperationType = OperationType.MODIFY,
                            Audit_Field = "Voucher status",
                            Audit_New_Vl = TISExpiredAuditStatus,
                            Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                            Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                        });
                    }
                    ticket.IsVoucherRedeemed = true;
                }
                    
                
                #endregion forTISExpiredVoucher                

                else if (_objOnlineTicketDetail.TicketStatusCode == -14 && (Settings.RedeemConfirm) && !Settings.RedeemExpiredTicket)
                {
                    LogManager.WriteLog("Processing expired Voucher with status -14 with Barcode with EXPIRED TICKET setting as FALSE" + _objOnlineTicketDetail.TicketString, LogManager.enumLogLevel.Info);
                    ticket.IsVoucherRedeemed = false;                  
                    ticket.Status = Application.Current.Resources["CRedeemMultipleTicketRedeemExpired"].ToString();

                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.Voucher,
                        Audit_Screen_Name = "Vouchers|RedeemVoucher",
                        Audit_Desc = "Invalid Voucher Redemption Attempt",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Number",
                        Audit_New_Vl = _objOnlineTicketDetail.TicketString
                    });			 
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -15 && (Settings.RedeemConfirm) && !Settings.RedeemExpiredTicket)
                {
                    LogManager.WriteLog("Processing expired Voucher with status -15 with Barcode with EXPIRED TICKET setting as FALSE" + _objOnlineTicketDetail.TicketString, LogManager.enumLogLevel.Info);
                    ticket.IsVoucherRedeemed = false;
                    ticket.Status = Application.Current.Resources["CRedeemMultipleVoucherTISExpiredVoucher"].ToString();

                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.Voucher,
                        Audit_Screen_Name = "Vouchers|RedeemVoucher",
                        Audit_Desc = "Invalid Voucher Redemption Attempt",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Number",
                        Audit_New_Vl = _objOnlineTicketDetail.TicketString
                    });			

                }    
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ticket.Status = Application.Current.Resources["CRedeemMultipleVoucherRedemptionFailed"].ToString();
				ticket.TicketStatus = -1;
            }
            return ticket.Status;
        }

        private bool Validate(string Barcode)
        {
            LogManager.WriteLog("inside Validate() Method,Validating the Barcode", LogManager.enumLogLevel.Info);
            bool bChecked = false;
            int ifirstDigitOfBarcode = Convert.ToInt32(Barcode.Substring(0, 1));
            try
            {

                if (Barcode.Trim().Length != 18)
                {
                    if (Barcode.Trim().Length == 8)
                    {
                        bChecked = true;
                    }
                    else
                    {
                        bChecked = false;
                    }
                }
                else if (Barcode.Trim().Length == 18)
                {
                    if (ifirstDigitOfBarcode == iTISPrefix)
                    {
                        bChecked = false;
                        LogManager.WriteLog("The TIS prefix and barcode first digit matches for :->"+Barcode+" "+"Voucher cannot be redeemed", LogManager.enumLogLevel.Info);
                      //  MessageBox.ShowBox("DisplayTISVoucher", BMC_Icon.Information, BMC_Button.OK);
                      //  txtBarcodeVal.Clear();

                        //insert into audit
                        //
                        bChecked = true;
                    }
                    else
                    {
                        bChecked = true;
                    }
                }
               
                else if (string.IsNullOrEmpty(Barcode) || string.IsNullOrWhiteSpace(Barcode))
                {
                    bChecked = false;
                }
                else if (Barcode.All(char.IsDigit))
                {
                    bChecked = true;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
            return bChecked;

        }

        private string DisplayNumberPad(string sBarcode)
        {
            try
            {

                LogManager.WriteLog("inside DisplayNumberPad", LogManager.enumLogLevel.Info);
                BMC.Presentation.NumberPadWind objKeyboard = new NumberPadWind();
                objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyBoard_Closing);
                objKeyboard.ValueText = sBarcode;
                objKeyboard.setMaxLength(iVoucherLength);
                objKeyboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                objKeyboard.Owner = Window.GetWindow(this);
                objKeyboard.ShowInTaskbar = false;
                objKeyboard.ShowDialog();

            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return strBarcode;
            }

            return strBarcode;
        }

        private void objKeyBoard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside objKeyBoard_Closing() event", LogManager.enumLogLevel.Info);
                if (((NumberPadWind)sender).DialogResult == true)
                {
                    strBarcode = ((NumberPadWind)sender).ValueText;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void validateVoucher(ReedemTicketInfo ticket)
        {
            LogManager.WriteLog("Inside validateVoucher() Method", LogManager.enumLogLevel.Info);
            int iCustomerID = 0;
            IRedeemOnlineTicket _objRedeemOnlineTicket = RedeemOnlineTicketBusinessObject.CreateInstance();            
            LinqDataAccessDataContext linqDBExchange = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
            RedeemTicket _redeemTicket = new RedeemTicket();            
            string strTableTicketNumber = string.Empty;
            int iInstallationID = 0;
            int iTicketValidationLength = 0;
            
            try
            {
                _objOnlineTicketDetail.RedeemedMachine = System.Environment.MachineName;
                _objOnlineTicketDetail.ClientSiteCode = Settings.SiteCode;


                LogManager.WriteLog("The Voucher Redeem is Validating", LogManager.enumLogLevel.Info);
                if (ticket.Barcode.Length != 18)
                {
                    LogManager.WriteLog("Checking for exact barcode if it is less than 18, The barcode is: " + ticket.Barcode, LogManager.enumLogLevel.Info);
                    
                    InstallationFromTicket _TicketInstallationDetails = linqDBExchange.GetInstallationNumber(ticket.Barcode).SingleOrDefault();
                    
                    if (_TicketInstallationDetails != null)
                    {   
                        strTableTicketNumber = _TicketInstallationDetails.strbarcode;                        
                        iInstallationID = Convert.ToInt32(_TicketInstallationDetails.installation_no);

                        LogManager.WriteLog("Barcode obtained from Table is :" + strTableTicketNumber, LogManager.enumLogLevel.Info);                       

                        //To get the validation length
                        if (iInstallationID > 0)
                        {
                            DataTable InstallationDetails = (new CommonDataAccess()).GetInstallationDetails(0, iInstallationID, false, false);
                            if (InstallationDetails.Rows.Count > 0)
                            {
                                int.TryParse(InstallationDetails.Rows[0]["Validation_length"].ToString(), out iTicketValidationLength);

                                //linqDBExchange.GetValidationLength(iInstallationID, ref iTicketValidationLength);
                                LogManager.WriteLog("The validation length for the entered barcode," + ticket.Barcode + "is:" + iTicketValidationLength, LogManager.enumLogLevel.Info);
                                if (iTicketValidationLength != 0 && ticket.Barcode.Length == iTicketValidationLength)// when validation length does not match with the barcode
                                {
                                    _objOnlineTicketDetail.TicketString = strTableTicketNumber;                                    
                                }
                                else
                                {
                                    LogManager.WriteLog("the validation length mismatch for barcode :->" + ticket.Barcode, LogManager.enumLogLevel.Info);
                                    FindVoucher(ticket);
                                    
                                }
                            }
                            else
                            {
                                LogManager.WriteLog("No installation details found for this Ticket :->" + ticket.Barcode, LogManager.enumLogLevel.Info);
                                FindVoucher(ticket);
                                
                            }
                        }
                        else
                        {

                            LogManager.WriteLog("No installation details found for this Ticket :->" + ticket.Barcode, LogManager.enumLogLevel.Info);
                            FindVoucher(ticket);
                           // _objOnlineTicketDetail.TicketString = ticket.Barcode;
                        }                    
                    }
                    else
                    {
                       
                        LogManager.WriteLog("No installation details found for this Ticket :->" + ticket.Barcode, LogManager.enumLogLevel.Info);
                        FindVoucher(ticket);
                        _objOnlineTicketDetail.TicketString = ticket.Barcode;
                    }
                }
                else               
                  _objOnlineTicketDetail.TicketString = ticket.Barcode;     
                
         
                _objOnlineTicketDetail.HostSiteCode = _objOnlineTicketDetail.TicketString.Substring(0, 4);

                LogManager.WriteLog("Validating the Voucher,Barcode is:" + _objOnlineTicketDetail.TicketString, LogManager.enumLogLevel.Info);

                ticket.Status = Application.Current.Resources["CRedeemMultipleTicketRedemptionValidating"].ToString();                
                
                _objOnlineTicketDetail = _objRedeemOnlineTicket.GetVoucherAmountAndStatusForMultipleTicket(_objOnlineTicketDetail);

                /*****************STATUS FOR VALIDATING VOUCHER*********************************
                 *******************************************************************************
                 * NORMAL VOUCHER                   *                TIS VOUCHER
                 * ******************************************************************************
                -1 Voucher not found                *              -2 TIS ticket not found,pending
                -3  Already paid					*			  -4 Already paid TIS
                -5  partially paid					*			  -6 Partially paid TIS
                -7  void voucher					*			  -8 void voucher for TIS 
                -9  cancelled voucher				*			  -10 cancelled voucher for TIS
                -11 Non cashable voucher			*			  -12 Non cashable voucher for TIS
                -13 Site code mismatch				*			  -15 Expired voucher for TIS
                -14 Expired voucher					*			   1  Valid voucher TIS
                 0  valid voucher                   *             -16 Player card required for TIS voucher
                 *                                  *             -17 TIS expired but player card requried
                 *                                                -18 TIS Effective Date Mismatch  
                 ********************************************************************************************** */

                //_objOnlineTicketDetail = _objRedeemOnlineTicket.GetRedeemTicketAmount(_objOnlineTicketDetail);
                _objOnlineTicketDetail.CustomerId = iCustomerID;
                ticket.TicketStatus = _objOnlineTicketDetail.TicketStatusCode;
                if (_objOnlineTicketDetail.TicketStatusCode == -2)
                {
                   // RTOnlineTicketDetail objTicketDetail = new RTOnlineTicketDetail();
                    IRedeemOnlineTicket objCashDeskOper = RedeemOnlineTicketBusinessObject.CreateInstance();
                    _objOnlineTicketDetail = objCashDeskOper.GetMultiRedeemTicketAmount(_objOnlineTicketDetail);

                    if (_objOnlineTicketDetail != null)
                    {   
                        if (_objOnlineTicketDetail.RedeemedAmount  > 0)
                        {

                            _objOnlineTicketDetail.RedeemedAmount = (_objOnlineTicketDetail.RedeemedAmount)/100;
                            _objOnlineTicketDetail.TicketValue = Convert.ToDouble(_objOnlineTicketDetail.RedeemedAmount);
                            _objOnlineTicketDetail.TicketStatusCode = 1;
                            ticket.TicketStatus = 1; //For making it valid tis voucher 
                            
                        }
                    }
                     
                }
                
                ticket.oRTOnlineTicketDetail = _objOnlineTicketDetail;

                string strRedeemedAmount = _objOnlineTicketDetail.TicketValue.ToString("0.00");
                
                ticket.Amount = Convert.ToDecimal(strRedeemedAmount);
        
                string str_status = "";
               // bool isTISPrintedTicket = VoucherHelper.IsTISPrintedTicketPrefix(_objOnlineTicketDetail.TicketString);

                
                _objOnlineTicketDetail.AuthorizedUser_No = Security.SecurityHelper.CurrentUser.User_No;
                _objOnlineTicketDetail.RedeemedUser = Security.SecurityHelper.CurrentUser.UserName;
               
                if (_objOnlineTicketDetail.TicketStatusCode == -234) //Exception Occured
                {
                    str_status = Application.Current.Resources["CRedeemMultipleTicketRedeemErrorMessage1"].ToString(); 
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -99) //Invalid ticket 
                {
                    str_status = Application.Current.Resources["CRedeemMultipleTicketRedeemErrorMessage2"].ToString();
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == 0) //Valid Voucher
                {
                    str_status = Application.Current.Resources["CRedeemMultipleTicketRedeemValidVoucher"].ToString();
                }                
                else if (_objOnlineTicketDetail.TicketStatusCode == 1) //Valid TIS Voucher
                {
                    str_status = Application.Current.Resources["CRedeemValidTISVoucher"].ToString();
                }
               
                else if (_objOnlineTicketDetail.TicketStatusCode == -1) //Voucher not found for  normal voucher
                {
                    str_status = Application.Current.Resources["CRedeemMultipleTicketRedeemVoucherNotFound"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });

                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -2) //TIS Voucher not found in local DB
                {
                    str_status = Application.Current.Resources["CRedeemTISMultipleTicketNotFound"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "TIS Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -3) //Already Paid
                {
                    str_status = Application.Current.Resources["CRedeemMultipleTicketRedeemAlreadyPaid"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });



                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -4) //TIS Already Paid
                {
                    str_status = Application.Current.Resources["CRedeemMultipleVoucherAlreadyPaidTISVoucher"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "TIS Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });

                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -5) //Partially paid
                {
                    str_status = Application.Current.Resources["CRedeemMultipleTicketsPartiallyPaidTickets"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -6) //TIS Partially paid
                {
                    str_status = Application.Current.Resources["CRedeemMultipleVoucherPartiallyPaidTISVoucher"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "TIS Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -7) //Voided ticket
                {
                    str_status = Application.Current.Resources["CRedeemMultipleTicketsVoidticket"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -8) //TIS Voided ticket
                {
                    str_status = Application.Current.Resources["CRedeemMultipleVoucherVoidTISVoucher"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "TIS Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -9) //Cancelled VOucher
                {
                    str_status = Application.Current.Resources["CRedeemMultipleTicketsCancelledTickets"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -10) //TIS Cancelled VOucher
                {
                    str_status = Application.Current.Resources["CRedeemMultipleVoucherTISCancelledVoucher"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -11) //Non Cashable
                {
                    str_status = Application.Current.Resources["CRedeemMultipleTicketsNonCashableTickets"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -12) // TIS Non Cashable
                {
                    str_status = Application.Current.Resources["CRedeemMultipleVoucherTISNonCashableVoucher"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -13) //Site Code Mismatch
                {
                    str_status = Application.Current.Resources["CRedeemMultipleTicketsSiteCodeMismatch"].ToString();

                    //insert into audit
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.MultipleVoucherRedeem,
                        Audit_Screen_Name = "Multiple Redeem Voucher",
                        Audit_Desc = "Voucher Number -" + _objOnlineTicketDetail.TicketString,
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Voucher Status",
                        Audit_New_Vl = str_status,
                        Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                        Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                    });
                }

                else if (_objOnlineTicketDetail.TicketStatusCode == -14) //Expired
                {
                    if (Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket"))
                    {
                        str_status = Application.Current.Resources["CRedeemMultipleTicketRedeemExpired"].ToString();
                    }
                    else
                    {
                        str_status = Application.Current.Resources["CRedeemExpireUserAccess"].ToString();

                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.MultipleVoucherRedeem,
                            Audit_Screen_Name = "Multiple Redeem Voucher",
                            Audit_Desc = "Expired Voucher Number -" + _objOnlineTicketDetail.TicketString,
                            AuditOperationType = OperationType.MODIFY,
                            Audit_Field = "Voucher Status",
                            Audit_New_Vl = Application.Current.Resources["CRedeemExpireUserAccess"].ToString(),
                            Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                            Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                        });
                    }
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -15) //TIS Expired
                {
                    if (Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket"))
                    {
                        str_status = Application.Current.Resources["CRedeemMultipleVoucherTISExpiredVoucher"].ToString();
                    }
                    else
                    {
                        str_status = Application.Current.Resources["CRedeemTISExpireUserAccess"].ToString();

                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.MultipleVoucherRedeem,
                            Audit_Screen_Name = "Multiple Redeem Voucher",
                            Audit_Desc = "Expired Voucher Number -" + _objOnlineTicketDetail.TicketString,
                            AuditOperationType = OperationType.MODIFY,
                            Audit_Field = "Voucher Status",
                            Audit_New_Vl = Application.Current.Resources["CRedeemExpireUserAccess"].ToString(),
                            Audit_User_ID = _objOnlineTicketDetail.AuthorizedUser_No,
                            Audit_User_Name = _objOnlineTicketDetail.RedeemedUser
                        });
                    }

                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -16)//TIS valid player details required , cannot redeem here
                {
                    str_status = Application.Current.Resources["CRedeemTISMultipleVoucherPlayerDetailsRequired"].ToString();
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -17)//TIS expired voucher player details required , cannot redeem here-expired
                {
                    str_status = Application.Current.Resources["CRedeemTISMultipleVoucherExpiredPlayerdetailsRequired"].ToString();
                }

                else if (_objOnlineTicketDetail.TicketStatusCode == -18)//TIS Effective date mismatch
                {
                    str_status = Application.Current.Resources["CRedeemsTISEffectiveDateMismatch"].ToString();
                }
                else if (_objOnlineTicketDetail.TicketStatusCode == -990) // When TIS web service hits fails
                {
                    str_status = Application.Current.Resources["CRedeemsTISWebServiceError"].ToString();
                }

               
                #region offlineVoucherHandler-Commented
                    //if (!_objOnlineTicketDetail.ShowOfflineTicketScreen)
                    //{
                    // str_status = Application.Current.Resources["CRedeemMultipleTicketRedeemVoucherNotFound"].ToString();
                    //}
                    //else
                    //{
                    //_iCheckOfflineVoucher = _redeemTicket.CheckSDGOfflineTicket(ticket.Barcode); //checks whether it is offline voucher or not
                    //if (_iCheckOfflineVoucher == -14) //since SP returns -14 if its offline voucher
                    //{
                    //    str_status = Application.Current.Resources["CRedeemMultipleTicketsOfflineTickets"].ToString();
                    //}
                    //else
                    //{
                    //    str_status = Application.Current.Resources["CRedeemMultipleTicketRedeemVoucherNotFound"].ToString();
                    //}
                //} 
                #endregion offlineVoucherHandler-Commented


                Dispatcher.BeginInvoke((Action)(() =>
                     {
                         ticket.Status = str_status;
                         LogManager.WriteLog("the Barcode " + ticket.Barcode + "has returned" + str_status, LogManager.enumLogLevel.Info);
                     }), null);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void AddingToGrid(string Barcode)
        {
            LogManager.WriteLog("inside AddingToGrid() method", LogManager.enumLogLevel.Info);
            try
            {
                if (String.IsNullOrEmpty(Barcode))
                    return;

                if (!Validate(Barcode))
                {
                    txtErrorStatus.Visibility = System.Windows.Visibility.Visible;
                    txtErrorStatus.Text = Application.Current.Resources["CRedeemMultipleTicketRedeemInvalidTicketEntered"].ToString();
                    return;

                }
                if (!_objTickets.Exists((x) => { return x.Barcode == Barcode; }))
                {
                    ReedemTicketInfo objReedemTicketInfo = new ReedemTicketInfo() { Barcode = Barcode, Status = Application.Current.Resources["RedeemMultipleTicketProcessingMessage"].ToString(), IsRemoveVoucherEnable = true };
                    this.validateVoucher(objReedemTicketInfo);

                    //To calculate the total valid voucher amount count in the grid.
                    decimal dTotalAmount = 0;
                    int iTicketcount = 0;
                    _objTickets.Insert(0, objReedemTicketInfo);
                    _objTickets.ForEach((x) =>
                    {
                        if (x.TicketStatus == 0 ) //for valid voucher
                        {
                            dTotalAmount += x.Amount; iTicketcount++;
                        }

                        else if (x.TicketStatus == -14 && Settings.RedeemConfirm && Settings.RedeemExpiredTicket && Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket")) //Expired normal voucher
                        {
                            dTotalAmount += x.Amount; iTicketcount++;
                        }
                        else if (x.TicketStatus == -15 && Settings.RedeemConfirm && Settings.RedeemExpiredTicket && Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket")) //TIS expired voucher
                        {
                            dTotalAmount += x.Amount; iTicketcount++;
                        }

                        else if (x.TicketStatus == 1) //for valid TIS voucher
                        {
                            dTotalAmount += x.Amount; iTicketcount++;
                        }
                      
                    });
                    txtAmountVal.Text = dTotalAmount.ToString("0.00");
                    txtCountVal.Text = iTicketcount.ToString();
                }
                else
                {
                    txtErrorStatus.Visibility = System.Windows.Visibility.Visible;
                    txtBarcodeVal.Clear();
                    txtBarcodeVal.Focus();
                    txtErrorStatus.Text = Application.Current.Resources["CRedeemMultipleTicketRedeemAlreadyInTheList"].ToString();                    
                    return;
                }
                lvTickets.ItemsSource = null;
                lvTickets.ItemsSource = _objTickets;
                txtBarcodeVal.Clear();
                txtBarcodeVal.Focus();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ClearAll()
        {

            txtBarcodeVal.Text = string.Empty;
            _objTickets.Clear();
            lvTickets.ItemsSource = null;
            txtAmountVal.Text = string.Empty;
            txtCountVal.Text = string.Empty;
            txtErrorStatus.Text = string.Empty;
            txtBarcodeVal.Focus();
            btnRedeemTickets.IsEnabled = true;
            txtErrorStatus.Foreground = new SolidColorBrush(Colors.Red);


        }

        private void DisableRemoveVoucher(bool Enable)
        {
            foreach (ReedemTicketInfo ticket in _objTickets)
            {
                ticket.IsRemoveVoucherEnable = Enable;
            }
        }

        /// <summary>
        /// To make the text accept only numerics
        /// </summary>
        /// <param name="e"></param>
        private void CheckIsNumericValue(TextCompositionEventArgs e)
        { 
         if(!Char.IsDigit(e.Text,e.Text.Length -1))
         {
          e.Handled = true;
         }

        }

        private void FindVoucher(ReedemTicketInfo ticket)
        {
            try
            {
                LogManager.WriteLog("Inside FindVoucher(),to return voucher not found", LogManager.enumLogLevel.Info);
                ticket.Status = Application.Current.Resources["CRedeemMultipleTicketRedeemVoucherNotFound"].ToString();               
                ticket.TicketStatus = -1;
                _objOnlineTicketDetail.TicketStatusCode = -1;
                _objOnlineTicketDetail.TicketString = ticket.Barcode;
                return;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        

      
        #endregion Methods

       


    }




}


