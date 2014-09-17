using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExMonitor.Server.Handlers.Meter;
using OfficeOpenXml;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.Common.Persistence;

namespace BMC.ExMonitor.Server.Handlers
{
    public sealed class MonitorExecutionContext
        : DisposableObject,
        IExecutorKeyFreeThread
    {
        private Lazy<List<MonitorEntity_MsgTgt>> _h2gTargets = null;

        public MonitorExecutionContext()
        {
            _h2gTargets = new Lazy<List<MonitorEntity_MsgTgt>>(() =>
            {
                return new List<MonitorEntity_MsgTgt>();
            });
        }

        public MonitorExecutionContext(MonMsg_G2H sourceMessage)
            : this()
        {
            this.G2HMessage = sourceMessage;
        }

        public MonMsg_G2H G2HMessage { get; private set; }

        public MonMsg_H2G H2GMessage { get; set; }

        public MonitorEntity_MsgTgt G2HTarget { get; set; }

        public List<MonitorEntity_MsgTgt> H2GTargets
        {
            get { return _h2gTargets.Value; }
        }

        //public FF_FlowDirection FlowDirection { get; private set; }

        public string UniqueKey
        {
            get { return string.Empty; }
        }
    }

    internal interface IMonitorHandler
        : IMonitorProcessor
    {
        ExMonitorServerImpl MonitorServer { get; }
        bool IsExecuting { get; }
        bool Execute(MonitorExecutionContext context, MonitorEntity_MsgTgt target);
        void ForceMeterRead(MonitorExecutionContext context, MonitorEntity_MsgTgt target);
    }

    internal abstract class MonitorHandlerBase
        : DisposableObject, IMonitorHandler
    {
        protected readonly IExMonitorServerConfigStore _configStore = ExMonitorServerConfigStoreFactory.Store;
        protected static readonly IConfig_ExchangeServer _configExchange = null;
        protected ExMonitorServerImpl _monitorServer =
            ExCommsServerHostFactoryActivatorFactory.Current.MonitorServer as ExMonitorServerImpl;

        private bool _isExecuting = false;

        static MonitorHandlerBase()
        {
            _configExchange = ConfigApplicationFactory.GetAny<IConfig_ExchangeServer>();
        }

        public ExMonitorServerImpl MonitorServer
        {
            get { return _monitorServer; }
        }

        public bool IsExecuting
        {
            get { return _isExecuting; }
        }

        public bool ProcessG2HMessage(MonMsg_G2H request)
        {
            bool result = false;

            try
            {
                // force meters add
                //this.ForceMeterRead(request);

                // processing
                result = this.ProcessG2HMessageInternal(request);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }

            return result;
        }

        protected virtual void OnForceMeterRead(MonitorExecutionContext context, MonitorEntity_MsgTgt target) { }

        public virtual void ForceMeterRead(MonitorExecutionContext context, MonitorEntity_MsgTgt target) { }

        protected virtual bool ProcessG2HMessageInternal(MonMsg_G2H request) { return true; }

        public virtual bool ProcessH2GMessage(MonMsg_H2G response)
        {
            bool result = false;

            try
            {
                result = _monitorServer._transceiver.ProcessH2GMessage(response);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }

            return result;
        }

        protected virtual bool ProcessH2GMessageInternal(MonMsg_H2G request)
        {
            return _monitorServer._transceiver.ProcessH2GMessage(request);
        }

        protected EPIManager CurrentEPIManager
        {
            get { return EPIManager.Current; }
        }

        protected EPIMsgProcessor CurrentEPIMsgProcessor
        {
            get { return EPIMsgProcessor.Current; }
        }

        protected ExCommsDataContext CurrentDataContext
        {
            get { return ExCommsDataContext.Current; }
        }

        public override string ToString()
        {
            return this.GetHashCode().ToString();
        }

        public bool Execute(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Execute"))
            {
                bool result = default(bool);
                string key = (target != null ? target.TypeFaultSourceTypeKey : string.Empty);

                try
                {
                    if (!key.IsEmpty()) method.Info("!&! HANDLER STARTED FOR : " + key);

                    // force meters add
                    this.OnForceMeterRead(context, target);

                    // execute the target
                    result = this.OnExecuteInternal(context, target);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    if (!key.IsEmpty()) method.Info("!&! HANDLER COMPLETED FOR : " + key);
                }

                return result;
            }
        }

        protected virtual bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return true;
        }

