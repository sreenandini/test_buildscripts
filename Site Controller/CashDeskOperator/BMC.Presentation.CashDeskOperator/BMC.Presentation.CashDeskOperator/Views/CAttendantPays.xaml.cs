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
using BMC.Common.ExceptionManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Transport;
using System.Globalization;
using BMC.Common.Utilities;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.LogManagement;
using BMC.CashDeskOperator;
using BMCIPC;
using System.Data;

using Microsoft.Win32;
using BMC.Common.ConfigurationManagement;
using BMC.Presentation.POS.Helper_classes;
using System.Threading;
using System.Windows.Threading;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CAttendantPays.xaml
    /// </summary>
    public partial class CAttendantPays : UserControl
    {
        #region "Declarations"
        TextBox txtBox;
        public IEnumerable<FillTreasuryList> treasuryList;
        public int InstallationNumber;
        public string BarPosition = string.Empty;
        int Treasury_No;
        private BMC.Presentation.POS.Views.CustomerDetails oCustomerDetails;
        private long Custid = 0;
        private bool ProcessCancelled = false;
        string Asset;
        const string TYPE = "Manual ";
        private bool IsHandpayVoid = false;
        private int temp_InstallationNumber;
        private string sPos = "";
        private string sTEid = "";
        List<BarPositions> Positions;
        IHandpay handpay;
        double? VoidAmount = 0.0;
        // RegistryKey installationPathkey;
        #endregion
        private CashDispenserWorker _worker = null;
        private System.Windows.Threading.DispatcherTimer Timer;

        public CAttendantPays()
        {
            InitializeComponent();
            if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CAttendantpay.btnVoid"))
                btnVoid.Visibility = Visibility.Hidden;

            //Cage Setting
            if (Settings.CAGE_ENABLED)
            {
                btnProcess.Visibility = Visibility.Hidden;
                btnGenerateSlipNo.Visibility = Visibility.Visible;
            }
            else
            {
                btnProcess.Visibility = Visibility.Visible;
                btnGenerateSlipNo.Visibility = Visibility.Hidden;
            }
            // Helper_classes.Common.BindListView(treasuryList, dgHandpay);
            handpay = HandpayBusinessObject.CreateInstance();
            FillTreasury(string.Empty);


            // Helper_classes.Common.BindListView(treasuryList, dgHandpay);
            handpay = HandpayBusinessObject.CreateInstance();

            this.Unloaded += new RoutedEventHandler(CAttendantPays_Unloaded);
            _worker = new CashDispenserWorker(this, ModuleName.ManualAttendantPay);
        }

        void CAttendantPays_Unloaded(object sender, RoutedEventArgs e)
        {
            Timer.Tick -= Timer_Tick;
            App.clientObj.ClientReceived -= this.clientObj_ClientReceived;
        }

        void clientObj_ClientReceived(object obj)
        {
            if (obj != null)
                if (obj is JackpotEventRequest)
                    LogManager.WriteLog((obj as JackpotEventRequest).InstallationNo.ToString() + "," + (obj as JackpotEventRequest).CardNumber.ToString(), LogManager.enumLogLevel.Info);
        }

        private void ValueCalcComp_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {

                txtBox = (TextBox)((ValueCalcComp)sender).txtDisplay;
                txtBox.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void PostHandpayEvent(Treasury objTreasury)
        {
            try
            {
                List<AssetNumberResult> lstTreasury = handpay.GetAssetNumber(objTreasury.InstallationNumber);

                BMCIPC.JackpotEventRequest request = new BMCIPC.JackpotEventRequest();

                request.CardNumber = handpay.GetEPIDetails(InstallationNumber);
                request.InstallationNo = InstallationNumber;
                request.JackpotAmount = objTreasury.TreasuryAmount * 100;

                if (App.client != null)
                    App.client.SendToServer(App.clientObj, request);
                else
                    LogManager.WriteLog("CAttendantPays PostHandpayEvent - Connection not set", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("PostHandpayEvent" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }
        string DefaultAmount()
        {
            Double DefaultAmt = 0.00;
            return DefaultAmt.ToString("N", new CultureInfo(ExtensionMethods.CurrentCurrenyCulture));
        }
        private void GenerateManulJackpot()
        {

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
                        objAuthorize.ShowDialogEx(this);
                        if (!objAuthorize.IsAuthorized)
                            return;
                    }
                    else
                    {
                        objAuthorize.IsAuthorized = true;
                    }
                }
                if ((dgHandpay.SelectedItem as FillTreasuryList).HP_Type.ToUpper() == "HANDPAY")
                {
                    HP_Type = "AttendantPay Credit";
                    jpinfo.jackpotTypeId = 4;
                }
                else if ((dgHandpay.SelectedItem as FillTreasuryList).HP_Type.ToUpper() == "JACKPOT")
                {
                    HP_Type = "AttendantPay Jackpot";
                    jpinfo.jackpotTypeId = 5;
                }
                else
                {
                    HP_Type = "AttendantPay Progressive";
                    jpinfo.jackpotTypeId = 6;
                }

                DataSet oDt = handpay.createTickeException_HandpayCAGE(int.Parse(this.BarPosition), iAmount, 0, HP_Type);
                if (Asset.IsNullOrEmpty())
                {
                    Asset = handpay.GetAssetNumber(InstallationNumber)[0].Stock_No;
                }

                jpinfo.Slot = this.BarPosition;
                jpinfo.assetConfigNumber = Asset;
                jpinfo.Denom = 0.01M;
                jpinfo.hpjpAmount = long.Parse(oDt.Tables[0].Rows[0]["TE_VALUE"].ToString());
                jpinfo.jackpotNetAmount = long.Parse(oDt.Tables[0].Rows[0]["TE_VALUE"].ToString());
                jpinfo.sequenceNumber = long.Parse(oDt.Tables[0].Rows[0]["TE_ID"].ToString());
                jpinfo.TransactionDate = oDt.Tables[0].Rows[0]["TE_Date"].ToString();
                jpinfo.UserID = objAuthorize.User.SecurityUserID.ToString();
                jpinfo.siteNo = Settings.SiteName;
                jpinfo.siteId = Convert.ToInt32(Settings.SiteCode);
                txtBox.Text = this.DefaultAmount();
                handpay.PrintSlip(jpinfo);

                //send PT 10 from client

                Treasury treasury = new Treasury();
                treasury.ActualTreasuryDate = Convert.ToDateTime(jpinfo.TransactionDate);
                treasury.Asset = Asset;
                treasury.Authorized_Date = Convert.ToDateTime(jpinfo.TransactionDate);
                treasury.AuthorizedUser_No = BMC.Security.SecurityHelper.CurrentUser.User_No;
                treasury.CustomerID = BMC.Security.SecurityHelper.CurrentUser.User_No;
                treasury.InstallationNumber = InstallationNumber;
                treasury.TreasuryAmount = iAmount;

                treasury.UserID = BMC.Security.SecurityHelper.CurrentUser.User_No;
                treasury.TreasuryType = HP_Type;

                PostHandpayEvent(treasury);
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }

        }

        private void dgHandpay_Loaded(object sender, RoutedEventArgs e)
        {
            //Helper_classes.Common.BindListView(treasuryList, dgHandpay);
            RefreshSlot();
        }

        private void dgHandpay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgHandpay.Items.Count > 0)

                    if (dgHandpay.SelectedItem != null)
                    {
                        txtAmount.Text = Convert.ToDecimal(((dgHandpay.SelectedItem as FillTreasuryList).Amount.Value)).GetUniversalCurrencyFormat();
                        if (Settings.SHOWHANDPAYCODE == "TRUE")
                        {
                            // TextBlock_11.Text = "#" + (dgHandpay.SelectedItem as FillTreasuryList).Pos + (dgHandpay.SelectedItem as FillTreasuryList).TreasuryDate.ToString().Replace("/", "").Replace(":", "").Replace("AM", "0").Replace("PM", "1").Replace(" ", "");
                            TextBlock_11.Text = "#" + (dgHandpay.SelectedItem as FillTreasuryList).Pos + Convert.ToDateTime((dgHandpay.SelectedItem as FillTreasuryList).TreasuryDate).ToString("ddMMyyyyHHmmss");
                        }
                    }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
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
                    });
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            int iResult = 0;
            LockHandler Lock = new LockHandler();
            btnProcess.IsEnabled = false;
            try
            {
                if (dgHandpay.Items.Count > 0)
                {
                    if (dgHandpay.SelectedItem != null)
                    {
                        sPos = "";
                        sTEid = "";

                        sPos = Convert.ToString((dgHandpay.SelectedItem as FillTreasuryList).Pos);
                        sTEid = Convert.ToString((dgHandpay.SelectedItem as FillTreasuryList).TE_ID);

                        /*Lock the Handpay Process Transaction*/
                        iResult = Lock.InsertLockRecord(0, "", "HANDPAY", "HP", sTEid);
                        LogManager.WriteLog("Lock Result: " + iResult.ToString(), LogManager.enumLogLevel.Debug);
                        if (iResult == 1)
                        {
                            MessageBox.ShowBox("MessageID380", BMC_Icon.Error);
                            return;
                        }

                        if (MessageBox.ShowBox("MessageID106", BMC_Icon.Question, BMC_Button.YesNo) ==
                            System.Windows.Forms.DialogResult.Yes)
                        {
                            if (Clearhandpay(Convert.ToInt32((dgHandpay.SelectedItem as FillTreasuryList).Installation_No)))
                            {
                                ProcessHandpay(false);
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID382", BMC_Icon.Information);
                            }
                        }
                        Lock.DeleteLockRecord(0, "", "HANDPAY", "HP", sTEid);
                    }
                    else
                        MessageBox.ShowBox("MessageID107", BMC_Icon.Information);
                }
                else
                    MessageBox.ShowBox("MessageID108", BMC_Icon.Information);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Process Handpay Exception", LogManager.enumLogLevel.Debug);
            }
            finally
            {

                btnProcess.IsEnabled = true;
            }
        }


        private int VoidHandpay()
        {
            try
            {
                var voidTran = new VoidTranCreate
                {
                    TreasuryID = Treasury_No.ToString(),
                    UserNo = Security.SecurityHelper.CurrentUser.User_No.ToString(),
                    Date = DateTime.Now
                };
                return handpay.VoidTransaction(voidTran);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -99;
            }
        }

        private void FillTreasury(string BarPos)
        {
            treasuryList = handpay.GetHandpays(BarPos);
        }

        private void btnVoid_Click(object sender, RoutedEventArgs e)
        {

            int iVoid;
            int iResult = 0;
            LockHandler Lock = new LockHandler();

            if (dgHandpay.Items.Count > 0)
            {
                if (dgHandpay.SelectedItem != null)
                {
                    sPos = "";
                    sTEid = "";
                    FillTreasuryList handpay_Fill = dgHandpay.SelectedItem as FillTreasuryList;
                    sPos = handpay_Fill.Pos;
                    sTEid = handpay_Fill.TE_ID.ToString();
                    iResult = Lock.InsertLockRecord(0, "", "HANDPAY", "HP", sTEid);
                    //
                    if (iResult == 1)
                    {
                        MessageBox.ShowBox("MessageID380", BMC_Icon.Error);
                        return;
                    }
                    if (MessageBox.ShowBox("MessageID109", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (Clearhandpay(Convert.ToInt32(handpay_Fill.Installation_No)))
                        {
                            IsHandpayVoid = true;
                            ProcessHandpay(true);

                            if (Treasury_No < 0 || Treasury_No == 0)
                                return;
                            iVoid = VoidHandpay();

                            try
                            {

                                Asset = handpay_Fill.Asset.ToString();
                                VoidAmount = handpay_Fill.Amount;
                                if (iVoid > 0)
                                {
                                    MessageBox.ShowBox("MessageID110", BMC_Icon.Information);

                                    // Newly Added by Venkatesh Kumar.J - SGVI Requirements
                                    if (!Settings.CAGE_ENABLED)
                                        (oCommonUtilities.CreateInstance()).PrintCommonReceipt(true, handpay_Fill.HP_Type, iVoid.ToString());
                                    //if (VoidAmount > Settings.W2GWinAmount)
                                    //{
                                    //    if (Settings.W2GMessage)
                                    //    {
                                    //        if (AppSettings.IsReceiptRequired && !Settings.CAGE_ENABLED)
                                    //        {

                                    //            (oCommonUtilities.CreateInstance()).PrintCommonReceipt(true, handpay_Fill.HP_Type, iVoid.ToString());


                                    //        }
                                    //    }
                                    //}

                                    //else
                                    //{
                                    //    if (VoidAmount <= Settings.W2GWinAmount && !Settings.CAGE_ENABLED)
                                    //    {
                                    //        (oCommonUtilities.CreateInstance()).PrintCommonReceipt(true, handpay_Fill.HP_Type, iVoid.ToString());

                                    //    }

                                    //}



                                    //if (AppSettings.IsReceiptRequired && (!Settings.CAGE_ENABLED))
                                    //{
                                    //    (oCommonUtilities.CreateInstance()).PrintCommonReceipt(true, handpay_Fill.HP_Type, iVoid.ToString());
                                    //}

                                    //-----------Untill Here-------------------------------

                                    string TreasuryDate = handpay_Fill.TreasuryDate.ToString();
                                    string Amount = handpay_Fill.Amount.ToString();
                                    string TreasuryNo = handpay_Fill.TE_ID.ToString();
                                    string TreasuryType = handpay_Fill.HP_Type;
                                    string amount = BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(Convert.ToDouble(Amount));

                                    //FillTreasury((dgHandpay.SelectedItem as FillTreasuryList).Pos);
                                    FillTreasury(string.Empty);
                                    //BindListView();
                                    // Helper_classes.Common.BindListView(treasuryList, dgHandpay);

                                    dgHandpay.ItemsSource = treasuryList;

                                    if (Settings.CAGE_ENABLED)
                                    {
                                        jackpotProcessInfoDTO jpinfo = new jackpotProcessInfoDTO();
                                        jpinfo.Slot = this.BarPosition;
                                        jpinfo.assetConfigNumber = Asset;
                                        jpinfo.Denom = 0.01M;

                                        jpinfo.jackpotTypeId = handpay_Fill.HP_Type.ToLower() == "attendantpay Credit" ? Convert.ToInt16(1) :
                                                         handpay_Fill.HP_Type.ToLower() == "attendantpay jackpot" ? Convert.ToInt16(2) :
                                                         handpay_Fill.HP_Type.ToLower() == "progressive" ? Convert.ToInt16(3) : Convert.ToInt16(1);


                                        jpinfo.Slot = handpay_Fill.Pos;
                                        jpinfo.assetConfigNumber = Asset;

                                        double value = 0;
                                        double.TryParse(Amount, out value);
                                        jpinfo.hpjpAmount = (long)(-1 * (value * 100));
                                        try { jpinfo.jackpotNetAmount = (long)value; }
                                        catch { }


                                        double.TryParse(TreasuryNo, out value);
                                        jpinfo.sequenceNumber = (long)value;

                                        jpinfo.TransactionDate = TreasuryDate;
                                        jpinfo.UserID = Security.SecurityHelper.CurrentUser.SecurityUserID.ToString();
                                        jpinfo.siteNo = Settings.SiteName;
                                        jpinfo.siteId = Convert.ToInt32(Settings.SiteCode);
                                        //Commented for not printing in cage slip format

                                        handpay.PrintSlip(jpinfo);
                                    }
                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {

                                        AuditModuleName = ModuleName.Void,
                                        Audit_Screen_Name = "Postion Details|Attendant Pay|Void",
                                        Audit_Desc = TreasuryType + " Date: " + TreasuryDate + " Amount:" + amount,
                                        AuditOperationType = OperationType.ADD,
                                        Audit_Field = "Treasury Number",
                                        Audit_New_Vl = TreasuryNo,
                                        Audit_Slot = Asset
                                    });
                                    //BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(0.00)
                                }
                                else if (iVoid == -1)
                                    MessageBox.ShowBox("MessageID111", BMC_Icon.Information);
                                else
                                {
                                    MessageBox.ShowBox("MessageID113", BMC_Icon.Error);

                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        AuditModuleName = ModuleName.Void,
                                        Audit_Screen_Name = "Postion Details|Attendant Pay|Void",
                                        Audit_Desc = "Error occured while voiding this transaction.",
                                        AuditOperationType = OperationType.ADD,
                                        Audit_Slot = Asset
                                    });
                                }
                            }
                            catch (Exception Ex)
                            {
                                ExceptionManager.Publish(Ex);
                                MessageBox.ShowBox("MessageID113", BMC_Icon.Error);
                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {
                                    AuditModuleName = ModuleName.Void,
                                    Audit_Screen_Name = "Postion Details|Attendant Pay|Void",
                                    Audit_Desc = "Error occured while voiding this transaction.",
                                    AuditOperationType = OperationType.ADD,
                                    Audit_Slot = Asset
                                });
                            }
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID382", BMC_Icon.Information);
                        }
                    }
                    Lock.DeleteLockRecord(0, "", "HANDPAY", "HP", sTEid);
                }
                else
                    MessageBox.ShowBox("MessageID114", BMC_Icon.Information);
            }
            else
                MessageBox.ShowBox("MessageID115", BMC_Icon.Information);
        }

        private void ProcessHandpay(bool IsVoid)
        {
            int Auth_User_ID = 0;
            Window Owner;
            double amount = 0;
            CAuthorize objAuthorize = null;
            Treasury treasury = null;
            try
            {
                //
                if (!IsHandpayVoid)
                {
                    if ((dgHandpay.SelectedItem as FillTreasuryList).Amount != null && (double)(dgHandpay.SelectedItem as FillTreasuryList).Amount > 0)
                    {
                        //double.TryParse((dgHandpay.SelectedItem as FillTreasuryList).Amount.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out amount);
                        //------->
                        // Issue fix for ->In ITALY environment if user currency setting is set as italy then while processing handpay amount 
                        // of 99,36 customer information screen is displaying. (If we set user currency setting as US or UK then 
                        // we are able to process handpays properly)
                        //<--------
                        double.TryParse((dgHandpay.SelectedItem as FillTreasuryList).Amount.ToString(), out amount);
                    }

                    //Code to check if the selected attendantpay is already processed.


                    if (handpay.CheckIfHandpayProcessed(dgHandpay.SelectedItem as FillTreasuryList))
                    {
                        MessageBox.ShowBox("MessageID405", BMC_Icon.Information);
                        return;
                    }


                    if (Settings.W2GMessage)
                    {
                        if (amount > Settings.W2GWinAmount)
                        {
                            MessageBox.ShowBox("MessageID367", BMC_Icon.Information);
                            if (!Settings.ProcessW2GAmount)
                            {
                                MessageBox.ShowBox("MessageID531", BMC_Icon.Information);
                                return;
                            }
                        }
                    }
                    Auth_User_ID = Security.SecurityHelper.CurrentUser.User_No;
                    if (Settings.Client != null && Settings.Client.ToLower() == "winchells"
                        && Settings.MaxHandPayAuthRequired
                        && (dgHandpay.SelectedItem as FillTreasuryList).Amount != null
                        && (amount > Settings.HandpayPayoutCustomer_Max))
                    {
                        objAuthorize = new CAuthorize("CashdeskOperator.Authorize.cs.MaxHandpay");
                        objAuthorize.User = Security.SecurityHelper.CurrentUser;
                        objAuthorize.User.User_No = Security.SecurityHelper.CurrentUser.SecurityUserID;
                        if (!Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MaxHandpay"))
                        {

                            objAuthorize.ShowDialogEx(this);
                            if (!objAuthorize.IsAuthorized)
                                return;
                            else
                                Auth_User_ID = handpay.GetUserID(objAuthorize.User.SecurityUserID);

                        }
                        else
                        {
                            objAuthorize.IsAuthorized = true;
                        }
                    }

                    if (Settings.RegulatoryEnabled == true && Settings.RegulatoryType == "AAMS")
                    {
                        if ((dgHandpay.SelectedItem as FillTreasuryList).Amount != null)
                        {
                            Custid = 0;
                            ProcessCancelled = false;
                            if (amount >= Settings.HandpayPayoutCustomer_Min && amount <= Settings.HandpayPayoutCustomer_Max)
                            {
                                oCustomerDetails = new BMC.Presentation.POS.Views.CustomerDetails();
                                oCustomerDetails.delCustomerUpdated += new BMC.Presentation.POS.Views.CustomerDetails.CustomerUpdateHandler(delCustomerUpdated);
                                oCustomerDetails.delCustomerCancelled += new BMC.Presentation.POS.Views.CustomerDetails.CustomerCancelHandler(delCustomerCancelled);
                                Owner = Window.GetWindow(this);
                                oCustomerDetails.ShowDialog();
                            }
                            else if (amount >= Settings.HandpayPayoutCustomer_BankAccNo)
                            {
                                oCustomerDetails = new BMC.Presentation.POS.Views.CustomerDetails(true);
                                oCustomerDetails.delCustomerUpdated += new BMC.Presentation.POS.Views.CustomerDetails.CustomerUpdateHandler(delCustomerUpdated);
                                oCustomerDetails.delCustomerCancelled += new BMC.Presentation.POS.Views.CustomerDetails.CustomerCancelHandler(delCustomerCancelled);
                                Owner = Window.GetWindow(this);
                                oCustomerDetails.ShowDialog();
                            }
                            else if (amount >= Settings.HandpayPayoutCustomer_Max && amount <= Settings.HandpayPayoutCustomer_BankAccNo)
                            {
                                oCustomerDetails = new BMC.Presentation.POS.Views.CustomerDetails();
                                oCustomerDetails.delCustomerUpdated += new BMC.Presentation.POS.Views.CustomerDetails.CustomerUpdateHandler(delCustomerUpdated);
                                oCustomerDetails.delCustomerCancelled += new BMC.Presentation.POS.Views.CustomerDetails.CustomerCancelHandler(delCustomerCancelled);
                                Owner = Window.GetWindow(this);
                                oCustomerDetails.ShowDialog();
                            }
                        }
                        if (ProcessCancelled)
                            return;
                    }
                }

                int TE_ID = (dgHandpay.SelectedItem as FillTreasuryList).TE_ID;
                treasury = new Treasury
                {
                    InstallationNumber = (dgHandpay.SelectedItem as FillTreasuryList).Installation_No,
                    TreasuryType = (dgHandpay.SelectedItem as FillTreasuryList).HP_Type,
                    TreasuryAmount = (double)(dgHandpay.SelectedItem as FillTreasuryList).Amount,
                    TreasuryTemp = IsVoid ? true : false,
                    ActualTreasuryDate =
                        (DateTime)((dgHandpay.SelectedItem as FillTreasuryList).TreasuryDate)
                };

                //temp_InstallationNumber = (dgHandpay.SelectedItem as FillTreasuryList).Installation_No;

                treasury.CustomerID = Custid;
                treasury.UserID = Auth_User_ID;

                treasury.Authorized_Date = DateTime.Now.DBMinValue();

                if (objAuthorize != null && objAuthorize.IsAuthorized)
                {
                    treasury.AuthorizedUser_No = objAuthorize.User.SecurityUserID;
                    treasury.Authorized_Date = DateTime.Now;

                    //Audit for authorization
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.ManualAttendantPay,
                        Audit_Screen_Name = "PositionDetails|ManualAttendantPay",
                        Audit_Desc = "Manual AttendantPay Type-" + treasury.TreasuryType,
                        AuditOperationType = OperationType.ADD,
                        Audit_Field = "AuthorizedUser_No",
                        Audit_New_Vl = objAuthorize.User.SecurityUserID.ToString(),
                        Audit_Slot = (dgHandpay.SelectedItem as FillTreasuryList).Asset
                    });

                }
                Treasury_No = handpay.ProcessHandPay(treasury, TE_ID);

                if (Treasury_No > 0)
                {
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.AttendantPay,
                        Audit_Screen_Name = "PositionDetails|AttendantPay",
                        Audit_Desc = "AttendantPay Type-" + treasury.TreasuryType,
                        AuditOperationType = OperationType.ADD,
                        Audit_Field = "Treasury Amount",
                        Audit_New_Vl = String.Format("{0:0.00}", treasury.TreasuryAmount),
                        Audit_Slot = (dgHandpay.SelectedItem as FillTreasuryList).Asset
                    });

                    txtAmount.Text = "";

                    if (!IsVoid)
                    {

                        #region GCD
                        if (Settings.IsGloryCDEnabled && Settings.CashDispenserEnabled)
                        {
                            LoadingWindow ld = new LoadingWindow(this, ModuleName.AttendantPay, Treasury_No.ToString(), sPos, Convert.ToInt32(treasury.TreasuryAmount * 100));
                            ld.Topmost = true;
                            ld.ShowDialogEx(this);
                            BMC.Business.CashDeskOperator.Result res = ld.Result;
                            if (res.IsSuccess && (Treasury_No > 0))
                            {
                                LogManager.WriteLog(string.Format("Cash Dispensed Successfully - Treasury Amount: {0:0.00}", treasury.TreasuryAmount), LogManager.enumLogLevel.Info);
                                LogManager.WriteLog("Export HandPay Details to Enterprise", LogManager.enumLogLevel.Info);
                                handpay.ExportHandPay(Treasury_No);

                                BMC.Presentation.MessageBox.ShowBox(res.error.Message, res.error.MessageType.Equals("Error") ? BMC_Icon.Error : BMC_Icon.Information, true);

                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {

                                    AuditModuleName = ModuleName.AttendantPay,
                                    Audit_Screen_Name = "PositionDetails|AttendantPay|HandPay",
                                    Audit_Desc = "HandPay Succeed",
                                    AuditOperationType = OperationType.ADD,
                                    Audit_Old_Vl = "Ticket_ExceptionID:" + TE_ID + ";TreasuryNo:" + Treasury_No + ";",

                                });

                            }
                            else
                            {
                                BMC.Presentation.MessageBox.ShowBox(res.error.Message, res.error.MessageType.Equals("Error") ? BMC_Icon.Error : BMC_Icon.Information, true);
                                LogManager.WriteLog(string.Format("Unable to Dispense Cash - Treasury Amount: {0:0.00}", treasury.TreasuryAmount), LogManager.enumLogLevel.Info);
                                LogManager.WriteLog("Rollback HandPay Process", LogManager.enumLogLevel.Info);
                                handpay.RollbackHandPay(TE_ID, Treasury_No);
                                MessageBox.ShowBox("MessageID117", BMC_Icon.Error);
                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {

                                    AuditModuleName = ModuleName.AttendantPay,
                                    Audit_Screen_Name = "PositionDetails|AttendantPay",
                                    Audit_Desc = treasury.TreasuryType + " processing was not completed.",
                                    AuditOperationType = OperationType.ADD,
                                    Audit_Slot = (dgHandpay.SelectedItem as FillTreasuryList).Asset
                                });
                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {
                                    AuditModuleName = ModuleName.AttendantPay,
                                    Audit_Screen_Name = "PositionDetails|AttendantPay|HandPay Process Failed",
                                    Audit_Desc = "Rollback HandPay Process Voucher due to cash dispenser error",
                                    AuditOperationType = OperationType.MODIFY,
                                    Audit_Old_Vl = "Ticket_ExceptionID:" + TE_ID + ";TreasuryNo:" + Treasury_No + ";"
                                });
                            }
                        }
                        else
                        {
                            this.ProcessCashDispense("AttendantPay Type", treasury.TreasuryType, Convert.ToDecimal(treasury.TreasuryAmount));
                            MessageBox.ShowBox("MessageID116", BMC_Icon.Information);
                        }
                        #endregion


                        // FillTreasury(sPos);
                        treasuryList = handpay.GetHandpays(string.Empty);
                        //Helper_classes.Common.BindListView(treasuryList, dgHandpay);                        

                        dgHandpay.ItemsSource = treasuryList;

                        dgHandpay.Items.Refresh();

                        // Newly Added by Venkatesh Kumar.J - As per SGVI Requirements--------

                        if (AppSettings.IsReceiptRequired)
                        {
                            if (objAuthorize != null && objAuthorize.User != null)
                                (oCommonUtilities.CreateInstance()).PrintCommonReceipt(false, treasury.TreasuryType, Treasury_No.ToString(), objAuthorize.User);
                            else
                                (oCommonUtilities.CreateInstance()).PrintCommonReceipt(false, treasury.TreasuryType, Treasury_No.ToString());
                        }

                        //------------------************************************---------------
                        LogManager.WriteLog("Binding of Hand Pay completed for the pos : " + sPos, LogManager.enumLogLevel.Info);

                    }

                }
                else
                {

                    switch (Treasury_No)
                    {

                        case -2://LockExists
                        case -3://LockError
                            {
                                MessageBox.ShowBox("MessageID373", BMC_Icon.Error);

                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {

                                    AuditModuleName = ModuleName.AttendantPay,
                                    Audit_Screen_Name = "PositionDetails|AttendantPay",
                                    Audit_Desc = treasury.TreasuryType + " has been locked by another user for processing.",
                                    AuditOperationType = OperationType.ADD,
                                    Audit_Slot = (dgHandpay.SelectedItem as FillTreasuryList).Asset
                                });

                                break;
                            }
                        case -4://DatabaseError
                            {
                                MessageBox.ShowBox("MessageID374", BMC_Icon.Error);

                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {

                                    AuditModuleName = ModuleName.AttendantPay,
                                    Audit_Screen_Name = "PositionDetails|AttendantPay",
                                    Audit_Desc = treasury.TreasuryType + " -Unable to Access the database.",
                                    AuditOperationType = OperationType.ADD,
                                    Audit_Slot = (dgHandpay.SelectedItem as FillTreasuryList).Asset
                                });
                                break;
                            }
                        default:
                            {
                                MessageBox.ShowBox("MessageID117", BMC_Icon.Error);

                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {

                                    AuditModuleName = ModuleName.AttendantPay,
                                    Audit_Screen_Name = "PositionDetails|AttendantPay",
                                    Audit_Desc = treasury.TreasuryType + " processing was not completed.",
                                    AuditOperationType = OperationType.ADD,
                                    Audit_Slot = (dgHandpay.SelectedItem as FillTreasuryList).Asset
                                });
                                break;
                            }
                    }


                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                MessageBox.ShowBox("MessageID117", BMC_Icon.Error);

                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {

                    AuditModuleName = ModuleName.AttendantPay,
                    Audit_Screen_Name = "PositionDetails|AttendantPay",
                    Audit_Desc = treasury.TreasuryType + " processing was not completed.",
                    AuditOperationType = OperationType.ADD,
                    Audit_Slot = (handpay.GetAssetNumber(InstallationNumber)[0] as AssetNumberResult).Stock_No
                });
            }
            finally
            {
                IsHandpayVoid = false;
                //Send PT 10 to CMP
                if (treasury != null)
                {
                    InstallationNumber = treasury.InstallationNumber;
                    //InstallationNumber = (dgHandpay.SelectedItem as FillTreasuryList).Installation_No;
                    PostHandpayEvent(treasury);
                }
            }


        }
        private bool Clearhandpay(int InstallationNo)
        {
            try
            {
                if (!Settings.ClearHandpayTilt)
                {
                    LogManager.WriteLog("Clearhandpay Skipping as [Settings.ClearHandpayTilt=False]", LogManager.enumLogLevel.Debug);
                    return true;
                }
                LogManager.WriteLog("Clearhandpay Entered", LogManager.enumLogLevel.Debug);
                int iResult;
                var installationDataContext = new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                List<FloorStatusData> barPositions = new List<FloorStatusData>();
                barPositions = installationDataContext.GetSlotStatus("", InstallationNo);
                foreach (var barPostion in barPositions)
                {
                    if ((SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), barPostion.Slot_Status) == SlotMachineStatus.ForceFinalCollection)
                    {
                        LogManager.WriteLog("Clearhandpay: ForceFinalCollection", LogManager.enumLogLevel.Debug);
                        return true;
                    }
                }
                IHandpay objCashDeskOperator = HandpayBusinessObject.CreateInstance();
                iResult = objCashDeskOperator.Clearhandpay(InstallationNo);
                LogManager.WriteLog("Clearhandpay InstallationNo: " + InstallationNo.ToString() + " Result:" + iResult.ToString(), LogManager.enumLogLevel.Debug);
                if (iResult != 0)
                {
                    if (MessageBox.ShowBox("MessageID384", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        LogManager.WriteLog("Clearhandpay: Handpay Clear Failure.Process Cancelled.", LogManager.enumLogLevel.Debug);
                        return false;
                    }
                    else
                    {
                        LogManager.WriteLog("Clearhandpay: Ignore Handpay Clear Failure.", LogManager.enumLogLevel.Debug);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Clearhandpay Exception", LogManager.enumLogLevel.Debug);
                return false;
            }
            return true;
        }
        private void btnGenerateSlipNo_Click(object sender, RoutedEventArgs e)
        {
            int AuthUserId = 0;
            int Result = 0;
            try
            {
                if (dgHandpay.Items.Count > 0)
                {
                    LogManager.WriteLog("Generating Slip", LogManager.enumLogLevel.Debug);
                    if (Clearhandpay(Convert.ToInt32((dgHandpay.SelectedItem as FillTreasuryList).Installation_No)))
                    {
                        jackpotProcessInfoDTO jpinfo = new jackpotProcessInfoDTO();
                        CAuthorize objAuthorize = null;
                        int TE_ID = (dgHandpay.SelectedItem as FillTreasuryList).TE_ID;
                        Treasury treasury = new Treasury
                        {
                            InstallationNumber =
                                (dgHandpay.SelectedItem as FillTreasuryList).Installation_No,
                            TreasuryType = (dgHandpay.SelectedItem as FillTreasuryList).HP_Type,
                            TreasuryAmount = (double)(dgHandpay.SelectedItem as FillTreasuryList).Amount,
                            TreasuryTemp = false,
                            ActualTreasuryDate =
                                (DateTime)((dgHandpay.SelectedItem as FillTreasuryList).TreasuryDate)
                        };
                        AuthUserId = Security.SecurityHelper.CurrentUser.User_No;


                        if (treasury.TreasuryAmount > Settings.HandpayPayoutCustomer_Max)
                        {
                            objAuthorize = new CAuthorize("CashdeskOperator.Authorize.cs.MaxHandpay");
                            objAuthorize.User = Security.SecurityHelper.CurrentUser;

                            if (!Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MaxHandpay"))
                            {
                                objAuthorize.ShowDialogEx(this);
                                if (!objAuthorize.IsAuthorized)
                                    return;
                                else
                                    AuthUserId = handpay.GetUserID(objAuthorize.User.SecurityUserID);
                            }
                            else
                            {
                                objAuthorize.IsAuthorized = true;
                            }
                        }

                        treasury.CustomerID = Custid;
                        treasury.UserID = AuthUserId;
                        treasury.Authorized_Date = DateTime.Now.DBMinValue();

                        jpinfo.Slot = (dgHandpay.SelectedItem as FillTreasuryList).Pos;
                        jpinfo.assetConfigNumber = (dgHandpay.SelectedItem as FillTreasuryList).Asset;
                        jpinfo.Denom = 0.01M;
                        jpinfo.hpjpAmount = Convert.ToInt32((dgHandpay.SelectedItem as FillTreasuryList).Amount * 100);
                        jpinfo.jackpotNetAmount = Convert.ToInt32((dgHandpay.SelectedItem as FillTreasuryList).Amount);
                        jpinfo.jackpotTypeId = (dgHandpay.SelectedItem as FillTreasuryList).HP_Type.ToLower() == "attendantpay Credit" ? Convert.ToInt16(1) :
                            (dgHandpay.SelectedItem as FillTreasuryList).HP_Type.ToLower() == "attendantpay jackpot" ? Convert.ToInt16(2) :
                              (dgHandpay.SelectedItem as FillTreasuryList).HP_Type.ToLower() == "progressive" ? Convert.ToInt16(3) : Convert.ToInt16(1);



                        jpinfo.sequenceNumber = (dgHandpay.SelectedItem as FillTreasuryList).TE_ID;
                        jpinfo.TransactionDate = (dgHandpay.SelectedItem as FillTreasuryList).TreasuryDate.ToString();
                        jpinfo.UserID = Security.SecurityHelper.CurrentUser.SecurityUserID.ToString();
                        jpinfo.siteNo = Settings.SiteName;
                        jpinfo.siteId = Convert.ToInt32(Settings.SiteCode);

                        handpay.PrintSlip(jpinfo);
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID382", BMC_Icon.Information);
                    }

                }
                else
                    MessageBox.ShowBox("MessageID383", BMC_Icon.Information);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
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

        public void RefreshSlot()
        {
            try
            {
                int oldIndex = dgHandpay.SelectedIndex;
                FillTreasury(string.Empty);
                //Helper_classes.Common.BindListView(treasuryList, dgHandpay);

                dgHandpay.ItemsSource = treasuryList;
                dgHandpay.SelectedIndex = ((oldIndex >= 0 && oldIndex < dgHandpay.Items.Count) ? oldIndex : 0);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            try
            {
                timer.IsEnabled = false;
                RefreshSlot();
                //RefreshButtons(ucSlotMachine.Status);
                Thread.Sleep(100);
            }
            finally
            {
                timer.IsEnabled = true;
            }
        }

        private void AttendantPay_Loaded(object sender, RoutedEventArgs e)
        {
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, Int32.Parse(ConfigManager.Read("RefreshTime").ToString()));
            Timer.IsEnabled = true;
            Timer.Tick += Timer_Tick;
            Timer.Start();
            LogManager.WriteLog("AttendantPay_Loaded Timer started", LogManager.enumLogLevel.Info);
        }

    }
}
