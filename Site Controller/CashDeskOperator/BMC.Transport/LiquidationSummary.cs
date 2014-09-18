using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public class LiquidationSummary
    {
        public DateTime Date_Collected
        {
            get;
            set;
        }

        public string Retailer_Name
        {
            get;
            set;
        }
        public decimal Gross
        {
            get;
            set;
        }
        public decimal Tickets_Expected
        {
            get;
            set;
        }
        public decimal Net
        {
            get;
            set;
        }
        public decimal Net_Percentage
        {
            get;
            set;
        }
        public decimal Percentage_Setting
        {
            get;
            set;
        }
        public decimal Retail_Negative_Net
        {
            get;
            set;
        }


        public decimal Retailer_Share
        {
            get;
            set;
        }
        public decimal Tickets_Paid
        {
            get;
            set;
        }
        public decimal Advance_To_Retailer
        {
            get;
            set;
        }
        public decimal Retailer
        {
            get;
            set;
        }
        public decimal Balance_Due
        {
            get;
            set;
        }
    }
}
