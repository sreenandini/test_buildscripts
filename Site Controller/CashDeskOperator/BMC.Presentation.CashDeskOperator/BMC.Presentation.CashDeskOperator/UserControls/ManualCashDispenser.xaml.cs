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
using BMC.CashDispenser.Core;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using Audit.BusinessClasses;
using Audit.Transport;

namespace BMC.Presentation.POS.UserControls
{
    /// <summary>
    /// Interaction logic for ManualCashDispenser.xaml
    /// </summary>
    public partial class ManualCashDispenser : Window, IDisposable
    {
        private CashDispenserWorker _worker = null;
        private int _cd1Value = 0;
        private int _cd2Value = 0;

        private decimal _upperDenomination = 0;
        private decimal _lowerDenomination = 0;
        private ModuleName _parentModuleName = ModuleName.Cash_Dispenser;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualCashDispenser"/> class.
        /// </summary>
        /// <param name="worker">The worker.</param>
        public ManualCashDispenser(CashDispenserWorker worker)
        {
            _parentModuleName = worker.ModuleName;
            InitializeComponent();
            _worker = worker;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            _worker.LoadDecks();
            if (_cd1Value == 0 && _cd2Value == 0)
            {
                MessageBox.ShowBox("MessageID388", BMC_Icon.Information, BMC_Button.OK);
                txtCD1.Focus();
                return;
            }
            if (_cd1Value != 0 &&
                _cd1Value > _worker.UpperDeck.TotalValue)
            {
                MessageBox.ShowBox("MessageID392", BMC_Icon.Information, BMC_Button.OK);
                txtCD1.Focus();
                return;
            }
            if (_cd2Value != 0 &&
                _cd2Value > _worker.LowerDeck.TotalValue)
            {
                MessageBox.ShowBox("MessageID392", BMC_Icon.Information, BMC_Button.OK);
                txtCD2.Focus();
                return;
            }

            try
            {
                string message = string.Empty;
                _worker.Dispense(_cd1Value, _cd2Value, out message);
                MessageBox.ShowBox(message, true, 280);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (_cd1Value > 0)
                    this.WriteAuditLog(_worker.UpperDeck, _cd1Value);
                if (_cd2Value > 0)
                    this.WriteAuditLog(_worker.LowerDeck, _cd2Value);
            }
            this.Close();
        }

        private void WriteAuditLog(CashDispenserItem deck, decimal value)
        {
            try
            {
                string auditValue = deck.CassetteAlias + " : " + deck.Denimination + ", " + value.ToString();
                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    AuditModuleName = ModuleName.Cash_Dispenser,
                    Audit_Screen_Name = "Manual Cash Dispense",
                    Audit_Desc = "From Module : " + _parentModuleName.ToString(),
                    AuditOperationType = OperationType.ADD,
                    Audit_Field = "Amount",
                    Audit_New_Vl = auditValue,
                    Audit_Slot = string.Empty
                });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadItems();
        }

        /// <summary>
        /// Loads the items.
        /// </summary>
        private void LoadItems()
        {
            try
            {
                using (ICashDispenser dispenser = CashDispenserFactory.GetDispenser())
                {
                    string currencySymbol = ExtensionMethods.GetCurrencySymbol(string.Empty);

                    for (int i = 0; i < dispenser.DispenserItems.Count; i++)
                    {
                        CashDispenserItem item = dispenser.DispenserItems[i];
                        string sText = item.CassetteAlias + " : " + currencySymbol + " " + item.Denimination.ToString();

                        if (item.DeckType == CashDispenserItem.TypeOfDeck.Upper)
                        {
                            _upperDenomination = item.Denimination;
                            lblCD1.Text = sText;
                            string balance = item.TotalValue.ToString();
                            if (string.IsNullOrEmpty(balance)) balance = "0";
                            lblBalance1.Text = balance;
                            txtCD1.Text = "";
                            txtCD1.Tag = item;
                            txtCD1_TextChanged(txtCD1, null);
                        }
                        else
                        {
                            _lowerDenomination = item.Denimination;
                            lblCD2.Text = sText;
                            string balance = item.TotalValue.ToString();
                            if (string.IsNullOrEmpty(balance)) balance = "0";
                            lblBalance2.Text = balance;
                            txtCD2.Text = "";
                            txtCD2.Tag = item;
                            txtCD2_TextChanged(txtCD2, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Displays the number pad.
        /// </summary>
        /// <param name="keytext">The keytext.</param>
        /// <returns>Selected Text.</returns>
        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind();

            try
            {

                ObjNumberpadWind.ValueText = keytext;
                ObjNumberpadWind.Owner = this;

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

        private void txtCD1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int32.TryParse(((TextBox)sender).Text, out _cd1Value);
            lblCD1Calc.Text = ExtensionMethods.GetUniversalCurrencyFormatWithSymbol(_cd1Value * _upperDenomination);
            this.CalcTotal();
        }

        private void txtCD2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int32.TryParse(((TextBox)sender).Text, out _cd2Value);
            lblCD2Calc.Text = ExtensionMethods.GetUniversalCurrencyFormatWithSymbol(_cd2Value * _lowerDenomination);
            this.CalcTotal();
        }

        /// <summary>
        /// Calcs the total.
        /// </summary>
        private void CalcTotal()
        {
            decimal total = (_cd1Value * _upperDenomination) + (_cd2Value * _lowerDenomination);
            lblTotalCalc.Text = ExtensionMethods.GetUniversalCurrencyFormatWithSymbol(total);
        }

        #region IDisposable Members

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
                        this.Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
                        this.txtCD1.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtCD1_TextChanged);
                        this.txtCD2.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtCD2_TextChanged);
                        this.btnProcess.Click += new System.Windows.RoutedEventHandler(this.btnProcess_Click);
                        this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);

                        // others

                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> ManualCashDispenser objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        ~ManualCashDispenser()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// Handles the PreviewMouseUp event of the txtCD1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void txtCD1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ShowKeyboardAndGetText(sender as TextBox);
        }

        /// <summary>
        /// Handles the PreviewMouseUp event of the txtCD2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void txtCD2_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ShowKeyboardAndGetText(sender as TextBox);
        }

        /// <summary>
        /// Shows the keyboard and get text.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        private void ShowKeyboardAndGetText(TextBox textBox)
        {
            int maxLength = textBox.MaxLength;
            if (!Settings.OnScreenKeyboard)
                return;
            string text = DisplayNumberPad(textBox.Text.Trim());
            int value = 0;
            Int32.TryParse(text, out value);
            string text2 = value.ToString();
            if (text2.Length > maxLength) text2 = text2.Substring(0, maxLength);
            textBox.Text = text2;
        }
    }
}
