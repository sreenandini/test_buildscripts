using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Tickets
{
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_Ticket_Void) }
        )]
    internal class FFTgtHandler_Ticket_Void_G2H_GMU
        : FFTgtHandler
    {        
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_Ticket_Void) }
        )]
    internal class FFTgtHandler_Ticket_Void_G2H_SIM
        : FFTgtHandler
    {

    }
}
