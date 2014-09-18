using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;
using SerialComLib.CoinDispenser;
using System.Configuration;
using BMC.Common.LogManagement;

namespace BMC.CashDispenser.Core
{
    /// <summary>
    /// CashDispenserItem
    /// </summary>
    public class CashDispenserItem
    {
        /// <summary>
        /// Success Error Code
        /// </summary>
        //private const int ERROR_CODE_SUCCESS = 0x30;

        /// <summary>
        /// Success Error Codes
        /// </summary>
        private static IDictionary<uint, uint> _successCodes = null;
        private static IDictionary<uint, uint> _uppertrayCodes = null;
        private static IDictionary<uint, uint> _lowertrayCodes = null;

        /// <summary>
        /// Initializes the <see cref="CashDispenserItem"/> class.
        /// </summary>
        static CashDispenserItem()
        {
            _successCodes = new SortedDictionary<uint, uint>() {
                {0x30, 0x30}, 
                {0x31, 0x31}
            };
            _uppertrayCodes = new SortedDictionary<uint, uint>();
            _lowertrayCodes = new SortedDictionary<uint, uint>();
            foreach (rsp_GetCashDispenserErrorCodes cashDispenserErrorCodes in CashDispenserFactory.GetCashDispenserErrorCodes())
            {
                if (Convert.ToBoolean(cashDispenserErrorCodes.CDECApplicableToUpperTray))
                    _uppertrayCodes.Add(Convert.ToUInt32(cashDispenserErrorCodes.CDEC_ErrorCode), Convert.ToUInt32(cashDispenserErrorCodes.CDEC_ErrorCode));
                if (Convert.ToBoolean(cashDispenserErrorCodes.CDECApplicableToLowerTray))
                _lowertrayCodes.Add(Convert.ToUInt32(cashDispenserErrorCodes.CDEC_ErrorCode), Convert.ToUInt32(cashDispenserErrorCodes.CDEC_ErrorCode));
            }
             
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CashDispenserItem"/> class.
        /// </summary>
        /// <param name="cassetteName">Name of the cassette.</param>
        /// <param name="cassetteAlias">The cassette alias.</param>
        public CashDispenserItem(string cassetteName, string cassetteAlias)
        {
            this.CassetteName = cassetteName;
            this.CassetteAlias = cassetteAlias;
            this.DeckType = TypeOfDeck.Upper;
        }

        /// <summary>
        /// Gets or sets the cassette ID.
        /// </summary>
        /// <value>The cassette ID.</value>
        public int CassetteID { get; internal set; }

        /// <summary>
        /// Gets or sets the name of the cassette.
        /// </summary>
        /// <value>The name of the cassette.</value>
        public string CassetteName { get; internal set; }

        /// <summary>
        /// Gets or sets the cassette alias.
        /// </summary>
        /// <value>The cassette alias.</value>
        public string CassetteAlias { get; set; }

        /// <summary>
        /// Gets or sets the denimination.
        /// </summary>
        /// <value>The denimination.</value>
        public int Denimination { get; set; }

        /// <summary>
        /// Gets or sets the total value.
        /// </summary>
        /// <value>The total value.</value>
        public decimal TotalValue { get; set; }

        /// <summary>
        /// Gets or sets the issued value.
        /// </summary>
        /// <value>The issued value.</value>
        public decimal IssuedValue { get; set; }

        /// <summary>
        /// Gets or sets the rejected value.
        /// </summary>
        /// <value>The rejected value.</value>
        public decimal RejectedValue { get; set; }

        /// <summary>
        /// Gets the remaining value.
        /// </summary>
        /// <value>The remaining value.</value>
        public decimal RemainingValue
        {
            get
            {
                decimal remaining = this.TotalValue;// (this.TotalValue - (this.IssuedValue + this.RejectedValue));
                if (remaining < 0) remaining = 0;
                return remaining;
            }
        }

        /// <summary>
        /// Gets the remaining value.
        /// </summary>
        /// <value>The remaining value.</value>
        public decimal RemainingValueCalculated
        {
            get
            {
                return (this.RemainingValue * this.Denimination);
            }
        }

        /// <summary>
        /// Gets the value calculated.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Calculated value.</returns>
        public decimal GetValueCalculated(decimal value)
        {
            return (value * this.Denimination);
        }

        /// <summary>
        /// Gets or sets the type of the deck.
        /// </summary>
        /// <value>The type of the deck.</value>
        public TypeOfDeck DeckType { get; internal set; }

        /// <summary>
        /// Gets the name of the COM port.
        /// </summary>
        /// <returns>COM Port Name.</returns>
        private static string GetCOMPortName()
        {
            string result = string.Empty;
            try
            {
                object value = ConfigurationManager.AppSettings["CoinDispenserCOMPort"];
                if (value != null)
                {
                    result = value.ToString();
                }
            }
            catch { }
            finally
            {
                if (string.IsNullOrEmpty(result))
                {
                    result = "COM1";
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the coin dispenser.
        /// </summary>
        /// <returns>Coin dispenser</returns>
        private static ICoinDispenser GetCoinDispenser()
        {
            ICoinDispenser dispenser = CoinDispenser.Initialise();
            dispenser.SerialPortName = GetCOMPortName();
            return dispenser;
        }

        /// <summary>
        /// Determines whether the specified error code is succeeded.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>
        /// 	<c>true</c> if the specified error code is succeeded; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSucceeded(uint errorCode)
        {
            return _successCodes.ContainsKey(errorCode);
        }
        //
        public static bool IsUpperTrayError(uint errorCode)
        {
            return _uppertrayCodes.ContainsKey(errorCode);
        }
        //
        public static bool IsLowerTrayError(uint errorCode)
        {
            return  _lowertrayCodes.ContainsKey(errorCode);
        }
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <returns>DispenserStatus</returns>
        public static DispenserStatus GetStatus(out string errorDesc, out uint iErrorcode)
        {
            DispenserStatus status = DispenserStatus.NotAvailable;
            ICoinDispenser coinDispenser = null;
            errorDesc = string.Empty;
            iErrorcode = 1;
            try
            {
                coinDispenser = GetCoinDispenser();
                CoinDispStatus dispStatus = coinDispenser.GetStatus();

                if (dispStatus != null)
                {
                    if (IsSucceeded(dispStatus.ErrorCode))
                    {
                        status = DispenserStatus.Available;
                    }
                    errorDesc = dispStatus.ErrDesc;
                    iErrorcode = dispStatus.ErrorCode;
                }
                else
                {
                    errorDesc = "Unable to connect cash dispenser.";
                }

                LogManager.WriteLog("|=> (GetStatus) : " + errorDesc, LogManager.enumLogLevel.Info);
            }
            //catch (System.IO.IOException ex)
            //{
            //    ExceptionManager.Publish(ex);
            //}
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                coinDispenser = null;
                if (string.IsNullOrEmpty(errorDesc))
                {
                    errorDesc = "Unable to connect cash dispenser.";
                }
            }

            return status;
        }

        /// <summary>
        /// Tests the status.
        /// </summary>
        /// <param name="errorDesc">The error desc.</param>
        /// <returns>DispenserStatus</returns>
        public DispenserStatus TestStatus(out string errorDesc)
        {
            DispenserStatus status = DispenserStatus.NotAvailable;
            ICoinDispenser coinDispenser = null;
            errorDesc = string.Empty;
            string deckMsg = "Deck Type : " + ((this.DeckType == TypeOfDeck.Lower) ? "Lower" : "Upper");
            CoinDispStatus dispStatus = null;
            bool canUpdateToDB = false;
            decimal issuedValue = 0;
            decimal rejectedValue = 0;
            decimal? issuedValue2 = 0;
            decimal? rejectedValue2 = 0;

            try
            {
                coinDispenser = GetCoinDispenser();

                if (this.DeckType == TypeOfDeck.Lower)
                    dispStatus = coinDispenser.TestLowerDeck();
                else
                    dispStatus = coinDispenser.TestUpperDeck();

                if (dispStatus != null)
                {
                    if (IsSucceeded(dispStatus.ErrorCode))
                    {
                        status = DispenserStatus.Available;
                    }
                    errorDesc = dispStatus.ErrDesc;
                }
                else
                {
                    errorDesc = "Unable to connect cash dispenser.";
                }

                LogManager.WriteLog("|=> (TestStatus) : " + deckMsg + ", " + errorDesc, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                try
                {
                    if (dispStatus != null)
                    {
                        LogManager.WriteLog("|=> (Dispense) : Error Code : " + dispStatus.ErrorCode.ToString(), LogManager.enumLogLevel.Info);
                        canUpdateToDB = true;

                        if (this.DeckType == TypeOfDeck.Lower)
                        {
                            issuedValue = dispStatus.LowDispNotes;
                            rejectedValue = dispStatus.LowRejNotes;
                        }
                        else
                        {
                            issuedValue = dispStatus.UPDispNotes;
                            rejectedValue = dispStatus.UPRejNotes;
                        }
                    }

                    if (canUpdateToDB)
                    {
                        // UPDATE IT INTO DATABASE
                        using (CashDispenserDBDataContext context = new CashDispenserDBDataContext(CashDispenserFactory.ExchangeConnectionString))
                        {
                            int? iResult = 0;
                            context.UpdateCashDispenserItemValues(this.CassetteID,
                                issuedValue,
                                rejectedValue,
                                ref issuedValue2,
                                ref rejectedValue2,
                                ref iResult);
                            LogManager.WriteLog("|=> (TestStatus) : Result : " + (iResult.Value > 0).ToString(), LogManager.enumLogLevel.Info);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
                coinDispenser = null;
            }

            return status;
        }

        /// <summary>
        /// Dispenses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>DispenseResult</returns>
        public DispenseResult Dispense(decimal value)
        {
            ICoinDispenser coinDispenser = null;
            DispenseResult result = new DispenseResult();
            decimal issuedValue = value;
            decimal rejectedValue = 0;
            decimal? issuedValue2 = 0;
            decimal? rejectedValue2 = 0;
            CoinDispStatus dispStatus = null;
            bool success = false;
            LogManager.WriteLog("|=> (Dispense) : " +
                ((this.DeckType == TypeOfDeck.Lower) ? "Lower" : "Upper") + " " +
                "Value : " + value.ToString(),
                LogManager.enumLogLevel.Info);
            bool canUpdateToDB = false;

            try
            {
                // COM API CALL
                result.Value = value;
                try
                {
                    coinDispenser = GetCoinDispenser();
                    if (this.DeckType == TypeOfDeck.Lower)
                        dispStatus = coinDispenser.DispenseLowDeck((ushort)value);
                    else
                        dispStatus = coinDispenser.DispenseUPDeck((ushort)value);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
                finally
                {
                    if (dispStatus != null)
                    {
                        LogManager.WriteLog("|=> (Dispense) : Error Code : " + dispStatus.ErrorCode.ToString(), LogManager.enumLogLevel.Info);
                        success = IsSucceeded(dispStatus.ErrorCode);
                        canUpdateToDB = true;
                        string outputMessage = "Output Message => Deck => {0}, Dispensed Value => {1:F}, Rejected Value => {2:F}";

                        if (this.DeckType == TypeOfDeck.Lower)
                        {
                            issuedValue = dispStatus.LowDispNotes;
                            rejectedValue = dispStatus.LowRejNotes;
                            try
                            {
                                outputMessage = string.Format(outputMessage, "Lower", issuedValue, rejectedValue);
                            }
                            catch { }
                        }
                        else
                        {
                            issuedValue = dispStatus.UPDispNotes;
                            rejectedValue = dispStatus.UPRejNotes;
                            try
                            {
                                outputMessage = string.Format(outputMessage, "Upper", issuedValue, rejectedValue);
                            }
                            catch { }
                        }
                        result.ErrorDescription = dispStatus.ErrDesc;
                        LogManager.WriteLog(outputMessage, LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        result.ErrorDescription = "Unable to dispense from " + this.CassetteAlias + ".";
                        result.Failed = true;
                    }

                    LogManager.WriteLog("|=> (Dispense) : " + result.ErrorDescription, LogManager.enumLogLevel.Info);
                    coinDispenser = null;
                }

                if (canUpdateToDB)
                {
                    // UPDATE IT INTO DATABASE
                    using (CashDispenserDBDataContext context = new CashDispenserDBDataContext(CashDispenserFactory.ExchangeConnectionString))
                    {
                        int? iResult = 0;
                        context.UpdateCashDispenserItemValues(this.CassetteID,
                            issuedValue,
                            rejectedValue,
                            ref issuedValue2,
                            ref rejectedValue2,
                            ref iResult);

                        if (iResult != null && iResult.HasValue)
                        {
                            result.Result = (iResult.Value > 0);
                        }
                        LogManager.WriteLog("|=> (Dispense) : Result : " + result.Result.ToString(), LogManager.enumLogLevel.Info);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (result != null)
                {
                    result.Result = success;
                    if (canUpdateToDB)
                    {
                        result.IssuedValue = issuedValue;
                        result.RejectedValue = rejectedValue;

                        if (issuedValue2 != null && issuedValue2.HasValue)
                        {
                            this.IssuedValue = issuedValue2.Value;

                        }
                        if (rejectedValue2 != null && rejectedValue2.HasValue)
                        {
                            this.RejectedValue = rejectedValue2.Value;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Dispenser Status
        /// </summary>
        public enum DispenserStatus
        {
            /// <summary>
            /// NotAvailable
            /// </summary>
            NotAvailable = 0,
            /// <summary>
            /// Available
            /// </summary>
            Available = 1
        }

        /// <summary>
        /// TypeOfDeck
        /// </summary>
        public enum TypeOfDeck
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,
            /// <summary>
            /// Upper
            /// </summary>
            Upper = 1,
            /// <summary>
            /// Lower
            /// </summary>
            Lower = 2
        }

        /// <summary>
        /// DispenseResult
        /// </summary>
        public class DispenseResult
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DispenseResult"/> class.
            /// </summary>
            public DispenseResult() { }

            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="DispenseResult"/> is result.
            /// </summary>
            /// <value><c>true</c> if result; otherwise, <c>false</c>.</value>
            public bool Result { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="DispenseResult"/> is failed.
            /// </summary>
            /// <value><c>true</c> if failed; otherwise, <c>false</c>.</value>
            public bool Failed { get; internal set; }

            /// <summary>
            /// Gets or sets the error description.
            /// </summary>
            /// <value>The error description.</value>
            public string ErrorDescription { get; internal set; }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>The value.</value>
            internal decimal Value { get; set; }

            /// <summary>
            /// Gets or sets the issued value.
            /// </summary>
            /// <value>The issued value.</value>
            public decimal IssuedValue { get; internal set; }

            /// <summary>
            /// Gets or sets the rejected value.
            /// </summary>
            /// <value>The rejected value.</value>
            public decimal RejectedValue { get; internal set; }
        }
    }
}
