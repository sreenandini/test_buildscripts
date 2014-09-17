using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Helpers;
using BMC.EBSComms.Contracts.Dto;
using BMC.EBSComms.Contracts.Interfaces.SMS2EBS;
using BMC.EBSComms.Contracts.Messages.SMS2EBS;
using BMC.EBSComms.Contracts.Proxies;
using BMC.EBSComms.DataLayer.Dto;

namespace BMC.EBSComms.Server
{
    public partial class EBSCommServer
    {
        private const int SEND_SUCCEEDED = 100;
        private const int SEND_SKIPPED = 200;
        private const int SEND_FAILED = -1;

        private const string LASTMSGID_SEND = "EBSLastMessageId_Send";

        private string _ebsEndpointUrl = string.Empty;
        private EndpointAddress _ebsEndpointAddress = null;
        private Binding _sentToEBSBinding = null;

        private void InitializeSendDataToEBSThread()
        {
            try
            {
                BMC.CoreLib.Extensions.CreateThreadAndStart(new ThreadStart(this.OnListen_SendDataToEBS));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void OnListen_SendDataToEBS()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Method");

            try
            {
                LogManager.WriteLog("SendDataToEBS - Started.", LogManager.enumLogLevel.Info);

                int Interval = BMC.CoreLib.Extensions.GetAppSettingValueInt("TimerIntervalinSecs", 60) * 1000;
                LogManager.WriteLog("SendDataToEBS - Interval: " + Interval.ToString(), LogManager.enumLogLevel.Info);

                DLSettingDto setting = _di.GetSettings();
                _ebsEndpointUrl = setting.EBSEndPointURL;
                _ebsEndpointAddress = new EndpointAddress(_ebsEndpointUrl);
                _sentToEBSBinding = EBSCommClientFactory.CreateBinding(IS_EBS_13_0);
                LogManager.WriteLog("SendDataToEBS - EndPOintURL: " + _ebsEndpointUrl, LogManager.enumLogLevel.Info);

                IExecutorService exec = this.Executor;
                do
                {
                    try
                    {
                        setting = _di.GetSettings();
                        if (setting.IsEnabled && setting.SendDataToEBS)
                        {
                            int recordStatus = SEND_FAILED;
                            int id = -1;
                            XElement newValue = null;
                            DataTable dt = _di.GetUnprocessedRecords();
                            LogManager.WriteLog("[BMC->EBS] SendDataToEBS - Unprocessed Record Count: " + dt.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                            foreach (DataRow dr in dt.Rows)
                            {
                                recordStatus = -1;
                                id = -1;
                                try
                                {
                                    id = dr.GetFieldValue<int>("EH_ID");
                                    string siteCode = dr.GetFieldValue<string>("EH_SiteCode");
                                    Log.InfoV(PROC, "[BMC->EBS] SendDataToEBS - Start Processing Record Id : {0}, Site Code : {1}", id, siteCode);

                                    string type = dr.GetFieldValue<string>("EH_TYPE");
                                    string value = dr.GetFieldValue<string>("EH_VALUE");
                                    bool isDeleted = dr.GetFieldValue<bool>("EH_IsDeleted");
                                    bool proceed = true;
                                    if (!value.IsEmpty())
                                    {
                                        try
                                        {
                                            newValue = XElement.Parse(value);
                                        }
                                        catch
                                        {
                                            proceed = false;
                                            recordStatus = SEND_SKIPPED;
                                        }
                                    }
                                    else
                                    {
                                        proceed = false;
                                        recordStatus = SEND_SKIPPED;
                                    }

                                    if (proceed &&
                                        this.SendDataToEBS_AllOrSingleSite(siteCode, id, type, newValue, isDeleted))
                                    {
                                        recordStatus = SEND_SUCCEEDED;
                                        LogManager.WriteLog("[BMC->EBS] Successfully sent data to EBS. ID: " + id.ToString(), LogManager.enumLogLevel.Info);
                                    }
                                    else
                                    {
                                        LogManager.WriteLog("[BMC->EBS] Failed to send data to EBS. ID: " + id.ToString(), LogManager.enumLogLevel.Info);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogManager.WriteLog("[BMC->EBS] Failed to send data to EBS. ID: " + id.ToString(), LogManager.enumLogLevel.Info);
                                    ExceptionManager.Publish(ex);
                                }
                                finally
                                {
                                    _di.UpdateRecordStatus(id, recordStatus);
                                }

                                if (recordStatus == SEND_FAILED) break;
                                if (exec.WaitForShutdown(10)) break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(ex);
                    }
                } while (!exec.WaitForShutdown(Interval));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private bool SendDataToEBS_AllOrSingleSite(string siteCode, int ehID, string type, XElement data, bool isDeleted)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "SendDataToEBS_AllOrSingleSite");
            bool result = default(bool);

            try
            {
                if ((siteCode.IsEmpty() || siteCode == "0"))
                {
                    // get all the active sites
                    var sites = _di.GetSites(string.Empty);
                    if (sites != null)
                    {
                        bool result2 = true;
                        try
                        {
                            Parallel.For(0, sites.Count,
                                (i, l) =>
                                {
                                    DLSiteDto site = sites[i];
                                    result2 &= this.SendDataToEBS(site.SiteId, ehID, type, data, isDeleted);
                                    if (!result2)
                                    {
                                        l.Break();
                                    }
                                });
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(PROC, ex);
                        }
                        finally
                        {
                            result = result2;
                        }
                    }
                }
                else
                {
                    result = this.SendDataToEBS(siteCode, ehID, type, data, isDeleted);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private bool SendDataToEBS(string siteCode, int ehID, string type, XElement data, bool isDeleted)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "SendDataToEBS");
            bool result = default(bool);
            long messageId = _di.GetSettingValue<long>(LASTMSGID_SEND, 0);

            try
            {
                // create the message and configuration 
                s2sMessage msgs2s = this.CreateS2SMessage(ref messageId);
                this.AddConfigurationInfoUpdate(msgs2s);

                // set the site code as property id
                msgs2s.p_body.p_infoUpdate.propertyId = siteCode;
                if (isDeleted)
                {
                    msgs2s.p_body.p_infoUpdate.p_infoUpdateData.action = "delete";
                }

                // parse the xml and fill into configuration info
                bool dataFilled = this.InvokeWorkInternal_ToEBS(msgs2s, msgs2s.p_body.p_infoUpdate.p_infoUpdateData, type, data);

                if (dataFilled)
                {
                    // update the timestamp now and sent
                    msgs2s.p_body.p_infoUpdate.dateTime = DateTime.UtcNow;
                    msgs2s.p_header.dateTimeSent = DateTime.UtcNow;
                    string request = this.ConvertObjectToXml(msgs2s, false, false);
                    LogManager.WriteLog("Request: " + request, LogManager.enumLogLevel.Debug);

                    // send the message to EBS
                    using (IEBSCommClient client = EBSCommClientFactory.CreateClient(IS_EBS_13_0, _sentToEBSBinding, _ebsEndpointAddress))
                    {
                        string response = client.S2SMessagePostOperation(request);
                        LogManager.WriteLog("Response: " + response, LogManager.enumLogLevel.Debug);

                        if (!response.IsEmpty())
                        {
                            // log the incoming message                
                            _di.UpdateMessageHistory(_configStore.LogRawMessages,
                                                     (int)EBSSystemNames.BMC, (int)EBSSystemNames.EBS,
                                                     siteCode, DateTime.Now, ehID, request, response);

                            // update the last message id
                            Log.InfoV(PROC, "[BMC->EBS] Successfully sent the message for id : {0:D}", ehID);
                            _di.UpdateSettingValue(LASTMSGID_SEND, messageId.ToString());
                            result = true;
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
    }
}
