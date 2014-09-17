namespace BMC.EnterpriseClient.Views
{
    partial class frmModelType
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
            this.cmbModelType = new System.Windows.Forms.ComboBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkNGA = new System.Windows.Forms.CheckBox();
            this.lblModelType = new System.Windows.Forms.Label();
            this.gpModelType = new System.Windows.Forms.GroupBox();
            this.lblLine = new System.Windows.Forms.Label();
            this.txtModelDesc = new System.Windows.Forms.TextBox();
            this.lblModelDesc = new System.Windows.Forms.Label();
            this.lblModelName = new System.Windows.Forms.Label();
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gpModelType.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbModelType
            // 
            this.cmbModelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModelType.FormattingEnabled = true;
            this.cmbModelType.Location = new System.Drawing.Point(303, 36);
            this.cmbModelType.Name = "cmbModelType";
            this.cmbModelType.Size = new System.Drawing.Size(237, 21);
            this.cmbModelType.TabIndex = 2;
            this.cmbModelType.SelectedIndexChanged += new System.EventHandler(this.cmbModelType_SelectedIndexChanged);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(188, 169);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(87, 29);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkNGA
            // 
            this.chkNGA.AutoSize = true;
            this.chkNGA.Location = new System.Drawing.Point(16, 38);
            this.chkNGA.Name = "chkNGA";
            this.chkNGA.Size = new System.Drawing.Size(146, 17);
            this.chkNGA.TabIndex = 0;
            this.chkNGA.Text = "Is Non Gaming Asset";
            this.chkNGA.UseVisualStyleBackColor = true;
            this.chkNGA.CheckedChanged += new System.EventHandler(this.chkNGA_CheckedChanged);
            // 
            // lblModelType
            // 
            this.lblModelType.AutoSize = true;
            this.lblModelType.Location = new System.Drawing.Point(299, 16);
            this.lblModelType.Name = "lblModelType";
            this.lblModelType.Size = new System.Drawing.Size(126, 13);
            this.lblModelType.TabIndex = 1;
            this.lblModelType.Text = "Cabinet/Model Type:";
            // 
            // gpModelType
            // 
            this.gpModelType.Controls.Add(this.lblLine);
            this.gpModelType.Controls.Add(this.txtModelDesc);
            this.gpModelType.Controls.Add(this.lblModelDesc);
            this.gpModelType.Controls.Add(this.lblModelName);
            this.gpModelType.Controls.Add(this.txtModelName);
            this.gpModelType.Controls.Add(this.lblModelType);
            this.gpModelType.Controls.Add(this.cmbModelType);
            this.gpModelType.Controls.Add(this.chkNGA);
            this.gpModelType.Location = new System.Drawing.Point(7, 0);
            this.gpModelType.Name = "gpModelType";
            this.gpModelType.Size = new System.Drawing.Size(559, 156);
            this.gpModelType.TabIndex = 0;
            this.gpModelType.TabStop = false;
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Location = new System.Drawing.Point(10, 61);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(532, 13);
            this.lblLine.TabIndex = 3;
            this.lblLine.Text = "___________________________________________________________________________";
            // 
            // txtModelDesc
            // 
            this.txtModelDesc.Location = new System.Drawing.Point(303, 107);
            this.txtModelDesc.MaxLength = 50;
            this.txtModelDesc.Name = "txtModelDesc";
            this.txtModelDesc.Size = new System.Drawing.Size(237, 21);
            this.txtModelDesc.TabIndex = 7;
            // 
            // lblModelDesc
            // 
            this.lblModelDesc.AutoSize = true;
            this.lblModelDesc.Location = new System.Drawing.Point(303, 91);
            this.lblModelDesc.Name = "lblModelDesc";
            this.lblModelDesc.Size = new System.Drawing.Size(173, 13);
            this.lblModelDesc.TabIndex = 6;
            this.lblModelDesc.Text = "* Cabinet/Model Description:";
            // 
            // lblModelName
            // 
            this.lblModelName.AutoSize = true;
            this.lblModelName.Location = new System.Drawing.Point(16, 91);
            this.lblModelName.Name = "lblModelName";
            this.lblModelName.Size = new System.Drawing.Size(174, 13);
            this.lblModelName.TabIndex = 4;
            this.lblModelName.Text = "* Cabinet/Model Type Name:";
            // 
            // txtModelName
            // 
            this.txtModelName.Location = new System.Drawing.Point(16, 107);
            this.txtModelName.MaxLength = 20;
            this.txtModelName.Name = "txtModelName";
            this.txtModelName.Size = new System.Drawing.Size(237, 21);
            this.txtModelName.TabIndex = 5;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(282, 169);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(87, 29);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(380, 169);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(87, 29);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(475, 169);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 29);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmModelType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(570, 208);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.gpModelType);
            this.Controls.Add(this.btnNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModelType";
            this.Text = "Cabinet/Model Type";
            this.Load += new System.EventHandler(this.frmModelType_Load);
            this.gpModelType.ResumeLayout(false);
            this.gpModelType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbModelType;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.CheckBox chkNGA;
        private System.Windows.Forms.Label lblModelType;
        private System.Windows.Forms.GroupBox gpModelType;
        private System.Windows.Forms.Label lblModelName;
        private System.Windows.Forms.TextBox txtModelName;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.TextBox txtModelDesc;
        private System.Windows.Forms.Label lblModelDesc;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
    }
}