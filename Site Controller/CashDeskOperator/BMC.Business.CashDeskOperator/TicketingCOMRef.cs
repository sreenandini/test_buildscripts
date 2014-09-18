using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using BMC.Common.Security;
using BMC.Transport;
using BMC.Transport.CashDeskOperatorEntity;

namespace BMC.Business.CashDeskOperator
{
    // NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in Web.config.
    [ServiceContract]
    public interface ITicketingService
    {
        [OperationContract]
        RTOnlineTicketDetail GetRedeemTicketAmount(RTOnlineTicketDetail TicketDetail);

        [OperationContract]
        RTOnlineTicketDetail RedeemOnlineTicket(RTOnlineTicketDetail TicketDetail);

        [OperationContract]
        ReedeemTicketDetailsCommsResponse RedeemTicketStartComms(ReedeemTicketDetailsComms TicketDetail);

        [OperationContract]
        ReedeemTicketDetailsComms RedeemTicketCompleteComms(ReedeemTicketDetailsComms TicketDetail);

        [OperationContract]
        ReedeemTicketDetailsComms RedeemTicketCancelComms(ReedeemTicketDetailsComms TicketDetail);

        [OperationContract]
        ReedeemTicketDetailsCommsResponse TestRedeem(ReedeemTicketDetailsComms TicketDetail);

        [OperationContract]
        voucherDTO[] SearchTicketForCage(String Barcode, string strClientSiteCode);
        [OperationContract]
        int CheckSDGTicket(string strBarcode);
        //[OperationContract]
        //string ValidateSite();
    }
    [DataContract]
    public class ReedeemTicketDetailsCommsResponse
    {
        [DataMember]
        public string Barcode { get; set; }
        [DataMember]
        public string DeviceId { get; set; }
        [DataMember]
        public int retAmount { get; set; }
        [DataMember]
        public int retResult { get; set; }
        [DataMember]
        public string retBarcode { get; set; }
        [DataMember]
        public int retTicketType { get; set; }
        [DataMember]
        public int iErrorCode { get; set; }
        [DataMember]
        public string HostSiteCode { get; set; }
        [DataMember]
        public string ClientSiteCode { get; set; }
        [DataMember]
        public int iVoucherid { get; set; }
        [DataMember]
        public string VoucherXMLData { get; set; }

    }

    public partial class TicketingServiceClient : System.ServiceModel.ClientBase<ITicketingService>, ITicketingService
    {
        public TicketingServiceClient()
        {
        }

        public TicketingServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public TicketingServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public TicketingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public TicketingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public RTOnlineTicketDetail GetRedeemTicketAmount(RTOnlineTicketDetail TicketDetail)
        {
            return base.Channel.GetRedeemTicketAmount(TicketDetail);
        }

        public RTOnlineTicketDetail RedeemOnlineTicket(RTOnlineTicketDetail TicketDetail)
        {
            return base.Channel.RedeemOnlineTicket(TicketDetail);
        }

        public ReedeemTicketDetailsCommsResponse RedeemTicketStartComms(ReedeemTicketDetailsComms TicketDetail)
        {
            return base.Channel.RedeemTicketStartComms(TicketDetail);
        }

        public ReedeemTicketDetailsComms RedeemTicketCompleteComms(ReedeemTicketDetailsComms TicketDetail)
        {
            return base.Channel.RedeemTicketCompleteComms(TicketDetail);
        }

        public ReedeemTicketDetailsComms RedeemTicketCancelComms(ReedeemTicketDetailsComms TicketDetail)
        {
            return base.Channel.RedeemTicketCancelComms(TicketDetail);
        }

        public TicketingServiceClient(EndpointAddress eURL, string SiteCode) :
            base(new BasicHttpBinding(), eURL)
        {
            AddCustomHeaderUserInformation(new OperationContextScope(base.InnerChannel), SiteCode);
        }

        private static void AddCustomHeaderUserInformation(OperationContextScope scope, string SiteCode)
        {
            string sEncryption = CryptographyHelper.Encrypt(SiteCode);
            MessageHeader<string> customHeaderSiteCode = new MessageHeader<string>(sEncryption);
            MessageHeader untypedHeaderSiteCode = customHeaderSiteCode.GetUntypedHeader("SiteCode", "CustomHeader");
            OperationContext.Current.OutgoingMessageHeaders.Add(untypedHeaderSiteCode);
        }


        #region ITicketingService Members


        public ReedeemTicketDetailsCommsResponse TestRedeem(ReedeemTicketDetailsComms TicketDetail)
        {
            return base.Channel.TestRedeem(TicketDetail);
        }

        public voucherDTO[] SearchTicketForCage(string Barcode, string strClientSiteCode)
        {
            return base.Channel.SearchTicketForCage(Barcode, strClientSiteCode);
        }

        public int CheckSDGTicket(string strBarcode)
        {
            return base.Channel.CheckSDGTicket(strBarcode);
        }

        #endregion
    }


    //// Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
