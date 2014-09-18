using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using System.Data;

namespace BMC.Business.CashDeskOperator
{
   public  class AFTSettings
    {
        AFTSettingsDataAccess settingsDataAccess = new AFTSettingsDataAccess();

        /// <summary>
        /// Returns the datatable with the list of Settings.
        /// </summary>
        /// <returns>Datatable</returns>        

        public List<Transport.AFTSetting> GetAFTSettingsDetails(int iDenom )
        {
            try
            {
                return settingsDataAccess.GetAFTSettingsDetails(iDenom);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<Transport.AFTSetting>();
            }
        }

        public bool SaveAFTSettings(List<Transport.AFTSetting> lstSettings)
        {
            return settingsDataAccess.SaveAFTSettings(lstSettings);
        }
        public DataTable GetDenoms()
        {
            return settingsDataAccess.GetDenoms();  
        }
    }

   public class AftAssets
   {
       BatchDataAccess aftAssets = new BatchDataAccess(ConnectionStringHelper.ExchangeConnectionString);  

       public List<Transport.AftAssets> GetAFTAssets()
       {
           try
           {
               return aftAssets.GetAFTAssets().ToList();
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               return new List<Transport.AftAssets>();
           }
       }

       public int UpdateAftStatus(int installationNo, int Status)
       {
           try
           {
               return aftAssets.UpdateAftStatus(installationNo, Status);
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
               return 0;
           }
       }
   }

}
