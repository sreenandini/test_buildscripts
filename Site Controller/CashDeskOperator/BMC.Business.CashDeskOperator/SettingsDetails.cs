using System;
using System.Data;
using System.Xml;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace BMC.Business.CashDeskOperator
{
    public class SettingsDetails
    {
        SettingsDataAccess settingsDataAccess = new SettingsDataAccess();

        /// <summary>
        /// Returns the datatable with the list of Settings.
        /// </summary>
        /// <returns>Datatable</returns>        

        public DataSet GetSettingsDetails()
        {
            try
            {
                return settingsDataAccess.GetSettingsDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataSet();
            }
        }

        public string FillSettingsToBeSkipped()
        {
            return settingsDataAccess.FillSettingsToBeSkipped();
        }
    }
}
