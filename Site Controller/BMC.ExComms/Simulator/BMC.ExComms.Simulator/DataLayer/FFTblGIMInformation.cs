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
    public class FFTblGIMInformation : SQLiteDbTable
    {
        private readonly string QUERY_INSERT_RECORD = string.Empty;
        private readonly string QUERY_GET_ALLRECORDS = string.Empty;
        private readonly string QUERY_UPDATE_IPADDR = string.Empty;
        private readonly string QUERY_UPDATE_RECORD = string.Empty;
        private readonly string QUERY_DELETE_RECORD = string.Empty;

        public FFTblGIMInformation(ISQLiteDataManager dataManager, string tableName)
            : base(dataManager, tableName)
        {
            _ddlScript = "CREATE TABLE [" + tableName + "] (" +
                        "    HashCode INT NOT NULL, " +
                        "    IPAddress      VARCHAR( 20 ),  AssetNo        VARCHAR( 50 )," +
                        "    GmuNo          VARCHAR( 50 ),  SerialNo       VARCHAR( 50 )," +
                        "    ManufacturerID VARCHAR( 50 ),  MACAddress     VARCHAR( 50 )," +
                        "    GmuVersion     VARCHAR( 100 ), SASVersion     VARCHAR( 50 ) " +
                        ");";
            QUERY_INSERT_RECORD = "INSERT INTO " + _name + "(HashCode, IPAddress, AssetNo, GmuNo, SerialNo, ManufacturerID, MACAddress, GmuVersion, SASVersion) " +
                                    "VALUES(@HashCode, @IPAddress, @AssetNo, @GmuNo, @SerialNo, @ManufacturerID, @MACAddress, @GmuVersion, @SASVersion)";
            QUERY_GET_ALLRECORDS = "SELECT * FROM VW_" + tableName + "_GetAllRecords";
            QUERY_UPDATE_IPADDR = "UPDATE " + tableName + " SET IPAddress = @IPAddress WHERE ROWID = @ROWID";
            QUERY_UPDATE_RECORD = "UPDATE " + tableName + " SET IPAddress = @IPAddress, " +
                                                            "AssetNo = @AssetNo, " +
                                                            "GmuNo = @GmuNo, " +
                                                            "SerialNo = @SerialNo, " +
                                                            "ManufacturerID = @ManufacturerID, " +
                                                            "MACAddress = @MACAddress, " +
                                                            "GmuVersion = @GmuVersion, " +
                                                            "SASVersion = @SASVersion " +
                                                            " WHERE ROWID = @ROWID";
            QUERY_DELETE_RECORD = "DELETE FROM " + tableName + " WHERE ROWID = @ROWID";

            this.Indexes.Add(
                new SQLiteDbIndex(this.DataManager, "IDX_" + tableName + "_HashCode",
                "CREATE INDEX [IDX_" + tableName + "_HashCode] ON [" + tableName + "] ([HashCode] ASC)")
            );
            this.Indexes.Add(
                new SQLiteDbIndex(this.DataManager, "IDX_" + tableName + "_IPAddress",
                "CREATE INDEX [IDX_" + tableName + "_IPAddress] ON [" + tableName + "] ([IPAddress] ASC)")
            );

            this.Views.Add(
                new SQLiteDbView(this.DataManager, "VW_" + tableName + "_GetAllRecords",
                    "CREATE VIEW [VW_" + tableName + "_GetAllRecords] AS" +
                        " SELECT A.RowId, " +
                        "        A.HashCode, " +
                        "        A.IPAddress, " +
                        "        A.AssetNo, " +
                        "        A.GmuNo, " +
                        "        A.SerialNo, " +
                        "        A.ManufacturerID, " +
                        "        A.MACAddress, " +
                        "        A.GmuVersion, " +
                        "        A.SASVersion" +
                        " FROM   " + tableName + " A " +
                        " ORDER BY " +
                        "        A.RowId")
            );
        }

        public bool Add(int hashCode)
        {
            return this.Add(hashCode, IPAddress.Loopback.ToString(), string.Empty, string.Empty, string.Empty,
                            string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public bool Add(int hashCode, string ipAddress)
        {
            return this.Add(hashCode, ipAddress, string.Empty, string.Empty, string.Empty,
                            string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public bool Add(int hashCode, string ipAddress, string assetNo, string gmuNo, string serialNo,
                        string manufacturerID, string macAddress, string gmuVersion, string sasVersion)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Add");
            bool result = false;

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DbParameter[] parameters = db.CreateParametersV(
                                db.CreateParameter("@HashCode", hashCode),
                                db.CreateParameter("@IPAddress", ipAddress),
                                db.CreateParameter("@AssetNo", assetNo),
                                db.CreateParameter("@GmuNo", gmuNo),
                                db.CreateParameter("@SerialNo", serialNo),
                                db.CreateParameter("@ManufacturerID", manufacturerID),
                                db.CreateParameter("@MACAddress", macAddress),
                                db.CreateParameter("@GmuVersion", gmuVersion),
                                db.CreateParameter("@SASVersion", sasVersion)
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
                Log.InfoV(PROC, "Data with Hash code {0:D} was inserted into GIM Information ({1}).", hashCode, (result ? "Success" : "Failure"));
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

        public bool Update(long rowId, string ipAddress, string assetNo, string gmuNo, string serialNo,
                            string manufacturerID, string macAddress, string gmuVersion, string sasVersion)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Add");
            bool result = false;

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DbParameter[] parameters = db.CreateParametersV(
                                db.CreateParameter("@IPAddress", ipAddress),
                                db.CreateParameter("@AssetNo", assetNo),
                                db.CreateParameter("@GmuNo", gmuNo),
                                db.CreateParameter("@SerialNo", serialNo),
                                db.CreateParameter("@ManufacturerID", manufacturerID),
                                db.CreateParameter("@MACAddress", macAddress),
                                db.CreateParameter("@GmuVersion", gmuVersion),
                                db.CreateParameter("@SASVersion", sasVersion),
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

        public bool UpdateIPAddress(long rowId, string ipAddress)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateIPAddress");
            bool result = default(bool);

            try
            {
                using (Database db = this.DataManager.Connect())
                {
                    DbParameter[] parameters = db.CreateParametersV(
                            db.CreateParameter("@IPAddress", ipAddress),
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
                Log.InfoV(PROC, "Data with row id {0:D} was updated for IP Address ({1}).", rowId, (result ? "Success" : "Failure"));
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
