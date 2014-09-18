using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Services;
using BMC.CoreLib.WcfHelper.Behaviors;
using BMC.CoreLib.WcfHelper.Helpers;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.CoreLib.Xml;
using BMC.EBSComms.Contracts.Dto;
using BMC.EBSComms.Contracts.Interfaces.SMS2EBS;

namespace BMC2EBSServer
{
    class Program
    {
        private EBSCommClientHostFactory _factory = null;

        public Program()
        {
            Log.WriteToExternalLog += Log_WriteToExternalLog;
            _factory = new EBSCommClientHostFactory(ExecutorServiceFactory.CreateExecutorService());
        }

        void Log_WriteToExternalLog(string formattedMessage, BMC.CoreLib.Diagnostics.LogEntryType type, object extra)
        {
            Console.WriteLine(formattedMessage);
        }

        public void OnStart()
        {
            _factory.Start();
        }

        public void OnStop()
        {
            _factory.Stop();
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.OnStart();

            Console.WriteLine("End?");
            Console.ReadLine();
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class EBSCommClient : ListenerBase, IEBSCommClient_13_1
    {
        public EBSCommClient(IExecutorService executorService)
            : base(executorService)
        {
            this.Intialize();
        }

        protected override bool StartInternal()
        {
            return false;
        }

        protected override bool StopInternal()
        {
            return false;
        }

        public BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationSoapOut S2SMessagePostOperation(BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationSoapIn request)
        {
            ModuleProc PROC = new ModuleProc("", "Method");

            try
            {
                Console.WriteLine("Received : " + request.Request.request);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return new BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationSoapOut()
             {
                 Response = new BMC.EBSComms.Contracts.Dto.SMS2EBS.ResponseType_13_0()
                     {
                         response = "success",
                     },
             };
        }

        public BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationResponse S2SMessagePostOperation(BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationRequest request)
        {
            ModuleProc PROC = new ModuleProc("", "Method");

            try
            {
                Console.WriteLine("Received : " + request.Request.Request);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            s2sMessage msg = new s2sMessage();
            s2sHeader hdr = new s2sHeader()
            {
                fromSystem = "EBS",
                toSystem = "SDS",
                dateTimeSent = DateTime.Now,
            };
            s2sBody bdy = new s2sBody()
            {
                Items = new object[] { 
                    new infoUpdate() {
                        propertyId="1313",
                        Item = new infoUpdateDataAck() {}
                    },
                },
            };
            msg.Items = new object[] { hdr, bdy };
            string resp = XmlSerializerHelper.ConvertObjectToXml(msg, null, false, false);

            return new BMC.EBSComms.Contracts.Messages.SMS2EBS.S2SMessagePostOperationResponse
            {
                Response = new BMC.EBSComms.Contracts.Dto.SMS2EBS.ResponseType_13_1()
                  {
                      Response = resp,
                  }
            };
        }
    }

    public sealed class EBSCommClientFactory : WcfExecutorServiceFactoryBase
    {
        public override Type ServiceType
        {
            get { return typeof(EBSCommClient); }
        }

        public override object OnCreateSingletonInstance(IExecutorService executorService)
        {
            return new EBSCommClient(executorService);
        }

        public override Type[] KnownTypes
        {
            get
            {
                return null;
            }
        }
    }

    public class EBSCommClientHostFactory : WcfExecutorServiceHostFactory
    {
        public EBSCommClientHostFactory(IExecutorService executorService)
            : base(executorService, "S2SServer",
            0,
            5031,
            0) { }

        protected override IWcfExecutorServiceFactory OnCreateExecutorService()
        {
            return new EBSCommClientFactory();
        }

        protected override WcfServiceHostBase OnCreateServiceHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
        {
            return new EBSCommClientHost(singletonInstance, knownTypes, baseAddresses);
        }

        protected override Binding OnCreateHttpBinding()
        {
            Binding binding = ContractBindingsHelper.CreateBasicHttpBinding() as Binding;
            //binding.Name = "IS2S";
            //binding.Namespace = "http://tempuri.org/";
            return binding;
        }
    }

    public class EBSCommClientBehavior : WcfCustomBehavior
    {
        public EBSCommClientBehavior() { }

        public override bool LogRequestMessage
        {
            get
            {
                return true;
            }
            set
            {
                //EBSCommClientConfigStoreFactory.Store.LogIncomingMessages = value;
            }
        }

        public override bool LogResponseMessage
        {
            get
            {
                return true;
            }
            set
            {
                //EBSCommClientConfigStoreFactory.Store.LogOutgoingMessages = value;
            }
        }
    }

    public sealed class EBSCommClientHost : WcfExecutorServiceHost
    {
        public EBSCommClientHost(Type serviceType, Type[] knownTypes, params Uri[] baseAddresses)
            : base(serviceType, knownTypes, baseAddresses) { }

        public EBSCommClientHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
            : base(singletonInstance, knownTypes, baseAddresses) { }

        protected override WcfCustomBehaviorBase CreateCustomBehavior()
        {
            return new EBSCommClientBehavior();
        }
    }

}
