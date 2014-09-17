using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using BMC.EventsTransmitter.Messages;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using BMC.EventsTransmitter.Utils;
using System.Messaging;

namespace BMC.EventsTransmitter
{
    public delegate void TransactionLog(string Message);
    public class EventTransmitter : IRelaucher
    {
        public TransactionLog GetStatus;
        delegate void AsyncExecute();
        static EventTransmitter _EventTransmitter;
        static object _Lock = new object();
        static object _QueueReadLock = new object();
        QueueManager _Message;
        
        string strXML;
        //public GK10 _GK10;
        IMessageFormatter _MessageFormatter;
        bool _IsTransmitting = false;
        Log Logger = Log.GetInstance(); 
        //int iMaxTransmissionRetry;
        private ManualResetEvent _evtTransmitShutDown;
        ISender _Sender;

        ThreadStart _ThreadStartInfo ;
        Thread _TransmitterThread ;
        //Refresh App event 
        //ManualResetEvent m_evntRefreshApp = new ManualResetEvent(false); 
        private EventTransmitter()
        {
            ReadConfig();
            _evtTransmitShutDown = new ManualResetEvent(false);
        }
        public bool IsTransmitting()
        {
            return _IsTransmitting;
        }
        
        public static EventTransmitter GetInstance()
        {
            lock (_Lock)
            {
                if (_EventTransmitter == null)
                {
                    _EventTransmitter = new EventTransmitter();
                    Relauncher.GetInstance().RegisterForUpdate(_EventTransmitter);    
                }
            }

            return _EventTransmitter;
        }

