using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Executor;
using BMC.ExComms.Server.Transceiver;

namespace BMC.ExComms.Server
{
    internal partial class ExCommsServerImpl
    {
        /// <summary>
        /// Initialize_s the freeform executor.
        /// </summary>
        /// <returns></returns>
        private bool Initialize_FreeformExecutor()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Initialize_FreeformExecutor"))
            {
                bool result = false;

                try
                {
                    ExCommsExecutorFactory.StartThreads();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        /// <summary>
        /// Uninitialize_s the freeform executor.
        /// </summary>
        /// <returns></returns>
        private bool Uninitialize_FreeformExecutor()
        {
            bool result = true;
            ExCommsExecutorFactory.StopThreads();
            return result;
        }
    }
}
