using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.LogManagement;
using System.Windows;
using BMC.Presentation.POS.UserControls;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using CDC = BMC.CashDispenser.Core;
using BMC.Common.Utilities;
using BMC.CashDispenser.Core;
using Audit.Transport;
using System.Windows.Media;
using BMC.Presentation.POS.Views;
using Audit.BusinessClasses;
using System.Windows.Controls;

namespace BMC.Presentation.POS.Helper_classes
{
    /// <summary>
    /// DispenserWorkerResult
    /// </summary>
    public enum DispenserWorkerResult
    {
        /// <summary>
        /// None
        /// </summary>
        None = 1,
        /// <summary>
        /// Success
        /// </summary>
        Success = 2,
        /// <summary>
        /// Failed
        /// </summary>
        Failed = 4,
        /// <summary>
        /// UpperDeckOK
        /// </summary>
        UpperDeckOK = 8,
        /// <summary>
        /// UpperDeckFail
        /// </summary>
        UpperDeckFail = 16,
        /// <summary>
        /// LowerDeckOK
        /// </summary>
        LowerDeckOK = 32,
        /// <summary>
        /// LowerDeckFail
        /// </summary>
        LowerDeckFail = 64,
        /// <summary>
        /// BothDeckFail
        /// </summary>
        BothDeckFail = 128,
    }

    public class DispenserText
    {
        private static Brush redBrush = Brushes.Red;
        private static Brush greenBrush = Brushes.Green;

        public DispenserText()
        {
            this.Text = "...";
            this.Text2 = "...";
            this.StatusText = "";
            this.IsOK = false;
        }
        public string Text { get; set; }
        public string Text2 { get; set; }
        public string StatusText { get; set; }
        public Brush BackBrush { get; private set; }

        private bool _isOK = false;
        public bool IsOK
        {
            get { return _isOK; }
            set
            {
                _isOK = value;
                this.SetBrush(value);
            }
        }

        private void SetBrush(bool isOK)
        {
            this.BackBrush = (isOK ? greenBrush : redBrush);
        }

        public string GetFormattedStatusText()
        {
            return this.StatusText;// (this.IsOK ? this.StatusText : string.Empty);
        }
    }

    public interface ICashDispenserStatusParent
    {
        bool ParentLoaded { get; set; }
        DispenserText UpperDeckText { get; set; }
        DispenserText LowerDeckText { get; set; }
        string StatusText { get; set; }
    }

    public class DeckDenomValue
    {
        private decimal _denomination = 0;
        public decimal Denomination
        {
            get { return _denomination; }
            set
            {
                _denomination = value;
                this.CalculateTotalCount();
            }
        }

        private decimal _value = 0;
        public decimal Value
        {
            get { return _value; }
            set
            {
                _value = value;
                this.CalculateTotalCount();
            }
        }

        private void CalculateTotalCount()
        {
            if (_denomination == 0)
            {
                _totalCount = 0;
            }
            else
            {
                _totalCount = (_value / _denomination);
            }
        }

        private decimal _totalCount = 0;
        public decimal TotalCount
        {
            get { return _totalCount; }
        }
    }

    /// <summary>
    /// Cash Dispenser Worker Class
    /// </summary>
    public class CashDispenserWorker : IDisposable
    {
        private ModuleName _moduleName = ModuleName.Cash_Dispenser;

        public ModuleName ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        public string SlotMachine { get; set; }       

        /// <summary>
        /// Initializes a new instance of the <see cref="CashDispenserWorker"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public CashDispenserWorker(UIElement parent, ModuleName moduleName)
        {
            this.ParentElement = parent;
            _moduleName = moduleName;
        }

        /// <summary>
        /// Loads the decks.
        /// </summary>
        public void LoadDecks()
        {
            using (ICashDispenser dispenser = CashDispenserFactory.GetDispenser())
            {
                IList<CashDispenserItem> items = dispenser.DispenserItems;
                foreach (CashDispenserItem item in items)
                {
                    if (item.DeckType == CashDispenserItem.TypeOfDeck.Lower)
                        this.LowerDeck = item;
                    else
                        this.UpperDeck = item;
                }
            }
        }

