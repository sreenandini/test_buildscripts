using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BMC.CoreLib.Tasks
{
    public class CountdownEvent : DisposableObject
    {
        private int _initialCount = 0;
        private volatile int _currentCount = 0;

        private ManualResetEvent _mre = new ManualResetEvent(false);

        public CountdownEvent(int initialCount)
        {
            if (initialCount <= 0) initialCount = 1;
            _initialCount = initialCount;
            _currentCount = initialCount;
        }

        public bool IsSet
        {
            get { return (_currentCount <= 0); }
        }

        public bool Signal()
        {
            if (_currentCount <= 0) return false;

#pragma warning disable 0420
            int oldCount = Interlocked.Decrement(ref _currentCount);
#pragma warning restore 0420

            if (oldCount == 0)
            {
                _mre.Set();
                return true;
            }
            return false;
        }

        public void Wait()
        {
            _mre.WaitOne();
        }
    }
}
