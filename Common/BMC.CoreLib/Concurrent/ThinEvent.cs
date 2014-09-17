using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BMC.CoreLib.Concurrent
{
    public interface IThinEvent
    {
        bool IsCompleted { get; }
        bool IsSignalled { get; }

        void Set();
        void Reset();
        bool Wait();
        bool Wait(TimeSpan timeout);
        bool Wait(TimeSpan timeout, CancellationToken ctk);
        bool Wait(int timeout, CancellationToken ctk);
        bool Wait(IExecutorService executorService);
        WaitHandle WaitHandle { get; }
    }

    public class ThinEvent : DisposableObject, IThinEvent
    {
        private volatile int _status = 0;
        private ManualResetEvent _mre = null;

        public ThinEvent(bool signalled)
        {
            _mre = new ManualResetEvent(false);
            _status = 1;
            if (signalled) this.Set();
        }

        public void Set()
        {
            try
            {
                if (_status != 2)
                {
                    _mre.Set();
#pragma warning disable 0420
                    Interlocked.CompareExchange(ref _status, 2, 1);
#pragma warning restore 0420
                }
            }
            catch { }
        }

        public void Reset()
        {
            try
            {
                if (_status != 1)
                {
                    _mre.Reset();
#pragma warning disable 0420
                    Interlocked.CompareExchange(ref _status, 1, 2);
#pragma warning restore 0420
                }
            }
            catch { }
        }

        public bool IsCompleted
        {
            get
            {
                return (_status == 2);
            }
        }

        public bool IsSignalled
        {
            get
            {
                return this.IsCompleted;
            }
        }

        public bool Wait()
        {
            return _mre.WaitOne();
        }

        public bool Wait(TimeSpan timeout)
        {
            return _mre.WaitOne(timeout);
        }

        public WaitHandle WaitHandle
        {
            get { return _mre; }
        }

        public bool Wait(TimeSpan timeout, CancellationToken ctk)
        {
            return _mre.WaitOne(timeout);
        }

        public bool Wait(int timeout, CancellationToken ctk)
        {
            return _mre.WaitOne(timeout);
        }

        public bool Wait(IExecutorService executorService)
        {
            return (executorService.WaitAny(this.WaitHandle, -1) == 1);
        }
    }

    public class ThinEventSlim : DisposableObject, IThinEvent
    {
        private ManualResetEventSlim _mre = null;
        private object _lockObject = new object();

        public ThinEventSlim(bool signalled)
        {
            _mre = new ManualResetEventSlim(false);
            if (signalled) this.Set();
        }

        public void Set()
        {
            try
            {
                if (!_mre.IsSet)
                {
                    _mre.Set();
                }
            }
            catch { }
        }

        public void Reset()
        {
            try
            {
                if (_mre.IsSet)
                {
                    _mre.Reset();
                }
            }
            catch { }
        }

        public bool IsCompleted
        {
            get
            {
                return _mre.IsSet;
            }
        }

        public bool IsSignalled
        {
            get
            {
                return this.IsCompleted;
            }
        }

        public bool Wait()
        {
            _mre.Wait();
            return true;
        }

        public bool Wait(TimeSpan timeout)
        {
            return _mre.Wait(timeout);
        }

        public bool Wait(CancellationToken ctk)
        {
            _mre.Wait(ctk);
            return true;
        }

        public bool Wait(TimeSpan timeout, CancellationToken ctk)
        {
            return _mre.Wait(timeout, ctk);
        }

        public bool Wait(int timeout, CancellationToken ctk)
        {
            return _mre.Wait(timeout, ctk);
        }

        public bool Wait(IExecutorService executorService)
        {
            return (executorService.WaitAny(this.WaitHandle, -1) == 1);
        }

        public WaitHandle WaitHandle
        {
            get { return _mre.WaitHandle; }
        }
    }
}
