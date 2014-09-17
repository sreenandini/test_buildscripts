using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using BMC.EventsTransmitter.Messages;
using BMC.EventsTransmitter.Utils;  

namespace BMC.EventsTransmitter
{
    internal class TCPSender : ISender
    {
        TcpClient oClient;
        
        private bool _IsConnected;
        Log Logger = Log.GetInstance(); 
        public bool Initialize(string IPAddress, int Port)
        {
            try
            {
                oClient = new TcpClient();
                oClient.Connect(IPAddress, Port);
                byte[] bufferReceive = new byte[20];
                oClient.Client.Receive(bufferReceive);
                oClient.Client.Send(bufferReceive);
                _IsConnected = true;
              
            }
            catch(Exception Ex)
            {
                _IsConnected = false;
                BMC.EventsTransmitter.Utils.Log.GetInstance().Error("TCPSender", "Initialize", Ex.Message); 
                throw Ex;
            }
            return IsConnected;
        }

        public int  Send(IMessageFormatter Formatter)
        {
            if (_IsConnected)
            {
                NetworkStream oStream = oClient.GetStream();
                oClient.Client.Send(Formatter.GetBytes());
                byte[] buffer = new Byte[100];
                oClient.Client.Receive(buffer);
                AckMessage msg;
                String strResponse = System.Text.Encoding.ASCII.GetString(buffer);
                Logger.Info("Event Response "  + strResponse);
                msg = AckMessage.GetMessage(buffer);
                if (msg.Ack == 6)// to be checked && msg.SequenceNo == bSequenceNo)// ACK
                {
                    return 0;
                }
                return -1;
            }
            else
            {
                throw new Exception("Sender Not Initialized");
            }
        }

        public bool CloseSender()
        {
            if (oClient != null)
            {
                oClient.Close();
            }
            return true;
        }

  
       public  bool IsConnected
        {
            get { return _IsConnected; }
        }


    }

}
