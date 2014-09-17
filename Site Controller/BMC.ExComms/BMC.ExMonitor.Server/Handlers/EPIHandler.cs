using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.Crypto;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExMonitor.Server.Handlers
{
    [MonitorHandlerMapping((int)FaultSource.ECash, typeof(FaultType_ECash))]
    [MonitorHandlerMapping((int)FaultSource.PriorityEvent, (int)FaultType_PriorityEvent.PlayerCardIn)]
    [MonitorHandlerMapping((int)FaultSource.PriorityEvent, (int)FaultType_PriorityEvent.PlayercardOut)]
    public class EPIHandler : MonitorHandlerBase
    {
        private delegate bool EPI_G2H_Delegate(MonMsg_G2H request);
        private IDictionary<int, EPI_G2H_Delegate> _epiMsg_G2H = null;
        private HandlerHelper _handlerInstance = null;

        public EPIHandler()
        {
            _handlerInstance = HandlerHelper.HandlerHelperInstance;
            FillGIMFaultTypeDictionary();
        }

        private void FillGIMFaultTypeDictionary()
        {
            try
            {
                _epiMsg_G2H = new Dictionary<int, EPI_G2H_Delegate>()
                {
                    {Convert.ToInt32(FaultType_ECash.AFTAccountList), new EPI_G2H_Delegate(this.ProcessBalanceRequest) },
                };
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        private bool ProcessBalanceRequest(MonMsg_G2H request)
        {
            try
            {
                ModuleProc PROC = new ModuleProc("EPIHandler", "ProcessBalanceRequest");

                MonTgt_G2H_EFT_BalanceRequest balnceRequest = request.Targets[0] as MonTgt_G2H_EFT_BalanceRequest;
                if (balnceRequest == null) return false;
                
                string _pin = Crypto.Crypto.AsciiToHex(balnceRequest.Pin, _handlerInstance.IsASCIIEncoding() ? Encoding.ASCII : ASCIIEncoding.Default);
                string _encryptedPin = Crypto.Crypto.EncryptHexString(_handlerInstance.EncryptionKey, _pin, _handlerInstance.IsASCIIEncoding() ? Encoding.ASCII : ASCIIEncoding.Default);
                Log.Info("Encrypted Pin " + _encryptedPin);

                InstallationDetailsForMSMQ installationDetails = EFT_DataAccess.GetInstance().GetInstallationDetailsByDatapak(request.InstallationNo);
                

                return false;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }
    
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            try
            {
 	        if (_epiMsg_G2H.ContainsKey(request.FaultType))
                    return _epiMsg_G2H[request.FaultType](request);

                return false;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }
    }
}
