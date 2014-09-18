using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComExchangeLib;
using System.Threading;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
# if NEW_EXCOMMS
using BMC.ExComms.Contracts;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Proxies;
# endif

namespace BMC.Business.CashDeskOperator
{
    public class MachineManagerInterface
    {
        #region private Variable
# if !NEW_EXCOMMS
        private ExchangeClient _exchangeClient;
        private IExchangeAdmin _iExchangeAdmin;
#endif
        public static volatile bool AckMessage;
        private bool _disposed;
        private volatile List<string> _udpCollection;

        #endregion

        public readonly object HoldingObject = new object();

        #region ctor

        public MachineManagerInterface()
        {
# if !NEW_EXCOMMS
            _exchangeClient = new ExchangeClient();

            _exchangeClient.ExchangeSectorUpdate += ExchangeClientExchangeSectorUpdate;
            _exchangeClient.ACK += ExchangeClientAck;
            _exchangeClient.UDPUpdate += ExchangeClientUDPUpdate;
            _exchangeClient.ServerUpdate += ExchangeClientServerUpdate;

            _exchangeClient.InitialiseExchange(0);
            //RefreshActiveServer();
            _iExchangeAdmin = (IExchangeAdmin)_exchangeClient;
#endif
        }

        #endregion

        #region public Function

        /// <summary>
        /// Manage(Enable/Disable) Slot Ports - AUX Serial port, GAT Serial port, Slot Line port
        /// </summary>
        /// <param name="datapak">The datapak and Message</param>
        /// <returns></returns>
        public int ManageSlotPorts(int datapak, string message)
        {
            int messageID;
            lock (HoldingObject)
            {
#if !NEW_EXCOMMS
                messageID = SendSector203Comexchange(datapak, 115, 3, message);
#else
                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = datapak,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_ManageSlotPorts { Message = message });
                messageID = ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
            }
            return messageID;
        }

        #endregion public Function

        #region private Functions

        private int SendSector201Comexchange(int datapak, int command, byte[] byteArray)
        {
#if !NEW_EXCOMMS    
        var sector201Data = new Sector201Data { Command = Convert.ToByte(command) };
            sector201Data.PutCommandDataVB(byteArray);
            _exchangeClient.RequestExWriteSector(datapak, 201, sector201Data);
            return _iExchangeAdmin.LastMessageID;
#else
            return 0;
#endif
        }

        private void SendSector205Comexchange(int udp, string message)
        {
#if !NEW_EXCOMMS    
        var sector205Data = new Sector205Data();
            var data = new byte[50];
            var stringArray = (message + ",0").Split(',');

            int i;
            for (i = 0; i < stringArray.Length; i++)
                data[i] = Convert.ToByte(stringArray[i]);

            sector205Data.CommandLength = Convert.ToByte(i);
            sector205Data.Command = Convert.ToByte(data[0]);
            sector205Data.PutCommandDataVB(data);

            _exchangeClient.RequestExWriteSector(udp, 205, sector205Data);
#else
            return;
#endif
        }

        private void SendSector203Comexchange(int datapak, byte command)
        {
#if !NEW_EXCOMMS    
         var sector203Data = new Sector203Data { Command = command };
            _exchangeClient.RequestExWriteSector(datapak, 203, sector203Data);
#else
            return;
#endif
        }

        private int SendSector203Comexchange(int datapak, int command, int commandLength, string strMessage)
        {
#if !NEW_EXCOMMS    
        string[] strMessageArray = strMessage.Split(',');

            byte[] messageBytes = new byte[3];

            for (int i = 0; i < strMessageArray.Length; i++)
            {
                messageBytes[i] = ConvertToByte(strMessageArray[i]);
            }

            var sector203Data = new Sector203Data { Command = Convert.ToByte(command) };
            sector203Data.PutCommandDataVB(messageBytes);

            _exchangeClient.RequestExWriteSector(datapak, 203, sector203Data);

            return _iExchangeAdmin.LastMessageID;
#else
            return 0;
#endif
        }

        public int SendSector203Comexchange(int datapak, int command, int commandLength, byte[] messageBytes)
        {
#if !NEW_EXCOMMS    
        var sector203Data = new Sector203Data { Command = Convert.ToByte(command) };
            sector203Data.PutCommandDataVB(messageBytes);

            _exchangeClient.RequestExWriteSector(datapak, 203, sector203Data);

            return _iExchangeAdmin.LastMessageID;
#else
            return 0;
#endif
        }

