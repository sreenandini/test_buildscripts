/* ================================================================================= 
 * Purpose		:	Object with disposable feature and notify event notifications.
 * File Name	:   DisposableObjectNotify.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	02/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 25/11/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Xml.Serialization;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Collections;

namespace BMC.CoreLib
{
    /// <summary>
    /// Object base class with disposable feature and notify event notifications.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DisposableObjectNotify
        : DisposableObject,
        INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableObjectNotify"/> class.
        /// </summary>
        public DisposableObjectNotify()
            : base() { }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (object.Equals(field, value)) return false;
            field = value;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool SetProperty<K, V>(IDictionary<K, V> collection, K key,  V value, string propertyName)
        {
            if (!collection.ContainsKey(key))
            {
                collection.Add(key, value);
                return true;
            }

            V oldValue = collection[key];
            if (object.Equals(oldValue, value)) return false;

            collection[key] = value;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged = null;

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public virtual void NotifyPropertyChanged(string propertyName)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "NotifyPropertyChanged");

            try
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            } 
        }

        #endregion
    }

    public enum ObjectNotifyStates
    {
        Initial = 0,
        Updated = 1
    }

#if !SILVERLIGHT
    [Serializable]
#endif
    public class DisposableObjectNotifyState : DisposableObjectNotify, IDataErrorInfo
    {
        private IDictionary<string, string> _requiredMessages = new StringDictionary<string>();

        public DisposableObjectNotifyState() { }

        public ObjectNotifyStates State { get; set; }

        public DisposableObjectNotifyState AddRequiredMessage(string columnName, string errorMessage)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddRequiredMessage");

            try
            {
                if (!_requiredMessages.ContainsKey(columnName))
                {
                    _requiredMessages.Add(columnName, errorMessage);
                }
                else
                {
                    _requiredMessages[columnName] = errorMessage;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return this;
        }

        public override void NotifyPropertyChanged(string propertyName)
        {
            base.NotifyPropertyChanged(propertyName);
            this.State = ObjectNotifyStates.Updated;
        }

        public virtual string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "this");
                string result = default(string);

                try
                {
                    if (this.State == ObjectNotifyStates.Updated)
                    {
                        if (_requiredMessages.ContainsKey(columnName))
                        {
                            object propertyValue = this.GetType().GetProperty(columnName).GetValue(this, null);
                            if (propertyValue != null &&
                                propertyValue.GetType() == typeof(string) &&
                                propertyValue.ToString().IsEmpty())
                            {
                                return _requiredMessages[columnName];
                            }
                        }

                        result = this.GetErrorInfoMessage(columnName);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }

                return result;
            }
        }

        protected virtual string GetErrorInfoMessage(string columnName)
        {
            return string.Empty;
        }
    }
}
