using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using System.Data.Linq;
using BMC.Common.LogManagement;
using BMC.Security;

namespace BMC.CashDispenser.Core
{
    /// <summary>
    /// Cash Dispenser Interface.
    /// </summary>
    public interface ICashDispenser : IDisposable
    {
        /// <summary>
        /// Gets the dispenser items.
        /// </summary>
        /// <value>The dispenser items.</value>
        IList<CashDispenserItem> DispenserItems { get; }
    }

    /// <summary>
    /// Cash Dispenser Implementation.
    /// </summary>
    internal class CashDispenserImpl : ICashDispenser
    {
        /// <summary>
        /// List of dispenser items.
        /// </summary>
        private IList<CashDispenserItem> _dispenserItems = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashDispenserImpl"/> class.
        /// </summary>
        /// <param name="dispenserItems">The dispenser items.</param>
        internal CashDispenserImpl(List<CashDispenserItem> dispenserItems)
        {
            _dispenserItems = dispenserItems;
            //LogManager.WriteLog("|=> (CashDispenserImpl) : Constructor Invoked.", LogManager.enumLogLevel.Info);
        }

        #region ICashDispenser Members

        /// <summary>
        /// Gets the dispenser items.
        /// </summary>
        /// <value>The dispenser items.</value>
        public IList<CashDispenserItem> DispenserItems
        {
            get { return _dispenserItems; }
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

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CashDispenserImpl"/> is reclaimed by garbage collection.
        /// </summary>
        ~CashDispenserImpl()
        {
            Dispose(false);
        }

        #endregion
    }

    /// <summary>
    /// Cash Dispenser Factory
    /// </summary>
    public static class CashDispenserFactory
    {
        /// <summary>
        /// Gets the dispenser.
        /// </summary>
        /// <returns>ICashDispenser</returns>
        public static ICashDispenser GetDispenser()
        {
            ICashDispenser dispenser = new CashDispenserImpl(new List<CashDispenserItem>());
            LogManager.WriteLog("|=> (GetDispenser) : Inside Get Cash Dispenser.", LogManager.enumLogLevel.Info);

            try
            {
                try
                {
                    using (CashDispenserDBDataContext context = new CashDispenserDBDataContext(ExchangeConnectionString))
                    {
                        ISingleResult<rsp_GetCashDispenserItemsResult> items = context.GetCashDispenserItems();
                        if (items != null)
                        {
                            CashDispenserItem.TypeOfDeck prevDeckType = CashDispenserItem.TypeOfDeck.None;
                            foreach (rsp_GetCashDispenserItemsResult item in items)
                            {
                                CashDispenserItem item2 = new CashDispenserItem(item.CassetteName, item.CassetteAlias);
                                item2.CassetteID = item.CassetteID;
                                item2.Denimination = item.Denomination;
                                item2.TotalValue = item.TotalValue;
                                item2.IssuedValue = item.IssuedValue;
                                item2.RejectedValue = item.RejectedValue;
                                LogManager.WriteLog(string.Format("|=> (GetDispenser) : Cassette [{0}], Denonimation : [{1:D}], Balance : [{2:F}].", 
                                    item2.CassetteAlias, 
                                    item2.Denimination,
                                    item2.RemainingValueCalculated), LogManager.enumLogLevel.Debug);

                                if (prevDeckType == CashDispenserItem.TypeOfDeck.Upper)
                                {
                                    item2.DeckType = CashDispenserItem.TypeOfDeck.Lower;
                                    //LogManager.WriteLog(string.Format("|=> (GetDispenser) : Upper Deck [{0}].", item2.CassetteAlias), LogManager.enumLogLevel.Info);
                                }
                                else
                                {
                                    prevDeckType = item2.DeckType;
                                    //LogManager.WriteLog(string.Format("|=> (GetDispenser) : Lower Deck [{0}].", item2.CassetteAlias), LogManager.enumLogLevel.Info);
                                }

                                dispenser.DispenserItems.Add(item2);
                            }
                        }
                    }
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return dispenser;
        }
        //
        public static List<rsp_GetCashDispenserErrorCodes> GetCashDispenserErrorCodes()
        {
            LogManager.WriteLog("|=> (GetCashDispenserErrorCodes) : Inside GetCashDispenserErrorCodes.", LogManager.enumLogLevel.Info);
            ISingleResult<rsp_GetCashDispenserErrorCodes> items;
            try
            {
                try
                {
                    using (CashDispenserDBDataContext context = new CashDispenserDBDataContext(ExchangeConnectionString))
                    {
                        items = context.GetCashDispenserErrorCodes();
                        return items.ToList();
                    }
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return null;
        }
        /// <summary>
        /// Updates the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static bool UpdateItem(CashDispenserItem item)
        {
            bool result = false;
            LogManager.WriteLog("|=> (UpdateItem) : Inside Update Item.", LogManager.enumLogLevel.Info);

            try
            {
                using (CashDispenserDBDataContext context = new CashDispenserDBDataContext(ExchangeConnectionString))
                {
                    int userID = 0;
                    string userName = string.Empty;

                    if (SecurityHelper.CurrentUser != null && 
                        SecurityHelper.CurrentUser.SecurityUserID != 0 && 
                        SecurityHelper.CurrentUser.UserName != null)
                    {
                        userID = SecurityHelper.CurrentUser.SecurityUserID;
                        userName = SecurityHelper.CurrentUser.DisplayName;
                    }

                    int? iResult = 0;
                    context.UpdateCashDispenserItem(item.CassetteID,
                        item.CassetteAlias,
                        item.Denimination,
                        item.TotalValue,
                        userID,
                        userName,
                        ref iResult);

                    if (iResult != null && iResult.HasValue)
                        result = iResult.Value > 0;
                    LogManager.WriteLog("|=> (UpdateItem) : Result : " + result.ToString(), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the exchange connection string.
        /// </summary>
        /// <value>The exchange connection string.</value>
        public static string ExchangeConnectionString
        {
            get { return ConnectionStringHelper.ExchangeConnectionString; }
        }
    }
}
