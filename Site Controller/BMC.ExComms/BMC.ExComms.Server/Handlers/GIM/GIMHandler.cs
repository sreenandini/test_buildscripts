using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.PID;
using BMC.ExComms.Server.Handlers.Security;
using BMC.ExComms.Server.Handlers.Targets.GVA;

namespace BMC.ExComms.Server.Handlers.GIM
{
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.GIM,
        Request = new Type[] { typeof(FFTgt_G2H_GIM_GameIDInfo) },
        Response = new Type[] { typeof(FFTgt_H2G_GIM_GameIDInfo) }
        )]
    internal sealed class FFTgtHandler_GIM_G2H_GMU
        : FFTgtHandler
    {

    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.GIM,
        Request = new Type[] { typeof(FFTgt_G2H_GIM_GameIDInfo) },
        Response = new Type[] { typeof(FFTgt_H2G_GIM_GameIDInfo) }
        )]
    internal sealed class FFTgtHandler_GIM_G2H_SIM
        : FFTgtHandler
    {
    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.GIM,
        Request = new Type[] { typeof(FFTgt_H2G_GIM_GameIDInfo) }
        )]
    internal sealed class FFTgtHandler_GIM_H2G_GMU
        : FFTgtHandler
    {
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.GIM,
        Request = new Type[] { typeof(FFTgt_H2G_GIM_GameIDInfo) }
        )]
    internal sealed class FFTgtHandler_GIM_H2G_GIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            string ipAddress = context.SourceMessage.IpAddress;

            // rsa enabled
            var msgRSAQuerySystem = PIDHelper.RSAQuerySystemRequest(ipAddress, false);

            // initiate a key exchange (ticket)
            var msgTicket = this.MessageHandlerFactory.SecurityTables.InitKeyExchangeStartG2H_SIM(ipAddress, FF_AppId_SessionIds.Security, this.NewTransactionId);

            // initiate a key exchange (eft)
            var msgEFT = this.MessageHandlerFactory.SecurityTables.InitKeyExchangeStartG2H_SIM(ipAddress, FF_AppId_SessionIds.ECash, this.NewTransactionId);

            // ticket parameter request
            var msgTicketParameters = TicketParametersHelper.InitTicketDataRequest(ipAddress);

            context.FreeformTargets.AddRange(new IFreeformEntity[] { 
                msgRSAQuerySystem,
                msgTicket, 
                msgEFT,
                msgTicketParameters
            });
            return true;
        }
    }
}
