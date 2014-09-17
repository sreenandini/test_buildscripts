using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.EnterpriseBusiness.Entities
{
    public class UserEntity : DisposableObject
    {
        public UserEntity() { }

        public int SecurityUserID { get; set; }
        public string WindowsUserName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int LanguageID { get; set; }
        public int CurrencyCulture { get; set; }
        public int DateCulture { get; set; }
        public bool ChangePassword { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PasswordChangeDate { get; set; }
        public bool IsReset { get; set; }
        public bool IsLocked { get; set; }
        public int StaffID { get; set; }
        public string RoleName { get; set; }
        public int SecurityRoleID { get; set; }
    }
}
