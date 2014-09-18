using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Business.CashDeskOperator;
using BMC.Transport;

namespace BMC.CashDeskOperator
{
    public class GMUPing
    {
        GMUPingBiz objGMUPingBiz = new GMUPingBiz();

        public List<GMUListtoPing> GetGmuList()
        {
                return objGMUPingBiz.GetGmuList();
        }
    }
}
