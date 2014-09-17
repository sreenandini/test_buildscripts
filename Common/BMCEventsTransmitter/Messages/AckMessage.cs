using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EventsTransmitter.Utils;

namespace BMC.EventsTransmitter.Messages
{

    [Serializable]
    public class AckMessage
    {
        static Log Logger = Log.GetInstance(); 
        private const Byte DLE = 0x10;
        private const Byte STX = 0x02;
        private const Byte ETX = 0x03;

        public byte Flags { get; set; }

        public byte PacketNo { get; set; }

        public byte ProcessID { get; set; }

        public byte CommandID { get; set; }

        public byte SequenceNo { get; set; }

        public byte SpacePad1 { get; set; } //space holder byte 

        public byte Ack { get; set; }

        public static int Size
        {
            get
            {
                return 7;
            }
        }

        public static AckMessage GetMessage(byte[] buffer)
        {
            AckMessage message = null;
            string strResponse=string.Empty;
            try
            {
                int index = -1;
                if (buffer != null &&
                    buffer.Length > (4 + Size))
                {
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        if (buffer[i] == 0x0)
                        {
                           
                            index = (i - 3);
                            break;
                        } 
                        strResponse = strResponse + string.Format("[{0}]", buffer[i].ToString());  
                    }
                }

                if (index > -1)
                {
                    message = new AckMessage();
                    message.Ack = buffer[index--];
                    message.SequenceNo = buffer[index--];
                    message.SpacePad1 = buffer[index--];
                    message.CommandID = buffer[index--];
                    message.ProcessID = buffer[index--];
                    message.PacketNo = buffer[index--];
                    message.Flags = buffer[index--];

                }
                else
                {
                    message = new AckMessage() { Ack = 17, SequenceNo = 0 }; //Null Response from server 
                    Logger.Error("Empty Response from server.Setting ACK:15 SequenceNo=0."); 
                }
                if (message == null)
                {
                    message = new AckMessage() { Ack = 16, SequenceNo = 0 }; //Null Response from server 
                    Logger.Error("Empty Response from server.Setting ACK:16 SequenceNo=0.");
                }
                  Logger.Info("Event Response [STRING]" + strResponse);
            }
            catch(Exception Ex) 
            {
                message = new AckMessage() { Ack =15,SequenceNo=0 };
                Logger.Error("Error parsing Response Message.Setting ACK:17 SequenceNo=0." + Ex.Message); 
            }
           
            return message;
        }
    }
}
