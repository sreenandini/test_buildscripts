using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server
{
    public class TransReceiverHelpers
    {
        private 
            IProducerConsumerQueue<T>

        public static void AddReceiverToQueue(UdpFreeformEntity udpFreeformEntity)
        {
            BlockingCollection<UdpFreeformEntity>
            itemUdpFreeformEntities.Enqueue(udpFreeformEntity);
        }

        public static UdpFreeformEntity ReadFromQueue()
        {
            return itemUdpFreeformEntities.Dequeue();
        }


    }
}
