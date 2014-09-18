using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.VaultInterface.Business.Model
{
    public class Response
    {
        int _ErrCode;
        string _ErrDesc;
        public Response()
        {
             _ErrCode = 0;
             _ErrDesc = "Success";
        }

        public int ErrCode { get { return _ErrCode; } set { _ErrCode = value; } }
        public string ErrDesc { get { return _ErrDesc; } set { _ErrDesc = value; } }
        public override string ToString()
        {
            return string.Format("<Response><ErrCode>{0}</ErrCode><ErrDesc>{1}</ErrDesc></Response>", ErrCode.ToString(), ErrDesc);
        }
    }
}
