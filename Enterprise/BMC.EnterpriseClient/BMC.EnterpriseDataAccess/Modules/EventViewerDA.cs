using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Rsp_GetEvents")]
        public ISingleResult<Rsp_GetEventsResult> GetSiteEvents([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] DateTime startDate, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] DateTime endDate, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> siteID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> priority, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> eventType, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> showautoclosed)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), startDate, endDate, siteID, priority, eventType, showautoclosed);
            return ((ISingleResult<Rsp_GetEventsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetEnterpriseEvents")]
        public ISingleResult<rsp_GetEnterpriseEventsResult> GetEnterpriseEvents([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] DateTime startDate, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] DateTime endDate, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), startDate, endDate, siteID);
            return ((ISingleResult<rsp_GetEnterpriseEventsResult>)(result.ReturnValue));
        }
    }

    public partial class Rsp_GetEventsResult
    {

        private int _site_ID;

        private string _Site_name;

        private string _Position;

        private string _Game_Title;

        private System.Nullable<System.DateTime> _Date_and_time_of_event;

        private string _Details_of_the_event;

        private string _Description_of_event;

        private System.Nullable<bool> _Priority;

        private System.Nullable<int> _Type_of_event;

        private string _Event_Auto_Closed;

        public Rsp_GetEventsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_site_ID", DbType = "Int NOT NULL")]
        public int site_ID
        {
            get
            {
                return this._site_ID;
            }
            set
            {
                if ((this._site_ID != value))
                {
                    this._site_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_name", DbType = "VarChar(50)")]
        public string Site_name
        {
            get
            {
                return this._Site_name;
            }
            set
            {
                if ((this._Site_name != value))
                {
                    this._Site_name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Position", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Game_Title", DbType = "VarChar(50)")]
        public string Game_Title
        {
            get
            {
                return this._Game_Title;
            }
            set
            {
                if ((this._Game_Title != value))
                {
                    this._Game_Title = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Date_and_time_of_event", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Date_and_time_of_event
        {
            get
            {
                return this._Date_and_time_of_event;
            }
            set
            {
                if ((this._Date_and_time_of_event != value))
                {
                    this._Date_and_time_of_event = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Details_of_the_event", DbType = "VarChar(50)")]
        public string Details_of_the_event
        {
            get
            {
                return this._Details_of_the_event;
            }
            set
            {
                if ((this._Details_of_the_event != value))
                {
                    this._Details_of_the_event = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description_of_event", DbType = "VarChar(14) NOT NULL", CanBeNull = false)]
        public string Description_of_event
        {
            get
            {
                return this._Description_of_event;
            }
            set
            {
                if ((this._Description_of_event != value))
                {
                    this._Description_of_event = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Priority", DbType = "Bit")]
        public System.Nullable<bool> Priority
        {
            get
            {
                return this._Priority;
            }
            set
            {
                if ((this._Priority != value))
                {
                    this._Priority = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type_of_event", DbType = "Int")]
        public System.Nullable<int> Type_of_event
        {
            get
            {
                return this._Type_of_event;
            }
            set
            {
                if ((this._Type_of_event != value))
                {
                    this._Type_of_event = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Event_Auto_Closed", DbType = "VarChar(3) NOT NULL", CanBeNull = false)]
        public string Event_Auto_Closed
        {
            get
            {
                return this._Event_Auto_Closed;
            }
            set
            {
                if ((this._Event_Auto_Closed != value))
                {
                    this._Event_Auto_Closed = value;
                }
            }
        }
    }

    public partial class rsp_GetEnterpriseEventsResult
    {

        private System.Nullable<int> _Evt_Site_ID;

        private string _Site_Name;

        private System.Nullable<System.DateTime> _Evt_Datetime;

        private string _Description_of_event;

        private string _Details_of_the_event;

        public rsp_GetEnterpriseEventsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Evt_Site_ID", DbType = "Int")]
        public System.Nullable<int> Evt_Site_ID
        {
            get
            {
                return this._Evt_Site_ID;
            }
            set
            {
                if ((this._Evt_Site_ID != value))
                {
                    this._Evt_Site_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Name", DbType = "VarChar(50)")]
        public string Site_Name
        {
            get
            {
                return this._Site_Name;
            }
            set
            {
                if ((this._Site_Name != value))
                {
                    this._Site_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Evt_Datetime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Evt_Datetime
        {
            get
            {
                return this._Evt_Datetime;
            }
            set
            {
                if ((this._Evt_Datetime != value))
                {
                    this._Evt_Datetime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description_of_event", DbType = "VarChar(14) NOT NULL", CanBeNull = false)]
        public string Description_of_event
        {
            get
            {
                return this._Description_of_event;
            }
            set
            {
                if ((this._Description_of_event != value))
                {
                    this._Description_of_event = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Details_of_the_event", DbType = "VarChar(50)")]
        public string Details_of_the_event
        {
            get
            {
                return this._Details_of_the_event;
            }
            set
            {
                if ((this._Details_of_the_event != value))
                {
                    this._Details_of_the_event = value;
                }
            }
        }
    }
}
