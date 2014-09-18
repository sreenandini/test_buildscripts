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
using BMC.Transport;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.ExceptionManagement;
using System.Globalization;
using BMC.Common.Utilities;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.LogManagement;
using System.Data;
using BMCIPC;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using Microsoft.Win32;
using BMC.Common.ConfigurationManagement;
using BMC.Presentation.POS.Helper_classes;
using BMC.Presentation.POS.UserControls;
using BMC.Security;
using BMC.Business.CashDeskOperator;


namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CManualAttendantPays.xaml
    /// 
    /// </summary>
    public partial class CManualAttendantPays : UserControl, IDisposable
    {
        #region "Declarations"
        TextBox txtBox;

        public delegate void ScannerClick(object sender, RoutedEventArgs e);
        public int InstallationNumber;
        public string BarPosition = string.Empty;
        int Treasury_No;
        Treasury treasury = null;
        private BMC.Presentation.POS.Views.CustomerDetails oCustomerDetails;
        private long Custid = 0;
        private bool ProcessCancelled = false;
        string Asset;
        const string TYPE = "Manual ";
        private bool IsHandpayVoid = false;
        private bool isScannerFired = false;        
        private string sPos = "";
        private string sTEid = "";
        List<BarPositions> Positions;
        IHandpay handpay;
        //RegistryKey installationPathkey;
        #endregion

        private CashDispenserWorker _worker = null;
        private CashDispenserStatus _dispenserStatus = null;
        bool IsProcessed = false;
        public CManualAttendantPays()
        {
            InitializeComponent();
            this.ucValueCalcComp.MaxLength = 9;
            optHandpay.IsChecked = true;

            if (!Settings.CAGE_ENABLED)
            {
                btnSave.Content = Application.Current.FindResource("CViewHandpay_xaml_btnSave");
            }
            else
            {
                btnSave.Content = Application.Current.FindResource("CViewHandpay_xaml_btnGenerateSlipNo");
            }

            handpay = HandpayBusinessObject.CreateInstance();
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            //installationPathkey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("ExchangeClientInstallationPath"));
            this.Unloaded += new RoutedEventHandler(CManualAttendantPays_Unloaded);
            if (!Settings.IsGloryCDEnabled)
            {
                this.InitializeCashDispenser();
            }

            this.ucValueCalcComp.EnterClicked += (btnSave_Click);

        }

        /// <summary>
        /// Initializes the cash dispenser.
        /// </summary>
        private void InitializeCashDispenser()
        {
            _worker = new CashDispenserWorker(this, ModuleName.ManualAttendantPay);
            _dispenserStatus = CashDispenserStatusHelper.AddCashDispenserStatus(gridCashDispenser);
        }

        /// <summary>
        /// Processes the cash dispense.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private void ProcessCashDispense(string fieldName, string fieldValue, decimal amount)
        {
            try
            {
                if (Settings.CashDispenserEnabled)
                {
                    _worker.SlotMachine = this.Asset;
                    _worker.Dispense(fieldName, fieldValue, amount, () =>
                    {
                        if (_dispenserStatus != null)
                        {
                            _dispenserStatus.LoadItemsAysnc();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void CManualAttendantPays_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        private void ValueCalcComp_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                txtBox = (TextBox)((ValueCalcComp)sender).txtDisplay;
                if(!Settings.AllowManualKeyboard)
                txtBox.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        
        

        private void ManualAttendantPay_Loaded(object sender, RoutedEventArgs e)
        {
            Positions = handpay.GetBarPositions();

            Positions.Insert(0, new BarPositions { Bar_Pos_No = 0, Bar_Pos_Name = "---Select Any---", Installation_No = 0 });

            cmbBarPositions.ItemsSource = Positions.Distinct();

            cmbBarPositions.DisplayMemberPath = "Bar_Pos_Name";
            cmbBarPositions.SelectedIndex = 0;

            //this.ucValueCalcComp.isCurrencyPad = true;
            this.ucValueCalcComp.txtDisplay.Text = "";
            this.ucValueCalcComp.s_UnformattedText = "";
            this.ucValueCalcComp.txtDisplay.Focus();
        }        
        
        #region "Private functions"
        private void SaveManualHandpay()
        {
            if (cmbBarPositions.SelectedIndex <= 0)
                return;
            Window Owner;
            double amount = 0;
            int Auth_User_ID = 0;
            try
            {
                CAuthorize objAuthorize = null;

                if (txtBox != null && txtBox.Text.Length > 0)
                {
                    // Issue fix for ->set cultureinfo='en-US',  currencyculture='it-IT'
                    double.TryParse(txtBox.Text.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out amount);
                }
                Auth_User_ID = Security.SecurityHelper.CurrentUser.User_No;
                if (Settings.Client != null && Settings.Client.ToLower() == "winchells"
                    && Settings.MaxHandPayAuthRequired
                    && txtBox != null
                    && (amount > Settings.HandpayPayoutCustomer_Max))
                {
                    objAuthorize = new CAuthorize("CashdeskOperator.Authorize.cs.MaxHandpay");
                   Auth_User_ID = Security.SecurityHelper.CurrentUser.User_No;
                    if (!Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MaxHandpay"))
                    {
                        objAuthorize.ShowDialogEx(this);
                        if (!objAuthorize.IsAuthorized)
                        {
                            IsProcessed = true;
                            return;
                        }
                        else
                        {
                            Auth_User_ID = handpay.GetUserID(objAuthorize.User.SecurityUserID);
                        }
                    }
                    else
                    {
                       objAuthorize.IsAuthorized = true;
                    }
                }

                if (Settings.RegulatoryEnabled == true && Settings.RegulatoryType == "AAMS")
                {
                    if (txtBox != null)
                    {
                        Custid = 0;
                        ProcessCancelled = false;
                        if (amount >= Settings.HandpayPayoutCustomer_Min && amount <= Settings.HandpayPayoutCustomer_Max)
                        {
                            oCustomerDetails = new BMC.Presentation.POS.Views.CustomerDetails();
                            oCustomerDetails.delCustomerUpdated += new BMC.Presentation.POS.Views.CustomerDetails.CustomerUpdateHandler(delCustomerUpdated);
                            oCustomerDetails.delCustomerCancelled += new BMC.Presentation.POS.Views.CustomerDetails.CustomerCancelHandler(delCustomerCancelled);
                            Owner = Window.GetWindow(this);
                            oCustomerDetails.ShowDialogEx(this);
                        }
                        else if (amount >= Settings.HandpayPayoutCustomer_BankAccNo)
                        {
                            oCustomerDetails = new BMC.Presentation.POS.Views.CustomerDetails(true);
                            oCustomerDetails.delCustomerUpdated += new BMC.Presentation.POS.Views.CustomerDetails.CustomerUpdateHandler(delCustomerUpdated);
                            oCustomerDetails.delCustomerCancelled += new BMC.Presentation.POS.Views.CustomerDetails.CustomerCancelHandler(delCustomerCancelled);
                            Owner = Window.GetWindow(this);
                            oCustomerDetails.ShowDialogEx(this);
                        }
                        else if (amount >= Settings.HandpayPayoutCustomer_Max && amount <= Settings.HandpayPayoutCustomer_BankAccNo)
                        {
                            oCustomerDetails = new BMC.Presentation.POS.Views.CustomerDetails();
                            oCustomerDetails.delCustomerUpdated += new BMC.Presentation.POS.Views.CustomerDetails.CustomerUpdateHandler(delCustomerUpdated);
                            oCustomerDetails.delCustomerCancelled += new BMC.Presentation.POS.Views.CustomerDetails.CustomerCancelHandler(delCustomerCancelled);
                            Owner = Window.GetWindow(this);
                            oCustomerDetails.ShowDialogEx(this);
                        }
                    }

                    if (ProcessCancelled) // if the process cancelled from the customer then  back to the handpay screen
                        return;
                }

                List<AssetNumberResult> lstasset = handpay.GetAssetNumber((cmbBarPositions.SelectedItem as BarPositions).Installation_No);

                string Asset = lstasset[0].Stock_No;

                treasury = new Treasury { InstallationNumber = (cmbBarPositions.SelectedItem as BarPositions).Installation_No };

                if (optHandpay.IsChecked == true)
                    treasury.TreasuryType = "AttendantPay Credit";
                else if (optJackpot.IsChecked == true)
                    treasury.TreasuryType = "AttendantPay Jackpot";
                else
                    treasury.TreasuryType = "PROGRESSIVE";
                treasury.TreasuryAmount = amount;
                treasury.ActualTreasuryDate = DateTime.Now;

              // treasury.UserID = Security.SecurityHelper.CurrentUser.User_No;               
                treasury.UserID = Auth_User_ID;


                treasury.Authorized_Date = DateTime.MinValue.DBMinValue();
                if (objAuthorize != null && objAuthorize.IsAuthorized)
                {
                    treasury.AuthorizedUser_No = Auth_User_ID;
                    treasury.Authorized_Date = DateTime.Now;

                    //Audit for authorization
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.ManualAttendantPay,
                        Audit_Screen_Name = "PositionDetails|ManualAttendantPay",
                        Audit_Desc = "Manual AttendantPay Type-" + treasury.TreasuryType,
                        AuditOperationType = OperationType.ADD,
                        Audit_Field = "AuthorizedUser_No",
                        Audit_New_Vl = Auth_User_ID.ToString(),
                        Audit_Slot = Asset
                    });

                }

                treasury.CustomerID = Custid; // add the customer to the treasury if amt between 1000 & 4000 or >5000

                Treasury_No = handpay.ProcessHandPay(treasury, 0);


                IsProcessed = true;
                if (Treasury_No > 0)
                {
                    DateTime dtTreasury = (DateTime)handpay.GetTreasuryDateTime(Treasury_No);

                    TextBlock_11.Text = "#" + (cmbBarPositions.SelectedItem as BarPositions).Bar_Pos_Name + dtTreasury.ToString("ddMMyyyyHHmmss");
                    txtAmount.Text = Convert.ToDecimal((treasury.TreasuryAmount)).GetUniversalCurrencyFormat();
                    #region GCD
                    if (Settings.IsGloryCDEnabled && Settings.CashDispenserEnabled)
                    {

                        LoadingWindow ld = new LoadingWindow(Window.GetWindow(this), ModuleName.ManualAttendantPay, Treasury_No.ToString(), (cmbBarPositions.SelectedItem as BarPositions).Bar_Pos_Name, Convert.ToInt32(treasury.TreasuryAmount * 100));
                        ld.Topmost = true;
                        ld.ShowDialogEx(this);
                        Result res = ld.Result;
                        if (res.IsSuccess && (Treasury_No > 0))
                        {
                            LogManager.WriteLog(string.Format("Cash Dispensed Successfully - Treasury Amount: {0:0.00}", treasury.TreasuryAmount), LogManager.enumLogLevel.Info);
                            LogManager.WriteLog("Export Manual AttendantPay Details to Enterprise", LogManager.enumLogLevel.Info);
                            handpay.ExportHandPay(Treasury_No);

                            BMC.Presentation.MessageBox.ShowBox(res.error.Message, res.error.MessageType.Equals("Error") ? BMC_Icon.Error : BMC_Icon.Information, true);

                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.AttendantPay,
                                Audit_Screen_Name = "PositionDetails|AttendantPay|Manual HandPay",
                                Audit_Desc = "Manual HandPay Succeed",
                                AuditOperationType = OperationType.ADD,
                                Audit_Old_Vl = "Ticket_ExceptionID:0 (Manual Handpay); TreasuryNo:" + Treasury_No + ";",

                            });

                        }
                        else
                        {
                            BMC.Presentation.MessageBox.ShowBox(res.error.Message, res.error.MessageType.Equals("Error") ? BMC_Icon.Error : BMC_Icon.Information, true);
                            LogManager.WriteLog(string.Format("Unable to Dispense Cash - Treasury Amount: {0:0.00}", treasury.TreasuryAmount), LogManager.enumLogLevel.Info);
                            LogManager.WriteLog("Rollback Manual HandPay Process", LogManager.enumLogLevel.Info);
                            handpay.RollbackHandPay(0, Treasury_No);
                            MessageBox.ShowBox("MessageID117", BMC_Icon.Error);
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.AttendantPay,
                                Audit_Screen_Name = "PositionDetails|AttendantPay",
                                Audit_Desc = treasury.TreasuryType + " processing was not completed.",
                                AuditOperationType = OperationType.ADD,
                                Audit_Slot = Asset
                            });
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                AuditModuleName = ModuleName.AttendantPay,
                                Audit_Screen_Name = "PositionDetails|AttendantPay|Manual HandPay Process Failed",
                                Audit_Desc = "Rollback HandPay Process Voucher due to cash dispenser error",
                                AuditOperationType = OperationType.MODIFY,
                                Audit_Old_Vl = "Ticket_ExceptionID:0 (Manual Handpay); TreasuryNo:" + Treasury_No + ";"
                            });
                        }
                    }
                    else
                    {
                        this.ProcessCashDispense("Manual AttendantPay Type", treasury.TreasuryType, Convert.ToDecimal(amount));
                        MessageBox.ShowBox("MessageID116", BMC_Icon.Information);
                    }
                    #endregion

                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.ManualAttendantPay,
                        Audit_Screen_Name = "PositionDetails|ManualAttendantPay",
                        Audit_Desc = "Manual AttendantPay Type-" + treasury.TreasuryType,
                        AuditOperationType = OperationType.ADD,
                        Audit_Field = "Amount",
                        Audit_New_Vl = String.Format("{0:0.00}", treasury.TreasuryAmount),
                        Audit_Slot = Asset
                    });

                    if (txtBox != null)
                    {
                        //txtBox.Text = "0.00";

                        txtBox.Text = this.DefaultAmount();
                        ucValueCalcComp.s_UnformattedText = "";
                    }

                    if (objAuthorize != null && objAuthorize.User != null)
                        (oCommonUtilities.CreateInstance()).PrintCommonReceipt(false, treasury.TreasuryType, Treasury_No.ToString(), objAuthorize.User);
                    else
                    (oCommonUtilities.CreateInstance()).PrintCommonReceipt(false, treasury.TreasuryType, Treasury_No.ToString());
                    if (((bool)optJackpot.IsChecked) || ((bool)optProgressive.IsChecked))// || ((bool)optHandpay.IsChecked))
                    {
                        treasury.Asset = Asset;
                        //string installationType = installationPathkey.GetValue("InstallationType").ToString();

                        //if (installationType.ToUpper().Equals("EXCHANGECLIENT"))
                        //{
                        //    if (Settings.SendPT10FromClient)
                        //        PostHandpayEvent(treasury);
                        //}
                        //else
                        PostHandpayEvent(treasury);
                    }
                }
                else
                {
                    if (optHandpay.IsChecked == true)
                        MessageBox.ShowBox("MessageID119", BMC_Icon.Error);
                    else if (optJackpot.IsChecked == true)
                        MessageBox.ShowBox("MessageID1191", BMC_Icon.Error);
                    else
                        MessageBox.ShowBox("MessageID1192", BMC_Icon.Error);


                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.ManualAttendantPay,
                        Audit_Screen_Name = "PositionDetails|ManualAttendantPay",

                        Audit_Desc = "Manual AttendantPay Type-" + treasury.TreasuryType + " processing was not completed.",
                        AuditOperationType = OperationType.ADD,
                        Audit_Slot = Asset
                    });
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                MessageBox.ShowBox("MessageID119", BMC_Icon.Error);

                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {

                    AuditModuleName = ModuleName.ManualAttendantPay,
                    Audit_Screen_Name = "PositionDetails|ManualAttendantPay",
                    Audit_Desc = "Manual AttendantPay Type-" + treasury.TreasuryType + " processing was not completed.",
                    AuditOperationType = OperationType.ADD,
                    Audit_Slot = Asset
                });
            }
        }

        internal void PostHandpayEvent(Treasury objTreasury)
        {
            try
            {

                List<AssetNumberResult> lstTreasury = handpay.GetAssetNumber((cmbBarPositions.SelectedItem as BarPositions).Installation_No);

                JackpotEventRequest request = new JackpotEventRequest();


                request.CardNumber = handpay.GetEPIDetails((cmbBarPositions.SelectedItem as BarPositions).Installation_No);
                request.JackpotAmount = objTreasury.TreasuryAmount * 100;
                request.InstallationNo = (cmbBarPositions.SelectedItem as BarPositions).Installation_No;

                if (App.client != null)
                    App.client.SendToServer(App.clientObj, request);
                else
                    LogManager.WriteLog("CManualAttendantPays PostHandpayEvent - Connection not set", LogManager.enumLogLevel.Error);


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("PostHandpayEvent" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        enum TreasuryType
        {
            Handpay = 1,
            Jackpot = 2,
            Progressive = 3
        }
        string DefaultAmount()
        {
            Double DefaultAmt = 0.00;
            return DefaultAmt.ToString("N", new CultureInfo(ExtensionMethods.CurrentCurrenyCulture));
        }

        //Delegates to identify  cancel initiated in the customer screen
        void delCustomerCancelled(object sender, RoutedEventArgs e)
        {
            ProcessCancelled = true;
        }

        //Delegates to identify save  initiated in the customer screen
        void delCustomerUpdated(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Custid = Convert.ToInt64(oCustomerDetails.txtCustID.Text.ToString());
            LogManager.WriteLog("customer id:" + Custid.ToString(), LogManager.enumLogLevel.Info);
        }

        private void cmbBarPositions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbBarPositions_GotFocus(object sender, RoutedEventArgs e)
        {
            cmbBarPositions.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnSave.IsEnabled = false;
                
                if (cmbBarPositions.SelectedIndex <= 0)
                {
                    MessageBox.ShowBox("MessageID433", BMC_Icon.Information, BMC_Button.OK);
                    cmbBarPositions.Focus();
                    return;
                }
                if (IsProcessed)
                {
                    //if (MessageBox.ShowBox("MessageID368", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    //{
                    btnSave.IsEnabled = true;
                    txtAmount.Text = string.Empty;
                    TextBlock_11.Text = string.Empty;
                    this.ucValueCalcComp.txtDisplay.Text = "";
                    this.ucValueCalcComp.s_UnformattedText = "";
                    btnSave.Content = Settings.CAGE_ENABLED ? Application.Current.FindResource("CViewHandpay_xaml_btnGenerateSlipNo") : Application.Current.FindResource("CViewHandpay_xaml_btnSave");
                    IsProcessed = false;
                    this.ucValueCalcComp.IsEnabled = true;
                    this.ucValueCalcComp.txtDisplay.Focus();
                    optHandpay.IsEnabled = true;
                    optJackpot.IsEnabled = true;
                    optProgressive.IsEnabled = true;
                    btnCancel.Visibility = Visibility.Visible;
                    cmbBarPositions.SelectedIndex = 0;
                    //}

                }
                else
                {
                    if (ValidInfo())
                    {
                        double amount = 0;
                        IsProcessed = false;
                        txtAmount.Text = txtBox.Text;
                        btnSave.Content = Application.Current.FindResource("CViewHandpay_xaml_btnReset");
                        this.ucValueCalcComp.IsEnabled = false;
                        optHandpay.IsEnabled = false;
                        optJackpot.IsEnabled = false;
                        optProgressive.IsEnabled = false;
                        btnCancel.Visibility = Visibility.Hidden;
                        if (txtBox != null && txtBox.Text.Length > 0)
                        {
                            double.TryParse(txtBox.Text.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out amount);
                        }

                        if (Settings.W2GMessage)
                        {
                            if (amount > Settings.W2GWinAmount)
                            {
                                MessageBox.ShowBox("MessageID367", BMC_Icon.Information);
                                if (!Settings.ProcessW2GAmount)
                                {
                                    MessageBox.ShowBox("MessageID531", BMC_Icon.Information);
                                    IsProcessed = true;
                                    return;
                                }
                            }
                        }
                        if (Settings.CAGE_ENABLED)
                        {
                            GenerateManulJackpot();
                            IsProcessed = true;
                            this.ucValueCalcComp.txtDisplay.Text = "";
                            this.ucValueCalcComp.s_UnformattedText = "";
                        }
                        else
                        {
                            if (MessageBox.ShowBox("MessageID123", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            {
                                SaveManualHandpay();
                                btnSave.IsEnabled = true;
                                btnSave.Focus();
                            }
                            else
                            {
                                IsProcessed = true;
                                this.ucValueCalcComp.txtDisplay.Text = "";
                                this.ucValueCalcComp.s_UnformattedText = "";
                                btnSave.IsEnabled = true;
                                btnSave.Focus();
                            }
                        }
                    }
                    else
                    {
                        this.ucValueCalcComp.txtDisplay.Focus();                        
                        this.ucValueCalcComp.txtDisplay.Select(0, this.ucValueCalcComp.txtDisplay.Text.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnSave.IsEnabled = true;
               // e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox != null)
            {
                txtBox.Text = this.DefaultAmount();
                ucValueCalcComp.s_UnformattedText = "";
            }
            this.ucValueCalcComp.txtDisplay.Text = "";
            this.ucValueCalcComp.s_UnformattedText = "";
            this.ucValueCalcComp.txtDisplay.Focus();
            this.ucValueCalcComp.txtDisplay.Select(0, this.ucValueCalcComp.txtDisplay.Text.Length);
        }

        private bool ValidInfo()
        {
           try{
            txtBox =(TextBox) ucValueCalcComp.txtDisplay;

            if (txtBox != null)
            {
                if ((txtBox.Text.Length == 0) || (Convert.ToDouble(txtBox.Text) == 0.00))
                {
                    MessageBox.ShowBox("MessageID120", BMC_Icon.Information);
                    return false;
                }
                if (txtBox.Text.Length > 9)
                {
                    MessageBox.ShowBox("MessageID121", BMC_Icon.Information);
                    if (txtBox != null)
                    {
                        txtBox.Text = this.DefaultAmount();
                        ucValueCalcComp.s_UnformattedText = "";
                    }
                    return false;
                }
            }
            else
            {
                MessageBox.ShowBox("MessageID122", BMC_Icon.Information);
                return false;
            }

            return true;
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID120", BMC_Icon.Information);
                return false;
            }
            
        }

        private void chkChecked_Checked(object sender, RoutedEventArgs e)
        {
            if (optHandpay.IsChecked == true)
            {
                cmbBarPositions.SelectedIndex = 0;
                if (txtBox != null)
                {
                    txtBox.Text = this.DefaultAmount();
                    ucValueCalcComp.s_UnformattedText = "";
                }
            }

            if (optProgressive.IsChecked == true)
            {
                cmbBarPositions.SelectedIndex = 0;
                if (txtBox != null)
                {
                    txtBox.Text = this.DefaultAmount();
                    ucValueCalcComp.s_UnformattedText = "";
                }
            }
            if (optJackpot.IsChecked == true)
            {
                cmbBarPositions.SelectedIndex = 0;
                if (txtBox != null)
                {
                    txtBox.Text = this.DefaultAmount();
                    ucValueCalcComp.s_UnformattedText = "";
                }
            }
        }        

        private void GenerateManulJackpot()
        {
            double amount = 0;
            if (txtBox != null && txtBox.Text.Length > 0)
            {
                double.TryParse(txtBox.Text.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out amount);
            }

            //if (amount > Settings.W2GWinAmount)
            //{
            //    MessageBox.ShowBox("MessageID367", BMC_Icon.Information);
            //    //return;
            //}
            try
            {
                jackpotProcessInfoDTO jpinfo = new jackpotProcessInfoDTO();
                CAuthorize objAuthorize = null;
                string HP_Type = string.Empty;
                Double iAmount = 0;
                if (txtBox != null && txtBox.Text.Length > 0)
                {
                    // Issue fix for ->set cultureinfo='en-US',  currencyculture='it-IT'
                    Double.TryParse(txtBox.Text.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out iAmount);
                }

                if (Settings.Client != null && Settings.Client.ToLower() == "winchells"
               && Settings.MaxHandPayAuthRequired
               && txtBox != null
               && (iAmount > Settings.HandpayPayoutCustomer_Max))
                {
                    objAuthorize = new CAuthorize("CashdeskOperator.Authorize.cs.MaxHandpay");
                    objAuthorize.User = Security.SecurityHelper.CurrentUser;
                    if (!Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MaxHandpay"))
                    {
                        objAuthorize.ShowDialog();
                        if (!objAuthorize.IsAuthorized)
                            return;
                    }
                    else
                    {
                        objAuthorize.IsAuthorized = true;
                    }
                }
                if (optHandpay.IsChecked == true)
                {
                    HP_Type = "AttendantPay Credit";
                    jpinfo.jackpotTypeId = 4;
                }
                else if (optJackpot.IsChecked == true)
                {
                    HP_Type = "AttendantPay Jackpot";
                    jpinfo.jackpotTypeId = 5;
                }
                else
                {
                    HP_Type = "AttendantPay Progressive";
                    jpinfo.jackpotTypeId = 6;
                }

                // Finding Denom for the machine-CR# 167677

                decimal DenomValue = 0;
                DataSet oDt = handpay.createTickeException_HandpayCAGE(int.Parse((cmbBarPositions.SelectedItem as BarPositions).Bar_Pos_Name), iAmount, 0, HP_Type);
                List<DenomValueResult> lstDenom = handpay.GetDenomValue(oDt.Tables[0].Rows[0]["Asset"].ToString());
                foreach (var denom in lstDenom)
                {
                    DenomValue = denom.Denom;
                }
                jpinfo.Slot = (cmbBarPositions.SelectedItem as BarPositions).Bar_Pos_Name;
                jpinfo.assetConfigNumber = oDt.Tables[0].Rows[0]["Asset"].ToString();
                jpinfo.Denom = DenomValue;
                jpinfo.hpjpAmount = long.Parse(oDt.Tables[0].Rows[0]["TE_VALUE"].ToString());
                jpinfo.jackpotNetAmount = long.Parse(oDt.Tables[0].Rows[0]["TE_VALUE"].ToString());
                jpinfo.sequenceNumber = long.Parse(oDt.Tables[0].Rows[0]["TE_ID"].ToString());
                jpinfo.TransactionDate = oDt.Tables[0].Rows[0]["TE_Date"].ToString();
                jpinfo.UserID = Security.SecurityHelper.CurrentUser.SecurityUserID.ToString();
                jpinfo.siteNo = Settings.SiteName;
                jpinfo.siteId = Convert.ToInt32(Settings.SiteCode);
                txtBox.Text = this.DefaultAmount();
                handpay.PrintSlip(jpinfo);


                //send PT 10 from client

                Treasury treasury = new Treasury();
                treasury.ActualTreasuryDate = Convert.ToDateTime(jpinfo.TransactionDate);
                treasury.Asset = Asset;
                treasury.Authorized_Date = Convert.ToDateTime(jpinfo.TransactionDate);
                treasury.AuthorizedUser_No = SecurityHelper.CurrentUser.User_No;                
                treasury.CustomerID = SecurityHelper.CurrentUser.User_No;                
                treasury.InstallationNumber = InstallationNumber;
                treasury.TreasuryAmount = iAmount;
                treasury.UserID = SecurityHelper.CurrentUser.User_No;                
                treasury.TreasuryType = HP_Type;

                if (((bool)optJackpot.IsChecked) || ((bool)optProgressive.IsChecked) )//|| ((bool)optHandpay.IsChecked))
                {
                    PostHandpayEvent(treasury);
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }

        }

        private void cmbBarPositions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)            
                btnSave_Click(sender, e);            
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
                        if (_dispenserStatus != null)
                        {
                            _dispenserStatus.KillLoadItemsThread();
                        }
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CManualAttendantPays objects are released successfully.");

                }
                disposed = true;
            }
        }


       
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CManualAttendantPays"/> is reclaimed by garbage collection.
        /// </summary>
        ~CManualAttendantPays()
        {
            Dispose(false);
        }

        #endregion

      
    }
}