        /// <summary>
        ///  Begin Event Transmission Asyncronously 
        /// </summary>
        public void BeginTransmissionASync()
        {

            try
            {
                if (Settings.IsTransmitterEnabled > 0)
                {
                    if (!this._IsTransmitting)
                    {
                        _evtTransmitShutDown.Reset();
                        _ThreadStartInfo = new ThreadStart(this.Transmit);
                        _TransmitterThread = new Thread(_ThreadStartInfo);
                        _TransmitterThread.Start();
                    }
                    else
                    {
                        Logger.Info("EventTransmitter: Already running");
                    }
                }
                else
                {
                    Logger.Info("EventTransmitter DISABLED");
                }
            }
            catch(Exception Ex) 
            {
                Logger.Error("EventTransmitter", "BeginTransmissionASync", Ex);
            }
        }
        /// <summary>
        ///  Begin Event Transmission 
        /// </summary>
        private void BeginTransmmission()
        {


            try
            {
                //Read configurations
                //Start Transmission 
            }
            catch (Exception Ex)
            {
                Logger.Error("EventTransmitter", "BeginTransmmission", Ex);
            }


        }
        /// <summary>
        /// Sets AutoResetEvent to shutdown transmission. 
        /// </summary>
        public void StopTransmission()
        {
            if (_IsTransmitting)
            {
                _evtTransmitShutDown.Set();
                _Message.ReleaseLock();
                if (_TransmitterThread != null)
                {
                    _TransmitterThread.Join();
                    _TransmitterThread = null;
                }
            }
        }
        /// <summary>
        /// transmmit message sycronously
        /// </summary>
        /// <param name="StrMessage"></param>
        /// <returns></returns>
        public int SendMessage(string StrMessage)
        {
            ISender _RawSender=null;
            try
            {
                int iResponse=-1;
                Logger.Debug("EventTransmitter", "SendMessage", StrMessage);
                //Getting mode of transfer
                _RawSender = SenderFactory.GetSender();
                 Logger.Debug("EventTransmitter", "SendMessage", "Initiate Sender");
                _RawSender.Initialize(Settings.STMServerIP, 0);
                Logger.Debug("EventTransmitter", "SendMessage", "Send Data");
                iResponse=_RawSender.Send(FormatFactory.GetFormatter(StrMessage, false));
                Logger.Debug("EventTransmitter", "SendMessage", "STM Response:" + iResponse.ToString() );
                return iResponse;
            }
            catch (Exception Ex)
            {
                Logger.Error("EventTransmitter", "SendMessage", Ex.Message);
                throw Ex;
            }
            finally
            {
                _RawSender.CloseSender();
            }
        }
        /// <summary>
        /// Transmit Messages from queue to Server
        /// </summary>
        void Transmit()
        {
            try
            {
                UInt32 iSequenceNo = 0;
                Byte bSequenceNo;
                Logger.Info("EventTransmitter: STARTING");
                _Message = new QueueManager(Settings.EventTransmitterSourceQ);
    
                //Getting mode of transfer
               _Sender = SenderFactory.GetSender();

                if (this.InitializeWithRetry())
                {
                    _Message.OpenQueue();

                    while (!_evtTransmitShutDown.WaitOne(10))
                    {

                        Int32 iResponse;
                            try
                            {
                                _IsTransmitting = true;

                                if (iSequenceNo >= 255)
                                {
                                    iSequenceNo = 1;
                                }
                                else
                                {
                                    iSequenceNo++;
                                }
                                bSequenceNo = Convert.ToByte(iSequenceNo);
                                Logger.Info("Reading queue");
                                
                                if (!_Message.IsQueueOpen)
                                {
                                    _Message.OpenQueue();
                                }

                                strXML = _Message.ReadMessage();
                                Logger.Info("Read Complete");
                                SendToExternalLog("SENDING: " + strXML);
                                // parse return message 
                                //Construct STM format message 

                                // Refresh or empty message
                                if (string.IsNullOrEmpty(strXML))
                                {
                                    Logger.Debug("Either Queue refresh or no message received.");
                                    continue;
                                }

                                _MessageFormatter = FormatFactory.GetFormatter(strXML, true);

                                Logger.Debug("Event Request SeqNo[" + iSequenceNo.ToString() + "]" + _MessageFormatter.MessageStream);
                            }
                            catch (MessageFilteredException MesEx)
                            {
                                Logger.Error("EventTransmitter", "Transmit", MesEx.Message);
                                Logger.Info("EventTransmitter", "Transmit", "Committing Queue[Filtered Message]");
                                _Message.Commit();
                                continue;
                            }
                            catch (Exception Ex)
                            {
                                Logger.Error("EventTransmitter", "Transmit", Ex);
                                Logger.Info("EventTransmitter", "Transmit", "Error in message:" + strXML);

                                if (Settings.DeleteMessageOnParseErr == 1)
                                {
                                    Logger.Error("EventTransmitter", "Transmit", "Committing Queue[DeleteMessageOnParseErr]");
                                    _Message.Commit();
                                    continue;
                                }
                                _evtTransmitShutDown.WaitOne(5000);
                            }
                            int iRetryOnNWErr = 0;
                            do
                            {
                                if (_evtTransmitShutDown.WaitOne(50))
                                {
                                    break;
                                }

                                try
                                {
                                    if (iRetryOnNWErr >= Settings.iMaxTransmissionRetry)
                                    {
                                        //Close transmission when maximum retry reached 
                                        throw new Exception(String.Format("Stopping Transmision after Retrying [{0}] times. ", iRetryOnNWErr.ToString()));
                                    }
                                    else
                                    {

                                        //transmit message to server 
                                        iResponse = _Sender.Send(_MessageFormatter);
                                         Logger.Debug("Response from Server: " +  iResponse.ToString());
                                        if (iResponse > 0)// to be checked && msg.SequenceNo == bSequenceNo)// ACK
                                        {
                                             
                                            //Set retry value to MAX+1 to quit loop 
                                            iRetryOnNWErr = Settings.iMaxTransmissionRetry + 1;
                                            //Commiting Queue read After succesfull Transaction 
                                            Logger.Info("Committing Queue");
                                            _Message.Commit();
                                        }
                                            
                                        else if (iResponse==0)
                                        {
                                            Logger.Info("Response from Server [0] Message not processed at server");
                                            //Set retry value to MAX+1 to quit loop 
                                            iRetryOnNWErr = Settings.iMaxTransmissionRetry + 1;
                                            //Commiting Queue read After succesfull Transaction 
                                            Logger.Info("Committing Queue");
                                            _Message.Commit();
                                        }
                                        else
                                        {
                                           
                                            if (Settings.EventTransmit_RetryRepeat == 0)
                                            {
                                                iRetryOnNWErr++;
                                                Logger.Info(string.Format("Retrying to send message (Attempt[{0}]) ", iRetryOnNWErr.ToString()));
                                            }
                                            else
                                            {
                                                Logger.Info("Retrying to send message");
                                                iRetryOnNWErr = 0;
                                            }
                                        }
                                    }
                                }
                                catch (Exception Ex1)
                                {
                                    _evtTransmitShutDown.WaitOne(5000);
                                    Logger.Error("EventTransmitter", "Transmit", Ex1);
                                    //refresh connection and retry till configured number of times 
                                    iRetryOnNWErr++;
                                    _Sender.CloseSender();
                                    _IsTransmitting = false;
                                    _Sender.Initialize(Settings.STMServerIP, Settings.STMServerPort);
                                }
                            } while (iRetryOnNWErr <= Settings.iMaxTransmissionRetry);
                        

                    }
                }
            }
            catch (Exception Ex)
            {
                Logger.Error("EventTransmitter", "Transmit", Ex.Message);
                if (Settings.EventTransmit_RecoverOnError != 0)
                {
                    _evtTransmitShutDown.WaitOne(2000);
                    this.Transmit();
                }
            }
            finally
            {
                this.CloseTransmission(); 
            }
           
        }
        /// <summary>
        /// Close event Transmission 
        /// </summary>
        void CloseTransmission()
        {
            try
            {
               if (_IsTransmitting)
                {
                    Logger.Info("EventTransmitter: Closing Event Transmitter");
                    if (_Sender != null)
                    { 
                        _Sender.CloseSender();
                        _Sender = null;
                    }
                    if (_Message != null)
                    {
                        _Message.CloseQueue();
                        _Message = null;
                    }
                    _IsTransmitting = false;
                
                }
                else
                {
                    Logger.Info("EventTransmitter", "CloseTransmission", "Transmission Already Stopped");
                }
            }
            catch (Exception Ex)
            {
                Logger.Error("EventTransmitter", "CloseTransmission", Ex);
            }
            finally
            {
                //m_evntRefreshApp.Set(); //Trigger for app refresh 
                _IsTransmitting = false;
                Logger.Info("EventTransmitter: STOPPED.");
            }
        }
        /// <summary>
        /// Retries connecting setver untill successfull connection 
        /// </summary>
        bool InitializeWithRetry()
        {
            int iRetries = 1;
            while (iRetries <= Settings.iMaxTransmissionRetry)
            {
                try
                {
                    if (_evtTransmitShutDown.WaitOne(1000))
                    {
                        //iRetries = Settings.iMaxTransmissionRetry;
                        return false;
                    }

                    Logger.Debug("EventTransmitter", "InitializeWithRetry", string.Format("Connecting to server  {0}:{1}", Settings.STMServerIP, Settings.STMServerPort));
                    if (_Sender == null)
                    {
                        Logger.Debug("Re-Initialize Sender");
                        _Sender = new TCPSender();

                    }
                    _Sender.Initialize(Settings.STMServerIP, Settings.STMServerPort);
                    return true;
                }
                catch (Exception Ex)
                {
                    Logger.Error("EventTransmitter", "InitializeWithRetry", Ex);                    
                    if (Settings.EventTransmit_RetryRepeat == 0)
                    {
                        Logger.Debug(string.Format("InitializeWithRetry: Retrying to connect server (Attempt[{0}]) of {1} tries", iRetries.ToString(), Settings.iMaxTransmissionRetry.NulltoString("1")));
                        iRetries++;
                    }
                    else
                    {
                        Logger.Debug("InitializeWithRetry: Infinite retry to connect server");
                        iRetries = 0;
                    }
                }
            }
            Logger.Debug("EventTransmitter", "InitializeWithRetry", "Existing Method");
            return true;
        }
        public void RefreshApp()
        {
            this.StopTransmission();
            //m_evntRefreshApp.WaitOne(); //Wait untill transmission stops completely.
            //m_evntRefreshApp.Reset();   //Reset once transmission stopped. 
            Logger.Info("EventTransmitter", "RefreshApp", "Refreshing Config Values");
            try
            {
                ReadConfig();
            }
            catch(Exception Ex)
            {
                Logger.Error("EventTransmitter", "RefreshApp Read config", Ex);
            }
            this.BeginTransmissionASync();  
        }
        void ReadConfig()
        {
            try
            {
               Settings.Initialize();
            }
            catch (Exception Ex)
            {
                Logger.Error("EventTransmitter","ReadConfig", "Error While reading Initial Config");
                throw Ex;
            }
        }
        public int IsTransmitterEnabled()
        {
            return Settings.IsTransmitterEnabled;
        }
        public void SendToExternalLog(string str)
        {
            if(GetStatus!=null)
            {
                GetStatus(str);
            }
        }
        /// <summary>
        /// Crates a queue if not exists in the path in the given path  
        /// Exceptions has to handled by the client 
        /// </summary>
        /// <returns></returns>
        public static WriteOnlyQueue GetEventsQueue()
        {
            try
            {
               return  new WriteOnlyQueue(ConfigurationManager.AppSettings["EventTrasnmitterSrcQ"].NulltoString(@".\private$\STMQueue"));   
            }
            catch(Exception Ex)
            {
               Log.GetInstance().Error("EventTransmitter","GetEventsQueue" , Ex.Message);
                throw;
            }
        }
    }
}