        private bool RefreshUDPList()
        {
#if !NEW_EXCOMMS
    new Thread(() =>
            {
                lock (HoldingObject)
                {
                    AckMessage = false;
                    Thread.MemoryBarrier();

                    _exchangeClient.RefreshActiveUPDs(GetServerName());
                    Monitor.Wait(HoldingObject, TimeSpan.FromSeconds(15));
                }
            }).Start();

            lock (HoldingObject)
            {
                Monitor.Wait(HoldingObject, 1000 * 10);
                return AckMessage;
            }
#else
            return true;
#endif
        }

        private static string GetServerName()
        {
            return BMCRegistryHelper.GetRegKeyValue(@"Cashmaster\Exchange", "Default_Server_Ip");

            //var key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(@"Software\Honeyframe\Cashmaster\Exchange", true);
            //if (key != null) return key.GetValue("Default_Server_Ip").ToString();
            //throw new InvalidDataException("Registry key not Set.  Default_Server_IP not configured");
        }

        private void RefreshActiveServer()
        {
# if !NEW_EXCOMMS
            _exchangeClient.RefreshActiveServers();
#endif
        }

        public int UpdateTicketConfig(int installationNo, int NoOfDays)
        {  //
#if !NEW_EXCOMMS
            return _exchangeClient.SetTicketParameters(installationNo, NoOfDays);
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = installationNo,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_UpdateTicketConfig { });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
        }

        private byte ConvertToByte(string message)
        {
            return Convert.ToByte(message);
        }

        #endregion

        #region eventHandlers

        void ExchangeClientServerUpdate()
        {
#if !NEW_EXCOMMS
            _exchangeClient.RefreshActiveUPDs("");
#endif
        }

        void ExchangeClientUDPUpdate()
        {
#if !NEW_EXCOMMS
            lock (HoldingObject)
            {
                _udpCollection = new List<string>();
                foreach (var udP in _exchangeClient.ActiveUDPs)
                    _udpCollection.Add(udP.ToString());

                AckMessage = true;
                Thread.MemoryBarrier();
                Monitor.Pulse(HoldingObject);
            }
#endif
        }

        void ExchangeClientAck(MessageAck messageAck)
        {
#if !NEW_EXCOMMS
            lock (HoldingObject)
            {
                AckMessage = messageAck.ACK;
                Thread.MemoryBarrier();
                Monitor.Pulse(HoldingObject);
            }
#endif
        }

        void ExchangeClientExchangeSectorUpdate()
        {
#if !NEW_EXCOMMS
            object punk;
            _exchangeClient.ExchangeReadSector(out punk);
            var udPinfo = (IUDPinfo)punk;
            var udpNo = udPinfo.UDPNo;

            if (punk.GetType() == typeof(Sector205Data))
            {
                var sector205Data = (Sector205Data)punk;
                var counter205 = sector205Data.Get205Data;
                var returnObjectLength = sector205Data.CommandLength;
                //TODO: Implement Sector205 Code Here
            }

            lock (HoldingObject)
            {
                AckMessage = true;
                Thread.MemoryBarrier();
                Monitor.Pulse(HoldingObject);
            }
#endif
        }

        #endregion

        #region internal Function
        /// <summary>
        /// Clears the handpay lock.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal int ClearHandpayLock(int datapak)
        {
            //if (!RefreshUDPList()) return false;
#if !NEW_EXCOMMS
            int messageID;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageID = SendSector201Comexchange(datapak, 0x94, byteArray);
            }
            return messageID;
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = datapak,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_ClearHandpayLock { ClearHandpayLock = true });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
        }

        public int ClearHandPayTilt(int datapak)
        {
#if !NEW_EXCOMMS
            return _exchangeClient.ClearHandPay(datapak);
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = datapak,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_ClearHandpayLock { ClearHandpayLock = true });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
        }

        /// <summary>
        /// Disables the machine.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal int DisableMachine(int datapak)
        {
            //if (!RefreshUDPList()) return false;
#if !NEW_EXCOMMS
            int messageID;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageID = SendSector201Comexchange(datapak, 0x1, byteArray);
            }
            return messageID;
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = datapak,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_EnableDisbleMachine { EnableDisable = false });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
        }

        /// <summary>
        /// Enables the machine.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal int EnableMachine(int datapak)
        {
            //if (!RefreshUDPList()) return false;
#if !NEW_EXCOMMS
            int messageID;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageID = SendSector201Comexchange(datapak, 0x2, byteArray);
            }
            return messageID;
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = datapak,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_EnableDisbleMachine { EnableDisable = true });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
        }

        public int DisableMachineFromUI(int installationNo)
        {
#if !NEW_EXCOMMS
            return _exchangeClient.EnableDisableMachine(1, installationNo);
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = installationNo,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_EnableDisableAFT { EnableDisable = false });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
        }

        public int EnableMachineFromUI(int installationNo)
        {
#if !NEW_EXCOMMS
            return _exchangeClient.EnableDisableMachine(2, installationNo);
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = installationNo,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_EnableDisableAFT { EnableDisable = true });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
        }

        public int EnableDisableAFT(int installationNo, int command)
        {
#if !NEW_EXCOMMS
            return _exchangeClient.AFTEn_Dis( installationNo, command );
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = installationNo,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_EnableDisableAFT { EnableDisable = Convert.ToBoolean(command) });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
        }

        /// <summary>
        /// Disables the note acceptor.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal int DisableNoteAcceptor(int datapak)
        {
            //if (!RefreshUDPList()) return false;
#if !NEW_EXCOMMS
            int messageID;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageID = SendSector201Comexchange(datapak, 0x7, byteArray);
            }
            return messageID;
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = datapak,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_EnableDisableNoteAcceptor { EnableDisableNoteAcceptor = false });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
        }

        /// <summary>
        /// Enables the note acceptor.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal int EnableNoteAcceptor(int datapak)
        {
            //if (!RefreshUDPList()) return false;
#if !NEW_EXCOMMS
            int messageID;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageID = SendSector201Comexchange(datapak, 0x6, byteArray);
            }
            return messageID;
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = datapak,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_EnableDisableNoteAcceptor { EnableDisableNoteAcceptor = true });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
        }

        /// <summary>
        /// Adds the UDP to list.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <param name="barPositionPortNo">The bar position port no.</param>
        /// <returns></returns> 
        internal bool AddUDPToList(int datapak, int barPositionPortNo)
        {
            lock (HoldingObject)
            {

                AckMessage = false;
                Thread.MemoryBarrier();

#if !NEW_EXCOMMS
                _iExchangeAdmin.AddUDPToList(GetServerName(), barPositionPortNo, datapak, 0, 7);
                Monitor.Wait(HoldingObject, TimeSpan.FromSeconds(60));
# else
                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = datapak,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_AddUDPToList
                {
                    Port = barPositionPortNo,
                    ServerIP = GetServerName(),
                    PollingID = 0,
                    Type = 7
                });
                AckMessage = ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G);
