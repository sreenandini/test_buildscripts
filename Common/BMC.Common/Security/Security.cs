using System;
using System.Collections.Generic;
using System.Text;

namespace BMC.Common
{
    public static class clsSecurity
    {
        private static string s_UserName = string.Empty;
        private static int i_UserID = 0;
  private static int i_UserNo = 0;
        public static string UserName
        {
            get
            {
                return s_UserName;
            }
            set
            {
                if (s_UserName != value)
                {
                    s_UserName = value;

                }

            }
        }
        public static int UserID
        {
            get
            {
                return i_UserID;
            }
            set
            {
                if (i_UserID != value)
                {
                    i_UserID = value;

                }

            }
        }

        public static int UserNo
        {
            get
            {
                return i_UserNo;
            }
            set
            {
                if (i_UserNo != value)
                {
                    i_UserNo = value;

                }

            }
        }
    }
}
