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

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CashEntry.xaml
    /// </summary>
    public partial class CashEntry : Window
    {
        List<UndeclaredCollectionRecord> lstCollections;
        List<UndeclaredCollectionRecord> lstCurrentItem;
        CollectionHelper _objCollectionHelper = null;
        ExchangeHelper _objExchangeHelper = null;
        TicketsHelper _objTicketsHelper = null;
        List<Bills> objBills = new List<Bills>();
        string[] sCurrencyList;
       
        NoteCumTktScanLib.CNoteCumTktScan objNoteCumTktScanLib = new NoteCumTktScanLib.CNoteCumTktScan();
        public IEnumerable<ParentVoucher.ValidVouchers> ValidTicketsHolder;
        public List<ParentVoucher.ValidVouchers> lsValidTicketsHolder;
        List<ParentVoucher.InValidVouchers> invalidVouchersList;
        DeclarationBiz oDeclarationBiz = null;
        private ListCollectionView _viewSource = null;
        Dictionary<string, int> _DictTickets = new Dictionary<string, int>();
        Dictionary<string, int> _DictTickets_BKP = new Dictionary<string, int>();
        Dictionary<TextBox, bool> _DictShowNumericPad = new Dictionary<TextBox, bool>();
        Brush _EnableBrush = Application.Current.FindResource("TextBoxGradient") as Brush;
        Brush _DisaleBrush = Application.Current.FindResource("TextBoxDIsableGradient") as Brush; 
     
        bool bStartClicked;
        decimal dZeroDollars = Convert.ToDecimal(0.0);
        bool bShowUncommitedChangesPopup = true;
        bool bRecalculateTotal = true;
        bool bAutoSaveOnMoveNext = false;
        bool bExcludeVouchersNotInSystem = false;
        int iVoucherAutoAddOnLength= 18;
        bool bDisableVoucherAutoAddOnLength = false;
        bool bRefreshVouchersOnSave = true;
        bool _IsPartCollection = false; 
        bool _IsTicketDeclarationMethodManual = false;
        bool _errorOccurred = false;
        bool bEnableDefaultZero = false;
        TextBox _errorTextBox = null;
       // bool repeatStoper = false;


        public event RoutedEventHandler ValueChanged;
        public string sFormat = string.Empty;
        public CashEntry()
        {
            InitializeComponent();
            LoadRegionSettings();
            cmbPosition.Focus();
        }

        public CashEntry(int batch, DeclarationFilterBy filterBy, string filterValue, string SiteCode, int CurrentIndex,IList<UndeclaredCollectionRecord> Collections)
            : this()
        {
            _objCollectionHelper = new CollectionHelper();
            _objExchangeHelper = new ExchangeHelper();
            _objTicketsHelper = new TicketsHelper();
            oDeclarationBiz = new DeclarationBiz();
            txtSiteInfo.Text = Application.Current.FindResource("ManualCashEntry_xaml_lblSiteCode").ToString().Trim() + " " + SiteCode;
            _IsPartCollection = (batch == 0) ? true : false;
            bEnableDefaultZero = Settings.ManualCashEntryEnableZero;
            this.Initialize(batch, filterBy, filterValue, CurrentIndex, Collections);
            cmbPosition.Focus();
        }

        public CashEntry(int batch, DeclarationFilterBy filterBy, string filterValue, string SiteCode, int CurrentIndex, string ExchangeConn, string TicketingConn, IList<UndeclaredCollectionRecord> Collections)
            : this()
        {
            _objCollectionHelper = new CollectionHelper(ExchangeConn);
            _objExchangeHelper = new ExchangeHelper(ExchangeConn);
            _objTicketsHelper = new TicketsHelper(TicketingConn);
            oDeclarationBiz = new DeclarationBiz(ExchangeConn, TicketingConn);
            txtSiteInfo.Text = Application.Current.FindResource("ManualCashEntry_xaml_lblSiteCode").ToString().Trim() + " " + SiteCode;
            _IsPartCollection = (batch == 0) ? true : false;
			bEnableDefaultZero = Settings.ManualCashEntryEnableZero;
            this.Initialize(batch, filterBy, filterValue, CurrentIndex, Collections);
            cmbPosition.Focus();

        }

        //Load currency based on the region settings 
        private void LoadRegionSettings()
        {
            string sCurrencySymbol = string.Empty;
            try
            {
                sCurrencySymbol = "".GetCurrencySymbol();
                lbl_VoucherTotal.Text = string.Format("{0} ({1})", lbl_VoucherTotal.Text, sCurrencySymbol);
                lblTotal.Text = string.Format("{0} ({1})", lblTotal.Text, sCurrencySymbol);
                lbl_GrandTotal.Text = string.Format("{0} ({1})", lbl_GrandTotal.Text, sCurrencySymbol);
                LogManager.WriteLog("CashEntry->LoadRegionSettings() Culture:  " + ExtensionMethods.CurrentSiteCulture, LogManager.enumLogLevel.Debug);
                sCurrencyList = ConfigurationManager.AppSettings[ExtensionMethods.CurrentSiteCulture].ToString().Split(',');

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                LogManager.WriteLog("CashEntry->LoadRegionSettings() Culture:  " + "Loading default Currency", LogManager.enumLogLevel.Error);
                sCurrencyList = new string[] { "ONES", "TWOS", "FIVES", "TENS", "TWENTIES", "FIFTIES", "HUNDREDS" };
            }

            try
            {

                
                LogManager.WriteLog("CashEntry->LoadRegionSettings() BillsDictionary", LogManager.enumLogLevel.Error);

                foreach (var Denom in sCurrencyList)
                {

                    switch (Denom)
                    {
                        case "ONES":
                            {
                                lbl_Ones.Text = sCurrencySymbol + " 1";
                                txt_Ones.Text = "";
                                lbl_Ones.Visibility = Visibility.Visible;
                                txt_Ones.Visibility = Visibility.Visible;
                                txt_Onescnt.Visibility = Visibility.Visible;
                                _DictShowNumericPad.Add(txt_Ones, Settings.IsBillCounterAmountEditable);
                                _DictShowNumericPad.Add(txt_Onescnt, !Settings.IsBillCounterAmountEditable);
                                break;
                            }
                        case "TWOS":
                            {
                                lbl_Twos.Text = sCurrencySymbol + " 2";
                                txt_Twos.Text = "";
                                lbl_Twos.Visibility = Visibility.Visible;
                                txt_Twos.Visibility = Visibility.Visible;
                                txt_Twoscnt.Visibility = Visibility.Visible;
                                _DictShowNumericPad.Add(txt_Twos, Settings.IsBillCounterAmountEditable);
                                _DictShowNumericPad.Add(txt_Twoscnt, !Settings.IsBillCounterAmountEditable);
                                break;
                            }
                        case "FIVES":
                            {
                                lbl_Fives.Text = sCurrencySymbol + " 5";
                                txt_Fives.Text = "";
                                lbl_Fives.Visibility = Visibility.Visible;
                                txt_Fives.Visibility = Visibility.Visible;
                                txt_Fivescnt.Visibility = Visibility.Visible;
                                _DictShowNumericPad.Add(txt_Fives, Settings.IsBillCounterAmountEditable);
                                _DictShowNumericPad.Add(txt_Fivescnt, !Settings.IsBillCounterAmountEditable);
                                break;
                            }
                        case "TENS":
                            {
                                lbl_Tens.Text = sCurrencySymbol + " 10";
                                txt_Tens.Text = "";
                                lbl_Tens.Visibility = Visibility.Visible;
                                txt_Tens.Visibility = Visibility.Visible;
                                txt_Tenscnt.Visibility = Visibility.Visible;
                                _DictShowNumericPad.Add(txt_Tens, Settings.IsBillCounterAmountEditable);
                                _DictShowNumericPad.Add(txt_Tenscnt, !Settings.IsBillCounterAmountEditable);
                                break;
                            }
                        case "TWENTIES":
                            {
                                lbl_Twenties.Text = sCurrencySymbol + " 20";
                                txt_Twenties.Text = "";
                                lbl_Twenties.Visibility = Visibility.Visible;
                                txt_Twenties.Visibility = Visibility.Visible;
                                txt_Twentiescnt.Visibility = Visibility.Visible;
                                _DictShowNumericPad.Add(txt_Twenties, Settings.IsBillCounterAmountEditable);
                                _DictShowNumericPad.Add(txt_Twentiescnt, !Settings.IsBillCounterAmountEditable);                                
                                break;
                            }
                        case "FIFTIES":
                            {
                                lbl_Fifties.Text = sCurrencySymbol + " 50";
                                txt_Fifties.Text = "";
                                lbl_Fifties.Visibility = Visibility.Visible;
                                txt_Fifties.Visibility = Visibility.Visible;
                                txt_Fiftiescnt.Visibility = Visibility.Visible;
                                _DictShowNumericPad.Add(txt_Fifties, Settings.IsBillCounterAmountEditable);
                                _DictShowNumericPad.Add(txt_Fiftiescnt, !Settings.IsBillCounterAmountEditable);                                
                                break;
                            }
                        case "HUNDREDS":
                            {
                                lbl_Hundreds.Text = sCurrencySymbol + " 100";
                                txt_Hundreds.Text = "";
                                lbl_Hundreds.Visibility = Visibility.Visible;
                                txt_Hundreds.Visibility = Visibility.Visible;
                                txt_Hundredscnt.Visibility = Visibility.Visible;
                                _DictShowNumericPad.Add(txt_Hundreds, Settings.IsBillCounterAmountEditable);
                                _DictShowNumericPad.Add(txt_Hundredscnt, !Settings.IsBillCounterAmountEditable);
                                break;
                            }
                        case "TWO_HUNDREDS":
                            {
                                lbl_TwoHundreds.Text = sCurrencySymbol + " 200";
                                txt_TwoHundreds.Text = "";
                                lbl_TwoHundreds.Visibility = Visibility.Visible;
                                txt_TwoHundreds.Visibility = Visibility.Visible;
                                txt_TwoHundredscnt.Visibility = Visibility.Visible;
                                _DictShowNumericPad.Add(txt_TwoHundreds, Settings.IsBillCounterAmountEditable);
                                _DictShowNumericPad.Add(txt_TwoHundredscnt, !Settings.IsBillCounterAmountEditable);
                                break;
                            }
                        case "FIVE_HUNDREDS":
                            {
                                lbl_FiveHundreds.Text = sCurrencySymbol + " 500";
                                txt_FiveHundreds.Text = "";
                                lbl_FiveHundreds.Visibility = Visibility.Visible;
                                txt_FiveHundreds.Visibility = Visibility.Visible;
                                txt_FiveHundredscnt.Visibility = Visibility.Visible;
                                _DictShowNumericPad.Add(txt_FiveHundreds, Settings.IsBillCounterAmountEditable);
                                _DictShowNumericPad.Add(txt_FiveHundredscnt, !Settings.IsBillCounterAmountEditable);
                                break;
                            }
                        default:
                            break;

                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            ApplyStyles(_DictShowNumericPad);
        }

        private void ApplyStyles(Dictionary<TextBox, bool> _dict)
        {
            foreach (KeyValuePair<TextBox,bool> item in _dict)
            {
                if (item.Value)
                {
                    item.Key.Background = _EnableBrush;
                    item.Key.IsReadOnly = false;
                }
                else
                {
                    item.Key.Background = _DisaleBrush;
                    item.Key.IsReadOnly = true;
                }
            }
        }

        private void Initialize(int batch, DeclarationFilterBy filterBy, string filterValue, int CurrentIndex, IList<UndeclaredCollectionRecord>  Collections)
        {
            try
            {

                LogManager.WriteLog("CashEntry->Initialize()", LogManager.enumLogLevel.Debug);
                //Skip the first record having the total.
                lstCollections = (from c in Collections
                                  where !c.AssetNo.IsNullOrEmpty()
                                  select c).ToList();
                //Show/Hide controls based on declaration method.
                this.CheckTicketDeclarationMethod();
                _viewSource = new ListCollectionView(lstCollections);
                cmbPosition.ItemsSource = _viewSource;

                // reassign index once data binding is done.To load data.
                cmbPosition.SelectedIndex = -1;

                //After loading data add index changed even to avoid unwanted reloads of data.
                cmbPosition.SelectionChanged += new SelectionChangedEventHandler(cmbPosition_SelectionChanged);

                //Reassign index to trigger index changed event
                cmbPosition.SelectedIndex = CurrentIndex - 1;

                //Read setting to show/Hide popups and Autosave.
                bShowUncommitedChangesPopup = bool.Parse(ConfigurationManager.AppSettings["ShowUncommitedChangesPopup"].ToString());
                bAutoSaveOnMoveNext = bool.Parse(ConfigurationManager.AppSettings["AutoSaveOnMoveNext"].ToString());
                bExcludeVouchersNotInSystem = bool.Parse(ConfigurationManager.AppSettings["ExcludeVouchersNotInSystem"].ToString());
                iVoucherAutoAddOnLength = Int32.Parse(ConfigurationManager.AppSettings["VoucherAutoAddOnLength"].ToString());
                bDisableVoucherAutoAddOnLength = bool.Parse(ConfigurationManager.AppSettings["DisableVoucherAutoAddOnLength"].ToString());
                txt_AddVoucher.MaxLength = iVoucherAutoAddOnLength;
                txt_SearchVoucher.MaxLength = iVoucherAutoAddOnLength;
                bRefreshVouchersOnSave = bool.Parse(ConfigurationManager.AppSettings["RefreshVouchersOnSave"].ToString());
                //Remove text change handler 
                if (bDisableVoucherAutoAddOnLength)
                {
                    txt_AddVoucher.TextChanged -= txt_AddVoucher_TextChanged;
                }

                var d = Convert.ToDecimal(1.1);
                decimal.TryParse(d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out d);
                sFormat = d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)).Substring(1, 1);
                txt_CoinsTotal.TextChanged += new TextChangedEventHandler(txt_CoinsTotal_TextChanged);
                txt_CoinsTotal.AddHandler(TextBox.PreviewKeyDownEvent, new KeyEventHandler(txt_CoinsTotal_KeyDown), true);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        void RefreshCollection(string strMessage, bool bLoadTickets)
        {
            try
            {
                LogManager.WriteLog("CashEntry->RefreshCollection()", LogManager.enumLogLevel.Debug);
                List<UndeclaredCollectionRecord> tempLst = null;
                var batchNo = ((UndeclaredCollectionRecord)cmbPosition.SelectedItem).CollectionBatchNo;
                var collectionNo = ((UndeclaredCollectionRecord)cmbPosition.SelectedItem).CollectionNo;

                if(batchNo>0)
                    tempLst = _objCollectionHelper.GetUndeclaredCollectionByBatchNo(batchNo, DeclarationFilterBy.Position, ((UndeclaredCollectionRecord)cmbPosition.SelectedItem).Position);
                else
                    tempLst = _objCollectionHelper.GetUndeclaredCollectionByCollectionNo(collectionNo, DeclarationFilterBy.Position, ((UndeclaredCollectionRecord)cmbPosition.SelectedItem).Position);

                if (tempLst == null)
                {
                   MessageBox.ShowBox("MessageID381", BMC_Icon.Error);
                   return; 
                }

                lstCurrentItem = (from c in tempLst
                                  where !c.AssetNo.IsNullOrEmpty()
                                  select c).ToList();
                this.DataContext = lstCurrentItem[0];
                LogManager.WriteLog("CashEntry->RefreshCollection: " + lstCurrentItem[0].CollectionNo.ToString(), LogManager.enumLogLevel.Debug);

                bRecalculateTotal = false;

                txt_CoinsTotal.Text = ((lstCurrentItem[0]).TotalCoinsValue.GetUniversalCurrencyFormat() == "0" && !bEnableDefaultZero) ? string.Empty : (lstCurrentItem[0]).TotalCoinsValue.GetUniversalCurrencyFormat();
                txt_Onescnt.Text = ((lstCurrentItem[0]).P100.ToString() == "0" && !bEnableDefaultZero) ? string.Empty : (lstCurrentItem[0]).P100.ToString();
                txt_Twoscnt.Text = (((lstCurrentItem[0]).P200 / 2).ToString() == "0" && !bEnableDefaultZero) ? string.Empty : ((lstCurrentItem[0]).P200 / 2).ToString();
                txt_Fivescnt.Text = (((lstCurrentItem[0]).P500 / 5).ToString() == "0" && !bEnableDefaultZero) ? string.Empty : ((lstCurrentItem[0]).P500 / 5).ToString();
                txt_Tenscnt.Text = (((lstCurrentItem[0]).P1000 / 10).ToString() == "0" && !bEnableDefaultZero) ? string.Empty : ((lstCurrentItem[0]).P1000 / 10).ToString();
                txt_Twentiescnt.Text = (((lstCurrentItem[0]).P2000 / 20).ToString() == "0" && !bEnableDefaultZero) ? string.Empty : ((lstCurrentItem[0]).P2000 / 20).ToString();
                txt_Fiftiescnt.Text = (((lstCurrentItem[0]).P5000 / 50).ToString() == "0" && !bEnableDefaultZero) ? string.Empty : ((lstCurrentItem[0]).P5000 / 50).ToString();
                txt_Hundredscnt.Text = (((lstCurrentItem[0]).P10000 / 100).ToString() == "0" && !bEnableDefaultZero) ? string.Empty : ((lstCurrentItem[0]).P10000 / 100).ToString();
                txt_TwoHundredscnt.Text = (((lstCurrentItem[0]).P20000 / 200).ToString() == "0" && !bEnableDefaultZero) ? string.Empty : ((lstCurrentItem[0]).P20000 / 200).ToString();
                txt_FiveHundredscnt.Text = (((lstCurrentItem[0]).P50000 / 500).ToString() == "0" && !bEnableDefaultZero) ? string.Empty : ((lstCurrentItem[0]).P50000 / 500).ToString();
                txt_VoucherTotal.Text = (lstCurrentItem[0]).TicketsInValue.GetUniversalCurrencyFormat();

                if (_IsTicketDeclarationMethodManual && !_IsPartCollection)
                {
                    if (bLoadTickets)
                    {
                        ClearVouchers();
                        LoadDeclaredTickets();
                    }
                }
                bRecalculateTotal = true;
                RecalculateTotal();
                lblcounterWarning.Text = strMessage;
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }

        }

        void LoadDeclaredTickets()
        {

            try
            {
                LogManager.WriteLog("CashEntry->LoadDeclaredTickets()", LogManager.enumLogLevel.Debug);
                IMultipleResults Result = _objExchangeHelper.GetDeclaredTicketByCollection(((UndeclaredCollectionRecord)cmbPosition.SelectedItem).CollectionNo, ((UndeclaredCollectionRecord)cmbPosition.SelectedItem).InstallationNo);
                ValidTicketsHolder = Result.GetResult<ParentVoucher.ValidVouchers>();
                lsValidTicketsHolder = ValidTicketsHolder.ToList();
                lvTickets.ItemsSource = lsValidTicketsHolder;
                _DictTickets_BKP.Clear();
                foreach (var ticket in lsValidTicketsHolder)
                {
                    //sTickets = sTickets + "," + ticket.strBarcode;
                    this.AddVoucherToDictionary(ticket.strBarcode, 0);
                    //to check for changes 
                    if (!_DictTickets_BKP.ContainsKey(ticket.strBarcode))
                        _DictTickets_BKP.Add(ticket.strBarcode, 0);
                }

                //sTemptickets = sTickets;

                foreach (var value in Result.GetResult<ParentVoucher.ValidVouchersQty>())
                {
                    txt_VoucherQuantity.Text = value.Quantity.ToString();
                    txt_VoucherTotal.Text = (Convert.ToDecimal(value.Total) / 100).GetUniversalCurrencyFormat();

                }
                RecalculateGrandTotal();
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }
      
        private void RefreshTickets()
        {
            //Tickets List View
            LogManager.WriteLog("CashEntry->RefreshTickets()", LogManager.enumLogLevel.Debug);
            if (_DictTickets.Count == 0)
            //if (sTickets == string.Empty)
            {
                ClearVouchers();
                RecalculateGrandTotal();
                return;
            }
            StringBuilder _strTicketsList = new StringBuilder();
            foreach (string val in _DictTickets.Keys)
            {
                if (_strTicketsList.ToString() == string.Empty)
                    _strTicketsList.Append(val);
                else
                    _strTicketsList.Append("," + val);
            }
            LogManager.WriteLog("CashEntry->RefreshTickets tickets:" + _strTicketsList.ToString(), LogManager.enumLogLevel.Debug);

            IMultipleResults Result = _objTicketsHelper.GetValidInvalidVouchers_Counter(_strTicketsList.ToString(), txtAsset.Text, ((UndeclaredCollectionRecord)cmbPosition.SelectedItem).CollectionNo, ((UndeclaredCollectionRecord)cmbPosition.SelectedItem).InstallationNo);

            ValidTicketsHolder = Result.GetResult<ParentVoucher.ValidVouchers>();
            lsValidTicketsHolder = ValidTicketsHolder.ToList();
            lvTickets.ItemsSource = lsValidTicketsHolder;

            invalidVouchersList = Result.GetResult<ParentVoucher.InValidVouchers>().ToList();
            lvInValidTickets.ItemsSource = invalidVouchersList;

            foreach (var value in Result.GetResult<ParentVoucher.ValidVouchersQty>())
            {
                txt_VoucherQuantity.Text = value.Quantity.ToString();
                txt_VoucherTotal.Text = (Convert.ToDecimal(value.Total) / 100).GetUniversalCurrencyFormat();
            }

            foreach (var value in Result.GetResult<ParentVoucher.InValidVouchersQty>())
            {
                txt_InvalidVoucherQuantity.Text = value.Quantity.ToString();
            }
            RecalculateGrandTotal();
            SelectAllInvalidVouchers(chk_CheckAllInvalidVouchers.IsChecked.Value);
            txt_AddVoucher.Text = string.Empty;
            txt_AddVoucher.Focus();


        }

        void SetCurrentCollection()
        {
            try
            {
                LogManager.WriteLog("CashEntry->SetCurrentCollection()", LogManager.enumLogLevel.Debug);
                if (lstCurrentItem != null)    //To avoid IsUncommited check on initial load. 
                {
                    if (this.IsUncommited())
                    {
                        // if show popup is enabled     
                        if (bShowUncommitedChangesPopup)
                        {

                            System.Windows.Forms.DialogResult dr = MessageBox.ShowBox("MessageID463", BMC_Icon.Warning, BMC_Button.YesNo, string.Empty);
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (Settings.IsBillCounterAmountEditable)
                                {
                                    ValidateAmount();
                                    if (_errorOccurred) return;
                                }
                                this.SaveDeclaration(false);
                            }
                            else
                            {
                                if (Settings.IsBillCounterAmountEditable)
                                    ClearTextBoxes();
                                LogManager.WriteLog("CashEntry->SetCurrentCollection() Exiting without save CollectionNO:" + lstCurrentItem[0].CollectionNo.ToString(), LogManager.enumLogLevel.Debug);
                            }

                        } //For Auto save on move next 
                        else if (bAutoSaveOnMoveNext)
                        {
                            if (Settings.IsBillCounterAmountEditable)
                            {
                                ValidateAmount();
                                if (_errorOccurred) return;
                            }
                            this.SaveDeclaration(false);
                        }
                    }
                }
                if (cmbPosition.SelectedItem != null)
                {

                    RefreshCollection(string.Empty,true);
                    txt_CurrentCollection.Text = (cmbPosition.SelectedIndex + 1).ToString() + "/" + (lstCollections.Count).ToString();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }


        }

        void SaveDeclaration(bool bRefreshAfterSave)
        {
            //STEP 1 : Convert all vouchers into xml and update DB. Returns 0 on Success.
            //STEP 2 : Save all bills counted 
            //STEP 3 : Reload Collection data

            try
            {
                if (_IsPartCollection)
                {
                    SavePartDeclaration(true);
                    return;
                }
                int iTicketDeclarationStatus = 0;
                LogManager.WriteLog("CashEntry->SaveDeclaration()", LogManager.enumLogLevel.Debug);
                if (bStartClicked)
                {
                    lblcounterWarning.Text = Application.Current.FindResource("MessageID355") as string;
                    return;
                }
				
                //Validate CoinType
                if (!ValidCoinType())
                    return;

                //SAMPLE VOUCHERS XML*************************************************************************
                //<Vouchers>
                //        <Voucher Code="123" Amount="10"/>
                //        <Voucher Code="124" Amount="10"/>
                //</Vouchers>
                //SAMPLE XML**********************************************************************************

                //STEP 1
                XmlDocument doc = new XmlDocument();
                doc.AppendChild(doc.CreateElement("Vouchers"));
                if (_IsTicketDeclarationMethodManual)
                {
                    foreach (var voucher in lsValidTicketsHolder)
                    {
                        XmlNode node = doc.CreateElement("Voucher");
                        XmlAttribute code = doc.CreateAttribute("Code");
                        code.Value = voucher.strBarcode;
                        XmlAttribute Amount = doc.CreateAttribute("Amount");
                        Amount.Value = voucher.iAmount.Value.ToString();
                        node.Attributes.Append(code);
                        node.Attributes.Append(Amount);

                        doc.DocumentElement.AppendChild(node);
                    }
                    LogManager.WriteLog("CashEntry->SaveDeclaration:Tickets:" + doc.OuterXml, LogManager.enumLogLevel.Debug);
                    iTicketDeclarationStatus = oDeclarationBiz.InsertDeclaredTickets(doc.OuterXml, SecurityHelper.CurrentUser.SecurityUserID, (lstCurrentItem[0]).InstallationNo, (lstCurrentItem[0]).CollectionNo);
                }

                //STEP 2
                if (iTicketDeclarationStatus == 0)
                {
                    oDeclarationBiz.AddCollectionToFullCollection((txt_FiveHundreds.Text == string.Empty) ? "0" : txt_FiveHundreds.Text, (txt_TwoHundreds.Text == string.Empty) ? "0" : txt_TwoHundreds.Text, (txt_Hundreds.Text == string.Empty) ? "0" : txt_Hundreds.Text, (txt_Fifties.Text == string.Empty) ? "0" : txt_Fifties.Text, (txt_Twenties.Text == string.Empty) ? "0" : txt_Twenties.Text,
                        (txt_Tens.Text == string.Empty) ? "0" : txt_Tens.Text, (txt_Fives.Text == string.Empty) ? "0" : txt_Fives.Text, (txt_Twos.Text == string.Empty) ? "0" : txt_Twos.Text, (txt_Ones.Text == string.Empty) ? "0" : txt_Ones.Text, (txt_CoinsTotal.Text == string.Empty) ? "0" : txt_CoinsTotal.Text, (txt_VoucherTotal.Text == string.Empty) ? "0" : txt_VoucherTotal.Text,
                        (lstCurrentItem[0]).Installation_Token_Value, (lstCurrentItem[0]).CollectionNo);
                }
                LogManager.WriteLog("CashEntry->SaveDeclaration() Complete for collection no " + lstCurrentItem[0].CollectionNo.ToString(), LogManager.enumLogLevel.Debug);


                //STEP 3 
                if (bRefreshAfterSave)
                {
                    //update _DictTickets_BKP collection with latest tickets to avoid Iscommited failure after save. 
                    if (!bRefreshVouchersOnSave)
                    {
                        _DictTickets_BKP.Clear();
                        foreach (var item in _DictTickets.Keys)
                            _DictTickets_BKP.Add(item, 0);
                    }
                    RefreshCollection(Application.Current.FindResource("MessageID464") as string, bRefreshVouchersOnSave);
                }
                else
                    lblcounterWarning.Text = Application.Current.FindResource("MessageID464") as string;

            }
            catch (Exception Ex)
            {
                lblcounterWarning.Text = Application.Current.FindResource("MessageID465") as string;
                ExceptionManager.Publish(Ex);
            }
        }

        //Validate CoinType 
        private bool ValidCoinType()
        {            
            decimal dCoin = 0;
            try
            {
                decimal.TryParse(txt_CoinsTotal.Text, out dCoin);
                if ((dCoin * 100) % (lstCurrentItem[0]).Installation_Token_Value != 0)
                {
                    lblcounterWarning.Text = string.Format((Application.Current.FindResource("MessageID466_CashEntry") as string), "".GetCurrencySymbol(), (((decimal)(lstCurrentItem[0]).Installation_Token_Value) / 100).ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        void SavePartDeclaration(bool bRefreshAfterSave)
        {
            try
            {
                decimal dCoin = 0;
                decimal.TryParse(txt_CoinsTotal.Text, out dCoin);

                //Validate CoinType
                if (!ValidCoinType())
                    return;

                //LogManager.WriteLog("CashEntry->SavePartDeclaration()");
                //oDeclarationBiz.AddCollectionToPartCollection(txt_FiveHundreds.Text, txt_TwoHundreds.Text, txt_Hundreds.Text, txt_Fifties.Text, txt_Twenties.Text,
                // txt_Tens.Text, txt_Fives.Text, txt_Twos.Text, txt_Ones.Text, txt_CoinsTotal.Text, txt_VoucherTotal.Text, (lstCurrentItem[0]).Installation_Token_Value, (lstCurrentItem[0]).CollectionNo);
                //LogManager.WriteLog("CashEntry->SavePartDeclaration() Complete");

                oDeclarationBiz.AddCollectionToPartCollection((txt_FiveHundreds.Text == string.Empty) ? "0" : txt_FiveHundreds.Text, (txt_TwoHundreds.Text == string.Empty) ? "0" : txt_TwoHundreds.Text, (txt_Hundreds.Text == string.Empty) ? "0" : txt_Hundreds.Text, (txt_Fifties.Text == string.Empty) ? "0" : txt_Fifties.Text, (txt_Twenties.Text == string.Empty) ? "0" : txt_Twenties.Text,
                      (txt_Tens.Text == string.Empty) ? "0" : txt_Tens.Text, (txt_Fives.Text == string.Empty) ? "0" : txt_Fives.Text, (txt_Twos.Text == string.Empty) ? "0" : txt_Twos.Text, (txt_Ones.Text == string.Empty) ? "0" : txt_Ones.Text, (txt_CoinsTotal.Text == string.Empty) ? "0" : txt_CoinsTotal.Text, (txt_VoucherTotal.Text == string.Empty) ? "0" : txt_VoucherTotal.Text,
                      (lstCurrentItem[0]).Installation_Token_Value, (lstCurrentItem[0]).CollectionNo);


                RefreshCollection(Application.Current.FindResource("MessageID464") as string, bRefreshVouchersOnSave);

               }
            catch (Exception Ex)
            {
                lblcounterWarning.Text = Application.Current.FindResource("MessageID465") as string;
                ExceptionManager.Publish(Ex);
            }
        }
        #region Form Events
        private void txt_CoinsChanged(object sender, TextChangedEventArgs e)
        {
            decimal tempCoin;
            try
            {
                if (string.IsNullOrEmpty(txt_CoinsTotal.Text) && !bEnableDefaultZero)
                    txt_CoinsTotal.Text = string.Format("0{0}00", ExtensionMethods.GetCurrencyDecimalDelimiter());

                if (txt_CoinsTotal.Text != string.Empty)
                {
                    if (!ValidCoinType())
                    {
                        lblTotalCoins.Visibility = System.Windows.Visibility.Visible;
                        _errorOccurred = true;
                        return;
                    }

                    else
                    {
                        _errorOccurred = false;
                        lblcounterWarning.Text = string.Empty;
                        lblTotalCoins.Visibility = System.Windows.Visibility.Collapsed;
                    }                   
                }
                tempCoin = Decimal.Parse(txt_CoinsTotal.Text);

                RecalculateTotal();
            }
            catch (Exception Ex)
            {
                txt_CoinsTotal.Text = string.Empty;
                ExceptionManager.Publish(Ex);
            }
        }

        void txt_CoinsTotal_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                RaiseValueChangeEvent(e);
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            //if (string.IsNullOrEmpty(txtPromoTickAmt.Text))
            //    txtPromoTickAmt.Text = string.Format("0{0}00", ExtensionMethods.GetCurrencyDecimalDelimiter());
        }

        private void RaiseValueChangeEvent(TextChangedEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged.Invoke(this, e as RoutedEventArgs);
            }
        }
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

                    //    "TICKET:101000000000020064,TICKET:101000000000049348,TICKET:101000000000066406,TICKET:100100000000075141,TICKET:100100000000088059,TICKET:100100000000097808,TICKET:100100000000109655,TICKET:100100000000110040,
                    //    TICKET:100100000000125358,TICKET:100100000000130628,TICKET:100100000000148258,TICKET:100100000000158646,TICKET:100100000000164197,TICKET:100100000000179467,TICKET:100100000000188773,TICKET:100100000000199007,
                    //  TICKET:100100000000203490,TICKET:100100000000213727,TICKET:100100000000223276,TICKET:100100000000238386,TICKET:100100000000242970,TICKET:100100000000253204,TICKET:100100000000262510,TICKET:100100000000277620,
                    //TICKET:100100000000280217,TICKET:100100000000290605,TICKET:100100000000302773,TICKET:100100000000312529,TICKET:100100000000327110,TICKET:100100000000331988,TICKET:100100000000342250,TICKET:100100000000352006,
                    //TICKET:100100000000366355,TICKET:100100000000371229,TICKET:100100000000381891,TICKET:100100000000391487,TICKET:100100000000404859,TICKET:100100000000419563,TICKET:100100000000424314,TICKET:100100000000433903,
                    //TICKET:100100000000446170,TICKET:100100000000450887,TICKET:100100000000465393,TICKET:100100000000474982,TICKET:100100000000482376,TICKET:100100000000497240,TICKET:100100000000509578,TICKET:100100000000514848,
                    //TICKET:100100000000524236,TICKET:100100000000534624,TICKET:100100000000540892,TICKET:100100000000556169,TICKET:100100000000565314,TICKET:100100000000575702,TICKET:100100000000586937,TICKET:100100000000592044,
                    //TICKET:100100000000606857,TICKET:100100000000611967,TICKET:100100000000627432,TICKET:100100000000637660,TICKET:100100000000647294,TICKET:100100000000652403,TICKET:100100000000668114,TICKET:100100000000678342,
                    //TICKET:100100000000684374,TICKET:100100000000699644,TICKET:100100000000702290,TICKET:100100000000717164,TICKET:100100000000727675,TICKET:100100000000737421,TICKET:100100000000742739,TICKET:100100000000757603,
                    //TICKET:100100000000768357,TICKET:100100000000778103,TICKET:100100000000789659,TICKET:100100000000794363,TICKET:100100000000808138,TICKET:100100000000815167,TICKET:100100000000828631,TICKET:100100000000830788,
                    //TICKET:100100000000848813,TICKET:100100000000855842,TICKET:100100000000869078,TICKET:100100000000871224,TICKET:100100000000881254,TICKET:100100000000898443,TICKET:100100000000906131,TICKET:100100000000919087,
                    //TICKET:100100000000922070,TICKET:100100000000939900,TICKET:100100000000946816,TICKET:100100000000959762,TICKET:100100000000962519,TICKET:100100000000970347,TICKET:100100000000987178,TICKET:100100000000999966,
                    //TICKET:100100000001000890,TICKET:100100000001016167,TICKET:100100000001021710,TICKET:100100000001032105,TICKET:100100000001042135,TICKET:100100000001057405,TICKET:100100000001062713,TICKET:100100000001073108,
                    //TICKET:100100000001080014,TICKET:100100000001095124,TICKET:100100000001107452,TICKET:100100000001112166,TICKET:100100000001123070,TICKET:100100000001132669,TICKET:100100000001148691,TICKET:100100000001153404,
                    //TICKET:100100000001164073,TICKET:100100000001173662,TICKET:100100000001184170,TICKET:100100000001199044,TICKET:100100000001209132,TICKET:100100000001214006,TICKET:100100000001228355,TICKET:100100000001238101,
                    //TICKET:100100000001240777,TICKET:100100000001255641,TICKET:100100000001260232,TICKET:100100000001279982,TICKET:100100000001288250,TICKET:100100000001292967,TICKET:100100000001304974,TICKET:100100000001310081,
                    //TICKET:100100000001329397,TICKET:100100000001339624,TICKET:100100000001346615,TICKET:100100000001351725,TICKET:100100000001361274,TICKET:100100000001371501,TICKET:100100000001381692,TICKET:100100000001396962,
                    //TICKET:100100000001408252,TICKET:100100000001418640,TICKET:100100000001427796,TICKET:100100000001433063,TICKET:100100000001447091,TICKET:100100000001457489,TICKET:100100000001466870,TICKET:100100000001472147,
                    //TICKET:100100000001485611,TICKET:100100000001495849,TICKET:100100000001508655,TICKET:100100000001518241,TICKET:100100000001522750,TICKET:100100000001537464,TICKET:100100000001547494,TICKET:100100000001557080,
                    //TICKET:100100000001561834,TICKET:100100000001576548,TICKET:100100000001586172,TICKET:100100000001595921,TICKET:100100000001605859,TICKET:100100000001615605,TICKET:100100000001626359,TICKET:100100000001631223,
                    //TICKET:100100000001644292,TICKET:100100000001654048,TICKET:100100000001664559,TICKET:100100000001679423,TICKET:100100000001683215,TICKET:100100000001692804,TICKET:100100000001705290,TICKET:100100000001715527";
                    bStartClicked = false;

                    string[] stemp = sNotesString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] NameValuePair;

                    string sTemp = string.Empty;
                    string sCurrencySymbol = sTemp.GetCurrencySymbol();
                    int iTempVal;

                    bRecalculateTotal = false;

                    foreach (var Entry in stemp)
                    {
                        NameValuePair = Entry.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                        //Skip if the an invalid denom is counted 
                        if (!sCurrencyList.Contains(NameValuePair[0]) && NameValuePair[0] != "TICKET")
                        {
                            LogManager.WriteLog("CashEntry->btnStart_Click: Invalid Denom " + NameValuePair[0], LogManager.enumLogLevel.Debug);
                            continue;
                        }
                        if (NameValuePair.Length != 2)
                            return;
                        switch (NameValuePair[0])
                        {
                            case "TICKET":
                                {
                                    if (!_IsPartCollection) 
                                    this.AddVoucherToDictionary(NameValuePair[1], 0);
                                    break;

                                }
                            case "ONES":
                                {
                                    int.TryParse(txt_Onescnt.Text, out iTempVal);
                                    txt_Onescnt.Text = ((iTempVal + int.Parse(NameValuePair[1])).ToString() == "0" && !bEnableDefaultZero) ? "" : (iTempVal + int.Parse(NameValuePair[1])).ToString();
                                    break;
                                }
                            case "TWOS":
                                {
                                    int.TryParse(txt_Twoscnt.Text, out iTempVal);
                                    txt_Twoscnt.Text = ((iTempVal + int.Parse(NameValuePair[1])).ToString() == "0" && !bEnableDefaultZero) ? "" : (iTempVal + int.Parse(NameValuePair[1])).ToString();
                                    break;
                                }
                            case "FIVES":
                                {
                                    int.TryParse(txt_Fivescnt.Text, out iTempVal);
                                    txt_Fivescnt.Text = ((iTempVal + int.Parse(NameValuePair[1])).ToString() == "0" && !bEnableDefaultZero) ? "" : (iTempVal + int.Parse(NameValuePair[1])).ToString();
                                    break;
                                }
                            case "TENS":
                                {
                                    int.TryParse(txt_Tenscnt.Text, out iTempVal);
                                    txt_Tenscnt.Text = ((iTempVal + int.Parse(NameValuePair[1])).ToString() == "0" && !bEnableDefaultZero) ? "" : (iTempVal + int.Parse(NameValuePair[1])).ToString();
                                    break;
                                }
                            case "TWENTIES":
                                {
                                    int.TryParse(txt_Twentiescnt.Text, out iTempVal);
                                    txt_Twentiescnt.Text = ((iTempVal + int.Parse(NameValuePair[1])).ToString() == "0" && !bEnableDefaultZero) ? "" : (iTempVal + int.Parse(NameValuePair[1])).ToString();
                                    break;
                                }
                            case "FIFTIES":
                                {
                                    int.TryParse(txt_Fiftiescnt.Text, out iTempVal);
                                    txt_Fiftiescnt.Text = ((iTempVal + int.Parse(NameValuePair[1])).ToString() == "0" && !bEnableDefaultZero) ? "" : (iTempVal + int.Parse(NameValuePair[1])).ToString();
                                    break;
                                }
                            case "HUNDREDS":
                                {
                                    int.TryParse(txt_Hundredscnt.Text, out iTempVal);
                                    txt_Hundredscnt.Text = ((iTempVal + int.Parse(NameValuePair[1])).ToString() == "0" && !bEnableDefaultZero) ? "" : (iTempVal + int.Parse(NameValuePair[1])).ToString();
                                    break;
                                }
                            case "TWO_HUNDREDS":
                                {
                                    int.TryParse(txt_TwoHundredscnt.Text, out iTempVal);
                                    txt_TwoHundredscnt.Text = ((iTempVal + int.Parse(NameValuePair[1])).ToString() == "0" && !bEnableDefaultZero) ? "" : (iTempVal + int.Parse(NameValuePair[1])).ToString();
                                    break;
                                }
                            case "FIVE_HUNDREDS":
                                {
                                    int.TryParse(txt_FiveHundredscnt.Text, out iTempVal);
                                    txt_FiveHundredscnt.Text = ((iTempVal + int.Parse(NameValuePair[1])).ToString() == "0" && !bEnableDefaultZero) ? "" : (iTempVal + int.Parse(NameValuePair[1])).ToString();
                                    break;
                                }
                            default:
                                break;
                        }
                    }

                    bRecalculateTotal = true;

                    RecalculateTotal();
                    if (_DictTickets.Count > 0)
                    //if (sTickets.Length > 0)
                    {
                        LogManager.WriteLog("CashEntry->btnStart_Click: Start refresh tickets ", LogManager.enumLogLevel.Debug);
                        RefreshTickets();
                        LogManager.WriteLog("CashEntry->btnStart_Click: refresh tickets Complete", LogManager.enumLogLevel.Debug);
                    }
                    startCounter(false);

                }
                else
                {
                    startCounter(true);
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

        private void btn_DeleteVoucher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strBarcode = ((ParentVoucher.ValidVouchers)(((Button)sender).DataContext)).strBarcode.Trim();
                LogManager.WriteLog("CashEntry->btn_DeleteVoucher_Click() Removing " + strBarcode, LogManager.enumLogLevel.Debug);
                _DictTickets.Remove(strBarcode);
                lsValidTicketsHolder.Remove(lsValidTicketsHolder.Where(c => c.strBarcode == strBarcode).Single<ParentVoucher.ValidVouchers>());
                lvTickets.ItemsSource = null;
                lvTickets.ItemsSource = lsValidTicketsHolder;
                txt_VoucherQuantity.Text = lsValidTicketsHolder.Count.ToString();
                lblcounterWarning.Text = strBarcode + " " + Application.Current.FindResource("MessageID470") as string;
                RecalculateVoucherTotal();
                txt_AddVoucher.Focus();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        private void btn_ClearBils_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.IsBillCounterAmountEditable)
                ClearTextBoxes();
                ClearBills();
        }

        private void btn_ClearVouchers_Click(object sender, RoutedEventArgs e)
        {

            ClearVouchers();
        }

        private void btnProcessException_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string str = string.Empty;
                
                foreach (var item in lvInValidTickets.Items)
                {
                    if (((ParentVoucher.InValidVouchers)item).IsSelected.Value)
                    {
                        str = str + ((ParentVoucher.InValidVouchers)item).strBarcode + ',';
                    }
                }
                if (str.Length > 0)
                {
                    LogManager.WriteLog("CashEntry->btnProcessException_Click Vouchers:" + str, LogManager.enumLogLevel.Debug);
                    oDeclarationBiz.UpdatePPTicketsAsPaid(str, txtAsset.Text);
                    LogManager.WriteLog("CashEntry->btnProcessException_Click: Start refresh tickets ", LogManager.enumLogLevel.Debug);
                    chk_CheckAllInvalidVouchers.IsChecked = false;
                    RefreshTickets();
                    LogManager.WriteLog("CashEntry->btnProcessException_Click: refresh tickets Complete", LogManager.enumLogLevel.Debug);
                }
                else
                {
                    LogManager.WriteLog("CashEntry->btnProcessException_Click: No voucher selected to process", LogManager.enumLogLevel.Debug);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.IsBillCounterAmountEditable)
                ClearTextBoxes();
            ClearBills();
            ClearVouchers();
        }

        private void txtNumber_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (_errorTextBox != null && _errorTextBox != (TextBox)sender)
                {
                    _errorTextBox.Focus();
                    return;
                }
                TextBox obj = (TextBox)sender;
                if (!Settings.OnScreenKeyboard || (_DictShowNumericPad.ContainsKey((TextBox)sender) && !_DictShowNumericPad[(TextBox)sender]))
                    return;
                obj.Text = DisplayNumberPad(obj.Text.Trim(), false, obj.MaxLength <= 0 ? 5 : obj.MaxLength);
                
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        
      private void txAmount_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (_errorTextBox != null && _errorTextBox != (TextBox)sender)
                {
                    _errorTextBox.Focus();
                    return;
                }
                TextBox obj = (TextBox)sender;
                if (!Settings.OnScreenKeyboard || (_DictShowNumericPad.ContainsKey((TextBox)sender) && !_DictShowNumericPad[(TextBox)sender]))
                    return;
                obj.Text = DisplayNumberPad(obj.Text.Trim(), false, obj.MaxLength <= 0 ? 5 : obj.MaxLength);
                ValidateDenom(sender);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void txt_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (_errorTextBox != null && _errorTextBox != (TextBox)sender)
                {
                    _errorTextBox.Focus();
                    return;
                }
                TextBox obj = (TextBox)sender;
                if (!Settings.OnScreenKeyboard)
                    return;
                obj.Text = DisplayNumberPad(obj.Text.Trim(), true, 9);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int iValue;
                string sValue = ((TextBox)sender).Text.Trim();
                if (sValue.Length > 5)
                {
                    sValue = sValue.Substring(0, 5);
                    ((TextBox)sender).Text = sValue;
                }
                int.TryParse(sValue, out iValue);

                switch (((TextBox)sender).Name)
                {

                    case "txt_Onescnt":
                        txt_Ones.Text = ((iValue * 1).ToString()=="0" && !bEnableDefaultZero)?"":(iValue * 1).ToString();
                        break;
                    case "txt_Twoscnt":
                        txt_Twos.Text = ((iValue * 2).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue * 2).ToString();
                        break;
                    case "txt_Fivescnt":
                        txt_Fives.Text = ((iValue * 5).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue * 5).ToString();
                        break;
                    case "txt_Tenscnt":
                        txt_Tens.Text = ((iValue * 10).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue * 10).ToString();
                        break;
                    case "txt_Twentiescnt":
                        txt_Twenties.Text = ((iValue * 20).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue * 20).ToString();
                        break;
                    case "txt_Fiftiescnt":
                        txt_Fifties.Text = ((iValue * 50).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue * 50).ToString();
                        break;
                    case "txt_Hundredscnt":
                        txt_Hundreds.Text = ((iValue * 100).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue * 100).ToString();
                        break;
                    case "txt_TwoHundredscnt":
                        txt_TwoHundreds.Text = ((iValue * 200).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue * 200).ToString();
                        break;
                    case "txt_FiveHundredscnt":
                        txt_FiveHundreds.Text = ((iValue * 500).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue * 500).ToString();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                ((TextBox)sender).Text = "0";
            }
            RecalculateTotal();

        }

        void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender is TextBox)
                {
                    TextBox s = e.Source as TextBox;

                    if (s != null)
                    {
                        if (Settings.TicketDeclaration.ToUpper() == "MANUAL")
                        {
                            if (s == txt_CoinsTotal) { if (ValidCoinType()) s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); }
                            else { s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); }
                        }
                        else
                        {
                            if (s == txt_CoinsTotal) { if (ValidCoinType()) btnApply_Click(sender, e); }
                            else { s.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); }
                        }
                    }
                }
                else if (sender is ComboBox)
                {
                    ComboBox cb = e.Source as ComboBox;

                    if (cb != null)
                    { cb.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); }
                }
                else if (sender is Button)
                {
                    Button b = e.Source as Button;

                    if (b != null && b.Name == "btnMoveNext")
                        btnApply_Click(sender, e);
                }
                e.Handled = true;
            }
        }

        private void GetFocused(object sender, RoutedEventArgs e)
        {
            if (_errorTextBox != null && _errorTextBox != (TextBox)sender)
            {
                _errorTextBox.Focus();
            }
        }

        private void txt_AmountTextChanged(object sender, EventArgs e)
        {
            TextBox obj = null;
            bool errorMessage = false;
            Int32 MaxLength = 99999;
            try
            {
                if (!Settings.IsBillCounterAmountEditable)// || repeatStoper)
                    return;
                int iValue;
                string sValue = ((TextBox)sender).Text.Trim();
                int.TryParse(sValue, out iValue);
               

                switch (((TextBox)sender).Name)
                {
                    case "txt_Ones":                       
                            txt_Onescnt.Text = (iValue / 1).ToString();                      
                        break;
                    case "txt_Twos":
                        if (iValue % 2 != 0)
                        {
                            obj = txt_Twoscnt;
                            lblTwos.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 2 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Twoscnt.Text = ((iValue / 2).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 2).ToString();
                        break;
                    case "txt_Fives":
                        if (iValue % 5 != 0)
                        {
                            obj = txt_Fivescnt;
                            lblFives.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 5 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Fivescnt.Text = ((iValue / 5).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 5).ToString();
                        break;
                    case "txt_Tens":
                        if (iValue % 10 != 0)
                        {
                            obj = txt_Tenscnt;
                            lblTens.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 10 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Tenscnt.Text = ((iValue / 10).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 10).ToString();
                        break;
                    case "txt_Twenties":
                        if (iValue % 20 != 0)
                        {
                            obj = txt_Twentiescnt;
                            lblTwenties.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 20 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Twentiescnt.Text = ((iValue / 20).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 20).ToString();
                        break;
                    case "txt_Fifties":
                        if (iValue % 50 != 0)
                        {
                            obj = txt_Fiftiescnt;
                            lblFifties.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 50 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Fiftiescnt.Text = ((iValue / 50).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 50).ToString();
                        break;
                    case "txt_Hundreds":
                        if (iValue % 100 != 0)
                        {
                            obj = txt_Hundredscnt;
                            lblHundreds.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 100 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Hundredscnt.Text = ((iValue / 100).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 100).ToString();
                        break;
                    case "txt_TwoHundreds":
                        if (iValue % 200 != 0)
                        {
                            obj = txt_TwoHundredscnt;
                            lblTwoHundreds.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 200 > MaxLength)
                            errorMessage = true;
                        else
                            txt_TwoHundredscnt.Text = ((iValue / 200).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 200).ToString();
                        break;
                    case "txt_FiveHundreds":
                        if (iValue % 500 != 0)
                        {
                            obj = txt_FiveHundredscnt;
                            lblFiveHundreds.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 500 > MaxLength)
                            errorMessage = true;
                        else
                            txt_FiveHundredscnt.Text = ((iValue / 500).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 500).ToString();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                ((TextBox)sender).Text = "0";
                _errorOccurred = true;
            }
            if (obj != null)
            {
                ((TextBox)sender).Focus();
                lblcounterWarning.Text = Application.Current.FindResource("MessageID562") as string;
                _errorOccurred = true;
                _errorTextBox = ((TextBox)sender);
                return;
            }
            if (errorMessage)
            {
                ((TextBox)sender).Focus();
                lblcounterWarning.Text = Application.Current.FindResource("MessageID563") as string;
                _errorOccurred = true;
                _errorTextBox = ((TextBox)sender);
                return;
            }
            if (obj == null && !errorMessage)
            {
                lblcounterWarning.Text = "";
                _errorTextBox = null;

                lblFiveHundreds.Visibility = System.Windows.Visibility.Collapsed;
                lblTwoHundreds.Visibility = System.Windows.Visibility.Collapsed;
                lblHundreds.Visibility = System.Windows.Visibility.Collapsed;
                lblFifties.Visibility = System.Windows.Visibility.Collapsed;
                lblTwenties.Visibility = System.Windows.Visibility.Collapsed;
                lblTens.Visibility = System.Windows.Visibility.Collapsed;
                lblFives.Visibility = System.Windows.Visibility.Collapsed;
                lblTwos.Visibility = System.Windows.Visibility.Collapsed;
                lblOnes.Visibility = System.Windows.Visibility.Collapsed;                
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                CloseForm();
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void ValidateAmount()
        {
             _errorOccurred = false;
             foreach (KeyValuePair<TextBox,bool> item in _DictShowNumericPad)
             {
                if (!_errorOccurred && item.Value)
                    txt_AmountTextChanged(item.Key, new EventArgs());
             }

        }

        private void ClearTextBoxes()
        {
            foreach (KeyValuePair<TextBox, bool> item in _DictShowNumericPad)
            {
                item.Key.Text = "0";
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Settings.IsBillCounterAmountEditable)
                {
                    ValidateAmount();
                    if (_errorOccurred) return;
                }

                if (_IsPartCollection)
                    SavePartDeclaration(true);
                else
                    SaveDeclaration(true);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                lblcounterWarning.Text = Application.Current.FindResource("MessageID465") as string;
            }
            finally { cmbPosition.Focus(); }
        }

        private void cmbPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                SetCurrentCollection();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            if (Settings.NotesCounter_AutoStart && btnStart.Visibility == Visibility.Visible)
                btnStart.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void NumericOnly(Object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = IsTextNumeric(e.Text);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        private void CurrencyOnly(Object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = IsCurrency(e.Text, txt_CoinsTotal.Text,((TextBox)sender).SelectionStart);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btn_SearchVoucher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_SearchVoucher.Text.Trim() != string.Empty)
                {
                    var item = lsValidTicketsHolder.Where(c => c.strBarcode == txt_SearchVoucher.Text.Trim());
                    if (item != null && item.Count() > 0)
                    {

                        var ticket = item.Single<ParentVoucher.ValidVouchers>();
                        lvTickets.SelectedItem = ticket;
                        lvTickets.ScrollIntoView(ticket);
                        lblcounterWarning.Text = txt_SearchVoucher.Text + " Found [valid Voucher]";
                    }
                    else
                    {
                        if (lvInValidTickets.ItemsSource != null)
                        {
                            var item2 = ((List<ParentVoucher.InValidVouchers>)lvInValidTickets.ItemsSource).Where(c => c.strBarcode == txt_SearchVoucher.Text.Trim());
                            if (item2 != null)
                            {
                                if (item2.Count() > 0)
                                {
                                    var InValidticket = item2.Single<ParentVoucher.InValidVouchers>();
                                    lvTickets.SelectedItem = InValidticket;
                                    lvInValidTickets.SelectedItem = InValidticket;
                                    lvInValidTickets.ScrollIntoView(InValidticket);
                                    lblcounterWarning.Text = txt_SearchVoucher.Text + " Found [invalid Voucher]";
                                }
                                else
                                {
                                    lblcounterWarning.Text = txt_SearchVoucher.Text + " Not found ";
                                }
                            }
                        }
                        else
                        {
                            lblcounterWarning.Text = txt_SearchVoucher.Text + " Not found ";
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void txt_SearchVoucher_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txt_SearchVoucher.Text = DisplayNumberPad(txt_SearchVoucher.Text.Trim(), false, 0);
        }

        private void chk_CheckAllInvalidVouchers_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectAllInvalidVouchers(chk_CheckAllInvalidVouchers.IsChecked.Value);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        
        #endregion

        #region AddVoucher

        private void txt_AddVoucher_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strBarcode = string.Empty;
            try
            {
                strBarcode = txt_AddVoucher.Text.Trim();
                if (strBarcode == string.Empty)
                    return;

                if (strBarcode.Length == this.iVoucherAutoAddOnLength)
                {
                    this.AddVoucher(strBarcode);
                }
            }
            catch (Exception Ex)
            {
                txt_AddVoucher.Text = string.Empty;
                RemoveTicketsFromDict(strBarcode);
                ExceptionManager.Publish(Ex);
            }
        }

        private void txt_AddVoucher_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            string strBarcode = string.Empty;
            try
            {
                if (!Settings.OnScreenKeyboard)
                    return;
                strBarcode = DisplayNumberPad(txt_AddVoucher.Text.Trim(), false, 0);
                if (strBarcode.Length > 5)
                {
                    this.AddVoucher(strBarcode);
                }
            }
            catch (Exception Ex)
            {
                RemoveTicketsFromDict(strBarcode);
                ExceptionManager.Publish(Ex);
            }
        }

        private void txt_AddVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            string strBarcode = string.Empty;
            try
            {
                if (e.Key == Key.Enter)
                {

                    strBarcode = txt_AddVoucher.Text.Trim();
                    if (strBarcode.Length > 5)
                    {
                        this.AddVoucher(strBarcode);
                    }

                }
            }
            catch (Exception Ex)
            {
                RemoveTicketsFromDict(strBarcode);
                ExceptionManager.Publish(Ex);
            }
        }
        void RemoveTicketsFromDict(string strBarcode)
        {
            try
            {
                _DictTickets.Remove(strBarcode);
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        void AddVoucher(string strBarcode)
        {
            if (AddVoucherToDictionary(strBarcode, 0))
            {
                if (bExcludeVouchersNotInSystem)
                {
                    if (!IsVoucherInSystem(strBarcode))
                    {
                        lblcounterWarning.Text = strBarcode + " Voucher not in System.";
                        return;
                    }
                }
                LogManager.WriteLog("CashEntry->AddVoucher: Start refresh tickets ", LogManager.enumLogLevel.Debug);
                RefreshTickets();
                LogManager.WriteLog("CashEntry->AddVoucher: refresh tickets Complete", LogManager.enumLogLevel.Debug);
                lblcounterWarning.Text = strBarcode + " " + Application.Current.FindResource("MessageID468") as string;
            }
            else
            {
                lblcounterWarning.Text = strBarcode + " " + Application.Current.FindResource("MessageID469") as string;
                txt_AddVoucher.Text = string.Empty;
            }
        }

        #endregion

        #region Navigation
        private void cmbPosition_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (bAutoSaveOnMoveNext)
            {
                if (this.IsUncommited())
                {
                    SaveDeclaration(false);
                }
            }
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //Validate CoinType
                if (!ValidCoinType())
                    return;

                cmbPosition.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Validate CoinType
                if (!ValidCoinType())
                    return;

                if (cmbPosition.SelectedIndex > 0)
                {
                    cmbPosition.SelectedIndex = cmbPosition.SelectedIndex - 1;

                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        private void btnMoveNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Validate CoinType
                if (!ValidCoinType())
                    return;

                if (cmbPosition.SelectedIndex < cmbPosition.Items.Count - 1)
                {
                    cmbPosition.SelectedIndex = cmbPosition.SelectedIndex + 1;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }
        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Validate CoinType
                if (!ValidCoinType())
                    return;

                cmbPosition.SelectedIndex = cmbPosition.Items.Count - 1;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        #endregion
      
        #region Calculate Total
        void RecalculateTotal()
        {

            try
            {
                if (!bRecalculateTotal) // To avoid total calculation on text change during notes count .
                    return;
                LogManager.WriteLog("CashEntry->RecalculateTotal()", LogManager.enumLogLevel.Debug);
                decimal dTotal = 0;

                foreach (var Denom in sCurrencyList)
                {

                  
                    switch (Denom)
                    {
                        case "ONES":
                            {
                                dTotal = dTotal + decimal.Parse((txt_Ones.Text == string.Empty) ? "0" : txt_Ones.Text);
                                break;
                            }
                        case "TWOS":
                            {
                                dTotal = dTotal + decimal.Parse((txt_Twos.Text == string.Empty) ? "0" : txt_Twos.Text);
                                break;
                            }
                        case "FIVES":
                            {
                                dTotal = dTotal + decimal.Parse((txt_Fives.Text == string.Empty) ? "0" : txt_Fives.Text);
                                break;
                            }
                        case "TENS":
                            {
                                dTotal = dTotal + decimal.Parse((txt_Tens.Text == string.Empty) ? "0" : txt_Tens.Text);
                                break;
                            }
                        case "TWENTIES":
                            {
                                dTotal = dTotal + decimal.Parse((txt_Twenties.Text == string.Empty) ? "0" : txt_Twenties.Text);

                                break;
                            }
                        case "FIFTIES":
                            {
                                dTotal = dTotal + decimal.Parse((txt_Fifties.Text == string.Empty) ? "0" : txt_Fifties.Text);

                                break;
                            }
                        case "HUNDREDS":
                            {

                                dTotal = dTotal + decimal.Parse((txt_Hundreds.Text == string.Empty) ? "0" : txt_Hundreds.Text);

                                break;
                            }
                        case "TWO_HUNDREDS":
                            {

                                dTotal = dTotal + decimal.Parse((txt_TwoHundreds.Text == string.Empty) ? "0" : txt_TwoHundreds.Text);

                                break;
                            }
                        case "FIVE_HUNDREDS":
                            {
                                dTotal = dTotal + decimal.Parse((txt_FiveHundreds.Text == string.Empty) ? "0" : txt_FiveHundreds.Text);
                                break;
                            }
                        default:
                            break;

                    }

                }
                dTotal = dTotal+Convert.ToDecimal(((txt_CoinsTotal.Text == string.Empty) ? "0" : txt_CoinsTotal.Text), CultureInfo.CreateSpecificCulture(ExtensionMethods.CurrentCurrenyCulture));
                txtTotal.Text = dTotal.GetUniversalCurrencyFormat();
                RecalculateGrandTotal();

            }
            catch (Exception Ex)
            {
                txtTotal.Text = "0";
                ExceptionManager.Publish(Ex);
            }
        }

        void RecalculateVoucherTotal()
        {

            decimal dVoucherTotal = 0;
            try
            {
                foreach (var item in lsValidTicketsHolder)
                {
                    dVoucherTotal = dVoucherTotal + decimal.Parse(item.iAmount.Value.ToString()) / 100;
                }
                txt_VoucherTotal.Text = dVoucherTotal.GetUniversalCurrencyFormat();

            }
            catch (Exception Ex)
            {
                txt_VoucherTotal.Text = "0";
                ExceptionManager.Publish(Ex);
            }

            RecalculateGrandTotal();

        }

        void RecalculateGrandTotal()
        {
            try
            {
                if (txtTotal.Text.Trim() != string.Empty && txt_VoucherTotal.Text.Trim() != string.Empty)
                    txt_GrandTotal.Text = (
                        Convert.ToDecimal(txtTotal.Text, CultureInfo.CreateSpecificCulture(ExtensionMethods.CurrentCurrenyCulture))
                        + Convert.ToDecimal(txt_VoucherTotal.Text, CultureInfo.CreateSpecificCulture(ExtensionMethods.CurrentCurrenyCulture))).GetUniversalCurrencyFormat();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }
        
        #endregion

        private bool IsTextNumeric(string str)
        {
            Regex reg = new Regex("[^0-9]");
            return reg.IsMatch(str);
        }
        private bool IsCurrency(string str,string currentText, int CurrentKeyPosition)
        {
            char currencyDelimit= '.';
            if(ExtensionMethods.CurrentCurrenyCulture.ToUpper()=="ES-AR")
            {
                currencyDelimit=',';
            }
            int iIndex;
            if (currentText.Length > 8)
                return true;
            iIndex = currentText.IndexOf(currencyDelimit);


            //to allow single "."
            if (iIndex < 0 && str == currencyDelimit.ToString())
            {
                if (CurrentKeyPosition <  currentText.Length - 2)
                {
                    return true;
                }
                return false;
            }
            
            //to check .00
            if (iIndex >= 0 && CurrentKeyPosition > iIndex  )
            {
                if( currentText.Split(currencyDelimit).Length >1 && currentText.Split(currencyDelimit)[1].Length>=2)
                    return true;
            }
            

            Regex reg = new Regex("[^0-9]");
            return reg.IsMatch(str);
        }
        void ClearBills()
        {
            try
            {

                bRecalculateTotal = false;
                LogManager.WriteLog("CashEntry->ClearBills()", LogManager.enumLogLevel.Debug);
                txt_Onescnt.Text =(bEnableDefaultZero)? "0":string.Empty;
                txt_Twoscnt.Text = (bEnableDefaultZero) ? "0" : string.Empty;
                txt_Fivescnt.Text = (bEnableDefaultZero) ? "0" : string.Empty;
                txt_Tenscnt.Text = (bEnableDefaultZero) ? "0" : string.Empty;
                txt_Twentiescnt.Text = (bEnableDefaultZero) ? "0" : string.Empty;
                txt_Fiftiescnt.Text = (bEnableDefaultZero) ? "0" : string.Empty;
                txt_Hundredscnt.Text = (bEnableDefaultZero) ? "0" : string.Empty;
                txt_TwoHundredscnt.Text = (bEnableDefaultZero) ? "0" : string.Empty;
                txt_FiveHundredscnt.Text = (bEnableDefaultZero) ? "0" : string.Empty;
                txt_CoinsTotal.Text = (!bEnableDefaultZero && dZeroDollars.GetUniversalCurrencyFormat() == "0") ? string.Empty : dZeroDollars.GetUniversalCurrencyFormat();
                lblcounterWarning.Text = String.Empty;
                bRecalculateTotal = true;
                RecalculateTotal();
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        void ClearVouchers()
        {
            try
            {

                LogManager.WriteLog("CashEntry->ClearVouchers()", LogManager.enumLogLevel.Debug);

                _DictTickets.Clear();

                //sTickets = string.Empty;
                txt_AddVoucher.Text = string.Empty;
                txt_InvalidVoucherQuantity.Text = "0";
                txt_VoucherQuantity.Text = "0";
                txt_VoucherTotal.Text = dZeroDollars.GetUniversalCurrencyFormat();
                lvTickets.ItemsSource = null;
                lvInValidTickets.ItemsSource = null;
                if (lsValidTicketsHolder != null)
                    lsValidTicketsHolder.Clear();
                if (invalidVouchersList != null)
                    invalidVouchersList.Clear();
                lblcounterWarning.Text = string.Empty;
                txt_SearchVoucher.Text = string.Empty;
                chk_CheckAllInvalidVouchers.IsChecked = false;                
                txt_AddVoucher.Focus();
                RecalculateGrandTotal();
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        void DisableNotesCounter()
        {
            LogManager.WriteLog("CashEntry->DisableCounter()", LogManager.enumLogLevel.Debug);
            btn_ClearBils.Visibility = Visibility.Hidden;
            btn_ClearVouchers.Visibility = Visibility.Hidden;
            btnClearAll.Visibility = Visibility.Hidden;
            btnProcessException.Visibility = Visibility.Hidden;
        }

        private string DisplayNumberPad(string keytext, bool IsCurrency, int MaxLength)
        {
            string strNumberPadText = keytext;
            NumberPadWind ObjNumberpadWind=null;
            try
            {
                ObjNumberpadWind= new NumberPadWind(IsCurrency);
                if (MaxLength > 0)
                    ObjNumberpadWind.setMaxLength(MaxLength);

                ObjNumberpadWind.ValueText = keytext;
                if(!Settings.AllowManualKeyboard)
				 ObjNumberpadWind.ucTicketEntry.txtDisplay.IsReadOnly = true;

                if (IsCurrency)
                {

                    ObjNumberpadWind.ucTicketEntry.txtDisplay.Text = string.Empty;
                }
                ObjNumberpadWind.Owner = this;
                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    if (ObjNumberpadWind.ValueText == "")
                    {
                            strNumberPadText = string.Empty;
                    }
                    else
                    {
                        strNumberPadText =  ObjNumberpadWind.ValueText;
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

        void startCounter(bool isStarted)
        {
            if (isStarted)
            {
                btnClearAll.Visibility = Visibility.Hidden;
                stk_Navigation.Visibility = Visibility.Hidden;
                btnApply.Visibility = Visibility.Hidden;

                grdCashEntry.IsEnabled = false;
                cmbPosition.IsEnabled = false;
                bStartClicked = true;

                btnStart.Content = Application.Current.FindResource("BillsTicketCounter_xaml_btnStop") as string;
                lblcounterWarning.Text = Application.Current.FindResource("MessageID352") as string;
            }
            else
            {
                if(!_IsPartCollection && _IsTicketDeclarationMethodManual)
                    btnClearAll.Visibility = Visibility.Visible;
                stk_Navigation.Visibility = Visibility.Visible;
                btnApply.Visibility = Visibility.Visible;

                grdCashEntry.IsEnabled = true;
                cmbPosition.IsEnabled = true;
                bStartClicked = false;
                btnStart.Content = Application.Current.FindResource("BillsTicketCounter_xaml_btnStart") as string;

                lblcounterWarning.Text = string.Empty;
            }
        }

        void CheckTicketDeclarationMethod()
        {
            try
            {
                LogManager.WriteLog("CashEntry->CheckTicketDeclarationMethod()", LogManager.enumLogLevel.Debug);
                if (_IsPartCollection)
                {
                    txtHeader.Text = FindResource("CDeclaration_xaml_btnCashEntry").ToString();
                    btnClearAll.Visibility = Visibility.Hidden;
                    if (!Settings.EnableCounterInManualCashEntry)
                        btnStart.Visibility = Visibility.Hidden;
                    txt_AddVoucher.IsEnabled = false;
                    btnProcessException.Visibility = Visibility.Hidden;
                    btn_ClearVouchers.IsEnabled = false;
                    txt_AddVoucher.IsEnabled = false;
                    grdCashEntry.HorizontalAlignment = HorizontalAlignment.Center;
                    grp_bills.Width = 500;
                    grp_Total.Width = 500;
                    grdCashEntry.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Star);
                    grdCashEntry.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
                    grdCashEntry.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Star);
                    return;
                }

                if (!_objCollectionHelper.IsNoteCounterVisible())
                {
                    txtHeader.Text = FindResource("CDeclaration_xaml_btnCashEntry").ToString();
                    btnClearAll.Visibility = Visibility.Hidden;
                    if (!Settings.EnableCounterInManualCashEntry)
                        btnStart.Visibility = Visibility.Hidden;
                    txt_AddVoucher.IsEnabled = false;
                    btnProcessException.Visibility = Visibility.Hidden;
                    btn_ClearVouchers.IsEnabled = false;
                    txt_AddVoucher.IsEnabled = false;
                    grdCashEntry.HorizontalAlignment = HorizontalAlignment.Center;
                    grp_bills.Width = 500;
                    grp_Total.Width = 500;
                    grdCashEntry.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Star);
                    grdCashEntry.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
                    grdCashEntry.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Star);
                }
                else
                {
                    _IsTicketDeclarationMethodManual = true;
            	 }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

       
    
        void CloseForm()
        {
            LogManager.WriteLog("CashEntry->CloseForm()", LogManager.enumLogLevel.Debug);
            
            bool bCanClose = true;

            if (bStartClicked)
            {
                lblcounterWarning.Text = Application.Current.FindResource("MessageID355") as string;
                return;
            }
            if (this.IsUncommited())
            {
                if (bShowUncommitedChangesPopup)
                {

                    System.Windows.Forms.DialogResult dr = MessageBox.ShowBox("MessageID463", BMC_Icon.Warning, BMC_Button.YesNoCancel, string.Empty);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (Settings.IsBillCounterAmountEditable)
                        {
                            ValidateAmount();
                            if (!ValidCoinType())
                                return;


                            if (_errorOccurred) return;
                        }
                        this.SaveDeclaration(false);
                    }
                    else if (dr == System.Windows.Forms.DialogResult.Cancel)
                    {
                        bCanClose = false;
                        this.Focus();
                    }
                    else
                    {
                        if (Settings.IsBillCounterAmountEditable)
                            ClearTextBoxes();
                    }

                }
                else if (bAutoSaveOnMoveNext)
                {
                    LogManager.WriteLog("CashEntry->CloseForm():AutoSave on Close", LogManager.enumLogLevel.Debug);
                    if (Settings.IsBillCounterAmountEditable)
                    {
                        ValidateAmount();
                        if (!ValidCoinType())
                            return;
                        if (_errorOccurred) return;
                    }
                    this.SaveDeclaration(false);
                }
            }

            if(bCanClose)
                this.Close();
        }

        bool IsUncommited()
        {

            try
            {
                if (lstCurrentItem == null)
                {
                    LogManager.WriteLog("CashEntry->IsUncommited() Collection not set", LogManager.enumLogLevel.Debug);
                    return false;
                }

                LogManager.WriteLog("CashEntry->IsUncommited()", LogManager.enumLogLevel.Debug);

                if (String.IsNullOrEmpty(txt_CoinsTotal.Text))
                {
                   
                    if ((lstCurrentItem[0]).TotalCoinsValue.GetUniversalCurrencyFormat()!="0" )
                    {
                        return true;
                    }

                    //if (!bEnableDefaultZero)
                    //    txt_CoinsTotal.Text = string.Empty;
                }

                else
                {
                    if (((txt_CoinsTotal.Text.Trim()==string.Empty)?0:decimal.Parse(txt_CoinsTotal.Text)) != (lstCurrentItem[0]).TotalCoinsValue)
                    {
                        return true;
                    }
                }

                if (String.IsNullOrEmpty(txt_Ones.Text))
                {

                  
                  
                    if ( (lstCurrentItem[0]).P100.ToString()!="0")
                    {
                        return true;
                    }
                    //if (!bEnableDefaultZero)
                    //    txt_Ones.Text = string.Empty;
                }
                else
                {
                    if (txt_Ones.Text != (lstCurrentItem[0]).P100.ToString())
                    {
                        return true;
                    }
                }
                if (String.IsNullOrEmpty(txt_Twos.Text))
                {
                    //txt_Twos.Text = "0";
                    if (((lstCurrentItem[0]).P200).ToString()!="0")
                    {
                        return true;
                    }
                    //if (!bEnableDefaultZero)
                    //    txt_Twos.Text = string.Empty;
                }
                else
                {
                    if (txt_Twos.Text != ((lstCurrentItem[0]).P200).ToString())
                    {
                        return true;
                    }
                }
                if (String.IsNullOrEmpty(txt_Fives.Text))
                {
                 //   txt_Fives.Text = "0";
                    if ( ((lstCurrentItem[0]).P500).ToString()!="0")
                    {
                        return true;
                    }
                    //if (!bEnableDefaultZero)
                    //    txt_Fives.Text = string.Empty;
                }
                else
                {
                    if (txt_Fives.Text != ((lstCurrentItem[0]).P500).ToString())
                    {
                        return true;
                    }
                }

                if (String.IsNullOrEmpty(txt_Tens.Text))
                {
                  //  txt_Tens.Text = "0";
                    if ( ((lstCurrentItem[0]).P1000).ToString()!="0")
                    {
                        return true;
                    }
                    //if (!bEnableDefaultZero)
                    //    txt_Tens.Text = string.Empty;
                }
                else
                {
                    if (txt_Tens.Text != ((lstCurrentItem[0]).P1000).ToString())
                    {
                        return true;
                    }
                }
                if (String.IsNullOrEmpty(txt_Twenties.Text))
                {
                   // txt_Twenties.Text = "0";
                    if ( ((lstCurrentItem[0]).P2000).ToString()!="0")
                    {
                        return true;
                    }
                    //if (!bEnableDefaultZero)
                    //    txt_Twenties.Text = string.Empty;
                }
                else
                {
                    if (txt_Twenties.Text != ((lstCurrentItem[0]).P2000).ToString())
                    {
                        return true;
                    }
                }
                if (String.IsNullOrEmpty(txt_Fifties.Text))
                {
                    //txt_Fifties.Text = "0";
                    if ( ((lstCurrentItem[0]).P5000).ToString()!="0")
                    {
                        return true;
                    }
                    //if (!bEnableDefaultZero)
                    //    txt_Fifties.Text = string.Empty;
                }

                else
                {
                    if (txt_Fifties.Text != ((lstCurrentItem[0]).P5000).ToString())
                    {
                        return true;
                    }
                }
                if (String.IsNullOrEmpty(txt_Hundreds.Text))
                {
                    //txt_Hundreds.Text = "0";
                    if ( ((lstCurrentItem[0]).P10000).ToString()!="0")
                    {
                        return true;
                    }
                    //if (!bEnableDefaultZero)
                    //    txt_Hundreds.Text = string.Empty;
                }
                else
                {
                    if (txt_Hundreds.Text != ((lstCurrentItem[0]).P10000).ToString())
                    {
                        return true;
                    }
                }
                if (String.IsNullOrEmpty(txt_TwoHundreds.Text))
                {
                   // txt_TwoHundreds.Text = "0";
                    if (((lstCurrentItem[0]).P20000).ToString()!="0")
                    {
                        return true;
                    }
                    //if (!bEnableDefaultZero)
                    //    txt_TwoHundreds.Text = string.Empty;
                }
                else
                {
                    if (txt_TwoHundreds.Text != ((lstCurrentItem[0]).P20000).ToString())
                    {
                        return true;
                    }
                }
                if (String.IsNullOrEmpty(txt_FiveHundreds.Text))
                {
                    //txt_FiveHundreds.Text = "0";
                    if ( ((lstCurrentItem[0]).P50000).ToString()!="0")
                    {
                        return true;
                    }
                    //if (!bEnableDefaultZero)
                    //    txt_FiveHundreds.Text = string.Empty;
                }
                else
                {
                    if (txt_FiveHundreds.Text != ((lstCurrentItem[0]).P50000).ToString())
                    {
                        return true;
                    }
                }
                if (String.IsNullOrEmpty(txt_VoucherTotal.Text))
                {
                  //  txt_VoucherTotal.Text = "0";
                    if ( (lstCurrentItem[0]).TicketsInValue.GetUniversalCurrencyFormat()!="0")
                    {
                        return true;
                    }
                    //if (!bEnableDefaultZero)
                    //    txt_VoucherTotal.Text = string.Empty;
                }
                else
                {
                    if (txt_VoucherTotal.Text != (lstCurrentItem[0]).TicketsInValue.GetUniversalCurrencyFormat())
                    {
                        return true;
                    }
                }

                int iSimilarTickets = (from srckey in _DictTickets_BKP
                                       join currentkey in _DictTickets
                                       on srckey.Key equals currentkey.Key
                                       select srckey.Key).Count();

                if (_DictTickets.Count != iSimilarTickets)
                {
                    return true;
                }



            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }

            return false;

        }

        bool AddVoucherToDictionary(string strbarcode, int Status)
        {
            if (!_DictTickets.ContainsKey(strbarcode))
            {
                _DictTickets.Add(strbarcode, Status);
                return true;
            }
            else
            {
                LogManager.WriteLog("CashEntry->AddVoucherToDictionary:Duplicate Voucher in collection " + strbarcode, LogManager.enumLogLevel.Debug);
                return false;
            }
        }

        bool IsVoucherInSystem(string strBarcode)
        {
            //Tickets List View
            LogManager.WriteLog("CashEntry->IsVoucherInSystem()", LogManager.enumLogLevel.Debug);

            IMultipleResults Result = _objTicketsHelper.GetValidInvalidVouchers_Counter(strBarcode, txtAsset.Text, ((UndeclaredCollectionRecord)cmbPosition.SelectedItem).CollectionNo, ((UndeclaredCollectionRecord)cmbPosition.SelectedItem).InstallationNo);
            var tempTickets = Result.GetResult<ParentVoucher.ValidVouchers>();
            List<ParentVoucher.InValidVouchers> invalidVouchersList = Result.GetResult<ParentVoucher.InValidVouchers>().ToList();
            if (invalidVouchersList.Count() > 0)
            {
                if (invalidVouchersList[0].Reasonid == 2)
                    return false;
            }
            return true;
        }

        void SelectAllInvalidVouchers(bool IsSelected)
        {
            if (invalidVouchersList != null && invalidVouchersList.Count()>0)
            {
                foreach (var item in invalidVouchersList)
                {
                    if (item.CanUpdate.Value)
                        item.IsSelected = IsSelected;
                }
            }
        }

        private void txt_CoinsTotal_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_CoinsTotal.Text == string.Empty && !bEnableDefaultZero)
                    txt_CoinsTotal.Text = decimal.Parse("0.001").GetUniversalCurrencyFormat();

                if (txt_CoinsTotal.Text != string.Empty)
                {
                    if (!ValidCoinType())                                            
                        return;                    
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);                
            }
        }

        private void txt_CoinsTotal_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);

            if (e.Key == Key.OemComma && sFormat == ",")
                e.Handled = false;

            if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal) && sFormat == ".")
                e.Handled = false;

        }

        private void txt_CoinsTotal_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!BMC.Transport.Settings.OnScreenKeyboard)
                return;

            txt_CoinsTotal.Text = DisplayNumPadWin(txt_CoinsTotal.Text);          
            txt_CoinsTotal.SelectionStart = txt_CoinsTotal.Text.Length;
            txt_CoinsTotal.Focus();
        }

        private string DisplayNumPadWin(string keytext)
        {
            string strNumPadText = string.Empty;
            NumPadWin ObjNumpadWin = new NumPadWin();

            try
            {
                
                ObjNumpadWin.ValueText = keytext;
                ObjNumpadWin.vcValueCalcComp.txtDisplay.Text = keytext;
                ObjNumpadWin.Owner = this;
                if (ObjNumpadWin.ShowDialog() == true)
                {
                    if (ObjNumpadWin.ValueText == "")
                    {
                        strNumPadText = string.Empty;
                    }
                    else
                    {
                        strNumPadText = ObjNumpadWin.ValueText;

                    }
                }
            }
            catch (Exception ex)
            {
                strNumPadText = ObjNumpadWin.ValueText;
                ObjNumpadWin.Close();
                ExceptionManager.Publish(ex);
            }
            return strNumPadText;
        }

        void ValidateDenom(object sender)
        {
            TextBox obj = null;
            bool errorMessage = false;
            Int32 MaxLength = 99999;
            try
            {
                if (!Settings.IsBillCounterAmountEditable)// || repeatStoper)
                    return;
                int iValue;
                string sValue = ((TextBox)sender).Text.Trim();
                int.TryParse(sValue, out iValue);


                switch (((TextBox)sender).Name)
                {
                    case "txt_Ones":
                        txt_Onescnt.Text = (iValue / 1).ToString();
                        break;
                    case "txt_Twos":
                        if (iValue % 2 != 0)
                        {
                            obj = txt_Twoscnt;
                            lblTwos.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 2 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Twoscnt.Text = ((iValue / 2).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 2).ToString();
                        break;
                    case "txt_Fives":
                        if (iValue % 5 != 0)
                        {
                            obj = txt_Fivescnt;
                            lblFives.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 5 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Fivescnt.Text = ((iValue / 5).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 5).ToString();
                        break;
                    case "txt_Tens":
                        if (iValue % 10 != 0)
                        {
                            obj = txt_Tenscnt;
                            lblTens.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 10 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Tenscnt.Text = ((iValue / 10).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 10).ToString();
                        break;
                    case "txt_Twenties":
                        if (iValue % 20 != 0)
                        {
                            obj = txt_Twentiescnt;
                            lblTwenties.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 20 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Twentiescnt.Text = ((iValue / 20).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 20).ToString();
                        break;
                    case "txt_Fifties":
                        if (iValue % 50 != 0)
                        {
                            obj = txt_Fiftiescnt;
                            lblFifties.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 50 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Fiftiescnt.Text = ((iValue / 50).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 50).ToString();
                        break;
                    case "txt_Hundreds":
                        if (iValue % 100 != 0)
                        {
                            obj = txt_Hundredscnt;
                            lblHundreds.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 100 > MaxLength)
                            errorMessage = true;
                        else
                            txt_Hundredscnt.Text = ((iValue / 100).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 100).ToString();
                        break;
                    case "txt_TwoHundreds":
                        if (iValue % 200 != 0)
                        {
                            obj = txt_TwoHundredscnt;
                            lblTwoHundreds.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 200 > MaxLength)
                            errorMessage = true;
                        else
                            txt_TwoHundredscnt.Text = ((iValue / 200).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 200).ToString();
                        break;
                    case "txt_FiveHundreds":
                        if (iValue % 500 != 0)
                        {
                            obj = txt_FiveHundredscnt;
                            lblFiveHundreds.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (iValue / 500 > MaxLength)
                            errorMessage = true;
                        else
                            txt_FiveHundredscnt.Text = ((iValue / 500).ToString() == "0" && !bEnableDefaultZero) ? "" : (iValue / 500).ToString();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                ((TextBox)sender).Text = "0";
                _errorOccurred = true;
            }
            if (obj != null)
            {
                ((TextBox)sender).Focus();
                lblcounterWarning.Text = Application.Current.FindResource("MessageID562") as string;
                _errorOccurred = true;
                _errorTextBox = ((TextBox)sender);
                return;
            }
            if (errorMessage)
            {
                ((TextBox)sender).Focus();
                lblcounterWarning.Text = Application.Current.FindResource("MessageID563") as string;
                _errorOccurred = true;
                _errorTextBox = ((TextBox)sender);
                return;
            }
            if (obj == null && !errorMessage)
            {
                lblcounterWarning.Text = "";
                _errorTextBox = null;

                lblFiveHundreds.Visibility = System.Windows.Visibility.Collapsed;
                lblTwoHundreds.Visibility = System.Windows.Visibility.Collapsed;
                lblHundreds.Visibility = System.Windows.Visibility.Collapsed;
                lblFifties.Visibility = System.Windows.Visibility.Collapsed;
                lblTwenties.Visibility = System.Windows.Visibility.Collapsed;
                lblTens.Visibility = System.Windows.Visibility.Collapsed;
                lblFives.Visibility = System.Windows.Visibility.Collapsed;
                lblTwos.Visibility = System.Windows.Visibility.Collapsed;
                lblOnes.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void txt_Ones_KeyUp(object sender, KeyEventArgs e)
        {
            ValidateDenom(sender);
        }


        private void btn_Close_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {

                cmbPosition.Focus();
                e.Handled = true;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            txt_Ones.Focusable = Settings.IsBillCounterAmountEditable;
            txt_Twos.Focusable = Settings.IsBillCounterAmountEditable;
            txt_Fives.Focusable = Settings.IsBillCounterAmountEditable;
            txt_Tens.Focusable = Settings.IsBillCounterAmountEditable;
            txt_Twenties.Focusable = Settings.IsBillCounterAmountEditable;
            txt_Fifties.Focusable = Settings.IsBillCounterAmountEditable;
            txt_Hundreds.Focusable = Settings.IsBillCounterAmountEditable;
            txt_TwoHundreds.Focusable = Settings.IsBillCounterAmountEditable;
            txt_FiveHundreds.Focusable = Settings.IsBillCounterAmountEditable;
            txt_Onescnt.Focusable = !Settings.IsBillCounterAmountEditable;
            txt_Twoscnt.Focusable = !Settings.IsBillCounterAmountEditable;
            txt_Fivescnt.Focusable = !Settings.IsBillCounterAmountEditable;
            txt_Tenscnt.Focusable = !Settings.IsBillCounterAmountEditable;
            txt_Twentiescnt.Focusable = !Settings.IsBillCounterAmountEditable;
            txt_Fiftiescnt.Focusable = !Settings.IsBillCounterAmountEditable;
            txt_Hundredscnt.Focusable = !Settings.IsBillCounterAmountEditable;
            txt_TwoHundredscnt.Focusable = !Settings.IsBillCounterAmountEditable;
            txt_FiveHundredscnt.Focusable = !Settings.IsBillCounterAmountEditable;

        }



    }

}

