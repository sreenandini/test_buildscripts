using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using BMC.EventsTransmitter.Utils;
using System.Data;
using System.Xml;
using System.IO;

namespace BMC.EventsTransmitter
{

    internal interface BMCMessage // to be used in message formatter 
    {

    }
    internal class SASExceptionCode
    {
        public const  string HANPAY = "10"; // hex 10 
        //public const string PLAYERCARDIN = "77";
        //public const string PLAYERCARDINFO = "39";
    }
    internal class GK10 : BMCMessage
    {
        Log Logger = Log.GetInstance(); 
        Installations _Installations = Installations.GetInstance();
        XmlDocument oDoc = null;
        private string _Flags = string.Empty;//0
        private string _PacketNumber = string.Empty;	//1
        private string _ProcessId = string.Empty;//2
        private string _CommandId = string.Empty;	//3
        private string _SequenceNumber = string.Empty;	//4
        private string _SDSVersion = string.Empty;	//5
        private string _MessageType = string.Empty;
        private string _TransactionCode = string.Empty;
        //private string _TAB = " ";

        private string _ExceptionCode = string.Empty;
        private string _CasinoNumber = string.Empty;
        private string _CBMessageNumber = string.Empty;
        private string _SlotDoorFlags = string.Empty;
        private string _TransactionDate = string.Empty;
        private string _TransactionTime = string.Empty;
        private string _QueueID = string.Empty;
        private string _SideID = string.Empty;
        private string _SlotNumber = string.Empty;
        private string _Stand = string.Empty;
        private string _EmployeeID = string.Empty;
        private string _HomeCasinoID = string.Empty;
        private string _PlayerCardNumber = string.Empty;
        private string _TransactionAmount = string.Empty;
        private string _OriginalJackpotAmount = string.Empty;
        private string _JackpotID = string.Empty;
        private string _BonusPoints = string.Empty;
        private string _OptionsFlag = string.Empty;
        private string _TotalBets = string.Empty;
        private string _TotalWins1 = string.Empty;
        private string _TotalCoinDrop = string.Empty;
        private string _TotalPlays = string.Empty;
        private string _TotalHandPaidJackpots = string.Empty;
        private string _TotalMachineFills = string.Empty;
        private string _TotalBillsAccepted = string.Empty;
        private string _TotalCouponsAccepted = string.Empty;
        private string _TotalCoinPurchases = string.Empty;
        private string _TotalCoinsCollected = string.Empty;
        private string _TotalNonCashableeCashIn = string.Empty;
        private string _TotalNonCashableeCashOut = string.Empty;
        private string _TotalCashableeCashIn = string.Empty;
        private string _TotalCashableeCashOut = string.Empty;
        private string _LastSMICode = string.Empty;
        private string _TicketInPromo = string.Empty;
        private string _TicketOutPromo = string.Empty;
        private string _TicketIn = string.Empty;
        private string _TicketOut = string.Empty;
        private string _Region;

        //Readonly Properties
        public string Flags { get { return _Flags; } }
        public string PacketNumber { set { _PacketNumber = value; } get { return _PacketNumber; } }
        public string ProcessId { get { return _ProcessId; } }
        public string CommandId { get { return _CommandId; } }
        public string SequenceNumber { set { _SequenceNumber = value; } get { return _SequenceNumber; } }

