#define HOST_TEST1

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BMC.Common.Utilities;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.IoC;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Contracts.Native;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.ExComms.Server;
using BMC.ExComms.Server.ExecutionSteps;
using BMC.ExComms.Server.Handlers;
using BMC.ExComms.Server.Handlers.ECash;
using BMC.ExComms.Server.Handlers.GMUEvent;
using BMC.ExComms.Server.Handlers.Security;
using BMC.ExComms.Server.Handlers.Targets.GVA;
using BMC.ExComms.Server.Handlers.Tickets;

namespace ConsoleTesting
{
    class Program
    {
        private static ExCommsServerImpl _server = null;
        private static string IPADDR = "192.168.10.2";

        static void Main(string[] args)
        {
            Console.Title = "ExCommsServer Testing";
            Console.SetWindowSize(80, 58);

            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
            Log.AddAppFileLoggingSystem();
            Log.GlobalWriteToExternalLog += Log_GlobalWriteToExternalLog;

            TestPIDData();
            //MonMsg_H2G monH2G = new MonMsg_H2G();
            //MonTgt_H2G_AckNack nack = new MonTgt_H2G_AckNack();
            //nack.Nack = true;
            //monH2G.AddTarget(nack);
            //var msg2 = MonitorEntityFactory.CreateEntity(monH2G);
            //var ffBuffer = FreeformEntityFactory.CreateBuffer(msg2);

            var configStore = ExMonitorServerConfigStoreFactory.Store;
            var cat = ErrorEventCategoryFactory.Categories;

            string barcode = "889900027000052237";
            TicketIDInfo idInfo = new TicketIDInfo(barcode);
            int seqno = idInfo.SequenceNumber;
            //byte[] packed = FreeformHelper.GetBCDToBytes(sUnpacked, sUnpacked.Length / 2);
            //string sUnpacked2 = packed.GetBCDValueString(0, 0, 9);
            
            

            //EncryptSecurity();

            //var dr = ExCommsDataContext.Current.GetMeterDeltaForPlayerSession(27, "1000012345", "IR");
            //ExecutionStepsTest();
            //return;

            //ExCommsHostingModuleTypeHelper.Current.SetAll();
            //_server = new ExCommsServerImpl(ExecutorServiceFactory.CreateExecutorService());
            //_server.Start();
            IExecutorService exec = ExecutorServiceFactory.CreateExecutorService();
            //ExecutionStepFactory.Initialize(exec, false, ExecutionStepDeviceTypes.GMU);
            //FFMsgHandlerFactory.Initialize(exec, FFTgtHandlerDeviceTypes.GMU, () =>
            //{
            //    return new FFMsgTransmitter();
            //});
            MonTgt_G2H_GIM_GameIDInfo target = new MonTgt_G2H_GIM_GameIDInfo();
            MonTgt_G2H_GVA_TED_Request r = new MonTgt_G2H_GVA_TED_Request();
            //TestGIM();

            //Console.ForegroundColor = ConsoleColor.Cyan;
            //GenerateKeys(SECURITY_KEY_INDEX.TICKET_KEY, IPADDR);
            //GenerateKeys(SECURITY_KEY_INDEX.EFT_KEY, IPADDR);
            //Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Start ExComms Server?");
            Console.ResetColor();
            string anser = "y";// Console.ReadLine();

            if (anser == "y")
            {
                var activators =
                    MEFHelper.GetExportedValues<IExCommsServerHostFactoryActivator>();

                ExCommsServerHostFactoryActivatorFactory factory = new ExCommsServerHostFactoryActivatorFactory(exec,
                    ExCommsHostingModuleType.CommunicationServer |
                    ExCommsHostingModuleType.MonitorServer4CommsServer |
                    ExCommsHostingModuleType.MonitorServer4MonProcessor
                    ,
                    activators);
                factory.Start();
            }

            long ticks = DateTime.Now.Ticks;
            byte[] lohi = FFDataTypeHelper.GetInt64Bytes(ticks, FFEndianType.LittleEndian);
            //TestGIM();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press any key to continue...");
                Console.ResetColor();
                string s = Console.ReadLine();
                if (s == "q") return;

                //TestEncryptedTicketMessage();
                //InitKeyExchangeStart();
                //if (s == "k1") InitKeyExchangeStartG2H();
                //if (s == "k2") InitKeyExchangeEnd();
                //if (s == "t") GenerateKeys(SECURITY_KEY_INDEX.TICKET_KEY);
                //if (s == "e") GenerateKeys(SECURITY_KEY_INDEX.EFT_KEY);
                if (s == "g") ExecutionStepsTest();
                if (s == "ss") EncryptSecurity();
                if (s == "pci") TestPlayerCardIn();
                if (s == "pco") TestPlayerCardOut();
                if (s == "bal") TestECashBalanceRequest();
            }
            //TestKeyExchangePartialKey();
            return;

