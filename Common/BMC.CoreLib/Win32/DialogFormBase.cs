using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Win32
{
    public partial class DialogFormBase : Form, IUserInterface
    {
        protected bool _isNotSaved = true;

        public DialogFormBase()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.OnOKButtonClick();
        }

        /// <summary>
        /// Called when [OK button click].
        /// </summary>
        protected void OnOKButtonClick()
        {
            if (this.SaveUI())
            {
                _isNotSaved = false;

                if (!this.PreventCloseOnOK)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [prevent close on OK].
        /// </summary>
        /// <value><c>true</c> if [prevent close on OK]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool PreventCloseOnOK { get; set; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool HideOKButton { get; set; }

        /// <summary>
        /// Gets or sets the OK caption.
        /// </summary>
        /// <value>The OK caption.</value>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string OKCaption
        {
            get { return btnOK.Text; }
            set { btnOK.Text = value; }
        }

        /// <summary>
        /// Gets or sets the cancel caption.
        /// </summary>
        /// <value>The cancel caption.</value>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string CancelCaption
        {
            get { return btnCancel.Text; }
            set { btnCancel.Text = value; }
        }

        private void DialogFormBase_Load(object sender, EventArgs e)
        {
            this.LoadForm();
        }

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
            ModuleProc PROC = new ModuleProc("GenericDialogFormBase", "LoadUI");

            try
            {
                //btnOK.Enabled = !this.HideOKButton;
                btnOK.Visible = !this.HideOKButton;
                if (this.HideOKButton)
                {
                    //tblButtons.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);
                }
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
        protected virtual void SaveChanges() { }

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

        #endregion        
    }
}
