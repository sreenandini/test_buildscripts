using BMC.CoreLib;
using BMC.CoreLib.Data;
using BMC.CoreLib.Diagnostics;
using BMC.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BMC.SQLite.DBObjects
{
    /// <summary>
    /// SQLite Data Manager Interface
    /// </summary>
    public interface ISQLiteDataManager : IDisposable
    {
        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        /// <value>
        /// The directory.
        /// </value>
        string Directory { get; }

        /// <summary>
        /// Gets or sets the database path.
        /// </summary>
        /// <value>
        /// The database path.
        /// </value>
        string DatabasePath { get; }

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        string DatabaseName { get; }

        /// <summary>
        /// Gets the collection of tables.
        /// </summary>
        /// <value>
        /// The tables.
        /// </value>
        ISQLiteDbTable[] Tables { get; }

        /// <summary>
        /// Connects to the sqlite database.
        /// </summary>
        /// <returns>Database instance.</returns>
        Database Connect();

        /// <summary>
        /// Executes the query and returns the total rows affected.
        /// </summary>
        /// <param name="query">Query to be executed.</param>
        /// <returns>No of rows affetced.</returns>
        int ExecuteNonQuery(string query);

        /// <summary>
        /// Executes the query and returns the data set.
        /// </summary>
        /// <param name="query">Query to be executed.</param>
        /// <returns>Result set contains the data.</returns>
        DataSet ExecuteDataSet(string query);

        /// <summary>
        /// Executes the query and returns the data table.
        /// </summary>
        /// <param name="query">Query to be executed.</param>
        /// <param name="tableIndex">Index of the table.</param>
        /// <returns>Datatable contains the data</returns>
        DataTable ExecuteDataTable(string query, int tableIndex);

        /// <summary>
        /// Determines whether the given table exists or not.
        /// </summary>
        /// <param name="name">Name of the table.</param>
        /// <returns>Returns True if succeeded; otherwise false.</returns>
        bool IsTableExists(string name);

        /// <summary>
        /// Determines whether the given index exists or not.
        /// </summary>
        /// <param name="name">Name of the index.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>Returns True if succeeded; otherwise false.</returns>
        bool IsIndexExists(string name, string tableName);

        /// <summary>
        /// Determines whether the given view exists or not.
        /// </summary>
        /// <param name="name">Name of the view.</param>
        /// <returns>Returns True if succeeded; otherwise false.</returns>
        bool IsViewExists(string name);
    }

    /// <summary>
    /// SQLite Database Class
    /// </summary>
    public abstract class SQLiteDataManager : DisposableObject, ISQLiteDataManager
    {
        /// <summary>
        /// Database connection string
        /// </summary>
        protected string _connectionString = string.Empty;

        /// <summary>
        /// Full database path
        /// </summary>
        private string _databasePath = string.Empty;

        /// <summary>
        /// The destination directory
        /// </summary>
        private string _directory = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDataManager"/> class.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        protected SQLiteDataManager(string dbName)
            : this(Extensions.GetStartupDirectory(), dbName) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDataManager"/> class.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        protected SQLiteDataManager(string directory, string dbName)
        {
            _directory = directory;
            _databasePath = Path.Combine(_directory, dbName);
            this.DatabaseName = dbName;
        }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        /// <value>
        /// The directory.
        /// </value>
        public string Directory
        {
            get { return _directory; }
        }

        /// <summary>
        /// Gets or sets the database path.
        /// </summary>
        /// <value>
        /// The database path.
        /// </value>
        public string DatabasePath
        {
            get { return _databasePath; }
        }

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName { get; private set; }

        /// <summary>
        /// Gets the collection of tables.
        /// </summary>
        /// <value>
        /// The tables.
        /// </value>
        public abstract ISQLiteDbTable[] Tables { get; }

        /// <summary>
        /// Initializes the database objects.
        /// </summary>
        protected virtual void Initialize()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Initialize");

            try
            {
                _connectionString = String.Format("Data Source=\"{0}\"; Pooling=true; FailIfMissing=false", _databasePath);

                foreach (var dbTable in this.Tables)
                {
                    // tables
                    if (!this.IsTableExists(dbTable.Name))
                    {
                        int result = this.ExecuteNonQuery(dbTable.DDLScript);
                        Log.InfoV(PROC, "Table Creation ({0}) : {1}", dbTable.Name, (result >= 0 ? "SUCCESS" : "FAILURE"));
                    }

                    // indexes
                    foreach (var dbIndex in dbTable.Indexes)
                    {
                        if (!this.IsIndexExists(dbIndex.Name, dbTable.Name))
                        {
                            int result = this.ExecuteNonQuery(dbIndex.DDLScript);
                            Log.InfoV(PROC, "Index Creation ({0}) : {1}", dbIndex.Name, (result >= 0 ? "SUCCESS" : "FAILURE"));
                        }
                    }

                    // views
                    foreach (var dbView in dbTable.Views)
                    {
                        if (!this.IsViewExists(dbView.Name))
                        {
                            int result = this.ExecuteNonQuery(dbView.DDLScript);
                            Log.InfoV(PROC, "View Creation ({0}) : {1}", dbView.Name, (result >= 0 ? "SUCCESS" : "FAILURE"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Connects to the sqlite database.
        /// </summary>
        /// <returns>
        /// Database instance.
        /// </returns>
        public Database Connect()
        {
            return new SQLiteDatabase(_connectionString);
        }

        /// <summary>
        /// Determines whether the given database item exists or not.
        /// </summary>
        /// <param name="query">Query to be executed.</param>
        /// <returns>
        /// Returns True if succeeded; otherwise false.
        /// </returns>
        public bool IsItemExists(string query)
        {
            DataTable dt = this.ExecuteDataTable(query, 0);
            return (dt != null && dt.Rows.Count > 0);
        }

        /// <summary>
        /// Determines whether the given table exists or not.
        /// </summary>
        /// <param name="name">Name of the table.</param>
        /// <returns>
        /// Returns True if succeeded; otherwise false.
        /// </returns>
        public bool IsTableExists(string name)
        {
            return this.IsItemExists(string.Format("SELECT 1 FROM sqlite_master WHERE type='table' AND name='{0}'", name));
        }

        /// <summary>
        /// Determines whether the given index exists or not.
        /// </summary>
        /// <param name="name">Name of the index.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>
        /// Returns True if succeeded; otherwise false.
        /// </returns>
        public bool IsIndexExists(string name, string tableName)
        {
            return this.IsItemExists(string.Format("SELECT 1 FROM sqlite_master WHERE type='index' AND name='{0}' AND tbl_name = '{1}'", name, tableName));
        }

        /// <summary>
        /// Determines whether the given view exists or not.
        /// </summary>
        /// <param name="name">Name of the view.</param>
        /// <returns>
        /// Returns True if succeeded; otherwise false.
        /// </returns>
        public bool IsViewExists(string name)
        {
            return this.IsItemExists(string.Format("SELECT 1 FROM sqlite_master WHERE type='view' AND name='{0}'", name));
        }

        /// <summary>
        /// Executes the query and returns the total rows affected.
        /// </summary>
        /// <param name="query">Query to be executed.</param>
        /// <returns>
        /// No of rows affetced.
        /// </returns>
        public int ExecuteNonQuery(string query)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ExecuteNonQuery");
            int result = 0;

            try
            {
                using (Database db = this.Connect())
                {
                    result = db.ExecuteNonQuery(CommandType.Text, query);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        /// <summary>
        /// Executes the query and returns the data table.
        /// </summary>
        /// <param name="query">Query to be executed.</param>
        /// <param name="tableIndex">Index of the table.</param>
        /// <returns>
        /// Datatable contains the data
        /// </returns>
        public DataTable ExecuteDataTable(string query, int tableIndex)
        {
            DataSet ds = this.ExecuteDataSet(query);
            return ds.GetDataTable(tableIndex);
        }

        /// <summary>
        /// Executes the query and returns the data set.
        /// </summary>
        /// <param name="query">Query to be executed.</param>
        /// <returns>
        /// Result set contains the data.
        /// </returns>
        public DataSet ExecuteDataSet(string query)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "IsTableExists");
            DataSet result = default(DataSet);

            try
            {
                using (Database db = this.Connect())
                {
                    result = db.ExecuteDataset(CommandType.Text, query);
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
