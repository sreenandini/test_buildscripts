using System;
using System.Text;
using System.Xml;

namespace BMC.EnterpriseBusiness.Entities
{
    /// <summary>
    /// UserGroup
    /// Holds the user group details
    /// </summary>
    public class UserGroup
    {
        private int _User_Group_ID;

        private string _User_Group_Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserGroup"/> class.
        /// </summary>
        public UserGroup()
        {
        }

        /// <summary>
        /// Property
        /// Gets or sets the User_Group_ID
        /// </summary>
        /// <value>
        /// User_Group_ID
        /// </value>
        public int User_Group_ID
        {
            get
            {
                return this._User_Group_ID;
            }
            set
            {
                if ((this._User_Group_ID != value))
                {
                    this._User_Group_ID = value;
                }
            }
        }

        /// <summary>
        /// Property
        /// Gets or sets the User_Group_Name
        /// </summary>
        /// <value>
        /// User_Group_Name
        /// </value>
        public string User_Group_Name
        {
            get
            {
                return this._User_Group_Name;
            }
            set
            {
                if ((this._User_Group_Name != value))
                {
                    this._User_Group_Name = value;
                }
            }
        }
    }

    /// <summary>
    /// User_Access
    /// Holds User access settings and its values from DB
    /// </summary>
    public class User_Access
    {

        private int _User_Group_ID;

        private int _Access_Id;

        private string _Access_Key;

        private string _Access_Name;

        private int _Access_Parent_Id;

        private bool _Access_Value;

        private bool _isModified;

        /// <summary>
        /// Initializes a new instance of the <see cref="User_Access"/> class.
        /// </summary>
        public User_Access()
        {
        }

        /// <summary>
        /// Property
        /// Gets or sets the user_ group_ ID
        /// </summary>
        /// <value>
        /// User_ group_ID
        /// </value>
        public int User_Group_ID
        {
            get
            {
                return this._User_Group_ID;
            }
            set
            {
                if ((this._User_Group_ID != value))
                {
                    this._User_Group_ID = value;
                }
            }
        }

        /// <summary>
        /// Property
        /// Gets or sets the Access_Id
        /// </summary>
        /// <value>
        /// Access_Id.
        /// </value>
        public int Access_Id
        {
            get
            {
                return this._Access_Id;
            }
            set
            {
                if ((this._Access_Id != value))
                {
                    this._Access_Id = value;
                }
            }
        }

        /// <summary>
        /// Property
        /// Gets or sets the Access_Key
        /// </summary>
        /// <value>
        /// Access_Key
        /// </value>
        public string Access_Key
        {
            get
            {
                return this._Access_Key;
            }
            set
            {
                if ((this._Access_Key != value))
                {
                    this._Access_Key = value;
                }
            }
        }

        /// <summary>
        /// Property
        /// Gets or sets the Access_Name
        /// </summary>
        /// <value>
        /// Access_Name
        /// </value>
        public string Access_Name
        {
            get
            {
                return this._Access_Name;
            }
            set
            {
                if ((this._Access_Name != value))
                {
                    this._Access_Name = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Access_Parent_Id
        /// </summary>
        /// <value>
        /// The access_ parent_ id
        /// </value>
        public int Access_Parent_Id
        {
            get
            {
                return this._Access_Parent_Id;
            }
            set
            {
                if ((this._Access_Parent_Id != value))
                {
                    this._Access_Parent_Id = value;
                }
            }
        }

        /// <summary>
        /// Property
        /// Gets or sets a Access_Value
        /// </summary>
        /// <value>
        /// Access_Value
        /// </value>
        public bool Access_Value
        {
            get
            {
                return this._Access_Value;
            }
            set
            {
                if ((this._Access_Value != value))
                {
                    this._Access_Value = value;
                    this.Access_Value_New = value;
                }
            }
        }

        public bool Access_Value_New { get; set; }

        public bool IsModified
        {
            get
            {
                return (_Access_Value != this.Access_Value_New);
            }
        }

        /// <summary>
        /// Property
        /// Gets or sets when User_acess is Modified
        /// </summary>
        /// <value>
        /// Modified
        /// </value>
        public bool Modified
        {
            get
            {
                return this._isModified;
            }
            set
            {
                if ((this._isModified != value))
                {
                    this._isModified = value;
                }
            }
        }
    }

    /// <summary>
    /// User_Role_Item
    /// Used to build XML for the treeview control
    /// </summary>
    /// <typeparam name="T">Takes TreeNode class</typeparam>
    public class User_Role_Item<T> where T : class
    {
        static XmlWriterSettings settings = null;

        /// <summary>
        /// Initializes the <see cref="User_Role_Item&lt;T&gt;"/> class.
        /// </summary>
        static User_Role_Item()
        {
            settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User_Role_Item&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="name">Node name</param>
        /// <param name="node">Single node</param>
        /// <param name="getValue">Gets the status of Check box</param>
        public User_Role_Item(User_Access userAccess, T node,
            Func<T, string> getValue)
        {
            this.UserAccess = userAccess;
            this.Name = userAccess.Access_Key;
            this.Node = node;
            this.GetValue = getValue;
        }

        /// <summary>
        /// Property
        /// Gets the node name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Property
        /// Gets the tree node.
        /// </summary>
        public T Node { get; private set; }

        public User_Access UserAccess { get; private set; }

        /// <summary>
        /// Property
        /// Gets the get check box value
        /// </summary>
        public Func<T, string> GetValue { get; private set; }

        /// <summary>
        /// Builds the XML for based on Treeview nodes
        /// </summary>
        /// <returns>XML string with RoleName as element and its Value</returns>
        public string GetXmlValue()
        {
            StringBuilder sbXMLDoc = new StringBuilder();
            using (XmlWriter xwXMLDoc = XmlWriter.Create(sbXMLDoc, settings))
            {
                xwXMLDoc.WriteStartElement(this.Name);
                xwXMLDoc.WriteValue(this.GetValue(this.Node));
                xwXMLDoc.WriteEndElement();
                xwXMLDoc.Close();
            }
            return sbXMLDoc.ToString();
        }
    }
}
