using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Targets.GVA
{
    internal static class TicketParametersHelper
    {
        internal static FFMsg_G2H InitTicketDataRequest(string ipAddress)
        {
            return GVAHelper.WrapMessageAndReturn(ipAddress, FF_AppId_SessionIds.Tickets,
                    new FFTgt_G2H_GVA_TN_Request(),
                    new FFTgt_G2H_GVA_TSSlotID_Request(),
                    new FFTgt_G2H_GVA_TPD_Request(),
                    new FFTgt_G2H_GVA_TED_Request(),
                    new FFTgt_G2H_GVA_TK_Request(),
                    new FFTgt_G2H_GVA_TimeOfDay_Request(),
                    new FFTgt_G2H_GVA_RTE_Days_Request(),
                    new FFTgt_G2H_GVA_EnablePrint_RT_Request(),
                    new FFTgt_G2H_GVA_MOT_Allowed_Request(),
                    new FFTgt_G2H_GVA_OFFTKT_TxtLine1_Request(),
                    new FFTgt_G2H_GVA_OFFTKT_TxtLine2_Request()
                    );
        }
    }
}
