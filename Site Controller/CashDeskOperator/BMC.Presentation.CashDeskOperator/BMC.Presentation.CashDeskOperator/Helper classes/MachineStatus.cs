using System;
using BMC.Common.Utilities;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using BMC.CashDeskOperator;
using BMC.Transport;

namespace BMC.Presentation.POS.Helper_classes
{
    [System.Windows.Data.ValueConversion(typeof(int), typeof(string))]
    class MachineStatus
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string resultval = (int)value == 1 ? "Machine Enabled" : "Machine Disabled";
                return resultval;
            }
            catch (System.Exception exp)
            {
                return null;
            }
        }
         public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
         {
            string resultval = (int)value == 0 ? "" : "";
            return resultval;
         }
    }
       [System.Windows.Data.ValueConversion(typeof(int), typeof(string))]
       class StatusColor : System.Windows.Data.IValueConverter
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
            return (int)value == 1 ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Firebrick);
        }
    }
}
