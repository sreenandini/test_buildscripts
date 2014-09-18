
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BMC.CashDeskOperator
{

    public partial class userDataContext : System.Data.Linq.DataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public userDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        [Function(Name = "dbo.usp_Export_History")]
        public int Export_History([Parameter(Name = "Reference1", DbType = "VarChar(10)")] string reference1, [Parameter(Name = "Reference2", DbType = "VarChar(10)")] string reference2, [Parameter(Name = "Type", DbType = "VarChar(30)")] string type, [Parameter(Name = "Status", DbType = "VarChar(1)")] System.Nullable<char> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reference1, reference2, type, status);
            return ((int)(result.ReturnValue));
        }
    }
}
