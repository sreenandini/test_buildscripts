using BMC.CoreLib;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
    {
        public bool GetTicketExpire(ref int? exprDays)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    if (DataContext.rsp_GetTicketExpire(ref exprDays) > -1)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return false;
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetTicketExpire")]
        public int rsp_GetTicketExpire([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TktExpDays", DbType = "Int")] ref System.Nullable<int> tktExpDays)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), tktExpDays);
            tktExpDays = ((System.Nullable<int>)(result.GetParameterValue(0)));
            return ((int)(result.ReturnValue));
        }
    }
}
