using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using System.Windows.Forms;
using System.Data.Linq;
using BMC.CoreLib.Data;
using System.Data.Common;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Collections;
using BMC.CoreLib;

namespace BMC.Business.CashDeskOperator
{
    public class HourlyDetails
    {
        HourlyDataAccess hourlyDataAccess;
        public static bool HourlyBasedOnCalendarDay { get; set; }

        public HourlyDetails()
        {
            hourlyDataAccess = new HourlyDataAccess();

        }

        #region Internal Classes
        internal class HourlyDetailModel 
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


        public static void ReadSettings(DataRow settingsRow)
        {
            try
            {
                HourlyBasedOnCalendarDay = Settings.GetBoolValue(settingsRow, "HourlyBasedOnCalendarDay");
                LogManager.WriteLog("HourlyBasedOnCalendarDay : " + HourlyBasedOnCalendarDay.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        
        public DataTable GetInstallationDetails()
        {
            return hourlyDataAccess.InstallationDetailsTable;
        }
        public DataTable GetMachineTypeDetails()
        {
            return hourlyDataAccess.FillMachineTypes();
        }
        public DataTable GetZoneDetails()
        {
            return hourlyDataAccess.FillZones();
        }
        public DataTable GetPositionDetails()
        {
            return hourlyDataAccess.FillPositions();
        }
        public DataTable GetSiteName()
        {
            return hourlyDataAccess.GetSiteName();
        }

        public DataTable GetHSTypes()
        {
            return hourlyDataAccess.FillHSTypes();
        }

         private void GetBarPositionsEnrolledOnGamingDay(ICollection<string> barPositions, DateTime date, int startHour)
        {
            try
            {
               
                    var dbResults = hourlyDataAccess.GetBarPositionsEnrolledOnGamingDay(date,startHour);

                    if (dbResults != null)
                    {
                        foreach (var dbResult in dbResults.AsEnumerable())
                        {
                            barPositions.Add(dbResult["Bar_Pos_Name"].ToString());
                        }
                    }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message,LogManager.enumLogLevel.Error);
            }
        }


		/// <summary>
		/// Main method to get the Hourly Statistics Details
		/// </summary>
		 public HourlyDetailsEntity GetHourlyStatistics(int startHour, int rows, string dataType, int? category,
			 int? zone, int? position, DateTime? date, bool isCalenderDay)
		 {
			 //HourlyDetailsEntity result = new HourlyDetailsEntity();
             HourlyDetailsEntity[] resultSet = null;
			 
			 HourlyDetailCollectionModel mappings = new HourlyDetailCollectionModel();
			 ICollection<string> barPositions = new Rbtree<string>();
			 bool isDetails = (date.HasValue && date.Value != DateTime.MinValue);
			 bool isOccupancy = dataType.ToUpper() == "OCCUPANCY(%)" ? true : false;
			 bool isOccupancyForPosition = isOccupancy && (position != 0);
			 bool isOccupancyForNonPosition = isOccupancy && (position == 0);
			 int displayedPositions = 1;
			 
			 try
			 {
				 // bar positions enrolled on gaming day
				 if (isDetails)
				 {
					 this.GetBarPositionsEnrolledOnGamingDay(barPositions, (DateTime)date, (int)startHour);
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
							 dto[index] = Convert.ToDouble(dbResult[string.Format("HS_Hour{0:D}_Value", (shiftIndex + 1))]);
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


				 DataSet ds = hourlyDataAccess.GetHourlyStatistics(dataType, (int)rows, (int)startHour, (int)category, (int)zone, (int)position, (DateTime)date);

				 if (ds.Tables.Count > 0)
				 {
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

						 DataTable dt = ds.Tables[counter];
					 

					     // only for occupancy
					     //

					     foreach (DataRow dr in dt.Rows)
					     {
						     HourlyDetailEntity r = new HourlyDetailEntity();
						     r.ID = dr.Field<int>("ID");
						     r.Date = dr.Field<DateTime>("Date");
						     r.Day = dr.Field<string>("Day");
						     r.Bar_Position_Name = dr.Field<string>("Bar_Position_Name");
						     r.Machine_Name = dr.Field<string>("Machine_Name");
						     r.Machine_Category = dr.Field<string>("Machine_Category");
						     r.Stock_No = dr.Field<string>("Stock_No");

						     // set the db values
						     for (int i = 0; i < 24; i++)
						     {
							     string hourColumn = string.Format("HS_Hour{0:D}_Value", (i + 1));
							     if (!isOccupancy)
							     {
								     if (isCalenderDay)
									     r[i] = Convert.ToDouble(dr[hourColumn]);
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
									     r[i] = Convert.ToDouble(dr[hourColumn]);
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
									    prevDayDetail[i] = Convert.ToDouble(dr[string.Format("HS_Hour{0:D}_Value", (i + 1))]);
									
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

					     if (dt.Rows.Count == 0)
					     {
						     HourlyDetailEntity r = new HourlyDetailEntity();
						     r.ID = 0;
						     r.Date = DateTime.MinValue;
						     r.Day = string.Empty;
						     r.Bar_Position_Name = string.Empty;
						     r.Machine_Name = string.Empty;
						     r.Machine_Category = string.Empty;
						     r.Stock_No = string.Empty;
						     r.HS_Hour1_Value = 0.00;
						     r.HS_Hour2_Value = 0.00;
						     r.HS_Hour3_Value = 0.00;
						     r.HS_Hour4_Value = 0.00;
						     r.HS_Hour5_Value = 0.00;
						     r.HS_Hour6_Value = 0.00;
						     r.HS_Hour7_Value = 0.00;
						     r.HS_Hour8_Value = 0.00;
						     r.HS_Hour9_Value = 0.00;
						     r.HS_Hour10_Value = 0.00;
						     r.HS_Hour11_Value = 0.00;
						     r.HS_Hour12_Value = 0.00;
						     r.HS_Hour13_Value = 0.00;
						     r.HS_Hour14_Value = 0.00;
						     r.HS_Hour15_Value = 0.00;
						     r.HS_Hour16_Value = 0.00;
						     r.HS_Hour17_Value = 0.00;
						     r.HS_Hour18_Value = 0.00;
						     r.HS_Hour19_Value = 0.00;
						     r.HS_Hour20_Value = 0.00;
						     r.HS_Hour21_Value = 0.00;
						     r.HS_Hour22_Value = 0.00;
						     r.HS_Hour23_Value = 0.00;
						     r.HS_Hour24_Value = 0.00;
						     r.Total = 0.00;
						     resultSet[counter].Add(r);
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
					         DateTime requestedDate = (DateTime)date;
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
									         double hourValue2 = ((hourValue / Convert.ToDouble(item.OccupancyHour)) * 100.00);
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
							         double total2 = ((item.Total / Convert.ToDouble(item.OccupancyHour * item.SiteOpeningHour)) * 100.0);
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
                                                       where ((int)rows) <= 0 || r.Date >= (DateTime.Now.Date.AddDays(-(((int)rows) - 1)))
											           group r by r.Date);
                             //DateTimeFormatInfo dtInfo = new System.Globalization.DateTimeFormatInfo();
                             //string[] newWeekDays = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
                             //dtInfo.AbbreviatedDayNames = newWeekDays;

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
                                         Day = occupancyGrouped.Key.DayOfDate(),
							         };
							         result2.Add(resultItem);

							         foreach (var occupancyItem in occupancyGrouped)
							         {
								         double[] hourValues = occupancyItem.GetHourValues();
								         for (int j = 0; j < hourValues.Length; j++)
								         {
									         double hourValue = hourValues[j];
									         resultItem[j] += (hourValue /Convert.ToDouble(count));
								         }
								         resultItem.Total += (occupancyItem.Total / Convert.ToDouble(count));
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
							         total[j] = ((hourValue / Convert.ToDouble(displayedPositions)));
						         }

						         // occupany overall total
						         total.Total = (total.Total / Convert.ToDouble(displayedPositions));
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
                 if(resultSet != null && resultSet.Length == 1)
			        return (resultSet[0].Count == 0 ? new HourlyDetailsEntity() : resultSet[0]);
                 else
                 {
                     return new HourlyDetailsEntity((from creditsWagered in resultSet[0]
                                              join gamesBet  in resultSet[1] on
                                              creditsWagered.Date equals gamesBet.Date
                                                     where (!isDetails ||                                                      
                                                            (isDetails && 
                                                                Convert.ToUInt32(creditsWagered.Bar_Position_Name) == Convert.ToUInt32(gamesBet.Bar_Position_Name) &&
                                                                (gamesBet.Stock_No.IgnoreCaseCompare(creditsWagered.Stock_No))))
													            select (new HourlyDetailEntity() {
						     ID = gamesBet.ID,
						     Date = gamesBet.Date,
                             Day = gamesBet.Day,
                             Bar_Position_Name = gamesBet.Bar_Position_Name,
                             Machine_Name = gamesBet.Machine_Name,
                             Machine_Category = gamesBet.Machine_Category,
                             Stock_No = gamesBet.Stock_No,
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
		 }

	  
	}



