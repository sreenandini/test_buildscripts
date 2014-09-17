using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.Security.Entity;
using BMC.Security.Interfaces;

namespace BMC.Security.Manager
{
    public class UserManager : System.Data.Linq.DataContext
    {
        public UserManager(string connectionString)
            : base(connectionString)
        {

        }

        public IUser GetUserObject(string userName, string password, string windowsUserName)
        {
            return new User { UserName = userName, Password = password, WindowsUserName = windowsUserName };
        }

        public bool CreateUser(string userName, string password, string windowsUserName)
        {
            var user = new User { UserName = userName, Password = password, WindowsUserName = windowsUserName };

            foreach (var userResult in CreateNewUser(user.UserName, user.Password, user.WindowsUserName))
                return (userResult.Result == "SUCCESS");

            return false;
        }

        public bool DeleteUser(IUser user, IUser deletedBy)
        {
            foreach (var userResult in RemoveUser(user.UserName, deletedBy.SecurityUserID))
                return (userResult.Result == "SUCCESS");

            return false;
        }

        public IUser GetUserProfileById(int userID)
        {
            if (userID == 0)
                throw new ArgumentNullException("Invalid UserID passed to function");

            foreach (var userResult in GetUser(userID, ""))
                return userResult;

            return null;
        }

        public IUser GetUserProfileByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("Invalid Username passed to function");

            foreach (var userResult in GetUser(0, userName))
                return userResult;

            return null;
        }

        public IEnumerable<IUser> GetAllUser()
        {
            foreach (var item in GetUser(0, ""))
                yield return item;
        }

        public bool ChangePassword(IUser user)
        {
            foreach (var result in ChangePassword(user.SecurityUserID, user.Password))
                return (result.Result == "SUCCESS");

            return false;
        }

        public bool LockUser(IUser user)
        {
            foreach (var Obj in LockUser(user.SecurityUserID))
                return (Obj.Result == "SUCCESS");

            return false;
        }

        public bool ResetPassword(IUser user)
        {
            foreach (var Obj in ResetPassword(user.SecurityUserID, user.Password))
                return (Obj.Result == "SUCCESS");

            return false;
        }

        #region Private Methods
        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.CreateNewUser")]
        private ISingleResult<SProcResult> CreateNewUser([Parameter(DbType = "VarChar(200)")] string username, [Parameter(DbType = "VarChar(200)")] string password, [Parameter(DbType = "VarChar(200)")] string windowsUserName)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), username, password, windowsUserName);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }
        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.ASPNET_SP_RemoveUser")]
        private ISingleResult<SProcResult> RemoveUser([Parameter(Name = "Username", DbType = "VarChar(200)")] string username, [Parameter(Name = "DeletedUserID", DbType = "Int")] System.Nullable<int> deletedUserID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), username, deletedUserID);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }
        [ResultType(typeof(User))]
        [Function(Name = "dbo.ASPNET_SP_GetUser")]
        private ISingleResult<User> GetUser([Parameter(Name = "ID", DbType = "Int")] System.Nullable<int> iD, [Parameter(Name = "Name", DbType = "VarChar(200)")] string name)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), iD, name);
            return ((ISingleResult<User>)(result.ReturnValue));
        }

        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.ASPNET_SP_ChangePassword")]
        private ISingleResult<SProcResult> ChangePassword([Parameter(Name = "SecurityUserID", DbType = "Int")] System.Nullable<int> securityUserID, [Parameter(DbType = "VarChar(200)")] string password)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), securityUserID, password);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }

        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.ASPNET_SP_LockUser")]
        private ISingleResult<SProcResult> LockUser([Parameter(Name = "SecurityUserID", DbType = "Int")] System.Nullable<int> securityUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), securityUserID);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ASPNET_SP_ResetPassword")]
        private ISingleResult<SProcResult> ResetPassword([Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [Parameter(Name = "Password", DbType = "VarChar(200)")] string password)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, password);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }



        //[Function(Name = "dbo.usp_UpdateUser_isLocked")]
        //private int ResetUserPassword([Parameter(Name = "UserID", DbType = "VarChar(5)")] string userID, [Parameter(Name = "Password", DbType = "VarChar(200)")] string password, [Parameter(Name = "Staff_Password", DbType = "VarChar(50)")] string staff_Password)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, password, staff_Password);
        //    return ((int)(result.ReturnValue));
        //}

        //[Function(Name = "dbo.ASPNET_SP_ChangePassword_STAFF")]
        //private int ChangePassword_STAFF([Parameter(Name = "SecurityuserID", DbType = "Int")] System.Nullable<int> securityuserID, [Parameter(Name = "Staff_Password", DbType = "VarChar(50)")] string staff_Password)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), securityuserID, staff_Password);
        //    return ((int)(result.ReturnValue));
        //}

        #endregion
    }

    internal class SProcResult
    {

        private string _RESULT;

        [Column(Storage = "_RESULT", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
        public string Result
        {
            get
            {
                return _RESULT;
            }
            set
            {
                if ((_RESULT != value))
                {
                    _RESULT = value;
                }
            }
        }
    }
}