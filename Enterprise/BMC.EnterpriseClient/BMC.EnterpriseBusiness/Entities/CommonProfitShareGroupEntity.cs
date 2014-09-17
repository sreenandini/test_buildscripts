using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class CommonProfitShareGroupEntity
    {
        private int _Id = 0;
        private string _Name = string.Empty;
        private double _Percentage = 0;
        private string _Description = string.Empty;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Id
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
        public string Name
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


        public double Percentage
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

        public string Description
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
