using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class PurgeCategory
    {
        public string PCCategoryName { get; set; }

        public int PCTypeID { get; set; }

        public bool PCIsActive { get; set; }

        public int PurgeTableID { get; set; }
    }

    public class PurgeTableInfo
    {
        public string PCtablename { get; set; }

        public string PcCategoryName { get; set; }

        public DateTime LastPurgedDateTime { get; set; }

        public bool PurgeStatus { get; set; }

        public int PurgeInterval { get; set; }
    }

    public class PurgeTables
    {
        public int PurgeTableID { get; set; }

        public string Purgetablename { get; set; }

        public string TableDisplayName { get; set; }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    