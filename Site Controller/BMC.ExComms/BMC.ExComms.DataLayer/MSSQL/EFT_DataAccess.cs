using System.Data.Linq.Mapping;
using BMC.CoreLib;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data;
using BMC.CoreLib.Data;
using System.Data.Common;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
    {
        public InstallationDetailsForMSMQ GetInstallationDetailsByDatapak(int? datapakNo)
        {
            try
            {
                return GetInstallationDetailsByDatapak(datapakNo, false);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return new InstallationDetailsForMSMQ();
            }
        }

        public InstallationDetailsForMSMQ GetInstallationDetailsByDatapak(int? datapakNo, bool? eCash)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.rsp_GetInstallationDetailsForMSMQ(datapakNo, eCash).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return new InstallationDetailsForMSMQ();
            }
        }

        public List<InstallationDetailsForMSMQ> GetInstallationDetailsByDatapak()
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    List<InstallationDetailsForMSMQ> retInstallationDetailsForMSMQ = null;
                    retInstallationDetailsForMSMQ = DataContext.rsp_GetInstallationDetailsForMSMQ(0, false).ToList();
                    return retInstallationDetailsForMSMQ;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return null;
            }
        }

        public bool UpdateDisplayMessageForEFT(int installationNo, string cardNo, Guid requestID, string displayMessage)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.usp_UpdatePlayerGatewayMessages(installationNo, cardNo, requestID, displayMessage);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        public bool InsertAFTTransactions(int instalaltionNo, int playerID, int collectionNo, double cashableAmt, double noncashableAmt, double wATAmt, DateTime transactionDate, string transactionType, int transferID, string accountType, bool transactionStatus)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.esp_InsertAFTTransactions(instalaltionNo, playerID, collectionNo, cashableAmt, noncashableAmt, wATAmt, transactionDate, transactionType, transferID, accountType, transactionStatus);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        public bool AuditAFTTransactions(int installationNo, DateTime transactionDate, string transactionType, double cashableAmt, double nonCashableAmt, double watAmt, int playerID, bool ecashEnabled, int errorCode, string errorMsg, int transferID)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.esp_AuditAFTTransactions(installationNo, transactionDate, transactionType, cashableAmt, nonCashableAmt, watAmt, playerID, ecashEnabled, errorCode, errorMsg, transferID);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        public List<CMPErrorCodes> GetCMPErrorCodes()
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.rsp_GetCMPErrorCode().ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return new List<CMPErrorCodes>();
            }
        }

        public int InsertPlayerSessionRating(int installationNo, int cardNo, string process, string type)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.esp_InsertPlayerSessionRating(cardNo, installationNo, process, type);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public int InsertPCSessionRating(int installationNo, int cardNo, string process, string type)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.esp_InsertPCSessionRating(cardNo, installationNo, process, type);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public int RemovePlayerSessionRating(int installationNo, int cardNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.esp_RemovePlayerSessionRating(cardNo, installationNo);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public int RemovePCSessionRating(int installationNo, int cardNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.esp_RemovePCSessionRating(cardNo, installationNo);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public int OpenOrCloseGamePlaySessionForInstallation(int installationNo, int operationType)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.usp_OpenOrCloseGamePlaySessionForInstallation(installationNo, operationType);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public int OpenUserSessionForCardedGamePlay(int installationNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.usp_OpenUserSessionForCardedGamePlay(installationNo);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public int OpenUserSessionForGamePlay(int installationNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.usp_OpenUserSessionForGamePlay(installationNo);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public int CloseUserSessionForCardedGamePlay(int installationNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.usp_CloseGamePlaySessionForCardedPlay(installationNo);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public int CloseGamePlaySessionOnZeroCreditEvent(int installationNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.usp_CloseGamePlaySessionOnZeroCreditEvent(installationNo);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public bool IsCardSessionExists(int installationNo, int cardNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    var result = DataContext.IsCardSessionExists(installationNo, cardNo).FirstOrDefault();
                    return (result != null);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        public int UpdateFloorStatus(int installation_No,
                                    DateTime systemLastUpdate,
                                    string ePIDetails = "",
                                    System.Nullable<System.DateTime> meterLastUpdate = null,
                                    System.Nullable<int> doorStatus = null,
                                    System.Nullable<int> powerStatus = null,
                                    System.Nullable<int> ePIStatus = null,
                                    System.Nullable<int> datapak_Variant = null,
                                    System.Nullable<int> datapak_LastEventNo = null,
                                    System.Nullable<int> datapak_PollingStatus = null,
                                    string empCardNo = "",
                                    string empCardInTime = "",
                                    System.Nullable<int> gMU_Machine_Status = null)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.usp_UpdateFloorStatus(installation_No, systemLastUpdate, meterLastUpdate,
                        doorStatus, powerStatus, ePIStatus, ePIDetails, datapak_Variant, datapak_LastEventNo,
                        datapak_PollingStatus, empCardNo, empCardInTime, gMU_Machine_Status);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return -1;
            }
        }

        public DataRow GetMeterDeltaForPlayerSession(int installationNo, string cardNo, string sessionType)
        {
            try
            {
                using (Database db = DbFactory.OpenDB(ConnectionString))
                {
                    DbParameter[] parameters = db.CreateParametersV(
                        db.CreateParameter("@InstallationNo", installationNo),
                        db.CreateParameter("@PlayercardNo", cardNo),
                        db.CreateParameter("@SessionType", sessionType));
                    return db.ExecuteDataset("rsp_GetMeterDeltaForPlayerSession", parameters).GetDataRow(0, 0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return null;
            }
        }

        public string GetDevSettingValue(string keyName, string defaultValue)
        {
            try
            {
                using (Database db = DbFactory.OpenDB(ConnectionString))
                {
                    DataRow dr = db.ExecuteDataset(CommandType.Text, "SELECT * FROM DEVSETTINGS").GetDataRow(0, 0);
                    if (dr != null &&
                        dr[keyName] != DBNull.Value)
                    {
                        return dr[keyName].ToStringSafe();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }

            return defaultValue;
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetInstallationDetailsForMSMQ")]
        public ISingleResult<InstallationDetailsForMSMQ> rsp_GetInstallationDetailsForMSMQ([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Datapak_Serial", DbType = "Int")] System.Nullable<int> datapak_Serial, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Ecash", DbType = "Bit")] System.Nullable<bool> ecash)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), datapak_Serial, ecash);
            return ((ISingleResult<InstallationDetailsForMSMQ>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdatePlayerGatewayMessages")]
        public int usp_UpdatePlayerGatewayMessages([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardNumber", DbType = "VarChar(50)")] string cardNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RequestID", DbType = "UniqueIdentifier")] System.Nullable<System.Guid> requestID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DisplayMessage", DbType = "VarChar(1000)")] string displayMessage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNo, cardNumber, requestID, displayMessage);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_InsertAFTTransactions")]
        public int esp_InsertAFTTransactions([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PlayerID", DbType = "Int")] System.Nullable<int> playerID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CollectionNo", DbType = "Int")] System.Nullable<int> collectionNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CashableAmt", DbType = "Float")] System.Nullable<double> cashableAmt, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NoncashableAmt", DbType = "Float")] System.Nullable<double> noncashableAmt, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "WATAmt", DbType = "Float")] System.Nullable<double> wATAmt, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TransactionDate", DbType = "DateTime")] System.Nullable<System.DateTime> transactionDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TransactionType", DbType = "VarChar(30)")] string transactionType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TransferID", DbType = "Int")] System.Nullable<int> transferID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AccountType", DbType = "VarChar(30)")] string accountType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TransactionStatus", DbType = "Bit")] System.Nullable<bool> transactionStatus)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNo, playerID, collectionNo, cashableAmt, noncashableAmt, wATAmt, transactionDate, transactionType, transferID, accountType, transactionStatus);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_AuditAFTTransactions")]
        public int esp_AuditAFTTransactions([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AFTInstallationNo", DbType = "Int")] System.Nullable<int> aFTInstallationNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AFTTransactionDate", DbType = "DateTime")] System.Nullable<System.DateTime> aFTTransactionDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AFTTransactionType", DbType = "VarChar(30)")] string aFTTransactionType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CashableAmt", DbType = "Float")] System.Nullable<double> cashableAmt, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NoncashableAmt", DbType = "Float")] System.Nullable<double> noncashableAmt, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "WATAMT", DbType = "Float")] System.Nullable<double> wATAMT, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PlayerID", DbType = "Int")] System.Nullable<int> playerID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EcashEnabled", DbType = "Bit")] System.Nullable<bool> ecashEnabled, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ErrorCode", DbType = "Int")] System.Nullable<int> errorCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ErrorMessage", DbType = "VarChar(100)")] string errorMessage, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TransferID", DbType = "Int")] System.Nullable<int> transferID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), aFTInstallationNo, aFTTransactionDate, aFTTransactionType, cashableAmt, noncashableAmt, wATAMT, playerID, ecashEnabled, errorCode, errorMessage, transferID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "CMP.rsp_GetCMPErrorCode")]
        public ISingleResult<CMPErrorCodes> rsp_GetCMPErrorCode()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<CMPErrorCodes>)(result.ReturnValue));
        }

        [Function(Name = "dbo.esp_InsertPlayerSessionRating")]
        public int esp_InsertPlayerSessionRating([Parameter(Name = "PlayercardNo", DbType = "Int")] System.Nullable<int> playercardNo, [Parameter(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo, [Parameter(Name = "Process", DbType = "VarChar(50)")] string process, [Parameter(Name = "Type", DbType = "VarChar(50)")] string type)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), playercardNo, installationNo, process, type);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.esp_InsertPCSessionRating")]
        public int esp_InsertPCSessionRating([Parameter(Name = "PlayercardNo", DbType = "Int")] System.Nullable<int> playercardNo, [Parameter(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo, [Parameter(Name = "Process", DbType = "VarChar(50)")] string process, [Parameter(Name = "Type", DbType = "VarChar(50)")] string type)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), playercardNo, installationNo, process, type);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.esp_RemovePlayerSessionRating")]
        public int esp_RemovePlayerSessionRating([Parameter(Name = "PlayerCardNo", DbType = "Int")] System.Nullable<int> playerCardNo, [Parameter(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), playerCardNo, installationNo);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.esp_RemovePCSessionRating")]
        public int esp_RemovePCSessionRating([Parameter(Name = "PlayerCardNo", DbType = "Int")] System.Nullable<int> playerCardNo, [Parameter(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), playerCardNo, installationNo);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_OpenOrCloseGamePlaySessionForInstallation")]
        public int usp_OpenOrCloseGamePlaySessionForInstallation([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [Parameter(Name = "Operation_Type", DbType = "Int")] System.Nullable<int> operation_Type)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, operation_Type);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_OpenUserSessionForCardedGamePlay")]
        public int usp_OpenUserSessionForCardedGamePlay([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_OpenUserSessionForGamePlay")]
        public int usp_OpenUserSessionForGamePlay([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_CloseGamePlaySessionForCardedPlay")]
        public int usp_CloseGamePlaySessionForCardedPlay([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_CloseGamePlaySessionOnZeroCreditEvent")]
        public int usp_CloseGamePlaySessionOnZeroCreditEvent([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.IsCardSessionExists")]
        public ISingleResult<IsCardSessionExistsResult> IsCardSessionExists([Parameter(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo, [Parameter(Name = "CardNumber", DbType = "Int")] System.Nullable<int> cardNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNo, cardNumber);
            return ((ISingleResult<IsCardSessionExistsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetMeterDeltaForPlayerSession")]
        public int rsp_GetMeterDeltaForPlayerSession([Parameter(DbType = "Int")] System.Nullable<int> installationNo, [Parameter(Name = "PlayercardNo", DbType = "VarChar(20)")] string playercardNo, [Parameter(DbType = "NChar(5)")] string sessionType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNo, playercardNo, sessionType);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class IsCardSessionExistsResult
    {

        private int _Column1;

        public IsCardSessionExistsResult()
        {
        }

        [Column(Storage = "_Column1", DbType = "Int NOT NULL")]
        public int Column1
        {
            get
            {
                return this._Column1;
            }
            set
            {
                if ((this._Column1 != value))
                {
                    this._Column1 = value;
                }
            }
        }
    }

    public partial class InstallationDetailsForMSMQ
    {

        private System.Nullable<int> _Machine_No;

        private int _Installation_No;

        private int _Bar_Pos_No;

        private System.Nullable<int> _TBR_Node_Serial;

        private string _Bar_Pos_Name;

        private System.Nullable<int> _Bar_Pos_Comm;

        private System.Nullable<int> _Bar_Pos_Port;

        private System.Nullable<int> _Bar_Pos_Poll;

        private string _Model_Code;

        private string _Stock_No;

        private string _Machine_Manufacturers_Serial_No;

        private string _Machine_Alternative_Serial_Numbers;

        private System.Nullable<int> _Installation_Price_Of_Play;

        private int _Installation_Token_Value;

        private System.Nullable<int> _Installation_Jackpot;

        private System.Nullable<float> _Anticipated_Percentage_Payout;

        private string _Location;

        private System.Nullable<System.DateTime> _End_Date;

        private string _Pack_description;

        private System.Nullable<int> _Datapak_Serial;

        private int _Zone_No;

        private string _Zone_Name;

        private System.Nullable<int> _Zone_TBR_Serial_No;

        private System.Nullable<int> _Machine_Type_ID;

        private string _Machine_Type_Code;

        private System.Nullable<int> _Installation_Float_Status;

        private System.Nullable<int> _Machine_Class_Special_Features;

        private int _isSAS;

        private System.Nullable<int> _Datapak_Variant;

        private System.DateTime _LastMeterUpdate;

        private bool _BarPositionMachineEnabled;

        private System.Nullable<int> _Datapak_LastEventNo;

        private string _Name;

        private char _GameType;

        private System.Nullable<System.DateTime> _SystemLastUpdate;

        private bool _Datapak_PollingStatus;

        private int _GMU_Machine_Status;

        private string _EPI_Details;

        private System.Nullable<System.DateTime> _CardInTime;

        private System.Nullable<int> _Validation_Length;

        private string _SiteCode;

        private string _CMPCode;

        private int _TransactionID;

        private string _EmployeeCardNumber;

        public InstallationDetailsForMSMQ()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_No", DbType = "Int")]
        public System.Nullable<int> Machine_No
        {
            get
            {
                return this._Machine_No;
            }
            set
            {
                if ((this._Machine_No != value))
                {
                    this._Machine_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Pos_No", DbType = "Int NOT NULL")]
        public int Bar_Pos_No
        {
            get
            {
                return this._Bar_Pos_No;
            }
            set
            {
                if ((this._Bar_Pos_No != value))
                {
                    this._Bar_Pos_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TBR_Node_Serial", DbType = "Int")]
        public System.Nullable<int> TBR_Node_Serial
        {
            get
            {
                return this._TBR_Node_Serial;
            }
            set
            {
                if ((this._TBR_Node_Serial != value))
                {
                    this._TBR_Node_Serial = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Pos_Name", DbType = "VarChar(50)")]
        public string Bar_Pos_Name
        {
            get
            {
                return this._Bar_Pos_Name;
            }
            set
            {
                if ((this._Bar_Pos_Name != value))
                {
                    this._Bar_Pos_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Pos_Comm", DbType = "Int")]
        public System.Nullable<int> Bar_Pos_Comm
        {
            get
            {
                return this._Bar_Pos_Comm;
            }
            set
            {
                if ((this._Bar_Pos_Comm != value))
                {
                    this._Bar_Pos_Comm = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Pos_Port", DbType = "Int")]
        public System.Nullable<int> Bar_Pos_Port
        {
            get
            {
                return this._Bar_Pos_Port;
            }
            set
            {
                if ((this._Bar_Pos_Port != value))
                {
                    this._Bar_Pos_Port = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Pos_Poll", DbType = "Int")]
        public System.Nullable<int> Bar_Pos_Poll
        {
            get
            {
                return this._Bar_Pos_Poll;
            }
            set
            {
                if ((this._Bar_Pos_Poll != value))
                {
                    this._Bar_Pos_Poll = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Model_Code", DbType = "VarChar(50)")]
        public string Model_Code
        {
            get
            {
                return this._Model_Code;
            }
            set
            {
                if ((this._Model_Code != value))
                {
                    this._Model_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Stock_No", DbType = "VarChar(50)")]
        public string Stock_No
        {
            get
            {
                return this._Stock_No;
            }
            set
            {
                if ((this._Stock_No != value))
                {
                    this._Stock_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Manufacturers_Serial_No", DbType = "VarChar(50)")]
        public string Machine_Manufacturers_Serial_No
        {
            get
            {
                return this._Machine_Manufacturers_Serial_No;
            }
            set
            {
                if ((this._Machine_Manufacturers_Serial_No != value))
                {
                    this._Machine_Manufacturers_Serial_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Alternative_Serial_Numbers", DbType = "VarChar(50)")]
        public string Machine_Alternative_Serial_Numbers
        {
            get
            {
                return this._Machine_Alternative_Serial_Numbers;
            }
            set
            {
                if ((this._Machine_Alternative_Serial_Numbers != value))
                {
                    this._Machine_Alternative_Serial_Numbers = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_Price_Of_Play", DbType = "Int")]
        public System.Nullable<int> Installation_Price_Of_Play
        {
            get
            {
                return this._Installation_Price_Of_Play;
            }
            set
            {
                if ((this._Installation_Price_Of_Play != value))
                {
                    this._Installation_Price_Of_Play = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_Token_Value", DbType = "Int NOT NULL")]
        public int Installation_Token_Value
        {
            get
            {
                return this._Installation_Token_Value;
            }
            set
            {
                if ((this._Installation_Token_Value != value))
                {
                    this._Installation_Token_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_Jackpot", DbType = "Int")]
        public System.Nullable<int> Installation_Jackpot
        {
            get
            {
                return this._Installation_Jackpot;
            }
            set
            {
                if ((this._Installation_Jackpot != value))
                {
                    this._Installation_Jackpot = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Anticipated_Percentage_Payout", DbType = "Real")]
        public System.Nullable<float> Anticipated_Percentage_Payout
        {
            get
            {
                return this._Anticipated_Percentage_Payout;
            }
            set
            {
                if ((this._Anticipated_Percentage_Payout != value))
                {
                    this._Anticipated_Percentage_Payout = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Location", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Location
        {
            get
            {
                return this._Location;
            }
            set
            {
                if ((this._Location != value))
                {
                    this._Location = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> End_Date
        {
            get
            {
                return this._End_Date;
            }
            set
            {
                if ((this._End_Date != value))
                {
                    this._End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Pack_description", DbType = "VarChar(50)")]
        public string Pack_description
        {
            get
            {
                return this._Pack_description;
            }
            set
            {
                if ((this._Pack_description != value))
                {
                    this._Pack_description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Datapak_Serial", DbType = "Int")]
        public System.Nullable<int> Datapak_Serial
        {
            get
            {
                return this._Datapak_Serial;
            }
            set
            {
                if ((this._Datapak_Serial != value))
                {
                    this._Datapak_Serial = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Zone_No", DbType = "Int NOT NULL")]
        public int Zone_No
        {
            get
            {
                return this._Zone_No;
            }
            set
            {
                if ((this._Zone_No != value))
                {
                    this._Zone_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Zone_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Zone_Name
        {
            get
            {
                return this._Zone_Name;
            }
            set
            {
                if ((this._Zone_Name != value))
                {
                    this._Zone_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Zone_TBR_Serial_No", DbType = "Int")]
        public System.Nullable<int> Zone_TBR_Serial_No
        {
            get
            {
                return this._Zone_TBR_Serial_No;
            }
            set
            {
                if ((this._Zone_TBR_Serial_No != value))
                {
                    this._Zone_TBR_Serial_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Type_ID", DbType = "Int")]
        public System.Nullable<int> Machine_Type_ID
        {
            get
            {
                return this._Machine_Type_ID;
            }
            set
            {
                if ((this._Machine_Type_ID != value))
                {
                    this._Machine_Type_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
        public string Machine_Type_Code
        {
            get
            {
                return this._Machine_Type_Code;
            }
            set
            {
                if ((this._Machine_Type_Code != value))
                {
                    this._Machine_Type_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_Float_Status", DbType = "Int")]
        public System.Nullable<int> Installation_Float_Status
        {
            get
            {
                return this._Installation_Float_Status;
            }
            set
            {
                if ((this._Installation_Float_Status != value))
                {
                    this._Installation_Float_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_Special_Features", DbType = "Int")]
        public System.Nullable<int> Machine_Class_Special_Features
        {
            get
            {
                return this._Machine_Class_Special_Features;
            }
            set
            {
                if ((this._Machine_Class_Special_Features != value))
                {
                    this._Machine_Class_Special_Features = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_isSAS", DbType = "Int NOT NULL")]
        public int isSAS
        {
            get
            {
                return this._isSAS;
            }
            set
            {
                if ((this._isSAS != value))
                {
                    this._isSAS = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Datapak_Variant", DbType = "Int")]
        public System.Nullable<int> Datapak_Variant
        {
            get
            {
                return this._Datapak_Variant;
            }
            set
            {
                if ((this._Datapak_Variant != value))
                {
                    this._Datapak_Variant = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastMeterUpdate", DbType = "DateTime NOT NULL")]
        public System.DateTime LastMeterUpdate
        {
            get
            {
                return this._LastMeterUpdate;
            }
            set
            {
                if ((this._LastMeterUpdate != value))
                {
                    this._LastMeterUpdate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BarPositionMachineEnabled", DbType = "Bit NOT NULL")]
        public bool BarPositionMachineEnabled
        {
            get
            {
                return this._BarPositionMachineEnabled;
            }
            set
            {
                if ((this._BarPositionMachineEnabled != value))
                {
                    this._BarPositionMachineEnabled = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Datapak_LastEventNo", DbType = "Int")]
        public System.Nullable<int> Datapak_LastEventNo
        {
            get
            {
                return this._Datapak_LastEventNo;
            }
            set
            {
                if ((this._Datapak_LastEventNo != value))
                {
                    this._Datapak_LastEventNo = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "VarChar(50)")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GameType", DbType = "Char(1) NOT NULL")]
        public char GameType
        {
            get
            {
                return this._GameType;
            }
            set
            {
                if ((this._GameType != value))
                {
                    this._GameType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SystemLastUpdate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> SystemLastUpdate
        {
            get
            {
                return this._SystemLastUpdate;
            }
            set
            {
                if ((this._SystemLastUpdate != value))
                {
                    this._SystemLastUpdate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Datapak_PollingStatus", DbType = "Bit NOT NULL")]
        public bool Datapak_PollingStatus
        {
            get
            {
                return this._Datapak_PollingStatus;
            }
            set
            {
                if ((this._Datapak_PollingStatus != value))
                {
                    this._Datapak_PollingStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GMU_Machine_Status", DbType = "Int NOT NULL")]
        public int GMU_Machine_Status
        {
            get
            {
                return this._GMU_Machine_Status;
            }
            set
            {
                if ((this._GMU_Machine_Status != value))
                {
                    this._GMU_Machine_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EPI_Details", DbType = "VarChar(50)")]
        public string EPI_Details
        {
            get
            {
                return this._EPI_Details;
            }
            set
            {
                if ((this._EPI_Details != value))
                {
                    this._EPI_Details = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CardInTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> CardInTime
        {
            get
            {
                return this._CardInTime;
            }
            set
            {
                if ((this._CardInTime != value))
                {
                    this._CardInTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Validation_Length", DbType = "Int")]
        public System.Nullable<int> Validation_Length
        {
            get
            {
                return this._Validation_Length;
            }
            set
            {
                if ((this._Validation_Length != value))
                {
                    this._Validation_Length = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SiteCode", DbType = "VarChar(50)")]
        public string SiteCode
        {
            get
            {
                return this._SiteCode;
            }
            set
            {
                if ((this._SiteCode != value))
                {
                    this._SiteCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CMPCode", DbType = "VarChar(50)")]
        public string CMPCode
        {
            get
            {
                return this._CMPCode;
            }
            set
            {
                if ((this._CMPCode != value))
                {
                    this._CMPCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TransactionID", DbType = "Int NOT NULL")]
        public int TransactionID
        {
            get
            {
                return this._TransactionID;
            }
            set
            {
                if ((this._TransactionID != value))
                {
                    this._TransactionID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EmployeeCardNumber", DbType = "VarChar(20)")]
        public string EmployeeCardNumber
        {
            get
            {
                return this._EmployeeCardNumber;
            }
            set
            {
                if ((this._EmployeeCardNumber != value))
                {
                    this._EmployeeCardNumber = value;
                }
            }
        }
    }

    public partial class CMPErrorCodes
    {

        private System.Nullable<int> _CMPErrorCode;

        private System.Nullable<int> _GmuErrorCode;

        private string _ErrorDescription;

        public CMPErrorCodes()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CMPErrorCode", DbType = "Int")]
        public System.Nullable<int> CMPErrorCode
        {
            get
            {
                return this._CMPErrorCode;
            }
            set
            {
                if ((this._CMPErrorCode != value))
                {
                    this._CMPErrorCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GmuErrorCode", DbType = "Int")]
        public System.Nullable<int> GmuErrorCode
        {
            get
            {
                return this._GmuErrorCode;
            }
            set
            {
                if ((this._GmuErrorCode != value))
                {
                    this._GmuErrorCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ErrorDescription", DbType = "VarChar(500)")]
        public string ErrorDescription
        {
            get
            {
                return this._ErrorDescription;
            }
            set
            {
                if ((this._ErrorDescription != value))
                {
                    this._ErrorDescription = value;
                }
            }
        }
    }
}
