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
using BMC.CashDispenser.Core;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.Transport;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CCoinDispenser.xaml
    /// </summary>
    public partial class CCoinDispenser : UserControl
    {
        private ICashDispenser _dispenser = null;
        private MainScreen _mainScreen = null;

        public CCoinDispenser(MainScreen mainScreen)
        {
            InitializeComponent();
            _mainScreen = mainScreen;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadItems();
        }

        private void LoadItems()
        {
            try
            {
                _dispenser = CashDispenserFactory.GetDispenser();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (_dispenser != null)
                {
                  //Gets the denom and total value for UPPER Cassette Alias
                  IList<CashDispenserItem> dispenserUpperItems = _dispenser.DispenserItems;//.Where(Item => Item.DeckType.ToString().ToUpper() == "UPPER")
                        //.ToList();
                  foreach (CashDispenserItem item in dispenserUpperItems)
                  {
                      if (item.DeckType == CashDispenserItem.TypeOfDeck.Upper)
                      {
                          txtCassetteAlias.Text = item.CassetteAlias;
                          txtDenom.Text = item.Denimination.ToString().GetDecimal().ToString();
                          txtTotalValue.Text = "0";
                          txtRemaining1.Text = item.TotalValue.ToString().GetDecimal().ToString();
                          lblDeck1.Text = item.DeckType.ToString();
                          txtCassetteAlias.Tag = item;
                      }
                      else
                      {
                          txtCassetteAlias2.Text = item.CassetteAlias;
                          txtDenom2.Text = item.Denimination.ToString().GetSingleFromString().ToString();
                          txtTotal2.Text = "0";
                          txtRemaining2.Text = item.TotalValue.ToString().GetDecimal().ToString();
                          lblDeck2.Text = item.DeckType.ToString();
                          txtCassetteAlias2.Tag = item;
                      }
                  }                    
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int cd1Value = 0, cd2Value = 0;
            Int32.TryParse(txtRemaining1.Text, out cd1Value);
            Int32.TryParse(txtRemaining1.Text, out cd2Value);

            if (cd1Value > 0 || cd2Value > 0)
            {
                if (MessageBox.ShowBox("MessageID391", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                    return;
            }

            bool overallResult = true;

            try
            {

                bool result = false;

                CashDispenserItem item = txtCassetteAlias.Tag as CashDispenserItem;

                //Upper Cassette
                item.CassetteAlias = txtCassetteAlias.Text;
                item.Denimination = Convert.ToInt32(txtDenom.Text);
                item.TotalValue = txtTotalValue.Text.GetDecimal();
                result = CashDispenserFactory.UpdateItem(item);
              
                //Lower Cassette
                item = txtCassetteAlias2.Tag as CashDispenserItem;
                item.CassetteAlias = txtCassetteAlias2.Text;
                item.Denimination = Convert.ToInt32(txtDenom2.Text);
                item.TotalValue = txtTotal2.Text.GetDecimal();
              
                result = CashDispenserFactory.UpdateItem(item);

                overallResult = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (overallResult)
                {
                    MessageBox.ShowBox("MessageID389", BMC_Icon.Information);
                }
                else
                {
                    MessageBox.ShowBox("MessageID390", BMC_Icon.Information);
                }

                this.LoadItems();
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
                ObjNumberpadWind.Owner = _mainScreen;

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

        private void ShowNumericKeyboard(TextBox textBox)
        {
            if (!Settings.OnScreenKeyboard)
            {
                return;
            }
            textBox.Text = DisplayNumberPad(textBox.Text.Trim());
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            _mainScreen.LoadFlooView();  
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
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.btnSave.Click -= (this.btnSave_Click);
                        this.btnClose.Click -= (this.btnClose_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CCoindispenser objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CAFTSetting"/> is reclaimed by garbage collection.
        /// </summary>
        ~CCoinDispenser()
        {
            Dispose(false);
        }

        #endregion

        private void txtCassetteAlias_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void txtTotalValue_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ShowNumericKeyboard(sender as TextBox);
        }

        private void txtCassetteAlias2_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtDenom2_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void txtTotal2_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ShowNumericKeyboard(sender as TextBox);
        }

        private void txtDenom_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void txtRemaining1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtRemaining2_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
