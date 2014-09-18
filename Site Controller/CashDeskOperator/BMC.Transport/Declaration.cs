using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.Transport
{
    public class ExceptionVouchers : INotifyPropertyChanged
    {
        private bool _isChecked;
        private string _strBarcode;
        private decimal  _iAmount;
        private int _ErrCode;
        private string _Device;
        private int _Installation_no;
        private int _Collection_No;
        private string _Error_Description;
        public bool IsChecked 
        {
            get { return _isChecked; }
            set { _isChecked = value; if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("IsChecked")); }
        }
        public string strBarcode 
        {
            get { return _strBarcode; }
            set { _strBarcode = value; }
        }
        public decimal iAmount 
        {
            get { return _iAmount; }
            set { _iAmount = value; }
        }

        public int ErrCode
        {
            get { return _ErrCode; }
            set { _ErrCode = value; }
        }

        public string Device 
        {
            get { return _Device; }
            set { _Device = value; }
        }
        public int Installation_no
        {
            get {  return _Installation_no; }
            set { _Installation_no = value; }
        }
        public int Collection_No
        {
            get { return _Collection_No; }
            set { _Collection_No = value; }
        }
        public string Error_Description
        {
            get { return _Error_Description; }
            set { _Error_Description = value; }
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

