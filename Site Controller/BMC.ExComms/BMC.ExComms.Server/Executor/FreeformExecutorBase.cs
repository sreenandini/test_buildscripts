using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Server.Handlers;
using BMC.ExComms.Server.ExecutionSteps;

namespace BMC.ExComms.Server.Executor
{
    internal abstract class FreeformExecutorBase
        : DisposableObject, IFreeformExecutor
    {
        private TaskItemCollection<TaskItem> g2hQueue = null;
        private TaskItemCollection<TaskItem> h2gQueue = null;

        //protected IFreeformHandler _handler = null;

        internal FreeformExecutorBase()
        {
            //_handler = new FreeformHandlerFactory();
        }

        public virtual bool ProcessMessage(FFMsg_G2H message)
        {
            bool retval = true;
            try
            {
                g2hQueue.AddToQueue(message);
            }
            catch (Exception ex)
            {
                retval = false;
                Log.Exception(ex);
            }
            return retval;
        }

        public virtual bool ProcessMessage(FFMsg_H2G message)
        {
            bool retval = true;
            try
            {
                h2gQueue.AddToQueue(message);
            }
            catch (Exception ex)
            {
                retval = false;
                Log.Exception(ex);
            }
            return retval;
        }

        public virtual bool Start(int g2hThreadCount, int h2gThreadCount)
        {
            bool retval = true;
            try
            {
                g2hQueue = new TaskItemCollection<TaskItem>(g2hThreadCount);
                g2hQueue.ProcessQueueItem += G2HQueue_ProcessQueueItem;
                g2hQueue.Initialize();

                h2gQueue = new TaskItemCollection<TaskItem>(h2gThreadCount);
                h2gQueue.ProcessQueueItem += H2GQueue_ProcessQueueItem;
                h2gQueue.Initialize();
            }
            catch (Exception ex)
            {
                retval = false;
                Log.Exception(ex);
            }
            return retval;
        }

        void G2HQueue_ProcessQueueItem(ICommsEntity queueItem)
        {
            //_handler.ProcessMessage(queueItem as FFMsg_G2H);
            //ExecutionStepFactory.Current.Execute(queueItem as FFMsg_G2H);
            FFMsgHandlerFactory.Current.Execute(queueItem as FFMsg_G2H);
        }

        void H2GQueue_ProcessQueueItem(ICommsEntity queueItem)
        {
            //_handler.ProcessMessage(queueItem as FFMsg_H2G);
            //ExecutionStepFactory.Current.Execute(queueItem as FFMsg_H2G);
            FFMsgHandlerFactory.Current.Execute(queueItem as FFMsg_H2G);
        }

        public virtual bool Stop()
        {
            bool retval = true;
            try
            {
                g2hQueue.StopAllThreads();
                h2gQueue.StopAllThreads();
            }
            catch (Exception ex)
            {
                retval = false;
                Log.Exception(ex);
            }
            return retval;
        }
    }
}
