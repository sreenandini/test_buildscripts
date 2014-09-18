using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ConfigurationManagement;
using System.Windows;
using BMC.Business.CashDeskOperator;
using Audit.Transport;
using BMC.Common.LogManagement;


namespace BMC.Presentation.CashDeskOperator
{
    #region Factory Class BmcCashDispenserFactory
    public static class BmcCashDispenserFactory
    {
        public static IBmcCashDispenser GetDispenser(UIElement parent, ModuleName ctype)
        {           
            try
            {
               
                if (BMC.Transport.Settings.IsGloryCDEnabled)
                {
                    LogManager.WriteLog("Cash Dispenser Type : Glory", LogManager.enumLogLevel.Info);
                   return new GloryCashDispenser(parent, ctype);
                }
                else
                {
                    LogManager.WriteLog("Cash Dispenser Type Not Found", LogManager.enumLogLevel.Info);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    #endregion
}
