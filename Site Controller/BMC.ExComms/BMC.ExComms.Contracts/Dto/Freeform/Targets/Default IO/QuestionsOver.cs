using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{  
    #region Request
    /// <summary>
    /// This indicates that the dialog is over, and normal operations will resume.
    /// </summary>
    public class FFTgt_G2H_DefaultIO_QuestionsOverRequest 
        : FFTgt_B2B_DefaultIO_Data, IFFTgt_G2H
    {
        public string MessageSequenceNumber { get; set; }

        public string PlayerCard { get; set; }

        public string Message { get; set; }
       
        public TimeSpan DisplayTimeinSeconds { get; set; }

        public string IViewContentIdentifier { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_DefaultIO_Types.QuestionOver;
            }
        }
    }
    #endregion
}
