using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.TransReceiver;

namespace BMC.ExComms.Server
{
    public class FFTransReceiver_InMemory 
        : FFTransReceiver
    {
        private IProducerConsumerQueue<UdpFreeformEntity> _queue = null;

        public FFTransReceiver_InMemory(FFTransceiverArgs arg, IExecutorService executorService)
            : base(arg, executorService)
        {
            // create a listener thread and do the receive processing
            _queue = ProducerConsumerQueueFactory.Create<UdpFreeformEntity>(executorService, -1);
        }

        public override event ProducerConsumerDequeueHandler<UdpFreeformEntity> Receive
        {
            add { if (_queue != null) _queue.Dequeue += value; }
            remove { if (_queue != null) _queue.Dequeue -= value; }
        }

        protected override void OnReceiveFromSocket(UdpFreeformEntity udp)
        {
            _queue.Enqueue(udp);
        }
    }
}
