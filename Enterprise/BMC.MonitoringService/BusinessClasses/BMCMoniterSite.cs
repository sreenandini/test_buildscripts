using System;
using System.Threading;
using System.Collections.Generic;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using System.Xml.Linq;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.BusinessClasses.Proxy;
using Timers = System.Timers;
using System.Linq;

namespace BMC.MonitoringService
{
    class BMCMoniterSite
    {
        private Int32 checkSiteConnectivityInterval = 5; //In Seconds
        private Double MaxGraceTimeForHourlyRun = 5;//In Minites
        private Double MaxGraceTimeForReadRun = 15;//In Minites
        private bool isSendMultipleSTMMessages = false;
        private bool isSTMAlertRequered = false;
        private bool isStop = false;
        int dBChecker = 60;//In Minites
        List<SpecificSiteDetails> siteDetails;
        DateTime presentDate;
        private DataHelper dataHelper = null;
        private Int32 readTimeInterval = 15;
        private DateTime ReadListUpdateTime;
        private string defaultReadTime = string.Empty;

        private ManualResetEvent _mreShutDown = new ManualResetEvent(false);
        private ManualResetEvent _hourlyChecker = new ManualResetEvent(false);
        private ManualResetEvent _readChecker = new ManualResetEvent(false);

        private Thread workerThread;
        private Thread hourlyStartThread;
        private Thread readFirstThread;


