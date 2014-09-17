using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_GM_SAS_LongPollCommand
        : FFTgt_G2H_GameMessage_SASCommand
    {
        public FFTgt_G2H_GM_SAS_LongPollCommand(int command)
        {
            this.LongPollCommand = command;
        }
    }

    public class FFTgt_H2G_GM_SAS_LongPollCommand
        : FFTgt_H2G_GameMessage_SASCommand
    {
        public FFTgt_H2G_GM_SAS_LongPollCommand(int command)
        {
            this.LongPollCommand = command;
        }
    }

    public class FFTgt_H2G_GM_SAS_CurrentCredits
        : FFTgt_H2G_GM_SAS_LongPollCommand
    {
        public FFTgt_H2G_GM_SAS_CurrentCredits()
            : base(0x1A) { }
    }

    public class FFTgt_H2G_GM_SAS_HandpayInfo
        : FFTgt_H2G_GM_SAS_LongPollCommand
    {
        public FFTgt_H2G_GM_SAS_HandpayInfo()
            : base(0x1B) { }
    }

    public class FFTgt_H2G_GM_SAS_GameMachineInfo
        : FFTgt_H2G_GM_SAS_LongPollCommand
    {
        public FFTgt_H2G_GM_SAS_GameMachineInfo()
            : base(0x1F) { }
    }

    public class FFTgt_H2G_GM_SAS_TotalGames
        : FFTgt_H2G_GM_SAS_LongPollCommand
    {
        public FFTgt_H2G_GM_SAS_TotalGames()
            : base(0x51) { }
    }

    public class FFTgt_G2H_GM_SAS_TotalGames
        : FFTgt_G2H_GM_SAS_LongPollCommand
    {
        public FFTgt_G2H_GM_SAS_TotalGames()
            : base(0x51) { }

        public short TotalGames { get; set; }

        public override void FromRawData(byte[] rawData)
        {
            this.TotalGames = rawData.GetBytesToBCDInt16(0, 2);
        }

        public override byte[] ToRawData()
        {
            return this.TotalGames.GetBCDToBytes(2);
        }
    }

    public class FFTgt_H2G_GM_SAS_GetGameInfo
        : FFTgt_H2G_GM_SAS_LongPollCommand
    {
        public FFTgt_H2G_GM_SAS_GetGameInfo()
            : base(0x53) { }
    }

    public class FFTgt_H2G_GM_SAS_GetExtendedGameInfo
        : FFTgt_H2G_GM_SAS_LongPollCommand
    {
        public FFTgt_H2G_GM_SAS_GetExtendedGameInfo()
            : base(0xB5) { }
    }
}
