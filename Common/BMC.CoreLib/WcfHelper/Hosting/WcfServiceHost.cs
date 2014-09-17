using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using BMC.CoreLib.WcfHelper.Behaviors;
using System.ServiceModel.Description;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    #region Wcf Windows Service Host
    /// <summary>
    /// Wcf Service Host
    /// </summary>
    public class WcfServiceHost : WcfServiceHostBase
    {
        public WcfServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses) { }

        public WcfServiceHost(object singletonInstance, params Uri[] baseAddresses)
            : base(singletonInstance, baseAddresses) { }

        public WcfServiceHost(Type serviceType, Type[] knownTypes, params Uri[] baseAddresses)
            : base(serviceType, knownTypes, baseAddresses) { }

        public WcfServiceHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
            : base(singletonInstance, knownTypes, baseAddresses) { }

        protected override void ApplyServiceBehaviorInternal(WcfServiceHostParam param)
        {
            base.ApplyServiceBehaviorInternal(param);
            this.AddDefaultServiceBehaviors();
            WcfCustomBehaviorBase serviceBehavior = this.CreateCustomBehavior();
            if (serviceBehavior != null)
            {
                param.ServiceBehaviors.Add(serviceBehavior);
            }
        }

        protected virtual WcfCustomBehaviorBase CreateCustomBehavior()
        {
            return new WcfCustomBehavior();
        }
    }

    public class WcfWebServiceHost : WcfWebServiceHostBase
    {
        private Type[] _knownTypes = null;
        private ServiceMetadataBehavior _serviceMetadata = null;

        public WcfWebServiceHost(Type serviceType, Type[] knownTypes, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            _knownTypes = knownTypes;
        }

        public WcfWebServiceHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
            : base(singletonInstance, baseAddresses)
        {
            _knownTypes = knownTypes;
        }

        protected virtual WcfCustomBehaviorBase CreateCustomBehavior()
        {
            return new WcfCustomBehavior();
        }

        protected override void ApplyServiceBehaviorInternal()
        {
            base.ApplyServiceBehaviorInternal();
            this.AddDefaultServiceBehaviors();

            IServiceBehavior behavior = this.Description.Behaviors.Find<WcfCustomBehavior>();
            if (behavior == null)
            {
                this.Description.Behaviors.Add(this.CreateCustomBehavior());
            }
        }

        private void AddDefaultServiceBehaviors()
        {
            ModuleProc PROC = new ModuleProc("ExOneServiceHost", "AddDefaultServiceBehaviors");

            try
            {
                // ServiceMetadataBehavior
                _serviceMetadata = this.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (_serviceMetadata == null)
                {
                    _serviceMetadata = new ServiceMetadataBehavior();
                    this.Description.Behaviors.Add(_serviceMetadata);
                }
                _serviceMetadata.HttpGetEnabled = false;
                //bServiceMetadata.HttpGetUrl = uriHttp;                            

                // ServiceDebugBehavior
                ServiceDebugBehavior bServiceDebug = this.Description.Behaviors.Find<ServiceDebugBehavior>();
                if (bServiceDebug == null)
                {
                    bServiceDebug = new ServiceDebugBehavior();
                    this.Description.Behaviors.Add(bServiceDebug);
                }
                bServiceDebug.IncludeExceptionDetailInFaults = false;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void AddOperationKnownTypes()
        {
            ModuleProc PROC = new ModuleProc("ExOneServiceHost", "AddOperationKnownTypes");

            try
            {
                if (_knownTypes != null)
                {
                    foreach (ServiceEndpoint ep in this.Description.Endpoints)
                    {
                        ContractDescription cd = ep.Contract;
                        if (cd.ContractType != typeof(IMetadataExchange))
                        {
                            foreach (OperationDescription od in cd.Operations)
                            {
                                foreach (Type knownType in _knownTypes)
                                {
                                    if (od.KnownTypes.IndexOf(knownType) == -1)
                                    {
                                        od.KnownTypes.Add(knownType);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        internal void AddHttpGetUrl(Uri getUri)
        {
            _serviceMetadata.HttpGetUrl = getUri;
        }

        protected override void ModifyBehaviorsOnOpening()
        {
            base.ModifyBehaviorsOnOpening();
            this.AddOperationKnownTypes();
        }
    }

    #endregion
}
