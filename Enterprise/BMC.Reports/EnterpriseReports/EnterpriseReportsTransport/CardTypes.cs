using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseReportsTransport
{
    public partial class CardTypes
    {
        public CardTypes()
        {
        }
        public int EmpCardTypeId
        { get; set; }

        public string EmpCardType
        { get; set; }

        public string EmpCardDisplay
        { get; set; }
    }

    public partial class UserRoles
    {
        public UserRoles()
        {
        }
        public int SecurityRoleID
        { get; set; }

        public string RoleName
        { get; set; }
        public string RoleDescription
        { get; set; }
    }
}
