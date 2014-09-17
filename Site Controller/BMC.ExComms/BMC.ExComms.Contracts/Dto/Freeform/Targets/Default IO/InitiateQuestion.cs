using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// It is a request operation that starts a player dialog, with an internal GMU timeout waiting for the first 8.5 message or an 8.6 message. 
    /// This is created at the GMU in response to a key press followed by up to 4 digit entry.
    /// </summary>
    public class FFTgt_G2H_DefaultIO_InitiateQuestion
        : FFTgt_B2B_DefaultIO_Data, IFFTgt_G2H
    {
        public string RequestCode { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_DefaultIO_Types.InitiateQuestion;
            }
        }
    }

    /// <summary>
    /// The system will respond with either an 8.5 or 8.6 question operation.  Display of “Please Wait” until 8.5/8.6 message or internal timeout value reached
    /// </summary>
    public class FFTgt_H2G_DefaultIO_InitiateQuestion
        : FFTgt_B2B_DefaultIO_Data, IFFTgt_H2G
    {
        public string PlayerCard { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_DefaultIO_Types.InitiateQuestion;
            }
        }
    }
}
