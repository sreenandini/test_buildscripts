using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMC.Business.NetworkService;
using BMC.Transport.NetworkService;

namespace BMCNetworkService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoEnableDisable.EntryPoint();
        }
    }
}