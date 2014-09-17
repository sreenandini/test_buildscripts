/* ================================================================================= 
 * Purpose		:	Asynchronous Pattern Implementation Class
 * File Name	:   AsyncResult!1.cs
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
    public class AsyncResult<TResult>
        : AsyncResult
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncResult&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="asyncCallback">The async callback.</param>
        /// <param name="asyncState">State of the async.</param>
        public AsyncResult(AsyncCallback asyncCallback, object asyncState)
            : base(asyncCallback, asyncState)
        {
            this.IsGeneric = true;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Sets the completed.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="completedSuccessfully">if set to <c>true</c> [completed successfully].</param>
        public void SetCompleted(TResult result, bool completedSuccessfully)
        {
            base._SetCompleted(result, null, completedSuccessfully);
        }

        /// <summary>
        /// Ends the invoke.
        /// </summary>
        /// <returns>Result of the asynchronous operation.</returns>
        new public TResult EndInvoke()
        {
            return (TResult)base._EndInvoke();
        }

        #endregion
    }
}
