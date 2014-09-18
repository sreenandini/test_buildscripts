#region File Description
//-------------------------------------------------------------------------------------
// File: CKeyboard.Xaml.cs
//
// Namespace: BMC.Presentation
//
// Description: Keyboard user control interaction logic
//
// Copyright: Bally Technologies Inc. 2008
//
// Revision History:
//------------------------------------------------------------------------------------
// Date				    Engineer			        Description
// -----------------------------------------------------------------------------------
// 22/10/2008          Balasubramanyam.G        UI Keyboard interaction logic
//------------------------------------------------------------------------------------
#endregion

#region Namespaces
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using BMC.Presentation.POS.Helper_classes;
#endregion

namespace BMC.Presentation
{
    public partial class CKeyboard : IDisposable
    {
        #region Properties, variables and events

        private string _sCurrentVal = string.Empty;

        private int _iCurrentCursorPosition = 0;

        /// <summary>
        /// Event raised when enter is clicked
        /// </summary>
        public event EventHandler EnterClicked;

        /// <summary>
        /// Event raised when Cancel is clicked
        /// </summary>
        public event EventHandler CancelClicked;

        /// <summary>
        /// Event raised when user attempts to move the keyboard
        /// </summary>
        public event EventHandler MoveKeyboard;

        /// <summary>
        /// Gets the current value of the On screen keyboard
        /// </summary>
        public string CurrentValue
        {
            get
            {
                return _sCurrentVal;
            }
            set
            {
                _sCurrentVal = value;
                if (IsPasswordMode)
                {
                    txtDisplay.Text = "";
                    for (int i = 1; i <= _sCurrentVal.Length; i++)
                    {
                        txtDisplay.Text += Passwordchar;
                    }
                }
                else
                {
                    txtDisplay.Text = _sCurrentVal;
                    txtDisplay.SelectionStart = txtDisplay.Text.Length + 1;
                    txtDisplay.Focus();
                }

            }
        }



