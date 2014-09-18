using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using BMC.DataAccess;
using System.Reflection;

namespace BMC.DBInterface.CashDeskOperator
{
   public  partial class EnableDisableMachineDataAccess : DataContext
    {
       static MappingSource mappingSource = new AttributeMappingSource();
       #region Extensibility Method Definitions
       partial void OnCreated();
       #endregion

       public EnableDisableMachineDataAccess(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
        }

       [Function(Name = "dbo.rsp_GetActiveMachines")]
       public ISingleResult<rsp_GetActiveMachinesResult> GetActiveMachines()
       {
           IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
           return ((ISingleResult<rsp_GetActiveMachinesResult>)(result.ReturnValue));
       }

       [Function(Name = "dbo.usp_UpdateBarPositionForMachineControl")]
       public int usp_UpdateBarPositionForMachineControl([Parameter(Name = "BarPosNo", DbType = "VarChar(30)")] string barPosNo, [Parameter(DbType = "Bit")] System.Nullable<bool> isMachine, [Parameter(Name = "Status", DbType = "Bit")] System.Nullable<bool> status, [Parameter(DbType = "Int")] System.Nullable<int> exportHistory)
       {
           IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPosNo, isMachine, status, exportHistory);
           return ((int)(result.ReturnValue));
       }
    }

   public partial class rsp_GetActiveMachinesResult
   {

       private string _StockNo;

       private string _BarPosNumber;

       public rsp_GetActiveMachinesResult()
       {
       }

       [Column(Storage = "_StockNo", DbType = "VarChar(50)")]
       public string StockNo
       {
           get
           {
               return this._StockNo;
           }
           set
           {
               if ((this._StockNo != value))
               {
                   this._StockNo = value;
               }
           }
       }

       [Column(Storage = "_BarPosNumber", DbType = "VarChar(50)")]
       public string BarPosNumber
       {
           get
           {
               return this._BarPosNumber;
           }
           set
           {
               if ((this._BarPosNumber != value))
               {
                   this._BarPosNumber = value;
               }
           }
       }
   }
}
