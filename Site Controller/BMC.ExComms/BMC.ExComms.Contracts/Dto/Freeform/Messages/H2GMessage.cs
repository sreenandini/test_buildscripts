using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFMsg_H2G : FreeformEntity_Msg
    {
        public FFMsg_H2G()
        {
            this.PollCode = FF_AppId_H2G_PollCodes.TransportACK;
        }

        public override FF_FlowDirection FlowDirection
        {
            get
            {
                return FF_FlowDirection.H2G;
            }
        }

        public FF_AppId_H2G_PollCodes PollCode { get; set; }
    }

    public class FFMsg_H2G_1 : FFMsg_H2G
    {
        public FFMsg_H2G_1()
        {
            this.SegmentNumber = 1;
            this.SegmentCount = 1;
        }

        public ushort SegmentNumber { get; set; }
        public ushort SegmentCount { get; set; }
    }

    public class FFMsg_H2G_2 : FFMsg_H2G_1 { }

    public class FFMsg_H2G_3 : FFMsg_H2G { }
}
