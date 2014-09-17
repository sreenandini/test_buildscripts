using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Freeform for Game Information
    /// </summary>
    public class FFTgt_B2B_Encrypted
        : FFTgt_B2B, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return FreeformConstants.FF_ISSECURED | (int)FF_AppId_TargetIds.Security;
            }
        }

        public byte[] DecryptedData { get; set; }
    }
}
