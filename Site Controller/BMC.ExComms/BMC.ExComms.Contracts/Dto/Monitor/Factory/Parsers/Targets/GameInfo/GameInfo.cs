using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
               typeof(FFTgt_G2H_GameInfo),
               (int)FaultSource.NonPriorityEvent,
               (int)FaultType_NonPriorityEvent.NewGameSelected,
               new int[]
                    {
                        (int)FF_AppId_SessionIds.Game,
                        (int)FF_AppId_TargetIds.GameInfo                      
                    })]
    internal class MonTgtParser_GameInfo_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GameInfo ffTgt = request as FFTgt_G2H_GameInfo;
            if (ffTgt != null)
            {

                MonTgt_G2H_GameInfo monTgt = new MonTgt_G2H_GameInfo()
                {
                    CurrentGameDenom = ffTgt.CurrentGameDenomination,
                    CurrentGameID = ffTgt.CurrentGameID,
                    CurrentGameName = ffTgt.CurrentGameName,
                    CurrentGamePayback = ffTgt.CurrentGamePayback,
                    GameProtocolVersion = ffTgt.GameProtocolVersion,
                    PaytableID = ffTgt.PaytableID

                };
                return monTgt;
            }
            return null;
        }
    }

}
