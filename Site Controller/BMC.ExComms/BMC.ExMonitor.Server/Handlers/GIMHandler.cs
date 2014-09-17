using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;

namespace BMC.ExMonitor.Server.Handlers
{
    [MonitorHandlerMapping((int)FaultSource.GIM_Event, (int)FaultType_GIM.Game_Id_Info_G2H)]
    public class GIMHandler : MonitorHandlerBase
    {
        #region Data Members

        private delegate bool GIM_G2H_Delegate(MonMsg_G2H request);
        private IDictionary<int, GIM_G2H_Delegate> _gimMsg_G2H = null;

        #endregion //Data Members

        #region Constructor

        public GIMHandler()
        {
            FillGIMFaultTypeDictionary();
        }

        #endregion //Constructor

        #region Override Methods

        /// <summary>
        /// Process GIM G2H Message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            try
            {
                if (_gimMsg_G2H.ContainsKey(request.FaultType))
                    return _gimMsg_G2H[request.FaultType](request);

                return false;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        #endregion //Override Methods

        #region Private Methods

        /// <summary>
        /// To fill the dictionary with fault type and appropriate invoke methods
        /// </summary>
        private void FillGIMFaultTypeDictionary()
        {
            try
            {
                _gimMsg_G2H = new Dictionary<int, GIM_G2H_Delegate>()
                {
                    {Convert.ToInt32(FaultType_GIM.Game_Id_Info_G2H), new GIM_G2H_Delegate(this.ProcessGameIdInfoMessage) },
                };
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        /// <summary>
        /// TO process GameIdInfo message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool ProcessGameIdInfoMessage(MonMsg_G2H request)
        {
            try
            {
                MonTgt_G2H_GIM_GameIDInfo monTgtMsg = request.Targets[0] as MonTgt_G2H_GIM_GameIDInfo;
                if (monTgtMsg == null) return false;

                int? installationNo = 0;
                string assetNo = string.Empty;
                char? gamePrefix = '\0';

                if (GIM_DataAccess.GetInstance().InsertGMULogin(monTgtMsg, request.IpAddress, ref installationNo, ref assetNo, ref gamePrefix))
                {
                    MonMsg_H2G monH2G_Msg = new MonMsg_H2G();
                    MonTgt_H2G_GIM_GameIDInfo monH2G_GameIdInfo_Msg = new MonTgt_H2G_GIM_GameIDInfo();

                    monH2G_Msg.FaultSource = Convert.ToInt32(FaultSource.GIM_Event);
                    monH2G_Msg.FaultType = Convert.ToInt32(FaultType_GIM.Game_Id_Info_H2G);
                    monH2G_Msg.IpAddress = request.IpAddress;
                    monH2G_Msg.InstallationNo = installationNo.GetValueOrDefault();

                    monH2G_GameIdInfo_Msg.SourceAddress = new System.Net.IPAddress(Convert.ToByte(request.IpAddress));
                    monH2G_GameIdInfo_Msg.AssetNumber = assetNo;
                    monH2G_GameIdInfo_Msg.PokerGamePrefix = gamePrefix.ToString();

                    monH2G_Msg.AddTarget(monH2G_GameIdInfo_Msg);
                    this.ProcessH2GMessage(monH2G_Msg);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        #endregion //Private Methods
    }
}
