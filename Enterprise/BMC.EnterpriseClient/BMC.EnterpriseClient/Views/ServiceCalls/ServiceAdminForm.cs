using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Business;
using BMC.CoreLib.Win32.Validation;

namespace BMC.EnterpriseClient.Views.ServiceCalls
{
    public partial class ServiceAdminForm : Form, IServiceAdmin
    {
        private IDictionary<string, Func<IUserControl2>> _userControls = null;
        private IUserControl2 _activeControl = null;
        private ServiceCallBusiness _business = new ServiceCallBusiness();

        public ServiceAdminForm()
        {
            InitializeComponent();
            this.InitControls();
        }

        private void InitControls()
        {
            ModuleProc PROC = new ModuleProc("", "InitControls");

            try
            {
                _userControls = new SortedDictionary<string, Func<IUserControl2>>()
                {
                    { "0", () => { return new UcSAFaultCodes(this); } },
                    { "1", () => { return new UcSAFixCodes(this); } },
                    { "2", () => { return new UcSASource(this); } },
                    { "3", () => { return new UcSASLAContracts(this); } },
                };
                grpContent.Padding = new Padding(5, 0, 5, 5);
                rbtnFaultCodes.Checked = true;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void OnSource_CheckedChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "OnSource_CheckedChanged");
            RadioButton rbtn = sender as RadioButton;
            if (!rbtn.Checked) return;

            try
            {
                if (_activeControl != null)
                {
                    IDisposable disposable = _activeControl as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                        _activeControl = null;
                    }
                }

                grpContent.Text = rbtn.Text;
                pnlContent.Controls.Clear();

                IUserControl2 uc = _userControls[((Control)sender).Tag.ToString()]();
                UserControl uc2 = uc as UserControl;
                uc2.Dock = DockStyle.Fill;
                uc2.BackColor = Color.DarkGray;
                uc.LoadControl();

                pnlContent.Controls.Add(uc2);
                _activeControl = uc;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public ServiceCallBusiness Business { get { return _business; } }
        //public ValidationSummary ValSummary { get { return vldSummary; } }
        //public CustomValidator ValCustom { get { return vldCustom; } }
    }

    public interface IServiceAdmin : IDisposable
    {
        ServiceCallBusiness Business { get; }
        //ValidationSummary ValSummary { get; }
        //CustomValidator ValCustom { get; }
    }
}
