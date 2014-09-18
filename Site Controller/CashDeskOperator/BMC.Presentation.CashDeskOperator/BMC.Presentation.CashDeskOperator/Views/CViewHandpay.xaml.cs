using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.Utilities;
using BMC.Transport;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Helper_classes;
using Audit.BusinessClasses;
using Audit.Transport;
using System.Globalization;
using Microsoft.Win32;
using System.Messaging;
using System.Data;
using BMC.CashDeskOperator;
using System.Net;
using System.Data.Linq;
using System.Linq;
using BMCIPC;
using BMC.Security;
using BMC.Common.ConfigurationManagement;
using BMC.Business.CashDeskOperator;


namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CViewHandpay.xaml
    /// </summary>
    public partial class CViewHandpay : IDisposable
    {
        public delegate void ScannerClick(object sender, RoutedEventArgs e);
        public IHandpay handpay = HandpayBusinessObject.CreateInstance();
        public IEnumerable<FillTreasuryList> treasuryList;
        public int InstallationNumber;
        public string BarPosition = string.Empty;
        TextBox txtBox;
        int Treasury_No;
        private BMC.Presentation.POS.Views.CustomerDetails oCustomerDetails;
        private long Custid = 0;
        private bool ProcessCancelled = false;
        string Asset;
        const string TYPE = "Manual ";
        private bool IsHandpayVoid = false;
        private string sPos = "";
        private string sTEid = "";
        private CashDispenserWorker _worker = null;
        //RegistryKey installationPathkey;
        bool IsProcessed = false;
        double? VoidAmount = 0.0;
        private static int iSelecteedHandPay = 0;
        bool isProcessCompleted = true;
        string TreasuryType1 = string.Empty;
        private CViewHandpay()
        {
            InitializeComponent();
            this.ucValueCalcComp.MaxLength = 9;
            optHandpay.IsChecked = true;
            //Cage Setting
            if ((Settings.CAGE_ENABLED))
            {
                btnProcess.Visibility = Visibility.Hidden;
                //btnVoid.Visibility = Visibility.Hidden;
                btnGenerateSlipNo.Visibility = Visibility.Visible;
                btnSave.Content = Application.Current.FindResource("CViewHandpay_xaml_btnGenerateSlipNo");
            }
            else
            {
                if (Security.SecurityHelper.HasAccess("BMC.Presentation.CViewHandpay.btnProcess"))
                    btnProcess.Visibility = Visibility.Visible;
                else
                    btnProcess.Visibility = Visibility.Hidden;
                //btnVoid.Visibility = Visibility.Visible;
                btnGenerateSlipNo.Visibility = Visibility.Hidden;
            }
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            //installationPathkey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("ExchangeClientInstallationPath"));
            if (!Settings.IsGloryCDEnabled)
            {
                this.InitializeCashDispenser();
            }

            this.ucValueCalcComp.EnterClicked += (btnSave_Click);
        }

        public CViewHandpay(string BarPos, ICashDispenserStatusParent parent)
        {
            InitializeComponent();

            if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CViewHandpay.btnManual") || !Settings.HandpayManual)
                btnManual.Visibility = Visibility.Hidden;

            if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CViewHandpay.btnVoid"))
                btnVoid.Visibility = Visibility.Hidden;

            //ucValueCalcComp.isCurrencyPad = true;

            this.BarPosition = BarPos;
            FillTreasury(BarPos);
            this.ucValueCalcComp.MaxLength = 9;
            optHandpay.IsChecked = true;

            //CR#-169567 - Modified By - Durga - Modified On-26th June 2013

            //Not able to give access for an User to 'Process' the Attendant Pays (From Machine) without rights to Process Manual Attendant Pays.
            //Cage Setting
            if ((Settings.CAGE_ENABLED))
            {
                btnProcess.Visibility = Visibility.Hidden;

                btnGenerateSlipNo.Visibility = Visibility.Visible;
                btnSave.Content = Application.Current.FindResource("CViewHandpay_xaml_btnGenerateSlipNo");
            }
            else
            {
                if (Security.SecurityHelper.HasAccess("BMC.Presentation.CViewHandpay.btnProcess"))
                    btnProcess.Visibility = Visibility.Visible;
                else
                    btnProcess.Visibility = Visibility.Hidden;

                btnGenerateSlipNo.Visibility = Visibility.Hidden;
            }
            dispenserStatus.StatusParent = parent;
            if (!Settings.IsGloryCDEnabled)
            {
                this.InitializeCashDispenser();
            }
            else
            {
                dispenserStatus.Visibility = Visibility.Hidden;
            }


            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            //installationPathkey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("ExchangeClientInstallationPath"));
            this.ucValueCalcComp.EnterClicked += (btnSave_Click);
        }

        /// <summary>
        /// Initializes the cash dispenser.
        /// </summary>
        private void InitializeCashDispenser()
        {
            _worker = new CashDispenserWorker(this, ModuleName.ManualAttendantPay);
            this.dispenserStatus.Visibility = CashDispenserWorker.Visibliity;
        }

        /// <summary>
        /// Processes the cash dispense.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private void ProcessCashDispense(string fieldName, string fieldValue, decimal amount)
        {
            try
            {
                _worker.Dispense(fieldName, fieldValue, amount, () =>
                {
                    this.dispenserStatus.LoadItemsAysnc();
                });
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
                if (lstHandpay.Items.Count > 0)
                {
                    if (lstHandpay.SelectedItem != null)
                    {
                        sPos = "";
                        sTEid = "";

                        sPos = Convert.ToString((lstHandpay.SelectedItem as FillTreasuryList).Pos);
                        sTEid = Convert.ToString((lstHandpay.SelectedItem as FillTreasuryList).TE_ID);

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
                            btnProcess.IsEnabled = false;
                            if (Clearhandpay(Convert.ToInt32((lstHandpay.SelectedItem as FillTreasuryList).Installation_No)))
                            {
                                ProcessHandpay(false);

                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID382", BMC_Icon.Information);
                            }
                        }
                        Lock.DeleteLockRecord(0, "", "HANDPAY", "HP", sTEid);
                        // btnProcess.IsEnabled = true;
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
        private void btnVoid_Click(object sender, RoutedEventArgs e)
        {
            int iVoid;
            int iResult = 0;
            LockHandler Lock = new LockHandler();

            if (lstHandpay.Items.Count > 0)
            {
                if (lstHandpay.SelectedItem != null)
                {
                    sPos = "";
                    sTEid = "";
                    sPos = Convert.ToString((lstHandpay.SelectedItem as FillTreasuryList).Pos);
                    sTEid = Convert.ToString((lstHandpay.SelectedItem as FillTreasuryList).TE_ID);
                    iResult = Lock.InsertLockRecord(0, "", "HANDPAY", "HP", sTEid);
                    //
                    if (iResult == 1)
                    {
                        MessageBox.ShowBox("MessageID380", BMC_Icon.Error);
                        return;
                    }
                    if (MessageBox.ShowBox("MessageID109", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (Clearhandpay(Convert.ToInt32((lstHandpay.SelectedItem as FillTreasuryList).Installation_No)))
                        {
                            IsHandpayVoid = true;
                            ProcessHandpay(true);
                            if (Treasury_No < 0 || Treasury_No == 0)
                                return;
                            iVoid = VoidHandpay();

                            try
                            {

                                string TreasuryDate = (lstHandpay.SelectedItem as FillTreasuryList).TreasuryDate.ToString();
                                string Amount = (lstHandpay.SelectedItem as FillTreasuryList).Amount.ToString();
                                string TreasuryNo = (lstHandpay.SelectedItem as FillTreasuryList).TE_ID.ToString();
                                string TreasuryType = (lstHandpay.SelectedItem as FillTreasuryList).HP_Type;
                                string amount = BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(Convert.ToDouble(Amount));
                                string Position = (lstHandpay.SelectedItem as FillTreasuryList).Pos.ToString();

                                Asset = (lstHandpay.SelectedItem as FillTreasuryList).Asset.ToString();
                                VoidAmount = (lstHandpay.SelectedItem as FillTreasuryList).Amount;

                                if (iVoid > 0)
                                {
                                    MessageBox.ShowBox("MessageID110", BMC_Icon.Information);
                                    if (!Settings.CAGE_ENABLED)
                                    {
                                        oCommonUtilities objCommUtil = oCommonUtilities.CreateInstance();

                                        objCommUtil.PrintCommonReceipt(true, TreasuryType, iVoid.ToString());
                                    }
                                    // Already handled in the CAGE Printslip

                                    // Newly Added - Venkatesh Kumart.J - SGVI Requirements

                                    ////if (VoidAmount> Settings.W2GWinAmount)
                                    ////{
                                    ////    if (Settings.W2GMessage)
                                    ////    {
                                    ////        if (AppSettings.IsReceiptRequired && !Settings.CAGE_ENABLED)
                                    ////        {

                                    ////            (oCommonUtilities.CreateInstance()).PrintCommonReceipt(true, (lstHandpay.SelectedItem as FillTreasuryList).HP_Type, iVoid.ToString());


                                    ////        }
                                    ////    }
                                    ////}

                                    ////else
                                    ////{
                                    ////    if (VoidAmount <= Settings.W2GWinAmount && !Settings.CAGE_ENABLED)
                                    ////    {
                                    ////        (oCommonUtilities.CreateInstance()).PrintCommonReceipt(true, (lstHandpay.SelectedItem as FillTreasuryList).HP_Type, iVoid.ToString());
                                    ////    }

                                    ////}

                                    //if (AppSettings.IsReceiptRequired)
                                    //{
                                    //    if (!Settings.CAGE_ENABLED)
                                    //        (oCommonUtilities.CreateInstance()).PrintCommonReceipt(true, (lstHandpay.SelectedItem as FillTreasuryList).HP_Type, iVoid.ToString());
                                    //}

                                    //-----------------Untill Here -------------------------



                                    FillTreasury(Position);
                                    //BindListView();
                                    Helper_classes.Common.BindListView(treasuryList, lstHandpay);

                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {

                                        AuditModuleName = ModuleName.Void,
                                        Audit_Screen_Name = "Postion Details|Attendant Pay|Void",
                                        Audit_Desc = TreasuryType + " Date: " + TreasuryDate + " Amount:" + Amount,
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


        private void lstHandpay_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void ProcessHandpay(bool IsVoid)
        {
            Window Owner;
            double amount = 0;
            CAuthorize objAuthorize = null;
            Treasury treasury = null;
            int AuthUserID = 0;
            try
            {
                //
                if (!IsHandpayVoid)
                {
                    if ((lstHandpay.SelectedItem as FillTreasuryList).Amount != null && (double)(lstHandpay.SelectedItem as FillTreasuryList).Amount > 0)
                    {
                        //double.TryParse((lstHandpay.SelectedItem as FillTreasuryList).Amount.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out amount);
                        //------->
                        // Issue fix for ->In ITALY environment if user currency setting is set as italy then while processing handpay amount 
                        // of 99,36 customer information screen is displaying. (If we set user currency setting as US or UK then 
                        // we are able to process handpays properly)
                        //<--------
                        double.TryParse((lstHandpay.SelectedItem as FillTreasuryList).Amount.ToString(), out amount);
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
                    AuthUserID = Security.SecurityHelper.CurrentUser.User_No;

                    if (Settings.Client != null && Settings.Client.ToLower() == "winchells"
                        && Settings.MaxHandPayAuthRequired
                        && (lstHandpay.SelectedItem as FillTreasuryList).Amount != null
                        && (amount > Settings.HandpayPayoutCustomer_Max))
                    {
                        objAuthorize = new CAuthorize("CashdeskOperator.Authorize.cs.MaxHandpay");
                        objAuthorize.User = Security.SecurityHelper.CurrentUser;
                        if (!Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MaxHandpay"))
                        {
                            objAuthorize.ShowDialog();
                            if (!objAuthorize.IsAuthorized)
                                return;

                            else
                            {
                                AuthUserID = handpay.GetUserID(objAuthorize.User.SecurityUserID);

                            }
                        }
                        else
                        {
                            objAuthorize.IsAuthorized = true;
                        }
                    }

                    if (Settings.RegulatoryEnabled == true && Settings.RegulatoryType == "AAMS")
                    {
                        if ((lstHandpay.SelectedItem as FillTreasuryList).Amount != null)
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

                int TE_ID = (lstHandpay.SelectedItem as FillTreasuryList).TE_ID;
                treasury = new Treasury
                {
                    InstallationNumber =
                        (lstHandpay.SelectedItem as FillTreasuryList).Installation_No,
                    TreasuryType = (lstHandpay.SelectedItem as FillTreasuryList).HP_Type,
                    TreasuryAmount = (double)(lstHandpay.SelectedItem as FillTreasuryList).Amount,
                    TreasuryTemp = IsVoid ? true : false,
                    ActualTreasuryDate =
                        (DateTime)((lstHandpay.SelectedItem as FillTreasuryList).TreasuryDate)
                };
                treasury.CustomerID = Custid;
                treasury.UserID = AuthUserID;
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
                        Audit_Slot = (lstHandpay.SelectedItem as FillTreasuryList).Asset
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
                        Audit_Slot = (lstHandpay.SelectedItem as FillTreasuryList).Asset
                    });

                    txtAmount.Text = "";

                    if ((bool)(lstHandpay.SelectedItem as FillTreasuryList).HP_Uncleared)
                    {
                        //LogManager.WriteLog("Executing Path : " + BMCRegistryHelper.GetRegLocalMachine().OpenSubKey("Software\\Honeyframe").GetValue("InstallationPath").ToString().Trim() + Common.ConfigurationManagement.ConfigManager.Read(
                        //            "HandpayCommandLinePrompt") + " ClearHandpay " + (lstHandpay.SelectedItem as FillTreasuryList).Datapak_No, LogManager.enumLogLevel.Info);



                        //System.Diagnostics.Process.Start(BMCRegistryHelper.GetRegLocalMachine().OpenSubKey("Software\\Honeyframe").GetValue("InstallationPath").ToString().Trim() + Common.ConfigurationManagement.ConfigManager.Read(
                        //            "HandpayCommandLinePrompt"), " ClearHandpay " + (lstHandpay.SelectedItem as FillTreasuryList).Datapak_No);

                        //var proc = new System.Diagnostics.Process
                        //{
                        //    StartInfo =
                        //    {
                        //        FileName =
                        //            Environment.CurrentDirectory + "\\" + Common.ConfigurationManagement.ConfigManager.Read(
                        //            "HandpayCommandLinePrompt"),
                        //        Arguments =
                        //            "ClearHandpay "+ (lstHandpay.SelectedItem as FillTreasuryList).Datapak_No
                        //    }
                        //};
                        //proc.Start();
                    }

                    if (!IsVoid)
                    {

                        #region GCD
                        if (Settings.IsGloryCDEnabled && Settings.CashDispenserEnabled)
                        {
                            LoadingWindow ld = new LoadingWindow(this, ModuleName.AttendantPay, Treasury_No.ToString(), sPos, Convert.ToInt32(treasury.TreasuryAmount * 100));
                            ld.Topmost = true;
                            ld.ShowDialog();
                            Result res = ld.Result;
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
                                    Audit_Slot = (lstHandpay.SelectedItem as FillTreasuryList).Asset
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

                        FillTreasury(sPos);
                        Helper_classes.Common.BindListView(treasuryList, lstHandpay);

                        //Newly Added - Venkatesh Kumar - SGVI

                        if (AppSettings.IsReceiptRequired)
                        {
                            if (objAuthorize != null && objAuthorize.User != null)
                                (oCommonUtilities.CreateInstance()).PrintCommonReceipt(false, treasury.TreasuryType, Treasury_No.ToString(), objAuthorize.User);
                            else
                                (oCommonUtilities.CreateInstance()).PrintCommonReceipt(false, treasury.TreasuryType, Treasury_No.ToString());
                        }

                        //---------************---------------

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
                                    Audit_Slot = (lstHandpay.SelectedItem as FillTreasuryList).Asset
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
                                    Audit_Slot = (lstHandpay.SelectedItem as FillTreasuryList).Asset
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
                                    Audit_Slot = (lstHandpay.SelectedItem as FillTreasuryList).Asset
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
                    Audit_Slot = handpay.GetAssetNumber(InstallationNumber)[0].Stock_No
                });
            }
            finally
            {
                IsHandpayVoid = false;
            }
        }

        private void SaveManualHandpay()
        {
            Window Owner;
            double amount = 0;
            Treasury treasury = null;
            int AuthUserID = 0;
            try
            {
                CAuthorize objAuthorize = null;
                if (txtBox != null && txtBox.Text.Length > 0)
                {
                    // Issue fix for ->set cultureinfo='en-US',  currencyculture='it-IT'
                    double.TryParse(txtBox.Text.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out amount);
                }
                AuthUserID = Security.SecurityHelper.CurrentUser.User_No;
                if (Settings.Client != null && Settings.Client.ToLower() == "winchells"
                    && Settings.MaxHandPayAuthRequired
                    && txtBox != null
                    && (amount > Settings.HandpayPayoutCustomer_Max))
                {
                    objAuthorize = new CAuthorize("CashdeskOperator.Authorize.cs.MaxHandpay");
                    objAuthorize.User = Security.SecurityHelper.CurrentUser;
                    AuthUserID = Security.SecurityHelper.CurrentUser.User_No;

                    if (!Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MaxHandpay"))
                    {
                        objAuthorize.ShowDialog();
                        if (!objAuthorize.IsAuthorized)
                        {
                            IsProcessed = true;
                            return;
                        }
                        else
                        {
                            AuthUserID = handpay.GetUserID(objAuthorize.User.SecurityUserID);

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

                    if (ProcessCancelled) // if the process cancelled from the customer then  back to the handpay screen
                        return;
                }


                treasury = new Treasury { InstallationNumber = InstallationNumber };

                if (optHandpay.IsChecked == true)
                    treasury.TreasuryType = "AttendantPay Credit";
                else if (optJackpot.IsChecked == true)
                    treasury.TreasuryType = "AttendantPay Jackpot";
                else
                    treasury.TreasuryType = "PROGRESSIVE";
                treasury.TreasuryAmount = amount;
                treasury.ActualTreasuryDate = DateTime.Now;
                treasury.UserID = AuthUserID;

                treasury.Authorized_Date = DateTime.MinValue.DBMinValue();
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
                        Audit_Slot = handpay.GetAssetNumber(InstallationNumber)[0].Stock_No
                    });

                }

                treasury.CustomerID = Custid; // add the customer to the treasury if amt between 1000 & 4000 or >5000

                //if (Settings.W2GMessage)
                //{
                //    if (amount > Settings.W2GWinAmount)
                //    {
                //        MessageBox.ShowBox("MessageID367", BMC_Icon.Information);
                //        //Treasury_No = 0;
                //    }

                //    else
                //    {
                Treasury_No = handpay.ProcessHandPay(treasury, 0);
                //    }
                //}

                IsProcessed = true;
                if (Treasury_No > 0)
                {
                    DateTime dtTreasury = (DateTime)handpay.GetTreasuryDateTime(Treasury_No);
                    TextBlock_11.Text = "#" + BarPosition + dtTreasury.ToString("ddMMyyyyHHmmss");
                    txtAmount.Text = Convert.ToDecimal((treasury.TreasuryAmount)).GetUniversalCurrencyFormat();

                    #region GCD
                    if (Settings.IsGloryCDEnabled && Settings.CashDispenserEnabled)
                    {

                        LoadingWindow ld = new LoadingWindow(Window.GetWindow(this), ModuleName.ManualAttendantPay, Treasury_No.ToString(), BarPosition, Convert.ToInt32(treasury.TreasuryAmount * 100));
                        ld.Topmost = true;
                        ld.ShowDialog();
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
                                Audit_Slot = (lstHandpay.SelectedItem as FillTreasuryList).Asset
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
                        Audit_Slot = handpay.GetAssetNumber(InstallationNumber)[0].Stock_No
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
                        Audit_Slot = handpay.GetAssetNumber(InstallationNumber)[0].Stock_No
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
                    Audit_Slot = handpay.GetAssetNumber(InstallationNumber)[0].Stock_No
                });
            }
        }

        internal void PostHandpayEvent(Treasury objTreasury)
        {
            try
            {

                List<AssetNumberResult> lstTreasury = handpay.GetAssetNumber(objTreasury.InstallationNumber);

                JackpotEventRequest request = new JackpotEventRequest();

                request.CardNumber = handpay.GetEPIDetails(InstallationNumber);
                request.JackpotAmount = objTreasury.TreasuryAmount * 100;
                request.InstallationNo = InstallationNumber;

                if (App.client != null)
                    App.client.SendToServer(App.clientObj, request);
                else
                    LogManager.WriteLog("CViewHandpay PostHandpayEvent - Connection not set", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("PostHandpayEvent" + ex.Message, LogManager.enumLogLevel.Error);
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

        private int VoidHandpay()
        {
            int result = 0;
            VoidTranCreate voidTran = null;

            try
            {
                voidTran = new VoidTranCreate
                                   {
                                       TreasuryID = Treasury_No.ToString(),
                                       Date = DateTime.Now,
                                       UserNo = Security.SecurityHelper.CurrentUser.User_No.ToString()
                                       //Date = DateTime.Now
                                   };

                result = handpay.VoidTransaction(voidTran);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -99;
            }
            finally
            {
                if (result > 0)
                {
                    try
                    {
                        jackpotProcessInfoDTO jpinfo = new jackpotProcessInfoDTO();
                        jpinfo.Slot = this.BarPosition;
                        jpinfo.assetConfigNumber = Asset;
                        jpinfo.Denom = 0.01M;

                        string TreasuryDate = string.Empty;
                        string Amount = string.Empty;
                        string TreasuryNo = string.Empty;
                        string TreasuryType = string.Empty;
                        short jackpotTypeId = 0;
                        string slot = string.Empty;
                        string assetConfigNumber = string.Empty;

                        try
                        {
                            FillTreasuryList list = (lstHandpay.SelectedItem as FillTreasuryList);
                            if (list != null)
                            {
                                TreasuryDate = list.TreasuryDate.ToString();
                                Amount = list.Amount.ToString();
                                TreasuryNo = list.TE_ID.ToString();
                                TreasuryType = list.HP_Type;
                                jackpotTypeId = list.HP_Type.ToLower() == "attendantpay Credit" ? Convert.ToInt16(1) :
                                                list.HP_Type.ToLower() == "attendantpay jackpot" ? Convert.ToInt16(2) :
                                                list.HP_Type.ToLower() == "progressive" ? Convert.ToInt16(3) : Convert.ToInt16(1);

                                slot = list.Pos;
                                assetConfigNumber = list.Asset;
                            }
                            LogManager.WriteLog("|==> VOID AMOUNT IS : " + Amount, LogManager.enumLogLevel.Info);
                        }
                        catch (Exception ex)
                        {
                            LogManager.WriteLog("|==> Exception Occured in VoidHandpy " + Amount, LogManager.enumLogLevel.Info);
                            ExceptionManager.Publish(ex);
                        }

                        jpinfo.Slot = slot;
                        jpinfo.assetConfigNumber = assetConfigNumber;

                        double value = 0;
                        double.TryParse(Amount, out value);
                        jpinfo.hpjpAmount = (long)(-1 * (value * 100));
                        try { jpinfo.jackpotNetAmount = (long)value; }
                        catch { }
                        jpinfo.jackpotTypeId = jackpotTypeId;

                        double.TryParse(TreasuryNo, out value);
                        jpinfo.sequenceNumber = (long)value;

                        jpinfo.TransactionDate = TreasuryDate;
                        jpinfo.UserID = Security.SecurityHelper.CurrentUser.SecurityUserID.ToString();
                        jpinfo.siteNo = Settings.SiteName;
                        jpinfo.siteId = Convert.ToInt32(Settings.SiteCode);
                        //Commented for not printing in cage slip format
                        if (Settings.CAGE_ENABLED)
                            handpay.PrintSlip(jpinfo);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }

            return result;
        }

        public void Reload(string barPos, int installationNo)
        {
            try
            {
                this.InstallationNumber = installationNo;
                this.BarPosition = barPos;
                RefreshListView(barPos);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillTreasury(string BarPos)
        {
            if (Settings.HandpayManual == false)
                btnManual.Visibility = Visibility.Hidden;

            treasuryList = handpay.GetHandpays(BarPos);
        }

        private bool ValidInfo()
        {
            txtBox = (TextBox)this.ucValueCalcComp.txtDisplay;
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID120", BMC_Icon.Information);
                ExceptionManager.Publish(ex);
                return false;
            }

            return true;

        }

        private void ValueCalcComp_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                txtBox = (TextBox)((ValueCalcComp)sender).txtDisplay;
                if (!Settings.AllowManualKeyboard)
                    txtBox.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void optHandpay_Checked(object sender, RoutedEventArgs e)
        {
            if (optHandpay.IsChecked == true)
            {
                lblManualheader.Text = btnManual.Content.ToString();
                lblSelheaderValue.Text = "[" + optHandpay.Content.ToString() + "]";

                if (txtBox != null)
                {
                    txtBox.Text = this.DefaultAmount();
                    ucValueCalcComp.s_UnformattedText = "";
                }
                ucValueCalcComp.txtDisplay.Focus();
            }
        }

        private void optJackpot_Checked(object sender, RoutedEventArgs e)
        {
            if (optJackpot.IsChecked == true)
            {
                lblManualheader.Text = TYPE + optJackpot.Content.ToString();
                lblSelheaderValue.Text = "[" + optJackpot.Content.ToString() + "]";

                if (txtBox != null)
                {
                    txtBox.Text = this.DefaultAmount();
                    ucValueCalcComp.s_UnformattedText = "";
                }
                ucValueCalcComp.txtDisplay.Focus();
            }
        }

        private void optProgressive_Checked(object sender, RoutedEventArgs e)
        {
            if (optProgressive.IsChecked == true)
            {
                lblManualheader.Text = TYPE + optProgressive.Content.ToString();
                lblSelheaderValue.Text = "[" + optProgressive.Content.ToString() + "]";

                if (txtBox != null)
                {
                    txtBox.Text = this.DefaultAmount();
                    ucValueCalcComp.s_UnformattedText = "";
                }
                ucValueCalcComp.txtDisplay.Focus();
            }
        }


        private void btnManual_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_11.Text = string.Empty;
            txtAmount.Text = string.Empty;
            GridManualHandpay.Visibility = Visibility.Visible;
            GHandpay.Visibility = Visibility.Hidden;
            ucValueCalcComp.txtDisplay.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox != null)
            {
                txtBox.Text = this.DefaultAmount();
                ucValueCalcComp.s_UnformattedText = "";
            }

            GridManualHandpay.Visibility = Visibility.Hidden;
            GHandpay.Visibility = Visibility.Visible;
        }

        private void lstHandpay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                try
                {
                    if (lstHandpay.Items.Count > 0)

                        if (lstHandpay.SelectedItem != null)
                        {
                            txtAmount.Text = Convert.ToDecimal(((lstHandpay.SelectedItem as FillTreasuryList).Amount.Value)).GetUniversalCurrencyFormat();
                            if (Settings.SHOWHANDPAYCODE == "TRUE")
                            {
                                //TextBlock_11.Text = "#" + (lstHandpay.SelectedItem as FillTreasuryList).Pos + (lstHandpay.SelectedItem as FillTreasuryList).TreasuryDate.ToString().Replace("/", "").Replace(":", "").Replace("AM", "0").Replace("PM", "1").Replace(" ", "");
                                TextBlock_11.Text = "#" + (lstHandpay.SelectedItem as FillTreasuryList).Pos + Convert.ToDateTime((lstHandpay.SelectedItem as FillTreasuryList).TreasuryDate).ToString("ddMMyyyyHHmmss");
                            }
                            iSelecteedHandPay = (lstHandpay.SelectedItem as FillTreasuryList).TE_ID;
                        }
                }

                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double amount = 0;
                btnSave.IsEnabled = false;

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
                    optHandpay.IsEnabled = true;
                    optJackpot.IsEnabled = true;
                    optProgressive.IsEnabled = true;
                    btnCancel.Visibility = Visibility.Visible;
                    ucValueCalcComp.txtDisplay.Focus();
                    //}

                }
                else
                {
                    if (ValidInfo())
                    {
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
                //e.Handled = true;
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
        private void btnGenerateSlipNo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Auth_User_ID = 0;
                if (lstHandpay.Items.Count > 0)
                {
                    LogManager.WriteLog("Generating Slip", LogManager.enumLogLevel.Debug);
                    if (Clearhandpay(Convert.ToInt32((lstHandpay.SelectedItem as FillTreasuryList).Installation_No)))
                    {
                        jackpotProcessInfoDTO jpinfo = new jackpotProcessInfoDTO();
                        CAuthorize objAuthorize = null;
                        int TE_ID = (lstHandpay.SelectedItem as FillTreasuryList).TE_ID;
                        Treasury treasury = new Treasury
                        {
                            InstallationNumber =
                                (lstHandpay.SelectedItem as FillTreasuryList).Installation_No,
                            TreasuryType = (lstHandpay.SelectedItem as FillTreasuryList).HP_Type,
                            TreasuryAmount = (double)(lstHandpay.SelectedItem as FillTreasuryList).Amount,
                            TreasuryTemp = false,
                            ActualTreasuryDate =
                                (DateTime)((lstHandpay.SelectedItem as FillTreasuryList).TreasuryDate)
                        };
                        Auth_User_ID = Security.SecurityHelper.CurrentUser.User_No;

                        if (treasury.TreasuryAmount > Settings.HandpayPayoutCustomer_Max)
                        {
                            objAuthorize = new CAuthorize("CashdeskOperator.Authorize.cs.MaxHandpay");
                            objAuthorize.User = Security.SecurityHelper.CurrentUser;
                            if (!Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MaxHandpay"))
                            {
                                objAuthorize.ShowDialog();
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
                        treasury.CustomerID = Custid;
                        treasury.UserID = Auth_User_ID;
                        treasury.Authorized_Date = DateTime.Now.DBMinValue();
                        jpinfo.Slot = (lstHandpay.SelectedItem as FillTreasuryList).Pos;
                        jpinfo.assetConfigNumber = (lstHandpay.SelectedItem as FillTreasuryList).Asset;
                        jpinfo.Denom = 0.01M;
                        jpinfo.hpjpAmount = Convert.ToInt32((lstHandpay.SelectedItem as FillTreasuryList).Amount * 100);
                        jpinfo.jackpotNetAmount = Convert.ToInt32((lstHandpay.SelectedItem as FillTreasuryList).Amount);
                        jpinfo.jackpotTypeId = (lstHandpay.SelectedItem as FillTreasuryList).HP_Type.ToLower() == "attendantpay Credit" ? Convert.ToInt16(1) :
                            (lstHandpay.SelectedItem as FillTreasuryList).HP_Type.ToLower() == "attendantpay jackpot" ? Convert.ToInt16(2) :
                              (lstHandpay.SelectedItem as FillTreasuryList).HP_Type.ToLower() == "progressive" ? Convert.ToInt16(3) : Convert.ToInt16(1);



                        jpinfo.sequenceNumber = (lstHandpay.SelectedItem as FillTreasuryList).TE_ID;
                        jpinfo.TransactionDate = (lstHandpay.SelectedItem as FillTreasuryList).TreasuryDate.ToString();
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

                DataSet oDt = handpay.createTickeException_HandpayCAGE(int.Parse(this.BarPosition), iAmount, 0, HP_Type);
                if (Asset.IsNullOrEmpty())
                {
                    Asset = handpay.GetAssetNumber(InstallationNumber)[0].Stock_No;
                }
                decimal DenomValue = 0;

                List<DenomValueResult> lstDenom = handpay.GetDenomValue(Asset);
                foreach (var denom in lstDenom)
                {
                    DenomValue = denom.Denom;
                }
                jpinfo.Slot = this.BarPosition;
                jpinfo.assetConfigNumber = Asset;
                jpinfo.Denom = DenomValue;
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
                treasury.AuthorizedUser_No = SecurityHelper.CurrentUser.User_No;
                treasury.CustomerID = SecurityHelper.CurrentUser.User_No;
                treasury.InstallationNumber = InstallationNumber;
                treasury.TreasuryAmount = iAmount;
                treasury.UserID = SecurityHelper.CurrentUser.User_No;
                treasury.TreasuryType = HP_Type;


                if (((bool)optJackpot.IsChecked) || ((bool)optProgressive.IsChecked) || ((bool)optHandpay.IsChecked))
                {
                    // string installationType = installationPathkey.GetValue("InstallationType").ToString();

                    //if (installationType.ToUpper().Equals("EXCHANGECLIENT"))
                    //{
                    //    if (Settings.SendPT10FromClient)
                    //        PostHandpayEvent(treasury);
                    //}
                    //else
                    PostHandpayEvent(treasury);
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }

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
                        this.btnManual.Click -= (this.btnManual_Click);
                        this.lstHandpay.Loaded -= (this.lstHandpay_Loaded);
                        this.lstHandpay.SelectionChanged -= (this.lstHandpay_SelectionChanged);
                        this.btnProcess.Click -= (this.btnProcess_Click);
                        this.btnVoid.Click -= (this.btnVoid_Click);
                        this.btnSave.Click -= (this.btnSave_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                        this.optHandpay.Checked -= (this.optHandpay_Checked);
                        this.optJackpot.Checked -= (this.optJackpot_Checked);
                        this.optProgressive.Checked -= (this.optProgressive_Checked);
                        if (dispenserStatus != null)
                        {
                            dispenserStatus.KillLoadItemsThread();
                        }
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CViewHandpay objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CViewHandpay"/> is reclaimed by garbage collection.
        /// </summary>
        ~CViewHandpay()
        {
            Dispose(false);
        }

        #endregion

        private void ViewHandpay_Loaded(object sender, RoutedEventArgs e)
        {
            Helper_classes.Common.BindListView(treasuryList, lstHandpay);
            try
            {
                var i = from te in treasuryList
                        where te.TE_ID == iSelecteedHandPay
                        select te;
                if (i != null)
                {
                    lstHandpay.SelectedItem = i;
                    lstHandpay.Focus();
                }

                this.ucValueCalcComp.txtDisplay.Text = "";
                this.ucValueCalcComp.s_UnformattedText = "";
                //this.ucValueCalcComp.hClickEvent += new ScannerClick(btnSave_Click);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void RefreshListView(string barPosition)
        {
            FillTreasury(barPosition);
            Helper_classes.Common.BindListView(treasuryList, lstHandpay);
            try
            {
                var i = from te in treasuryList
                        where te.TE_ID == iSelecteedHandPay
                        select te;
                if (i != null)
                {
                    lstHandpay.SelectedItem = i;
                    lstHandpay.Focus();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
    }
}
