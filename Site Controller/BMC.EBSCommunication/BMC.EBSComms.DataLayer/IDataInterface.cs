using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.Utilities;
using BMC.CoreLib;
using BMC.CoreLib.IoC;
using BMC.EBSComms.DataLayer.Dto;
using System.Data;

namespace BMC.EBSComms.DataLayer
{
    public interface IDataInterface : IDisposable
    {
        DLSiteCollectionDto GetSites(string siteCode);
        DLManufacturerCollectionDto GetManufacturers(string siteCode, int manufacturerId);
        DLDenominationCollectionDto GetDenominations(string siteCode, object denominationId);
        DLGameCollectionDto GetGames(string siteCode, object gameId);
        DLMachineCollectionDto GetMachines(string siteCode, object machineID);
        DLZoneCollectionDto GetZones(string siteCode, int zoneId);

 	    DataTable GetUnprocessedRecords();
        bool UpdateRecordStatus(int ID, int Status);
        bool UpdateMessageHistory(bool logMessage, int fromSystem, int toSystem, string siteCode, 
                                  DateTime dateTime, int refID, string request, string response);

        DLSettingDto GetSettings();
        T GetSettingValue<T>(string settingName, T defaultValue);
        bool UpdateSettingValue(string settingName, string settingValue);
    }

 	public abstract partial class DataInterfaceBase : DisposableObject, IDataInterface
    {
        protected string _connectionString = string.Empty;

        protected DataInterfaceBase() { }
    }

    public sealed partial class CommonDataInterface : DataInterfaceBase
    {
        public CommonDataInterface()
        {
            if (BMCRegistryHelper.ActiveInstallationType == BMCCategorizedInstallationTypes.Enterprise)
            {
                _connectionString = DatabaseHelper.GetEnterpriseConnectionString();
            }
            else
            {
                _connectionString = DatabaseHelper.GetExchangeConnectionString();
            }
        }
    }

    public static class DataInterfaceFactory
    {
        private static IDataInterface _object = null;
        private static object _lock = new object();

        static DataInterfaceFactory() { }

        public static IDataInterface GetInterface()
        {
            if (_object == null)
            {
                lock (_lock)
                {
                    if (_object == null)
                    {
                        try
                        {
                        	_object = MEFHelper.GetExportedValue<IDataInterface>("DataInterface");
                    	}
                        catch (Exception)
                        {
                            _object = new CommonDataInterface();
                		}
           			 }
                }
            }
            return _object;
        }
    }
}
