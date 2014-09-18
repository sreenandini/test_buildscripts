using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace BMC.Transport
{
    public class CageVoucher : Voucher
    {
        public System.DateTime expireDate;
        public string tktStatus { get; set; }
        public short tktStatusID { get; set; }
        public int expiryDays { get; set; }
        public int ErrorCode { get; set; }
    }
}
   