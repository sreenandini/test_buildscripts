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

namespace BMC.ExComms.Server
{
    public interface IFFTransReceiver : IDisposable
    {
        event ProducerConsumerDequeueHandler<UdpFreeformEntity> Receive;
        bool Send(UdpFreeformEntity entity);
    }

    public class FFTransceiverArgs : DisposableObject
    {
        public IPAddress LocalIpAddress { get; set; }
        public int ReceivePortNo { get; set; }
        public int SendPortNo { get; set; }
        public IPAddress MulticastIpAddress { get; set; }
        public IPAddress InterfaceIpAddress { get; set; }
    }
}
