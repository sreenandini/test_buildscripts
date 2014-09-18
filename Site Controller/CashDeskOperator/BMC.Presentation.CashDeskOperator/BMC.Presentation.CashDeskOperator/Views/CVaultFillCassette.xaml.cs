using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator;
using BMC.Common.Utilities;
using BMC.Security;
using System.Text.RegularExpressions;
using System.Configuration;
using Audit.BusinessClasses;
using Audit.Transport;
using System.Globalization;
using BMC.Presentation.POS.Helper_classes;


namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CVaultFillCassette.xaml
    /// </summary>
    public partial class CVaultFillCassette : Window
    {

        #region PrivateVariables
        private VaultTransactionType _VType;
        private List<GetNGADetailsResult> lst_NGADetails;
        private const string stitle = "CVaultFillCassette--> ";
        private int _vaultid = 0;
        private int _withdrawalflag = 0;
        private string _transactiontype;
        private int _transaction_reason_id = 0;
        private string strKeyText = "";
        private bool _isdroptransaction = false;
        private bool _isfinaldrop = false;
        private bool _isnegativechange = false;
        private bool _isadjustment = false;
        private string _VaultName = "";
        private IDictionary<VaultTransactionType, string> dic_transtype = null;
        private Action _act_refresh = null;
        private List<DenomCombo> _lst_denoms = null;
        private bool IsAmountEditable = false;
        private string sFormat = "";
        private bool _DropFollowedByFill = false;

        #endregion

        #region Constructor
        /// <summary>
        /// Do Following Transactions [Fill,Bleed,Drop,Adjustment]
        /// </summary>
        /// <param name="vault_ID">Specify Device ID </param>
        /// <param name="VaultName">Specify Device Name </param>
        /// <param name="withdrawalflag">0 - [Fill], 1 - [Bleed,-ve adjustment]</param>
        /// <param name="vtype">Specify VaultTransactionType</param>
        /// <param name="transactiontype">Type must be "FILL" or "DROP"</param>
        /// <param name="transaction_reason_id">1- [Fill], 4-[Drop]</param>
        public CVaultFillCassette(int vault_ID, string VaultName, int withdrawalflag,
            VaultTransactionType vtype, string transactiontype, int transaction_reason_id,
            string Manufacturer, string TypePrefix, string TransactionReason, List<DenomCombo> lst_denoms, bool DropFollowedByFill, Action act_refresh)
        {
            InitializeComponent();

            _vaultid = vault_ID;
            _VaultName = VaultName;
            _withdrawalflag = withdrawalflag;
            _transactiontype = transactiontype;
            _transaction_reason_id = transaction_reason_id;
            _VType = vtype;
            _act_refresh = act_refresh;
            _isfinaldrop = (_VType == VaultTransactionType.FinalDrop);
            _isdroptransaction = (_VType == VaultTransactionType.StandardDrop) || _isfinaldrop;
            _isadjustment = (_VType == VaultTransactionType.NegativeAdjustment || _VType == VaultTransactionType.PositiveAdjustment);
            _isnegativechange = (_VType == VaultTransactionType.NegativeAdjustment || _VType == VaultTransactionType.Bleed);
            _lst_denoms = lst_denoms;
            txtManufacturer.Text = Manufacturer;
            txtTypePrefix.Text = TypePrefix;
            txtVault.Text = VaultName;
            txtNotes.Text = TransactionReason;
            txtNotes.IsEnabled = false;
            _DropFollowedByFill = DropFollowedByFill;
        }
        #endregion


        #region Form Events

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                IsAmountEditable = Settings.IsBillCounterAmountEditable;
                LoadDefaultTransactionType();
                LoadVaultCassetteDetails(_vaultid);
                LoadRegionSettings();
                if (_isdroptransaction)
                {
                    g_cassettedetails.Columns[3].Width = 0;
                    g_cassettedetails.Columns[4].Width = 0;
                    g_cassettedetails.Columns[6].Width = 0;
                    g_cassettedetails.Columns[2].Width = g_cassettedetails.Columns[2].Width + 50;
                    g_cassettedetails.Columns[5].Width = g_cassettedetails.Columns[5].Width + 50;
                }
                else if (_isadjustment)
                {
                    g_cassettedetails.Columns[6].Width = 0;
                }
                LogManager.WriteLog("CVaultFillCassette:UserControl_Loaded TransactionType:" + _VType.ToString(), LogManager.enumLogLevel.Debug);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                MessageBox.ShowBox("Vault_MessageID2", BMC_Icon.Error);
            }

        }

        /// <summary>
        ///  Loads currency based on the region settings 
        /// </summary>
        private void LoadRegionSettings()
        {
            string sCurrencySymbol = string.Empty;
            try
            {
                sCurrencySymbol = "".GetCurrencySymbol();

                //lbl_GrandTotal.Text = string.Format("{0} ({1})", lbl_GrandTotal.Text, sCurrencySymbol);
                LogManager.WriteLog("CVaultFillCassette->LoadRegionSettings() Culture:  " + ExtensionMethods.CurrentSiteCulture, LogManager.enumLogLevel.Debug);

                var d = Convert.ToDecimal(1.1);

                decimal.TryParse(d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out d);
                sFormat = d.ToString(new System.Globalization.CultureInfo(ExtensionMethods.CurrentCurrenyCulture)).Substring(1, 1);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                LogManager.WriteLog("CVaultFillCassette->LoadRegionSettings() Culture:  " + "Loading default Currency", LogManager.enumLogLevel.Error);

            }


        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (lst_CassetteDetails.SelectedItem != null)
                {
                    TextBox txt_value = sender as TextBox;
                    if (IsAmountEditable)
                    {
                        txt_value.TextChanged -= TextBox_TextChanged;
                        return;
                    }

                    int Cassette_ID = Convert.ToInt32("0" + txt_value.Tag);
                    if (Cassette_ID > 0)
                    {
                        GetNGADetailsResult sel_Cassette = lst_NGADetails.Find(obj => obj.Cassette_ID == Cassette_ID);
                        sel_Cassette.Amount = Convert.ToInt32("0" + txt_value.Text) * Convert.ToDecimal(sel_Cassette.Denom);

                        sel_Cassette.Total = _isnegativechange ? (sel_Cassette.CurrentBalance ?? 0) - sel_Cassette.Amount :
                            sel_Cassette.Amount + (sel_Cassette.CurrentBalance ?? 0);


                        string ErrorMsg = "";
                        object defaultValue = null;
                        if (!Validate(sel_Cassette, ref defaultValue, ref ErrorMsg))
                        {
                            MessageBox.ShowBox(ErrorMsg, BMC_Icon.Error, true);
                            txt_value.Text = defaultValue.ToString();
                        }
                        else
                        {
                            CalculateTotal();

                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void cmb_Denom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lst_NGADetails != null)
                {
                    ComboBox cmb_val = sender as ComboBox;
                    int Cassette_ID = Convert.ToInt32("0" + cmb_val.Tag);
                    DenomCombo d_val = cmb_val.SelectedItem as DenomCombo;
                    if (Cassette_ID > 0 && d_val != null)
                    {
                        GetNGADetailsResult sel_Cassette = lst_NGADetails.Find(obj => obj.Cassette_ID == Cassette_ID);

                        sel_Cassette.Amount = Convert.ToDecimal(d_val.DenomValue * sel_Cassette.Quantity);
                        CalculateTotal();
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void chkCassetteAll_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lst_NGADetails == null)
                    return;
                CheckBox chk = sender as CheckBox;


                foreach (GetNGADetailsResult sel_Cassette in lst_NGADetails)
                {
                    if (sel_Cassette.EnableControls)
                    {
                        if (IsFillRejection(sel_Cassette.Cassette_Type, sel_Cassette.FillRejection))
                        {
                            sel_Cassette.IsChecked = chk.IsChecked.Value;
                        }
                    }
                }

                CalculateTotal();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private bool IsFillRejection(int CassetteTypeID, bool FillRejection)
        {
            if (CassetteTypeID == (int)CassetteTypes.RejectionCassette)
            {
                if (_VType == VaultTransactionType.StandardFill || _VType == VaultTransactionType.Fill)
                {
                    return FillRejection;
                }
                else if (_VType == VaultTransactionType.Bleed)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void CalculateTotal()
        {
            decimal tot_amount = lst_NGADetails.Where(obj => obj.EnableControls && obj.IsChecked).Sum(obj => obj.Amount);
            decimal currentinventorytotal = lst_NGADetails.Where(obj => obj.EnableControls).Sum(obj => (obj.CurrentBalance ?? 0));
            lst_NGADetails.Last().Amount = tot_amount;
            lst_NGADetails.Last().Total = _isnegativechange ? (currentinventorytotal - tot_amount) : (tot_amount + currentinventorytotal);
        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                LogManager.WriteLog(stitle + "btnSubmit_Click()", LogManager.enumLogLevel.Debug);
                int error_flag = 0;
                string ErrorMsg = "";
                string CassetteInfo = "";
                bool sendAlert = false;
                System.Windows.Forms.DialogResult dlgResult = new System.Windows.Forms.DialogResult();
                XElement Cassette_xml = FrameXml(_isdroptransaction);
                if (!lst_NGADetails.Any(obj => obj.IsChecked))
                {
                    MessageBox.ShowBox("VaultCassette_MessageID6", BMC_Icon.Information);
                    return;
                }

                #region Validate
                if (!_isdroptransaction)
                {
                    if (IsAmountEditable && !ValidateAmount())
                        return;
                    if (!Validate(ref ErrorMsg))
                    {
                        MessageBox.ShowBox(ErrorMsg, BMC_Icon.Error, true);
                        return;
                    }
                    object defaultValue = null;
                    bool check_onetime = false;
                    foreach (GetNGADetailsResult sel_Cassette in lst_NGADetails)
                    {
                        if (sel_Cassette.IsChecked)
                        {
                            CassetteInfo += "CassetteName: " + sel_Cassette.Cassette_Name + ", Amount: " + sel_Cassette.Amount + "; ";  //for audit purpose
                            if (_VType == VaultTransactionType.StandardFill && (!check_onetime))
                            {
                                if (sel_Cassette.Amount != sel_Cassette.OldAmount)
                                {
                                    dlgResult = MessageBox.ShowBox("Vault_MessageID21", BMC_Icon.Question, BMC_Button.YesNo, "");
                                    if (dlgResult == System.Windows.Forms.DialogResult.No)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        check_onetime = true;
                                    }
                                }
                            }
                            else if (_VType == VaultTransactionType.PositiveAdjustment)
                            {
                                if (sel_Cassette.Amount < sel_Cassette.CurrentBalance.Value)
                                {
                                    ErrorMsg = Application.Current.FindResource("VaultCassette_MessageID10").ToString()
                                                .Replace("*****", sel_Cassette.Cassette_Name)
                                                .Replace("@@@@@", sel_Cassette.CurrentBalance.Value.GetUniversalCurrencyFormatWithSymbol());
                                    MessageBox.ShowBox(ErrorMsg, BMC_Icon.Error, true);
                                    lst_CassetteDetails.SelectedItem = sel_Cassette;
                                    return;
                                }
                            }
                            else if (_VType == VaultTransactionType.NegativeAdjustment)
                            {
                                if (sel_Cassette.Amount > sel_Cassette.CurrentBalance.Value)
                                {
                                    ErrorMsg = Application.Current.FindResource("VaultCassette_MessageID11").ToString()
                                               .Replace("*****", sel_Cassette.Cassette_Name)
                                               .Replace("@@@@@", sel_Cassette.CurrentBalance.Value.GetUniversalCurrencyFormatWithSymbol());
                                    MessageBox.ShowBox(ErrorMsg, BMC_Icon.Error, true);
                                    lst_CassetteDetails.SelectedItem = sel_Cassette;
                                    return;
                                }
                            }

                            if (!Validate(sel_Cassette, ref defaultValue, ref ErrorMsg))
                            {
                                MessageBox.ShowBox(ErrorMsg, BMC_Icon.Error, true);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    //for audit purpose
                    foreach (GetNGADetailsResult sel_Cassette in lst_NGADetails)
                    {
                        if (sel_Cassette.IsChecked)
                        {
                            CassetteInfo += "CassetteName: " + sel_Cassette.Cassette_Name + ", Amount: " + sel_Cassette.Amount + "; ";
                        }
                    }
                }

                if (_VType == VaultTransactionType.StandardFill)
                {
                    if (Vault.CreateInstance().IsStandardFillDoneTwice(_vaultid))
                    {
                        dlgResult = MessageBox.ShowBox("Vault_MessageID20", BMC_Icon.Question, BMC_Button.YesNo, "");
                        if (dlgResult == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            sendAlert = true;
                        }
                    }
                }
                #endregion

                bool result = false;
                usp_Vault_DropResult v_droprep = null;
                usp_Vault_FillVaultResult v_fillrep = null;
                decimal tot_amount = 0;
                if (_isdroptransaction)
                {
                    if (Settings.ShowVaultConfirmMessage)
                    {
                        dlgResult = MessageBox.ShowBox("Vault_MessageID9", BMC_Icon.Question, BMC_Button.YesNo, "");
                    }

                }
                else
                {
                    tot_amount = lst_NGADetails.Where(obj => obj.IsChecked).Sum(obj => obj.Amount);
                    if (Settings.ShowVaultConfirmMessage)
                    {
                        dlgResult = MessageBox.ShowBox("Vault_MessageID8", BMC_Icon.Question, BMC_Button.YesNo, dic_transtype[_VType].ToLower(),
                            ExtensionMethods.GetCurrencySymbol(string.Empty) + ((_VType == VaultTransactionType.Bleed) ? " -" : " ") + tot_amount);
                    }
                }
                if (!Settings.ShowVaultConfirmMessage)
                {
                    dlgResult = System.Windows.Forms.DialogResult.Yes;
                }
                if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                {
                    if (_isdroptransaction)
                    {
                        result = Vault.CreateInstance().DropVaultCassettes(_vaultid, SecurityHelper.CurrentUser.SecurityUserID,
                            _transaction_reason_id, Cassette_xml, _isfinaldrop, ref error_flag, ref  v_droprep);

                        if (result)
                        {


                            LogManager.WriteLog(stitle + "Drop Succeeded, VaultID: " + _vaultid + " IsFinalDrop: " + _isfinaldrop, LogManager.enumLogLevel.Debug);

                            ///<Cassette Cassette_ID="18" Denom="50" FillAmount="100.00
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                AuditModuleName = ModuleName.Vault,
                                Audit_Screen_Name = "Vault Transaction",
                                Audit_Desc = "Droping Vault  ID: " + _vaultid + " Transaction : " + _transactiontype +
                                " Device Name:" + _VaultName + "Cassette_info:" + CassetteInfo,
                                AuditOperationType = OperationType.ADD,
                            });
                        }
                    }
                    else
                    {

                        if (_VType == VaultTransactionType.NegativeAdjustment)
                        {
                            tot_amount = lst_NGADetails.Where(obj => obj.IsChecked).Sum(obj => (obj.CurrentBalance.Value - obj.Amount));

                        }
                        else if (_VType == VaultTransactionType.PositiveAdjustment)
                        {
                            tot_amount = lst_NGADetails.Where(obj => obj.IsChecked).Sum(obj => (obj.Amount - obj.CurrentBalance.Value));

                        }
                        result = Vault.CreateInstance().FillAmountinCassettes(_vaultid, tot_amount, 0m,
                           SecurityHelper.CurrentUser.SecurityUserID, _withdrawalflag, _transactiontype,
                           true, _transaction_reason_id, Cassette_xml, ref error_flag, ref v_fillrep, sendAlert);
                        if (result)
                        {

                            LogManager.WriteLog(stitle + "Transaction Succeeded, VaultID: " + _vaultid + " Type: " + _transactiontype + " Amount: " + tot_amount, LogManager.enumLogLevel.Debug);

                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                AuditModuleName = ModuleName.Vault,
                                Audit_Screen_Name = "Vault Transaction",
                                Audit_Desc = "Updating Vault:ID " + _vaultid + " Transaction : " + _transactiontype + " Device Name:" + _VaultName + " Amount:" + tot_amount + " Cassette_info:" + CassetteInfo,
                                AuditOperationType = OperationType.ADD,
                            });
                        }
                    }
                    if (result)
                    {
                        if (Settings.ShowVaultSuccessMessage)
                        {
                            MessageBox.ShowBox("VaultCassette_MessageID3", BMC_Icon.Information);
                        }

                        if (Settings.ShowVaultPrintMessage)
                        {
                            dlgResult = MessageBox.ShowBox("Vault_MessageID10", BMC_Icon.Question, BMC_Button.YesNo, "");
                            if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (_isdroptransaction)
                                {
                                    if (v_droprep != null)
                                        CreateVaultCassettesCurrentDropReport(v_droprep);
                                }
                                else
                                {
                                    if (v_fillrep != null)
                                        CreateVaultCassettesCurrentTransactionReport(v_fillrep, dic_transtype[_VType]);
                                }
                            }
                        }
                        if (_act_refresh != null)
                        {
                            _act_refresh();
                        }
                        this.Close();
                    }
                    else
                    {
                        ShowErrorStatus(error_flag);
                    }
                }


            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                MessageBox.ShowBox("Vault_MessageID2", BMC_Icon.Error);

            }
        }

        private void ShowErrorStatus(int error_flag)
        {
            switch (error_flag)
            {
                case -1:
                    MessageBox.ShowBox("Vault_MessageID7", BMC_Icon.Error);
                    LogManager.WriteLog(stitle + "Transaction Failed, VaultID: " + _vaultid + " Type: " + _transactiontype + " Reason: Bleed/Negative adjustment amount cannot be greater than current balance", LogManager.enumLogLevel.Debug);
                    if (_act_refresh != null)
                    {
                        _act_refresh();
                    }
                    break;
                case -2:
                    MessageBox.ShowBox("Vault_MessageID19", BMC_Icon.Error);
                    LogManager.WriteLog(stitle + "Transaction Failed, VaultID: " + _vaultid + " Type: " + _transactiontype + " Reason: Asset had been terminated.", LogManager.enumLogLevel.Debug);
                    if (_act_refresh != null)
                    {
                        _act_refresh();
                    }
                    break;
                case -3:
                    MessageBox.ShowBox("Vault_MessageID23", BMC_Icon.Error);
                    LogManager.WriteLog(stitle + "Transaction Failed, VaultID: " + _vaultid + " Type: " + _transactiontype + " Reason: Asset had been terminated.", LogManager.enumLogLevel.Debug);
                    if (_act_refresh != null)
                    {
                        _act_refresh();
                    }
                    break;
                case -4:
                    MessageBox.ShowBox("Vault_MessageID25", BMC_Icon.Error);
                    LogManager.WriteLog(stitle + "Transaction Failed, VaultID: " + _vaultid + " Type: " + _transactiontype + " Reason: Asset had been final dropped.", LogManager.enumLogLevel.Debug);
                    if (_act_refresh != null)
                    {
                        _act_refresh();
                    }
                    break;
                default:
                    LogManager.WriteLog(stitle + "Transaction Failed, VaultID: " + _vaultid + " Type: " + _transactiontype, LogManager.enumLogLevel.Debug);
                    MessageBox.ShowBox("VaultCassette_MessageID4", BMC_Icon.Error);
                    break;

            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void chkCassetteAll_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (_isfinaldrop || _isadjustment)
                e.Handled = true;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Prohibit space
            e.Handled = !IsNumberKey(e.Key) && !IsDelOrBackspaceOrTabKey(e.Key);
        }

        private void txt_amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt_value = sender as TextBox;
            try
            {
                if (lst_CassetteDetails.SelectedItem != null)
                {

                    if (!IsAmountEditable)
                    {
                        txt_value.TextChanged -= txt_amount_TextChanged;
                        return;
                    }
                }
                int Cassette_ID = Convert.ToInt32("0" + txt_value.Tag);
                if (Cassette_ID > 0)
                {
                    GetNGADetailsResult sel_Cassette = lst_NGADetails.Find(obj => obj.Cassette_ID == Cassette_ID);
                    sel_Cassette.Amount = Convert.ToDecimal("0" + txt_value.Text);

                    sel_Cassette.Total = _isnegativechange ? (sel_Cassette.CurrentBalance ?? 0) - sel_Cassette.Amount :
                        sel_Cassette.Amount + (sel_Cassette.CurrentBalance ?? 0);


                    string ErrorMsg = "";
                    object defaultValue = null;
                    if (!Validate(sel_Cassette, ref defaultValue, ref ErrorMsg))
                    {
                        MessageBox.ShowBox(ErrorMsg, BMC_Icon.Error, true);
                        txt_value.Text = defaultValue.ToString();
                    }
                    else
                    {
                        CalculateTotal();

                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txt_amount_KeyDown(object sender, KeyEventArgs e)
        {

            TextBox txt_value = sender as TextBox;
            try
            {

                if (!IsAmountEditable)
                {
                    txt_value.KeyDown -= txt_amount_KeyDown;
                    return;
                }

                int Cassette_ID = Convert.ToInt32("0" + txt_value.Tag);
                if (Cassette_ID > 0)
                {
                    GetNGADetailsResult sel_Cassette = lst_NGADetails.Find(obj => obj.Cassette_ID == Cassette_ID);

                    e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);
                    if (sel_Cassette.Cassette_Type == (int)CassetteTypes.Hopper)
                    {

                        if (e.Key == Key.OemComma && sFormat == ",")
                            e.Handled = false;

                        if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal) && sFormat == ".")
                            e.Handled = false;

                        if (txt_value.Text.IndexOf(sFormat) != -1)
                        {
                            string[] str_val = txt_value.Text.Split(sFormat[0]);
                            if (e.Key == Key.Decimal)
                            {
                                e.Handled = true;
                            }
                            else if (str_val[0].Length + 1 > 6 && (txt_value.Text.Length - txt_value.CaretIndex >= 3))
                            {
                                e.Handled = true;
                            }
                            else if (str_val[1].Length == 2)
                            {
                                if (txt_value.Text.Length - 2 <= txt_value.CaretIndex)
                                {
                                    e.Handled = true;
                                }
                            }
                            else if (txt_value.Text.Length > 9)
                            {
                                e.Handled = true;
                            }
                        }
                        else if (txt_value.Text.Length + 1 > 6 && e.Key != Key.Decimal && e.Key != Key.OemPeriod)
                        {
                            e.Handled = true;
                        }
                        else if (sFormat == "." && (txt_value.Text.Length - txt_value.CaretIndex >= 3))
                        {
                            e.Handled = true;
                        }
                    }
                    else if (txt_value.Text.Length + 1 > 6)
                    {
                        e.Handled = true;
                    }
                    if (e.Key == Key.Enter)
                    {
                        if (sel_Cassette != null)
                        {
                            string strValue = txt_value.Text;//.Length
                            if (strValue.Length > 6 && strValue.IndexOf('.') == -1)
                            {
                                strValue = strValue.Substring(0, 6);
                                txt_value.Text = strValue;
                            }
                            decimal declaredbal = Convert.ToDecimal(strValue);
                            //sel_Cassette.Amount = declaredbal;
                            decimal denom = Convert.ToDecimal(sel_Cassette.Denom);
                            sel_Cassette.Quantity = Convert.ToInt32(Math.Truncate((declaredbal / denom)));
                            if (declaredbal % denom != 0)
                            {
                                string strMsg = Application.Current.FindResource("VaultCassette_MessageID7").ToString().Replace("@@@@@", sel_Cassette.Cassette_Name);
                                sel_Cassette.Amount = declaredbal;
                                MessageBox.ShowBox(strMsg, BMC_Icon.Warning, true);

                                return;
                            }
                            sel_Cassette.Amount = denom * sel_Cassette.Quantity;

                        }
                    }


                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }

        }

        private void txt_amount_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TextBox txt_value = sender as TextBox;

                if (!IsAmountEditable)
                {
                    txt_value.PreviewMouseUp -= txt_amount_PreviewMouseUp;
                    return;
                }
                if (!Settings.OnScreenKeyboard)
                    return;
                if (lst_CassetteDetails.SelectedItem != null)
                {

                    int Cassette_ID = Convert.ToInt32("0" + txt_value.Tag);

                    if (Cassette_ID > 0)
                    {
                        GetNGADetailsResult sel_Cassette = lst_NGADetails.Find(obj => obj.Cassette_ID == Cassette_ID);
                        if (sel_Cassette != null)
                        {

                            bool isdecimal = sel_Cassette.Cassette_Type == (int)CassetteTypes.Hopper;
                            int maxlength = 9;
                            if (!isdecimal)
                            {
                                txt_value.Text = Convert.ToInt32(Convert.ToDecimal(txt_value.Text)).ToString();
                                maxlength = 6;
                            }
                            txt_value.Text = DisplayNumberPad(txt_value.Text.Trim(), isdecimal, maxlength);
                            txt_value.SelectionStart = txt_value.Text.Length;
                            decimal declaredbal = Convert.ToDecimal(txt_value.Text);
                            //sel_Cassette.Amount = declaredbal;
                            decimal denom = Convert.ToDecimal(sel_Cassette.Denom);
                            sel_Cassette.Quantity = Convert.ToInt32(Math.Truncate((declaredbal / denom)));

                            if (declaredbal % denom != 0)
                            {
                                string strMsg = Application.Current.FindResource("VaultCassette_MessageID7").ToString().Replace("@@@@@", sel_Cassette.Cassette_Name);
                                sel_Cassette.Amount = declaredbal;
                                MessageBox.ShowBox(strMsg, BMC_Icon.Warning, true);

                                return;
                            }
                            sel_Cassette.Amount = denom * sel_Cassette.Quantity;
                            //CalculateTotal(c_res.CassetteType_ID);

                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        /// <summary>
        /// Frame cassettes xml 
        /// </summary>
        /// <param name="IsDrop"></param>
        /// <returns></returns>
        private XElement FrameXml(bool IsDrop)
        {

            XElement xml_Cassette = new XElement("CassetteDetails");

            Func<GetNGADetailsResult, decimal> fn = (cassette) =>
                {
                    decimal d_retval = 0;
                    if (_VType == VaultTransactionType.NegativeAdjustment)
                    {
                        d_retval = cassette.CurrentBalance.Value - cassette.Amount;
                    }
                    else if (_VType == VaultTransactionType.PositiveAdjustment)
                    {
                        d_retval = cassette.Amount - cassette.CurrentBalance.Value;
                    }
                    else
                    {
                        d_retval = (IsDrop ? (cassette.CurrentBalance ?? 0) : cassette.Amount);
                    }
                    return d_retval;
                };

            foreach (GetNGADetailsResult cassette in lst_NGADetails)
            {
                if (cassette.IsChecked && cassette.EnableControls)
                {
                    decimal d_retval = fn(cassette);
                    if (d_retval != 0 || IsDrop)
                    {
                        xml_Cassette.Add(new XElement("Cassette",
                                                  new XAttribute("Cassette_ID", cassette.Cassette_ID),
                                                  new XAttribute("Denom", cassette.Denom),
                                                  new XAttribute("FillAmount", d_retval)));
                    }
                }

            }
            return xml_Cassette;
        }
        #endregion

        #region LoadMethods

        private void LoadVaultCassetteDetails(int VaultID)
        {
            try
            {
                LogManager.WriteLog(stitle + "LoadVaultCassetteDetails()", LogManager.enumLogLevel.Debug);


                lst_NGADetails = Vault.CreateInstance().GetCassetteDetails(_lst_denoms, VaultID, _isdroptransaction, _VType);

                if (lst_NGADetails != null)
                {
                    if (_VType == VaultTransactionType.StandardFill)
                    {
                        foreach (GetNGADetailsResult cassette in lst_NGADetails)
                        {
                            cassette.PropertyChanged -= CVaultFillCassette_PropertyChanged;
                            cassette.PropertyChanged += CVaultFillCassette_PropertyChanged;
                        }
                    }
                    lst_CassetteDetails.ItemsSource = lst_NGADetails;

                    if (dic_transtype != null)
                    {
                        txtHeader.Text = dic_transtype[_VType].ToString() + " - " + _VaultName;
                    }
                    this.DataContext = lst_NGADetails;
                    if (_DropFollowedByFill)
                    {
                        CheckOnlyStandardDrop();
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        void CVaultFillCassette_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "IsChecked")
                {
                    CalculateTotal();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void CheckOnlyStandardDrop()
        {
            try
            {
                foreach (GetNGADetailsResult cassette in lst_NGADetails)
                {
                    if (cassette.EnableControls)
                    {
                        cassette.IsChecked = IsFillRejection(cassette.Cassette_Type, cassette.FillRejection) && cassette.DroppedRecently;
                        if (!cassette.CanChangeDenom)
                        {
                            cassette.Quantity = 0;
                            cassette.Amount = 0;
                            CalculateTotal();
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private IDictionary<VaultTransactionType, string> LoadDefaultTransactionType()
        {
            dic_transtype = new Dictionary<VaultTransactionType, string>();
            dic_transtype.Add(new KeyValuePair<VaultTransactionType, string>(VaultTransactionType.Fill, "Fill"));
            dic_transtype.Add(new KeyValuePair<VaultTransactionType, string>(VaultTransactionType.StandardFill, "Standard Fill"));
            dic_transtype.Add(new KeyValuePair<VaultTransactionType, string>(VaultTransactionType.Bleed, "Bleed"));
            dic_transtype.Add(new KeyValuePair<VaultTransactionType, string>(VaultTransactionType.PositiveAdjustment, "Positive Adjustment"));
            dic_transtype.Add(new KeyValuePair<VaultTransactionType, string>(VaultTransactionType.NegativeAdjustment, "Negative Adjustment"));
            dic_transtype.Add(new KeyValuePair<VaultTransactionType, string>(VaultTransactionType.StandardDrop, "Standard Drop"));
            dic_transtype.Add(new KeyValuePair<VaultTransactionType, string>(VaultTransactionType.FinalDrop, "Final Drop"));
            return dic_transtype;
        }
        #endregion

        #region ValidateMethods
        private bool Validate(GetNGADetailsResult sel_Cassette, ref object defaultValue, ref string ErrorMsg)
        {
            bool retVal = true;
            try
            {
                switch (_VType)
                {
                    case VaultTransactionType.PositiveAdjustment:
                    case VaultTransactionType.StandardFill:
                    case VaultTransactionType.Fill:
                        if (sel_Cassette.Total > sel_Cassette.MaxFillAmount)
                        {
                            ErrorMsg = Application.Current.FindResource("VaultCassette_MessageID1").ToString()
                                .Replace("*****", sel_Cassette.Cassette_Name)
                                .Replace("@@@@@", sel_Cassette.MaxFillAmount.GetUniversalCurrencyFormatWithSymbol());
                            retVal = false;
                            defaultValue = 0;
                        }
                        break;
                    //case VaultTransactionType.NegativeAdjustment:
                    case VaultTransactionType.Bleed:
                        if (sel_Cassette.Amount > sel_Cassette.CurrentBalance.Value)
                        {
                            ErrorMsg = Application.Current.FindResource("VaultCassette_MessageID2").ToString()
                                .Replace("*****", sel_Cassette.Cassette_Name)
                                .Replace("@@@@@", sel_Cassette.CurrentBalance.Value.GetUniversalCurrencyFormatWithSymbol());
                            retVal = false;
                            defaultValue = 0;
                        }
                        break;

                }


            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                retVal = false;
            }
            return retVal;
        }

        private bool Validate(ref string ErrorMsg)
        {
            bool retVal = true;
            try
            {
                switch (_VType)
                {
                    case VaultTransactionType.FinalDrop:
                        break;

                    case VaultTransactionType.StandardFill:
                    case VaultTransactionType.Fill:
                    case VaultTransactionType.Bleed:
                        if (lst_NGADetails != null)
                        {
                            GetNGADetailsResult cassette = lst_NGADetails.Find(obj => obj.IsChecked && obj.Amount == 0);
                            if (cassette != null)
                            {
                                string strtemp = ((cassette.Cassette_Type == (int)CassetteTypes.Cassette) || (cassette.Cassette_Type == (int)CassetteTypes.RejectionCassette) ? "Cassette " : "Hopper ") + "[" + cassette.Cassette_Name + "]";
                                ErrorMsg = Application.Current.FindResource("VaultCassette_MessageID5").ToString().Replace("@@@@@", strtemp);
                                retVal = false;
                            }

                        }
                        break;
                }

                if (retVal && (_isadjustment))
                {
                    List<GetNGADetailsResult> lst_val = lst_NGADetails.Where(obj => obj.IsChecked && (obj.CurrentBalance.Value != obj.Amount)).ToList<GetNGADetailsResult>();
                    if (lst_val != null && lst_val.Count == 0)
                    {
                        ErrorMsg = Application.Current.FindResource("VaultCassette_MessageID12").ToString();
                        retVal = false;
                    }
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                retVal = false;
            }
            return retVal;
        }

        bool ValidateAmount()
        {
            bool retval = true;
            try
            {
                foreach (GetNGADetailsResult cassette in lst_NGADetails)
                {
                    if (cassette.EnableControls)
                    {
                        if (cassette.Amount % Convert.ToDecimal(cassette.Denom) != 0)
                        {
                            string strMsg = Application.Current.FindResource("VaultCassette_MessageID7").ToString().Replace("@@@@@", cassette.Cassette_Name);
                            MessageBox.ShowBox(strMsg, BMC_Icon.Warning, true);

                            lst_CassetteDetails.SelectedItem = cassette;
                            retval = false;
                            break;
                        }
                        else
                        {

                            cassette.Quantity = Convert.ToInt32(Math.Truncate(cassette.Amount / Convert.ToDecimal(cassette.Denom)));
                        }

                    }

                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                retval = false;
            }
            return retval;
        }
        #endregion

        #region VirtualKeyboardMethods

        private void txt_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!Settings.OnScreenKeyboard)
                    return;
                TextBox txtMouseUp = sender as TextBox;

                if (txtMouseUp.Name == "txtNotes")
                {
                    txtMouseUp.Text = DisplayKeyboard(txtMouseUp.Text);
                }
                else
                {
                    string strNumberPadValue = DisplayNumberPad(txtMouseUp.Text, false, 8);

                    txtMouseUp.Text = strNumberPadValue;

                }
                txtMouseUp.SelectionStart = txtMouseUp.Text.Length;


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public string DisplayKeyboard(string keyText)
        {
            strKeyText = "";
            KeyboardInterface objKeyboard = null;

            try
            {
                Window w = Window.GetWindow(this);
                Point pt = default(Point);
                Size sz = default(Size);
                if (w != null)
                {
                    pt = new Point(w.Left, w.Top);
                    sz = new Size(w.Width, w.Height);
                }

                objKeyboard = new KeyboardInterface();
                objKeyboard.Owner = w;
                objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
                objKeyboard.KeyString = keyText;
                objKeyboard.Top = pt.Y + (sz.Height - objKeyboard.Height);
                objKeyboard.Left = pt.X + (sz.Width / 2) - (objKeyboard.Width / 2);
                objKeyboard.ShowInTaskbar = false;
                objKeyboard.ShowDialog();

                if (objKeyboard != null)
                {
                    objKeyboard.Closing -= this.objKeyboard_Closing;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {

            }
            return strKeyText;
        }

        private string DisplayNumberPad(string keytext, bool IsCurrency, int MaxLength)
        {
            string strNumberPadText = keytext;
            NumberPadWind ObjNumberpadWind = null;
            try
            {
                ObjNumberpadWind = new NumberPadWind(IsCurrency);
                if (MaxLength > 0)
                    ObjNumberpadWind.setMaxLength(MaxLength);

                ObjNumberpadWind.ValueText = keytext;
                ObjNumberpadWind.ucTicketEntry.txtDisplay.IsReadOnly = true;

                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    if (ObjNumberpadWind.ValueText == "")
                    {
                        if (IsCurrency)
                            strNumberPadText = "0.0";
                        else
                            strNumberPadText = "0";
                    }
                    else
                    {
                        strNumberPadText = ObjNumberpadWind.ValueText;
                    }
                }
            }
            catch (Exception ex)
            {
                strNumberPadText = ObjNumberpadWind.ValueText;
                ObjNumberpadWind.Close();
                ExceptionManager.Publish(ex);
            }
            return strNumberPadText;
        }

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside objKeyboard_Closing", LogManager.enumLogLevel.Info);

                if (((KeyboardInterface)sender).DialogResult == true)
                {
                    strKeyText = ((KeyboardInterface)sender).KeyString;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsDelOrBackspaceOrTabKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Tab;
        }


        #endregion

        #region ReportsMethods
        public void CreateVaultCassettesCurrentTransactionReport(usp_Vault_FillVaultResult Vault, string TransactionType)
        {
            try
            {
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowVaultCurrentTransactionSlip(Vault.Vault_ID, Vault.Name, Vault.Serial_NO, Vault.Manufacturer_Name, Vault.Type_Prefix, SecurityHelper.CurrentUser.DisplayName,
                                                              Convert.ToDateTime(Vault.CreatedDate), Vault.IsWebServiceEnabled, Convert.ToDecimal(Vault.FillAmount),
                                                              Convert.ToDecimal(Vault.TotalAmountOnFill), Convert.ToDecimal(Vault.CurrentBalance), TransactionType);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.ShowDialog();
                }


                LogManager.WriteLog("Show Vault Transaction Slip Report Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void CreateVaultCassettesCurrentDropReport(usp_Vault_DropResult Vault)
        {
            try
            {
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.ShowVaultCurrentDropSlip(Vault.Vault_ID, Vault.Name, Vault.Serial_NO, Vault.Manufacturer_Name, Vault.Type_Prefix, SecurityHelper.CurrentUser.DisplayName,
                                                              Convert.ToDateTime(Vault.CreatedDate), Vault.IsWebServiceEnabled, Convert.ToDecimal(Vault.FillAmount),
                                                              Convert.ToDecimal(Vault.TotalAmountOnFill), Convert.ToDecimal(Vault.CurrentBalance), _isfinaldrop);


                    cReportViewer.ShowDialog();
                }
                LogManager.WriteLog("Show Vault Drop Transaction Slip Report Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion



    }



}