        public string SDSVersion { get { return _SDSVersion; } }
        public string MessageType { get { return _MessageType; } }
        public string TransactionCode { get { return _TransactionCode; } }
        //public string TAB { get { return _TAB; } }
        public string ExceptionCode
        {
            get
            {
                return _ExceptionCode;
            }
            private set
            {
                _ExceptionCode = value;
                switch (_ExceptionCode)
                {
                    case SASExceptionCode.HANPAY:

                        try
                        {
                            string strJacpotValue = oDoc.SelectSingleNode("Polled_Event/Fault_Message").GetInnerText();
                            string[] Arr = strJacpotValue.Split(',');
                            _JackpotID = Arr[1].NullToInt().ToString("X");
                            _TransactionAmount = (Arr[2].NullToInt().ToString("X").STMFormat(2) + Arr[3].NullToInt().ToString("X").STMFormat(2) + Arr[4].NullToInt().ToString("X").STMFormat(2) + Arr[5].NullToInt().ToString("X").STMFormat(2) + Arr[6].NullToInt().ToString("X").STMFormat(2)).NullToInt(0).ToString();
                            _OriginalJackpotAmount = (Arr[2].NullToInt().ToString("X").STMFormat(2) + Arr[3].NullToInt().ToString("X").STMFormat(2) + Arr[4].NullToInt().ToString("X").STMFormat(2) + Arr[5].NullToInt().ToString("X").STMFormat(2) + Arr[6].NullToInt().ToString("X").STMFormat(2)).NullToInt(0).ToString();
                        }
                        catch
                        {
                            Logger.Error("Error Parsing Jackpot Amount GK10 message");
                            throw;
                        }
                        break;
                    //case SASExceptionCode.PLAYERCARDIN: case SASExceptionCode.PLAYERCARDINFO:
                    //    _PlayerCardNumber = oDoc.SelectSingleNode("Polled_Event/CardNumber").GetInnerText();
                    //    break;
                }
            }
        }
        public string CasinoNumber { get { return _CasinoNumber; } }
        public string CBMessageNumber { get { return _CBMessageNumber; } }
        public string SlotDoorFlags { get { return _SlotDoorFlags; } }
        public string TransactionDate { get { return _TransactionDate; } }
        public string TransactionTime { get { return _TransactionTime; } }
        public string QueueID { get { return _QueueID; } }
        public string SideID { get { return _SideID; } }
        public string SlotNumber { get { return _SlotNumber; } }
        public string Stand { get { return _Stand; } }
        public string EmployeeID { get { return _EmployeeID; } }
        public string HomeCasinoID { get { return _HomeCasinoID; } }
        public string PlayerCardNumber { get { return _PlayerCardNumber; } }
        public string TransactionAmount { get { return _TransactionAmount; } }
        public string OriginalJackpotAmount { get { return _OriginalJackpotAmount; } }
        public string JackpotID { get { return _JackpotID; } }
        public string BonusPoints { get { return _BonusPoints; } }
        public string OptionsFlag { get { return _OptionsFlag; } }
        public string TotalBets { get { return _TotalBets; } }
        public string TotalWins { get { return _TotalWins1; } }
        public string TotalCoinDrop { get { return _TotalCoinDrop; } }
        public string TotalPlays { get { return _TotalPlays; } }
        public string TotalHandPaidJackpots { get { return _TotalHandPaidJackpots; } }
        public string TotalMachineFills { get { return _TotalMachineFills; } }
        public string TotalBillsAccepted { get { return _TotalBillsAccepted; } }
        public string TotalCouponsAccepted { get { return _TotalCouponsAccepted; } }
        public string TotalCoinPurchases { get { return _TotalCoinPurchases; } }
        public string TotalCoinsCollected { get { return _TotalCoinsCollected; } }
        public string TotalNonCashableeCashIn { get { return _TotalNonCashableeCashIn; } }
        public string TotalNonCashableeCashOut { get { return _TotalNonCashableeCashOut; } }
        public string TotalCashableeCashIn { get { return _TotalCashableeCashIn; } }
        public string TotalCashableeCashOut { get { return _TotalCashableeCashOut; } }
        public string LastSMICode { get { return _LastSMICode; } }
        public string TicketInPromo { get { return _TicketInPromo; } }
        public string TicketOutPromo { get { return _TicketOutPromo; } }
        public string TicketIn { get { return _TicketIn; } }
        public string TicketOut { get { return _TicketOut; } }
        public string Region { get { return _Region; } }
      

