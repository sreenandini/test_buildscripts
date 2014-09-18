using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Globalization;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.ExceptionManagement;
using BMC.Transport;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for ValueCalcComp.xaml
    /// </summary>
    public partial class ValueCalcComp : IDisposable
    {
        /// <summary>
        /// Event raised when the value in the textbox changes
        /// </summary>
        public event RoutedEventHandler ValueChanged;
        private string s_ActualText = string.Empty;
        public string s_UnformattedText = string.Empty;
        private string sFormat;
        string strPreviousString = string.Empty;
       
        public event ValueCalcCompEnterClickedHandler EnterClicked = null;
        public static readonly DependencyProperty AllowFocusProperty = DependencyProperty.Register("NoDefaultFocus", typeof(bool), typeof(ValueCalcComp), null);
        public bool NoDefaultFocus
        {
            get { return (bool)GetValue(AllowFocusProperty); }
            set { SetValue(AllowFocusProperty, value); }
        }
        public ValueCalcComp()
        {
            this.InitializeComponent();
            BindButtonEvents();
            // txtDisplay.TextChanged += new TextChangedEventHandler(txtDisplay_TextChanged);
            // txtDisplay.AddHandler(TextBox.PreviewKeyDownEvent, new KeyEventHandler(txtDisplay_KeyDown), true);
            if (!Settings.AllowManualKeyboard)
            {
                this.txtDisplay.IsReadOnly = true;
            }
            txtDisplay.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, ShortcutDisable));
            //txtDisplay.Text = string.Format("0{0}00", ExtensionMethods.GetCurrencyDecimalDelimiter());
            txtDisplay.MaxLength = 9;
            txtDisplay.Focus();
            txtDisplay.SelectionLength = txtDisplay.Text.Length;
           
            var d = Convert.ToDecimal(1.1);
            decimal.TryParse(d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)), NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture), out d);
            sFormat = d.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture)).Substring(1, 1);
            txtDisplay.Focus();

        }
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(ValueCalcComp), new UIPropertyMetadata(8));

        void txtDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDisplay.Text))
            {
                s_UnformattedText = string.Empty;
                s_ActualText = string.Empty;
                txtDisplay.Focus();
            }
            else
            {
                s_UnformattedText = txtDisplay.Text.Replace(ExtensionMethods.GetCurrencyDecimalDelimiter(), "");
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

      

        private void UnbindButtonEvents()
        {
            foreach (UIElement elmt in pnlNumbers.Children)
            {
                if (elmt is Button)
                {
                    ((Button)elmt).Click -= new RoutedEventHandler(Number_Click);
                }
            }
        }


        private void ShortcutDisable(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        void Number_Click(object sender, RoutedEventArgs e)
        {
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


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //txtDisplay.Text = "0" + ExtensionMethods.GetCurrencyDecimalDelimiter() + "00";
            //txtDisplay.Text = string.Format("0{0}00", ExtensionMethods.GetCurrencyDecimalDelimiter());
            //txtDisplay.SelectionLength = txtDisplay.Text.Length;
            txtDisplay.Text = string.Empty;
            s_UnformattedText = string.Empty; ;
            s_ActualText = string.Empty; ;
            txtDisplay.Focus();
        }

        private void txtDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt_value = sender as TextBox;
            e.Handled = !CustomerDetailsConstants.AllowedNumerics.Contains(e.Key);

            if (e.Key == Key.Enter)
            {
                if (EnterClicked != null)
                {
                    EnterClicked(null, null);
                    e.Handled = true;
                    return;
                }
            }
            if (e.Key == Key.Tab)
            {
                return;
            }            
            if (e.Key == Key.OemComma && sFormat == ",")
                e.Handled = false;

            if ((e.Key == Key.OemPeriod || e.Key == Key.Decimal) && sFormat == ".")
                e.Handled = true;


        }

        private List<Key> AllowedKeys = new List<Key> 
        {
            Key.D0, 
            Key.D1,
            Key.D2,
            Key.D3,
            Key.D4,
            Key.D6,
            Key.D7,
            Key.D8,
            Key.D9,

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

            Key.Enter,
            Key.Back,
            Key.Delete,
        };


        public void ClearValues()
        {
            txtDisplay.Clear();
            txtDisplay.Text = string.Empty;
            txtDisplay.SelectionLength = txtDisplay.Text.Length;
            txtDisplay.Focus();
        }

        public void ClearAll()
        {
            txtDisplay.Clear();
            txtDisplay.Text = string.Empty;
            txtDisplay.SelectionLength = txtDisplay.Text.Length;
            s_UnformattedText = "";
            s_ActualText = "";
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
                        this.txtDisplay.KeyDown -= (this.txtDisplay_KeyDown);
                        this.btnClear.Click -= (this.Clear_Click);
                        txtDisplay.TextChanged -= (txtDisplay_TextChanged);
                        txtDisplay.RemoveHandler(TextBox.PreviewKeyDownEvent, new KeyEventHandler(txtDisplay_KeyDown));
                        this.UnbindButtonEvents();
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("ValueCalcComp objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ValueCalcComp"/> is reclaimed by garbage collection.
        /// </summary>
        ~ValueCalcComp()
        {
            Dispose(false);
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!NoDefaultFocus)
            {
                this.txtDisplay.Focus();
            }
        }

        private void txtDisplay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (!Settings.AllowManualKeyboard)
            {
                e.Handled = true;
                return;
            }
            s_UnformattedText = txtDisplay.Text.TrimStart('0').Replace(ExtensionMethods.GetCurrencyDecimalDelimiter(), "");

            if (s_UnformattedText.Length < txtDisplay.MaxLength && char.IsNumber(e.Text,0))
                s_UnformattedText += e.Text;

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
            e.Handled = true;
        }

      

        private void txtDisplay_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right ||e.Key==Key.Space)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Delete)
            {
                ClearAll();
                e.Handled = true;
            }
            
        }

        private void txtDisplay_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            int i_Selection = txtDisplay.Text.Length;
            txtDisplay.Focus();
            txtDisplay.SelectionStart = i_Selection;
            txtDisplay.SelectionLength = 0;
            e.Handled = true;
        }


    }

    public delegate void ValueCalcCompEnterClickedHandler(object sender, RoutedEventArgs e);
}