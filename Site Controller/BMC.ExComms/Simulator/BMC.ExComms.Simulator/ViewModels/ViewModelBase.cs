using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BMC.CoreLib;

namespace BMC.ExComms.Simulator.ViewModels
{
    public class ViewModelBase 
        : DependencyObject, IDisposable
    {
        [NonSerialized]
        protected bool _disposed = false;

        protected ViewModelBase() { }

        protected virtual string DYN_MODULE_NAME
        {
            get
            {
                return this.GetType().Name;
            }
        }
        
        public bool IsDisposed
        {
            get { return _disposed; }
        }

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
