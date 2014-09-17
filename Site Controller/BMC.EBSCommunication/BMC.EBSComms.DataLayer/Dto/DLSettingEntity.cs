using BMC.CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EBSComms.DataLayer.Dto
{
    public class DLSettingDto : DisposableObject
    {
        public DLSettingDto() { }

        public bool IsEnabled { get; set; }

        public bool SendDataToEBS { get; set; }

        public string EBSEndPointURL { get; set; }

        public string EBSVersion { get; set; }
    }
}
