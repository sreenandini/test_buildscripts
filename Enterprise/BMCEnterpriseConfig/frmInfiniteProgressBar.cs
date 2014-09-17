using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BMC.Common;

namespace BMC
{
    public partial class frmInfiniteProgressBar : Form
    {
        [Browsable(true)]
        public Color SetBG
        {
            set
            {
                this.BackColor = value;
            }
            get
            {
                return this.BackColor;
            }

            
        }
        public frmInfiniteProgressBar()
        {
            InitializeComponent();
            setTagProperty();
        }

        private void setTagProperty()
        {
            lblPleaseWait.Tag = "Key_EC_PleaseWait";
            this.ResolveResources();
        }

        private void frmInfiniteProgressBar_Load(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void frmInfiniteProgressBar_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
