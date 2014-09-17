using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Contracts;

namespace BMC.CoreLib.WcfHelper.Behaviors
{
    public abstract class WcfCustomBehaviorBase : DisposableObject,
        IServiceBehavior,
        IEndpointBehavior,
        IContractBehavior,
        IDispatchMessageInspector,
        IClientMessageInspector,
        IErrorHandler,
        IWsdlExportExtension
    {
        [CLSCompliant(false)]
        protected List<CustomHeaderExportInfo> _endpointHeaders = null;
        [CLSCompliant(false)]
        protected string _ipAddress = null;

        protected WcfCustomBehaviorBase()
        {
            _endpointHeaders = new List<CustomHeaderExportInfo>();
        }

        #region Overridable Members

        public virtual bool LogRequestMessage { get; set; }

        public virtual bool LogResponseMessage { get; set; }

        public virtual bool NeedOperationContext { get; set; }

        public virtual bool CustomExport { get; set; }

        public virtual bool SingleWsdl { get; set; }

        protected virtual void OnServiceDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase) { }

        protected virtual void OnEndpointDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }

        protected virtual void OnEndpointClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime) { }

        protected virtual void OnIterateChannelDispatcher(ChannelDispatcher dispatcher) { }

        protected virtual void OnIterateEndpointDispatcher(EndpointDispatcher dispatcher) { }

        protected virtual void OnIterateServiceEndpoint(ServiceEndpoint endpoint) { }

        protected virtual IEndpointBehavior[] CreateEndpointBehaviors(ServiceEndpoint endpoint) { return null; }

        protected virtual IDispatchMessageInspector[] CreateDispatchMessageInspectors(DispatchRuntime runtime) { return null; }

        protected virtual IClientMessageFormatter[] CreateClientMessageInspector(ClientRuntime runtime) { return null; }

        protected virtual IErrorHandler[] CreateErrorHandlers(ChannelDispatcher dispatcher) { return null; }

        protected virtual MessageFilter CreateContractFilter(EndpointDispatcher dispatcher) { return null; }

        protected virtual MessageFilter CreateAddressFilter(EndpointDispatcher dispatcher) { return null; }

        protected virtual IDispatchOperationSelector CreateOperationSelector(EndpointDispatcher dispatcher, IDispatchOperationSelector oldOperationSelector) { return null; }

        protected virtual void OnContractServiceOperation(DispatchOperation operation) { }

        protected virtual void OnContractServiceOperation(OperationDescription operation,
            bool hasMessages,
            MessageDescription msgRequest,
            MessageDescription msgResponse) { }

        protected virtual void OnContractClientOperation(OperationDescription operation,
            bool hasMessages,
            MessageDescription msgRequest,
            MessageDescription msgResponse) { }

        protected virtual void OnProcessRequestMessage(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel,
                                                        System.ServiceModel.InstanceContext instanceContext, OperationContext context) { }

        protected virtual void OnProcessResponseMessage(ref System.ServiceModel.Channels.Message reply, object correlationState) { }
        #endregion

        #region IServiceBehavior
        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters) { }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "WcfCustomBehaviorBase");

            try
            {
                // on process the service description
                this.OnServiceDispatchBehavior(serviceDescription, serviceHostBase);

                // on each service endpoints
                this.OnIterateServiceEndpoints(serviceDescription);

                // dispatchers
                foreach (ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
                {
                    // on each channel dispatcher
                    this.OnIterateChannelDispatcher(cd);

                    // Error handlers
                    IErrorHandler[] errorHandlers = this.CreateErrorHandlers(cd);
                    if (errorHandlers != null)
                    {
                        foreach (var errorHandler in errorHandlers)
                        {
                            Log.Info(PROC, "(" + cd.BindingName + ") Error Handler Added : " + errorHandler.ToString());
                            cd.ErrorHandlers.Add(errorHandler);
                        }
                    }

                    // on each endpoint dispatcher
                    foreach (EndpointDispatcher ep in cd.Endpoints)
                    {
                        // Contract Filter (Action)
                        MessageFilter contractFilter = this.CreateContractFilter(ep);
                        if (contractFilter != null)
                        {
                            Log.Info(PROC, "(" + ep.EndpointAddress.ToString() + ") Contract Filter Added : " + contractFilter.ToString());
                            ep.ContractFilter = contractFilter;
                        }

                        // Address Filter (To)
                        MessageFilter addressFilter = this.CreateAddressFilter(ep);
                        if (addressFilter != null)
                        {
                            Log.Info(PROC, "(" + ep.EndpointAddress.ToString() + ") Address Filter Added : " + addressFilter.ToString());
                            ep.AddressFilter = addressFilter;
                        }
                        
                        // Operation Selector
                        IDispatchOperationSelector operationSelector = this.CreateOperationSelector(ep, ep.DispatchRuntime.OperationSelector);
                        if (operationSelector != null)
                        {
                            Log.Info(PROC, "(" + ep.EndpointAddress.ToString() + ") Operation Selector Added : " + operationSelector.ToString());
                            ep.DispatchRuntime.OperationSelector = operationSelector;
                        }

                        // on each endpoint dispatcher
                        this.OnIterateEndpointDispatcher(ep);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void OnIterateServiceEndpoints(ServiceDescription description)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnIterateServiceEndpoints");

            try
            {
                // endpoint behaviors
                foreach (ServiceEndpoint sep in description.Endpoints)
                {
                    // not an mex end point
                    if (sep.Contract.ContractType != typeof(IMetadataExchange))
                    {
                        // on each endpoint
                        this.OnIterateServiceEndpoint(sep);

                        // endpoint behaviors
                        IEndpointBehavior[] endpointBehaviors = this.CreateEndpointBehaviors(sep);
                        if (endpointBehaviors != null)
                        {
                            foreach (var endpointBehavior in endpointBehaviors)
                            {
                                Log.Info(PROC, "(" + description.Name + ") Endpoint Behavior Added : " + endpointBehavior.ToString());
                                sep.Behaviors.Add(endpointBehavior);
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

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {

        }
        #endregion

        #region IEndpointBehavior

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters) { }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ApplyClientBehavior");

            try
            {
                // on process the endpoint description
                this.OnEndpointClientBehavior(endpoint, clientRuntime);

                // on process the endpoint operations
                this.ProcessContractOperations(endpoint, true);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ApplyDispatchBehavior");

            try
            {
                // on process the endpoint description
                this.OnEndpointDispatchBehavior(endpoint, endpointDispatcher);
                
                // dispatch message inspector
                DispatchRuntime runtime = endpointDispatcher.DispatchRuntime;                
                IDispatchMessageInspector[] messageInspectors = this.CreateDispatchMessageInspectors(runtime);
                if (messageInspectors != null)
                {
                    foreach (var messageInspector in messageInspectors)
                    {
                        Log.Info(PROC, "(" + endpoint.Address + ") Dispath Message Inspector Added : " + messageInspector.ToString());
                        runtime.MessageInspectors.Add(messageInspector);
                    }
                }

                // on process the endpoint operations
                this.ProcessContractOperations(endpointDispatcher);
                this.ProcessContractOperations(endpoint, false);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint) { }

        private void ProcessContractOperations(EndpointDispatcher ep)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ProcessContractOperations");

            try
            {
                if (ep.ContractName.EndsWith(typeof(IMetadataExchange).Name)) return;

                foreach (DispatchOperation dop in ep.DispatchRuntime.Operations)
                {
                    this.OnContractServiceOperation(dop);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
        private void ProcessContractOperations(ServiceEndpoint endpoint, bool isClient)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ProcessServiceOperations");

            try
            {
                ContractDescription contractDesc = endpoint.Contract;
                foreach (OperationDescription oprDesc in contractDesc.Operations)
                {
                    MessageDescription msgRequest = null;
                    MessageDescription msgResponse = null;

                    if (oprDesc.Messages != null)
                    {
                        foreach (MessageDescription msgDesc in oprDesc.Messages)
                        {
                            if (msgDesc.Direction == MessageDirection.Input)
                                msgRequest = msgDesc;
                            else if (msgDesc.Direction == MessageDirection.Output)
                                msgResponse = msgDesc;
                        }
                    }

                    // customize the service operation
                    try
                    {
                        if (!isClient)
                        {
                            this.OnContractServiceOperation(oprDesc, (msgRequest != null || msgResponse != null),
                                msgRequest, msgResponse);
                        }
                        else
                        {
                            this.OnContractClientOperation(oprDesc, (msgRequest != null || msgResponse != null),
                                msgRequest, msgResponse);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }

                    if (!isClient)
                    {
                        // Fault contracts
                        try
                        {
                            FaultDescription faultDesc = oprDesc.Faults.Find(WcfExceptionDetail.ACTION);
                            if (faultDesc == null)
                            {
                                faultDesc = new FaultDescription(WcfExceptionDetail.ACTION);
                                faultDesc.Namespace = WcfExceptionDetail.NS;
                                faultDesc.Name = WcfExceptionDetail.NAME;
                                faultDesc.DetailType = typeof(WcfExceptionDetail);
                                //oprDesc.Faults.Add(faultDesc);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(PROC, ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region IContractBehavior
        void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters) { }

        void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }

        void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime) { }

        void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint) { }
        #endregion

        #region IDispatchMessageInspector

        object IDispatchMessageInspector.AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AfterReceiveRequest");

            try
            {
                this.InspectRequestMessage(ref request, channel, instanceContext);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return null;
        }

        void IDispatchMessageInspector.BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "BeforeSendReply");

            try
            {
                if (this.LogResponseMessage)
                {
                    string message = string.Empty;
                    if (reply != null) message = reply.ToString();
                    this.OnLogResponseMessage(PROC, message);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void OnLogRequestMessage(ModuleProc PROC, string message)
        {
            Log.Info(PROC, "Received Request : " + message);
        }

        protected virtual void OnLogResponseMessage(ModuleProc PROC, string message)
        {
            Log.Info(PROC, "Response Sent : " + message);
        }

        protected virtual void InspectRequestMessage(ref System.ServiceModel.Channels.Message request,
                                                    System.ServiceModel.IClientChannel channel,
                                                    System.ServiceModel.InstanceContext instanceContext)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "LogReceiveRequest");

            try
            {
                if (this.LogRequestMessage)
                {
                    this.OnLogRequestMessage(PROC, request.ToString());
                }

                // process the message 
                try
                {
                    OperationContext context = null;
                    OperationContextScope scope = null;

                    if (this.NeedOperationContext &&
                        OperationContext.Current != null)
                    {
                        context = OperationContext.Current;
                    }

                    try
                    {
                        if (this.NeedOperationContext &&
                            context == null)
                        {
                            scope = new OperationContextScope(channel);
                            context = OperationContext.Current;
                        }

                        this.OnProcessRequestMessage(ref request, channel, instanceContext, context);
                    }
                    finally
                    {
                        if (scope != null)
                        {
                            scope.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected T GetMessageHeaderData<T>(ref System.ServiceModel.Channels.Message message,
                                           OperationContext context,
                                           string headerNamespace, string headerName)
           where T : class
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetMessageHeaderData");
            T result = default(T);

            try
            {
                if (message != null)
                {
                    if (!headerNamespace.IsEmpty() &&
                        !headerName.IsEmpty())
                    {
                        if (context != null &&
                            context.IncomingMessageHeaders != null)
                        {
                            MessageHeaders headers = context.IncomingMessageHeaders;
                            int index = headers.FindHeader(headerName, headerNamespace);
                            if (index != -1)
                            {
                                result = DeserializeDC<T>(headers.GetReaderAtHeader(index));
                                headers.RemoveAt(index);
                            }
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

        private static T DeserializeDC<T>(XmlDictionaryReader reader)
            where T : class
        {
            ModuleProc PROC = new ModuleProc("WcfCustomBehaviorBase", "DeserializeDC");
            T result = null;

            try
            {
                T obj = null;

                try
                {
                    string outerXml = reader.ReadOuterXml();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                        StreamWriter sw = new StreamWriter(ms);
                        sw.Write(outerXml);
                        sw.Flush();
                        ms.Position = 0;
                        obj = serializer.ReadObject(ms) as T;
                    }
                }
                catch { obj = null; }
                finally
                {
                    if (obj != null)
                    {
                        result = obj;
                    }
                    else
                    {
                        Log.Info(PROC, string.Format("Value passed is not the instance of [{0}]."));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private static T DeserializeXml<T>(XmlDictionaryReader reader)
            where T : class
        {
            ModuleProc PROC = new ModuleProc("WcfCustomBehaviorBase", "DeserializeXml");
            T result = null;

            try
            {
                T obj = null;

                try
                {
                    string outerXml = reader.ReadOuterXml();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        StreamWriter sw = new StreamWriter(ms);
                        sw.Write(outerXml);
                        sw.Flush();
                        ms.Position = 0;
                        obj = serializer.Deserialize(ms) as T;
                    }
                }
                catch { obj = null; }
                finally
                {
                    if (obj != null)
                    {
                        result = obj;
                    }
                    else
                    {
                        Log.Info(PROC, string.Format("Value passed is not the instance of [{0}]."));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public static T GetMessageHeaderDataDC<T>(MessageHeaders headers,
                                                 string headerNamespace, string headerName)
             where T : class
        {
            ModuleProc PROC = new ModuleProc("WcfCustomBehaviorBase", "GetMessageHeaderDataDC");
            T result = default(T);

            try
            {
                if (headers != null)
                {
                    if (!headerNamespace.IsEmpty() &&
                        !headerName.IsEmpty())
                    {
                        int index = headers.FindHeader(headerName, headerNamespace);
                        if (index != -1)
                        {
                            result = DeserializeDC<T>(headers.GetReaderAtHeader(index));
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

        public static T GetMessageHeaderDataXml<T>(MessageHeaders headers,
                                                   string headerNamespace, string headerName)
            where T : class
        {
            ModuleProc PROC = new ModuleProc("WcfCustomBehaviorBase", "GetMessageHeaderDataXml");
            T result = default(T);

            try
            {
                if (headers != null)
                {
                    if (!headerNamespace.IsEmpty() &&
                        !headerName.IsEmpty())
                    {
                        int index = headers.FindHeader(headerName, headerNamespace);
                        if (index != -1)
                        {
                            result = DeserializeXml<T>(headers.GetReaderAtHeader(index));
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

        #endregion

        #region IErrorHandler
        bool IErrorHandler.HandleError(Exception error)
        {
            return true;
        }

        void IErrorHandler.ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            // always raise exceptions as FaultException<ExceptionDetail>, it is compulsary
            FaultException<WcfExceptionDetail> exFault = null;

            // already a fault exception of exception detail?
            if (error is FaultException<WcfExceptionDetail>)
            {
                exFault = error as FaultException<WcfExceptionDetail>;
            }
            else
            {
                // not yet, so raise as a fault exception
                WcfExceptionDetail exDetail = new WcfExceptionDetail(error);
                exFault = new FaultException<WcfExceptionDetail>(exDetail, new FaultReason(error.Message));
            }

            // converts the original message into fault message
            MessageFault msgFault = exFault.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, WcfExceptionDetail.ACTION);
        }
        #endregion

        #region IWsdlExportExtension Members

        /// <summary>
        /// Writes custom Web Services Description Language (WSDL) elements into the generated WSDL for a contract.
        /// </summary>
        /// <param name="exporter">The <see cref="T:System.ServiceModel.Description.WsdlExporter"/> that exports the contract information.</param>
        /// <param name="context">Provides mappings from exported WSDL elements to the contract description.</param>
        void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context) { }

        /// <summary>
        /// Writes custom Web Services Description Language (WSDL) elements into the generated WSDL for an endpoint.
        /// </summary>
        /// <param name="exporter">The <see cref="T:System.ServiceModel.Description.WsdlExporter"/> that exports the endpoint information.</param>
        /// <param name="context">Provides mappings from exported WSDL elements to the endpoint description.</param>
        void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ExportEndpoint");

            try
            {
                if (this.SingleWsdl)
                {
                    using (WcfSingleWsdlExportEndpoint wsdlExport = new WcfSingleWsdlExportEndpoint(exporter, context))
                    {
                        wsdlExport.ExportEndpoint();
                    }
                }

                if (this.CustomExport)
                {
                    this.GetEndpointExportedHeaders(_endpointHeaders);
                    if (_endpointHeaders.Count > 0)
                    {
                        using (WcfCustomWsdlExportEndpoint wsdlExport = new WcfCustomWsdlExportEndpoint(exporter, context))
                        {
                            foreach (CustomHeaderExportInfo info in _endpointHeaders)
                            {
                                try
                                {
                                    wsdlExport.Export(info);
                                }
                                finally
                                {
                                    Log.Info(PROC, string.Format("[{0}] custom header was successfully added.", info.ExportName));
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

        /// <summary>
        /// Gets the endpoint exported headers.
        /// </summary>
        /// <param name="infos">The infos.</param>
        protected virtual void GetEndpointExportedHeaders(List<CustomHeaderExportInfo> endpointHeaders) { }

        protected void AddMessageHeader(MessageDescription message, string headerNamespace, string headerName, Type headerType)
        {
            if (message != null)
            {
                MessageBodyDescription messageBody = message.Body;
                if (messageBody != null)
                {
                    if (headerNamespace.IsEmpty())
                    {
                        if (!messageBody.WrapperNamespace.IsEmpty())
                        {
                            headerNamespace = messageBody.WrapperNamespace;
                            if (!messageBody.WrapperName.IsEmpty())
                            {
                                headerNamespace += "." + messageBody.Parts[1].Name;
                            }
                        }
                        else if (messageBody.Parts != null && messageBody.Parts.Count > 0)
                        {
                            headerNamespace = messageBody.Parts[0].Namespace;
                        }
                    }

                    if (!headerNamespace.IsEmpty())
                    {
                        MessageHeaderDescription header = (from h in message.Headers.OfType<MessageHeaderDescription>()
                                                           where h.Name.IgnoreCaseCompare(headerName) &&
                                                                   h.Namespace.IgnoreCaseCompare(headerNamespace)
                                                           select h).FirstOrDefault();
                        if (header == null)
                        {
                            header = new MessageHeaderDescription(headerName, headerNamespace);
                            header.MustUnderstand = true;
                            header.MemberInfo = headerType;
                            header.Type = headerType;
                            header.TypedHeader = true;
                            message.Headers.Add(header);
                        }
                    }
                }
            }
        }

        #endregion

        #region IClientMessageInspector Members

        private void PopulteIPAddress()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "PopulteIPAddress");

            try
            {
                string hostName = string.Empty;
                IPAddress addr = Extensions.GetIpAddress(0, out hostName);
                if (addr != null)
                {
                    _ipAddress = string.Format("{0} [{1}]", hostName, addr.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application.
        /// </summary>
        /// <param name="reply">The message to be transformed into types and handed back to the client application.</param>
        /// <param name="correlationState">Correlation state data.</param>
        void IClientMessageInspector.AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AfterReceiveReply");

            try
            {
                this.OnAfterReceiveReply(ref reply);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application.
        /// </summary>
        /// <param name="reply">The message to be transformed into types and handed back to the client application.</param>
        protected virtual void OnAfterReceiveReply(ref System.ServiceModel.Channels.Message reply) { }

        /// <summary>
        /// Enables inspection or modification of a message before a request message is sent to a service.
        /// </summary>
        /// <param name="request">The message to be sent to the service.</param>
        /// <param name="channel">The WCF client object channel.</param>
        /// <returns>
        /// The object that is returned as the <paramref name="correlationState "/>argument of the <see cref="M:System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object)"/> method. This is null if no correlation state is used.The best practice is to make this a <see cref="T:System.Guid"/> to ensure that no two <paramref name="correlationState"/> objects are the same.
        /// </returns>
        object IClientMessageInspector.BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "BeforeSendRequest");

            try
            {
                this.OnBeforeSendRequest(ref request);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return null;
        }

        /// <summary>
        /// Enables inspection or modification of a message before a request message is sent to a service.
        /// </summary>
        /// <param name="request">The message to be sent to the service.</param>
        protected virtual void OnBeforeSendRequest(ref System.ServiceModel.Channels.Message request) { }

        /// <summary>
        /// Adds the message header.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <param name="ns">The ns.</param>
        /// <param name="createHeader">The create header.</param>
        protected virtual MessageHeader AddMessageHeader(ref System.ServiceModel.Channels.Message request,
                                                        string name, string ns, Func<MessageHeader> createHeader)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddMessageHeader");
            MessageHeader result = null;

            try
            {
                MessageHeaders headers = request.Headers;
                if (headers != null)
                {
                    if (headers.FindHeader(name, ns) == -1)
                    {
                        headers.Add((result = createHeader()));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        #endregion
    }
}
