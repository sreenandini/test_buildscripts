﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BMC.EventsTransmitter.STMServer {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="STMServer.IXMLDRService")]
    public interface IXMLDRService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IXMLDRService/process", ReplyAction="http://tempuri.org/IXMLDRService/processResponse")]
        int process(string xmlMessage);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IXMLDRServiceChannel : BMC.EventsTransmitter.STMServer.IXMLDRService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class XMLDRServiceClient : System.ServiceModel.ClientBase<BMC.EventsTransmitter.STMServer.IXMLDRService>, BMC.EventsTransmitter.STMServer.IXMLDRService {
        
        public XMLDRServiceClient() {
        }
        
        public XMLDRServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public XMLDRServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public XMLDRServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public XMLDRServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int process(string xmlMessage) {
            return base.Channel.process(xmlMessage);
        }
    }
}