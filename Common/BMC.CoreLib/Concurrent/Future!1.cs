using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BMC.CoreLib.Concurrent
{
    public class Future<T> : DisposableObject
    {
        private ThinEvent _te = new ThinEvent(false);
        private T _result = default(T);
        private Exception _ex = null;

        public Future(Func<T> func)
            : this(func, true) { }

        public Future(Func<T> func, bool useThreadPool)
        {
            if (useThreadPool)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoWork), func);
            }
            else
            {
                Extensions.CreateThreadAndStart(this.DoWork, func);
            }
        }

        private void DoWork(object o)
        {
            try
            {
                _result = ((Func<T>)o)();
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                _te.Set();
            }
        }

        public T Value
        {
            get
            {
                if (!_te.IsCompleted)
                    _te.Wait();
                if (_ex != null)
                    throw _ex;
                return _result;
            }
        }
    }

    public class Future : Future<object>
    {
        public Future(Action func)
            : this(func, true) { }

        public Future(Action func, bool useThreadPool)
            : base(() =>
            {
                func();
                return null;
            }, useThreadPool) { }
    }
}
