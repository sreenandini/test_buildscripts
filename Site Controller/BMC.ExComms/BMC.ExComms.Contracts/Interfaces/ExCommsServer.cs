using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using BMC.CoreLib.Services;
using BMC.CoreLib.WcfHelper.Contracts;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.Interfaces
{
    [ServiceContract(Namespace = "BMC.ExComms.Contracts.Interfaces",
         Name = "ExCommsServer",
         SessionMode = SessionMode.Allowed,
         CallbackContract = typeof(IExCommsServerCallback))]
    public interface IExCommsServer : IWcfExecutorService, IListener
    {
        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExCommsServer.Subscribe",
            IsOneWay = true)]
        void Subscribe(ExCommsServerCallbackTypes callbackType, SubscribeRequestEntity request);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExCommsServer.Unsubscribe",
            IsOneWay = true)]
        void Unsubscribe(ExCommsServerCallbackTypes callbackType, UnsubscribeRequestEntity request);
    }

    public interface IExCommsServerCallback : IServiceCallbackContractBase
    {
        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExCommsServerCallback.RawMessage",
            IsOneWay = true)]
        void RawMessage(ExCommsRawMessageEntity entity);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExCommsServerCallback.ExecutionStepSubscribed",
            IsOneWay = true)]
        void ExecutionStepSubscribed(List<ExCommsExecutionStepEntity> entity);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExCommsServerCallback.ExecutionStepChanged",
            IsOneWay = true)]
        void ExecutionStepChanged(ExCommsExecutionStepEntity entity);
    }
}
