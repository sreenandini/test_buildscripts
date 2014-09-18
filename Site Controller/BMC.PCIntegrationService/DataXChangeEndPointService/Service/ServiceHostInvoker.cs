using System;
using System.ServiceProcess;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using BMC.Common.Security;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.WcfHelper.Behaviors;
using System.IdentityModel.Selectors;
using BMC.Crypto;
using DataXChangeEndPointService.Data;

namespace DataXChangeEndPointService.Service
{
    
    public class PCServiceBehavior : WcfCustomBehavior
    {
        public PCServiceBehavior() { }

        protected override void OnProcessRequestMessage(ref Message request, IClientChannel channel, InstanceContext instanceContext, OperationContext context)
        {
            base.OnProcessRequestMessage(ref request, channel, instanceContext, context);
        }
    }

    public class PCUserNameValidator : UserNamePasswordValidator
    {
        private static string _userName;
        private static string _passWord;
        private static string _encryptionKey;
        Crypto objCrypto = new Crypto();

        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(_userName))
                {
                    _userName = ConfigurationManager.AppSettings["WsdlLoginName"].ToString();
                }
                return _userName;
            }
        }

        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(_passWord))
                {
                    _passWord = ConfigurationManager.AppSettings["WsdlPassword"].ToString();
                }
                return _passWord;
            }
        }

        public string EncryptionKey
        {
            get
            {
                if (string.IsNullOrEmpty(_encryptionKey))
                {
                    _encryptionKey = ConfigurationManager.AppSettings["EncryptionKey"].ToString();
                }
                return _encryptionKey;
            }
        }

        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) throw new ArgumentNullException("Username or Password is empty");

            if (userName == UserName && password == objCrypto.CustomDecryption(Password, EncryptionKey, Encoding.Unicode))
                return;
                
            throw new ArgumentException("Invalid user name or password.");
        }
    }


    public class ServiceHostInvoker : ServiceBase
    {
        private ServiceHost _host = null;

        public ServiceHostInvoker()
        {
            try
            {
                string hostName = Dns.GetHostName();
                Uri url = new Uri(ConfigurationManager.AppSettings["ServicePath"].ToString()); // "http://" + hostName + ":18080/BMC/Data");

                _host = new ServiceHost(typeof(DataXChangeEndPointImpl), new Uri[] { url });
                _host.Description.Behaviors.Add(new PCServiceBehavior());
                _host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = System.ServiceModel.Security.UserNamePasswordValidationMode.Custom;
                _host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new PCUserNameValidator();

                ServiceDebugBehavior debug = _host.Description.Behaviors.Find<ServiceDebugBehavior>();

                // if not found - add behavior with setting turned on 
                if (debug == null)
                {
                    _host.Description.Behaviors.Add(
                         new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
                }
                else
                {
                    // make sure setting is turned ON
                    if (!debug.IncludeExceptionDetailInFaults)
                    {
                        debug.IncludeExceptionDetailInFaults = true;
                    }
                }

                _host.AddServiceEndpoint(typeof(DataXChangeEndPoint), CreateBasicHttpBinding(), "");

                ServiceMetadataBehavior meta = _host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (meta == null)
                {
                    _host.Description.Behaviors.Add((meta = new ServiceMetadataBehavior()));
                }
                meta.HttpGetEnabled = true;
                meta.HttpGetUrl = new Uri(url.AbsoluteUri);
                _host.Opened += _host_Opened;
            }
            catch (FaultException ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void _host_Opened(object sender, EventArgs e)
        {
            ServiceHostBase host = sender as ServiceHostBase;
            foreach (var cd in host.ChannelDispatchers)
            {
                LogManager.WriteLog("Listen Uri : " + cd.Listener.Uri.AbsoluteUri, LogManager.enumLogLevel.Info);
                //Console.WriteLine("Listen Uri : " + cd.Listener.Uri.AbsoluteUri);
            }
        }

        private static BasicHttpBinding CreateBasicHttpBinding()
        {
            BasicHttpBinding bindingHttp = new BasicHttpBinding()
            {
                OpenTimeout = new TimeSpan(0, 10, 0),
                CloseTimeout = new TimeSpan(0, 10, 0),
                SendTimeout = new TimeSpan(0, 10, 0),
                ReceiveTimeout = new TimeSpan(0, 10, 0),
            };
            bindingHttp.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;

             //transport
            bindingHttp.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            bindingHttp.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            bindingHttp.Security.Transport.Realm = string.Empty;

            // message
            bindingHttp.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;
            bindingHttp.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;

            return bindingHttp;
        }

        protected override void OnStart(string[] args)
        {
            LogManager.WriteLog("Starting Service...", LogManager.enumLogLevel.Info);
            _host.Open();
            DMMessageHandler.Initialize();

            if (ResponseDataAccess.IsPCEnabled)
            {
                MessageHandler.Initialize();
            }
        }

        protected override void OnStop()
        {
            LogManager.WriteLog("Stopping Service...", LogManager.enumLogLevel.Info);
            _host.Close();
            DMMessageHandler.StopProcess();

            if (ResponseDataAccess.IsPCEnabled)
            {
                MessageHandler.StopProcess();
            }
        }

        //public void Start()
        //{
        //    Console.WriteLine("Starting Service...", LogManager.enumLogLevel.Info);
        //    //_bRunThread = true;
        //    //_processThread = new Thread(ProcessMessages);
        //    _host.Open();
        //    DMMessageHandler.Initialize();
        //    if (ResponseDataAccess.IsPCEnabled)
        //    {
        //        MessageHandler.Initialize();
        //    }
        //}

        //public void Stop()
        //{
        //    Console.WriteLine("Stopping Service...", LogManager.enumLogLevel.Info);
        //    //_bRunThread = false;
        //    _host.Close();
        //    DMMessageHandler.StopProcess();
        //    if (ResponseDataAccess.IsPCEnabled)
        //    {
        //        MessageHandler.StopProcess();
        //    }
        //}

    }
}
