using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_LongPoll : FFTgt_G2H
    {
        #region Properties

        public byte PollCode { get; set; }
        public byte[] LongPollData { get; set; }
        public int DataSize { get; set; }
        public bool Ack { get; set; }

        #endregion 
    }
}
