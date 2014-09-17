using System.Data.Linq.Mapping;

namespace BMC.Security.Entity
{
    internal class RoleAccess:Interfaces.IRoleAccess
    {

        private int _RoleAccessID;

        private string _ParentName;

        private int? _ParentID;

        private string _RoleAccessName;

        private string _Description;
		

        #region IRoleAccess Members
        [Column(Storage = "_RoleAccessID", DbType = "Int NOT NULL")]
        public int RoleAccessID
        {
            get
            {
                return _RoleAccessID;
            }
            set
            {
                _RoleAccessID = value;
            }
        }
        [Column(Storage = "_ParentID", DbType = "Int")]
        public int? ParentID
        {
            get
            {
                return _ParentID;
            }
            set
            {
                _ParentID = value;
            }
        }
        [Column(Storage = "_RoleAccessName", DbType = "VarChar(200)")]
        public string RoleAccessName
        {
            get
            {
                return _RoleAccessName;
            }
            set
            {
                _RoleAccessName = value;
            }
        }
        [Column(Storage = "_Description", DbType = "VarChar(200)")]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }
        [Column(Storage = "_ParentName", DbType = "VarChar(200)")]
        public string ParentName
        {
            get
            {
                return _ParentName;
            }
            set
            {
                _ParentName = value;
            }
        }

        #endregion
    }
}
