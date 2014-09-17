using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmInputBox : Form
    {
        private string Message;
         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;
         private bool IsNumeric = false;

        public string TextValue { get; set; }

        public frmInputBox(string sMessageBox, string sLabel)
        {
            InitializeComponent();
            Message = sMessageBox;
            lblName.Text = sLabel;
            objDatawatcher = new Helpers.Datawatcher(this);
            
            //Resource File Changes
            SetTagProperty();
            this.ResolveResources();
        }

        public frmInputBox(string sMessageBox, string sLabel, string sInputMessage) : this(sMessageBox, sLabel)
        {
            txtValue.Text = sInputMessage;
            txtValue.SelectAll();
        }

        public frmInputBox(string sMessageBox, string sLabel, string sInputMessage,bool isNumeric)
            : this(sMessageBox, sLabel)
        {
            IsNumeric = isNumeric;
            txtValue.Visible = true;
            updValue.Visible = true;
            if (isNumeric)
            {
                txtValue.Visible = false;
                int value = 0;
                Int32.TryParse(sInputMessage, out value);
                updValue.Value = value;
            }
            else
            {
                updValue.Visible = false;
                txtValue.Text = sInputMessage;
                txtValue.SelectAll();
            }
        }

        //Resource File Changes
        private void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnOk.Tag = "Key_OKCaption";
           // this.lblName.Tag = "Key_Pleaseenterthenameofthenewdepot";
            this.Tag = "Key_BallyMultiConnectEnterprise";           
        }



        private void btnOk_Click(object sender, EventArgs e)
        {
            if (IsNumeric)
            {
                TextValue = updValue.Value.ToString();
            }
            else{
                if (string.IsNullOrEmpty(txtValue.Text.Trim()))
                {
                    Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1,"MSG_IB_ENTERVALIDVALUE"), this.Text);
                    txtValue.Focus();
                    return;
                }
                TextValue = txtValue.Text;
            }
            objDatawatcher.DataModify = false;
            this.Close();
        }
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //txtValue.Text = string.Empty;
            TextValue = string.Empty;
            this.Close();
        }
        
    }
}
