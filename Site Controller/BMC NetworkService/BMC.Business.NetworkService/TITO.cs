using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComExchangeLib;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Runtime.InteropServices;
using System.Data;
using BMC.DBInterface.NetworkService;
using System.Timers;
using BMC.Common.ConfigurationManagement;
using System.Diagnostics;
using BMC.Transport.NetworkService;
using System.Threading;
#if NEW_EXCOMMS
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Proxies;
#endif
//using System.Threading;

namespace BMC.Business.NetworkService
{
    public class TITO : ObjectStateObserver, IDisposable
    {
        #region Declarations
        private ExchangeClient _exchangeClientSiteCode;
        private IExchangeAdmin _iExchangeAdminSiteCode;

        private ExchangeClient _exchangeClientTITOEnDis;
        private IExchangeAdmin _iExchangeAdminTITOEnDis;

        private ExchangeClient _exchangeClientTITOEXP;
        private IExchangeAdmin _iExchangeAdminTITOEXP;

        private Sector203Data m_SectorData=new Sector203Data();

        public IDictionary<int, TITOThreadDataRequest> dTITORequest = null;
        private ThreadDispatcher<TITOThreadDataResponse> _thAckResponse = null;

        public IDictionary<int, int> dTicketExpireRequest = null;
        private ThreadDispatcher<TITOThreadDataResponse> _thTicketExpireAckResponse = null;

        public IDictionary<int, int> dSiteCodeRequest = null;
        private ThreadDispatcher<TITOThreadDataResponse> _thSiteCodeAckResponse = null;

        private bool _disposed;
        private object _lockRes = new object();
        private object _lockTicketExpire = new object();
        private object _lockSiteCode = new object();
        private System.Timers.Timer _tmrRequest = null;
        System.Threading.ManualResetEvent mEvent = new System.Threading.ManualResetEvent(false); 
        #endregion

        #region Constructor

        public TITO()
        {
            _exchangeClientSiteCode = new ExchangeClient();
            _exchangeClientSiteCode.OPT_PARAM_ACK += ExchangeClientSiteCodeAck;
            _exchangeClientSiteCode.InitialiseExchange(0);

            _exchangeClientTITOEnDis = new ExchangeClient();
            _exchangeClientTITOEnDis.TITO_ENDIS_ACK += ExchangeClientTITOAck;
            _exchangeClientTITOEnDis.InitialiseExchange(0);

            _exchangeClientTITOEXP = new ExchangeClient();
            _exchangeClientTITOEXP.TITO_PARAM_ACK += ExchangeClientVoucherAck;
            _exchangeClientTITOEXP.InitialiseExchange(0);

            if (dTITORequest == null)
                dTITORequest = new SortedDictionary<int, TITOThreadDataRequest>();

            if (dTicketExpireRequest == null)
                dTicketExpireRequest = new SortedDictionary<int, int>();

            if (dSiteCodeRequest == null)
                dSiteCodeRequest = new SortedDictionary<int, int>();

            _tmrRequest = new System.Timers.Timer(Int32.Parse(ConfigManager.Read("TITOConfigInterval")) * 1000);
            _tmrRequest.Elapsed += new ElapsedEventHandler(ProcessRequest);

            _thAckResponse = new ThreadDispatcher<TITOThreadDataResponse>(1, "_thTITOAckResponse");
            _thAckResponse.AddProcessThreadData(new ProcessThreadDataHandler<TITOThreadDataResponse>(this.TITOProcessResponse));
            _thAckResponse.Initialize();

            _thTicketExpireAckResponse = new ThreadDispatcher<TITOThreadDataResponse>(1, "_thTicketExpireAckResponse");
            _thTicketExpireAckResponse.AddProcessThreadData(new ProcessThreadDataHandler<TITOThreadDataResponse>(this.TicketExpireProcessResponse));
            _thTicketExpireAckResponse.Initialize();

            _thSiteCodeAckResponse = new ThreadDispatcher<TITOThreadDataResponse>(1, "_thSiteCodeAckResponse");
            _thSiteCodeAckResponse.AddProcessThreadData(new ProcessThreadDataHandler<TITOThreadDataResponse>(this.SiteCodeProcessResponse));
            _thSiteCodeAckResponse.Initialize();

            _iExchangeAdminSiteCode = (IExchangeAdmin)_exchangeClientSiteCode;
            _iExchangeAdminTITOEnDis = (IExchangeAdmin)_exchangeClientTITOEnDis;
            _iExchangeAdminTITOEXP = (IExchangeAdmin)_exchangeClientTITOEXP;

            ObjectStateNotifier.AddObserver(this);

            _tmrRequest.Start();
        }

