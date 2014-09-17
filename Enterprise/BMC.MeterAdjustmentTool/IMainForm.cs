using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.MeterAdjustmentTool.Helpers;

namespace BMC.MeterAdjustmentTool
{
    public interface IMainForm
    {
        rsp_GetSiteDetailsResult SelectedSite { get; }

        BmcConnectionStringBuilder ConnectionStringBuilder { get; }

        string ConnectionString { get; }
    }
}
