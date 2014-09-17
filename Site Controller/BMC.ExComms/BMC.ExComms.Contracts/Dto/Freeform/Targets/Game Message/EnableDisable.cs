using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_GM_SAS_EnableDisable
        : FFTgt_H2G_GameMessage_SASCommand
    {
        public FFTgt_H2G_GM_SAS_EnableDisable() { }

        public bool EnableDisable
        {
            get
            {
                return this.LongPollCommand == 2;
            }
            set
            {
                this.LongPollCommand = (byte)(value ? 2 : 1);
            }
        }
    }
}
