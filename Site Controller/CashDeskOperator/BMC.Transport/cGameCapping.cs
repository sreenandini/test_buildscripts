using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public partial class GameCapDetails : INotifyPropertyChanged
    {
        private int _GameCappingID;

        private int _InstallationNo;

        private string _Position;

        private string _ReservedBy;

        private string _ReservedFor;

        private System.Nullable<System.DateTime> _SessionStartTime;

        private long _ElapsedSec;

        private long _AlertUnCapSec;

        private bool _AlertCame;

        private string _Status;

        private bool _IsEnabled;
        

        public GameCapDetails()
        {
        }

        public int GameCappingID
        {
            get
            {
                return this._GameCappingID;
            }
            set
            {
                if ((this._GameCappingID != value))
                {
                    this._GameCappingID = value;
                }
            }
        }

        public int InstallationNo
        {
            get
            {
                return this._InstallationNo;
            }
            set
            {
                if ((this._InstallationNo != value))
                {
                    this._InstallationNo = value;
                }
            }
        }

        public string Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                if ((this._Position != value))
                {
                    this._Position = value;
                }
            }
        }

        public string ReservedBy
        {
            get
            {
                return this._ReservedBy;
            }
            set
            {
                if ((this._ReservedBy != value))
                {
                    this._ReservedBy = value;
                }
            }
        }

        public string ReservedFor
        {
            get
            {
                return this._ReservedFor;
            }
            set
            {
                if ((this._ReservedFor != value))
                {
                    this._ReservedFor = value;
                }
            }
        }

        public System.Nullable<System.DateTime> SessionStartTime
        {
            get
            {
                return this._SessionStartTime;
            }
            set
            {
                if ((this._SessionStartTime != value))
                {
                    this._SessionStartTime = value;
                }
            }
        }

        public string ElapsedTime
        {
            get
            {
                return ((this._ElapsedSec / 3600).ToString("0#") + ":" + ((this._ElapsedSec % 3600) / 60).ToString("0#") + ":" + (this._ElapsedSec % 60).ToString("0#"));
            }
        }

        public long ElapsedSec
        {
            get
            {
                return this._ElapsedSec;
            }
            set
            {
                if ((this._ElapsedSec != value))
                {
                    this._ElapsedSec = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ElapsedTime"));
                    }
                }
            }
        }

        public string AlertUnCap
        {
            get
            {
                if (this.AlertCame)
                    if (this._AlertUnCapSec<=0)
                        return TimeSpan.FromSeconds(0).ToString();
                    else
                        return ((this._AlertUnCapSec / 3600).ToString("0#") + ":" + ((this._AlertUnCapSec % 3600) / 60).ToString("0#") + ":" + (this._AlertUnCapSec % 60).ToString("0#"));
                else
                    return ">" + TimeSpan.FromSeconds(this._AlertUnCapSec).ToString();
            }
        }

        public long AlertUnCapSec
        {
            get
            {
                return this._AlertUnCapSec;
            }
            set
            {
                if ((this._AlertUnCapSec != value))
                {
                    this._AlertUnCapSec = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("AlertUnCap"));
                    }

                }
            }
        }

        public bool AlertCame
        {
            get
            {
                return this._AlertCame;
            }
            set
            {
                if ((this._AlertCame != value))
                {
                    this._AlertCame = value;                    
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this._IsEnabled;
            }
            set
            {
                if ((this._IsEnabled != value))
                {
                    this._IsEnabled = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("IsEnabled"));
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
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;        
    }

    public partial class GameCapResult
    {

        private string _Message;

        private int _ReserveGameAsset;

        private int _MaxCapNotExceeded;

        private int _SelfReserve;

        private int _AllowReserve;

        private string _TimeOption;

        private int _AutoRelease;

        private int _ExpireMinstoAlert;

        public GameCapResult()
        {
        }

        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                if ((this._Message != value))
                {
                    this._Message = value;
                }
            }
        }

        public int ReserveGameAsset
        {
            get
            {
                return this._ReserveGameAsset;
            }
            set
            {
                if ((this._ReserveGameAsset != value))
                {
                    this._ReserveGameAsset = value;
                }
            }
        }

        public int MaxCapNotExceeded
        {
            get
            {
                return this._MaxCapNotExceeded;
            }
            set
            {
                if ((this._MaxCapNotExceeded != value))
                {
                    this._MaxCapNotExceeded = value;
                }
            }
        }

        public int SelfReserve
        {
            get
            {
                return this._SelfReserve;
            }
            set
            {
                if ((this._SelfReserve != value))
                {
                    this._SelfReserve = value;
                }
            }
        }

        public int AllowReserve
        {
            get
            {
                return this._AllowReserve;
            }
            set
            {
                if ((this._AllowReserve != value))
                {
                    this._AllowReserve = value;
                }
            }
        }

        public string TimeOption
        {
            get
            {
                return this._TimeOption;
            }
            set
            {
                if ((this._TimeOption != value))
                {
                    this._TimeOption = value;
                }
            }
        }

        public int AutoRelease
        {
            get
            {
                return this._AutoRelease;
            }
            set
            {
                if ((this._AutoRelease != value))
                {
                    this._AutoRelease = value;
                }
            }
        }

        public int ExpireMinstoAlert
        {
            get
            {
                return this._ExpireMinstoAlert;
            }
            set
            {
                if ((this._ExpireMinstoAlert != value))
                {
                    this._ExpireMinstoAlert = value;
                }
            }
        }
    }
}
