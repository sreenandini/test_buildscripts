using System;
using BMC.ExComms.Contracts.DTO.Freeform;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FFEntityTests
{
    [TestClass]
    public class EFTTests
    {
        [TestMethod]
        public void Entity_WithdrawalRequest()
        {
            byte[] buffer = TestHelper.GetBuffer("[C0][A8][02][10][05][A2][BC][0E][01][00][01][00][01][00][0F]"+
                                                 "[0E][0D][01][03][00][00][25][20][10][00][50][00][26][06][52][3B]");
            Assert.IsNotNull(buffer);
            UdpFreeformEntity udp = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buffer);
            Assert.IsNotNull(udp);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, udp);
            Assert.IsNotNull(ff);
            byte[] buffer2 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, ff);
            string sBuffer = buffer2.GetConvertBytesToHexString(string.Empty);
        }

        [TestMethod]
        public void Buffer_WithdrawalRequest()
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                new FFCreateEntityRequest_G2H()
                {
                    MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                    Command = FF_AppId_G2H_Commands.ResponseRequest,
                    SessionID = FF_AppId_SessionIds.ECash,
                    TransactionID = 1,
                });
            FFTgt_B2B_EFT tgt = new FFTgt_B2B_EFT();
            msg.AddTarget(tgt);
            FFTgt_G2H_EFT_WithdrawalRequest tgt2 = new FFTgt_G2H_EFT_WithdrawalRequest();            
            tgt.AddTarget(tgt2);
            tgt2.AccountType = FF_AppId_EFT_AccountTypes.PlayerCash;
            tgt2.AmountRequested = 2520;
            tgt2.PlayerCardNumber = "1000500026";
            tgt2.Pin = "0652";
            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            Assert.IsNotNull(buffer);
        }

        [TestMethod]
        public void Buffer_WithdrawalAuthorization()
        {
            FFMsg_H2G msg = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
                new FFCreateEntityRequest_H2G()
                {
                    PollCode = FF_AppId_H2G_PollCodes.FreeformResponse,
                    SessionID = FF_AppId_SessionIds.ECash,
                    TransactionID = 1,
                });
            FFTgt_B2B_EFT tgt = new FFTgt_B2B_EFT();
            msg.AddTarget(tgt);
            FFTgt_H2G_EFT_WithdrawalAuthorization tgt2 = new FFTgt_H2G_EFT_WithdrawalAuthorization();
            tgt.AddTarget(tgt2);
            tgt2.NonCashableAmount = 4550;
            tgt2.CashableAmount = 2520;
            tgt2.ErrorCode = 0;
            tgt2.PlayerCardNumber = "1000500026";
            tgt2.PlayerFlags.Flag3.Flag_DepositCash = true;
            tgt2.DisplayMessage = "Welcome Dinesh, you have 20 points, 100 Cash";
            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            Assert.IsNotNull(buffer);
        }
    }
}
