/* ================================================================================= 
 * Purpose		:	Asynchronous Result Invoker Class
 * File Name	:   AsyncResultInvoker.cs
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
    /// Asynchronous Result Invoker Class
    /// </summary>
    public static class AsyncResultInvoker
    {
        /// <summary>
        /// Begins the invoke.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="funcSync">The func sync.</param>
        /// <param name="asyncCallback">The async callback.</param>
        /// <param name="asyncState">State of the async.</param>
        /// <returns>IAsyncResult interface implementation</returns>
        public static IAsyncResult BeginInvoke<TResult>(Func<object> funcSync, AsyncCallback asyncCallback, object asyncState)
        {
            return BeginInvoke<TResult>(funcSync, asyncCallback, asyncState, false);
        }

        /// <summary>
        /// Begins the invoke.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="funcSync">The func sync.</param>
        /// <param name="asyncCallback">The async callback.</param>
        /// <param name="asyncState">State of the async.</param>
        /// <returns>IAsyncResult interface implementation</returns>
        public static IAsyncResult BeginInvoke<TResult>(Func<object> funcSync, AsyncCallback asyncCallback, object asyncState, bool dedicatedThread)
        {
            AsyncResult<TResult> ar = new AsyncResult<TResult>(asyncCallback, asyncState);
            ar.FuncSync = funcSync;
            if (dedicatedThread)
                Extensions.CreateThreadAndStart(new ParameterizedThreadStart(DoTaskHelper), ar);
            else
                ThreadPool.QueueUserWorkItem(DoTaskHelper, ar);
            return ar;
        }

        /// <summary>
        /// Begins the invoke.
        /// </summary>
        /// <param name="actionSync">The action sync.</param>
        /// <param name="asyncCallback">The async callback.</param>
        /// <param name="asyncState">State of the async.</param>
        /// <returns></returns>
        public static IAsyncResult BeginInvoke(Action actionSync, AsyncCallback asyncCallback, object asyncState)
        {
            return BeginInvoke(actionSync, asyncCallback, asyncState, false);
        }

        /// <summary>
        /// Begins the invoke.
        /// </summary>
        /// <param name="actionSync">The action sync.</param>
        /// <param name="asyncCallback">The async callback.</param>
        /// <param name="asyncState">State of the async.</param>
        /// <returns></returns>
        public static IAsyncResult BeginInvoke(Action actionSync, AsyncCallback asyncCallback, object asyncState, bool dedicatedThread)
        {
            AsyncResult ar = new AsyncResult(asyncCallback, asyncState);
            ar.ActionSync = actionSync;
            if (dedicatedThread)
                Extensions.CreateThreadAndStart(new ParameterizedThreadStart(DoTaskHelper), ar);
            else
                ThreadPool.QueueUserWorkItem(DoTaskHelper, ar);
            return ar;
        }

        /// <summary>
        /// Ends the invoke.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="asyncResult">The async result.</param>
        /// <returns>Result of the asynchronous operation.</returns>
        public static TResult EndInvoke<TResult>(IAsyncResult asyncResult)
        {
            AsyncResult<TResult> ar = asyncResult as AsyncResult<TResult>;
            if (ar != null) return ar.EndInvoke();
            return default(TResult);
        }

        /// <summary>
        /// Ends the invoke.
        /// </summary>
        /// <param name="asyncResult">The async result.</param>
        public static void EndInvoke(IAsyncResult asyncResult)
        {
            AsyncResult ar = asyncResult as AsyncResult;
            if (ar != null) ar.EndInvoke();
        }

        /// <summary>
        /// Does the task helper.
        /// </summary>
        /// <param name="asyncResult">The async result.</param>
        private static void DoTaskHelper(object asyncResult)
        {
            AsyncResult ar = asyncResult as AsyncResult;
            try
            {
                if (ar.IsGeneric)
                {
                    ar._SetCompleted(ar.FuncSync(), null, false);
                }
                else
                {
                    ar.ActionSync();
                    ar.SetCompleted(null, false);
                }
            }
            catch (Exception ex)
            {
                ar.SetCompleted(ex, false);
            }
        }
    }
}
