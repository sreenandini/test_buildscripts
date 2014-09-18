using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Business.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class SettingsBusinessObject : ISettingsDetails
    {
       #region Private Variables

        SettingsDetails settings = new SettingsDetails();

        #endregion

       #region Constructor

        private SettingsBusinessObject() { }

        #endregion

       #region Public Functions

        public DataSet GetSettingDetails()
        {
            return settings.GetSettingsDetails();
        }

        public string FillSettingsToBeSkipped()
        {
            return settings.FillSettingsToBeSkipped();
        }

        #endregion

       #region Public Static Function
        public static ISettingsDetails CreateInstance()
        {
            return new SettingsBusinessObject();
        }
        #endregion
    }
}
