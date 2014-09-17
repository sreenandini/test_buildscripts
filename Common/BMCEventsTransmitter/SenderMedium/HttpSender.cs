using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EventsTransmitter.STMServer;
using System.ServiceModel;
using System.Configuration;
using BMC.EventsTransmitter.Utils;

namespace BMC.EventsTransmitter
{
   internal class HttpSender:ISender
    {
       XMLDRServiceClient oClient = null;
       private bool _IsConnected;
       public bool IsConnected
       {
           get { return _IsConnected; }
       }

        public bool Initialize(string IPAddress, int Port)
        {
            //string uri = ConfigurationManager.AppSettings["STMUrl"].NulltoString();
            oClient = new XMLDRServiceClient(new BasicHttpBinding(), new EndpointAddress(Settings.STMServerIP));
            _IsConnected = true;
            return true;
         }  

        public int Send(IMessageFormatter Formatter)
        {
           return oClient.process(Formatter.MessageStream);
        }

        public bool CloseSender()
        {
            oClient = null;
            _IsConnected = false;
            return true;
        }


    }
}