#endif
                var result = AckMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }
            return AckMessage;
        }

        /// <summary>
        /// Adds the UDP to list.
        /// </summary>
        /// <param name="installatioNo">The installation no.</param>
        /// <param name="barPositionPortNo">The bar position port no.</param> 
        /// <returns></returns>
        public int AddUDPToListWithoutWait(int installatioNo, int barPositionPortNo)
        {
#if !NEW_EXCOMMS
            return _iExchangeAdmin.AddUDPToList(GetServerName(), barPositionPortNo, installatioNo, 0, 7);
#else
            MonMsg_H2G message = new MonMsg_H2G()
            {
                InstallationNo = installatioNo,
            };
            message.Targets.Add(new MonTgt_H2G_Client_AddUDPToList
            {
                ServerIP = GetServerName(),
                Port = barPositionPortNo,
                PollingID = 0,
                Type = 7,
                InstallationNo = installatioNo,
            });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(message) ? 0 : -1;
#endif
            //
        }

        /// <summary>
        /// Removes the UDP from list.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal bool RemoveUDPFromList(int datapak)
        {
            if (!RefreshUDPList()) return false;

            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;

                AckMessage = false;
                Thread.MemoryBarrier();

#if !NEW_EXCOMMS
                _exchangeClient.RemoveUDPFromList(datapak,0);
                Monitor.Wait(HoldingObject, TimeSpan.FromSeconds(60));
# else
                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = datapak,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_RemoveUDPFromList { });
                AckMessage = ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G);
