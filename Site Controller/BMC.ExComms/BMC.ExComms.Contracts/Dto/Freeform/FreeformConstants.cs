using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public static class FreeformConstants
    {
        public const int LEN_GMU_IPADDRESS = 4;
        public const int LEN_GMU_DEVICETYPE_START = LEN_GMU_IPADDRESS + 1;
        public const int LEN_GMU_HEADER = (LEN_GMU_IPADDRESS + 7);
        public const int LEN_GMU_MSG_MIN_LEN = (LEN_GMU_IPADDRESS + 7);

        public const int LEN_G2H_LEN = 11;
        public const int LEN_H2G_LEN = 10;

        public const int LEN_FREEFORM1 = (LEN_GMU_IPADDRESS + 10);
        public const int LEN_FREEFORM2 = LEN_FREEFORM1;
        public const int LEN_FREEFORM3 = (LEN_GMU_IPADDRESS + 7);

        public const int IDX_GMU_DEVICETYPE = 0;

        public const int IDX_RECV_MSG_TYPE = 1;
        public const int IDX_SEND_POLLCODE = 1;

        public const int IDX_RECV_COMMAND = 2;
        
        public const int IDX_RECV_SESSION_ID = 3;
        public const int IDX_SEND_SESSION_ID = 2;

        public const int IDX_RECV_TRANSACTION_ID = 4;
        public const int IDX_SEND_TRANSACTION_ID = 3;

        public const int PORTNO_RECEIVE = 11112;
        public const int PORTNO_SEND = 1031;

        public const int GMU_DEVICETYPE = 0x05;

        public const int MAX_CARD_LEN = 10;

        public const int FF_ISSECURED = 0x80;

        public static byte MakeSecured(FF_GmuId_TargetIds targetId)
        {
            return (byte)(FF_ISSECURED | (int)targetId);
        }

        public static byte MakeSecured(FF_AppId_TargetIds targetId)
        {
            return (byte)(FF_ISSECURED | (int)targetId);
        }

        public const int FF_ISRESPONSEREQUIRED = 0x80;

        public static byte MakeResponseRequired(FF_GmuId_TargetIds targetId)
        {
            return (byte)(FF_ISRESPONSEREQUIRED | (int)targetId);
        }

        public static byte MakeResponseRequired(FF_AppId_TargetIds targetId)
        {
            return (byte)(FF_ISRESPONSEREQUIRED | (int)targetId);
        }
    }
}
