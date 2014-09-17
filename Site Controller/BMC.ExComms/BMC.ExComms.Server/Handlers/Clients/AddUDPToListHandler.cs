using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.Security;
using BMC.ExComms.Server.Handlers.Targets.GVA;

namespace BMC.ExComms.Server.Handlers.Client
{
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Internal,
        Request = new Type[] { typeof(FFTgt_H2G_Client_AddUDPToList) }
        )]
    internal sealed class FFTgtHandler_Client_AddUDPToList_H2G_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            FFTgt_H2G_Client_AddUDPToList tgtSrc = target as FFTgt_H2G_Client_AddUDPToList;
            return ExCommsExecutorFactory.AddInstallation(tgtSrc.InstallationNo);
        }
    }
}
