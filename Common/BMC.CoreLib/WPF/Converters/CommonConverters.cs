// -----------------------------------------------------------------------
// <copyright file="DateConverter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.WPF.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using System.Windows;

    #region DateTimeConverter
#if !SILVERLIGHT
    [ValueConversion(typeof(DateTime), typeof(string))]
#endif
    public class DateTimeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime dt = TypeSystem.GetValueDateTime(value);
            if (dt == DateTime.MinValue) return string.Empty;
            else return dt.GetUTCDateString(culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return TypeSystem.GetValueDateTime(value);
        }

        #endregion
    } 
    #endregion

    #region VisibilityConverter
#if !SILVERLIGHT
    [ValueConversion(typeof(bool), typeof(Visibility))]
#endif
    public class VisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return TypeSystem.GetValueBool(value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (((Visibility)value) == Visibility.Visible);
        }

        #endregion
    } 
    #endregion

    #region Visibility2Converter
#if !SILVERLIGHT
    [ValueConversion(typeof(bool), typeof(Visibility))]
#endif
    public class Visibility2Converter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return TypeSystem.GetValueBool(value) == false ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (((Visibility)value) == Visibility.Collapsed);
        }

        #endregion
    }
    #endregion
}
