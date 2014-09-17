/* ================================================================================= 
 * Purpose		:	Core Library Manager
 * File Name	:   CoreLinManager.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	10/02/2012
 * ================================================================================= 
 * Copyright (C) 2012 Bally Technologies, Inc. All rights reserved.
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 10/02/2012		A.Vinod Kumar    Initial Version
 * ================================================================================= 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib
{
    /// <summary>
    /// Core Library Manager
    /// </summary>
    public static class CoreLibManager
    {
        /// <summary>
        /// Initializes the <see cref="CoreLibManager"/> class.
        /// </summary>
        static CoreLibManager()
        {
            ExecutorService = ExecutorServiceFactory.CreateExecutorService();
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomain_DomainUnload);
        }

        /// <summary>
        /// Handles the DomainUnload event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            ExecutorService.Shutdown();
        }

        /// <summary>
        /// Gets or sets the executor service.
        /// </summary>
        /// <value>The executor service.</value>
        public static IExecutorService ExecutorService { get; private set; }
    }

    public static class ShutdownHelper
    {
        public static void Shutdown()
        {
            try
            {
                if (ShutdownInitiated != null)
                {
                    ShutdownInitiated(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }
        }

        public static event EventHandler ShutdownInitiated = null;
    }
}
