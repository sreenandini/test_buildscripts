using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class ProfitShareGroupEntity
    {

        private int _Id = -1;
        private string _Name = string.Empty;
        private float _Percentage = -1;
        private string _Description = string.Empty;

              
        public int ProfitShareGroupId
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        public string ProfitShareGroupName
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

      
        public float   ProfitSharePercentage
        {
            get
            {
                return _Percentage;
            }

            set
            {
                _Percentage = value;
            }
        }

        public string  ProfitShareGroupDescription
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }
    }
}
    