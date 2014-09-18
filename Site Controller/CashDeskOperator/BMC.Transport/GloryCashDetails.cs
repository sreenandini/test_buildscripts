using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public class GloryCashDetails
    {
        public int? Sequenceid { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionStarttime { get; set; }
        public DateTime? TransactionEndtime { get; set; }
        public string TicketNo { get; set; }
        public string ValidationNo { get; set; }
        public string AssetNo { get; set; }
        public int TransactionAmount { get; set; }
        public string UserID { get; set; }
        public string SessionID { get; set; }
        public string Device { get; set; }
        public char Status { get; set; }
        public string ErrorCode { get; set; }

    }

    public static class UserGroup
    {
        public const string GROUP_ADMIN = "Administrator";
        public const string GROUP_POWERUSER = "PowerUser";
        public const string GROUP_USER = "User";
        public const string GROUP_IT = "IT";
    }

    public class UserInformation
    {
        private static string msID;
        public static string ID
        {
            get { return msID; }
            set { msID = value; }
        }

        private static int _SeqID;
        public static int SeqID
        {
            get { return _SeqID; }
            set { _SeqID = value; }
        }

        private static string msUser;
        public static string User
        {
            get { return msUser; }
            set { msUser = value; }
        }

        private static string msPassword;
        public static string Password
        {
            get { return msPassword; }
            set { msPassword = value; }
        }

        private static string msDevice;
        public static string Device
        {
            get { return msDevice; }
            set { msDevice = value; }
        }

        private static string msParts;
        public static string Parts
        {
            get { return msParts; }
            set { msParts = value; }
        }

        private static Boolean _FlagConnected;
        public static Boolean FlagConnected
        {
            get { return _FlagConnected; }
            set { _FlagConnected = value; }
        }

        private static string msSessionID;
        public static string SessionID
        {
            get { return msSessionID; }
            set { msSessionID = value; }
        }

        private static int _Heartbeat;
        public static int Heartbeat
        {
            get { return _Heartbeat; }
            set { _Heartbeat = value; }
        }

        private static int _HeartbeatCountdown;
        public static int HeartbeatCountdown
        {
            get { return _HeartbeatCountdown; }
            set { _HeartbeatCountdown = value; }
        }

        private static string msGroup;
        public static string Group
        {
            get { return msGroup; }
            set { msGroup = value; }
        }

        private static bool msOccupied;
        public static bool Occupied
        {
            get { return msOccupied; }
            set { msOccupied = value; }
        }

        private static uint msSequenceNumber;
        public static uint SequenceNumber
        {
            get { return ++msSequenceNumber; }
        }

        public static void initialize()
        {
            msID = "";
            msUser = "";
            msPassword = "";

            _FlagConnected = false;
            msSessionID = "";
            _Heartbeat = 60;
            _HeartbeatCountdown = _Heartbeat;

            msOccupied = false;
            msParts = "RBURIGHT";
            msSequenceNumber = 0;
        }
    }

}
