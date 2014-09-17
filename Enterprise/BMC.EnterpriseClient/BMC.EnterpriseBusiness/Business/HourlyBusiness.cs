using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using System.Data.Linq;
using BMC.CoreLib.Data;
using System.Data;
using System.Data.Common;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Collections;
using System.Globalization;
using BMC.CoreLib.Win32;
using System.Threading;
using BMC.Common;
using System.Text.RegularExpressions;

namespace BMC.EnterpriseBusiness.Business
{
    public class HourlyBusiness : DisposableObject
    {
        public HourlyBusiness() { }

        #region Internal Classes
        internal class HourlyDetailModel : DisposableObject
        {
            private IDictionary<int, double> _values = null;

            internal HourlyDetailModel()
            {
                _values = new SortedDictionary<int, double>();
            }

            public HourlyDetailEntity Detail { get; set; }

            public double this[int index]
            {
                get
                {
                    if (_values.ContainsKey(index))
                    {
                        return _values[index];
                    }
                    else
                    {
                        return 0;
                    }
                }
                set
                {
                    if (_values.ContainsKey(index))
                    {
                        _values[index] = value;
                    }
                    else
                    {
                        _values.Add(index, value);
                    }
                }
            }

            public bool IsExists(int index)
            {
                return _values.ContainsKey(index);
            }
        }

        internal class HourlyDetailCollectionModel : Dictionary<KeyValuePair<DateTime, string>, HourlyDetailModel>
        {
            public HourlyDetailCollectionModel() { }
        }
        #endregion