        private void StartMoniterSite()
        {
            try
            {
                LogManager.WriteLog("StartMoniterSite entry", LogManager.enumLogLevel.Info);
                int heartBitCounter = 0;
                int secondCounter = (dBChecker * 60);
                while (!_mreShutDown.WaitOne(1000))
                {
                    try
                    {
                        if (secondCounter++ >= (dBChecker * 60))
                        {
                            secondCounter = 0;
                            siteDetails = dataHelper.GetSpecificSiteDetails();
                        }
                        if (heartBitCounter++ >= checkSiteConnectivityInterval)
                        {
                            CheckConnectivity();
                            heartBitCounter = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                }
                LogManager.WriteLog("StartMoniterSite end", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CheckConnectivity()
        {
            try
            {
                for (int counter = 0; counter < siteDetails.Count; counter++)
                {
                    try
                    {
                        Proxy proxy = new Proxy(siteDetails[counter].WebURL, Convert.ToBoolean(siteDetails[counter].IsCertificateRequired), siteDetails[counter].CertificateIssuer);
                        bool pr = proxy.CheckConnectivity();
                        siteDetails[counter].isSTMAlertSend = false;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                        if (!isSendMultipleSTMMessages && siteDetails[counter].isSTMAlertSend) return;
                        dataHelper.UpdateSiteDownAlert(siteDetails[counter].Site_ID);
                        siteDetails[counter].isSTMAlertSend = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void Stop()
        {
            try
            {
                if (!isSTMAlertRequered) return;
                isStop = true;
                _mreShutDown.Set();
                _hourlyChecker.Set();
	           _readChecker.Set();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void Start()
        {
            try
            {
                LogManager.WriteLog("StartMethod entry", LogManager.enumLogLevel.Info);
                if (!isSTMAlertRequered)
                {
                    LogManager.WriteLog("STM Alert Not enabled.\r\n StartMethod end", LogManager.enumLogLevel.Info);
                    return;
                }
                workerThread = new Thread((StartMoniterSite)) { Name = "MoniterSiteThread" };
                workerThread.Priority = ThreadPriority.Normal;
                workerThread.Start();
                hourlyStartThread = new Thread((HourlyTimeCalculator)) { Name = "hourlyStartThread" };
                hourlyStartThread.Priority = ThreadPriority.Normal;
                hourlyStartThread.Start();
                readFirstThread = new Thread((ReadChecker)) { Name = "readFirstThread" };
                readFirstThread.Priority = ThreadPriority.Normal;
                readFirstThread.Start();
                LogManager.WriteLog("StartMethod end", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public BMCMoniterSite()
        {
            try
            {
                LogManager.WriteLog("BMCMoniterSite ==> Constructor BMCMoniterSite entry", LogManager.enumLogLevel.Info);
                Int32 graceTime = 300;
                ReadIntFromConfig("dBChecker", 0, 60, ref dBChecker);
                isSTMAlertRequered = BMC.CoreLib.Extensions.GetAppSettingValueBool("STMAlertRequired", false);
                if (!isSTMAlertRequered)
                {
                    LogManager.WriteLog("BMCMoniterSite ==> Constructor STM Alert Not enabled.\r\nBMCMoniterSite end", LogManager.enumLogLevel.Info);
                    return;
                }
                ReadIntFromConfig("checkSiteConnectivityInterval", 0, 15, ref checkSiteConnectivityInterval);
                ReadIntFromConfig("MaxGraceTimeForHourlyRun", 0, 5, ref graceTime);
                MaxGraceTimeForHourlyRun = graceTime;
                ReadIntFromConfig("MaxGraceTimeForReadRun", 0, 15, ref graceTime);
                MaxGraceTimeForReadRun = graceTime;
                
                try
                {
                    isSendMultipleSTMMessages = Convert.ToString(ConfigManager.Read("SendMultipleSTMAlert")).Trim() == "1";
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    isSendMultipleSTMMessages = false;
                }
                ReadIntFromConfig("ReadTimeInterval", 0, 15, ref readTimeInterval);
                try
                {
                    string readListUpdateTime = Convert.ToString(ConfigManager.Read("ReadListUpdateTime")).Trim();
                    if (!string.IsNullOrEmpty(readListUpdateTime) && readListUpdateTime.Contains(":"))
                        ReadListUpdateTime = DateTime.Now.Date
                            .AddHours(Convert.ToInt32(readListUpdateTime.Split(':')[0]))
                            .AddMinutes(Convert.ToInt32(readListUpdateTime.Split(':')[1]));
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    ReadListUpdateTime = DateTime.Now;
                }
                try
                {
                    defaultReadTime = Convert.ToString(ConfigManager.Read("DefaultReadTime")).Trim();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    defaultReadTime = "06:30";
                }
                dataHelper = new DataHelper();
                presentDate = DateTime.Now.Date;
                LogManager.WriteLog("BMCMoniterSite ==> Constructor BMCMoniterSite end", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private void HourlyTimeCalculator()
        {
            try
            {
                LogManager.WriteLog("BMCMoniterSite ==> Method HourlyTimeCalculator entry", LogManager.enumLogLevel.Info);
                
                //Ignore minutes and seconds for hourly
                DateTime hourly = DateTime.Now.Date.AddHours(DateTime.Now.Hour);
                long seconds = 0;
                TimeSpan span;
                //Check current time is less then the hourly time
                if (DateTime.Now.CompareTo(hourly.AddMinutes(MaxGraceTimeForHourlyRun)) < 0)
                {
                    span = hourly.AddMinutes(MaxGraceTimeForHourlyRun).Subtract(DateTime.Now);
                }
                else //If current time is greater then the Hourly time
                {
                    LogManager.WriteLog("BMCMoniterSite ==> Check hourly run for present hour", LogManager.enumLogLevel.Info);
                    CheckHourlyRun();//Check hourly for First hour
                    span = hourly.AddHours(1).AddMinutes(MaxGraceTimeForHourlyRun).Subtract(DateTime.Now);
                }
                LogManager.WriteLog("BMCMoniterSite ==> Method HourlyTimeCalculator Span Time for sleep(Minutes) :" + span.TotalMinutes, LogManager.enumLogLevel.Info);
                seconds = Convert.ToInt64(span.TotalSeconds) + 5;

                Int64 counter = 0;
                while (!_hourlyChecker.WaitOne(1000))
                {
                    if (counter++ > seconds)
                        break;
                }
                if (isStop) return;
                CheckHourlyRun();//Check hourly for present hour
                while (!_hourlyChecker.WaitOne(60 * 60 * 1000))
                {
                    CheckHourlyRun();
                }
                LogManager.WriteLog("BMCMoniterSite ==> Method HourlyTimeCalculator exit", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        
        private void ReadIntFromConfig(string key, Int32 minValue, Int32 defaultValue, ref Int32 value)
        {
            try
            {
                value = Convert.ToInt32(ConfigManager.Read(key));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            value = (value <= minValue) ? defaultValue : value;
        }

        private void CheckHourlyRun()
        {
            try
            {
                LogManager.WriteLog("BMCMoniterSite ==> Method CheckHourlyRun entry", LogManager.enumLogLevel.Info);
                if (DateTime.Now.Hour == 0) dataHelper.ResetHourlyNotRun();
                siteDetails = dataHelper.GetSpecificSiteDetails();
                List<SiteStatusEntity> _SiteStatusEntity = dataHelper.GetSiteStatusByID(0);
                
                foreach (SpecificSiteDetails item in siteDetails)
                {
                    try
                    {
                        XElement siteStatusXML = _SiteStatusEntity.Find(X => X.Site_ID == item.Site_ID).Site_Status;
                        if (IsHourlyRun(siteStatusXML)) continue;
                        if (item.Site_ID > 0)
                        {
                            LogManager.WriteLog("BMCMoniterSite ==> Hourly Not Run for Site ID : " + item.Site_ID, LogManager.enumLogLevel.Info);
                            dataHelper.UpdateHourlyNotRun(item.Site_ID);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            
            LogManager.WriteLog("BMCMoniterSite ==> Method CheckHourlyRun exit", LogManager.enumLogLevel.Info);
        }

        private XElement GetLatestStatus(string siteCode, Int32 siteID)
        {
            LogManager.WriteLog("BMCMoniterSite ==> Method GetLatestStatus entry", LogManager.enumLogLevel.Info);

            try
            {
                Proxy pro = new Proxy(siteCode);
                String xml = pro.GetSiteStatus();
                if (!string.IsNullOrEmpty(xml.Trim()))
                {
                    dataHelper.UpdateSiteStatus(xml, siteCode);
                    return XElement.Parse(xml);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("BMCMoniterSite ==> Method GetLatestStatus exit", LogManager.enumLogLevel.Info);
            return null;
        }

        private bool IsHourlyRun(XElement siteStatusXML)
        {
            LogManager.WriteLog("BMCMoniterSite ==> Method IsHourlyRun entry", LogManager.enumLogLevel.Info);
            try
            {
                if (siteStatusXML != null)
                {
                    XElement result = siteStatusXML.Descendants("HourlyReadHour").FirstOrDefault();
                    if (result != null && !string.IsNullOrEmpty(result.Value.Trim()))
                    {
                        if (Convert.ToInt32(result.Value) == DateTime.Now.Hour)
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("BMCMoniterSite ==> Method IsHourlyRun exit", LogManager.enumLogLevel.Info);

            return false;
        }

        private bool IsReadRun(XElement siteStatusXML, DateTime date)
        {
            LogManager.WriteLog("BMCMoniterSite ==> Method IsReadRun entry", LogManager.enumLogLevel.Info);
            try
            {
                if (siteStatusXML != null)
                {
                    XElement result = siteStatusXML.Descendants("ReadDate").FirstOrDefault();
                    if (result != null && !string.IsNullOrWhiteSpace(result.Value))
                    {
                        if (Convert.ToDateTime(result.Value).Date >= date.Date)
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("BMCMoniterSite ==> Method IsReadRun exit", LogManager.enumLogLevel.Info);
            return false;
        }

        private bool CheckReadRun(int site_ID)
        {
            LogManager.WriteLog("BMCMoniterSite ==> Method CheckReadRun entry", LogManager.enumLogLevel.Info);
            try
            {
                List<SiteStatusEntity> _SiteStatusEntity = dataHelper.GetSiteStatusByID(site_ID);
                if (_SiteStatusEntity != null)
                {
                    XElement siteStatusXml = _SiteStatusEntity[0].Site_Status;
                    if (IsReadRun(siteStatusXml, DateTime.Now))
                        return true;
                    LogManager.WriteLog("BMCMoniterSite ==> Read not run for the site("+site_ID+").", LogManager.enumLogLevel.Info);
                    dataHelper.UpdateReadNotRun(site_ID);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            LogManager.WriteLog("BMCMoniterSite ==> Method CheckReadRun exit", LogManager.enumLogLevel.Info);
            return true;
        }

        private void ReadChecker()
        {
            LogManager.WriteLog("BMCMoniterSite ==> Method ReadChecker entry", LogManager.enumLogLevel.Info);
            try
            {
                List<ReadList> list = dataHelper.GetReadList(null,defaultReadTime);
                do
                {
                    try
                    {
                        if (!DateTime.Now.Date.Equals(presentDate))
                        {
                            ResetIsProcessed(list);
                        }
                        if (DateTime.Now.Subtract(ReadListUpdateTime).TotalSeconds >= 0)
                        {
                            ReadListUpdateTime = ReadListUpdateTime.AddDays(1);
                            list = dataHelper.GetReadList(list,defaultReadTime);
                        }
                        for (int i = 0; i < list.Count; i++)
                        {
                            
                            if (!list[i].IsProcessed && list[i].ReadTime.Contains(":") &&
                                (DateTime.Now.Subtract(DateTime.Now.Date
                                .AddHours(Convert.ToInt32(list[i].ReadTime.Split(':')[0]))
                                .AddMinutes(Convert.ToInt32(list[i].ReadTime.Split(':')[1]))
                                .AddMinutes(MaxGraceTimeForReadRun)).TotalSeconds >= 0))
                            {
                                LogManager.WriteLog("BMCMoniterSite ==> ReadChecker Site ID => " + list[i].Site_ID + " Read time = " + list[i].ReadTime, LogManager.enumLogLevel.Info);
                                if(CheckReadRun(list[i].Site_ID))
                                    list[i].IsProcessed = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                } while (!_readChecker.WaitOne(readTimeInterval*60*1000));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("BMCMoniterSite ==> Method ReadChecker exit", LogManager.enumLogLevel.Info);

        }

        void ResetIsProcessed(List<ReadList> present)
        {
            try
            {
                presentDate = DateTime.Now.Date;
                if (present != null && present.Count > 0)
                {
                    present = present.Select(X => new ReadList { Site_ID = X.Site_ID, Site_Code = X.Site_Code, ReadTime = X.ReadTime, IsProcessed = false }).ToList<ReadList>();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
