using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.DBInterface.CashDeskOperator;

namespace BMC.Business.CashDeskOperator
{
    public class Drop
    {
        DropDataAccess dropDataAccess = new DropDataAccess();

        public DataTable GetCurrentMachines()
        {
            return dropDataAccess.GetCurrentMachines();
        }
       
        public DataTable GetMeterList(string display, int record_No, int hour_No)
        {
            return dropDataAccess.GetMeterList(display, record_No, hour_No);
        }


        public bool GetSGVISetting()
        {
            return dropDataAccess.GetSGVISetting();
        }
    }
}
