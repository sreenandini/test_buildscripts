using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Request

    public class FFTgt_H2G_DefaultIO_PlayerQuery 
        : FFTgt_B2B_DefaultIO, IFFTgt_H2G
    {
        /// <summary>
        /// Text to be shown to player.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Prompt to be shown to player.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// Number of seconds in which player has to enter their response.
        /// </summary>
        public int ResponseTimeout { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_DefaultIO_Types.PlayerQuery;
            }
        }
    }

    #endregion

    #region Response
    public class FFTgt_G2H_DefaultIO_PlayerQuery 
        : FFTgt_B2B_DefaultIO, IFFTgt_G2H
    {
        /// <summary>
        /// Answer provided by player
        /// </summary>
        public string AnswerText { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_DefaultIO_Types.PlayerQuery;
            }
        }
    } 
    #endregion

}
