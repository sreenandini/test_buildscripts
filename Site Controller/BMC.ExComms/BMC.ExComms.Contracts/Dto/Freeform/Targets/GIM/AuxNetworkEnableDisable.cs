using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{

    /// <summary>
    /// Host to GMU GIM for Aux Network Enable/Disable 
    /// </summary>
    public class FFTgt_H2G_GIM_AuxNetworkEnableDisable
        : FFTgt_B2B_GIM_Data
    {
        private bool _enabledisable;

        /// <summary>
        /// 0 Disable; 1 Enable
        /// </summary>
        public bool EnableDisable
        {
            get
            {
                return this._enabledisable;
            }
            set
            {
                this._enabledisable = value;
            }
        }

        /// <summary>
        ///  Text to be displayed at the GMU.
        /// </summary>
        public string Display { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GIM_SubTargets.AuxNetworkEnableDisable;
            }
        }
    }
}
