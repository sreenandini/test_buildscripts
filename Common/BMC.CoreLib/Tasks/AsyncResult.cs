/* ================================================================================= 
 * Purpose		:	Asynchronous Pattern Implementation Class
 * File Name	:   AsyncResult1.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	20/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 20/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BMC.CoreLib.Tasks
{
    /// <summary>
    /// Asynchronous Pattern Implementation Class
    /// </summary>
    public class AsyncResult
        : IAsyncResult
    {
        #region Local Variables

        protected AsyncCallback _asyncCallBack = null;
        protected object _asyncState = null;

        protected ManualResetEvent _waitHandle = null;
        protected const int STATE_PENDING = 0;
        protected const int STATE_COMPLETED_SYNC = 1;
        protected const int STATE_COMPLETED_ASYNC = 2;
        protected int _waitState = STATE_PENDING;
        protected Exception _exception = null;

        protected object _boxResult = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncResult&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="asyncCallback">The async callback.</param>
        /// <param name="asyncState">State of the async.</param>
        public AsyncResult(AsyncCallback asyncCallback, object asyncState)
        {
            _asyncCallBack = asyncCallback;
            _asyncState = asyncState;
            this.IsGeneric = false;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Sets the completed.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="completedSuccessfully">if set to <c>true</c> [completed successfully].</param>
        public void SetCompleted(Exception exception, bool completedSuccessfully)
        {
            // null means no exception
            _exception = exception;

            // set the wait state
            int oldState = Interlocked.Exchange(ref _waitState,
                (completedSuccessfully ? STATE_COMPLETED_SYNC : STATE_COMPLETED_ASYNC));

            // raise exception if it is already completed
            if (oldState != STATE_PENDING)
                throw new InvalidOperationException("You can set a result only once");

            // signal the wait handle
            if (_waitHandle != null) _waitHandle.Set();

            // invoke the callback
            if (_asyncCallBack != null) _asyncCallBack(this);
        }

        /// <summary>
        /// Sets the completed.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="completedSuccessfully">if set to <c>true</c> [completed successfully].</param>
        internal void _SetCompleted(object result, Exception exception, bool completedSuccessfully)
        {
            _boxResult = result;
            this.SetCompleted(exception, completedSuccessfully);
        }

        /// <summary>
        /// Ends the invoke.
        /// </summary>
        public void EndInvoke()
        {
            // implements wait-until pattern
            if (!this.IsCompleted)
            {
                this.AsyncWaitHandle.WaitOne();
                this.AsyncWaitHandle.Close();
                _waitHandle = null;
            }

            // raise the exception if any
            if (_exception != null) throw _exception;
        }

        /// <summary>
        /// _s the end invoke.
        /// </summary>
        /// <returns>Result of the asynchronous operation.</returns>
        internal object _EndInvoke()
        {
            this.EndInvoke();
            return _boxResult;
        }

        #endregion

        #region IAsyncResult Members

        /// <summary>
        /// Gets a user-defined object that qualifies or contains information about an asynchronous operation.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A user-defined object that qualifies or contains information about an asynchronous operation.
        /// </returns>
        public object AsyncState
        {
            get { return _asyncState; }
        }

        /// <summary>
        /// Gets a <see cref="T:System.Threading.WaitHandle"/> that is used to wait for an asynchronous operation to complete.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A <see cref="T:System.Threading.WaitHandle"/> that is used to wait for an asynchronous operation to complete.
        /// </returns>
        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get
            {
                // not already created
                if (_waitHandle == null)
                {
                    bool done = this.IsCompleted;
                    ManualResetEvent mre = new ManualResetEvent(done);

                    if (Interlocked.CompareExchange<ManualResetEvent>(ref _waitHandle, mre, null) != null)
                    {
                        // some other thread has created the wait handle,
                        // so destroy this event
                        mre.Close();
                        mre = null;
                    }
                    else
                    {
                        // Check one more time for the completed state
                        if (!done && this.IsCompleted)
                        {
                            _waitHandle.Set();
                        }
                    }
                }
                return _waitHandle;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the asynchronous operation completed synchronously.
        /// </summary>
        /// <value></value>
        /// <returns>true if the asynchronous operation completed synchronously; otherwise, false.
        /// </returns>
        public bool CompletedSynchronously
        {
            get { return (_waitState == STATE_COMPLETED_SYNC); }
        }

        /// <summary>
        /// Gets a value that indicates whether the asynchronous operation has completed.
        /// </summary>
        /// <value></value>
        /// <returns>true if the operation is complete; otherwise, false.
        /// </returns>
        public bool IsCompleted
        {
            get { return (_waitState != STATE_PENDING); }
        }

        #endregion

        #region Internal Members

        /// <summary>
        /// Gets or sets a value indicating whether this instance is generic.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is generic; otherwise, <c>false</c>.
        /// </value>
        internal bool IsGeneric { get; set; }

        /// <summary>
        /// Gets or sets the func sync.
        /// </summary>
        /// <value>The func sync.</value>
        internal Func<object> FuncSync { get; set; }

        /// <summary>
        /// Gets or sets the action sync.
        /// </summary>
        /// <value>The action sync.</value>
        internal Action ActionSync { get; set; }

        #endregion
    }
}
