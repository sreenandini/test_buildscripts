using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Net;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO.Freeform;
using System.Collections;

namespace BMC.ExComms.Server.Transceiver
{
    internal abstract class FFTransceiver
        : ExecutorServiceBase, IFFTransceiver
    {
        private static ILogger LOGGER = Log.AddFileLogger("BMC_ExComms_Transceiver");
        private static ILogger LOGGER_UDP_RECV = Log.AddFileLogger("BMC_ExComms_UdpRecv");
        private static ILogger LOGGER_UDP_SEND = Log.AddFileLogger("BMC_ExComms_UdpSend");

        private IUdpSocketReceiver _sockReceiver = null;
        private IUdpSocketTransmitter _sockTransmitter = null;
        private FFTransceiverArgs _arg = null;
        protected readonly IExCommsServerConfigStore _configStore = ExCommsServerConfigStoreFactory.Store;

        protected FFTransceiver(FFTransceiverArgs arg, IExecutorService executorService)
            : base(executorService)
        {
            _arg = arg;
            this.Initialize();
        }

        public virtual event ProducerConsumerDequeueHandler<UdpFreeformEntity> Receive;

        private void Initialize()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Initialize"))
            {
                try
                {
                    // transceiver socket for sending and receiving freeform messages
                    IUdpSocketTransceiver socket = UdpSocketTransceiverFactory.Create(this.ExecutorService, new UdpSocketTransceiverParameter()
                    {
                        IPAddress = !_arg.LocalIpAddress.IsEmpty() ? _arg.LocalIpAddress : IPAddress.Any.ToString(),
                        ListenWaitTime = 100,
                        InterfaceIP = _arg.InterfaceIpAddress,
                        MulticastIP = _arg.MulticastIpAddress,
                        PortNo = _arg.ReceivePortNo,
                        UseInternalQueue = false,
                    });
                    _sockReceiver = socket;
                    _sockTransmitter = socket;
                    _sockReceiver.DataReceived += new UdpSocketReceivedDataHandler(OnReceiveUdpEntityFromSocket);
                    _sockReceiver.Start();
                    method.Info("Initialize (Success)");
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public bool Send(IFreeformEntity_Msg request)
        {
            using (ILogMethod method = Log.LogMethod(LOGGER, this.DYN_MODULE_NAME, "Send"))
            {
                bool result = default(bool);

                try
                {
                    if (request == null)
                    {
                        method.Info("Unable to send message. request is empty");
                        return false;
                    }

                    using (UdpFreeformEntity entity = new UdpFreeformEntity())
                    {
                        entity.Address = request.IpAddress2;
                        entity.EntityData = request;
                        result = this.Send(entity);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public bool Send(UdpFreeformEntity udpEntity)
        {
            using (ILogMethod method = Log.LogMethod(LOGGER, this.DYN_MODULE_NAME, "Send"))
            {
                bool result = default(bool);

                try
                {
                    if (udpEntity == null)
                    {
                        method.Info("Unable to send message (request is empty).");
                        return false;
                    }

                    byte[] rawData = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.H2G, udpEntity);
                    if (rawData == null ||
                        rawData.Length == 0)
                    {
                        method.Info("Unable to send message (rawData was null or zero length).");
                        return false;
                    }

                    if (_configStore.LogRawFreeformMessages)
                    {
                        udpEntity.ProcessDate = DateTime.Now;
                        string udpEntityString = udpEntity.ToStringRaw(FF_FlowDirection.H2G);
                        Log.Info(udpEntityString);
                        LOGGER.WriteLogInfo(udpEntityString + Environment.NewLine);
                        LOGGER_UDP_SEND.WriteLogInfo(udpEntity.ProcessDate.ToStringSafe());
                        LOGGER_UDP_SEND.WriteLogInfo(udpEntityString + Environment.NewLine);
                    }

                    using (UdpSocketSendData sendData = new UdpSocketSendData()
                    {
                        IPAddress = udpEntity.Address,
                        PortNo = _configStore.TransmitPortNo,
                        Buffer = rawData,
                    })
                    {
                        result = _sockTransmitter.Transmit(sendData);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        private void OnReceiveUdpEntityFromSocket(UdpSocketReceiveData message)
        {
            using (ILogMethod method = Log.LogMethod(LOGGER, this.DYN_MODULE_NAME, "OnReceiveUdpEntityFromSocket"))
            {
                try
                {
                    // create the raw entity message
                    method.InfoV("RECV_SOCK : Received from {0} / {1:D}",
                        message.RemoteEndpoint.ToString(), message.BufferLength);
                    UdpFreeformEntity udpEntity = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, message.Buffer);
                    if (udpEntity == null ||
                        udpEntity.RawData == null ||
                        udpEntity.RawData.Length == 0)
                    {
                        method.Info("RECV_SOCK : Unable to parse the data from socket (Invalida message format).");
                        if (udpEntity != null &&
                            udpEntity.Address != null)
                        {
                            // send the nack
                            this.Send(FreeformEntityFactory.CreateH2GMessageAckNack(udpEntity.AddressString, true));
                        }
                        return;
                    }

                    // convert the freeform entity message
                    FFMsg_G2H g2hMessage = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, udpEntity) as FFMsg_G2H;
                    if (g2hMessage == null)
                    {
                        method.Info("RECV_SOCK : Unable to create the freeform entity from udp entity message.");
                        // send the nack
                        this.Send(FreeformEntityFactory.CreateH2GMessageAckNack(udpEntity.AddressString, true));
                        return;
                    }

                    // send the ack message (if necessary)
                    FFMsg_H2G msgAck = FreeformEntityFactory.CreateH2GMessageAckNack(udpEntity.AddressString, g2hMessage.Command);
                    if (msgAck != null)
                    {
                        this.Send(msgAck);
                    }

                    // process the message
                    udpEntity.EntityData = g2hMessage;
                    method.InfoV("RECV_SOCK (Success) : {0} [{1}] was received for processing.",
                        udpEntity, udpEntity.ProcessDate.ToStringSafe());
                    if (_configStore.LogRawFreeformMessages)
                    {
                        string udpEntityString = udpEntity.ToStringRaw(FF_FlowDirection.G2H);
                        Log.Info(udpEntityString);
                        LOGGER.WriteLogInfo(udpEntityString + Environment.NewLine);
                        LOGGER_UDP_RECV.WriteLogInfo(udpEntity.ProcessDate.ToStringSafe());
                        LOGGER_UDP_RECV.WriteLogInfo(udpEntityString + Environment.NewLine);
                    }
                    this.OnReceiveFromSocket(udpEntity);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        protected void RaiseReceiveEvent(UdpFreeformEntity udp)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "RaiseReceiveEvent"))
            {
                try
                {
                    if (this.Receive != null)
                    {
                        this.Receive(udp);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        protected abstract void OnReceiveFromSocket(UdpFreeformEntity udp);
    }
}