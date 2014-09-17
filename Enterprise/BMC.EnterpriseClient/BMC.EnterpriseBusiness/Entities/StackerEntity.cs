using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class StackerEntity
    {
        private int _StackerID = -1;
        private string _StackerName = string.Empty;
        private int _StackerSize = 0;
        private string _StackerDescription = string.Empty;
        private bool _StackerStatus = false;

        public int StackerID
        {
            get
            {
                return _StackerID;
            }
            set
            {
                _StackerID = value;
            }
        }

        public string StackerName
        {
            get
            {
                return _StackerName;
            }
            set
            {
                _StackerName = value;
            }
        }
        
        public int StackerSize
        {
            get
            {
                return _StackerSize;
            }
            set
            {
                _StackerSize = value;
            }
        }
        
        public string StackerDescription
        {
            get
            {
                return _StackerDescription;
            }
            set
            {
                _StackerDescription = value;
            }
        }
    
        public bool StackerStatus
        {
            get
            {
                return _StackerStatus;
            }
            set
            {
                _StackerStatus = value;
            }
        }
    }

}
