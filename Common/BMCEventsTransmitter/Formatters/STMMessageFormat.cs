using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EventsTransmitter.Utils;

namespace BMC.EventsTransmitter
{
    internal class STMMessageFormat : IMessageFormatter
    {

        private GK10 _Message;
        StringBuilder strMessage = new StringBuilder();
        private const Byte DLE = 0x10;
        private const Byte STX = 0x02;
        private const Byte ETX = 0x03;
        string _MessageStream = string.Empty;
        Log Logger =Log.GetInstance(); 
        public string GetMessageString()
        {
            return strMessage.ToString(); 
        }
        public string MessageStream
        {
            get { return _MessageStream; }
        }

        public STMMessageFormat(GK10 Message)
        {
            if (Message == null)
            {
                throw new Exception("Null object[GK10]");
            }
            _Message = Message;
        }
        private string FormatMessage()
        {   
            strMessage.Append(_Message.Flags.STMFormat(1));                     //1
            strMessage.Append(_Message.PacketNumber.STMFormat(1));              //1
            strMessage.Append(_Message.ProcessId.STMFormat(1));                 //1
            strMessage.Append(_Message.CommandId.STMFormat(1));                 //1
            strMessage.Append(_Message.SequenceNumber.STMFormat(1));            //1
            strMessage.Append(_Message.SDSVersion.STMFormat(5));                //5
            strMessage.Append(_Message.MessageType.STMFormat(2));	            //2
            strMessage.Append(_Message.TransactionCode.STMFormat(2));	        //2
            //strMessage.Append(_Message.TAB);                                    //1
            strMessage.Append(_Message.ExceptionCode.STMFormat(3));	            //3
            strMessage.Append(_Message.CasinoNumber.STMFormat(3));	            //3
            strMessage.Append(_Message.CBMessageNumber.STMFormat(3));	        //3
            strMessage.Append(_Message.SlotDoorFlags.STMFormat(3));	            //3
            strMessage.Append(_Message.TransactionDate.STMFormat(8));	        //8
            strMessage.Append(_Message.TransactionTime.STMFormat(6));	        //6
            strMessage.Append(_Message.QueueID.STMFormat(6));	                //6
            strMessage.Append(_Message.SideID.STMFormat(1));	                //1
            strMessage.Append(_Message.SlotNumber.STMFormat(6));	            //6
            strMessage.Append(_Message.Stand.STMFormat(6));	                    //6
            strMessage.Append(_Message.EmployeeID.STMFormat(6));	            //6
            strMessage.Append(_Message.HomeCasinoID.STMFormat(3));	            //3
            strMessage.Append(_Message.PlayerCardNumber.STMFormat(10));	        //10
            strMessage.Append(_Message.TransactionAmount.STMFormat(8));	        //8
            strMessage.Append(_Message.OriginalJackpotAmount.STMFormat(8));	    //8
            strMessage.Append(_Message.JackpotID.STMFormat(2));	                //2
            strMessage.Append(_Message.BonusPoints.STMFormat(4));	            //4
            strMessage.Append(_Message.OptionsFlag.STMFormat(3));	            //3
            strMessage.Append(_Message.TotalBets.STMFormat(10));	            //10
            strMessage.Append(_Message.TotalWins.STMFormat(10));	            //10
            strMessage.Append(_Message.TotalCoinDrop.STMFormat(10));	        //10
            strMessage.Append(_Message.TotalPlays.STMFormat(10));	            //10
            strMessage.Append(_Message.TotalHandPaidJackpots.STMFormat(10));	//10
            strMessage.Append(_Message.TotalMachineFills.STMFormat(10));	    //10
            strMessage.Append(_Message.TotalBillsAccepted.STMFormat(10));	    //10
            strMessage.Append(_Message.TotalCouponsAccepted.STMFormat(10));	    //10
            strMessage.Append(_Message.TotalCoinPurchases.STMFormat(10));	    //10
            strMessage.Append(_Message.TotalCoinsCollected.STMFormat(10));	    //10
            strMessage.Append(_Message.TotalNonCashableeCashIn.STMFormat(10));	//10
            strMessage.Append(_Message.TotalNonCashableeCashOut.STMFormat(10));	//10
            strMessage.Append(_Message.TotalCashableeCashIn.STMFormat(10));	    //10
            strMessage.Append(_Message.TotalCashableeCashOut.STMFormat(10));	//10
            strMessage.Append(_Message.LastSMICode.STMFormat(8));	            //8
            strMessage.Append(_Message.TicketInPromo.STMFormat(8));	            //8
            strMessage.Append(_Message.TicketOutPromo.STMFormat(8));	        //8
            strMessage.Append(_Message.TicketIn.STMFormat(8));	                //8
            strMessage.Append(_Message.TicketOut.STMFormat(8));	                //8
            return strMessage.ToString();
        }
        private Byte[] Encapsulate(string Message, Byte bSequenceNumber)
        {
            byte[] byteBuffer = new byte[Message.Length + 4];
            byte[] byteArrayPayload = null;
            int byteBufferLength = 0;
            byteBuffer[byteBufferLength++] = DLE; //0x10
            byteBuffer[byteBufferLength++] = STX;
            byteArrayPayload = Encoding.ASCII.GetBytes(Message);
            Array.Copy(byteArrayPayload, 0, byteBuffer, byteBufferLength, byteArrayPayload.Length);
            byteBufferLength += byteArrayPayload.Length;
            //Message End 2 bytes - DLE followed by ETX
            byteBuffer[byteBufferLength++] = DLE;//0x10
            byteBuffer[byteBufferLength++] = ETX;//0x03
            byteBuffer[3] = bSequenceNumber;
            byteBuffer[6] = bSequenceNumber;
            _MessageStream = System.Text.Encoding.ASCII.GetString(byteBuffer);
            return byteBuffer;
        }
        public byte[] GetBytes()
        {
            try
            {
                byte[] byteBuffer = this.Encapsulate(this.FormatMessage(), Convert.ToByte('0'));
                return byteBuffer;
            }
            catch
            {
                Logger.Error("STMMessageFormat","GetBytes","Error while formatting GK10 to STM format");
                throw;
            }
        }
        public byte[] GetBytes(Byte bSequenceNumber)
        {
            try
            {
                byte[] byteBuffer = this.Encapsulate(this.FormatMessage(), bSequenceNumber);
                return byteBuffer;
            }
            catch
            {
                Logger.Error("STMMessageFormat", "GetBytes(" + bSequenceNumber .ToString()+ ")", "Error while formatting GK10 to STM format");
                throw;
            }
        }

    }


}
