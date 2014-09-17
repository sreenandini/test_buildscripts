using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Freeform;
using System.Collections;

namespace BMC.ExComms.Server
{
    public abstract class FFTransReceiver
        : ExecutorServiceBase, IFFTransReceiver
    {
        private FFTransceiverArgs _arg = null;
        private UdpClient _udpClient = new UdpClient();

        protected FFTransReceiver(FFTransceiverArgs arg, IExecutorService executorService)
            : base(executorService)
        {
            _arg = arg;
            this.Initialize();

            // receive from socket
            new Thread(new ThreadStart(OnListenReceiveFromSocket))
            {
                IsBackground = true,
            }.Start();
        }

        public virtual event ProducerConsumerDequeueHandler<UdpFreeformEntity> Receive;

        private void Initialize()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, _arg.ReceivePortNo);
                _udpClient.Client.Bind(endPoint);
                if (_arg.MulticastIpAddress != null && _arg.InterfaceIpAddress != null)
                    _udpClient.JoinMulticastGroup(_arg.MulticastIpAddress, _arg.InterfaceIpAddress);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool Send(UdpFreeformEntity entity)
        {
            bool result = false;

            try
            {
                byte[] buffer = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, entity.EntityData);
                _udpClient.Send(buffer, buffer.Length);
                result = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        private void OnListenReceiveFromSocket()
        {
            try
            {
                int waitTime = 100; // 100 milliseconds
                IPEndPoint remoteEndPoint = null;
                IExecutorService exec = this.ExecutorService;
                while (!exec.WaitForShutdown(100)) // Invokes after every  10 secs
                {
                    // rangesh starts here
                    try
                    {
                        IAsyncResult ahReceive = _udpClient.BeginReceive(null, null);
                        int idx = exec.WaitAny(ahReceive.AsyncWaitHandle, -1);
                        if (idx == 0) break;

                        byte[] rawData = _udpClient.EndReceive(ahReceive, ref remoteEndPoint);
                        if (rawData != null && rawData.Length > 0)
                        {
                            UdpFreeformEntity udp = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, rawData);
                            IFreeformEntity freeformEntity = FreeformEntityFactory.CreateEntity(FF_FlowDirection.G2H, udp);
                            udp.EntityData = freeformEntity;
                            this.OnReceiveFromSocket(udp);
                        }
                    }
                    catch (Exception e)
                    {
                        ExceptionManager.Publish(e);
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionManager.Publish(e);
            }
        }

        protected void RaiseReceiveEvent(UdpFreeformEntity udp)
        {
            if (this.Receive != null)
            {
                this.Receive(udp);
            }
        }

        protected abstract void OnReceiveFromSocket(UdpFreeformEntity udp);
    }
}