using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMCIPC;
using System.Threading;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;

namespace BMC.BusinessClasses.BusinessLogic
{
    public class FloorStatus
    {
        #region Reference Type
        static FloorStatus _objFloorStatus = null;        
        Server _oServer = null;
        ManualResetEvent mre_isStoped = null;
        int iInterval = 5000;
        private bool _updateFloorStatusOld = false;
        private IExecutorService _exec = null;
        #endregion Refernce Type

        public static FloorStatus GetInstance()
        {
            if (_objFloorStatus == null)
                _objFloorStatus = new FloorStatus();

            return _objFloorStatus;
        }

        private FloorStatus()
        {
            try
            {
                _updateFloorStatusOld = Extensions.GetAppSettingValueBool("UpdateFloorStatusOld", false);
                if (_updateFloorStatusOld)
                {
                    _oServer = IPCServer.GetInstance(ConfigManager.Read("RemotingServer"),
                    Convert.ToInt32(ConfigManager.Read("RemotingServerPort")));

                    int itmpInterval = Convert.ToInt32(ConfigManager.Read("RemotingServerInterval")) * 1000;

                    if (itmpInterval > 5000)
                    {
                        iInterval = itmpInterval;
                    }

                    mre_isStoped = new ManualResetEvent(false);
                    Thread th = new Thread(UpdateFloorStatus);
                    th.Start();
                }
                else
                {
                    _exec = ExecutorServiceFactory.CreateExecutorService();
                    CDOCentralServerHostApplication.Start(_exec);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("FloorStatus : Unable to initialize the reference type." + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        void UpdateFloorStatus()
        {
            LogManager.WriteLog("FloorStatus : Floor status updation initiated...", LogManager.enumLogLevel.Info);

            while (!mre_isStoped.WaitOne(iInterval))
            {
                try
                {
                    LogManager.WriteLog("FloorStatus : Updating floor status...", LogManager.enumLogLevel.Info);

                    if (_oServer.CheckClientCounts())
                    {
                        LogManager.WriteLog("Inside UpdateFloorStatus()", LogManager.enumLogLevel.Info);
                    InstallationDataContext InstallationDataContext =
                        new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());

                    List<FloorStatusData> actualRetResults =
                        InstallationDataContext.GetSlotStatus("", -1);

                    _oServer.SendToClients(actualRetResults);
                    }
                    else
                    {
                        LogManager.WriteLog("No Clients", LogManager.enumLogLevel.Info);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }
        }

        public void Stop()
        {            
            try
            {
                if (_updateFloorStatusOld)
                {
                    _oServer.StopServerSession();
                    mre_isStoped.Set();
                }
                else
                {
                    CDOCentralServerHostApplication.Stop();
                }
            }
            catch
            {
            }
            LogManager.WriteLog("FloorStatus : Floor status updation is stopped", LogManager.enumLogLevel.Info);
        }
    }

    public class IPCServer
    {
        private static Server _IPCServer;
        private static string _ServerName;
        private static int _port;

        public static Server GetInstance(string ServerName , int Port)
        {
            try
            {
                _ServerName = ServerName;
                _port = Port;
                if (_IPCServer == null)
                {
                    LogManager.WriteLog("IPCServer : Initiating Remote object", LogManager.enumLogLevel.Info);
                    _IPCServer = new Server(_ServerName);
                    _IPCServer.StartServerSession(_port);
                }
            }
            catch(Exception ex)
            {
                LogManager.WriteLog("IPCServer : Error in initiating Remote object" + ex.Message, LogManager.enumLogLevel.Error);
            }

            return _IPCServer;
        }

        public static Server GetInstance()
        {
            try
            {
                if (_IPCServer == null)
                {
                    LogManager.WriteLog("IPCServer : Initiating Remote object", LogManager.enumLogLevel.Info);
                    _IPCServer = new Server(_ServerName);
                    _IPCServer.StartServerSession(_port);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("IPCServer : Error in initiating Remote object" + ex.Message, LogManager.enumLogLevel.Error);
            }

            return _IPCServer;
        }

        public IPCServer()
        {
        }
    }
}