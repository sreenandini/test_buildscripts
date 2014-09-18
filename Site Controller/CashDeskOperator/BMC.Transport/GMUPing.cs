using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.ComponentModel;


namespace BMC.Transport
{

    public partial class GMUListtoPing : INotifyPropertyChanged
    {

        private string _GMUNo;

        private string _IPAddress;
        private string _Status = string.Empty;
        private bool _IsEnabled;
        private string  _TextColor;

        public GMUListtoPing()
        {
            _IsEnabled = true;
        }

        [Column(Storage = "_GMUNo", DbType = "VarChar(50)")]
        public string GMUNo
        {
            get
            {
                return this._GMUNo;
            }
            set
            {
                if ((this._GMUNo != value))
                {
                    this._GMUNo = value;
                }
            }
        }

        [Column(Storage = "_IPAddress", DbType = "VarChar(50)")]
        public string IPAddress
        {
            get
            {
                return this._IPAddress;
            }
            set
            {
                if ((this._IPAddress != value))
                {
                    this._IPAddress = value;
                }
            }
        }

        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
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
                _IsEnabled = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsEnabled"));
            }
        }
        public string  StatusColor
        { 
            get
            {
                return _TextColor;
            }
            set 
            {
                _TextColor = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("StatusColor"));
            } 
        }
  

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
