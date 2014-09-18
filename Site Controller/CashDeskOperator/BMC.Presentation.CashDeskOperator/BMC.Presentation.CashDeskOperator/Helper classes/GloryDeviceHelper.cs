using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using iCAS.BMC.GDB.GDBService;
using System.ServiceModel.Description;
using BMC.Common.LogManagement;
using BMC.Transport;
using System.ServiceModel.Configuration;
using System.Configuration;

namespace BMC.Presentation.CashDeskOperator
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public sealed class GloryDeviceHelper : IBallyCallback
    {
        #region Variables
        public static String _ServerName;
        public static int _ServerPort;
        public static Boolean _SSL;
        public static String _CertificateThumbprint;

        private static BallyClient _Client;
        private static GloryDeviceHelper _instance;
        private CDeviceInfo _DeviceInfo = null;
        private DateTime _LastComm = DateTime.Now;
        private Boolean _IsOccupied = false;
        private int _HeartbeatValue = 60;
        private Boolean _FlagHeartbeatStop = true;
        private const string DispenserType = "Glory Cash Dispenser ==> ";
        #endregion

        #region Properties
        public static GloryDeviceHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GloryDeviceHelper();
                }
                return _instance;
            }
        }
        #endregion

        #region Open Session

        public BallyCOpenSessionResponse OpenSession()
        {
            BallyCOpenSessionResponse res = new BallyCOpenSessionResponse();
            try
            {

                LogManager.WriteLog(DispenserType + "OpenSession Start", LogManager.enumLogLevel.Info);

                if (!doCreateClient())
                {
                    res.Result = _SYS_CODE.SYS_ERROR;
                    return res;
                }
                BallyCOpenSessionRequest req = new BallyCOpenSessionRequest();
                req.Id = UserInformation.ID;
                req.User = UserInformation.User;
                req.UserPwd = UserInformation.Password;
                req.DeviceName = UserInformation.Device;
                req.SeqNo = UserInformation.SequenceNumber;
                req.SessionID = string.Empty;//get from open session response
                try
                {
                    res = _Client.OpenSession(req);
                    doProcOpenSessionResponse(res);
                    LogManager.WriteLog(DispenserType + "OpenSession End", LogManager.enumLogLevel.Info);
                }
                catch (Exception ex)
                {
                    res.Result = _SYS_CODE.SYS_ERROR_COMMUNICATION;
                    LogManager.WriteLog(DispenserType + "OpenSession Result " + res.Result, LogManager.enumLogLevel.Info);
                }


            }
            catch (Exception x)
            {
                LogManager.WriteLog(DispenserType + "OpenSession :" + x.ToString(), LogManager.enumLogLevel.Error);
            }

            return res;
        }

        private bool doCreateClient()
        {
            bool retVal = true;
            try
            {
                if (_SSL)
                {
                    NetTcpBinding binding = new NetTcpBinding(SecurityMode.TransportWithMessageCredential);
                    binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
                    binding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;

                    binding.MaxReceivedMessageSize = Int32.MaxValue;
                    binding.MaxBufferSize = Int32.MaxValue;
                    binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                    binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;

                    InstanceContext ic = new InstanceContext(GloryDeviceHelper.Instance);
                    Uri uri = new Uri(string.Format("net.tcp://{0}:{1}/GDB", _ServerName, _ServerPort));
                    EndpointAddress epa = new EndpointAddress(uri);

                    _Client = new BallyClient(new InstanceContext(Instance), binding, epa);
                    //_Client.ClientCredentials.ClientCertificate.SetCertificate
                    //            (StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, _CertificateThumbprint);
                    _Client.ClientCredentials.ClientCertificate.SetCertificate
                                (StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByIssuerName, "GloryCA");
                    _Client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;
                }
                else
                {
                    //get binding name from app config
                    string BindingName = "";
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    ServiceModelSectionGroup serviceModelSectionGroup = ServiceModelSectionGroup.GetSectionGroup(configuration);
                    if (serviceModelSectionGroup.Bindings.NetTcpBinding.ConfiguredBindings.Count <= 0)
                    {
                        retVal = false;
                        LogManager.WriteLog("AppConfig doesn't have a binding (i.e. binding name is missing)", LogManager.enumLogLevel.Error);
                        return retVal;
                    }
                    BindingName = serviceModelSectionGroup.Bindings.NetTcpBinding.ConfiguredBindings[0].Name;
                    NetTcpBinding binding = new NetTcpBinding(BindingName);


                    InstanceContext ic = new InstanceContext(GloryDeviceHelper.Instance);
                    Uri uri = new Uri(string.Format("net.tcp://{0}:{1}/GDB", _ServerName, _ServerPort));
                    EndpointAddress epa = new EndpointAddress(uri);

                    _Client = new BallyClient(new InstanceContext(Instance), binding, epa);
                }

                foreach (OperationDescription op in _Client.ChannelFactory.Endpoint.Contract.Operations)
                {
                    DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;

                    if (dataContractBehavior != null)
                    {

                        dataContractBehavior.MaxItemsInObjectGraph = Int32.MaxValue;

                    }

                }

                //_Client.ChannelFactory.Endpoint.Binding.SendTimeout = new TimeSpan(0, 2, 0);
            }
            catch (Exception x)
            {
                retVal = false;
                LogManager.WriteLog(DispenserType + "doCreateClient :" + x.ToString(), LogManager.enumLogLevel.Error);
            }
            return retVal;
        }

        private void doProcOpenSessionResponse(BallyCOpenSessionResponse res)
        {
            if (res.Result == _SYS_CODE.SYS_SUCCESS)
            {
                UserInformation.Occupied = false;
                //EventInformation.Enabled = true;

                UserInformation.SessionID = res.SessionID;

                _HeartbeatValue = res.Heartbeat;
                HeartbeatStart();

                if (res.Group.Equals("IT", StringComparison.OrdinalIgnoreCase))
                    UserInformation.Group = UserGroup.GROUP_IT;
                else if (res.Group.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    UserInformation.Group = UserGroup.GROUP_ADMIN;
                else if (res.Group.Equals("Super", StringComparison.OrdinalIgnoreCase))
                    UserInformation.Group = UserGroup.GROUP_POWERUSER;
                else
                    UserInformation.Group = UserGroup.GROUP_USER;

                _DeviceInfo = res.DeviceInfo;
            }
        }

        #endregion

        #region Heartbeat

        private void HeartbeatStart()
        {
            LogManager.WriteLog(DispenserType + "Heartbeat Started Successfully", LogManager.enumLogLevel.Debug);
            Thread thread = new Thread(new ThreadStart(ThreadHeartbeat));
            _FlagHeartbeatStop = false;
            thread.Start();
        }

        public void HeartbeatStop()
        {
            _FlagHeartbeatStop = true;
            LogManager.WriteLog(DispenserType + "Heartbeat Stopped", LogManager.enumLogLevel.Debug);
        }

        private void ThreadHeartbeat()
        {
            while (!_FlagHeartbeatStop)
            {
                try
                {
                    if (DateTime.Now.Subtract(_LastComm).TotalSeconds >= _HeartbeatValue)
                    {
                        _LastComm = DateTime.Now;
                        Heartbeat();
                    }
                }
                catch (Exception x)
                {
                    LogManager.WriteLog(DispenserType + "ThreadHeartbeat :" + x.ToString(), LogManager.enumLogLevel.Error);
                }

                Thread.Sleep(10);
            }
        }

        public BallyCHeartbeatResponse Heartbeat()
        {
            BallyCHeartbeatResponse res = new BallyCHeartbeatResponse();
            try
            {

                LogManager.WriteLog(DispenserType + "Heartbeat Start", LogManager.enumLogLevel.Info);
                BallyCHeartbeatRequest req = new BallyCHeartbeatRequest();
                req.Id = UserInformation.ID;
                req.SeqNo = UserInformation.SequenceNumber;
                req.SessionID = UserInformation.SessionID;

                res = _Client.Heartbeat(req);
                doProcHeartbeatResponse(res);
                LogManager.WriteLog(DispenserType + "Heartbeat End", LogManager.enumLogLevel.Info);

            }
            catch (Exception x)
            {
                LogManager.WriteLog("Heartbeat :" + x.ToString(), LogManager.enumLogLevel.Error);
            }

            return res;
        }

        private void doProcHeartbeatResponse(BallyCHeartbeatResponse res)
        {
            if (res.Result == _SYS_CODE.SYS_SUCCESS)
            {
            }
        }

        #endregion

        #region Occupy

        public BallyCOccupyResponse Occupy()
        {
            BallyCOccupyResponse res = new BallyCOccupyResponse();
            try
            {

                LogManager.WriteLog(DispenserType + "Occupy Device " + UserInformation.Device + " Start", LogManager.enumLogLevel.Info);
                BallyCOccupyRequest req = new BallyCOccupyRequest();

                req.Id = UserInformation.ID;
                req.SeqNo = UserInformation.SequenceNumber;
                req.SessionID = UserInformation.SessionID;
                req.DeviceName = UserInformation.Device;
                res = _Client.Occupy(req);
                doProcOccupyResponse(res);
                LogManager.WriteLog(DispenserType + "Occupy Device " + UserInformation.Device + " End", LogManager.enumLogLevel.Info);

            }
            catch (Exception x)
            {
                res.Result = _SYS_CODE.SYS_ERROR_COMMUNICATION;
                HeartbeatStop();
                LogManager.WriteLog(DispenserType + "Occupy :" + x.ToString(), LogManager.enumLogLevel.Error);
            }

            return res;
        }

        private void doProcOccupyResponse(BallyCOccupyResponse res)
        {
            _IsOccupied = (res.Result == _SYS_CODE.SYS_SUCCESS);
        }

        #endregion

        #region Cashout

        public _SYS_CODE Cashout(List<CGDBSCCashoutDetail> ListCashoutDetail,_TRANS_TYPE _type)
        {
            _SYS_CODE res = _SYS_CODE.SYS_ERROR;
            try
            {

                LogManager.WriteLog(DispenserType + "Cashout Start", LogManager.enumLogLevel.Info);
                if (_DeviceInfo != null && Instance._IsOccupied)
                {
                    if (!_DeviceInfo.HasCashInventory)
                    {
                        LogManager.WriteLog(DispenserType + "  ..Device does not have cash inventory", LogManager.enumLogLevel.Error);

                        return _SYS_CODE.SYS_ERROR_CASH_NOT_SUPPORTED;
                    }
                   
                    BallyCCashoutRequest req = new BallyCCashoutRequest();
                    req.Id = UserInformation.ID;
                    req.UserID = UserInformation.User;
                    req.SeqNo = UserInformation.SequenceNumber;
                    req.SessionID = UserInformation.SessionID;
                    req.CashoutDetail = ListCashoutDetail.ToArray();
                    req.TransType = _type;  
                    res = _Client.Cashout(req);
                }
                LogManager.WriteLog(DispenserType + "Cashout End", LogManager.enumLogLevel.Info);

            }
            catch (Exception x)
            {
                HeartbeatStop();
                LogManager.WriteLog(DispenserType + "Cashout :" + x.ToString(), LogManager.enumLogLevel.Error);
            }

            return res;
        }

        #region Event Handlers

        public event EventHandler _evtStatusChanged;
        public event EventHandler _evtProcessComplete;

        public void OnStatusChanged(BallyCEAStatusChanged e)
        {
            try
            {
                if (_evtStatusChanged != null)
                    _evtStatusChanged(this, e);
            }
            catch (Exception x)
            {
                LogManager.WriteLog(DispenserType + "OnStatusChanged :" + x.ToString(), LogManager.enumLogLevel.Error);
            }
        }

        public void OnProcessComplete(BallyCEAProcessComplete e)
        {
            try
            {
                _LastComm = DateTime.Now;
                _OP_TYPE opType = e.OpType;
                switch (opType)
                {
                    case _OP_TYPE.Cashout: doProcCashoutResponse(e); break;
                    default: break;
                }

                if (_evtProcessComplete != null)
                    _evtProcessComplete(this, e);
            }
            catch (Exception x)
            {
                LogManager.WriteLog(DispenserType + "OnProcessComplete :" + x.ToString(), LogManager.enumLogLevel.Error);
            }
        }

        private void doProcCashoutResponse(BallyCEAProcessComplete e)
        {

        }

        #endregion

        #endregion

        #region Release

        public BallyCReleaseResponse Release()
        {
            BallyCReleaseResponse res = new BallyCReleaseResponse();
            try
            {

                LogManager.WriteLog(DispenserType + "Release Device " + UserInformation.Device + " Start", LogManager.enumLogLevel.Info);
                BallyCReleaseRequest req = new BallyCReleaseRequest();

                req.Id = UserInformation.ID;
                req.SeqNo = UserInformation.SequenceNumber;
                req.SessionID = UserInformation.SessionID;

                res = _Client.Release(req);
                doProcReleaseResponse(res);
                LogManager.WriteLog(DispenserType + "Release Device " + UserInformation.Device + " End", LogManager.enumLogLevel.Info);

            }
            catch (Exception x)
            {
                res.Result = _SYS_CODE.SYS_ERROR;
                LogManager.WriteLog(DispenserType + "Release :" + x.ToString(), LogManager.enumLogLevel.Error);
            }

            return res;
        }

        private void doProcReleaseResponse(BallyCReleaseResponse res)
        {
            _IsOccupied = (res.Result != _SYS_CODE.SYS_SUCCESS);
        }

        #endregion

        #region Close Session

        public BallyCCloseSessionResponse CloseSession()
        {
            BallyCCloseSessionResponse res = new BallyCCloseSessionResponse();
            try
            {

                LogManager.WriteLog(DispenserType + "CloseSession Start", LogManager.enumLogLevel.Info);
                HeartbeatStop();

                BallyCCloseSessionRequest req = new BallyCCloseSessionRequest();
                req.Id = UserInformation.ID;
                req.SeqNo = UserInformation.SequenceNumber;
                req.SessionID = UserInformation.SessionID;

                res = _Client.CloseSession(req);
                doProcCloseSessionResponse(res);
                LogManager.WriteLog(DispenserType + "CloseSession End", LogManager.enumLogLevel.Info);

            }
            catch (Exception x)
            {
                res.Result = _SYS_CODE.SYS_ERROR;
                HeartbeatStop();
                LogManager.WriteLog(DispenserType + "CloseSession :" + x.ToString(), LogManager.enumLogLevel.Error);
            }

            return res;
        }

        private void doProcCloseSessionResponse(BallyCCloseSessionResponse res)
        {
            if (res.Result == _SYS_CODE.SYS_SUCCESS)
            {
                UserInformation.Occupied = false;
                //EventInformation.Enabled = false;

                UserInformation.SessionID = String.Empty;
                UserInformation.Group = String.Empty;

                _DeviceInfo = null;

                _Client = null;
            }
        }

        #endregion

    }
}
