using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using System.Data.Linq;
using System.Collections;
using System.Collections.ObjectModel;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Xml.Linq;
using System.Windows.Forms;
using BMC.Common;
using System.Text.RegularExpressions;

namespace BMC.EnterpriseBusiness.Business
{
    public class EmployeeCardBiz
    {

        #region Local Declaration

        private static EmployeeCardBiz _EmpCard;

        #endregion Local Declaration

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns>EmployeeCardBiz Object</returns>
        public static EmployeeCardBiz CreateInstance()
        {
            if (_EmpCard == null)
                _EmpCard = new EmployeeCardBiz();

            return _EmpCard;
        }

        /// <summary>
        /// GetEmployeeCardDetails
        /// Gets the employee card details 
        /// </summary>
        /// <returns>Employee card  List object</returns>
        public List<EmployeeCardEntity> GetEmployeeCardDetails(string EmpCardNumber)
        {
            LogManager.WriteLog("GetEmployeeCardDetails started", LogManager.enumLogLevel.Info);

            List<EmployeeCardEntity> lstRetUserGroup = null;
            //List<rsp_GetEmployeeCardDetailsResult> lstEmpCardDetails;


            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    var lstEmpCardDetails = DataContext.GetEmployeeCardDetails(EmpCardNumber);

                    lstRetUserGroup = (from Records in lstEmpCardDetails
                                       select new EmployeeCardEntity
                                       {
                                           EmpID = Records.EmpID,
                                           EmployeeCardNumber = Records.EmployeeCardNumber,
                                           CardType = Records.CardType,
                                           EmployeeName = Records.EmployeeName,
                                           IsActive = Records.IsActive,
                                           IsMasterCard = Records.IsMasterCard,
                                           Mapped = Records.Mapped,
                                           IsChecked = Records.IsChecked,
                                           UserID = Records.UserID,
                                           SiteCode = Records.SiteCode,
                                           EmpCardLevel = Records.CardLevel
                                       }).ToList<EmployeeCardEntity>();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return lstRetUserGroup;
        }
        /// <summary>
        /// GetEmployeeCardDetails
        /// Gets the employee card details 
        /// </summary>
        /// <returns>Employee card  List object</returns>
        public List<EmployeeCardEntity> GetEmployeeCardInfo(string EmpCardNumber)
        {
            //LogManager.WriteLog("GetEmployeeCardDetails started", LogManager.enumLogLevel.Info);

            List<EmployeeCardEntity> lstRetUserGroup = null;
            //List<rsp_GetEmployeeCardDetailsResult> lstEmpCardDetails;


            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    var lstEmpCardDetails = DataContext.GetEmployeeCardInfo(EmpCardNumber);

                    lstRetUserGroup = (from Records in lstEmpCardDetails
                                       select new EmployeeCardEntity
                                       {
                                           EmpID = Records.EmpID,
                                           EmployeeCardNumber = Records.EmployeeCardNumber,
                                           CardType = Records.CardType,
                                           EmployeeName = Records.EmployeeName,
                                           IsActive = Records.IsActive,
                                           IsMasterCard = Records.IsMasterCard,
                                           Mapped = Records.Mapped,
                                           IsChecked = Records.IsChecked,
                                           UserID = Records.UserID,
                                           SiteCode = Records.SiteCode,
                                           EmpCardLevel = Records.CardLevel,

                                       }).ToList<EmployeeCardEntity>();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return lstRetUserGroup;
        }

        public List<CardLevel> GetCardLevelBasedOnRole()
        {
            List<CardLevel> lstRetUserGroup = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    var lstEmpCardDetails = DataContext.GetCardLevelBasedOnRole();

                    lstRetUserGroup = (from Records in lstEmpCardDetails
                                       select new CardLevel
                                       {
                                           CardLevelID = Records.CardLevel,
                                           RoleID = Records.SecurityRoleID
                                       }).ToList<CardLevel>();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return lstRetUserGroup;
        }

