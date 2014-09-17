/* ================================================================================= 
 * Purpose		:	Queue Process Mode
 * File Name	:   QueueProcessMode.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	02/04/11
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 02/04/11		A.Vinod Kumar    Initial Version
 * ===============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Diagnostics
{
    /// <summary>
    /// QueueProcessMode
    /// </summary>
    public enum QueueProcessMode {
        /// <summary>
        /// Synchronous
        /// </summary>
        Synchronous = 0,
        /// <summary>
        /// Asynchronous
        /// </summary>
        Asynchronous = 1,
        /// <summary>
        /// Thread
        /// </summary>
        Thread = 2
    }
}
