using System;
using BMC.ExComms.Contracts.DTO.Freeform;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FFEntityTests
{
    [TestClass]
    public class ToBufferTests
    {
        [TestMethod]
        public void GMUEvent_Standard()
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                new FFCreateEntityRequest_G2H()
                {
                    MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                    Command = FF_AppId_G2H_Commands.ACK,
                    SessionID = FF_AppId_SessionIds.A1,
                    TransactionID = 1,
                });
            FFTgt_G2H_GMUEvent tgt = new FFTgt_G2H_GMUEvent();
            msg.AddTarget(tgt);
            FFTgt_G2H_GMUEvent_StdData std = new FFTgt_G2H_GMUEvent_StdData();
            std.ExceptionCode = FF_AppId_GMUEventExceptionCodes.Jackpot;
            tgt.AddTarget(std);
            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, tgt);
            Assert.IsNotNull(buffer);
        }
    }
}
