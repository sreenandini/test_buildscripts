using System.Data.Linq.Mapping;
using BMC.Security.Interfaces;

namespace BMC.Security.Entity
{
    internal class User : IUser
    {

        private int _securityUserID;

        private string _windowsUserName;

        private string _userName;

        private string _password;

        private string _CultureInfo;

        private string _CurrencyCulture;

        private string _DateCulture;

        private System.DateTime _CreatedDate;

        private System.DateTime _PasswordChangeDate;

        private bool _isReset;

        private bool _isLocked;

        private string _FirstName;

        private string _LastName;

        private int _UserNo;

        private bool? _IsUserTerminated;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        internal User() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        public User(int userID)
        {
            SecurityUserID = userID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public User(string userName, string password,int UserNo)
        {
            UserName = userName;
            Password = password;
            User_No = UserNo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        public User(string domainName)            
        {
            WindowsUserName = domainName;
        }

        #endregion

        

        #region Properties

        [Column(Storage = "_securityUserID", DbType = "Int NOT NULL")]
        public int SecurityUserID
        {
            get
            {
                return _securityUserID;
            }
            set
            {
                if ((_securityUserID != value))
                {
                    _securityUserID = value;
                }
            }
        }

        [Column(Storage = "_windowsUserName", DbType = "VarChar(200)")]
        public string WindowsUserName
        {
            get
            {
                return _windowsUserName;
            }
            set
            {
                if ((_windowsUserName != value))
                {
                    _windowsUserName = value;
                }
            }
        }

        [Column(Storage = "_userName", DbType = "VarChar(200)")]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if ((_userName != value))
                {
                    _userName = value;
                }
            }
        }

        [Column(Storage = "_password", DbType = "VarChar(200)")]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if ((_password != value))
                {
                    _password = CryptoHelper.CreateHash(value);
                }
            }
        }


        [Column(Storage = "_CultureInfo", DbType = "VarChar(10)")]
        public string CultureInfo
        {
            get
            {
                return _CultureInfo;
            }
            set
            {
                if ((_CultureInfo != value))
                {
                    _CultureInfo = value;
                }
            }
        }

        [Column(Storage = "_CurrencyCulture", DbType = "VarChar(10)")]
        public string CurrencyCulture
        {
            get
            {
                return _CurrencyCulture;
            }
            set
            {
                if ((_CurrencyCulture != value))
                {
                    _CurrencyCulture = value;
                }
            }
        }

        [Column(Storage = "_DateCulture", DbType = "VarChar(10)")]
        public string DateCulture
        {
            get
            {
                return _DateCulture;
            }
            set
            {
                if ((_DateCulture != value))
                {
                    _DateCulture = value;
                }
            }
        }

        [Column(Storage = "_CreatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                if ((_CreatedDate != value))
                {
                    _CreatedDate = value;
                }
            }
        }

        [Column(Storage = "_PasswordChangeDate", DbType = "DateTime NOT NULL")]
        public System.DateTime PasswordChangeDate
        {
            get
            {
                return _PasswordChangeDate;
            }
            set
            {
                if ((_PasswordChangeDate != value))
                {
                    _PasswordChangeDate = value;
                }
            }
        }

        [Column(Storage = "_isReset", DbType = "Bit NOT NULL")]
        public bool isReset
        {
            get
            {
                return _isReset;
            }
            set
            {
                if ((_isReset != value))
                {
                    _isReset = value;
                }
            }
        }

        [Column(Storage = "_isLocked", DbType = "Bit NOT NULL")]
        public bool isLocked
        {
            get
            {
                return this._isLocked;
            }
            set
            {
                if ((this._isLocked != value))
                {
                    this._isLocked = value;
                }
            }
        }

        [Column(Storage = "_FirstName", DbType = "VarChar(50)")]
        public string First_Name
        {
            get
            {
                return _FirstName;
            }
            set
            {
                if ((_FirstName != value))
                {
                    _FirstName = value;
                }
            }
        }

        [Column(Storage = "_LastName", DbType = "VarChar(50)")]
        public string Last_Name
        {
            get
            {
                return _LastName;
            }
            set
            {
                if ((_LastName != value))
                {
                    _LastName = value;
                }
            }
        }

        [Column(Storage = "_UserNo", DbType = "int")]
        public int User_No
        {
            get
            {
                return _UserNo;
            }
            set
            {
                if ((_UserNo != value))
                {
                    _UserNo = value;
                }
            }
        }

        [Column(Storage = "_IsUserTerminated", DbType = "BIT")]
        public bool? IsUserTerminated
        {
            get
            {
                return _IsUserTerminated;
            }
            set
            {
                if ((_IsUserTerminated != value))
                {
                    _IsUserTerminated = value;
                }
            }
        }

        #endregion

        #region IUser Members

        public string DisplayName
        {
            get { return string.Format("{0} , {1}", First_Name, Last_Name); }
            private set { }
        }

        #endregion
    }
}