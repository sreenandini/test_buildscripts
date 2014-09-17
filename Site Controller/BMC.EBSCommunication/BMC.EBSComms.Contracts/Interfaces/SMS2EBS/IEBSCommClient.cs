using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.EBSComms.Contracts.Dto.SMS2EBS;
using BMC.EBSComms.Contracts.Messages.SMS2EBS;

namespace BMC.EBSComms.Contracts.Interfaces.SMS2EBS
{
    public interface IEBSCommClient : IWcfExecutorService, IDisposable
    {
        string S2SMessagePostOperation(string request);
    }
    
    [ServiceContract(Name = "IS2S")]
    public interface IEBSCommClient_13_0 : IWcfExecutorService
    {
        [OperationContract(Action = "http://tempuri.org/IS2S/S2SMessagePostOperation",
            ReplyAction = "http://tempuri.org/IS2S/S2SMessagePostOperationResponse")]
        BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationSoapOut S2SMessagePostOperation(BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationSoapIn request);
    }

    [ServiceContract(Namespace = "http://www.ballytech.com/sds/s2s",
        Name = "IS2S",
        SessionMode = SessionMode.Allowed)]
    public interface IEBSCommClient_13_1 : IWcfExecutorService
    {
        [OperationContract(Action = "http://ballytech.com/ebs/s2s/IS2S/S2SMessagePostOperation",
            ReplyAction = "http://ballytech.com/ebs/s2s/IS2S/S2SMessagePostOperationResponse")]
        BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationResponse S2SMessagePostOperation(BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationRequest request);
    }
}
