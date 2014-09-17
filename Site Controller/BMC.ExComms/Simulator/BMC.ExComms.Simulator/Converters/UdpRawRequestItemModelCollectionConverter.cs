using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Data;
using BMC.ExComms.Simulator.Models;

namespace BMC.ExComms.Simulator.Converters
{
    public class UdpRawRequestItemModelCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ListCollectionView nw = value as ListCollectionView;
            return nw.SourceCollection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
