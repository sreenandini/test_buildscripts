using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Concurrent
{
    public abstract class ExecutorServiceThreadBase : ExecutorServiceBase
    {
        protected Thread _thFunc = null;
        private InitializeStatus _thStatus = InitializeStatus.Uninitialized;
        private object _thLock = new object();

        private string _threadName = string.Empty;
        private int _waitTimeout = 100;

        public ExecutorServiceThreadBase(IExecutorService executorService, string threadName, int waitTimeout)
            : base(executorService)
        {
            _threadName = threadName;
            _waitTimeout = (waitTimeout <= 0 ? 10 : waitTimeout);
        }

        public virtual void Initialize()
        {
            Extensions.InitializeThreadFunc(ref _thFunc, ref _thStatus, _thStatus, new ThreadStart(this.DoWork), _threadName);
        }

        private void DoWork()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "DoWork");

            try
            {
                while (!this.ExecutorService.WaitForShutdown(_waitTimeout))
                {
                    try
                    {
                        this.DoWorkInternal();
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
        }

        protected abstract void DoWorkInternal();
    }
}
