using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Xml.Linq;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Messages
{
    /// <summary>
    /// Polled Event Message Formatter
    /// </summary>
    public class PolledEventMessageFormatter 
        : DisposableObject, IMessageFormatter
    {
        internal const short VT_BSTR = 8;
        internal const short VT_LPSTR = 30;
        internal const short VT_LPWSTR = 31;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolledEventMessageFormatter"/> class.
        /// </summary>
        public PolledEventMessageFormatter() { }

        #region IMessageFormatter Members

        /// <summary>
        /// Gets the type of the message body.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Body type.</returns>
        private int GetMessageBodyType(Message message)
        {
            int variantType = -1;
            try
            {
                variantType = message.BodyType;
            }
            catch (System.InvalidOperationException) { variantType = -1; }
            catch (Exception) { variantType = -1; }
            return variantType;
        }

        /// <summary>
        /// When implemented in a class, determines whether the formatter can deserialize the contents of the message.
        /// </summary>
        /// <param name="message">The <see cref="T:System.Messaging.Message"/> to inspect.</param>
        /// <returns>
        /// true if the formatter can deserialize the message; otherwise, false.
        /// </returns>
        public bool CanRead(Message message)
        {
            if (message == null)
                return false;

            int variantType = this.GetMessageBodyType(message);
            if (variantType == VT_BSTR ||
                variantType == VT_LPSTR ||
                variantType == VT_LPWSTR) return true;
            return false;
        }

        /// <summary>
        /// When implemented in a class, reads the contents from the given message and creates an object that contains data from the message.
        /// </summary>
        /// <param name="message">The <see cref="T:System.Messaging.Message"/> to deserialize.</param>
        /// <returns>The deserialized message.</returns>
        public object Read(Message message)
        {
            if (message == null) return null;

            int variantType = this.GetMessageBodyType(message);
            switch (variantType)
            {
                case VT_LPSTR:
                    {
                        return PolledEventMessageFactory.ParseMessage(message.BodyStream, Encoding.ASCII);
                    }

                case VT_BSTR:
                case VT_LPWSTR:
                    {
                        return PolledEventMessageFactory.ParseMessage(message.BodyStream, Encoding.Unicode);
                    }

                default:
                    return default(PolledEventMessage);
            }
        }

        /// <summary>
        /// When implemented in a class, serializes an object into the body of the message.
        /// </summary>
        /// <param name="message">The <see cref="T:System.Messaging.Message"/> that will contain the serialized object.</param>
        /// <param name="obj">The object to be serialized into the message.</param>
        public void Write(Message message, object obj)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Write");

            try
            {
                if ((obj == null) ||
                    (obj.GetType() != typeof(PolledEventMessage)))
                {
                    Log.Info(PROC, "Invalid object passed.");
                    return;
                }

                PolledEventMessage objPE = obj as PolledEventMessage;
                message.BodyStream = objPE.GetMessageStream(Encoding.Unicode);
                message.BodyType = VT_LPWSTR;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override object Clone()
        {
            return new PolledEventMessageFormatter();
        }

        #endregion
    }
}
