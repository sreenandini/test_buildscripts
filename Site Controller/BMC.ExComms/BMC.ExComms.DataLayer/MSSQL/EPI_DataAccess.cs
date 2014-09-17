using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
    {
        public bool UpdateEmployeeCardSessions(string empcardNumber, System.Nullable<System.DateTime> empcardTime, System.Nullable<int> installationNo, string type)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateEmployeeCardSessions"))
            {
                Log.Info(PROC, "Get the Display Message for the player");
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.esp_InsertEmployeeSessionDetails(empcardNumber, empcardTime, installationNo, type);
                        Log.Info(PROC, "Session Details Inserted successfully");
                        return true;

                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    Log.Info(PROC, "Session Details Insertion Failed");
                    return false;
                }
            }
        }

        public bool SendEmployeeSTMALert(string Bar_Position_Id, System.Nullable<System.DateTime> eventDateTime, string eventType, string emp_card_no)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "SendEmployeeSTMALert"))
            {

                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.rsp_GetSTMAlertForEmployeeCard(Bar_Position_Id, eventDateTime, eventType, emp_card_no);
                        return true;

                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    return false;
                }
            }
        }

        public string GetEmployeeFlags(string emp_card_no)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetEmployeeFlags"))
            {
                Log.Info(PROC, "Employee Flags started");
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        return DataContext.rsp_GetEmployeeFlags(emp_card_no).SingleOrDefault<ExCommsSQLDataAccess.rsp_GetEmployeeFlagsResult>().EmployeeFlags;

                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    return "";
                }
            }
        }

        public bool InsertPlayerInformation(int cardNo, string firstName, string lastName, double pointsBalance, bool isVIP, int cardLevel)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "InsertPlayerInformation"))
            {

                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.esp_InsertPlayerInformation(cardNo, firstName,
                            lastName, pointsBalance, "0", isVIP, cardLevel);
                        Log.Info(PROC, " Updated successfully");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Info(PROC, "InsertPlayerInformation Failed");
                    Log.Exception(ex);
                    return false;
                }
            }
        }

        public bool UpdateDisplayMessageForPDResponse(string messageType, System.Nullable<int> transactionCode, string cardNumber, string displayMessage)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateDisplayMessageForPDResponse"))
            {

                try
                {

                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.usp_UpdatePDDisplayMessage(messageType, transactionCode, cardNumber, displayMessage);
                        Log.Info(PROC, " Updated successfully");
                        return true;

                    }
                }
                catch (Exception ex)
                {
                    Log.Info(PROC, "Update Display Message for PD Response Failed");
                    Log.Exception(ex);
                    return false;
                }
            }
        }



    }
    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_InsertEmployeeSessionDetails")]
        public int esp_InsertEmployeeSessionDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmpcardNumber", DbType = "VarChar(20)")] string empcardNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmpcardTime", DbType = "DateTime")] System.Nullable<System.DateTime> empcardTime, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Type", DbType = "VarChar(20)")] string type)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), empcardNumber, empcardTime, installationNo, type);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetSTMAlertForEmployeeCard")]
        public void rsp_GetSTMAlertForEmployeeCard([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Bar_Position_Id", DbType = "VarChar(20)")] string bar_Position_Id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EventDateTime", DbType = "DateTime")] System.Nullable<System.DateTime> eventDateTime, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EventType", DbType = "VarChar(20)")] string eventType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Emp_card_no", DbType = "VarChar(20)")] string emp_card_no)
        {
            this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), bar_Position_Id, eventDateTime, eventType, emp_card_no);
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetEmployeeFlags")]
        public ISingleResult<rsp_GetEmployeeFlagsResult> rsp_GetEmployeeFlags([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Emp_card_no", DbType = "VarChar(20)")] string emp_card_no)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), emp_card_no);
            return ((ISingleResult<rsp_GetEmployeeFlagsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_InsertPlayerInformation")]
        public int esp_InsertPlayerInformation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PlayerID", DbType = "Int")] System.Nullable<int> playerID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FirstName", DbType = "VarChar(50)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LastName", DbType = "VarChar(50)")] string lastName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PointsBal", DbType = "Float")] System.Nullable<double> pointsBal, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ClubStatus", DbType = "VarChar(30)")] string clubStatus, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsVIP", DbType = "Bit")] System.Nullable<bool> isVIP, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardLevel", DbType = "Int")] System.Nullable<int> cardLevel)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), playerID, firstName, lastName, pointsBal, clubStatus, isVIP, cardLevel);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "CMP.usp_UpdatePDDisplayMessage")]
        public int usp_UpdatePDDisplayMessage([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MessageType", DbType = "VarChar(2)")] string messageType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TransactionCode", DbType = "Int")] System.Nullable<int> transactionCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CardNumber", DbType = "Char(10)")] string cardNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DisplayMessage", DbType = "VarChar(MAX)")] string displayMessage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), messageType, transactionCode, cardNumber, displayMessage);
            return ((int)(result.ReturnValue));
        }

        public partial class rsp_GetEmployeeFlagsResult
        {

            private string _EmployeeFlags;

            public rsp_GetEmployeeFlagsResult()
            {
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EmployeeFlags", DbType = "VarChar(500)")]
            public string EmployeeFlags
            {
                get
                {
                    return this._EmployeeFlags;
                }
                set
                {
                    if ((this._EmployeeFlags != value))
                    {
                        this._EmployeeFlags = value;
                    }
                }
            }
        }
    }
}
