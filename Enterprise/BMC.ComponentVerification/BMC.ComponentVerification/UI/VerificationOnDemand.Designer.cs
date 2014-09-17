namespace BMC.ComponentVerification.UI
{
    partial class VerificationOnDemand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerificationOnDemand));
            this.label1 = new System.Windows.Forms.Label();
            this.cbSite = new System.Windows.Forms.ComboBox();
            this.cbMachines = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbComponents = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnCanel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Site Name";
            // 
            // cbSite
            // 
            this.cbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSite.FormattingEnabled = true;
            this.cbSite.Location = new System.Drawing.Point(113, 26);
            this.cbSite.Name = "cbSite";
            this.cbSite.Size = new System.Drawing.Size(155, 21);
            this.cbSite.TabIndex = 1;
            this.cbSite.SelectedIndexChanged += new System.EventHandler(this.cbSite_SelectedIndexChanged);
            // 
            // cbMachines
            // 
            this.cbMachines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMachines.Enabled = false;
            this.cbMachines.FormattingEnabled = true;
            this.cbMachines.Location = new System.Drawing.Point(113, 91);
            this.cbMachines.Name = "cbMachines";
            this.cbMachines.Size = new System.Drawing.Size(155, 21);
            this.cbMachines.TabIndex = 3;
            this.cbMachines.SelectedIndexChanged += new System.EventHandler(this.cbMachines_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Machine Serial No";
            // 
            // cbComponents
            // 
            this.cbComponents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComponents.Enabled = false;
            this.cbComponents.FormattingEnabled = true;
            this.cbComponents.Location = new System.Drawing.Point(113, 158);
            this.cbComponents.Name = "cbComponents";
            this.cbComponents.Size = new System.Drawing.Size(155, 21);
            this.cbComponents.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Component Name";
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(21, 15);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(80, 33);
            this.btnVerify.TabIndex = 6;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // btnCanel
            // 
            this.btnCanel.Location = new System.Drawing.Point(147, 15);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(80, 33);
            this.btnCanel.TabIndex = 7;
            this.btnCanel.Text = "Cancel";
            this.btnCanel.UseVisualStyleBackColor = true;
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbMachines);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbSite);
            this.groupBox1.Controls.Add(this.cbComponents);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 215);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnVerify);
            this.groupBox2.Controls.Add(this.btnCanel);
            this.groupBox2.Location = new System.Drawing.Point(9, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 54);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // VerificationOnDemand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 282);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VerificationOnDemand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verification On Demand";
            this.Load += new System.EventHandler(this.VerificationOnDemand_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSite;
        private System.Windows.Forms.ComboBox cbMachines;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbComponents;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}