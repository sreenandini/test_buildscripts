using System;
using System.Data;
using System.Collections.Generic;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using System.Data.Linq;
using System.Linq;
using BMC.Transport;
using System.Data.SqlClient;

namespace BMC.Business.CashDeskOperator
{
    public class GMUPingBiz
    {
        public List<GMUListtoPing> GetGmuList()
        {
            List<GMUListtoPing> tempList;
            using (GMUPingDataAccess db = new GMUPingDataAccess(new SqlConnection(CommonUtilities.ExchangeConnectionString)))
            {
               tempList=  db.rsp_GetGmusforPing().ToList();
            }
            return tempList;
        }
    }
}
