using System;
using BMC.Presentation.POS.Helper_classes;
using System.Globalization;
using System.Windows.Data;
using BMC.Common.Utilities;

namespace BMC.Presentation.POS_
{
    [ValueConversion(typeof(decimal), typeof(string))]
    public class PriceConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal price = 0;

            if (value != null && value.ToString().ToUpper() == "UNDECLARED")
                return value;
            if (value != null && 
                value.ToString() != String.Empty && 
                decimal.TryParse(value.ToString(), out price))
            {
            }
            return price.GetUniversalCurrencyFormat();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string price = value.ToString();

            decimal result;
            if (Decimal.TryParse(price, out result))
            {
                return result;
            }
            return value;
        }
        #endregion
    }

    [ValueConversion(typeof(decimal), typeof(string))]
    public class PriceConverterWithSymbol : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal price = 0;

            if (value != null && value.ToString().ToUpper() == "UNDECLARED")
                return value;
            if (value != null &&
                value.ToString() != String.Empty &&
                decimal.TryParse(value.ToString(), out price))
            {
            }
            return price.GetUniversalCurrencyFormatWithSymbol();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string price = value.ToString();

            decimal result;
            if (Decimal.TryParse(price, out result))
            {
                return result;
            }
            return value;
        }
        #endregion
    }

    [ValueConversion(typeof(decimal), typeof(string))]
    public class CurrencyConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal price = 0;
            if (value != null && value.ToString() != String.Empty && Decimal.TryParse(value.ToString(), out price))
            {

            }
            return price.GetUniversalCurrencyFormat();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string price = value.ToString();

            decimal result;
            if (Decimal.TryParse(price, NumberStyles.Any, null, out result))
            {
                return result;
            }
            return value;
        }

        #endregion
    }

    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.ToString() != String.Empty)
                try
                {
                    return value.ToString().ReadDate().GetUniversalDateFormat();
                }
                catch (Exception)
                {
                    return string.Empty;
                }

            return DateTime.Now.GetUniversalDateFormat();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().ReadDate();
        }
    }

    [ValueConversion(typeof(decimal), typeof(string))]
    public class CurrencyPriceConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal price = 0;
            if (value != null &&
                value.ToString() != String.Empty &&
                decimal.TryParse(value.ToString(), out price))
            {
                price = System.Convert.ToDecimal(value);
            }
            return price.GetUniversalCurrencyFormatWithSymbol();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string price = value.ToString();

            decimal result;
            if (Decimal.TryParse(price, NumberStyles.Any, null, out result))
            {
                return result;
            }
            return value;
        }
        #endregion
    }
}
