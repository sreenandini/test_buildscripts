using System;
using BMC.Common.Utilities;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using BMC.CashDeskOperator;
using BMC.Transport;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Configuration;

namespace BMC.Presentation.POS.Helper_classes
{
    //[ValueConversion(typeof(int), typeof(Brush))]
    //public class UndeclaredCollectionConvertorBackColor : IValueConverter
    //{
    //    #region IValueConverter Members

    //    public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        if (value != null && (int)value == 0)
    //            return new SolidColorBrush(Colors.Pink);

    //        //if (value != null && (int)value == 2)  //Waiting
    //        //    return new SolidColorBrush(Colors.Blue);

    //        //if (value != null && (int)value == 3)  //InProgress
    //        //    return new SolidColorBrush(Colors.Transparent);

    //        if (value != null && (int)value == 4)  //Success
    //            return new SolidColorBrush(Colors.Pink);

    //        if (value != null && (int)value == 5)  //Failed
    //            return new SolidColorBrush(Colors.Red);

    //        return new SolidColorBrush(Colors.Transparent);
    //    }

    //    public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        return 4;
    //    }

    //    #endregion
    //}

    //Manoj 31st Aug 2010 - CR#77002
    //Following TimeFormat class will help to format the Hour, Minute and Time
    //to be displayed with specified format.
    //Eaelier if the hour is 1 it simple displays 1, but we wanted to be displyed
    //as 01. Following class will help to the same.
    [System.Windows.Data.ValueConversion(typeof(double), typeof(string))]
    class TimeFormat : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    value = "0";
                }
                string resultval = String.Format("{0:00}", value);
                return resultval;
            }
            catch (System.Exception exp)
            {
                return String.Format("{0:00}", 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    value = "0";
                }
                string resultval = String.Format("{0:00}", value);
                return resultval;
            }
            catch (System.Exception exp)
            {
                return String.Format("{0:00}", 0);
            }

        }
    }

    [System.Windows.Data.ValueConversion(typeof(int), typeof(string))]
    class AftStatus : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string resultval = (int)value == 1 ? "Aft Enabled" : "Aft Disabled";
                return resultval;
            }
            catch (System.Exception exp)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            string resultval = (int)value == 0 ? "" : "";
            return resultval;
        }
    }

    [System.Windows.Data.ValueConversion(typeof(int), typeof(string))]
    class AftStatusColor : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return (int)value == 1 ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Firebrick);
            }
            catch (System.Exception exp)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return (int)value == 1 ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Firebrick);
        }
    }



    [System.Windows.Data.ValueConversion(typeof(string), typeof(string))]
    class PromotionStatusColor : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch (value.ToString())
                {
                    case "Active":
                        return new SolidColorBrush(Colors.DarkMagenta);

                    default:
                        return new SolidColorBrush(Colors.Black);

                }


            }
            catch (System.Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            try
            {
                switch (value.ToString())
                {
                    case "Active":
                        return new SolidColorBrush(Colors.DarkMagenta);

                    default:
                        return new SolidColorBrush(Colors.Black);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
    }

   

    [ValueConversion(typeof(int), typeof(bool))]
    public class UndeclaredCollectionEnableState : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null || (int)value != 0;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }

        #endregion
    }

    public class UndeclaredCollectionConvertorBackColor : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values[0] != null && (int)values[0] == 6 && values[1] != null && (int)values[1] == 0))
                return new SolidColorBrush(Colors.Goldenrod);

            if ((values[1] != null && (int)values[1] == 1))
                return new SolidColorBrush(Colors.Gray);

            if (values[0] != null && (int)values[0] == 0)
                return new SolidColorBrush(Colors.Pink);

            if (values[0] != null && (int)values[0] == 2) //Waiting
                return new SolidColorBrush(Colors.Blue);

            //if (value != null && (int)value == 3)  //InProgress
            //    return new SolidColorBrush(Colors.Transparent);

            if (values[0] != null && (int)values[0] == 4)  //Success
                return new SolidColorBrush(Colors.Pink);

            if (values[0] != null && (int)values[0] == 5)  //Failed
                return new SolidColorBrush(Colors.Red);

            return new SolidColorBrush(Colors.Transparent);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    public class UndeclaredCollectionEnableDisableState : IMultiValueConverter
    {

        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null) return true;
            if (values[1] == null) return true;
            try
            {
                if ((int)values[0] == 1 && (int)values[1] == 0)
                    return true;
                if (Settings.AllowMultipleDrops && (int)values[1] == 0 && (int)values[2] == 0)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }


            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class BarPositionCheckStateByRouteIDCheckState : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (!(bool)values[2]) return false;

                foreach (var barPositionRouteNo in new CollectionHelper().GetBarPositionByRouteNo((string)values[1]))
                    if (barPositionRouteNo.Bar_Pos_No == (int)values[0]) return true;
            }
            catch { }
            return false;

        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object[] { };
        }

        #endregion
    }

    [ValueConversion(typeof(int), typeof(System.Windows.Visibility))]
    public class UndeclaredCollectionVisibilityStatus : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((int)value)
            {
                case 1:
                    return System.Windows.Visibility.Visible;
                default:
                    return System.Windows.Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    [ValueConversion(typeof(int), typeof(System.Windows.Visibility))]
    public class FinalDropMoneyBagVisibilityStatus : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((int)value)
            {
                case 0:
                    return System.Windows.Visibility.Visible;
                case 1:
                    return System.Windows.Visibility.Collapsed;
                default:
                    return System.Windows.Visibility.Hidden;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    [ValueConversion(typeof(bool), typeof(System.Windows.Visibility))]
    public class WeekBatchGridVisibility : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
                return System.Windows.Visibility.Visible;

            return System.Windows.Visibility.Collapsed;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    [ValueConversion(typeof(int), typeof(System.Windows.Visibility))]
    public class CollectionVisibilityStatus : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((int)value)
            {
                case 3:
                    return System.Windows.Visibility.Visible;
                default:
                    return System.Windows.Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(int), typeof(bool))]
    public class ItemFocusStatus : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((int)value)
            {
                case 3:
                    return true;
                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    [ValueConversion(typeof(string), typeof(System.Windows.Visibility))]
    public class ImageVisibilityStatus : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == "Total")
                return System.Windows.Visibility.Collapsed;
            return System.Windows.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(object), typeof(Visibility))]
    public class ManualCashEntryVisibility : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UndeclaredCollectionRecord)
                return (value as UndeclaredCollectionRecord).Zone.ToUpper().Contains("TOTAL")
                           ? Visibility.Collapsed
                           : Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(string), typeof(Brush))]
    public class GridrowBackgroundColor : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString().ToLower() == "total" || value.ToString().ToLower() == "defloat")
                return new SolidColorBrush(Colors.Blue);

            if (value.ToString().ToLower() == "undeclared")
                return new SolidColorBrush(Colors.Red);          

            return value.ToString().ToLower() == "full" ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(string), typeof(FontWeights))]
    public class TotalRowBoldProperty : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.ToString().ToLower() == "total") return FontWeights.UltraBold;
            return FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    [ValueConversion(typeof(int), typeof(Brush))]
    public class InstallationFloatForeColor : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value != null && (int)value == 1)
                    return new SolidColorBrush(Colors.Blue);
            }
            catch { }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 4;
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

    [ValueConversion(typeof(decimal), typeof(string))]
    public class CurrencyPriceConverter : IValueConverter
    {
        public bool ShowCurrencySymbol = true;
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal price = 0;
            if (value == null || (value.ToString() == string.Empty))
                return "NA";
            // Issue fix for ->set cultureinfo='en-US',  currencyculture='it-IT' DateCulture = 'it-IT'
            else
                if (ShowCurrencySymbol)
                {
                    if (value.GetType() == typeof(double) || value.GetType() == typeof(decimal) || value.GetType() == typeof(int))
                        price = System.Convert.ToDecimal(value);
                    else
                        decimal.TryParse(value.ToString(), NumberStyles.Any, new CultureInfo(Common.Utilities.ExtensionMethods.CurrentCurrenyCulture), out price);

                    return price.GetUniversalCurrencyFormatWithSymbol();
                }

            return value.ToString();
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

    [ValueConversion(typeof(decimal), typeof(string))]
    public class CurrencyPriceConverterWithSymbol : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal price = 0;
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
    public class CurrencyPriceConverterWithSymbolFormattedToZeroDecimal : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal price = 0;
            if (value != null &&
                value.ToString() != String.Empty &&
                decimal.TryParse(value.ToString(), out price))
            {

            }
            return price.GetUniversalCurrencyFormatWithSymbolFormattedToZeroDecimal();
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
    public class PriceConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal price = 0;
            var cul = new System.Globalization.CultureInfo(BMC.Common.Utilities.ExtensionMethods.CurrentSiteCulture);

            if (value != null && value.ToString().ToUpper() == "UNDECLARED")
                return value;

            if (value != null && value.ToString().ToUpper() == "NAN")
                return value;

            if (value != null &&
                value.ToString() != String.Empty &&
                decimal.TryParse(value.ToString(), NumberStyles.Currency, cul, out price))
            {
                price = System.Convert.ToDecimal(value, cul);
            }
            return price.GetUniversalCurrencyFormat();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string price = value.ToString();
            var cul = new System.Globalization.CultureInfo(BMC.Common.Utilities.ExtensionMethods.CurrentSiteCulture);

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
            var cul = new System.Globalization.CultureInfo(BMC.Common.Utilities.ExtensionMethods.CurrentSiteCulture);

            if (value != null && value.ToString().ToUpper() == "UNDECLARED")
                return value;

            if (value != null &&
                value.ToString() != String.Empty &&
                decimal.TryParse(value.ToString(), NumberStyles.Currency, cul, out price))
            {
                price = System.Convert.ToDecimal(value, cul);
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

    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateTimeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.ToString() != String.Empty)
                try
                {
                    var dt = DateTime.Now;
                    if (DateTime.TryParse(value.ToString(), out dt))
                        return dt == DateTime.MinValue ? "" : value.ToString().ReadDate().GetUniversalDateTimeFormat();
                }
                catch (Exception)
                {
                    return string.Empty;
                }

            //return DateTime.Now.GetUniversalDateTimeFormat();
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().ReadDate();
        }
    }

    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.ToString() != String.Empty)
                try
                {
                    var dt = DateTime.Now;
                    if (DateTime.TryParse(value.ToString(), out dt))
                        return dt == DateTime.MinValue ? "" : value.ToString().ReadDate().GetUniversalDateFormat();
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

    [ValueConversion(typeof(DateTime), typeof(string))]
    public class TimeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.ToString() != String.Empty)
                try
                {
                    var dt = DateTime.Now;
                    if (DateTime.TryParse(value.ToString(), out dt))
                        return dt == DateTime.MinValue ? "" : value.ToString().ReadDate().GetUniversalTimeFormat();
                }
                catch (Exception)
                {
                    return string.Empty;
                }

            return DateTime.Now.GetUniversalTimeFormat();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().ReadTimeWithSeconds();
        }
    }

    [ValueConversion(typeof(DateTime), typeof(string))]
    public class TimeConverterWithoutSeconds : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.ToString() != String.Empty)
                try
                {
                    var dt = DateTime.Now;
                    if (DateTime.TryParse(value.ToString(), out dt))
                        return dt == DateTime.MinValue ? "" : value.ToString().ReadDate().GetUniversalDateTimeFormatWithoutSeconds();
                }
                catch (Exception)
                {
                    return string.Empty;
                }

            return DateTime.Now.GetUniversalTimeFormat();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().ReadTimeWithSeconds();
        }
    }

    [ValueConversion(typeof(decimal), typeof(string))]
    public class ConvertDecimalToString : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return decimal.Parse(value.ToString()).GetUniversalCurrencyFormat();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(bool?), typeof(Visibility))]
    public class SlotMachineVisiblityConvertor : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    [ValueConversion(typeof(decimal), typeof(decimal))]
    public class DecimalValueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (System.Convert.ToDecimal(value) / 100).GetUniversalCurrencyFormatWithSymbol();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(string), typeof(string))]
    public class DeclerationTypeDisplay : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString().ToLower() == "total")
                return "";
            else if (value.ToString().ToLower() == "defloat")
                return Application.Current.FindResource("MessageID321") as string;
            else if (value.ToString().ToLower() == "part")
                return "Part";
            else
                return Application.Current.FindResource("MessageID322") as string;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    [ValueConversion(typeof(string), typeof(string))]
    public class MaskTicketNumber : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CashDeskManager.UserControls.CashDeskManagerAllDetails.lvViewAll.TicketValue") & (value.ToString().Trim().Length == 18))
                        return value.ToString().Trim().Substring(0, 13) + "****";
                    else
                        return value.ToString();
                }
            }
            return "";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class ReasonConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "NA";

            if (value.ToString() != string.Empty)
            {
                switch (value.ToString())
                {
                    case "1":
                        return "Duplicate Voucher";
                    case "2":
                        return "Voucher Not in System";
                    case "3":
                        return "Incorrect Position";
                    case "4":
                        return "Already assigned to collection";
                    case "5":
                        return "Active Voucher";
                    case "6":
                        return "Expired Voucher";
                    case "7":
                        return "Active Voucher (Exception)";
                    case "8":
                        return "Partially Paid (Exception)";
                    case "9":
                        return "Expired Voucher (Exception)";
                    default:
                        return "Invalid Voucher";
                }
            }

            return "NA";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class IntToStringConverter : IValueConverter
    {

        #region IntToString Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {

                int Count = 0;
                Count = System.Convert.ToInt32(value);
                return Count;
            }
            catch
            {
                return "0";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        #endregion
    }

    [ValueConversion(typeof(string), typeof(string))]
    public class AssetNumberSubStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            string result = value.ToString();

            try
            {
                if (result.Length > 8) result = result.Substring(0, 7) + "..";
            }
            catch { result = value.ToString(); }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }


    [ValueConversion(typeof(string), typeof(System.Windows.Visibility))]
    public class CurrencyVisibilityStatus : IValueConverter
    {
        static string[] sCurrencyList = ConfigurationManager.AppSettings[ExtensionMethods.CurrentSiteCulture].ToString().Split(',');
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            foreach (string temp in sCurrencyList)
            {
                if (temp.ToUpper().Trim().Equals((string)value))
                    return System.Windows.Visibility.Visible;
            }
            return System.Windows.Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    [ValueConversion(typeof(decimal), typeof(string))]
    public class HourlyCurrencyPriceConverter : IValueConverter
    {
        public bool ShowCurrencySymbol = true;
        public bool IsOccupancy = false;
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal price = 0;
            if (value == null || (value.ToString() == string.Empty))
                return "NA";
            // Issue fix for ->set cultureinfo='en-US',  currencyculture='it-IT' DateCulture = 'it-IT'
            else
                if (ShowCurrencySymbol)
                {
                    if (value.GetType() == typeof(double) || value.GetType() == typeof(decimal) || value.GetType() == typeof(int))
                        price = System.Convert.ToDecimal(value);
                    else
                        decimal.TryParse(value.ToString(), NumberStyles.Any, new CultureInfo(Common.Utilities.ExtensionMethods.CurrentCurrenyCulture), out price);

                    return price.GetUniversalCurrencyFormatWithSymbol();
                }

            if (IsOccupancy)
            {
                if (value is double)
                    return (Math.Round((double)value, 2) == 0 ? value.ToString() : ((double)value).ToString("#,##0.00", new CultureInfo(Common.Utilities.ExtensionMethods.CurrentCurrenyCulture)));
                else
                    return value.ToString();
            }
            else
                return value.ToString();
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

    [ValueConversion(typeof(string), typeof(Brush))]
    public class GridRowBackColor : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var bc = new BrushConverter();
            return (Brush)bc.ConvertFrom(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(int), typeof(Brush))]
    public class MultiTicketsColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((int)value)
            {
               
                case 0:
                    return new SolidColorBrush(Colors.Green);               
                case 1:
                    return new SolidColorBrush(Colors.Green);
                case -2:
                    return new SolidColorBrush(Colors.Red);
                case -16:
                    return new SolidColorBrush(Colors.Red);
                case -17:
                    return new SolidColorBrush(Colors.Red);
                case -14:
                    if (Settings.RedeemConfirm && Settings.RedeemExpiredTicket && Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket"))
                    {
                        return new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        return new SolidColorBrush(Colors.Red);
                    }
                case -15:
                    if (Settings.RedeemConfirm && Settings.RedeemExpiredTicket && Security.SecurityHelper.HasAccess("CashdeskOperator.Authorize.cs.MultipleRedeemExpiredTicket"))
                    {
                        return new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        return new SolidColorBrush(Colors.Red);
                    }
                
                default:
                    return new SolidColorBrush(Colors.Red);

            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

