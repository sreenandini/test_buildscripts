using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BMC.Common.Utilities;
using System.Collections.Generic;
using BMC.CashDeskOperator;
using BMC.Transport;
using System.Linq;
using System.Collections.ObjectModel;
using BMC.Security;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Helper_classes;


namespace BMC.Presentation.POS.Views
{
    #region Class ManualCashEntry
    /// <summary>
    /// Interaction logic for ManualCashEntry.xaml
    /// </summary>
    public partial class ManualCashEntry : IDisposable
    {

        #region Private Variables
        private TextBox _currentCurrency;
        private ObservableCollection<rsp_GetDeclaredTicketResult> objTicketIn;
        private ObservableCollection<rsp_GetDeclaredTicketResult> objDeletedTicketIn;
        private int? iValidationLength;
        private string _sKeyText = string.Empty;
        private bool isScannerFired = false;
        //private bool isCommonCDOforDeclaration = false;
        #endregion

        #region Public Variables
        public static RoutedCommand ValidateEntryCommand = new RoutedCommand();
        public static int iInstallationNo;
        public static int iCollectionNo;
        public static int BatchNo;
        public static string sAsset;
        public static string sPosition;
        public static string sSiteCode = string.Empty;
        CollectionHelper objColHelper = null;
        #endregion

        public string ExchangeConnectionString;
        public string TicketingConnectionString;
        bool isCommonCDO;
        #region Public Property
        public static decimal TokenValue { get; set; }
        #endregion

        #region Constructor
        public ManualCashEntry()
        {
            InitializeComponent();
            _currentCurrency = txt500;
            _currentCurrency.TextChanged += CurrentCurrencyTextChanged;
            TicketEntryScreen.Visibility = Visibility.Collapsed;
            //txtTicketValue.AddHandler(TextBox.PreviewKeyDownEvent, new KeyEventHandler(txtTicketValue_KeyDown), true);
            txtScanedTicket.AddHandler(TextBox.PreviewKeyDownEvent, new KeyEventHandler(txtScanedTicket_KeyDown), true);
            var d = Convert.ToDecimal(1.1);
            decimal.TryParse(d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out d);
            btnDot.Content = d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)).Substring(1, 1);
            this.DataContext = this;
            ShowCurrencyByRegion();
            MessageBox.childOwner = this;
            TokenValue = 1;

            if (Settings.TicketDeclaration.ToUpper() == "AUTO" || Settings.TicketDeclaration.ToUpper() == "METER")
            {
                txtTicketsIn.IsEnabled = false;
            }
            //if (Security.SecurityHelper.HasAccess("BMC.Presentation.CommonCDOforDeclaration"))
            //{
            //    isCommonCDOforDeclaration = true;
            //}
            //CalculateTotal();

