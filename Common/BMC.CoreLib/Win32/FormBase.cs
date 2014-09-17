using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;

namespace BMC.CoreLib.Win32
{
    public partial class FormBase : Form, IUserInterface
    {
        protected bool _isNotSaved = true;

        public FormBase()
        {
            InitializeComponent();
        }

        #region Form Events
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadForm();
        }
        #endregion

        #region IUserInterface Members

        /// <summary>
        /// Loads the form.
        /// </summary>
        protected void LoadForm()
        {
            this.LoadUI();
        }

        /// <summary>
        /// Loads the UI.
        /// </summary>
        public void LoadUI()
        {
            ModuleProc PROC = new ModuleProc("GenericFormBase", "LoadUI");

            try
            {
                this.LoadResourceValues();
                this.LoadChanges();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Loads the changes.
        /// </summary>
        protected virtual void LoadChanges() { }

        /// <summary>
        /// Loads the resource values.
        /// </summary>
        protected virtual void LoadResourceValues() { }

        /// <summary>
        /// Saves the UI.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        public bool SaveUI()
        {
            ModuleProc PROC = new ModuleProc("GenericDialogFormBase", "LoadUI");

            try
            {
                if (this.ValidateUI())
                {
                    this.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return false;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>        
        protected virtual bool SaveChanges() { return true; }

        /// <summary>
        /// Validates the UI.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        public bool ValidateUI()
        {
            ModuleProc PROC = new ModuleProc("GenericDialogFormBase", "LoadUI");

            try
            {
                return this.ValidateChanges();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return false;
        }

        protected virtual bool ValidateChanges() { return true; }

        /// <summary>
        /// Called when [accept button clicked].
        /// </summary>
        protected void OnAcceptButtonClicked()
        {
            if (this.SaveUI())
            {
                _isNotSaved = false;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Called when [cancel button clicked].
        /// </summary>
        protected void OnCancelButtonClicked()
        {
            this.Close();
        }

        #endregion

        private void FormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnBeforeFormClosing(e.CloseReason);
        }

        protected virtual void OnBeforeFormClosing(CloseReason reason) { }
    }
}
