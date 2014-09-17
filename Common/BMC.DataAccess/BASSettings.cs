using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMC.DataAccess
{
    public class BASSettings
    {
        private static bool _IsMAPICSIntegrationRequired;

        private static string _Client;


        public static bool IsMAPICSIntegrationRequired
        {
            get { return _IsMAPICSIntegrationRequired; }
            set { _IsMAPICSIntegrationRequired = value; }
        }

        public static string Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

    }
}
