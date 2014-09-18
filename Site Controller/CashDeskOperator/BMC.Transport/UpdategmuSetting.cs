using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq.Mapping;



namespace BMC.Transport
{

  /// <summary>
    /// GetGMUPosDetails with IP,BarPostion,Status
    /// 
  /// </summary>
    public partial class GetGMUPosDetailsResult : INotifyPropertyChanged
    {

        private System.Nullable<bool> _IsChecked;

        private string _IP;

        private string _BarPostion;

        private string _Status;

        public GetGMUPosDetailsResult()
        {
        }
        /// <summary>
        /// PropertyChangedEvent for  Checkbox used for sellect all IP
        /// </summary>
        [Column(Storage = "_IsChecked", DbType = "Bit")]
        public System.Nullable<bool> IsChecked
        {
            get
            {
                return this._IsChecked;
            }
            set
            {
                if ((this._IsChecked != value))
                {
                    this._IsChecked = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
                    }
                }
            }
        }

        /// <summary>
        /// PropertyChangedEvent for  IP
        /// </summary>
        [Column(Storage = "_IP", DbType = "NVarChar(50)")]
        public string IP
        {
            get
            {
                return this._IP;
            }
            set
            {
                if ((this._IP != value))
                {
                    this._IP = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IP"));
                    }
                }
            }
        }
        /// <summary>
        /// PropertyChangedEvent for  BarPostion Message
        /// </summary>
        [Column(Storage = "_BarPostion", DbType = "VarChar(50)")]
        public string BarPostion
        {
            get
            {
                return this._BarPostion;
            }
            set
            {
                if ((this._BarPostion != value))
                {
                    this._BarPostion = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("BarPostion"));
                    }
                }
            }
        }
        /// <summary>
        /// PropertyChangedEvent for Status
        /// </summary>

        [Column(Storage = "_Status", DbType = "NVarChar(500)")]
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
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Status"));
                    }
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
    //public partial class GetGMUPosDetailsResult
    //{

    //    private System.Nullable<bool> _IsChecked;

    //    private string _IP;

    //    private string _BarPostion;

    //    private string _Status;

    //    public GetGMUPosDetailsResult()
    //    {
    //    }

    //    [Column(Storage = "_IsChecked", DbType = "Bit")]
    //    public System.Nullable<bool> IsChecked
    //    {
    //        get
    //        {
    //            return this._IsChecked;
    //        }
    //        set
    //        {
    //            if ((this._IsChecked != value))
    //            {
    //                this._IsChecked = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_IP", DbType = "NVarChar(50)")]
    //    public string IP
    //    {
    //        get
    //        {
    //            return this._IP;
    //        }
    //        set
    //        {
    //            if ((this._IP != value))
    //            {
    //                this._IP = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_BarPostion", DbType = "VarChar(50)")]
    //    public string BarPostion
    //    {
    //        get
    //        {
    //            return this._BarPostion;
    //        }
    //        set
    //        {
    //            if ((this._BarPostion != value))
    //            {
    //                this._BarPostion = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Status", DbType = "NVarChar(500)")]
    //    public string Status
    //    {
    //        get
    //        {
    //            return this._Status;
    //        }
    //        set
    //        {
    //            if ((this._Status != value))
    //            {
    //                this._Status = value;
    //            }
    //        }
    //    }
    //}

}