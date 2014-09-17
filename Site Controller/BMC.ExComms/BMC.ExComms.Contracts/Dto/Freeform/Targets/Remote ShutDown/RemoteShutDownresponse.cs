using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_RemoteShutDown_Response :
        FFTgt_G2H
    {
        private FF_AppId_RemoteShutdown_ResponseTypes _responseType = FF_AppId_RemoteShutdown_ResponseTypes.GameStatusReturn;

        public FFTgt_G2H_RemoteShutDown_Response(FF_AppId_RemoteShutdown_ResponseTypes responseType)
        {
            _responseType = responseType;
        }

        #region Properties

        public FF_AppId_RemoteShutdown_ResponseStatus Status { get; set; }
        public int Applications { get; set; }
        public DateTime Date { get; set; }

        #endregion

        public override int EntityId
        {
            get
            {
                return (int)_responseType;
            }
        }
    }
}
