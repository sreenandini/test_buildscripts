using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Request
    public class FFTgt_G2H_DefaultIO_QuestionRequest 
        : FFTgt_B2B_DefaultIO_Data, IFFTgt_G2H
    {
        public string MessageSequenceNumber { get; set; }

        public string PlayerCard { get; set; }

        public string Question { get; set; }

        /// <summary>
        /// 0-enter any value; 1-6, the number of choices following.
        /// </summary>
        public byte NumberOrChoices { get; set; }

        public FFTgt_G2H_DefaultIO_QuestionRequest_ChoiceData ChoiceData { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_DefaultIO_Types.QuestionRequest;
            }
        }
    } 
    #endregion

    #region Response
    /// <summary>
    /// Automatic application ACK on player responding within response timeout with 8,L(block length), x, 5, TEXT (10 bytes of Message Sequence Number) ,  TEXT (10 bytes PlayerCard), y in data field
    /// </summary>
    public class FFTgt_G2H_DefaultIO_QuestionRequest_ChoiceData
        : FFTgt_B2B_DefaultIO_Data, IFFTgt_H2G
    {
       public byte ChoiceNumber { get; set; }

        public string ChoiceText { get; set; }

        public string ResponseText { get; set; }

        public byte ResponseTimeout { get; set; }

        public byte TimeToWaitForNextQuestion { get; set; }

        /// <summary>
        /// 0-Response must be full; 1-Less than a full response allowed.
        /// </summary>
        public FF_AppId_DefaultIO_QuestionRequest_ResponseStatus LessThanFullResponseAllowed { get; set; }

        /// <summary>
        /// 0-No encryption; 1- encryption used in response.
        /// </summary>
        public FF_GmuId_DefaultIO_QuestionRequest_EncryptionStatus Encryption { get; set; }
    } 
    #endregion
}
