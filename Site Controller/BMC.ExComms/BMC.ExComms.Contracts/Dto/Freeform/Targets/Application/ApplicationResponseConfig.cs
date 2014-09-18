using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_B2B_AppResponseConfig
        : FFTgt_B2B, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.ApplicationResponseConfiguration;
            }
        }

        public FFTgt_B2B_AppResponseConfig_Data QualifierData { get; set; }
    }

    public class FFTgt_B2B_AppResponseConfig_Data
        : FFTgt_B2B { }

    public class FFTgt_B2B_AppResponseConfig_PlayerData
        : FFTgt_B2B_AppResponseConfig_Data, IFFTgt_G2H
    {
        public byte Bitmap { get; set; }

        public byte[] PlayerData { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_AppResponseConfig.PlayerData;
            }
        }
    }
}
