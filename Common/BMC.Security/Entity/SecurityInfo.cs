using System.Data.Linq.Mapping;
using BMC.Security.Enums;

namespace BMC.Security.Entity
{
    internal class AuthorizationInfo
    {
        private string _ObjectName;

        private System.Nullable<int> _ParentID;

        private System.Nullable<int> _ObjectType;

        private int _Right;


        internal AuthorizationInfo() { }


        [Column(Storage = "_ObjectName", DbType = "VarChar(200)")]
        public string ObjectName
        {
            get
            {
                return _ObjectName;
            }
            set
            {
                if ((_ObjectName != value))
                {
                    _ObjectName = value;
                }
            }
        }

        [Column(Storage = "_ParentID", DbType = "Int")]
        public System.Nullable<int> ParentID
        {
            get
            {
                return _ParentID;
            }
            set
            {
                if ((_ParentID != value))
                {
                    _ParentID = value;
                }
            }
        }

        [Column(Storage = "_ObjectType", DbType = "Int")]
        public System.Nullable<int> ObjectType
        {
            get
            {
                return _ObjectType;
            }
            set
            {
                if ((_ObjectType != value))
                {
                    _ObjectType = value;
                }
            }
        }

        [Column(Name = "[Right]", Storage = "_Right", DbType = "Int NOT NULL")]
        public int Right
        {
            get
            {
                return _Right;
            }
            set
            {
                if ((_Right != value))
                {
                    _Right = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [Column(Name = "ObjectType", Storage = "_ObjectType", DbType = "Int")]
        public ObjectType ObjectValueType
        {
            get { return (ObjectType)_ObjectType; }
            set { _ObjectType = (int)value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [Column(Name = "Right", Storage = "_Right", DbType = "Int")]
        public RightType RightType
        {
            get { return (RightType)_Right; }
            set { _Right = (int)value; }
        }
    }
}