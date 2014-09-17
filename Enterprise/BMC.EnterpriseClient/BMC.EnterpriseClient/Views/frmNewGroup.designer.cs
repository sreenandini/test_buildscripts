namespace BMC
{
    partial class frmNewGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewGroup));
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpBox = new System.Windows.Forms.GroupBox();
            this.txtGrpName = new System.Windows.Forms.TextBox();
            this.lblGrpName = new System.Windows.Forms.Label();
            this.grpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(8, 88);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(97, 25);
            this.btnAddGroup.TabIndex = 1;
            this.btnAddGroup.Text = "&Add New Group";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(128, 88);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 25);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.txtGrpName);
            this.grpBox.Controls.Add(this.lblGrpName);
            this.grpBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpBox.Location = new System.Drawing.Point(8, 0);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(217, 81);
            this.grpBox.TabIndex = 0;
            this.grpBox.TabStop = false;
            // 
            // txtGrpName
            // 
            this.txtGrpName.Location = new System.Drawing.Point(24, 44);
            this.txtGrpName.MaxLength = 50;
            this.txtGrpName.Name = "txtGrpName";
            this.txtGrpName.Size = new System.Drawing.Size(161, 20);
            this.txtGrpName.TabIndex = 1;
            // 
            // lblGrpName
            // 
            this.lblGrpName.AutoSize = true;
            this.lblGrpName.Location = new System.Drawing.Point(24, 24);
            this.lblGrpName.Name = "lblGrpName";
            this.lblGrpName.Size = new System.Drawing.Size(92, 13);
            this.lblGrpName.TabIndex = 0;
            this.lblGrpName.Text = "New Group Name";
            // 
            // frmNewGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 122);
            this.ControlBox = false;
            this.Controls.Add(this.grpBox);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNewGroup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpBox;
        private System.Windows.Forms.TextBox txtGrpName;
        private System.Windows.Forms.Label lblGrpName;
    }
}