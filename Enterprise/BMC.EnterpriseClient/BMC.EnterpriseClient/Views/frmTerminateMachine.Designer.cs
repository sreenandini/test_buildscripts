namespace BMC.EnterpriseClient.Views
{
    partial class frmTerminateMachine
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
            this.lbl_TerminationReason = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTerminateDate = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.cmbTerminateReason = new System.Windows.Forms.ComboBox();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.cmbTerminateReason);
            this.pnlContainer.Controls.Add(this.txtComments);
            this.pnlContainer.Controls.Add(this.txtUserName);
            this.pnlContainer.Controls.Add(this.txtTerminateDate);
            this.pnlContainer.Controls.Add(this.label4);
            this.pnlContainer.Controls.Add(this.label3);
            this.pnlContainer.Controls.Add(this.label2);
            this.pnlContainer.Controls.Add(this.lbl_TerminationReason);
            this.pnlContainer.Size = new System.Drawing.Size(352, 203);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 203);
            this.pnlButtons.Size = new System.Drawing.Size(352, 41);
            // 
            // lbl_TerminationReason
            // 
            this.lbl_TerminationReason.AutoSize = true;
            this.lbl_TerminationReason.Location = new System.Drawing.Point(7, 14);
            this.lbl_TerminationReason.Name = "lbl_TerminationReason";
            this.lbl_TerminationReason.Size = new System.Drawing.Size(102, 13);
            this.lbl_TerminationReason.TabIndex = 0;
            this.lbl_TerminationReason.Text = "Termination Reason";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Termination Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "User Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Comments";
            // 
            // txtTerminateDate
            // 
            this.txtTerminateDate.Enabled = false;
            this.txtTerminateDate.Location = new System.Drawing.Point(112, 40);
            this.txtTerminateDate.Name = "txtTerminateDate";
            this.txtTerminateDate.Size = new System.Drawing.Size(126, 20);
            this.txtTerminateDate.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(112, 69);
            this.txtUserName.MaxLength = 100;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(126, 20);
            this.txtUserName.TabIndex = 5;
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(112, 98);
            this.txtComments.MaxLength = 250;
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(235, 88);
            this.txtComments.TabIndex = 7;
            // 
            // cmbTerminateReason
            // 
            this.cmbTerminateReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTerminateReason.FormattingEnabled = true;
            this.cmbTerminateReason.Location = new System.Drawing.Point(112, 10);
            this.cmbTerminateReason.Name = "cmbTerminateReason";
            this.cmbTerminateReason.Size = new System.Drawing.Size(126, 21);
            this.cmbTerminateReason.TabIndex = 1;
            // 
            // frmTerminateMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 244);
            this.Name = "frmTerminateMachine";
            this.OKCaption = "&Terminate";
            this.Text = "Terminate Machine";
            this.Load += new System.EventHandler(this.frmTerminateMachine_Load);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_TerminationReason;
        private System.Windows.Forms.ComboBox cmbTerminateReason;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtTerminateDate;
    }
}