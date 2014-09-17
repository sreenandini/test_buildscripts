using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO;

namespace BMC.ExComms.Server.Executor
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Concurrent;
    using BMC.ExComms.Contracts.DTO.Freeform;
    using BMC.ExComms.Contracts.DTO.Monitor;
    using BMC.CoreLib.Concurrent;

    public class TaskItem : ITaskItem
    {
        private FreeForm_LocalQueue<ICommsEntity> Q_local = null;
        private ManualResetEventSlim m_wait = null;
        private ManualResetEventSlim m_shutdown = null;
        private int _localQcount = 0;

        public event QueueProcessedHandler QueueProcessed;

        public event ProcessTaskQueueItem<ICommsEntity> ProcessQueueItem = null;

        public int TaskID
        {
            get;
            private set;
        }

        public bool IsAvailable
        {
            get
            {
                return (_localQcount == 0);
            }
        }

        public int InnerQueueCount
        {
            get
            {
                return _localQcount;
            }
        }

        public void AddDataItem(ICommsEntity Msg)
        {
            try
            {
                if (Q_local == null)
                    Q_local = new FreeForm_LocalQueue<ICommsEntity>();
                Q_local.Enqueue(Msg);
                Interlocked.Increment(ref _localQcount);
                m_wait.Set();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        public void StartCurrentTask()
        {
            try
            {
                m_wait = new ManualResetEventSlim(false);
                m_shutdown = new ManualResetEventSlim(false);
                Q_local = new FreeForm_LocalQueue<ICommsEntity>();

                Task t_item = FreeformTaskHelper.CreateAndExecuteTask(ExecuteLocalQueue, TaskCreationOptions.LongRunning);
                this.TaskID = t_item.Id;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        public void StopCurrentTask()
        {
            try
            {
                m_shutdown.Set();
                m_wait.Set();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        private void ExecuteLocalQueue()
        {
            try
            {
                while (!m_shutdown.Wait(1000))
                {
                    m_wait.Wait();
                    if (m_shutdown.IsSet) break; // shutdown called

                    if (Q_local.Count > 0)
                    {
                        ICommsEntity p_msg = null;
                        Q_local.TryDequeue(out p_msg);
                        this.ProcessQueueItem(p_msg);
                        Interlocked.Decrement(ref _localQcount);
                        QueueProcessed(new TaskItemEventArgs()
                        {
                            TaskID = this.TaskID,
                            Q_Count = _localQcount,
                            FFMessage = p_msg,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