#endif
                var result = AckMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }
            return AckMessage;
        }

        /// <summary>
        /// Removes the UDP from list.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal int RemoveUDPFromListWithoutWait(int datapak, int nDisMachine)
        {
            //lock (HoldingObject)
            //{
#if !NEW_EXCOMMS
            return _exchangeClient.RemoveUDPFromList(datapak, nDisMachine);
#else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
               {
                   InstallationNo = datapak,
               };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_RemoveUDPFromList { });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
#endif
            //AckMessage = false;
            //Thread.MemoryBarrier();
            //}
        }

        /// <summary>
        /// Gets the game info.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal bool GetGameInfo(int datapak)
        {
            lock (HoldingObject)
            {
                AckMessage = false;
                Thread.MemoryBarrier();

#if !NEW_EXCOMMS
                SendSector203Comexchange(datapak, 112);
                Monitor.Wait(HoldingObject, TimeSpan.FromSeconds(60));
#else
                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = datapak,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_GetGameInfo { });
                AckMessage = ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G);
#endif
                var result = AckMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }

            return AckMessage;
        }

        internal bool MeterForcePeriodic(int datapak)
        {
            lock (HoldingObject)
            {
                AckMessage = false;
                Thread.MemoryBarrier();

#if !NEW_EXCOMMS
                SendSector203Comexchange(datapak, 48);
                Monitor.Wait(HoldingObject, TimeSpan.FromMilliseconds(500));
#else
                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = datapak,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_MeterForcePeriodic { });
                AckMessage = ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G);
#endif
                var result = AckMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }
            return AckMessage;
        }

        internal bool UpdateOptionFileParameter(int datapak)
        {
            lock (HoldingObject)
            {
                AckMessage = false;
                Thread.MemoryBarrier();

#if !NEW_EXCOMMS
                SendSector203Comexchange(datapak, 114);
                Monitor.Wait(HoldingObject, TimeSpan.FromMilliseconds(500));
#else
                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = datapak,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_UpdateOptionFileParameter { });
                AckMessage = ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G);
#endif
                var result = AckMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }
            return AckMessage;
        }

        /// <summary>
        /// Fetch meter values (force periodic) for SiteIntegorration and SpotCheck 
        /// </summary>
        /// <param name="datapak"></param>
        /// <returns></returns>
        internal bool MeterForcePeriodicForSIandSC(int datapak)
        {
            lock (HoldingObject)
            {
                AckMessage = false;
                Thread.MemoryBarrier();

#if !NEW_EXCOMMS
                SendSector203Comexchange(datapak, 48);
                Monitor.Wait(HoldingObject, TimeSpan.FromMilliseconds(1));
#else
                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = datapak,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_MeterForcePeriodic { });
                AckMessage = ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G);
#endif
                var result = AckMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }
            return AckMessage;
        }

        public bool SendSector203(int datapak, byte command)
        {
            lock (HoldingObject)
            {
                AckMessage = false;
                Thread.MemoryBarrier();

                SendSector203Comexchange(datapak, command);
                Monitor.Wait(HoldingObject, TimeSpan.FromSeconds(60));

                var result = AckMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }
            return AckMessage;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

#if !NEW_EXCOMMS
            try
            {
                _exchangeClient.ExchangeSectorUpdate -= ExchangeClientExchangeSectorUpdate;
                _exchangeClient.ACK -= ExchangeClientAck;
                _exchangeClient.UDPUpdate -= ExchangeClientUDPUpdate;
                _exchangeClient.ServerUpdate -= ExchangeClientServerUpdate;

                var i = Marshal.FinalReleaseComObject(_exchangeClient);
                LogManager.WriteLog("|=> _iExchangeAdmin was released successfully.", LogManager.enumLogLevel.Info);
            }
            catch
            { }

            _iExchangeAdmin = null;
            _exchangeClient = null;
#endif
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="MachineManagerInterface"/> is reclaimed by garbage collection.
        /// </summary>
        ~MachineManagerInterface()
        {
            Dispose();
            _disposed = true;
        }

        #endregion

        public bool SendEmpCard(int datapak, int command, byte[] bData)
        {
            lock (HoldingObject)
            {
                AckMessage = false;
                Thread.MemoryBarrier();

#if !NEW_EXCOMMS
                SendSector203Comexchange(datapak, command, command.ToString().Length,bData);

                Monitor.Wait(HoldingObject, TimeSpan.FromSeconds(60));
#else
                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = datapak,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_Client_EmployeeCard { Message = "" });
                AckMessage = ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G);
#endif
                var result = AckMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }
            return AckMessage;
        }
    }
}
