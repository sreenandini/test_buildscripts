using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.LongPoll
{
    internal static class LongPollHelper
    {
        private const string DYN_MODULE_NAME = "LongPollHelper";

        private static FFMsg_G2H WrapMessageAndReturn(string ipAddress, FFTgt_B2B_GameMessage data)
        {
            return WrapMessageAndReturn(ipAddress, FF_FlowInitiation.Any, data);
        }

        private static FFMsg_G2H WrapMessageAndReturn(string ipAddress, FF_FlowInitiation flowInitiation, FFTgt_B2B_GameMessage data)
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateG2HMessage(ipAddress, FF_AppId_G2H_Commands.ResponseRequest,
                FF_AppId_G2H_MessageTypes.FreeForm, FF_AppId_SessionIds.GameMessage, false,
                data);
            msg.FlowInitiation = flowInitiation;
            return msg;
        }

        private static FFMsg_H2G WrapMessageAndReturnH2G(string ipAddress, FF_AppId_H2G_PollCodes pollCode, FFTgt_B2B_GameMessage data)
        {
            return WrapMessageAndReturnH2G(ipAddress, pollCode, FF_FlowInitiation.Any, data);
        }

        private static FFMsg_H2G WrapMessageAndReturnH2G(string ipAddress, FF_AppId_H2G_PollCodes pollCode, FF_FlowInitiation flowInitiation, FFTgt_B2B_GameMessage data)
        {
            FFMsg_H2G msg = FreeformEntityFactory.CreateH2GMessage(ipAddress, pollCode,
                FF_AppId_SessionIds.GameMessage, false,
                data);
            msg.FlowInitiation = flowInitiation;
            return msg;
        }

        internal static FFMsg_H2G EnableDisableRequest(string ipAddress, bool enable)
        {
            return WrapMessageAndReturnH2G(ipAddress, FF_AppId_H2G_PollCodes.FreeformNoResponse, FF_FlowInitiation.Gmu, new FFTgt_H2G_GM_SAS_EnableDisable()
            {
                EnableDisable = enable,
            });
        }

        internal static FFMsg_G2H EnableDisableResponse(string ipAddress, bool enable)
        {
            return WrapMessageAndReturn(ipAddress, FF_FlowInitiation.Gmu, new FFTgt_G2H_GameMessage_SASCommand()
            {
                LongPollCommand = (int)(enable ? FF_AppId_LongPollCodes.LPC_MachineEnable : FF_AppId_LongPollCodes.LPC_MachineDisable),
            });
        }

        internal static FFMsg_H2G TotalGamesRequest(string ipAddress)
        {
            return WrapMessageAndReturnH2G(ipAddress, FF_AppId_H2G_PollCodes.FreeformNoResponse, FF_FlowInitiation.Gmu, new FFTgt_H2G_GM_SAS_TotalGames());
        }

        internal static FFMsg_G2H TotalGamesResponse(string ipAddress, short totalGames)
        {
            return WrapMessageAndReturn(ipAddress, FF_FlowInitiation.Gmu, new FFTgt_G2H_GM_SAS_TotalGames()
            {
                TotalGames = totalGames,
            });
        }
    }
}
