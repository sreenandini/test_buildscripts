/* ================================================================================= 
 * Purpose		:	Database Class
 * File Name	:   Database.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	21/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 21/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using BMC.CoreLib;

namespace BMC.CoreLib.Data {
    /// <summary>
    /// Database Class
    /// </summary>
    public abstract class Database : DisposableObjectNotify {
        #region Variables
        /// <summary>
        /// Database connection
        /// </summary>
        private DbConnection _dbConnection = null;
        private object _lock = new object();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        protected Database(string connectionString) {
            this.ConnectionString = connectionString;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public DbConnection Connection {
            get {
                return _dbConnection;
            }
        }
        #endregion

        #region Connection Methods
        /// <summary>
        /// Opens the connection.
        /// </summary>
        public virtual void Open() {
            lock (_lock) {
                if (_dbConnection == null) {
                    //SharedData.GlobalLogger.WriteLogInfo("Creating connection object.");
                    _dbConnection = InitConnection();
                }
                if (_dbConnection.State != System.Data.ConnectionState.Open) {
                    _dbConnection.Open();
                }
            }
        }

        /// <summary>
        /// Inits the connection.
        /// </summary>
        /// <returns>Database connection.</returns>
        protected abstract DbConnection InitConnection();

        /// <summary>
        /// Gets the connection string builder.
        /// </summary>
        /// <returns>Connection string builder instance.</returns>
        public virtual DbConnectionStringBuilder GetConnectionStringBuilder() {
            return this.GetConnectionStringBuilder(this.ConnectionString);
        }

        /// <summary>
        /// Gets the connection string builder.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Connection string builder instance.</returns>
        public abstract DbConnectionStringBuilder GetConnectionStringBuilder(string connectionString);

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public virtual void Close() {
            if (_dbConnection != null) {
                if (_dbConnection.State == ConnectionState.Open) {
                    _dbConnection.Close();
                }
            }
        }

        /// <summary>
        /// Overridable method which releases the managed resources.
        /// </summary>
        protected override void DisposeManaged() {
            base.DisposeManaged();

            if (_dbConnection != null) {
                this.Close();
                _dbConnection.Dispose();
                _dbConnection = null;
            }
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>
        /// An int representing the number of rows affected by the command
        /// </returns>
        /// <remarks>
        /// e.g.:
        /// int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        public abstract int ExecuteNonQuery(CommandType commandType, string commandText);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection
        /// using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>
        /// An int representing the number of rows affected by the command
        /// </returns>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        /// int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
        /// </remarks>
        public abstract int ExecuteNonQuery(string spName, params DbParameter[] parameterValues);

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>
        /// An int representing the number of rows affected by the command
        /// </returns>
        /// <remarks>
        /// e.g.:
        /// int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        public abstract int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] parameterValues);
        #endregion

        #region ExecuteDataset
        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
        /// the connection string.
        /// </summary>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>
        /// A dataset containing the resultset generated by the command
        /// </returns>
        /// <remarks>
        /// e.g.:
        /// DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        public abstract DataSet ExecuteDataset(CommandType commandType, string commandText);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameters">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>
        /// A dataset containing the resultset generated by the command
        /// </returns>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        /// DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
        /// </remarks>
        public abstract DataSet ExecuteDataset(string spName, params DbParameter[] parameterValues);

        /// <summary>
        /// Prepares the command.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <returns>Command object.</returns>
        public abstract IDbCommand PrepareCommand(CommandType commandType, string commandText);

        /// <summary>
        /// Prepares the command.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns>Command object.</returns>
        public abstract IDbCommand PrepareCommand(string spName, params DbParameter[] parameterValues);
        #endregion

        #region Execute Scalar
        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public abstract object ExecuteScalar(CommandType commandType, string commandText);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public abstract object ExecuteScalar(string spName, params object[] parameterValues);
        #endregion

        #region Parameters
        /// <summary>
        /// Creates the parameters.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>Array of parameters.</returns>
        public abstract DbParameter[] CreateParameters(int count);

        /// <summary>
        /// Creates the parameters.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>Array of parameters.</returns>
        public virtual DbParameter[] CreateParametersV(params DbParameter[] parameters)
        {
            DbParameter[] parameters2 = this.CreateParameters(parameters.Length);
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters2[i] = parameters[i];
            }
            return parameters2;
        }

        /// <summary>
        /// Creates the parameter value only.
        /// </summary>
        /// <param name="paramValue">The param value.</param>
        /// <returns>Concreate parameter instance.</returns>
        public abstract DbParameter CreateParameterValueOnly(object paramValue);

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramValue">The param value.</param>
        /// <returns>Concreate parameter instance.</returns>
        public abstract DbParameter CreateParameter(string paramName, object paramValue);

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramType">Type of the param.</param>
        /// <param name="paramValue">The param value.</param>
        /// <returns>Concreate parameter instance.</returns>
        public abstract DbParameter CreateParameter(string paramName, DbType paramType, object paramValue);

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramType">Type of the param.</param>
        /// <param name="size">The size.</param>
        /// <param name="paramValue">The param value.</param>
        /// <returns>Concreate parameter instance.</returns>
        public abstract DbParameter CreateParameter(string paramName, DbType paramType, int size, object paramValue);

        /// <summary>
        /// Creates the out parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <returns>Concreate parameter instance.</returns>
        public abstract DbParameter CreateOutParameter(string paramName);

        /// <summary>
        /// Creates the out parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramType">Type of the param.</param>
        /// <returns>Concreate parameter instance.</returns>
        public abstract DbParameter CreateOutParameter(string paramName, DbType paramType);

        /// <summary>
        /// Creates the out parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramType">Type of the param.</param>
        /// <param name="size">The size.</param>
        /// <param name="paramValue">The param value.</param>
        /// <returns>Concreate parameter instance.</returns>
        public abstract DbParameter CreateOutParameter(string paramName, DbType paramType, int size);

        /// <summary>
        /// Creates the ret value parameter.
        /// </summary>
        /// <param name="paramType">Type of the param.</param>
        /// <returns>Concreate parameter instance.</returns>
        public abstract DbParameter CreateRetValueParameter(DbType paramType);

        /// <summary>
        /// Creates the ret value parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramType">Type of the param.</param>
        /// <param name="size">The size.</param>
        /// <returns>Concreate parameter instance.</returns>
        public abstract DbParameter CreateRetValueParameter(DbType paramType, int size);
        #endregion

        #region Helper Methods

        /// <summary>
        /// Executes the non query and return.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Execute the result.</returns>
        public object ExecuteNonQueryAndReturn(string spName, DbParameter[] parameters) {
            DbParameter retValue = this.CreateRetValueParameter(DbType.Int32);
            parameters[parameters.Length - 1] = retValue;
            this.ExecuteNonQuery(spName, parameters);

            if (retValue.Value != DBNull.Value) {
                return retValue.Value;
            }
            return null;
        }

        /// <summary>
        /// Executes the non query and return int.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="failedValue">The failed value.</param>
        /// <returns>Return integer result.</returns>
        public int ExecuteNonQueryAndReturnInt(string spName, DbParameter[] parameters, int failedValue) {
            object value = ExecuteNonQueryAndReturn(spName, parameters);
            if (value == null) return failedValue;
            return (int)value;
        }

        /// <summary>
        /// Executes the non query and return int OK.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Return integer result.</returns>
        public bool ExecuteNonQueryAndReturnIntOK(string spName, DbParameter[] parameters) {
            return (ExecuteNonQueryAndReturnInt(spName, parameters, -1) == 0);
        }

        #endregion
    }
}
