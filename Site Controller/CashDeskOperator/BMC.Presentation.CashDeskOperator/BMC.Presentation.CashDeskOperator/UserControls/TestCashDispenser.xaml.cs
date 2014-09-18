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
using BMC.Presentation.POS.Helper_classes;
using BMC.CashDispenser.Core;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using Audit.Transport;

namespace BMC.Presentation.POS.UserControls
{
    /// <summary>
    /// Interaction logic for TestCashDispenser.xaml
    /// </summary>
    public partial class TestCashDispenser : Window, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCashDispenser"/> class.
        /// </summary>
        public TestCashDispenser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadItems(true);
        }

        /// <summary>
        /// Handles the Click event of the btnTestDispense control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void btnTestDispense_Click(object sender, RoutedEventArgs e)
        {
            this.LoadItems(false);
        }

        /// <summary>
        /// Loads the items.
        /// </summary>
        private void LoadItems(bool isInit)
        {
            Brush redBrush = Brushes.Red;
            Brush greenBrush = Brushes.Green;

            try
            {

                using (ICashDispenser dispenser = CashDispenserFactory.GetDispenser())
                {
                    string currencySymbol = ExtensionMethods.GetCurrencySymbol(string.Empty);

                    for (int i = 0; i < dispenser.DispenserItems.Count; i++)
                    {
                        CashDispenserItem item = dispenser.DispenserItems[i];
                        string sText = item.CassetteAlias + " : ";
                        string sText2 = currencySymbol + " " + item.Denimination.ToString();

                        string message = string.Empty;
                        bool isOK = isInit ? false : (item.TestStatus(out message) == CashDispenserItem.DispenserStatus.Available);
                        lblErrorDesc.Text = (isOK ? string.Empty : message);

                        Brush brush = isOK ? greenBrush : redBrush;
                        string statusText = string.Empty;

                        if (item.DeckType == CashDispenserItem.TypeOfDeck.Upper)
                        {
                            lblCD1.Text = sText;
                            lblCD1Calc.Text = sText2;
                            rcCD1Status.Content = statusText;
                            rcCD1Status.Background = brush;
                        }
                        else
                        {
                            lblCD2.Text = sText;
                            lblCD2Calc.Text = sText2;
                            rcCD2Status.Content = statusText;
                            rcCD2Status.Background = brush;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
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

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="TestCashDispenser"/> is reclaimed by garbage collection.
        /// </summary>
        ~TestCashDispenser()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
