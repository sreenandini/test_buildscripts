using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.DataAccess;
using BMC.Common.Utilities;
using System.Data.Linq.Mapping;

namespace BMC.DBInterface.CashDeskOperator
{
    /// <summary>
    /// Data access Methods of hourly screen.
    /// </summary>
    /// <Author>Madhu</Author>
    /// <date>24 aug 09</date>
    public class HourlyDataAccess
    {

        private readonly string Get24HourStatisticsPROC = "rsp_24HourStatisticsByType3";
        private readonly string GetInstallationDetailsPROC = "rsp_getinstallationDetails";
        private readonly string GetHourlyStatisticsTypes = "rsp_GetHourlyStatisticsTypes";
        private readonly string rsp_GetBarPositionsEnrolledOnGamingDay = "rsp_GetBarPositionsEnrolledOnGamingDay";
        private readonly string Get24HourStatisticsByOccupancy = "rsp_24HourStatisticsByOccupancy";
        private DataTable objInstallationDetails;

        public DataTable InstallationDetailsTable { get { return objInstallationDetails; } }
        public HourlyDataAccess()
        {
            if (objInstallationDetails == null || objInstallationDetails.Rows.Count == 0)
            {
                GetInstallationDetailsTable();
            }
        }
        public DataTable GetSiteName()
        {
            SqlCommand objComm = new SqlCommand();
            objComm.CommandText = "select Name from site";
            DataTable dtSite = new DataTable();
            try
            {
                //objComm.Connection = new SqlConnection(DataBaseServiceHandler.ConnectionString);
                //DataBaseServiceHandler.Fill(objComm, out dtSite);
                dtSite = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.Text, "select Name from site", dtSite);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return dtSite;
        }

        /// <summary>
        /// Gets the VTP hourly Data
        /// </summary>
        /// <param name="objHourlyDetails">Hourly Details entity</param>
        /// <returns></returns>
        public DataSet GetHourlyStatistics(string Datatype,int Rows,int StartHour,int Category,int Zone,int Position,DateTime Date)
        {
            DataSet dtHourlyData = new DataSet();
            SqlParameter[] Param = new SqlParameter[7];
            try
            {
                Param[0] = DataBaseServiceHandler.AddParameter<string>("@DataType", DbType.String, Datatype);
                Param[1] = DataBaseServiceHandler.AddParameter<int>("@rows", DbType.Int32, Rows);
                Param[2] = DataBaseServiceHandler.AddParameter<int>("@starthour", DbType.Int32, StartHour);
                Param[3] = Category == 0 ? DataBaseServiceHandler.AddParameter<object>("@category", DbType.Int32, DBNull.Value) : DataBaseServiceHandler.AddParameter<int>("@category", DbType.Int32, Category);
                Param[4] = Zone == 0 ? DataBaseServiceHandler.AddParameter<object>("@zone", DbType.Int32, DBNull.Value) : DataBaseServiceHandler.AddParameter<int>("@zone", DbType.Int32, Zone);
                Param[5] = Position == 0 ? DataBaseServiceHandler.AddParameter<object>("@position", DbType.Int32, DBNull.Value) : DataBaseServiceHandler.AddParameter<int>("@position", DbType.Int32, Position);
                Param[6] = Date == DateTime.MinValue ? DataBaseServiceHandler.AddParameter<object>("@date", DbType.DateTime, DBNull.Value) : DataBaseServiceHandler.AddParameter<DateTime>("@date", DbType.DateTime, Date);

                dtHourlyData = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, Get24HourStatisticsPROC, dtHourlyData, Param);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return dtHourlyData;
        }

