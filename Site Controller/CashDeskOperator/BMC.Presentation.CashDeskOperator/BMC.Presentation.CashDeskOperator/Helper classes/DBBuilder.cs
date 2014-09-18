using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;

namespace BMC.Presentation.Helper_classes
{
    class DBBuilder
    {
        /// <summary>
        /// Private Variable Declaration.  
        /// All objects declared here are 
        /// Static (only one instance of 
        /// these objects are created 
        /// across the application)
        /// </summary>
        #region Private Variable
        private static DBBuilder Obj;
        private static SqlCommand FillCommand;
        private static SqlCommand ExecuteCommand;
        private static string _connectionstring;
        private static int ReturnInteger;
        private static SqlDataAdapter _Adapter;
        private static string strConnectionString = string.Empty;
        #endregion

        #region Constructor

        /// <summary>
        /// Private Constructor.
        /// This object cannot be instanciated outside this class.
        /// </summary>
        private DBBuilder()
        {
            _connectionstring = getConnectionString();
        }

        /// <summary>
        /// Function creates an instance of the Static class 
        /// for the first time.  But for the subsequent times 
        /// existing instance is returned.
        /// </summary>
        /// <returns>DatabaseServiceHandler</returns>
        public static DBBuilder createinstance()
        {
            if (Obj == null)
            {
                Obj = new DBBuilder();
            }

            return Obj;

        }

        #endregion

        #region Private Shared Function
        /// <summary>
        /// Returns the Connection String from the .Config file
        /// </summary>
        /// <returns>String</returns>
        private static string getConnectionString()
        {
            _connectionstring = GetExchangeConnectionString();
            return _connectionstring;
        }


        public static SqlCommand CreateStoredProcedureCommand()
        {
            SqlCommand NewCommand = new SqlCommand();
            NewCommand.CommandType = CommandType.StoredProcedure;
            NewCommand.Connection = CreateConnection();
            NewCommand.Connection.ConnectionString = getConnectionString();
            return NewCommand;
        }


        public static SqlCommand CreateTextCommand()
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

        public static SqlParameter AddOutputParameter<T>(string ParamName, DbType DataType, T Value)
        {
            SqlParameter Param = new SqlParameter();
            Param.DbType = DataType;
            Param.ParameterName = ParamName;
            Param.Direction = ParameterDirection.Output;
            Param.Value = Value;
            return Param;
        }

        private static SqlDataAdapter CreateAdapter()
        {
            return new SqlDataAdapter();
        }

        #endregion

        #region Public Functions

        public static Dictionary<int, DataTable> LoadFromCommand(DataTable dataSetFill, System.Data.SqlClient.SqlCommand fillCommand)
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
            Dictionary<int, DataTable> ReturnType = new Dictionary<int, DataTable>();
            ReturnType.Add(ReturnInteger, dataSetFill);
            return ReturnType;
        }

