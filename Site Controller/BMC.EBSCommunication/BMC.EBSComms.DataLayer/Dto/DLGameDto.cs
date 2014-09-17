using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EBSComms.DataLayer.Dto
{
    public class DLGameDto
    {
        public string GameID { get; set; }

        public string GameName { get; set; }

        public bool IsActive { get; set; }
    }

    public class DLGameCollectionDto : List<DLGameDto> { }
}
