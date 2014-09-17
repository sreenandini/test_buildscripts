using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host GMU Event
    /// </summary>
    public class FFTgt_G2H_GMUEvent
        : FFTgt_G2H
    {
        public FFTgt_B2B_GMUEventData EventData
        {
            get { return this.GetPrimaryTarget<FFTgt_B2B_GMUEventData>(); }
            set { this.SetPrimaryTarget<FFTgt_B2B_GMUEventData>(value); }
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.StatusBlock;
            }
        }
    }

    /// <summary>
    /// GMU to Host GMU Event Data Information
    /// </summary>
    public class FFTgt_B2B_GMUEventData 
        : FFTgt_B2B { }

    /// <summary>
    /// GMU to Host GMU Event Data Information
    /// </summary>
    public class FFTgt_B2B_GMUEventData_Primary
        : FFTgt_B2B_GMUEventData, IFreeformEntity_MsgTgt_Primary { }
}
