using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Comparers;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.GMUEvent
{
    internal static class GMUStdEventHelper
    {
        internal static IDictionary<Type, FF_AppId_GMUEvent_DataSetIds> _mappings =
            new SortedDictionary<Type, FF_AppId_GMUEvent_DataSetIds>(new TypeComparer())
            {
                { typeof(FFTgt_G2H_GMUEvent_StdData), FF_AppId_GMUEvent_DataSetIds.Standard },
                { typeof(FFTgt_G2H_GMUEvent_TicketData), FF_AppId_GMUEvent_DataSetIds.Ticket },
                { typeof(FFTgt_G2H_GMUEvent_EFTData), FF_AppId_GMUEvent_DataSetIds.EFT },
                { typeof(FFTgt_G2H_GMUEvent_CouponData), FF_AppId_GMUEvent_DataSetIds.Coupon },
                { typeof(FFTgt_G2H_GMUEvent_NoteAcceptorData), FF_AppId_GMUEvent_DataSetIds.NoteAcceptor },
                { typeof(FFTgt_G2H_GMUEvent_PrinterData), FF_AppId_GMUEvent_DataSetIds.Printer },
                //{ typeof(FFTgt_G2H_GMUEvent_StdData), FF_AppId_GMUEvent_DataSetIds.TaggedStatus },
            };

        internal static FFTgt_G2H_GMUEvent_StdData Create(FF_AppId_GMUEvent_XCodes exceptionCode)
        {
            FFTgt_G2H_GMUEvent_StdData data = new FFTgt_G2H_GMUEvent_StdData()
            {
                ExceptionCode = exceptionCode,
                EmployeeCardID = "00",
                JackpotID = FF_AppId_GMUEvent_JackpotIDs.NoJackpot,                
            };
            return data;
        }

        private static FFTgt_G2H_GMUEvent WrapTargetAndReturn(FFTgt_B2B_GMUEventData data)
        {
            return new FFTgt_G2H_GMUEvent()
            {
                EventData = data,
            };
        }

        private static FFMsg_G2H WrapMessageAndReturn(string ipAddress, FFTgt_B2B_GMUEventData data)
        {
            return FreeformEntityFactory.CreateG2HMessage(ipAddress, FF_AppId_G2H_Commands.GMUInitA0,
                FF_AppId_G2H_MessageTypes.FreeForm, FF_AppId_SessionIds.A1,
                WrapTargetAndReturn(data));
        }

        internal static FFMsg_G2H PlayerCardIn(string ipAddress, string cardNumber)
        {
            FFTgt_G2H_GMUEvent_StdData data = Create(FF_AppId_GMUEvent_XCodes.PlayerCardInInfoXC);
            data.PlayerCard = cardNumber.GetMaxCharacters(FreeformConstants.MAX_CARD_LEN);
            return WrapMessageAndReturn(ipAddress, data);
        }

        internal static FFMsg_G2H PlayerCardOut(string ipAddress, string cardNumber)
        {
            FFTgt_G2H_GMUEvent_StdData data = Create(FF_AppId_GMUEvent_XCodes.PlayerCardOutXC);
            data.PlayerCard = cardNumber.GetMaxCharacters(FreeformConstants.MAX_CARD_LEN);
            return WrapMessageAndReturn(ipAddress, data);
        }

        internal static FFMsg_G2H EmployeeCardIn(string ipAddress, string cardNumber)
        {
            FFTgt_G2H_GMUEvent_StdData data = Create(FF_AppId_GMUEvent_XCodes.EmpCardInXC);
            data.PlayerCard = cardNumber.GetMaxCharacters(FreeformConstants.MAX_CARD_LEN);
            return WrapMessageAndReturn(ipAddress, data);
        }

        internal static FFMsg_G2H EmployeeCardOut(string ipAddress, string cardNumber)
        {
            FFTgt_G2H_GMUEvent_StdData data = Create(FF_AppId_GMUEvent_XCodes.EmpCardOutXC);
            data.PlayerCard = cardNumber.GetMaxCharacters(FreeformConstants.MAX_CARD_LEN);
            return WrapMessageAndReturn(ipAddress, data);
        }

        internal static FFMsg_G2H PostStandardEvent(string ipAddress, FF_AppId_GMUEvent_XCodes code)
        {
            FFTgt_G2H_GMUEvent_StdData data = Create(code);
            return WrapMessageAndReturn(ipAddress, data);
        }
    }
}
