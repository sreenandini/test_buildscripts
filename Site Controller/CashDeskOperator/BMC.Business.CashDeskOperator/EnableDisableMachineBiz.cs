using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Transport;
using BMC.DBInterface.CashDeskOperator;
using System.Data.Linq;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;


namespace BMC.Business.CashDeskOperator
{
    public class EnableDisableMachineBiz
    {
        private static EnableDisableMachineDataAccess _oEnableDisableDA = new EnableDisableMachineDataAccess(CommonDataAccess.ExchangeConnectionString);
        private static EnableDisableMachineBiz _ObjBiz;
     
        public static EnableDisableMachineBiz CreateInstance()
        {
            if (_ObjBiz == null)
                _ObjBiz = new EnableDisableMachineBiz();

            return _ObjBiz;
        }

        public List<EnableDisableMachine> GetActiveMachine()
        {
            List<EnableDisableMachine> lstReturnValue = null;
            ISingleResult<rsp_GetActiveMachinesResult> result = null;

            try
            {
                result = _oEnableDisableDA.GetActiveMachines();

                lstReturnValue = (from obj in result
                                  select new EnableDisableMachine
                                  {
                                      StockNo=obj.StockNo,
                                      BarPosNumber=obj.BarPosNumber,
                                      IsSelected= true
                                     
                                  }
                                  ).ToList();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return lstReturnValue;
        }

        public void UpdateBarPositionMachine(string BarPosition ,bool IsMachine,bool Status,int ExportHistory)
        {
            try
            {
                _oEnableDisableDA.usp_UpdateBarPositionForMachineControl(BarPosition, IsMachine, Status, ExportHistory);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
