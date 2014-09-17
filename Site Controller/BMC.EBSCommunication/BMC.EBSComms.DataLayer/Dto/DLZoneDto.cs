using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EBSComms.DataLayer.Dto
{
    public class DLZoneDto
    {
        public int ZoneID { get; set; }

        public string ZoneName { get; set; }

        public bool IsActive { get; set; }
    }

    public class DLZoneCollectionDto : List<DLZoneDto> { }
}
