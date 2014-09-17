using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.ComponentVerification.BusinessLayer;

namespace BMC.ComponentVerification.UI
{
    public partial class CreateComponentType : Form
    {
        ComponentTypeAccessor cta;
        public CreateComponentType()
        {
            InitializeComponent();
            cta = new ComponentTypeAccessor();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int result = cta.SaveComponentType(txtName.Text.Trim(), txtDesc.Text.Trim());
            if (result < 1)
            {
                MessageBox.Show("Component created successfully");
                this.Close();
            }            
        }
    }
}