        public char Passwordchar
        {
            get { return (char)GetValue(PasswordcharProperty); }
            set { SetValue(PasswordcharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Passwordchar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordcharProperty =
            DependencyProperty.Register("Passwordchar", typeof(char), typeof(CKeyboard), new UIPropertyMetadata('•'));



        public bool IsPasswordMode
        {
            get { return (bool)GetValue(IsPasswordModeProperty); }
            set { SetValue(IsPasswordModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPasswordMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPasswordModeProperty =
            DependencyProperty.Register("IsPasswordMode", typeof(bool), typeof(CKeyboard), new UIPropertyMetadata(false, new PropertyChangedCallback(IsPasswordModeChanged)));



        #endregion

        #region Constructor

        public CKeyboard()
        {
            this.InitializeComponent();
            this.BindEvents();
        }

        #endregion

        #region Event handlers

        void CKeyboard_Click(object sender, RoutedEventArgs e)
        {
            AppendText(((Button)sender).Content.ToString());
        }

        void CKeyboardSymbols_Click(object sender, RoutedEventArgs e)
        {
            AppendText(((Button)sender).Content.ToString());
            pnlSymbols.Visibility = Visibility.Hidden;
            pnlSpl.Visibility = Visibility.Hidden;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ChangeCase(chkCaps.IsChecked.Value);
        }

        private void chkCaps_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeCase(chkCaps.IsChecked.Value);

        }

        private void chkShift_Checked(object sender, RoutedEventArgs e)
        {
            ChangeCase(!chkCaps.IsChecked.Value);
        }

        private void chkShift_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeCase(chkCaps.IsChecked.Value);
        }



        private void BackSpace_Click(object sender, RoutedEventArgs e)
        {
            BackSpacePressed();
        }


        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (EnterClicked != null)
            {
                EnterClicked.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (CancelClicked != null)
            {
                CancelClicked.Invoke(this, EventArgs.Empty);
            }
        }



        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MoveKeyboard != null)
            {
                MoveKeyboard.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Private methods


        /// <summary>
        /// Method raised when backspace button is pressed
        /// </summary>
        private void BackSpacePressed()
        {
            int i_Selection = txtDisplay.SelectionStart;
            int i_SelectionLength = txtDisplay.SelectionLength;
            if (i_Selection > 0 && i_SelectionLength == 0)
            {
                txtDisplay.Text = txtDisplay.Text.Remove(i_Selection - 1, 1);
                _sCurrentVal = _sCurrentVal.Remove(i_Selection - 1, 1);
                txtDisplay.SelectionStart = i_Selection - 1;
            }
            else
            {
                txtDisplay.Text = txtDisplay.Text.Remove(i_Selection, i_SelectionLength);
                _sCurrentVal = _sCurrentVal.Remove(i_Selection, i_SelectionLength);
                txtDisplay.SelectionStart = i_Selection;
            }
            txtDisplay.Focus();

            txtDisplay.SelectionLength = 0;
        }



        /// <summary>
        /// Method to change case of keyboard
        /// </summary>
        /// <param name="IsCaps">Boolean value to indicate Caps status</param>
        private void ChangeCase(bool IsCaps)
        {
            if (IsCaps)
            {
                foreach (UIElement elmt in pnlAlphabets.Children)
                {
                    if (elmt is Button)
                    {
                        ((Button)elmt).Content = ((Button)elmt).Content.ToString().ToUpper();
                    }

                }
            }
            else
            {
                foreach (UIElement elmt in pnlAlphabets.Children)
                {
                    if (elmt is Button)
                    {
                        ((Button)elmt).Content = ((Button)elmt).Content.ToString().ToLower();
                    }
                }

            }
        }



        /// <summary>
        /// Method to append text to current keyboard selection
        /// </summary>
        /// <param name="ValueToBeAdded">The value to be appended</param>
        void AppendText(string ValueToBeAdded)
        {
            _iCurrentCursorPosition = txtDisplay.SelectionStart;
           // CurrentValue = txtDisplay.Text;
            string s_Temp = CurrentValue;
            if (txtDisplay.SelectionLength > 0)
            {
                s_Temp = s_Temp.Remove(txtDisplay.SelectionStart, txtDisplay.SelectionLength);
            }
            s_Temp = s_Temp.Insert(txtDisplay.SelectionStart, ValueToBeAdded);
            CurrentValue = s_Temp;

            txtDisplay.SelectionStart = _iCurrentCursorPosition + 1;

            //Toggle Shift status to false if it is true
            if (chkShift.IsChecked.Value)
            {
                chkShift.IsChecked = false;
            }
            txtDisplay.Focus();
        }



        /// <summary>
        /// Dynamically bind events to all the alphabet and number buttons in the keyboard
        /// </summary>
        private void BindEvents()
        {
            foreach (UIElement elmt in pnlAlphabets.Children)
                if (elmt is Button)
                    ((Button) elmt).Click += CKeyboard_Click;

            foreach (UIElement elmt in pnlNumbers.Children)
                if (elmt is Button)
                    ((Button) elmt).Click += CKeyboard_Click;
            foreach (UIElement elmt in pnlSymbols.Children)
                if (elmt is Button)
                    ((Button)elmt).Click += CKeyboardSymbols_Click;
        }


        static void IsPasswordModeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (!((bool) e.NewValue)) return;

            ((CKeyboard)source).txtDisplay.FontFamily = new FontFamily("Times New Roman");
            ((CKeyboard)source).txtDisplay.FontSize = 36;
        }

        #endregion
     
        private void btnSC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnSC.IsEnabled = false;
                if (pnlSymbols.Visibility == Visibility.Visible && pnlSpl.Visibility == Visibility.Visible)
                {
                    pnlSymbols.Visibility = Visibility.Hidden;
                    pnlSpl.Visibility = Visibility.Hidden;
                }
                else
                {
                    pnlSymbols.Visibility = Visibility.Visible;
                    pnlSpl.Visibility = Visibility.Visible;
                }
            }
            finally
            {
                btnSC.IsEnabled = true;
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
                        this.chkCaps.Checked -= (this.CheckBox_Checked);
                        this.chkCaps.Unchecked -= (this.chkCaps_Unchecked);
                        this.chkShift.Checked -= (this.chkShift_Checked);
                        this.chkShift.Unchecked -= (this.chkShift_Unchecked);
                        this.btnEnter.Click -= (this.Enter_Click);
                        this.btnSC.Click -= (this.btnSC_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CKeyboard objects are released successfully.");
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CKeyboard"/> is reclaimed by garbage collection.
        /// </summary>
        ~CKeyboard()
        {
            Dispose(false);
        }

        #endregion

    }
}