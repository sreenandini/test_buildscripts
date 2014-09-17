using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Request
    public class FFTgt_H2G_DefaultIO_DisplayText_Request
        : FFTgt_B2B_DefaultIO_Data, IFFTgt_H2G
    {
        /// <summary>
        /// The text to display
        /// </summary>
        public string Text { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_DefaultIO_Types.DisplayText;
            }
        }
    }    
    #endregion

    #region Response
    public class FFTgt_G2H_DefaultIO_DisplayText_Response
        : FFTgt_B2B_DefaultIO_Data, IFFTgt_G2H
    {
        /// <summary>
        /// 0-failure; 1-success
        /// </summary>
        public FF_AppId_DefaultIO_DisplayText_ResponseStatus Success { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_DefaultIO_Types.DisplayText;
            }
        }

    } 
    #endregion
}
