using System.Net.Sockets;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Serialization;
using BMC.CoreLib.Diagnostics;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
        Name = "UdpFreeformEntity")]
    public class UdpFreeformEntity : DisposableObject, IExecutorKeyFreeThread
    {
        private byte[] _data = null;
        private IPAddress _ipAddress = null;
        private string _addressString = string.Empty;

        public UdpFreeformEntity() { }

        public UdpFreeformEntity(IPAddress address, byte[] data)
        {
            this.Address = address;
            this.RawData = data;
        }

        public UdpFreeformEntity(IPAddress address, IFreeformEntity data)
        {
            this.Address = address;
            this.EntityData = data;
        }

        [DataMember(Order = 0)]
        public IPAddress Address
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                if (value != null)
                {
                    _addressString = value.ToString();
                }
            }
        }

        [DataMember(Order = 1)]
        public byte[] RawData
        {
            get { return _data; }
            set { _data = value; }
        }

        [DataMember(Order = 2)]
        public DateTime ProcessDate { get; set; }

        [XmlIgnore]
        public string AddressString
        {
            get { return _addressString; }
        }

        [XmlIgnore]
        public object Extra { get; set; }

        [XmlIgnore]
        public IFreeformEntity EntityData { get; set; }

        public override string ToString()
        {
            string address = (this.Address != null ? this.Address.ToString() : string.Empty);
            int length = (this.RawData != null ? this.RawData.Length : 0);
            if (length > 0)
                return string.Format("{0} / {1:D}", address, length);
            else
                return string.Format("{0}", address);
        }

        public string ToStringRaw(FF_FlowDirection direction)
        {
            if (_data == null) return string.Empty;

            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ToStringRaw");
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append((direction == FF_FlowDirection.G2H ? " ==> " : " <== ") + this.AddressString + " : ");
                FreeformHelper.ConvertBytesToHexString(this.RawData, sb, string.Empty);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return sb.ToString();
        }

        public virtual string ToStringDetail()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ToStringDetail"))
            {
                StringBuilder sb = new StringBuilder();

                try
                {
                    if (this.RawData != null)
                    {
                        string prefix = string.Empty;

                        sb.AppendLine();
                        FreeformHelper.WriteLogStringLine(sb, prefix);
                        sb.AppendLine(prefix + string.Format("Length : {0:D}", this.RawData.Length));
                        sb.AppendLine("Data : ");
                        FreeformHelper.ConvertBytesToHexString(this.RawData, sb, prefix);
                        sb.AppendLine();
                        FreeformHelper.WriteLogStringLine(sb, prefix);

                        if (this.EntityData != null)
                        {
                            this.EntityData.ToStringDetail(sb, "\t");
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return sb.ToString();
            }
        }

        public string UniqueKey
        {
            get { return _addressString; }
        }
    }
}
