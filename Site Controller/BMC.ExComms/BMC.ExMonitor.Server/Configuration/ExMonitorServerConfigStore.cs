using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.CoreLib;

namespace BMC.ExMonitor.Server.Configuration
{
    [Export("ExMonitorServerConfigStore", typeof(IExMonitorServerConfigStore))]
    internal class ExMonitorServerConfigStore2
        : ExMonitorServerConfigStore
    {
        public override string DoCustomAction(string subKey, string propertyName, string propertyValue)
        {
            string result = string.Empty;
            if (subKey.IgnoreCaseCompare("DEVSETTINGS"))
                result = ExCommsDataContext.Current.GetDevSettingValue(propertyName, propertyValue);
            else
                result = ExCommsDataContext.Current.GetSettingValue(propertyName, propertyValue);

            if (result.IsEmpty() &&
                !propertyValue.IsEmpty())
                result = propertyValue;
            return result;
        }
    }
}
