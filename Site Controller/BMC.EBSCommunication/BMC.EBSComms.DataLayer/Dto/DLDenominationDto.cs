using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EBSComms.DataLayer.Dto
{
    public class DLDenominationDto
    {
        public string DenominationId { get; set; }

        public string DenominationName { get; set; }

        public string DenominationValue { get; set; }

        public bool IsActive { get; set; }
    }

    public class DLDenominationCollectionDto : List<DLDenominationDto> { }
}
