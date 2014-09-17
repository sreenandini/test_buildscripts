using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EventsTransmitter.DataAccess;
using System.Collections;

namespace BMC.EventsTransmitter.Utils
{
    public class GMUINFO
    {
        public string Asset { get; set; }
        public string Position { get; set; }
    }
    internal class Installations : IRelaucher
    {
        Log Logger = Log.GetInstance(); 
       DataAdapter _DataAdapter = null;
       static Installations _Instance;
       static object _obj = new object(); // Lock
       Hashtable _InstallationtoAsset;

       private Installations()
       {
           try
           {
               _DataAdapter = new DataAdapter();
               Logger.Debug("Installations", "CONSTRUCTOR" , "Loading....");
               _InstallationtoAsset = _DataAdapter.GetInstallations();
               Logger.Debug("Installations", "CONSTRUCTOR", "Loading Complete.");
           }
           catch
           {
               Logger.Error("Installations", "CONSTRUCTOR", "Error Getting Installation Details:");
               throw;
           }
       }
       void RefreshList()
       {
           try
           {
               _InstallationtoAsset = _DataAdapter.GetInstallations();
           }
           catch
           {
               Logger.Error("Installations","RefreshList", "Error refreshing Installation Details:");
               throw;
           }
       }
       public static Installations GetInstance()
       {
           lock (_obj)
           {
               if (_Instance == null)
               {
                   _Instance = new Installations();
                   Relauncher.GetInstance().RegisterForUpdate(_Instance);    
               }
           }
           return _Instance;
       }
       GMUINFO GetStockNumber(int InstallationNo)
       {
           if (_InstallationtoAsset.ContainsKey(InstallationNo))
           {
               return (GMUINFO)_InstallationtoAsset[InstallationNo];
           }
           else
           {
               RefreshList();
               if (_InstallationtoAsset.ContainsKey(InstallationNo))
               {
                   return (GMUINFO)_InstallationtoAsset[InstallationNo];
               }
               else
               {
                   Logger.Warning("Installations","GetStockNumber", string.Format("Installation Number[{0}] not found ",InstallationNo.ToString()));
                   return new GMUINFO(){Asset=string.Empty,Position=string.Empty} ;
               }
           }
       }
        public GMUINFO this[int InstallationNo]
        {
             get {return this.GetStockNumber(InstallationNo);}

        }

       
        public void RefreshApp()
        {
            Logger.Debug("Installations", "RefreshApp", "Refreshing Installations");
            _InstallationtoAsset = null;
            _InstallationtoAsset = _DataAdapter.GetInstallations();// this line of code Can be removed 
        }

       
    }
}
