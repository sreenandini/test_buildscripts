namespace BMC.MonitoringService
{
    #region Namespaces

    using System.Data.Linq.Mapping;

    #endregion Namespaces

    #region Public Class

    public class ServiceDetails
    {
        #region Private Variables

        private string _Setting_Name;

        private string _Setting_Value;

        #endregion Private Variables

        #region Constructor

        public ServiceDetails()
        {

        }

        #endregion Constructor

        #region Public Properties

        [Column(Storage = "_Setting_Name", DbType = "VarChar(100)")]
        public string Setting_Name
        {
            get
            {
                return this._Setting_Name;
            }
            set
            {
                if ((this._Setting_Name != value))
                {
                    this._Setting_Name = value;
                }
            }
        }

        [Column(Storage = "_Setting_Value", DbType = "VarChar(8000)")]
        public string Setting_Value
        {
            get
            {
                return this._Setting_Value;
            }
            set
            {
                if ((this._Setting_Value != value))
                {
                    this._Setting_Value = value;
                }
            }
        }

        #endregion Public Properties
    }

    #endregion Public Class
}
