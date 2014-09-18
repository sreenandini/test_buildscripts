using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.Transport
{
    public class AGSdetails :INotifyPropertyChanged
    {

        private string _AssetNo;

        private string _BarPositionNo;

        private System.Nullable<int> _Installation_No;

        private string _ActualAssetNo;

        private string _GMUNo;

       

        private string _SerialNo;

        private bool _IsEnabled;
        private bool _IsFilterEnabled;

        private string _EditSave;

        public AGSdetails()
        {
        }
        public int MachineID
        {
            get;
            set;
        }
       
        public string AssetNo
        {
            get
            {
                return this._AssetNo.Trim();
            }
            set
            {
                if ((this._AssetNo != value))
                {
                    this._AssetNo = value;
                }
            }
        }

        public string OldGMUNo
        {
            get;
            set;
        }
       
        public string BarPositionNo
        {
            get
            {
                return this._BarPositionNo.Trim();
            }
            set
            {
                if ((this._BarPositionNo != value))
                {
                    this._BarPositionNo = value;
                }
            }
        }

        
        public System.Nullable<int> Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        public bool IsNull
        {
            get;
            set;
        }
        
        public string ActualAssetNo
        {
            get
            {
                return this._ActualAssetNo.Trim();
            }
            set
            {
                if ((this._ActualAssetNo != value))
                {
                    this._ActualAssetNo = value;
                }
            }
        }
       
        public string GMUNo
        {
            get
            {
                return this._GMUNo.Trim();
            }
            set
            {
                if ((this._GMUNo != value))
                {
                    this._GMUNo = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GMUNo")); 
                }
            }
        }
        
        public string SerialNo
        {
            get
            {
                return this._SerialNo;
            }
            set
            {
                if ((this._SerialNo != value))
                {
                    this._SerialNo = value;
                   
                }
            }
        }

        public string EditSave
        {
            get
            {
                return _EditSave;
            }
            set
            {
                if (this._EditSave != value)
                {
                    this._EditSave = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("EditSave")); 
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _IsEnabled;
            }
            set
            {
                if (this._IsEnabled != value)
                {
                    this._IsEnabled = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
        }

        public bool IsFilterEnabled
        {
            get
            {
                return _IsFilterEnabled;
            }
            set
            {
                if (this._IsFilterEnabled != value)
                {
                    this._IsFilterEnabled = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("IsFilterEnabled"));
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
