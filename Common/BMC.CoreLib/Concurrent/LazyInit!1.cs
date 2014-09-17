using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BMC.CoreLib.Concurrent
{
    public class LazyInitObject<T>
        : DisposableObject
        where T : class
    {
        private Func<T> _factory = null;
        private T _value = null;
        private object _lock = new object();

        public LazyInitObject(Func<T> factory)
        {
            _factory = factory;
        }

        public T Value
        {
            get
            {
                if (_value == null)
                {
                    lock (_lock)
                    {
                        if (_value == null)
                        {
                            T obj = _factory();
                            if (Interlocked.CompareExchange<T>(ref _value, obj, null) != null &&
                                obj is IDisposable)
                            {
                                ((IDisposable)obj).Dispose();
                            }
                        }
                    }
                }
                return _value;
            }
        }
    }

    public class LazyInitValue<T>
        : DisposableObject
        where T : struct
    {

        private class Boxed : DisposableObject
        {
            internal Boxed(Func<T> factory)
            {
                this.Value = factory();
            }

            public T Value { get; set; }
        }

        private Func<T> _factory = null;
        private Boxed _value = null;
        private object _lock = new object();

        public LazyInitValue(Func<T> factory)
        {
            _factory = factory;
        }

        public T Value
        {
            get
            {
                if (_value == null)
                {
                    lock (_lock)
                    {
                        if (_value == null)
                        {
                            Boxed obj = new Boxed(_factory);
                            if (Interlocked.CompareExchange<Boxed>(ref _value, obj, null) != null)
                            {
                                obj.Dispose();
                            }
                        }
                    }
                }
                return _value.Value;
            }
        }
    }
}
