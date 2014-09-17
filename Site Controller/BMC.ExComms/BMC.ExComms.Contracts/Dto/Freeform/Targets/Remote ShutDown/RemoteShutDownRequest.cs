using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_RemoteShutDown_Request :
        FFTgt_H2G
    {
        private FF_AppId_RemoteShutdown_RequestTypes _requestType = FF_AppId_RemoteShutdown_RequestTypes.SendStatus;

        public FFTgt_H2G_RemoteShutDown_Request(FF_AppId_RemoteShutdown_RequestTypes requestType)
        {
            _requestType = requestType;
        }

        #region Properties

        public TimeSpan WaitTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public FF_AppId_RemoteShutdown_RequestGameStatus GameStatus { get; set; }

        #endregion

        public override int EntityId
        {
            get
            {
                return (int)_requestType;
            }
        }
    }
}
