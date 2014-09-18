using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class CommonBusinessObjects
    {
        public static string GetCurrency(double CurrencyUnitValue)
        {
            return CommonUtilities.GetCurrency(CurrencyUnitValue);
        }

        public static string GetCurrency(string CurrencyUnitValue)
        {
            return GetCurrency(double.Parse(CurrencyUnitValue));
        }
    }
}
