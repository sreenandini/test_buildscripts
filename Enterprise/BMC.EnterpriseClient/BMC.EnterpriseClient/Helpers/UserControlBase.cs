using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Views;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;

namespace BMC.EnterpriseClient.Helpers
{
    public partial class UserControlBase : UserControl, IUserControl2
    {
        public UserControlBase()
        {
            InitializeComponent();
        }

        #region IUserControl2 Members

        public virtual void ClearControl() { }

        #endregion

        #region IUserControl Members

        public virtual bool IsControlInitialized { get; set; }

        public void LoadControl()
        {
            ModuleProc PROC = new ModuleProc("UserControlBase", "Method");

            try
            {
                this.LoadControlInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void LoadControlInternal() { }

        public bool SaveControl()
        {
            ModuleProc PROC = new ModuleProc("UserControlBase", "SaveControl");
            bool result = default(bool);

            try
            {
                result = this.SaveControlInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected virtual bool SaveControlInternal() { return true; }

        #endregion
    }
}