        public static Dictionary<int, DataSet> LoadFromCommandToDataSet(DataSet dataSetFill, System.Data.SqlClient.SqlCommand fillCommand)
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
            Dictionary<int, DataSet> ReturnType = new Dictionary<int, DataSet>();
            ReturnType.Add(ReturnInteger, dataSetFill);
            return ReturnType;
        }


        public static Dictionary<int, DataSet> LoadFromCommandToDataSet(DataSet dataSetFill, string DataTableName, System.Data.SqlClient.SqlCommand fillCommand)
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
            Dictionary<int, DataSet> ReturnType = new Dictionary<int, DataSet>();
            ReturnType.Add(ReturnInteger, dataSetFill);
            return ReturnType;
        }

        public static Dictionary<int, DataTable> Fill(string Procedure)
        {
            try
            {
                DataTable DatasetFill = new DataTable();
                FillCommand = CreateStoredProcedureCommand();
                FillCommand.Connection.Open();
                FillCommand.CommandText = Procedure;
                return LoadFromCommand(DatasetFill, FillCommand);
            }
            finally
            {
                FillCommand.Connection.Close();
                FillCommand.Dispose();
            }
        }

        public static Dictionary<int, DataTable> FillByParameters(string Procedure, params SqlParameter[] Param)
        {
            try
            {
                DataTable DatasetFill = new DataTable();
                FillCommand = CreateStoredProcedureCommand();
                FillCommand.CommandText = Procedure;
                FillCommand.Connection.Open();
                FillCommand.Parameters.AddRange(Param);
                return LoadFromCommand(DatasetFill, FillCommand);
            }
            finally
            {
                FillCommand.Connection.Close();
                FillCommand.Dispose();
            }
        }

        public static Dictionary<int, DataTable> FillByParameters(string Procedure, DataTable DataSetFill, params SqlParameter[] Param)
        {
            try
            {
                FillCommand = CreateStoredProcedureCommand();
                FillCommand.CommandText = Procedure;
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

        public static string GetExchangeConnectionString()
        {          
            if (strConnectionString == string.Empty)
                strConnectionString = BMC.DBInterface.CashDeskOperator.DBBuilder.GetExchangeConnectionString();
            return strConnectionString;   
        }

        public static Dictionary<int, DataTable> FillByParametersByText(string SQLQuery, DataTable DataSetFill)
        {
            try
            {
                FillCommand = CreateTextCommand();
                FillCommand.Connection = CreateConnection();
                FillCommand.Connection.ConnectionString = getConnectionString();
                FillCommand.CommandText = SQLQuery;
                FillCommand.Connection.Open();
                return LoadFromCommand(DataSetFill, FillCommand);
            }
            finally
            {
                FillCommand.Connection.Close();
                FillCommand.Dispose();
            }
        }

        public static Dictionary<int, DataSet> FillByParametersForDataset(string Procedure, params SqlParameter[] Param)
        {
            try
            {
                DataSet DatasetFill = new DataSet();
                FillCommand = CreateStoredProcedureCommand();
                FillCommand.CommandText = Procedure;
                FillCommand.Connection.Open();
                FillCommand.Parameters.AddRange(Param);
                return LoadFromCommandToDataSet(DatasetFill, FillCommand);
            }
            finally
            {
                FillCommand.Connection.Close();
                FillCommand.Dispose();
            }
        }

        public static Dictionary<int, DataSet> FillByParametersForDataset(string Procedure, DataSet DataSetFill, params SqlParameter[] Param)
        {
            try
            {

                FillCommand = CreateStoredProcedureCommand();
                FillCommand.CommandText = Procedure;
                FillCommand.Connection.Open();
                FillCommand.Parameters.AddRange(Param);
                return LoadFromCommandToDataSet(DataSetFill, FillCommand);
            }
            finally
            {
                FillCommand.Connection.Close();
                FillCommand.Dispose();
            }
        }

        public static Dictionary<int, DataSet> FillByParametersForDataset(string Procedure, DataSet DataSetFill, string DataTableName, params SqlParameter[] Param)
        {
            try
            {

                FillCommand = CreateStoredProcedureCommand();
                FillCommand.CommandText = Procedure;
                FillCommand.Connection.Open();
                FillCommand.Parameters.AddRange(Param);
                return LoadFromCommandToDataSet(DataSetFill, DataTableName, FillCommand);
            }
            finally
            {
                FillCommand.Connection.Close();
                FillCommand.Dispose();
            }
        }


        public static int ExecuteNonQuery(string Procedure, params SqlParameter[] ParamArray)
        {
            try
            {
                ExecuteCommand = CreateStoredProcedureCommand();
                ExecuteCommand.CommandText = Procedure;
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

        public static int ExecuteNonQueryWithoutParameters(string Procedure)
        {
            try
            {
                ExecuteCommand = CreateStoredProcedureCommand();
                ExecuteCommand.CommandText = Procedure;
                ExecuteCommand.Connection.Open();
                ReturnInteger = ExecuteCommand.ExecuteNonQuery();
            }
            finally
            {
                ExecuteCommand.Connection.Close();
                ExecuteCommand.Dispose();
            }
            return ReturnInteger;
        }

        public static Dictionary<int, DataTable> FillCommandDatatable(SqlCommand Cmd)
        {
            try
            {
                DataTable DatasetFill = new DataTable();
                Cmd.Connection.Open();
                return LoadFromCommand(DatasetFill, Cmd);
            }
            finally
            {
                Cmd.Connection.Close();
                Cmd.Dispose();
            }
        }

        public static Dictionary<int, DataSet> FillCommandDataSet(SqlCommand Cmd)
        {
            try
            {
                DataSet DatasetFill = new DataSet();
                Cmd.Connection.Open();
                return LoadFromCommandToDataSet(DatasetFill, Cmd);
            }
            finally
            {
                Cmd.Connection.Close();
                Cmd.Dispose();
            }
        }


        #endregion

    }
}

