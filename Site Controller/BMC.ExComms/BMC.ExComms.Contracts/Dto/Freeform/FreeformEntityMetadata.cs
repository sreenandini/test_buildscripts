using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FreeformEntityMetadataAttribute : Attribute
    {
        public FreeformEntityMetadataAttribute() { }
    }

    public class FFGmuIdAppIdMappingAttribute : Attribute
    {
        public FFGmuIdAppIdMappingAttribute(Type appIdType)
            : this(appIdType, MasterCOMVersions.MC300) { }

        public FFGmuIdAppIdMappingAttribute(Type appIdType, MasterCOMVersions version)
        {
            this.AppIdType = appIdType;
            this.Version = version;
        }

        public Type GmuIdType { get; internal set; }

        public Type AppIdType { get; private set; }

        public MasterCOMVersions Version { get; set; }
    }
}
