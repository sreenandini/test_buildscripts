using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_B2B_GameMessage
        : FFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.GameMessage;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Message Response NACK and ACK
    /// </summary>
    public class FFTgt_G2H_GameMessage_ResponseAckNack
        : FFTgt_B2B_GameMessage
    {
        public FF_AppId_GameMessage_ResponseTypes Status { get; set; }
    }

    /// <summary>
    /// Host to GMU (Or) GMU to Host Freeform for  Message
    /// </summary>
    public class FFTgt_B2B_GameMessage2
        : FFTgt_B2B_GameMessage
    {
        public FF_AppId_GameMessage_ProtocolTypes ProtocolType { get; set; }
        public FF_AppId_GameMessage_GameResponses IsGameResponseExpected { get; set; }
        public byte[] MessageData { get; set; }
    }

    /// <summary>
    /// Host to GMU Freeform for  Message Request
    /// </summary>
    public class FFTgt_H2G_GameMessage_MessageRequest
        : FFTgt_B2B_GameMessage2, IFFTgt_H2G { }

    public class FFTgt_H2G_GameMessage_SASCommand
        : FFTgt_H2G_GameMessage_MessageRequest, IFFTgt_H2G
    {
        public FFTgt_H2G_GameMessage_SASCommand()
        {
            this.ProtocolType = FF_AppId_GameMessage_ProtocolTypes.SAS;
            this.IsGameResponseExpected = FF_AppId_GameMessage_GameResponses.Return;
        }

        public int LongPollCommand { get; set; }
    }

    /// <summary>
    /// GMU to Host Freeform for Message Response Timeout
    /// </summary>
    public class FFTgt_G2H_GameMessage_MessageResponseTimeout
        : FFTgt_B2B_GameMessage2, IFFTgt_G2H { }

    /// <summary>
    /// GMU to Host Freeform for Message Not Sent
    /// </summary>
    public class FFTgt_G2H_GameMessage_MessageNotSent
        : FFTgt_B2B_GameMessage2, IFFTgt_G2H { }

    /// <summary>
    ///  GMU to Host Freeform for Message Response
    /// </summary>
    public class FFTgt_G2H_GameMessage_MessageResponse
        : FFTgt_B2B_GameMessage2, IFFTgt_G2H { }

    public class FFTgt_G2H_GameMessage_SASCommand
        : FFTgt_G2H_GameMessage_MessageResponse
    {
        public FFTgt_G2H_GameMessage_SASCommand()
        {
            this.ProtocolType = FF_AppId_GameMessage_ProtocolTypes.SAS;
            this.IsGameResponseExpected = FF_AppId_GameMessage_GameResponses.Return;
        }

        public int LongPollCommand { get; set; }
    }
}
