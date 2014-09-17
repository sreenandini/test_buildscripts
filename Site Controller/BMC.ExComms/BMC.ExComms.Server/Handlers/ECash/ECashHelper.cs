using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.ECash
{
    internal static class ECashHelper
    {
        private static FFTgt_B2B_EFT WrapTargetAndReturnSecured(FFTgt_B2B_EFT_Data data)
        {
            return new FFTgt_B2B_EFT()
            {
                EFTData = data,
                IsSecured = true,
            };
        }

        private static FFTgt_B2B_EFT WrapTargetAndReturn(FFTgt_B2B_EFT_Data data)
        {
            return new FFTgt_B2B_EFT()
            {
                EFTData = data,
                IsSecured = false,
            };
        }

        private static FFMsg_G2H WrapMessageAndReturnG2H(string ipAddress, bool isSecured, FFTgt_B2B_EFT_Data data)
        {
            return WrapMessageAndReturnG2H(ipAddress, FF_FlowInitiation.Any, isSecured, data);
        }

        private static FFMsg_G2H WrapMessageAndReturnG2H(string ipAddress, FF_FlowInitiation flowInitiation, bool isSecured, FFTgt_B2B_EFT_Data data)
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateG2HMessage(ipAddress, FF_AppId_G2H_Commands.ResponseRequest,
                FF_AppId_G2H_MessageTypes.FreeForm, FF_AppId_SessionIds.ECash,
                (isSecured ? WrapTargetAndReturnSecured(data) : WrapTargetAndReturn(data)));
            msg.FlowInitiation = flowInitiation;
            return msg;
        }

        private static FFMsg_H2G WrapMessageAndReturnH2G(string ipAddress, FF_AppId_H2G_PollCodes pollCode, bool isSecured, FFTgt_B2B_EFT_Data data)
        {
            return WrapMessageAndReturnH2G(ipAddress, pollCode, FF_FlowInitiation.Any, isSecured, data);
        }

        private static FFMsg_H2G WrapMessageAndReturnH2G(string ipAddress, FF_AppId_H2G_PollCodes pollCode,
            FF_FlowInitiation flowInitiation, bool isSecured, FFTgt_B2B_EFT_Data data)
        {
            FFTgt_B2B_EFT eft = WrapTargetAndReturn(data);
            FFMsg_H2G msg = FreeformEntityFactory.CreateH2GMessage(ipAddress, pollCode,
                FF_AppId_SessionIds.ECash, true,
                (isSecured ? WrapTargetAndReturnSecured(data) : WrapTargetAndReturn(data)));
            msg.FlowInitiation = flowInitiation;
            return msg;
        }

        internal static FFMsg_G2H BalanceRequest(string ipAddress, string cardNumber, string pin)
        {
            return WrapMessageAndReturnG2H(ipAddress, FF_FlowInitiation.Gmu, true, new FFTgt_G2H_EFT_BalanceRequest()
            {
                PlayerCardNumber = cardNumber,
                Pin = pin,
            });
        }

        internal static FFMsg_H2G SystemDisableRequest(string ipAddress)
        {
            return WrapMessageAndReturnH2G(ipAddress, FF_AppId_H2G_PollCodes.FreeformNoResponse, false, new FFTgt_H2G_EFT_SystemDisable());
        }

        internal static FFMsg_H2G SystemEnableRequest(string ipAddress)
        {
            return WrapMessageAndReturnH2G(ipAddress, FF_AppId_H2G_PollCodes.FreeformNoResponse, false, new FFTgt_H2G_EFT_SystemEnable());
        }

        internal static FFMsg_G2H SystemDisableResponse(string ipAddress, FF_AppId_EFT_ResponseTypes responseType)
        {
            return WrapMessageAndReturnG2H(ipAddress, false, new FFTgt_G2H_EFT_SystemDisable()
            {
                ResponseType = responseType,
            });
        }

        internal static FFMsg_G2H SystemEnableResponse(string ipAddress, FF_AppId_EFT_ResponseTypes responseType)
        {
            return WrapMessageAndReturnG2H(ipAddress, false, new FFTgt_G2H_EFT_SystemEnable()
            {
                ResponseType = responseType,
            });
        }
    }
}
