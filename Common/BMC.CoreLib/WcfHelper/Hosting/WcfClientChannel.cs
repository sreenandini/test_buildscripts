using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using BMC.CoreLib.WcfHelper.Contracts;
using BMC.CoreLib.Diagnostics;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using BMC.CoreLib.WcfHelper.Behaviors;
using BMC.CoreLib.Services;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    public class WcfClientChannel<TChannel>
        : DisposableObjectNotify
        where TChannel : IServiceContractBase
    {
        private ChannelFactory<TChannel> _factory = null;
        private bool _isFixedChannel = false;
        private InstanceContext _callbackInstance = null;
        private IServiceCallbackContractBase _callbackInstanceLocal = null;

        private Func<ChannelFactory<TChannel>> _createChannelFactory = null;
        private TChannel _innerChannel = default(TChannel);
        private ICommunicationObject _commChannel = null;
        private object _channelLock = new object();
        private IEnumerable<Type> _knownTypes = null;

        private WcfClientChannel()
        {
            _createChannelFactory = () => { return new ChannelFactory<TChannel>(this.ClientBinding); };
        }

        public WcfClientChannel(Binding clientBinding, string address)
            : this((InstanceContext)null, clientBinding, address, null) { }

        public WcfClientChannel(Binding clientBinding, string address, IEnumerable<Type> knownTypes)
            : this((InstanceContext)null, clientBinding, address, knownTypes) { }

        public WcfClientChannel(IServiceCallbackContractBase callbackInstance, Binding clientBinding, string address)
            : this(new InstanceContext(callbackInstance), clientBinding, address, null) { }

        public WcfClientChannel(IServiceCallbackContractBase callbackInstance, Binding clientBinding, string address, IEnumerable<Type> knownTypes)
            : this(new InstanceContext(callbackInstance), clientBinding, address, knownTypes) { }

        public WcfClientChannel(InstanceContext callbackInstance, Binding clientBinding, string address)
            : this(callbackInstance, clientBinding, address, null) { }

        public WcfClientChannel(InstanceContext callbackInstance, Binding clientBinding, string address, IEnumerable<Type> knownTypes)
            : this()
        {
            _knownTypes = knownTypes;
            this.ClientBinding = clientBinding;
            this.Address = new EndpointAddress(address);
            _callbackInstance = callbackInstance;
            if (callbackInstance != null)
            {
                _createChannelFactory = () => { return new DuplexChannelFactory<TChannel>(_callbackInstance, this.ClientBinding); };
            }
            this.EnsureChannelOpened();
        }

        public WcfClientChannel(TChannel innerChannel)
            : this()
        {
            _innerChannel = innerChannel;
            _isFixedChannel = (innerChannel != null);
            this.EnsureChannelOpened();
        }

        public WcfClientChannel(TChannel innerChannel, IServiceCallbackContractBase callbackInstanceLocal)
            : this(innerChannel)
        {
            _callbackInstanceLocal = callbackInstanceLocal;
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            if (_factory != null)
            {
                _factory.Close();
                _factory = null;
            }
        }

        public TChannel InnerChannel
        {
            get
            {
                if (!_isFixedChannel)
                {
                    if (_innerChannel == null)
                    {
                        lock (_channelLock)
                        {
                            if (_innerChannel == null)
                            {
                                this.EnsureChannelOpened();
                            }
                        }
                    }
                }
                return _innerChannel;
            }
        }

        public IServiceCallbackContractBase CallbackInstanceLocal
        {
            get { return _callbackInstanceLocal; }
        }

        public Binding ClientBinding { get; private set; }

        public EndpointAddress Address { get; private set; }

        public ServiceEndpoint EndPoint
        {
            get
            {
                if (_factory != null)
                {
                    return _factory.Endpoint;
                }
                return null;
            }
        }

        public IEnumerable<Type> KnownTypes
        {
            get { return _knownTypes; }
            set { _knownTypes = value; }
        }

        private void EnsureKnownTypesAdded()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "EnsureKnownTypesAdded");

            try
            {
                if (_factory.Endpoint == null) return;

                ContractDescription cd = _factory.Endpoint.Contract;
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
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected bool Open()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Close");
            bool result = default(bool);

            try
            {
                if (!_isFixedChannel)
                {
                    this.Close();
                    if (_factory == null)
                    {
                        _factory = _createChannelFactory();
                        if (_knownTypes != null)
                        {
                            this.EnsureKnownTypesAdded();
                        }
                        _factory.Open();
                    }
                    _innerChannel = _factory.CreateChannel(this.Address);
                    _commChannel = _innerChannel as ICommunicationObject;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected bool Close()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Close");
            bool result = default(bool);

            try
            {
                if (!_isFixedChannel)
                {
                    if (_commChannel != null)
                    {
                        CommunicationState state = _commChannel.State;
                        if (state != CommunicationState.Faulted)
                        {
                            _commChannel.Close();
                            _commChannel = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public bool IsChannelAvailable
        {
            get
            {
                if (_isFixedChannel)
                {
                    return true;
                }
                else
                {
                    if (_commChannel != null)
                    {
                        return (_commChannel.State == CommunicationState.Opened);
                    }
                }
                return false;
            }
        }

        private void EnsureChannelOpened()
        {
            if (!this.IsChannelAvailable)
            {
                this.Open();
            }
        }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <typeparam name="S">Type of the request.</typeparam>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="workDelegate">The work delegate.</param>
        /// <returns>Message response.</returns>
        protected TResponse InvokeMethod<TResponse>(Func<TChannel, TResponse> workDelegate)
        {
            WcfExceptionDetail exception = null;
            this.LastExceptionDetail = null;
            TResponse response = default(TResponse);

            try
            {
                this.EnsureChannelOpened();
                response = workDelegate(this.InnerChannel);
            }
            catch (FaultException<WcfExceptionDetail> ex)
            {
                exception = ex.Detail;
            }
            catch (FaultException ex)
            {
                exception = new WcfExceptionDetail(ex);
            }
            catch (CommunicationException ex)
            {
                exception = new WcfExceptionDetail(ex);
                this.Close();
            }
            catch (ObjectDisposedException ex)
            {
                exception = new WcfExceptionDetail(ex);
                this.Close();
            }
            catch (TimeoutException ex)
            {
                exception = new WcfExceptionDetail(ex);
                this.Close();
            }
            catch (Exception ex)
            {
                exception = new WcfExceptionDetail(ex);
            }
            finally
            {
                if (response == null)
                {
                    response = Activator.CreateInstance<TResponse>();
                }
                if (response != null &&
                    response is WcfMessageContextBase)
                {
                    WcfMessageContextBase context = response as WcfMessageContextBase;
                    context.Exception = exception;
                }
            }

            this.LastExceptionDetail = exception;
            return response;
        }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <typeparam name="S">Type of the request.</typeparam>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="workDelegate">The work delegate.</param>
        /// <returns>Message response.</returns>
        protected void InvokeMethod(Action<TChannel> workDelegate)
        {
            WcfExceptionDetail exception = null;
            this.LastExceptionDetail = null;

            try
            {
                this.EnsureChannelOpened();
                workDelegate(this.InnerChannel);
            }
            catch (FaultException<WcfExceptionDetail> ex)
            {
                exception = ex.Detail;
            }
            catch (FaultException ex)
            {
                exception = new WcfExceptionDetail(ex);
            }
            catch (CommunicationException ex)
            {
                exception = new WcfExceptionDetail(ex);
                this.Close();
            }
            catch (ObjectDisposedException ex)
            {
                exception = new WcfExceptionDetail(ex);
                this.Close();
            }
            catch (TimeoutException ex)
            {
                exception = new WcfExceptionDetail(ex);
                this.Close();
            }
            catch (Exception ex)
            {
                exception = new WcfExceptionDetail(ex);
            }

            this.LastExceptionDetail = exception;
        }

        public WcfExceptionDetail LastExceptionDetail { get; private set; }

        protected override void ToString(StringBuilder sb)
        {
            if (_isFixedChannel)
            {
                sb.AppendFormat("Local Service : {0}", this.InnerChannel.GetType().FullName);
            }
            else
            {
                sb.AppendFormat("Remote Service : " + this.ClientBinding.Name + "/" + this.Address.Uri.AbsoluteUri);
            }
        }
    }

    public class WcfClientChannelListener<TChannel, TCallback>
        : WcfClientChannel<TChannel>, IListener
        where TChannel : IServiceContractBase
        where TCallback : IServiceCallbackContractBase
    {
        #region Constructors
        public WcfClientChannelListener(Binding clientBinding, string address, IEnumerable<Type> knownTypes)
            : base(clientBinding, address, knownTypes) { }

        public WcfClientChannelListener(TChannel innerChannel)
            : base(innerChannel) { }

        public WcfClientChannelListener(TCallback callbackInstance, Binding clientBinding, string address, IEnumerable<Type> knownTypes)
            : base((IServiceCallbackContractBase)callbackInstance, clientBinding, address, knownTypes) { }

        public WcfClientChannelListener(TChannel innerChannel, TCallback callbackInstance)
            : base(innerChannel, callbackInstance) { }
        #endregion

        public virtual void Intialize() { }

        public virtual Concurrent.IExecutorService Executor { get; set; }

        public virtual bool Start()
        {
            return true;
        }

        public virtual bool Stop()
        {
            return true;
        }
    }
}
