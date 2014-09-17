using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Roulette

    /// <summary>
    /// GMU to Host Freeform for Roulette
    /// </summary>
    public class FFTgt_G2H_Roulette
        : FFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.Roulette;
            }
        }
    }

    #endregion //Roulette
}
