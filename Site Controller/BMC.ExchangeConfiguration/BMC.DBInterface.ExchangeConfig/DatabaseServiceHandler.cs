using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace BMC.DBInterface.ExchangeConfig
{
    /// <summary>
    /// This class is constructed based on Singleton Pattern
    /// </summary>
    public class DataBaseServiceHandler
    {
        /// <summary>
        /// Private Variable Declaration.  
        /// All objects declared here are 
        /// Static (only one instance of 
        /// these objects are created 
        /// across the application)
        /// </summary>
        #region Private Variable
        private static DataBaseServiceHandler Obj;
        private static SqlCommand FillCommand;
        private static SqlCommand ExecuteCommand;
        private static string _connectionstring;
        private static int ReturnInteger;
        private static SqlDataAdapter _Adapter;
        #endregion

        #region Constructor

        /// <summary>
        /// Private Constructor.
        /// This object cannot be instanciated outside this class.
        /// </summary>
        private DataBaseServiceHandler()
        {
            _connectionstring = getConnectionString();
        }

        /// <summary>
        /// Function creates an instance of the Static class 
        /// for the first time.  But for the subsequent times 
        /// existing instance is returned.
        /// </summary>
        /// <returns>DatabaseServiceHandler</returns>
        public static DataBaseServiceHandler createinstance()
        {
            if (Obj == null)
            {                
                Obj = new DataBaseServiceHandler();
            }            
            return Obj;

        }

        #endregion


        #region "Public Property"

        public static string ConnectionString
        {
            get { return _connectionstring; }
            set { _connectionstring = value; }
        }
	
        #endregion

        #region Private Shared Function
        /// <summary>
        /// Returns the Connection String from the .Config file
        /// </summary>
        /// <returns>String</returns>
        private static string getConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionstring))
                _connectionstring = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            if (string.IsNullOrEmpty(_connectionstring))
                throw new Exception("Unable to Connect to Database.  Invalid Connection String or Empty Connection String");
            return _connectionstring;
        }


        private static SqlCommand GetCommandType(QueryType SQLCommandType)
        {
            switch (SQLCommandType)
            {
                case QueryType.Procedure:
                    return CreateStoredProcedureCommand();
                case QueryType.Text:
                    return CreateTextCommand();
                default:
                    return CreateTextCommand();
            }
        }

        private static SqlCommand CreateStoredProcedureCommand()
        {
            SqlCommand NewCommand = new SqlCommand();
            NewCommand.CommandType = CommandType.StoredProcedure;
            NewCommand.Connection = CreateConnection();
            NewCommand.Connection.ConnectionString = getConnectionString();
            return NewCommand;
        }


        private static SqlCommand CreateTextCommand()
        {
            SqlCommand NewCommand = new SqlCommand();
            NewCommand.CommandType = CommandType.Text; 
            NewCommand.Connection = CreateConnection();
            NewCommand.Connection.ConnectionString = getConnectionString();
            return NewCommand;
        }

        private static SqlConnection CreateConnection()
        {
            return new SqlConnection();
        }

        private static SqlCommand CreateStoredProcedureCommand(SqlConnection dbConnection)
        {
            SqlCommand NewCommand = new SqlCommand();
            NewCommand.CommandType = CommandType.StoredProcedure;
            NewCommand.Connection = dbConnection;
            return NewCommand;
        }

        public static SqlParameter AddParameter<T>(string ParamName, DbType DataType, T Value)
        {
            SqlParameter Param = new SqlParameter();
            Param.DbType = DataType;
            Param.ParameterName = ParamName;
            Param.Value = Value;
            return Param;
        }

        private static SqlDataAdapter CreateAdapter()
        {
            return new SqlDataAdapter();
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// This Function Loads the Datatable from the SQL Command Passed.  This function is exposed if the User wants to Create Connection 
        /// from the Application.  This could be useful in places where users wants handle Transaction from the Application Level
        /// </summary>
        /// <param name="DataTable">The Name of Datatable for Table Mapping.</param>
        /// <param name="fillCommand">SQLCommand Object</param>
        /// <returns></returns>
        public static IDictionary<int, DataTable> LoadFromCommand(DataTable DataTable, System.Data.SqlClient.SqlCommand fillCommand)
        {
            try
            {
                _Adapter = CreateAdapter();
                _Adapter.SelectCommand = fillCommand;
                ReturnInteger = _Adapter.Fill(DataTable);
            }
            finally
            {
                _Adapter.Dispose();
            }
            IDictionary<int, DataTable> ReturnType = new Dictionary<int, DataTable>();
            ReturnType.Add(ReturnInteger, DataTable);
            return ReturnType;
        }

        public static IDictionary<int, DataSet> LoadFromCommand(DataSet dataSetFill, System.Data.SqlClient.SqlCommand fillCommand)
        {
            try
            {
                _Adapter = CreateAdapter();
                _Adapter.SelectCommand = fillCommand;
                ReturnInteger = _Adapter.Fill(dataSetFill);
            }
            finally
            {
                _Adapter.Dispose();
            }
            IDictionary<int, DataSet> ReturnType = new Dictionary<int, DataSet>();
            ReturnType.Add(ReturnInteger, dataSetFill);
            return ReturnType;
        }

        public static IDictionary<int, DataSet> LoadFromCommand(DataSet dataSetFill, string DataTableName, System.Data.SqlClient.SqlCommand fillCommand)
        {
            try
            {
                _Adapter = CreateAdapter();
                _Adapter.SelectCommand = fillCommand;
                ReturnInteger = _Adapter.Fill(dataSetFill, DataTableName);
            }
            finally
            {
                _Adapter.Dispose();
            }
            IDictionary<int, DataSet> ReturnType = new Dictionary<int, DataSet>();
            ReturnType.Add(ReturnInteger, dataSetFill);
            return ReturnType;
        }

        public static IDictionary<int, DataTable> Fill(QueryType SQLCommandDataType, string Query, DataTable DataSetFill, params SqlParameter[] Param)
        {
            try
            {
                FillCommand = GetCommandType(SQLCommandDataType);
                FillCommand.CommandText = Query;
                FillCommand.Connection.Open();
                FillCommand.Parameters.AddRange(Param);
                return LoadFromCommand(DataSetFill, FillCommand);
            }
            finally
            {
                FillCommand.Connection.Close();
                FillCommand.Dispose();
            }
        }

        public static IDictionary<int, DataSet> Fill(QueryType SQLCommandDataType, string Query, DataSet DataSetFill, params SqlParameter[] Param)
        {
            try
            {

                FillCommand = GetCommandType(SQLCommandDataType);
                FillCommand.CommandText = Query;
                FillCommand.Connection.Open();
                FillCommand.Parameters.AddRange(Param);
                return LoadFromCommand(DataSetFill, FillCommand);
            }
            finally
            {
                FillCommand.Connection.Close();
                FillCommand.Dispose();
            }
        }

        public static IDictionary<int, DataSet> Fill(QueryType SQLCommandDataType, string Query, DataSet DataSetFill, string DataTableName, params SqlParameter[] Param)
        {
            try
            {
                FillCommand = GetCommandType(SQLCommandDataType);
                FillCommand.CommandText = Query;
                FillCommand.Connection.Open();
                FillCommand.Parameters.AddRange(Param);
                return LoadFromCommand(DataSetFill, DataTableName, FillCommand);
            }
            finally
            {
                FillCommand.Connection.Close();
                FillCommand.Dispose();
            }
        }

        public static int ExecuteNonQuery(QueryType SQLCommandDataType,string Query, params SqlParameter[] ParamArray)
        {
            try
            {
                ExecuteCommand = GetCommandType(SQLCommandDataType);
                ExecuteCommand.CommandText = Query;
                ExecuteCommand.Connection.Open();
                ExecuteCommand.Parameters.AddRange(ParamArray);
                ReturnInteger = ExecuteCommand.ExecuteNonQuery();                
            }
            finally
            {
                ExecuteCommand.Connection.Close();
                ExecuteCommand.Dispose();
            }
            return ReturnInteger;
        }

        public static T ExecuteScalar<T>(QueryType SQLCommandDataType, string Query, params SqlParameter[] ParamArray)
        {
            T ReturnObject;
            try
            {
                ExecuteCommand = GetCommandType(SQLCommandDataType);
                ExecuteCommand.CommandText = Query;
                ExecuteCommand.Connection.Open();
                ExecuteCommand.Parameters.AddRange(ParamArray);
                ReturnObject = (T)ExecuteCommand.ExecuteScalar();
            }
            finally
            {
                ExecuteCommand.Connection.Close();
                ExecuteCommand.Dispose();
            }
            return ReturnObject;
        }

        public static IDictionary<int, DataTable> Fill(SqlCommand Cmd,out DataTable Datatable)
        {
            try
            {
                Datatable = new DataTable();
                Cmd.Connection.Open();
                return LoadFromCommand(Datatable, Cmd);
            }
            finally
            {
                Cmd.Connection.Close();
                Cmd.Dispose();
            }
        }

        public static IDictionary<int, DataSet> Fill(SqlCommand Cmd, out DataSet Dataset)
        {
            try
            {
                Dataset = new DataSet();
                Cmd.Connection.Open();
                return LoadFromCommand(Dataset, Cmd);
            }
            finally
            {
                Cmd.Connection.Close();
                Cmd.Dispose();
            }
        }

        #endregion

    }

    public enum QueryType
    {
        Procedure,
        Text
    }
}
