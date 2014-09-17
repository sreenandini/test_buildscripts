using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common;

namespace BMC.MeterAdjustmentTool
{
    public partial class UxInstalltionFilter : UserControl
    {
        public UxInstalltionFilter()
        {
            InitializeComponent();

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.lblEndDate.Tag = "Key_EndDateColon";
            this.chkALL.Tag = "Key_FilterByColon";
            this.btnProcess.Tag = "Key_Process";
            this.lblStartDate.Tag = "Key_StartDateColon";
        }

        private void UxInstalltionFilter_Load(object sender, EventArgs e)
        {
            // For externalization
            this.ResolveResources();
        }
    }
}
