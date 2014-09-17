using System.Data;
using System.Data.SqlClient;


namespace BMC.DataAccess
{
    /// <summary>
    /// This class is constructed based on Singleton Pattern
    /// </summary>
    public static class DataBaseServiceHandler
    {
        #region Private Variable
        private static readonly object CountLock = new object();
        private static SqlDataAdapter _adapter;
        #endregion

        #region Constructor

        /// <summary>
        /// Function creates an instance of the Static class
        /// for the first time.  But for the subsequent times
        /// existing instance is returned.
        /// </summary>
        /// <returns>DatabaseServiceHandler</returns>       

        #endregion

        #region Private Shared Function

        private static SqlCommand CreateCommand(CommandType commandType, SqlConnection sqlConnection, string query)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.Connection.Open();
            command.CommandType = commandType;
            command.CommandText = query;
            command.CommandTimeout = SqlHelper.LoadCommandTimeout();
            return command;
        }

        private static SqlDataAdapter CreateAdapter()
        {
            return new SqlDataAdapter();
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SqlParameter AddParameter<T>(string paramName, DbType dataType, T value)
        {
            SqlParameter param = new SqlParameter();
            param.DbType = dataType;
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="value">The value.</param>
        /// <param name="parameterDirection">The parameter direction.</param>
        /// <returns></returns>
        public static SqlParameter AddParameter<T>(string paramName, DbType dataType, T value, ParameterDirection parameterDirection)
        {
            SqlParameter param = new SqlParameter();
            param.DbType = dataType;
            param.ParameterName = paramName;
            param.Direction = parameterDirection;
            param.Value = value;
            return param;
        }

        /// <summary>
        /// This Function Loads the Datatable from the SQL Command Passed.  This function is exposed if the User wants to Create Connection
        /// from the Application.  This could be useful in places where users wants handle Transaction from the Application Level
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public static T FillDataTable<T>(SqlCommand command, T returnTable) where T : DataTable, new()
        {
            try
            {
                _adapter = CreateAdapter();
                _adapter.SelectCommand = command;
                _adapter.Fill(returnTable);
                return returnTable;
            }
            finally
            {
                _adapter.Dispose();
            }
        }

        /// <summary>
        /// Loads from command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public static T Fill<T>(SqlCommand command) where T : DataSet, new()
        {
            T returnSet = new T();
            try
            {
                _adapter = CreateAdapter();
                _adapter.SelectCommand = command;
                _adapter.Fill(returnSet);
                return returnSet;
            }
            finally
            {
                _adapter.Dispose();
            }
        }

        /// <summary>
        /// Fills the specified SQL command data type.
        /// </summary>
        /// <param name="sqlConnectionstring">The SQL connectionstring.</param>
        /// <param name="sqlCommandDataType">Type of the SQL command data.</param>
        /// <param name="query">The query.</param>
        /// <param name="dataTableFill">The data table fill. Pass this function incase of Typed Dataset</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public static DataTable Fill(string sqlConnectionstring, CommandType sqlCommandDataType, string query, DataTable dataTableFill, params SqlParameter[] param)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionstring);
            if (dataTableFill == null)
                dataTableFill = new DataTable();
            lock (CountLock)
            {
                try
                {
                    SqlCommand fillCommand = CreateCommand(sqlCommandDataType, sqlConnection, query);
                    fillCommand.Parameters.AddRange(param);
                    return FillDataTable<DataTable>(fillCommand, dataTableFill);
                }
                finally
                {
                    SqlConnection.ClearPool(sqlConnection);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
        }

        /// <summary>
        /// Fills the specified SQL command data type.
        /// </summary>
        /// <param name="sqlConnectionstring">The SQL connectionstring.</param>
        /// <param name="sqlCommandDataType">Type of the SQL command data.</param>
        /// <param name="query">The query.</param>
        /// <param name="dataSetFill">The data set fill.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public static DataSet Fill(string sqlConnectionstring, CommandType sqlCommandDataType, string query, DataSet dataSetFill, params SqlParameter[] param)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionstring);
            lock (CountLock)
            {
                try
                {
                    SqlCommand fillCommand = CreateCommand(sqlCommandDataType, sqlConnection, query);
                    fillCommand.Parameters.AddRange(param);
                    return Fill<DataSet>(fillCommand);
                }
                finally
                {
                    SqlConnection.ClearPool(sqlConnection);
                    sqlConnection.Close();
                    sqlConnection.Dispose();

                }
            }
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="sqlConnectionString">The SQL connection string.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="query">The query.</param>
        /// <param name="paramArray">The param array.</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sqlConnectionString, CommandType commandType, string query, params SqlParameter[] paramArray)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            lock (CountLock)
            {
                try
                {
                    SqlCommand executeCommand = CreateCommand(commandType, sqlConnection, query);
                    executeCommand.Parameters.AddRange(paramArray);
                    return executeCommand.ExecuteNonQuery();
                }
                finally
                {
                    SqlConnection.ClearPool(sqlConnection);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }

        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlConnectionString">The SQL connection string.</param>
        /// <param name="sqlCommandDataType">Type of the SQL command data.</param>
        /// <param name="query">The query.</param>
        /// <param name="paramArray">The param array.</param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(string sqlConnectionString, CommandType sqlCommandDataType, string query, params SqlParameter[] paramArray)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            lock (CountLock)
            {
                try
                {
                    SqlCommand executeCommand = CreateCommand(sqlCommandDataType, sqlConnection, query);
                    executeCommand.Parameters.AddRange(paramArray);
                    return (T)executeCommand.ExecuteScalar();
                }
                finally
                {
                    SqlConnection.ClearPool(sqlConnection);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
        }

        public static string ExecuteXMLReader<T>(string sqlConnectionString, CommandType sqlCommandDataType, string query, params SqlParameter[] paramArray)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            lock (CountLock)
            {
                try
                {
                    SqlCommand executeCommand = CreateCommand(sqlCommandDataType, sqlConnection, query);
                    System.Xml.XmlReader xmlr;
                    string sReturn = "";

                    executeCommand.Parameters.AddRange(paramArray);
                    xmlr = executeCommand.ExecuteXmlReader();

                    xmlr.Read();
                    while (xmlr.ReadState != System.Xml.ReadState.EndOfFile)
                    {
                        sReturn = sReturn + xmlr.ReadOuterXml();
                    }
                    return sReturn;

                }
                finally
                {
                    SqlConnection.ClearPool(sqlConnection);
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
        }

        #endregion

    }

}
