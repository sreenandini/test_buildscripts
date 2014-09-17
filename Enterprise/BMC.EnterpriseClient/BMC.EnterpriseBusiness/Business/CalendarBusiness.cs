using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using System.Data;
using BMC.DataAccess;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;


namespace BMC.EnterpriseBusiness.Business
{
    public class CalendarBusiness
    {
        private static CalendarBusiness _CalendarBiz;
        public static CalendarBusiness CreateInstance()
        {
            if (_CalendarBiz == null)
                _CalendarBiz = new CalendarBusiness();

            return _CalendarBiz;
        }
        private const string DBFormat = "dd-MM-yyyy HH:mm:ss";

        public List<CalendarEntity> GetLstCalendarDetails()
        {
            List<CalendarEntity> obcoll = null;
            try
            {
                List<rsp_GetLstCalendarResult> CalendarList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    CalendarList = DataContext.rsp_GetLstCalendar().ToList();
                }

                obcoll = (from obj in CalendarList
                          select new CalendarEntity
                          {
                              Calendar_ID = obj.Calendar_ID,
                              Calendar_Description = obj.Calendar_Description,
                              Calendar_Year_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Year_Start_Date),
                              Calendar_Year_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Year_End_Date)
                          }).ToList<CalendarEntity>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<GetCalendarListEntity> GetCalendarList()
        {
            List<GetCalendarListEntity> obcoll = null;
            try
            {
                List<rsp_GetCalendarListResult> CalendarList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    CalendarList = DataContext.rsp_GetCalendarList().ToList();
                }

                obcoll = (from obj in CalendarList
                          select new GetCalendarListEntity
                          {
                              Calendar_ID = obj.Calendar_ID,
                              Calendar_Description = obj.Calendar_Description,
                              Calendar_Year_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Year_Start_Date),
                              Calendar_Year_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Year_End_Date),
                              IsCompleteCalendar = obj.IsCompleteCalendar
                          }).ToList<GetCalendarListEntity>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public bool CheckCompleteCalendar(int? Calendar_ID, ref bool? bValid)
        {

            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.CheckIncompleteCalendar(Calendar_ID, ref bValid);
            }
            return Convert.ToBoolean(bValid);
        }
        DateTime ConvertToCurrentDateFormat(string str_Value)
        {
            return DateTime.ParseExact(str_Value, DBFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
        public List<CalendarEntity> GetCalendar(int Calendar_ID)
        {
            List<CalendarEntity> obcal = null;
            try
            {
                List<rsp_GetCalendarResult> lsCal;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lsCal = DataContext.rsp_GetCalendar(Calendar_ID).ToList();
                }
                obcal = (from obj in lsCal
                         select new CalendarEntity
                         {
                             Calendar_ID = obj.Calendar_ID,
                             Calendar_Description = obj.Calendar_Description,
                             Calendar_Year_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Year_Start_Date),
                             Calendar_Year_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Year_End_Date),
                             CalendarBasedOn = obj.CalendarBasedOn,
                             IsCalendarCreatedUsingAutoCalendar = obj.IsCalendarCreatedUsingAutoCalendar
                         }).ToList<CalendarEntity>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return obcal;
        }

        public List<CompanyCalEntity> GetCompanyInfo()
        {
            List<rsp_GetCompanyInfoResult> DetailList = null;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DetailList = DataContext.GetCompanyInfo().ToList();
            }

            return DetailList.Select(X => new CompanyCalEntity
            {
                Company_ID = X.Company_ID,
                Company_Name = X.Company_Name,
                Sub_Company_ID = X.Sub_Company_ID,
                Sub_Company_Name = X.Sub_Company_Name

            }).ToList();
        }

        public List<CalendarPeriod> GetCalendarPeriod(int Calendar_ID)
        {
            List<CalendarPeriod> calperiod = null;
            try
            {
                List<rsp_GetCalendarPeriodResult> lscalPeriod;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lscalPeriod = DataContext.rsp_GetCalendarPeriod(Calendar_ID).ToList();
                }
                calperiod = (from obj in lscalPeriod
                             select new CalendarPeriod
                         {
                             Calendar_ID = obj.Calendar_ID,
                             Calendar_Period_ID = obj.Calendar_Period_ID,
                             Calendar_Period_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Period_Start_Date),
                             Calendar_Period_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Period_End_Date),
                             Calendar_Period_Number = obj.Calendar_Period_Number
                         }).ToList<CalendarPeriod>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return calperiod;
        }
        public List<CalendarPeriod> GetLstCalPeriod(int Calendar_ID, int Calendar_Period_Number, int Calendar_Period_ID)
        {
            List<CalendarPeriod> calperiod = null;
            try
            {
                List<rsp_GetLstCalendarPeriodResult> lscalPeriod;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lscalPeriod = DataContext.rsp_GetLstCalendarPeriod(Calendar_ID, Calendar_Period_Number, Calendar_Period_ID).ToList();
                }
                calperiod = (from obj in lscalPeriod
                             select new CalendarPeriod
                             {
                                 Calendar_ID = obj.Calendar_ID,
                                 Calendar_Period_ID = obj.Calendar_Period_ID,
                                 Calendar_Period_Number = obj.Calendar_Period_Number,
                                 Calendar_Period_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Period_Start_Date),
                                 Calendar_Period_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Period_End_Date),
                             }).ToList<CalendarPeriod>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return calperiod;
        }
        public List<CalendarPeriod> GetCalPeriod(int Calendar_Period_ID)
        {
            List<CalendarPeriod> calperiod = null;
            try
            {
                List<rsp_GetCalPeriodResult> lscalPeriod;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lscalPeriod = DataContext.rsp_GetCalPeriod(Calendar_Period_ID).ToList();
                }
                calperiod = (from obj in lscalPeriod
                             select new CalendarPeriod
                             {
                                 Calendar_ID = obj.Calendar_ID,
                                 Calendar_Period_ID = obj.Calendar_Period_ID,
                                 Calendar_Period_Number = obj.Calendar_Period_Number,
                                 Calendar_Period_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Period_Start_Date),
                                 Calendar_Period_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Period_End_Date),
                             }).ToList<CalendarPeriod>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return calperiod;
        }
        public List<CalendarPeriod> GetCancelPeriod(int CalendarID, int CalendarPeriodNumber)
        {
            List<CalendarPeriod> calperiod = null;
            try
            {
                List<rsp_CancelPeriodCalendarResult> lscalPeriod;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lscalPeriod = DataContext.rsp_CancelPeriodCalendar(CalendarID, CalendarPeriodNumber).ToList();
                }
                calperiod = (from obj in lscalPeriod
                             select new CalendarPeriod
                             {
                                 Calendar_ID = obj.Calendar_ID,
                                 Calendar_Period_ID = obj.Calendar_Period_ID,
                                 Calendar_Period_Number = obj.Calendar_Period_Number,
                                 Calendar_Period_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Period_Start_Date),
                                 Calendar_Period_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Period_End_Date),
                             }).ToList<CalendarPeriod>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return calperiod;
        }

        public List<CalendarWeek> GetLstCalendarWeek(int Calendar_ID, int Calendar_Week_Number, int Calendar_Week_ID)
        {
            List<CalendarWeek> calWeek = null;
            try
            {
                List<rsp_GetLstCalendarWeekResult> lscalWeek;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lscalWeek = DataContext.rsp_GetLstCalendarWeek(Calendar_ID, Calendar_Week_Number, Calendar_Week_ID).ToList();
                }
                calWeek = (from obj in lscalWeek
                           select new CalendarWeek
                           {
                               Calendar_ID = obj.Calendar_ID,
                               Calendar_Week_ID = obj.Calendar_Period_ID,
                               Calendar_Week_Number = obj.Calendar_Week_Number,
                               Calendar_Week_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Week_Start_Date),
                               Calendar_Week_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Week_End_Date),
                           }).ToList<CalendarWeek>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return calWeek;
        }
        public List<CalendarWeek> GetCalWeek(int Calendar_Week_ID)
        {
            List<CalendarWeek> calWeek = null;
            try
            {
                List<rsp_GetCalWeekResult> lscalWeek;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lscalWeek = DataContext.rsp_GetCalWeek(Calendar_Week_ID).ToList();
                }
                calWeek = (from obj in lscalWeek
                           select new CalendarWeek
                             {
                                 Calendar_ID = obj.Calendar_ID,
                                 Calendar_Week_ID = obj.Calendar_Period_ID,
                                 Calendar_Week_Number = obj.Calendar_Week_Number,
                                 Calendar_Week_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Week_Start_Date),
                                 Calendar_Week_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Week_End_Date),
                             }).ToList<CalendarWeek>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return calWeek;
        }
        public List<CalendarWeek> GetCancelWeek(int CalendarID, int CalendarWeekNumber)
        {
            List<CalendarWeek> calWeek = null;
            try
            {
                List<rsp_CancelWeekCalendarResult> lscalWeek;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lscalWeek = DataContext.rsp_CancelWeekCalendar(CalendarID, CalendarWeekNumber).ToList();
                }
                calWeek = (from obj in lscalWeek
                           select new CalendarWeek
                           {
                               Calendar_ID = obj.Calendar_ID,
                               Calendar_Period_ID = obj.Calendar_Period_ID,
                               Calendar_Week_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Week_End_Date),
                               Calendar_Week_ID = obj.Calendar_Week_ID,
                               Calendar_Week_Number = obj.Calendar_Week_Number,
                               Calendar_Week_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Week_Start_Date)

                           }).ToList<CalendarWeek>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return calWeek;
        }
        public List<CalendarWeek> GetCalendarWeek(int Calendar_ID)
        {
            List<CalendarWeek> CalWeek = null;
            try
            {
                List<rsp_GetCalendarWeekResult> lscalWeek;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lscalWeek = DataContext.rsp_GetCalendarWeek(Calendar_ID).ToList();
                }
                CalWeek = (from obj in lscalWeek
                           select new CalendarWeek
                             {
                                 Calendar_ID = obj.Calendar_ID,
                                 Calendar_Period_ID = obj.Calendar_Period_ID,
                                 Calendar_Week_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Week_End_Date),
                                 Calendar_Week_ID = obj.Calendar_Week_ID,
                                 Calendar_Week_Number = obj.Calendar_Week_Number,
                                 Calendar_Week_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Week_Start_Date)

                             }).ToList<CalendarWeek>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CalWeek;
        }



        public List<Operator_Calendar> GetOperatorCalendar(int Operator_Id, int Calendar_Id)
        {
            List<Operator_Calendar> CalWeek = null;
            try
            {
                List<rsp_GetOperatorCalendarResult> lsOperator;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lsOperator = DataContext.rsp_GetOperatorCalendar(Operator_Id, Calendar_Id).ToList();
                }
                CalWeek = (from obj in lsOperator
                           select new Operator_Calendar
                           {
                               Calendar_ID = obj.Calendar_ID,
                               Operator_Calendar_Active = obj.Operator_Calendar_Active,
                               Operator_Calendar_ID = obj.Operator_Calendar_ID,
                               Operator_ID = obj.Operator_ID
                           }).ToList<Operator_Calendar>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CalWeek;

        }
        public List<Operator_Cal> GetOperatorCal(int Operator_Id)
        {
            List<Operator_Cal> OperCal = null;
            try
            {
                List<rsp_GetOperatorCalResult> Op;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    Op = DataContext.rsp_GetOperatorCal(Operator_Id).ToList();
                }
                OperCal = (from obj in Op
                           select new Operator_Cal
                           {
                               Calendar_ID = obj.Calendar_ID,
                               Operator_ID = obj.Operator_ID
                           }).ToList<Operator_Cal>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperCal;
        }
        public List<Operator_Calendar> GetOperatorByActive(int Operator_Id)
        {
            List<Operator_Calendar> OperCal = null;
            try
            {
                List<rsp_GetOperatorByActiveResult> Op;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    Op = DataContext.rsp_GetOperatorByActive(Operator_Id).ToList();
                }
                OperCal = (from obj in Op
                           select new Operator_Calendar
                           {
                               Calendar_ID = obj.Calendar_ID,
                               Operator_Calendar_Active = obj.Operator_Calendar_Active,
                               Operator_Calendar_ID = obj.Operator_Calendar_ID,
                               Operator_ID = obj.Operator_ID
                           }).ToList<Operator_Calendar>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OperCal;
        }

        public List<SubCompanyCalendar> GetSubCompanyDetails(int SubCompanyId, int CalendarId)
        {
            List<SubCompanyCalendar> objSCC = null;
            try
            {
                List<rsp_GetSubcompanyCalendarByCalendarIdResult> objSubcmpny;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objSubcmpny = DataContext.rsp_GetSubcompanyCalendarByCalendarId(SubCompanyId, CalendarId).ToList();
                }
                objSCC = (from obj in objSubcmpny
                          select new SubCompanyCalendar
                          {
                              Calendar_ID = obj.Calendar_ID,
                              Sub_Company_Calendar_Active = obj.Sub_Company_Calendar_Active,
                              Sub_Company_Calendar_ID = obj.Sub_Company_Calendar_ID,
                              Sub_Company_ID = obj.Calendar_ID
                          }).ToList<SubCompanyCalendar>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objSCC;
        }
        public List<SubCompanyCal> GetSubCompanyByID(int SubCompanyId)
        {
            List<SubCompanyCal> objSubComapany = null;
            try
            {
                List<rsp_GetSubCompanyCalResult> objSubcmpny;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objSubcmpny = DataContext.rsp_GetSubCompanyCal(SubCompanyId).ToList();
                }
                objSubComapany = (from obj in objSubcmpny
                                  select new SubCompanyCal
                                  {
                                      Calendar_ID = obj.Calendar_ID,
                                      Sub_Company_ID = obj.Sub_Company_ID
                                  }).ToList<SubCompanyCal>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objSubComapany;
        }
        public List<SubCompanyCalendar> GetSubCompanyByActive(int SubCompanyId)
        {
            List<SubCompanyCalendar> objSubComapany = null;
            try
            {
                List<rsp_GetSubCompanyCalenderByActiveResult> objSubActive;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objSubActive = DataContext.rsp_GetSubCompanyCalenderByActive(SubCompanyId).ToList();
                }
                objSubComapany = (from obj in objSubActive
                                  select new SubCompanyCalendar
                                  {
                                      Calendar_ID = obj.Calendar_ID,
                                      Sub_Company_Calendar_Active = obj.Sub_Company_Calendar_Active,
                                      Sub_Company_Calendar_ID = obj.Sub_Company_Calendar_ID,
                                      Sub_Company_ID = obj.Calendar_ID
                                  }).ToList<SubCompanyCalendar>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objSubComapany;
        }

        public List<CurrentCalendarDetails> GetCurrentCalendarDetails(int Calendar_ID)
        {
            List<CurrentCalendarDetails> lstCurrentCalDtl = null;

            try
            {
                List<rsp_GetCurrentCalendarDetailsResult> objCurrentCal;
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objCurrentCal = Datacontext.GetCurrentCalendarDetails(Calendar_ID).ToList();
                }
                lstCurrentCalDtl = (from calDetail in objCurrentCal
                                    select new CurrentCalendarDetails
                                    {
                                        Calendar_Period_End_Date = ConvertToCurrentDateFormat(calDetail.Calendar_Period_End_Date),
                                        Calendar_Period_Number = calDetail.Calendar_Period_Number,
                                        Calendar_Week_Number = calDetail.Calendar_Week_Number

                                    }).ToList<CurrentCalendarDetails>();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return lstCurrentCalDtl;
        }

        public List<ExportCalendar> GetExportCalendar(int SubCompanyId)
        {
            List<ExportCalendar> objSubComapany = null;
            try
            {
                List<rsp_GetExportCalendarResult> objSubActive;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objSubActive = DataContext.rsp_GetExportCalendar(SubCompanyId).ToList();
                }
                objSubComapany = (from obj in objSubActive
                                  select new ExportCalendar
                                  {
                                      Site_ID = obj.Site_ID,
                                      Site_Name = obj.Site_Name
                                  }).ToList<ExportCalendar>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objSubComapany;
        }
        public List<ExportCalendar> GetExportOperaotorID(int Operator_ID)
        {
            List<ExportCalendar> objOper = null;
            try
            {
                List<rsp_GetExportCalendarResult> objExp;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objExp = DataContext.rsp_GetOperatorIDForExport(Operator_ID).ToList();
                }
                objOper = (from obj in objExp
                           select new ExportCalendar
                           {
                               Site_ID = obj.Site_ID,
                               Site_Name = obj.Site_Name
                           }).ToList<ExportCalendar>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOper;
        }




        public List<Operator_Calendar> GetOperatorDetails()
        {
            List<Operator_Calendar> objOperator = null;
            try
            {
                List<rsp_GetOperatorForCalendarResult> objGetOperator;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objGetOperator = DataContext.rsp_GetOperatorForCalendar().ToList();
                }
                objOperator = (from obj in objGetOperator
                               select new Operator_Calendar
                               {
                                   Operator_ID = obj.Operator_ID,
                                   Operator_Name = obj.Operator_Name

                               }).ToList<Operator_Calendar>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objOperator;
        }
        public List<GetSubCompanyCalendarActive> GetCalendarHistory(int SCompanyID)
        {
            List<GetSubCompanyCalendarActive> objActive = null; ;
            try
            {
                List<rsp_GetSubCompanyCalendarActiveResult> objHistory;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objHistory = DataContext.rsp_GetSubCompanyCalendarActive(SCompanyID).ToList();
                }
                objActive = (from obj in objHistory
                             select new GetSubCompanyCalendarActive
                             {
                                 Calendar_Description = obj.Calendar_Description,
                                 Calendar_Year_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Year_Start_Date),
                                 Calendar_Year_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Year_End_Date),
                                 Calendar_ID = obj.Calendar_ID,
                                 Sub_Company_Calendar_Active = obj.Sub_Company_Calendar_Active,
                                 Sub_Company_Calendar_ID = obj.Sub_Company_Calendar_ID,
                                 Calendar_History = obj.Calendar_History
                             }).ToList<GetSubCompanyCalendarActive>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objActive;
        }

        public List<GetOperatorCalendarActive> GetOperatorHistory(int OperatoID)
        {
            List<GetOperatorCalendarActive> objActive = null;
            try
            {
                List<rsp_GetOperatorCalendarActiveResult> objHistory;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    objHistory = DataContext.rsp_GetOperatorCalendarActive(OperatoID).ToList();
                }
                objActive = (from obj in objHistory
                             select new GetOperatorCalendarActive
                             {
                                 Calendar_Description = obj.Calendar_Description,
                                 Calendar_Year_Start_Date = ConvertToCurrentDateFormat(obj.Calendar_Year_Start_Date),
                                 Calendar_Year_End_Date = ConvertToCurrentDateFormat(obj.Calendar_Year_End_Date),
                                 Calendar_ID = obj.Calendar_ID,
                                 Operator_Calendar_Active = obj.Operator_Calendar_Active,
                                 Operator_Calendar_ID = obj.Operator_Calendar_ID,
                                 Calendar_History = obj.Calendar_History
                             }).ToList<GetOperatorCalendarActive>();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objActive;
        }



        public int IsNameExists(String CalendarName, int CalendarID, ref int? NameCount)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.rsp_ISCalendarNameExists(CalendarName,CalendarID, ref NameCount);
            }
        }
        public int GetCalendarId(int CalendarId, ref int? subcompanyactive)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.rsp_GetSubCompanyCalender(CalendarId, ref subcompanyactive);

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }


        }
        public int UpdateOpertor(int Operator_Id)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.Usp_UpdateOperator(Operator_Id);

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int InsertNewOperatorCalendar(int Operator_Id, int Calendar_Id, bool Operator_Calendar_Active)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.Usp_InsertNewOperatorCalendar(Operator_Id, Calendar_Id, Operator_Calendar_Active);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int UpdateSubCompany(int SubCompanyID)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.Usp_UpdateSubComapnyCalendar(SubCompanyID);

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int InsertNewSubCompanyCalendar(int SubCompanyID, int Calendar_Id, bool @Sub_Company_Calendar_Active)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.Usp_InsertNewSubCompanyCalendar(SubCompanyID, Calendar_Id, @Sub_Company_Calendar_Active);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int InsertExportHistory(string Reference, string Type, int id)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.usp_Export_History(Reference, Type, id);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int InsertCalendar(string Calendar_Description, string Calendar_Year_Start_Date, string Calendar_Year_End_Date)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.usp_InsertNewCalendar(Calendar_Description, Calendar_Year_Start_Date, Calendar_Year_End_Date);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int UpdateCalendar(int Calendar_ID, string calendarDescription, string Calendar_Year_Start_Date, string Calendar_Year_End_Date)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.usp_UpdateCalendar(Calendar_ID,calendarDescription, Calendar_Year_Start_Date, Calendar_Year_End_Date);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int AddNewCalendarPeriod(int Calendar_Period_Number, string Calendar_Period_Start_Date, string Calendar_Period_End_Date, int CalendarID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.usp_InsertNewCalendarPeriod(Calendar_Period_Number, Calendar_Period_Start_Date, Calendar_Period_End_Date, CalendarID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 2;
            }
        }
        public int AddNewCalendarWeek(int Calendar_Week_Number, string Calendar_Week_Start_Date, string Calendar_Week_End_Date, int CalendarID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.usp_InsertNewCalendarWeek(Calendar_Week_Number, Calendar_Week_Start_Date, Calendar_Week_End_Date, CalendarID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 2;
            }
        }
        public int UpdateCalendarPeriod(int CalendarPeriodNumber, string CalendarPeriodStartDate, string CalendarPeriodEndDate, int CalendarID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.usp_UpdateCalendarPeriod(CalendarPeriodNumber, CalendarPeriodStartDate, CalendarPeriodEndDate, CalendarID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int UpdateWeekCalendar(int CalendarWeekNumber, string CalendarWeekStartDate, string CalendarEndDate, int CalendarId)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.usp_UpdateWeekCalendar(CalendarWeekNumber, CalendarWeekStartDate, CalendarEndDate, CalendarId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int CalendarClose()
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.usp_CalendarClose();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        public int CheckCalendarDurationExists(string cType, string Start_Date, string End_Date, ref bool? bRetValue)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return Datacontext.CheckCalendarDurationExists(cType, Start_Date, End_Date, ref bRetValue);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
       
    }
}


