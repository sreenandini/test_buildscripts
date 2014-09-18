using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using iCAS.BMC.GDB.GDBService;
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.Common.Utilities;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Presentation;
using System.Threading;
using System.Windows.Threading;
using BMC.Business.CashDeskOperator;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ConfigurationManagement;
namespace BMC.Presentation.CashDeskOperator
{
    #region GloryCashDispenser

    public class GloryCashDispenser : BmcCashDispenserBase
    {
        private CGDBSCCashoutDetail _cOut { get; set; }
        private ModuleName _cType { get; set; }
        enum MessageType
        {
            Error,
            Information
        }
        string _ValidationNo { get; set; }
        string _TicketNo { get; set; }
        int _RequestAmount { get; set; }
        public GloryCashDispenser(UIElement parent, ModuleName ctype)
            : base(parent, ctype)
        {
            _cType = ctype;

        }
        private const string DispenserType = "Glory Cash Dispenser ==> ";
        public override event BmcCashDispenserFinishedHandler Finished;
        public override event BmcCashDispenserStatusChangeHandler StatusChanged;

        public override Result Dispense(string TicketNo, string BarPosNo, int Amount)
        {
            Result res = new Result();
            //Implement glory dispenser interface
            try
            {
                _RequestAmount = Amount;
                _TicketNo = TicketNo;
                DateTime PrintDate = DateTime.Now;

                _ValidationNo = "#" + BarPosNo + DateTime.Now.ToString("ddMMyyyyHHmmss");
                _cOut = new CGDBSCCashoutDetail();
                _cOut.ValidationNo = _ValidationNo;//ticket no
                _cOut.Amount = Amount;//in Cents
                _cOut.AssetNo = BarPosNo;
                res.IsSuccess = true;
                res.error = new Error();
                res.error.MessageType = MessageType.Information.ToString();

                LogManager.WriteLog("Copying Glory Server Details from Registry" + UserInformation.User, LogManager.enumLogLevel.Info);
                IDictionary<string, string> dicGloryCDDetails = CommonDataAccess.GetGloryServerDetails();
                if (dicGloryCDDetails == null || dicGloryCDDetails.Count == 0)
                {
                    LogManager.WriteLog("Unable to find Glory Server Details from Registry" + UserInformation.User, LogManager.enumLogLevel.Info);
                    res.IsSuccess = false;
                    res.error.Message = "Unable to find Glory Server Details";
                    res.error.MessageType = MessageType.Error.ToString();
                    this.OnFinished(res);
                }
                else
                {
                    GloryDeviceHelper._ServerName = dicGloryCDDetails["ServerName"];
                    GloryDeviceHelper._ServerPort = Convert.ToInt32(dicGloryCDDetails["PortNo"]);

                    UserInformation.User = dicGloryCDDetails["UserName"];
                    UserInformation.Device = dicGloryCDDetails["DeviceName"];

                    UserInformation.Password = CommonDataAccess.GetGloryServerPassword();

                    UserInformation.ID = "BMC";
                    GloryDeviceHelper._SSL = dicGloryCDDetails["SSL"].ToUpper().Equals("TRUE") ? true : false;


                    //Finding Mode Of Glory CashDispenser is Duplex or Non Duplex
                    string strDispenserType = "";
                    try
                    {
                        strDispenserType = ConfigManager.GetConfigurationObject().Read("CD_Duplex");
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog(DispenserType + " Mode not found. Choosing default type-'DUPLEX MODE'", LogManager.enumLogLevel.Info);
                        strDispenserType = "TRUE";
                    }

                    bool IsNotDuplex = strDispenserType.ToUpper().Equals("TRUE") ? false : true;
                    //Finding SSL Mode Of Glory CashDispenser
                    //try
                    //{
                    //    GloryDeviceHelper._SSL = ConfigManager.GetConfigurationObject().Read("GLORYCD_SSL").ToUpper().Equals("TRUE") ? true : false;
                    //    // GloryDeviceHelper._SSL = dicGloryCDDetails["SSL"].ToUpper().Equals("TRUE") ? true : false;
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogManager.WriteLog(DispenserType + " SSL configuration not found. Choosing default type SSL-'false'", LogManager.enumLogLevel.Info);
                    //    GloryDeviceHelper._SSL = false;
                    //}

                    GloryDeviceHelper._CertificateThumbprint = String.Empty;

                    //Open Session
                    OnStatusChanged("Opening Session...");
                    _SYS_CODE sOpen = doOpenSession();
                    if (sOpen == _SYS_CODE.SYS_SUCCESS)
                    {
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = _cType,
                            Audit_Screen_Name = _cType + "|Open Session",
                            Audit_Desc = "Open Session Succeed ; Session ID:" + UserInformation.SessionID + "; StartTime:" + DateTime.Now.GetUniversalDateTimeFormat(),
                            AuditOperationType = OperationType.ADD,
                            Audit_Field = "Session ID"
                        });
                        OnStatusChanged("Session Opened Successfully");
                        AuditOpenSessionGloryDetails();
                        LogManager.WriteLog(DispenserType + "User:" + UserInformation.User, LogManager.enumLogLevel.Info);

                        //Occupy
                        OnStatusChanged("Occupying Device...");
                        _SYS_CODE sOcc = doOccupy();
                        if (sOcc == _SYS_CODE.SYS_SUCCESS)
                        {
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                AuditModuleName = _cType,
                                Audit_Screen_Name = _cType + "|Occupy",
                                Audit_Desc = "Occupy Succeed ; Device Name:" + UserInformation.Device,
                                AuditOperationType = OperationType.ADD,
                                Audit_Field = "Device Name"
                            });
                            OnStatusChanged("Device: " + UserInformation.Device + " Occupied");

                            //Cashout
                            List<CGDBSCCashoutDetail> listCashoutDetail = new List<CGDBSCCashoutDetail>();
                            listCashoutDetail.Add(_cOut);
                            LogManager.WriteLog(DispenserType + "Cashout :- TicketNo: " + TicketNo + "; Bar Position No: " + BarPosNo + "; Amount: " + Amount, LogManager.enumLogLevel.Info);

                            OnStatusChanged(" Dispensing Cash [" + Amount + "] in Cents...");
                            _TRANS_TYPE _type = _TRANS_TYPE.TICKET_REDEMPTION;
                            if (_cType != null)
                            {
                                switch (_cType)
                                {
                                    case ModuleName.AttendantPay:
                                        _type = _TRANS_TYPE.VOUCHER_REDEMPTION;
                                        break;
                                    case ModuleName.ManualAttendantPay:
                                        _type = _TRANS_TYPE.JACKPOT;
                                        break;
                                }
                            }

                            _SYS_CODE sRes = doCashout(listCashoutDetail, IsNotDuplex, _type);
                            if (sRes != _SYS_CODE.SYS_SUCCESS)
                            {
                                res.IsSuccess = false;
                                res.error.Message = sRes.ToString();
                                res.error.Code = (int)sRes;
                                res.error.MessageType = MessageType.Error.ToString();
                                AuditCloseSessionGloryDetails(res.IsSuccess, res.error.Message);
                                GloryDeviceHelper.Instance.HeartbeatStop();
                                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                {
                                    AuditModuleName = _cType,
                                    Audit_Screen_Name = _cType + "|Cashout",
                                    Audit_Desc = "Cashout Failed ; Session ID:" + UserInformation.SessionID,
                                    AuditOperationType = OperationType.ADD,
                                    Audit_Field = "Session ID"
                                });
                                res.error.Message = Application.Current.FindResource("MessageID442") as string;
                                this.OnFinished(res);
                            }
                            //Non Duplex Mode
                            if (IsNotDuplex)
                            {
                                #region Release
                                _SYS_CODE syRel = doRelease();
                                if (syRel != _SYS_CODE.SYS_SUCCESS)
                                {
                                    LogManager.WriteLog(DispenserType + "Releasing Device Failed: Msg:-" + syRel.ToString() + "Error Code:-" + (int)syRel, LogManager.enumLogLevel.Info);
                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        AuditModuleName = _cType,
                                        Audit_Screen_Name = _cType + "|Release Session",
                                        Audit_Desc = "Failed to Release ; Device Name:" + UserInformation.Device,
                                        AuditOperationType = OperationType.ADD,
                                        Audit_Field = "Device"
                                    });
                                    OnStatusChanged("Failed to Release ; Device Name:" + UserInformation.Device);
                                }
                                else
                                {
                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        AuditModuleName = _cType,
                                        Audit_Screen_Name = _cType + "|Release Session",
                                        Audit_Desc = "Release Succeed ; Device Name:" + UserInformation.Device,
                                        AuditOperationType = OperationType.ADD,
                                        Audit_Field = "Device"
                                    });
                                    OnStatusChanged("Device Name:" + UserInformation.Device + " released succeesfully");
                                    LogManager.WriteLog(DispenserType + "Device Name:" + UserInformation.Device + " released succeesfully", LogManager.enumLogLevel.Info);
                                }
                                #endregion

                                #region Close Session
                                _SYS_CODE syObj = doCloseSession();
                                if (syObj != _SYS_CODE.SYS_SUCCESS)
                                {
                                    LogManager.WriteLog(DispenserType + "CloseSession Failed: Msg:-" + syObj.ToString() + "Error Code:-" + (int)syObj, LogManager.enumLogLevel.Info);
                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        AuditModuleName = _cType,
                                        Audit_Screen_Name = _cType + "|Close Session",
                                        Audit_Desc = "Close Session Failed ; Session ID:" + UserInformation.SessionID + "; EndTime:" + DateTime.Now.GetUniversalDateTimeFormat(),
                                        AuditOperationType = OperationType.ADD,
                                        Audit_Field = "Session ID"
                                    });
                                    OnStatusChanged("Close Session Failed");
                                    res.error.Message = Application.Current.FindResource("MessageID441") as string;
                                }
                                else
                                {
                                    AuditCloseSessionGloryDetails(true, string.Empty);
                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        AuditModuleName = _cType,
                                        Audit_Screen_Name = _cType + "|Close Session",
                                        Audit_Desc = "Close Session Succeed ; Session ID:" + UserInformation.SessionID + "; EndTime:" + DateTime.Now.GetUniversalDateTimeFormat(),
                                        AuditOperationType = OperationType.ADD,
                                        Audit_Field = "Session ID"
                                    });
                                    OnStatusChanged("Close Session Succeed");
                                    res.error.Message = Application.Current.FindResource("MessageID441") as string;
                                    LogManager.WriteLog(DispenserType + "Close Session Succeed", LogManager.enumLogLevel.Info);


                                }

                                #endregion

                                this.OnFinished(res);

                            }
                        }
                        else
                        {
                            res.IsSuccess = false;
                            res.error.Message = sOcc.ToString();
                            res.error.Code = (int)sOcc;
                            res.error.MessageType = MessageType.Error.ToString();
                            AuditCloseSessionGloryDetails(res.IsSuccess, res.error.Message);
                            GloryDeviceHelper.Instance.HeartbeatStop();
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                AuditModuleName = _cType,
                                Audit_Screen_Name = _cType + "|Occupy",
                                Audit_Desc = "Occupy Failed ; Device Name:" + UserInformation.Device,
                                AuditOperationType = OperationType.ADD,
                                Audit_Field = "Device Name"
                            });
                            res.error.Message = Application.Current.FindResource("MessageID442") as string;
                            this.OnFinished(res);

                        }
                    }
                    else
                    {
                        res.IsSuccess = false;
                        res.error.Message = sOpen.ToString();
                        res.error.Code = (int)sOpen;
                        res.error.MessageType = MessageType.Error.ToString();
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = _cType,
                            Audit_Screen_Name = _cType + "|Open Session",
                            Audit_Desc = "Open Session Failed; StartTime:" + DateTime.Now.GetUniversalDateTimeFormat(),
                            AuditOperationType = OperationType.ADD,
                            Audit_Field = "Session ID"
                        });
                        res.error.Message = Application.Current.FindResource("MessageID443") as string;
                        this.OnFinished(res);

                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(DispenserType + "Dispense" + ex.Message, LogManager.enumLogLevel.Error);
                res.error.Message = ex.Message;
                res.IsSuccess = false;
                res.error.MessageType = MessageType.Error.ToString();
                this.OnFinished(res);
            }

            return res;
        }

        private bool AuditOpenSessionGloryDetails()
        {
            bool retVal = true;
            try
            {
                if (_cOut != null)
                {
                    GloryCashDetails gCash = new GloryCashDetails();
                    gCash.AssetNo = _cOut.AssetNo;
                    gCash.Device = UserInformation.Device;
                    gCash.ErrorCode = null;
                    gCash.SessionID = UserInformation.SessionID;
                    gCash.Status = '0';
                    gCash.TicketNo = _TicketNo;
                    gCash.ValidationNo = _cOut.ValidationNo;
                    gCash.TransactionAmount = _cOut.Amount;
                    gCash.TransactionStarttime = DateTime.Now;
                    gCash.TransactionEndtime = null;
                    gCash.TransactionType = _cType.ToString();
                    gCash.UserID = UserInformation.ID;
                    GloryCashDispDataAccess GDB = new GloryCashDispDataAccess();
                    int SeqID = 0;
                    if (GDB.InsertGloryOpenSessDetails(gCash, ref SeqID))
                    {
                        UserInformation.SeqID = SeqID;
                        LogManager.WriteLog(DispenserType + "Open Session Glory Details Inserted Succeesful", LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        retVal = false;
                        LogManager.WriteLog(DispenserType + "Open Session Glory Details Failed to Insert", LogManager.enumLogLevel.Info);
                    }
                    // InsertGloryOpenSessDetails
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                ExceptionManager.Publish(ex);
            }
            return retVal;

        }

        private bool AuditCloseSessionGloryDetails(bool IsSuccess, string ErrorCode)
        {
            bool retVal = true;
            try
            {
                if (_cOut != null)
                {
                    GloryCashDetails gCash = new GloryCashDetails();
                    gCash.AssetNo = _cOut.AssetNo;
                    gCash.Device = UserInformation.Device;
                    gCash.ErrorCode = ErrorCode.Equals("") ? null : ErrorCode;
                    gCash.SessionID = UserInformation.SessionID;
                    gCash.Status = IsSuccess ? '1' : '0';
                    gCash.TicketNo = _cOut.ValidationNo;
                    gCash.TransactionAmount = _cOut.Amount;
                    gCash.TransactionStarttime = DateTime.Now;
                    gCash.TransactionEndtime = null;
                    gCash.TransactionType = _cType.ToString();
                    gCash.UserID = UserInformation.ID;
                    gCash.Sequenceid = UserInformation.SeqID;
                    GloryCashDispDataAccess GDB = new GloryCashDispDataAccess();
                    if (GDB.UpdateGloryDetails(gCash))
                    {
                        LogManager.WriteLog(DispenserType + "Close Session Glory Details Inserted Succeesful", LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        retVal = false;
                        LogManager.WriteLog(DispenserType + "Close Session Glory Details Failed to Insert", LogManager.enumLogLevel.Info);
                    }
                    // InsertGloryOpenSessDetails
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                ExceptionManager.Publish(ex);
            }
            return retVal;

        }

        private _SYS_CODE doOpenSession()
        {

            try
            {

                BallyCOpenSessionResponse res = GloryDeviceHelper.Instance.OpenSession();

                if (res != null)
                {

                    LogManager.WriteLog(DispenserType + "OpenSession result:" + res.Result, LogManager.enumLogLevel.Info);
                    if (res.Result == iCAS.BMC.GDB.GDBService._SYS_CODE.SYS_SUCCESS)
                    {
                        UserInformation.FlagConnected = true;
                        UserInformation.SessionID = res.SessionID;
                        UserInformation.Heartbeat = res.Heartbeat;
                        UserInformation.HeartbeatCountdown = UserInformation.Heartbeat;
                        LogManager.WriteLog(DispenserType + "Session Opened:" + res.Result, LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog(DispenserType + "Error:" + res.Result + res.Desc, LogManager.enumLogLevel.Error);
                        UserInformation.FlagConnected = false;
                        UserInformation.SessionID = String.Empty;


                    }
                }
                return res.Result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(DispenserType + "Dispense.doOpenSession::" + ex.Message, LogManager.enumLogLevel.Error);
                return _SYS_CODE.SYS_ERROR;
            }

        }

        private void evtProcessCompleteCashout(object sender, EventArgs e)
        {
            int totalPiecesDispensed = 0;
            double totalAmountDispensed = 0;
            Result _result = new Result();
            _result.IsSuccess = true;
            _result.error = new Error();
            _result.error.MessageType = MessageType.Information.ToString();
            try
            {
                if (((BallyCEAProcessComplete)e).OpType != _OP_TYPE.Cashout) return;
                GloryDeviceHelper.Instance._evtProcessComplete -= new EventHandler(evtProcessCompleteCashout);


                BallyCEAProcessComplete ea = (BallyCEAProcessComplete)e;
                BallyCCashoutResponse res = (BallyCCashoutResponse)ea.ResponseInfo;

                if (res != null)
                {
                    #region Cashout
                    LogManager.WriteLog(DispenserType + "Cashout result:" + res.Result, LogManager.enumLogLevel.Info);
                    if (res.Result == _SYS_CODE.SYS_SUCCESS)
                    {
                        String msg = String.Format("Cashout result: {0}", res.Result);
                        StringBuilder strPieceFace = new StringBuilder();
                        strPieceFace.Append("FaceValue*Piece==> ");
                        foreach (CGDBSCDenomination denom in res.CashoutDetail)
                        {
                            msg = String.Format("{0}\n{1:c2} : {2}", msg, denom.FaceValue / 100, denom.Piece);

                            #region Update the total
                            {
                                totalPiecesDispensed += denom.Piece;
                                totalAmountDispensed += ((double)denom.Piece * denom.FaceValue / 100);
                                strPieceFace.Append(" " + denom.FaceValue + "F" + "*" + denom.Piece + "Piece;");
                                LogManager.WriteLog(DispenserType + " Piece :" + denom.Piece + " * FaceValue :" + denom.FaceValue + "==> AmountDispensed= " + (denom.Piece * denom.FaceValue), LogManager.enumLogLevel.Info);
                            }
                            #endregion
                        }
                        strPieceFace.Append("TotalAmountDispensed==>" + (totalAmountDispensed * 100));
                        LogManager.WriteLog(DispenserType + "Total Piece Dispensed :" + totalPiecesDispensed + " Total AmountDispensed= " + (totalAmountDispensed * 100), LogManager.enumLogLevel.Info);

                        LogManager.WriteLog(DispenserType + "RequestAmount:" + _RequestAmount + " ResponseAmount:" + (totalAmountDispensed * 100), LogManager.enumLogLevel.Info);
                        if (_RequestAmount == (totalAmountDispensed * 100))
                        {
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                AuditModuleName = _cType,
                                Audit_Screen_Name = _cType + "|Cashout Session",
                                Audit_Desc = "Cashout Session Succeed ; Session ID:" + UserInformation.SessionID + "; TicketNo:" + _TicketNo + ";RequestAmount:" + _RequestAmount + " ResponseAmount:" + (totalAmountDispensed * 100) + ";",
                                AuditOperationType = OperationType.ADD,
                                Audit_Field = "Amount",
                                Audit_New_Vl = strPieceFace.ToString()
                            });
                        }
                        else
                        {
                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                AuditModuleName = _cType,
                                Audit_Screen_Name = _cType + "|Cashout Session",
                                Audit_Desc = "Cashout Session InComplete ; Session ID:" + UserInformation.SessionID + "; TicketNo:" + _TicketNo + ";RequestAmount:" + _RequestAmount + " ResponseAmount:" + (totalAmountDispensed * 100) + ";",
                                AuditOperationType = OperationType.ADD,
                                Audit_Field = "Amount",
                                Audit_New_Vl = strPieceFace.ToString()
                            });
                        }
                        OnStatusChanged("Cash Dispensed Successfully");
                    }
                    else
                    {
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = _cType,
                            Audit_Screen_Name = _cType + "|Cashout Session",
                            Audit_Desc = "Cashout Failed ; Session ID:" + UserInformation.SessionID + "; TicketNo:" + _TicketNo + "; Amount:" + totalAmountDispensed + ";",
                            AuditOperationType = OperationType.ADD,
                            Audit_Field = "Session ID"
                        });
                        _result.IsSuccess = false;
                        _result.error.Message = Application.Current.FindResource("MessageID442") as string;
                        _result.error.Code = (int)res.Result;
                        _result.error.MessageType = MessageType.Error.ToString();
                        LogManager.WriteLog(DispenserType + "Error:" + res.Result + res.Desc, LogManager.enumLogLevel.Error);

                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(DispenserType + "evtProcessCompleteCashout::" + ex.Message, LogManager.enumLogLevel.Error);
                _result.IsSuccess = false;
                _result.error.Message = ex.Message;
            }
            finally
            {
                try
                {
                    #region Release
                    _SYS_CODE syRel = doRelease();
                    if (syRel != _SYS_CODE.SYS_SUCCESS)
                    {
                        LogManager.WriteLog(DispenserType + "Releasing Device Failed: Msg:-" + syRel.ToString() + "Error Code:-" + (int)syRel, LogManager.enumLogLevel.Info);
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = _cType,
                            Audit_Screen_Name = _cType + "|Release Session",
                            Audit_Desc = "Failed to Release ; Device Name:" + UserInformation.Device,
                            AuditOperationType = OperationType.ADD,
                            Audit_Field = "Device"
                        });
                        OnStatusChanged("Failed to Release ; Device Name:" + UserInformation.Device);
                        //_result.IsSuccess = false;
                        //_result.error.Message = "Failed to Release ; Device Name:" + UserInformation.Device + " " + syRel.ToString();
                        //_result.error.Code = (int)syRel;
                        //_result.error.MessageType = MessageType.Error.ToString();
                    }
                    else
                    {
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                           {
                               AuditModuleName = _cType,
                               Audit_Screen_Name = _cType + "|Release Session",
                               Audit_Desc = "Release Succeed ; Device Name:" + UserInformation.Device,
                               AuditOperationType = OperationType.ADD,
                               Audit_Field = "Device"
                           });
                        OnStatusChanged("Device Name:" + UserInformation.Device + " released succeesfully");
                        LogManager.WriteLog(DispenserType + "Device Name:" + UserInformation.Device + " released succeesfully", LogManager.enumLogLevel.Info);
                    }
                    #endregion

                    #region Close Session
                    _SYS_CODE syObj = doCloseSession();
                    if (syObj != _SYS_CODE.SYS_SUCCESS)
                    {
                        LogManager.WriteLog(DispenserType + "CloseSession Failed: Msg:-" + syObj.ToString() + "Error Code:-" + (int)syObj, LogManager.enumLogLevel.Info);
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = _cType,
                            Audit_Screen_Name = _cType + "|Close Session",
                            Audit_Desc = "Close Session Failed ; Session ID:" + UserInformation.SessionID + "; EndTime:" + DateTime.Now.GetUniversalDateTimeFormat(),
                            AuditOperationType = OperationType.ADD,
                            Audit_Field = "Session ID"
                        });
                        OnStatusChanged("Close Session Failed");
                        //_result.IsSuccess = false;
                        //_result.error.Message = "Close Session Failed" + syObj.ToString();
                        //_result.error.Code = (int)syObj;
                        //_result.error.MessageType = MessageType.Error.ToString();
                        _result.error.Message = Application.Current.FindResource("MessageID441") as string;
                    }
                    else
                    {
                        AuditCloseSessionGloryDetails(true, string.Empty);
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = _cType,
                            Audit_Screen_Name = _cType + "|Close Session",
                            Audit_Desc = "Close Session Succeed ; Session ID:" + UserInformation.SessionID + "; EndTime:" + DateTime.Now.GetUniversalDateTimeFormat(),
                            AuditOperationType = OperationType.ADD,
                            Audit_Field = "Session ID"
                        });
                        OnStatusChanged("Close Session Succeed");
                        if (_result.IsSuccess)
                        {
                            _result.error.Message = Application.Current.FindResource("MessageID441") as string;
                        }
                        LogManager.WriteLog(DispenserType + "Close Session Succeed", LogManager.enumLogLevel.Info);


                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(DispenserType + "Exception Occured in Release module" + ex.Message, LogManager.enumLogLevel.Info);
                }

                    #endregion

                this.OnFinished(_result);
            }
        }

        private void OnFinished(Result syObj)
        {
            if (this.Finished != null)
            {
                this.Finished(this, new GloryDispenserResult(syObj));
            }
        }

        private void OnStatusChanged(string Status)
        {

            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, Status);
            }
        }

        private class GloryDispenserResult : IBmcCashDispenseresult
        {
            internal GloryDispenserResult(Result result)
            {
                this.Result = result;
            }

            #region IBmcCashDispenseresult Members

            public Result Result { get; set; }

            #endregion
        }

        _SYS_CODE doCloseSession()
        {
            BallyCCloseSessionResponse res = GloryDeviceHelper.Instance.CloseSession();

            if (res != null)
            {
                LogManager.WriteLog(DispenserType + "CloseSession result:" + res.Result, LogManager.enumLogLevel.Info);
                UserInformation.FlagConnected = false;
                UserInformation.SessionID = String.Empty;
                UserInformation.Heartbeat = 60;
                UserInformation.HeartbeatCountdown = UserInformation.Heartbeat;

                return res.Result;
            }

            return _SYS_CODE.SYS_ERROR_COMMUNICATION;
        }

        _SYS_CODE doHeartbeat()
        {
            BallyCHeartbeatResponse res = GloryDeviceHelper.Instance.Heartbeat();

            if (res != null)
            {
                LogManager.WriteLog(DispenserType + "Heartbeat result:" + res.Result, LogManager.enumLogLevel.Info);
                return res.Result;
            }

            return _SYS_CODE.SYS_ERROR_COMMUNICATION;
        }

        _SYS_CODE doOccupy()
        {
            BallyCOccupyResponse res = GloryDeviceHelper.Instance.Occupy();

            if (res != null)
            {
                LogManager.WriteLog(DispenserType + "Occupy result:" + res.Result, LogManager.enumLogLevel.Info);
                return res.Result;
            }

            return _SYS_CODE.SYS_ERROR_COMMUNICATION;
        }

        _SYS_CODE doRelease()
        {
            BallyCReleaseResponse res = GloryDeviceHelper.Instance.Release();

            if (res != null)
            {
                LogManager.WriteLog(DispenserType + "Release result:" + res.Result, LogManager.enumLogLevel.Info);
                return res.Result;
            }

            return _SYS_CODE.SYS_ERROR_COMMUNICATION;
        }

        _SYS_CODE doCashout(List<CGDBSCCashoutDetail> ListCashoutDetail, bool IsNotDuplex, _TRANS_TYPE _type)
        {
            if (!IsNotDuplex)
            {
                GloryDeviceHelper.Instance._evtProcessComplete += new EventHandler(evtProcessCompleteCashout);
            }

            _SYS_CODE rc = GloryDeviceHelper.Instance.Cashout(ListCashoutDetail, _type);

            LogManager.WriteLog(DispenserType + "Cashout result:" + rc, LogManager.enumLogLevel.Info);
            return rc;
        }


    }


    #endregion
}
