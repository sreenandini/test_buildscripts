using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Concurrent
{
    /// <summary>
    /// Critical Section Interface
    /// </summary>
    public interface ICriticalSection : IDisposable
    {
        void Enter();
        void Leave();

        bool IsBusy { get; }
        event CriticalSectionStatusHandler NotifyStatus;
    }

    /// <summary>
    ///  Critical Section Status
    /// </summary>
    public enum CriticalSectionStatus
    {
        /// <summary>
        /// Acquired
        /// </summary>
        Acquired = 0,
        /// <summary>
        /// StillLocked
        /// </summary>
        StillLocked = 1,
        /// <summary>
        /// Released
        /// </summary>
        Released = 2
    }

    /// <summary>
    /// Critical Section Status Handler
    /// </summary>
    public delegate void CriticalSectionStatusHandler(CriticalSectionStatus status);

    /// <summary>
    ///  Critical Section Implementation
    /// </summary>
    public class CriticalSection : DisposableObject, ICriticalSection
    {
        [CLSCompliant(false)]
        protected object _syncRoot = new object();

        [CLSCompliant(false)]
        protected int _syncCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="CriticalSection"/> class.
        /// </summary>
        public CriticalSection() { }

        #region ICriticalSection Members

        /// <summary>
        /// Enters this instance.
        /// </summary>
        public void Enter()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Enter");

            try
            {
                if (_syncCount > 0)
                    this.OnNotifyStatus(CriticalSectionStatus.StillLocked);

                Monitor.Enter(_syncRoot);
                Interlocked.Increment(ref _syncCount);

                if (_syncCount == 1)
                    this.OnNotifyStatus(CriticalSectionStatus.Acquired);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Leaves this instance.
        /// </summary>
        public void Leave()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Enter");

            try
            {
                if (_syncCount > 0)
                {
                    Interlocked.Decrement(ref _syncCount);
                    if (_syncCount >= 0)
                    {
                        Monitor.Exit(_syncRoot);

                        if (_syncCount == 0)
                            this.OnNotifyStatus(CriticalSectionStatus.Released);
                        else
                            this.OnNotifyStatus(CriticalSectionStatus.StillLocked);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return (_syncCount > 0);
            }
        }

        public event CriticalSectionStatusHandler NotifyStatus = null;

        /// <summary>
        /// Called when [notify status].
        /// </summary>
        /// <param name="status">The status.</param>
        protected virtual void OnNotifyStatus(CriticalSectionStatus status)
        {
            if (this.NotifyStatus != null)
            {
                this.NotifyStatus(status);
            }
        }

        /// <summary>
        /// Overridable method which releases the managed resources.
        /// </summary>
        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            this.Leave();
        }

        #endregion
    }

    /// <summary>
    /// Auto Critical Section
    /// </summary>
    public sealed class AutoCS : DisposableObject
    {
        private ICriticalSection _syncObject = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCS"/> class.
        /// </summary>
        /// <param name="syncObject">The sync object.</param>
        private AutoCS(ICriticalSection syncObject)
        {
            _syncObject = syncObject;
        }

        /// <summary>
        /// Acquires the lock.
        /// </summary>
        /// <param name="syncObject">The sync object.</param>
        /// <returns></returns>
        public static AutoCS AcquireLock(ICriticalSection syncObject)
        {
            AutoCS cs = new AutoCS(syncObject);
            cs._syncObject.Enter();
            return cs;
        }

        /// <summary>
        /// Overridable method which releases the managed resources.
        /// </summary>
        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            if (_syncObject != null)
            {
                _syncObject.Leave();
            }
        }
    }
}
