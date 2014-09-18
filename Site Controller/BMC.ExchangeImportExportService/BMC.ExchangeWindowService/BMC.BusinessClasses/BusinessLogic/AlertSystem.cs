using BMC.Business.CashDeskOperator.WebServices;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace BMC.BusinessClasses.BusinessLogic
{
    public class AlertSystem : IAlert
    {
        #region "Declarations"
            Thread thrAlert = null;
            static AlertSystem invoke = null;
            Queue _Queue = null;
            private ManualResetEvent _processEvent = null;
            int iInterval = 0;
            Proxy _proxy = null;
        #endregion

        public AlertSystem()
        { }

        /// <summary>
        /// Create instance for this class.
        /// </summary>
        /// <returns></returns>
        public static AlertSystem CreateInstance()
        {
            if (invoke == null)
            {
                invoke = new AlertSystem();
            }
            return invoke;
        }

        /// <summary>
        ///  Initialize the settings.
        /// </summary>
        public void Init()
        {
            LogManager.WriteLog("Initalizing AlertSystem .... ", LogManager.enumLogLevel.Info);
            iInterval = Convert.ToInt32(GetAppSettingValue("ServiceInterval"));
            _proxy = DataHelper.GetWebService();
        }

        #region "DoWork"
        /// <summary>
        /// do the actual work.
        /// </summary>
        public void DoWork()
        {
            _processEvent = new ManualResetEvent(true);

            _Queue = new Queue();

            thrAlert = new Thread(new ThreadStart(ProcessAlerts));
            thrAlert.Start();
        }

        /// <summary>
        /// Get the list of alert details to be sent to Enterprise
        /// </summary>
        /// <returns>alert details</returns>
        public List<AlertDetails> GetAlertDetails()
        {
            IList<AlertDetails> lstAlertDetails = null;

            try
            {
                using (AlertDataContext context = new AlertDataContext(DatabaseHelper.GetConnectionString()))
                {
                    lstAlertDetails = context.GetAlertDetails().ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return lstAlertDetails as List<AlertDetails>;
        }

        /// <summary>
        /// Get the list of alerts and send to Enterprise
        /// </summary>
        public void ProcessAlerts()
        {
            try
            {
                while (!_processEvent.WaitOne(iInterval))
                {
                    //Get the alerts to process

                    List<AlertDetails> details = GetAlertDetails();

                    List<AlertDetails> FilteredDetails = details.Where(item => (item.AlertStatus == 0)).ToList();

                    foreach (AlertDetails detail in FilteredDetails)
                        EnQueue(detail);

                    ProcessMessages();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        ///<summary>
        /// export to enterprise
        ///</summary>

        internal void ProcessMessages()
        {
            try
            {
                AlertDetails aDetail = DeQueue();

                //Export to Enterprise
                _proxy.ImportAlertDetails(AlertDataContext.SiteCode, Serialize(aDetail).ToString());
                //using (AlertDataContext context = new AlertDataContext())
                //    context.ImportAlertDetails(AlertDataContext.SiteCode, Serialize(aDetail).ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        #region "Utilities"
        // source: source object instance to serialize
        // target: target file name to write the XML to
        StringWriter Serialize(object source)
        {
            StringWriter stringwriter = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings();
            try
            {

                settings.OmitXmlDeclaration = true;
                settings.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(stringwriter, settings))
                {
                    XmlSerializer serializer = new XmlSerializer(source.GetType());

                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty);

                    serializer.Serialize(writer, source, namespaces);
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }

            return stringwriter;
        }


        /// <summary>
        /// Read the value from App setting using key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetAppSettingValue(string key)
        {
            string Appvalue = string.Empty;

            Appvalue = ConfigManager.Read(key);
            return Appvalue;
        }

        ///<summary>
        /// add the alert to the Queue.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void EnQueue(AlertDetails item)
        {
            try
            {
                _Queue.Enqueue(item);
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
        }

        ///<summary>
        /// get the item from the Queue.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public AlertDetails DeQueue()
        {
            AlertDetails aItem = null;
            try
            {
                aItem = _Queue.Dequeue() as AlertDetails;
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }

            return aItem;
        }
        #endregion

        public void UnInit()
        {
            try
            {
                _processEvent.Set();
                LogManager.WriteLog("Alert System Thread Stopped ", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
