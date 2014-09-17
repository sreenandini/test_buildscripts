using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using BMC.CoreLib;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    /// <summary>
    /// Wcf Service Host Wrapper
    /// </summary>
    public class WcfServiceHostWrapper
        : DisposableObject
    {
        /// <summary>
        /// Actual custom service host.
        /// </summary>
        private IWcfServiceHost _serviceHost = null;

        /// <summary>
        /// Service Host Factory
        /// </summary>
        private IWcfServiceHostFactory _serviceHostFactory = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsHostingServiceWrapper"/> class.
        /// </summary>
        public WcfServiceHostWrapper()
            : this(new WcfServiceHostFactory()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsHostingServiceWrapper"/> class.
        /// </summary>
        /// <param name="serviceHostFactory">The service host factory.</param>
        public WcfServiceHostWrapper(IWcfServiceHostFactory serviceHostFactory)
        {
            _serviceHostFactory = serviceHostFactory;
        }

        #region Members

        /// <summary>
        /// Starts the customizaed Wcf Service Host.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="hostConfig">The host config.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool Start(Type serviceType, Type[] knownTypes, params Uri[] baseAddresses)
        {
            ModuleProc PROC = new ModuleProc("WindowsHostingServiceWrapper", "Start");
            bool result = false;

            try
            {
                if (_serviceHost == null)
                {
                    Log.Info(PROC, "Initialize the service host object.");
                    _serviceHost = _serviceHostFactory.Create(serviceType, knownTypes, baseAddresses) as IWcfServiceHost;
                    Log.Info(PROC, "Successfully initialized the service host object.");
                    return _serviceHost.Start();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return result;
        }

        /// <summary>
        /// Stops the customizaed Wcf Service Host.
        /// </summary>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool Stop()
        {
            ModuleProc PROC = new ModuleProc("HostingServiceWrapper", "Stop");
            bool result = false;

            try
            {
                if (_serviceHost != null)
                {
                    _serviceHost.Stop();
                    _serviceHost.Dispose();
                    _serviceHost = null;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return result;
        }

        /// <summary>
        /// Overridable method which releases the managed resources.
        /// </summary>
        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            this.Stop();
        }
        #endregion
    }
}
