using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;

namespace BMC.DBInterface.NetworkService
{
    public static class DataAccess
    {
        #region Private Variables

        private static string strExchangeConnectionString = string.Empty;

        #endregion Private Variables

        #region Properties
        public static string ExchangeConnectionString
        {
            get
            {
                return strExchangeConnectionString;
            }
            set
            {
                strExchangeConnectionString = value;
            }
        }

        #endregion Properties
        public static string GetExchangeConnectionString()
        {
            try
            {
                if (!string.IsNullOrEmpty(ExchangeConnectionString))
                {
                    return ExchangeConnectionString;
                }
                ExchangeConnectionString = BMC.Common.Utilities.DatabaseHelper.GetExchangeConnectionString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return ExchangeConnectionString;
        }
    }
}
