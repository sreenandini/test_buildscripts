using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BMC.MeterAdjustmentTool
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class DisposableObject
        : MarshalByRefObject,
        IDisposable
    {
        private bool _disposed = false;

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
                return string.Empty;
            }
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
    }
}
