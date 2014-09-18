using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public static class AppSettings
    {
        private static bool _IsReceiptRequired;

        private static bool _REDEEM_TICKET_POP_UP_ALERT_VISIBILITY;

        private static bool _Is_Confirmation_Required_on_Declaration;
        
        private static bool _FlrView_SortBy_Asset;
        private static bool _FlrView_SortBy_Position;

        public static bool IsReceiptRequired
        {
            get { return _IsReceiptRequired; }
            set { _IsReceiptRequired = value; }
        }

        public static bool REDEEM_TICKET_POP_UP_ALERT_VISIBILITY
        {
            get { return _REDEEM_TICKET_POP_UP_ALERT_VISIBILITY; }
            set { _REDEEM_TICKET_POP_UP_ALERT_VISIBILITY = value; }
        }
        public static bool FlrView_SortBy_Asset
        {
            get { return _FlrView_SortBy_Asset; }
            set { _FlrView_SortBy_Asset = value; }
        }
        public static bool FlrView_SortBy_Position
        {
            get { return _FlrView_SortBy_Position; }
            set { _FlrView_SortBy_Position = value; }
        }

        public static bool Is_Confirmation_Required_on_Declaration
        {
            get { return _Is_Confirmation_Required_on_Declaration; }
            set { _Is_Confirmation_Required_on_Declaration = value; }
        }

    }
}
