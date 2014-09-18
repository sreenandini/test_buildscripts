using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.Transport
{
    public class AFTSetting : INotifyPropertyChanged
    {
        public AFTSetting()
        {

        }
        public AFTSetting(string name, string value)
        {
            Name = name;
            Value = value;
            IsCheckBox = false;

        }

        private bool active;
        public bool IsActive
        {
            get { return active; }
            set { active = value; ReportChange("IsActive"); }
        }

        public string Name { get; set; }
         private string Val;
         public string Value { get {return Val;} set { Val = value; ReportChange("Value"); } }

        public bool IsCheckBox { get; set; }

        private void ReportChange(string propertyName)
        { if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region INotifyPropertyChanged Members

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add {  }
            remove {  }
        }

        #endregion
    }

    public class AftAssets: INotifyPropertyChanged
    {
        private int isChecked;
        private string name;
        private string message;
        private int status;
        private int installationNo;

        public int IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("IsChecked")); }
        }
        //
        public string Name
        {
            get { return name; }
            set { name = value; if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("Name")); }
        }
        //
        public int Status
        {
            get { return status; }
            set { status = value; if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("Status")); }
        }
        //
        public string Message
        {
            get { return message; }
            set { message = value; if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("Message")); }
        }
        //
        public int InstallationNo
        {
            get { return installationNo; }
            set { installationNo = value;}
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
