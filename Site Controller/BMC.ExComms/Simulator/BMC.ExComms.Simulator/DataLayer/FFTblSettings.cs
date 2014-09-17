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
    public class FFTblSettings : SQLiteDbTable
    {
        private readonly string QUERY_INSERT_RECORD = string.Empty;
        private readonly string QUERY_GET_ALLRECORDS = string.Empty;
        private readonly string QUERY_UPDATE_VALUE = string.Empty;
        private readonly string QUERY_UPDATE_RECORD = string.Empty;
        private readonly string QUERY_DELETE_RECORD = string.Empty;

        public FFTblSettings(ISQLiteDataManager dataManager, string tableName)
            : base(dataManager, tableName)
        {
            _ddlScript = "CREATE TABLE [" + tableName + "] (" +
                        "    SettingName      VARCHAR( 255 ),  SettingValue        VARCHAR( 255 )" +
                        ");";
            QUERY_INSERT_RECORD = "INSERT INTO " + _name + "(SettingName, SettingValue) " +
                                    "SELECT @SettingName, @SettingValue " +
                                    "WHERE NOT EXISTS (SELECT 1 FROM " + tableName +
                                    " WHERE SettingName = @SettingName)";
            QUERY_GET_ALLRECORDS = "SELECT * FROM VW_" + tableName + "_GetAllRecords";
            QUERY_UPDATE_VALUE = "UPDATE " + tableName + " SET SettingValue = @SettingValue WHERE ROWID = @ROWID";
            QUERY_UPDATE_RECORD = "UPDATE " + tableName + " SET SettingName = @SettingName, " +
                                                            "SettingValue = @SettingValue " +
                                                            " WHERE ROWID = @ROWID";
            QUERY_DELETE_RECORD = "DELETE FROM " + tableName + " WHERE ROWID = @ROWID";

            this.Indexes.Add(
                new SQLiteDbIndex(this.DataManager, "IDX_" + tableName + "_SettingName",
                "CREATE INDEX [IDX_" + tableName + "_SettingName] ON [" + tableName + "] ([SettingName] ASC)")
            );

            this.Views.Add(
                new SQLiteDbView(this.DataManager, "VW_" + tableName + "_GetAllRecords",
                    "CREATE VIEW [VW_" + tableName + "_GetAllRecords] AS" +
                        " SELECT A.RowId, " +
                        "        A.SettingName, " +
                        "        A.SettingValue " +
                        " FROM   " + tableName + " A " +
                        " ORDER BY " +
                        "        A.RowId")
            );
        }

        public bool Add(string settingName, string settingValue)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddIfNotExists");
            bool result = false;

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DbParameter[] parameters = db.CreateParametersV(
                                db.CreateParameter("@SettingName", settingName),
                                db.CreateParameter("@SettingValue", settingValue)
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
                if (result)
                    Log.InfoV(PROC, "Data with Hash code {0} was inserted into GIM Information ({1}).", settingName, (result ? "Success" : "Failure"));
            }

            return result;
        }

        public DataTable GetAllRecords()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetAllRecords");
            DataTable result = default(DataTable);

            try
            {
                result = this.DataManager.ExecuteDataTable(QUERY_GET_ALLRECORDS, 0);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public bool Update(long rowId, string settingName, string settingValue)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Add");
            bool result = false;

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DbParameter[] parameters = db.CreateParametersV(
                                db.CreateParameter("@SettingName", settingName),
                                db.CreateParameter("@SettingValue", settingValue),
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

        public bool UpdateValue(long rowId, string settingValue)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateIPAddress");
            bool result = default(bool);

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DbParameter[] parameters = db.CreateParametersV(
                            db.CreateParameter("@SettingValue", settingValue),
                            db.CreateParameter("@ROWID", rowId)
                        );
                    result = (db.ExecuteNonQuery(CommandType.Text, QUERY_UPDATE_VALUE, parameters) > 0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                Log.InfoV(PROC, "Data with row id {0:D} was updated for setting value ({1}).", rowId, (result ? "Success" : "Failure"));
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
