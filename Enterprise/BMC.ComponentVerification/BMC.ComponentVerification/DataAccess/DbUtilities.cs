using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ComponentVerification.DataAccess
{
    public static class DbUtilities
    {
        public static string GetConnectionString()
        {
            return BMC.Common.Utilities.DatabaseHelper.GetConnectionString();            
        }

    }
}
