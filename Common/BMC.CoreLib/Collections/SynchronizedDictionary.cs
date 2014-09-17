using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Collections
{
    [CLSCompliant(true)]
    public class SynchronizedDictionary<TKey, TValue>
        : DisposableObject,
        IDictionary<TKey, TValue>
    {
        protected IDictionary<TKey, TValue> _inner = null;
        private object _lockObject = new object();

        public SynchronizedDictionary()
            : this(null) { }

        public SynchronizedDictionary(IComparer<TKey> comparer)
        {
            if (comparer == null)
                _inner = new Dictionary<TKey, TValue>();
            else
                _inner = new SortedDictionary<TKey, TValue>(comparer);
        }

        #region IDictionary<TKey,TValue> Members

        public void Add(TKey key, TValue value)
        {
            lock (_lockObject)
            {
                if (!_inner.ContainsKey(key))
                    _inner.Add(key, value);
                else
                    _inner[key] = value;
            }
        }

        public bool ContainsKey(TKey key)
        {
            lock (_lockObject)
            {
                return _inner.ContainsKey(key);
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                lock (_lockObject)
                {
                    return _inner.Keys;
                }
            }
        }

        public bool Remove(TKey key)
        {
            lock (_lockObject)
            {
                if (_inner.ContainsKey(key))
                {
                    return _inner.Remove(key);
                } 
                return false;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (_lockObject)
            {
                return _inner.TryGetValue(key, out value);
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                lock (_lockObject)
                {
                    return _inner.Values;
                }
            }
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                lock (_lockObject)
                {
                    return _inner[key];
                }
            }
            set
            {
                lock (_lockObject)
                {
                    _inner[key] = value;
                }
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            lock (_lockObject)
            {
                _inner.Add(item);
            }
        }

        public void Clear()
        {
            lock (_lockObject)
            {
                _inner.Clear();
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            lock (_lockObject)
            {
                return _inner.Contains(item);
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            lock (_lockObject)
            {
                _inner.CopyTo(array, arrayIndex);
            }
        }

        public int Count
        {
            get
            {
                lock (_lockObject)
                {
                    return _inner.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                lock (_lockObject)
                {
                    return _inner.IsReadOnly;
                }
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            lock (_lockObject)
            {
                return _inner.Remove(item);
            }
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_inner).GetEnumerator();
        }

        #endregion
    }

    public abstract class SynchronizedDictionaryBase<K, V> 
        : SynchronizedDictionary<K, V>
    {
        protected SynchronizedDictionaryBase(IComparer<K> comparer)
            : base(comparer) { }

        public V Modify(K key)
        {
            ModuleProc PROC = new ModuleProc("DictionaryBase", "Modify");
            V result = default(V);

            try
            {
                if (this.ContainsKey(key))
                {
                    result = this[key];
                }
                else
                {
                    result = this.CreateDefaultVlue();
                    this.Add(key, result);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected virtual V CreateDefaultVlue() { return default(V); }

        public V Modify(K key, V value)
        {
            ModuleProc PROC = new ModuleProc("DictionaryBase", "Modify");
            V result = default(V);

            try
            {
                if (this.ContainsKey(key))
                {
                    result = this[key];
                }
                else
                {
                    result = value;
                    this.Add(key, result);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