            //short sval = 9999;
            //short ss1 = sval;
            //List<byte> ll = new List<byte>();
            //while (true)
            //{
            //    byte b1 = (byte)(ss1 % 10);
            //    ll.Add(b1);
            //    ss1 /= 10;
            //}

            byte[] b1234 = new byte[] { 0x12, 0x34, 0x56, 0x78 };
            ulong u = b1234.GetBytesToBCDUInt64(0, 4);
            int v = 99999;
            byte b1 = (byte)(v & 0x0F);
            byte b2 = (byte)((v >> 4) & 0x0F);
            byte[] buf = "1000500026".GetBCDToBytes(5);
            byte[] iv = FFDataTypeHelper.GetInt32Bytes(99999, FFEndianType.LittleEndian);
            byte[] sb1 = FFDataTypeHelper.GetInt16Bytes(9999, FFEndianType.LittleEndian);
            byte[] sb2 = FFDataTypeHelper.GetInt16Bytes(9999, FFEndianType.BigEndian);
            short s1 = FFDataTypeHelper.GetInt16(sb1, FFEndianType.LittleEndian);
            short s2 = FFDataTypeHelper.GetInt16(sb2, FFEndianType.BigEndian);

            string cc = "1A345";
            byte[] b = cc.GetHexBytesValue(2);
            //FF_AppId_SessionIds sval = FFEnumParserFactory.GetAppId<FF_GmuId_SessionIds, FF_AppId_SessionIds>(FF_GmuId_SessionIds.ECash);
            //FF_GmuId_SessionIds ss2 = FFEnumParserFactory.GetGmuId<FF_AppId_SessionIds, FF_GmuId_SessionIds>(FF_AppId_SessionIds.GIM);

            //byte b = 23;
            //FF_AppId_SessionIds ss3 = b.GetAppId<FF_GmuId_SessionIds, FF_AppId_SessionIds>();
        }

        static void Log_GlobalWriteToExternalLog(string formattedMessage, BMC.CoreLib.Diagnostics.LogEntryType type, object extra)
        {
            Console.WriteLine(formattedMessage);
        }

        static void GIMTest()
        {

        }