            this.txt500.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            this.txt200.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            this.txt100.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            this.txt50.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            this.txt20.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            this.txt10.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            this.txt5.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            this.txt2.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            this.txt1.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            this.txtTotalCoins.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            this.txtTicketsIn.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CurrentCurrencyTextChanged);
            if (!String.IsNullOrEmpty(sSiteCode))
            {
                this.tbHeader.Text += "\t      " + Application.Current.FindResource("ManualCashEntry_xaml_lblSiteCode").ToString().Trim() + " " + sSiteCode;
                this.tbHeader1.Text += "\t\t  " + Application.Current.FindResource("ManualCashEntry_xaml_lblSiteCode").ToString().Trim() + " " + sSiteCode;
            }
            objColHelper = new CollectionHelper();

        }
        public ManualCashEntry(string strExchangeConnection, string strTicketingConnection)
            : this()
        {
            ExchangeConnectionString = strExchangeConnection;
            TicketingConnectionString = strTicketingConnection;
            if (Security.SecurityHelper.HasAccess("BMC.Presentation.CommonCDOforDeclaration"))
            {
                isCommonCDO = true;
                objColHelper = new CollectionHelper(ExchangeConnectionString);
            }
            else
            {

                isCommonCDO = false;
            }
        }
        #endregion

        #region Public Methods

        private void ShowCurrencyByRegion()
        {
            txt500.Visibility = Visibility.Visible;
            txt200.Visibility = Visibility.Visible;
            txt100.Visibility = Visibility.Visible;
            txt50.Visibility = Visibility.Visible;
            txt20.Visibility = Visibility.Visible;
            txt10.Visibility = Visibility.Visible;
            txt5.Visibility = Visibility.Visible;
            txt2.Visibility = Visibility.Visible;
            txt1.Visibility = Visibility.Visible;
            lblDoller1.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + lblDoller1.Text;
            lblDoller2.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + lblDoller2.Text;
            lbl5.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + lbl5.Text;
            lbl10.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + lbl10.Text;
            lbl20.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + lbl20.Text;
            lbl50.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + lbl50.Text;
            lbl100.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + lbl100.Text;
            lbl200.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + lbl200.Text;
            lbl500.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + lbl500.Text;

            lbl500.Visibility = Visibility.Visible;
            lbl200.Visibility = Visibility.Visible;
            lblDoller2.Visibility = Visibility.Visible;
            lblDoller1.Visibility = Visibility.Visible;

            string sRegionCode = ExtensionMethods.CurrentSiteCulture.ToUpper().Substring(3, 2);

            switch (sRegionCode)
            {
                case "US":
                    {
                        txt500.Visibility = Visibility.Collapsed;
                        txt200.Visibility = Visibility.Collapsed;
                        txt2.Visibility = Visibility.Collapsed;

                        txt1.Visibility = Visibility.Visible;

                        lbl500.Visibility = Visibility.Collapsed;
                        lbl200.Visibility = Visibility.Collapsed;
                        lblDoller2.Visibility = Visibility.Collapsed;

                        lblDoller1.Visibility = Visibility.Visible;

                        txt2.Text = "0";
                        txt200.Text = "0";
                        txt500.Text = "0";

                        txt100.Focus();
                        break;
                    }
                case "IT":
                    {
                        txt2.Visibility = Visibility.Collapsed;
                        txt1.Visibility = Visibility.Collapsed;

                        lblDoller2.Visibility = Visibility.Collapsed;
                        lblDoller1.Visibility = Visibility.Collapsed;

                        txt2.Text = "0";
                        txt1.Text = "0";

                        txt500.Visibility = Visibility.Visible;
                        txt200.Visibility = Visibility.Visible;
                        lbl500.Visibility = Visibility.Visible;
                        lbl200.Visibility = Visibility.Visible;
                        txt500.Focus();
                        break;
                    }
                case "AR":
                    {
                        txt1.Visibility = Visibility.Collapsed;
                        lblDoller1.Visibility = Visibility.Collapsed;
                        txt500.Visibility = Visibility.Collapsed;
                        txt200.Visibility = Visibility.Collapsed;
                        lbl500.Visibility = Visibility.Collapsed;
                        lbl200.Visibility = Visibility.Collapsed;

                        lblDoller2.Visibility = Visibility.Visible;
                        txt2.Visibility = Visibility.Visible;
                        txt1.Text = "0";
                        txt200.Text = "0";
                        txt500.Text = "0";
                        txt100.Focus();
                        break;
                    }
                default:
                    {
                        txt500.Visibility = Visibility.Collapsed;
                        txt200.Visibility = Visibility.Collapsed;
                        txt2.Visibility = Visibility.Collapsed;

                        txt1.Visibility = Visibility.Visible;

                        lbl500.Visibility = Visibility.Collapsed;
                        lbl200.Visibility = Visibility.Collapsed;
                        lblDoller2.Visibility = Visibility.Collapsed;

                        lblDoller1.Visibility = Visibility.Visible;

                        txt2.Text = "0";
                        txt200.Text = "0";
                        txt500.Text = "0";

                        txt100.Focus();
                        break;
                    }
            }

        }

        #endregion

        #region Private Methods

        private void LoadExistingTickets()
        {
            //Localiization - Later
            //Log or Audit -Later
            try
            {
              
                objTicketIn = new ObservableCollection<rsp_GetDeclaredTicketResult>();
                objDeletedTicketIn = new ObservableCollection<rsp_GetDeclaredTicketResult>();

                ObservableCollection<rsp_GetDeclaredTicketResult> objData = objColHelper.GetDeclaredTicket(iCollectionNo, iInstallationNo);

                objTicketIn = objData;
                CalculateTotalandCount();
                lstTicketsIn.ItemsSource = objTicketIn;

                objColHelper.GetValidationLength(iInstallationNo, ref iValidationLength);
                //iValidationLength = 18;
                txtScanedTicket.MaxLength = iValidationLength.Value;
                //txtTicketValue.MaxLength = 8;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private bool IsNumeric(string str)
        {
            foreach (char ch in str.ToCharArray())
            {
                if ((!Char.IsNumber(ch))
                    && ch.ToString() != btnDot.Content.ToString())
                {
                    return false;
                }
            }
            return true;
        }

        private void CalculateTotalandCount()
        {

            decimal TotalAmount = ((decimal)objTicketIn.Sum<rsp_GetDeclaredTicketResult>(k => k.Value));
            txtTicketsTotalAmount.Text = (TotalAmount.GetUniversalCurrencyFormat());
            txtTicketsTotalCount.Text = objTicketIn.Count.ToString();
        }

        private int GetTicketIndex(string sBarCode)
        {
            int i = 0;
            foreach (rsp_GetDeclaredTicketResult obj in lstTicketsIn.Items)
            {
                if (obj.BarCode == sBarCode)
                    return i;
                i++;
            }
            return -1;
        }

        private string DisplayNumberPadDecimal(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumPadWin ObjNumberpadWind = new NumPadWin();
            try
            {

                ObjNumberpadWind.ValueText = keytext;

                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    if (ObjNumberpadWind.ValueText == "")
                    {
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

        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind();

            try
            {
                ObjNumberpadWind.ValueText = keytext;

                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    if (ObjNumberpadWind.ValueText == "")
                    {
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

        private void CalculateTotal()
        {
            try
            {
                txt500.Text = string.IsNullOrEmpty(txt500.Text) ? "0" : txt500.Text;
                txt200.Text = string.IsNullOrEmpty(txt200.Text) ? "0" : txt200.Text;
                txt100.Text = string.IsNullOrEmpty(txt100.Text) ? "0" : txt100.Text;
                txt50.Text = string.IsNullOrEmpty(txt50.Text) ? "0" : txt50.Text;
                txt20.Text = string.IsNullOrEmpty(txt20.Text) ? "0" : txt20.Text;
                txt10.Text = string.IsNullOrEmpty(txt10.Text) ? "0" : txt10.Text;
                txt5.Text = string.IsNullOrEmpty(txt5.Text) ? "0" : txt5.Text;
                txt2.Text = string.IsNullOrEmpty(txt2.Text) ? "0" : txt2.Text;
                txt1.Text = string.IsNullOrEmpty(txt1.Text) ? "0" : txt1.Text;
                txtTotalCoins.Text = string.IsNullOrEmpty(txtTotalCoins.Text) ? "0" : txtTotalCoins.Text;
                txtTicketsIn.Text = string.IsNullOrEmpty(txtTicketsIn.Text) ? "0" : txtTicketsIn.Text;

                switch (Settings.Region.ToUpper())
                {
                    case "AR":

                        txtAmount.Text =
                         (
                           decimal.Parse(txt100.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt50.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt20.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt10.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt5.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt2.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txtTotalCoins.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txtTicketsIn.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         ).ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture));

                        break;


                    case "US":
                        {
                            txtAmount.Text =
                         (
                           decimal.Parse(txt500.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt200.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt100.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt50.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt20.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt10.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt5.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt2.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt1.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txtTotalCoins.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txtTicketsIn.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         ).ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture));

                            break;
                        }
                    default:
                        {
                            txtAmount.Text =
                         (
                           decimal.Parse(txt500.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt200.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt100.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt50.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt20.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt10.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt5.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt2.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txt1.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txtTotalCoins.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         + decimal.Parse(txtTicketsIn.Text, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))
                         ).ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture));

                            break;
                        }

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AddTicket()
        {
            this.AddTicket(true);
        }

        private void AddTicket(bool showMessageBox)
        {
            try
            {
                if (!(txtScanedTicket.Text.Trim().Length > 0 && IsNumeric(txtScanedTicket.Text.Trim()) && (txtScanedTicket.Text.Trim().Length == iValidationLength)))
                {
                    if (showMessageBox)
                    {
                        MessageBox.ShowBox("MessageID264");
                    }
                }
                else
                {
                    //if (!(txtTicketValue.GetCurrencyValueAsString().Trim().Length > 0 && IsNumeric(txtTicketValue.GetCurrencyValueAsString().Trim())
                    //    && decimal.Parse(txtTicketValue.GetCurrencyValueAsString().Trim(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) > 0))
                    //{
                    //    MessageBox.ShowBox("MessageID265");
                    //}
                    //else
                    //{
                    rsp_GetDeclaredTicketResult obj = new rsp_GetDeclaredTicketResult();
                
                    //if (!String.IsNullOrEmpty(sSiteCode))
                    //{
                    //    objCollectionHelper = new CollectionHelper(ExchangeConnectionString);
                    //}
                    //else
                    //{
                    //    objCollectionHelper = new CollectionHelper();
                    //}
                    int? iCount = 0;
                    int? iAmt = 0;
                    decimal iActualAmt = 0;
                    obj.BarCode = txtScanedTicket.Text.Trim();
                    //obj.Value = decimal.Parse(txtTicketValue.GetCurrencyValueAsString().Trim(), new CultureInfo(Common.Utilities.ExtensionMethods.CurrentCurrenyCulture));
                    if (!(GetTicketIndex(obj.BarCode) > -1))
                    {
                        if (Settings.ManualEntryTicketValidation)
                        {
                            objColHelper.IsPaidTicket(txtScanedTicket.Text, iInstallationNo, ref iCount, ref iAmt);
                            //iAmt = 3333;
                            //iCount = 1;
                            iActualAmt = ((decimal)iAmt / 100);
                            obj.Value = iActualAmt;
                        }
                        //else
                        //{
                        //    iCount = 1;
                        //    iActualAmt = obj.Value.Value;
                        //}

                        if (iCount > 0)
                        {
                            if (iActualAmt == obj.Value)
                                objTicketIn.Add(obj);
                            //else
                            //{
                            //    MessageBox.ShowBox("MessageID305");
                            //}
                        }
                        else
                        {
                            if (showMessageBox)
                            {
                                MessageBox.ShowBox("MessageID280");
                            }
                        }
                    }
                    else
                    {
                        if (showMessageBox)
                        {
                            MessageBox.ShowBox("MessageID266");
                        }
                    }
                    //}
                    txtScanedTicket.Clear();
                    txtScanedTicket.Focus();
                    //txtTicketValue.Clear();
                    CalculateTotalandCount();
                }
            }
            catch (Exception ex)
            {
                txtScanedTicket.Clear();
                txtScanedTicket.Focus();
                if (showMessageBox)
                {
                    MessageBox.ShowBox("MessageID265");
                }
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region Events

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CurrentCurrencyTextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateTotal();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;
            if (button == null) return;


            if (_currentCurrency.Text.Trim().Length > 5 && button.Uid != "Arrow")
                return;

            if (string.IsNullOrEmpty(_currentCurrency.Text.Replace(" ", "")))
                _currentCurrency.Text = "0";

            decimal decimalvalue;

            switch (button.Content.ToString())
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if (!decimal.TryParse(_currentCurrency.GetCurrencyValueAsString().Replace(" ", "").Replace(" ", "") + button.Content, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out decimalvalue) || Convert.ToInt32(decimal.Parse(_currentCurrency.GetCurrencyValueAsString().Replace(" ", "") + button.Content, NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture))).ToString().Length > 8)
                        return;
                    _currentCurrency.Text = (decimalvalue.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)));
                    break;
                case ".":
                case ",":
                    if (!decimal.TryParse(_currentCurrency.GetCurrencyValueAsString().Replace(" ", "") + button.Content + "1", NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out decimalvalue) || _currentCurrency.GetCurrencyValueAsString().Replace(" ", "").Length > 9)
                        return;
                    _currentCurrency.Text = (_currentCurrency.GetCurrencyValueAsString() + button.Content.ToString());
                    break;
                default:
                    _currentCurrency.Text = (_currentCurrency.GetCurrencyValueAsString().Replace(" ", "").Substring(0, _currentCurrency.GetCurrencyValueAsString().Replace(" ", "").Length - 1));
                    break;
            }

            _currentCurrency.Focus();
        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentCurrency == null) return;

                TextBox objText = (sender as TextBox);
                TicketEntryScreen.Visibility = Visibility.Collapsed;
                btnDot.Visibility = Visibility.Collapsed;

                if (objText.Name == "txtTicketsIn")
                {
                    TicketEntryScreen.Visibility = Visibility.Visible;
                    txtScanedTicket.Text = "";
                    //txtTicketValue.Text = "";
                    LoadExistingTickets();
                    txtScanedTicket.Clear();
                    txtScanedTicket.Focus();
                    return;
                }
                if (objText.Name == "txtTotalCoins")
                {
                    btnDot.Visibility = Visibility.Visible;
                }

                _currentCurrency = objText;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Populate the list of vouchers, if the voucher is deleted from the list and 
            // the change is not updated on the Manual Cash Entry screen (Cancel in Manual Cash Entry) 
            if (objDeletedTicketIn != null && objDeletedTicketIn.Count > 0)
            {
                foreach (rsp_GetDeclaredTicketResult objDel in objDeletedTicketIn)
                {
                    if (objTicketIn != null && !objTicketIn.Contains(objDel))
                    {
                        txtScanedTicket.Text = objDel.BarCode;
                        AddTicket(false);
                    }
                }
                btnTicketsAccept_Click(sender, e);
            }

            Close();

            // LockHandler Lock = new LockHandler();
            //Lock.DeleteLockRecord(SecurityHelper.CurrentUser.SecurityUserID, "", "DECLARATION", "COLL", BatchNo.ToString());
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_currentCurrency.Name == "txtTotalCoins" || _currentCurrency.Name == "txtTicketsIn")
                btnDot.Visibility = Visibility.Visible;
            else
                btnDot.Visibility = Visibility.Collapsed;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Don't delete the below code.
            txtTotalCoins.Text = txtTotalCoins.Text;
            var hasError = Validation.GetHasError(txt500) ||
                          Validation.GetHasError(txt200) ||
                          Validation.GetHasError(txt100) ||
                          Validation.GetHasError(txt50) ||
                          Validation.GetHasError(txt20) ||
                          Validation.GetHasError(txt10) ||
                          Validation.GetHasError(txt5) ||
                          Validation.GetHasError(txt2) ||
                          Validation.GetHasError(txt1) ||
                          Validation.GetHasError(txtTotalCoins)
                          ;
            if (hasError)
            {
                MessageBox.ShowBox("MessageID314");
                if (Validation.GetHasError(txt500))
                {
                    txt500.Focus();
                }
                if (Validation.GetHasError(txt200))
                {
                    txt200.Focus();
                }
                if (Validation.GetHasError(txt100))
                {
                    txt100.Focus();
                }
                if (Validation.GetHasError(txt50))
                {
                    txt50.Focus();
                }
                if (Validation.GetHasError(txt20))
                {
                    txt20.Focus();
                }
                if (Validation.GetHasError(txt10))
                {
                    txt10.Focus();
                }
                if (Validation.GetHasError(txt5))
                {
                    txt5.Focus();
                }
                if (Validation.GetHasError(txt2))
                {
                    txt2.Focus();
                }
                if (Validation.GetHasError(txt1))
                {
                    txt1.Focus();
                }
                if (Validation.GetHasError(txtTotalCoins))
                {
                    txtTotalCoins.Focus();
                }
                return;
            }


            DialogResult = true;
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void btnTicketsAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnTicketsAccept.IsEnabled = false;
                //CollectionHelper objCollectionHelper = null;
                //if (!String.IsNullOrEmpty(sSiteCode))
                //{
                //    objCollectionHelper = new CollectionHelper(ExchangeConnectionString);
                //}
                //else
                //{
                //    objCollectionHelper = new CollectionHelper();
                //}

              
                foreach (rsp_GetDeclaredTicketResult obj in objTicketIn)
                {
                    if (obj.ID == 0)
                    {
                        int? i = 0;
                        objColHelper.InsertDeclaredTicket(obj.BarCode, decimal.Parse(obj.Value.ToString()),
                            SecurityHelper.CurrentUser.SecurityUserID,
                            0, 0, iInstallationNo, iCollectionNo, ref i);
                        objColHelper.UpdateVoucherCollection(obj.BarCode, "1");
                    }
                }

                foreach (rsp_GetDeclaredTicketResult objDel in objDeletedTicketIn)
                {
                    if (objDel.ID != 0)
                    {
                        objColHelper.DeleteDeclaredTicket(objDel.ID, iInstallationNo, iCollectionNo);
                        objColHelper.UpdateVoucherCollection(objDel.BarCode, "0");
                    }
                }

                txtTicketsIn.Text = (txtTicketsTotalAmount.GetCurrencyValueAsString());

                TicketEntryScreen.Visibility = Visibility.Collapsed;
                btnManualCashEntry.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnTicketsAccept.IsEnabled = true;
            }
        }

        private void DeleteTicket(object sender, RoutedEventArgs e)
        {
            try
            {
                //var i = objTicketIn.IndexOf(((rsp_GetDeclaredTicketResult)((Button)sender).DataContext));
                int i = GetTicketIndex(((rsp_GetDeclaredTicketResult)((Button)sender).DataContext).BarCode);
                objDeletedTicketIn.Add(objTicketIn[i]);
                objTicketIn.RemoveAt(i);
                CalculateTotalandCount();
                txtScanedTicket.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTicketsCancel_Click(object sender, RoutedEventArgs e)
        {
            TicketEntryScreen.Visibility = Visibility.Collapsed;
            btnManualCashEntry.Focus();
        }

        private void AddButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (isScannerFired) //check done not to fire the verify event twice while verifying a ticket using scanner
            {
                isScannerFired = false;
                return;
            }
            if ((sender is System.Windows.Controls.TextBox))
                isScannerFired = true;
            else
                isScannerFired = false;

            if (isScannerFired)
                MessageBox.ShowBox("MessageID314");
            //AddTicket();
        }

        private void txtScanedTicket_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtScanedTicket.Text = DisplayNumberPad(txtScanedTicket.Text.Trim());
            if (txtScanedTicket.Text.Trim() != "")
                AddTicket();
        }

        //private void txtTicketValue_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    if (!Settings.OnScreenKeyboard)
        //        return;
        //    txtTicketValue.Text = DisplayNumberPadDecimal(txtTicketValue.Text.Trim());
        //}

        private void txtScanedTicket_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);
        }

        //private void txtTicketValue_KeyDown(object sender, KeyEventArgs e)
        //{
        //    e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);

        //    if (e.Key == Key.OemComma && btnDot.Content.ToString() == ",")
        //        e.Handled = false;

        //    if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal )&& btnDot.Content.ToString() == ".")
        //        e.Handled = false;
        //}

        //private void txtTicketValue_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    //if (txtTicketValue.Text.Trim().Length > 0 && IsNumeric(txtTicketValue.Text))
        //    //    txtTicketValue.Text = decimal.Parse(txtTicketValue.Text).GetUniversalCurrencyFormat();
        //}

        #endregion

        private void btnNoteCounter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnNoteCounter.IsEnabled = false;
                if (txtTicketsIn.Text != "0.00")
                {
                    if (!string.IsNullOrEmpty(txtTicketsIn.Text) && (txtTicketsIn.Text != "0"))
                    {
                        MessageBox.ShowBox("MessageID363", BMC_Icon.Warning, BMC_Button.OK);
                        return;
                    }
                }
                BillsTicketCounter objBillsTicketCounter = null;

                if (!isCommonCDO)
                {
                    objBillsTicketCounter = new BillsTicketCounter();
                }
                else
                {
                    objBillsTicketCounter = new BillsTicketCounter(ExchangeConnectionString, TicketingConnectionString);
                }
                objBillsTicketCounter.Owner = this;
                objBillsTicketCounter.txtAsset.Text = sAsset;
                objBillsTicketCounter.txtPosition.Text = sPosition;
                objBillsTicketCounter.iCollectionNo = iCollectionNo;
                objBillsTicketCounter.sPosition = sPosition;
                objBillsTicketCounter.txtGame.Text = "";

                if (objBillsTicketCounter.ShowDialog() != true)
                {
                    if (Settings.HANDLE_EXCEPTIONTICKETS_COUNTER)
                    {
                        objColHelper.DiscardExceptionVoucherChanges(iCollectionNo, iInstallationNo, true);
                    }
                    LoadExistingTickets();
                    txtTicketsIn.Text = txtTicketsTotalAmount.Text.ToString();
                    return;
                }

                if (Settings.HANDLE_EXCEPTIONTICKETS_COUNTER)
                {
                    objColHelper.DiscardExceptionVoucherChanges(iCollectionNo, iInstallationNo, false);
                }

                if (!objBillsTicketCounter.txtTotal.Text.IsNullOrEmpty())
                {
                    txtTicketsIn.Text = (txtTicketsIn.GetCurrencyValueAsDecimal()
                                                            + objBillsTicketCounter.txtTotal.GetCurrencyValueAsDecimal()).ToString();


              
                    int? i = 0;

                    foreach (var listitem in objBillsTicketCounter.lsValidTicketsHolder)
                    {
                        objColHelper.InsertDeclaredTicket(listitem.strBarcode, Convert.ToDecimal(listitem.iAmount) / 100,
                                                                    SecurityHelper.CurrentUser.SecurityUserID,
                                                                     0, 0, iInstallationNo, iCollectionNo, ref i);
                        objColHelper.UpdateVoucherCollection(listitem.strBarcode, "1");
                    }
                }

                switch (ExtensionMethods.CurrentSiteCulture)
                {
                    case "it-IT":
                        {
                            if (objBillsTicketCounter.BillsDict.ContainsKey("FIVES"))
                                txt5.SetCurrencyText(objBillsTicketCounter.BillsDict["FIVES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("TENS"))
                                txt10.SetCurrencyText(objBillsTicketCounter.BillsDict["TENS"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("TWENTIES"))
                                txt20.SetCurrencyText(objBillsTicketCounter.BillsDict["TWENTIES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("FIFTIES"))
                                txt50.SetCurrencyText(objBillsTicketCounter.BillsDict["FIFTIES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("HUNDREDS"))
                                txt100.SetCurrencyText(objBillsTicketCounter.BillsDict["HUNDREDS"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("TWO_HUNDREDS"))
                                txt200.SetCurrencyText(objBillsTicketCounter.BillsDict["TWO_HUNDREDS"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("FIVE_HUNDREDS"))
                                txt500.SetCurrencyText(objBillsTicketCounter.BillsDict["FIVE_HUNDREDS"]);
                            break;
                        }

                    case "es-ar":
                        {
                            if (objBillsTicketCounter.BillsDict.ContainsKey("TWOS"))
                                txt2.Text = objBillsTicketCounter.BillsDict["TWOS"].ToString();//txt1.SetCurrencyText(objBillsTicketCounter.BillsDict["ONES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("FIVES"))
                                txt5.SetCurrencyText(objBillsTicketCounter.BillsDict["FIVES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("TENS"))
                                txt10.SetCurrencyText(objBillsTicketCounter.BillsDict["TENS"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("TWENTIES"))
                                txt20.SetCurrencyText(objBillsTicketCounter.BillsDict["TWENTIES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("FIFTIES"))
                                txt50.SetCurrencyText(objBillsTicketCounter.BillsDict["FIFTIES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("HUNDREDS"))
                                txt100.SetCurrencyText(objBillsTicketCounter.BillsDict["HUNDREDS"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("TWO_HUNDREDS"))
                                txt200.SetCurrencyText(objBillsTicketCounter.BillsDict["TWO_HUNDREDS"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("FIVE_HUNDREDS"))
                                txt500.SetCurrencyText(objBillsTicketCounter.BillsDict["FIVE_HUNDREDS"]);
                            break;
                        }

                    default:
                        {
                            if (objBillsTicketCounter.BillsDict.ContainsKey("ONES"))
                                txt1.Text = objBillsTicketCounter.BillsDict["ONES"].ToString();//txt1.SetCurrencyText(objBillsTicketCounter.BillsDict["ONES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("FIVES"))
                                txt5.Text = objBillsTicketCounter.BillsDict["FIVES"].ToString();//txt5.SetCurrencyText(objBillsTicketCounter.BillsDict["FIVES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("TENS"))
                                txt10.Text = objBillsTicketCounter.BillsDict["TENS"].ToString();//txt10.SetCurrencyText(objBillsTicketCounter.BillsDict["TENS"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("TWENTIES"))
                                txt20.Text = objBillsTicketCounter.BillsDict["TWENTIES"].ToString();//txt20.SetCurrencyText(objBillsTicketCounter.BillsDict["TWENTIES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("FIFTIES"))
                                txt50.Text = objBillsTicketCounter.BillsDict["FIFTIES"].ToString();//txt50.SetCurrencyText(objBillsTicketCounter.BillsDict["FIFTIES"]);
                            if (objBillsTicketCounter.BillsDict.ContainsKey("HUNDREDS"))
                                txt100.Text = objBillsTicketCounter.BillsDict["HUNDREDS"].ToString();//txt100.SetCurrencyText(objBillsTicketCounter.BillsDict["HUNDREDS"]);
                            break;
                        }

                }

                CalculateTotal();
            }
            finally
            {
                btnNoteCounter.IsEnabled = true;
            }

        }

        private void txtScanedTicket_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (txtScanedTicket.Text.Trim().Length >= iValidationLength)
            {
                AddTicket();
            }
        }

        private void txtScanedTicket_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txtScanedTicket.Text.Trim().Length >= iValidationLength)
            //{
            //    MessageBox.ShowBox("MessageID314");
            //    MessageBox.ShowBox("MessageID314");
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //btnNoteCounter.Visibility =( new CollectionHelper().IsNoteCounterVisible() ? Visibility.Visible : Visibility.Collapsed);
            btnNoteCounter.Visibility = Visibility.Collapsed;
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
                        ((BMC.Presentation.POS.Views.ManualCashEntry)(this)).Loaded -= (this.Window_Loaded);
                        this.txtScanedTicket.PreviewMouseUp -= (this.txtScanedTicket_PreviewMouseUp);
                        this.txtScanedTicket.KeyDown -= (this.txtScanedTicket_KeyDown);
                        this.txtScanedTicket.PreviewTextInput -= (this.txtScanedTicket_PreviewTextInput);
                        this.txtScanedTicket.KeyUp -= (this.txtScanedTicket_KeyUp);
                        this.btnTicketsAccept.Click -= (this.btnTicketsAccept_Click);
                        this.btnTicketsCancel.Click -= (this.btnTicketsCancel_Click);
                        this.btnExit.Click -= (this.btnExit_Click);
                        this.txt500.GotFocus -= (this.txt_GotFocus);
                        this.txt500.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txt500.LostFocus -= (this.txt_LostFocus);
                        this.txt200.GotFocus -= (this.txt_GotFocus);
                        this.txt200.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txt200.LostFocus -= (this.txt_LostFocus);
                        this.txt100.GotFocus -= (this.txt_GotFocus);
                        this.txt100.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txt100.LostFocus -= (this.txt_LostFocus);
                        this.txt50.GotFocus -= (this.txt_GotFocus);
                        this.txt50.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txt50.LostFocus -= (this.txt_LostFocus);
                        this.txt20.GotFocus -= (this.txt_GotFocus);
                        this.txt20.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txt20.LostFocus -= (this.txt_LostFocus);
                        this.txt10.GotFocus -= (this.txt_GotFocus);
                        this.txt10.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txt10.LostFocus -= (this.txt_LostFocus);
                        this.txt5.GotFocus -= (this.txt_GotFocus);
                        this.txt5.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txt5.LostFocus -= (this.txt_LostFocus);
                        this.txt2.GotFocus -= (this.txt_GotFocus);
                        this.txt2.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txt2.LostFocus -= (this.txt_LostFocus);
                        this.txt1.GotFocus -= (this.txt_GotFocus);
                        this.txt1.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txt1.LostFocus -= (this.txt_LostFocus);
                        this.txtTotalCoins.GotFocus -= (this.txt_GotFocus);
                        this.txtTotalCoins.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txtTotalCoins.LostFocus -= (this.txt_LostFocus);
                        this.txtTicketsIn.GotFocus -= (this.txt_GotFocus);
                        this.txtTicketsIn.TextChanged -= (this.CurrentCurrencyTextChanged);
                        this.txtTicketsIn.LostFocus -= (this.txt_LostFocus);


                        this.btnNoteCounter.Click -= (this.btnNoteCounter_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("ManualCashEntry objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ManualCashEntry"/> is reclaimed by garbage collection.
        /// </summary>
        ~ManualCashEntry()
        {
            Dispose(false);
        }

        #endregion

    }
    #endregion

    #region Class ManualCashEntryModel
    public class ManualCashEntryModel : ViewModelBase
    {
        private string _c500 = "0";
        private string _c200 = "0";
        private string _c100 = "0";
        private string _c50 = "0";
        private string _c20 = "0";
        private string _c10 = "0";
        private string _c5 = "0";
        private string _c2 = "0";
        private string _c1 = "0";
        private string _coinValue = "0";
        private string _TicketsIn = "0";
        private string _AttendantPay = "0";

        public string C500
        {
            get { return _c500; }
            set
            {
                _c500 = value;
                PropertyChangedEvent("C500");
            }
        }
        public string C200
        {
            get { return _c200; }
            set
            {
                _c200 = value;
                PropertyChangedEvent("C200");
            }
        }
        public string C100
        {
            get { return _c100; }
            set
            {
                _c100 = value;
                PropertyChangedEvent("C100");
            }
        }
        public string C50
        {
            get { return _c50; }
            set
            {
                _c50 = value;
                PropertyChangedEvent("C50");
            }
        }
        public string C20
        {
            get { return _c20; }
            set
            {
                _c20 = value;
                PropertyChangedEvent("C20");
            }
        }
        public string C10
        {
            get { return _c10; }
            set
            {
                _c10 = value;
                PropertyChangedEvent("C10");
            }
        }
        public string C5
        {
            get { return _c5; }
            set
            {
                _c5 = value;
                PropertyChangedEvent("C5");
            }
        }
        public string C2
        {
            get { return _c2; }
            set
            {
                _c2 = value;
                PropertyChangedEvent("C2");
            }
        }
        public string C1
        {
            get { return _c1; }
            set
            {
                _c1 = value;
                PropertyChangedEvent("C1");
            }
        }
        public string Coins
        {
            get { return _coinValue; }
            set
            {
                _coinValue = value;
                PropertyChangedEvent("Coins");
            }
        }
        public string TicketsIn
        {
            get { return _TicketsIn; }
            set
            {
                _TicketsIn = value;
                PropertyChangedEvent("TicketsIn");
            }
        }

        public string AttendantPay
        {
            get { return _AttendantPay; }
            set
            {
                _AttendantPay = value;
                PropertyChangedEvent("AttendantPay");
            }
        }
    }
    #endregion

    #region Class ViewModelBase
    public class ViewModelBase : INotifyPropertyChanged
    {
        public void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
    #endregion

    #region Class ManualCashEntryValidationRule
    public class ManualCashEntryValidationRule : ValidationRule
    {
        private decimal _denom = Convert.ToDecimal("0.1");

        public decimal Amount { get; set; }
        public decimal Denom { get { return _denom; } set { _denom = value; } }
        public string FieldName { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal dc;

            if (value != null && value.ToString().Length > 0)
            {
                if (value.ToString().IndexOf(ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol()) > -1)
                {
                    string[] sText = value.ToString().Split(' ');
                    if (sText[1] != null && sText[1].Length > 0)
                        value = sText[1];
                }
            }

            if (!decimal.TryParse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out dc))
                return new ValidationResult(true, null);


            switch (FieldName)
            {
                case "C500":
                    if (decimal.Parse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) % 500 == 0)
                        return new ValidationResult(true, null);

                    return new ValidationResult(false, string.Format("Invalid Amount! Entered Amount should be a Multiple of [{0}]", 500));

                case "C200":
                    if (decimal.Parse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) % 200 == 0)
                        return new ValidationResult(true, null);
                    return new ValidationResult(false, string.Format("Invalid Amount! Entered Amount should be a Multiple of [{0}]", 200));

                case "C100":
                    if (decimal.Parse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) % 100 == 0)
                        return new ValidationResult(true, null);
                    return new ValidationResult(false, string.Format("Invalid Amount! Entered Amount should be a Multiple of [{0}]", 100));

                case "C50":
                    if (decimal.Parse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) % 50 == 0)
                        return new ValidationResult(true, null);
                    return new ValidationResult(false, string.Format("Invalid Amount! Entered Amount should be a Multiple of [{0}]", 50));

                case "C20":
                    if (decimal.Parse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) % 20 == 0)
                        return new ValidationResult(true, null);
                    return new ValidationResult(false, string.Format("Invalid Amount! Entered Amount should be a Multiple of [{0}]", 20));

                case "C10":
                    if (decimal.Parse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) % 10 == 0)
                        return new ValidationResult(true, null);
                    return new ValidationResult(false, string.Format("Invalid Amount! Entered Amount should be a Multiple of [{0}]", 10));

                case "C5":
                    if (decimal.Parse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) % 5 == 0)
                        return new ValidationResult(true, null);
                    return new ValidationResult(false, string.Format("Invalid Amount! Entered Amount should be a Multiple of [{0}]", 5));

                case "C2":
                    if (decimal.Parse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) % 2 == 0)
                        return new ValidationResult(true, null);
                    return new ValidationResult(false, string.Format("Invalid Amount! Entered Amount should be a Multiple of [{0}]", 2));

                case "C1":
                    if (decimal.Parse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) % 1 == 0)
                        return new ValidationResult(true, null);
                    return new ValidationResult(false, string.Format("Invalid Amount! Entered Amount should be a Multiple of [{0}]", 1));

                case "Coins":
                    if (ManualCashEntry.TokenValue == 0)
                        return new ValidationResult(true, null);
                    if (decimal.Parse(value.ToString(), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)) % ManualCashEntry.TokenValue == 0)
                        return new ValidationResult(true, null);
                    return new ValidationResult(false, string.Format("Invalid Amount! Entered Amount should be a Multiple of [{0}]", Denom));
                case "TicketsIn":
                    return new ValidationResult(true, null);
                case "AttendantPay":
                    return new ValidationResult(true, null);
                default:
                    return new ValidationResult(true, null);

            }
        }
    }
    #endregion
}
