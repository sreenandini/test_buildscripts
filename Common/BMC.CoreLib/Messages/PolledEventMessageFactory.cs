using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.IO;
using System.Xml.Linq;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using System.Xml;

namespace BMC.CoreLib.Messages
{
    /// <summary>
    /// Queue Message Factory
    /// </summary>
    public static class PolledEventMessageFactory
    {
        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <returns>Polled Event message.</returns>
        public static PolledEventMessage CreateMessage()
        {
            return new PolledEventMessage();
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <returns>Polled Event message.</returns>
        public static PolledEventMessage CreateMessage(params object[] values)
        {
            PolledEventMessage pe = new PolledEventMessage();
            pe.SetValues(values);
            return pe;
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>Polled Event message.</returns>
        public static PolledEventMessage CreateMessage(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            ModuleProc PROC = new ModuleProc("PolledEventMessage", "CreateMessage");
            PolledEventMessage result = default(PolledEventMessage);
            MemoryStream ms = null;
            Encoding unicode = Encoding.Unicode;

            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    StreamReader sr = new StreamReader(fs);

                    int actualLength = (int)fs.Length;
                    int desiredLength = (int)fs.Length;

                    if (sr.CurrentEncoding != Encoding.Unicode)
                    {
                        desiredLength *= 2;
                    }

                    fs.Position = 0;
                    char[] originalData = sr.ReadToEnd().ToCharArray();
                    byte[] copiedData = new byte[desiredLength];

                    fs.Position = 0;
                    unicode.GetBytes(originalData, 0, actualLength, copiedData, 0);

                    ms = new MemoryStream(copiedData);
                    sr.Close();
                }

                if (ms != null)
                {
                    result = ParseMessage(ms, Encoding.Unicode);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    Extensions.DisposeObject(ref ms);
                }
            }

            return result;
        }

        ///// <returns>Polled Event message.</returns>
        //public static PolledEventMessage ParseMessage(Stream messageStream, Encoding encoding)
        //{
        //}

        /// <summary>
        /// Parses the message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns>Polled Event message.</returns>
        public static PolledEventMessage ParseMessage(Stream messageStream, Encoding encoding)
        {
            ModuleProc PROC = new ModuleProc("QueueMessageFactory", "ParseMessage");
            PolledEventMessage result = default(PolledEventMessage);

            try
            {
                if (messageStream != null)
                {
                    string rawMessage = string.Empty;
                    messageStream.Position = 0;
                    try
                    {
                        int length = (int)messageStream.Length;
                        byte[] bytes = new byte[length];
                        messageStream.Read(bytes, 0, length);
                        rawMessage = encoding.GetString(bytes);
                    }
                    catch
                    {
                        Log.Info(PROC, "Unable to copy the messageStream stream.");
                    }
                    finally
                    {
                        messageStream.Dispose();
                        messageStream.Close();
                    }
                    
                    XElement xRoot = XElement.Parse(rawMessage);
                    if (xRoot != null &&
                        xRoot.Name.LocalName.IgnoreCaseCompare("Polled_Event"))
                    {
                        result = new PolledEventMessage(rawMessage)
                        {
                            SiteCode = xRoot.GetElementValue("Site_Code"),
                            BarPosition = xRoot.GetElementValue("Bar_Pos"),
                            FaultSource = xRoot.GetElementValueInt("Fault_Source"),
                            FaultType = xRoot.GetElementValueInt("Fault_Type"),
                            InstallationNo = xRoot.GetElementValueInt("Serial_No"), /*xRoot.GetElementValueInt("Installation"),*/
                            FaultMessage = xRoot.GetElementValue("Fault_Message"),
                            Date = xRoot.GetElementValueDateTime("Date"),
                            EventValue = xRoot.GetElementValue("Event_Value"),
                            Description = xRoot.GetElementValue("Description"),
                        };

                        IEnumerable<XElement> counters = xRoot.GetElement("sector").GetElements("counter");
                        if (counters != null)
                        {
                            foreach (XElement counter in counters)
                            {
                                int meterId = counter.GetAttributeValueInt("counterid");
                                long meterValue = counter.GetTextValueInt64(-99);

                                // special cases
                                if (result.FaultSource == 21 && 
                                    result.FaultType == 20)
                                {
                                    result.MetersReceived.Add(meterId, meterValue);
                                    meterValue = 0;
                                }
                                result.Meters.Add(meterId, meterValue);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the message string.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>Message stream.</returns>
        public static Stream GetMessageStream(this PolledEventMessage message, Encoding encoding)
        {
            ModuleProc PROC = new ModuleProc("", "GetMessageString");
            Stream result = default(Stream);

            try
            {
                string rawData = message.RawMessageData;
                byte[] bytes = encoding.GetBytes(rawData);
                result = new MemoryStream(bytes);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