        public GK10(string xmlData)
        {
            try
            {
                
                oDoc = new XmlDocument();
                oDoc.LoadXml(xmlData);
                _Flags = "0";
                //_PacketNumber = "0";="0" -- to be updated while formatting 
                _ProcessId = "0";
                _CommandId = "0";
                //_SequenceNumber ="0" -- to be updated while formatting 
                _SDSVersion = Settings.Version;
                _MessageType = "GK";
                _TransactionCode = "10";

                //handle Jackpot /HandPay/PROG 
                this.ExceptionCode = oDoc.SelectSingleNode("Polled_Event/ExceptionCode").GetInnerText().NullToInt().ToString("X");

                _CasinoNumber = oDoc.SelectSingleNode("Polled_Event/Site_Code").GetInnerText().STMFormat(3);
                //_CBMessageNumber = string.Empty;
                _SlotDoorFlags = oDoc.SelectSingleNode("Polled_Event/DoorFlags").GetInnerText();
                _TransactionDate = DateTime.Now.ToString("yyyyMMdd");
                _TransactionTime = DateTime.Now.ToString("HHmmss");
                //_QueueID = string.Empty;
                _SideID = oDoc.SelectSingleNode("Polled_Event/Site_Code").GetInnerText();
                 GMUINFO oAssetInfo = _Installations[oDoc.SelectSingleNode("Polled_Event/Serial_No").GetInnerText().NullToInt()];
                _SlotNumber = oAssetInfo.Asset;
                _Stand = oAssetInfo.Position;
                //_Stand = string.Empty;
                _EmployeeID = oDoc.SelectSingleNode("Polled_Event/EmpID").GetInnerText();
                _HomeCasinoID =  oDoc.SelectSingleNode("Polled_Event/Site_Code").GetInnerText();
                _PlayerCardNumber = oDoc.SelectSingleNode("Polled_Event/CardNumber").GetInnerText();
                
                /* Will be filed on Jacpot/handpay 
                _TransactionAmount = string.Empty;
                _OriginalJackpotAmount = string.Empty;
                _JackpotID = string.Empty;
                */

                _BonusPoints = oDoc.SelectSingleNode("Polled_Event/BonusPoints").GetInnerText();
                _OptionsFlag = oDoc.SelectSingleNode("Polled_Event/OptionsFlag").GetInnerText();
                _TotalBets = oDoc.SelectSingleNode("Polled_Event/sector/counter[@counterid=50]").GetInnerText();  ///50
                _TotalWins1 = oDoc.SelectSingleNode("Polled_Event/sector/counter[@counterid=51]").GetInnerText(); // 51
                _TotalCoinDrop = oDoc.SelectSingleNode("Polled_Event/sector/counter[@counterid=52]").GetInnerText();  //52
                _TotalPlays = oDoc.SelectSingleNode("Polled_Event/sector/counter[@counterid=54]").GetInnerText();  //54 
                _TotalHandPaidJackpots = oDoc.SelectSingleNode("Polled_Event/sector/counter[@counterid=53]").GetInnerText();  //53
                // _TotalMachineFills = string.Empty; 
                // _TotalBillsAccepted = string.Empty; // sum all bills 
                // _TotalCouponsAccepted = string.Empty; NA
                // _TotalCoinPurchases = string.Empty;
                // _TotalCoinsCollected = string.Empty;
                // _TotalNonCashableeCashIn = string.Empty; //91 
                // _TotalNonCashableeCashOut = string.Empty; //92 
                // _TotalCashableeCashIn = string.Empty; //89
                // _TotalCashableeCashOut = string.Empty;//90
                 _LastSMICode =oDoc.SelectSingleNode("Polled_Event/SMICode").GetInnerText();
                // _TicketInPromo = string.Empty; //?
                // _TicketOutPromo = string.Empty;//?
                _TicketIn = XmlExtension.GetInnerText(oDoc.SelectSingleNode("Polled_Event/sector/counter[@counterid=89]"));  //89 
                _TicketOut = XmlExtension.GetInnerText(oDoc.SelectSingleNode("Polled_Event/sector/counter[@counterid=90]"));  //90 
                _Region = SiteDetails.GetInstance().Region;
            }
            catch
            {
                Logger.Error("Error Parsing XML as Gk10 [ XML Message ]" + oDoc.OuterXml);
                throw;
            }
        }

        private string GetCardID(string sEventValue, string sFaultMessage)
        {
            // convert card id (Hex): 43 19 19 19 02 13 10, to 999230 
            string strCard = string.Empty;
            try
            {
                long nValue = long.Parse(sEventValue);
                if ((nValue % 128).ToString("X").Length > 1)
                {
                    strCard = (nValue % 128).ToString("X").Substring(1, 1);
                }
                else
                {
                    strCard = (nValue % 128).ToString("X");
                }

                //=========================================================================
                nValue = nValue / 256;
                if ((nValue % 128).ToString("X").Length > 1)
                {
                    strCard = (nValue % 128).ToString("X").Substring(1, 1) + strCard;
                }
                else
                {
                    strCard = (nValue % 128).ToString("X") + strCard;
                }

                //=========================================================================
                nValue = nValue / 256;
                if ((nValue % 128).ToString("X").Length > 1)
                {
                    strCard = (nValue % 128).ToString("X").Substring(1, 1) + strCard;
                }
                else
                {
                    strCard = (nValue % 128).ToString("X") + strCard;
                }
                for (int iCount = 1; iCount <= sFaultMessage.Length - 1; iCount += 2)
                {
                    strCard = strCard + sFaultMessage.Substring(iCount, 1);
                }
            }
            catch (Exception Ex)
            {
                Logger.Error("GK10","GetCardID", Ex);   
            }
            return strCard;
        }

    }

}
