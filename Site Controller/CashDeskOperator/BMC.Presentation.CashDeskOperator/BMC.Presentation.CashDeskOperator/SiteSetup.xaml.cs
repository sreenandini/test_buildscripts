using System;
using System.Collections;
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
using System.Net.NetworkInformation;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Presentation.POS.Views;
using BMC.Transport;
using System.Data;
using Microsoft.Win32;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;
using BMC.Common;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for SiteSetup.xaml
    /// </summary>
    public partial class SiteSetup : Window  
    {
        #region Declarations        
        private static SiteSetup instance = null;
        private SiteSetupConfiguration oSiteSetupConfiguration = SiteSetupConfiguration.SiteSetupConfigurationInstance;  
        SiteCheckPoints oCSitecheckpoints;     
        private string strKeyText = "";
        private string[] strListarray = null;
        private string strServiceStatus = string.Empty;
        #endregion

        #region Constructor

         public SiteSetup()
            {
                InitializeComponent();
                GetInitialSettings();
                MessageBox.parentOwner = this;
            }

         public static SiteSetup SiteSetupInstance
         {
             get
             {
                 if (instance == null)
                 {
                     instance = new SiteSetup();
                 }
                 return instance;
             }
         }

        #endregion
                
        private void ClearContents()
        {
            txtAutCode.Text="";
            txtEnterpriseURL.Text = "";
            txtSiteCode.Text = "";
            statusbaritemlblStatus.Text = "Ready";
            statusbaritempbStatus.Value= 10;   
        }

        private void StopAllServices()
        {
            StringBuilder strServicelist = new StringBuilder();
            bool bServiceStatus = false;
            //Stop all serivces before doing clearing data
            if (strListarray != null)
            {
                for (int i = 0; i < strListarray.Length; i++)
                {
                    if (strListarray[i] != null && strListarray[i] != string.Empty)
                    {
                        strServicelist.Append(strListarray[i].ToString() + ",");
                        strServiceStatus = oSiteSetupConfiguration.GetServiceStatus(strListarray[i]);
                        if (strServiceStatus.ToUpper() == "NOSERVICE")
                        {
                            LogManager.WriteLog(strListarray[i] + "Service not found", LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            bServiceStatus = oSiteSetupConfiguration.EndService(strListarray[i].ToString());
                            System.Windows.Forms.Application.DoEvents();
                            if (bServiceStatus)
                            {
                                LogManager.WriteLog("Status of  " + strListarray[i] + "is" + bServiceStatus.ToString(), LogManager.enumLogLevel.Info);
                            }
                            else
                            {
                                LogManager.WriteLog("Status of  " + strListarray[i] + "is" + bServiceStatus.ToString(), LogManager.enumLogLevel.Info);
                            }
                        }
                    }
                }
            }
        }

        public void GetInitialSettings()
        {
            DataSet objDsSetting;
            var objCdo = oCommonUtilities.CreateInstance();
            DataRow settingsRow;
            try
            {
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
                    Settings.IsAFTIncludedInCalculation = (settingsRow["IsAFTIncludedInCalculation"].ToString() != string.Empty) ? bool.Parse(settingsRow["IsAFTIncludedInCalculation"].ToString()) : false;
                    Settings.Auto_Declare_Monies = (settingsRow["Auto_Declare_Monies"].ToString() != string.Empty) ? bool.Parse(settingsRow["Auto_Declare_Monies"].ToString()) : false;
                    Settings.Disable_Machine_On_Drop = (settingsRow["DISABLE_MACHINE_ON_DROP"].ToString() != string.Empty) ? bool.Parse(settingsRow["DISABLE_MACHINE_ON_DROP"].ToString()) : false;
                    Settings.NoWaitForDisableMachine = (settingsRow["NoWaitForDisableMachine"].ToString() != string.Empty) ? bool.Parse(settingsRow["NoWaitForDisableMachine"].ToString()) : false;
                    Settings.AUTOLOGOFF_TIMEOUT = (settingsRow["AUTOLOGOFF_TIMEOUT"].ToString() != string.Empty) ? Int32.Parse(settingsRow["AUTOLOGOFF_TIMEOUT"].ToString()) : 0;
                    Settings.SHOWHANDPAYCODE = (settingsRow.Table.Columns.Contains("SHOWHANDPAYCODE")) ? Convert.ToString(settingsRow["SHOWHANDPAYCODE"]).ToUpper() : "TRUE";
                    Settings.IsEmployeeCardTrackingEnabled = (!settingsRow.IsNull("IsEmployeeCardTrackingEnabled")) ? bool.Parse(settingsRow["IsEmployeeCardTrackingEnabled"].ToString()) : false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetInitialSettings::Error in loading Intial settings from setting table", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            ExtensionMethods.CurrentSiteCulture = ConfigManager.Read("GetDefaultCultureForRegion");

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearContents();           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //this.Close(); 
            System.Windows.Forms.DialogResult dr;
            dr = MessageBox.ShowBox("MessageID18", BMC_Icon.Question, BMC_Button.YesNo);

            if (dr.ToString() == "No")
            {
                return;
            }
            else
            {
                //this.Visibility = Visibility.Hidden;
                App.Current.Shutdown();
                instance = null;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearContents();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            int iSiteCode = 0;
            string strZonesResult = string.Empty;
            string strBarPositionsResult = string.Empty;
            string strMachinesResult = string.Empty;
            string strInstallationsResult = string.Empty;
            int iAuthResult = 0;
            int  iServerResult = 0;
            btnOK.IsEnabled = false;
            btnCancel.IsEnabled = false;

            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;
                statusbaritemlblStatus.Text = "Verifying with server...";
                statusbaritempbStatus.Value = 30;
                //if (txtAutCode.Text == string.Empty || txtEnterpriseURL.Text == string.Empty || txtSiteCode.Text == string.Empty)
                //{
                if (txtAutCode.Text == string.Empty || txtSiteCode.Text == string.Empty)
                {
                    MessageBox.ShowBox("MessageID20", BMC_Icon.Warning, BMC_Button.OK);
                    txtSiteCode.Focus();
                    statusbaritempbStatus.Value = 0;
                    return;
                }
                else
                {
                    if (txtSiteCode.Text.Length == 0)
                    {
                        MessageBox.ShowBox("MessageID21", BMC_Icon.Warning, BMC_Button.OK);
                        statusbaritempbStatus.Value = 0;
                        return;
                    }

                    //if (txtEnterpriseURL.Text.Length == 0)
                    //{
                    //    MessageBox.ShowBox("MessageID22", BMC_Icon.Warning, BMC_Button.OK);
                    //    statusbaritempbStatus.Value = 0;
                    //    return;
                    //}

                    //Check Site code validation
                    iSiteCode = int.Parse(txtSiteCode.Text.ToString());

                    if (!oSiteSetupConfiguration.IsValidSiteCode(iSiteCode))
                    {
                        MessageBox.ShowBox("MessageID312", BMC_Icon.Warning, BMC_Button.OK);
                        statusbaritempbStatus.Value = 0;
                        return;
                    }
                    //Check connection established with enterprise.
                    iServerResult = oSiteSetupConfiguration.EnterpriseUrlIsExists(txtEnterpriseURL.Text.ToString(), txtSiteCode.Text.ToString());
                    if (iServerResult==-1)
                    {
                        statusbaritempbStatus.Value = 0;
                        MessageBox.ShowBox("MessageID23", BMC_Icon.Warning, BMC_Button.OK);
                        statusbaritemlblStatus.Text = "Failed";
                        return;
                    }
                    else if (iServerResult == -2)
                    {
                        statusbaritempbStatus.Value = 0;
                        MessageBox.ShowBox("MessageID203", BMC_Icon.Warning, BMC_Button.OK);
                        statusbaritemlblStatus.Text = "Failed";
                        return;
                    }
                    statusbaritempbStatus.Value = 40;

                    try
                    {
                        iAuthResult = oSiteSetupConfiguration.CheckAuthorizationCode(txtAutCode.Text.Trim(), iSiteCode,"New");
                    }
                    catch (Exception exError)
                    {
                        iAuthResult = -1;
                        ExceptionManager.Publish(exError);
                    }

                    switch (iAuthResult)
                    {
                        case -1:
                            {
                                statusbaritempbStatus.Value = 0;
                                MessageBox.ShowBox("MessageID24", BMC_Icon.Warning, BMC_Button.OK);
                                statusbaritemlblStatus.Text = "Failed";
                                return;
                            }
                        case 0:
                            {
                                statusbaritempbStatus.Value = 0;
                                MessageBox.ShowBox("MessageID25", BMC_Icon.Warning, BMC_Button.OK);
                                statusbaritemlblStatus.Text = "Failed";
                                return;
                            }
                        case 1:
                            {
                                statusbaritempbStatus.Value = 0;
                                bool bUpdateStatus = oSiteSetupConfiguration.ResetTransactionKey(txtAutCode.Text.Trim(), iSiteCode);
                                MessageBox.ShowBox("MessageID25", BMC_Icon.Warning, BMC_Button.OK);
                                statusbaritemlblStatus.Text = "Failed";
                                return;
                            }
                        case 4:
                            {
                                statusbaritempbStatus.Value = 0;
                                MessageBox.ShowBox("MessageID167", BMC_Icon.Warning, BMC_Button.OK);
                                statusbaritemlblStatus.Text = "Failed";
                                return;
                            }
                        case 6:
                            {
                                statusbaritempbStatus.Value = 0;
                                MessageBox.ShowBox("MessageID28", BMC_Icon.Warning, BMC_Button.OK);
                                statusbaritemlblStatus.Text = "Failed";
                                return;
                            }
                        case 7:
                            {
                                statusbaritempbStatus.Value = 0;
                                MessageBox.ShowBox("MessageID199", BMC_Icon.Warning, BMC_Button.OK);
                                statusbaritemlblStatus.Text = "Failed";
                                return;
                            }
                        case 8:
                            {
                                statusbaritempbStatus.Value = 0;
                                MessageBox.ShowBox("MessageID200", BMC_Icon.Warning, BMC_Button.OK);
                                statusbaritemlblStatus.Text = "Failed";
                                return;
                            }
                    }

                    ////Check for NGA Asset in Enterprise.
                    //Get list of MAC Address of the local system & Send the list to Enterprise.
                    int iNGAStatus = -1;

                    try
                    {
                        string strCheckNGAResult = oSiteSetupConfiguration.CheckNGA(GetCurentSystemMACAddress(), iSiteCode);
                        string[] strNGAResultSet = strCheckNGAResult.Split(',');


                        foreach (string strNGAResult in strNGAResultSet)
                        {
                            if (strNGAResult.Trim().Length > 0)
                            {
                                string[] strMACList = new string[2];
                                strMACList = strNGAResult.Split('/');
                                iNGAStatus = Convert.ToInt32(strMACList[1]);

                                LogManager.WriteLog("MAC Adress - " + strMACList[0] + " Result - " + strMACList[1], LogManager.enumLogLevel.Info);

                                if (iNGAStatus > 1 && iNGAStatus < 5)
                                {                                    
                                    break;
                                }
                            }
                        }

                    }
                    catch (Exception exCheckNGA)
                    {
                        ExceptionManager.Publish(exCheckNGA);
                        iNGAStatus = -1;
                    }
                    

                    //Based on the status decide course of action.
                    switch (iNGAStatus)
                    {
                        case -1:
                        case 0:
                        case 1:
                            {
                                statusbaritempbStatus.Value = 0;
                                bool bUpdateStatus = oSiteSetupConfiguration.ResetTransactionKey(txtAutCode.Text.Trim(), iSiteCode);
                                MessageBox.ShowBox("MessageID29", BMC_Icon.Warning, BMC_Button.OK);
                                statusbaritemlblStatus.Text = "Failed";
                                return;
                            }
                        case 5:
                            {
                                statusbaritempbStatus.Value = 0;
                                bool bUpdateStatus = oSiteSetupConfiguration.ResetTransactionKey(txtAutCode.Text.Trim(), iSiteCode);
                                MessageBox.ShowBox("MessageID30", BMC_Icon.Warning, BMC_Button.OK);
                                statusbaritemlblStatus.Text = "Failed";
                                return;
                            }
                       
                    }

                    //Check database is empty or not.                   
                    if (oSiteSetupConfiguration.DatabaseIsEmpty() == false)
                    {
                        statusbaritempbStatus.Value = 0;
                        System.Windows.Forms.DialogResult dr;
                        dr = MessageBox.ShowBox("MessageID31", BMC_Icon.Question, BMC_Button.YesNo);
                        if (dr.ToString().ToUpper() == "NO")
                        {
                            statusbaritempbStatus.Value = 0;
                            statusbaritemlblStatus.Text = "Failed";
                            return;
                        }
                        else
                        {
                            if (oSiteSetupConfiguration.FlattenSystem())
                            {
                                MessageBox.ShowBox("MessageID32", BMC_Icon.Information, BMC_Button.OK);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID33", BMC_Icon.Error, BMC_Button.OK);
                                this.Close();
                            }
                        }                        
                    }
                    if (ConfigManager.Read("ServicesListFromDB") != null)
                    {
                        if (ConfigManager.Read("ServicesListFromDB").ToUpper() == "TRUE")
                        {
                            strListarray = null;
                            strListarray = oSiteSetupConfiguration.GetSettingValue("ServiceNames").Split(',');
                            StopAllServices();
                        }
                    }

                    statusbaritempbStatus.Value = 60;
                    System.Threading.Thread.Sleep(5);
                    statusbaritempbStatus.Value = 100;

                    switch (iAuthResult)
                    {
                        case 2:
                            {
                                //Call Site Recovery mode
                                oCSitecheckpoints = new SiteCheckPoints("RECOVERY", iSiteCode);
                                this.Hide();
                                oCSitecheckpoints.Owner = this;
                                oCSitecheckpoints.Show();
                                break;
                            }
                        case 3:
                            {
                                //Call Auto Configure mode
                                oCSitecheckpoints = new SiteCheckPoints("NEW", iSiteCode);
                                this.Hide();
                                oCSitecheckpoints.Owner = this;
                                oCSitecheckpoints.Show();
                                break;
                            }
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                statusbaritempbStatus.Value = 0;
                statusbaritemlblStatus.Text = "Failed";
            }
            finally
            {
               Cursor = System.Windows.Input.Cursors.Arrow;
               btnOK.IsEnabled = true;
               btnCancel.IsEnabled = true;
            }
        }

        private void oCSitecheckpoints_Loaded(object sender, RoutedEventArgs e)
        {
            oCSitecheckpoints.Opacity = 1.0;
        }

        private void txtSiteCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSiteCode.Text.Trim().Length <= 0)            
            {
                MessageBox.ShowBox("MessageID34", BMC_Icon.Warning, BMC_Button.OK);
                return;
            }
        }

        private void txtSiteCode_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (!FactoryResetConstants.AllowedKeys.Contains(e.Key)) { e.Handled = true; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtEnterpriseURL_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtEnterpriseURL.Text.Trim().Length <= 0)            
            {
                MessageBox.ShowBox("MessageID35", BMC_Icon.Warning, BMC_Button.OK);
                return;
            }
        }

        private void txtAutCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAutCode.Text.Trim().Length <= 0)
            {
                MessageBox.ShowBox("MessageID36", BMC_Icon.Warning, BMC_Button.OK);        
                return;
            }
        }

        private void txtAutCode_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (!FactoryResetConstants.AllowedKeys.Contains(e.Key)) { e.Handled = true; }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtAutCode_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!BMC.Transport.Settings.OnScreenKeyboard)
                            return;
            txtAutCode.Text = DisplayKeyboard(string.Empty, string.Empty);
            txtAutCode.SelectAll();
        }

        private void txtSiteCode_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!BMC.Transport.Settings.OnScreenKeyboard)
                            return;
            txtSiteCode.Text = DisplayKeyboard(txtSiteCode.Text, string.Empty);
            txtSiteCode.SelectionStart = txtSiteCode.Text.Length;
        }

        private void txtEnterpriseURL_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!BMC.Transport.Settings.OnScreenKeyboard)
                            return;
            txtEnterpriseURL.Text = DisplayKeyboard(txtEnterpriseURL.Text, string.Empty);
            txtEnterpriseURL.SelectionStart = txtEnterpriseURL.Text.Length;
        }

        public string GetCurentSystemMACAddress()
        {
            string macList = string.Empty;

            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            if (nics == null || nics.Length < 1)
            {
                LogManager.WriteLog("  No network interfaces found.", LogManager.enumLogLevel.Info);
            }
            
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                PhysicalAddress address = adapter.GetPhysicalAddress();
                byte[] bytes = address.GetAddressBytes();
                string strMACAddress = string.Empty;

                for (int i = 0; i < bytes.Length; i++)
                {
                    // Display the physical address in hexadecimal.
                    strMACAddress = strMACAddress + bytes[i].ToString("X2");
                    // Insert a hyphen after each byte, unless we are at the end of the 
                    // address.
                    if (i != bytes.Length - 1)
                    {
                        strMACAddress = strMACAddress + "-";
                    }
                }

                if (strMACAddress.Trim().Length > 0)
                {
                    if (macList.Trim().Length > 0)
                    {
                        macList = macList + "," + strMACAddress;
                    }
                    else
                    {
                        macList = strMACAddress;
                    }
                }
            }

            LogManager.WriteLog("MAC Adress List - " + macList, LogManager.enumLogLevel.Info);
            return macList;
        }

        #region Keyboard events
        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                strKeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        private string DisplayKeyboard(string KeyText, string type)
        {
            strKeyText = "";
            KeyboardInterface objKeyboard = new KeyboardInterface();
            if (type == "Pwd")
            {
                objKeyboard.IsPwd = true;
            }
            objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
            objKeyboard.KeyString = KeyText;
            Point locationFromScreen = this.PointToScreen(new Point(0, 0));
            PresentationSource source = PresentationSource.FromVisual(this);
            System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
            objKeyboard.Top = targetPoints.Y + SiteSetup.SiteSetupInstance.Height / 2;
            objKeyboard.Left = targetPoints.X;
            objKeyboard.ShowDialogEx(this);
            return strKeyText;
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
                        this.txtEnterpriseURL.PreviewMouseUp -= (this.txtEnterpriseURL_PreviewMouseUp);
                        this.txtSiteCode.PreviewMouseUp -= (this.txtSiteCode_PreviewMouseUp);
                        this.txtSiteCode.KeyDown -= (this.txtSiteCode_KeyDown);
                        this.txtAutCode.KeyDown -= (this.txtAutCode_KeyDown);
                        this.txtAutCode.PreviewMouseUp -= (this.txtAutCode_PreviewMouseUp);
                        this.btnOK.Click -= (this.btnOK_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                        this.btnExit.Click -= (this.btnExit_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("SiteSetup objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="SiteSetup"/> is reclaimed by garbage collection.
        /// </summary>
        ~SiteSetup()
        {
            Dispose(false);
        }

        #endregion
    }
}
