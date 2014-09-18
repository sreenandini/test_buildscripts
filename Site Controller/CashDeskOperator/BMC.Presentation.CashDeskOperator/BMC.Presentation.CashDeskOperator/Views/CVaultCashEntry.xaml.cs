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
using BMC.CashDeskOperator;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using NoteCumTktScanLib;
using BMC.Common.LogManagement;
using System.Text.RegularExpressions;
using BMC.Business.CashDeskOperator;
using System.Data.Linq;
using System.Threading;
using System.Xml;
using BMC.Security;
using System.Configuration;
using System.Globalization;
using System.Windows.Controls.Primitives;
using Audit.BusinessClasses;
using Audit.Transport;
using System.Xml.Linq;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// To declare cassette level vault drops by collecting bills using notes counter
    /// </summary>
    public partial class CVaultCashEntry : Window
    {

        #region Private Variables
        private string[] sCassetteDenom;
        private string[] sHopperDenom;
        private NoteCumTktScanLib.CNoteCumTktScan objNoteCumTktScanLib = null;
        private decimal dZeroDollars = Convert.ToDecimal(0.0);
        private bool bStartClicked;
        public List<CassetteDropsResult> lst_cassettes = null;
        private IDictionary<string, int> dic_denoms = null;
        private CassetteDropsResult p_cassette = null;
        private CassetteDropsResult p_hopper = null;
        private bool IsAmountEditable = false;

        private string sFormat = "";
        #endregion

        #region Constructor
        /// <summary>
        /// Intialize a new instance of CVaultCashEntry
        /// </summary>
        public CVaultCashEntry()
        {
            InitializeComponent();
            objNoteCumTktScanLib = new NoteCumTktScanLib.CNoteCumTktScan();

            LoadRegionSettings();
            LoadDictionary();
            IsAmountEditable = Settings.IsBillCounterAmountEditable;

        }
        #endregion

        #region LoadMethods

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadVaultDropDetails();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
        }

        /// <summary>
        /// Load vault drop details from table[tVault_Drops]
        /// </summary>
        private void LoadVaultDropDetails()
        {
            try
            {

                LogManager.WriteLog("CVaultCashEntry->LoadVaultDropDetails()", LogManager.enumLogLevel.Debug);
                List<GetUndeclaredVaultDrops> lst_vdrops = Vault.CreateInstance().GetUndeclaredDrops(true);
                if (lst_vdrops != null && lst_vdrops.Count > 0)
                {
                    this.DataContext = lst_vdrops;
                    if (lst_vdrops[0].Drop_ID > 0)
                    {

                        //lv_dropdetails.SelectionChanged -= lv_dropdetails_SelectionChanged;
                        lst_vdrops = SelectUndeclaredDrop(lst_vdrops);
                        lv_dropdetails.SelectedIndex = -1;
                        lv_dropdetails.ItemsSource = lst_vdrops;

                        //lv_dropdetails.SelectionChanged += lv_dropdetails_SelectionChanged;
                        lst_vdrops = SelectUndeclaredDrop(lst_vdrops);
                        lv_dropdetails.SelectedItem = lst_vdrops.Last();
                        lv_dropdetails.ScrollIntoView(lv_dropdetails.SelectedItem);


                    }
                    else
                    {
                        lv_dropdetails.ItemsSource = null;
                    }
                }
                else
                {
                    this.DataContext = null;
                    lv_dropdetails.ItemsSource = null;
                    lv_CassetteDetails.ItemsSource = null;
                    lblcounterWarning.Text = "";
                    btnStart.Visibility = Visibility.Hidden;
                    btnApply.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception Ex)
            {
                lblcounterWarning.Text = Application.Current.FindResource("Vault_MessageID2") as string;
                ExceptionManager.Publish(Ex);
            }
        }

        private List<GetUndeclaredVaultDrops> SelectUndeclaredDrop(List<GetUndeclaredVaultDrops> lst_vdrops)
        {
            try
            {

                GetUndeclaredVaultDrops undec_vdrop = lst_vdrops.Last();//.Find(obj => obj.Declared == false);
                if (undec_vdrop.Cassettes.Count > 0)
                {
                    if (undec_vdrop.Cassettes.FindAll(obj => obj.CassetteType_ID == (int)CassetteTypes.Cassette).Count == 0)
                    {
                        btnStart.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        btnStart.Visibility = Visibility.Visible;

                    }
                }
                undec_vdrop.ToDeclared = true;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return lst_vdrops;
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
                LogManager.WriteLog("CVaultCashEntry->LoadRegionSettings() Culture:  " + ExtensionMethods.CurrentSiteCulture, LogManager.enumLogLevel.Debug);
                sCassetteDenom = ConfigurationManager.AppSettings[ExtensionMethods.CurrentSiteCulture].ToString().Split(',');
                string[] strDenom = ConfigurationManager.AppSettings[ExtensionMethods.CurrentSiteCulture + "_Denom"].ToString().Split(':');
                sHopperDenom = (strDenom.Count() > 1) ? strDenom[1].Split(',') : null;
                var d = Convert.ToDecimal(1.1);

                decimal.TryParse(d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out d);
                sFormat = d.ToString(new System.Globalization.CultureInfo(ExtensionMethods.CurrentCurrenyCulture)).Substring(1, 1);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                LogManager.WriteLog("CVaultCashEntry->LoadRegionSettings() Culture:  " + "Loading default Currency", LogManager.enumLogLevel.Error);
                sCassetteDenom = new string[] { "ONES", "TWOS", "FIVES", "TENS", "TWENTIES", "FIFTIES", "HUNDREDS" };
            }


        }
        #endregion

        #region SaveVaultDetails

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblcounterWarning.Text = "";
                bool IsValidAmount = false;
                SaveVaultDeclaration(ref IsValidAmount);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lblcounterWarning.Text = Application.Current.FindResource("MessageID465") as string;
            }

        }

        private void SaveVaultDeclaration(ref bool IsValidAmount)
        {
            try
            {
                LogManager.WriteLog("CVaultCashEntry->SaveVaultDeclaration()", LogManager.enumLogLevel.Debug);
                if (bStartClicked)
                {
                    lblcounterWarning.Text = Application.Current.FindResource("MessageID355") as string;
                    return;
                }


                GetUndeclaredVaultDrops v_drop = lv_dropdetails.SelectedItem as GetUndeclaredVaultDrops;
                if (v_drop != null)
                {

                    if (!ValidateAmount(v_drop))
                    {
                        IsValidAmount = false;
                        return;
                    }
                    else
                    {
                        IsValidAmount = true;
                    }


                    int ErrorCode = 0;
                    System.Xml.Linq.XElement xml_cassette = null;
                    if (!v_drop.IsEmptyCassette)
                    {
                        xml_cassette = FrameXml();
                    }
                    if (Settings.ShowVaultConfirmMessage)
                    {
                        System.Windows.Forms.DialogResult dlgResult;
                        decimal Val = 0;
                        if (p_cassette != null)
                        {
                            Val = p_cassette.DeclaredBalance;
                        }
                        if (p_hopper != null)
                        {
                            Val += p_hopper.DeclaredBalance;
                        }
                        if (Val == 0)
                        {
                            dlgResult = MessageBox.ShowBox("VaultCassette_MessageID8", BMC_Icon.Question, BMC_Button.YesNo);

                            if (dlgResult == System.Windows.Forms.DialogResult.No)
                                return;
                            else
                                goto dodrop;
                        }
                        else
                        {
                            dlgResult = MessageBox.ShowBox("VaultCassette_MessageID9", BMC_Icon.Question, BMC_Button.YesNo);
                            if (dlgResult == System.Windows.Forms.DialogResult.No)
                                return;

                        }
                    }


                dodrop:
                    if (Vault.CreateInstance().UpdateVaultDrops(v_drop.Declared_Balance, true, v_drop.Drop_ID, xml_cassette, ref ErrorCode))
                    {

                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = ModuleName.Vault,
                            Audit_Screen_Name = "Vault Declaration",
                            Audit_Desc = "Complete Declaration For Drop ID " + v_drop.Drop_ID + " Vault Name : " + v_drop.VaultName + " User Name :" + BMC.Security.SecurityHelper.CurrentUser.UserName,
                            AuditOperationType = OperationType.MODIFY,
                        });
                        LogManager.WriteLog("CVaultCashEntry->SaveVaultDeclaration() Complete for Drop ID " + v_drop.Drop_ID.ToString(), LogManager.enumLogLevel.Debug);
                        if (Settings.ShowVaultSuccessMessage)
                        {
                            MessageBox.ShowBox("MessageID464", BMC_Icon.Information);
                        }
                        lblcounterWarning.Text = Application.Current.FindResource("MessageID464") as string;
                        LoadVaultDropDetails();
                    }
                    else
                    {
                        if (ErrorCode == -2)
                        {
                            lblcounterWarning.Text = Application.Current.FindResource("MessageID466") as string;

                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                lblcounterWarning.Text = Application.Current.FindResource("MessageID465") as string;
                ExceptionManager.Publish(Ex);
            }
        }

        bool ValidateAmount(GetUndeclaredVaultDrops v_drops)
        {
            bool retval = true;
            try
            {
                foreach (CassetteDropsResult cassette in v_drops.Cassettes)
                {
                    if (cassette.EnableControls)
                    {

                        if (cassette.DeclaredBalance > cassette.MaxFillAmount && (!v_drops.IsEmptyCassette))
                        {
                            string strType = "";
                            strType = (cassette.CassetteType_ID == (int)CassetteTypes.Cassette) ? " cassette " : " hopper ";
                            strType += cassette.Cassette_Name;
                            lblcounterWarning.Text = (Application.Current.FindResource("Vault_MessageID24").ToString() + strType).Replace("@@@@@", cassette.MaxFillAmount.GetUniversalCurrencyFormatWithSymbol());
                            lv_CassetteDetails.SelectedItem = cassette;
                            retval = false;
                            break;
                        }
                        if (IsAmountEditable)
                        {
                            if (cassette.DeclaredBalance % Convert.ToDecimal(cassette.Denom.Value) != 0)
                            {
                                string strMsg = Application.Current.FindResource("VaultCassette_MessageID7").ToString().Replace("@@@@@", cassette.Cassette_Name);
                                //MessageBox.ShowBox(strMsg, BMC_Icon.Warning, true);           
                                lblcounterWarning.Text = strMsg;
                                lv_CassetteDetails.SelectedItem = cassette;
                                retval = false;
                                break;
                            }
                            else
                            {

                                cassette.Quantity = Convert.ToInt32(Math.Truncate(cassette.DeclaredBalance / Convert.ToDecimal(cassette.Denom.Value)));
                                CalculateTotal(cassette.CassetteType_ID);
                            }
                        }

                    }

                }
                if (v_drops.IsEmptyCassette && retval)
                {
                    decimal tot_val = (p_cassette != null ? p_cassette.DeclaredBalance : 0) + (p_hopper != null ? p_hopper.DeclaredBalance : 0);
                    if (tot_val > v_drops.VaultCapacity)
                    {
                        lblcounterWarning.Text = Application.Current.FindResource("Vault_MessageID24").ToString().Replace("@@@@@", v_drops.VaultCapacity.GetUniversalCurrencyFormatWithSymbol());
                        retval = false;

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

        private XElement FrameXml()
        {

            XElement xml_Cassette = new XElement("CassetteDetails");

            foreach (CassetteDropsResult cassette in lst_cassettes)
            {
                if (cassette.EnableControls)
                {

                    xml_Cassette.Add(new XElement("Cassette",
                                              new XAttribute("Cassette_ID", cassette.Cassette_ID),
                                              new XAttribute("Denom", cassette.Denom),
                                                                           new XAttribute("FillAmount", cassette.DeclaredBalance)));
                }


            }
            return xml_Cassette;
        }

        #endregion

        #region NotesCounterMethods

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (bStartClicked)
                {

                    string sNotesString = string.Empty;

                    LogManager.WriteLog("CloseSerialCom() CALL", LogManager.enumLogLevel.Info);
                    objNoteCumTktScanLib.CloseSerialCom();
                    LogManager.WriteLog("CloseSerialCom() ACK", LogManager.enumLogLevel.Info);

                    LogManager.WriteLog("GetString() CALL", LogManager.enumLogLevel.Info);
                    objNoteCumTktScanLib.GetString(out sNotesString);
                    LogManager.WriteLog("GetString() ACK", LogManager.enumLogLevel.Info);

                    LogManager.WriteLog(sNotesString, LogManager.enumLogLevel.Info);
                    bStartClicked = false;

                    string[] stemp = sNotesString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] NameValuePair;

                    string sTemp = string.Empty;
                    string sCurrencySymbol = sTemp.GetCurrencySymbol();

                    int iTotalBillVal = 0;


                    foreach (var Entry in stemp)
                    {
                        NameValuePair = Entry.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                        //Skip if the an invalid denom is counted 
                        if (!sCassetteDenom.Contains(NameValuePair[0]) && NameValuePair[0] != "TICKET")
                        {
                            LogManager.WriteLog("CVaultCashEntry->btnStart_Click: Invalid Denom " + NameValuePair[0], LogManager.enumLogLevel.Debug);
                            continue;
                        }
                        if (NameValuePair.Length != 2)
                            return;

                        int d_val = dic_denoms[NameValuePair[0]];
                        foreach (CassetteDropsResult cd_res in lst_cassettes.FindAll(obj => obj.Denom.HasValue && obj.Denom.Value == d_val))
                        {
                            if (cd_res != null && cd_res.IsChecked)
                            {
                                int Val = int.Parse(NameValuePair[1]);
                                cd_res.Quantity += Val;
                                if (IsAmountEditable)
                                {
                                    cd_res.DeclaredBalance = Convert.ToDecimal(cd_res.Denom.Value) * cd_res.Quantity;
                                    CalculateTotal((int)CassetteTypes.Cassette);
                                }
                                iTotalBillVal += Val;
                                break;
                            }

                        }
                    }
                    StartCounter(false);
                    if (sNotesString.Trim() == string.Empty || iTotalBillVal == 0)
                    {
                        lblcounterWarning.Text = Application.Current.FindResource("Vault_MessageID14") as string;
                    }
                }
                else
                {
                    StartCounter(true);
                    LogManager.WriteLog("OpenSerialComPort() CALL", LogManager.enumLogLevel.Info);
                    if (objNoteCumTktScanLib.OpenSerialComPort(Settings.BillVoucherCounterCOMPort) != 0)
                    {
                        LogManager.WriteLog("OpenSerialComPort() NACK", LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("CloseSerialCom() CALL", LogManager.enumLogLevel.Info);
                        objNoteCumTktScanLib.CloseSerialCom();
                        LogManager.WriteLog("CloseSerialCom() ACK", LogManager.enumLogLevel.Info);
                        MessageBox.ShowBox("MessageID295", BMC_Icon.Error);
                        return;
                    }
                    if (ExtensionMethods.CurrentSiteCulture == "en-US")
                    {
                        objNoteCumTktScanLib.SetDenom(1);
                        LogManager.WriteLog("SetDenom(1) en-US", LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        objNoteCumTktScanLib.SetDenom(2);
                        LogManager.WriteLog("SetDenom(2) it-IT", LogManager.enumLogLevel.Info);
                    }
                    LogManager.WriteLog("OpenSerialComPort() ACK", LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("StartRead() CALL", LogManager.enumLogLevel.Info);
                    objNoteCumTktScanLib.StartRead();
                    LogManager.WriteLog("StartRead() ACK", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("Exception Thrown is Start/Stop Button Click", LogManager.enumLogLevel.Debug);
                lblcounterWarning.Text = Application.Current.FindResource("MessageID295") as string;
                if (!string.IsNullOrEmpty(Ex.Message))
                {
                    MessageBox.ShowBox(Ex.Message, BMC_Icon.Error, true);
                }
                ExceptionManager.Publish(Ex);

            }
            finally
            {

                btnStart.IsEnabled = true;
            }
        }

        private void StartCounter(bool isStarted)
        {
            GetUndeclaredVaultDrops v_drop = lv_dropdetails.SelectedItem as GetUndeclaredVaultDrops;
            if (v_drop != null)
            {

                if (isStarted)
                {
                    //btnApply.Visibility = Visibility.Hidden;
                    v_drop.ToDeclared = false;
                    gp_dropdetails.IsEnabled = false;
                    gp_declaration.IsEnabled = false;
                    gp_vaultdetails.IsEnabled = false;
                    bStartClicked = true;
                    btnStart.Content = Application.Current.FindResource("BillsTicketCounter_xaml_btnStop") as string;
                    lblcounterWarning.Text = Application.Current.FindResource("MessageID352a") as string;
                }
                else
                {
                    if (lv_dropdetails.SelectedIndex == lv_dropdetails.Items.Count - 1)
                        v_drop.ToDeclared = true;
                    //btnApply.Visibility = Visibility.Visible;
                    gp_dropdetails.IsEnabled = true;
                    gp_declaration.IsEnabled = true;
                    gp_vaultdetails.IsEnabled = true;
                    bStartClicked = false;
                    btnStart.Content = Application.Current.FindResource("BillsTicketCounter_xaml_btnStart") as string;
                    lblcounterWarning.Text = string.Empty;
                }
            }
        }
        #endregion

        #region Form Events

        private void btn_ClearBils_Click(object sender, RoutedEventArgs e)
        {
            ClearBills();
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblcounterWarning.Text = "";
                CheckUncommitedData();
                LoadVaultDropDetails();
                lv_dropdetails.IsEnabled = true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (lv_CassetteDetails.SelectedItem != null)
                {
                    TextBox txt_value = sender as TextBox;
                    if (IsAmountEditable)
                    {
                        txt_value.TextChanged -= TextBox_TextChanged;
                        return;
                    }
                    int Cassette_ID = Convert.ToInt32("0" + txt_value.Tag);
                    if (lst_cassettes != null)
                    {
                        CassetteDropsResult c_res = lst_cassettes.Find(obj => obj.Cassette_ID == Cassette_ID);
                        if (c_res != null)
                        {
                            decimal dc_bal = Convert.ToInt32("0" + txt_value.Text) * Convert.ToDecimal(c_res.Denom);

                            c_res.DeclaredBalance = dc_bal;
                            CalculateTotal(c_res.CassetteType_ID);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {

            TextBox txt_value = sender as TextBox;
            try
            {

                if (!IsAmountEditable)
                {
                    txt_value.KeyDown -= txtAmount_KeyDown;
                    return;
                }

                int Cassette_ID = Convert.ToInt32("0" + txt_value.Tag);
                if (lst_cassettes != null)
                {
                    CassetteDropsResult c_res = lst_cassettes.Find(obj => obj.Cassette_ID == Cassette_ID);

                    e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);
                    if (c_res.CassetteType_ID == (int)CassetteTypes.Hopper)
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
                                //return;
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
                            //return;
                        }
                        else if (sFormat == "." && (txt_value.Text.Length - txt_value.CaretIndex >= 3))
                        {
                            e.Handled = true;
                        }
                    }
                    else if (txt_value.Text.Length + 1 > 6)
                    {
                        e.Handled = true;
                        //return;
                    }
                    if (e.Key == Key.Enter)
                    {
                        if (c_res != null)
                        {
                            string strValue = txt_value.Text;//.Length
                            if (strValue.Length > 6 && strValue.IndexOf('.') == -1)
                            {
                                strValue = strValue.Substring(0, 6);
                                txt_value.Text = strValue;
                            }
                            decimal declaredbal = Convert.ToDecimal(strValue);
                            c_res.DeclaredBalance = declaredbal;
                            decimal denom = Convert.ToDecimal(c_res.Denom);
                            c_res.Quantity = Convert.ToInt32(Math.Truncate((declaredbal / denom)));


                            if (!ValidateAmount(lv_dropdetails.SelectedItem as GetUndeclaredVaultDrops))
                            {
                                //string strMsg = Application.Current.FindResource("VaultCassette_MessageID7").ToString().Replace("@@@@@", c_res.Cassette_Name);
                                //lblcounterWarning.Text = strMsg;
                                lv_dropdetails.IsEnabled = false;
                            }
                            else
                            {
                                lblcounterWarning.Text = "";
                                c_res.DeclaredBalance = denom * c_res.Quantity;
                                lv_dropdetails.IsEnabled = true;
                            }


                            CalculateTotal(c_res.CassetteType_ID);

                        }
                    }


                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }

        }

        private void txtAmount_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            try
            {
                TextBox txt_value = sender as TextBox;

                if (!IsAmountEditable)
                {
                    txt_value.PreviewMouseUp -= txtAmount_PreviewMouseUp;
                    return;
                }
                if (!Settings.OnScreenKeyboard)
                    return;
                if (lv_CassetteDetails.SelectedItem != null)
                {

                    int Cassette_ID = Convert.ToInt32("0" + txt_value.Tag);
                    if (lst_cassettes != null)
                    {
                        CassetteDropsResult c_res = lst_cassettes.Find(obj => obj.Cassette_ID == Cassette_ID);
                        if (c_res != null)
                        {

                            bool isdecimal = c_res.CassetteType_ID == (int)CassetteTypes.Hopper;
                            int maxlength = 9;
                            if (!isdecimal)
                            {
                                txt_value.Text = Convert.ToInt32(Convert.ToDecimal(txt_value.Text)).ToString();
                                maxlength = 6;
                            }
                            txt_value.Text = DisplayNumberPad(txt_value.Text.Trim(), isdecimal, maxlength);
                            txt_value.SelectionStart = txt_value.Text.Length;
                            decimal declaredbal = Convert.ToDecimal(txt_value.Text);
                            c_res.DeclaredBalance = declaredbal;
                            decimal denom = Convert.ToDecimal(c_res.Denom);
                            c_res.Quantity = Convert.ToInt32(Math.Truncate((declaredbal / denom)));
                            if (!ValidateAmount(lv_dropdetails.SelectedItem as GetUndeclaredVaultDrops))
                            {
                                //string strMsg = Application.Current.FindResource("VaultCassette_MessageID7").ToString().Replace("@@@@@", c_res.Cassette_Name);
                                //lblcounterWarning.Text = strMsg;
                                lv_dropdetails.IsEnabled = false;
                            }
                            else
                            {
                                lblcounterWarning.Text = "";
                                c_res.DeclaredBalance = denom * c_res.Quantity;
                                lv_dropdetails.IsEnabled = true;
                            }

                            CalculateTotal(c_res.CassetteType_ID);

                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private bool CheckUncommitedData()
        {
            bool retval = false;
            try
            {
                if (lv_dropdetails.SelectedItem != null)
                {
                    if (Settings.ShowVaultConfirmMessage)
                    {
                        GetUndeclaredVaultDrops v_drop = lv_dropdetails.SelectedItem as GetUndeclaredVaultDrops;
                        if (v_drop.Declared_Balance > 0 && v_drop.ToDeclared)
                        {
                            if (MessageBox.ShowBox("Vault_MessageID15", BMC_Icon.Information, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            {
                                lv_dropdetails.SelectionChanged -= lv_dropdetails_SelectionChanged;

                                SaveVaultDeclaration(ref retval);
                                lv_dropdetails.SelectionChanged += lv_dropdetails_SelectionChanged;

                            }
                            else
                            {
                                retval = true;
                            }
                        }
                        else
                        {
                            retval = true;
                        }
                    }
                }
                else if (lv_dropdetails.Items.Count == 0)
                {
                    retval = true;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return retval;
        }

        private void CalculateTotal(int CassetteType_ID)
        {
            switch (CassetteType_ID)
            {
                case (int)CassetteTypes.Cassette:
                    p_cassette.DeclaredBalance = lst_cassettes.Where(obj => obj.CassetteType_ID == CassetteType_ID && obj.EnableControls).Sum(o => o.DeclaredBalance);
                    break;
                case (int)CassetteTypes.Hopper:
                    p_hopper.DeclaredBalance = lst_cassettes.Where(obj => obj.CassetteType_ID == CassetteType_ID && obj.EnableControls).Sum(o => o.DeclaredBalance);
                    break;
            }

            if (lv_dropdetails.SelectedItem != null)
            {
                GetUndeclaredVaultDrops vd_drop = lv_dropdetails.SelectedItem as GetUndeclaredVaultDrops;
                vd_drop.Declared_Balance = lst_cassettes.Where(obj => obj.EnableControls).Sum(o => o.DeclaredBalance);
            }

        }

        private void lv_dropdetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (e.AddedItems.Count > 0)
                {
                    GetUndeclaredVaultDrops vd_select = e.AddedItems[0] as GetUndeclaredVaultDrops;
                    if (vd_select != null)
                    {

                        if (vd_select.Cassettes != null && vd_select.Cassettes.Count > 0)
                        {
                            lst_cassettes = vd_select.Cassettes;
                            if (Settings.AutoFillDeclaredAmount)
                            {
                                foreach (CassetteDropsResult casette in vd_select.Cassettes)
                                {
                                    casette.DeclaredBalance = casette.VaultBalance.Value;
                                    if (casette.EnableControls)
                                    {
                                        casette.Quantity = Convert.ToInt32(Math.Truncate(casette.DeclaredBalance / Convert.ToDecimal(casette.Denom.Value)));
                                    }

                                }
                            }

                            p_cassette = lst_cassettes.Find(obj => obj.CassetteType_ID == (int)CassetteTypes.Cassette && !obj.EnableControls);
                            p_hopper = lst_cassettes.Find(obj => obj.CassetteType_ID == (int)CassetteTypes.Hopper && !obj.EnableControls);

                            if (lst_cassettes.FindAll(obj => obj.CassetteType_ID == (int)CassetteTypes.Cassette).Count == 0)
                            {
                                btnStart.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                btnStart.Visibility = Visibility.Visible;

                            }

                            lv_CassetteDetails.ItemsSource = lst_cassettes;
                            if (!vd_select.IsEmptyCassette)
                            {
                                vd_select.IsEmptyCassette = false;
                            }
                            vd_select.Declared_Balance = (p_cassette != null ? p_cassette.DeclaredBalance : 0) + (p_hopper != null ? p_hopper.DeclaredBalance : 0);
                        }
                        else
                        {
                            LoadEmptyCassette(vd_select);
                            if (lst_cassettes.FindAll(obj => obj.CassetteType_ID == (int)CassetteTypes.Cassette).Count == 0)
                            {
                                btnStart.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                btnStart.Visibility = Visibility.Visible;

                            }
                            vd_select.Declared_Balance = 0;
                            vd_select.IsEmptyCassette = true;
                        }
                        if (!vd_select.ToDeclared)
                        {
                            lblcounterWarning.Text = Application.Current.FindResource("Vault_MessageID16") as string;
                        }
                        else
                        {
                            lblcounterWarning.Text = "";
                        }
                        if (e.RemovedItems.Count > 0)
                        {
                            GetUndeclaredVaultDrops vd_removeselect = e.RemovedItems[0] as GetUndeclaredVaultDrops;
                            if (vd_removeselect != null)
                            {
                                if (vd_removeselect.Declared_Balance > 0 && vd_removeselect.ToDeclared)
                                {
                                    if ((Settings.ShowVaultConfirmMessage) && MessageBox.ShowBox("Vault_MessageID15", BMC_Icon.Information, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        lv_dropdetails.SelectionChanged -= lv_dropdetails_SelectionChanged;
                                        lv_dropdetails.SelectedItem = vd_removeselect;
                                        bool IsValidAmount = false;
                                        SaveVaultDeclaration(ref IsValidAmount);
                                        lv_dropdetails.SelectionChanged += lv_dropdetails_SelectionChanged;

                                        return;
                                    }
                                    else
                                    {
                                        foreach (CassetteDropsResult casette in vd_removeselect.Cassettes)
                                        {
                                            casette.Quantity = 0;
                                            casette.DeclaredBalance = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (CassetteDropsResult casette in vd_removeselect.Cassettes)
                                    {
                                        casette.Quantity = 0;
                                        casette.DeclaredBalance = 0;
                                    }

                                }

                            }

                            vd_removeselect.Declared_Balance = 0;

                        }

                        vd_select.BillsTotal = 0;
                        vd_select.TotalCoinsValueAsCurrency = 0;

                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        private void LoadEmptyCassette(GetUndeclaredVaultDrops vd_select)
        {
            try
            {
                var redcolor = new System.Windows.Media.SolidColorBrush(Colors.DarkRed);
                var blackcolor = new System.Windows.Media.SolidColorBrush(Colors.Black);

                var BoldFont = new System.Windows.FontWeight();
                BoldFont = System.Windows.FontWeights.Bold;

                var NormalFont = new System.Windows.FontWeight();
                NormalFont = System.Windows.FontWeights.Normal;
                List<CassetteDropsResult> lst_emptycassette = new List<CassetteDropsResult>();
                int temp_cassetteid = 1;
                foreach (string strdenom in sCassetteDenom)
                {

                    int idenom = dic_denoms[strdenom];

                    lst_emptycassette.Add(new CassetteDropsResult
                    {
                        Cassette_ID = temp_cassetteid++,
                        CassetteType_ID = (int)CassetteTypes.Cassette,
                        Cassette_Name = "Cassette :" + idenom,
                        DeclaredBalance = 0,
                        Denom = idenom,
                        IsChecked = true,
                        Drop_ID = vd_select.Drop_ID,
                        VaultBalance = 0,
                        EnableControls = true,
                        FontColor = blackcolor,
                        CustomFontWeight = NormalFont,
                        IsBillCounterAmountEditable = Settings.IsBillCounterAmountEditable,
                        IsBillCounterQuantityEditable = !Settings.IsBillCounterAmountEditable
                    });
                }
                if (lst_emptycassette.Count > 0)
                {
                    p_cassette = new CassetteDropsResult
                    {
                        Cassette_ID = 0,
                        CassetteType_ID = (int)CassetteTypes.Cassette,
                        Cassette_Name = "Total Cassettes (" + lst_emptycassette.Count + ")",
                        DeclaredBalance = 0,
                        Denom = 0,
                        Drop_ID = vd_select.Drop_ID,
                        VaultBalance = 0,
                        EnableControls = false,
                        FontColor = redcolor,
                        CustomFontWeight = BoldFont,
                        IsBillCounterAmountEditable = false,
                        IsBillCounterQuantityEditable = false
                    };
                    lst_emptycassette.Add(p_cassette);
                }

                if (sHopperDenom != null && sHopperDenom.Length > 0)
                {
                    foreach (string str in sHopperDenom)
                    {
                        float val = 0f;
                        if (float.TryParse(str, out val))
                        {
                            lst_emptycassette.Add(new CassetteDropsResult
                            {
                                Cassette_ID = temp_cassetteid++,
                                CassetteType_ID = (int)CassetteTypes.Hopper,
                                Cassette_Name = "Hopper :" + val,
                                DeclaredBalance = 0,
                                Denom = val,
                                IsChecked = true,
                                Drop_ID = vd_select.Drop_ID,
                                VaultBalance = 0,
                                EnableControls = true,
                                FontColor = blackcolor,
                                CustomFontWeight = NormalFont,
                                IsBillCounterAmountEditable = Settings.IsBillCounterAmountEditable,
                                IsBillCounterQuantityEditable = !Settings.IsBillCounterAmountEditable
                            });
                        }

                    }
                    if (lst_emptycassette.Count > 0)
                    {
                        p_hopper = new CassetteDropsResult
                        {
                            Cassette_ID = 0,
                            CassetteType_ID = (int)CassetteTypes.Hopper,
                            Cassette_Name = "Total Hoppers (" + sHopperDenom.Length + ")",
                            DeclaredBalance = 0,
                            Denom = 0,
                            Drop_ID = vd_select.Drop_ID,
                            VaultBalance = 0,
                            EnableControls = false,
                            FontColor = redcolor,
                            CustomFontWeight = BoldFont,
                            IsBillCounterAmountEditable = false,
                            IsBillCounterQuantityEditable = false
                        };
                        lst_emptycassette.Add(p_hopper);
                    }
                }
                vd_select.Cassettes = lst_emptycassette;
                lst_cassettes = lst_emptycassette;
                lv_CassetteDetails.ItemsSource = lst_emptycassette;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void ClearBills()
        {
            try
            {


                LogManager.WriteLog("CVaultCashEntry->ClearBills()", LogManager.enumLogLevel.Debug);

                lblcounterWarning.Text = String.Empty;

                if (lst_cassettes != null)
                {
                    foreach (CassetteDropsResult casette in lst_cassettes)
                    {
                        casette.Quantity = 0;
                        casette.DeclaredBalance = 0;
                    }
                    (lv_dropdetails.SelectedItem as GetUndeclaredVaultDrops).Declared_Balance = 0;
                    txt_DecBal.Text = "0";
                }
                lv_dropdetails.IsEnabled = true;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void LoadDictionary()
        {

            dic_denoms = new Dictionary<string, int>();
            dic_denoms.Add(new KeyValuePair<string, int>("ONES", 1));
            dic_denoms.Add(new KeyValuePair<string, int>("TWOS", 2));
            dic_denoms.Add(new KeyValuePair<string, int>("FIVES", 5));
            dic_denoms.Add(new KeyValuePair<string, int>("TENS", 10));
            dic_denoms.Add(new KeyValuePair<string, int>("TWENTIES", 20));
            dic_denoms.Add(new KeyValuePair<string, int>("FIFTIES", 50));
            dic_denoms.Add(new KeyValuePair<string, int>("HUNDREDS", 100));
            dic_denoms.Add(new KeyValuePair<string, int>("TWO_HUNDREDS", 200));
            dic_denoms.Add(new KeyValuePair<string, int>("FIVE_HUNDREDS", 500));


        }

        private void txtAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt_value = sender as TextBox;
            try
            {

                if (!IsAmountEditable)
                {
                    txt_value.TextChanged -= txtAmount_TextChanged;
                    return;
                }

                if (lst_cassettes != null && txt_value.Text.Trim() != "")
                {
                    int Cassette_ID = Convert.ToInt32("0" + txt_value.Tag);
                    CassetteDropsResult c_res = lst_cassettes.Find(obj => obj.Cassette_ID == Cassette_ID && obj.EnableControls);
                    if (c_res != null)
                    {
                        c_res.DeclaredBalance = Convert.ToDecimal(txt_value.Text);
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Prohibit space
            e.Handled = !IsNumberKey(e.Key) && !IsDelOrBackspaceOrTabKey(e.Key);
        }



        #endregion

        #region CloseForm

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckUncommitedData())
                {
                    e.Handled = true;
                    return;
                }
                CloseForm();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckUncommitedData())
                {
                    e.Handled = true;
                    return;
                }
                CloseForm();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void CloseForm()
        {
            LogManager.WriteLog("CVaultCashEntry->CloseForm()", LogManager.enumLogLevel.Debug);
            if (bStartClicked)
            {
                lblcounterWarning.Text = Application.Current.FindResource("MessageID355") as string;
                return;
            }


            this.Close();
        }
        #endregion

        #region VirtualKeyboardMethods
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



        private void txt_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                TextBox obj = (TextBox)sender;
                if (!Settings.OnScreenKeyboard)
                    return;

                obj.Text = DisplayNumberPad(obj.Text.Trim(), false, 8);
                obj.SelectionStart = obj.Text.Length;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
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

    }

   
}

