using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using System.Collections.ObjectModel;
using BMC.Common.ExceptionManagement;
using BMC.Common;
using System.Text.RegularExpressions;
namespace BMC.EnterpriseBusiness.Business
{
    public class UserAdministrationBiz
    {

        private static UserAdministrationBiz _UserAdmin;

        private UserAdministrationBiz() { }

        public static UserAdministrationBiz CreateInstance()
        {
            if (_UserAdmin == null)
                _UserAdmin = new UserAdministrationBiz();

            return _UserAdmin;
        }
        public List<OperatorEntity> GetOperatorDetails()
        {
            List<OperatorEntity> obcoll = null;
            try
            {
                List<rsp_GetOperatorDetailsResult> OpList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    OpList = DataContext.GetOperatorDetails(null).ToList();
                }

                obcoll = (from obj in OpList
                          select new OperatorEntity
                          {
                              Operator_ID = obj.Operator_ID,
                              Operator_Name = obj.Operator_Name

                          }).ToList<OperatorEntity>();
                obcoll.Insert(0, new OperatorEntity { Operator_ID = -1, Operator_Name = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_Any"), SelectedDepot = null });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<DepotEntity> GetDepotDetails(int SupplierID)
        {
            List<DepotEntity> obcoll = null;
            try
            {
                List<rsp_GetDepotDetailsResult> DepList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DepList = DataContext.GetDepotDetails(null, (SupplierID == -1) ? (int?)null : SupplierID).ToList();
                }

                obcoll = (from obj in DepList
                          select new DepotEntity
                          {
                              Depot_ID = obj.Depot_ID,
                              Depot_Name = obj.Depot_Name
                          }).ToList<DepotEntity>();
                obcoll.Insert(0, new DepotEntity { Depot_ID = -1, Depot_Name = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_Any") });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<ServiceAreasDetailsResult> GetServiceAreasDetails(int DepotID)
        {
            List<ServiceAreasDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetServiceAreasDetailsResult> ServiceList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    ServiceList = DataContext.GetServiceAreasDetails(DepotID).ToList();
                }

                obcoll = (from obj in ServiceList
                          select new ServiceAreasDetailsResult
                          {
                              Service_Area_ID = obj.Service_Area_ID,
                              Service_Area_Name = obj.Service_Area_Name

                          }).ToList<ServiceAreasDetailsResult>();
                obcoll.Insert(0, new ServiceAreasDetailsResult { Service_Area_ID = -1, Service_Area_Name = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_Any") });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<CustomerAccessDetailsResult> GetCustomerAccessDetails()
        {
            List<CustomerAccessDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetCustomerAccessDetailsResult> CustList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    CustList = DataContext.GetCustomerAccessDetails().ToList();
                }

                obcoll = (from obj in CustList
                          orderby obj.Customer_Access_Name
                          select new CustomerAccessDetailsResult
                          {
                              Customer_Access_ID = obj.Customer_Access_ID,
                              Customer_Access_Name = obj.Customer_Access_Name

                          }).ToList<CustomerAccessDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<UserGroupDetailsResult> GetUserGroupDetails()
        {
            List<UserGroupDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetUserGroupDetailsResult> UserGroupList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    UserGroupList = DataContext.GetUserGroupDetails().ToList();
                }

                obcoll = (from obj in UserGroupList
                          select new UserGroupDetailsResult
                          {
                              User_Group_ID = obj.User_Group_ID,
                              User_Group_Name = obj.User_Group_Name

                          }).ToList<UserGroupDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<StaffNameResult> GetStaffName(int? StaffID, bool? isRep, bool? isTerminated)
        {
            List<StaffNameResult> obcoll = null;
            try
            {
                List<rsp_GetStaffNameResult> StaffDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    StaffDetails = DataContext.GetStaffName(StaffID, isRep, isTerminated).ToList();
                }

                obcoll = (from obj in StaffDetails
                          select new StaffNameResult
                          {
                              Staff_ID = obj.Staff_ID,
                              Staff_Name = obj.Staff_Last_Name + ", " + obj.Staff_First_Name,
                              Staff_Representative_Name = obj.Staff_First_Name + obj.Staff_Last_Name,

                              UserTableID = obj.UserTableID

                          }).ToList<StaffNameResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<StaffDetailsResult> GetStaffDetails(int StaffID)
        {
            List<StaffDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetStaffDetailsResult> StaffDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    StaffDetails = DataContext.GetStaffDetails(StaffID).ToList();
                }

                obcoll = (from obj in StaffDetails
                          select new StaffDetailsResult
                          {
                              Staff_ID = obj.Staff_ID,

                              Operator_ID = obj.Operator_ID,

                              User_Group_ID = obj.User_Group_ID,

                              Staff_First_Name = obj.Staff_First_Name,

                              Staff_Last_Name = obj.Staff_Last_Name,

                              Staff_Title = obj.Staff_Title,

                              Staff_Address = obj.Staff_Address,

                              Staff_Postcode = obj.Staff_Postcode,

                              Staff_Phone_No = obj.Staff_Phone_No,

                              Staff_Extension_No = obj.Staff_Extension_No,

                              Staff_Mobile_No = obj.Staff_Mobile_No,

                              Staff_Job_Title = obj.Staff_Job_Title,

                              Staff_Department = obj.Staff_Department,

                              Staff_IsACollector = obj.Staff_IsACollector,

                              Staff_IsAnEngineer = obj.Staff_IsAnEngineer,

                              Staff_IsARepresentative = obj.Staff_IsARepresentative,

                              Staff_IsAStockController = obj.Staff_IsAStockController,

                              Staff_Start_Date = obj.Staff_Start_Date,

                              Staff_End_Date = obj.Staff_End_Date,

                              Staff_Username = obj.Staff_Username,

                              Staff_Password = obj.Staff_Password,

                              Depot_ID = obj.Depot_ID,

                              Service_Area_ID = obj.Service_Area_ID,

                              Supplier_ID = obj.Supplier_ID,

                              Staff_Personel_No = obj.Staff_Personel_No,

                              Staff_Terminated = obj.Staff_Terminated,

                              Staff_Notes = obj.Staff_Notes,

                              Email_Address = obj.Email_Address,

                              UserTableID = obj.UserTableID,

                          }).ToList<StaffDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<UserLanguagesDetailsResult> GetUserLangDetails()
        {
            List<UserLanguagesDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetUserLanguagesDetailsResult> UserLangList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    UserLangList = DataContext.GetUserLanguagesDetails().ToList();
                }

                obcoll = (from obj in UserLangList
                          select new UserLanguagesDetailsResult
                          {
                              LanguageID = obj.LanguageID,
                              LanguageName = obj.LanguageName,
                              LanguageDisplayName = ResourceExtensions.GetResourceTextByKey(null, "Key_DBV_" + Regex.Replace(obj.LanguageName.Trim(), "[^0-9a-zA-Z]+", "")),

                          }).ToList<UserLanguagesDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<UserDetailsResult> GetUserDetails(int? UserID)
        {
            List<UserDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetUserDetailsResult> UserLangList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    UserLangList = DataContext.GetUserDetails(UserID).ToList();
                }

                obcoll = (from obj in UserLangList
                          select new UserDetailsResult
                          {
                              WindowsUserName = obj.WindowsUserName,
                              LanguageID = obj.LanguageID,
                              CurrencyCulture = obj.CurrencyCulture,
                              DateCulture = obj.DateCulture,
                              UserName = obj.UserName,
                              isLocked = obj.isLocked,
                              isReset = obj.isReset,

                          }).ToList<UserDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<StaffCustomerAccessResult> GetStaffCustomerAccess(int StaffID)
        {
            List<StaffCustomerAccessResult> obcoll = null;
            try
            {
                List<rsp_GetStaffCustomerAccessResult> StaffCustDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    StaffCustDetails = DataContext.GetStaffCustomerAccess(StaffID).ToList();
                }

                obcoll = (from obj in StaffCustDetails
                          select new StaffCustomerAccessResult
                          {
                              Customer_Access_ID = obj.Customer_Access_ID,
                              Staff_Customer_Access_ID = obj.Staff_Customer_Access_ID
                          }).ToList<StaffCustomerAccessResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<OperatorEntity> GetOperatorandDepotDetails()
        {
            List<OperatorEntity> obcoll = new List<OperatorEntity>();
            try
            {
                List<rsp_GetOperatorandDepotDetailsResult> OptDepotList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    OptDepotList = DataContext.GetOperatorandDepotDetails().ToList();
                }

                OperatorEntity op = null;

                if (OptDepotList != null)
                {
                    OptDepotList.ForEach((r) =>
                    {
                        if (op == null || op.Operator_ID != r.Operator_ID)
                        {
                            op = new OperatorEntity()
                            {
                                Operator_ID = r.Operator_ID,
                                Operator_Name = r.Operator_Name
                            };
                            obcoll.Add(op);
                        }

                        if (r.Depot_ID != null && !string.IsNullOrEmpty(r.Depot_Name))
                        {
                            op.Depots.Add(new DepotEntity()
                            {
                                Depot_ID = Convert.ToInt32(r.Depot_ID),
                                Depot_Name = r.Depot_Name
                            });
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<StaffDepotResult> GetStaffDepot(int StaffID)
        {
            List<StaffDepotResult> obcoll = null;
            try
            {
                List<rsp_GetStaffDepotResult> StaffDepotDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    StaffDepotDetails = DataContext.GetStaffDepot(StaffID).ToList();
                }

                obcoll = (from obj in StaffDepotDetails
                          select new StaffDepotResult
                          {
                              Depot_ID = obj.Depot_ID,
                          }).ToList<StaffDepotResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public bool UpdateStaffCustomerAccess(int StaffID, int Customer_Access_ID)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.UpdateStaffCustomerAccess(StaffID, Customer_Access_ID) == 0)
                    {
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public int UpdateUserDetails(string WindowsUserName, string Password, string UserName, int LanguageID, int CurrencyCulture, int DateCulture, ref int? SecurityUserID)
        {
            int retVal = 0;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retVal = DataContext.UpdateUserDetails(WindowsUserName, Password, UserName, LanguageID, CurrencyCulture, DateCulture, ref SecurityUserID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public bool AddModifyStaffDetails(int User_Group_ID, string Staff_First_Name, string Staff_Last_Name
           , string Staff_Title, string Staff_Address, string Staff_Postcode
           , string Staff_Phone_No, string Staff_Extension_No, string Staff_Mobile_No
           , string Staff_Job_Title, string Staff_Department, bool Staff_IsAnEngineer
           , bool Staff_IsARepresentative, bool Staff_IsAStockController, string Staff_Start_Date
           , string Staff_End_Date, string Staff_Username, string Staff_Password
           , int Depot_ID, int Service_Area_ID, int Supplier_ID
           , string Staff_Personel_No, bool Staff_Terminated, string Staff_Notes
           , string Email_Address, int UserTableID, ref int? StaffID)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retVal = (DataContext.AddModifyStaffDetails(User_Group_ID
                       , Staff_First_Name
                       , Staff_Last_Name
                       , Staff_Title
                       , Staff_Address
                       , Staff_Postcode
                       , Staff_Phone_No
                       , Staff_Extension_No
                       , Staff_Mobile_No
                       , Staff_Job_Title
                       , Staff_Department
                       , Staff_IsAnEngineer
                       , Staff_IsARepresentative
                       , Staff_IsAStockController
                       , Staff_Start_Date
                       , Staff_End_Date
                       , Staff_Username
                       , Staff_Password
                       , Depot_ID
                       , Service_Area_ID
                       , Supplier_ID
                       , Staff_Personel_No
                       , Staff_Terminated
                       , Staff_Notes
                       , Email_Address
                       , UserTableID
                       , ref StaffID) >= 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public bool UpdateStaffDepot(int StaffID, int Depot_ID)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retVal = (DataContext.UpdateStaffDepot(StaffID, Depot_ID) >= 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        public bool UpdateRoleAccess(int SecurityUserID, string UserLevelName)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retVal = (DataContext.UpdateRoleAccess(SecurityUserID, UserLevelName) >= 0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public List<UserLockStatusResult> UserLockStatus(int? StaffID, int? isLocked)
        {
            List<UserLockStatusResult> obcoll = null;
            try
            {
                List<usp_userlockstatusResult> LockStatusDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    LockStatusDetails = DataContext.UserLockStatus(StaffID, isLocked).ToList();
                }

                obcoll = (from obj in LockStatusDetails
                          select new UserLockStatusResult
                          {
                              RESULT = obj.RESULT
                          }).ToList<UserLockStatusResult>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return obcoll;
        }

        public int CheckUserNameAlreadyExists(string strUserName, int iStaffId)
        {
            int result = -1;
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = Datacontext.CheckUserNameAlreadyExists(strUserName, iStaffId).Count();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }
    }
}
