using BMC.CoreLib.Services;
using BMC.CoreLib.WcfHelper.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.DTO;

namespace BMC.ExComms.Contracts.Interfaces
{
    [ServiceContract(Namespace = "BMC.ExComms.Contracts.Interfaces",
        Name = "IExMonServer4CommsServer",
        SessionMode = SessionMode.Allowed,
        CallbackContract = typeof(IExMonServer4CommsServerCallback))]
    public interface IExMonServer4CommsServer : IWcfCallbackService, IWcfExecutorService, IListener
    {
        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4CommsServer.Subscribe",
            IsOneWay = true)]
        void Subscribe(ExMonServer4CommsServerCallbackTypes callbackType, SubscribeRequestEntity request);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4CommsServer.Unsubscribe",
            IsOneWay = true)]
        void Unsubscribe(ExMonServer4CommsServerCallbackTypes callbackType, UnsubscribeRequestEntity request);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4CommsServer.ProcessG2HMessage",
            IsOneWay = false)]
        bool ProcessG2HMessage(MonMsg_G2H request);
    }

    public interface IExMonServer4CommsServer2 : IExMonServer4CommsServer
    {
        bool ProcessH2GMessage(MonMsg_H2G request);
    }

    public interface IExMonServer4CommsServerCallback : IServiceCallbackContractBase
    {
        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4CommsServerCallback.ProcessH2GMessage",
            IsOneWay = false)]
        bool ProcessH2GMessage(MonMsg_H2G request);
    }

    [ServiceContract(Namespace = "BMC.ExComms.Contracts.Server",
        Name = "IExMonServer4MonProcessor",
        SessionMode = SessionMode.Allowed,
        CallbackContract = typeof(IExMonServer4MonProcessorCallback))]
    public interface IExMonServer4MonProcessor : IWcfExecutorService
    {
        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4MonProcessor.Subscribe",
            IsOneWay = true)]
        void Subscribe(ExMonServer4MonProcessorCallbackTypes callbackType, SubscribeRequestEntity request);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4MonProcessor.Unsubscribe",
            IsOneWay = true)]
        void Unsubscribe(ExMonServer4MonProcessorCallbackTypes callbackType, UnsubscribeRequestEntity request);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4MonProcessor.ProcessH2GMessage",
            IsOneWay = false)]
        bool ProcessH2GMessage(MonMsg_H2G request);
    }

    public interface IExMonServer4MonProcessorCallback : IServiceCallbackContractBase
    {
        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4MonProcessorCallback.ProcessG2HMessage",
            IsOneWay = false)]
        bool ProcessG2HMessage(MonMsg_G2H request);
    }

    [ServiceContract(Namespace = "BMC.ExComms.Contracts.Interfaces",
        Name = "IExMonServer4MonClient",
        SessionMode = SessionMode.Allowed,
        CallbackContract = typeof(IExMonServer4MonClientCallback))]
    public interface IExMonServer4MonClient : IWcfExecutorService
    {
        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4MonClient.Subscribe",
            IsOneWay = true)]
        void Subscribe(ExMonServer4MonClientCallbackTypes callbackType, SubscribeRequestEntity request);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4MonClient.Unsubscribe",
            IsOneWay = true)]
        void Unsubscribe(ExMonServer4MonClientCallbackTypes callbackType, UnsubscribeRequestEntity request);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4MonClient.ProcessG2HMessage",
            IsOneWay = false)]
        bool ProcessG2HMessage(MonMsg_G2H request);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4MonClient.ProcessH2GMessage", 
            IsOneWay = false)]
        bool ProcessH2GMessage(MonMsg_H2G request);

        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4MonClient.ProcessH2GMessageSync",
            IsOneWay = false)]
        MonMsg_G2H ProcessH2GMessageSync(MonMsg_H2G request);
    }

    public interface IExMonServer4MonClientCallback : IServiceCallbackContractBase
    {
        [OperationContract(Action = "BMC.ExComms.Contracts.Interfaces.IExMonServer4MonClientCallback.ProcessG2HMessage",
            IsOneWay = false)]
        bool ProcessG2HMessage(MonMsg_G2H request);
    }
}
