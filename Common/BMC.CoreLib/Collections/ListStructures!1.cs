using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using BMC.CoreLib.Diagnostics;
using System.Collections;
using BMC.CoreLib.Comparers;

namespace BMC.CoreLib.Collections
{
    #region Double Linked List
    [Serializable]
    [ComVisible(false)]
    [DebuggerTypeProxy(typeof(CoreLib_LinkedListDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    public class DoubleLinkedList<T> : DisposableObject, ICollection<T>
    {
        private class DoubleLinkedListNode : DisposableObject
        {
            public DoubleLinkedListNode(T value)
            {
                this.Value = value;
            }

            public DoubleLinkedListNode Next { get; set; }
            public DoubleLinkedListNode Prev { get; set; }
            public T Value { get; private set; }

            public override string ToString()
            {
                if (this.Value != null)
                {
                    return this.Value.ToString();
                }
                return base.ToString();
            }

            protected override void DisposeManaged()
            {
                base.DisposeManaged();
            }
        }

        private DoubleLinkedListNode _head = null;
        private EqualityComparer<T> _comparer = null;
        private int _count = 0;

        public DoubleLinkedList()
            : this(EqualityComparer<T>.Default) { }

        public DoubleLinkedList(EqualityComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public DoubleLinkedList(ICollection<T> collection)
        {
            if (collection != null)
            {
                foreach (T item in collection)
                {
                    this.AddLast(item);
                }
            }
        }

        public void AddFirst(T item)
        {
            bool emptyNode = (_head == null);
            DoubleLinkedListNode node = new DoubleLinkedList<T>.DoubleLinkedListNode(item);
            this.InsertNode(ref _head, node);
            if (emptyNode != null) _head = node;
        }

        public void AddLast(T item)
        {
            DoubleLinkedListNode node = new DoubleLinkedList<T>.DoubleLinkedListNode(item);
            this.InsertNode(ref _head, node);
        }

        public bool RemoveFirst()
        {
            return this.RemoveNode(_head);
        }

        internal T RemoveFirstInternal()
        {
            T value = default(T);
            this.RemoveNode(_head, out value);
            return value;
        }

        internal T PeekFirstInternal()
        {
            T value = default(T);
            if (_head != null)
            {
                value = _head.Value;
            }
            return value;
        }

        public bool RemoveLast()
        {
            return this.RemoveNode(_head.Prev);
        }

        internal T RemoveLastInternal()
        {
            T value = default(T);
            this.RemoveNode(_head.Prev, out value);
            return value;
        }

        internal T PeekLastInternal()
        {
            T value = default(T);
            if (_head != null &&
                _head.Prev != null)
            {
                value = _head.Prev.Value;
            }
            return value;
        }

        private DoubleLinkedListNode InsertNode(ref DoubleLinkedListNode node, DoubleLinkedListNode newNode)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InsertNode");

            try
            {
                if (node == null)
                {
                    newNode.Next = newNode;
                    newNode.Prev = newNode;
                    node = newNode;
                }
                else
                {
                    newNode.Next = node;
                    newNode.Prev = node.Prev;
                    node.Prev.Next = newNode;
                    node.Prev = newNode;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                _count++;
            }

            return node;
        }

        private bool RemoveNode(DoubleLinkedListNode node)
        {
            T value = default(T);
            return this.RemoveNode(node, out value);
        }

        private bool RemoveNode(DoubleLinkedListNode node, out T value)
        {
            value = default(T);
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "RemoveNode");
            if (node == null) return false;

            try
            {
                value = node.Value;
                if (node.Next == node)
                {
                    _head = null;
                }
                else
                {
                    node.Next.Prev = node.Prev;
                    node.Prev.Next = node.Next;
                    if (node == _head) _head = node.Next;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                node.Dispose();
                _count--;
            }
            return true;
        }

        private void IterateNodes(Func<DoubleLinkedListNode, bool?> doWork)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "IterateNodes");

            try
            {
                DoubleLinkedListNode node = _head;
                if (node != null)
                {
                    do
                    {
                        bool? result = doWork(node);
                        if (result == null)
                        {
                            DoubleLinkedListNode oldNode = node;
                            node = node.Next;
                            oldNode.Dispose();
                            _count--;
                        }
                        else
                        {
                            if (result.Value) break;
                            node = node.Next;
                        }
                    } while (node != _head);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private DoubleLinkedListNode FindNode(T item)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "FindNode");
            DoubleLinkedListNode result = default(DoubleLinkedListNode);

            try
            {
                this.IterateNodes((n) =>
                {
                    if (_comparer.Equals(n.Value, item))
                    {
                        result = n;
                        return true;
                    }
                    return false;
                });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        #region ICollection<T> Members

        public void Add(T item)
        {
            this.AddLast(item);
        }

        public void Clear()
        {
            this.IterateNodes((i) => { return null; });
            _head = null;
            Debug.Assert((_count == 0));
        }

        public bool Contains(T item)
        {
            DoubleLinkedListNode node = this.FindNode(item);
            return (node != null);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.CopyToInternal<T>(array, arrayIndex, (i) => { return i.Value; });
        }

        internal void CopyToString(string[] array, int arrayIndex)
        {
            this.CopyToInternal<String>(array, arrayIndex, (i) =>
            {
                string s = "( ";
                if (i.Prev != null) s += i.Prev.ToString() + " <-- ";
                s += i.Value.ToString();
                if (i.Next != null) s += " --> " + i.Next.ToString();
                s += " )";
                return s;
            });
        }

        private void CopyToInternal<S>(S[] array, int arrayIndex, Func<DoubleLinkedListNode, S> doWork)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "CopyTO");
            if ((array == null) ||
                (arrayIndex < 0 || arrayIndex > array.Length) ||
                ((array.Length - arrayIndex) < _count)) return;

            try
            {
                int i = arrayIndex;
                this.IterateNodes((n) =>
                {
                    array[i++] = doWork(n);
                    return false;
                });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public int Count
        {
            get { return _count; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            DoubleLinkedListNode node = this.FindNode(item);
            if (node != null)
            {
                this.RemoveNode(node);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        private struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private DoubleLinkedList<T> _parent;
            private DoubleLinkedListNode _node;
            private T _current;

            public Enumerator(DoubleLinkedList<T> parent)
            {
                _parent = parent;
                _node = _parent._head;
                _current = default(T);
            }

            #region IEnumerator<T> Members

            public T Current
            {
                get { return _current; }
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return _current; }
            }

            public bool MoveNext()
            {
                if (_node == null)
                {
                    return false;
                }

                _current = _node.Value;
                _node = _node.Next;
                if (_node == _parent._head)
                {
                    _node = null;
                }
                return true;
            }

            public void Reset()
            {
                _current = default(T);
                _node = _parent._head;
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {

            }

            #endregion
        }

    }
    #endregion

    #region List Base
    [Serializable]
    [ComVisible(false)]
    [DebuggerTypeProxy(typeof(CoreLib_ListDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    public abstract class DoubleLinkedListBase<T> : IEnumerable<T>, ICollection
    {
        protected DoubleLinkedList<T> _list = null;
        [NonSerialized]
        protected Object _syncRoot = null;

        public DoubleLinkedListBase()
            : this(EqualityComparer<T>.Default) { }

        public DoubleLinkedListBase(EqualityComparer<T> comparer)
        {
            _list = new DoubleLinkedList<T>(comparer);
        }

        public DoubleLinkedListBase(ICollection<T> collection)
        {
            _list = new DoubleLinkedList<T>(collection);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator() as IEnumerator<T>;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int index)
        {

        }

        public int Count
        {
            get { return _list.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange(ref _syncRoot, new Object(), null);
                }
                return _syncRoot;
            }
        }

        #endregion
    }

    #endregion

    #region Stack
    [DebuggerTypeProxy(typeof(CoreLib_ListDebugView<>))]
    public class DoubleLinkedListStack<T> : DoubleLinkedListBase<T>
    {
        public DoubleLinkedListStack()
            : base() { }

        public DoubleLinkedListStack(EqualityComparer<T> comparer)
            : base(comparer) { }

        public DoubleLinkedListStack(ICollection<T> collection)
            : base(collection) { }

        public void Push(T item)
        {
            _list.AddLast(item);
        }

        public T Pop()
        {
            return _list.RemoveLastInternal();
        }

        public T Peek()
        {
            return _list.PeekLastInternal();
        }
    }

    #endregion

    #region Queue
    public class DoubleLinkedListQueue<T> : DoubleLinkedListBase<T>
    {
        public DoubleLinkedListQueue()
            : base() { }

        public DoubleLinkedListQueue(EqualityComparer<T> comparer)
            : base(comparer) { }

        public DoubleLinkedListQueue(ICollection<T> collection)
            : base(collection) { }

        public void Enqueue(T item)
        {
            _list.AddLast(item);
        }

        public T Dequeue()
        {
            return _list.RemoveFirstInternal();
        }

        public T Peek()
        {
            return _list.PeekFirstInternal();
        }
    }

    #endregion

    #region Debug Views
    public sealed class CoreLib_CollectionDebugView<T>
    {
        private ICollection<T> collection;

        public CoreLib_CollectionDebugView(ICollection<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            this.collection = collection;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                T[] items = new T[collection.Count];
                collection.CopyTo(items, 0);
                return items;
            }
        }
    }

    public sealed class CoreLib_LinkedListDebugView<T>
    {
        private DoubleLinkedList<T> collection;

        public CoreLib_LinkedListDebugView(DoubleLinkedList<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            this.collection = collection;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public String[] Items
        {
            get
            {
                String[] items = new string[collection.Count];
                collection.CopyToString(items, 0);
                return items;
            }
        }
    }

    public sealed class CoreLib_ListDebugView<T>
    {
        private DoubleLinkedListBase<T> _list;

        public CoreLib_ListDebugView(DoubleLinkedListBase<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("stack");
            }

            this._list = list;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                return _list.ToArray();
            }
        }
    }
    #endregion

    #region List of primitive types

    public class IntList : List<int> { }

    public class StringList : List<string> { }

    public class IntDictionary : SortedDictionary<int, bool>
    {
        public IntDictionary()
            : base() { }
    }

    public class IntDictionary<T> : SortedDictionary<int, T>
    {
        public IntDictionary()
            : base() { }
    }

    public class IntConcurrentDictionary<T> : ConcurrentDictionary<int, T>
    {
        public IntConcurrentDictionary()
            : base() { }
    }

    public class StringDictionary : SortedDictionary<string, bool>
    {
        public StringDictionary()
            : base(StringComparer.InvariantCultureIgnoreCase) { }
    }

    public class StringDictionary<T> : SortedDictionary<string, T>
    {
        public StringDictionary()
            : base(StringComparer.InvariantCultureIgnoreCase) { }
    }

    public class StringConcurrentDictionary : System.Collections.Concurrent.ConcurrentDictionary<string, bool>
    {
        public StringConcurrentDictionary()
            : base(StringComparer.InvariantCultureIgnoreCase) { }
    }

    public class StringConcurrentDictionary<T> : System.Collections.Concurrent.ConcurrentDictionary<string, T>
    {
        public StringConcurrentDictionary()
            : base(StringComparer.InvariantCultureIgnoreCase) { }
    }

    public class StringConcurrentDictionaryIgnoreCase<T> : System.Collections.Concurrent.ConcurrentDictionary<string, T>
    {
        public StringConcurrentDictionaryIgnoreCase()
            : base(StringComparer.InvariantCultureIgnoreCase) { }
    }

    public class TypeDictionary : SortedDictionary<Type, bool>
    {
        public TypeDictionary()
            : base(new TypeComparer()) { }
    }

    public class TypeDictionary<T> : SortedDictionary<Type, T>
    {
        public TypeDictionary()
            : base(new TypeComparer()) { }
    }

    #endregion

    #region DoubleKeyDictionary
    public interface IDoubleKeyDictionary<K, V>
    {
        void Add(K key1, K key2, V value);
        V GetValueFromKey1(K key);
        V GetValueFromKey2(K key);
    }

    public class DoubleKeyDictionary<K, V>
        : SortedDictionary<K, V>, IDoubleKeyDictionary<K, V>
    {
        private IDictionary<K, KeyValuePair<K, V>> _keyMappings = null;

        public DoubleKeyDictionary(IComparer<K> comparer)
            : base(comparer)
        {
            _keyMappings = new SortedDictionary<K, KeyValuePair<K, V>>(comparer);
        }

        public virtual void Add(K key1, K key2, V value)
        {
            if (this.ContainsKey(key1))
            {
                this[key1] = value;
            }
            else
            {
                this.Add(key1, value);
            }

            if (_keyMappings.ContainsKey(key2))
            {
                _keyMappings[key2] = new KeyValuePair<K, V>(key1, value);
            }
            else
            {
                _keyMappings.Add(key2, new KeyValuePair<K, V>(key1, value));
            }
        }

        public virtual V GetValueFromKey1(K key)
        {
            if (this.ContainsKey(key))
                return this[key];
            else
                return default(V);
        }

        public virtual V GetValueFromKey2(K key)
        {
            if (_keyMappings.ContainsKey(key))
                return _keyMappings[key].Value;
            else
                return default(V);
        }
    }
    #endregion

    #region Double Key Concurrent Dictionary
    public class KeyValuePair2<TKey, TValue>
    {
        public KeyValuePair2(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public TKey Key;
        public TValue Value;

        public override string ToString()
        {
            return this.Key.ToStringSafe() + " => " + this.Value.ToStringSafe();
        }
    }

    public interface IDoubleKeyConcurrentDictionary<K1, K2, V>
    {
        void Add(K1 key1, K2 key2, V value);
        V GetValueFromKey1(K1 key);
        V GetValueFromKey2(K2 key);
        bool IsKey1Exists(K1 key);
        bool IsKey2Exists(K2 key);
        K1 GetKey1(K2 key2);
        void Update(K2 key2, K1 key1);
    }

    public class DoubleKeyConcurrentDictionary<K1, K2, V>
        : DisposableObject,
        IDictionary<K1, V>,
        ICollection<KeyValuePair<K1, V>>,
        IDictionary,
        ICollection,
        IEnumerable,
        IDoubleKeyConcurrentDictionary<K1, K2, V>
    {
        private readonly IDictionary<K1, V> _collection = null;
        private readonly IDictionary<K2, KeyValuePair2<K1, V>> _keyMappings = null;
        private readonly Func<V, K2> _getKey2 = null;

        public DoubleKeyConcurrentDictionary(Func<V, K2> getKey2)
            : this(getKey2, null, null) { }

        public DoubleKeyConcurrentDictionary(Func<V, K2> getKey2, IEqualityComparer<K1> comparer1)
            : this(getKey2, comparer1, null) { }

        public DoubleKeyConcurrentDictionary(Func<V, K2> getKey2, IEqualityComparer<K2> comparer2)
            : this(getKey2, null, comparer2) { }

        public DoubleKeyConcurrentDictionary(Func<V, K2> getKey2, IEqualityComparer<K1> comparer1, IEqualityComparer<K2> comparer2)
        {
            _getKey2 = getKey2;
            if (comparer1 != null) _collection = new ConcurrentDictionary<K1, V>(comparer1);
            else _collection = new ConcurrentDictionary<K1, V>();

            if (comparer2 != null) _keyMappings = new ConcurrentDictionary<K2, KeyValuePair2<K1, V>>(comparer2);
            else _keyMappings = new ConcurrentDictionary<K2, KeyValuePair2<K1, V>>();
        }

        public void Add(K1 key1, K2 key2, V value)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Add"))
            {
                try
                {
                    if (_collection.ContainsKey(key1))
                    {
                        _collection[key1] = value;
                    }
                    else
                    {
                        _collection.Add(key1, value);
                    }

                    if (_keyMappings.ContainsKey(key2))
                    {
                        _keyMappings[key2] = new KeyValuePair2<K1, V>(key1, value);
                    }
                    else
                    {
                        _keyMappings.Add(key2, new KeyValuePair2<K1, V>(key1, value));
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public V GetValueFromKey1(K1 key)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetValueFromKey1"))
            {
                V result = default(V);

                try
                {
                    if (_collection.ContainsKey(key))
                        return _collection[key];
                    else
                        return default(V);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public V GetValueFromKey2(K2 key)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetValueFromKey2"))
            {
                V result = default(V);

                try
                {
                    if (_keyMappings.ContainsKey(key))
                        return _keyMappings[key].Value;
                    else
                        return default(V);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public bool IsKey1Exists(K1 key)
        {
            return _collection.ContainsKey(key);
        }

        public bool IsKey2Exists(K2 key)
        {
            return _keyMappings.ContainsKey(key);
        }

        public K1 GetKey1(K2 key2)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetKey1"))
            {
                K1 result = default(K1);

                try
                {
                    if (_keyMappings.ContainsKey(key2))
                    {
                        result = _keyMappings[key2].Key;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public void Update(K2 key2, K1 key1)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Update"))
            {
                try
                {
                    if (_keyMappings.ContainsKey(key2))
                    {
                        _keyMappings[key2].Key = key1;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public virtual void Add(K1 key, V value)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Add"))
            {
                try
                {
                    this.Add(key, _getKey2(value), value);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public bool ContainsKey(K1 key)
        {
            return _collection.ContainsKey(key);
        }

        public ICollection<K1> Keys
        {
            get { return _collection.Keys; }
        }

        public bool Remove(K1 key)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Remove"))
            {
                bool result = default(bool);

                try
                {
                    if (this.ContainsKey(key))
                    {
                        V value = this[key];
                        K2 key2 = _getKey2(value);
                        if (!key.Equals(default(K2)) &&
                            _keyMappings.ContainsKey(key2))
                        {
                            _keyMappings.Remove(key2);
                        }
                        result = _collection.Remove(key);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public bool TryGetValue(K1 key, out V value)
        {
            return _collection.TryGetValue(key, out value);
        }

        public ICollection<V> Values
        {
            get { throw new NotImplementedException(); }
        }

        public V this[K1 key]
        {
            get
            {
                return _collection.AddOrGet(key, null);
            }
            set
            {
                _collection.AddOrUpdate2(key, value);
            }
        }

        public void Add(KeyValuePair<K1, V> item)
        {
            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(KeyValuePair<K1, V> item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(KeyValuePair<K1, V>[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return _collection.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<K1, V> item)
        {
            return _collection.Remove(item);
        }

        public IEnumerator<KeyValuePair<K1, V>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add(object key, object value)
        {
            ((IDictionary)_collection).Add(key, value);
        }

        public bool Contains(object key)
        {
            return ((IDictionary)_collection).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)_collection).GetEnumerator();
        }

        public bool IsFixedSize
        {
            get { return ((IDictionary)_collection).IsFixedSize; }
        }

        ICollection IDictionary.Keys
        {
            get { return ((IDictionary)_collection).Keys; }
        }

        public void Remove(object key)
        {
            ((IDictionary)_collection).Remove(key);
        }

        ICollection IDictionary.Values
        {
            get { return ((IDictionary)_collection).Values; }
        }

        public object this[object key]
        {
            get
            {
                return ((IDictionary)_collection)[key];
            }
            set
            {
                ((IDictionary)_collection)[key] = value;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }
    }
    #endregion
}
