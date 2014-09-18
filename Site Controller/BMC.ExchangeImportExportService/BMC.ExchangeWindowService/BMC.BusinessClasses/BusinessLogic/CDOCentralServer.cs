using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMCIPC;
using BMCIPC.CDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.WMI.Win32;
using BMC.CoreLib.Data;
using Wmi = BMC.CoreLib.WMI.Win32;
using BMC.Common.ExceptionManagement;
using System.Threading;

namespace BMC.BusinessClasses.BusinessLogic
{
    internal class CDOCentralServerImpl : CDOCentralServerBase
    {
        private oCommonUtilities _commonUtilities = oCommonUtilities.CreateInstance();
        private IDictionary<string, ServiceStatusDto> _serviceStatuses = null;
        private SiteLicensingExpiryChecker _siteLicensing = SiteLicensingExpiryChecker.Instance;

        internal CDOCentralServerImpl(IExecutorService executor)
            : base(executor)
        {
            _serviceStatuses = new SortedDictionary<string, ServiceStatusDto>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "Stopped", ServiceStatusDto.Stopped },
                { "StartPending", ServiceStatusDto.StartPending },
                { "StopPending", ServiceStatusDto.StopPending },
                { "Running", ServiceStatusDto.Running },
                { "ContinuePending", ServiceStatusDto.ContinuePending },
                { "PausePending", ServiceStatusDto.PausePending },
                { "Paused", ServiceStatusDto.Paused },
            };
        }

        protected override BMCIPC.FloorStatusDataResponse GetSlotStatus()
        {
            List<FloorStatusData> actualRetResults = null;
            using (InstallationDataContext InstallationDataContext =
                        new InstallationDataContext(_commonUtilities.GetConnectionString()))
            {
                actualRetResults = InstallationDataContext.GetSlotStatus("", -1);
            }
            return new FloorStatusDataResponse()
            {
                Response = actualRetResults
            };
        }

        protected override ExchangeServiceStatusResponse GetServiceStatus()
        {
            ExchangeServiceStatusResponse response = new ExchangeServiceStatusResponse()
            {
                Response = new List<ExchangeServiceStatusDto>(),
            };
            string serviceNames = DataHelper.GetServiceNames();
            if (!serviceNames.IsEmpty())
            {
                string[] services = serviceNames.Split(',');
                var wmiServices = (from s in Wmi.Service.GetInstances().OfType<Service>()
                                   join n in services on s.Name equals n
                                   select s);
                foreach (var wmiService in wmiServices)
                {
                    ExchangeServiceStatusDto dto = new ExchangeServiceStatusDto()
                    {
                        Name = wmiService.DisplayName,
                        Description = wmiService.Description,
                        Status = _serviceStatuses[wmiService.State],
                    };
                    response.Response.Add(dto);
                }
            }

            return response;
        }

        protected override FloorPositionDto GetFloorPositionInternal(int userID)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetFloorPositionInternal");
            FloorPositionDto result = null;

            try
            {
                result = DataHelper.GetFloorPosition(userID);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected override void SaveFloorPositionInternal(FloorPositionDto request)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "SaveFloorPositionInternal");

            try
            {
                if (request != null)
                {
                    foreach (var pair in request)
                    {
                        DataHelper.SaveFloorPosition(pair.Key.BarPosition, pair.Key.UserID, pair.Value.Top, pair.Value.Left);
                        Thread.Sleep(10);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override SiteLicensingDataResponse GetSiteLicensing()
        {
            try
            {
                return _siteLicensing.GetSiteLicenseValidation();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
    }

    public static class CDOCentralServerHostApplication
    {
        private static CDOCentralServerHostFactory _factory = null;
        private static CDOCentralServerConfigStore _store = CDOCentralServerConfigStoreFactory.Store;

        public static void Start(IExecutorService executorService)
        {
            Start(executorService, string.Empty);
        }

        public static void Start(IExecutorService executorService, string exchangeServerAddress)
        {
            ModuleProc PROC = new ModuleProc("CDOCentralServerHostApplication", "Start");
            Log.Info(PROC, "Called");

            try
            {
                if (exchangeServerAddress.IsEmpty())
                {
                    exchangeServerAddress = CDOCentralServerConfigStoreFactory.Store.ExchangeServerAddress;
                }

                if (_factory == null)
                {
                    _factory = new CDOCentralServerHostFactory(executorService,
                        new CDOCentralServerImpl(executorService), exchangeServerAddress);
                    _factory.Start();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static void Stop()
        {
            ModuleProc PROC = new ModuleProc("", "Start");
            Log.Info(PROC, "Called");

            try
            {
                if (_factory != null)
                {
                    _factory.Stop();
                    _factory = null;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static ICDOCentralServer Instance
        {
            get
            {
                return _factory.ServiceInstance;
            }
        }
    }
}
