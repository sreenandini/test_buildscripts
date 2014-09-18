using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class clsIssueTicket
    {
        private static string sType = string.Empty;
        private static double _dblValue = 0;
        private static long _lnglValue = 0;
        private static DateTime _dteDate;
        private static long _lngTicketId = 0;
        private static string _strFullTicketNumber = string.Empty;
        private static string _strSBarcode = string.Empty;
        private static long _lngPence = 0;

        public long lPence
        {
            get { return _lngPence; }
            set
            {
                _lngPence = value;
            }

        }
        public String SType
        {
            get { return sType; }
            set
            {
                if (String.IsNullOrEmpty(value) == false)
                {
                    sType = value;
                }
            }

        }
        public double dblValue
        {
            get { return _dblValue; }
            set
            {
                _dblValue = value;
            }

        }
        public long lnglValue
        {
            get { return _lnglValue; }
            set
            {
                _lnglValue = value;
            }

        }
        public DateTime dDate
        {
            get { return _dteDate; }
            set
            {
                _dteDate = value;
            }

        }
        public long lTicketID
        {
            get { return _lngTicketId; }
            set
            {
                _lngTicketId = value;
            }

        }
        public string strFullTicketNumber
        {
            get { return _strFullTicketNumber; }
            set
            {
                if (String.IsNullOrEmpty(value) == false)
                {
                    _strFullTicketNumber = value;
                }

            }

        }
        public String sBarcode
        {
            get { return _strSBarcode; }
            set
            {
                if (String.IsNullOrEmpty(value) == false)
                {
                    _strSBarcode = value;
                }
            }

        }
    }
}