        private void ProcessRequest(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = sender as System.Timers.Timer;

            try
            {
                timer.Stop();
                lock (_lockSiteCode)
                {
                    dSiteCodeRequest.Clear();
                }


                lock (_lockRes)
                {
                    dTITORequest.Clear();

                }

                lock (_lockTicketExpire)
                {
                    dTicketExpireRequest.Clear();
                }

                // Database hit and store this value in this list.
                this.SiteCodeUpdate();
                this.TITOConfig();
                this.TicketExpireUpdate();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (!this.IsObjectInactive)
                {
                    timer.Start();
                }
            }
        } 

        #endregion

        #region SiteCode
        private void SiteCodeUpdate()
        {
            if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
            {
                return;
            }


            try
            {
                DataTable dtSiteCodeInstallations;
                dtSiteCodeInstallations = DBBuilder.GetInstallationsForGMUSiteCodeUpdate();

                LogManager.WriteLog("SiteCodeUpdate | Number of Installation to Process: " + dtSiteCodeInstallations.Rows.Count.ToString()
                                 , LogManager.enumLogLevel.Info);

                foreach (DataRow row in dtSiteCodeInstallations.Rows)
                {
                    if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
                    {
                        break;
                    }

                    int nInstallationNo = Convert.ToInt32(row["Installation_No"]);

                    int nMessageID;
                    nMessageID = SetSiteCode(nInstallationNo);

                    if (!dSiteCodeRequest.ContainsKey(nMessageID))
                        dSiteCodeRequest.Add(nMessageID, nInstallationNo);

                    LogManager.WriteLog("SiteCodeUpdate | Request for Installation: " + nInstallationNo.ToString()
                                            + " , MessageID:" + nMessageID.ToString(), LogManager.enumLogLevel.Info);

                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("SiteCodeUpdate | Exception Occured." + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
        }

        private int SetSiteCode(int nInstallationNo)
        {
            try
            {
                /*Send OPtParam*/
                m_SectorData.Command = 0x81;
                byte[] bData = { };
                m_SectorData.PutCommandDataVB(bData);

                #if !NEW_EXCOMMS
                _exchangeClientSiteCode.RequestExWriteSector(nInstallationNo, 203, m_SectorData);
                return _iExchangeAdminSiteCode.LastMessageID;
                #else
                   MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = nInstallationNo,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_SetSiteCodeNW { });
                return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
                #endif

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SetSiteCode | Exception Occured." + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        private void ExchangeClientSiteCodeAck(MessageAck messageACK)
        {
            try
            {
                _thSiteCodeAckResponse.AddThreadData(new TITOThreadDataResponse()
                {
                    MessageID = messageACK.MessageID,
                    Ack = messageACK.ACK,
                });
                LogManager.WriteLog("ExchangeClientSiteCodeAck | MessageID: " + messageACK.MessageID.ToString() + ", ACK Status: "
                                       + messageACK.ACK.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {

                LogManager.WriteLog("ExchangeClientSiteCodeAck | Exception Occured." + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public void SiteCodeProcessResponse(TITOThreadDataResponse threadData)
        {
            lock (_lockSiteCode)
            {
                if (dSiteCodeRequest.Count <= 0)
                    return;
            }

            try
            {
                if (mEvent.WaitOne(NetworkServiceSettings.DBHitWaitTime))
                {
                    return;
                }

                lock (_lockSiteCode)
                {
                    if (dSiteCodeRequest.ContainsKey(threadData.MessageID))
                    {
                        int nInstallationNo = dSiteCodeRequest[threadData.MessageID];

                        //Update DB and remove from both lists
                        if (threadData.Ack == true)
                        {
                            Stopwatch stopwatch = new Stopwatch();
                            //stopwatch.Start();
                            DBBuilder.UpdateGMUSiteCodeStatus(nInstallationNo, 0);
                            //stopwatch.Stop();
                            //LogManager.WriteLog("SiteCodeProcessResponse | Time Taken For DB Update: " + stopwatch.Elapsed.TotalMilliseconds.ToString()
                                               //, LogManager.enumLogLevel.Info);
                            LogManager.WriteLog("SiteCodeProcessResponse | ACK Received For Installation: " + nInstallationNo.ToString()
                                                , LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            LogManager.WriteLog("SiteCodeProcessResponse | NACK Received For Installation: " + nInstallationNo.ToString()
                                                , LogManager.enumLogLevel.Info);
                        }
                        dSiteCodeRequest.Remove(threadData.MessageID);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SiteCodeProcessResponse | Exception Occured." + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region TITOConfig
        /// <summary>
        /// Entry Method for Enable/Disable Machines
        /// </summary>
        private void TITOConfig()
        {
            if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
            {
                return;
            }


            try
            {
                DataTable getTITOConig;
                getTITOConig = DBBuilder.GetTITOConfig();
                int TickerExpire = Convert.ToInt32(DBBuilder.GetSettingFromDB("TICKET_EXPIRE", "10"));

                LogManager.WriteLog("TITOConfig | Number of Installation to Process: " + getTITOConig.Rows.Count.ToString()
                                 , LogManager.enumLogLevel.Info);

                if (getTITOConig.Rows.Count > 0)
                {

                    foreach (DataRow row in getTITOConig.Rows)
                    {
                        if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
                        {
                            break;
                        }
                        TITOThreadDataRequest threadData = new TITOThreadDataRequest()
                        {
                            InstallationNo = Convert.ToInt32(row["Installation_No"]),
                            TITOEnabled = Convert.ToInt32(row["TITOEnabled"]),
                            SiteTITOEnabled = Convert.ToInt32(row["SiteTITOEnabled"]),
                            SiteNonCashEnabled = Convert.ToInt32(row["SiteNonCashEnabled"]),
                            MachineTITOEnabled = Convert.ToInt32(row["MachineTITOEnabled"]),
                            MachineNonCashEnabled = Convert.ToInt32(row["MachineNonCashEnabled"]),
                            TicketExpireDays = TickerExpire,
                        };


                        if (threadData.TITOEnabled == 1)
                        {
                            threadData.Command = eCommand.Enable;

                            int nMessageID;
                            nMessageID = EnableTITO(threadData.InstallationNo);

                            if (!dTITORequest.ContainsKey(nMessageID))
                                dTITORequest.Add(nMessageID, threadData);

                            LogManager.WriteLog("TITOConfig | Request for Installation: " + threadData.InstallationNo.ToString()
                                                + " , Command:" + threadData.Command.ToString()
                                                + " , MessageID:" + nMessageID.ToString(), LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            threadData.Command = eCommand.Disable;
                            int nMessageID;
                            nMessageID = DisableTITO(threadData.InstallationNo);

                            if (!dTITORequest.ContainsKey(nMessageID))
                                dTITORequest.Add(nMessageID, threadData);

                            LogManager.WriteLog("TITOConfig | Request for Installation: " + threadData.InstallationNo.ToString()
                                                + " , Command:" + threadData.Command.ToString()
                                                + " , MessageID:" + nMessageID.ToString(), LogManager.enumLogLevel.Info);

                        }



                    }
                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("TITOConfig Error " + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }

        }

        private int EnableTITO(int installation_no)
        {
            try
            {
                /*Send Tito Enable/Disable*/
                m_SectorData.Command = 0x82;
                byte[] bData = { 1 }; /*0-disable , 1 - enable*/
                m_SectorData.PutCommandDataVB(bData);
               
                #if !NEW_EXCOMMS
                        _exchangeClientTITOEnDis.RequestExWriteSector(installation_no, 203, m_SectorData);
                        return _iExchangeAdminTITOEnDis.LastMessageID;
                #else
                                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = installation_no,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_EnableDisableTITONW { EnableDisable = true });
                return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
                #endif
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(string.Format("Failed TITO enable", ex.Message), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return -1;
            }

        }
        //
        private int DisableTITO(int installation_no)
        {
            try
            {
                /*Send Tito Enable/Disable*/
                m_SectorData.Command = 0x82;
                byte[] bData = { 0 }; /*0-disable , 1 - enable*/
                m_SectorData.PutCommandDataVB(bData);
                
                
                #if !NEW_EXCOMMS
                _exchangeClientTITOEnDis.RequestExWriteSector(installation_no, 203, m_SectorData);
                return _iExchangeAdminTITOEnDis.LastMessageID;

                #else
                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = installation_no,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_EnableDisableTITONW { EnableDisable = false });
                return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
                #endif

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(string.Format("Failed TITO Disable", ex.Message), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return -1;
            }

        }

        private void ExchangeClientTITOAck(MessageAck messageACK)
        {
            try
            {
                _thAckResponse.AddThreadData(new TITOThreadDataResponse()
                    {
                        MessageID = messageACK.MessageID,
                        Ack = messageACK.ACK,
                    });
                LogManager.WriteLog("ExchangeClientTITOAck | MessageID: " + messageACK.MessageID.ToString() + ", ACK Status: " + messageACK.ACK.ToString()
                                         , LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExchangeClientTITOAck | Exception Occured." + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public void TITOProcessResponse(BMC.Business.NetworkService.TITOThreadDataResponse threadData)
        {
            lock (_lockRes)
            {
                if (dTITORequest.Count <= 0)
                    return;
            }

            try
            {
                if (mEvent.WaitOne(NetworkServiceSettings.DBHitWaitTime))
                {
                    return;
                }
                lock (_lockRes)
                {
                    if (dTITORequest.ContainsKey(threadData.MessageID))
                    {
                        TITOThreadDataRequest Requestitem = dTITORequest[threadData.MessageID];

                        //Update DB and remove from both lists
                        if (threadData.Ack == true)
                        {
                            int EnableCommand = Requestitem.Command == eCommand.Enable ? 1 : 0;
                            DBBuilder.UpdatedTITOConfig(Requestitem.InstallationNo, Requestitem.SiteTITOEnabled,
                                        Requestitem.SiteNonCashEnabled, Requestitem.MachineTITOEnabled, Requestitem.MachineNonCashEnabled, EnableCommand);

                            LogManager.WriteLog("TITOProcessResponse | ACK Received For Installation: " + Requestitem.InstallationNo.ToString() + ", Command: " + Requestitem.Command.ToString(), LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            LogManager.WriteLog("TITOProcessResponse | NACK Received For Installation: " + Requestitem.InstallationNo.ToString() + ", Command: " + Requestitem.Command.ToString(), LogManager.enumLogLevel.Info);
                        }
                        dTITORequest.Remove(threadData.MessageID);
                    }
                }
            }
            catch (Exception ex)
            {
                
                LogManager.WriteLog("TITOProcessResponse | Exception Occured." + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }
        
        #endregion

        #region TicketExpire
        private void TicketExpireUpdate()
        {
            if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
            {
                return;
            }


            try
            {
                DataTable dtTicketExpireInstallations;
                dtTicketExpireInstallations = DBBuilder.GetInstallationsForTicketExpireUpdate();
                int TickerExpire = Convert.ToInt32(DBBuilder.GetSettingFromDB("TICKET_EXPIRE", "10"));

                LogManager.WriteLog("TicketExpireUpdate | Number of Installation to Process: " + dtTicketExpireInstallations.Rows.Count.ToString()
                                 , LogManager.enumLogLevel.Info);

                foreach (DataRow row in dtTicketExpireInstallations.Rows)
                {
                    if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
                    {
                        break;
                    }

                    int nInstallationNo = Convert.ToInt32(row["Installation_No"]);

                    int nMessageID;
                    nMessageID = SetTicketExpire(nInstallationNo, TickerExpire);

                    if (!dTicketExpireRequest.ContainsKey(nMessageID))
                        dTicketExpireRequest.Add(nMessageID, nInstallationNo);

                    LogManager.WriteLog("TicketExpireUpdate | Request for Installation: " + nInstallationNo.ToString()
                                            + " , MessageID:" + nMessageID.ToString(), LogManager.enumLogLevel.Info);

                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("TicketExpireUpdate | Exception Occured." + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
        }

        public int SetTicketExpire(int installation_no, int TicketExpireDays)
        {
            try
            {
                m_SectorData.Command = 0x83;
                byte[] bData = { };
                m_SectorData.PutCommandDataVB(bData);
                
                #if !NEW_EXCOMMS
                _exchangeClientTITOEXP.RequestExWriteSector(installation_no, 203, m_SectorData);
                return _iExchangeAdminTITOEXP.LastMessageID;
                #else
                MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
                {
                    InstallationNo = installation_no,
                };
                monMsg_H2G.Targets.Add(new MonTgt_H2G_SetTicketExpireNW { NoOfDays = TicketExpireDays });
                return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
                #endif

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SetTicketExpire | Exception Occured." + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return -1;
            }

        }

        private void ExchangeClientVoucherAck(MessageAck messageACK)
        {
            try
            {
                _thTicketExpireAckResponse.AddThreadData(new TITOThreadDataResponse()
                    {
                        MessageID = messageACK.MessageID,
                        Ack = messageACK.ACK,
                    });
                LogManager.WriteLog("ExchangeClientVoucherAck | MessageID: " + messageACK.MessageID.ToString() + ", ACK Status: "
                                       + messageACK.ACK.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                
               LogManager.WriteLog("ExchangeClientVoucherAck | Exception Occured." + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public void TicketExpireProcessResponse(TITOThreadDataResponse threadData)
        {
            lock (_lockTicketExpire)
            {
                if (dTicketExpireRequest.Count <= 0)
                    return;
            }

            try
            {
                if (mEvent.WaitOne(NetworkServiceSettings.DBHitWaitTime))
                {
                    return;
                }
                lock (_lockTicketExpire)
                {
                    if (dTicketExpireRequest.ContainsKey(threadData.MessageID))
                    {
                        int nInstallationNo = dTicketExpireRequest[threadData.MessageID];

                        //Update DB and remove from both lists
                        if (threadData.Ack == true)
                        {
                            DBBuilder.UpdateTicketExpireInstallations(nInstallationNo);

                            LogManager.WriteLog("TicketExpireProcessResponse | ACK Received For Installation: " + nInstallationNo.ToString()
                                                , LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            LogManager.WriteLog("TicketExpireProcessResponse | NACK Received For Installation: " + nInstallationNo.ToString()
                                                , LogManager.enumLogLevel.Info);
                        }
                        dTicketExpireRequest.Remove(threadData.MessageID);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TicketExpireProcessResponse | Exception Occured." + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        
        #endregion

        #region Overriden Methods
        public override void NotifyState(ObjectState state)
        {
            base.NotifyState(state);
            if (state == ObjectState.Deactivated) mEvent.Set();
        }
        
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_disposed) return;
            ObjectStateNotifier.RemoveObserver(this);

            try
            {
                Releaseobject(_exchangeClientSiteCode, "_exchangeClientSiteCode");
                Releaseobject(_exchangeClientTITOEnDis, "_exchangeClientTITOEnDis");
                Releaseobject(_exchangeClientTITOEXP, "_exchangeClientTITOEXP");

                Releaseobject(_iExchangeAdminSiteCode, "_iExchangeAdminSiteCode");
                Releaseobject(_iExchangeAdminTITOEnDis, "_iExchangeAdminTITOEnDis");
                Releaseobject(_iExchangeAdminTITOEXP, "_iExchangeAdminTITOEXP");

                LogManager.WriteLog("Dispose | All Objects were released successfully.", LogManager.enumLogLevel.Info);
            }
            catch
            { }

            _exchangeClientSiteCode = null;
            _exchangeClientTITOEnDis = null;
            _exchangeClientTITOEXP = null;

            _iExchangeAdminSiteCode = null;
            _iExchangeAdminTITOEnDis = null;
            _iExchangeAdminTITOEXP = null;

            GC.SuppressFinalize(this);
        }


        void Releaseobject(object objRelease,string objName)
        {
            var i = Marshal.ReleaseComObject(objRelease);
            Thread.Sleep(10);
            while (i > 0)
            {
                LogManager.WriteLog("Number of objects in "+objName+": " + i, LogManager.enumLogLevel.Info);
                i = Marshal.ReleaseComObject(objRelease);
            }
            LogManager.WriteLog("Releaseobject |  " + objName + " was released successfully.", LogManager.enumLogLevel.Info);

            //i = Marshal.ReleaseComObject(_iExchangeAdmin);
            //while (i > 0)
            //{
            //    LogManager.WriteLog("Number of objects in _iExchangeAdmin = " + i, LogManager.enumLogLevel.Info);
            //    i = Marshal.ReleaseComObject(_iExchangeAdmin);
            //}
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="AutoEnableDisable"/> is reclaimed by garbage collection.
        /// </summary>
        ~TITO()
        {
            Dispose();
            _disposed = true;
        }

        #endregion
    }

    #region Message Classes

    public class TITOThreadDataResponse : ThreadData
    {
        //public int InstallationNo { get; set; }
        public int MessageID { get; set; }
        public bool Ack { get; set; }

        #region IThreadData Members

        public override string UniqueKey
        {
            get { return MessageID.ToString(); }
        }

        #endregion
    }

    public class TITOThreadDataRequest : ThreadData
    {
        public int InstallationNo { get; set; }
        public eCommand Command;
        public int TITOEnabled { get; set; }
        public int SiteTITOEnabled { get; set; }
        public int SiteNonCashEnabled { get; set; }
        public int MachineTITOEnabled { get; set; }
        public int MachineNonCashEnabled { get; set; }
        public int TicketExpireDays { get; set; }
        public bool Ack { get; set; }

        #region IThreadData Members

        public override string UniqueKey
        {
            get { return InstallationNo.ToString(); }
        }

        #endregion
    }

    public enum eCommand
    {
        Enable,
        Disable
    } 
    #endregion
}
