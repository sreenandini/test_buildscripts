using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class DepreciationEntity
    {
        private int _Depreciation_Policy_ID;

        private string _Depreciation_Policy_Description;

        private System.Nullable<float> _Depreciation_Policy_Residual_Value;

        private int _Depreciation_Policy_Details_ID;    

        private System.Nullable<int> _Depreciation_Policy_Details_Period;

        private System.Nullable<int> _Depreciation_Policy_Details_Duration;

        private System.Nullable<int> _Depreciation_Policy_Details_Percentage;

        public DepreciationEntity()
        {
        }


        public int Depreciation_Policy_ID
        {
            get
            {
                return this._Depreciation_Policy_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_ID != value))
                {
                    this._Depreciation_Policy_ID = value;
                }
            }
        }

        public string Depreciation_Policy_Description
        {
            get
            {
                return this._Depreciation_Policy_Description;
            }
            set
            {
                if ((this._Depreciation_Policy_Description != value))
                {
                    this._Depreciation_Policy_Description = value;
                }
            }
        }

        public System.Nullable<float> Depreciation_Policy_Residual_Value
        {
            get
            {
                return this._Depreciation_Policy_Residual_Value;
            }
            set
            {
                if ((this._Depreciation_Policy_Residual_Value != value))
                {
                    this._Depreciation_Policy_Residual_Value = value;
                }
            }
        }

        public int Depreciation_Policy_Details_ID
        {
            get
            {
                return this._Depreciation_Policy_Details_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_ID != value))
                {
                    this._Depreciation_Policy_Details_ID = value;
                }
            }
        }      

        public System.Nullable<int> Depreciation_Policy_Details_Period
        {
            get
            {
                return this._Depreciation_Policy_Details_Period;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_Period != value))
                {
                    this._Depreciation_Policy_Details_Period = value;
                }
            }
        }

        public System.Nullable<int> Depreciation_Policy_Details_Duration
        {
            get
            {
                return this._Depreciation_Policy_Details_Duration;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_Duration != value))
                {
                    this._Depreciation_Policy_Details_Duration = value;
                }
            }
        }

        public System.Nullable<int> Depreciation_Policy_Details_Percentage
        {
            get
            {
                return this._Depreciation_Policy_Details_Percentage;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_Percentage != value))
                {
                    this._Depreciation_Policy_Details_Percentage = value;
                }
            }
        }
    }

    public partial class GetDepreciationPolicyPercentResult
    {

        private System.Nullable<int> _TotalDrop;

        public GetDepreciationPolicyPercentResult()
        {
        }
       
        public System.Nullable<int> TotalDrop
        {
            get
            {
                return this._TotalDrop;
            }
            set
            {
                if ((this._TotalDrop != value))
                {
                    this._TotalDrop = value;
                }
            }
        }
    }
}

