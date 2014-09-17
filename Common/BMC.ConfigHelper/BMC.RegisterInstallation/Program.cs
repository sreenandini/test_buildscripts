using BMC.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using System.IO;

namespace BMC.RegisterInstallation
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                Console.WriteLine("Invalid arguments passed.");
                return;
            }
            string getEnv = string.Empty;
            getEnv = Environment.GetEnvironmentVariable("BMCConfigPath", EnvironmentVariableTarget.Machine);
            Console.WriteLine("getEnv :" + getEnv);

            if (string.IsNullOrWhiteSpace(getEnv))
            {
                getEnv = Path.GetFullPath(Path.Combine(Extensions.GetStartupDirectory(), ".."));
                Environment.SetEnvironmentVariable("BMCConfigPath", getEnv, EnvironmentVariableTarget.Machine);
            }

            IConfigApplication app = null;
            switch (args[0].ToLower())
            {
                case "exchangeserver":
                    app = ConfigApplicationFactory.Get<IConfig_ExchangeServer>();
                    break;

                case "exchangeclient":
                    app = ConfigApplicationFactory.Get<IConfig_ExchangeClient>();
                    break;

                case "enterpriseserver":
                    app = ConfigApplicationFactory.Get<IConfig_EnterpriseServer>();
                    break;

                case "enterpriseclient":
                    app = ConfigApplicationFactory.Get<IConfig_EnterpriseClient>();
                    break;

                default:
                    break;
            }

            if (app == null)
            {
                Console.WriteLine("Invalid arguments passed.");
                return;
            }

            if (args.Length > 1 && args[1].ToLower() == "u")
            {
                app.RemoveValues();
            }
            else
            {
                app.InitializeToDefaultValues();
            }
            app.Save();
            Console.WriteLine("Configuration saved successfully.");
        }
    }
}
