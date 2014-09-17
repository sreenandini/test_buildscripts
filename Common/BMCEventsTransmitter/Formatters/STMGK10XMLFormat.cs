using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EventsTransmitter.Utils;

namespace BMC.EventsTransmitter
{
    internal class STMGK10XMLFormat : IMessageFormatter
    {

        private GK10 _Message;
        StringBuilder strMessage = new StringBuilder();
        string _MessageStream = string.Empty;
        Log Logger =Log.GetInstance(); 
 
        public string MessageStream
        {
            get { return strMessage.ToString(); }
        }

        public STMGK10XMLFormat(GK10 Message)
        {
            try
            {
                if (Message == null)
                {
                    throw new Exception("Null object[GK10]");
                }
                _Message = Message;
                this.FormatMessage();
            }
            catch (Exception Ex)
            {
                Logger.Error("STMGK10XMLFormat", "Constructor", Ex);
                throw;
            }
        }
        private string FormatMessage()
        {
            strMessage.Append("<BMCRequest><Source>GK10</Source>");
            strMessage.Append("<OperatorId>000</OperatorId>");
            strMessage.Append(_Message.Flags.STMXMLFormat("Flags"));
            strMessage.Append(_Message.PacketNumber.STMXMLFormat("PacketNumber"));
            strMessage.Append(_Message.ProcessId.STMXMLFormat("ProcessId"));
            strMessage.Append(_Message.CommandId.STMXMLFormat("CommandId"));
            strMessage.Append(_Message.SequenceNumber.STMXMLFormat("SequenceNumber"));
            strMessage.Append(_Message.SDSVersion.STMXMLFormat("BMCVersion"));
            strMessage.Append(_Message.MessageType.STMXMLFormat("MessageType"));
            strMessage.Append(_Message.TransactionCode.STMXMLFormat("TransactionCode"));
            //strMessage.Append(_Message.TAB.STMXMLFormat("TAB"));
            strMessage.Append(_Message.ExceptionCode.STMFormat(3).STMXMLFormat("ExceptionCode"));
            strMessage.Append(_Message.CasinoNumber.STMFormat(3).STMXMLFormat("CasinoNumber"));
            strMessage.Append(_Message.CBMessageNumber.STMXMLFormat("CBMessageNumber"));
            strMessage.Append(_Message.SlotDoorFlags.STMXMLFormat("SlotDoorFlags"));
            strMessage.Append(_Message.TransactionDate.STMXMLFormat("TransactionDt"));
            strMessage.Append(_Message.TransactionTime.STMXMLFormat("TransactionTm"));
            strMessage.Append(_Message.QueueID.STMXMLFormat("QueueID"));
            strMessage.Append(_Message.SideID.STMXMLFormat("SideID"));
            strMessage.Append(_Message.SlotNumber.STMXMLFormat("SlotNumber"));
            strMessage.Append(_Message.Stand.STMXMLFormat("Stand"));
            strMessage.Append(_Message.EmployeeID.STMXMLFormat("EmployeeID"));
            strMessage.Append(_Message.HomeCasinoID.STMXMLFormat("HomeCasinoID"));
            strMessage.Append(_Message.PlayerCardNumber.STMXMLFormat("PlayerCardNumber"));
            strMessage.Append(_Message.TransactionAmount.STMXMLFormat("TransactionAmount"));
            strMessage.Append(_Message.OriginalJackpotAmount.STMXMLFormat("OriginalJackpotAmount"));
            strMessage.Append(_Message.JackpotID.STMXMLFormat("JackpotID"));
            strMessage.Append(_Message.BonusPoints.STMXMLFormat("BonusPoints"));
            strMessage.Append(_Message.OptionsFlag.STMXMLFormat("OptionsFlag"));
            strMessage.Append(_Message.TotalBets.STMXMLFormat("TotalBets"));
            strMessage.Append(_Message.TotalWins.STMXMLFormat("TotalWins"));
            strMessage.Append(_Message.TotalCoinDrop.STMXMLFormat("TotalCoinDrop"));
            strMessage.Append(_Message.TotalPlays.STMXMLFormat("TotalPlays"));
            strMessage.Append(_Message.TotalHandPaidJackpots.STMXMLFormat("TotalHandPaidJackpots"));
            strMessage.Append(_Message.TotalMachineFills.STMXMLFormat("TotalMachineFills"));
            strMessage.Append(_Message.TotalBillsAccepted.STMXMLFormat("TotalBillsAccepted"));
            strMessage.Append(_Message.TotalCouponsAccepted.STMXMLFormat("TotalCouponsAccepted"));
            strMessage.Append(_Message.TotalCoinPurchases.STMXMLFormat("TotalCoinPurchases"));
            strMessage.Append(_Message.TotalCoinsCollected.STMXMLFormat("TotalCoinsCollected"));
            strMessage.Append(_Message.TotalNonCashableeCashIn.STMXMLFormat("TotalNonCashableECashIn"));
            strMessage.Append(_Message.TotalNonCashableeCashOut.STMXMLFormat("TotalNonCashableECashOut"));
            strMessage.Append(_Message.TotalCashableeCashIn.STMXMLFormat("TotalCashableECashIn"));
            strMessage.Append(_Message.TotalCashableeCashOut.STMXMLFormat("TotalCashableECashOut"));
            strMessage.Append(_Message.LastSMICode.STMXMLFormat("LastSMICode"));
            strMessage.Append(_Message.TicketInPromo.STMXMLFormat("TicketInPromo"));
            strMessage.Append(_Message.TicketOutPromo.STMXMLFormat("TicketOutPromo"));
            strMessage.Append(_Message.TicketIn.STMXMLFormat("TicketIn"));
            strMessage.Append(_Message.TicketOut.STMXMLFormat("TicketOut"));
            strMessage.Append("".STMXMLFormat("SubCode")); //not yet used 
            strMessage.Append(_Message.Region.STMXMLFormat("Region"));
            strMessage.Append("</BMCRequest>");
            return strMessage.ToString();
        }
 
        public byte[] GetBytes()
        {
            throw new Exception("Bytes Response not supported in XML formatter");
        }
        public byte[] GetBytes(Byte bSequenceNumber)
        {
            throw new Exception("Bytes Response not supported in XML formatter");
        }

       
    }


}
