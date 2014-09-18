using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Common.ExceptionManagement;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.Transport;
using BMC.DataAccess;
namespace BMC.DBInterface.CashDeskOperator
{
    
    public class GMUSettingsDataAccess : DataContext
    {

        private static readonly MappingSource MappingSource = new AttributeMappingSource();

        public GMUSettingsDataAccess(string connection) :
            base(connection, MappingSource)
        {
            Connection.ConnectionString = CommonDataAccess.ExchangeConnectionString;
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
        }

        public GMUSettingsDataAccess(IDbConnection connection) :
            base(connection, MappingSource)
        { }

        public GMUSettingsDataAccess(string connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        { }

        public GMUSettingsDataAccess(IDbConnection connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        { }

        [Function(Name = "dbo.rsp_GetGMUPosDetails")]
        public ISingleResult<GetGMUPosDetailsResult> GetGMUPosDetails([Parameter(Name = "IPList", DbType = "NVarChar(MAX)")] string iPList)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iPList);
            return ((ISingleResult<GetGMUPosDetailsResult>)(result.ReturnValue));
        }
    }

    }

