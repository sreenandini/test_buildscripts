using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Simulator.Models
{
    public class GIMInformationModel
    {
        // to avoid wpf data binding issue
        internal bool _isPropertyModified = false;

        public long RowNo { get; set; }

        public string SNo { get; set; }

        public int HashCode { get; set; }

        #region IPAddress
        private string _ipAddress = default(string);

        public string IPAddress
        {
            get { return _ipAddress; }
            set
            {
                if (value != _ipAddress)
                {
                    _isPropertyModified = true;
                    _ipAddress = value;
                }
            }
        }
        #endregion

        #region AssetNo
        private string _assetNo = default(string);

        public string AssetNo
        {
            get { return _assetNo; }
            set
            {
                if (value != _assetNo)
                {
                    _isPropertyModified = true;
                    _assetNo = value;
                    this.PrepareDisplayText();
                }
            }
        }
        #endregion 

        #region GmuNo
        private string _gmuNo = default(string);

        public string GmuNo
        {
            get { return _gmuNo; }
            set
            {
                if (value != _gmuNo)
                {
                    _isPropertyModified = true;
                    _gmuNo = value;
                    this.PrepareDisplayText();
                }
            }
        }
        #endregion 

        #region SerialNo
		private string _serialNo = default(string);

        public string SerialNo
        {
            get { return _serialNo; }
            set
            {
				if(value != _serialNo) {
					_isPropertyModified = true;
					_serialNo = value;
                    this.PrepareDisplayText();
				}
            }
        }
	#endregion

        #region ManufacturerID
        private string _manufacturerID = default(string);

        public string ManufacturerID
        {
            get { return _manufacturerID; }
            set
            {
                if (value != _manufacturerID)
                {
                    _isPropertyModified = true;
                    _manufacturerID = value;
                }
            }
        }
        #endregion 

        #region MACAddress
        private string _macAddress = default(string);

        public string MACAddress
        {
            get { return _macAddress; }
            set
            {
                if (value != _macAddress)
                {
                    _isPropertyModified = true;
                    _macAddress = value;
                }
            }
        }
        #endregion 

        #region SASVersion
        private string _sasVersion = default(string);

        public string SASVersion
        {
            get { return _sasVersion; }
            set
            {
                if (value != _sasVersion)
                {
                    _isPropertyModified = true;
                    _sasVersion = value;
                }
            }
        }
        #endregion 

        #region GMUVersion
        private string _gmuVersion = default(string);

        public string GMUVersion
        {
            get { return _gmuVersion; }
            set
            {
                if (value != _gmuVersion)
                {
                    _isPropertyModified = true;
                    _gmuVersion = value;
                }
            }
        }
        #endregion 

        #region DisplayText
        private string _displayText = default(string);

        private void PrepareDisplayText()
        {
            StringBuilder sb = new StringBuilder();
            if (!_ipAddress.IsEmpty())
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append("IP Address : " + _ipAddress);
            }
            if (!_assetNo.IsEmpty())
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append("A : " + _assetNo);
            }
            if (!_gmuNo.IsEmpty())
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append("G : " + _gmuNo);
            }
            if (!_serialNo.IsEmpty())
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append("S : " + _serialNo);
            }
            _displayText = sb.ToString();
        }

        public string DisplayText
        {
            get { 
                
                return _displayText; }
            set
            {
                if (value != _displayText)
                {
                    _isPropertyModified = true;
                    _displayText = value;
                }
            }
        }
        #endregion 

        public bool IsAll
        {
            get { return this.RowNo == 0; }
        }

        public override string ToString()
        {
            return this.GetHashCode().ToString();
        }
    }

    public class GIMInformationModelCollection : ObservableCollection<GIMInformationModel>
    {
        public void AddWithSNo(GIMInformationModel model)
        {
            model.SNo = string.Format("{0:D}", (this.Count + 1));
            this.Add(model);
        }
    }
}
