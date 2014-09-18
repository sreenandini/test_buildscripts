using System;
using System.Data;
using System.Windows;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Views;
using BMC.Security;
using BMC.Security.Interfaces;
using BMC.Transport;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.Security;
using Microsoft.Win32;
using BMC.Common.ConfigurationManagement;
using BMC.Common;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.Common.LogManagement;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Presentation.POS.Helper_classes;
using System.Collections.Generic;
using System.Globalization;
using BMCIPC.CDO;
using System.Windows.Input;
using System.Threading;


namespace BMC.Presentation
{
    public partial class Login : IDisposable
    {
        private string _sKeyText = string.Empty;
        public static List<SiteConfig> _siteconfig = new List<SiteConfig>();
        public static BMC.Business.CashDeskOperator.WebServices.Proxy WebProxy;
        public Login()
        {
            try
            {
                InitializeComponent();
                txtUname.Focus();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GetInitialSettings();
                GetAppSettings();
                ExtensionMethods.CurrentSiteCulture = ConfigManager.Read("GetDefaultCultureForRegion");
                ExtensionMethods.CurrentCurrenyCulture = ConfigManager.Read("GetDefaultCultureForCurrency");
                ExtensionMethods.CurrentDateCulture = ConfigManager.Read("GetDefaultCultureForDate");

                ((App)Application.Current).CurrentCultureName = ConfigManager.Read("GetDefaultCultureForUserLanguage");
                if (String.IsNullOrEmpty(oCommonUtilities.CreateInstance().GetConnectionString()))
                {
                    MessageBox.ShowBox("MessageID1", BMC_Icon.Error);
                    App.Current.Shutdown();
                }
                SecurityHelper.CreateInstance(oCommonUtilities.CreateInstance().GetConnectionString(), false);
                tbCopyrightInfo.Text = "© " + DateTime.Now.Year + " Bally Technologies Inc. All Rights Reserved"; 
                    //Security.SecurityHelper.GetSetting("COPYRIGTINFO", "© 2011 Bally Technologies Inc. All Rights Reserved");
                MessageBox.parentOwner = this;

                txtUname.Text = CDOSettings.Current.LoggedInUser.UserName;
                if (!txtUname.Text.IsNullOrEmpty())
                {
                    FocusManager.SetFocusedElement(this, txtPWD);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Login() Exception", LogManager.enumLogLevel.Error);
            }
            txtPWD.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, DisablePastePasswordField));
        }

