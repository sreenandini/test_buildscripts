/* ================================================================================= 
 * Purpose		:	Queue Process Manager Class
 * File Name	:   QueueProcessManager!1.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	13/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 13/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.Diagnostics
{
    /// <summary>
    /// Queue Process Manager Class
    /// </summary>
    /// <typeparam name="T">Type of the item</typeparam>
    [Serializable]
    public abstract class QueueProcessManager<T>
        : DisposableObjectLogger
    {
        #region Local Variables
        [NonSerialized]
        protected Queue<T> _queue = null;
        [NonSerialized]
        protected object _queueSyncRoot = null;
        [NonSerialized]
        protected Thread _thProcess = null;
        [NonSerialized]
        protected readonly int NOTIFY_ITEMADDED_TIMEOUT = 60 * 1000; // 1 Minute
        [NonSerialized]
        private QueueProcessMode _mode = QueueProcessMode.Synchronous;
        [NonSerialized]
        protected bool _stopProcessing = false;
        [NonSerialized]
        private ProcessQueueItemHandler<T> _logItemHandler = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueProcessManager"/> class.
        /// </summary>
        /// <param name="asyncHandler">if set to <c>true</c> [async handler].</param>
        protected QueueProcessManager(QueueProcessMode mode)
        {
            _mode = mode;
            this.StopProcessing = false;

            if (mode == QueueProcessMode.Thread)
            {
                _queue = new Queue<T>();
                _queueSyncRoot = ((ICollection)_queue).SyncRoot;
                _thProcess = Extensions.CreateThread(new ThreadStart(this.ProcessQueueItems));
                _thProcess.Start();
            }
            else if (mode == QueueProcessMode.Asynchronous)
            {
                _logItemHandler = new ProcessQueueItemHandler<T>(this.ProcessQueueItem);
            }
        }
        #endregion

        #region Fetching the items from queue
        /// <summary>
        /// Processes the queue item.
        /// </summary>
        protected virtual void ProcessQueueItems()
        {
            this.OnBeforeProcessQueueItems();

            try
            {
                while (!_stopProcessing)
                {
                    T item = default(T);
                    bool canProcess = false;
                    this.OnDuringProcessQueueItems();

                    // anything to process
                    lock (_queueSyncRoot)
                    {
                        int count = _queue.Count;
                        if (count > 0)
                        {
                            if (this.CanProcessTheQueueItems())
                            {
                                item = _queue.Dequeue();
                                canProcess = true;
                            }
                            else
                            {
                                this.RemoveFailedQueueItems();
                            }
                        }
                        else
                        {
                            this.OnProcessEmptyQueueItems();
                        }
                    }

                    // some thing to process
                    if (canProcess)
                    {
                        this.ProcessQueueItem(item);
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                this.OnErrorProcessQueueItems(ex);
            }
            finally
            {
                this.OnAfterProcessQueueItems();
            }
        }
        #endregion

        #region Methods called during the queue items processing
        /// <summary>
        /// Called when [before process queue items].
        /// </summary>
        protected virtual void OnBeforeProcessQueueItems() { }

        /// <summary>
        /// Called when [during process queue items].
        /// </summary>
        protected virtual void OnDuringProcessQueueItems() { }

        /// <summary>
        /// Removes the failed queue items.
        /// </summary>
        protected virtual void RemoveFailedQueueItems() { }

        /// <summary>
        /// Called when [process empty queue items].
        /// </summary>
        protected virtual void OnProcessEmptyQueueItems() { }

        /// <summary>
        /// Determines whether this instance [can process the queue items].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance [can process the queue items]; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool CanProcessTheQueueItems() { return true; }

        /// <summary>
        /// Called when [error process queue items].
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected virtual void OnErrorProcessQueueItems(Exception ex) { }

        /// <summary>
        /// Called when [after process queue items].
        /// </summary>
        protected virtual void OnAfterProcessQueueItems() { }

        /// <summary>
        /// Displays the wait message.
        /// </summary>
        protected virtual void DisplayWaitMessage() { }

        /// <summary>
        /// Peeks the queue item.
        /// </summary>
        /// <returns>Queue item.</returns>
        protected T PeekQueueItem()
        {
            return _queue.Peek();
        }

        /// <summary>
        /// Dequeues the queue item.
        /// </summary>
        /// <returns>Queue item.</returns>
        protected T DequeueQueueItem()
        {
            return _queue.Dequeue();
        }
        #endregion

        #region Processing queue item
        /// <summary>
        /// Processes the queue item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected abstract void ProcessQueueItem(T item);

        /// <summary>
        /// Adds the item to queue.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void AddItemToQueue(T item)
        {
            if (_mode == QueueProcessMode.Thread)
            {
                _queue.Enqueue(item);
            }
            else if (_mode == QueueProcessMode.Asynchronous)
            {
                //_logItemHandler.BeginInvoke(item, null, null);
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    this.ProcessQueueItem((T)o);
                }, item);
            }
            else
            {
                this.ProcessQueueItem(item);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [stop processing].
        /// </summary>
        /// <value><c>true</c> if [stop processing]; otherwise, <c>false</c>.</value>
        protected bool StopProcessing
        {
            get { return _stopProcessing; }
            set { _stopProcessing = value; }
        }
        #endregion

        #region Cleaning
        /// <summary>
        /// Overridable method which releases the managed resources.
        /// </summary>
        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            if (_mode == QueueProcessMode.Thread)
            {
                _thProcess.WaitForThreadFinish();
            }
        }
        #endregion
    }

    [Serializable]
    public abstract class QueueProcessManagerEx<T>
        : ExecutorServiceBase
    {
        #region Local Variables
        [NonSerialized]
        protected IProducerConsumerQueue<T> _queue = null;
        [NonSerialized]
        private QueueProcessMode _mode = QueueProcessMode.Synchronous;
        #endregion

        #region Constructors
        protected QueueProcessManagerEx(QueueProcessMode mode, IExecutorService executorService)
            : this(mode, executorService, -1, 10, false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueProcessManager"/> class.
        /// </summary>
        /// <param name="asyncHandler">if set to <c>true</c> [async handler].</param>
        protected QueueProcessManagerEx(QueueProcessMode mode, IExecutorService executorService, int capacity, int queueTimeout, bool isLogging)
            : base(executorService)
        {
            _mode = mode;

            if (mode == QueueProcessMode.Thread)
            {
                _queue = ProducerConsumerQueueFactory.Create<T>(executorService, capacity, queueTimeout, isLogging);
                _queue.Dequeue += OnQueue_Dequeue;
            }
        }
        #endregion

        #region Processing queue item
        /// <summary>
        /// Processes the queue item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected abstract void ProcessQueueItem(T item);

        /// <summary>
        /// Adds the item to queue.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void AddItemToQueue(T item)
        {
            if (_mode == QueueProcessMode.Thread)
            {
                _queue.Enqueue(item);
            }
            else if (_mode == QueueProcessMode.Asynchronous)
            {
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    this.ProcessQueueItem((T)o);
                }, item);
            }
            else
            {
                this.ProcessQueueItem(item);
            }
        }

        private void OnQueue_Dequeue(T item)
        {
            this.ProcessQueueItem(item);
        }

        #endregion

        #region Cleaning
        /// <summary>
        /// Overridable method which releases the managed resources.
        /// </summary>
        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            if (_mode == QueueProcessMode.Thread)
            {
                this.ExecutorService.WaitForShutdown(120000); // 2 Minutes
            }
        }
        #endregion
    }
}
