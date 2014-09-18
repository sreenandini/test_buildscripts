using System.ComponentModel;

namespace BMC.Transport
{
    public class UnExportedData : INotifyPropertyChanged
    {

        private int _ID;

        private System.Nullable<System.DateTime> _Date;

        private string _Reference;

        private string _ExportType;

        private string _Status;

        private bool _IsSelected;

        public UnExportedData()
        {
        }

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        public System.Nullable<System.DateTime> Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this._Date = value;
                }
            }
        }

        public string Reference
        {
            get
            {
                return this._Reference;
            }
            set
            {
                if ((this._Reference != value))
                {
                    this._Reference = value;
                }
            }
        }

        public string ExportType
        {
            get
            {
                return this._ExportType;
            }
            set
            {
                if ((this._ExportType != value))
                {
                    this._ExportType = value;
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

        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if ((this._IsSelected != value))
                {
                    this._IsSelected = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class StatusType
    {

        private int _Type;

        private string _Description;

        public StatusType()
        {
        }

        public int Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this._Type = value;
                }
            }
        }
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this._Description = value;
                }
            }
        }
    }
}
