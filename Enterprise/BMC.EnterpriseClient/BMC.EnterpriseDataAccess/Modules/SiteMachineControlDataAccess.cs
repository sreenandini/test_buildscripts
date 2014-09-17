using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_getsitemachinedetails")]
        public ISingleResult<rsp_getsitemachinedetailsResult> GetSiteMachineDetails([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id);
            return ((ISingleResult<rsp_getsitemachinedetailsResult>)(result.ReturnValue));
        }
        
        [Function(Name = "dbo.usp_UpdateBarPositionForMachineControl")]
        public int UpdateBarPositionForMachineControl([Parameter(Name = "EH_Reference", DbType = "VarChar(4000)")] string eH_Reference, [Parameter(Name = "EH_Type", DbType = "VarChar(30)")] string eH_Type, [Parameter(Name = "BarPosStatus", DbType = "Int")] System.Nullable<int> barPosStatus)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), eH_Reference, eH_Type, barPosStatus);
            return ((int)(result.ReturnValue));
        }
    }
    public partial class rsp_getsitemachinedetailsResult
    {

        private System.Nullable<int> _bar_position_machine_enabled;

        private int _installation_id;

        private int _bar_position_id;

        private string _bar_position_name;

        private string _machine_stock_no;

        private string _machine_name;

        private string _current_change;

        private string _current_status;

        public rsp_getsitemachinedetailsResult()
        {
        }

        [Column(Storage = "_bar_position_machine_enabled", DbType = "Int")]
        public System.Nullable<int> bar_position_machine_enabled
        {
            get
            {
                return this._bar_position_machine_enabled;
            }
            set
            {
                if ((this._bar_position_machine_enabled != value))
                {
                    this._bar_position_machine_enabled = value;
                }
            }
        }

        [Column(Storage = "_installation_id", DbType = "Int NOT NULL")]
        public int installation_id
        {
            get
            {
                return this._installation_id;
            }
            set
            {
                if ((this._installation_id != value))
                {
                    this._installation_id = value;
                }
            }
        }

        [Column(Storage = "_bar_position_id", DbType = "Int NOT NULL")]
        public int bar_position_id
        {
            get
            {
                return this._bar_position_id;
            }
            set
            {
                if ((this._bar_position_id != value))
                {
                    this._bar_position_id = value;
                }
            }
        }

        [Column(Storage = "_bar_position_name", DbType = "VarChar(50)")]
        public string bar_position_name
        {
            get
            {
                return this._bar_position_name;
            }
            set
            {
                if ((this._bar_position_name != value))
                {
                    this._bar_position_name = value;
                }
            }
        }

        [Column(Storage = "_machine_stock_no", DbType = "VarChar(50)")]
        public string machine_stock_no
        {
            get
            {
                return this._machine_stock_no;
            }
            set
            {
                if ((this._machine_stock_no != value))
                {
                    this._machine_stock_no = value;
                }
            }
        }

        [Column(Storage = "_machine_name", DbType = "VarChar(50)")]
        public string machine_name
        {
            get
            {
                return this._machine_name;
            }
            set
            {
                if ((this._machine_name != value))
                {
                    this._machine_name = value;
                }
            }
        }

        [Column(Storage = "_current_change", DbType = "VarChar(7) NOT NULL", CanBeNull = false)]
        public string current_change
        {
            get
            {
                return this._current_change;
            }
            set
            {
                if ((this._current_change != value))
                {
                    this._current_change = value;
                }
            }
        }

        [Column(Storage = "_current_status", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
        public string current_status
        {
            get
            {
                return this._current_status;
            }
            set
            {
                if ((this._current_status != value))
                {
                    this._current_status = value;
                }
            }
        }
    }
}
