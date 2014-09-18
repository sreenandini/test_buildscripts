using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.Transport
{
    
    public class ApplicationLock
    {
        public List<ApplicationTypes> ApplicationType { get; set; }
        public List<LockTypes> LockType { get; set; }
    }

    public class ApplicationTypes
    {

        private string _Lock_Application;

        public ApplicationTypes()
        {
        }

        public string Lock_Application
        {
            get
            {
                return this._Lock_Application;
            }
            set
            {
                if ((this._Lock_Application != value))
                {
                    this._Lock_Application = value;
                }
            }
        }
    }

    public class LockTypes
    {

        private string _Lock_Type;

        public LockTypes()
        {
        }

        public string Lock_Type
        {
            get
            {
                return this._Lock_Type;
            }
            set
            {
                if ((this._Lock_Type != value))
                {
                    this._Lock_Type = value;
                }
            }
        }
    }

    public class LockDetails : INotifyPropertyChanged
    {

        private int _Lock_ID;

        private string _Lock_User;

        private string _Lock_Machine;

        private string _Lock_Application;

        private string _Lock_Type;

        private string _Lock_Identifier;

        private System.Nullable<System.DateTime> _Lock_Created;

        private bool _IsSelected;

        public LockDetails()
        {
        }

        public int Lock_ID
        {
            get
            {
                return this._Lock_ID;
            }
            set
            {
                if ((this._Lock_ID != value))
                {
                    this._Lock_ID = value;
                }
            }
        }

        public string Lock_User
        {
            get
            {
                return this._Lock_User;
            }
            set
            {
                if ((this._Lock_User != value))
                {
                    this._Lock_User = value;
                }
            }
        }

        public string Lock_Machine
        {
            get
            {
                return this._Lock_Machine;
            }
            set
            {
                if ((this._Lock_Machine != value))
                {
                    this._Lock_Machine = value;
                }
            }
        }

        public string Lock_Application
        {
            get
            {
                return this._Lock_Application;
            }
            set
            {
                if ((this._Lock_Application != value))
                {
                    this._Lock_Application = value;
                }
            }
        }

        public string Lock_Type
        {
            get
            {
                return this._Lock_Type;
            }
            set
            {
                if ((this._Lock_Type != value))
                {
                    this._Lock_Type = value;
                }
            }
        }

        public string Lock_Identifier
        {
            get
            {
                return this._Lock_Identifier;
            }
            set
            {
                if ((this._Lock_Identifier != value))
                {
                    this._Lock_Identifier = value;
                }
            }
        }
        
        public System.Nullable<System.DateTime> Lock_Created
        {
            get
            {
                return this._Lock_Created;
            }
            set
            {
                if ((this._Lock_Created != value))
                {
                    this._Lock_Created = value;
                }
            }
        }

        public bool IsSelected
        {
            get 
            {
                return this._IsSelected; 
            }
            set
            {
                if ((this._IsSelected != value))
                {
                    this._IsSelected = value;
                    if (PropertyChanged!=null)
                        PropertyChanged(this,new PropertyChangedEventArgs("IsSelected")); 
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
