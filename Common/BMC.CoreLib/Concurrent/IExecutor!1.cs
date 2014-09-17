using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BMC.CoreLib.Concurrent
{
    public interface IExecutorKey : IDisposable
    {
        string UniqueKey { get; }
    }

    public interface IExecutorKeyThread : IExecutorKey
    {
        int GetThreadIndex(int capacity);
    }

    public interface IExecutorKeyFreeThread : IExecutorKey { }

    public interface IExecutorKeyResult : IExecutorKey
    {
        bool KeyResult { get; set; }
    }

    public interface IExecutor : IExecutorKey
    {
        EventWaitHandle WaitHandle { get; }
    }

    public interface IExecutor<T> : IExecutor
        where T : IExecutorKey
    {
        void QueueWorkerItem(T item);
    }

    public interface IThreadPoolExecutor<T>
        : IExecutor<T>
        where T : IExecutorKey
    {
        event ExecutorProcessItemHandler<T> ProcessItem;
        bool TrackItems { get; set; }
    }
    public abstract class ExecutorBase
       : ExecutorServiceBase, IExecutor
    {
        protected EventWaitHandle _mreShutdown = null;
        protected string _uniqueKey = string.Empty;

        public ExecutorBase(IExecutorService executorService)
            : base(executorService)
        {
            _mreShutdown = new ManualResetEvent(false);
        }

        #region IExecutor<T> Members

        public EventWaitHandle WaitHandle
        {
            get { return _mreShutdown; }
        }

        #endregion

        #region IExecutorKey Members

        public string UniqueKey
        {
            get { return _uniqueKey; }
        }

        #endregion

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            this.Shutdown();
        }

        protected void Shutdown()
        {
            _mreShutdown.Set();
            this.IsExecutorSignalled = true;
        }

        public bool IsExecutorSignalled { get; private set; }
    }

    public abstract class ExecutorBase<T>
        : ExecutorBase
        where T : IExecutorKey
    {
        public ExecutorBase(IExecutorService executorService)
            : base(executorService) { }

        #region IExecutor<T> Members

        public abstract void QueueWorkerItem(T item);

        #endregion
    }
}
