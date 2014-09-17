using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region GMU Variable Action

    /// <summary>
    /// GMU to Host GMU Event
    /// </summary>
    public class FFTgt_B2B_GMUVarAction
        : FFTgt_B2B
    {
        public FFTgt_B2B_GMUVarAction_Data ActionData
        {
            get { return this.GetPrimaryTarget<FFTgt_B2B_GMUVarAction_Data>(); }
            set { this.SetPrimaryTarget<FFTgt_B2B_GMUVarAction_Data>(value); }
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.GMUVariableAction;
            }
        }
    }

    /// <summary>
    /// GMU to Host GMU Event Data Information
    /// </summary>
    public class FFTgt_B2B_GMUVarAction_Data 
        : FFTgt_B2B { }

    #endregion //GMU Variable Action

    #region Variable Action Status

    /// <summary>
    /// GMU To Host Freeform for Variable Action Cardless Play Timeout Status
    /// </summary>
    public class FFTgt_G2H_GMUVariableAction_Status
        : FFTgt_B2B_GMUVarAction_Data, IFFTgt_G2H
    {
        #region Private Data Member

        private FF_AppId_ResponseStatus_Types _status;

        #endregion // Private Data Member

        #region Properties

        // 0 -> Error, 1 -> Success
        public FF_AppId_ResponseStatus_Types Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (this._status == value) return;
                this._status = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Variable Action Status
}
