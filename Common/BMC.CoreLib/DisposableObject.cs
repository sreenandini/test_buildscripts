using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib
{
    public interface IDisposableObject : IDisposable
    {
        bool IsDisposed { get; }
    }

#if !SILVERLIGHT
    [Serializable]
#endif
    public class DisposableObject
#if CROSS_DOMAIN_CALL
        : MarshalByRefObject,
#else
 : Object,
#endif
 IDisposableObject,
 IDisposable,
        ICloneable
    {
        [NonSerialized]
        protected bool _disposed = false;

        public DisposableObject() { }

        #region Logging Purpose
        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>The module name.</value>
        protected virtual string DYN_MODULE_NAME
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ToString(sb)");
            StringBuilder sb = new StringBuilder();
            try
            {
                this.ToString(sb);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            if (sb.Length > 0)
                return sb.ToString();
            else
                return base.ToString();
        }

        /// <summary>
        /// Toes the string.
        /// </summary>
        /// <param name="sb">The sb.</param>
        protected virtual void ToString(StringBuilder sb) { }

        public bool IsDisposed
        {
            get { return _disposed; }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // managed dispose
                if (disposing)
                {
                    this.DisposeManaged();
                }

                // unmanaged dispose
                this.DisposeUnmanaged();
                _disposed = true;
            }
        }

        /// <summary>
        /// Overridable method which releases the managed resources.
        /// </summary>
        protected virtual void DisposeManaged() { }

        /// <summary>
        /// Overridable method which releases the unmanaged resources.
        /// </summary>
        protected virtual void DisposeUnmanaged() { }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public virtual object Clone()
        {
            return this.DeepClone();
        }

        #endregion
    }
}
