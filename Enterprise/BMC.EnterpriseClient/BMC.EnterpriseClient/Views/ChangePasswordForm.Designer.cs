namespace BMC.EnterpriseClient.Views
{
    partial class ChangePasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePasswordForm));
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.vldSummary = new BMC.CoreLib.Win32.Validation.ValidationSummary(this.components);
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.vldCustom = new BMC.CoreLib.Win32.Validation.CustomValidator(this.components);
            this.pnlInnerContainer.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInnerContainer
            // 
            this.pnlInnerContainer.Controls.Add(this.tblContent);
            this.pnlInnerContainer.Size = new System.Drawing.Size(594, 147);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Size = new System.Drawing.Size(594, 202);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 202);
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 4;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContent.Controls.Add(this.vldSummary, 2, 4);
            this.tblContent.Controls.Add(this.lblOldPassword, 1, 1);
            this.tblContent.Controls.Add(this.lblPassword, 1, 2);
            this.tblContent.Controls.Add(this.txtNewPassword, 2, 2);
            this.tblContent.Controls.Add(this.txtOldPassword, 2, 1);
            this.tblContent.Controls.Add(this.lblConfirmPassword, 1, 3);
            this.tblContent.Controls.Add(this.txtConfirmPassword, 2, 3);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(0, 0);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 7;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Size = new System.Drawing.Size(594, 147);
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
            this.vldSummary.Location = new System.Drawing.Point(173, 103);
            this.vldSummary.Name = "vldSummary";
            this.vldSummary.Padding = new System.Windows.Forms.Padding(3);
            this.vldSummary.ShowMessageBox = false;
            this.vldSummary.Size = new System.Drawing.Size(408, 39);
            this.vldSummary.TabIndex = 6;
            this.vldSummary.Text = "validationSummary1";
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(23, 18);
            this.lblOldPassword.Margin = new System.Windows.Forms.Padding(13, 0, 3, 0);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(85, 13);
            this.lblOldPassword.TabIndex = 0;
            this.lblOldPassword.Text = "* Old &Password :";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(23, 48);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(13, 0, 3, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(91, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "* &New Password :";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewPassword.Location = new System.Drawing.Point(173, 45);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.txtNewPassword.MaxLength = 50;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.ShortcutsEnabled = false;
            this.txtNewPassword.Size = new System.Drawing.Size(403, 20);
            this.txtNewPassword.TabIndex = 3;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOldPassword.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtOldPassword.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtOldPassword.Location = new System.Drawing.Point(173, 15);
            this.txtOldPassword.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.txtOldPassword.MaxLength = 50;
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.ShortcutsEnabled = false;
            this.txtOldPassword.Size = new System.Drawing.Size(403, 20);
            this.txtOldPassword.TabIndex = 1;
            this.txtOldPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(23, 78);
            this.lblConfirmPassword.Margin = new System.Windows.Forms.Padding(13, 0, 3, 0);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(104, 13);
            this.lblConfirmPassword.TabIndex = 4;
            this.lblConfirmPassword.Text = "* Confir&m Password :";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(173, 75);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.txtConfirmPassword.MaxLength = 50;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.ShortcutsEnabled = false;
            this.txtConfirmPassword.Size = new System.Drawing.Size(403, 20);
            this.txtConfirmPassword.TabIndex = 5;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // vldCustom
            // 
            this.vldCustom.CancelFocusChangeWhenInvalid = false;
            this.vldCustom.ControlToValidate = null;
            this.vldCustom.ErrorMessage = "";
            this.vldCustom.HasErrors = false;
            this.vldCustom.Icon = ((System.Drawing.Icon)(resources.GetObject("vldCustom.Icon")));
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 243);
            this.HideControls = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangePasswordForm";
            this.Text = "Change Password";
            this.pnlInnerContainer.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.tblContent.ResumeLayout(false);
            this.tblContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private BMC.CoreLib.Win32.Validation.ValidationSummary vldSummary;
        private BMC.CoreLib.Win32.Validation.CustomValidator vldCustom;
    }
}