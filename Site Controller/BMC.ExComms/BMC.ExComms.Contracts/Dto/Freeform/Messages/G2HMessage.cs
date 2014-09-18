using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFMsg_G2H : FreeformEntity_Msg
    {
        internal FFMsg_G2H()
        {
            this.MessageType = FF_AppId_G2H_MessageTypes.FreeForm;
            this.Command = FF_AppId_G2H_Commands.GMUInitA0;
        }

        public override FF_FlowDirection FlowDirection
        {
            get
            {
                return FF_FlowDirection.G2H;
            }
        }

        public FF_AppId_G2H_MessageTypes MessageType { get; set; }
        public FF_AppId_G2H_Commands Command { get; set; }
    }

    public class FFMsg_G2H_1 : FFMsg_G2H
    {
        internal FFMsg_G2H_1()
        {
            this.SegmentNumber = 1;
            this.SegmentCount = 1;
        }

        public ushort SegmentNumber { get; set; }
        public ushort SegmentCount { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_Messages.Message1;
            }
        }
    }

    public class FFMsg_G2H_2 : FFMsg_G2H_1
    {
        internal FFMsg_G2H_2()
        {
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_Messages.Message2;
            }
        }
    }

    public class FFMsg_G2H_3 : FFMsg_G2H
    {
        internal FFMsg_G2H_3()
        {
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_Messages.Message3;
            }
        }
    }
}
