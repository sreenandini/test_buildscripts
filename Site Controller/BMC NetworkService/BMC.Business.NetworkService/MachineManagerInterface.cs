using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using ComExchangeLib;
using Microsoft.Win32;
using BMC.Common.Utilities;

namespace MachineManagerComInterface
{
    internal class MachineManagerInterface : IDisposable
    {
        #region private Variable

        private ExchangeClient _exchangeClient;
        private IExchangeAdmin _iExchangeAdmin;
        private volatile bool _ackMessage;
        private bool _disposed;
        private volatile List<string> _udpCollection;

        #endregion

        public readonly object HoldingObject = new object();

        #region ctor

        public MachineManagerInterface()
        {
            _exchangeClient = new ExchangeClient();

            _exchangeClient.ExchangeSectorUpdate += ExchangeClientExchangeSectorUpdate;
            _exchangeClient.ACK += ExchangeClientAck;
            _exchangeClient.UDPUpdate += ExchangeClientUDPUpdate;
            _exchangeClient.ServerUpdate += ExchangeClientServerUpdate;

            _exchangeClient.InitialiseExchange(0);
            //RefreshActiveServer();
            _iExchangeAdmin = (IExchangeAdmin)_exchangeClient;
        }

        #endregion

        #region private Functions

        private int SendSector201Comexchange(int datapak, int command, byte[] byteArray)
        {
            var sector201Data = new Sector201Data { Command = Convert.ToByte(command) };
            sector201Data.PutCommandDataVB(byteArray);
            _exchangeClient.RequestExWriteSector(datapak, 201, sector201Data);
            return _iExchangeAdmin.LastMessageID;
        }

        private void SendSector205Comexchange(int udp, string message)
        {
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
        }

        private void SendSector203Comexchange(int datapak, byte command)
        {
            var sector203Data = new Sector203Data { Command = command };
            _exchangeClient.RequestExWriteSector(datapak, 203, sector203Data);
        }

        private bool RefreshUDPList()
        {
            new Thread(() =>
            {
                lock (HoldingObject)
                {
                    _ackMessage = false;
                    Thread.MemoryBarrier();

                    _exchangeClient.RefreshActiveUPDs(GetServerName());
                    Monitor.Wait(HoldingObject, TimeSpan.FromSeconds(15));
                }
            }).Start();

            lock (HoldingObject)
            {
                Monitor.Wait(HoldingObject, 1000 * 10);
                return _ackMessage;
            }
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
            _exchangeClient.RefreshActiveServers();
        }

        #endregion

        #region eventHandlers

        void ExchangeClientServerUpdate()
        {
            _exchangeClient.RefreshActiveUPDs("");
        }

        void ExchangeClientUDPUpdate()
        {
            lock (HoldingObject)
            {
                _udpCollection = new List<string>();
                foreach (var udP in _exchangeClient.ActiveUDPs)
                    _udpCollection.Add(udP.ToString());

                _ackMessage = true;
                Thread.MemoryBarrier();
                Monitor.Pulse(HoldingObject);
            }
        }

        void ExchangeClientAck(MessageAck messageAck)
        {
            lock (HoldingObject)
            {
                _ackMessage = messageAck.ACK;
                Thread.MemoryBarrier();
                Monitor.Pulse(HoldingObject);
            }
        }

        void ExchangeClientExchangeSectorUpdate()
        {
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
                _ackMessage = true;
                Thread.MemoryBarrier();
                Monitor.Pulse(HoldingObject);
            }
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
            int messageID;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageID = SendSector201Comexchange(datapak, 0x94, byteArray);
            }
            return messageID;
        }

        /// <summary>
        /// Disables the machine.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal int DisableMachine(int datapak)
        {
            //if (!RefreshUDPList()) return false;
            int messageID;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageID = SendSector201Comexchange(datapak, 0x1, byteArray);
            }
            return messageID;
        }

        /// <summary>
        /// Disables the note acceptor.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal int DisableNoteAcceptor(int datapak)
        {
            //if (!RefreshUDPList()) return false;
            int messageID;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageID = SendSector201Comexchange(datapak, 0x7, byteArray);
            }
            return messageID;
        }

        /// <summary>
        /// Enables the machine.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal int EnableMachine(int datapak)
        {
            //if (!RefreshUDPList()) return false;
            int messageID;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageID = SendSector201Comexchange(datapak, 0x2, byteArray);
            }
            return messageID;
        }

        /// <summary>
        /// Enables the note acceptor.
        /// </summary>
        /// <param name="datapak">The datapak.</param>
        /// <returns></returns>
        internal int EnableNoteAcceptor(int datapak)
        {
            //if (!RefreshUDPList()) return false;
            int messageID;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageID = SendSector201Comexchange(datapak, 0x6, byteArray);
            }
            return messageID;
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

                _ackMessage = false;
                Thread.MemoryBarrier();

                _iExchangeAdmin.AddUDPToList(GetServerName(), barPositionPortNo, datapak, 0, 7);
                Monitor.Wait(HoldingObject, TimeSpan.FromSeconds(60));

                var result = _ackMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }
            return _ackMessage;
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

                _ackMessage = false;
                Thread.MemoryBarrier();

                _exchangeClient.RemoveUDPFromList(datapak,0);
                Monitor.Wait(HoldingObject, TimeSpan.FromSeconds(60));

                var result = _ackMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }
            return _ackMessage;
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
                _ackMessage = false;
                Thread.MemoryBarrier();

                SendSector203Comexchange(datapak, 112);
                Monitor.Wait(HoldingObject, TimeSpan.FromSeconds(60));

                var result = _ackMessage;               //Introducing Temp variable for using Memory Barrier See next Line`
                Thread.MemoryBarrier();

                if (!result) return false;
            }

            return _ackMessage;
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

            try
            {
                Marshal.ReleaseComObject(_exchangeClient);
                Marshal.ReleaseComObject(_iExchangeAdmin);
            }
            catch
            { }

            _iExchangeAdmin = null;
            _exchangeClient = null;
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
    }
}
