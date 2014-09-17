using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMC.CoreLib.Win32
{
    public partial class UserControlUI : UserControl, IUserInterface
    {
        public UserControlUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the UserControlUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UserControlUI_Load(object sender, EventArgs e)
        {
            this.LoadUserControl();
        }

        #region IUserInterface Members

        /// <summary>
        /// Loads the user control.
        /// </summary>
        protected void LoadUserControl()
        {
            this.LoadUI();
        }

        /// <summary>
        /// Loads the UI.
        /// </summary>
        public void LoadUI()
        {
            this.LoadResourceValues();
            this.LoadChanges();
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
            if (this.ValidateUI())
            {
                return this.SaveChanges();
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
        public virtual bool ValidateUI()
        {
            return true;
        }

        #endregion
    }
}