        public List<EmployeeCardEntity> GetCardLevels()
        {
            LogManager.WriteLog("GetEmployeeCardDetails started", LogManager.enumLogLevel.Info);

            List<EmployeeCardEntity> lstCardTypes = null;
            List<rsp_GetEmployeeCardLevelResult> lstCardType;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstCardType = DataContext.GetEmployeeCardLevel().ToList();
                }
                lstCardTypes = (from Records in lstCardType
                                select new EmployeeCardEntity
                                {
                                    EmpCardLevel = Records.CardLevel,
                                }).ToList<EmployeeCardEntity>();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return lstCardTypes;
        }


        /// <summary>
        /// 
        /// Insert the employee card details 
        /// </summary>
        /// <param name=""></param>
        /// <returns> List object</returns>
        public bool InsertEmployeeCardDetails(string empCardNumber, string EmpName, int? UserID, bool? isActive, string createdBy)
        {

            int? result = 0;
            using (EnterpriseDataContext datacontext = EnterpriseDataContextHelper.GetDataContext())
            {
                datacontext.InsertEmployeeCardDetails(empCardNumber, EmpName, UserID, isActive, createdBy, ref result);
            }
            return (result == 1 ? true : false);
        }

        /// <summary>
        /// 
        /// Insert the employee card types 
        /// </summary>
        /// <param name=""></param>
        /// <returns> List object</returns>
        public bool InsertEmployeeCardTypes(string CardType)
        {
            int? result = 0;
            using (EnterpriseDataContext datacontext = EnterpriseDataContextHelper.GetDataContext())
            {
                datacontext.InsertEmployeeCardTypes(CardType, ref result);
            }
            return (result == 1 ? true : false);
        }


