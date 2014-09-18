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
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Threading;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;
using BMC.Business.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Common.LogManagement;
using System.Globalization;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Helper_classes;
using System.ComponentModel;


namespace BMC.Presentation.POS.Views
{

    /// <summary>
    /// Interaction logic for PromoPrint.xaml
    /// </summary>
    public partial class PromoPrint : UserControl
    {
        
        public bool bTotalAmount = true;

        public CPrintPromotionalTickets objPrintTickets = null;
        double ExpiryDays = Convert.ToDouble(Settings.DefaultPromotionalTicketExpireDays);
        public event RoutedEventHandler ValueChanged;
        // CPrintPromotionalTickets CprintprmTick;
        DateTime dtDefaultExpiryDate = DateTime.Now;
        private string _sKeyText = string.Empty;
        public string sFormat = string.Empty;
        DateTime StartDate = DateTime.Now;

        public static string CurrentSiteCulture = BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForRegion");
        public static string CurrentCurrenyCulture = BMC.Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency");



        private PromoPrint()
        {
            InitializeComponent();
            try
            {

                txtPrinterName.Text = Settings.VoucherPrinterName.ToUpper();
                IIssueTicket objCashDeskOperator = IssueTicketBusinessObject.CreateInstance();
                string CurrencySymbol = new RegionInfo(CurrentSiteCulture).CurrencySymbol;
                lblPromoTickAmt.Content = "Promotional Voucher Amount in  " + CurrencySymbol + ":";
                lblTotPromoTickVal.Content = "Total Promotional Voucher Value in " + CurrencySymbol + ":";
                if (Settings.VoucherPrinterName.ToUpper() == "COUPONEXPRESS")
                    txtSerialNumber.Text = objCashDeskOperator.GetPrinterInformation();

                else
                    lblSerialNumber.Visibility = Visibility.Collapsed;

                txtPromoTickAmt.TextChanged += new TextChangedEventHandler(txtPromoTickAmt_TextChanged);
                txtPromoTickAmt.AddHandler(TextBox.PreviewKeyDownEvent, new KeyEventHandler(txtPromoTickAmt_KeyDown), true);


                var d = Convert.ToDecimal(1.1);
                decimal.TryParse(d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out d);
                sFormat = d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)).Substring(1, 1);


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public PromoPrint(CPrintPromotionalTickets obj)
            : this()
        {
            this.objPrintTickets = obj;
        }

        private PromotionVouchers _parent = null;

        public PromoPrint(PromotionVouchers parent)
            : this()
        {
            _parent = parent;
        }

