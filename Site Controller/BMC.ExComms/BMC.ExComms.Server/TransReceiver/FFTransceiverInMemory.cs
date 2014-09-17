using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Transceiver
{
    internal class FFTransReceiver_InMemory 
        : FFTransceiver
    {
        private readonly IProducerConsumerQueue<UdpFreeformEntity> _queue = null;

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