        /// <summary>
        /// Gets or sets the parent element.
        /// </summary>
        /// <value>The parent element.</value>
        public UIElement ParentElement { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        public static bool IsVisible
        {
            get
            {
                return Settings.CashDispenserEnabled;
            }
        }

        /// <summary>
        /// Gets the visibliity.
        /// </summary>
        /// <value>The visibliity.</value>
        public static Visibility Visibliity
        {
            get
            {
                return (IsVisible ? Visibility.Visible : Visibility.Collapsed);
            }
        }

        #region Cash Dispenser Settings (By A.Vinod Kumar on 29/12/2011)

        /// <summary>
        /// Gets or sets the upper deck.
        /// </summary>
        /// <value>The upper deck.</value>
        public CDC.CashDispenserItem UpperDeck { get; private set; }

        /// <summary>
        /// Gets or sets the lower deck.
        /// </summary>
        /// <value>The lower deck.</value>
        public CDC.CashDispenserItem LowerDeck { get; private set; }

        /// <summary>
        /// Dispenses the specified show message box.
        /// </summary>
        /// <param name="showMessageBox">if set to <c>true</c> [show message box].</param>
        public DispenserWorkerResult Dispense(decimal cd1Value, decimal cd2Value, out string outputString)
        {
            CDC.CashDispenserItem.DispenseResult result1 = null;
            CDC.CashDispenserItem.DispenseResult result2 = null;
            string cd1 = string.Empty;
            string cd2 = string.Empty;
            DispenserWorkerResult result = DispenserWorkerResult.None;
            outputString = string.Empty;
            this.EnsureDecksAreLoaded();

            try
            {
                if (cd1Value > 0)
                {
                    try
                    {
                        CDC.CashDispenserItem item1 = this.UpperDeck;
                        cd1 = item1.CassetteAlias;
                        result1 = item1.Dispense(cd1Value);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
                else
                {
                    result1 = new BMC.CashDispenser.Core.CashDispenserItem.DispenseResult() { Result = true };
                }

                if (cd2Value > 0)
                {
                    try
                    {
                        CDC.CashDispenserItem item2 = this.LowerDeck;
                        cd2 = item2.CassetteAlias;
                        result2 = item2.Dispense(cd2Value);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
                else
                {
                    result2 = new BMC.CashDispenser.Core.CashDispenserItem.DispenseResult(){Result = true};
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                string currencySymbol = ExtensionMethods.GetCurrencySymbol(string.Empty);
                StringBuilder sb = new StringBuilder();

                if (result1 != null)
                {
                    sb.Append(result1.IssuedValue.ToString() + " " + currencySymbol);
                    sb.Append(" issued, ");
                    sb.Append(result1.RejectedValue.ToString() + " " + currencySymbol);
                    sb.Append(" rejected from " + cd1);
                    sb.Append(" (" + result1.ErrorDescription + ")");

                    // Upper Deck
                    if (result1.Result)
                    {
                        result |= DispenserWorkerResult.UpperDeckOK;
                    }
                    else
                    {
                        result |= DispenserWorkerResult.UpperDeckFail;
                    }
                }

                // Lower Deck
                if (result2 != null)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(Environment.NewLine);
                    }
                    sb.Append(result2.IssuedValue.ToString() + " " + currencySymbol);
                    sb.Append(" issued, ");
                    sb.Append(result2.RejectedValue.ToString() + " " + currencySymbol);
                    sb.Append(" rejected from " + cd2);
                    sb.Append(" (" + result2.ErrorDescription + ")");

                    if (result2.Result)
                    {
                        result |= DispenserWorkerResult.LowerDeckOK;
                    }
                    else
                    {
                        result |= DispenserWorkerResult.LowerDeckFail;
                    }
                }

                outputString = sb.ToString();
            }

            // Final Condition
            if (((result & DispenserWorkerResult.LowerDeckOK) == DispenserWorkerResult.LowerDeckOK) &&
                ((result & DispenserWorkerResult.UpperDeckOK) == DispenserWorkerResult.UpperDeckOK))
            {
                result = DispenserWorkerResult.Success |
                    DispenserWorkerResult.LowerDeckOK |
                    DispenserWorkerResult.UpperDeckOK;
            }
            else
            {
                result &= ~DispenserWorkerResult.None;
                result |= DispenserWorkerResult.Failed;
            }
            return result;
        }

        /// <summary>
        /// Performs the cash dispense.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public void Dispense(string fieldName, string fieldValue, decimal value, Action action)
        {
            try
            {
                if (IsVisible)
                {
                    this.LoadDecks();
                    if (Settings.AutoCashDispenseRequired)
                    {
                        if (!this.AutoCashDispense(fieldName, fieldValue, value))
                        {
                            if (MessageBox.ShowBox("MessageID387", BMC_Icon.Question, BMC_Button.YesNo)
                                == System.Windows.Forms.DialogResult.Yes)
                            {
                                this.ManualCashDispense(); // Auto Dispense + Manual Dispense
                            }
                        }
                    }
                    else
                    {
                        this.ManualCashDispense(); // Manual Dispense
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (action != null)
                {
                    action();
                }
            }
        }

        /// <summary>
        /// Ensures the decks are loaded.
        /// </summary>
        private void EnsureDecksAreLoaded()
        {
            if (this.UpperDeck == null ||
                this.LowerDeck == null)
            {
                this.LoadDecks();
            }
        }
        //
        private void WriteAuditLog(CashDispenserItem UpperDeck, CashDispenserItem LowerDeck, decimal UpperCount, decimal LowerCount, string AutoDispense, decimal value)
        {
            try
            {
                string auditValue = UpperDeck.CassetteAlias + " : " + UpperDeck.Denimination + ", " + (UpperCount * UpperDeck.Denimination).ToString();
                auditValue += " | " + LowerDeck.CassetteAlias + " : " + LowerDeck.Denimination + ", " + (LowerCount * LowerDeck.Denimination).ToString();
                auditValue += " | " + AutoDispense + " : " + value.ToString();
                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    AuditModuleName = ModuleName.Cash_Dispenser,
                    Audit_Screen_Name = "Auto Cash Dispense",
                    Audit_Desc = "From Module : " + ModuleName.Cash_Dispenser.ToString(),
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
        //
        /// <summary>
        /// Performs the auto cash dispense.
        /// </summary>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool AutoCashDispense(string fieldName, string fieldValue, decimal value)
        {
            DispenserWorkerResult result = DispenserWorkerResult.None;
            string message = string.Empty;
            this.EnsureDecksAreLoaded();
            IDictionary<string, DeckDenomValue> pairs =
                new SortedDictionary<string, DeckDenomValue>(StringComparer.InvariantCultureIgnoreCase);
            string keyLower = "LOWER";
            string keyUpper = "UPPER";

            decimal upperCount = 0;
            decimal lowerCount = 0;
            decimal receiptAmount = 0;

            try
            {
                // Determine the number of counts from lower and upper deck
                decimal upperAvailAmount = this.UpperDeck.RemainingValueCalculated;
                decimal lowerAvailAmount = this.LowerDeck.RemainingValueCalculated;

                // denominations
                decimal upperDenom = this.UpperDeck.Denimination;
                decimal lowerDenom = this.LowerDeck.Denimination;
                if (lowerDenom > upperDenom)
                {
                    // denom
                    decimal tempDenom = lowerDenom;
                    lowerDenom = upperDenom;
                    upperDenom = tempDenom;

                    // total amount
                    decimal tempAmount = lowerAvailAmount;
                    lowerAvailAmount = upperAvailAmount;
                    upperAvailAmount = tempAmount;

                    keyLower = "UPPER";
                    keyUpper = "LOWER";
                }

                // mapping for upper and lower deck
                pairs.Add(keyLower, new DeckDenomValue() { Denomination = lowerDenom, Value = lowerAvailAmount });
                pairs.Add(keyUpper, new DeckDenomValue() { Denomination = upperDenom, Value = upperAvailAmount });

                // if valid amount is passed
                if (value > 0)
                {
                    decimal inputValue = value;

                    // upper count
                    upperCount = Math.Floor(inputValue / pairs[keyUpper].Denomination);
                    if (upperCount > pairs[keyUpper].TotalCount)
                    {
                        upperCount = pairs[keyUpper].TotalCount;
                    }
                    inputValue -= (upperCount * pairs[keyUpper].Denomination);

                    // lower count
                    lowerCount = Math.Floor(inputValue / pairs[keyLower].Denomination);
                    if (lowerCount > pairs[keyLower].TotalCount)
                    {
                        lowerCount = pairs[keyLower].TotalCount;
                    }
                    inputValue -= (lowerCount * pairs[keyLower].Denomination);

                    // updated values
                    pairs[keyUpper].Value = upperCount;
                    pairs[keyLower].Value = lowerCount;
                    receiptAmount = inputValue;
                }

                // dispense
                result = this.Dispense(pairs["UPPER"].Value, pairs["LOWER"].Value, out message);
                if (result != (DispenserWorkerResult.Success |
                    DispenserWorkerResult.LowerDeckOK |
                    DispenserWorkerResult.UpperDeckOK))
                {
                    LogManager.WriteLog(message, LogManager.enumLogLevel.Error);
                }
                else
                {
                    LogManager.WriteLog(message, LogManager.enumLogLevel.Info);
                }

                // try to print the remaing amount as receipt
                if (receiptAmount > 0)
                {
                    try
                    {
                        using (CReportViewer objReportViewer = new CReportViewer())
                        {
                            objReportViewer.PrintCashDispenserReceipt(System.Environment.MachineName, fieldName, fieldValue,
                                                                    Convert.ToDecimal(receiptAmount));
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }

                WriteAuditLog(this.UpperDeck, this.LowerDeck, upperCount, lowerCount, "Receipt", receiptAmount);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return (result == (DispenserWorkerResult.Success |
                    DispenserWorkerResult.LowerDeckOK |
                    DispenserWorkerResult.UpperDeckOK));
        }

        /// <summary>
        /// Shows the manual cash dispense message.
        /// </summary>
        /// <returns>True if succeeded; otherwise false.</returns>
        private bool ManualCashDispense()
        {
            bool result = false;

            try
            {
                this.EnsureDecksAreLoaded();
                using (ManualCashDispenser dispenser = new ManualCashDispenser(this))
                {
                    dispenser.ShowDialogEx(this.ParentElement);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
        #endregion

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

                    LogManager.WriteLog("CashDispenser objects are released successfully.", LogManager.enumLogLevel.Info);

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CashDispenserWorker"/> is reclaimed by garbage collection.
        /// </summary>
        ~CashDispenserWorker()
        {
            Dispose(false);
        }

        #endregion
    }
    public static class CashDispenserStatusHelper
    {
        public static CashDispenserStatus AddCashDispenserStatus(Panel parent)
        {
            CashDispenserStatus dispenserStatus = null;
            if (Settings.CashDispenserEnabled)
            {
                parent.Children.Add((
                    dispenserStatus = new CashDispenserStatus()
                    {
                        Height = 280,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    }));
            }
            return dispenserStatus;
        }
    }
}
