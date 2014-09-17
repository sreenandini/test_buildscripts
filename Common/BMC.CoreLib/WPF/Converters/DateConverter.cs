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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
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
}