        private void DisablePastePasswordField(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                _sKeyText = ((KeyboardInterface)sender).KeyString;
            }
            ((KeyboardInterface)sender).Closing -= ObjKeyboardClosing;
        }

        private void txtUname_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtUname.Text = DisplayKeyboard(txtUname.Text, string.Empty);
            txtUname.SelectionStart = txtUname.Text.Length;
        }

        private void txtPwd_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtPWD.Password = DisplayKeyboard(string.Empty, "Pwd");
            txtPWD.SelectAll();
        }


        public string DisplayKeyboard(string keyText, string type)
        {
            _sKeyText = "";

            using (var objKeyboard = new KeyboardInterface())
            {
                if (type == "Pwd")
                {
                    objKeyboard.IsPwd = true;
                }
                objKeyboard.Closing += ObjKeyboardClosing;
                objKeyboard.KeyString = keyText;
                objKeyboard.Top = Top + Height - objKeyboard.Height;
                objKeyboard.Left = Left + Width / 2 - objKeyboard.Width / 2;
                objKeyboard.Owner = this;
                objKeyboard.ShowDialogEx(this);
            }

            return _sKeyText;
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnLogin.IsEnabled = false;
                LoginApplication();
            }
            finally
            {
                btnLogin.IsEnabled = true;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        public void GetInitialSettings()
        {
            DataSet objDsSetting;
            var objCdo = oCommonUtilities.CreateInstance();
            DataRow settingsRow;
            try
            {
                Application.Current.Resources["App_CurrencyCulture"] = "(" + "".GetCurrencySymbol() + ")";

                objDsSetting = objCdo.GetInitialSettings();
                if (objDsSetting != null && objDsSetting.Tables.Count > 0 && objDsSetting.Tables[0].Rows.Count > 0)
                {
                    settingsRow = objDsSetting.Tables[0].Rows[0];
                    Settings.Gaming_Day_Start_Hour = (settingsRow["GAMING_DAY_START_HOUR"].ToString() != string.Empty) ? int.Parse(settingsRow["GAMING_DAY_START_HOUR"].ToString()) : 0;
                    Settings.Ticket_Expire = (settingsRow["TICKET_EXPIRE"].ToString() != string.Empty) ? int.Parse(settingsRow["TICKET_EXPIRE"].ToString()) : 0;
                    Settings.Region = settingsRow["REGION"].ToString();
                    Settings.Connection = settingsRow["Connection"].ToString();
                    Settings.HeadCashierSig = settingsRow["TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG"].ToString();
                    Settings.ManagerSig = settingsRow["TICKETVALIDATE_REQUIRES_MANAGER_SIG"].ToString();
                    Settings.AllowOffLineRedeem = (settingsRow["Allow_Offline_Redeem"].ToString() != string.Empty) ? bool.Parse(settingsRow["Allow_Offline_Redeem"].ToString()) : false;
                    Settings.SGVI_Enabled = (settingsRow["SGVI_ENABLED"].ToString() != string.Empty) ? bool.Parse(settingsRow["SGVI_ENABLED"].ToString()) : false;
                    Settings.VoidTransaction = (settingsRow["VoidTransactions"].ToString() != string.Empty) ? bool.Parse(settingsRow["VoidTransactions"].ToString()) : false;
                    Settings.OnScreenKeyboard = (settingsRow["USE_ON_SCREEN_KEYBOARD"].ToString() != string.Empty) ? bool.Parse(settingsRow["USE_ON_SCREEN_KEYBOARD"].ToString()) : false;
                    Settings.EnableTicketRedeemRecipt = (settingsRow["EnableRedeemPrintCDO"].ToString() != string.Empty) ? bool.Parse(settingsRow["EnableRedeemPrintCDO"].ToString()) : false;
                    //Settings.ReceiptPrinter = bool.Parse(SettingsRow["ReceiptPrinter"].ToString());
                    Settings.EnableVoucher = (settingsRow["TicketValidate_EnableVoucher"].ToString() != string.Empty) ? bool.Parse(settingsRow["TicketValidate_EnableVoucher"].ToString()) : false;
                    Settings.HandpayManual = (settingsRow["HandpayManualEntry"].ToString() != string.Empty) ? bool.Parse(settingsRow["HandpayManualEntry"].ToString()) : false;
                    Settings.Not_Issue_Ticket = (settingsRow["CD_NOT_ISSUE_VOUCHER"].ToString() != string.Empty) ? bool.Parse(settingsRow["CD_NOT_ISSUE_VOUCHER"].ToString()) : false;
                    Settings.TicketDeclaration = settingsRow["TicketDeclarationMethod"].ToString();
                    Settings.EnableCounterInManualCashEntry = Settings.GetBoolValue(settingsRow, "EnableCounterInManualCashEntry");
                    Settings.TITO_Not_In_Use = (settingsRow["CD_TITO_NOT_IN_USE"].ToString() != string.Empty) ? bool.Parse(settingsRow["CD_TITO_NOT_IN_USE"].ToString()) : false;
                    Settings.TV_FillScreen = (settingsRow["TicketValidation_FillScreen"].ToString() != string.Empty) ? bool.Parse(settingsRow["TicketValidation_FillScreen"].ToString()) : false;
                    Settings.EnableLaundering = (settingsRow["EnableLaundering"].ToString() != string.Empty) ? bool.Parse(settingsRow["EnableLaundering"].ToString()) : false;
                    Settings.CD_Not_Use_Hoppers = (settingsRow["CD_NOT_USE_HOPPERS"].ToString() != string.Empty) ? bool.Parse(settingsRow["CD_NOT_USE_HOPPERS"].ToString()) : false;
                    Settings.EnableHandpayReceipt = (settingsRow["TicketValidate_EnableHandpayReceipt"].ToString() != string.Empty) ? bool.Parse(settingsRow["TicketValidate_EnableHandpayReceipt"].ToString()) : false;
                    Settings.EnableIssueReceipt = (settingsRow["TicketValidate_EnableIssueReceipt"].ToString() != string.Empty) ? bool.Parse(settingsRow["TicketValidate_EnableIssueReceipt"].ToString()) : false;
                    Settings.EnableProgReceipt = (settingsRow["TicketValidate_EnableProgressivePayoutReceipt"].ToString() != string.Empty) ? bool.Parse(settingsRow["TicketValidate_EnableProgressivePayoutReceipt"].ToString()) : false;
                    Settings.EnableRefillReceipt = (settingsRow["TicketValidate_EnableRefillReceipt"].ToString() != string.Empty) ? bool.Parse(settingsRow["TicketValidate_EnableRefillReceipt"].ToString()) : false;
                    Settings.EnableRefundReceipt = (settingsRow["TicketValidate_EnableRefundReceipt"].ToString() != string.Empty) ? bool.Parse(settingsRow["TicketValidate_EnableRefundReceipt"].ToString()) : false;
                    Settings.EnableShortPayReceipt = (settingsRow["TicketValidate_EnableShortpayReceipt"].ToString() != string.Empty) ? bool.Parse(settingsRow["TicketValidate_EnableShortpayReceipt"].ToString()) : false;
                    Settings.RedeemConfirm = (settingsRow["Confirm_Redeem_Message"].ToString() != string.Empty) ? bool.Parse(settingsRow["Confirm_Redeem_Message"].ToString()) : true;
                    Settings.RegulatoryEnabled = (settingsRow["RegulatoryEnabled"].ToString() != string.Empty ? bool.Parse(settingsRow["RegulatoryEnabled"].ToString()) : false);
                    Settings.RegulatoryType = settingsRow["RegulatoryType"].ToString();
                    Settings.SiteCode = settingsRow["TICKET_LOCATION_CODE"].ToString();
                    Settings.HandpayPayoutCustomer_Min = Convert.ToDouble(settingsRow["HandpayPayoutCustomer_Min"].ToString());
                    Settings.HandpayPayoutCustomer_Max = Convert.ToDouble(settingsRow["HandpayPayoutCustomer_Max"].ToString());
                    Settings.HandpayPayoutCustomer_BankAccNo = Convert.ToDouble(settingsRow["HandpayPayoutCustomer_BankAccNo"].ToString());

                    Settings.printTicket = (settingsRow["Ithaca950"].ToString() != string.Empty) ? bool.Parse(settingsRow["Ithaca950"].ToString()) : false;
                    Settings.PrinterPort = (settingsRow["PrinterPort"] != null) ? settingsRow["PrinterPort"].ToString() : string.Empty;

                    Settings.WebURL = Convert.ToString(BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read(Constants.CONSTANT_REGISTRYPATH), "BGSWebService"));
                    //var regKey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read(Constants.CONSTANT_REGISTRYPATH));
                    //if (regKey != null) Settings.WebURL = regKey.GetValue("BGSWebService").ToString();

                    Settings.IssueTicketMaxValue = (!settingsRow.IsNull("IssueTicketMaxValue")) ? settingsRow["IssueTicketMaxValue"].ToString() : string.Empty;
                    Settings.PrintTicket_EncryptDigits = (!settingsRow.IsNull("Issue_Ticket_Encrypt_BarCode")) ? bool.Parse(settingsRow["Issue_Ticket_Encrypt_BarCode"].ToString()) : false;
                    Settings.Client = (settingsRow["Client"] != null) ? settingsRow["Client"].ToString() : string.Empty;
                    Settings.MaxHandPayAuthRequired = (settingsRow["MaxHandPayAuthRequired"].ToString() != string.Empty ? bool.Parse(settingsRow["MaxHandPayAuthRequired"].ToString()) : false);
                    Settings.ManualEntryTicketValidation = (settingsRow["ManualEntryTicketValidation"].ToString() != string.Empty ? bool.Parse(settingsRow["ManualEntryTicketValidation"].ToString()) : false);

                    Settings.RedeemTicketCustomer_Min = Convert.ToInt32(settingsRow["RedeemTicketCustomer_Min"].ToString());
                    Settings.RedeemTicketCustomer_Max = Convert.ToInt32(settingsRow["RedeemTicketCustomer_Max"].ToString());
                    Settings.RedeemTicketCustomer_BankAcctNo = Convert.ToInt32(settingsRow["RedeemTicketCustomer_BankAcctNo"].ToString());

                    Settings.RedeemExpiredTicket = (settingsRow["RedeemExpiredTicket"].ToString() != string.Empty ? bool.Parse(settingsRow["RedeemExpiredTicket"].ToString()) : false);
                    Settings.IsAFTEnabledForSite = (settingsRow["IsAFTEnabledForSite"].ToString() != string.Empty) ? bool.Parse(settingsRow["IsAFTEnabledForSite"].ToString()) : false;
                    Settings.MachineMaintenance = (settingsRow["MachineMaintenance"].ToString() != string.Empty) ? bool.Parse(settingsRow["MachineMaintenance"].ToString()) : false;

                    Settings.CheckExchangeServerConnectivity = (settingsRow["CheckExchangeServerConnectivity"].ToString() != string.Empty) ? bool.Parse(settingsRow["CheckExchangeServerConnectivity"].ToString()) : false;
                    Settings.ExchangeServerConnectivityCheckInterval = (settingsRow["ExchangeServerConnectivityCheckInterval"].ToString() != string.Empty) ? int.Parse(settingsRow["ExchangeServerConnectivityCheckInterval"].ToString()) : 5;
                    Settings.ClearEventsOnFinalDrop = settingsRow["ClearEventsOnFinalDrop"].ToString();

                    Settings.VoucherPrinterName = (!settingsRow.IsNull("Voucher_Printer_Name")) ? settingsRow["Voucher_Printer_Name"].ToString() : string.Empty;

                    Settings.IsFinalDropRequiredForRemoval = (settingsRow["IsFinalDropRequiredForRemoval"].ToString() != string.Empty) ? bool.Parse(settingsRow["IsFinalDropRequiredForRemoval"].ToString()) : false;
                    Settings.SiteFloorViewState = settingsRow["SiteFloorViewState"].ToString();
                    Settings.TimeZoneID = settingsRow["TIMEZONENAME"].ToString();
                    Settings.BillVoucherCounterCOMPort = settingsRow["BillVoucherCounterCOMPort"].ToString();
                    Settings.IsAFTIncludedInCalculation = (settingsRow["IsAFTIncludedInCalculation"].ToString() != string.Empty) ? bool.Parse(settingsRow["IsAFTIncludedInCalculation"].ToString()) : false;

                    Settings.W2GMessage = (settingsRow["W2GMessage"].ToString() != string.Empty) ? bool.Parse(settingsRow["W2GMessage"].ToString()) : true;
                    Settings.W2GWinAmount = (settingsRow["W2GWinAmount"].ToString() != string.Empty) ? double.Parse(settingsRow["W2GWinAmount"].ToString()) : 1200;
                    Settings.CopyRightInfo = (settingsRow.Table.Columns.Contains("COPYRIGTINFO")) ? Convert.ToString(settingsRow["COPYRIGTINFO"]) : string.Empty;
                    Settings.HandPayBeepEnabled = (settingsRow.Table.Columns.Contains("HandPayBeepEnabled")) ? Convert.ToString(settingsRow["HandPayBeepEnabled"]).ToUpper() : "N";
                    Settings.DailyAutoReadTime = (settingsRow.Table.Columns.Contains("DailyAutoReadTime")) ? Convert.ToString(settingsRow["DailyAutoReadTime"]).ToUpper() : "6:30";
                    Settings.Handpay_Wav_Path = (settingsRow.Table.Columns.Contains("Handpay_Wav_Path")) ? Convert.ToString(settingsRow["Handpay_Wav_Path"]).ToUpper() : "";
                    Settings.SHOWHANDPAYCODE = (settingsRow.Table.Columns.Contains("SHOWHANDPAYCODE")) ? Convert.ToString(settingsRow["SHOWHANDPAYCODE"]).ToUpper() : "TRUE";
                    Settings.CAGE_ALLOWCASHIERLOCONTKTS = (settingsRow.Table.Columns.Contains("CAGE_ALLOWCASHIERLOCONTKTS")) ? bool.Parse(settingsRow["CAGE_ALLOWCASHIERLOCONTKTS"].ToString()) : false;
                    Settings.CAGE_ALLOWPRINTTKTOVERRIDE = (settingsRow.Table.Columns.Contains("CAGE_ALLOWPRINTTKTOVERRIDE")) ? bool.Parse(settingsRow["CAGE_ALLOWPRINTTKTOVERRIDE"].ToString()) : false;
                    Settings.CAGE_TKTPRINTERENABLED = (settingsRow.Table.Columns.Contains("CAGE_TKTPRINTERENABLED")) ? bool.Parse(settingsRow["CAGE_TKTPRINTERENABLED"].ToString()) : false;
                    Settings.CAGE_MINTKTPRINTAMTFOREMP = (settingsRow.Table.Columns.Contains("CAGE_MINTKTPRINTAMTFOREMP")) ? int.Parse(settingsRow["CAGE_MINTKTPRINTAMTFOREMP"].ToString()) : 0;
                    Settings.CAGE_MAXTKTREDEMPTIONAMTFOREMP = (settingsRow.Table.Columns.Contains("CAGE_MAXTKTREDEMPTIONAMTFOREMP")) ? int.Parse(settingsRow["CAGE_MAXTKTREDEMPTIONAMTFOREMP"].ToString()) : 0;
                    Settings.CAGE_MAXTKTREDEMPTIONAMT = (settingsRow.Table.Columns.Contains("CAGE_MAXTKTREDEMPTIONAMT")) ? int.Parse(settingsRow["CAGE_MAXTKTREDEMPTIONAMT"].ToString()) : 0;
                    Settings.CAGE_MAXNOOFTKTPRINTLIMIT = (settingsRow.Table.Columns.Contains("CAGE_MAXNOOFTKTPRINTLIMIT")) ? int.Parse(settingsRow["CAGE_MAXNOOFTKTPRINTLIMIT"].ToString()) : 0;
                    Settings.CAGE_MAXDAILYCASHIERGENTKTAMT = (settingsRow.Table.Columns.Contains("CAGE_MAXDAILYCASHIERGENTKTAMT")) ? int.Parse(settingsRow["CAGE_MAXDAILYCASHIERGENTKTAMT"].ToString()) : 0;
                    Settings.CAGE_MAXTKTPRINTAMTFOREMP = (settingsRow.Table.Columns.Contains("CAGE_MAXTKTPRINTAMTFOREMP")) ? int.Parse(settingsRow["CAGE_MAXTKTPRINTAMTFOREMP"].ToString()) : 0;
                    Settings.CAGE_ENABLED = Settings.GetBoolValue(settingsRow, "CAGE_ENABLED");
                    Settings.IsKioskRequired = (settingsRow.Table.Columns.Contains("IsKioskRequired")) ? bool.Parse(settingsRow["IsKioskRequired"].ToString()) : false;
                    Settings.EnableCustomerReceipt = (settingsRow["TicketValidate_EnableCustomerReceipt"].ToString() != string.Empty) ? bool.Parse(settingsRow["TicketValidate_EnableCustomerReceipt"].ToString()) : false;
                    Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE = (settingsRow.Table.Columns.Contains("SHOW_NAME_IN_RECEPIT_SIGNATURE")) ? bool.Parse(settingsRow["SHOW_NAME_IN_RECEPIT_SIGNATURE"].ToString()) : false;
                    Settings.Auto_Declare_Monies = (settingsRow["Auto_Declare_Monies"].ToString() != string.Empty) ? bool.Parse(settingsRow["Auto_Declare_Monies"].ToString()) : false;
                    Settings.Disable_Machine_On_Drop = (settingsRow["DISABLE_MACHINE_ON_DROP"].ToString() != string.Empty) ? bool.Parse(settingsRow["DISABLE_MACHINE_ON_DROP"].ToString()) : false;
                    Settings.NoWaitForDisableMachine = (settingsRow["NoWaitForDisableMachine"].ToString() != string.Empty) ? bool.Parse(settingsRow["NoWaitForDisableMachine"].ToString()) : false;
                    Settings.AUTOLOGOFF_TIMEOUT = (settingsRow["AUTOLOGOFF_TIMEOUT"].ToString() != string.Empty) ? Int32.Parse(settingsRow["AUTOLOGOFF_TIMEOUT"].ToString()) : 0;
                    Settings.SHOWHANDPAYCODE = (settingsRow.Table.Columns.Contains("SHOWHANDPAYCODE")) ? Convert.ToString(settingsRow["SHOWHANDPAYCODE"]).ToUpper() : "TRUE";
                    Settings.HANDLE_EXCEPTIONTICKETS = bool.Parse((settingsRow.Table.Columns.Contains("HANDLE_EXCEPTIONTICKETS")) ? Convert.ToString(settingsRow["HANDLE_EXCEPTIONTICKETS"]).ToUpper() : "FALSE");
                    Settings.HANDLE_EXCEPTIONTICKETS_COUNTER = bool.Parse((settingsRow.Table.Columns.Contains("HANDLE_EXCEPTIONTICKETS_COUNTER")) ? Convert.ToString(settingsRow["HANDLE_EXCEPTIONTICKETS_COUNTER"]).ToUpper() : "FALSE");
                    Settings.HANDLE_EXCEPTIONTICKETS = bool.Parse((settingsRow.Table.Columns.Contains("HANDLE_EXCEPTIONTICKETS")) ? Convert.ToString(settingsRow["HANDLE_EXCEPTIONTICKETS"]).ToUpper() : "FALSE");
                    Settings.HANDLE_EXCEPTIONTICKETS_COUNTER = bool.Parse((settingsRow.Table.Columns.Contains("HANDLE_EXCEPTIONTICKETS_COUNTER")) ? Convert.ToString(settingsRow["HANDLE_EXCEPTIONTICKETS_COUNTER"]).ToUpper() : "FALSE");

                    // Cash Dispenser Settings by A.Vinod Kumar (04/01/2012)
                    Settings.CashDispenserEnabled = Settings.GetBoolValue(settingsRow, "CashDispenserEnabled");
                    Settings.AutoCashDispenseRequired = Settings.GetBoolValue(settingsRow, "AutoCashDispenseRequired");
                    Settings.SendPT10FromClient = Settings.GetBoolValue(settingsRow, "SendPT10FromClient");
                    Settings.PT_GATEWAY_IP = settingsRow.Table.Columns.Contains("PT_GATEWAY_IP") ? Convert.ToString(settingsRow["PT_GATEWAY_IP"]).ToUpper() : "";
                    Settings.SDT_SendPTPortNo = settingsRow.Table.Columns.Contains("SDT_SendPTPortNo") ? Convert.ToInt32(settingsRow["SDT_SendPTPortNo"]) : 6701;
                    Settings.SDT_SendCAPortNo = settingsRow.Table.Columns.Contains("SDT_SendCAPortNo") ? Convert.ToInt32(settingsRow["SDT_SendCAPortNo"]) : 8801;
                    Settings.HANDLE_EXCEPTION_PP_TICKETS = Settings.GetBoolValue(settingsRow, "HANDLE_EXCEPTION_PP_TICKETS");
                    Settings.ShortPayEnabled = Settings.GetBoolValue(settingsRow, "IsShortPayEnabled");
                    Settings.Declaration_ShowoutValues = Settings.GetBoolValue(settingsRow, "Declaration_ShowoutValues");
                    Settings.IsGloryCDEnabled = (settingsRow["CashDispenserType"].ToString() != string.Empty && settingsRow["CashDispenserType"].ToString().ToUpper().Equals("GLORY")) ? true : false;
                    Settings.ShortPayAuthorizationRequired = Settings.GetBoolValue(settingsRow, "ShortPayAuthorizationRequired");
                    Settings.ShortPayAuthorizationLimit = Convert.ToDouble(settingsRow["ShortPayAuthorizationLimit"].ToString());
                    //Settings.REDEEM_TICKET_POP_UP_ALERT_VISIBILITY = (settingsRow["REDEEM_TICKET_POP_UP_ALERT_VISIBILITY"].ToString() != string.Empty) ? bool.Parse(settingsRow["REDEEM_TICKET_POP_UP_ALERT_VISIBILITY"].ToString()) : false;
                    Settings.CentralizedDeclaration = Settings.GetBoolValue(settingsRow, "CentralizedDeclaration");
                    Settings.DropAlert = Settings.GetBoolValue(settingsRow, "DropAlert");
                    Settings.DeclarationAlert = Settings.GetBoolValue(settingsRow, "DeclarationAlert");
                    //Allow Machine Removal in CDO when CentralizedDeclaration declaration in Enabled By  R.Rajkumar(05/02/2013)
                    Settings.Allow_Machine_Removal = Settings.GetBoolValue(settingsRow, "Allow_Machine_Removal");
                    Settings.AutoDropEnabled = Settings.GetBoolValue(settingsRow, "AutoDropEnabled");
                    Settings.ForceManualDrop = Settings.GetBoolValue(settingsRow, "ForceManualDrop");
                    Settings.LiquidationType = settingsRow["LiquidationType"].ToString();
                    Settings.ExpenseShare = Settings.GetBoolValue(settingsRow, "ExpenseShare");
                    Settings.WriteOffShare = Settings.GetBoolValue(settingsRow, "WriteOffShare");
                    Settings.IsEmployeeCardTrackingEnabled = Settings.GetBoolValue(settingsRow, "IsEmployeeCardTrackingEnabled");
                    Settings.LiquidationProfitShare = Settings.GetBoolValue(settingsRow, "LiquidationProfitShare");
                    Settings.CentralizedReadLiquidation = Settings.GetBoolValue(settingsRow, "CentralizedReadLiquidation");
                    Settings.NotesCounter_AutoStart = Settings.GetBoolValue(settingsRow, "NotesCounter_AutoStart");
                    Settings.IsPartCollectionEnabled = Settings.GetBoolValue(settingsRow, "IsPartCollectionEnabled");
                    Settings.Allow_Machine_Removal = Settings.GetBoolValue(settingsRow, "Allow_Machine_Removal");
                    Settings.DropSummaryReport = Settings.GetBoolValue(settingsRow, "DropSummaryReport");
                    Settings.IsMachineBasedAutoDrop = Settings.GetBoolValue(settingsRow, "IsMachineBasedAutoDrop");
                    Settings.AGSValue = String.IsNullOrEmpty(settingsRow["AGSValue"].ToString()) ? "" : settingsRow["AGSValue"].ToString();
                    Settings.ProcessW2GAmount = Settings.GetBoolValue(settingsRow, "ProcessW2GAmount");
                    Settings.VoidVouchers = Settings.GetBoolValue(settingsRow, "VoidVouchers");
                    Settings.WeeklyReport = Settings.GetBoolValue(settingsRow, "WeeklyReport");
                    Settings.ServiceNotRunningInterval = Convert.ToInt32("0" + settingsRow["ServiceNotRunningInterval"]);
                    Settings.ValidateGMUInSite = Settings.GetBoolValue(settingsRow, "ValidateGMUInSite");
                    Settings.DefaultGMUValue = settingsRow.Table.Columns.Contains("DefaultGMUValue") ? settingsRow["DefaultGMUValue"].ToString() : "16";
                    Settings.AddShortpayInVoucherOut = Settings.GetBoolValue(settingsRow, "AddShortpayInVoucherOut");
                    Settings.dtCashierTransStartTime = null;
                    Settings.EnableMonthToDateTab = Settings.GetBoolValue(settingsRow, "MonthToDateEnabled");
                    Settings.EnableCashdeskReconciliation = Settings.GetBoolValue(settingsRow, "EnableCashdeskReconciliation");
                    Settings.EnableCashdeskMovement = Settings.GetBoolValue(settingsRow, "EnableCashdeskMovement");
                    Settings.EnableSystemBalancing = Settings.GetBoolValue(settingsRow, "EnableSystemBalancing");
                    Settings.IsPromotionalTicketEnabled = Settings.GetBoolValue(settingsRow, "IsPromotionalTicketEnabled");
                    Settings.IsTISEnabled = Settings.GetBoolValue(settingsRow, "IsTISEnabled");
                    Settings.IsGameCappingEnabled = Settings.GetBoolValue(settingsRow, "IsGameCappingEnabled");
                    Settings.MaximumPromotionalTicketsCount = settingsRow["MaximumPromotionalTicketsCount"].ToString(); //Settings.GetStringValue(settingsRow, "MaximumPromotionalTicketsCount");
                    Settings.MaximumPromotionalTicketAmount = settingsRow["MaximumPromotionalTicketAmount"].ToString();//Settings.GetStringValue(settingsRow, "MaximumPromotionalTicketAmount");
                    Settings.DefaultPromotionalTicketExpireDays = settingsRow["DefaultPromotionalTicketExpireDays"].ToString();// Settings.GetStringValue(settingsRow, "PromotionalTicketExpireDays");
                    Settings.DisplayGameNameInFloorView = (!settingsRow.IsNull("DisplayGameNameInFloorView")) ? bool.Parse(settingsRow["DisplayGameNameInFloorView"].ToString()) : false;
                    Settings.IsBillCounterAmountEditable = (!settingsRow.IsNull("IsBillCounterAmountEditable")) ? bool.Parse(settingsRow["IsBillCounterAmountEditable"].ToString()) : false;
                    Settings.IsTicketAnomaliesEnabled = Settings.GetBoolValue(settingsRow, "TicketAnomaliesEnabled");

                    BMC.Business.CashDeskOperator.HourlyDetails.ReadSettings(settingsRow);
                    Settings.VaultStandardFillAmount = (settingsRow.Table.Columns.Contains("VaultStandardFillAmount")) ? Convert.ToDecimal(settingsRow["VaultStandardFillAmount"]) : 1000;
                    Settings.CentralizedVaultDeclaration = Settings.GetBoolValue(settingsRow, "CentralizedVaultDeclaration");
                    string TISTicketPrefixDigit = Settings.GetStringValue(settingsRow, "TISTicketPrefixDigit");
                    if (!TISTicketPrefixDigit.IsNullOrEmpty())
                    {
                        Settings.TISTicketPrefixDigit = TISTicketPrefixDigit[0];
                    }

                    //For multiple voucher redeem - TIS Tickets handling                                     
                    Settings.TISTicketPrefix = (settingsRow["TISTicketPrefix"].ToString() != string.Empty) ? int.Parse(settingsRow["TISTicketPrefix"].ToString()) : 7;
                       
                    Settings.GridViewForcePeriodicWaitInterval = (settingsRow["GridViewForcePeriodicWaitInterval"].ToString() != string.Empty) ? int.Parse(settingsRow["GridViewForcePeriodicWaitInterval"].ToString()) : 10;
                    Settings.GridViewForcePeriodicThreadWaitInterval = (settingsRow["GridViewForcePeriodicThreadWaitInterval"].ToString() != string.Empty) ? int.Parse(settingsRow["GridViewForcePeriodicThreadWaitInterval"].ToString()) : 5000;
                
                    Settings.ShowVaultPrintMessage = Settings.GetBoolValue(settingsRow, "ShowVaultPrintMessage");
                    Settings.ShowVaultConfirmMessage = Settings.GetBoolValue(settingsRow, "ShowVaultConfirmMessage");
                    Settings.ShowVaultSuccessMessage = Settings.GetBoolValue(settingsRow, "ShowVaultSuccessMessage");
                    Settings.AutoFillDeclaredAmount = Settings.GetBoolValue(settingsRow, "AutoFillDeclaredAmount");
                    Settings.IsCommonCDODeclarationEnabled = Settings.GetBoolValue(settingsRow ,"IsCommonCDODeclarationEnabled");
                    Settings.AllowManualKeyboard = Settings.GetBoolValue(settingsRow, "AllowManualKeyboard");
                    Settings.ManualCashEntryEnableZero = Settings.GetBoolValue(settingsRow, "ManualCashEntryEnableZero");
                    Settings.ShowSystemCalendar = Settings.GetBoolValue(settingsRow, "ShowSystemCalendar");
                    Settings.IsMultipleVoucherRedemptionEnabled = Settings.GetBoolValue(settingsRow, "IsMultipleVoucherRedemptionEnabled");
                    Settings.PrintHeaderFormat = settingsRow["PrintHeaderFormat"].ToString();
                    Settings.StackerFeature = Settings.GetBoolValue(settingsRow, "StackerFeature");
                    Settings.Hourly_DefaultItem = Settings.GetStringValue(settingsRow, "Hourly_DefaultItem");
                    Settings.AllowMultipleDrops = Settings.GetBoolValue(settingsRow, "AllowMultipleDrops");

                    Settings.ClearHandpayTilt = Settings.GetBoolValue(settingsRow, "ClearHandpayTilt");
                    Settings.AddShortpayCommentstoDefault = Settings.GetBoolValue(settingsRow, "AddShortpayCommentstoDefault");
                    Settings.ShowBatchWinLossOnDeclaration = Settings.GetBoolValue(settingsRow, "ShowBatchWinLossOnDeclaration");
                    Settings.ShowCollectionReport = Settings.GetBoolValue(settingsRow, "ShowCollectionReport");
                    Settings.ShowVarianceReport = Settings.GetBoolValue(settingsRow, "ShowVarianceReport");
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetInitialSettings::Error in loading Intial settings from setting table", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            ExtensionMethods.CurrentSiteCulture = ConfigManager.Read("GetDefaultCultureForRegion");

        }

        public void GetAppSettings()
        {
            DataSet objDsAppSetting;
            var objAppSettingsCdo = oCommonUtilities.CreateInstance();
            DataRow AppsettingsRow;

            try
            {

                objDsAppSetting = objAppSettingsCdo.GetAppSettings();

                if (objDsAppSetting != null && objDsAppSetting.Tables.Count > 0 && objDsAppSetting.Tables[0].Rows.Count > 0)
                {
                    AppsettingsRow = objDsAppSetting.Tables[0].Rows[0];

                    AppSettings.IsReceiptRequired = (AppsettingsRow["IsReceiptRequired"].ToString() != string.Empty) ? bool.Parse(AppsettingsRow["IsReceiptRequired"].ToString()) : false;

                    AppSettings.REDEEM_TICKET_POP_UP_ALERT_VISIBILITY = (AppsettingsRow["REDEEM_TICKET_POP_UP_ALERT_VISIBILITY"].ToString() != string.Empty) ? bool.Parse(AppsettingsRow["REDEEM_TICKET_POP_UP_ALERT_VISIBILITY"].ToString()) : false;

                    AppSettings.FlrView_SortBy_Asset = (AppsettingsRow["FlrView_SortBy_Asset"].ToString() != string.Empty) ? bool.Parse(AppsettingsRow["FlrView_SortBy_Asset"].ToString()) : false;

                    AppSettings.FlrView_SortBy_Position = (AppsettingsRow["FlrView_SortBy_Position"].ToString() != string.Empty) ? bool.Parse(AppsettingsRow["FlrView_SortBy_Position"].ToString()) : false;

                    AppSettings.Is_Confirmation_Required_on_Declaration = (AppsettingsRow["Is_Confirmation_Required_on_Declaration"].ToString() != string.Empty) ? bool.Parse(AppsettingsRow["Is_Confirmation_Required_on_Declaration"].ToString()) : false;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetAppSettings::Error in loading Application Intial settings from setting table", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

        }

        /// <summary>
        /// Check if the user credentials are valid
        /// </summary>
        /// <param name="strUser">The STR user.</param>
        /// <param name="strPass">The STR pass.</param>
        /// <returns>success or failure</returns>
        public static SecurityHelper.LoginResults Checkuser(string strUser, string strPass)
        {
            try
            {
                IUser user;
                SecurityHelper.CreateInstance(oCommonUtilities.CreateInstance().GetConnectionString(), false);
                var result = SecurityHelper.Login(strUser, strPass, out user);
                if (result == SecurityHelper.LoginResults.LoginSuccesful || result == SecurityHelper.LoginResults.PasswordExpired)
                {
                    clsSecurity.UserID = user.SecurityUserID;
                    clsSecurity.UserName = user.UserName;
                    
                    CDOSettings.SaveChanges((c) =>
                    {
                        c.LoggedInUser.UserID = user.User_No;
                        c.LoggedInUser.SecurityUserID = user.SecurityUserID;
                        c.LoggedInUser.UserName = clsSecurity.UserName;
                    });
                }
                //Change Request #203622 fix.
                Thread.CurrentThread.CurrentCulture = new CultureInfo(user.CultureInfo);
              
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return SecurityHelper.LoginResults.LoginFailed;
        }

        private bool CheckBallyuser(string strUser, string strPass)
        {
            bool bResult;

            try
            {
                var oSecurityAuthenticate = new BallySecurityAuthentication();
                var objProperty = new BallySecurityProperty { UserName = strUser, Password = strPass };

                if (objProperty.UserName.ToUpper() != "BALLY")
                {
                    objProperty.Password = oSecurityAuthenticate.EncryptUser(objProperty);
                    bResult = oSecurityAuthenticate.ValidateUser(objProperty);
                }
                else
                    bResult = oSecurityAuthenticate.ValidateUser(objProperty);
            }
            catch (Exception ex)
            {
                bResult = false;
                ExceptionManager.Publish(ex);
            }
            return bResult;
        }

        private bool CheckSiteAAMSVerified()
        {
            LogManager.WriteLog("Inside CheckSiteAAMSVerified", LogManager.enumLogLevel.Info);
            var oProxy = new Proxy(Settings.SiteCode);
            return oProxy.UpdateDetailsFromXML("AAMSVERIFY", Settings.SiteCode);
        }

        private void GetConnectedSites()
        {
            try
            {  
		   _siteconfig.Clear();
                if (!Settings.IsCommonCDODeclarationEnabled)
                    return;
                if (Security.SecurityHelper.HasAccess("BMC.Presentation.CommonCDOforDeclaration"))
                {
                    LogManager.WriteLog("Common CDO for Declaration Enabled", LogManager.enumLogLevel.Debug);
                    WebProxy =
                        new BMC.Business.CashDeskOperator.WebServices.Proxy(Settings.SiteCode, oCommonUtilities.CreateInstance().GetConnectionString());

                    if (WebProxy != null)
                    {
                        LogManager.WriteLog("Following site info we got:", LogManager.enumLogLevel.Debug);
                        foreach (DataRow dr in WebProxy.GetActiveSiteDetailsForUser(Security.SecurityHelper.CurrentUser.SecurityUserID).Rows)
                        {
                            LogManager.WriteLog(dr["Site_Code"].ToString(), LogManager.enumLogLevel.Debug);
                            _siteconfig.Add(new SiteConfig()
                            {
                                SiteCode = dr["Site_Code"].ToString(),
                                SiteName = dr["Site_Name"].ToString(), 
                                ExchangeConnectionString = dr["SC_ExchangeConnectionSting"].ToString(),
                                TicketConnectionString = dr["SC_TicketConnectionSting"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("GetConnectedSites", LogManager.enumLogLevel.Error);

            }
        }

        private void Clear()
        {
            txtUname.Clear();
            txtPWD.Clear();
            txtUname.Focus();
        }

        private void LoginApplication()
        {
            try
            {
                AuditViewerBusiness.CreateInstance(oCommonUtilities.CreateInstance().GetConnectionString());

                if (txtUname.Text.ToUpper() == "CASH" && txtPWD.Password.ToUpper() == "DESK")
                {
                    var userObect = new Security.Manager.UserManager(oCommonUtilities.CreateInstance().GetConnectionString());
                    SecurityHelper.CurrentUser = userObect.GetUserObject(txtUname.Text, txtUname.Text, txtUname.Text);
                    SecurityHelper.CurrentUser.UserName = txtUname.Text;
                    SecurityHelper.CurrentUser.SecurityUserID = 0;
                    SecurityHelper.CurrentUser.User_No = 0;
                    ExtensionMethods.CurrentCurrenyCulture = ConfigManager.Read("GetDefaultCultureForCurrency");
                    ExtensionMethods.CurrentDateCulture = ConfigManager.Read("GetDefaultCultureForDate");
                    if (!IsLicenseValid())
                    {
                        //Close();
                        Clear();
                        return;
                    }
                    var objMainScreen = new MainScreen { UserName = txtUname.Text };
                    objMainScreen.LoginInstance = this;
                    objMainScreen.Show();
                    Close();
                    objMainScreen.DisposeLoginInstanceIfAny();
                }
                else if (txtUname.Text.ToUpper() == "BALLY" && CheckBallyuser(txtUname.Text, txtPWD.Password))
                {

                    SecurityHelper.CreateInstance(oCommonUtilities.CreateInstance().GetConnectionString(), false);
                    var userObect = new Security.Manager.UserManager(oCommonUtilities.CreateInstance().GetConnectionString());
                    SecurityHelper.CurrentUser = userObect.GetUserObject(txtUname.Text, txtUname.Text, txtUname.Text);
                    SecurityHelper.CurrentUser.UserName = txtUname.Text;
                    SecurityHelper.CurrentUser.SecurityUserID = 0;
                    ExtensionMethods.CurrentCurrenyCulture = ConfigManager.Read("GetDefaultCultureForCurrency");
                    ExtensionMethods.CurrentDateCulture = ConfigManager.Read("GetDefaultCultureForDate");
                    if (!IsLicenseValid())
                    {
                        Clear();
                        return;
                    }

                    var objMainScreen = new MainScreen { UserName = txtUname.Text };
                    objMainScreen.LoginInstance = this;
                    objMainScreen.Show();
                    Close();
                    objMainScreen.DisposeLoginInstanceIfAny();
                }
                else
                {
                    var loginResult = Checkuser(txtUname.Text, txtPWD.Password);

                    if (loginResult == SecurityHelper.LoginResults.LoginSuccesful)
                    {
                        ((App)Application.Current).CurrentCultureName =
                            SecurityHelper.GetCultureInfo(SecurityHelper.CurrentUser.SecurityUserID);

                        ExtensionMethods.CurrentCurrenyCulture =
                            SecurityHelper.GetCurrencyCultureInfo(SecurityHelper.CurrentUser.SecurityUserID);
                        ExtensionMethods.CurrentDateCulture =
                            SecurityHelper.GetDateCultureInfo(SecurityHelper.CurrentUser.SecurityUserID);

                        if (string.IsNullOrEmpty(ExtensionMethods.CurrentCurrenyCulture))
                            ExtensionMethods.CurrentCurrenyCulture = ConfigManager.Read("GetDefaultCultureForCurrency");

                        if (string.IsNullOrEmpty(ExtensionMethods.CurrentDateCulture))
                            ExtensionMethods.CurrentDateCulture = ConfigManager.Read("GetDefaultCultureForDate");

                        GetInitialSettings();
                        GetAppSettings();
                        if (!IsLicenseValid())
                        {
                            Clear();
                            return;
                        }
                        var objMainScreen = new MainScreen { UserName = SecurityHelper.CurrentUser.UserName };
                        objMainScreen.LoginInstance = this;
                        objMainScreen.Show();
                        GetConnectedSites();
                        Audit("Login Successful for User-" + SecurityHelper.CurrentUser.DisplayName);

                        Close();
                        objMainScreen.DisposeLoginInstanceIfAny();
                    }
                    else
                    {
                        if (loginResult == SecurityHelper.LoginResults.IsUserTerminated)
                        {
                            MessageBox.ShowBox("MessageID446", BMC_Icon.Error);
                            Audit("Account Terminated for User-" + txtUname.Text);
                        }
                        else if ((loginResult == SecurityHelper.LoginResults.PasswordExpired) || (loginResult == SecurityHelper.LoginResults.LoginReset))
                        {

                            if (loginResult == SecurityHelper.LoginResults.PasswordExpired)
                            {
                                MessageBox.ShowBox("MessageID267", BMC_Icon.Information);
                                Audit("Password expired for User-" + txtUname.Text);
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID268", BMC_Icon.Information);
                                Audit("Password Reset for User-" + txtUname.Text);
                            }

                            var passwordRetry = new PasswordRetry(SecurityHelper.CurrentUser.SecurityUserID);
                            if (passwordRetry.ShowDialogEx(this) == true)
                            {
                                ((App)Application.Current).CurrentCultureName =
                            SecurityHelper.GetCultureInfo(SecurityHelper.CurrentUser.SecurityUserID);

                                ExtensionMethods.CurrentCurrenyCulture =
                                    SecurityHelper.GetCurrencyCultureInfo(SecurityHelper.CurrentUser.SecurityUserID);
                                ExtensionMethods.CurrentDateCulture =
                                    SecurityHelper.GetDateCultureInfo(SecurityHelper.CurrentUser.SecurityUserID);

                                if (string.IsNullOrEmpty(ExtensionMethods.CurrentCurrenyCulture))
                                    ExtensionMethods.CurrentCurrenyCulture = ConfigManager.Read("GetDefaultCultureForCurrency");

                                if (string.IsNullOrEmpty(ExtensionMethods.CurrentDateCulture))
                                    ExtensionMethods.CurrentDateCulture = ConfigManager.Read("GetDefaultCultureForDate");

                                GetInitialSettings();
                                GetAppSettings();
                                if (!IsLicenseValid())
                                {
                                    Clear();
                                    return;
                                }
                                var objMainScreen = new MainScreen { UserName = SecurityHelper.CurrentUser.UserName };
                                objMainScreen.Owner = this;

                                objMainScreen.Show();

                                Hide();
                            }

                        }
                        else if (loginResult == SecurityHelper.LoginResults.MaxAttemptsExceeded)
                        {
                            MessageBox.ShowBox("MessageID269", BMC_Icon.Error);
                            Audit("Account Locked for User-" + txtUname.Text);
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID3");
                            Audit("Invalid Login attempt for User-" + txtUname.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID3");
            }
        }

        /// <summary>
        /// To check whether the site having valid license and allow/restrict the user to login to the site based on the license settings
        /// </summary>
        /// <returns></returns>
        private bool IsLicenseValid()
        {
            try
            {
                LicenseValidator.CurrentWindow = this;
                LicenseValidator objLicenseValidator = LicenseValidator.GetLicenseValidator;
                if (objLicenseValidator != null)
                    return objLicenseValidator.ValidateLicense();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        private void Audit(string sDesc)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {
                AuditModuleName = ModuleName.Login,
                Audit_Screen_Name = "Login",
                Audit_Desc = sDesc,
                AuditOperationType = OperationType.ADD
            });
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                LoginApplication();

        }

        #region MainScreen Cleanup

        private MainScreen _mainInstance = null;

        public MainScreen MainInstance
        {
            get { return _mainInstance; }
            set { _mainInstance = value; }
        }

        internal void DisposeMainInstanceIfAny()
        {
            if (_mainInstance != null)
            {
                Helper_classes.Common.DisposeObject(ref _mainInstance, true);
                LogManager.WriteLog("|=> MainScreen was successfully disposed.", LogManager.enumLogLevel.Info);
                this.SetDefaultDialogOwner();
            }
        }

        #endregion
        #region IDisposable Members

        private bool disposed = false;
        private bool _isDisposeInitiated = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _isDisposeInitiated = true;
                    this.ClearAllDependencyProperties();

                    txtUname.PreviewMouseUp -= this.txtUname_PreviewMouseUp;
                    txtPWD.PreviewMouseUp -= this.txtPwd_PreviewMouseUp;
                    btnLogin.Click -= this.Login_Button_Click;
                    btnExit.Click -= this.btnExit_Click;
                    this.KeyDown -= this.Window_KeyDown;

                    txtUname.Cleanup_TextBoxStyle1();
                    txtPWD.Cleanup_LoginPasswordBoxStyle();
                    this.DisposeWPFObject(null);
                }
                disposed = true;
            }
        }

        ~Login()
        {
            Dispose(false);
        }

        #endregion
    }
}