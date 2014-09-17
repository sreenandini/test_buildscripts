using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using BMC.CoreLib.Diagnostics;
using System.Reflection;

namespace BMC.CoreLib.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]    
    public class AppNotifyService : IAppNotifyService
    {
        private IAppNotifyServiceCallback _callback = null;

        static event EventHandler<AppNotifyDataEventArgs> NotifyData = null;

        public AppNotifyService() { }

        #region IAppNotifyService Members

        public void Subscribe()
        {
            ModuleProc PROC = new ModuleProc("AppNotifyService", "Subscribe");

            try
            {
                _callback = OperationContext.Current.GetCallbackChannel<IAppNotifyServiceCallback>();
                NotifyData += new EventHandler<AppNotifyDataEventArgs>(AppNotifyService_NotifyData);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Unsubscribe()
        {
            ModuleProc PROC = new ModuleProc("AppNotifyService", "Subscribe");

            try
            {
                NotifyData -= new EventHandler<AppNotifyDataEventArgs>(AppNotifyService_NotifyData);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        void AppNotifyService_NotifyData(object sender, AppNotifyDataEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("AppNotifyService", "Subscribe");

            try
            {
                if (((ICommunicationObject)_callback).State == CommunicationState.Opened)
                {
                    try
                    {
                        _callback.NotifyData(e.Data);
                    }
                    catch
                    {
                        this.Unsubscribe();
                    }
                }
                else
                {
                    this.Unsubscribe();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                this.Unsubscribe();
            }
        }

        internal static void OnNotifyData(AppNotifyData data)
        {
            ModuleProc PROC = new ModuleProc("AppNotifyService", "OnNotifyData");

            try
            {
                if (NotifyData != null)
                {
                    NotifyData(null, new AppNotifyDataEventArgs(data));
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }

    public class AppNotifyDataEventArgs : EventArgs
    {
        public AppNotifyDataEventArgs(AppNotifyData data)
        {
            this.Data = data;
        }

        public AppNotifyData Data { get; set; }
    }
}
