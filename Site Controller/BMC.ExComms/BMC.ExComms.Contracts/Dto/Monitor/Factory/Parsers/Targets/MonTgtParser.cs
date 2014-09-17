using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    internal abstract class MonTgtParser
        : MonParser, IMonTgtParser
    {
        public MonTgtParserMappingG2HAttribute MappingAttributeG2H { get; set; }
        public MonTgtParserMappingH2GAttribute MappingAttributeH2G { get; set; }

        protected override IMonitorEntity CreateEntityInternal(IMonitorEntity parent, IFreeformEntity request)
        {
            return this.CreateMonitorTarget(parent, request as IFreeformEntity_MsgTgt);
        }

        protected abstract IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request);

        protected override IFreeformEntity CreateEntityInternal(IMonitorEntity parent, IMonitorEntity request)
        {
            return this.CreateFreeformTarget(parent, request as IMonitorEntity_MsgTgt);
        }

        //protected abstract IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity_MsgTgt request);

        protected virtual IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            return this.CreateFreeformTarget(parent, request);
        }
    }

    internal abstract class MonTgtParser_G2H
        : MonTgtParser
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            return null;
        }
    }

    internal abstract class MonTgtParser_H2G
        : MonTgtParser
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            return null;
        }
    }
}
