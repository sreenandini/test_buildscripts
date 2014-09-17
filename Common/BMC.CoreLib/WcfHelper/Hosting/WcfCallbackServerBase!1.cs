using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Services;
using BMC.CoreLib.WcfHelper.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    public delegate void CallbackServerAfterSubscribedHandler<T>(T callback, object state)
        where T : IServiceContractBase;

    public abstract class WcfCallbackServerBase<T> : ListenerBase
        where T : IServiceContractBase
    {
        protected IList<T> _callbacks = null;
        protected IList<object> _callbackStates = null;
        protected object _callbacksLock = new object();
        protected ThinEvent _mreCallbackLock = new ThinEvent(false);
        private bool _logClients = false;
        private bool _hasClients = false;
        private readonly bool _supportsState = false;

        protected WcfCallbackServerBase(IExecutorService executor)
            : this(executor, false) { }

        protected WcfCallbackServerBase(IExecutorService executor, bool logClients)
            : this(executor, false, false)
        {
        }

        protected WcfCallbackServerBase(IExecutorService executor, bool logClients, bool supportsState)
            : base(executor)
        {
            _logClients = logClients;
            _callbacks = new List<T>();
            _callbackStates = new List<object>();
            _supportsState = supportsState;

            if (Extensions.UseTaskInsteadOfThread)
                Extensions.CreateLongRunningTask(this.OnListenCallbackClients);
            else
                Extensions.CreateThreadAndStart(new ThreadStart(this.OnListenCallbackClients),
                    string.Format("WcfCallbackServerBase<{0}>_OnListenCallbackClients_", typeof(T).Name));
        }

        public virtual void Subscribe()
        {
            this.Subscribe(null);
        }

        public virtual void Subscribe(object state)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Subscribe");

            try
            {
                T callback = OperationContext.Current.GetCallbackChannel<T>();
                this.Subscribe(callback, state);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public virtual void Subscribe(T callback)
        {
            this.Subscribe(callback, null);
        }

        public virtual void Subscribe(T callback, object state)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Subscribe");

            try
            {
                lock (_callbacksLock)
                {
                    if (callback != null &&
                        !_callbacks.Contains(callback))
                    {
                        Log.Info(PROC, ":::=> " + callback.ToString() + " is successfully added.");
                        _callbacks.Add(callback);
                        if (_supportsState) _callbackStates.Add(state);
                        this.OnAfterSubscribed(callback, state);
                    }
                    this.NotifyCallbackLock();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public virtual void Unsubscribe()
        {
            this.Unsubscribe(null);
        }

        public virtual void Unsubscribe(object state)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Unsubscribe");

            try
            {
                T callback = OperationContext.Current.GetCallbackChannel<T>();
                this.Unsubscribe(callback, state);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public virtual void Unsubscribe(T callback)
        {
            this.Unsubscribe(callback, null);
        }

        public virtual void Unsubscribe(T callback, object state)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Unsubscribe");

            try
            {
                lock (_callbacksLock)
                {
                    int idx = -1;
                    if (callback != null &&
                         (idx = _callbacks.IndexOf(callback)) != -1)
                    {
                        this.RemoveCallback(idx, state, false);
                    }
                    this.NotifyCallbackLock();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void RemoveCallback(int index, object state, bool clientDisconnected)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "RemoveCallback"))
            {
                try
                {
                    try
                    {
                        T callback = _callbacks[index];
                        Log.Info(":::=> " + callback.ToString() + " is successfully removed.");
                        if (_supportsState)
                        {
                            if (clientDisconnected) state = _callbackStates[index];
                        }
                        this.OnAfterUnsubscribed(callback, state);
                    }
                    finally
                    {
                        _callbacks.RemoveAt(index);
                        if (_supportsState)
                        {
                            _callbackStates.RemoveAt(index);
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public event CallbackServerAfterSubscribedHandler<T> AfterSubscribed = null;

        protected virtual void OnAfterSubscribed(T callback, object state)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnAfterSubscribed"))
            {
                try
                {
                    if (this.AfterSubscribed != null)
                    {
                        this.AfterSubscribed(callback, state);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public event CallbackServerAfterSubscribedHandler<T> AfterUnsubscribed = null;

        protected virtual void OnAfterUnsubscribed(T callback, object state)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnAfterSubscribed"))
            {
                try
                {
                    if (this.AfterUnsubscribed != null)
                    {
                        this.AfterUnsubscribed(callback, state);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void NotifyCallbackLock()
        {
            _hasClients = false;

            if (_callbacks.Count > 0)
            {
                _mreCallbackLock.Set();
                _hasClients = true;
            }
            else
            {
                _mreCallbackLock.Reset();
            }
        }

        private void OnListenCallbackClients()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnBroadcastMessages");

            try
            {
                while (!this.Executor.WaitForShutdown(10000))
                {
                    if (this.Executor.WaitAny(_mreCallbackLock.WaitHandle, -1) == 0) break;
                    this.MonitorCallbacks("OnListenCallbackClients", null);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public bool HasClients
        {
            get { return _hasClients; }
        }

        protected void MonitorCallbacks(string callee, Action<T> doCallback)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, (callee.IsEmpty() ? "" : "(" + callee + ") ") + "InvokeCallback");

            try
            {
                int length = _callbacks.Count;
                IList<int> faultCallbacks = new List<int>();
                if (_logClients &&
                    doCallback != null)
                {
                    Log.Info(PROC, string.Format("Sending {0} callback data to {1:D} clients.", callee, length));
                }

                ParallelLoopResult pResult = Parallel.For(0, length, (i) =>
                    {
                        if (this.Executor.IsShutdown) return;
                        if (!(i >= 0 && i < _callbacks.Count)) return;

                        try
                        {
                            T callback = _callbacks[i];
                            ICommunicationObject coCallback = callback as ICommunicationObject;

                            // callback inserted not from wcf channel
                            if (coCallback == null)
                            {
                                if (doCallback != null)
                                {
                                    doCallback(callback);
                                }
                            }
                            else
                            {
                                if (coCallback.State == CommunicationState.Opened)
                                {
                                    try
                                    {
                                        if (doCallback != null)
                                        {
                                            doCallback(callback);
                                        }
                                    }
                                    catch
                                    {
                                        faultCallbacks.Add(i);
                                    }
                                }
                                else
                                {
                                    faultCallbacks.Add(i);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(PROC, ex);
                        }
                    });

                // remove all the faulted callbacks
                if (faultCallbacks.Count > 0)
                {
                    lock (_callbacksLock)
                    {
                        foreach (var index in faultCallbacks)
                        {
                            this.RemoveCallback(index, null, true);
                        }
                    }

                    // notify the lock status
                    this.NotifyCallbackLock();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public virtual void InvokeCallback(Action<T> doCallback)
        {
            this.InvokeCallback(string.Empty, doCallback);
        }

        public virtual void InvokeCallback(string callee, Action<T> doCallback)
        {
            this.MonitorCallbacks(callee, doCallback);
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            this.UnsubscribeAll();
        }

        protected virtual void UnsubscribeAll() { }

        public bool WaitForCallbackClients()
        {
            return (this.Executor.WaitAny(_mreCallbackLock.WaitHandle, -1) == 1);
        }
    }

    public class WcfCallbackServerHelper<T>
        : WcfCallbackServerBase<T>
        where T : IServiceContractBase
    {
        public WcfCallbackServerHelper(IExecutorService executor)
            : base(executor)
        {
        }

        public WcfCallbackServerHelper(IExecutorService executor, bool logClients)
            : base(executor, logClients)
        {
        }

        public WcfCallbackServerHelper(IExecutorService executor, bool logClients, bool supportsState)
            : base(executor, logClients, supportsState)
        {
        }

        protected override bool StartInternal()
        {
            return true;
        }

        protected override bool StopInternal()
        {
            return true;
        }
    }
}
