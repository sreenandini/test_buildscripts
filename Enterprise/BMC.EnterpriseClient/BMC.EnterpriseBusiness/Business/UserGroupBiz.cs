using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using System.Data.Linq;
using System.Collections;
using BMC.EnterpriseBusiness.Entities;
using System.Collections.ObjectModel;
using BMC.Common.LogManagement;

namespace BMC.EnterpriseBusiness.Business
{
    /// <summary>
    /// UserGroupBiz
    /// User Group business logics
    /// </summary>
    public class UserGroupBiz
    {
        #region Local Declaration

        private static UserGroupBiz _UserGroup;

        #endregion Local Declaration

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns>UsergroupBiz Object</returns>
        public static UserGroupBiz CreateInstance()
        {
            if (_UserGroup == null)
                _UserGroup = new UserGroupBiz();

            return _UserGroup;
        }

        /// <summary>
        /// GetUserGroup
        /// Gets the user groups from DataAccess
        /// </summary>
        /// <returns>UserGroup List object</returns>
        public List<UserGroup> GetUserGroup()
        {
            List<UserGroup> lstRetUserGroup = null;
            try
            {
                List<rsp_getUserGroupResult> lstUserGroup;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstUserGroup = DataContext.GetUserGroup().ToList();
                }
                lstRetUserGroup = (from Records in lstUserGroup
                                   select new UserGroup
                                   {
                                       User_Group_ID = Records.User_Group_ID,
                                       User_Group_Name = Records.User_Group_Name
                                   }).ToList<UserGroup>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetUserGroup;
        }

        public void UpdateSuperUser(int _UserId, ref System.Nullable<bool> isSuperUser)
        {
            try
            {
                bool isSuperUservalue = false;
                EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext();
                DataContext.usp_Updatesuperuser(_UserId,ref isSuperUser);
                isSuperUservalue = Convert.ToBoolean(isSuperUser);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

       }

        /// <summary>
        /// User_Access
        /// Gets the User_Access from DataAccess.
        /// </summary>
        /// <param name="iGroupID">INT</param>
        /// <returns>User_Access List object</returns>
        public List<User_Access> GetUser_Access(int iGroupID)
        {
            List<User_Access> lstRetUser_Access = null;
            try
            {
                List<rsp_getUser_AccessResult> lstUser_Access;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstUser_Access = DataContext.GetUser_Access(iGroupID).ToList();
                }
                    lstRetUser_Access = (from Records in lstUser_Access
                                         select new User_Access
                                         {
                                             User_Group_ID = Records.User_Group_ID,
                                             Access_Id = Records.Access_Id,
                                             Access_Name = Records.Access_Name,
                                             Access_Key = Records.Access_Key,
                                             Access_Parent_Id = Records.Access_Parent_Id,
                                             Access_Value = Records.Access_Value
                                         }).ToList<User_Access>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetUser_Access;
        }

        /// <summary>
        /// NewUserGroup
        /// Sends the new user group to DataAccess
        /// </summary>
        /// <param name="strGroupName">New Group Name</param>
        /// <returns>Returns [0/1] *0-Already same user group name exist</returns>
        public int NewUserGroup(string strGroupName)
        {
            int? iResult = 0;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.NewUserGroup(strGroupName, ref iResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (int)iResult;
        }

        /// <summary>
        /// Updates the User_Access
        /// Sends the GroupID and XML along with all the Roles and its value to DataAccess
        /// </summary>
        /// <param name="iGroupID">INT</param>
        /// <param name="strXMLDoc">XML doc with User_Access values</param>
        /// <returns>[-1] -Error in updating User_Access</returns>
        public int UpdateUser_Access(int iGroupID, string strXMLDoc)
        {
            int? iResult = 0;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.UpdateUser_Access(iGroupID, strXMLDoc, ref iResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (int)iResult;
        }
    }
}
