﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3620
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.1432.
// 
namespace BallyTech.Bonusing.Core.SlotSystemGateway.Proxy
{
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "S2SEndPointBinding", Namespace = "http://www.ballytech.com/sds/s2s")]
    public partial class S2SEndPointService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback processS2SMessageOperationCompleted;

        /// <remarks/>
        public S2SEndPointService(string url)
        {
            this.Url = url;
        }

        /// <remarks/>
        public event processS2SMessageCompletedEventHandler processS2SMessageCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("processS2SMessageResponse", Namespace = "http://www.ballytech.com/sds/s2s")]
        public processS2SMessageResponse processS2SMessage([System.Xml.Serialization.XmlElementAttribute("processS2SMessage", Namespace = "http://www.ballytech.com/sds/s2s")] processS2SMessage processS2SMessage1)
        {
            object[] results = this.Invoke("processS2SMessage", new object[] {
                        processS2SMessage1});
            return ((processS2SMessageResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginprocessS2SMessage(processS2SMessage processS2SMessage1, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("processS2SMessage", new object[] {
                        processS2SMessage1}, callback, asyncState);
        }

        /// <remarks/>
        public processS2SMessageResponse EndprocessS2SMessage(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((processS2SMessageResponse)(results[0]));
        }

        /// <remarks/>
        public void processS2SMessageAsync(processS2SMessage processS2SMessage1)
        {
            this.processS2SMessageAsync(processS2SMessage1, null);
        }

        /// <remarks/>
        public void processS2SMessageAsync(processS2SMessage processS2SMessage1, object userState)
        {
            if ((this.processS2SMessageOperationCompleted == null))
            {
                this.processS2SMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnprocessS2SMessageOperationCompleted);
            }
            this.InvokeAsync("processS2SMessage", new object[] {
                        processS2SMessage1}, this.processS2SMessageOperationCompleted, userState);
        }

        private void OnprocessS2SMessageOperationCompleted(object arg)
        {
            if ((this.processS2SMessageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.processS2SMessageCompleted(this, new processS2SMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/s2s")]
    public partial class processS2SMessage
    {

        private string s2sMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string s2sMessage
        {
            get
            {
                return this.s2sMessageField;
            }
            set
            {
                this.s2sMessageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/s2s")]
    public partial class processS2SMessageResponse
    {

        private string returnField;

        /// <remarks/>
        public string @return
        {
            get
            {
                return this.returnField;
            }
            set
            {
                this.returnField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    public delegate void processS2SMessageCompletedEventHandler(object sender, processS2SMessageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class processS2SMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal processS2SMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public processS2SMessageResponse Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((processS2SMessageResponse)(this.results[0]));
            }
        }
    }
}


////------------------------------------------------------------------------------
//// <auto-generated>
////     This code was generated by a tool.
////     Runtime Version:2.0.50727.3620
////
////     Changes to this file may cause incorrect behavior and will be lost if
////     the code is regenerated.
//// </auto-generated>
////------------------------------------------------------------------------------

//// 
//// This source code was auto-generated by wsdl, Version=2.0.50727.1432.
//// 
//namespace BallyTech.Bonusing.Core.SlotSystemGateway.Proxy
//{
//    using System.Diagnostics;
//    using System.Web.Services;
//    using System.ComponentModel;
//    using System.Web.Services.Protocols;
//    using System;
//    using System.Xml.Serialization;


//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Web.Services.WebServiceBindingAttribute(Name = "S2SEndPointBinding", Namespace = "http://www.ballytech.com/sds/s2s")]
//    public partial class S2SEndPointService : System.Web.Services.Protocols.SoapHttpClientProtocol
//    {

//        private System.Threading.SendOrPostCallback processS2SMessageOperationCompleted;

//        /// <remarks/>
//        public S2SEndPointService()
//        {
//            this.Url = "http://10.11.145.9:18080/SDS/S2S";
//        }

//        /// <remarks/>
//        public event processS2SMessageCompletedEventHandler processS2SMessageCompleted;

//        /// <remarks/>
//        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
//        [return: System.Xml.Serialization.XmlElementAttribute("processS2SMessageResponse", Namespace = "http://www.ballytech.com/sds/s2s")]
//        public processS2SMessageResponse processS2SMessage([System.Xml.Serialization.XmlElementAttribute("processS2SMessage", Namespace = "http://www.ballytech.com/sds/s2s")] processS2SMessage processS2SMessage1)
//        {
//            object[] results = this.Invoke("processS2SMessage", new object[] {
//                        processS2SMessage1});
//            return ((processS2SMessageResponse)(results[0]));
//        }

//        /// <remarks/>
//        public System.IAsyncResult BeginprocessS2SMessage(processS2SMessage processS2SMessage1, System.AsyncCallback callback, object asyncState)
//        {
//            return this.BeginInvoke("processS2SMessage", new object[] {
//                        processS2SMessage1}, callback, asyncState);
//        }

//        /// <remarks/>
//        public processS2SMessageResponse EndprocessS2SMessage(System.IAsyncResult asyncResult)
//        {
//            object[] results = this.EndInvoke(asyncResult);
//            return ((processS2SMessageResponse)(results[0]));
//        }

//        /// <remarks/>
//        public void processS2SMessageAsync(processS2SMessage processS2SMessage1)
//        {
//            this.processS2SMessageAsync(processS2SMessage1, null);
//        }

//        /// <remarks/>
//        public void processS2SMessageAsync(processS2SMessage processS2SMessage1, object userState)
//        {
//            if ((this.processS2SMessageOperationCompleted == null))
//            {
//                this.processS2SMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnprocessS2SMessageOperationCompleted);
//            }
//            this.InvokeAsync("processS2SMessage", new object[] {
//                        processS2SMessage1}, this.processS2SMessageOperationCompleted, userState);
//        }

//        private void OnprocessS2SMessageOperationCompleted(object arg)
//        {
//            if ((this.processS2SMessageCompleted != null))
//            {
//                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
//                this.processS2SMessageCompleted(this, new processS2SMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
//            }
//        }

//        /// <remarks/>
//        public new void CancelAsync(object userState)
//        {
//            base.CancelAsync(userState);
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/s2s")]
//    public partial class processS2SMessage
//    {

//        private string arg0Field;

//        /// <remarks/>
//        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
//        public string arg0
//        {
//            get
//            {
//                return this.arg0Field;
//            }
//            set
//            {
//                this.arg0Field = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/s2s")]
//    public partial class processS2SMessageResponse
//    {

//        private string returnField;

//        /// <remarks/>
//        public string @return
//        {
//            get
//            {
//                return this.returnField;
//            }
//            set
//            {
//                this.returnField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
//    public delegate void processS2SMessageCompletedEventHandler(object sender, processS2SMessageCompletedEventArgs e);

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    public partial class processS2SMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
//    {

//        private object[] results;

//        internal processS2SMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
//            base(exception, cancelled, userState)
//        {
//            this.results = results;
//        }

//        /// <remarks/>
//        public processS2SMessageResponse Result
//        {
//            get
//            {
//                this.RaiseExceptionIfNecessary();
//                return ((processS2SMessageResponse)(this.results[0]));
//            }
//        }
//    }
//}