        /// <summary>
        /// 
        /// Revoke the Employee card associated to a user 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="empCardNumbers"></param>
        /// <returns> true/false</returns>
        public bool RevokeEmployeeCard(int userID,
            string empCardNumbers, int AuditUserId, string AuditUserName)
        {
            try
            {
                using (EnterpriseDataContext datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    datacontext.RevokeEmployeeCard(userID, empCardNumbers, AuditUserId, AuditUserName);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// 
        /// Associate the Employee card to a user 
        /// </summary>
        /// <param name="CardTrack"></param>
        /// <param name="UserID"></param>
        /// <param name="EmpCardNumbers"></param>
        /// <returns> true/false</returns>
        public int? UpdateUseronEmpCard(int userID, bool? CardTrack,
            string empCardNumbers)
        {
            int? Result = 0;
            try
            {
                using (EnterpriseDataContext datacontext = EnterpriseDataContextHelper.GetDataContext())
                    datacontext.UpdateUseronEmpCard(userID, CardTrack, empCardNumbers, ref Result);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return Result;
        }

        public void GetSetting(int setting_ID, string setting_Name, string setting_Default, ref string setting_Value)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.GetSetting(setting_ID, setting_Name, setting_Default, ref setting_Value);
            }
        }

        /// <summary>
        /// insert into export history
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="type"></param>
        /// <param name="SiteCode"></param>
        /// <returns></returns>
        public bool InsertExportHistory(string CardNumber, int? UserID, string type, string SiteCode)
        {
            try
            {
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                    context.Insert_ExportHistory(CardNumber, type, UserID, SiteCode);
                return true;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }

        }

        /// <summary>
        /// Get all the GMU Modes available
        /// </summary>
        /// 
        /// <returns>list of available GMU modes</returns>




        public List<ActiveSiteDetailsforuserResult> GetActiveSiteDetailsforuser(int UserID)
        {
            List<rsp_GetActiveSiteDetailsforuserResult> lstDetails;
            try
            {
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                    lstDetails = context.GetActiveSiteDetailsforuser(UserID).ToList();

                List<ActiveSiteDetailsforuserResult> lstSiteDetails = (from Records in lstDetails
                                                                       select new ActiveSiteDetailsforuserResult
                                                                       {
                                                                           SC_ExchangeConnectionSting = Records.SC_ExchangeConnectionSting,
                                                                           SC_TicketConnectionSting = Records.SC_TicketConnectionSting,
                                                                           Site_Code = Records.Site_Code,
                                                                           Site_ID = Records.Site_ID,
                                                                           Site_Name = Records.Site_Name
                                                                       }).ToList<ActiveSiteDetailsforuserResult>();
                return lstSiteDetails;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return null;
            }
        }


        /// <summary>
        /// 
        /// Gets the Card Types.
        /// </summary>
        /// <param name=""></param>
        /// <returns> List object</returns>
        public List<EmployeeCardTypes> GetCardTypes()
        {
            LogManager.WriteLog("GetEmployeeCardDetails started", LogManager.enumLogLevel.Info);

            List<EmployeeCardTypes> lstCardTypes = null;
            List<rsp_GetEmployeeCardTypesResult> lstCardType;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstCardType = DataContext.GetEmployeeCardTypes().ToList();
                }
                lstCardTypes = (from Records in lstCardType
                                select new EmployeeCardTypes
                                {
                                    EmpCardTypeID = Records.GMUModeGroupID,
                                    EmpCardType = Records.GMUModeGroupDescription,
                                }).ToList<EmployeeCardTypes>();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return lstCardTypes;
        }

        public List<EmployeeEventGroupTypes> GetEventGroupTypes()
        {
            LogManager.WriteLog("GetEmployeeCardDetails started", LogManager.enumLogLevel.Info);

            List<EmployeeEventGroupTypes> lstCardTypes = null;
            List<rsp_GetEventGroupTypesResult> lstCardType;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstCardType = DataContext.GetEventGroupTypes().ToList();
                }
                lstCardTypes = (from Records in lstCardType
                                select new EmployeeEventGroupTypes
                                {
                                    EmpEventGroupID = Records.GMUEventGroupID,
                                    EmpEventGroupType = Records.GMUEventGroupName,
                                }).ToList<EmployeeEventGroupTypes>();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return lstCardTypes;
        }

