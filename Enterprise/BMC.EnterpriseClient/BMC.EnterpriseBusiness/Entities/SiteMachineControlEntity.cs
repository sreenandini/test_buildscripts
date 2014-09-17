using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BMC.EnterpriseBusiness.Entities
{
    public partial class GetMachineControlDetails
    {

        private System.Nullable<int> _bar_position_machine_enabled;

        private int _installation_id;

        private int _bar_position_id;

        private string _bar_position_name;

        private string _machine_stock_no;

        private string _machine_name;

        private string _current_change;

        private string _current_status;

     

        private string _Display_Status;

        private string _Previous_Status;

        public GetMachineControlDetails()
        {
        }
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


        public int installation_id
        {
            get
            {
                return this._installation_id;
            }
            set
            {
                if (this._installation_id != value)
                {
                    this._installation_id = value;
                }
            }
        }

        public System.Drawing.Image Image_Index
        {
            get;
            set;
        }

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


        public string Position
        {
            get
            {
                return this._bar_position_name;
            }
            set
            {
                if (this._bar_position_name != value)
                {
                    this._bar_position_name = value;
                }
            }
        }


        public string Asset
        {
            get
            {
                return this._machine_stock_no;
            }
            set
            {
                if (this._machine_stock_no != value)
                {
                    this._machine_stock_no = value;
                }
            }
        }


        public string GameTitle
        {
            get
            {
                return this._machine_name;
            }
            set
            {
                if (this._machine_name != value)
                {
                    this._machine_name = value;
                }
            }
        }


        public string Current
        {
            get
            {
                return this._current_status;
            }
            set
            {
                if (this._current_status != value)
                {
                    this._current_status = value;
                }
            }
        }
        public string Change
        {
            get
            {
                return this._current_change;
            }
            set
            {
                if (this._current_change != value)
                {
                    this._current_change = value;
                }
            }
        }
        
        public string Display_Status
        {
            get
            {
                return this._Display_Status;
            }
            set
            {
                if (this._Display_Status != value)
                {
                    this._Display_Status = value;
                }
            }

        }
        public string Previous_Status
        {
            get
            {
                return this._Previous_Status;
            }
            set
            {
                if (this._Previous_Status != value)
                {
                    this._Previous_Status = value;
                }
            }
        }

    }

}

