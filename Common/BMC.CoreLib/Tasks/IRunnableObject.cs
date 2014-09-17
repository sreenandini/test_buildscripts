/* ================================================================================= 
 * Purpose		:	Runnable Object Interface
 * File Name	:   IRunnableObject.cs
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

namespace BMC.CoreLib.Tasks {
    /// <summary>
    /// Runnable Object Interface
    /// </summary>
    public interface IRunnableObject {
        /// <summary>
        /// Occurs when [before start].
        /// </summary>
        event MethodInvokedHandler BeforeStart;
        /// <summary>
        /// Occurs when [after start].
        /// </summary>
        event MethodInvokedHandler AfterStart;

        /// <summary>
        /// Occurs when [before stop].
        /// </summary>
        event MethodInvokedHandler BeforeStop;

        /// <summary>
        /// Occurs when [after stop].
        /// </summary>
        event MethodInvokedHandler AfterStop;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
    }
}
