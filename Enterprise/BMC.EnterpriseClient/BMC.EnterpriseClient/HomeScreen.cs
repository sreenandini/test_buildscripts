using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;

namespace BMC.EnterpriseClient
{
    public partial class HomeScreen : Form
    {
        public event EventHandler<WidgetEventArgs> WidgetLoad = null;
        public event EventHandler<WidgetEventArgs> WidgetClick = null;

        public HomeScreen()
        {
            InitializeComponent();
            widgets.WidgetLoad += new EventHandler<WidgetEventArgs>(OnWidgetLoad);
            widgets.WidgetClick += new EventHandler<WidgetEventArgs>(OnWidgetClick);
#if DEBUG
            widgets.BackgroundImage = null;
#endif
            widgets.Directory = "admin";
        }

        public string UserName { get; set; }

        public HomeScreenWidgets.WidgetDetails CaptureWidget(Form childForm, string uniqueKey)
        {
            return widgets.CaptureWidget(childForm, uniqueKey,
                childForm.Text, childForm.Bounds);
        }

        private void HomeScreen_Load(object sender, EventArgs e)
        {
            this.LoadWidgets();
        }

        private void OnWidgetLoad(object sender, WidgetEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "OnWidgetLoad");

            try
            {
                if (this.WidgetLoad != null)
                {
                    this.WidgetLoad(sender, e);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void OnWidgetClick(object sender, WidgetEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("HomeScreenWidgets", "OnWidgetLoad");

            try
            {
                if (this.WidgetClick != null)
                {
                    this.WidgetClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void LoadWidgets()
        {
            widgets.LoadWidgets();
        }
    }
}
