using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server
{
    public sealed class FFTransReceiver_SQLite
        : FFTransReceiver
    {
        private ProducerConsumerDequeueHandler<UdpFreeformEntity> _receive = null;

        public FFTransReceiver_SQLite(FFTransceiverArgs arg, IExecutorService executorService)
            : base(arg, executorService)
        {
            // create a listener thread and do the receive processing
            // create a new thread to receive from sqlite db
        }

        public override event ProducerConsumerDequeueHandler<UdpFreeformEntity> Receive
        {
            add { _receive += value; }
            remove { _receive -= value; }
        }

        protected override void OnReceiveFromSocket(UdpFreeformEntity udp)
        {
            // put it into sqlite db
        }

        private void OnListenReceiveFromSocket()
        {
            try
            {
                int waitTime = 100; // 100 milliseconds
                IExecutorService exec = this.ExecutorService;
                while (!exec.WaitForShutdown(100)) // Invokes after every  10 secs
                {
                    // rangesh starts here
                    try
                    {
                        // read from sqlite db
                        UdpFreeformEntity udp = null;//
                        this.RaiseReceiveEvent(udp);
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
    }
}
