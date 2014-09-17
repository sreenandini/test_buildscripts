using BMC.CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Data;
using System.Data;

namespace BMC.SQLite.DBObjects
{
    /// <summary>
    /// SQLite Database Object Interface
    /// </summary>
    public interface ISQLiteDbObject : IDisposable
    {
        /// <summary>
        /// Gets or sets the data manager.
        /// </summary>
        /// <value>
        /// The data manager.
        /// </value>
        ISQLiteDataManager DataManager { get; set; }

        /// <summary>
        /// Gets the name of the database object.
        /// </summary>
        /// <value>
        /// The name of the database object.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the Data Definition Langugage script.
        /// </summary>
        /// <value>
        /// The Data Definition Langugage script.
        /// </value>
        string DDLScript { get; }
    }

    /// <summary>
    /// SQLite Database Table Interface
    /// </summary>
    public interface ISQLiteDbTable : ISQLiteDbObject
    {
        /// <summary>
        /// Gets the collection of indexes.
        /// </summary>
        /// <value>
        /// The collection of indexes.
        /// </value>
        IList<ISQLiteDbIndex> Indexes { get; }

        /// <summary>
        /// Gets the collection of views.
        /// </summary>
        /// <value>
        /// The collection of views.
        /// </value>
        IList<ISQLiteDbView> Views { get; }
    }

    /// <summary>
    /// SQLite Database Index Interface
    /// </summary>
    public interface ISQLiteDbIndex : ISQLiteDbObject { }

    /// <summary>
    /// SQLite Database View Interface
    /// </summary>
    public interface ISQLiteDbView : ISQLiteDbObject { }

    /// <summary>
    /// SQLite Database Object Base Class
    /// </summary>
    public abstract class SQLiteDbObjectBase : DisposableObject, ISQLiteDbObject
    {
        protected string _name = string.Empty;
        protected string _ddlScript = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbObjectBase"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        public SQLiteDbObjectBase(ISQLiteDataManager dataManager)
        {
            this.DataManager = dataManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbObjectBase"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="name">The name.</param>
        public SQLiteDbObjectBase(ISQLiteDataManager dataManager, string name)
            : this(dataManager)
        {
            _name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbObjectBase"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="name">The name.</param>
        /// <param name="ddlScript">The DDL script.</param>
        public SQLiteDbObjectBase(ISQLiteDataManager dataManager, string name, string ddlScript)
            : this(dataManager)
        {
            _name = name;
            _ddlScript = ddlScript;
        }

        /// <summary>
        /// Gets the name of the database object.
        /// </summary>
        /// <value>
        /// The name of the database object.
        /// </value>
        public string Name { get { return _name; } }

        /// <summary>
        /// Gets the Data Definition Langugage script.
        /// </summary>
        /// <value>
        /// The Data Definition Langugage script.
        /// </value>
        public string DDLScript { get { return _ddlScript; } }

        /// <summary>
        /// Gets or sets the data manager.
        /// </summary>
        /// <value>
        /// The data manager.
        /// </value>
        public ISQLiteDataManager DataManager { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }
    }

    /// <summary>
    /// SQLite Database Table Class
    /// </summary>
    public class SQLiteDbTable : SQLiteDbObjectBase, ISQLiteDbTable
    {
        private string QUERY_SELECT_MAX_ROWID = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbTable"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        public SQLiteDbTable(ISQLiteDataManager dataManager)
            : base(dataManager) { this.Initialize(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbTable"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="name">The name.</param>
        public SQLiteDbTable(ISQLiteDataManager dataManager, string name)
            : base(dataManager, name) { this.Initialize(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbTable"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="name">The name.</param>
        /// <param name="ddlScript">The DDL script.</param>
        public SQLiteDbTable(ISQLiteDataManager dataManager, string name, string ddlScript)
            : base(dataManager, name, ddlScript) { this.Initialize(); }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.Indexes = new List<ISQLiteDbIndex>();
            this.Views = new List<ISQLiteDbView>();
            QUERY_SELECT_MAX_ROWID = "SELECT MAX(ROWID) AS MaxRowID FROM " + this.Name;
        }

        /// <summary>
        /// Gets the collection of indexes.
        /// </summary>
        /// <value>
        /// The collection of indexes.
        /// </value>
        public IList<ISQLiteDbIndex> Indexes { get; private set; }

        /// <summary>
        /// Gets the collection of views.
        /// </summary>
        /// <value>
        /// The collection of views.
        /// </value>
        public IList<ISQLiteDbView> Views { get; private set; }

        /// <summary>
        /// Gets the maximum row identifier.
        /// </summary>
        /// <returns>Maximum row identifier</returns>
        public long GetMaxRowID()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetMaxRowID");
            long result = default(long);

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DataRow dr = db.ExecuteDataset(CommandType.Text, QUERY_SELECT_MAX_ROWID).GetDataRow(0, 0);
                    if (dr != null)
                    {
                        if (dr["MaxRowID"] != DBNull.Value)
                        {
                            result = dr.Field<long>("MaxRowID");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }

    /// <summary>
    /// SQLite Database Index Class
    /// </summary>
    public class SQLiteDbIndex : SQLiteDbObjectBase, ISQLiteDbIndex
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbIndex"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        public SQLiteDbIndex(ISQLiteDataManager dataManager)
            : base(dataManager) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbIndex"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="name">The name.</param>
        public SQLiteDbIndex(ISQLiteDataManager dataManager, string name)
            : base(dataManager, name) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbIndex"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="name">The name.</param>
        /// <param name="ddlScript">The DDL script.</param>
        public SQLiteDbIndex(ISQLiteDataManager dataManager, string name, string ddlScript)
            : base(dataManager, name, ddlScript) { }
    }

    /// <summary>
    /// SQLite Database View Class
    /// </summary>
    public class SQLiteDbView : SQLiteDbObjectBase, ISQLiteDbView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbView"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        public SQLiteDbView(ISQLiteDataManager dataManager)
            : base(dataManager) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbView"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="name">The name.</param>
        public SQLiteDbView(ISQLiteDataManager dataManager, string name)
            : base(dataManager, name) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDbView"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="name">The name.</param>
        /// <param name="ddlScript">The DDL script.</param>
        public SQLiteDbView(ISQLiteDataManager dataManager, string name, string ddlScript)
            : base(dataManager, name, ddlScript) { }
    }
}
