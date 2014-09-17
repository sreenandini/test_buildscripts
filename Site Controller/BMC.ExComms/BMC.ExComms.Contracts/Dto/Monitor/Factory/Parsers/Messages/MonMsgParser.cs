using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    internal abstract class MonMsgParser
        : MonParser, IMonMsgParser
    {
        protected override IMonitorEntity CreateEntityInternal(IMonitorEntity parent, IFreeformEntity request)
        {
            IFreeformEntity_Msg msgFF = request as IFreeformEntity_Msg;
            IMonitorEntity_Msg msgMon = this.CreateMonitorMessage();
            msgMon.IpAddress = msgFF.IpAddress;
            return msgMon;
        }

        protected abstract IMonitorEntity_Msg CreateMonitorMessage();

        protected override IFreeformEntity CreateEntityInternal(IMonitorEntity parent, IMonitorEntity request)
        {
            IMonitorEntity_Msg monMsg = request as IMonitorEntity_Msg;
            IFreeformEntity_Msg ffMsg = this.CreateFreeformMessage();
            ffMsg.IpAddress = monMsg.IpAddress;
            return ffMsg;
        }

        protected abstract IFreeformEntity_Msg CreateFreeformMessage();
    }

    internal sealed class MonMsgParser_G2H
        : MonMsgParser
    {
        protected override IMonitorEntity_Msg CreateMonitorMessage()
        {
            return new MonMsg_G2H()
            {
                FaultDate = DateTime.Now,
            };
        }

        protected override IFreeformEntity_Msg CreateFreeformMessage()
        {
            return null;
        }
    }

    internal sealed class MonMsgParser_H2G
        : MonMsgParser
    {
        protected override IMonitorEntity_Msg CreateMonitorMessage()
        {
            return null;
        }

        protected override IFreeformEntity_Msg CreateFreeformMessage()
        {
            return null;
        }
    }
}
