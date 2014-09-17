using BMC.CoreLib;
using BMC.CoreLib.Data;
using BMC.CoreLib.Diagnostics;
using BMC.SQLite.DBObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BMC.ExComms.Simulator.DataLayer
{
    public class FFTblCardInformation : SQLiteDbTable
    {
        private readonly string QUERY_INSERT_RECORD = string.Empty;
        private readonly string QUERY_GET_ALLRECORDS = string.Empty;
        private readonly string QUERY_GET_PLAYERCARDRECORDS = string.Empty;
        private readonly string QUERY_GET_EMPLOYEECARDRECORDS = string.Empty;
        private readonly string QUERY_UPDATE_IPADDR = string.Empty;
        private readonly string QUERY_UPDATE_RECORD = string.Empty;
        private readonly string QUERY_DELETE_RECORD = string.Empty;

        public FFTblCardInformation(ISQLiteDataManager dataManager, string tableName)
            : base(dataManager, tableName)
        {
            _ddlScript = "CREATE TABLE [" + tableName + "] (" +
                        "    HashCode INT NOT NULL, " +
                        "    CardNo      VARCHAR( 50 ),  " +
                        "    CardType        INTEGER, " +
                        "    GmuRowID        INTEGER" +
                        ");";
            QUERY_INSERT_RECORD = "INSERT INTO " + _name + "(HashCode, CardNo, CardType, GmuRowID) " +
                                    "VALUES(@HashCode, @CardNo, @CardType, @GmuRowID)";
            QUERY_GET_ALLRECORDS = "SELECT * FROM VW_" + tableName + "_GetAllRecords";
            QUERY_GET_PLAYERCARDRECORDS = "SELECT * FROM VW_" + tableName + "_GetPlayerRecords";
            QUERY_GET_EMPLOYEECARDRECORDS = "SELECT * FROM VW_" + tableName + "_GetEmployeeRecords";
            QUERY_UPDATE_IPADDR = "UPDATE " + tableName + " SET CardNo = @CardNo WHERE ROWID = @ROWID";
            QUERY_UPDATE_RECORD = "UPDATE " + tableName + " SET CardNo = @CardNo, " +
                                                            "CardType = @CardType, " +
                                                            "GmuRowID = @GmuRowID, " +
                                                            " WHERE ROWID = @ROWID";
            QUERY_DELETE_RECORD = "DELETE FROM " + tableName + " WHERE ROWID = @ROWID";

            this.Indexes.Add(
                new SQLiteDbIndex(this.DataManager, "IDX_" + tableName + "_HashCode",
                "CREATE INDEX [IDX_" + tableName + "_HashCode] ON [" + tableName + "] ([HashCode] ASC)")
            );
            this.Indexes.Add(
                new SQLiteDbIndex(this.DataManager, "IDX_" + tableName + "_CardNo",
                "CREATE INDEX [IDX_" + tableName + "_CardNo] ON [" + tableName + "] ([CardNo] ASC)")
            );

            this.Views.Add(
                new SQLiteDbView(this.DataManager, "VW_" + tableName + "_GetAllRecords",
                    this.PrepareSelectQuery("_GetAllRecords", string.Empty))
            );
            this.Views.Add(
                new SQLiteDbView(this.DataManager, "VW_" + tableName + "_GetPlayerRecords",
                    this.PrepareSelectQuery("_GetPlayerRecords", " WHERE A.CardType = 0"))
            );
            this.Views.Add(
                new SQLiteDbView(this.DataManager, "VW_" + tableName + "_GetEmployeeRecords",
                    this.PrepareSelectQuery("_GetEmployeeRecords", " WHERE A.CardType = 1"))
            );
        }

        private string PrepareSelectQuery(string queryName, string where)
        {
            return "CREATE VIEW [VW_" + this.Name + queryName + "] AS" +
                        " SELECT A.RowId, " +
                        "        A.HashCode, " +
                        "        A.CardNo, " +
                        "        A.CardType, " +
                        "        A.GmuRowID" +
                        " FROM   " + this.Name + " A " +
                        where +
                        " ORDER BY " +
                        "        A.RowId";
        }

        public bool Add(int hashCode)
        {
            return this.Add(hashCode, string.Empty);
        }

        public bool Add(int hashCode, string cardNo)
        {
            return this.Add(hashCode, cardNo, 0);
        }

        public bool Add(int hashCode, string cardNo, int cardType)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Add");
            bool result = false;

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DbParameter[] parameters = db.CreateParametersV(
                                db.CreateParameter("@HashCode", hashCode),
                                db.CreateParameter("@CardNo", cardNo),
                                db.CreateParameter("@CardType", cardType),
                                db.CreateParameter("@GmuRowID", 0)
                            );
                    result = (db.ExecuteNonQuery(CommandType.Text, QUERY_INSERT_RECORD, parameters) > 0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                Log.InfoV(PROC, "Data with Hash code {0:D} was inserted into Card Information ({1}).", hashCode, (result ? "Success" : "Failure"));
            }

            return result;
        }

        public DataTable GetAllRecords()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetAllRecords");
            DataTable result = default(DataTable);

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    result = db.ExecuteDataset(CommandType.Text, QUERY_GET_ALLRECORDS).GetDataTable(0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public DataTable GetRecordsByCardType(int cardType)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetRecordsByCardType");
            DataTable result = default(DataTable);

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    string query = (cardType == 1 ? QUERY_GET_EMPLOYEECARDRECORDS : QUERY_GET_PLAYERCARDRECORDS);
                    result = db.ExecuteDataset(CommandType.Text, query).GetDataTable(0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public bool Update(long rowId, string cardNo, int cardType, int gmuRowId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Add");
            bool result = false;

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DbParameter[] parameters = db.CreateParametersV(
                                db.CreateParameter("@CardNo", cardNo),
                                db.CreateParameter("@CardType", cardType),
                                db.CreateParameter("@GmuRowID", gmuRowId),
                                db.CreateParameter("@ROWID", rowId)
                            );
                    result = (db.ExecuteNonQuery(CommandType.Text, QUERY_UPDATE_RECORD, parameters) > 0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                Log.InfoV(PROC, "Data with row id {0:D} was updated for all fields ({1}).", rowId, (result ? "Success" : "Failure"));
            }

            return result;
        }

        public bool UpdateCardNo(long rowId, string cardNo)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateCardNo");
            bool result = default(bool);

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DbParameter[] parameters = db.CreateParametersV(
                            db.CreateParameter("@CardNo", cardNo),
                            db.CreateParameter("@ROWID", rowId)
                        );
                    result = (db.ExecuteNonQuery(CommandType.Text, QUERY_UPDATE_IPADDR, parameters) > 0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                Log.InfoV(PROC, "Data with row id {0:D} was updated for Card No ({1}).", rowId, (result ? "Success" : "Failure"));
            }

            return result;
        }

        public bool Delete(long rowId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Delete");
            bool result = default(bool);

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DbParameter[] parameters = db.CreateParameters(1);
                    parameters[0] = db.CreateParameter("@ROWID", rowId);
                    result = (db.ExecuteNonQuery(CommandType.Text, QUERY_DELETE_RECORD, parameters) > 0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                Log.InfoV(PROC, "Data with row id {0:D} was deleted ({1}).", rowId, (result ? "Success" : "Failure"));
            }

            return result;
        }
    }
}
