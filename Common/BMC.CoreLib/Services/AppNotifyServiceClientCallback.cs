using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Concurrent;
using System.Threading;

namespace BMC.CoreLib.Services
{
    internal class AppNotifyServiceClientCallback : ExecutorBase, IAppNotifyServiceCallback
    {
        private IThreadSafeQueue<AppNotifyData> _queue = null;
        private IAppNotifyServiceCallback _callback = null;

        internal AppNotifyServiceClientCallback(IExecutorService executorService, IAppNotifyServiceCallback callback)
            : base(executorService)
        {
            _callback = callback;
            _queue = new BlockingBoundQueueUser<AppNotifyData>(executorService, -1);
            Extensions.CreateThreadAndStart(new ThreadStart(this.OnReceiveMessages), "AppNotifyServiceClientCallback_OnReceiveMessages_");
        }

        private void OnReceiveMessages()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnReceiveMessages");

            try
            {
                while (!this.ExecutorService.WaitForShutdown())
                {
                    try
                    {
                        AppNotifyData data = _queue.Dequeue();
                        if (data != null && _callback != null)
                        {
                            _callback.NotifyData(data);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.Shutdown();
            }
        }

        #region IAppNotifyServiceCallback Members

        public void NotifyData(AppNotifyData data)
        {
            _queue.Enqueue(data);
        }

        #endregion
    }
}
