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
using BMC.CashDeskOperator;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Business.CashDeskOperator;
using BMC.Presentation.POS.Views;
using BMC.Transport;
using BMC.Common.Utilities;
using System.Configuration;
using BMC.Security;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Note : Replace Vault instead of Unlock
    /// </summary>
    public partial class CFillVault : UserControl
    {
        Vault objVault = new Vault();
        DataTable VaultDeviceList = null;
        DataRow _DrVaultBalance = null;
        string _FillType = string.Empty;
        bool _IsStandardFillHasAccess = false;
        bool _IsStandardFill = false;
        bool _sendAlert = false;
        bool _IsFinalDrop = false;
        bool _IsWebServiceEnabled = false;
        private List<DenomCombo> lst_denoms = null;
        decimal VaultStandardFillAmount = 0m;
        string strInitialFill = "";
        const int InitialFillValue = 6; // [InitialFillValue] Hardcoded for initial  Fill
        public CFillVault()
        {
            LogManager.WriteLog("CFillVault:Load ", LogManager.enumLogLevel.Debug);
            InitializeComponent();
            ucValueCalc.MaxLength = 15;
        }

       #region "Form Events"

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                ucValueCalc.MaxLength = 11;
                ucValueCalc.txtDisplay.IsReadOnly = true;

                lst_denoms = LoadDenom();
                LogManager.WriteLog("CFillVault:Loading CFillVault:UserControl_Loaded ", LogManager.enumLogLevel.Debug);

                DataTable dtTransactionreasons = objVault.GetTransactionReasons();
                DataRow[] drReason = dtTransactionreasons.Select("Reason_ID='" + InitialFillValue + "'");
                if (drReason.Length > 0)
                {
                    strInitialFill = drReason[0]["Reason_Description"].ToString();
                }
                DataRow dr = dtTransactionreasons.NewRow();
                //Add select item to combobox
                dr["Reason_ID"] = -1;
                dr["Reason_Description"] = Application.Current.FindResource("CFillVault_cmbTransactionReason") as string;
                dtTransactionreasons.Rows.InsertAt(dr, 0);
                cmbTransactionReason.ItemsSource = dtTransactionreasons.DefaultView;
                cmbTransactionReason.SelectedIndex = 0;

                //List all vaults
                GetVaultDetails(true);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }
        }

        private void GetVaultDetails(bool Novault)
        {
            try
            {
                VaultDeviceList = objVault.GetVaultDevices();
                cmb_DeviceName.ItemsSource = VaultDeviceList.DefaultView;

                if (Novault == true && VaultDeviceList.Rows.Count > 0)
                {
                    _IsStandardFillHasAccess = Security.SecurityHelper.HasAccess("BMC.Presentation.CFillVault.btnVaultFill");
                    btn_StandardFill.Visibility = _IsStandardFillHasAccess ? Visibility.Visible : Visibility.Hidden;
                    gd_Drop.Visibility = Security.SecurityHelper.HasAccess("BMC.Presentation.CFillVault.btnVaultDrop") ? Visibility.Visible : Visibility.Hidden;
                    btn_Bleed.Visibility = Security.SecurityHelper.HasAccess("BMC.Presentation.CFillVault.btnVaultBleed") ? Visibility.Visible : Visibility.Hidden;
                    btn_FillVault.Visibility = _IsStandardFillHasAccess ? Visibility.Visible : Visibility.Hidden;
                    gd_adjustment.Visibility = Security.SecurityHelper.HasAccess("BMC.Presentation.CFillVault.btnVaultAdjustment") ? Visibility.Visible : Visibility.Hidden;
                    if (VaultDeviceList.Rows.Count > 0)
                        cmb_DeviceName.SelectedIndex = 0;
                }
                else
                {
                    EnableDisableControls(false);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }
        }

        private bool EnableDisableControls(bool bType)
        {
            //if Vaults exists then all the controls will be enabled in the screen.
            if (!bType)
            {
                lbl_Status.Text = "No Vaults Found";
                cmb_DeviceName.IsEnabled = bType;
                cmbTransactionReason.IsEnabled = bType;
                btn_Adjustment_Negative.Visibility = Visibility.Collapsed;
                btn_Adjustment_Positive.Visibility = Visibility.Collapsed;
                btn_FillVault.Visibility = Visibility.Collapsed;
                btn_RefreshBalance.Visibility = Visibility.Collapsed;
                btn_StandardFill.Visibility = Visibility.Collapsed;
                btn_Bleed.Visibility = Visibility.Collapsed;
                btn_PrintSlip.Visibility = Visibility.Collapsed;
                gd_Drop.Visibility = Visibility.Collapsed;
                ucValueCalc.IsEnabled = bType;
                txt_SerialNo.Text = string.Empty;
                txt_AlertLevel.Text = string.Empty;
                txt_CurrentBalance.Text = string.Empty;
                txt_Manufacturer.Text = string.Empty;
                txt_Type.Text = string.Empty;
                ucValueCalc.s_UnformattedText = string.Empty;
                gd_adjustment.Visibility = Visibility.Collapsed;
                ucValueCalc.txtDisplay.Clear();
                ucValueCalc.textBox1.Clear();
                ucValueCalc.ValueText = string.Empty;
                cmbTransactionReason.SelectionChanged -= cmbTransactionReason_SelectionChanged;
                cmbTransactionReason.SelectedIndex = 0;
                cmbTransactionReason.SelectionChanged += cmbTransactionReason_SelectionChanged;
            }
            else
            {
                lbl_Status.Text = string.Empty;

                cmbTransactionReason.IsEnabled = bType;
                cmbTransactionReason.IsEnabled = bType;
                btn_Adjustment_Negative.Visibility = Visibility.Visible;
                btn_Adjustment_Positive.Visibility = Visibility.Visible;
                btn_FillVault.Visibility = Visibility.Visible;
                btn_RefreshBalance.Visibility = Visibility.Visible;
                btn_StandardFill.Visibility = Visibility.Visible;
                btn_Bleed.Visibility = Visibility.Visible;
                btn_PrintSlip.Visibility = Visibility.Visible;
                gd_Drop.Visibility = Visibility.Visible;
                ucValueCalc.IsEnabled = bType;
            }
            return bType;
        }

        private void cmb_DeviceName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                //Refresh balance 
                showModel(RefreshVaultBalance);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }
        }

        private void btn_RefreshBalance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Refresh Balance
                showModel(RefreshVaultBalance);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }
        }

        private void btn_PrintSlip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Print currrent Balance
                if (_DrVaultBalance != null)
                {
                    CreateVaultCurrentBalanceSlipReport(Convert.ToInt16(_DrVaultBalance["Vault_Id"]), _DrVaultBalance["Name"].ToString(), Convert.ToString(_DrVaultBalance["Serial_No"]),
                                                             _DrVaultBalance["Manufacturer_Name"].ToString(), _DrVaultBalance["Type_Prefix"].ToString(), SecurityHelper.CurrentUser.DisplayName,
                                                             DateTime.Now, Convert.ToBoolean(_DrVaultBalance["IsWebServiceEnabled"]), Convert.ToDecimal(_DrVaultBalance["FillAmount"]),
                                                             Convert.ToDecimal(_DrVaultBalance["TotalAmountOnFill"]), Convert.ToDecimal(_DrVaultBalance["CurrentBalance"]));
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
                lbl_Status.Text = "Error printing slip";
            }

        }
        public void CreateVaultCurrentBalanceSlipReport(int Vault_Id, string VaultName, string Serial_No, string Manufacturer_Name, string Type_Prefix, string LoginUser,
                                                           DateTime CreatedDate, bool IsWebServiceEnabled, decimal FillAmount, decimal TotalAmountOnFill, decimal CurrentBalance)
        {
            try
            {
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                    cReportViewer.ShowVaultCurrentBalanceSlip(Vault_Id, VaultName, Serial_No, Manufacturer_Name, Type_Prefix, LoginUser, CreatedDate,
                                                                         IsWebServiceEnabled, Convert.ToDecimal(FillAmount), Convert.ToDecimal(TotalAmountOnFill), Convert.ToDecimal(CurrentBalance));
                    cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));
                    cReportViewer.Show();
                }
                LogManager.WriteLog("Show Vault Current Balance Slip Report Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Print transacrion slip
        /// </summary>
        /// <param name="drBalance">transaction record</param>
        /// <param name="strFillType">Transaction type</param>
        void Print(DataRow drBalance, string strFillType)
        {

            try
            {
                if (drBalance != null)
                {
                    using (CReportViewer cReportViewer = new CReportViewer())
                    {
                        cReportViewer.ShowVaultCurrentTransactionSlip(int.Parse(drBalance["Vault_ID"].ToString()), drBalance["Name"].ToString(), drBalance["Serial_NO"].ToString(), drBalance["Manufacturer_Name"].ToString(),
                                                 drBalance["Type_Prefix"].ToString(), SecurityHelper.CurrentUser.DisplayName, Convert.ToDateTime(drBalance["CreatedDate"]), Convert.ToBoolean(drBalance["IsWebServiceEnabled"]),
                                                 Convert.ToDecimal(drBalance["FillAmount"]), Convert.ToDecimal(drBalance["TotalAmountOnFill"]), Convert.ToDecimal(drBalance["CurrentBalance"]), strFillType);
                        cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));
                        cReportViewer.Show();
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        void PrintDrop(DataRow drBalance)
        {

            try
            {
                if (drBalance != null)
                {
                    using (CReportViewer cReportViewer = new CReportViewer())
                    {
                        cReportViewer.ShowVaultCurrentDropSlip(int.Parse(cmb_DeviceName.SelectedValue.ToString()), drBalance["Name"].ToString(), drBalance["Serial_NO"].ToString(), drBalance["Manufacturer_Name"].ToString(),
                                                 drBalance["Type_Prefix"].ToString(), SecurityHelper.CurrentUser.DisplayName, Convert.ToDateTime(drBalance["CreatedDate"]), Convert.ToBoolean(drBalance["IsWebServiceEnabled"]),
                                                 Convert.ToDecimal(drBalance["FillAmount"]), Convert.ToDecimal(drBalance["TotalAmountOnFill"]), Convert.ToDecimal(drBalance["CurrentBalance"]), _IsFinalDrop);
                        cReportViewer.SetOwner(System.Windows.Window.GetWindow(this));
                        cReportViewer.Show();
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }


        private void btn_FillVault_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Fill amount
                LogManager.WriteLog("CFillVault:Start Fill ", LogManager.enumLogLevel.Debug);
                if (cmbTransactionReason.SelectedIndex < 1)
                {
                    lbl_Status.Text = Application.Current.FindResource("Vault_MessageID6") as string;
                    cmbTransactionReason.Focus();
                    return;
                }
                _FillType = "FILL";
                _sendAlert = false;
                if (_IsWebServiceEnabled)
                {

                    int VaultID = int.Parse(cmb_DeviceName.SelectedValue.ToString());
                    CVaultFillCassette c_cassette = new CVaultFillCassette(VaultID, cmb_DeviceName.Text, 0, VaultTransactionType.Fill, _FillType, (int)cmbTransactionReason.SelectedValue, txt_Manufacturer.Text, txt_Type.Text, cmbTransactionReason.Text, lst_denoms,false, RefreshVaultBalance);
                    c_cassette.Owner = Window.GetWindow(this);
                    c_cassette.ShowDialog();
                    cmbTransactionReason.SelectedIndex = 0;
                }
                else
                {
                    showModel(FillVault);
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }

        }

        private void btn_StandardFill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Fill  standard amount
                _sendAlert = false;
                LogManager.WriteLog("CFillVault:Start standard Fill ", LogManager.enumLogLevel.Debug);
                if (cmbTransactionReason.SelectedIndex < 1)
                {
                    lbl_Status.Text = Application.Current.FindResource("Vault_MessageID6") as string;
                    cmbTransactionReason.Focus();
                    return;
                }

                _FillType = "STANDARDFILL";
                int VaultID = int.Parse(cmb_DeviceName.SelectedValue.ToString());

                if (_IsWebServiceEnabled)
                {

                    CVaultFillCassette c_cassette = new CVaultFillCassette(VaultID, cmb_DeviceName.Text, 0, VaultTransactionType.StandardFill, _FillType, (int)cmbTransactionReason.SelectedValue, txt_Manufacturer.Text, txt_Type.Text, cmbTransactionReason.Text, lst_denoms,false, RefreshVaultBalance);
                    c_cassette.Owner = Window.GetWindow(this);
                    c_cassette.ShowDialog();
                    cmbTransactionReason.SelectedIndex = 0;
                }
                else
                {
                    _IsStandardFill = true;
                    if (Vault.CreateInstance().IsStandardFillDoneTwice(VaultID))
                    {
                        System.Windows.Forms.DialogResult dlgResult = MessageBox.ShowBox("Vault_MessageID20", BMC_Icon.Question, BMC_Button.YesNo, "");
                        if (dlgResult == System.Windows.Forms.DialogResult.No)
                        {
                            _IsStandardFill = false;
                            return;
                        }
                        else
                        { _sendAlert = true; }
                    }
                    showModel(FillVault);
                    _IsStandardFill = false;

                }

            }
            catch (Exception Ex)
            {
                _IsStandardFill = false;
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }

        }

        private void btn_Bleed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Fill always negative amount
                _FillType = "BLEED";
                if (cmbTransactionReason.SelectedIndex < 1)
                {
                    lbl_Status.Text = Application.Current.FindResource("Vault_MessageID6") as string;
                    cmbTransactionReason.Focus();
                    return;
                }

                if (_IsWebServiceEnabled)
                {
                    int VaultID = int.Parse(cmb_DeviceName.SelectedValue.ToString());
                    CVaultFillCassette c_cassette = new CVaultFillCassette(VaultID, cmb_DeviceName.Text, 1, VaultTransactionType.Bleed, _FillType, (int)cmbTransactionReason.SelectedValue, txt_Manufacturer.Text, txt_Type.Text, cmbTransactionReason.Text, lst_denoms,false, RefreshVaultBalance);
                    c_cassette.Owner = Window.GetWindow(this);
                    c_cassette.ShowDialog();
                    cmbTransactionReason.SelectedIndex = 0;
                }
                else
                {
                    showModel(BleedVault);
                }
                //lbl_Status.Content = Application.Current.FindResource("Vault_MessageID1") as string;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }
        }

        private void btn_Adjustment_Positive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Fill always positive amount
                _FillType = "ADJUSTMENT";
                if (cmbTransactionReason.SelectedIndex < 1)
                {
                    lbl_Status.Text = Application.Current.FindResource("Vault_MessageID6") as string;
                    cmbTransactionReason.Focus();
                    return;
                }
                if (_IsWebServiceEnabled)
                {
                    int VaultID = int.Parse(cmb_DeviceName.SelectedValue.ToString());

                    CVaultFillCassette c_cassette = new CVaultFillCassette(VaultID, cmb_DeviceName.Text, 0, VaultTransactionType.PositiveAdjustment, _FillType, (int)cmbTransactionReason.SelectedValue, txt_Manufacturer.Text, txt_Type.Text, cmbTransactionReason.Text, lst_denoms,false, RefreshVaultBalance);
                    c_cassette.Owner = Window.GetWindow(this);
                    c_cassette.ShowDialog();
                    cmbTransactionReason.SelectedIndex = 0;
                }
                else
                {
                    showModel(FillVault);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }
        }

        private void btn_Adjustment_Negative_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Fill always negative amount
                _FillType = "ADJUSTMENT";
                if (cmbTransactionReason.SelectedIndex < 1)
                {
                    lbl_Status.Text = Application.Current.FindResource("Vault_MessageID6") as string;
                    cmbTransactionReason.Focus();
                    return;
                }
                if (_IsWebServiceEnabled)
                {
                    int VaultID = int.Parse(cmb_DeviceName.SelectedValue.ToString());

                    CVaultFillCassette c_cassette = new CVaultFillCassette(VaultID, cmb_DeviceName.Text, 1, VaultTransactionType.NegativeAdjustment, _FillType, (int)cmbTransactionReason.SelectedValue, txt_Manufacturer.Text, txt_Type.Text, cmbTransactionReason.Text, lst_denoms,false, RefreshVaultBalance);
                    c_cassette.Owner = Window.GetWindow(this);
                    c_cassette.ShowDialog();
                    cmbTransactionReason.SelectedIndex = 0;
                }
                else
                {
                    showModel(BleedVault);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }
        }

        private void btn_StandardDrop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Drop
                _FillType = "DROP";
                //showModel(DropVault);
                if (cmbTransactionReason.SelectedIndex < 1)
                {
                    lbl_Status.Text = Application.Current.FindResource("Vault_MessageID6") as string;
                    cmbTransactionReason.Focus();
                    return;
                }
                if (_IsWebServiceEnabled)
                {
                    int VaultID = int.Parse(cmb_DeviceName.SelectedValue.ToString());

                    //call standard method
                    Action act_Standard = new Action(() =>
                    {
                        System.Windows.Forms.DialogResult dlgResultStandardDrop = MessageBox.ShowBox("Vault_MessageID18", BMC_Icon.Question, BMC_Button.YesNo, "");
                        if (dlgResultStandardDrop == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                //Fill  standard amount
                                LogManager.WriteLog("CFillVault:Start standard Fill ", LogManager.enumLogLevel.Debug);
                                _FillType = "STANDARDFILL";

                                CVaultFillCassette c_cassettestandard = new CVaultFillCassette(VaultID, cmb_DeviceName.Text, 0, VaultTransactionType.StandardFill, _FillType, InitialFillValue, txt_Manufacturer.Text, txt_Type.Text, strInitialFill, lst_denoms,true, RefreshVaultBalance);
                                c_cassettestandard.Owner = Window.GetWindow(this);
                                c_cassettestandard.ShowDialog();
                                RefreshVaultBalance();
                            }
                            catch (Exception Ex)
                            {
                                ExceptionManager.Publish(Ex);
                                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
                            }
                        }
                        else
                        {
                            RefreshVaultBalance();
                        }

                    });

                    CVaultFillCassette c_cassette = new CVaultFillCassette(VaultID, cmb_DeviceName.Text, 0, VaultTransactionType.StandardDrop, _FillType,
                        (int)cmbTransactionReason.SelectedValue, txt_Manufacturer.Text, txt_Type.Text, cmbTransactionReason.Text, lst_denoms,false,
                       _IsStandardFillHasAccess ? act_Standard : RefreshVaultBalance);
                    c_cassette.Owner = Window.GetWindow(this);
                    c_cassette.ShowDialog();
                    cmbTransactionReason.SelectedIndex = 0;


                }
                else
                {
                    showModel(DropVault);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }
        }

        private void btn_FinalDrop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Drop
                _FillType = "FINALDROP";

                if (cmbTransactionReason.SelectedIndex < 1)
                {
                    lbl_Status.Text = Application.Current.FindResource("Vault_MessageID6") as string;
                    cmbTransactionReason.Focus();
                    return;
                }
                if (_IsWebServiceEnabled)
                {
                    int VaultID = int.Parse(cmb_DeviceName.SelectedValue.ToString());

                    CVaultFillCassette c_cassette = new CVaultFillCassette(VaultID, cmb_DeviceName.Text, 0, VaultTransactionType.FinalDrop, _FillType, (int)cmbTransactionReason.SelectedValue, txt_Manufacturer.Text, txt_Type.Text, cmbTransactionReason.Text, lst_denoms,false, NoVault);
                    c_cassette.Owner = Window.GetWindow(this);
                    c_cassette.ShowDialog();
                    cmbTransactionReason.SelectedIndex = 0;
                }
                else
                {
                    _IsFinalDrop = true;
                    showModel(DropVault);
                    _IsFinalDrop = false;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
            }
        }

        void NoVault()
        {
            GetVaultDetails(true);
        }

        private void cmbTransactionReason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbl_Status.Text = string.Empty;
        }

        private void ucValueCalc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbl_Status.Text = string.Empty;
        }


        #endregion

        #region OtherMethods

        void FillVault()
        {
            try
            {

                //Fill transaction     
                Dispatcher.Invoke(new Action(() =>
                {
                    if (cmbTransactionReason.SelectedIndex < 1)
                    {
                        lbl_Status.Text = Application.Current.FindResource("Vault_MessageID6") as string;
                        cmbTransactionReason.Focus();
                        return;
                    }
                    if (_IsStandardFill)
                    {
                        ucValueCalc.ValueText = VaultStandardFillAmount.ToString();
                    }

                    try // In case of error proceed with fill 
                    {
                        if (Decimal.Parse(_DrVaultBalance["Capacity"].ToString()) < (Decimal.Parse(ucValueCalc.ValueText) + Decimal.Parse(txt_CurrentBalance.Text)))
                        {
                            lbl_Status.Text = Application.Current.FindResource("Vault_MessageID17") as string + _DrVaultBalance["Capacity"].ToString();
                            return;
                        }

                    }
                    catch (Exception Ex)
                    {
                        ExceptionManager.Publish(Ex);
                    }
                    if (ucValueCalc.ValueText == string.Empty || Decimal.Parse(ucValueCalc.ValueText) <= 0)
                    {
                        if (_FillType.ToLower() == "adjustment")
                            lbl_Status.Text = Application.Current.FindResource("Vault_MessageID11") as string;
                        else
                            lbl_Status.Text = Application.Current.FindResource("Vault_MessageID3") as string;
                        return;
                    }
                    string strMsg = _IsStandardFill ? "standard fill" : ((_FillType.ToLower() == "adjustment") ? "positive adjustment" : _FillType.ToLower());
                    
                    System.Windows.Forms.DialogResult dlgResult = new System.Windows.Forms.DialogResult();
                    if (Settings.ShowVaultConfirmMessage)
                    {
                        dlgResult = MessageBox.ShowBox("Vault_MessageID8", BMC_Icon.Question, BMC_Button.YesNo, strMsg.ToLower(), ExtensionMethods.GetCurrencySymbol(string.Empty) + " " + ucValueCalc.ValueText);
                        if (dlgResult == System.Windows.Forms.DialogResult.No)
                        {
                            ucValueCalc.ValueText = "0.00";
                            ucValueCalc.s_UnformattedText = "";
                            return;
                        }
                    }

                    int iResult = 0;
                    LogManager.WriteLog("CFillVault:Start FillVault ", LogManager.enumLogLevel.Debug);
                    DataRow Dr = objVault.FillVault(int.Parse(cmb_DeviceName.SelectedValue.ToString()), Decimal.Parse(ucValueCalc.ValueText), Decimal.Parse(txt_CurrentBalance.Text), 0, _FillType, int.Parse(cmbTransactionReason.SelectedValue.ToString()), ref iResult, _sendAlert).Rows[0];

                    LogManager.WriteLog("CFillVault:FillVault Complete ", LogManager.enumLogLevel.Debug);
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        AuditModuleName = ModuleName.Vault,
                        Audit_Screen_Name = "Vault Transaction",
                        Audit_Desc = "Updating Vault:ID " + cmb_DeviceName.SelectedValue.ToString() + " Transaction : " + _FillType + " Device Name:" + cmb_DeviceName.Text + " Amount:" + ucValueCalc.ValueText,
                        AuditOperationType = OperationType.ADD,
                    });

                    _DrVaultBalance = Dr;

                    if (Settings.ShowVaultPrintMessage)
                    {
                        //Get slip print confirmation
                        dlgResult = MessageBox.ShowBox("Vault_MessageID10", BMC_Icon.Question, BMC_Button.YesNo, "");
                        if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.Print(Dr, strMsg);
                        }
                    }
                    txt_SerialNo.Text = Dr["Serial_NO"].ToString();
                    txt_AlertLevel.Text = Dr["Alert_Level"].ToString();
                    //txt_FillAmount.Text = Dr["FillAmount"].ToString();
                    //txt_TotalFillAmount.Text = Dr["TotalAmountOnFill"].ToString();
                    txt_CurrentBalance.Text = Dr["CurrentBalance"].ToString();
                    txt_Manufacturer.Text = Dr["Manufacturer_Name"].ToString();
                    txt_Type.Text = Dr["Type_Prefix"].ToString();
                    ucValueCalc.s_UnformattedText = string.Empty;
                    lbl_Status.Text = string.Empty;
                    ucValueCalc.txtDisplay.Clear();
                    ucValueCalc.textBox1.Clear();
                    ucValueCalc.ValueText = string.Empty;
                    DisableControls();
                    cmbTransactionReason.SelectedIndex = 0;
                    lbl_Status.Text = Application.Current.FindResource("Vault_MessageID1") as string;

                }));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            //RefreshVaultBalance();
        }

        void BleedVault()
        {

            try
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    if (cmbTransactionReason.SelectedIndex < 1)
                    {
                        lbl_Status.Text = Application.Current.FindResource("Vault_MessageID6") as string;
                        cmbTransactionReason.Focus();
                        return;
                    }
                    if (ucValueCalc.ValueText == string.Empty || Decimal.Parse(ucValueCalc.ValueText) <= 0)
                    {
                        if (_FillType.ToLower() == "adjustment")
                            lbl_Status.Text = Application.Current.FindResource("Vault_MessageID11") as string;
                        else
                            lbl_Status.Text = Application.Current.FindResource("Vault_MessageID4") as string;
                        return;
                    }
                    //Return if bleed amount greater than current balance
                    if (Decimal.Parse(ucValueCalc.ValueText) > Decimal.Parse(txt_CurrentBalance.Text))
                    {
                        RefreshVaultBalance();
                        lbl_Status.Text = Application.Current.FindResource("Vault_MessageID7") as string;
                        return;
                    }
                    string strMsg = (_FillType.ToLower() == "adjustment") ? "negative adjustment" : _FillType.ToLower();
                    System.Windows.Forms.DialogResult dlgResult = new System.Windows.Forms.DialogResult();
                    if (Settings.ShowVaultConfirmMessage)
                    {
                        dlgResult = MessageBox.ShowBox("Vault_MessageID8", BMC_Icon.Question, BMC_Button.YesNo, strMsg, ExtensionMethods.GetCurrencySymbol(string.Empty) + " -" + ucValueCalc.ValueText);
                        if (dlgResult == System.Windows.Forms.DialogResult.No)
                        {
                            ucValueCalc.ValueText = "0.00";
                            ucValueCalc.s_UnformattedText = "";
                            return;
                        }
                    }
                    int iResult = 0;
                    LogManager.WriteLog("CFillVault:Start Adjust Value:" + ucValueCalc.ValueText, LogManager.enumLogLevel.Debug);
                    DataRow Dr = objVault.FillVault(int.Parse(cmb_DeviceName.SelectedValue.ToString()), Decimal.Parse(ucValueCalc.ValueText), Decimal.Parse(txt_CurrentBalance.Text), 1, _FillType, int.Parse(cmbTransactionReason.SelectedValue.ToString()), ref iResult, false).Rows[0];
                    if (iResult == -1)
                    {
                        RefreshVaultBalance();
                        lbl_Status.Text = Application.Current.FindResource("Vault_MessageID7") as string;
                        return;
                    }
                    LogManager.WriteLog("CFillVault:Start Audit ", LogManager.enumLogLevel.Debug);
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {
                        AuditModuleName = ModuleName.Vault,
                        Audit_Screen_Name = "Vault Transaction",
                        Audit_Desc = "Updating Vault  ID: " + cmb_DeviceName.SelectedValue.ToString() + " Transaction : " + _FillType + " Device Name:" + cmb_DeviceName.Text + " Amount: -" + ucValueCalc.ValueText,
                        AuditOperationType = OperationType.ADD,
                    });

                    _DrVaultBalance = Dr;
                    if (Settings.ShowVaultPrintMessage)
                    {
                        //Get slip print confirmation
                        dlgResult = MessageBox.ShowBox("Vault_MessageID10", BMC_Icon.Question, BMC_Button.YesNo, "");
                        if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.Print(Dr, strMsg);
                        }
                    }
                    txt_SerialNo.Text = Dr["Serial_NO"].ToString();
                    txt_AlertLevel.Text = Dr["Alert_Level"].ToString();
                    //txt_FillAmount.Text = Dr["FillAmount"].ToString();
                    //txt_TotalFillAmount.Text = Dr["TotalAmountOnFill"].ToString();
                    txt_CurrentBalance.Text = Dr["CurrentBalance"].ToString();
                    txt_Manufacturer.Text = Dr["Manufacturer_Name"].ToString();
                    txt_Type.Text = Dr["Type_Prefix"].ToString();
                    ucValueCalc.s_UnformattedText = string.Empty;
                    lbl_Status.Text = string.Empty;
                    ucValueCalc.txtDisplay.Clear();
                    ucValueCalc.textBox1.Clear();
                    ucValueCalc.ValueText = string.Empty;

                    //Return if bleed amount greater than current balance(DB CHECK)
                    if (iResult == -1)
                    {
                        lbl_Status.Text = Application.Current.FindResource("Vault_MessageID7") as string;
                        return;
                    }

                    DisableControls();

                    cmbTransactionReason.SelectedIndex = 0;
                    lbl_Status.Text = Application.Current.FindResource("Vault_MessageID1") as string;

                }));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                throw;
            }
            //RefreshVaultBalance();
        }

        void RefreshVaultBalance()
        {


            Dispatcher.Invoke(new Action(() =>
            {

                LogManager.WriteLog("CFillVault:Start RefreshVaultBalance ", LogManager.enumLogLevel.Debug);
                int result = 0;
                if (cmb_DeviceName.SelectedItem == null && cmb_DeviceName.Items.Count > 0)
                {
                    cmb_DeviceName.SelectedItem = cmb_DeviceName.Items[0];
                }
                else if (cmb_DeviceName.SelectedItem == null && cmb_DeviceName.Items.Count == 0)
                {
                    return;
                }
                DataTable dt_balance = objVault.GetBalance(int.Parse(cmb_DeviceName.SelectedValue.ToString()), ref result);
                if (result == -2)
                {
                    MessageBox.ShowBox("Vault_MessageID19", BMC_Icon.Error);
                    return;
                }

                if (dt_balance == null || dt_balance.Rows.Count == 0)
                {
                    GetVaultDetails(false);
                    return;
                }

                DataRow dr_bal = dt_balance.Rows[0];
                _DrVaultBalance = dr_bal;
                txt_SerialNo.Text = dr_bal["Serial_NO"].ToString();
                txt_AlertLevel.Text = dr_bal["Alert_Level"].ToString();
                //txt_FillAmount.Text = dr_bal["FillAmount"].ToString();
                //txt_TotalFillAmount.Text = dr_bal["TotalAmountOnFill"].ToString();
                txt_CurrentBalance.Text = dr_bal["CurrentBalance"].ToString();
                txt_Manufacturer.Text = dr_bal["Manufacturer_Name"].ToString();
                txt_Type.Text = dr_bal["Type_Prefix"].ToString();
                VaultStandardFillAmount = Convert.ToDecimal(dr_bal["StandardFillAmount"]);
                ucValueCalc.s_UnformattedText = string.Empty;
                lbl_Status.Text = string.Empty;
                ucValueCalc.txtDisplay.Clear();
                ucValueCalc.textBox1.Clear();
                ucValueCalc.ValueText = string.Empty;

                DisableControls();
                _IsWebServiceEnabled = (dr_bal["IsWebServiceEnabled"].ToString().ToLower() == "true");
                if (_IsWebServiceEnabled)
                {
                    lbl_CurrentBalance.Content = Application.Current.FindResource("CFillVault_lbl_CurrentBalanceVault") as string;
                    ucValueCalc.IsEnabled = false;
                }
                else
                {
                    lbl_CurrentBalance.Content = Application.Current.FindResource("CFillVault_lbl_CurrentBalance") as string;
                    ucValueCalc.IsEnabled = true;
                }
                lbl_CurrentBalance.Content += Application.Current.FindResource("App_CurrencyCulture") as string + " : ";

            }));
        }

        void DropVault()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (cmbTransactionReason.SelectedIndex < 1)
                {
                    lbl_Status.Text = Application.Current.FindResource("Vault_MessageID6") as string;
                    cmbTransactionReason.Focus();
                    return;
                }
                System.Windows.Forms.DialogResult dlgResult = new System.Windows.Forms.DialogResult();
                if (Settings.ShowVaultConfirmMessage)
                {
                    dlgResult = MessageBox.ShowBox("Vault_MessageID9", BMC_Icon.Question, BMC_Button.OKCancel, "");
                    if (dlgResult == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }

                LogManager.WriteLog("CFillVault:Start Drop Vault ", LogManager.enumLogLevel.Debug);


                int result = 0;
                DataTable dt_balance = objVault.DropVault(int.Parse(cmb_DeviceName.SelectedValue.ToString()), int.Parse(cmbTransactionReason.SelectedValue.ToString()), _IsFinalDrop, ref result);
                if (result == -2)
                {
                    MessageBox.ShowBox("Vault_MessageID19", BMC_Icon.Error);
                    return;
                }

                DataRow dr_bal = dt_balance.Rows[0];
                _DrVaultBalance = dr_bal;
                if (Settings.ShowVaultPrintMessage)
                {
                    //Get slip print confirmation
                    dlgResult = MessageBox.ShowBox("Vault_MessageID10", BMC_Icon.Question, BMC_Button.YesNo, "");
                    if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.PrintDrop(dr_bal);
                    }
                }

                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    AuditModuleName = ModuleName.Vault,
                    Audit_Screen_Name = "Vault Transaction",
                    Audit_Desc = "Droping Vault  ID: " + cmb_DeviceName.SelectedValue.ToString() + " Transaction : " + _FillType + " Device Name:" + cmb_DeviceName.Text,
                    AuditOperationType = OperationType.ADD,
                });
                if (_IsFinalDrop)
                {
                    GetVaultDetails(false);
                    return;
                }
                txt_SerialNo.Text = dr_bal["Serial_NO"].ToString();
                txt_AlertLevel.Text = dr_bal["Alert_Level"].ToString();
                //txt_FillAmount.Text = dr_bal["FillAmount"].ToString();
                //txt_TotalFillAmount.Text = dr_bal["TotalAmountOnFill"].ToString();
                txt_CurrentBalance.Text = dr_bal["CurrentBalance"].ToString();
                txt_Manufacturer.Text = dr_bal["Manufacturer_Name"].ToString();
                txt_Type.Text = dr_bal["Type_Prefix"].ToString();
                ucValueCalc.s_UnformattedText = string.Empty;
                lbl_Status.Text = string.Empty;
                ucValueCalc.txtDisplay.Clear();
                ucValueCalc.textBox1.Clear();
                ucValueCalc.ValueText = string.Empty;

                DisableControls();



                cmbTransactionReason.SelectedIndex = 0;
                lbl_Status.Text = Application.Current.FindResource("Vault_MessageID1") as string;
                LogManager.WriteLog("CFillVault:Drop Vault Complete", LogManager.enumLogLevel.Debug);
                if (!_IsFinalDrop && _IsStandardFillHasAccess)
                {
                    System.Windows.Forms.DialogResult dlgResultStandardDrop = MessageBox.ShowBox("Vault_MessageID18", BMC_Icon.Question, BMC_Button.YesNo, "");
                    if (dlgResultStandardDrop == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            //Fill  standard amount
                            LogManager.WriteLog("CFillVault:Start standard Fill ", LogManager.enumLogLevel.Debug);
                            cmbTransactionReason.SelectedValue = InitialFillValue;
                            _IsStandardFill = true;
                            _FillType = "STANDARDFILL";
                            showModel(FillVault);
                            _IsStandardFill = false;
                        }
                        catch (Exception Ex)
                        {
                            _IsStandardFill = false;
                            ExceptionManager.Publish(Ex);
                            lbl_Status.Text = Application.Current.FindResource("Vault_MessageID2") as string;
                        }
                    }
                }


            }));
        }

        void showModel(Action Method)
        {
            ModalProgressBar dig = null;
            try
            {

                dig = new ModalProgressBar(Method);
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

        void DisableControls()
        {
            if (decimal.Parse(txt_CurrentBalance.Text) <= 0)
            {
                btn_Adjustment_Negative.Visibility = Visibility.Hidden;
                btn_Bleed.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_Adjustment_Negative.Visibility = Visibility.Visible;
                btn_Bleed.Visibility = Security.SecurityHelper.HasAccess("BMC.Presentation.CFillVault.btnVaultBleed") ? Visibility.Visible : Visibility.Hidden;
            }
        }


        private List<DenomCombo> LoadDenom()
        {
            List<DenomCombo> lst_denom = new List<DenomCombo>();
            try
            {

                string[] strDenom = ConfigurationManager.AppSettings[ExtensionMethods.CurrentSiteCulture + "_Denom"].ToString().Split(':');
                string[] strCassetteDenom = (strDenom.Count() > 0) ? strDenom[0].Split(',') : null;
                string[] strHopperDenom = (strDenom.Count() > 1) ? strDenom[1].Split(',') : null;
                if (strCassetteDenom != null)
                {
                    foreach (string str in strCassetteDenom)
                    {
                        int val = 0;
                        if (int.TryParse(str, out val))
                        {
                            lst_denom.Add(new DenomCombo { DenomValue = val, CassetteTypes = (int)CassetteTypes.Cassette });
                        }
                    }
                }
                if (strHopperDenom != null)
                {
                    foreach (string str in strHopperDenom)
                    {
                        float val = 0f;
                        if (float.TryParse(str, out val))
                        {
                            lst_denom.Add(new DenomCombo { DenomValue = val, CassetteTypes = (int)CassetteTypes.Hopper });
                        }

                    }
                }
                lst_denom.Add(new DenomCombo { DenomValue = 1, CassetteTypes = (int)CassetteTypes.RejectionCassette });
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return lst_denom;
        }
        #endregion





    }
}
