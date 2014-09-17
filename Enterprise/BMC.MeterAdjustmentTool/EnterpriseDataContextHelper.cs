using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using BMC.Common.Utilities;

namespace BMC.MeterAdjustmentTool
{
    public class EnterpriseDataContextHelper : EnterpriseDataContext
    {
        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        public EnterpriseDataContextHelper()
            : base(DatabaseHelper.GetConnectionString(), mappingSource) { }
    }
}
