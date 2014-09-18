using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace FFEntityTests
{
    /// <summary>
    /// Summary description for RawBufferTests
    /// </summary>
    [TestClass]
    public class RawBufferTests
    {
        public RawBufferTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GIMTest()
        {
            byte[] buffer = TestHelper.GetBuffer("[C0][A8][02][10][05][A2][BC][17][B1][00][01][00][01][00][5E][17][5C][01][5A][01][05][30][30][30][30][32][02][05][31][32][33][34][35][03][02][42][39][04][0C][30][30][30][30][30][30][30][31][32][33][34][35][05][06][00][16][80][01][47][4D][06][03][36][30][31][07][2B][4F][63][74][20][31][38][20][32][30][31][33][20][31][35][3A][35][32][3A][31][37][20][56][65][72][2D][33][30][30][2E][30][35][2E][31][34][61][20][4F][70][74][69][6F][6E][73][C4]");
            Assert.IsNotNull(buffer);
            UdpFreeformEntity udp = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buffer);
            Assert.IsNotNull(udp);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, udp);
            Assert.IsNotNull(ff);
            byte[] buffer2 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, ff); 
        }

        [TestMethod]
        public void CreateEntity()
        {
            FFMsg_G2H_3 ff = FreeformEntityFactory.CreateEntity<FFMsg_G2H_3>(FF_FlowDirection.G2H,
                new FFCreateEntityRequest_G2H()
                {
                    Command = FF_AppId_G2H_Commands.Freeform3Response
                });
            FFTgt_B2B_GIM gim = new FFTgt_B2B_GIM();
            ff.Targets.Add(gim);
            FFTgt_G2H_GIM_GameIDInfo gid = new FFTgt_G2H_GIM_GameIDInfo();
            gim.Targets.Add(gid);
        }

        [TestMethod]
        public void AuxNetworkEnableDisable()
        {
            FFMsg_H2G ff = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
                new FFCreateEntityRequest_H2G()
                {
                    PollCode = FF_AppId_H2G_PollCodes.Freeform2,
                    SessionID = FF_AppId_SessionIds.GIM, 
                    TransactionID = 178,
                });

            FFTgt_B2B_GIM gim = new FFTgt_B2B_GIM();
            ff.Targets.Add(gim);
            FFTgt_H2G_GIM_AuxNetworkEnableDisable gid = new FFTgt_H2G_GIM_AuxNetworkEnableDisable();
            gid.EnableDisable = true;
            gid.Display = "Welcome to BMC 12.5";
            gim.Targets.Add(gid);

            byte[] data = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, ff);
        }
    }
}
