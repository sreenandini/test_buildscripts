using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.ExComms.Server.Handlers.Tickets;

namespace BMC.ExComms.Server
{
    public sealed class ModuleHelper
        : DisposableObject
    {
        private readonly object _transactionIDLock = new object();
        private int _transactionID = 0;
        private ExCommsServerImpl _serverInstance = null;

        private TicketGlobalCollection _ticketGlobalsCollection = null;
        private rsp_GetSiteDetailsResult _siteDetail = null;
        private string _ticketLocationCode = string.Empty;

        #region Single Thread Helper (Current)

        private readonly static SingletonHelper<ModuleHelper> _moduleHelper = new SingletonHelper<ModuleHelper>(
            new Lazy<ModuleHelper>(() => new ModuleHelper()));

        public static ModuleHelper Current
        {
            get { return _moduleHelper.Current; }
        }

        public int TransactionID
        {
            get { return _transactionID; }
        }

        public int TransactionIDNew
        {
            get
            {
                lock (_transactionIDLock)
                {
                    if ((_transactionID + 1) > 255)
                        _transactionID = 0;
                    else
                        _transactionID++;
                    return _transactionID;
                }
            }
        }

        #endregion

        private ModuleHelper()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Initialize"))
            {
                try
                {
                    _serverInstance = ExCommsServerImpl.Current;
                    _ticketGlobalsCollection = new TicketGlobalCollection();
                    _siteDetail = ExCommsDataContext.Current.GetSiteDetails();
                    _ticketLocationCode = ExCommsDataContext.Current.GetSettingValue("TICKET_LOCATION_CODE", _siteDetail.Code);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    Environment.FailFast("Unable to intialize Module Helper");
                }
            }
        }

        public FFMsg_G2H CreateG2HMessage(string ipAddress, FF_AppId_SessionIds sessionId,
            FF_AppId_G2H_Commands command, FF_AppId_G2H_MessageTypes messageType,
            int transactionId)
        {
            return FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                new FFCreateEntityRequest_G2H()
                {
                    IPAddress = ipAddress,
                    Command = command,
                    MessageType = messageType,
                    SessionID = sessionId,
                    TransactionID = transactionId,
                });
        }

        public FFMsg_H2G CreateH2GMessage(string ipAddress, FF_AppId_SessionIds sessionId,
            FF_AppId_H2G_PollCodes pollCode)
        {
            return FreeformEntityFactory.CreateEntity<FFMsg_H2G>(FF_FlowDirection.H2G,
                new FFCreateEntityRequest_H2G()
                {
                    IPAddress = ipAddress,
                    PollCode = pollCode,
                    SessionID = sessionId,
                    TransactionID = this.TransactionIDNew,
                });
        }

        public bool PostMessageToGMU(IFreeformEntity_Msg message)
        {
            return _serverInstance.PostMessageToTransceiver(message);
        }

        internal TicketGlobalCollection TicketGlobals
        {
            get { return _ticketGlobalsCollection; }
        }

        internal TicketGlobal GetTicketGlobal(string key)
        {
            if (TicketGlobals.ContainsKey(key)) return TicketGlobals[key];
            return null;
        }

        public rsp_GetSiteDetailsResult SiteDetail
        {
            get { return _siteDetail; }
        }

        public string TicketLocationCode
        {
            get { return _ticketLocationCode; }
        }
    }
}
