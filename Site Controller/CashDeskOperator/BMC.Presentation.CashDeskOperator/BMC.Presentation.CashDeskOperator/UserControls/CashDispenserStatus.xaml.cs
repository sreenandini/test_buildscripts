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
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using Audit.Transport;
using System.Threading;

namespace BMC.Presentation.POS.UserControls
{
    /// <summary>
    /// Interaction logic for CashDispenserStatus.xaml
    /// </summary>
    public partial class CashDispenserStatus : UserControl, IDisposable
    {
        private Thread _thLoadItems = null;

        private DispenserText _upperText = null;
        private DispenserText _lowerText = null;
        private string _errorText = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashDispenserStatus"/> class.
        /// </summary>
        public CashDispenserStatus()
        {
            InitializeComponent();
            btnTestDispense.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Gets or sets the status parent.
        /// </summary>
        /// <value>The status parent.</value>
        public ICashDispenserStatusParent StatusParent { get; set; }

        /// <summary>
        /// Handles the Click event of the btnTestDispense control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void btnTestDispense_Click(object sender, RoutedEventArgs e)
        {
            using (TestCashDispenser cashDispenser = new TestCashDispenser())
            {
                cashDispenser.ShowDialogEx(this);
                this.LoadItems();
            }
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CashDispenserWorker.IsVisible)
                {
                    if (this.StatusParent == null)
                    {
                        _lowerText = new DispenserText();
                        _upperText = new DispenserText();
                        _errorText = "Loading...";
                    }
                    else
                    {
                        _lowerText = this.StatusParent.LowerDeckText;
                        _upperText = this.StatusParent.UpperDeckText;
                        _errorText = this.StatusParent.StatusText;
                    }

                    if (this.StatusParent == null ||
                        !this.StatusParent.ParentLoaded)
                    {
                        LogManager.WriteLog(":::=> (CashDispenserStatus) : UserControl_Loaded.", LogManager.enumLogLevel.Info);
                        this.LoadItemsAysnc();
                    }

                    if (string.Compare(_upperText.Text, "...", true) == 0 ||
                        string.Compare(_lowerText.Text, "...", true) == 0)
                    {
                        this.LoadDBItems();
                    }

                    // Update the texts
                    this.UpdateTexts();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UpdateTexts()
        {
            try
            {
                // Update the text
                lblCD1.Text = _upperText.Text;
                lblCD2.Text = _lowerText.Text;

                lblCD1Calc.Text = _upperText.Text2;
                lblCD2Calc.Text = _lowerText.Text2;

                rcCD1Status.Content = _upperText.GetFormattedStatusText();
                rcCD2Status.Content = _lowerText.GetFormattedStatusText();

                rcCD1Status.Background = _upperText.BackBrush;
                rcCD2Status.Background = _lowerText.BackBrush;

                lblErrorDesc.Text = _errorText;
                this.StatusParent.StatusText = _errorText;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Loads the items.
        /// </summary>
        public void LoadDBItems()
        {
            if (!CashDispenserWorker.IsVisible) return;
            LogManager.WriteLog("|=> (LoadDBItems) : Invoked.", LogManager.enumLogLevel.Info);

            try
            {
                using (ICashDispenser dispenser = CashDispenserFactory.GetDispenser())
                {
                    string currencySymbol = ExtensionMethods.GetCurrencySymbol(string.Empty);

                    // before calling GetStatus
                    for (int i = 0; i < dispenser.DispenserItems.Count; i++)
                    {
                        CashDispenserItem item = dispenser.DispenserItems[i];
                        string sText = item.CassetteAlias + " : ";
                        string sText2 = currencySymbol + " " + item.Denimination.ToString();
                        string statusText = item.RemainingValueCalculated.ToString();

                        if (item.DeckType == CashDispenserItem.TypeOfDeck.Upper)
                        {
                            _upperText.Text = sText;
                            _upperText.Text2 = sText2;
                            _upperText.StatusText = statusText;
                        }
                        else
                        {
                            _lowerText.Text = sText;
                            _lowerText.Text2 = sText2;
                            _lowerText.StatusText = statusText;
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
        /// Loads the items aysnc.
        /// </summary>
        public void LoadItemsAysnc()
        {
            if (!CashDispenserWorker.IsVisible) return;

            try
            {
                this.KillLoadItemsThread();
                try { LoadDBItems(); }
                catch { }
                _thLoadItems = new Thread(new ThreadStart(this.LoadItems));
                _thLoadItems.Start();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Loads the items.
        /// </summary>
        private void LoadItems()
        {
            LogManager.WriteLog("|=> (LoadItems) : Invoked.", LogManager.enumLogLevel.Info);
            string message = string.Empty;
            uint iErrorCode;
            try
            {
                using (ICashDispenser dispenser = CashDispenserFactory.GetDispenser())
                {
                    bool isOK = false;
                    try
                    {
                        // GetStatus Calling
                        isOK = (CashDispenserItem.GetStatus(out message, out iErrorCode) == CashDispenserItem.DispenserStatus.Available);
                    }
                    catch (Exception ex)
                    {
                        _errorText = "Unable to connect cash dispenser.";
                        iErrorCode = 1;
                        ExceptionManager.Publish(ex);
                    }

                    // after calling GetStatus
                    _errorText = (isOK ? string.Empty : message);
                    for (int i = 0; i < dispenser.DispenserItems.Count; i++)
                    {
                        CashDispenserItem item = dispenser.DispenserItems[i];

                        if (item.DeckType == CashDispenserItem.TypeOfDeck.Upper)
                        {
                            _upperText.IsOK = Convert.ToInt32(_upperText.StatusText) == 0 ? false: !CashDispenserItem.IsUpperTrayError(iErrorCode);
                        }
                        else
                        {
                            _lowerText.IsOK = Convert.ToInt32(_lowerText.StatusText) == 0 ? false : !CashDispenserItem.IsLowerTrayError(iErrorCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _errorText = "Unable to connect cash dispenser.";
                ExceptionManager.Publish(ex);
            }
            finally
            {
                this.Dispatcher.Invoke(new Action(
                    delegate()
                    {
                        this.UpdateTexts();
                    }
                    ));
            }
        }

        /// <summary>
        /// Kills the load items thread.
        /// </summary>
        public void KillLoadItemsThread()
        {
            if (!CashDispenserWorker.IsVisible)
            {
                return;
            }
            if (BMC.Transport.Settings.IsGloryCDEnabled)
            {
                return;
            }
            try
            {
                if (_thLoadItems != null &&
                    _thLoadItems.IsAlive)
                {
                    _thLoadItems.Abort();
                }
                _thLoadItems = null;
            }
            catch { LogManager.WriteLog("|=> (CashDispenserStatus) : KillLoadItemsThread ", LogManager.enumLogLevel.Info); }
            finally
            {
                LogManager.WriteLog("|=> (CashDispenserStatus) : KillLoadItemsThread Called.", LogManager.enumLogLevel.Info);
            }
        }

        #region IDisposable Members

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events

                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("MessageBox objects are released successfully.");
                }
                disposed = true;
            }
        }

        ~CashDispenserStatus()
        {
            Dispose(false);
        }

        #endregion
    }
}
