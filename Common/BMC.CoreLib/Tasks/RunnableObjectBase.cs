/* ================================================================================= 
 * Purpose		:	Runnable Object Abstract Base Class
 * File Name	:   RunnableObjectBase.cs
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
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Tasks
{
    /// <summary>
    /// Runnable Object Abstract Base Class
    /// </summary>
    public abstract class RunnableObjectBase
        : DisposableObject, IRunnableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunnableObjectBase"/> class.
        /// </summary>
        protected RunnableObjectBase() { }

        #region IRunnableObject Members

        /// <summary>
        /// Occurs when [before start].
        /// </summary>
        public event MethodInvokedHandler BeforeStart = null;

        /// <summary>
        /// Occurs when [after start].
        /// </summary>
        public event MethodInvokedHandler AfterStart = null;

        /// <summary>
        /// Occurs when [before stop].
        /// </summary>
        public event MethodInvokedHandler BeforeStop = null;

        /// <summary>
        /// Occurs when [after stop].
        /// </summary>
        public event MethodInvokedHandler AfterStop = null;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            Exception ex2 = null;
            if (this.BeforeStart != null) this.BeforeStart(ex2);

            try
            {
                ex2 = null;
                this.OnStart();
            }
            catch (Exception ex)
            {
                ex2 = ex;
            }
            if (this.AfterStart != null) this.AfterStart(ex2);
        }

        /// <summary>
        /// Called when [start].
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            Exception ex2 = null;
            if (this.BeforeStop != null) this.BeforeStop(ex2);

            try
            {
                ex2 = null;
                this.OnStop();
            }
            catch (Exception ex)
            {
                ex2 = ex;
            }
            if (this.AfterStop != null) this.AfterStop(ex2);
        }

        /// <summary>
        /// Called when [stop].
        /// </summary>
        protected abstract void OnStop();

        #endregion
    }
}
