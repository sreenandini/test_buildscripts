using System;
using System.Net;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FFEntityTests
{
    [TestClass]
    public class GIMTests
    {
        #region Game ID Info (G2H)
        [TestMethod]
        [TestCategory("GIM")]
        public void FF2Buf_GIM_GameIDInfo_G2H()
        {
            byte[] buffer = TestHelper.GetBuffer("[C0][A8][02][10][05][FB][17][01][00][01][00][01][00][1C][17][1A][02][09][01][53][75][63][63][65][73][73][21][03][04][0A][02][1E][68][04][04][00][01][23][45][05][01][47][3C]");
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
        [TestCategory("GIM")]
        public void FF2Mon_GIM_GameIDInfo_G2H()
        {
            byte[] buffer = TestHelper.GetBuffer("[C0][A8][02][10][05][A2][BC][17][B1][00][01][00][01][00][5E][17][5C][01][5A][01][05][30][30][30][30][32][02][05][31][32][33][34][35][03][02][42][39][04][0C][30][30][30][30][30][30][30][31][32][33][34][35][05][06][00][16][80][01][47][4D][06][03][36][30][31][07][2B][4F][63][74][20][31][38][20][32][30][31][33][20][31][35][3A][35][32][3A][31][37][20][56][65][72][2D][33][30][30][2E][30][35][2E][31][34][61][20][4F][70][74][69][6F][6E][73][C4]");
            Assert.IsNotNull(buffer);

            UdpFreeformEntity udp = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buffer);
            Assert.IsNotNull(udp);

            string sBuffer1 = buffer.GetConvertBytesToHexString(string.Empty);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, udp);
            Assert.IsNotNull(ff);

            byte[] buffer2 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, ff);
            string sBuffer2 = buffer2.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer1, sBuffer2);

            IMonitorEntity monitor = MonitorEntityFactory.CreateEntity(ff as FFMsg_G2H);
            Assert.IsNotNull(monitor);
        }

        [TestMethod]
        [TestCategory("GIM")]
        public void Buf2FF_GIM_GameIDInfo_G2H()
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                new FFCreateEntityRequest_G2H()
                {
                    MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                    Command = FF_AppId_G2H_Commands.ResponseRequest,
                    SessionID = FF_AppId_SessionIds.GIM,
                    TransactionID = 1,
                });

            FFTgt_B2B_GIM tgt = new FFTgt_B2B_GIM();
            msg.AddTarget(tgt);

            FFTgt_G2H_GIM_GameIDInfo tgt2 = new FFTgt_G2H_GIM_GameIDInfo();
            tgt.AddTarget(tgt2);

            tgt2.AssetNumber = "12345";

            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer,
                "[05][FB][17][01][00][01][00][01][00][1C][17][1A][02][09][01][53][75][63][63][65][73][73][21][03][04][0A][02][1E][68][04][04][00][01][23][45][05][01][47][3C]");
        }
        #endregion

        #region Game ID Info (H2G)
        [TestMethod]
        [TestCategory("GIM")]
        public void FF2Buf_GIM_GameIDInfo_H2G()
        {
            byte[] buffer = TestHelper.GetBuffer("[C0][A8][02][10][05][FB][17][01][00][01][00][01][00][1C][17][1A][02][09][01][53][75][63][63][65][73][73][21][03][04][0A][02][1E][68][04][04][00][01][23][45][05][01][47][3C]");
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
        [TestCategory("GIM")]
        public void Mon2FF_GIM_GameIDInfo_H2G()
        {
            MonMsg_H2G monMsg = new MonMsg_H2G()//(FaultSource.GIM_Event, (int)FaultType_GIM.Game_Id_Info_H2G)
            {
                InstallationNo = 4,
            };
            monMsg.Targets.Add(new MonTgt_H2G_GIM_GameIDInfo()
            {
                AssetNumberInt = 111,
                EnableNetworkMessaging = true,
                PokerGamePrefix = "A",
                SourceAddress = Extensions.GetIpAddress(-1),
            });
            IFreeformEntity ff = MonitorEntityFactory.CreateEntity(monMsg);
            Assert.IsNotNull(ff);

            byte[] buffer2 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, ff);
            string sBuffer2 = buffer2.GetConvertBytesToHexString(string.Empty);
        }

        [TestMethod]
        [TestCategory("GIM")]
        public void Buf2FF_GIM_GameIDInfo_H2G()
        {
            FFMsg_H2G msg = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
                new FFCreateEntityRequest_H2G()
                {
                    PollCode = FF_AppId_H2G_PollCodes.FreeformNoResponse,
                    SessionID = FF_AppId_SessionIds.GIM,
                    TransactionID = 1,
                });

            FFTgt_B2B_GIM tgt = new FFTgt_B2B_GIM();
            msg.AddTarget(tgt);

            FFTgt_H2G_GIM_GameIDInfo tgt2 = new FFTgt_H2G_GIM_GameIDInfo();
            tgt.AddTarget(tgt2);

            tgt2.AssetNumberInt = 00012345;
            tgt2.DisplayMessage = "Success!";
            tgt2.EnableNetworkMessaging = true;
            tgt2.SourceAddress = Extensions.GetIpAddress(-1);
            tgt2.PokerGamePrefix = "G";

            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            Assert.AreEqual(sBuffer,
                "[05][FB][17][01][00][01][00][01][00][1C][17][1A][02][09][01][53][75][63][63][65][73][73][21][03][04][0A][02][1E][68][04][04][00][01][23][45][05][01][47][3C]");
        }
        #endregion
    }
}
