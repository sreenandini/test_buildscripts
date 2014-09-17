using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.CoreLib;
using BMC.ExComms.DataLayer.MSSQL;
using Audit.BusinessClasses;
using Audit.Transport;

namespace BMC.ExMonitor.Server.Handlers.EPI
{
    [MonitorHandlerMapping((int)FaultSource.PriorityEvent, (int)FaultType_PriorityEvent.PlayerCardIn)]
    internal class MonitorHandler_EPI_22_5 :
        MonitorHandler_EPI_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            MonMsg_G2H request = context.G2HMessage;
            Log.Info("Player Card In Started for : " + request.CardNumber);

            // remove the existing card in
            CurrentEPIManager.RemoveTimeoutsIfExists(request.InstallationNo);

            // delete the epi message
            CurrentEPIMsgProcessor.DeleteEPIMessage(request.InstallationNo);

            // process the card in
            bool result = SDTMessages.Instance.ProcessPlayerCardIn(request, target as MonTgt_G2H_Status_PlayerCardIn);

            // open session for carded play
            if (_configStore.GamePlayInfoRequiredForSession)
            {
                Log.InfoV("Open GamePlay Session For CardedPlay {0:D}", request.InstallationNo);
                CurrentDataContext.OpenUserSessionForCardedGamePlay(request.InstallationNo);
            }
            return result;
        }
    }

    [MonitorHandlerMapping((int)FaultSource.PriorityEvent, (int)FaultType_PriorityEvent.PlayercardOut)]
    internal class MonitorHandler_EPI_22_6 :
        MonitorHandler_EPI_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            MonMsg_G2H request = context.G2HMessage;
            Log.Info("Player Card Out Started for : " + request.CardNumber);

            // delete the epi message
            CurrentEPIMsgProcessor.DeleteEPIMessage(request.InstallationNo);

            // check if the card in exists in the local dictionary
            if (!this.CheckCardInSession(request, (r) =>
            {
                CurrentEPIManager.PlayerCardOut(request.InstallationNo, request.CardNumber);
            })) return false;

            if (_configStore.GamePlayInfoRequiredForSession)
            {
                // close game session for carded play
                Log.InfoV("Close GamePlay Session For CardedPlay {0:D}", request.InstallationNo);
                CurrentDataContext.CloseUserSessionForCardedGamePlay(request.InstallationNo);
            }

            // process the card in
            return SDTMessages.Instance.ProcessPlayerCardOut(request, target as MonTgt_G2H_Status_PlayerCardOut);
        }
    }

    [MonitorHandlerMapping((int)FaultSource.PriorityEvent, (int)FaultType_PriorityEvent.AbandonedCard)]
    internal class MonitorHandler_EPI_22_12 :
        MonitorHandler_EPI_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            Log.Info("Started Abandoned Card");
            ForceMeterRead(context, target);

            MonMsg_G2H request = context.G2HMessage;
            CurrentEPIMsgProcessor.DeleteEPIMessage(request.InstallationNo);

            if (HandlerHelper.Current.IsGamePlayInfoRequiredForSession)
            {
                Log.Info("Close GamePlay Session For CardedPlay" + request.InstallationNo.ToString());
                ExCommsDataContext.Current.CloseUserSessionForCardedGamePlay(request.InstallationNo);
            }

            if (!CurrentEPIManager.EPIProcessExists(request.InstallationNo))
            {
                Log.Info("Abandoned card...");
                return false;
            }

            return SDTMessages.Instance.ProcessPlayerCardOut(request, target as MonTgt_G2H_Status_PlayerCardOut);
        }
    }

    [MonitorHandlerMapping((int)FaultSource.PriorityEvent, (int)FaultType_PriorityEvent.EmployeeCardIn)]
    internal class MonitorHandler_EPI_22_37 :
        MonitorHandler_EPI_Base
    {
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            bool retVal = true;
            const string msg_type = "Card In";
            InstallationDetailsForMSMQ installationDetails = null;

            // add the card detail
            var cardDetail = EPIManager.Current.AddOrUpdateCardInDetail(request.InstallationNo, request.CardNumber);

            using (ILogMethod method = Log.LogMethod("MonitorHandler_EPI_22_37", "ProcessG2HMessageInternal"))
            {
                try
                {
                    method.Info("Started Employee Card In for " + request.CardNumber);
                    EPIManager.Current.CreateProcess(request.InstallationNo);

                    if (HandlerHelper.Current.IsEmployeeCardTrackingEnabled)
                    {
                        method.Info("Insert Employee card Session Details");
                        ExCommsDataContext.Current.UpdateEmployeeCardSessions(request.CardNumber, request.FaultDate, request.InstallationNo, msg_type);
                        installationDetails = request.Extra as InstallationDetailsForMSMQ;
                        ExCommsDataContext.Current.SendEmployeeSTMALert(installationDetails.Bar_Pos_No.ToString(), request.FaultDate, msg_type, request.CardNumber);

                        try
                        {
                            Audit.Transport.Audit_History m_AuditInfo = new Audit.Transport.Audit_History();
                            m_AuditInfo.AuditModuleName = ModuleName.MSMQ;
                            m_AuditInfo.Audit_Date = DateTime.Now.Date;
                            m_AuditInfo.Audit_User_ID = 0;
                            m_AuditInfo.Audit_User_Name = "System";
                            m_AuditInfo.Audit_Screen_Name = "Employee card";
                            m_AuditInfo.Audit_Desc = "Employee Card In";
                            m_AuditInfo.AuditOperationType = OperationType.ADD;
                            m_AuditInfo.Audit_Field = "EmployeecardNumber";
                            m_AuditInfo.Audit_New_Vl = request.CardNumber;
                            m_AuditInfo.Audit_Slot = request.Asset;
                            Log.Info(method.PROC, "Insert Auditing info for Employee card In Event");
                            AuditViewerBusiness.InsertAuditData(m_AuditInfo);
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(ex);
                        }

                        string empflags = ExCommsDataContext.Current.GetEmployeeFlags(request.CardNumber);
                        if (!String.IsNullOrEmpty(empflags))
                        {
                            method.Info("Flags " + empflags);
                            empflags = empflags.Substring(2);
                            List<byte> enumver = Enumerable.Range(0, empflags.Length)
                                                .Where((x) => x % 2 == 0)
                                                .Select((x) => Convert.ToByte(empflags.Substring(x, 2), 16))
                                                .ToList();
                            List<byte> cardno = Enumerable.Range(0, request.CardNumber.PadLeft(10, '0').Length)
                                                .Where((x) => x % 2 == 0)
                                                .Select((x) => Convert.ToByte(request.CardNumber.PadLeft(10, '0').Substring(x, 2), 16))
                                                .ToList();
                            enumver.Insert(0, Convert.ToByte(empflags[0]));
                            enumver.Insert(1, Convert.ToByte(empflags[1]));
                            enumver.InsertRange(0, cardno);

                            // TODO: EPIMsgProcessor.SendCommand

                        }
                    }

                    ExCommsDataContext.Current.UpdateFloorStatus(request.InstallationNo, DateTime.Now, null, null, null, null,
                                                               null, null, null, null, request.CardNumber, request.FaultDate.ToString(), null);
                    ExCommsDataContext.Current.UpdateGameCappingDetails(request.CardNumber, 1, false);
                    try
                    {
                        if (SDTMessages.Instance.ProcessEmployeeCardIn(request))
                        {
                            EPIManager.Current.CreateInactivityTimeout(request.InstallationNo);
                            EPIManager.Current.CreateIntervalRatingTimer(request.InstallationNo);
                        }
                    }
                    finally { }
                }
                catch (Exception ex)
                {
                    retVal = false;
                    // TODO: EPIMsgProcessor.SendCommand
                    if (installationDetails != null)
                        CurrentEPIMsgProcessor.DisplayBallyWelcomeMsg(request.InstallationNo, installationDetails.Bar_Pos_Name, DateTime.Now);
                    if (cardDetail != null)
                        cardDetail.Clear();
                    Log.Exception(ex);
                }
                finally
                {
                    method.Info("Employee Card In completed for : " + request.CardNumber);
                }
            }
            return retVal;
        }
    }

    [MonitorHandlerMapping((int)FaultSource.PriorityEvent, (int)FaultType_PriorityEvent.EmployeecardOut)]
    internal class MonitorHandler_EPI_22_38 :
        MonitorHandler_EPI_Base
    {
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            bool retVal = true;
            const string msg_type = "Card Out";
            InstallationDetailsForMSMQ installationDetails = null;

            // add the card detail
            var cardDetail = EPIManager.Current.AddOrUpdateCardInDetail(request.InstallationNo, request.CardNumber);

            using (ILogMethod method = Log.LogMethod("MonitorHandler_EPI_22_38", "ProcessG2HMessageInternal"))
            {
                try
                {
                    method.Info("Started Employee Card Out for " + request.CardNumber);

                    if (HandlerHelper.Current.IsEmployeeCardTrackingEnabled)
                    {
                        ExCommsDataContext.Current.UpdateFloorStatus(request.InstallationNo, DateTime.Now, null, null, null, null,
                                                               null, null, null, null, "", request.FaultDate.ToString(), null);

                        EPIManager.Current.CreateProcess(request.InstallationNo);
                        method.Info("Insert Employee card Session Details");
                        ExCommsDataContext.Current.UpdateEmployeeCardSessions(request.CardNumber, request.FaultDate, request.InstallationNo, msg_type);
                        installationDetails = request.Extra as InstallationDetailsForMSMQ;
                        ExCommsDataContext.Current.SendEmployeeSTMALert(installationDetails.Bar_Pos_No.ToString(), request.FaultDate, msg_type, request.CardNumber);

                        try
                        {
                            Audit.Transport.Audit_History m_AuditInfo = new Audit.Transport.Audit_History();
                            m_AuditInfo.AuditModuleName = ModuleName.MSMQ;
                            m_AuditInfo.Audit_Date = DateTime.Now.Date;
                            m_AuditInfo.Audit_User_ID = 0;
                            m_AuditInfo.Audit_User_Name = "System";
                            m_AuditInfo.Audit_Screen_Name = "Employee card";
                            m_AuditInfo.Audit_Desc = "Employee Card Out";
                            m_AuditInfo.AuditOperationType = OperationType.ADD;
                            m_AuditInfo.Audit_Field = "EmployeecardNumber";
                            m_AuditInfo.Audit_New_Vl = request.CardNumber;
                            m_AuditInfo.Audit_Slot = request.Asset;
                            Log.Info(method.PROC, "Insert Auditing info for Employee card out Event");
                            AuditViewerBusiness.InsertAuditData(m_AuditInfo);
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(ex);
                        }

                        try
                        {
                            if (SDTMessages.Instance.ProcessEmployeeCardOut(request))
                            {
                                EPIManager.Current.CreateInactivityTimeout(request.InstallationNo);
                                EPIManager.Current.CreateIntervalRatingTimer(request.InstallationNo);
                            }
                        }
                        finally { }
                    }
                }
                catch (Exception ex)
                {
                    retVal = false;
                    // TODO: EPIMsgProcessor.SendCommand
                    if (installationDetails != null)
                        CurrentEPIMsgProcessor.DisplayBallyWelcomeMsg(request.InstallationNo, installationDetails.Bar_Pos_Name, DateTime.Now);
                    if (cardDetail != null)
                        cardDetail.Clear();
                    Log.Exception(ex);
                }
                finally
                {
                    method.Info("Employee Card Out completed for : " + request.CardNumber);
                }
            }
            return retVal;
        }
    }
}
