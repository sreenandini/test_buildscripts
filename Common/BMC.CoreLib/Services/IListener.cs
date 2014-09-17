// -----------------------------------------------------------------------
// <copyright file="IListener.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BMC.CoreLib.Services;
    using BMC.CoreLib.Concurrent;
    using BMC.CoreLib.Diagnostics;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IListener : IServiceHost
    {
        void Intialize();

        IExecutorService Executor { get; set; }
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class ListenerBase : DisposableObject, IListener
    {
        protected ListenerBase(IExecutorService executor)
        {
            this.Executor = executor;
        }

        #region IListener Members

        public void Intialize()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Initialize");

            try
            {
                this.InitializeInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void InitializeInternal() { }

        public IExecutorService Executor { get; set; }

        #endregion

        #region IServiceHost Members

        public bool Start()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Initialize");
            bool result = false;

            try
            {
                Log.Info(PROC, this.DYN_MODULE_NAME + " Start Called.");
                result = this.StartInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return true;
        }

        protected abstract bool StartInternal();

        public bool Stop()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Initialize");
            bool result = false;

            try
            {
                Log.Info(PROC, this.DYN_MODULE_NAME + " Stop Called.");
                result = this.StopInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return true;
        }

        protected abstract bool StopInternal();

        #endregion
    }
}
