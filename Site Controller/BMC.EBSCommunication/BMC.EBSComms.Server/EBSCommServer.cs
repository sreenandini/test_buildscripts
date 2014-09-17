using System;
using System.ServiceModel;
using System.Xml.Serialization;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Services;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.EBSComms.Contracts.Configuration;
using BMC.EBSComms.Contracts.Dto;
using BMC.EBSComms.Contracts.Interfaces.EBS2SMS;
using BMC.EBSComms.DataLayer;
using BMC.EBSComms.DataLayer.Dto;

namespace BMC.EBSComms.Server
{
    [ServiceBehavior(Namespace = "BMC.EBSComms.Server",//"http://www.gamingstandards.com/s2s/wsdl/v1.1/S2SMessageService.wsdl",
        Name = "EBSCommServer",
        InstanceContextMode = InstanceContextMode.Single)]
    public partial class EBSCommServer : ListenerBase, IEBSCommServer, IEBSCommServer_13_1
    {
        private bool IS_EBS_13_0 = false;

        private IDataInterface _di = null;
        private XmlSerializerNamespaces _ns = new XmlSerializerNamespaces();
        private IEBSCommServerConfigStore _configStore = EBSCommServerConfigStoreFactory.Store;

        public EBSCommServer(IExecutorService executorService)
            : base(executorService)
        {
            this.Intialize();
        }

        protected override void InitializeInternal()
        {
            base.InitializeInternal();
            this.FindEBSVersion();
            this.InitWorkHandlers();
            _di = DataInterfaceFactory.GetInterface();
            _ns.Add("gsa", s2sHelper.NS_GSA);
            _ns.Add("bS2S", s2sHelper.NS_BS2S);
            InitializeSendDataToEBSThread();
        }

        private void FindEBSVersion()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "FindEBSVersion");

            try
            {
                DLSettingDto entity = DataInterfaceFactory.GetInterface().GetSettings();
                if (entity != null)
                {
                    IS_EBS_13_0 = entity.EBSVersion.IgnoreCaseCompare("13.0");
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override bool StartInternal()
        {
            return false;
        }

        protected override bool StopInternal()
        {
            return false;
        }
    }

    public sealed class EBSCommServerFactory : WcfExecutorServiceFactoryBase
    {
        public override Type ServiceType
        {
            get { return typeof(EBSCommServer); }
        }

        public override object OnCreateSingletonInstance(IExecutorService executorService)
        {
            return new EBSCommServer(executorService);
        }

        public override Type[] KnownTypes
        {
            get
            {
                return null;
            }
        }
    }

    internal static class EBSCommServerExtensions
    {
        internal static int s2sId(this string id)
        {
            return (id.IgnoreCaseCompare("S2S_all") ? 0 : id.ConvertToInt32());
        }

        internal static float s2sfloatId(this string id)
        {
            return (id.IgnoreCaseCompare("S2S_all") ? 0 : TypeSystem.GetValueSingle(id));
        }

        internal static object s2sStringId(this string id)
        {
            if (id.IgnoreCaseCompare("S2S_all")) return DBNull.Value;
            else return id;
        }
    }

    internal enum EBSSystemNames
    {
        BMC = 0,
        EBS = 1,
    }
}
