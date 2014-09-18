using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using BMC.Transport;


namespace BMC.DBInterface.CashDeskOperator
{

    public class GMUPingDataAccess : System.Data.Linq.DataContext
    {
        public GMUPingDataAccess(IDbConnection dbConnection)
            : base(dbConnection)
        {
        }
     
        [Function(Name = "dbo.rsp_GetGmusforPing")]
        public ISingleResult<GMUListtoPing> rsp_GetGmusforPing()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<GMUListtoPing>)(result.ReturnValue));
        }
    }
}
