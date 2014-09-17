using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Targets.GVA
{
    internal static class GVAHelper
    {
        private static FFTgt_B2B_GMUVarAction[] WrapTargetsAndReturn(params FFTgt_B2B_GMUVarAction_Data[] datas)
        {
            List<FFTgt_B2B_GMUVarAction> result = new List<FFTgt_B2B_GMUVarAction>();
            foreach (var data in datas)
            {
                FFTgt_B2B_GMUVarAction target = new FFTgt_B2B_GMUVarAction();
                target.AddTarget(data);
                result.Add(target);
            }
            return result.ToArray();
        }

        internal static FFMsg_G2H WrapMessageAndReturn(string ipAddress, FF_AppId_SessionIds sessionId,
            params FFTgt_B2B_GMUVarAction_Data[] datas)
        {
            return WrapMessageAndReturn(ipAddress, FF_FlowInitiation.Any, sessionId, datas);
        }

        internal static FFMsg_G2H WrapMessageAndReturn(string ipAddress, FF_FlowInitiation flowInitiation,
            FF_AppId_SessionIds sessionId, params FFTgt_B2B_GMUVarAction_Data[] datas)
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateG2HMessage(ipAddress, FF_AppId_G2H_Commands.ResponseRequest,
                FF_AppId_G2H_MessageTypes.FreeForm, sessionId,
                WrapTargetsAndReturn(datas));
            msg.FlowInitiation = flowInitiation;
            return msg;
        }
    }
}
