﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4984
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BMC.EBSProxy
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RequestType", Namespace="http://schemas.datacontract.org/2004/07/BallyTech.Bonusing.Core.ContractsLibrary")]
    public partial class RequestType : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string RequestField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Request
        {
            get
            {
                return this.RequestField;
            }
            set
            {
                this.RequestField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResponseType", Namespace="http://schemas.datacontract.org/2004/07/BallyTech.Bonusing.Core.ContractsLibrary")]
    public partial class ResponseType : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string ResponseField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Response
        {
            get
            {
                return this.ResponseField;
            }
            set
            {
                this.ResponseField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BMC.EBSProxy.IS2S")]
    public interface IS2S
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS2S/S2SMessagePostOperation", ReplyAction="http://tempuri.org/IS2S/S2SMessagePostOperationResponse")]
        BMC.EBSProxy.ResponseType S2SMessagePostOperation(BMC.EBSProxy.RequestType request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IS2SChannel : BMC.EBSProxy.IS2S, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class S2SClient : System.ServiceModel.ClientBase<BMC.EBSProxy.IS2S>, BMC.EBSProxy.IS2S
    {
        
        public S2SClient()
        {
        }
        
        public S2SClient(string endpointConfigurationName) : 
                base(endpointConfigurationName)
        {
        }
        
        public S2SClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public S2SClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public S2SClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public BMC.EBSProxy.ResponseType S2SMessagePostOperation(BMC.EBSProxy.RequestType request)
        {
            return base.Channel.S2SMessagePostOperation(request);
        }
    }
}