        public List<EmployeeEventsGroup> GetEmpGMUEvents(int RoleID)
        {
            string PROC = "GetEmpGMUModes";
            try
            {
                List<rsp_GetEmpGMUEventsResult> lstDetails;
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                    lstDetails = context.GetEmpGMUEvents(RoleID).ToList();
                List<EmployeeEventsGroup> lstEmployeeGroup = (from rec in lstDetails
                                                              select new EmployeeEventsGroup
                                                              {
                                                                  GMUEventGroupID = rec.GMUEventGroupID,
                                                                  EmpGMUEventId = rec.EmpGMUEventId,
                                                                  GMUEventID = rec.GMUEventId,

                                                              }).ToList<EmployeeEventsGroup>();
                return lstEmployeeGroup;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " " + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return new List<EmployeeEventsGroup>();
            }
        }
        public List<EmployeeGMUModeGroup> GetEmpGMUModes(int RoleID)
        {
            string PROC = "GetEmpGMUModes";
            try
            {
                List<rsp_GetEmpGMUModesResult> lstDetails;
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                    lstDetails = context.GetEmpGMUModes(RoleID).ToList();
                List<EmployeeGMUModeGroup> lstEmployeeGroup = (from rec in lstDetails
                                                               select new EmployeeGMUModeGroup
                                                               {
                                                                   EmpGMUModeId = rec.EmpGMUModeId,
                                                                   GMUModeId = rec.GMUModeId,
                                                                   GMUMode = rec.GMUMode,
                                                                   GMUModeGroupID = rec.GMUModeGroupID,
                                                                   EmpGMUModeGroup = rec.EmpGMUModeGroup
                                                               }).ToList<EmployeeGMUModeGroup>();
                return lstEmployeeGroup;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " " + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return new List<EmployeeGMUModeGroup>();
            }
        }
        /// <summary>
        /// Get all the GMU Modes available
        /// </summary>
        /// 
        /// <returns>list of available GMU modes</returns>
        public List<EmployeeEvents> GetGMUEvents()
        {
            string PROC = "GetGMUEvents";
            try
            {
                List<rsp_GetGMUEventsResult> lstDetails;
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                    lstDetails = context.GetGMUEvents().ToList();
                List<EmployeeEvents> lstEmployeeGroup = (from rec in lstDetails
                                                         select new EmployeeEvents
                                                         {
                                                             GMUEventDescription = rec.GMUEventDescription,
                                                             GMUEventID = rec.GMUEventID,
                                                             GMUEventGroupID = rec.GMUEventGroupID,
                                                             GMUEventName = rec.GMUEventName,
                                                             Event_Fault_Source = rec.Event_Fault_Source,
                                                             Event_Fault_Type = rec.Event_Fault_Type
                                                         }).ToList<EmployeeEvents>();
                return lstEmployeeGroup;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " " + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return new List<EmployeeEvents>();
            }
        }
        public List<EmployeeModeGroup> GetGMUModes()
        {
            string PROC = "GetGMUModes";
            try
            {
                List<rsp_GetGMUModesResult> lstDetails;
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                    lstDetails = context.GetGMUModes().ToList();
                List<EmployeeModeGroup> lstEmployeeGroup = (from rec in lstDetails
                                                            select new EmployeeModeGroup
                                                            {
                                                                GMUMode = rec.GMUMode,
                                                                GMUModeGroupID = rec.GMUModeGroupID,
                                                                GMUModeID = rec.GMUModeID,
                                                                ModeDescription = rec.GMUModedescription,
                                                                ModeName = rec.GMUModeGroupName
                                                            }).ToList<EmployeeModeGroup>();
                return lstEmployeeGroup;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " " + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return new List<EmployeeModeGroup>();
            }
        }
        public List<UserDetailsResult> LoadEmployeeName()
        {
            try
            {
                List<rsp_GetActiveUserNameResult> lstDetails;
                using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
                    lstDetails = context.GetActiveUserName().ToList();
                List<UserDetailsResult> lstEmployeeGroup = (from rec in lstDetails
                                                            select new UserDetailsResult
                                                            {
                                                                UserName = rec.UserName,
                                                                SecurityUserID = rec.SecurityUserID
                                                            }).ToList<UserDetailsResult>();
                return lstEmployeeGroup;
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return new List<UserDetailsResult>();
            }
        }
        public int InsertEmpGMUMode(string EmpInfo, string ExpInfo, int UserId, string UserName, int ModuleId, string ModuleName, string Desc, int? EmpGroupID, int? CardLevel)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.InsertEmpGMUMode(EmpInfo, ExpInfo, UserId, UserName, ModuleId, ModuleName, Desc, EmpGroupID, CardLevel);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return 0;
        }
        public int InsertEmpGMUEvents(string EmpInfo, string ExpInfo, int UserId, string UserName, int ModuleId, string ModuleName, int Desc, int? CardLevel)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.InsertEmpGMUEvent(EmpInfo, ExpInfo, UserId, UserName, ModuleId, ModuleName, Desc, CardLevel);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return 0;
        }
        /// <summary>
        /// Loads Employee Card with name in chekced list box.
        /// </summary>
        public void LoadEmployeeCards(List<EmployeeCardEntity> lstEmpCards, string EmpNumb, bool bFilter, ListBox lbxCardNumb)
        {
            try
            {
                lbxCardNumb.Items.Clear();
                string[] EmpCardDetails;
                if (!bFilter)
                    EmpCardDetails = lstEmpCards.Select(obj => obj.EmployeeCardNumber).Distinct().ToArray();
                else
                    EmpCardDetails = lstEmpCards.Where(obj => obj.EmployeeCardNumber.Contains(EmpNumb)).Select(obj => obj.EmployeeCardNumber).Distinct().ToArray();
                if (EmpCardDetails != null)
                {
                    foreach (string eCard in EmpCardDetails)
                        lbxCardNumb.Items.Add(eCard);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }



        /// <summary>
        /// Generated XML for Employee Card information based on Modes or Events value.
        /// </summary>
        /// <param name="bExport"></param>
        /// <param name="bEvents"></param>
        /// <param name="oModes"></param>
        /// <param name="oModes1"></param>
        /// <param name="lstEmpCards"></param>
        /// <param name="dicIterateItems"></param>
        /// <returns></returns>
        public XElement GetEmployeeXML(bool bExport, bool bEvents, List<EmployeeModeGroup> oModes, List<EmployeeEvents> oModes1, List<EmployeeCardEntity> lstEmpCards, Dictionary<int, List<MarkGMUEventsorModes>> dicIterateItems)
        {
            XElement oRoot = null;
            foreach (KeyValuePair<int, List<MarkGMUEventsorModes>> GMUValue in dicIterateItems)
            {
                oRoot = new XElement("EmpCardIDs");
                if (oRoot == null)
                    oRoot = new XElement("EmpCardIDs");
                XElement oCard = new XElement("Role", new XAttribute("ID", GMUValue.Key));
                if (bEvents)
                {
                    foreach (MarkGMUEventsorModes oMarkEvent in GMUValue.Value)
                    {
                        if (oMarkEvent.isDelete || oMarkEvent.isNew)
                        {
                            int iFaultType = (oModes1.Where(o => o.GMUEventID == oMarkEvent.GMUEventorModeID).Select(o => o.Event_Fault_Type)).FirstOrDefault();
                            int iFaultSource = (oModes1.Where(o => o.GMUEventID == oMarkEvent.GMUEventorModeID).Select(o => o.Event_Fault_Source)).FirstOrDefault();

                            if (bExport)
                                oCard.Add(new XElement("GMUEvent", new XAttribute("Fault_Source", iFaultSource), new XAttribute("Fault_Type", iFaultType), new XAttribute("isDelete", oMarkEvent.isDelete ? 1 : 0),
                               new XAttribute("isNew", oMarkEvent.isNew ? 1 : 0)));
                            else
                                oCard.Add(new XElement("GMUEvent", new XAttribute("ID", oMarkEvent.GMUEventorModeID), new XAttribute("isDelete", oMarkEvent.isDelete ? 1 : 0),
                               new XAttribute("isNew", oMarkEvent.isNew ? 1 : 0)));
                        }
                    }
                }
                else
                {
                    foreach (MarkGMUEventsorModes oMarkMode in GMUValue.Value)
                    {
                        if (oMarkMode.isDelete || oMarkMode.isNew)
                        {
                            string oGMUMode = (bExport) ? (oModes.Where(o => o.GMUModeID == oMarkMode.GMUEventorModeID).Select(o => o.GMUMode)).FirstOrDefault() : oMarkMode.GMUEventorModeID.ToString();
                            string oModeName = oModes.Where(o => o.GMUModeID == oMarkMode.GMUEventorModeID).Select(o => o.ModeName).FirstOrDefault();
                            oCard.Add(new XElement("GMUMode", new XAttribute("ID", oGMUMode), new XAttribute("isDelete", oMarkMode.isDelete ? 1 : 0),
                               new XAttribute("isNew", oMarkMode.isNew ? 1 : 0), new XAttribute("Permission", oModeName)));
                        }
                    }
                }
                if (oCard.FirstNode != null)
                    oRoot.Add(oCard);
                else
                    oRoot = null;
            }
            return oRoot;
        }
    }
}
