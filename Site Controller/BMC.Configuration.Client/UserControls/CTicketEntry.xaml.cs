using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BMC.ExchangeConfig
{
	/// <summary>
	/// Interaction logic for ValueCalcComp.xaml
	/// </summary>
	public partial class CTicketEntry
	{
        /// <summary>
        /// Event raised when the value in the textbox changes
        /// </summary>
        public event RoutedEventHandler ValueChanged;
        //public event BMC.Presentation.CRedeemTicket.ScannerClick ClickEvent;
        //public event BMC.Presentation.CRedeemTicket.ClickClear ClickClearEvent;
        //IPlayerInformation playerInformationBusinessObject = PlayerInformationBusinessObject.CreateInstance();
        
        private int _iMaxLength;

        public bool isPlayerClub { get; set; }


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
		}

        public int MaxLength
        {
            get 
            { 
                return _iMaxLength; 
            }
            set
            {
                _iMaxLength = value;
                txtDisplay.MaxLength = _iMaxLength;
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
        }

        private void RaiseValueChangeEvent(TextChangedEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged.Invoke(this, e as RoutedEventArgs);
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

        void Number_Click(object sender, RoutedEventArgs e)
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
            //if (ClickClearEvent != null)
                //ClickClearEvent.Invoke(this);
        }

        /// <summary>
        /// Keydown validation to validate input through keyboard
        /// </summary>
        private void txtDisplay_KeyDown(object sender, KeyEventArgs e)
        {

            if (isPlayerClub)
            {
                if (e.Key == Key.Enter)
                {                    
                    txtDisplay.Text = GetPlayerInfo(txtDisplay.Text);
                    //if ( ClickEvent!= null)
                        //ClickEvent.Invoke(sender, e as RoutedEventArgs);
                }
            }
            else
                if (e.Key == Key.Enter)
                {
                    //if (ClickEvent != null)
                        //ClickEvent.Invoke(sender, e as RoutedEventArgs);                    
                }
                else
                {
                    e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);
                }
        }
        private string GetPlayerInfo(string CardNumber)
        {
            //string[] returnValue;

            //returnValue = playerInformationBusinessObject.GetCardInformation(CardNumber);

            //Regex Rx = new Regex(returnValue[0], RegexOptions.IgnoreCase);
            //Regex RxInner = new Regex(returnValue[1], RegexOptions.IgnoreCase);
            
            //if (Rx.Matches(CardNumber).Count >= 1)
            //{
            //    if (Rx.Matches(CardNumber)[0].Value.Replace(";", "").Replace("?", "").Length > int.Parse(returnValue[2]))
            //        return RxInner.Matches(Rx.Matches(CardNumber)[0].Value)[1].Value.Remove(0, int.Parse(returnValue[2]));
            //    else
            //        return CardNumber;
            //}
            //else
            //    return CardNumber;

            return string.Empty;

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
	}
}