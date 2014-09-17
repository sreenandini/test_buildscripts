using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    public static class MonitorEntityMessageFactory
    {
        private static IDictionary<KeyValuePair<int, int>, Func<MonitorEntity_MsgTgt>> _monEntitymsg = null;

        public MonitorEntityMessageFactory()
        {
            _monEntitymsg = new Dictionary<KeyValuePair<int, int>, Func<MonitorEntity_MsgTgt>>() {
                { new KeyValuePair<int, int>(Convert.ToInt32(Fault_Source.GIM_Event), Convert.ToInt32(GIM_Fault_Type.Game_Id_Info_G2H)), () => new MonTgt_G2H_GameIDInfo()  },
                { new KeyValuePair<int, int>(Convert.ToInt32(Fault_Source.GIM_Event), Convert.ToInt32(GIM_Fault_Type.Aux_Network_Enable_Disable_H2G)), () => new MonTgt_H2G_AuxNetworkEnableDisable()  },
                { new KeyValuePair<int, int>(Convert.ToInt32(Fault_Source.GIM_Event), Convert.ToInt32(GIM_Fault_Type.Game_Id_Request_H2G)), () => new MonTgt_H2G_GameIDRequest()  },
            };
        }

        public static MonitorEntity_MsgTgt GetMessage(int faultType, int faultSource)
        {
            MonitorEntity_MsgTgt bmcMessage = null;
            KeyValuePair<int, int> pair = new KeyValuePair<int, int>(faultType, faultSource);
            if (_monEntitymsg.ContainsKey(pair))
            {
                bmcMessage = _monEntitymsg[pair]() as MonitorEntity_MsgTgt;
            }
            return bmcMessage;
        }
    }
}
