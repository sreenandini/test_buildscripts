using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace BMC.CoreLib.Services
{
    [ServiceContract(CallbackContract = typeof(IAppNotifyServiceCallback))]
    public interface IAppNotifyService
    {
        [OperationContract(Action = "http://ballytech.com/BMC/IAppNotifyService/Subscribe", IsOneWay = true)]
        void Subscribe();

        [OperationContract(Action = "http://ballytech.com/BMC/IAppNotifyService/Unsubscribe", IsOneWay = true)]
        void Unsubscribe();
    }

    public interface IAppNotifyServiceCallback
    {
        [OperationContract(Action = "http://ballytech.com/BMC/IAppNotifyService/NotifyData", IsOneWay = true)]
        void NotifyData(AppNotifyData data);
    }

    [DataContract(Namespace = "http://ballytech.com/BMC/DTO/NotifyData", Name = "AppNotifyData")]
    public class AppNotifyData : IExtensibleDataObject
    {
        [DataMember(IsRequired = false)]
        public string Message { get; set; }

        [DataMember(IsRequired = false)]
        public string MessageType { get; set; }

        [DataMember(IsRequired = false)]
        public string UniqueKey { get; set; }

        [DataMember(IsRequired = false)]
        public List<string> Extra { get; set; }

        [DataMember(IsRequired = false)]
        public object UnsafeObject { get; set; }

        #region IExtensibleDataObject Members

        public ExtensionDataObject ExtensionData { get; set; }

        #endregion
    }
}
