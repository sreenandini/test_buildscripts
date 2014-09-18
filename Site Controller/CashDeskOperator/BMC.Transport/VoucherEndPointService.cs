namespace BMC.Transport
{
    //[assembly: System.Runtime.Serialization.ContractNamespaceAttribute("http://www.ballytech.com/sds/voucher", ClrNamespace = "www.ballytech.com.sds.voucher")]

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    public interface VoucherEndPoint
    {
        // CODEGEN: Generating message contract since the operation cancelRedeemVoucher is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        cancelRedeemVoucherResponse1 cancelRedeemVoucher(cancelRedeemVoucherRequest request);

        // CODEGEN: Generating message contract since the operation createBulkVoucher is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        createBulkVoucherResponse createBulkVoucher(createBulkVoucherRequest request);

        // CODEGEN: Generating message contract since the operation createVoucher is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        createVoucherResponse1 createVoucher(createVoucherRequest request);

        // CODEGEN: Generating message contract since the operation getTransactionReasonsForCashDesk is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        getTransactionReasonsForCashDeskResponse getTransactionReasonsForCashDesk(getTransactionReasonsForCashDeskRequest request);

        // CODEGEN: Generating message contract since the operation getVoucherParameters is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        getVoucherParametersResponse1 getVoucherParameters(getVoucherParametersRequest request);

        // CODEGEN: Generating message contract since the operation getVoucherTransactionHistory is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        getVoucherTransactionHistoryResponse getVoucherTransactionHistory(getVoucherTransactionHistoryRequest request);

        // CODEGEN: Generating message contract since the operation getVouchers is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        getVouchersResponse getVouchers(getVouchersRequest request);

        // CODEGEN: Generating message contract since the operation inquireVoucher is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        inquireVoucherResponse1 inquireVoucher(inquireVoucherRequest request);

        // CODEGEN: Generating message contract since the operation overrideRedeemVoucher is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        overrideRedeemVoucherResponse1 overrideRedeemVoucher(overrideRedeemVoucherRequest request);

        // CODEGEN: Generating message contract since the operation redeemRequestVoucher is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        redeemRequestVoucherResponse1 redeemRequestVoucher(redeemRequestVoucherRequest request);

        // CODEGEN: Generating message contract since the operation redeemVoucher is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        redeemVoucherResponse1 redeemVoucher(redeemVoucherRequest request);

        // CODEGEN: Generating message contract since the operation redeemVoucherWithoutStatusCheck is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        redeemVoucherWithoutStatusCheckResponse1 redeemVoucherWithoutStatusCheck(redeemVoucherWithoutStatusCheckRequest request);

        // CODEGEN: Generating message contract since the operation searchVouchers is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        searchVouchersResponse searchVouchers(searchVouchersRequest request);

        // CODEGEN: Generating message contract since the operation voidBulkVoucher is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        voidBulkVoucherResponse voidBulkVoucher(voidBulkVoucherRequest request);

        // CODEGEN: Generating message contract since the operation voidVoucher is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "")]
        [System.ServiceModel.FaultContractAttribute(typeof(www.ballytech.com.sds.voucher.SWSException), Action = "", Name = "SWSException")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(baseDTO))]
        voidVoucherResponse1 voidVoucher(voidVoucherRequest request);
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class cancelRedeemVoucher
    {
        private voucherDTO arg0Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class voucherDTO
    {
        private bool allowOverrideField;

        private bool allowRedeemField;

        private long amountField;

        private amountTypeEnum amountTypeField;

        //private bool amountTypeFieldSpecified;

        private string amountTypeValueField;

        private string barcodeField;

        private System.DateTime effectiveDateField;

        //private bool effectiveDateFieldSpecified;

        private string employeeIdField;

        private int errorCodeIdField;

        private System.DateTime expireDateField;

        //private bool expireDateFieldSpecified;

        private int expiryDaysField;

        private int optLockVersionField;

        private string overrideReasonField;

        private string overrideUserIdField;

        private string overrideUserPwdField;

        private string playerCardReqdField;

        private string playerIdField;

        private long sessionIdField;

      //  private bool sessionIdFieldSpecified;

        private long siteIdField;

        private long ticketIdField;

        private ticketTypeEnum ticketTypeField;

        //private bool ticketTypeFieldSpecified;

        private string ticketTypeValueField;

        private string tktStatusField;

        private short tktStatusIdField;

        private string transAssetNumberField;

        private System.DateTime transDateTimeField;

        //private bool transDateTimeFieldSpecified;

        private int userRoleTypeIdField;

        //private bool userRoleTypeIdFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public bool allowOverride
        {
            get
            {
                return this.allowOverrideField;
            }
            set
            {
                this.allowOverrideField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public bool allowRedeem
        {
            get
            {
                return this.allowRedeemField;
            }
            set
            {
                this.allowRedeemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
        public long amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
        public amountTypeEnum amountType
        {
            get
            {
                return this.amountTypeField;
            }
            set
            {
                this.amountTypeField = value;
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool amountTypeSpecified
        //{
        //    get
        //    {
        //        return this.amountTypeFieldSpecified;
        //    }
        //    set
        //    {
        //        this.amountTypeFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 4)]
        public string amountTypeValue
        {
            get
            {
                return this.amountTypeValueField;
            }
            set
            {
                this.amountTypeValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 5)]
        public string barcode
        {
            get
            {
                return this.barcodeField;
            }
            set
            {
                this.barcodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 6)]
        public System.DateTime effectiveDate
        {
            get
            {
                return this.effectiveDateField;
            }
            set
            {
                this.effectiveDateField = value;
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool effectiveDateSpecified
        //{
        //    get
        //    {
        //        return this.effectiveDateFieldSpecified;
        //    }
        //    set
        //    {
        //        this.effectiveDateFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 7)]
        public string employeeId
        {
            get
            {
                return this.employeeIdField;
            }
            set
            {
                this.employeeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 8)]
        public int errorCodeId
        {
            get
            {
                return this.errorCodeIdField;
            }
            set
            {
                this.errorCodeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 9)]
        public System.DateTime expireDate
        {
            get
            {
                return this.expireDateField;
            }
            set
            {
                this.expireDateField = value;
            }
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool expireDateSpecified
        //{
        //    get
        //    {
        //        return this.expireDateFieldSpecified;
        //    }
        //    set
        //    {
        //        this.expireDateFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 10)]
        public int expiryDays
        {
            get
            {
                return this.expiryDaysField;
            }
            set
            {
                this.expiryDaysField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 11)]
        public int optLockVersion
        {
            get
            {
                return this.optLockVersionField;
            }
            set
            {
                this.optLockVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 12)]
        public string overrideReason
        {
            get
            {
                return this.overrideReasonField;
            }
            set
            {
                this.overrideReasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 13)]
        public string overrideUserId
        {
            get
            {
                return this.overrideUserIdField;
            }
            set
            {
                this.overrideUserIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 14)]
        public string overrideUserPwd
        {
            get
            {
                return this.overrideUserPwdField;
            }
            set
            {
                this.overrideUserPwdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 15)]
        public string playerCardReqd
        {
            get
            {
                return this.playerCardReqdField;
            }
            set
            {
                this.playerCardReqdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 16)]
        public string playerId
        {
            get
            {
                return this.playerIdField;
            }
            set
            {
                this.playerIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 17)]
        public long sessionId
        {
            get
            {
                return this.sessionIdField;
            }
            set
            {
                this.sessionIdField = value;
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool sessionIdSpecified
        //{
        //    get
        //    {
        //        return this.sessionIdFieldSpecified;
        //    }
        //    set
        //    {
        //        this.sessionIdFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 18)]
        public long siteId
        {
            get
            {
                return this.siteIdField;
            }
            set
            {
                this.siteIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 19)]
        public long ticketId
        {
            get
            {
                return this.ticketIdField;
            }
            set
            {
                this.ticketIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 20)]
        public ticketTypeEnum ticketType
        {
            get
            {
                return this.ticketTypeField;
            }
            set
            {
                this.ticketTypeField = value;
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool ticketTypeSpecified
        //{
        //    get
        //    {
        //        return this.ticketTypeFieldSpecified;
        //    }
        //    set
        //    {
        //        this.ticketTypeFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 21)]
        public string ticketTypeValue
        {
            get
            {
                return this.ticketTypeValueField;
            }
            set
            {
                this.ticketTypeValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 22)]
        public string tktStatus
        {
            get
            {
                return this.tktStatusField;
            }
            set
            {
                this.tktStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 23)]
        public short tktStatusId
        {
            get
            {
                return this.tktStatusIdField;
            }
            set
            {
                this.tktStatusIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 24)]
        public string transAssetNumber
        {
            get
            {
                return this.transAssetNumberField;
            }
            set
            {
                this.transAssetNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 25)]
        public System.DateTime transDateTime
        {
            get
            {
                return this.transDateTimeField;
            }
            set
            {
                this.transDateTimeField = value;
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool transDateTimeSpecified
        //{
        //    get
        //    {
        //        return this.transDateTimeFieldSpecified;
        //    }
        //    set
        //    {
        //        this.transDateTimeFieldSpecified = value;
        //    }
        //}

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 26)]
        public int userRoleTypeId
        {
            get
            {
                return this.userRoleTypeIdField;
            }
            set
            {
                this.userRoleTypeIdField = value;
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public bool userRoleTypeIdSpecified
        //{
        //    get
        //    {
        //        return this.userRoleTypeIdFieldSpecified;
        //    }
        //    set
        //    {
        //        this.userRoleTypeIdFieldSpecified = value;
        //    }
        //}

        public voucherDTO Clone()
        {
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    BinaryFormatter bfm = new BinaryFormatter();
            //    bfm.Serialize(ms, this);
            //    ms.Seek(0, SeekOrigin.Begin);
            //    return bfm.Deserialize(ms) as voucherDTO;
            //}
            voucherDTO oVoucherDTO = new voucherDTO();
            oVoucherDTO.allowOverride = this.allowOverride;
            oVoucherDTO.allowRedeem = this.allowRedeem;
            oVoucherDTO.amount = this.amount;
            oVoucherDTO.amountType = this.amountType;
            oVoucherDTO.amountTypeValue = this.amountTypeValue;
            oVoucherDTO.barcode = this.barcode;
            oVoucherDTO.effectiveDate = this.effectiveDate;
            oVoucherDTO.employeeId = this.employeeId;
            oVoucherDTO.errorCodeId = this.errorCodeId;
            oVoucherDTO.expireDate = this.expireDate;
            oVoucherDTO.expiryDays = this.expiryDays;
            oVoucherDTO.optLockVersion = this.optLockVersion;
            oVoucherDTO.overrideReason = this.overrideReason;
            oVoucherDTO.overrideUserId = this.overrideUserId;
            oVoucherDTO.overrideUserPwd = this.overrideUserPwd;
            oVoucherDTO.playerCardReqd = this.playerCardReqd;
            oVoucherDTO.playerId = this.playerId;
            oVoucherDTO.sessionId = this.sessionId;
            oVoucherDTO.siteId = this.siteId;
            oVoucherDTO.ticketId = this.ticketId;
            oVoucherDTO.ticketType = this.ticketType;
            oVoucherDTO.ticketTypeValue = this.ticketTypeValue;
            oVoucherDTO.tktStatus = this.tktStatus;
            oVoucherDTO.tktStatusId = this.tktStatusId;
            oVoucherDTO.transAssetNumber = this.transAssetNumber;
            oVoucherDTO.transDateTime = this.transDateTime;
            oVoucherDTO.userRoleTypeId = this.userRoleTypeId;
            return oVoucherDTO;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public enum amountTypeEnum
    {
        /// <remarks/>
        UNKNOWN,

        /// <remarks/>
        CASHABLE,

        /// <remarks/>
        NONCASHABLE,

        /// <remarks/>
        NON_CASHABLE_PROMO,

        /// <remarks/>
        CASHABLE_PROMO,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public enum ticketTypeEnum
    {
        /// <remarks/>
        CICKASAW,

        /// <remarks/>
        CASHABLE_PROMO,

        /// <remarks/>
        NON_CASHABLE_PROMO,

        /// <remarks/>
        CASHABLE_PROMO_REQ_PLAYER_CARD,

        /// <remarks/>
        NON_CASHABLE_PROMO_REQ_PLAYER_CARD,

        /// <remarks/>
        SLOT_GENERATED,

        /// <remarks/>
        SLOT_GENERATED_NEW_JERSEY,

        /// <remarks/>
        NON_SLOT_GENERATED,

        /// <remarks/>
        DUMMY_TICKET_TYPE,

        /// <remarks/>
        S2S_GEN_TICKET_TYPE,

        /// <remarks/>
        ENHANCED_VALIDATION_TICKET_TYPE,

        /// <remarks/>
        STANDARD_VALIDATION_TICKET_TYPE,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class voidVoucherResponse
    {
        private voucherDTO returnField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO @return
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class voidVoucher
    {
        private voucherDTO arg0Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class searchVouchers
    {
        private string arg0Field;

        private int arg1Field;

        private long arg2Field;

        private int arg3Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public int arg1
        {
            get
            {
                return this.arg1Field;
            }
            set
            {
                this.arg1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
        public long arg2
        {
            get
            {
                return this.arg2Field;
            }
            set
            {
                this.arg2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
        public int arg3
        {
            get
            {
                return this.arg3Field;
            }
            set
            {
                this.arg3Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class redeemVoucherWithoutStatusCheckResponse
    {
        private voucherDTO returnField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO @return
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class redeemVoucherWithoutStatusCheck
    {
        private voucherDTO arg0Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class redeemVoucherResponse
    {
        private voucherDTO returnField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO @return
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class redeemVoucher
    {
        private voucherDTO arg0Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class redeemRequestVoucherResponse
    {
        private voucherDTO returnField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO @return
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class redeemRequestVoucher
    {
        private voucherDTO arg0Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class overrideRedeemVoucherResponse
    {
        private voucherDTO returnField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO @return
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class overrideRedeemVoucher
    {
        private voucherDTO arg0Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class inquireVoucherResponse
    {
        private voucherDTO returnField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO @return
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class inquireVoucher
    {
        private voucherDTO arg0Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class getVouchers
    {
        private ticketTypeEnum arg0Field;

        private bool arg0FieldSpecified;

        private string arg1Field;

        private int arg2Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public ticketTypeEnum arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool arg0Specified
        {
            get
            {
                return this.arg0FieldSpecified;
            }
            set
            {
                this.arg0FieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public string arg1
        {
            get
            {
                return this.arg1Field;
            }
            set
            {
                this.arg1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
        public int arg2
        {
            get
            {
                return this.arg2Field;
            }
            set
            {
                this.arg2Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class transDTO
    {
        private long adevIdField;

        private bool adevIdFieldSpecified;

        private string assetAreaField;

        private string assetDenomField;

        private long assetIdField;

        private bool assetIdFieldSpecified;

        private string assetStandNbrField;

        private short assetTypeField;

        private bool assetTypeFieldSpecified;

        private System.DateTime createdTsField;

        private bool createdTsFieldSpecified;

        private string createdUserField;

        private string deletedTsField;

        private string employeeIdField;

        private string employeeIdOverrideField;

        private int errorCodeIdField;

        private bool errorCodeIdFieldSpecified;

        private long msgIdField;

        private bool msgIdFieldSpecified;

        private int optimisticLockVersionField;

        private int resnIdField;

        private bool resnIdFieldSpecified;

        private long sessionIdField;

        private bool sessionIdFieldSpecified;

        private int siteIdField;

        private bool siteIdFieldSpecified;

        private string slotDenomField;

        private long spsMesgIdField;

        private bool spsMesgIdFieldSpecified;

        private int statIdField;

        private bool statIdFieldSpecified;

        private string transBarcodeField;

        private System.DateTime transDatetimeField;

        private bool transDatetimeFieldSpecified;

        private long transIdField;

        private bool transIdFieldSpecified;

        private string transLocationNbrField;

        private string transPlayerIdField;

        private long transSeqNbrField;

        private bool transSeqNbrFieldSpecified;

        private int transTypeIdField;

        private bool transTypeIdFieldSpecified;

        private string updatedTsField;

        private string updatedUserField;

        private int userRoleTypeIdField;

        private bool userRoleTypeIdFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public long adevId
        {
            get
            {
                return this.adevIdField;
            }
            set
            {
                this.adevIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool adevIdSpecified
        {
            get
            {
                return this.adevIdFieldSpecified;
            }
            set
            {
                this.adevIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public string assetArea
        {
            get
            {
                return this.assetAreaField;
            }
            set
            {
                this.assetAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
        public string assetDenom
        {
            get
            {
                return this.assetDenomField;
            }
            set
            {
                this.assetDenomField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
        public long assetId
        {
            get
            {
                return this.assetIdField;
            }
            set
            {
                this.assetIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool assetIdSpecified
        {
            get
            {
                return this.assetIdFieldSpecified;
            }
            set
            {
                this.assetIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 4)]
        public string assetStandNbr
        {
            get
            {
                return this.assetStandNbrField;
            }
            set
            {
                this.assetStandNbrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 5)]
        public short assetType
        {
            get
            {
                return this.assetTypeField;
            }
            set
            {
                this.assetTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool assetTypeSpecified
        {
            get
            {
                return this.assetTypeFieldSpecified;
            }
            set
            {
                this.assetTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 6)]
        public System.DateTime createdTs
        {
            get
            {
                return this.createdTsField;
            }
            set
            {
                this.createdTsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool createdTsSpecified
        {
            get
            {
                return this.createdTsFieldSpecified;
            }
            set
            {
                this.createdTsFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 7)]
        public string createdUser
        {
            get
            {
                return this.createdUserField;
            }
            set
            {
                this.createdUserField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 8)]
        public string deletedTs
        {
            get
            {
                return this.deletedTsField;
            }
            set
            {
                this.deletedTsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 9)]
        public string employeeId
        {
            get
            {
                return this.employeeIdField;
            }
            set
            {
                this.employeeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 10)]
        public string employeeIdOverride
        {
            get
            {
                return this.employeeIdOverrideField;
            }
            set
            {
                this.employeeIdOverrideField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 11)]
        public int errorCodeId
        {
            get
            {
                return this.errorCodeIdField;
            }
            set
            {
                this.errorCodeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool errorCodeIdSpecified
        {
            get
            {
                return this.errorCodeIdFieldSpecified;
            }
            set
            {
                this.errorCodeIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 12)]
        public long msgId
        {
            get
            {
                return this.msgIdField;
            }
            set
            {
                this.msgIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool msgIdSpecified
        {
            get
            {
                return this.msgIdFieldSpecified;
            }
            set
            {
                this.msgIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 13)]
        public int optimisticLockVersion
        {
            get
            {
                return this.optimisticLockVersionField;
            }
            set
            {
                this.optimisticLockVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 14)]
        public int resnId
        {
            get
            {
                return this.resnIdField;
            }
            set
            {
                this.resnIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool resnIdSpecified
        {
            get
            {
                return this.resnIdFieldSpecified;
            }
            set
            {
                this.resnIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 15)]
        public long sessionId
        {
            get
            {
                return this.sessionIdField;
            }
            set
            {
                this.sessionIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sessionIdSpecified
        {
            get
            {
                return this.sessionIdFieldSpecified;
            }
            set
            {
                this.sessionIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 16)]
        public int siteId
        {
            get
            {
                return this.siteIdField;
            }
            set
            {
                this.siteIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool siteIdSpecified
        {
            get
            {
                return this.siteIdFieldSpecified;
            }
            set
            {
                this.siteIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 17)]
        public string slotDenom
        {
            get
            {
                return this.slotDenomField;
            }
            set
            {
                this.slotDenomField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 18)]
        public long spsMesgId
        {
            get
            {
                return this.spsMesgIdField;
            }
            set
            {
                this.spsMesgIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool spsMesgIdSpecified
        {
            get
            {
                return this.spsMesgIdFieldSpecified;
            }
            set
            {
                this.spsMesgIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 19)]
        public int statId
        {
            get
            {
                return this.statIdField;
            }
            set
            {
                this.statIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool statIdSpecified
        {
            get
            {
                return this.statIdFieldSpecified;
            }
            set
            {
                this.statIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 20)]
        public string transBarcode
        {
            get
            {
                return this.transBarcodeField;
            }
            set
            {
                this.transBarcodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 21)]
        public System.DateTime transDatetime
        {
            get
            {
                return this.transDatetimeField;
            }
            set
            {
                this.transDatetimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool transDatetimeSpecified
        {
            get
            {
                return this.transDatetimeFieldSpecified;
            }
            set
            {
                this.transDatetimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 22)]
        public long transId
        {
            get
            {
                return this.transIdField;
            }
            set
            {
                this.transIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool transIdSpecified
        {
            get
            {
                return this.transIdFieldSpecified;
            }
            set
            {
                this.transIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 23)]
        public string transLocationNbr
        {
            get
            {
                return this.transLocationNbrField;
            }
            set
            {
                this.transLocationNbrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 24)]
        public string transPlayerId
        {
            get
            {
                return this.transPlayerIdField;
            }
            set
            {
                this.transPlayerIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 25)]
        public long transSeqNbr
        {
            get
            {
                return this.transSeqNbrField;
            }
            set
            {
                this.transSeqNbrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool transSeqNbrSpecified
        {
            get
            {
                return this.transSeqNbrFieldSpecified;
            }
            set
            {
                this.transSeqNbrFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 26)]
        public int transTypeId
        {
            get
            {
                return this.transTypeIdField;
            }
            set
            {
                this.transTypeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool transTypeIdSpecified
        {
            get
            {
                return this.transTypeIdFieldSpecified;
            }
            set
            {
                this.transTypeIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 27)]
        public string updatedTs
        {
            get
            {
                return this.updatedTsField;
            }
            set
            {
                this.updatedTsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 28)]
        public string updatedUser
        {
            get
            {
                return this.updatedUserField;
            }
            set
            {
                this.updatedUserField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 29)]
        public int userRoleTypeId
        {
            get
            {
                return this.userRoleTypeIdField;
            }
            set
            {
                this.userRoleTypeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool userRoleTypeIdSpecified
        {
            get
            {
                return this.userRoleTypeIdFieldSpecified;
            }
            set
            {
                this.userRoleTypeIdFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class getVoucherTransactionHistory
    {
        private string arg0Field;

        private int arg1Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public int arg1
        {
            get
            {
                return this.arg1Field;
            }
            set
            {
                this.arg1Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class voucherParameterDTO
    {
        private bool allowCashierLocOnTktsField;

        private bool allowPrintTktOverrideField;

        private bool allowVoidForTktPrintExpField;

        private int errorCodeIdField;

        private bool errorPresentField;

        private long maxDailyCashierGenTktAmtField;

        private int maxNoOfTktPrintLimitField;

        private double maxTktPrintAmtForEmpField;

        private long maxTktRedemptionAmtField;

        private double maxTktRedemptionAmtForEmpField;

        private double minTktPrintAmtForEmpField;

        private bool tktPrinterEnabledField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public bool allowCashierLocOnTkts
        {
            get
            {
                return this.allowCashierLocOnTktsField;
            }
            set
            {
                this.allowCashierLocOnTktsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public bool allowPrintTktOverride
        {
            get
            {
                return this.allowPrintTktOverrideField;
            }
            set
            {
                this.allowPrintTktOverrideField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
        public bool allowVoidForTktPrintExp
        {
            get
            {
                return this.allowVoidForTktPrintExpField;
            }
            set
            {
                this.allowVoidForTktPrintExpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
        public int errorCodeId
        {
            get
            {
                return this.errorCodeIdField;
            }
            set
            {
                this.errorCodeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 4)]
        public bool errorPresent
        {
            get
            {
                return this.errorPresentField;
            }
            set
            {
                this.errorPresentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 5)]
        public long maxDailyCashierGenTktAmt
        {
            get
            {
                return this.maxDailyCashierGenTktAmtField;
            }
            set
            {
                this.maxDailyCashierGenTktAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 6)]
        public int maxNoOfTktPrintLimit
        {
            get
            {
                return this.maxNoOfTktPrintLimitField;
            }
            set
            {
                this.maxNoOfTktPrintLimitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 7)]
        public double maxTktPrintAmtForEmp
        {
            get
            {
                return this.maxTktPrintAmtForEmpField;
            }
            set
            {
                this.maxTktPrintAmtForEmpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 8)]
        public long maxTktRedemptionAmt
        {
            get
            {
                return this.maxTktRedemptionAmtField;
            }
            set
            {
                this.maxTktRedemptionAmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 9)]
        public double maxTktRedemptionAmtForEmp
        {
            get
            {
                return this.maxTktRedemptionAmtForEmpField;
            }
            set
            {
                this.maxTktRedemptionAmtForEmpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 10)]
        public double minTktPrintAmtForEmp
        {
            get
            {
                return this.minTktPrintAmtForEmpField;
            }
            set
            {
                this.minTktPrintAmtForEmpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 11)]
        public bool tktPrinterEnabled
        {
            get
            {
                return this.tktPrinterEnabledField;
            }
            set
            {
                this.tktPrinterEnabledField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class getVoucherParametersResponse
    {
        private voucherParameterDTO returnField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherParameterDTO @return
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class getVoucherParameters
    {
        private long arg0Field;

        private string arg1Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public long arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public string arg1
        {
            get
            {
                return this.arg1Field;
            }
            set
            {
                this.arg1Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class userInfoDTO
    {
        private string firstNameField;

        private functionInfoDTO[] functionFormsField;

        private string ipAddressField;

        private string lastNameField;

        private string middleNameField;

        private parameterInfoDTO[] parameterFormsField;

        private string passwordField;

        private int pausedField;

        private bool pausedFieldSpecified;

        private string roleDescriptionField;

        private long roleIdField;

        private string roleNameField;

        private long siteIdField;

        private System.DateTime slipDropTimeField;

        private bool slipDropTimeFieldSpecified;

        private long userIdField;

        private string userNameField;

        private int userRoleTypeIdField;

        private bool userRoleTypeIdFieldSpecified;

        private string workStationField;

        private string workStationKeyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string firstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("functionForms", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public functionInfoDTO[] functionForms
        {
            get
            {
                return this.functionFormsField;
            }
            set
            {
                this.functionFormsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
        public string ipAddress
        {
            get
            {
                return this.ipAddressField;
            }
            set
            {
                this.ipAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
        public string lastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 4)]
        public string middleName
        {
            get
            {
                return this.middleNameField;
            }
            set
            {
                this.middleNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("parameterForms", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 5)]
        public parameterInfoDTO[] parameterForms
        {
            get
            {
                return this.parameterFormsField;
            }
            set
            {
                this.parameterFormsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 6)]
        public string password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 7)]
        public int paused
        {
            get
            {
                return this.pausedField;
            }
            set
            {
                this.pausedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool pausedSpecified
        {
            get
            {
                return this.pausedFieldSpecified;
            }
            set
            {
                this.pausedFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 8)]
        public string roleDescription
        {
            get
            {
                return this.roleDescriptionField;
            }
            set
            {
                this.roleDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 9)]
        public long roleId
        {
            get
            {
                return this.roleIdField;
            }
            set
            {
                this.roleIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 10)]
        public string roleName
        {
            get
            {
                return this.roleNameField;
            }
            set
            {
                this.roleNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 11)]
        public long siteId
        {
            get
            {
                return this.siteIdField;
            }
            set
            {
                this.siteIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 12)]
        public System.DateTime slipDropTime
        {
            get
            {
                return this.slipDropTimeField;
            }
            set
            {
                this.slipDropTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool slipDropTimeSpecified
        {
            get
            {
                return this.slipDropTimeFieldSpecified;
            }
            set
            {
                this.slipDropTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 13)]
        public long userId
        {
            get
            {
                return this.userIdField;
            }
            set
            {
                this.userIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 14)]
        public string userName
        {
            get
            {
                return this.userNameField;
            }
            set
            {
                this.userNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 15)]
        public int userRoleTypeId
        {
            get
            {
                return this.userRoleTypeIdField;
            }
            set
            {
                this.userRoleTypeIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool userRoleTypeIdSpecified
        {
            get
            {
                return this.userRoleTypeIdFieldSpecified;
            }
            set
            {
                this.userRoleTypeIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 16)]
        public string workStation
        {
            get
            {
                return this.workStationField;
            }
            set
            {
                this.workStationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 17)]
        public string workStationKey
        {
            get
            {
                return this.workStationKeyField;
            }
            set
            {
                this.workStationKeyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class functionInfoDTO : baseDTO
    {
        private string functionDescriptionField;

        private int functionIdField;

        private string functionNameField;

        private int moduleIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string functionDescription
        {
            get
            {
                return this.functionDescriptionField;
            }
            set
            {
                this.functionDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public int functionId
        {
            get
            {
                return this.functionIdField;
            }
            set
            {
                this.functionIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
        public string functionName
        {
            get
            {
                return this.functionNameField;
            }
            set
            {
                this.functionNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
        public int moduleId
        {
            get
            {
                return this.moduleIdField;
            }
            set
            {
                this.moduleIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(transactionReasonInfoDTO))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(parameterInfoDTO))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(functionInfoDTO))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class baseDTO
    {
        private bool errorPresentField;

        private message[] messagesField;

        private int optimisticLockVersionField;

        private userInfoDTO userFormDTOField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public bool errorPresent
        {
            get
            {
                return this.errorPresentField;
            }
            set
            {
                this.errorPresentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("messages", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public message[] messages
        {
            get
            {
                return this.messagesField;
            }
            set
            {
                this.messagesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 2)]
        public int optimisticLockVersion
        {
            get
            {
                return this.optimisticLockVersionField;
            }
            set
            {
                this.optimisticLockVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 3)]
        public userInfoDTO userFormDTO
        {
            get
            {
                return this.userFormDTOField;
            }
            set
            {
                this.userFormDTOField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class message
    {
        private string messageKeyField;

        private string[] msgParametersField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string messageKey
        {
            get
            {
                return this.messageKeyField;
            }
            set
            {
                this.messageKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("msgParameters", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public string[] msgParameters
        {
            get
            {
                return this.msgParametersField;
            }
            set
            {
                this.msgParametersField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class transactionReasonInfoDTO : baseDTO
    {
        private string descriptionField;

        private long idField;

        private bool idFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public long id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool idSpecified
        {
            get
            {
                return this.idFieldSpecified;
            }
            set
            {
                this.idFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class parameterInfoDTO : baseDTO
    {
        private string parameterNameField;

        private string parameterValueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string parameterName
        {
            get
            {
                return this.parameterNameField;
            }
            set
            {
                this.parameterNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public string parameterValue
        {
            get
            {
                return this.parameterValueField;
            }
            set
            {
                this.parameterValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class getTransactionReasonsForCashDesk
    {
        private int arg0Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public int arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class createVoucherResponse
    {
        private voucherDTO returnField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO @return
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class createVoucher
    {
        private voucherDTO arg0Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class createBulkVoucher
    {
        private voucherDTO arg0Field;

        private int arg1Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO arg0
        {
            get
            {
                return this.arg0Field;
            }
            set
            {
                this.arg0Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        public int arg1
        {
            get
            {
                return this.arg1Field;
            }
            set
            {
                this.arg1Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.2152")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ballytech.com/sds/voucher")]
    public partial class cancelRedeemVoucherResponse
    {
        private voucherDTO returnField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public voucherDTO @return
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

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class cancelRedeemVoucherRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public cancelRedeemVoucher cancelRedeemVoucher;

        public cancelRedeemVoucherRequest()
        {
        }

        public cancelRedeemVoucherRequest(cancelRedeemVoucher cancelRedeemVoucher)
        {
            this.cancelRedeemVoucher = cancelRedeemVoucher;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class cancelRedeemVoucherResponse1
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public cancelRedeemVoucherResponse cancelRedeemVoucherResponse;

        public cancelRedeemVoucherResponse1()
        {
        }

        public cancelRedeemVoucherResponse1(cancelRedeemVoucherResponse cancelRedeemVoucherResponse)
        {
            this.cancelRedeemVoucherResponse = cancelRedeemVoucherResponse;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class createBulkVoucherRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public createBulkVoucher createBulkVoucher;

        public createBulkVoucherRequest()
        {
        }

        public createBulkVoucherRequest(createBulkVoucher createBulkVoucher)
        {
            this.createBulkVoucher = createBulkVoucher;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class createBulkVoucherResponse
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Name = "createBulkVoucherResponse", Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public voucherDTO[] createBulkVoucherResponse1;

        public createBulkVoucherResponse()
        {
        }

        public createBulkVoucherResponse(voucherDTO[] createBulkVoucherResponse1)
        {
            this.createBulkVoucherResponse1 = createBulkVoucherResponse1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class createVoucherRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public createVoucher createVoucher;

        public createVoucherRequest()
        {
        }

        public createVoucherRequest(createVoucher createVoucher)
        {
            this.createVoucher = createVoucher;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class createVoucherResponse1
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public createVoucherResponse createVoucherResponse;

        public createVoucherResponse1()
        {
        }

        public createVoucherResponse1(createVoucherResponse createVoucherResponse)
        {
            this.createVoucherResponse = createVoucherResponse;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getTransactionReasonsForCashDeskRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public getTransactionReasonsForCashDesk getTransactionReasonsForCashDesk;

        public getTransactionReasonsForCashDeskRequest()
        {
        }

        public getTransactionReasonsForCashDeskRequest(getTransactionReasonsForCashDesk getTransactionReasonsForCashDesk)
        {
            this.getTransactionReasonsForCashDesk = getTransactionReasonsForCashDesk;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getTransactionReasonsForCashDeskResponse
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Name = "getTransactionReasonsForCashDeskResponse", Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public transactionReasonInfoDTO[] getTransactionReasonsForCashDeskResponse1;

        public getTransactionReasonsForCashDeskResponse()
        {
        }

        public getTransactionReasonsForCashDeskResponse(transactionReasonInfoDTO[] getTransactionReasonsForCashDeskResponse1)
        {
            this.getTransactionReasonsForCashDeskResponse1 = getTransactionReasonsForCashDeskResponse1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getVoucherParametersRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public getVoucherParameters getVoucherParameters;

        public getVoucherParametersRequest()
        {
        }

        public getVoucherParametersRequest(getVoucherParameters getVoucherParameters)
        {
            this.getVoucherParameters = getVoucherParameters;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getVoucherParametersResponse1
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public getVoucherParametersResponse getVoucherParametersResponse;

        public getVoucherParametersResponse1()
        {
        }

        public getVoucherParametersResponse1(getVoucherParametersResponse getVoucherParametersResponse)
        {
            this.getVoucherParametersResponse = getVoucherParametersResponse;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getVoucherTransactionHistoryRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public getVoucherTransactionHistory getVoucherTransactionHistory;

        public getVoucherTransactionHistoryRequest()
        {
        }

        public getVoucherTransactionHistoryRequest(getVoucherTransactionHistory getVoucherTransactionHistory)
        {
            this.getVoucherTransactionHistory = getVoucherTransactionHistory;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getVoucherTransactionHistoryResponse
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Name = "getVoucherTransactionHistoryResponse", Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public transDTO[] getVoucherTransactionHistoryResponse1;

        public getVoucherTransactionHistoryResponse()
        {
        }

        public getVoucherTransactionHistoryResponse(transDTO[] getVoucherTransactionHistoryResponse1)
        {
            this.getVoucherTransactionHistoryResponse1 = getVoucherTransactionHistoryResponse1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getVouchersRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public getVouchers getVouchers;

        public getVouchersRequest()
        {
        }

        public getVouchersRequest(getVouchers getVouchers)
        {
            this.getVouchers = getVouchers;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getVouchersResponse
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Name = "getVouchersResponse", Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public voucherDTO[] getVouchersResponse1;

        public getVouchersResponse()
        {
        }

        public getVouchersResponse(voucherDTO[] getVouchersResponse1)
        {
            this.getVouchersResponse1 = getVouchersResponse1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class inquireVoucherRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public inquireVoucher inquireVoucher;

        public inquireVoucherRequest()
        {
        }

        public inquireVoucherRequest(inquireVoucher inquireVoucher)
        {
            this.inquireVoucher = inquireVoucher;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class inquireVoucherResponse1
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public inquireVoucherResponse inquireVoucherResponse;

        public inquireVoucherResponse1()
        {
        }

        public inquireVoucherResponse1(inquireVoucherResponse inquireVoucherResponse)
        {
            this.inquireVoucherResponse = inquireVoucherResponse;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class overrideRedeemVoucherRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public overrideRedeemVoucher overrideRedeemVoucher;

        public overrideRedeemVoucherRequest()
        {
        }

        public overrideRedeemVoucherRequest(overrideRedeemVoucher overrideRedeemVoucher)
        {
            this.overrideRedeemVoucher = overrideRedeemVoucher;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class overrideRedeemVoucherResponse1
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public overrideRedeemVoucherResponse overrideRedeemVoucherResponse;

        public overrideRedeemVoucherResponse1()
        {
        }

        public overrideRedeemVoucherResponse1(overrideRedeemVoucherResponse overrideRedeemVoucherResponse)
        {
            this.overrideRedeemVoucherResponse = overrideRedeemVoucherResponse;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class redeemRequestVoucherRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public redeemRequestVoucher redeemRequestVoucher;

        public redeemRequestVoucherRequest()
        {
        }

        public redeemRequestVoucherRequest(redeemRequestVoucher redeemRequestVoucher)
        {
            this.redeemRequestVoucher = redeemRequestVoucher;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class redeemRequestVoucherResponse1
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public redeemRequestVoucherResponse redeemRequestVoucherResponse;

        public redeemRequestVoucherResponse1()
        {
        }

        public redeemRequestVoucherResponse1(redeemRequestVoucherResponse redeemRequestVoucherResponse)
        {
            this.redeemRequestVoucherResponse = redeemRequestVoucherResponse;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class redeemVoucherRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public redeemVoucher redeemVoucher;

        public redeemVoucherRequest()
        {
        }

        public redeemVoucherRequest(redeemVoucher redeemVoucher)
        {
            this.redeemVoucher = redeemVoucher;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class redeemVoucherResponse1
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public redeemVoucherResponse redeemVoucherResponse;

        public redeemVoucherResponse1()
        {
        }

        public redeemVoucherResponse1(redeemVoucherResponse redeemVoucherResponse)
        {
            this.redeemVoucherResponse = redeemVoucherResponse;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class redeemVoucherWithoutStatusCheckRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public redeemVoucherWithoutStatusCheck redeemVoucherWithoutStatusCheck;

        public redeemVoucherWithoutStatusCheckRequest()
        {
        }

        public redeemVoucherWithoutStatusCheckRequest(redeemVoucherWithoutStatusCheck redeemVoucherWithoutStatusCheck)
        {
            this.redeemVoucherWithoutStatusCheck = redeemVoucherWithoutStatusCheck;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class redeemVoucherWithoutStatusCheckResponse1
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public redeemVoucherWithoutStatusCheckResponse redeemVoucherWithoutStatusCheckResponse;

        public redeemVoucherWithoutStatusCheckResponse1()
        {
        }

        public redeemVoucherWithoutStatusCheckResponse1(redeemVoucherWithoutStatusCheckResponse redeemVoucherWithoutStatusCheckResponse)
        {
            this.redeemVoucherWithoutStatusCheckResponse = redeemVoucherWithoutStatusCheckResponse;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class searchVouchersRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public searchVouchers searchVouchers;

        public searchVouchersRequest()
        {
        }

        public searchVouchersRequest(searchVouchers searchVouchers)
        {
            this.searchVouchers = searchVouchers;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class searchVouchersResponse
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Name = "searchVouchersResponse", Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public voucherDTO[] searchVouchersResponse1;

        public searchVouchersResponse()
        {
        }

        public searchVouchersResponse(voucherDTO[] searchVouchersResponse1)
        {
            this.searchVouchersResponse1 = searchVouchersResponse1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class voidBulkVoucherRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("arg0", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public voucherDTO[] voidBulkVoucher;

        public voidBulkVoucherRequest()
        {
        }

        public voidBulkVoucherRequest(voucherDTO[] voidBulkVoucher)
        {
            this.voidBulkVoucher = voidBulkVoucher;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class voidBulkVoucherResponse
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Name = "voidBulkVoucherResponse", Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public voucherDTO[] voidBulkVoucherResponse1;

        public voidBulkVoucherResponse()
        {
        }

        public voidBulkVoucherResponse(voucherDTO[] voidBulkVoucherResponse1)
        {
            this.voidBulkVoucherResponse1 = voidBulkVoucherResponse1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class voidVoucherRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public voidVoucher voidVoucher;

        public voidVoucherRequest()
        {
        }

        public voidVoucherRequest(voidVoucher voidVoucher)
        {
            this.voidVoucher = voidVoucher;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class voidVoucherResponse1
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.ballytech.com/sds/voucher", Order = 0)]
        public voidVoucherResponse voidVoucherResponse;

        public voidVoucherResponse1()
        {
        }

        public voidVoucherResponse1(voidVoucherResponse voidVoucherResponse)
        {
            this.voidVoucherResponse = voidVoucherResponse;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface VoucherEndPointChannel : VoucherEndPoint, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class VoucherEndPointClient : System.ServiceModel.ClientBase<VoucherEndPoint>, VoucherEndPoint
    {
        public VoucherEndPointClient()
        {
        }

        public VoucherEndPointClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public VoucherEndPointClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public VoucherEndPointClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public VoucherEndPointClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        cancelRedeemVoucherResponse1 VoucherEndPoint.cancelRedeemVoucher(cancelRedeemVoucherRequest request)
        {
            return base.Channel.cancelRedeemVoucher(request);
        }

        public cancelRedeemVoucherResponse cancelRedeemVoucher(cancelRedeemVoucher cancelRedeemVoucher1)
        {
            cancelRedeemVoucherRequest inValue = new cancelRedeemVoucherRequest();
            inValue.cancelRedeemVoucher = cancelRedeemVoucher1;
            cancelRedeemVoucherResponse1 retVal = ((VoucherEndPoint)(this)).cancelRedeemVoucher(inValue);
            return retVal.cancelRedeemVoucherResponse;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        createBulkVoucherResponse VoucherEndPoint.createBulkVoucher(createBulkVoucherRequest request)
        {
            return base.Channel.createBulkVoucher(request);
        }

        public voucherDTO[] createBulkVoucher(createBulkVoucher createBulkVoucher1)
        {
            createBulkVoucherRequest inValue = new createBulkVoucherRequest();
            inValue.createBulkVoucher = createBulkVoucher1;
            createBulkVoucherResponse retVal = ((VoucherEndPoint)(this)).createBulkVoucher(inValue);
            return retVal.createBulkVoucherResponse1;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        createVoucherResponse1 VoucherEndPoint.createVoucher(createVoucherRequest request)
        {
            return base.Channel.createVoucher(request);
        }

        public createVoucherResponse createVoucher(createVoucher createVoucher1)
        {
            createVoucherRequest inValue = new createVoucherRequest();
            inValue.createVoucher = createVoucher1;
            createVoucherResponse1 retVal = ((VoucherEndPoint)(this)).createVoucher(inValue);
            return retVal.createVoucherResponse;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        getTransactionReasonsForCashDeskResponse VoucherEndPoint.getTransactionReasonsForCashDesk(getTransactionReasonsForCashDeskRequest request)
        {
            return base.Channel.getTransactionReasonsForCashDesk(request);
        }

        public transactionReasonInfoDTO[] getTransactionReasonsForCashDesk(getTransactionReasonsForCashDesk getTransactionReasonsForCashDesk1)
        {
            getTransactionReasonsForCashDeskRequest inValue = new getTransactionReasonsForCashDeskRequest();
            inValue.getTransactionReasonsForCashDesk = getTransactionReasonsForCashDesk1;
            getTransactionReasonsForCashDeskResponse retVal = ((VoucherEndPoint)(this)).getTransactionReasonsForCashDesk(inValue);
            return retVal.getTransactionReasonsForCashDeskResponse1;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        getVoucherParametersResponse1 VoucherEndPoint.getVoucherParameters(getVoucherParametersRequest request)
        {
            return base.Channel.getVoucherParameters(request);
        }

        public getVoucherParametersResponse getVoucherParameters(getVoucherParameters getVoucherParameters1)
        {
            getVoucherParametersRequest inValue = new getVoucherParametersRequest();
            inValue.getVoucherParameters = getVoucherParameters1;
            getVoucherParametersResponse1 retVal = ((VoucherEndPoint)(this)).getVoucherParameters(inValue);
            return retVal.getVoucherParametersResponse;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        getVoucherTransactionHistoryResponse VoucherEndPoint.getVoucherTransactionHistory(getVoucherTransactionHistoryRequest request)
        {
            return base.Channel.getVoucherTransactionHistory(request);
        }

        public transDTO[] getVoucherTransactionHistory(getVoucherTransactionHistory getVoucherTransactionHistory1)
        {
            getVoucherTransactionHistoryRequest inValue = new getVoucherTransactionHistoryRequest();
            inValue.getVoucherTransactionHistory = getVoucherTransactionHistory1;
            getVoucherTransactionHistoryResponse retVal = ((VoucherEndPoint)(this)).getVoucherTransactionHistory(inValue);
            return retVal.getVoucherTransactionHistoryResponse1;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        getVouchersResponse VoucherEndPoint.getVouchers(getVouchersRequest request)
        {
            return base.Channel.getVouchers(request);
        }

        public voucherDTO[] getVouchers(getVouchers getVouchers1)
        {
            getVouchersRequest inValue = new getVouchersRequest();
            inValue.getVouchers = getVouchers1;
            getVouchersResponse retVal = ((VoucherEndPoint)(this)).getVouchers(inValue);
            return retVal.getVouchersResponse1;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        inquireVoucherResponse1 VoucherEndPoint.inquireVoucher(inquireVoucherRequest request)
        {
            return base.Channel.inquireVoucher(request);
        }

        public inquireVoucherResponse inquireVoucher(inquireVoucher inquireVoucher1)
        {
            inquireVoucherRequest inValue = new inquireVoucherRequest();
            inValue.inquireVoucher = inquireVoucher1;
            inquireVoucherResponse1 retVal = ((VoucherEndPoint)(this)).inquireVoucher(inValue);
            return retVal.inquireVoucherResponse;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        overrideRedeemVoucherResponse1 VoucherEndPoint.overrideRedeemVoucher(overrideRedeemVoucherRequest request)
        {
            return base.Channel.overrideRedeemVoucher(request);
        }

        public overrideRedeemVoucherResponse overrideRedeemVoucher(overrideRedeemVoucher overrideRedeemVoucher1)
        {
            overrideRedeemVoucherRequest inValue = new overrideRedeemVoucherRequest();
            inValue.overrideRedeemVoucher = overrideRedeemVoucher1;
            overrideRedeemVoucherResponse1 retVal = ((VoucherEndPoint)(this)).overrideRedeemVoucher(inValue);
            return retVal.overrideRedeemVoucherResponse;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        redeemRequestVoucherResponse1 VoucherEndPoint.redeemRequestVoucher(redeemRequestVoucherRequest request)
        {
            return base.Channel.redeemRequestVoucher(request);
        }

        public redeemRequestVoucherResponse redeemRequestVoucher(redeemRequestVoucher redeemRequestVoucher1)
        {
            redeemRequestVoucherRequest inValue = new redeemRequestVoucherRequest();
            inValue.redeemRequestVoucher = redeemRequestVoucher1;
            redeemRequestVoucherResponse1 retVal = ((VoucherEndPoint)(this)).redeemRequestVoucher(inValue);
            return retVal.redeemRequestVoucherResponse;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        redeemVoucherResponse1 VoucherEndPoint.redeemVoucher(redeemVoucherRequest request)
        {
            return base.Channel.redeemVoucher(request);
        }

        public redeemVoucherResponse redeemVoucher(redeemVoucher redeemVoucher1)
        {
            redeemVoucherRequest inValue = new redeemVoucherRequest();
            inValue.redeemVoucher = redeemVoucher1;
            redeemVoucherResponse1 retVal = ((VoucherEndPoint)(this)).redeemVoucher(inValue);
            return retVal.redeemVoucherResponse;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        redeemVoucherWithoutStatusCheckResponse1 VoucherEndPoint.redeemVoucherWithoutStatusCheck(redeemVoucherWithoutStatusCheckRequest request)
        {
            return base.Channel.redeemVoucherWithoutStatusCheck(request);
        }

        public redeemVoucherWithoutStatusCheckResponse redeemVoucherWithoutStatusCheck(redeemVoucherWithoutStatusCheck redeemVoucherWithoutStatusCheck1)
        {
            redeemVoucherWithoutStatusCheckRequest inValue = new redeemVoucherWithoutStatusCheckRequest();
            inValue.redeemVoucherWithoutStatusCheck = redeemVoucherWithoutStatusCheck1;
            redeemVoucherWithoutStatusCheckResponse1 retVal = ((VoucherEndPoint)(this)).redeemVoucherWithoutStatusCheck(inValue);
            return retVal.redeemVoucherWithoutStatusCheckResponse;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        searchVouchersResponse VoucherEndPoint.searchVouchers(searchVouchersRequest request)
        {
            return base.Channel.searchVouchers(request);
        }

        public voucherDTO[] searchVouchers(searchVouchers searchVouchers1)
        {
            searchVouchersRequest inValue = new searchVouchersRequest();
            inValue.searchVouchers = searchVouchers1;
            searchVouchersResponse retVal = ((VoucherEndPoint)(this)).searchVouchers(inValue);
            return retVal.searchVouchersResponse1;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        voidBulkVoucherResponse VoucherEndPoint.voidBulkVoucher(voidBulkVoucherRequest request)
        {
            return base.Channel.voidBulkVoucher(request);
        }

        public voucherDTO[] voidBulkVoucher(voucherDTO[] voidBulkVoucher1)
        {
            voidBulkVoucherRequest inValue = new voidBulkVoucherRequest();
            inValue.voidBulkVoucher = voidBulkVoucher1;
            voidBulkVoucherResponse retVal = ((VoucherEndPoint)(this)).voidBulkVoucher(inValue);
            return retVal.voidBulkVoucherResponse1;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        voidVoucherResponse1 VoucherEndPoint.voidVoucher(voidVoucherRequest request)
        {
            return base.Channel.voidVoucher(request);
        }

        public voidVoucherResponse voidVoucher(voidVoucher voidVoucher1)
        {
            voidVoucherRequest inValue = new voidVoucherRequest();
            inValue.voidVoucher = voidVoucher1;
            voidVoucherResponse1 retVal = ((VoucherEndPoint)(this)).voidVoucher(inValue);
            return retVal.voidVoucherResponse;
        }
    }
    namespace www.ballytech.com.sds.voucher
    {
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
        [System.Xml.Serialization.XmlSchemaProviderAttribute("ExportSchema")]
        [System.Xml.Serialization.XmlRootAttribute(IsNullable = false)]
        public partial class SWSException : object, System.Xml.Serialization.IXmlSerializable
        {
            private System.Xml.XmlNode[] nodesField;

            private static System.Xml.XmlQualifiedName typeName = new System.Xml.XmlQualifiedName("SWSException", "http://www.ballytech.com/sds/voucher");

            public System.Xml.XmlNode[] Nodes
            {
                get
                {
                    return this.nodesField;
                }
                set
                {
                    this.nodesField = value;
                }
            }

            public void ReadXml(System.Xml.XmlReader reader)
            {
                this.nodesField = System.Runtime.Serialization.XmlSerializableServices.ReadNodes(reader);
            }

            public void WriteXml(System.Xml.XmlWriter writer)
            {
                System.Runtime.Serialization.XmlSerializableServices.WriteNodes(writer, this.Nodes);
            }

            public System.Xml.Schema.XmlSchema GetSchema()
            {
                return null;
            }

            public static System.Xml.XmlQualifiedName ExportSchema(System.Xml.Schema.XmlSchemaSet schemas)
            {
                System.Runtime.Serialization.XmlSerializableServices.AddDefaultSchema(schemas, typeName);
                return typeName;
            }
        }
    }


}