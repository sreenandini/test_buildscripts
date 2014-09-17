namespace BMC.Security.Entity
{
    using Interfaces;
    using System.Data.Linq.Mapping;

    internal class Role : IRole
    {
        private string _roleDescription;
        private string _roleName;
        private int _securityRoleID;

        [Column(Storage="_roleDescription", DbType="VarChar(300)")]
        public string RoleDescription
        {
            get
            {
                return _roleDescription;
            }
            set
            {
                _roleDescription = value;
            }
        }

        [Column(Storage="_roleName", DbType="VarChar(100)")]
        public string RoleName
        {
            get
            {
                return _roleName;
            }
            set
            {
                _roleName = value;
            }
        }

        [Column(Storage="_securityRoleID", DbType="Int NOT NULL")]
        public int SecurityRoleID
        {
            get
            {
                return _securityRoleID;
            }
            set
            {
                _securityRoleID = value;
            }
        }
    }
}

