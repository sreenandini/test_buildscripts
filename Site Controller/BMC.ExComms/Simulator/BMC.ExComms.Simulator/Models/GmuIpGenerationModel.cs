using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Data;

namespace BMC.ExComms.Simulator.Models
{
    public class GmuIpGenerationModel
    {
        public string StartingIPAddress { get; set; }

        public string SubnetMask { get; set; }

        public int TotalGMUs { get; set; }

        public ICollectionView NetworkInterfaces { get; set; }
    }
}
