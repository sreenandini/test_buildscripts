using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Server.Executor;
using BMC.ExComms.Server.Transceiver;

namespace BMC.ExComms.Server
{
    internal partial class ExCommsServerImpl
    {
        private IFFTransceiver _ffTranceiver = null;

        /// <summary>
        /// Initializes the freeform transceiver.
        /// </summary>
        /// <returns>True if succeeded; otherwise false.</returns>
        private bool Initialize_FreeformTransceiver()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Initialize_FreeformTransceiver"))
            {
                bool result = false;

                try
                {
                    _ffTranceiver = FFTransceiverFactory.Create(new FFTransceiverArgs
                    {
                        LocalIpAddress = string.Empty,
                        InterfaceIpAddress = _storeComm.InterfaceIp,
                        MulticastIpAddress = _storeComm.MulticastIp,
                        ReceivePortNo = _storeComm.ReceivePortNo,
                        SendPortNo = _storeComm.TransmitPortNo,

                    }, this.Executor);
                    _ffTranceiver.Receive += OnTransceiver_Receive;
                    result = true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        /// <summary>
        /// Uninitializes the freeform transceiver.
        /// </summary>
        /// <returns>True if succeeded; otherwise false.</returns>
        private bool Uninitialize_FreeformTransceiver()
        {
            bool result = true;
            _ffTranceiver.Dispose();
            return result;
        }

        void OnTransceiver_Receive(UdpFreeformEntity item)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ff_trans_Receive"))
            {
                Log.Info(PROC, "Processing G2H Message ...");
                ExCommsExecutorFactory.ProcessMessage(item.EntityData as FFMsg_G2H);
            }
        }

        public bool PostMessageToTransceiver(IFreeformEntity_Msg request)
        {
            return _ffTranceiver.Send(request);
        }
    }
}
