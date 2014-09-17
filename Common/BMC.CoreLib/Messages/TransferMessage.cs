using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BMC.CoreLib.Messages
{
    /// <summary>
    /// Transfer Message Interface
    /// </summary>
    /// <typeparam name="T">Type of the message.</typeparam>
    public interface ITransferMessage<T> : IDisposable, IEnumerable<KeyValuePair<string, T>>, IEnumerable
    {
        IDictionary<string, T> Properties { get; }
        int ArgCount { get; }
        T this[string name] { get; set; }

        T GetArgValue(string name);
        T GetArgValue(int index);
        string GetArgName(int index);
    }

    /// <summary>
    /// Transfer Message Interface
    /// </summary>
    public interface ITransferMessage : ITransferMessage<object> { }

    /// <summary>
    /// TransferMessageBase
    /// </summary>
    /// <typeparam name="T">Type of the message.</typeparam>
    public abstract class TransferMessageBase<T> :
        DisposableObjectNotify,
        ITransferMessage<T>,
        IEnumerable<KeyValuePair<string, T>>,
        IEnumerable
    {
        private IDictionary<string, T> _properties = null;
        protected IDictionary<int, string> _propIndexes = null;

        protected TransferMessageBase()
        {
            _properties = new Dictionary<string, T>();
            _propIndexes = new SortedDictionary<int, string>();
        }

        #region ITransferMessage Members

        public IDictionary<string, T> Properties
        {
            get { return _properties; }
        }

        public int ArgCount
        {
            get { return _properties.Count; }
        }

        public T this[string name]
        {
            get
            {
                if (_properties.ContainsKey(name))
                {
                    return _properties[name];
                }
                else
                {
                    return default(T);
                }
            }
            set
            {
                if (!_properties.ContainsKey(name))
                {
                    int index = _propIndexes.Count;
                    _propIndexes.Add(index, name);
                    _properties.Add(name, value);
                }
                else
                {
                    _properties[name] = value;
                }
            }
        }

        public T GetArgValue(string name)
        {
            if (_properties.ContainsKey(name))
                return _properties[name];
            else
                return default(T);
        }

        public T GetArgValue(int index)
        {
            if (_propIndexes.ContainsKey(index))
                return _properties[_propIndexes[index]];
            else
                return default(T);
        }

        public string GetArgName(int index)
        {
            if (_propIndexes.ContainsKey(index))
                return _propIndexes[index];
            else
                return string.Empty;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerator<T>)this.GetEnumerator());
        }

        #endregion
    }
}
