// -----------------------------------------------------------------------
// <copyright file="DbContextHelper_1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.EntityData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BMC.CoreLib;
    using System.Data.Entity;
    using System.Data.Objects;
    using BMC.CoreLib.Diagnostics;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DbContextHelper<C> : DisposableObject
        where C : DbContext
    {
        private string DB_CONN_STRING = "###DB_CONN_STRING##";
        private readonly string EF_EXCHANGE_CONN_STRING =
            "metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider=System.Data.SqlClient;provider connection string=\"###DB_CONN_STRING##multipleactiveresultsets=True;App=EntityFramework\"";

        private Func<string> _getConnectionString = null;
        private Func<string, C> _createInstance = null;

        public DbContextHelper(string dbContextName,
            Func<string> getConnectionString,
            Func<string, C> createInstance)
        {
            EF_EXCHANGE_CONN_STRING = string.Format(EF_EXCHANGE_CONN_STRING, dbContextName);
            _getConnectionString = getConnectionString;
            _createInstance = createInstance;
        }

        /// <summary>
        /// Gets the exchange data context.
        /// </summary>
        /// <returns>Data context instance.</returns>
        public C GetContext()
        {
            return DbContextHelper.GetDataContextInstance<C>(
                DbContextHelper.GetEFConnectionString(EF_EXCHANGE_CONN_STRING, DB_CONN_STRING, _getConnectionString()),
                _createInstance);
        }

        /// <summary>
        /// Executes the given function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exec">The exec.</param>
        /// <param name="evaluateResult">The evaluate result.</param>
        /// <param name="onFailure">The on failure.</param>
        public void ExecFuncEnumerable<T>(Func<C, ObjectResult<T>> exec,
            Action<IEnumerable<T>> onSuccess)
        {
            DbContextHelper.ExecFuncEnumerable<C, T>(GetContext(),
                exec, onSuccess, null);
        }

        /// <summary>
        /// Executes the given function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exec">The exec.</param>
        /// <param name="evaluateResult">The evaluate result.</param>
        /// <param name="onFailure">The on failure.</param>
        public void ExecFuncEnumerable<T>(Func<C, ObjectResult<T>> exec,
            Action<IEnumerable<T>> onSuccess,
            Action<ExecFuncFailureResult> onFailure)
        {
            DbContextHelper.ExecFuncEnumerable<C, T>(GetContext(),
                exec, onSuccess, onFailure);
        }

        /// <summary>
        /// Executes the given function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exec">The exec.</param>
        /// <param name="evaluateResult">The evaluate result.</param>
        /// <param name="onFailure">The on failure.</param>
        public void ExecFunc<T>(Func<C, ObjectResult<T>> exec,
            Func<T, long, bool> evaluateResult,
            Action<ExecFuncFailureResult> onFailure)
        {
            DbContextHelper.ExecFunc<C, T>(GetContext(),
                exec, evaluateResult, null);
        }

        /// <summary>
        /// Executes the given function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exec">The exec.</param>
        /// <param name="evaluateResult">The evaluate result.</param>
        public void ExecFunc<T>(Func<C, ObjectResult<T>> exec,
            Func<T, long, bool> evaluateResult)
        {
            ExecFunc<T>(exec, evaluateResult, null);
        }

        /// <summary>
        /// Executes the given function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exec">The exec.</param>
        /// <param name="evaluateResult">The evaluate result.</param>
        /// <param name="onFailure">The on failure.</param>
        public void ExecFunc<T>(Func<C, T> exec,
            Action<T> evaluateResult,
            Action<ExecFuncFailureResult> onFailure)
        {
            DbContextHelper.ExecFunc<C, T>(GetContext(),
                exec, evaluateResult, null);
        }

        /// <summary>
        /// Executes the given function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exec">The exec.</param>
        /// <param name="evaluateResult">The evaluate result.</param>
        public void ExecFunc<T>(Func<C, T> exec,
            Action<T> evaluateResult)
        {
            ExecFunc<T>(exec, evaluateResult, null);
        }

        public DbSet<T> GetDbSet<T>(Func<C, DbSet<T>> exec)
            where T: class
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetDbSet<T>");
            DbSet<T> result = default(DbSet<T>);

            try
            {
                using (C context = this.GetContext())
                {
                    result = exec(context);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public T ExecFunc<T>(Func<C, T> exec)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetDbSet<T>");
            T result = default(T);

            try
            {
                using (C context = this.GetContext())
                {
                    result = exec(context);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
