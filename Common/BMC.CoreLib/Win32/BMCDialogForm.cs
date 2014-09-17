using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BMC.CoreLib.Win32
{
    public partial class BMCDialogForm : BMC.CoreLib.Win32.DialogFormBase
    {
        public BMCDialogForm()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public string Description
        {
            get { return lblDescription.Text; }
            set
            {
                lblDescription.Text = value;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public bool HideControls
        {
            get { return pnlButtons.Visible; }
            set
            {
                pnlButtons.Visible = value;
            }
        }
    }
}
