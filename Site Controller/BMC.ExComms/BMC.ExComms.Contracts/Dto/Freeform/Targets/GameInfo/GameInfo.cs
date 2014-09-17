using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Game Information

    /// <summary>
    /// GMU to Host Freeform for Game Information
    /// </summary>
    public class FFTgt_G2H_GameInfo
        : FFTgt_G2H, IFFTgt_G2H
    {
        #region Private Data Member

        private string _paytableID;
        private int _currentGamePayback;
        private int _currentGameDenomination;
        private string _currentGameID;
        private string _gameProtocolVersion;
        private string _currentGameName;

        #endregion //Private Data Member

        #region Properties

        // Pay Table ID
        public string PaytableID
        {
            get
            {
                return _paytableID;
            }
            set
            {
                if (this._paytableID == value) return;
                _paytableID = value;
            }
        }

        // Current Game Payback
        public int CurrentGamePayback
        {
            get
            {
                return this._currentGamePayback;
            }
            set
            {
                this._currentGamePayback = value;
            }
        }

        // Current Game Denomination
        public int CurrentGameDenomination
        {
            get
            {
                return this._currentGameDenomination;
            }
            set
            {
                this._currentGameDenomination = value;
            }
        }

        // Current Game ID
        public string CurrentGameID
        {
            get
            {
                return this._currentGameID;
            }
            set
            {
                this._currentGameID = value;
            }
        }

        // Game Protocol Version
        public string GameProtocolVersion
        {
            get
            {
                return _gameProtocolVersion;
            }
            set
            {
                if (this._gameProtocolVersion == value) return;
                _gameProtocolVersion = value;
            }
        }

        // Current Game Name
        public string CurrentGameName
        {
            get
            {
                return _currentGameName;
            }
            set
            {
                if (this._currentGameName == value) return;
                _currentGameName = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.GameInfo;
            }
        }
    }

    #endregion //Game Info

    #region Connect

    /// <summary>
    /// GMU to Host Freeform for Connect
    /// </summary>
    public class FFTgt_G2H_Connect
        : FFTgt_G2H, IFFTgt_G2H { }

    #endregion //Connect


    public class FFTgt_G2H_ExtendedGameInfo : FFTgt_G2H, IFFTgt_G2H
    {
        public int GameNumber { get; set; }
        public int MaxBet { get; set; }
        public string GameName { get; set; }
        public string PayTableName { get; set; }
        public int ProgressLevel { get; set; }
        public int ProgressGroup { get; set; }
        public bool HasGameNameFramed { get; set; }

    }

}
