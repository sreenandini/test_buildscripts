using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class TransactionKeyStatus
    {
        private int _TransactionKeyId;

        private string _TransactionKey;

        private string _TransactionFlagName;

        private System.DateTime _CreatedDate;

        private System.Nullable<System.DateTime> _ExpiryDate;

        private string _Staff_First_Name;

        private string _Staff_Last_Name;

        private string _Status;

        public TransactionKeyStatus()
        {
        }
        
        public int TransactionKeyId
        {
            get
            {
                return this._TransactionKeyId;
            }
            set
            {
                if ((this._TransactionKeyId != value))
                {
                    this._TransactionKeyId = value;
                }
            }
        }

        public string TransactionKey
        {
            get
            {
                return this._TransactionKey;
            }
            set
            {
                if ((this._TransactionKey != value))
                {
                    this._TransactionKey = value;
                }
            }
        }

        public string TransactionFlagName
        {
            get
            {
                return this._TransactionFlagName;
            }
            set
            {
                if ((this._TransactionFlagName != value))
                {
                    this._TransactionFlagName = value;
                }
            }
        }

        public System.DateTime CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }

        public System.Nullable<System.DateTime> ExpiryDate
        {
            get
            {
                return this._ExpiryDate;
            }
            set
            {
                if ((this._ExpiryDate != value))
                {
                    this._ExpiryDate = value;
                }
            }
        }

        public string Staff_First_Name
        {
            get
            {
                return this._Staff_First_Name;
            }
            set
            {
                if ((this._Staff_First_Name != value))
                {
                    this._Staff_First_Name = value;
                }
            }
        }

        public string Staff_Last_Name
        {
            get
            {
                return this._Staff_Last_Name;
            }
            set
            {
                if ((this._Staff_Last_Name != value))
                {
                    this._Staff_Last_Name = value;
                }
            }
        }

        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this._Status = value;
                }
            }
        }        
    }

    public enum AuthenticationKeyType
    {
        FactoryReset = 1,
        SiteRecovery = 2,
        NewSite = 3
    }
   
}
