using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class TermsProfilesEntity
    {
        private System.Nullable<int> _Machine_Type_ID;

        private int _Terms_Profile_ID;

        private string _Machine_Type_Code;

        public TermsProfilesEntity()
        {
        }

        [Column(Storage = "_Machine_Type_ID", DbType = "Int")]
        public System.Nullable<int> Machine_Type_ID
        {
            get
            {
                return this._Machine_Type_ID;
            }
            set
            {
                if ((this._Machine_Type_ID != value))
                {
                    this._Machine_Type_ID = value;
                }
            }
        }

        [Column(Storage = "_Terms_Profile_ID", DbType = "INT NOT NULL")]
        public int Terms_Profile_ID
        {
            get
            {
                return this._Terms_Profile_ID;
            }
            set
            {
                if ((this._Terms_Profile_ID != value))
                {
                    this._Terms_Profile_ID = value;
                }
            }
        }

        [Column(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
        public string Machine_Type_Code
        {
            get
            {
                return this._Machine_Type_Code;
            }
            set
            {
                if ((this._Machine_Type_Code != value))
                {
                    this._Machine_Type_Code = value;
                }
            }
        }
    }

    public class TermsProfilesResultForMachine
    {
        private System.Nullable<int> _Machine_Type_ID;

        private string _Machine_Type_Code;

        public TermsProfilesResultForMachine()
        {
        }

        [Column(Storage = "_Machine_Type_ID", DbType = "INT")]
        public System.Nullable<int> Machine_Type_ID
        {
            get
            {
                return this._Machine_Type_ID;
            }
            set
            {
                if ((this._Machine_Type_ID != value))
                {
                    this._Machine_Type_ID = value;
                }
            }
        }

        [Column(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
        public string Machine_Type_Code
        {
            get
            {
                return this._Machine_Type_Code;
            }
            set
            {
                if ((this._Machine_Type_Code != value))
                {
                    this._Machine_Type_Code = value;
                }
            }
        }
    }

    public class TermsGroupResult
    {
        private System.Nullable<int> _Terms_Group_ID;

        private string _Terms_Group_Name;

        public TermsGroupResult()
        {
        }

        [Column(Storage = "Terms_Group_ID", DbType = "INT")]
        public System.Nullable<int> Terms_Group_ID
        {
            get
            {
                return this._Terms_Group_ID;
            }
            set
            {
                if ((this._Terms_Group_ID != value))
                {
                    this._Terms_Group_ID = value;
                }
            }
        }

        [Column(Storage = "Terms_Group_Name", DbType = "VarChar(50)")]
        public string Terms_Group_Name
        {
            get
            {
                return this._Terms_Group_Name;
            }
            set
            {
                if ((this._Terms_Group_Name != value))
                {
                    this._Terms_Group_Name = value;
                }
            }
        }
    }

    public partial class UnAssignedMachineTypes
    {

        private System.Nullable<int> _Machine_Type_ID;

        private string _Machine_Type_Code;

        public UnAssignedMachineTypes()
        {
        }

        [Column(Storage = "_Machine_Type_ID", DbType = "INT")]
        public System.Nullable<int> Machine_Type_ID
        {
            get
            {
                return this._Machine_Type_ID;
            }
            set
            {
                if ((this._Machine_Type_ID != value))
                {
                    this._Machine_Type_ID = value;
                }
            }
        }

        [Column(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
        public string Machine_Type_Code
        {
            get
            {
                return this._Machine_Type_Code;
            }
            set
            {
                if ((this._Machine_Type_Code != value))
                {
                    this._Machine_Type_Code = value;
                }
            }
        }
    }
}
