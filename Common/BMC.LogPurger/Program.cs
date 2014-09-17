using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using Microsoft.Win32;

namespace BMC.PurgeUtilities
{
    class Program
    {
        static void Main(string[] args)
        {
            Purge purge = new Purge();
            purge.CleanLogs();
        }


    }
}
