using System;
using System.Data.EntityClient;
using System.Data.Objects;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System.Data.Entity;
using System.Collections.Generic;

namespace BMC.EntityData
{
    /// <summary>
    /// ExecFuncFailureResult
    /// </summary>
    public enum ExecFuncFailureResult
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// DBConnectionFailure
        /// </summary>
        DBConnectionFailure,
        /// <summary>
        /// UnableToExecute
        /// </summary>
        UnableToExecute,
        /// <summary>
        /// ObjectResultsEmpty
        /// </summary>
        ObjectResultsEmpty,
        /// <summary>
        /// ObjectResultsZeroCount
        /// </summary>
        ObjectResultsZeroCount,
    }

    /// <summary>
    /// Data Context Helper
    /// </summary>
    public static class DbContextHelper
    {
        private const string EF_PROVIDER = "System.Data.SqlClient";
        //private const string EF_EXCHANGE_METADATA = "res://*/DataLayer.ExchangeDataModel.csdl|res://*/DataLayer.ExchangeDataModel.ssdl|res://*/DataLayer.ExchangeDataModel.msl";

        /// <summary>
        /// Initializes the <see cref="DataContextHelper"/> class.
        /// </summary>
        static DbContextHelper() { }

        /// <summary>
        /// Gets the EF connection string.
        /// </summary>
        /// <param name="formatString">The format string.</param>
        /// <param name="placeHolder">The place holder.</param>
        /// <param name="connectinString">The connectin string.</param>
        /// <returns></returns>
        public static string GetEFConnectionString(string formatString, string placeHolder, string connectinString)
        {
            if (placeHolder.IsEmpty()) return formatString;
            return formatString.Replace(placeHolder, connectinString);
        }

        /// <summary>
        /// Gets the EF connection string.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="connectinString">The connectin string.</param>
        /// <returns></returns>
        public static string GetEFConnectionString(string metadata, string connectinString)
        {
            EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder();
            builder.Provider = EF_PROVIDER;
            builder.Metadata = metadata;
            builder.ProviderConnectionString = connectinString;
            return builder.ToString();
        }

        /// <summary>
        /// Gets the data context instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="efConnectionString">The ef connection string.</param>
        /// <param name="createInstance">The create instance.</param>
        /// <returns>Data context instance.</returns>
        public static T GetDataContextInstance<T>(string efConnectionString, Func<string, T> createInstance)
            where T : DbContext
        {
            ModuleProc PROC = new ModuleProc("DataContextHelper", "GetDataContextInstance<T>");
            T result = default(T);

            try
            {
                EntityConnection conn = new EntityConnection(efConnectionString);
                result = createInstance(efConnectionString);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        /// <summary>
        /// Executes the given function.
        /// </summary>
        /// <typeparam name="C">Type of the object context.</typeparam>
        /// <typeparam name="T">Type of the object result.</typeparam>
        /// <param name="exec">The exec.</param>
        /// <param name="evaluateResult">The evaluate result.</param>
        /// <param name="onFailure">The on failure.</param>
        public static void ExecFuncEnumerable<C, T>(C dataContext,
            Func<C, ObjectResult<T>> exec,
            Action<IEnumerable<T>> onSuccess,
            Action<ExecFuncFailureResult> onFailure)
            where C : DbContext
        {
            ModuleProc PROC = new ModuleProc("DataContextHelper", "ExecFunc<T>");
            ObjectResult<T> result = null;
            ExecFuncFailureResult failureResult = ExecFuncFailureResult.None;

            try
            {
                using (C context = dataContext)
                {
                    if (context != null)
                    {
                        try
                        {
                            result = exec(context);
                            if (result != null &&
                                onSuccess != null)
                            {
                                try
                                {
                                    onSuccess(result);
                                }
                                catch (Exception ex)
                                {
                                    Log.Exception(PROC, ex);
                                }
                            }
                            else
                            {
                                failureResult = ExecFuncFailureResult.ObjectResultsEmpty;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(PROC, ex);
                            failureResult = ExecFuncFailureResult.UnableToExecute;
                        }
                    }
                    else
                    {
                        failureResult = ExecFuncFailureResult.DBConnectionFailure;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (failureResult != ExecFuncFailureResult.None &&
                    onFailure != null)
                {
                    onFailure(failureResult);
                }
            }
        }

        /// <summary>
        /// Executes the given function.
        /// </summary>
        /// <typeparam name="C">Type of the object context.</typeparam>
        /// <typeparam name="T">Type of the object result.</typeparam>
        /// <param name="exec">The exec.</param>
        /// <param name="evaluateResult">The evaluate result.</param>
        /// <param name="onFailure">The on failure.</param>
        public static void ExecFunc<C, T>(C dataContext,
            Func<C, ObjectResult<T>> exec,
            Func<T, long, bool> evaluateResult,
            Action<ExecFuncFailureResult> onFailure)
            where C : DbContext
        {
            ModuleProc PROC = new ModuleProc("DataContextHelper", "ExecFunc<T>");
            ExecFuncFailureResult failureResult = ExecFuncFailureResult.None;

            try
            {
                long index = 0;
                bool hasRows = false;

                using (C context = dataContext)
                {
                    if (context != null)
                    {
                        try
                        {
                            ObjectResult<T> resultItems = exec(context);
                            if (resultItems != null)
                            {
                                foreach (T resultItem in resultItems)
                                {
                                    hasRows |= true;
                                    if (evaluateResult != null)
                                    {
                                        try
                                        {
                                            bool isOK = evaluateResult(resultItem, index);
                                            if (!isOK) break;
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Exception(PROC, ex);
                                        }
                                    }

                                    index++;
                                }

                                if (!hasRows)
                                {
                                    failureResult = ExecFuncFailureResult.ObjectResultsZeroCount;
                                }
                            }
                            else
                            {
                                failureResult = ExecFuncFailureResult.ObjectResultsEmpty;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(PROC, ex);
                            failureResult = ExecFuncFailureResult.UnableToExecute;
                        }
                    }
                    else
                    {
                        failureResult = ExecFuncFailureResult.DBConnectionFailure;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (failureResult != ExecFuncFailureResult.None &&
                    onFailure != null)
                {
                    onFailure(failureResult);
                }
            }
        }

        /// <summary>
        /// Executes the given function.
        /// </summary>
        /// <typeparam name="C">Type of the object context.</typeparam>
        /// <typeparam name="T">Type of the object result.</typeparam>
        /// <param name="exec">The exec.</param>
        /// <param name="evaluateResult">The evaluate result.</param>
        /// <param name="onFailure">The on failure.</param>
        public static void ExecFunc<C, T>(C dataContext,
            Func<C, T> exec,
            Action<T> evaluateResult,
            Action<ExecFuncFailureResult> onFailure)
            where C : DbContext
        {
            ModuleProc PROC = new ModuleProc("DataContextHelper", "ExecFunc<T>");
            ExecFuncFailureResult failureResult = ExecFuncFailureResult.None;

            try
            {
                using (C context = dataContext)
                {
                    if (context != null)
                    {
                        try
                        {
                            T result = exec(context);
                            if (evaluateResult != null)
                            {
                                evaluateResult(result);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(PROC, ex);
                            failureResult = ExecFuncFailureResult.UnableToExecute;
                        }
                    }
                    else
                    {
                        failureResult = ExecFuncFailureResult.DBConnectionFailure;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (failureResult != ExecFuncFailureResult.None &&
                    onFailure != null)
                {
                    onFailure(failureResult);
                }
            }
        }
    }
}
