using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.Utilities;
using System;
using System.Globalization;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
	/// <summary>
	/// Interaction logic for ValueCalcComp.xaml
	/// </summary>
	public partial class CTicketEntry : IDisposable
	{
        /// <summary>
        /// Event raised when the value in the textbox changes
        /// </summary>
        public event RoutedEventHandler ValueChanged;
        public event BMC.Presentation.CRedeemTicket.ScannerClick ClickEvent;
        public event BMC.Presentation.CExceptionVoucher.ScannerClick ClickEvent_ExceptionVoucher;
        public event BMC.Presentation.CRedeemTicket.ClickClear ClickClearEvent;
        IPlayerInformation playerInformationBusinessObject = PlayerInformationBusinessObject.CreateInstance();

        public event BMC.Presentation.CVoidTicket.ScannerClick vClickEvent;
        public event BMC.Presentation.CVoidTicket.ClickClear vClickClearEvent;
        public event BMC.Presentation.CManualAttendantPays.ScannerClick mClickEvent;
        public event BMC.Presentation.CViewHandpay.ScannerClick hClickEvent;
        //public event BMC.Presentation.POS.Views.CShortpays.ScannerClick sClickEvent;
        
        private int _iMaxLength;
        private bool _isCurrencyPad;
        private string sFormat;
        public bool isPlayerClub { get; set; }
        public bool isCurrencyPad { get; set; }
        private string s_ActualText = string.Empty;
        public string s_UnformattedText = string.Empty;

        public event RoutedEventHandler EnterClicked;
        public event RoutedEventHandler CancelClicked;
        
        public CTicketEntry()
		{
			this.InitializeComponent();
            BindButtonEvents();
            txtDisplay.TextChanged += new TextChangedEventHandler(txtDisplay_TextChanged);            
            MaxLength = 18;
            txtDisplay.MaxLength = MaxLength;
            isPlayerClub = false;
            txtDisplay.AddHandler(TextBox.PreviewKeyDownEvent, new KeyEventHandler(txtDisplay_KeyDown), true);
            var d = Convert.ToDecimal(1.1);
            if (isCurrencyPad)
            {
                decimal.TryParse(d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out d);
                sFormat = d.ToString(new System.Globalization.CultureInfo(ExtensionMethods.CurrentCurrenyCulture)).Substring(1, 1);
            }
		}

        void Number_Click(object sender, RoutedEventArgs e)
        {
            if (!isCurrencyPad)
            {
                NonCurrency(sender, e);
                return;
            }
            s_UnformattedText = s_UnformattedText.TrimStart('0');

            if (s_UnformattedText.Length < txtDisplay.MaxLength)
                s_UnformattedText += ((Button)sender).Content.ToString();

            if (s_UnformattedText.Length < txtDisplay.MaxLength)
            {
                string _val = s_UnformattedText;

                if (_val.Length > 2)
                {
                    txtDisplay.Text = _val.Insert(_val.Length - 2, ExtensionMethods.GetCurrencyDecimalDelimiter());
                }
                else if (_val.Length == 2)
                {
                    txtDisplay.Text = "0" + ExtensionMethods.GetCurrencyDecimalDelimiter() + _val;
                }
                else if (_val.Length < 2)
                {
                    txtDisplay.Text = "0" + ExtensionMethods.GetCurrencyDecimalDelimiter() + "0" + _val;
                }
                s_ActualText = txtDisplay.Text;

            }
            else
            {
                txtDisplay.Text = s_ActualText;
                s_UnformattedText = s_ActualText.ToString().Replace(ExtensionMethods.GetCurrencyDecimalDelimiter(), "");
            }
            int i_Selection = txtDisplay.Text.Length;
            txtDisplay.Focus();
            txtDisplay.SelectionStart = i_Selection;
            txtDisplay.SelectionLength = 0;
        }
        //
        public int MaxLength
        {
            get 
            { 
                return _iMaxLength; 
            }
            set
            {
                _iMaxLength = value;
                textBox1.MaxLength = value;
                txtDisplay.MaxLength = value;

            }
        }

        public bool IsAcceptCancelVisible
        {
            get { return (bool)GetValue(IsAcceptCancelVisibleProperty); }
            set { SetValue(IsAcceptCancelVisibleProperty, value); }
        }
        
        public static readonly DependencyProperty IsAcceptCancelVisibleProperty =
            DependencyProperty.Register("IsAcceptCancelVisible", typeof(bool), typeof(CTicketEntry), new UIPropertyMetadata(false, new PropertyChangedCallback(IsAcceptCancelVisibleChangedCallback)));


        static void IsAcceptCancelVisibleChangedCallback(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                ((CTicketEntry)source).pnlButtons.Visibility = Visibility.Visible;
            }
            else
            {
                ((CTicketEntry)source).pnlButtons.Visibility = Visibility.Collapsed;
            }
        }

        void txtDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {

            RaiseValueChangeEvent(e);
            if (isCurrencyPad)
            {
                if (string.IsNullOrEmpty(txtDisplay.Text))
                    txtDisplay.Text = string.Format("0{0}00", ExtensionMethods.GetCurrencyDecimalDelimiter());
            }
        }

        private void RaiseValueChangeEvent(TextChangedEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged.Invoke(this, e as RoutedEventArgs);
            }
        }

        void NonCurrency(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text.Trim().Length < MaxLength)
            {
                txtDisplay.SelectedText = ((Button)sender).Content.ToString();
                int i_Selection = txtDisplay.SelectionStart;
                txtDisplay.Focus();
                txtDisplay.SelectionStart = i_Selection + 1;
                txtDisplay.SelectionLength = 0;
            }
        }
        public string ValueText
        {
            get
            {
                return this.txtDisplay.Text;
            }
            set 
            {
                txtDisplay.Text = value;
            }
        }

        private void BindButtonEvents()
        {
            foreach (UIElement elmt in pnlNumbers.Children)
            {
                if (elmt is Button)
                {
                    ((Button)elmt).Click += new RoutedEventHandler(Number_Click);
                }
            }
        }

        private void EnterClick(object sender, RoutedEventArgs e)
        {
            if (EnterClicked != null)
            {
                EnterClicked.Invoke(this, e);
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            if (CancelClicked != null)
            {
                CancelClicked.Invoke(this, e);
            }
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Clear();
            txtDisplay.Focus();
            if (isCurrencyPad)
            {
                s_UnformattedText = "";
                s_ActualText = "";
                txtDisplay.Text = string.Format("0{0}00", ExtensionMethods.GetCurrencyDecimalDelimiter());
            }
            if (ClickClearEvent != null)
                ClickClearEvent.Invoke(this);
            if (vClickClearEvent != null)
                vClickClearEvent.Invoke(this);
        }

        /// <summary>
        /// Keydown validation to validate input through keyboard
        /// </summary>
        private void txtDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            if (isCurrencyPad)
            {
                e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);
                if (isCurrencyPad)
                {
                    if (e.Key == Key.OemComma && sFormat == ",")
                        e.Handled = false;

                    if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal) && sFormat == ".")
                        e.Handled = false;
                }
            }
            if (isPlayerClub)
            {
                if (e.Key == Key.Enter)
                {                    
                    txtDisplay.Text = GetPlayerInfo(txtDisplay.Text);
                    if ( ClickEvent!= null)
                        ClickEvent.Invoke(sender, e as RoutedEventArgs);
                    
                    //Code by Melvi 21/03/2012
                    if (ClickEvent_ExceptionVoucher != null)
                        ClickEvent_ExceptionVoucher.Invoke(sender, e as RoutedEventArgs);
                }
            }
            else
                if (e.Key == Key.Enter)
                {
                    if (ClickEvent != null)
                        ClickEvent.Invoke(sender, e as RoutedEventArgs);
                    if(vClickEvent!=null)
                        vClickEvent.Invoke(sender, e as RoutedEventArgs);
                    if (mClickEvent != null)
                        mClickEvent.Invoke(sender, e as RoutedEventArgs);
                    if (hClickEvent != null)
                        hClickEvent.Invoke(sender, e as RoutedEventArgs);
                    //if (sClickEvent != null)
                    //    sClickEvent.Invoke(sender, e as RoutedEventArgs);

                    //Code by Melvi 21/03/2012
                    if (ClickEvent_ExceptionVoucher != null)
                        ClickEvent_ExceptionVoucher.Invoke(sender, e as RoutedEventArgs);

                }
                else
                {
                    e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);
                }
        }
        private string GetPlayerInfo(string CardNumber)
        {
            string[] returnValue;

            returnValue = playerInformationBusinessObject.GetCardInformation(CardNumber);

            Regex Rx = new Regex(returnValue[0], RegexOptions.IgnoreCase);
            Regex RxInner = new Regex(returnValue[1], RegexOptions.IgnoreCase);
            
            if (Rx.Matches(CardNumber).Count >= 1)
            {
                if (Rx.Matches(CardNumber)[0].Value.Replace(";", "").Replace("?", "").Length > int.Parse(returnValue[2]))
                    return RxInner.Matches(Rx.Matches(CardNumber)[0].Value)[1].Value.Remove(0, int.Parse(returnValue[2]));
                else
                    return CardNumber;
            }
            else
                return CardNumber;

        }

        private List<Key> AllowedKeys = new List<Key> 
        {
            //Numbers 0-9
            Key.D0, 
            Key.D1,
            Key.D2,
            Key.D3,
            Key.D4,
            Key.D5,
            Key.D6,
            Key.D7,
            Key.D8,
            Key.D9,

            //Keypad 0-9
            Key.NumPad0,
            Key.NumPad1,
            Key.NumPad2,
            Key.NumPad3,
            Key.NumPad4,
            Key.NumPad5,
            Key.NumPad6,
            Key.NumPad7,
            Key.NumPad8,
            Key.NumPad9,

            //Backspace,Decimal,enter and delete keys
            Key.Enter,
            Key.Back,
            Key.Delete,
            Key.Decimal
        };

        private void btnBckspace_Click(object sender, RoutedEventArgs e)
        {            
            int i_Selection = txtDisplay.SelectionStart - 1;

            if (i_Selection >= 0)
            {
                txtDisplay.Text = txtDisplay.Text.Remove(i_Selection, 1);
                if (isCurrencyPad)
                s_UnformattedText = txtDisplay.Text.ToString().Replace(ExtensionMethods.GetCurrencyDecimalDelimiter(), "") ;
                txtDisplay.SelectionStart = i_Selection;
                txtDisplay.SelectionLength = 0;
                txtDisplay.Focus();
            }
            else
            {
                txtDisplay.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDisplay.Focus();
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
                        this.Window.Loaded -= (this.Window_Loaded);
                        this.txtDisplay.KeyDown -= (this.txtDisplay_KeyDown);
                        this.btnBckspace.Click -= (this.btnBckspace_Click);
                        this.btnClear.Click -= (this.Clear_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CTicketEntry objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CTicketEntry"/> is reclaimed by garbage collection.
        /// </summary>
        ~CTicketEntry()
        {
            Dispose(false);
        }

        #endregion
	}
}