using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EBSComms.DataLayer.Dto
{
    public class DLMachineDto
    {
        public string MachineID { get; set; }

        public string Area { get; set; }

        public string GameName { get; set; }

        public string MachineLoc { get; set; }

        public string DenominationID { get; set; }

        public string ManufacturerName { get; set; }

        public string CasinoID { get; set; }

        public string MachineType { get; set; }

        public string ZoneID { get; set; }

        public bool IsActive { get; set; }

        public string Bank { get; set; }
    }

    public class DLMachineCollectionDto : List<DLMachineDto> { }
}
