using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public abstract class FFTgt_B2B_GVA_TSSlotID_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TicketSystemSlotID;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Ticket System Slot ID Request
    /// </summary>
    public class FFTgt_G2H_GVA_TSSlotID_Request
        : FFTgt_B2B_GVA_TSSlotID_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Ticket System Slot ID Response
    /// </summary>
    public class FFTgt_H2G_GVA_TSSlotID_Response
        : FFTgt_B2B_GVA_TSSlotID_Base, IFFTgt_H2G
    {
        public int SlotID { get; set; }
    }

    /// <summary>
    /// GMU To Host Freeform for Ticket System Slot ID Status
    /// </summary>
    public class FFTgt_G2H_GVA_TSSlotID_Status
        : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TicketSystemSlotID;
            }
        }
    }

}
