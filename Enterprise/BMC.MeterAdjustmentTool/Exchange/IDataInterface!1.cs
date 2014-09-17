using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BMC.MeterAdjustmentTool.Exchange
{
    public interface IParameterInfo : IDisposable
    {
        rsp_GetSiteDetailsResult Site { get; }

        int VarianceThreshold { get; }
    }

    public interface IUpdateSet : IDisposable
    {
        string UpdateSet { get; set; }

        string CurrentSet { get; set; }
    }

    public interface IQueryExecutor<R> : IParameterInfo
    {
        R ExecQuery(ExchangeDataContext context);
    }

    public interface IDataInterface : IDisposable
    {
        string ConnectionString { get; }

        DataSet SelectQuery(IParameterInfo parameterInfo);

        bool UpdateQuery(IParameterInfo parameterInfo);    
    }
}
