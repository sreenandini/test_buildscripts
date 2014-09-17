using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_B2B_EFT_Transactions")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_EFT_Transactions 
        : MonTgt_B2B
    {
        protected readonly Lazy<ExCommsPlayerFlags> _playerFlags2 = new Lazy<ExCommsPlayerFlags>(() => { return new ExCommsPlayerFlags(); });

        public MonTgt_B2B_EFT_Transactions() { }

        [DataMember]
        public string CardNumber { get; set; }

        public ExCommsPlayerFlags PlayerFlagsEx
        {
            get { return _playerFlags2.Value; }
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_B2B_EFT_PlayerAndMessageInfo")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_EFT_PlayerAndMessageInfo 
        : MonTgt_B2B_EFT_Transactions, IMonTgt_B2B
    {
        public MonTgt_B2B_EFT_PlayerAndMessageInfo() { }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public ExCommsPlayerFlags PlayerFlags
        {
            get { return _playerFlags2.Value; }
        }

        [DataMember]
        public byte DisplayMessageLength { get; set; }

        [DataMember]
        public string DisplayMessage { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_B2B_EFT_AmountDetails")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_EFT_AmountDetails
        : MonTgt_B2B_EFT_Transactions, IMonTgt_B2B
    {
        public MonTgt_B2B_EFT_AmountDetails() { }

        [DataMember]
        public double NonCashableAmount { get; set; }

        [DataMember]
        public double CashableAmount { get; set; }

    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_B2B_EFT_AmountDetails_CardNo")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_EFT_AmountDetails_CardNo
        : MonTgt_B2B_EFT_AmountDetails, IMonTgt_B2B
    {
        public MonTgt_B2B_EFT_AmountDetails_CardNo() { }

        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_B2B_EFT_AmountDetails_Error")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_EFT_AmountDetails_Error
        : MonTgt_B2B_EFT_Transactions, IMonTgt_B2B
    {
        public MonTgt_B2B_EFT_AmountDetails_Error() { }

        [DataMember]
        public double NonCashableAmount { get; set; }

        [DataMember]
        public double CashableAmount { get; set; }

        [DataMember]
        public byte GMUErrorCode { get; set; }

        [DataMember]
        public byte CMPErrorCode { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_B2B_EFT_AmountDetails_CardNo_Error")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_EFT_AmountDetails_CardNo_Error
        : MonTgt_B2B_EFT_AmountDetails_Error, IMonTgt_B2B
    {
        public MonTgt_B2B_EFT_AmountDetails_CardNo_Error() { }

        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_PlayerAndPinDetails")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_PlayerAndPinDetails
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_PlayerAndPinDetails() { }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public string Pin { get; set; }
    }
}
