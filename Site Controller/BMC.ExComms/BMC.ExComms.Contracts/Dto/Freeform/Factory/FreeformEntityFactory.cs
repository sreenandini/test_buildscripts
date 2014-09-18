using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.IoC;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public delegate IFreeformEntity_MsgTgt FFTgtParseBufferHandler(IFreeformEntity_MsgTgt target, int id, int length, byte[] data);

    public enum MasterCOMVersions
    {
        MC300 = 0,
        MC350 = 1,
        MC400 = 2,
        SoftGMU = 3,
    }

    public class FFCreateEntityRequest : DisposableObject
    {
        public string IPAddress { get; set; }
        public bool IsSecured { get; set; }
        public bool IsResponseRequired { get; set; }
        public bool SkipTransactionId { get; set; }
    }

    public interface IFFFactory : IDisposable
    {
        FF_FlowDirection Direction { get; }
        IFreeformEntity CreateEntity(byte[] buffer, object extra);
        byte[] CreateBuffer(IFreeformEntity entity);
        IFreeformEntity CreateEntity(FFCreateEntityRequest request);
    }

    internal abstract class FFFactory : DisposableObject, IFFFactory
    {
        protected IFFMsgParser _messageParser = null;
        protected IFFTgtParser _targetParser = null;

        internal FFFactory() { }

        public abstract FF_FlowDirection Direction { get; }

        public IFreeformEntity CreateEntity(byte[] buffer, object extra)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreateEntity"))
            {
                IFreeformEntity result = default(IFreeformEntity);

                try
                {
                    result = this.CreateEntityInternal(buffer, extra);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        internal abstract IFreeformEntity CreateEntityInternal(byte[] buffer, object extra);

        public IFreeformEntity CreateEntity(FFCreateEntityRequest request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreateEntity"))
            {
                IFreeformEntity result = default(IFreeformEntity);

                try
                {
                    result = this.CreateEntityInternal(request);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        internal abstract IFreeformEntity CreateEntityInternal(FFCreateEntityRequest request);

        public byte[] CreateBuffer(IFreeformEntity entity)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreateEntity"))
            {
                byte[] result = default(byte[]);

                try
                {
                    result = this.CreateBufferInternal(entity);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        internal abstract byte[] CreateBufferInternal(IFreeformEntity entity);
    }

    public static class FreeformEntityFactory
    {
        private class _GMU_Factory : DisposableObject
        {
            private IDictionary<FF_FlowDirection, IFFFactory> _factories = null;

            public _GMU_Factory()
            {
                _factories = new SortedDictionary<FF_FlowDirection, IFFFactory>();
            }

            public IFFFactory this[FF_FlowDirection direction]
            {
                get
                {
                    if (_factories.ContainsKey(direction))
                        return _factories[direction];
                    return null;
                }
                set
                {
                    if (!_factories.ContainsKey(direction))
                    {
                        _factories.Add(direction, value);
                    }
                }
            }
        }

        private const string DYN_MODULE_NAME = "FreeformEntityFactory";
        private static readonly IPAddress NO_IPADDRESS = IPAddress.None;
        private static readonly string NO_IPADDRESS_STRING = IPAddress.None.ToString();

        private static object _factoryLock = new object();
        [ThreadStatic]
        private static IDictionary<FF_FlowDirection, IDictionary<MasterCOMVersions, IFFFactory>> _factoryInstances = null;
        [ThreadStatic]
        private static IFFFactory _defaultFactory_G2H = null;
        [ThreadStatic]
        private static IFFFactory _defaultFactory_H2G = null;
        [ThreadStatic]
        private static IDictionary<string, _GMU_Factory> _gmuFactories = null;

        private static readonly int[] _headerLengths = null;
        private static readonly bool[] _hasIPAddresses = null;

        static FreeformEntityFactory()
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, ".cctor()"))
            {
                try
                {
                    _headerLengths = new int[] {
                        FreeformConstants.LEN_GMU_IPADDRESS + FreeformConstants.LEN_G2H_LEN,
                        FreeformConstants.LEN_H2G_LEN,
                    };
                    _hasIPAddresses = new bool[] { true, false };
                    InitEncryptionFactory();
                    GetGMUFactory(NO_IPADDRESS_STRING);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public static IFreeformEncryptionFactory EncryptionFactory { get; set; }

        private static void Initialize()
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "GetGMUFactory"))
            {
                try
                {
                    _factoryInstances = new SortedDictionary<FF_FlowDirection, IDictionary<MasterCOMVersions, IFFFactory>>()
                    {
                            { 
                                FF_FlowDirection.G2H, 
                                new SortedDictionary<MasterCOMVersions, IFFFactory>() 
                                {
                                    { MasterCOMVersions.MC300, new FFFactory_MC300_G2H() },
                                    { MasterCOMVersions.MC350, new FFFactory_MC350_G2H() },
                                    { MasterCOMVersions.MC400, new FFFactory_MC400_G2H() },
                                    { MasterCOMVersions.SoftGMU, new FFFactory_SoftGMU_G2H() },
                                }
                            },
                            { 
                                FF_FlowDirection.H2G, 
                                new SortedDictionary<MasterCOMVersions, IFFFactory>() 
                                {
                                    { MasterCOMVersions.MC300, new FFFactory_MC300_H2G() },
                                    { MasterCOMVersions.MC350, new FFFactory_MC350_H2G() },
                                    { MasterCOMVersions.MC400, new FFFactory_MC400_H2G() },
                                    { MasterCOMVersions.SoftGMU, new FFFactory_SoftGMU_H2G() },
                                }
                            }
                    };

                    _gmuFactories = new StringConcurrentDictionary<_GMU_Factory>();
                    _defaultFactory_G2H = _factoryInstances[FF_FlowDirection.G2H][MasterCOMVersions.MC300];
                    _defaultFactory_H2G = _factoryInstances[FF_FlowDirection.H2G][MasterCOMVersions.MC300];
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private static void InitEncryptionFactory()
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "Method"))
            {
                try
                {
                    EncryptionFactory = MEFHelper.GetExportedValue<IFreeformEncryptionFactory>("FreeformEncryptionFactory");
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private static _GMU_Factory GetGMUFactory(string ipAddress)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "GetGMUFactory"))
            {
                _GMU_Factory result = default(_GMU_Factory);

                try
                {
                    if (_factoryInstances == null)
                    {
                        lock (_factoryLock)
                        {
                            if (_factoryInstances == null)
                            {
                                Initialize();
                            }
                        }
                    }

                    if (ipAddress.IsEmpty())
                    {
                        ipAddress = NO_IPADDRESS_STRING;
                    }
                    if (!_gmuFactories.ContainsKey(ipAddress))
                    {
                        _gmuFactories.Add(ipAddress, result = new _GMU_Factory());
                        _gmuFactories[ipAddress][FF_FlowDirection.G2H] = _defaultFactory_G2H;
                        _gmuFactories[ipAddress][FF_FlowDirection.H2G] = _defaultFactory_H2G;
                    }
                    else
                    {
                        result = _gmuFactories[ipAddress];
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        /// <summary>
        /// Creates the raw freeform message from the given buffer.
        /// </summary>
        /// <param name="direction">The flow direction.</param>
        /// <param name="buffer">The given buffer.</param>
        /// <param name="givenAddress">The given address.</param>
        /// <returns>
        /// Raw freeform entity.
        /// </returns>
        public static UdpFreeformEntity CreateUdpEntity(FF_FlowDirection direction, byte[] buffer, string givenAddress)
        {
            using (ILogMethod method = Log.LogMethod("FreeformEntityFactory", "CreateUdpEntity"))
            {
                UdpFreeformEntity result = default(UdpFreeformEntity);

                try
                {
                    int minLength = _headerLengths[(int)direction];
                    int hasIPAddress = _headerLengths[(int)direction];
                    int ipOffset = 0;
                    int ipLength = 0;

                    // invalid message
                    if (buffer == null ||
                        buffer.Length < minLength)
                    {
                        Log.Info("Parsing buffer (Failure) : Buffer was not in expected length.");
                        return result;
                    }
                    method.Info("Parsing buffer (Success)");

                    // ip address
                    string ipAddress2 = givenAddress;
                    IPAddress ipAddress = null;
                    if (givenAddress.IsEmpty())
                    {
                        ipLength = FreeformConstants.LEN_GMU_IPADDRESS;
                        ipAddress2 = string.Format("{0:D}.{1:D}.{2:D}.{3:D}",
                            buffer[0], buffer[1],
                            buffer[2], buffer[3]);
                        ipOffset += ipLength;
                    }

                    // parse the ip address
                    if (!IPAddress.TryParse(ipAddress2, out ipAddress))
                    {
                        method.Info("Parsing ip address (Failure) : Invalid ipaddress from the buffer.");
                        return result;
                    }
                    method.Info("Parsing ip address (Success)");

                    // get the factory
                    int dataLength = (buffer.Length - ipLength);
                    _GMU_Factory gmuFactory = GetGMUFactory(ipAddress2);

                    // raw data
                    byte[] dataWithoutIP = new byte[dataLength];
                    Buffer.BlockCopy(buffer, ipOffset, dataWithoutIP, 0, dataWithoutIP.Length);
                    result = new UdpFreeformEntity()
                    {
                        Address = ipAddress,
                        RawData = dataWithoutIP,
                        ProcessDate = DateTime.Now,
                    };
                    method.Info("Received ip address : " + ipAddress + ", Data Length : " + dataWithoutIP.Length);
                }
                catch (Exception ex)
                {
                    Log.Exception(method.PROC, ex);
                }

                return result;
            }
        }

        public static UdpFreeformEntity CreateUdpEntity(FF_FlowDirection direction, byte[] buffer)
        {
            return CreateUdpEntity(direction, buffer, string.Empty);
        }

        public static T CreateEntity<T>(FF_FlowDirection direction, byte[] buffer, string givenAddress)
            where T : IFreeformEntity_Msg
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreateEntity"))
            {
                IFreeformEntity result = default(IFreeformEntity);

                try
                {
                    UdpFreeformEntity udpEntity = CreateUdpEntity(direction, buffer, givenAddress);
                    if (udpEntity != null)
                    {
                        result = CreateEntity(direction, udpEntity);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return (T)result;
            }
        }

        public static T CreateEntity<T>(FF_FlowDirection direction, byte[] buffer)
            where T : IFreeformEntity_Msg
        {
            return CreateEntity<T>(direction, buffer, string.Empty);
        }

        /// <summary>
        /// Creates the freeform entity from given buffer.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="udpEntity">The UDP entity.</param>
        /// <returns>
        /// Freeform entity.
        /// </returns>
        public static IFreeformEntity CreateEntity(FF_FlowDirection direction, UdpFreeformEntity udpEntity)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreateEntity"))
            {
                IFreeformEntity result = default(IFreeformEntity);

                try
                {
                    // get the gmu factory for this ip address
                    _GMU_Factory gmuFactory = GetGMUFactory(udpEntity.AddressString);
                    if (gmuFactory != null &&
                        udpEntity.RawData != null)
                    {
                        result = gmuFactory[direction].CreateEntity(udpEntity.RawData, udpEntity.AddressString);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        /// <summary>
        /// Creates the raw freeform buffer from the given udp entity.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="udpEntity">The UDP entity.</param>
        /// <returns>Raw freeform buffer.</returns>
        public static byte[] CreateBuffer(FF_FlowDirection direction, UdpFreeformEntity udpEntity)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreateEntity"))
            {
                byte[] result = default(byte[]);

                try
                {
                    // get the gmu factory for the given gmu ip adddress
                    _GMU_Factory gmuFactory = GetGMUFactory(udpEntity.AddressString);
                    if (gmuFactory != null)
                    {
                        if (udpEntity.EntityData != null)
                        {
                            result = gmuFactory[direction].CreateBuffer(udpEntity.EntityData);
                            udpEntity.RawData = result;
                        }
                        else if (udpEntity.RawData != null)
                        {
                            result = udpEntity.RawData;
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        /// <summary>
        /// Creates the raw freeform buffer from the given entity.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Raw freeform buffer.
        /// </returns>
        public static byte[] CreateBuffer(FF_FlowDirection direction, IFreeformEntity entity)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreateEntity"))
            {
                IPAddress ipAddress = IPAddress.None;
                if (entity is IFreeformEntity_Msg) ipAddress = ((IFreeformEntity_Msg)entity).IpAddress2;

                return CreateBuffer(direction,
                    new UdpFreeformEntity()
                    {
                        Address = ipAddress,
                        EntityData = entity,
                    });
            }
        }

        public static byte[] CreateBuffer(IFreeformEntity_Msg entity)
        {
            return CreateBuffer(entity.FlowDirection, entity);
        }

        public static string CreateBufferHexString(FF_FlowDirection direction, IFreeformEntity entity)
        {
            return CreateBuffer(direction, entity).GetConvertBytesToHexString(string.Empty);
        }

        public static T CreateEntity<T>(FF_FlowDirection direction, FFCreateEntityRequest request)
            where T : IFreeformEntity
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreateG2HEntity"))
            {
                T result = default(T);

                try
                {
                    _GMU_Factory gmuFactory = GetGMUFactory(request.IPAddress);
                    if (gmuFactory != null)
                    {
                        result = (T)gmuFactory[direction].CreateEntity(request);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public static FFMsg_G2H CreateG2HMessage(string ipAddress, FF_AppId_G2H_Commands command, FF_AppId_G2H_MessageTypes messageType,
                                                 FF_AppId_SessionIds sessionId,
                                                 params IFreeformEntity_MsgTgt[] targets)
        {
            return CreateG2HMessage(ipAddress, command, messageType, sessionId, false, targets);
        }

        public static FFMsg_G2H CreateG2HMessage(string ipAddress, FF_AppId_G2H_Commands command, FF_AppId_G2H_MessageTypes messageType,
                                                 FF_AppId_SessionIds sessionId, bool isResponseRequired,
                                                 params IFreeformEntity_MsgTgt[] targets)
        {
            FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                               new FFCreateEntityRequest_G2H_ResponseRequired()
                               {
                                   Command = command,
                                   MessageType = messageType,
                                   SessionID = sessionId,
                                   IPAddress = ipAddress,
                                   SkipTransactionId = true,
                                   IsResponseRequired = isResponseRequired,
                               });
            if (targets != null)
            {
                msg.AddTargets(targets);
            }
            return msg;
        }

        public static FFMsg_H2G CreateH2GMessage(string ipAddress, FF_AppId_H2G_PollCodes pollcode,
                                                 FF_AppId_SessionIds sessionId,
                                                 params IFreeformEntity_MsgTgt[] targets)
        {
            return CreateH2GMessage(ipAddress, pollcode, sessionId, false, targets);
        }

        public static FFMsg_H2G CreateH2GMessage(string ipAddress, FF_AppId_H2G_PollCodes pollcode,
                                                 FF_AppId_SessionIds sessionId, bool isResponseRequired,
                                                 params IFreeformEntity_MsgTgt[] targets)
        {
            FFMsg_H2G msg = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
                               new FFCreateEntityRequest_H2G_ResponseRequired()
                               {
                                   PollCode = pollcode,
                                   SessionID = sessionId,
                                   IPAddress = ipAddress,
                                   SkipTransactionId = true,
                                   IsResponseRequired = isResponseRequired,
                               });
            if (targets != null)
            {
                msg.AddTargets(targets);
            }
            return msg;
        }

        public static FFMsg_H2G CreateH2GMessageAckNack(string ipAddress, FF_AppId_G2H_Commands command) {
            switch (command)
            {
                case FF_AppId_G2H_Commands.NACK:
                    return CreateH2GMessageAckNack(ipAddress, true);

                case FF_AppId_G2H_Commands.GMUInitA0:
                    return CreateH2GMessageAckNack(ipAddress, false);

                default:
                    return null;
            }
        }

        public static FFMsg_H2G CreateH2GMessageAckNack(string ipAddress, bool nack)
        {
            FFMsg_H2G msg = FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
                               new FFCreateEntityRequest_H2G()
                               {
                                   PollCode = FF_AppId_H2G_PollCodes.Override,
                                   SessionID = FF_AppId_SessionIds.Override,
                                   IPAddress = ipAddress,
                                   SkipTransactionId = true,
                                   IsResponseRequired = false,
                               });
            msg.AddTarget(new FFTgt_H2G_AckNack()
            {
                Nack = nack,
            });
            return msg;
        }
    }
}
