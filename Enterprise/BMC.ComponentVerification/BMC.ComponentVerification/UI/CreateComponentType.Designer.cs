namespace BMC.ComponentVerification.UI
{
    partial class CreateComponentType
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
            this.pnlCreateCT = new System.Windows.Forms.Panel();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblCTName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbButton = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlCreateCT.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCreateCT
            // 
            this.pnlCreateCT.Controls.Add(this.panel1);
            this.pnlCreateCT.Controls.Add(this.txtDesc);
            this.pnlCreateCT.Controls.Add(this.txtName);
            this.pnlCreateCT.Controls.Add(this.btnCancel);
            this.pnlCreateCT.Controls.Add(this.btnSave);
            this.pnlCreateCT.Controls.Add(this.lblDesc);
            this.pnlCreateCT.Controls.Add(this.lblCTName);
            this.pnlCreateCT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCreateCT.Location = new System.Drawing.Point(0, 0);
            this.pnlCreateCT.Name = "pnlCreateCT";
            this.pnlCreateCT.Size = new System.Drawing.Size(730, 738);
            this.pnlCreateCT.TabIndex = 0;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(125, 73);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(148, 34);
            this.txtDesc.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(125, 25);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(148, 20);
            this.txtName.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(198, 143);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(76, 140);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(23, 73);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 1;
            this.lblDesc.Text = "Description";
            // 
            // lblCTName
            // 
            this.lblCTName.AutoSize = true;
            this.lblCTName.Location = new System.Drawing.Point(23, 25);
            this.lblCTName.Name = "lblCTName";
            this.lblCTName.Size = new System.Drawing.Size(35, 13);
            this.lblCTName.TabIndex = 0;
            this.lblCTName.Text = "Name";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 630);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 108);
            this.panel1.TabIndex = 7;
            // 
            // gbButton
            // 
            this.gbButton.Controls.Add(this.btnClose);
            this.gbButton.Location = new System.Drawing.Point(4, 29);
            this.gbButton.Name = "gbButton";
            this.gbButton.Size = new System.Drawing.Size(251, 67);
            this.gbButton.TabIndex = 4;
            this.gbButton.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(266, 26);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 29);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Visible = false;
            // 
            // CreateComponentType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 738);
            this.Controls.Add(this.pnlCreateCT);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateComponentType";
            this.Text = "CreateComponentType";
            this.pnlCreateCT.ResumeLayout(false);
            this.pnlCreateCT.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCreateCT;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblCTName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbButton;
        private System.Windows.Forms.Button btnClose;
    }
}