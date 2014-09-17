namespace BMC.EnterpriseClient.Views
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.vldSummary = new BMC.CoreLib.Win32.Validation.ValidationSummary(this.components);
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.vldCustom = new BMC.CoreLib.Win32.Validation.CustomValidator(this.components);
            this.pnlInnerContainer.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInnerContainer
            // 
            this.pnlInnerContainer.Controls.Add(this.tblContent);
            this.pnlInnerContainer.Size = new System.Drawing.Size(476, 103);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Size = new System.Drawing.Size(476, 158);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 158);
            this.pnlButtons.Size = new System.Drawing.Size(476, 41);
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 4;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContent.Controls.Add(this.vldSummary, 2, 3);
            this.tblContent.Controls.Add(this.lblUserName, 1, 1);
            this.tblContent.Controls.Add(this.lblPassword, 1, 2);
            this.tblContent.Controls.Add(this.txtPassword, 2, 2);
            this.tblContent.Controls.Add(this.txtUserName, 2, 1);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(0, 0);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 5;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Size = new System.Drawing.Size(476, 103);
            this.tblContent.TabIndex = 0;
            // 
            // vldSummary
            // 
            this.vldSummary.CausesValidation = false;
            this.vldSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vldSummary.ErrorMessage = "";
            this.vldSummary.ExitOnFirstFailure = true;
            this.vldSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vldSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.vldSummary.Location = new System.Drawing.Point(122, 73);
            this.vldSummary.Name = "vldSummary";
            this.vldSummary.Padding = new System.Windows.Forms.Padding(3);
            this.vldSummary.ShowMessageBox = false;
            this.vldSummary.Size = new System.Drawing.Size(341, 24);
            this.vldSummary.TabIndex = 5;
            this.vldSummary.Text = "validationSummary1";
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(23, 18);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(13, 0, 3, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(73, 13);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "* &User Name :";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(23, 48);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(13, 0, 3, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(66, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "* &Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(122, 45);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.txtPassword.MaxLength = 200;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ShortcutsEnabled = false;
            this.txtPassword.Size = new System.Drawing.Size(336, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUserName.Location = new System.Drawing.Point(122, 15);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.txtUserName.MaxLength = 200;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(336, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // vldCustom
            // 
            this.vldCustom.CancelFocusChangeWhenInvalid = false;
            this.vldCustom.ControlToValidate = null;
            this.vldCustom.ErrorMessage = "";
            this.vldCustom.HasErrors = false;
            this.vldCustom.Icon = ((System.Drawing.Icon)(resources.GetObject("vldCustom.Icon")));
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 199);
            this.Description = "Enter the credentials to proceed login";
            this.HideControls = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.OKCaption = "&Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.pnlInnerContainer.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.tblContent.ResumeLayout(false);
            this.tblContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private BMC.CoreLib.Win32.Validation.CustomValidator vldCustom;
        private BMC.CoreLib.Win32.Validation.ValidationSummary vldSummary;
    }
}