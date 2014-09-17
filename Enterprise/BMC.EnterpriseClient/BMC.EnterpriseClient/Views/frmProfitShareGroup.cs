using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmProfitShareGroup : GenericFormBase
    {
        public frmProfitShareGroup()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
