using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.Common.ConfigurationManagement;
using BMC.CashDeskOperator.BusinessObjects;


    public class SettingInitializer
    {
    

        private SettingInitializer()
        {
        }
        public static void  Initialize()
        {
            InitializeSettings();
        }
        private static void InitializeSettings()
        {
     
            try
            {
              
                    DataSet objDsSetting;
                    oCommonUtilities  objCdo = oCommonUtilities.CreateInstance();
                    DataRow settingsRow;
                    objDsSetting = objCdo.GetInitialSettings();
                    if (objDsSetting != null && objDsSetting.Tables.Count > 0 && objDsSetting.Tables[0].Rows.Count > 0)
                    {
                        settingsRow = objDsSetting.Tables[0].Rows[0];
                        Settings.Region = settingsRow["REGION"].ToString();
                        Settings.Connection = settingsRow["Connection"].ToString();
                        Settings.HeadCashierSig = settingsRow["TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG"].ToString();
                        Settings.AllowOffLineRedeem = (settingsRow["Allow_Offline_Redeem"].ToString() != string.Empty) ? bool.Parse(settingsRow["Allow_Offline_Redeem"].ToString()) : false;
                        Settings.VoidTransaction = (settingsRow["VoidTransactions"].ToString() != string.Empty) ? bool.Parse(settingsRow["VoidTransactions"].ToString()) : false;
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
                        Settings.VoucherPrinterName = (!settingsRow.IsNull("Voucher_Printer_Name")) ? settingsRow["Voucher_Printer_Name"].ToString() : string.Empty;
                        Settings.CAGE_ALLOWCASHIERLOCONTKTS=(settingsRow.Table.Columns.Contains("CAGE_ALLOWCASHIERLOCONTKTS"))?bool.Parse(settingsRow["CAGE_ALLOWCASHIERLOCONTKTS"].ToString()):false;  
                        Settings.CAGE_ALLOWPRINTTKTOVERRIDE=(settingsRow.Table.Columns.Contains("CAGE_ALLOWPRINTTKTOVERRIDE"))?bool.Parse(settingsRow["CAGE_ALLOWPRINTTKTOVERRIDE"].ToString()):false;  
                        Settings.CAGE_TKTPRINTERENABLED=(settingsRow.Table.Columns.Contains("CAGE_TKTPRINTERENABLED"))?bool.Parse(settingsRow["CAGE_TKTPRINTERENABLED"].ToString()):false;  
                        Settings.CAGE_MINTKTPRINTAMTFOREMP=(settingsRow.Table.Columns.Contains("CAGE_MINTKTPRINTAMTFOREMP"))?int.Parse(settingsRow["CAGE_MINTKTPRINTAMTFOREMP"].ToString()):0;  
                        Settings.CAGE_MAXTKTREDEMPTIONAMTFOREMP=(settingsRow.Table.Columns.Contains("CAGE_MAXTKTREDEMPTIONAMTFOREMP"))?int.Parse(settingsRow["CAGE_MAXTKTREDEMPTIONAMTFOREMP"].ToString()):0;  
                        Settings.CAGE_MAXTKTREDEMPTIONAMT=(settingsRow.Table.Columns.Contains("CAGE_MAXTKTREDEMPTIONAMT"))?int.Parse(settingsRow["CAGE_MAXTKTREDEMPTIONAMT"].ToString()):0;  
                        Settings.CAGE_MAXNOOFTKTPRINTLIMIT=(settingsRow.Table.Columns.Contains("CAGE_MAXNOOFTKTPRINTLIMIT"))?int.Parse(settingsRow["CAGE_MAXNOOFTKTPRINTLIMIT"].ToString()):0;  
                        Settings.CAGE_MAXDAILYCASHIERGENTKTAMT=(settingsRow.Table.Columns.Contains("CAGE_MAXDAILYCASHIERGENTKTAMT"))?int.Parse(settingsRow["CAGE_MAXDAILYCASHIERGENTKTAMT"].ToString()):0;  
                        Settings.CAGE_MAXTKTPRINTAMTFOREMP=(settingsRow.Table.Columns.Contains("CAGE_MAXTKTPRINTAMTFOREMP"))?int.Parse(settingsRow["CAGE_MAXTKTPRINTAMTFOREMP"].ToString()):0;
                        Settings.CAGE_ENABLED = (settingsRow.Table.Columns.Contains("CAGE_ENABLED")) ? bool.Parse(settingsRow["CAGE_ENABLED"].ToString()) : false;  
                        ExtensionMethods.CurrentSiteCulture = ConfigManager.Read("GetDefaultCultureForRegion");
                    }
                }
            catch (Exception ex)
            {

                LogManager.WriteLog("GetInitialSettings::Error in loading Intial settings from setting table", LogManager.enumLogLevel.Error);
                
                ExceptionManager.Publish(ex);
            }



        }
    }

