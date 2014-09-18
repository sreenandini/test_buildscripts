using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.Transport
{
    public class EnableDisableMachine : INotifyPropertyChanged
    {
        
        private bool _IsSelected;
        private string _Status;
        private string _StockNo;
        private string _BarPosNumber;
        private string _Message;

        public EnableDisableMachine()
        {
        }

        public string StockNo
        {
            get
            {
                return this._StockNo;
            }
            set
            {
                if ((this._StockNo != value))
                {
                    this._StockNo = value;
                }
            }
        }
        
        public string BarPosNumber
        {
            get
            {
                return this._BarPosNumber;
            }
            set
            {
                if ((this._BarPosNumber != value))
                {
                    this._BarPosNumber = value;
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
        
        public string Status
        {
            get
            {
                return _Status;
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

        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                if ((this._Message != value))
                {
                    this._Message = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Message"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