        static void TestGVA_TicketNum()
        {
            //FFMsg_H2G msg = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
            //                   new FFCreateEntityRequest_H2G()
            //                   {
            //                       //Command = FF_AppId_G2H_Commands.ResponseRequest,
            //                       //MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
            //                       PollCode = FF_AppId_H2G_PollCodes.FreeformResponse,
            //                       SessionID = FF_AppId_SessionIds.Tickets,
            //                       IPAddress = "192.168.10.2",
            //                       SkipTransactionId = true,
            //                   });
            //FFTgt_B2B_GMUVarAction gim = new FFTgt_B2B_GMUVarAction()
            //{
            //    //ActionData = new FFTgt_G2H_GVA_TN_Status()
            //    //{
            //    //    Status = FF_AppId_ResponseStatus_Types.Success,
            //    //}
            //    //ActionData = new FFTgt_G2H_GVA_TN_Request()
            //    ActionData = new FFTgt_H2G_GVA_TN_Response()
            //    {
            //        TicketNumber = "000456",
            //    }
            //};
            //msg.AddTarget(gim);
            var msg = TicketParametersHelper.InitTicketDataRequest(IPADDR);
            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
            var msg2 = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H, buffer);
        }

        static void TestGIM()
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                               new FFCreateEntityRequest_G2H()
                               {
                                   Command = FF_AppId_G2H_Commands.ResponseRequest,
                                   MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                                   SessionID = FF_AppId_SessionIds.GIM,
                                   IPAddress = "192.168.10.2",
                                   SkipTransactionId = true,
                               });
            FFTgt_B2B_GIM gim = new FFTgt_B2B_GIM()
            {
                GIMData = new FFTgt_G2H_GIM_GameIDInfo()
                {
                    AssetNumber = "1111",
                    GMUNumber = "1112",
                    SerialNumber = "1113",
                    ManufacturerID = "1114",
                    MACAddress = "1115",
                    SASVersion = "1116",
                    GMUVersion = "1117",
                }
            };
            msg.AddTarget(gim);
            FFMsgHandlerFactory.Current.Execute(msg);
        }

        static void EncryptSecurity()
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                      new FFCreateEntityRequest_G2H_ResponseRequired()
                      {
                          Command = FF_AppId_G2H_Commands.ResponseRequest,
                          MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                          SessionID = FF_AppId_SessionIds.Security,
                          TransactionID = 0xB1,
                          IPAddress = IPADDR,
                      });
            FFTgt_B2B_Security tgt = new FFTgt_B2B_Security_ResponseRequired()
            {
                SecurityData = new FFTgt_B2B_Security_KeyExchange_Request(),
            };
            msg.AddTarget(tgt);
            byte[] buf2 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
            var msgF = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H, buf2);

            FFMsg_H2G msg2 = msg.CopyTo(FF_FlowDirection.H2G, new FFCreateEntityRequest_H2G_ResponseRequired()
            {
                PollCode = FF_AppId_H2G_PollCodes.FreeformResponse,
            }) as FFMsg_H2G;
            FFTgt_B2B_Security tgt2 = new FFTgt_B2B_Security()
            {
                SecurityData = new FFTgt_B2B_Security_KeyExchange_PartialKey()
                {
                    PartialKey = new byte[] { 10, 11, 12, 34, 56, 78, 96, 89, 44 },
                }
            };
            msg2.AddTarget(tgt2);

            byte[] buf3 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, msg2);
            var msgF3 = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G, buf3, IPADDR);

            ExecutionStepFactory.Current.Execute(msg);
            //byte[] buf1 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, msg2);
            //UdpFreeformEntity buffer = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buf1);
            //IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, buffer);
            //byte[] buf2 = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
        }

        static void ExecutionStepsTest()
        {
            //var d = new ExecutionStepDictionary();
            //d.CreateExecutionSteps(ExecutionStepDeviceTypes.Simulator, null, null);
            //return;

            FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                new FFCreateEntityRequest_G2H()
                {
                    MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                    Command = FF_AppId_G2H_Commands.ResponseRequest,
                    SessionID = FF_AppId_SessionIds.GIM,
                    TransactionID = 1,
                    IPAddress = Extensions.GetIpAddressString(-1),
                });

            FFTgt_B2B_GIM tgt = new FFTgt_B2B_GIM();
            msg.AddTarget(tgt);

            FFTgt_G2H_GIM_GameIDInfo tgt2 = new FFTgt_G2H_GIM_GameIDInfo();
            tgt.AddTarget(tgt2);

            tgt2.AssetNumber = "00012345";
            tgt2.SerialNumber = "00012345";

            ExecutionStepFactory.Current.Execute(msg);
        }

        static void GenerateKeys(SECURITY_KEY_INDEX key, string ipAddress)
        {
            FreeformSecurityTableCollection host = ExecutionStepFactory.Current.GetSecurityTables(ipAddress);
            FreeformSecurityTableCollection gmu = new FreeformSecurityTableCollection();

            byte[] gmuPartialKey = gmu.CreatePartialKeyGmu(key);
            byte[] hostPartialKey = host.CreatePartialKeyHost(key);

            gmu.StoreOtherPartialKeyGmu(key, hostPartialKey);
            host.StoreOtherPartialKeyHost(key, gmuPartialKey);

            if (host.CreateCommonKey(key) &&
                gmu.CreateCommonKey(key))
            {
                Console.WriteLine("Successfully created the common keys for : " + key.ToString());
                //Console.WriteLine("Enter a string to encrypt?");
                //string data = Console.ReadLine();
                //byte[] original = Encoding.ASCII.GetBytes(data);
                //byte[] original2 = Encoding.ASCII.GetBytes(data);

                //gmu.EncryptGmu(key, ref original);
                //string data1 = Encoding.ASCII.GetString(original);

                //host.DecryptHost(key, ref original);
                //string data2 = Encoding.ASCII.GetString(original);

                //Console.WriteLine(key.ToString() + ", Local Partial Key : " + host[key].LocalPartialKey.GetConvertBytesToHexString(string.Empty));
                //Console.WriteLine(key.ToString() + ", Other Partial Key : " + host[key].OtherPartialKey.GetConvertBytesToHexString(string.Empty));
                //Console.WriteLine(key.ToString() + ", Common Key : " + host[key].CommonKey.GetConvertBytesToHexString(string.Empty));

                //Console.WriteLine(key.ToString() + ", Original : " + data);
                //Console.WriteLine(key.ToString() + ", Encrypted : " + data1);
                //Console.WriteLine(key.ToString() + ", Decrypted : " + data2);
            }
        }

        static void TestPlayerCardIn()
        {
            FFMsg_G2H msg = GMUStdEventHelper.PlayerCardIn("192.168.10.2", "1000012345");
            FFTgt_G2H_GMUEvent_StdData std = msg.EntityPrimaryTarget as FFTgt_G2H_GMUEvent_StdData;
            std.SMICode = "12";
            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            var msg2 = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H, buffer);
            FFTgt_G2H_GMUEvent_StdData std2 = msg2.EntityPrimaryTarget as FFTgt_G2H_GMUEvent_StdData;
            ExecutionStepFactory.Current.Execute(msg);
        }

        static void TestPlayerCardOut()
        {
            FFMsg_G2H msg = GMUStdEventHelper.PlayerCardOut("192.168.10.2", "1000012345");
            FFTgt_G2H_GMUEvent_StdData std = msg.EntityPrimaryTarget as FFTgt_G2H_GMUEvent_StdData;
            std.SMICode = "12";
            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            var msg2 = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H, buffer);
            FFTgt_G2H_GMUEvent_StdData std2 = msg2.EntityPrimaryTarget as FFTgt_G2H_GMUEvent_StdData;
            ExecutionStepFactory.Current.Execute(msg);
        }

        static void TestECashBalanceRequest()
        {
            FFMsg_G2H msg = ECashHelper.BalanceRequest("192.168.10.2", "1000012345", "0124");
            byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
            string sBuffer = buffer.GetConvertBytesToHexString(string.Empty);
            var msg2 = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H, buffer);
            FFTgt_G2H_GMUEvent_StdData std2 = msg2.EntityPrimaryTarget as FFTgt_G2H_GMUEvent_StdData;
            ExecutionStepFactory.Current.Execute(msg);
        }

        //static void InitKeyExchangeStartG2H()
        //{
        //    FreeformEncryptionHelper.InitKeyExchangeStart(
        //        ModuleHelper.Current.CreateH2GMessage(IPADDR,
        //        FF_AppId_SessionIds.Security,
        //        FF_AppId_H2G_PollCodes.FreeformResponse));
        //}

        //static void InitKeyExchangeStartH2G()
        //{
        //    FreeformEncryptionHelper.InitKeyExchangeStart(
        //        ModuleHelper.Current.CreateH2GMessage(IPADDR,
        //        FF_AppId_SessionIds.Security,
        //        FF_AppId_H2G_PollCodes.FreeformResponse));
        //}

        //static void InitKeyExchangeEnd()
        //{
        //    byte[] key = new byte[] { 10, 11, 12, 34, 56, 78, 96, 89, 44 };
        //    FreeformEncryptionHelper.InitKeyExchangeEnd(
        //        ModuleHelper.Current.CreateH2GMessage(IPADDR,
        //        FF_AppId_SessionIds.Security,
        //        FF_AppId_H2G_PollCodes.FreeformResponse),
        //        key);
        //}
        static void TestPIDData()
        {
            return;
            string msg = "[C0][A8][02][10][05][A2][B9][1C][4F][00][01][00][01][00][05][1C][03][0F][01][00][FF]";
            byte[] buf = GetBuffer(msg);
            UdpFreeformEntity buffer = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buf);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, buffer);
            IMonitorEntity_Msg mon = MonitorEntityFactory.CreateEntity(ff as FFMsg_G2H);
        }

        static void TestMeterMessage()
        {
            string msg = "[C0][A8][02][12][05][A2][B9][0F][B0][00][01][00][01][01][5B][10][2A][01][CF][00][00][00][00][00][0F][08][00][00][00][00][00][00][00][00][00][00][00][00][00][20][30][30][30][00][30][30][30][30][00][00][00][01][30][30][30][00][00][00][00][0F][49][02][01][04][00][00][31][17][02][04][00][50][45][55][03][04][00][09][40][35][04][04][00][00][00][00][05][04][00][00][00][00][06][04][00][00][00][00][29][04][00][01][51][21][2A][04][00][00][05][43][2B][04][00][00][00][00][2C][04][00][00][00][00][30][04][00][00][00][00][31][04][00][00][00][00][0F][3D][02][07][04][00][00][00][01][08][04][00][00][00][01][09][04][00][00][00][01][0A][04][00][00][00][00][0B][04][00][00][00][01][0C][04][00][00][00][00][2E][04][00][00][00][00][2F][04][00][00][00][00][0D][04][00][00][00][00][0E][04][00][00][66][00][0F][3F][02][13][05][00][00][00][85][21][14][05][00][00][15][07][82][15][05][00][00][00][00][00][16][05][00][00][00][00][00][17][04][00][00][00][03][18][04][00][00][00][52][19][04][00][00][00][00][1A][04][00][00][00][00][24][00][25][00][28][04][01][52][51][01][0F][32][02][0F][04][00][00][00][00][10][04][00][00][00][00][11][04][00][00][00][00][12][04][00][00][00][00][1F][04][00][00][00][00][20][04][00][00][00][00][26][05][00][00][00][00][00][27][04][00][00][00][00][0F][21][02][1B][04][01][19][70][00][1C][04][01][37][43][19][1D][04][00][00][00][00][1E][04][00][00][00][00][28][04][01][52][51][01][2D][00][10][0B][07][1D][03][13][24][39][1C][03][02][21][14][D6]";
            byte[] buf = GetBuffer(msg);
            UdpFreeformEntity buffer = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buf);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, buffer);
            IMonitorEntity_Msg mon = MonitorEntityFactory.CreateEntity(ff as FFMsg_G2H);
        }

        static void TestTicketMessage()
        {
            string msg =
                "[C0][A8][02][12][05][A2][BC][8A][8D][00][01][00][01][00][11][0A][02][06][00][10][0B][07][1D][03][12][57][09][1C][03][02][21][14][57]";
            byte[] buf = GetBuffer(msg);
            UdpFreeformEntity buffer = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buf);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, buffer);
            IMonitorEntity_Msg mon = MonitorEntityFactory.CreateEntity(ff as FFMsg_G2H);
        }

        static void TestKeyExchangeStart()
        {
            string msg =
                "[C0][A8][02][12][05][A2][BC][8E][01][00][01][00][01][00][03][0B][01][03][FA]";
            byte[] buf = GetBuffer(msg);
            UdpFreeformEntity buffer = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buf);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, buffer);
            //var ff2 = new FreeformHandlerFactory();
            //ff2.ProcessMessage(ff as FFMsg_G2H);
            //IMonitorEntity_Msg mon = MonitorEntityFactory.CreateEntity(ff as FFMsg_G2H);
        }

        static void TestKeyExchangePartialKey()
        {
            string msg =
                "[C0][A8][02][12][05][A2][BC][8E][01][00][01][00][01][00][0C][0B][0A][03][01][01][01][01][01][01][01][01][01][DF]";
            byte[] buf = GetBuffer(msg);
            UdpFreeformEntity buffer = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buf);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, buffer);
            //var ff2 = new FreeformHandlerFactory();
            //ff2.ProcessMessage(ff as FFMsg_G2H);
            //IMonitorEntity_Msg mon = MonitorEntityFactory.CreateEntity(ff as FFMsg_G2H);
        }

        static void TestEncryptedECashMessage()
        {
            string msg =
                "[C0][A8][02][12][05][A2][BC][8E][01][00][01][00][01][00][19][0B][0A][07][13][10][0B][2B][0C][0C][2C][B1][21][10][0B][07][1D][03][12][57][19][1C][03][02][21][14][4E]";
            byte[] buf = GetBuffer(msg);
            UdpFreeformEntity buffer = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buf);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, buffer);
            //var ff2 = new FreeformHandlerFactory();
            //ff2.ProcessMessage(ff as FFMsg_G2H);
            //IMonitorEntity_Msg mon = MonitorEntityFactory.CreateEntity(ff as FFMsg_G2H);
        }

        static void TestEncryptedTicketMessage()
        {
            string msg =
                "[C0][A8][02][12][05][A2][BC][8A][0A][00][01][00][01][00][37][8B][1A][02][0C][C4][45][63][27][29][35][AB][DC][44][3D][8E][7B][79][C0][49][86][DB][4C][66][38][22][25][DD][DC][06][0C][01][80][00][00][00][00][00][00][00][00][00][00][10][0B][07][1D][03][15][10][50][1C][03][08][21][13][A9]";
            byte[] buf = GetBuffer(msg);
            UdpFreeformEntity buffer = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buf);
            IFreeformEntity ff = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, buffer);
            //var ff2 = new FreeformHandlerFactory();
            //ff2.ProcessMessage(ff as FFMsg_G2H);
            //IMonitorEntity_Msg mon = MonitorEntityFactory.CreateEntity(ff as FFMsg_G2H);
        }

        public static byte[] GetBuffer(string input)
        {
            int idx = input.IndexOf("[");
            if (idx > 0)
            {
                input = input.Substring(idx, input.Length - idx);
            }
            string data = input.Replace("[", "").Replace("]", ",");
            string[] splitted = data.Split(new char[] { ',' });

            byte[] buffer = new byte[splitted.Length - 1];
            for (int i = 0; i < splitted.Length - 1; i++)
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(splitted[i], 16);
                }
                catch
                {
                    val = Convert.ToInt32(splitted[i], 10);
                }
                buffer[i] = (byte)val;
            }
            return buffer;
        }
    }
}
