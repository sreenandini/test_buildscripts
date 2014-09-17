using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Common.ExceptionManagement;

namespace BMC.MeterAdjustmentTool.Exchange
{
    public abstract class ExchangeDataInterface
        : DisposableObject,
        IDataInterface
    {
        protected string _connectionString = string.Empty;

        protected ExchangeDataInterface(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        #region IDataInterface<T> Members

        protected R ExecQuery<R>(IParameterInfo parameterInfo)
        {
            IQueryExecutor<R> queryInfo = parameterInfo as IQueryExecutor<R>;
            if (queryInfo == null) return default(R);
            R result = default(R);

            try
            {
                using (ExchangeDataContext context = new ExchangeDataContext(this.ConnectionString))
                {
                    try
                    {
                        result = queryInfo.ExecQuery(context);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        protected abstract bool CanExecute(IParameterInfo parameterInfo);

        public System.Data.DataSet SelectQuery(IParameterInfo parameterInfo)
        {
            if (!this.CanExecute(parameterInfo)) return null;
            return this.ExecQuery<DataSet>(parameterInfo);
        }

        public bool UpdateQuery(IParameterInfo parameterInfo)
        {
            if (!this.CanExecute(parameterInfo)) return false;
            return this.ExecQuery<bool>(parameterInfo);
        }

        #endregion
    }
}
