using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EBSComms.DataLayer.Dto
{
   public class DLSiteDto
    {
        public string SiteId { get; set; }

        public string SiteName { get; set; }

        public bool IsActive { get; set; }
    }

   public class DLSiteCollectionDto : List<DLSiteDto> { }
}
