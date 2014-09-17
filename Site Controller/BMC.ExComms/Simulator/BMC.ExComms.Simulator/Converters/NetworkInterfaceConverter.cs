using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Data;

namespace BMC.ExComms.Simulator.Converters
{
    public class NetworkInterfaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            NetworkInterface nw = value as NetworkInterface;
            return string.Format("{0} ({1})", nw.Name, nw.Description);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
