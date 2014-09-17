using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Net;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Transceiver
{
    public interface IFFTransceiver : IDisposable
    {
        event ProducerConsumerDequeueHandler<UdpFreeformEntity> Receive;
        bool Send(IFreeformEntity_Msg entity);
        bool Send(UdpFreeformEntity entity);
    }

    public class FFTransceiverArgs : DisposableObject
    {
        public string LocalIpAddress { get; set; }
        public int ReceivePortNo { get; set; }
        public int SendPortNo { get; set; }
        public string MulticastIpAddress { get; set; }
        public string InterfaceIpAddress { get; set; }
    }

    public static class FFTransceiverFactory
    {
        public static IFFTransceiver Create(FFTransceiverArgs args, IExecutorService executorService)
        {
            return new FFTransReceiver_InMemory(args, executorService);
        }
    }
}