        private void GetBarPositionsEnrolledOnGamingDay(ICollection<string> barPositions, DateTime date, int startHour)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetBarPositionsEnrolledOnGamingDay");

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetBarPositionsEnrolledOnGamingDayResult> dbResults = db.rsp_GetBarPositionsEnrolledOnGamingDay(date, startHour);
                    if (dbResults != null)
                    {
                        foreach (var dbResult in dbResults)
                        {
                            barPositions.Add(dbResult.Bar_Position_Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public HourlyDetailsEntity GetDetails(IAsyncProgress2 ap, int startHour, int rows, string dataType, int? category,
            int? zone, int? position, DateTime? date, int? site, bool isCalenderDay)
        {
            //HourlyDetailsEntity result = new HourlyDetailsEntity();
            //HourlyDetailEntity total = new HourlyDetailEntity();
            HourlyDetailsEntity[] resultSet = null;
            HourlyDetailCollectionModel mappings = new HourlyDetailCollectionModel();
            ICollection<string> barPositions = new Rbtree<string>();
            bool isDetails = date.IsValid();
            bool isOccupancy = dataType.IgnoreCaseCompare("OCCUPANCY(%)");
            bool isOccupancyForPosition = isOccupancy && (position.SafeValue() != 0);
            bool isOccupancyForNonPosition = isOccupancy && (position.SafeValue() == 0);
            int displayedPositions = 1;

            try
            {
                // bar positions enrolled on gaming day
                if (isDetails)
                {
                    this.GetBarPositionsEnrolledOnGamingDay(barPositions, date.SafeValue(), startHour);
                }

                // actual work
                Action<DateTime, DataRow, HourlyDetailEntity> actualWork = (curDay, dbResult, dto) =>
                {
                    // retrieves the current day values from the mappings
                    KeyValuePair<DateTime, string> pairCurDay = new KeyValuePair<DateTime, string>(curDay, dto.Bar_Position_Name);
                    HourlyDetailModel curDayDetail = null;
                    if (mappings.ContainsKey(pairCurDay))
                    {
                        curDayDetail = mappings[pairCurDay];
                    }

                    // shift the current day values
                    for (int index = 0; index < (24 - startHour); index++)
                    {
                        int shiftIndex = (index + startHour);

                        if (dbResult != null)
                        {
                            //  6   =>      0,   7  =>      1,   8    =>     2,  9    =>     3, 10    =>     4,  11    =>    5
                            // 12   =>      6,  13  =>      7,  14    =>     8, 15    =>     9, 16    =>    10,  17    =>   11
                            // 18   =>     12,  19  =>     13,  20    =>    14, 21    =>    15, 22    =>    16,  23    =>   17
                            dto[index] = TypeSystem.GetValueDouble(dbResult[string.Format("HS_Hour{0:D}_Value", (shiftIndex + 1))]);
                        }
                        else
                        {
                            dto[index] = dto[shiftIndex];
                        }
                    }

                    // get the 0 - {START HOUR - 1} values from the previous day
                    for (int index = 0; index < startHour; index++)
                    {
                        int shiftIndexMap = ((24 - startHour) + index);

                        // gets the value from previous day
                        if (curDayDetail != null &&
                            curDayDetail.IsExists(index))
                        {
                            dto[shiftIndexMap] = curDayDetail[index];
                        }
                        else
                        {
                            dto[shiftIndexMap] = 0.0;
                        }
                    }

                    // remove the current day from mapping
                    if (mappings.ContainsKey(pairCurDay))
                    {
                        mappings.Remove(pairCurDay);
                    }
                };

                ap.UpdateStatus(BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_FetchHourly"));
                DataSet ds;
                using (Database db = DbFactory.OpenDB(EnterpriseDataContextHelper.ConnectionString))
                {
                    db.Open();
                    DbParameter[] dbp = db.CreateParameters(8);
                    dbp[0] = db.CreateParameter("@starthour", startHour);
                    dbp[1] = db.CreateParameter("@rows", rows);
                    dbp[2] = db.CreateParameter("@dataType", dataType);
                    dbp[3] = db.CreateParameter("@category", category);
                    dbp[4] = db.CreateParameter("@zone", zone);
                    dbp[5] = db.CreateParameter("@position", position);
                    dbp[6] = db.CreateParameter("@date", date);
                    dbp[7] = db.CreateParameter("@site", site);
                    ds = db.ExecuteDataset("dbo.rsp_24HourStatisticsByType3", dbp);
                }
                if (ds.Tables.Count > 0)
                {
                    //DataTable dt = ds.GetDataTable(0);
                    int length = 0;
                    if (dataType.Equals("AVG_BET") && ds.Tables.Count == 2)
                    {
                        length = ds.Tables.Count;
                    }
                    else
                    {
                        length = 1;
                    }
                    resultSet = new HourlyDetailsEntity[length];
                    for (int counter = 0; counter < length; counter++)
                    {
                        resultSet[counter] = new HourlyDetailsEntity();
                        HourlyDetailEntity total = new HourlyDetailEntity();

                        DataTable dt = ds.GetDataTable(counter);

                        // only for occupancy


                        if (dt.Rows.Count > 0)
                        {
                            int count2 = dt.Rows.Count;
                            ap.InitializeProgress(1, dt.Rows.Count);
                            int proIdx = 0;
                            DateTime oldDate = DateTime.MinValue;

                            foreach (DataRow dr in dt.Rows)
                            {
                                HourlyDetailEntity r = new HourlyDetailEntity();
                                r.ID = dr.Field<int>("ID");
                                r.Date = dr.Field<DateTime>("Date");
                                r.Bar_Position_Name = dr.Field<string>("Bar_Position_Name");
                                r.Machine_Name = dr.Field<string>("Machine_Name");
                                r.Machine_Category = dr.Field<string>("Machine_Category");
                                r.Stock = dr.Field<string>("Stock_No");
                                if (oldDate != r.Date)
                                {
                                    ++proIdx;
                                    int proPerc = (int)(((float)proIdx / (float)count2) * 100.0);
                                    ap.UpdateStatusProgress(proIdx - 1, "Processing hourly details for date [" +
                                                         r.Date.ToString("dd/MM/yyyy") + "] . . . : " +
                                                        proIdx + " of " + count2.ToString() +
                                                        " (" + proPerc + "%)");
                                    oldDate = r.Date;
                                }

                                // set the db values
                                for (int i = 0; i < 24; i++)
                                {
                                    string hourColumn = string.Format("HS_Hour{0:D}_Value", (i + 1));
                                    if (!isOccupancy)
                                    {
                                        if (isCalenderDay)
                                            r[i] = TypeSystem.GetValueDouble(dr[hourColumn]);
                                        else
                                            r[i] = 0.0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            r.OccupancyHour = Math.Max(1, dr.Field<int>("OccupancyHour"));
                                        }
                                        catch { r.OccupancyHour = 1; }
                                        try
                                        {
                                            r.SiteOpeningHour = Math.Max(1, dr.Field<int>("SiteOpeningHour"));
                                        }
                                        catch { r.SiteOpeningHour = 1; }

                                        // get the db values
                                        if (isCalenderDay)
                                            r[i] = TypeSystem.GetValueDouble(dr[hourColumn]);
                                        else
                                            r[i] = 0.0;
                                    }
                                }

                                // shift the hour values only for gaming day
                                if (!isCalenderDay)
                                {
                                    // store the previous day values
                                    DateTime curDay = r.Date;
                                    DateTime prevDay = curDay.AddDays(-1);
                                    KeyValuePair<DateTime, string> pairPrevDay = new KeyValuePair<DateTime, string>(prevDay, r.Bar_Position_Name);
                                    if (!mappings.ContainsKey(pairPrevDay))
                                    {
                                        // stores the previous day values into the mappings
                                        HourlyDetailModel prevDayDetail = new HourlyDetailModel()
                                        {
                                            Detail = r
                                        };

                                        // shift the 0 - starthour values to previous day
                                        for (int i = 0; i < startHour; i++)
                                        {
                                            prevDayDetail[i] = TypeSystem.GetValueDouble(dr[string.Format("HS_Hour{0:D}_Value", (i + 1))]);
                                        }

                                        // mappings
                                        mappings.Add(pairPrevDay, prevDayDetail);
                                    }

                                    // retrieves the current day values from the mappings
                                    actualWork(curDay, dr, r);
                                }

                                // adds to the collection
                                resultSet[counter].Add(r);
                            }
                        }



                        // missing current day values for the previous day if the barpositions are enrolled on gaming day
                        if (mappings.Count > 0)
                        {
                            foreach (var mapping in (from m in mappings.Values.OfType<HourlyDetailModel>()
                                                     join j in barPositions.OfType<string>()
                                                     on m.Detail.Bar_Position_Name equals j
                                                     select m))
                            {
                                HourlyDetailEntity prevDto = mapping.Detail;
                                DateTime curDay = prevDto.Date.AddDays(-1);

                                HourlyDetailEntity dto = new HourlyDetailEntity()
                                {
                                    ID = prevDto.ID,
                                    Bar_Position_Name = prevDto.Bar_Position_Name,
                                    Date = curDay,
                                    Machine_Category = prevDto.Machine_Category,
                                    Machine_Name = prevDto.Machine_Name,
                                };

                                // retrieves the current day values from the mappings
                                actualWork(curDay, null, dto);
                            }
                        }

                        // remove the extra records for the detailed information and sort by position name
                        if (isDetails)
                        {
                            DateTime requestedDate = date.SafeValue();
                            resultSet[counter] = new HourlyDetailsEntity((from o in resultSet[counter]
                                                                          where o.Date == requestedDate
                                                                          orderby o.Bar_Position_Name
                                                                          select o));
                            if (isOccupancy) total.Total = 0;
                        }

                        // total calculation     
                        if (resultSet[counter].Count > 0)
                        {
                            foreach (var item in resultSet[counter])
                            {
                                // column wise total
                                double[] hourValues = item.GetHourValues();
                                if (isOccupancy)
                                {
                                    item.Total = 0;
                                }

                                for (int j = 0; j < hourValues.Length; j++)
                                {
                                    double hourValue = hourValues[j];

                                    //occupancy?
                                    if (isOccupancy)
                                    {
                                        double hourValue2 = ((hourValue / item.OccupancyHour) * 100.00);
                                        item[j] = hourValue2;
                                        item.Total += hourValue;
                                        if (isDetails)
                                        {
                                            total[j] += hourValue2;
                                        }
                                    }
                                    else
                                    {
                                        if (isDetails)
                                        {
                                            total[j] += hourValue;
                                        }
                                    }
                                }

                                // occupancy?
                                if (isOccupancy)
                                {
                                    // occupancy total => ((games_bet_total / (occupancyhour * site_opening_hour)) * 100)
                                    double total2 = ((item.Total / (item.OccupancyHour * item.SiteOpeningHour)) * 100);
                                    //item.Total = (!isDetails ? (total2 / activePositions) : total2);
                                    item.Total = total2;
                                    if (isDetails)
                                    {
                                        total.Total += item.Total;
                                    }
                                }
                            }
                        }

                        // //occupancy summary grouping
                        if (isOccupancy && !isDetails)
                        {
                            var occupanciesGrouped = (from r in resultSet[counter]
                                                      where rows <= 0 || r.Date >= (DateTime.Now.Date.AddDays(- (rows - 1)))
                                                      group r by r.Date);
                            HourlyDetailsEntity result2 = new HourlyDetailsEntity();
                            if (occupanciesGrouped != null)
                            {
                                foreach (var occupancyGrouped in occupanciesGrouped)
                                {
                                    int count = Math.Max(1, occupancyGrouped.Count());
                                    HourlyDetailEntity resultItem = new HourlyDetailEntity()
                                    {
                                        Total = 0,
                                        Date = occupancyGrouped.Key,
                                    };
                                    result2.Add(resultItem);

                                    foreach (var occupancyItem in occupancyGrouped)
                                    {
                                        double[] hourValues = occupancyItem.GetHourValues();
                                        for (int j = 0; j < hourValues.Length; j++)
                                        {
                                            double hourValue = hourValues[j];
                                            resultItem[j] += (hourValue / count);
                                        }


                                        resultItem.Total += (occupancyItem.Total / count);
                                    }
                                }
                            }
                            resultSet[counter].Clear();
                            resultSet[counter] = result2;
                        }

                        // detail total
                        if (isDetails)
                        {
                            displayedPositions = Math.Max(1, resultSet[counter].Count);

                            // occupancy?
                            if (isOccupancy)
                            {
                                // occupany column wise total                        
                                double[] hourValues = total.GetHourValues();
                                for (int j = 0; j < hourValues.Length; j++)
                                {
                                    double hourValue = hourValues[j];
                                    total[j] = (hourValue / displayedPositions);
                                }

                                // occupany overall total
                                total.Total /= displayedPositions;
                            }

                            total.Machine_Name = displayedPositions.ToString();
                            resultSet[counter].Insert(0, total);
                        }

                    }
                }
            }


            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            if (resultSet == null) return new HourlyDetailsEntity();
            if (resultSet != null && resultSet.Length == 1)
                return (resultSet[0].Count == 0 ? new HourlyDetailsEntity() : resultSet[0]);
            else
            {
                return new HourlyDetailsEntity((from creditsWagered in resultSet[0]
                                                join gamesBet in resultSet[1] on
                                                creditsWagered.Date equals gamesBet.Date
                                                where (!isDetails || 
                                                        (isDetails && 
                                                        Convert.ToUInt32(creditsWagered.Bar_Position_Name) == Convert.ToUInt32(gamesBet.Bar_Position_Name) &&
                                                        (gamesBet.Stock.IgnoreCaseCompare(creditsWagered.Stock))))
                                                select (new HourlyDetailEntity()
                                                {
                                                    ID = gamesBet.ID,
                                                    Date = gamesBet.Date,
                                                    Bar_Position_Name = gamesBet.Bar_Position_Name,
                                                    Machine_Name = gamesBet.Machine_Name,
                                                    Machine_Category = gamesBet.Machine_Category,
                                                    Stock = gamesBet.Stock,        // Added this for CR
                                                    HS_Hour1_Value = creditsWagered.HS_Hour1_Value / Math.Max(gamesBet.HS_Hour1_Value, 1.0),
                                                    HS_Hour2_Value = creditsWagered.HS_Hour2_Value / Math.Max(gamesBet.HS_Hour2_Value, 1.0),
                                                    HS_Hour3_Value = creditsWagered.HS_Hour3_Value / Math.Max(gamesBet.HS_Hour3_Value, 1.0),
                                                    HS_Hour4_Value = creditsWagered.HS_Hour4_Value / Math.Max(gamesBet.HS_Hour4_Value, 1.0),
                                                    HS_Hour5_Value = creditsWagered.HS_Hour5_Value / Math.Max(gamesBet.HS_Hour5_Value, 1.0),
                                                    HS_Hour6_Value = creditsWagered.HS_Hour6_Value / Math.Max(gamesBet.HS_Hour6_Value, 1.0),
                                                    HS_Hour7_Value = creditsWagered.HS_Hour7_Value / Math.Max(gamesBet.HS_Hour7_Value, 1.0),
                                                    HS_Hour8_Value = creditsWagered.HS_Hour8_Value / Math.Max(gamesBet.HS_Hour8_Value, 1.0),
                                                    HS_Hour9_Value = creditsWagered.HS_Hour9_Value / Math.Max(gamesBet.HS_Hour9_Value, 1.0),
                                                    HS_Hour10_Value = creditsWagered.HS_Hour10_Value / Math.Max(gamesBet.HS_Hour10_Value, 1.0),
                                                    HS_Hour11_Value = creditsWagered.HS_Hour11_Value / Math.Max(gamesBet.HS_Hour11_Value, 1.0),
                                                    HS_Hour12_Value = creditsWagered.HS_Hour12_Value / Math.Max(gamesBet.HS_Hour12_Value, 1.0),
                                                    HS_Hour13_Value = creditsWagered.HS_Hour13_Value / Math.Max(gamesBet.HS_Hour13_Value, 1.0),
                                                    HS_Hour14_Value = creditsWagered.HS_Hour14_Value / Math.Max(gamesBet.HS_Hour14_Value, 1.0),
                                                    HS_Hour15_Value = creditsWagered.HS_Hour15_Value / Math.Max(gamesBet.HS_Hour15_Value, 1.0),
                                                    HS_Hour16_Value = creditsWagered.HS_Hour16_Value / Math.Max(gamesBet.HS_Hour16_Value, 1.0),
                                                    HS_Hour17_Value = creditsWagered.HS_Hour17_Value / Math.Max(gamesBet.HS_Hour17_Value, 1.0),
                                                    HS_Hour18_Value = creditsWagered.HS_Hour18_Value / Math.Max(gamesBet.HS_Hour18_Value, 1.0),
                                                    HS_Hour19_Value = creditsWagered.HS_Hour19_Value / Math.Max(gamesBet.HS_Hour19_Value, 1.0),
                                                    HS_Hour20_Value = creditsWagered.HS_Hour20_Value / Math.Max(gamesBet.HS_Hour20_Value, 1.0),
                                                    HS_Hour21_Value = creditsWagered.HS_Hour21_Value / Math.Max(gamesBet.HS_Hour21_Value, 1.0),
                                                    HS_Hour22_Value = creditsWagered.HS_Hour22_Value / Math.Max(gamesBet.HS_Hour22_Value, 1.0),
                                                    HS_Hour23_Value = creditsWagered.HS_Hour23_Value / Math.Max(gamesBet.HS_Hour23_Value, 1.0),
                                                    HS_Hour24_Value = creditsWagered.HS_Hour24_Value / Math.Max(gamesBet.HS_Hour24_Value, 1.0),
                                                    Total = creditsWagered.Total / Math.Max(gamesBet.Total, 1.0),
                                                })).ToList());
            }
        }

        public HourlyStatisticsTypesEntity GetHourlyStatisticsTypes()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetHourlyStatisticsTypes");
            HourlyStatisticsTypesEntity result = new HourlyStatisticsTypesEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetHourlyStatisticsTypesResult> dbResults = db.rsp_GetHourlyStatisticsTypes();
                    if (dbResults != null)
                    {
                        foreach (rsp_GetHourlyStatisticsTypesResult dbResult in dbResults)
                        {
                            result.Add(new HourlyStatisticsTypeEntity()
                            {
                                HST_DisplayName = ResourceExtensions.GetResourceTextByKey(null, "Key_DBV_" + Regex.Replace(dbResult.HST_Desc.Trim(), "[^0-9a-zA-Z]+", "")).ToUpper(),
                                HST_Desc=dbResult.HST_Desc,
                                HST_ID = dbResult.HST_ID,
                                HST_Type = dbResult.HST_Type,
                            });
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



        //
        public void GetExchangeSitesettings(int SiteId, string SettingMaster_Name, ref string Setting_Value)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetHourlyStatisticsTypes");

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    int vale = db.GetSiteSetting(SiteId, SettingMaster_Name, ref Setting_Value);
                }

            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }


        }

        //
        public HourlyFilterByValuesEntity GetFilterByValues(HourlyFilterByEntity filterby, int? filterById)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetHourlyStatisticsTypes");
            HourlyFilterByValuesEntity result = new HourlyFilterByValuesEntity();

            try
            {
                using (EnterpriseDataContext db = EnterpriseDataContextHelper.GetDataContext())
                {
                    ISingleResult<rsp_GetHourlyFilterByInfoResult> dbResults = db.rsp_GetHourlyFilterByInfo((int)filterby, filterById);
                    if (dbResults != null)
                    {
                        foreach (rsp_GetHourlyFilterByInfoResult dbResult in dbResults)
                        {
                            result.Add(new HourlyFilterByValueEntity()
                            {
                                FilterById = dbResult.FilterById,
                                FilterByName = dbResult.FilterByName
                            });
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
}
