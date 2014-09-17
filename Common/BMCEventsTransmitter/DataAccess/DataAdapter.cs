using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using BMC.Common.ExceptionManagement;
using System.Collections;
using System.Data.Linq;
using BMC.EventsTransmitter.Utils;
using BMC.Common.Utilities;
using BMC.Common.Security;
using System.IO;
using BMC.Common.ConfigurationManagement;

namespace BMC.EventsTransmitter.DataAccess
{
   internal class DataAdapter
    {
        string _ExchangeConnectionString;
        public string ExchangeConnectionString {get{return _ExchangeConnectionString;}}
        DataAccessDataContext _ExchangeContext;
        Log Logger = Log.GetInstance(); 
        public DataAdapter()
        {
           _ExchangeConnectionString= GetConnectionString();

           _ExchangeContext = new DataAccessDataContext(_ExchangeConnectionString);
        }
       public Hashtable GetInstallations()
       {
           Hashtable _InstallationtoAssetNo = new Hashtable();
           ISingleResult<rsp_GetInstallationDetails_STMResult> ResultSet = _ExchangeContext.rsp_GetInstallationDetails();
           foreach (rsp_GetInstallationDetails_STMResult oInst in ResultSet)
           {
               _InstallationtoAssetNo.Add(oInst.Installation_No, new GMUINFO() { Asset = oInst.Stock_No, Position = oInst.Bar_Pos_Name });   
           }
           return _InstallationtoAssetNo;
       }

       public Hashtable GetExcludedEvents()
       {
           Hashtable _ExcludedEvents = new Hashtable();
           try
           {
               
               //ISingleResult<rsp_EventTransmitter_GetExcludedEventsResult> ResultSet = _ExchangeContext.rsp_EventTransmitter_GetExcludedEvents();
              
               foreach (string  Events in Settings.AllowedMessages.Split(','))
               {
                   _ExcludedEvents.Add(Events, Events);
               }
           }
           catch(Exception Ex)
           {
               Logger.Error ("DataAdapter","GetExcludedEvents()" , Ex);
           }
           return _ExcludedEvents;
       }

       public Hashtable GetSiteDetails()
       {
           Hashtable _SiteDetails = new Hashtable();
           try
           {
               Logger.Debug("Refreshing Site details:GetSiteDetails");
               ISingleResult<rsp_EventTransmitter_GetSiteDetailsResult> ResultSet = _ExchangeContext.rsp_EventTransmitter_GetSiteDetails();
              
               foreach (rsp_EventTransmitter_GetSiteDetailsResult oInst in ResultSet)
               {
                   _SiteDetails.Add("Area", oInst.sub_company_area_name);
                   _SiteDetails.Add("Company", oInst.Company_name);
                   _SiteDetails.Add("District", oInst.sub_company_District_Name);
                   _SiteDetails.Add("Region", oInst.Sub_Company_Region_Name);
                   _SiteDetails.Add("Sub_Company", oInst.sub_company_Name);
               }

           }
           catch (Exception Ex)
           {
               Logger.Error("DataAdapter", "GetSiteDetails()", Ex);
               _SiteDetails.Clear();
               _SiteDetails.Add("Area", string.Empty);
               _SiteDetails.Add("Company", string.Empty);
               _SiteDetails.Add("District", string.Empty);
               _SiteDetails.Add("Region", string.Empty);
               _SiteDetails.Add("Sub_Company", string.Empty);
           }
           return _SiteDetails;
       }

       public string GetSetting(string SettingName)
       {
           string strvalue = string.Empty;
            _ExchangeContext.rsp_GetSetting(0, SettingName, "",ref strvalue);
            return strvalue;
       }
        private  string  GetConnectionString()
        {
            try
            {
                
                if (!string.IsNullOrEmpty(_ExchangeConnectionString))
                {
                    return _ExchangeConnectionString;
                }

                string strConnectionString=string.Empty;
                if (BMCRegistryHelper.IsExchange())
                {
                    Logger.Debug("DataAdapter", "GetConnectionString", "Reading Exchange connection string ");
                    strConnectionString = DatabaseHelper.GetExchangeConnectionString();
                }
                else
                {
                    Logger.Debug("DataAdapter", "GetConnectionString", "Reading Enterprise connection string ");
                    strConnectionString = DatabaseHelper.GetEnterpriseConnectionString();

                }

                if (strConnectionString.ToUpper().Contains("SERVER"))
                    _ExchangeConnectionString = strConnectionString;
                else
                    throw new Exception("Error Decrypting Registry");

            }
            catch (Exception ex)
            {
                if (ex.Message == "Connectionstring Not Found.")
                    throw ex;
                else
                    ExceptionManager.Publish(ex);
            }
            return _ExchangeConnectionString;
        }
    }
}
