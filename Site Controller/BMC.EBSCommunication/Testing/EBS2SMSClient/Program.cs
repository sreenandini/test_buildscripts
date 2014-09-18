using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BallyTech.Bonusing.Core.SlotSystemGateway.Proxy;
using BMC.CoreLib;
using BMC.EBSComms.Contracts.Dto;

namespace EBS2SMSClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "EBS To BMC Web Service Simulator";

            string url = "http://127.0.0.1:18080/SDS/S2S";
            string request = string.Empty;

            using (Stream ms = typeof(Program).Assembly.GetManifestResourceStream("EBS2SMSClient.Input.Xml"))
            {
                using (StreamReader st = new StreamReader(ms))
                {
                    request = st.ReadToEnd();
                }
            }

            while (true)
            {
                Console.WriteLine("Press any key to continue...");
                string line = Console.ReadLine();
                if (line == "q") break;

                try
                {
                    S2SEndPointService soap = new S2SEndPointService(url);
                    var resp = soap.processS2SMessage(new processS2SMessage
                    {
                        s2sMessage = request,
                    });
                    string response = string.Empty;

                    if (resp != null &&
                        !resp.@return.IsEmpty())
                    {
                        response = resp.@return;
                    }

                    //File.WriteAllText(@"d:\ebs.txt", response);
                    string data = response;
                    if (data.Length > 255)
                    {
                        data = data.Substring(0, 255) + "...";
                    }
                    Console.WriteLine("Received : " + data);

                    object o = Extensions.ReadXmlObject<s2sMessage>(response, string.Empty);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
