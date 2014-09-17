using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.EnterpriseReportsTransport
{
    public partial class EmployeeID
    {        
       
		public EmployeeID()
		{
		}

        public int SecurityUserID { get; set; }

        public string UserName { get; set; } 
    }
}
