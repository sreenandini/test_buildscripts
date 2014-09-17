using System;
using System.Collections.Generic;
using System.Text;

namespace BMC.Business.EnterpriseConfig
{
    public class SecurityProperty
    {
        private string strUserName = string.Empty;
        private string strPassword = string.Empty;

        public string UserName
        {
            get { return strUserName; }
            set
            {
                if (String.IsNullOrEmpty(value) == false)
                {
                    strUserName = value;
                }
            }

        }

        public string Password
        {
            get { return strPassword; }
            set
            {
                if (String.IsNullOrEmpty(value) == false)
                {
                    strPassword = value;
                }
            }

        }

    }
}
