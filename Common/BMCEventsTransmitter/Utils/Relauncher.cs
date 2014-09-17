using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading;
  
namespace BMC.EventsTransmitter.Utils
{
    
    public interface IRelaucher
    {
        void RefreshApp();
  
    }
    
    public delegate void Del_Relaunch();

    public class Relauncher : MarshalByRefObject, IRelaucher
    {
        Log Logger = Log.GetInstance(); 
        static FileSystemWatcher ConfigWatcher;
        //public event Del_Relaunch OnRefresh;
        List<IRelaucher> oAppList = new List<IRelaucher>();
        static Relauncher _Launcher=null;
        static object _lock=new object();
        Object _RefLock = new object();  //wait till complete refresh
        
        private Relauncher()
        {
            try
            {
                ConfigWatcher = new FileSystemWatcher(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "*.Config");
                ConfigWatcher.NotifyFilter = NotifyFilters.LastWrite;                   
                ConfigWatcher.Changed += new FileSystemEventHandler(FileChanged);
                ConfigWatcher.Created += new FileSystemEventHandler(FileChanged);
                ConfigWatcher.EnableRaisingEvents = true; 
                
            }
            catch (Exception Ex)
            {
                Logger.Error("Relauncher", "Constructor", Ex  ); 
            }
        }

        void ConfigWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            RefreshApp(); 
        }

        void FileChanged(object sender, FileSystemEventArgs e)
        {
            RefreshApp(); 
        }
        public static Relauncher GetInstance()
        {
            lock (_lock)
            {
                if (_Launcher == null)
                {
                    _Launcher = new Relauncher();
                }
            }
            return _Launcher;
        }

        public  void RegisterForUpdate(IRelaucher App)
        {
            try
            {
                oAppList.Add(App);
            }
            catch (Exception Ex)
            {
                Logger.Error("Relauncher", "RegisterForUpdate", Ex);
            }
        }
        public void RefreshApp()
        {
            try
            {
                lock (_RefLock)
                {
                    while (true)
                    {
                        try
                        {
                            ConfigurationManager.RefreshSection("appSettings");
                        }
                        catch //In case of refresh error 
                        {
                            Thread.Sleep(10); 
                            continue; 
                        } 
                        break; // on succesfull refresh 
                    }
                    //reload all subscribers
                    if (ConfigurationManager.AppSettings["AppRelauncher_Enable"].NulltoString("0") != "0")
                    {
                        foreach (IRelaucher app in oAppList)
                        {
                            app.RefreshApp();
                        }
                    }
                }
            }
            catch(Exception Ex)
            {
                Logger.Error("Relauncher", "RefreshApp", Ex);
            }
        }
    }
}
