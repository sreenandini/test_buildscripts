using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Server.Executor
{
    public class TaskItemCollection<T>
        where T : ITaskItem, new()
    {
        private IDictionary<string, T> innerTaskItem;
        private FreeForm_FreeQueue<T> free_Q = new FreeForm_FreeQueue<T>();
        private int _ThreadCount;
        private Action a_stopthreads = null;

        public event QueueProcessedHandler TaskItemQueueProcessed;

        private ProcessTaskQueueItem<ICommsEntity> _processItem = null;

        public event ProcessTaskQueueItem<ICommsEntity> ProcessQueueItem
        {
            add { _processItem += value; }
            remove { _processItem -= value; }
        }

        private TaskItemCollection()
        { }

        public TaskItemCollection(int ThreadCount)
        {
            innerTaskItem = new Dictionary<string, T>();
            _ThreadCount = ThreadCount;
        }

        private void QueueProcessed(TaskItemEventArgs e)
        {
            try
            {
                if (e.Q_Count == 0)
                {
                    T t_item = (T)e.TaskItem;
                    if (t_item != null)
                    {
                        free_Q.Enqueue(t_item);
                        if (this.TaskItemQueueProcessed != null)
                        {
                            this.TaskItemQueueProcessed(e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        private T GetTaskItem(string Key)
        {
            T t_item = default(T);

            try
            {
                innerTaskItem.TryGetValue(Key, out t_item);
                if (t_item == null)
                {
                    if (free_Q.Count == 0)
                    {
                        t_item = this.FindSmallerQueue();
                    }
                    else
                    {
                        free_Q.TryDequeue(out t_item);
                    }
                    innerTaskItem.Add(Key, t_item);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return t_item;
        }

        private void Clear()
        {
            innerTaskItem.Clear();
        }

        private bool Contains(string Key)
        {
            return innerTaskItem.ContainsKey(Key);
        }

        private int Count
        {
            get { return innerTaskItem.Count(); }
        }

        private bool Remove(string Key)
        {
            if (innerTaskItem.ContainsKey(Key))
            {
                return innerTaskItem.Remove(Key);
            }
            return false;
        }

        private T FindByKey(string Key)
        {
            if (innerTaskItem.ContainsKey(Key))
            {
                return innerTaskItem[Key];
            }
            return default(T);
        }

        private T FindSmallerQueue()
        {
            return innerTaskItem.Values.OrderBy(o => o.InnerQueueCount).First();
        }

        public void Initialize()
        {
            try
            {
                for (int i = 0; i < _ThreadCount; i++)
                {
                    T t_item = new T();
                    t_item.ProcessQueueItem += _processItem;
                    t_item.QueueProcessed += QueueProcessed;
                    t_item.StartCurrentTask();
                    a_stopthreads += t_item.StopCurrentTask;
                    free_Q.Enqueue(t_item);
                }

            }
            catch (Exception ex)
            {
               Log.Exception(ex);
            }
        }

        public void AddToQueue(Contracts.DTO.Freeform.FreeformEntity_Msg G2H_Msg)
        {
            try
            {
                T t_item = this.GetTaskItem(G2H_Msg.InstallationNo.ToString());
                t_item.AddDataItem(G2H_Msg);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        public void StopAllThreads()
        {
           try
            {
                if (a_stopthreads != null)
                {
                    a_stopthreads();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }
    }
}