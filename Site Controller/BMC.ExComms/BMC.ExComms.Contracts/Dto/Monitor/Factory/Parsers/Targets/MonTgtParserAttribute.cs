using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.Security;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public abstract class MonTgtParserMappingAttribute : Attribute
    {
        protected MonTgtParserMappingAttribute(FF_FlowDirection direction,
                                                Type freeformTargetType)
            : this(direction, freeformTargetType, null, -1, -1)
        {
        }

        protected MonTgtParserMappingAttribute(FF_FlowDirection direction,
                                                Type freeformTargetType,
                                                Type monitorTargetType,
                                                int faultSource,
                                                int faultType)
        {
            this.FreeformTargetType = freeformTargetType;
            this.MonitorTargetType = monitorTargetType;
            this.FaultSource = faultSource;
            this.FaultType = faultType;
            this.Direction = direction;
            this.IsPrimary = (faultSource >= -1 && faultType >= -1);
        }

        public Type FreeformTargetType { get; private set; }

        public Type MonitorTargetType { get; private set; }

        public int FaultSource { get; private set; }

        public int FaultType { get; private set; }

        public FF_FlowDirection Direction { get; private set; }

        public bool IsPrimary { get; private set; }

        public string FaultSourceTypeKey
        {
            get { return string.Format("{0:D}_{1:D}", this.FaultSource, this.FaultType); }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MonTgtParserMappingG2HAttribute
        : MonTgtParserMappingAttribute
    {
        public MonTgtParserMappingG2HAttribute(FF_FlowDirection direction,
                                                Type freeformTargetType,
                                                params int[] parendIds)
            : this(direction, freeformTargetType, -1, -1, parendIds)
        {
        }

        public MonTgtParserMappingG2HAttribute(FF_FlowDirection direction,
                                                Type freeformTargetType,
                                                int faultSource,
                                                int faultType,
                                                params int[] parendIds)
            : this(direction, freeformTargetType, null, faultSource, faultType, parendIds) { }

        public MonTgtParserMappingG2HAttribute(FF_FlowDirection direction,
                                                Type freeformTargetType,
                                                Type monitorTargetType,
                                                int faultSource,
                                                int faultType,
                                                params int[] parendIds)
            : base(direction, freeformTargetType, monitorTargetType, faultSource, faultType)
        {
            if (parendIds != null)
            {
                this.ParentIds = parendIds;
                StringBuilder sb = new StringBuilder();
                foreach (var parendId in parendIds)
                {
                    if (sb.Length > 0) sb.Append("_");
                    sb.Append(parendId);
                }
                this.ChildKey = sb.ToString();
            }
        }

        public int[] ParentIds { get; set; }

        public string ChildKey { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MonTgtParserMappingH2GAttribute
        : MonTgtParserMappingAttribute
    {
        public MonTgtParserMappingH2GAttribute(FF_FlowDirection direction,
                                                Type monitorTargetType,
                                                int faultSource,
                                                int faultType,
                                                FF_AppId_H2G_PollCodes pollCode,
                                                FF_AppId_SessionIds sessionID)
            : this(direction, null, monitorTargetType, faultSource, faultType, pollCode, sessionID) { }

        public MonTgtParserMappingH2GAttribute(FF_FlowDirection direction,
                                                Type freeformTargetType,
                                                Type monitorTargetType,
                                                int faultSource,
                                                int faultType,
                                                FF_AppId_H2G_PollCodes pollCode,
                                                FF_AppId_SessionIds sessionID)
            : base(direction, freeformTargetType, monitorTargetType, faultSource, faultType)
        {
            this.PollCode = pollCode;
            this.SessionID = sessionID;
        }

        public FF_AppId_H2G_PollCodes PollCode { get; private set; }
        public FF_AppId_SessionIds SessionID { get; private set; }
    }
}
