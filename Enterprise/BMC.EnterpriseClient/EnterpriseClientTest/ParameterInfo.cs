using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace EnterpriseClientTest
{
    public class ParameterInfo
    {
        private static string _clientExe = null;

        static ParameterInfo()
        {
            _clientExe = Path.Combine(Path.GetDirectoryName(typeof(ParameterInfo).Assembly.Location), "BMC.EnterpriseClient.exe");
        }

        public ParameterInfo(string displayMember, string valueMember)
        {
            this.DisplayMember = displayMember;
            this.ValueMember = valueMember;
        }

        public string DisplayMember { get; set; }

        public string ValueMember { get; set; }

        public bool IsEnabled
        {
            get
            {
                return File.Exists(_clientExe);
            }
        }

        public void StartProcess()
        {
            ProcessStartInfo psi = new ProcessStartInfo(_clientExe, this.ValueMember);
            Process ps = new Process();
            ps.StartInfo = psi;
            ps.Start();
            ps.WaitForExit(2000);
        }
    }

    public class ParameterInfoCollection : System.Collections.ObjectModel.ObservableCollection<ParameterInfo>
    {
        public ParameterInfoCollection()
        {
            this.Add(new ParameterInfo("Stacker", "stacker"));
            this.Add(new ParameterInfo("Drop Schedule", "drop_schedule"));
            this.Add(new ParameterInfo("Depot", "depot"));
            this.Add(new ParameterInfo("Share Holder", "shareholder"));
            this.Add(new ParameterInfo("Profit Share Group", "ProfitShareGroup"));
            this.Add(new ParameterInfo("Profit Share Group Master", "ProfitShareGroupMaster"));
			this.Add(new ParameterInfo("User Site Access", "UserSiteAccess"));
            this.Add(new ParameterInfo("User Admin Form", "UserAdminForm"));
            this.Add(new ParameterInfo("User Group Form", "UserGroup"));
			this.Add(new ParameterInfo("Operator", "operator"));
            this.Add(new ParameterInfo("Depreciation", "depreciation"));
            this.Add(new ParameterInfo("Read Based Liquidation", "ReadLiquidation"));
            this.Add(new ParameterInfo("Admin Site", "frmAdminSite"));
            this.Add(new ParameterInfo("Liquidation", "frmLiquidationBatch"));
            this.Add(new ParameterInfo("VaultAdmin", "frmVaultAdmin"));
        }
    }
}
