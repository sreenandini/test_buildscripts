using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Net;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Simulator.Handlers
{
    public delegate void ReceiveUdpEntityDataHandler(UdpFreeformEntity udpEntity);

    public delegate void AfterSendUdpEntityDataHandler(UdpFreeformEntity udpEntity);

    public class SocketTransceiver
        : DisposableObject
    {
        private IUdpSocketReceiver _sockReceiver = null;
        private IUdpSocketTransmitter _sockTransmitter = null;
        protected readonly IExCommsServerConfigStore _configStore = ExCommsServerConfigStoreFactory.Store;

        public SocketTransceiver(IExecutorService executorService, string ipAddress, int portNo)
        {
            IUdpSocketTransceiver socket = UdpSocketTransceiverFactory.Create(executorService, new UdpSocketTransceiverParameter()
            {
                IPAddress = ipAddress,
                ListenWaitTime = 100,
                PortNo = portNo,
                UseInternalQueue = false,
            });
            _sockReceiver = socket;
            _sockTransmitter = socket;
            _sockReceiver.DataReceived += new UdpSocketReceivedDataHandler(OnReceiveUdpEntityFromSocket);
            Current = this;
        }

        public static SocketTransceiver Current { get; private set; }

        public event ReceiveUdpEntityDataHandler AfterSendUdpEntityData = null;

        public event ReceiveUdpEntityDataHandler ReceiveUdpEntityData = null;

        public void Start()
        {
            _sockReceiver.Start();
        }

        public void Stop()
        {
            _sockTransmitter.Stop();
        }

        public bool Send(IFreeformEntity_Msg request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Send"))
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

        public bool Send(UdpFreeformEntity request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Send"))
            {
                bool result = default(bool);

                try
                {
                    if (request == null)
                    {
                        method.Info("Unable to send message (request is empty).");
                        return false;
                    }

                    byte[] rawData = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, request);
                    if (rawData == null ||
                        rawData.Length == 0)
                    {
                        method.Info("Unable to send message (rawData was null or zero length).");
                        return false;
                    }

                    using (UdpSocketSendData sendData = new UdpSocketSendData()
                    {
                        IPAddress = Extensions.GetIpAddress(-1),
                        PortNo = _configStore.ReceivePortNo,
                        Buffer = rawData,
                    })
                    {
                        result = _sockTransmitter.Transmit(sendData);

                        if (this.AfterSendUdpEntityData != null)
                        {
                            byte[] rawDataWithoutIp = new byte[rawData.Length - 4];
                            Buffer.BlockCopy(rawData, 4, rawDataWithoutIp, 0, rawDataWithoutIp.Length);
                            request.RawData = rawDataWithoutIp;
                            request.ProcessDate = DateTime.Now;
                            this.AfterSendUdpEntityData(request);
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

        private void OnReceiveUdpEntityFromSocket(UdpSocketReceiveData message)
        {
            using (ILogMethod method = Log.LogMethod("", "OnReceiveUdpEntityFromSocket"))
            {
                try
                {
                    // create the raw entity message
                    string ipAddress = ((System.Net.IPEndPoint)message.RemoteEndpoint).Address.ToStringSafe();
                    method.InfoV("RECV_SOCK : Received from {0} / {1:D}",
                        ipAddress, message.BufferLength);
                    UdpFreeformEntity udpEntity = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.H2G, message.Buffer, ipAddress);
                    if (udpEntity == null ||
                        udpEntity.RawData == null ||
                        udpEntity.RawData.Length == 0)
                    {
                        method.Info("RECV_SOCK : Unable to parse the data from socket (Invalida message format).");
                        return;
                    }

                    // convert the freeform entity message
                    IFreeformEntity ffEntity = FreeformEntityFactory.CreateEntity(FF_FlowDirection.H2G, udpEntity);
                    if (ffEntity == null)
                    {
                        method.Info("RECV_SOCK : Unable to create the freeform entity from udp entity message.");
                        return;
                    }

                    // process the message
                    udpEntity.EntityData = ffEntity;
                    method.InfoV("RECV_SOCK (Success) : {0} [{1}] was received for processing.",
                        udpEntity, udpEntity.ProcessDate.ToStringSafe());

                    if (this.ReceiveUdpEntityData != null)
                    {
                        this.ReceiveUdpEntityData(udpEntity);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }
}
