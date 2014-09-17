using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.PlayerGateway.Gateway;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExMonitor.Server.Handlers.EPI
{
    internal abstract class MonitorHandler_EPI_GameCapping_CapUncap :
        MonitorHandler_EPI_Base
    {
        protected override bool ProcessH2GMessageInternal(MonMsg_H2G request)
        {
            MonTgt_G2H_GameCapping_StartEnd tgt_G2H_GameCapping_Start = request.Targets[0] as MonTgt_G2H_GameCapping_StartEnd;
            IGameCapping gameCapping = GameCapping.GetInstance();
            gameCapping.Process_GameCapping(request.SiteCode, request.InstallationNo, tgt_G2H_GameCapping_Start, this.IsGameUnCapping);
            return true;
        }

        protected abstract bool IsGameUnCapping { get; }
    }

    //Need to get event type
    [MonitorHandlerMapping((int)FaultSource.Game_Capping, (int)FaultType_Game_Capping.Lock)]
    internal class MonitorHandler_EPI_32_1 :
        MonitorHandler_EPI_GameCapping_CapUncap
    {
        protected override bool IsGameUnCapping
        {
            get { return false; }
        }
    }

    //Need to get event type
    [MonitorHandlerMapping((int)FaultSource.Game_Capping, (int)FaultType_Game_Capping.Release)]
    internal class MonitorHandler_EPI_32_2 :
        MonitorHandler_EPI_GameCapping_CapUncap
    {
        protected override bool IsGameUnCapping
        {
            get { return true; }
        }
    }

    //Need to get event type
    [MonitorHandlerMapping((int)FaultSource.Game_Capping, (int)FaultType_Game_Capping.TimerExpiry)]
    internal class MonitorHandler_EPI_32_3 :
        MonitorHandler_EPI_Base
    {
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            MonTgt_G2H_GameCapping_StartEnd tgt_G2H_GameCapping_Start = request.Targets[0] as MonTgt_G2H_GameCapping_StartEnd;
            ExCommsDataContext.Current.AlertGameCappingSessionExpires(request.InstallationNo);
            return true;
        }
    }

    interface IGameCapping
    {
        void Process_GameCapping(string siteCode, int installationNo, MonTgt_G2H_GameCapping_StartEnd tgt_G2H_GameCapping_Start, bool isGameUnCapping);
        GameCapInformation GetGameCappingInformation(int installationNo, string cardNo, bool isGameUnCapping);
        MonTgt_H2G_GameCapping SetGameCappingMessage(GameCapInformation gameCapInformation, bool validPin);
        bool SendCapInformationToGMU(int installationNo, MonTgt_H2G_GameCapping monTgt_H2G_GameCapping);
        void StartEndGameCappingSession(bool IsGameUnCapping, int installationNo, string stock, string stand, string reservedCardNo, string reservedForCardNo);
    }

    public class GameCapping : IGameCapping
    {
        static readonly object _LockInstanceBlock = new object();
        static GameCapping _gameCapping = null;

        private GameCapping()
        {
        }

        public static GameCapping GetInstance()
        {
            if (_gameCapping == null)
            {
                lock (_LockInstanceBlock)
                {
                    if (_gameCapping == null)
                    {
                        _gameCapping = new GameCapping();
                    }
                }
            }

            return _gameCapping;
        }

        public void Process_GameCapping(string siteCode, int installationNo, MonTgt_G2H_GameCapping_StartEnd tgt_G2H_GameCapping_Start, bool isGameUnCapping)
        {
            string playerCardNo = tgt_G2H_GameCapping_Start.PlayerCardNumber;
            string empCardNo = tgt_G2H_GameCapping_Start.EmployeeCardNumber;
            bool isEmpCardCapping = false;

            if (Convert.ToInt64(empCardNo) == 0)
            {
                empCardNo = playerCardNo;
                isEmpCardCapping = false;
            }
            else
            {
                isEmpCardCapping = true;
            }

            Encoding _encoding = HandlerHelper.Current.Encode;
            string pinNumber = Crypto.Crypto.AsciiToHex(tgt_G2H_GameCapping_Start.PlayerPIN, _encoding);
            string encryptedPin = HandlerHelper.Current.GetEncryptedPIN(pinNumber);
            playerCardNo = (Convert.ToInt64(playerCardNo) != 0) ? playerCardNo : empCardNo;

            this.ValidateAndStartGameCapping(siteCode, isEmpCardCapping, installationNo, empCardNo, playerCardNo, pinNumber, isGameUnCapping);
        }

        private void ValidateAndStartGameCapping(string siteCode, bool isEmpCardCapping, int installationNo, string reservedCardNo, string reservedForCardNo, string pinNumber, bool IsGameUnCapping)
        {
            GameCapInformation gameCapInformation = GetGameCappingInformation(installationNo, reservedCardNo, IsGameUnCapping);

            if (gameCapInformation != null)
            {
                if (!isEmpCardCapping)
                {
                    if (ExMonitorServerConfigStoreFactory.Store.GameCapPINValidationRequired)//App setting for pin validation is required
                    {
                        if (((!IsGameUnCapping) || 
                            (IsGameUnCapping && !HandlerHelper.Current.GameCappingReleaseOnPlayerCardIn)) &&
                            (pinNumber != "46464646")) //Setting from Db
                            this.AuthenticatePinNumber(siteCode, installationNo, gameCapInformation.Asset, reservedCardNo, pinNumber, IsGameUnCapping);
                        return;
                    }
                }

                 MonTgt_H2G_GameCapping monTgt_H2G_GameCapping = this.SetGameCappingMessage(gameCapInformation, true);

                 if (this.SendCapInformationToGMU(installationNo, monTgt_H2G_GameCapping))
                 {
                     this.StartEndGameCappingSession(IsGameUnCapping, installationNo, gameCapInformation.Asset, gameCapInformation.Position, reservedCardNo, reservedForCardNo);
                 }
            }
        }

        public GameCapInformation GetGameCappingInformation(int installationNo, string cardNo, bool isGameUnCapping)
        {
            return ExCommsDataContext.Current.GetGameCappingInformation(installationNo, cardNo, isGameUnCapping);
        }

        private void AuthenticatePinNumber(string siteCode, int installationNo, string asset, string playerCardNumber, string encryptedPin, bool IsGameUnCapping)
        {
            InstallationDetailsForMSMQ installationDetails = ExCommsDataContext.Current.GetInstallationDetailsByDatapak(installationNo);
            HandlerHelper.Current.SaveSDTRequest(playerCardNumber, installationNo);

            asset = HandlerHelper.Current.GetAssetByStockPrefix(asset);
            DateTime transDate = DateTime.Now;

            GameCapAuthenticationRequest pinAuthenticationRequest = new GameCapAuthenticationRequest
            {
                TransactionID = HandlerHelper.Current.NextPTRequestID().ToString(),
                TransactionDate = transDate.ToString("yyyyMMdd").PadLeft(8, '0'),
                TransactionTime = transDate.ToString("HHmmss").PadLeft(6, '0'),

                InstallationNo = installationNo,
                BarPosition = installationDetails.Bar_Pos_Name,
                SlotNumber = installationDetails.Bar_Pos_Name.PadLeft(8, '0'),
                SlotIndex = installationDetails.Bar_Pos_Name.PadLeft(6, '0'),
                Stand = asset,

                CardNo = playerCardNumber,
                EncryptedPin = encryptedPin.PadLeft(16, '0'),
                SiteCode = siteCode,
                //Authentication = 
            };

            if (IsGameUnCapping)
                HandlerHelper.PlayerGatewayInstance.GameCapPINAuthentication(pinAuthenticationRequest, this.UnCapAuthenticatePinNumberResponse);
            else
                HandlerHelper.PlayerGatewayInstance.GameCapPINAuthentication(pinAuthenticationRequest, this.CapAuthenticatePinNumberResponse);
        }

        public MonTgt_H2G_GameCapping SetGameCappingMessage(GameCapInformation gameCapInformation, bool validPin)
        {
            MonTgt_H2G_GameCapping monTgt_H2G_GameCapping = new MonTgt_H2G_GameCapping();

            try
            {
                string[] TimeOption = gameCapInformation.TimeOption.Split(',');

                monTgt_H2G_GameCapping.DisplayMessageLength = Convert.ToByte(gameCapInformation.Message.Length);
                monTgt_H2G_GameCapping.DisplayMessage = gameCapInformation.Message;
                monTgt_H2G_GameCapping.IsEnabled = (gameCapInformation.ReserveGameAsset == 1);
                monTgt_H2G_GameCapping.IsMaxCappingExceeded = (gameCapInformation.MaxCapNotExceeded == 1);
                monTgt_H2G_GameCapping.IsSelfCapping = (gameCapInformation.SelfReserve == 1);
                monTgt_H2G_GameCapping.IsAllowed = (gameCapInformation.AllowReserve == 1);

                try { monTgt_H2G_GameCapping.Option1 = Convert.ToByte(Convert.ToInt32(TimeOption[0])); }
                catch { }
                try { monTgt_H2G_GameCapping.Option2 = Convert.ToByte(Convert.ToInt32(TimeOption[1])); }
                catch { }
                try { monTgt_H2G_GameCapping.Option3 = Convert.ToByte(Convert.ToInt32(TimeOption[2])); }
                catch { }
                try { monTgt_H2G_GameCapping.Option4 = Convert.ToByte(Convert.ToInt32(TimeOption[3])); }
                catch { }
                try { monTgt_H2G_GameCapping.Option5 = Convert.ToByte(Convert.ToInt32(TimeOption[4])); }
                catch { }

                monTgt_H2G_GameCapping.AutoRelease = (gameCapInformation.AutoRelease == 1);
                monTgt_H2G_GameCapping.ValidPin = validPin;
                monTgt_H2G_GameCapping.MinutesToExpire = Convert.ToByte(gameCapInformation.ExpireMinstoAlert);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return monTgt_H2G_GameCapping;
        }

        public bool SendCapInformationToGMU(int installationNo, MonTgt_H2G_GameCapping monTgt_H2G_GameCapping)
        {
            try
            {
                EPIMsgProcessor.Current.SendCommand(installationNo, monTgt_H2G_GameCapping);
                return true;
            }
            catch
            { return false; }
        }

        public void StartEndGameCappingSession(bool IsGameUnCapping, int installationNo, string stock, string stand, string reservedCardNo, string reservedForCardNo)
        {
            if (IsGameUnCapping)
                ExCommsDataContext.Current.EndGameCapSession(installationNo, reservedCardNo);
            else
                ExCommsDataContext.Current.StartGameCapSession(installationNo, stock, stand, reservedCardNo, reservedForCardNo);
        }

        private void CapAuthenticatePinNumberResponse(GameCapPINAuthenticationCompleteResponse response)
        {
            SDTMessages.Instance.ProcessAFTInformation(response.ReturnValue, MonitorConstants.GAMECAPPING_AUTHENTICATION, response.RequestID, response.ResultStatus == ResponseStatus.Success ? true : false);
        }

        private void UnCapAuthenticatePinNumberResponse(GameCapPINAuthenticationCompleteResponse response)
        {
            SDTMessages.Instance.ProcessAFTInformation(response.ReturnValue, MonitorConstants.GAMEUNCAPPING_AUTHENTICATION, response.RequestID, response.ResultStatus == ResponseStatus.Success ? true : false);
        }
    }
}