using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EBSComms.DataLayer.Dto
{
    public class DLManufacturerDto
    {
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public string ManufacturerValue { get; set; }

        public bool IsActive { get; set; }
    }

    public class DLManufacturerCollectionDto : List<DLManufacturerDto> { }
}