        protected bool AddFaultEvent(int installationNo, int faultSource, int faultType,
            string faultDescription, bool polled, DateTime eventDate)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "AddFaultEvent"))
            {
                bool retVal = true;

                try
                {
                    retVal = ExCommsDataContext.Current.AddFaultEvent(installationNo, faultSource, faultType, faultDescription, polled, eventDate);
                }
                catch (Exception ex)
                {
                    retVal = false;
                    Log.Exception(ex);
                }
                return retVal;
            }
        }

        protected virtual bool AddFaultEvent(MonitorExecutionContext context, MonitorEntity_MsgTgt target, string description, bool polled)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "AddFaultEvent"))
            {
                bool result = default(bool);

                try
                {
                    MonMsg_G2H request = context.G2HMessage;
                    method.InfoV("FAULT EVENT : Inserting fault event for {0:D}/{1:D}", target.FaultSource, target.FaultType);
                    result = this.AddFaultEvent(request.InstallationNo, target.FaultSource, target.FaultType,
                        description, polled, request.FaultDate);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
    }

    internal abstract class MonitorHandlerBase_Meter
        : MonitorHandlerBase
    {
        private bool _forceMeterRead = false;

        #region Single Thread Helper (MeterHandler)

        private static readonly SingletonThreadHelper<IMonitorHandler> _meterHandlerHelper = new SingletonThreadHelper<IMonitorHandler>(
                                    new Lazy<IMonitorHandler>(() => new MonitorHandler_Meter()));

        public IMonitorHandler MeterHandler
        {
            get { return _meterHandlerHelper.Current; }
        }

        #endregion

        internal MonitorHandlerBase_Meter() { }

        internal MonitorHandlerBase_Meter(bool forceMeterRead)
        {
            _forceMeterRead = forceMeterRead;
        }

        protected bool ProcessMeters(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return this.MeterHandler.Execute(context, target);
        }

        protected override void OnForceMeterRead(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            string key = target.FaultSourceTypeKey;
            if (_forceMeterRead ||
                _configStore.ForceMeterReads.ContainsKey(key))
            {
                this.ForceMeterRead(context, target);
            }
        }

        public virtual void ForceMeterRead(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            Log.Info("ForceMeterRead : " + target.TypeFaultSourceTypeKey);
            this.ProcessMeters(context, target);
        }

        internal EPIMeterValueDictionary ForceMeterReadAndGetLatest(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            this.ForceMeterRead(context, target);
            EPIMeterValueDictionary result = new EPIMeterValueDictionary();
            result.GetLatestMeters(context.G2HMessage.InstallationNo, EPIMeterValueTypes.Start);
            return result;
        }

        internal void ForceMeterReadAndGetLatest(MonitorExecutionContext context, MonitorEntity_MsgTgt target,
            Action<int, int, int, int> action)
        {
            int installationNo = context.G2HMessage.InstallationNo;
            EPIMeterValueTypes valueType = EPIMeterValueTypes.Start;

            using (EPIMeterValueDictionary meterValues = this.ForceMeterReadAndGetLatest(context, target)) 
            {
                action(installationNo,
                    (int)meterValues[EPIMeterTypes.Handpay][valueType],
                    (int)meterValues[EPIMeterTypes.Jackpot][valueType],
                    (int)meterValues[EPIMeterTypes.VouchersOut][valueType]);
            }
        }

        internal void SetTicketRequestMeters(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            this.ForceMeterReadAndGetLatest(context, target, (i, h, j, v) =>
            {
                ExCommsDataContext.Current.UpdateTicketExceptionRequestMeters(i, h, j, v);
            });
        }

        internal void SetTicketResponseMeters(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            this.ForceMeterReadAndGetLatest(context, target, (i, h, j, v) =>
            {
                ExCommsDataContext.Current.UpdateTicketExceptionResponseMeters(i, h, j, v);
            });
        }
    }

    internal abstract class MonitorHandlerBase_H2G
        : MonitorHandlerBase
    {
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            return false;
        }
    }
}
