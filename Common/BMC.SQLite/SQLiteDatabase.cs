/* ================================================================================= 
 * Purpose		:	SQL Database Class
 * File Name	:   SqlDatabase.cs
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
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using BMC.CoreLib.Data;
using System.Data.SQLite;

namespace BMC.SQLite {
    /// <summary>
    /// 
    /// </summary>
    public sealed class SQLiteDatabase
        : Database {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDatabase"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SQLiteDatabase(string connectionString)
            : base(connectionString) { }

        /// <summary>
        /// Inits the connection.
        /// </summary>
        /// <returns>Database connection.</returns>
        protected override System.Data.Common.DbConnection InitConnection() {
            return new SQLiteConnection(this.ConnectionString);
        } 
        #endregion

        #region Properties
        /// <summary>
        /// Gets the SQL connection.
        /// </summary>
        /// <value>The SQL connection.</value>
        public SQLiteConnection SQLiteConnection {
            get {
                return base.Connection as SQLiteConnection;
            }
        } 
        #endregion

        #region Connection Methods
        /// <summary>
        /// Gets the connection string builder.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>Connection string builder instance.</returns>
        public override DbConnectionStringBuilder GetConnectionStringBuilder(string connectionString) {
            return new SQLiteConnectionStringBuilder(connectionString);
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// Execute a SQLiteCommand (that returns no resultset and takes no parameters) against the provided SQLiteConnection.
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
        public override int ExecuteNonQuery(CommandType commandType, string commandText) {
            this.Open();
            return SQLiteHelper.ExecuteNonQuery(this.SQLiteConnection, commandType, commandText);
        }

        /// <summary>
        /// Execute a stored procedure via a SQLiteCommand (that returns no resultset) against the specified SQLiteConnection
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
        public override int ExecuteNonQuery(string spName, params DbParameter[] parameterValues) {
            this.Open();
            return SQLiteHelper.ExecuteNonQuery(this.SQLiteConnection, CommandType.StoredProcedure, spName, (SQLiteParameter[])parameterValues);
        }

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
        public override int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] parameterValues)
        {
            this.Open();
            return SQLiteHelper.ExecuteNonQuery(this.SQLiteConnection, commandType, commandText, (SQLiteParameter[])parameterValues);
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// Execute a SQLiteCommand (that returns a resultset and takes no parameters) against the database specified in
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
        public override System.Data.DataSet ExecuteDataset(System.Data.CommandType commandType, string commandText) {
            this.Open();
            return SQLiteHelper.ExecuteDataset(this.SQLiteConnection, commandType, commandText);
        }

        /// <summary>
        /// Execute a stored procedure via a SQLiteCommand (that returns a resultset) against the database specified in
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues"></param>
        /// <returns>
        /// A dataset containing the resultset generated by the command
        /// </returns>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        /// DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
        /// </remarks>
        public override System.Data.DataSet ExecuteDataset(string spName, params DbParameter[] parameterValues) {
            this.Open();
            return SQLiteHelper.ExecuteDataset(this.SQLiteConnection, CommandType.StoredProcedure, spName, (SQLiteParameter[])parameterValues);
        }

        /// <summary>
        /// Prepares the command.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <returns>Command object.</returns>
        public override IDbCommand PrepareCommand(CommandType commandType, string commandText)
        {
            this.Open();
            SQLiteCommand command = new SQLiteCommand();
            bool mustCloseConnection = false;
            SQLiteHelper.PrepareCommand(command, this.SQLiteConnection, null, commandType, commandText, null, out mustCloseConnection);
            return command;
        }

        /// <summary>
        /// Prepares the command.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns>Command object.</returns>
        public override IDbCommand PrepareCommand(string spName, params DbParameter[] parameterValues)
        {
            this.Open();
            SQLiteCommand command = new SQLiteCommand();
            bool mustCloseConnection = false;
            SQLiteHelper.PrepareCommand(command, this.SQLiteConnection, null, CommandType.StoredProcedure, spName, (SQLiteParameter[])parameterValues, out mustCloseConnection);
            return command;
        }
        #endregion

        #region Execute Scalar
        /// <summary>
        /// Execute a SQLiteCommand (that returns a 1x1 resultset and takes no parameters) against the provided SQLiteConnection.
        /// </summary>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>
        /// An object containing the value in the 1x1 resultset generated by the command
        /// </returns>
        /// <remarks>
        /// e.g.:
        /// int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        public override object ExecuteScalar(CommandType commandType, string commandText) {
            this.Open();
            return SQLiteHelper.ExecuteScalar(this.SQLiteConnection, commandType, commandText);
        }

        /// <summary>
        /// Execute a stored procedure via a SQLiteCommand (that returns a 1x1 resultset) against the specified SQLiteConnection
        /// using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>
        /// An object containing the value in the 1x1 resultset generated by the command
        /// </returns>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        /// int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        public override object ExecuteScalar(string spName, params object[] parameterValues) {
            this.Open();
            return SQLiteHelper.ExecuteScalar(this.SQLiteConnection, CommandType.StoredProcedure, spName, (SQLiteParameter[])parameterValues);
        }
        #endregion

        #region Create the parameters

        /// <summary>
        /// Creates the parameters.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>Array of parameters.</returns>
        public override DbParameter[] CreateParameters(int count) {
            return new SQLiteParameter[count];
        }

        /// <summary>
        /// Creates the parameter value only.
        /// </summary>
        /// <param name="paramValue">The param value.</param>
        /// <returns>Concreate parameter instance.</returns>
        public override DbParameter CreateParameterValueOnly(object paramValue) {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.Value = paramValue;
            return parameter;
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramValue">The param value.</param>
        /// <returns>Concreate parameter instance.</returns>
        public override DbParameter CreateParameter(string paramName, object paramValue) {
            SQLiteParameter parameter = new SQLiteParameter(paramName, paramValue);
            return parameter;
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramType">Type of the param.</param>
        /// <param name="paramValue">The param value.</param>
        /// <returns>Concreate parameter instance.</returns>
        public override DbParameter CreateParameter(string paramName, DbType paramType, object paramValue) {
            SQLiteParameter parameter = new SQLiteParameter(paramName, paramValue);
            parameter.DbType = paramType;
            return parameter;
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramType">Type of the param.</param>
        /// <param name="size">The size.</param>
        /// <param name="paramValue">The param value.</param>
        /// <returns>Concreate parameter instance.</returns>
        public override DbParameter CreateParameter(string paramName, DbType paramType, int size, object paramValue) {
            SQLiteParameter parameter = new SQLiteParameter(paramName, paramValue);
            parameter.DbType = paramType;
            parameter.Size = size;
            return parameter;
        }

        /// <summary>
        /// Creates the out parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <returns>Concreate parameter instance.</returns>
        public override DbParameter CreateOutParameter(string paramName) {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.ParameterName = paramName;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        /// <summary>
        /// Creates the out parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramType">Type of the param.</param>
        /// <returns>Concreate parameter instance.</returns>
        public override DbParameter CreateOutParameter(string paramName, DbType paramType) {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.ParameterName = paramName;
            parameter.DbType = paramType;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        /// <summary>
        /// Creates the out parameter.
        /// </summary>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="paramType">Type of the param.</param>
        /// <param name="size">The size.</param>
        /// <returns>Concreate parameter instance.</returns>
        public override DbParameter CreateOutParameter(string paramName, DbType paramType, int size) {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.ParameterName = paramName;
            parameter.DbType = paramType;
            parameter.Size = size;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        /// <summary>
        /// Creates the ret value parameter.
        /// </summary>
        /// <param name="paramType">Type of the param.</param>
        /// <returns>Concreate parameter instance.</returns>
        public override DbParameter CreateRetValueParameter(DbType paramType) {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.ParameterName = "@RETURN_VALUE";
            parameter.DbType = paramType;
            parameter.Direction = ParameterDirection.ReturnValue;
            return parameter;
        }

        /// <summary>
        /// Creates the ret value parameter.
        /// </summary>
        /// <param name="paramType">Type of the param.</param>
        /// <param name="size">The size.</param>
        /// <returns>Concreate parameter instance.</returns>
        public override DbParameter CreateRetValueParameter(DbType paramType, int size) {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.ParameterName = "@RETURN_VALUE";
            parameter.DbType = paramType;
            parameter.Size = size;
            parameter.Direction = ParameterDirection.ReturnValue;
            return parameter;
        } 

        #endregion
    }
}
