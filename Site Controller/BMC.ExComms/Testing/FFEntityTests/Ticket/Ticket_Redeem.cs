using System;
using BMC.ExComms.Contracts.DTO.Freeform;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FFEntityTests.Ticket
{
    [TestClass]
    public class Ticket_Redeem
    {
        #region Ticket Redeem Start (G2H)
        [TestMethod]
        [TestCategory("Ticket")]
        public void Buffer_TicketRedeem_Start_G2H()
        {
            byte[] buffer = TestHelper.GetBuffer("[C0][A8][02][10][05][A2][BC][8A][01][00][01][00][01][00][0C]" +
                                                 "[0A][0A][03][10][00][00][00][00][78][78][90][98][C5]");
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
        public void Entity_TicketRedeem_Start_G2H()
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

            FFTgt_G2H_Ticket_Redemption_Request tgt2 = new FFTgt_G2H_Ticket_Redemption_Request();
            tgt.AddTarget(tgt2);

            tgt2.Barcode = "100000000078789098";

            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer,
                "[05][A2][BC][8A][01][00][01][00][01][00][0C][0A][0A][03][10][00][00][00][00][78][78][90][98][C5]");
        }
        #endregion

        #region Ticket Redeem Start (H2G)
        [TestMethod]
        [TestCategory("Ticket")]
        public void Buffer_TicketRedeem_Start_H2G()
        {
            byte[] buffer = TestHelper.GetBuffer("[C0][A8][02][10][05][FB][8A][01][00][01][00][01][00][11]" +
                                                 "[0A][0F][03][10][00][00][00][00][78][78][90][98][00][00][99][76][00][0F]");
            Assert.IsNotNull(buffer);

            UdpFreeformEntity udp = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.H2G, buffer);
            Assert.IsNotNull(udp);

            string sBuffer1 = udp.RawData.GetConvertBytesToHexString(string.Empty);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.H2G, udp);
            Assert.IsNotNull(ff);

            byte[] buffer2 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, ff);
            string sBuffer2 = buffer2.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer1, sBuffer2);
        }

        [TestMethod]
        [TestCategory("Ticket")]
        public void Entity_TicketRedeem_Start_H2G()
        {
            FFMsg_H2G msg = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
                new FFCreateEntityRequest_H2G()
                {
                    PollCode = FF_AppId_H2G_PollCodes.FreeformResponse,
                    SessionID = FF_AppId_SessionIds.Tickets,
                    TransactionID = 1,
                });

            FFTgt_B2B_TicketInfo tgt = new FFTgt_B2B_TicketInfo();
            msg.AddTarget(tgt);

            FFTgt_H2G_Ticket_Redemption_Response tgt2 = new FFTgt_H2G_Ticket_Redemption_Response();
            tgt.AddTarget(tgt2);

            tgt2.Barcode = "100000000078789098";
            tgt2.Amount = 9976;
            tgt2.Type = FF_AppId_TicketTypes.Cashable;

            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer,
                "[05][FB][8A][01][00][01][00][01][00][11][0A][0F][03][10][00][00][00][00][78][78][90][98][00][00][99][76][00][0F]");
        }
        #endregion

        #region Ticket Redeem End (G2H)
        [TestMethod]
        [TestCategory("Ticket")]
        public void Buffer_TicketRedeem_End_G2H()
        {
            byte[] buffer = TestHelper.GetBuffer("[C0][A8][02][10][05][A2][BC][8A][01][00][01][00][01][00][12]" +
                                                 "[0A][10][04][00][10][00][00][00][00][78][78][90][98][00][00][98][56][00][CA]");
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
        public void Entity_TicketRedeem_End_G2H()
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

            FFTgt_G2H_Ticket_Redemption_Close tgt2 = new FFTgt_G2H_Ticket_Redemption_Close();
            tgt.AddTarget(tgt2);

            tgt2.Status = FF_AppId_TicketRedemption_Close_Status.Success;
            tgt2.Barcode = "100000000078789098";
            tgt2.Amount = 9856;
            tgt2.Type = FF_AppId_TicketTypes.Cashable;

            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer,
                "[05][A2][BC][8A][01][00][01][00][01][00][12][0A][10][04][00][10][00][00][00][00][78][78][90][98][00][00][98][56][00][CA]");
        }
        #endregion

        #region Ticket Redeem End (H2G)
        [TestMethod]
        [TestCategory("Ticket")]
        public void Buffer_TicketRedeem_End_H2G()
        {
            byte[] buffer = TestHelper.GetBuffer("[C0][A8][02][10][05][FB][8A][01][00][01][00][01][00][04][0A][02][04][00][5F]");
            Assert.IsNotNull(buffer);

            UdpFreeformEntity udp = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.H2G, buffer);
            Assert.IsNotNull(udp);

            string sBuffer1 = udp.RawData.GetConvertBytesToHexString(string.Empty);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.H2G, udp);
            Assert.IsNotNull(ff);

            byte[] buffer2 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, ff);
            string sBuffer2 = buffer2.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer1, sBuffer2);
        }

        [TestMethod]
        [TestCategory("Ticket")]
        public void Entity_TicketRedeem_End_H2G()
        {
            FFMsg_H2G msg = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
                new FFCreateEntityRequest_H2G()
                {
                    PollCode = FF_AppId_H2G_PollCodes.FreeformResponse,
                    SessionID = FF_AppId_SessionIds.Tickets,
                    TransactionID = 1,
                });

            FFTgt_B2B_TicketInfo tgt = new FFTgt_B2B_TicketInfo();
            msg.AddTarget(tgt);

            FFTgt_H2G_Ticket_Redemption_Close tgt2 = new FFTgt_H2G_Ticket_Redemption_Close();
            tgt.AddTarget(tgt2);

            tgt2.Status = FF_AppId_ResponseStatus_Types.Fail;

            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer,
                "[05][FB][8A][01][00][01][00][01][00][04][0A][02][04][00][5F]");
        }
        #endregion
    }
}