        private void dtpPromoTickExp_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpPromoTickExp.SelectedDate);
                StartDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpPromoExpTime.SelectedHour, tmpPromoExpTime.SelectedMinute, tmpPromoExpTime.SelectedSecond);
                txtPromoExpDate.Text = Convert.ToDateTime(StartDate).ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));

            }
            catch (StackOverflowException ex)
            {
                ExceptionManager.Publish(ex);
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            chkNonCash.IsChecked = true;
            if (ExpiryDays > 0)
            {
                dtDefaultExpiryDate = DateTime.Now.AddDays(ExpiryDays);
                dtpPromoTickExp.SelectedDate = DateTime.Now.Date;

            }
            else if (ExpiryDays == 0)
            {
                dtpPromoTickExp.SelectedDate = DateTime.Now.Date;

            }
            dtpPromoTickExp.IsTodayHighlighted = true;
            dtpPromoTickExp.BlackoutDates.AddDatesInPast();
        }

        private void chkNonCash_Changed(object sender, RoutedEventArgs e)
        {
            chkCash.IsChecked = !chkNonCash.IsChecked.Value;
        }

        private void chkCash_Changed(object sender, RoutedEventArgs e)
        {
            chkNonCash.IsChecked = !chkCash.IsChecked.Value;
        }

        private void txtPromoTickAmt_Change(object sender, RoutedEventArgs e)
        {
          
            double Val = 0.0;
            if (!string.IsNullOrEmpty(txtPromoTickAmt.Text))
            {
                //    txtPromoTickAmt.Text = string.Format("0{0}00", ExtensionMethods.GetCurrencyDecimalDelimiter());
                Val = fnTotalTicketAmount();
                if (bTotalAmount)
                    txtTotPromoTickValData.Text = Val.ToString();
                else
                {
                    txtPromoTickAmt.Text = string.Empty;
                    txtNumPromoTicks.Text = string.Empty;
                    txtNumPromoTicks.Focus();
                    txtTotPromoTickValData.Text = string.Empty;
                    MessageBox.ShowBox("MessageID553", BMC_Icon.Warning);
                    bTotalAmount = true;
                    return;
                }
            }



        }

        void txtPromoTickAmt_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseValueChangeEvent(e);

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
        private void txtNumPromoTicks_Change(object sender, RoutedEventArgs e)
        {
           
            double Val = 0.0;
            if (!string.IsNullOrEmpty(txtNumPromoTicks.Text))
            {

                Val = fnTotalTicketAmount();
                if (bTotalAmount)
                    txtTotPromoTickValData.Text = Val.ToString();
                else
                {
                    txtPromoTickAmt.Text = string.Empty;
                    txtNumPromoTicks.Text = string.Empty;
                    txtNumPromoTicks.Focus();
                    txtTotPromoTickValData.Text = string.Empty;
                    MessageBox.ShowBox("MessageID553", BMC_Icon.Warning);
                    bTotalAmount = true;
                    return;
                }
            }
           // txtTotPromoTickValData.Text = fnTotalTicketAmount().ToString();
        }

        private double fnTotalTicketAmount()
        {
            int intPromoTicketCount = 0;
            double dblPromoTicketAmount = 0.0;
            double dblTotalTicketAmount = 0.0;
            int.TryParse(txtNumPromoTicks.Text.ToString(), out intPromoTicketCount);
            double.TryParse(txtPromoTickAmt.Text.ToString(), out dblPromoTicketAmount);
            if (intPromoTicketCount > 0)
                intPromoTicketCount = int.Parse(txtNumPromoTicks.Text.ToString());
            if (dblPromoTicketAmount > 0)
                dblPromoTicketAmount = Double.Parse(txtPromoTickAmt.Text.ToString());

            try
            {
                dblTotalTicketAmount = intPromoTicketCount * dblPromoTicketAmount;
                if ((Convert.ToDecimal(dblTotalTicketAmount.ToString()) > decimal.MaxValue) || (intPromoTicketCount==0))
                {
                    bTotalAmount = false;
                    txtNumPromoTicks.Text = string.Empty;
                    txtPromoTickAmt.Text = string.Empty;
                    txtNumPromoTicks.Focus();
                    
                }
            }
            catch (Exception ex)
            {
                bTotalAmount = false;
                txtNumPromoTicks.Text = string.Empty;
                txtPromoTickAmt.Text = string.Empty;
                txtNumPromoTicks.Focus();
                ExceptionManager.Publish(ex);
            }
            return dblTotalTicketAmount;
        }

        private void btnPromotionalPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Promo Name validation
                string strPromoName = txtPromoName.Text.ToString();
                if (strPromoName == string.Empty || strPromoName.Length > 75)
                {
                    MessageBox.ShowBox("MessageID409", BMC_Icon.Warning); //Please Enter the Promotional Name
                    txtPromoName.Text = "";
                    txtPromoName.Focus();
                    return;
                }

                //Check whether the promo name already exists
                Promotional objPromotioal = new Promotional();
                bool blPromoNameAlreadyExists = (objPromotioal.BPromotionNameExist(strPromoName) > 0);
                if (blPromoNameAlreadyExists)
                {
                    MessageBox.ShowBox("MessageID407", BMC_Icon.Warning); // Promotion Name already Exist
                    txtPromoName.Text = "";
                    txtPromoName.Focus();
                    return;
                }


                //Ticket Count validation
                int intPromoTicketCount = 0;
                int.TryParse(txtNumPromoTicks.Text.ToString(), out intPromoTicketCount);
                int MaxPromoCount = Convert.ToInt32(Settings.MaximumPromotionalTicketsCount);
                if (MaxPromoCount != 0)
                {
                    if (intPromoTicketCount <= 0 || intPromoTicketCount > int.MaxValue)
                    {

                        MessageBox.ShowBox("MessageID410", BMC_Icon.Warning);//Please Enter the Number of Promotional Tickets
                        txtNumPromoTicks.Text = "";
                        txtNumPromoTicks.Focus();
                        return;
                    }
                    else if (intPromoTicketCount > MaxPromoCount)
                    {
                        MessageBox.ShowBox("MessageID554", BMC_Icon.Warning);
                        txtNumPromoTicks.Text = "";
                        txtNumPromoTicks.Focus();
                        return;
                    }


                }

                else if (MaxPromoCount == 0)
                {
                    if (intPromoTicketCount <= 0 || intPromoTicketCount > int.MaxValue)
                    {

                        MessageBox.ShowBox("MessageID410", BMC_Icon.Warning);//Please Enter the Number of Promotional Tickets
                        txtNumPromoTicks.Text = "";
                        txtNumPromoTicks.Focus();
                        return;
                    }
                }
                //Ticket Amount validation
                double dblPromoTicketAmount = 0.0;
                double MaxPromoTicketAmt = Convert.ToDouble(Settings.MaximumPromotionalTicketAmount);
                Double.TryParse(txtPromoTickAmt.Text.ToString(), out dblPromoTicketAmount);
                if (MaxPromoTicketAmt != 0)
                {
                    if (dblPromoTicketAmount <= 0 || dblPromoTicketAmount > double.MaxValue)
                    {
                        MessageBox.ShowBox("MessageID411", BMC_Icon.Warning); //Please Enter the Promotional Ticket Amount

                        txtPromoTickAmt.Text = "";
                        txtPromoTickAmt.Focus();
                        return;

                    }
                    else if (dblPromoTicketAmount > MaxPromoTicketAmt)
                    {
                        MessageBox.ShowBox("MessageID555", BMC_Icon.Warning); //Please Enter the Promotional Ticket Amount
                        txtPromoTickAmt.Text = "";
                        txtPromoTickAmt.Focus();
                        return;

                    }
                }
                else if (MaxPromoTicketAmt == 0)
                {
                    if (dblPromoTicketAmount <= 0 || dblPromoTicketAmount > double.MaxValue)
                    {
                        MessageBox.ShowBox("MessageID411", BMC_Icon.Warning); //Please Enter the Promotional Ticket Amount
                        txtPromoTickAmt.Text = "";
                        txtPromoTickAmt.Focus();
                        return;
                    }
                }

                string txtTicketAmount = string.Empty;
                if (txtPromoTickAmt.Text != null)
                {
                    txtTicketAmount = txtPromoTickAmt.Text;
                }
                string intPart = string.Empty;
                string decPart = string.Empty;
                if (txtTicketAmount.Contains("."))
                {
                    string[] txtdecimal = txtTicketAmount.Split('.');
                    intPart = txtdecimal[0];
                    decPart = txtdecimal[1];
                    if (decPart.Length > 2)
                    {
                        MessageBox.ShowBox("MessageID557", BMC_Icon.Warning); //Please Enter the Promotional Ticket Amount
                        txtPromoTickAmt.Text = string.Empty;
                        txtPromoTickAmt.Focus();
                        return;
                    }
                }



                //  Expiry Date validation
                DateTime dtPromoExpDate = DateTime.Now;

                DateTime.TryParse(dtpPromoTickExp.SelectedDate.ToString(), out dtPromoExpDate);

                if (ExpiryDays > 0)
                {
                    if (dtpPromoTickExp.SelectedDate.ToString() == null)
                    {
                        MessageBox.ShowBox("MessageID412", BMC_Icon.Warning);
                        dtpPromoTickExp.Focus();
                        return;
                    }
                    else if (dtPromoExpDate > dtDefaultExpiryDate)
                    {
                        MessageBox.ShowBox("MessageID556", BMC_Icon.Warning);
                        dtpPromoTickExp.Focus();
                        return;
                    }
                    else if (dtpPromoTickExp.SelectedDate == DateTime.Now.Date && tmpPromoExpTime.SelectedTime < DateTime.Now.TimeOfDay)
                    {
                        MessageBox.ShowBox("MessageID879", BMC_Icon.Warning);
                        dtpPromoTickExp.Focus();
                        return;
                    }
                }
                else if (ExpiryDays == 0)
                {
                    if (dtpPromoTickExp.SelectedDate.ToString() == null)
                    {
                        MessageBox.ShowBox("MessageID412", BMC_Icon.Warning);//Please Enter the Valid Expiry Date
                        dtpPromoTickExp.Focus();
                        return;
                    }
                    else if (dtpPromoTickExp.SelectedDate == DateTime.Now.Date && tmpPromoExpTime.SelectedTime < DateTime.Now.TimeOfDay)
                    {

                        MessageBox.ShowBox("MessageID879", BMC_Icon.Warning);//Please Enter the Valid Expiry Date
                        dtpPromoTickExp.Focus();
                        return;
                    }
                }



                double dblTotalTicketAmount = 0.0;
                dblTotalTicketAmount = intPromoTicketCount * dblPromoTicketAmount;

                // setting
                int intPromoTicketType = 1;
                if (chkCash.IsChecked == true)
                {
                    intPromoTicketType = 0;
                }

                try
                {
                    //Insert data in Promotional Table
                    DateTime d = dtpPromoTickExp.SelectedDate.Value;

                    DateTime dtCombined = new DateTime(d.Year, d.Month, d.Day, tmpPromoExpTime.SelectedTime.Hours, tmpPromoExpTime.SelectedTime.Minutes, tmpPromoExpTime.SelectedTime.Seconds);
                    int resultInsertPromotional = objPromotioal.BInsertPromotionalTicket(intPromoTicketType, strPromoName, intPromoTicketCount, dblPromoTicketAmount, dtCombined);
                    if (resultInsertPromotional == -1)
                    {
                        MessageBox.ShowBox("MessageID553", BMC_Icon.Information);
                        txtNumPromoTicks.Text = string.Empty;
                        txtPromoTickAmt.Text = string.Empty;
                        txtNumPromoTicks.Focus();
                        return;
                    }
                    MessageBox.ShowBox("MessageID408", BMC_Icon.Information);

                    //Print Dialog Box

                    int PromotionTicketID = objPromotioal.BGetPromotionTicketID(strPromoName);
                    if (PromotionTicketID == 0)
                    {
                        MessageBox.ShowBox("MessageID552", BMC_Icon.Information);
                        return;
                    }

                    CPrintPromotionalTickets tickets = new CPrintPromotionalTickets();
                    MainScreen.ActiveInstance.PromotionActiveElement = tickets;
                    tickets.PrintValues(PromotionTicketID, dblPromoTicketAmount, dtCombined, intPromoTicketType, intPromoTicketCount);
                    //objPrintTickets.PrintValues(PromotionTicketID, dblPromoTicketAmount, dtCombined, intPromoTicketType, intPromoTicketCount);

                    //objPrintTickets.PromoName = strPromoName;
                    //if (intPromoTicketCount > 0)
                    //{
                    //    objPrintTickets.PromoTicketCount = intPromoTicketCount.ToString();
                    //}

                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }

        private void txtNumPromoTicks_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
           
                if (!BMC.Transport.Settings.OnScreenKeyboard)
                    return;

                txtNumPromoTicks.Text = DisplayNumberPad(txtNumPromoTicks.Text);
                txtNumPromoTicks.SelectionStart = txtNumPromoTicks.Text.Length;
                txtNumPromoTicks.Focus();
           
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

        private void txtPromoTickAmt_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
           
                if (!BMC.Transport.Settings.OnScreenKeyboard)
                    return;

                txtPromoTickAmt.Text = DisplayNumPadWin(txtPromoTickAmt.Text);
                txtPromoTickAmt.SelectionStart = txtPromoTickAmt.Text.Length;
                txtPromoTickAmt.Focus();
            
        }
        private string DisplayNumPadWin(string keytext)
        {
            string strNumPadText = string.Empty;
            NumPadWin ObjNumpadWin = new NumPadWin();

            try
            {

                ObjNumpadWin.ValueText = keytext;
                if (ObjNumpadWin.ShowDialog() == true)
                {
                    if (ObjNumpadWin.ValueText == "")
                    {
                        strNumPadText = "0";
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

        private void txtPromoName_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
           
                if (!Settings.OnScreenKeyboard)
                    return;
                txtPromoName.Text = DisplayKeyboard(txtPromoName.Text, string.Empty);
                txtPromoName.SelectionStart = txtPromoName.Text.Length;
           
        }
        public string DisplayKeyboard(string keyText, string type)
        {
            _sKeyText = "";

            using (var objKeyboard = new KeyboardInterface())
            {
                objKeyboard.Closing += ObjKeyboardClosing;
                objKeyboard.KeyString = keyText;
                objKeyboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                // objKeyboard.Owner = CprintprmTick;

                objKeyboard.ShowDialog();
            }
            return _sKeyText;
        }
        void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                _sKeyText = ((KeyboardInterface)sender).KeyString;
            }
            ((KeyboardInterface)sender).Closing -= ObjKeyboardClosing;
        }

        private void txtPromoTickAmt_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);
            var d = Convert.ToDecimal(1.1);
            decimal.TryParse(d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out d);
            sFormat = d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)).Substring(1, 1);
            if (e.Key == Key.OemComma && sFormat == ",")
                e.Handled = false;

            if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal) && sFormat == ".")
                e.Handled = false;
        }

        private void txtPromoExpDate_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void txtNumPromoTicks_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);

        }
    }
}


