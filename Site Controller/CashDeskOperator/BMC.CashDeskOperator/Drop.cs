using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Business.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class DropBusinessObject: IDrop
    {
        #region "Private variables"

        Drop drop = new Drop();

        #endregion

        #region Constructor

        private DropBusinessObject() { }
        
        #endregion

        #region Public Static Function
        public static IDrop CreateInstance()
        {
            return new DropBusinessObject();
        }
        #endregion

        #region "Public Functions"

        public DataTable GetCurrentMachines()
        {
            return drop.GetCurrentMachines();
        }

        public DataTable GetMeterList(string display, int record_No, int hour_No)
        {
            return drop.GetMeterList(display, record_No, hour_No);
        }

        #endregion

        #region IDrop Members


        public bool GetSGVISetting()
        {
            return drop.GetSGVISetting();
        }

        #endregion
    }
}
