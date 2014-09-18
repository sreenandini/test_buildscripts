using System;
using BMC.ExComms.Contracts.DTO.Freeform;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FFEntityTests.Ticket
{
    [TestClass]
    public class Ticket_Void
    {
        #region Ticket Void (G2H)
        [TestMethod]
        [TestCategory("Ticket")]
        public void Buffer_TicketVoid_G2H()
        {
            byte[] buffer = TestHelper.GetBuffer("[C0][A8][02][10][05][A2][BC][8A][01][00][01][00][01][00][0D]" +
                                                 "[0A][0B][02][10][00][00][00][00][78][78][90][98][01][C3]");
            Assert.IsNotNull(buffer);

            UdpFreeformEntity udp = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buffer);
            Assert.IsNotNull(udp);

            string sBuffer1 = udp.RawData.GetConvertBytesToHexString(string.Empty);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, udp);
            Assert.IsNotNull(ff);

            byte[] buffer2 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, ff);
            string sBuffer2 = buffer2.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer1, sBuffer2);
        }

        [TestMethod]
        [TestCategory("Ticket")]
        public void Entity_TicketVoid_G2H()
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                new FFCreateEntityRequest_G2H()
                {
                    MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                    Command = FF_AppId_G2H_Commands.ResponseRequest,
                    SessionID = FF_AppId_SessionIds.Tickets,
                    TransactionID = 1,
                });

            FFTgt_B2B_TicketInfo tgt = new FFTgt_B2B_TicketInfo();
            msg.AddTarget(tgt);

            FFTgt_G2H_Ticket_Void tgt2 = new FFTgt_G2H_Ticket_Void();
            tgt.AddTarget(tgt2);

            tgt2.Barcode = "100000000078789098";
            tgt2.Error = FF_AppId_TicketPrintStatus.PaperOut;

            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer,
                "[05][A2][BC][8A][01][00][01][00][01][00][0D][0A][0B][02][10][00][00][00][00][78][78][90][98][01][C3]");
        }
        #endregion
    }
}
