using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EventsTransmitter.Utils
{
    public class AssemblyDetails
    {   
        /// <summary>
        /// Returns Assembly version 
        /// </summary>
        public static Version AssemblyVersion { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version; } }
        /// <summary>
        /// Returns Assembly version witn no special characters
        /// </summary>
        public static string VersionAsNumber { get { return AssemblyVersion.ToString().Replace(".",string.Empty); } }
    }

}
    
