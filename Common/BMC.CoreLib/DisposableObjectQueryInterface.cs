using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.CoreLib
{
    /// <summary>
    /// Object base class with disposable feature and notify event notifications.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DisposableObjectQueryInterface
        : DisposableObjectNotify,
        INotifyPropertyChanged
    {
        protected readonly IDictionary<Type, IQueryInterface> _queryInterfaces = null;

        public DisposableObjectQueryInterface()
        {
            _queryInterfaces = new Dictionary<Type, IQueryInterface>();
        }

        #region IQueryInterface Members

        /// <summary>
        /// Registers the interface.
        /// </summary>
        /// <typeparam name="T">Type of the destination object.</typeparam>
        /// <param name="createInstace">The create instace.</param>
        /// <returns>Interface implementation.</returns>
        protected T RegisterInterface<T>(Func<T> createInstace)
            where T : class, IQueryInterface
        {
            Type typeT = typeof(T);

            if (_queryInterfaces.ContainsKey(typeT))
            {
                return _queryInterfaces[typeT] as T;
            }
            else
            {
                T value = createInstace();
                _queryInterfaces.Add(typeof(T), value);
                return value;
            }
        }

        /// <summary>
        /// Registers the interface.
        /// </summary>
        /// <typeparam name="T">Type of the destination object.</typeparam>
        /// <param name="createInstace">The create instace.</param>
        /// <returns>Interface implementation.</returns>
        protected T RegisterInterface<T>(T value)
            where T : class, IQueryInterface
        {
            Type typeT = typeof(T);

            if (_queryInterfaces.ContainsKey(typeT))
            {
                return _queryInterfaces[typeT] as T;
            }
            else
            {
                _queryInterfaces.Add(typeof(T), value);
                return value;
            }
        }

        /// <summary>
        /// Queries the interface.
        /// </summary>
        /// <typeparam name="T">Type of the destination object.</typeparam>
        /// <returns>Interface Implementation.</returns>
        public T QueryInterface<T>()
            where T : class, IQueryInterface
        {
            Type typeT = typeof(T);
            string key = typeT.FullName;

            foreach (KeyValuePair<Type, IQueryInterface> pairItem in _queryInterfaces)
            {
                if (typeT.IsAssignableFrom(pairItem.Key))
                {
                    return pairItem.Value as T;
                }
            }

            foreach (KeyValuePair<Type, IQueryInterface> pairSubItem in _queryInterfaces)
            {
                IQueryInterface query = pairSubItem.Value.QueryInterface<T>() as IQueryInterface;
                if (query != null)
                {
                    return query as T;
                }
            }
            return default(T);
        }

        #endregion
    }
}
