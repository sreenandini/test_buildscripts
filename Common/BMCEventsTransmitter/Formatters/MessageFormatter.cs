using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BMC.EventsTransmitter.Utils;
 
namespace BMC.EventsTransmitter
{
    public enum MessageType
    {
        GK10
    }
    internal interface IMessageFormatter
    {
        Byte[] GetBytes();
        Byte[] GetBytes(Byte bSequenceNumber);
        string MessageStream { get; }
    }
    internal class FormatFactory  
    {
        static STMMessageFilter _STMMessageFilter;
        //public static IMessageFormatter GetFormatter(string XmlMessage)
        //{
        //   GK10 _BMCMessage = new GK10(XmlMessage);
        //   return new STMMessageFormat(_BMCMessage);
        //}
        public static IMessageFormatter GetFormatter(string XmlMessage,bool FilterMessage)
        {
            //Events from slot machine 
            if (XmlMessage.StartsWith("<Polled_Event>"))
            {
                GK10 _BMCMessage = new GK10(XmlMessage);
                if (FilterMessage)
                {
                    if (_STMMessageFilter == null)
                    {
                        _STMMessageFilter = STMMessageFilter.GetInstance();
                    }
                    if (_STMMessageFilter.IsFiltered(_BMCMessage.ExceptionCode))
                    {
                        throw new MessageFilteredException(_BMCMessage.ExceptionCode);
                    }
                }
                return new STMGK10XMLFormat(_BMCMessage);
            }
            else // /send unprocessed data
            {
                return new RawDataFormatter(XmlMessage);
            }
            
            return null;
        }

    }
 
}