        public DataTable GetHourlyStatisticsForOccupancy(Transport.CashDeskOperatorEntity.HourlyDetails objHourlyDetails)
        {
            DataTable dtHourlyData = new DataTable();
            SqlParameter[] Param = new SqlParameter[7];
            try
            {
                Param[0] = DataBaseServiceHandler.AddParameter<string>("@DataType", DbType.String, objHourlyDetails.Datatype);
                Param[1] = DataBaseServiceHandler.AddParameter<int>("@rows", DbType.Int32, objHourlyDetails.Rows);
                Param[2] = DataBaseServiceHandler.AddParameter<int>("@starthour", DbType.Int32, objHourlyDetails.StartHour);
                Param[3] = objHourlyDetails.Category == 0 ? DataBaseServiceHandler.AddParameter<object>("@category", DbType.Int32, DBNull.Value) : DataBaseServiceHandler.AddParameter<int>("@category", DbType.Int32, objHourlyDetails.Category);
                Param[4] = objHourlyDetails.Zone == 0 ? DataBaseServiceHandler.AddParameter<object>("@zone", DbType.Int32, DBNull.Value) : DataBaseServiceHandler.AddParameter<int>("@zone", DbType.Int32, objHourlyDetails.Zone);
                Param[5] = objHourlyDetails.Position == 0 ? DataBaseServiceHandler.AddParameter<object>("@position", DbType.Int32, DBNull.Value) : DataBaseServiceHandler.AddParameter<int>("@position", DbType.Int32, objHourlyDetails.Position);
                Param[6] = objHourlyDetails.Date == DateTime.MinValue ? DataBaseServiceHandler.AddParameter<object>("@date", DbType.DateTime, DBNull.Value) : DataBaseServiceHandler.AddParameter<DateTime>("@date", DbType.DateTime, objHourlyDetails.Date);

                dtHourlyData = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, Get24HourStatisticsByOccupancy, dtHourlyData, Param);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return dtHourlyData;
        }

        private void GetInstallationDetailsTable()
        {
            CommonDataAccess commonDataAccess = new CommonDataAccess();
            try
            {
                objInstallationDetails = CommonDataAccess.GetInstallationDetailsForHourly();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        public DataTable FillMachineTypes()
        {
            return FillData("Machine_Type_ID", "Machine_Type_Code");
        }

        public DataTable FillZones()
        {
            return FillData("Zone_No", "Zone_Name");
        }
        public DataTable FillPositions()
        {
            return FillData("Bar_Pos_No", "Bar_Pos_Name");
        }

        public DataTable FillHSTypes()
        {
            DataTable dtHSTypes = new DataTable();
            SqlParameter[] Param = new SqlParameter[1];
            try
            {
                if (ExtensionMethods.CurrentSiteCulture.ToUpper().Contains("IT"))
                    Param[0] = DataBaseServiceHandler.AddParameter<string>("@Language", DbType.String, "it-IT");
                else
                    Param[0] = DataBaseServiceHandler.AddParameter<string>("@Language", DbType.String, "en-US");

                dtHSTypes = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, GetHourlyStatisticsTypes, dtHSTypes, Param);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return dtHSTypes;
        }

        private DataTable FillData(string ValueFieldName, string TextFieldName)
        {
            DataTable dtFillData = new DataTable();
            dtFillData.Columns.Add("ID");
            dtFillData.Columns.Add("Name");
            DataRow objRow;
            try
            {
                var DistinctNames = (from row in objInstallationDetails.AsEnumerable()
                                     select new { ID = row[ValueFieldName], Name = row[TextFieldName] }).Distinct();
                foreach (var item in DistinctNames)
                {
                    if ((int)item.ID != 0)
                    {
                        objRow = dtFillData.NewRow();
                        objRow["ID"] = item.ID;
                        objRow["Name"] = item.Name;
                        dtFillData.Rows.Add(objRow);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return dtFillData;
        }

        public DataTable GetBarPositionsEnrolledOnGamingDay(DateTime Date, int StartHour)
        {
            DataTable dtHourlyData = new DataTable();
            SqlParameter[] Param = new SqlParameter[2];
            try
            {
                Param[0] = DataBaseServiceHandler.AddParameter<DateTime>("@date", DbType.DateTime, Date);
                Param[1] = DataBaseServiceHandler.AddParameter<int>("@starthour", DbType.Int32, StartHour);

                dtHourlyData = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, rsp_GetBarPositionsEnrolledOnGamingDay, dtHourlyData, Param);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtHourlyData;
        }

        public double GetOperationalHours(DateTime? date)
        {
            double? result = 0;
            using (LinqDataAccessDataContext context = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString))
                context.GetOperationalHours(date, ref result);
            return Convert.ToDouble(result);
        }
    }
}
