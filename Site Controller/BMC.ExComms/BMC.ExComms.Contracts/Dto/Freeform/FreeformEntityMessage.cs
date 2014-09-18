using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public interface IFreeformEntity_Msg
        : IFreeformEntity,
        ICommsEntity,
        IFreeformEntity_Msg_Copyable
    {
        //IPAddress GmuIpAddress { get; set; }
        FF_FlowDirection FlowDirection { get; }
        FF_FlowInitiation FlowInitiation { get; set; }

        FF_GmuId_DeviceTypes DeviceType { get; set; }
        FF_AppId_SessionIds SessionID { get; set; }
        int TransactionID { get; set; }
        ushort DataLength { get; set; }
        byte Checksum { get; set; }
        byte ChecksumCalculated { get; set; }
        bool IsValid { get; }

        int ActualSessionID { get; set; }
    }

    public interface IFreeformEntity_Msg_Copyable
    {
        IFreeformEntity_Msg CopyTo(FF_FlowDirection direction, FFCreateEntityRequest request);
    }

    public class FreeformEntity_Msg
        : FreeformEntity,
        IFreeformEntity_Msg,
        IFreeformEntity_Msg_Copyable
    {
        private string _ipAddressString = string.Empty;
        private IPAddress _ipAddress = null;
        private FF_AppId_SessionIds _sessionID = FF_AppId_SessionIds.None;

        public FreeformEntity_Msg()
        {
            this.FlowInitiation = FF_FlowInitiation.Any;
            this.DeviceType = FF_GmuId_DeviceTypes.Ethernet;
            this.SessionID = FF_AppId_SessionIds.A1;
            _ipAddressString = IPAddress.None.ToString();
            _ipAddress = IPAddress.None;
        }

        //public IPAddress GmuIpAddress { get; set; }
        public string IpAddress
        {
            get { return _ipAddressString; }
            set
            {
                _ipAddress = value.ToIPAddress();
                _ipAddressString = _ipAddress.ToString();
            }
        }

        public IPAddress IpAddress2
        {
            get { return _ipAddress; }
        }

        public string HostIpAddress { get; set; }
        public int InstallationNo { get; set; }

        public virtual FF_FlowDirection FlowDirection { get { return FF_FlowDirection.G2H; } }
        public virtual FF_FlowInitiation FlowInitiation { get; set; }

        public FF_GmuId_DeviceTypes DeviceType { get; set; }
        public FF_AppId_SessionIds SessionID
        {
            get { return _sessionID; }
            set
            {
                _sessionID = value;
                this.ActualSessionID = (int)value;
            }
        }
        public int TransactionID { get; set; }
        public ushort DataLength { get; set; }
        public byte Checksum { get; set; }
        public byte ChecksumCalculated { get; set; }
        public int ActualSessionID { get; set; }

        public virtual bool IsValid
        {
            get
            {
                return (this.Checksum == this.ChecksumCalculated);
            }
        }

        public override bool IsRootNode
        {
            get
            {
                return true;
            }
        }

        public override string UniqueKey
        {
            get { return this.IpAddress; }
        }

        public override int UniqueEntityId
        {
            get
            {
                return (int)this.SessionID;
            }
        }

        public override int EntityPrimaryKeyId
        {
            get
            {
                return (int)this.SessionID;
            }
        }

        public override string EntityUniqueKeyDirection
        {
            get
            {
                FreeformEntityKeyValue keyValue = new FreeformEntityKeyValue(this.EntityUniqueKeyString, this.FlowDirection);
                return keyValue.FullKey;
            }
        }

        public IFreeformEntity_Msg CopyTo(FF_FlowDirection direction, FFCreateEntityRequest request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CopyTo"))
            {
                IFreeformEntity_Msg result = null;

                try
                {
                    result = FreeformEntityFactory.CreateEntity<IFreeformEntity_Msg>(direction, request);
                    result.IpAddress = this.IpAddress;
                    result.DeviceType = this.DeviceType;
                    result.SessionID = this.SessionID;
                    if (!request.SkipTransactionId)
                        result.TransactionID = this.TransactionID;
                    result.IsSecured = this.IsSecured;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected virtual void CopyToInternal(IFreeformEntity_Msg message) { }

        protected override void ToString(StringBuilder sb)
        {
            base.ToString(sb);
            sb.Append(this.GetType().Name + ", ");
            sb.Append(this.SessionID.ToString());
        }
    }
}
