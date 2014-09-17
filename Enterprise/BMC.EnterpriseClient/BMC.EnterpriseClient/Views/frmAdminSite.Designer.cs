namespace BMC.EnterpriseClient.Views
{
    partial class frmAdminSite
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
            this.SSTab1 = new System.Windows.Forms.TabControl();
            this.tbDetails1 = new System.Windows.Forms.TabPage();
            this.details1UC1 = new Details1.Details1UC();
            this.tbDetails2 = new System.Windows.Forms.TabPage();
            this.details2UC1 = new Details2.Details2UC();
            this.tbOwner = new System.Windows.Forms.TabPage();
            this.tbDefaults = new System.Windows.Forms.TabPage();
            this.tbAreas = new System.Windows.Forms.TabPage();
            this.areas1 = new BMC.EnterpriseClient.Views.Areas();
            this.tbZonePos = new System.Windows.Forms.TabPage();
            this.tbComms = new System.Windows.Forms.TabPage();
            this.ucComms1 = new BMC.EnterpriseClient.Views.ucComms();
            this.tbNotes = new System.Windows.Forms.TabPage();
            this.tbImages = new System.Windows.Forms.TabPage();
            this.tbMaintenance = new System.Windows.Forms.TabPage();
            this.tbAFTSettings = new System.Windows.Forms.TabPage();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.zonePosition1 = new BMC.EnterpriseClient.Views.ZonePosition();
            this.SSTab1.SuspendLayout();
            this.tbDetails1.SuspendLayout();
            this.tbDetails2.SuspendLayout();
            this.tbAreas.SuspendLayout();
            this.tbZonePos.SuspendLayout();
            this.tbComms.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SSTab1
            // 
            this.SSTab1.Controls.Add(this.tbDetails1);
            this.SSTab1.Controls.Add(this.tbDetails2);
            this.SSTab1.Controls.Add(this.tbOwner);
            this.SSTab1.Controls.Add(this.tbDefaults);
            this.SSTab1.Controls.Add(this.tbAreas);
            this.SSTab1.Controls.Add(this.tbZonePos);
            this.SSTab1.Controls.Add(this.tbComms);
            this.SSTab1.Controls.Add(this.tbNotes);
            this.SSTab1.Controls.Add(this.tbImages);
            this.SSTab1.Controls.Add(this.tbMaintenance);
            this.SSTab1.Controls.Add(this.tbAFTSettings);
            this.SSTab1.ItemSize = new System.Drawing.Size(53, 20);
            this.SSTab1.Location = new System.Drawing.Point(0, 0);
            this.SSTab1.Multiline = true;
            this.SSTab1.Name = "SSTab1";
            this.SSTab1.Padding = new System.Drawing.Point(26, 3);
            this.SSTab1.SelectedIndex = 0;
            this.SSTab1.Size = new System.Drawing.Size(564, 489);
            this.SSTab1.TabIndex = 0;
            // 
            // tbDetails1
            // 
            this.tbDetails1.Controls.Add(this.details1UC1);
            this.tbDetails1.Location = new System.Drawing.Point(4, 44);
            this.tbDetails1.Name = "tbDetails1";
            this.tbDetails1.Padding = new System.Windows.Forms.Padding(3);
            this.tbDetails1.Size = new System.Drawing.Size(556, 441);
            this.tbDetails1.TabIndex = 0;
            this.tbDetails1.Text = "Details 1";
            this.tbDetails1.UseVisualStyleBackColor = true;
            // 
            // details1UC1
            // 
            this.details1UC1.AutoScroll = true;
            this.details1UC1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.details1UC1.DlgDetail1UC = null;
            this.details1UC1.Location = new System.Drawing.Point(3, 0);
            this.details1UC1.Name = "details1UC1";
            this.details1UC1.Size = new System.Drawing.Size(553, 441);
            this.details1UC1.TabIndex = 0;
            // 
            // tbDetails2
            // 
            this.tbDetails2.Controls.Add(this.details2UC1);
            this.tbDetails2.Location = new System.Drawing.Point(4, 44);
            this.tbDetails2.Name = "tbDetails2";
            this.tbDetails2.Padding = new System.Windows.Forms.Padding(3);
            this.tbDetails2.Size = new System.Drawing.Size(556, 441);
            this.tbDetails2.TabIndex = 1;
            this.tbDetails2.Text = "Details 2";
            this.tbDetails2.UseVisualStyleBackColor = true;
            // 
            // details2UC1
            // 
            this.details2UC1.AutoScroll = true;
            this.details2UC1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.details2UC1.Location = new System.Drawing.Point(0, 0);
            this.details2UC1.Name = "details2UC1";
            this.details2UC1.Size = new System.Drawing.Size(556, 441);
            this.details2UC1.TabIndex = 0;
            // 
            // tbOwner
            // 
            this.tbOwner.Location = new System.Drawing.Point(4, 44);
            this.tbOwner.Name = "tbOwner";
            this.tbOwner.Size = new System.Drawing.Size(556, 441);
            this.tbOwner.TabIndex = 2;
            this.tbOwner.Text = "Owner";
            this.tbOwner.UseVisualStyleBackColor = true;
            // 
            // tbDefaults
            // 
            this.tbDefaults.Location = new System.Drawing.Point(4, 44);
            this.tbDefaults.Name = "tbDefaults";
            this.tbDefaults.Size = new System.Drawing.Size(556, 441);
            this.tbDefaults.TabIndex = 3;
            this.tbDefaults.Text = "Defaults";
            this.tbDefaults.UseVisualStyleBackColor = true;
            // 
            // tbAreas
            // 
            this.tbAreas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbAreas.Controls.Add(this.areas1);
            this.tbAreas.Location = new System.Drawing.Point(4, 44);
            this.tbAreas.Name = "tbAreas";
            this.tbAreas.Size = new System.Drawing.Size(556, 441);
            this.tbAreas.TabIndex = 4;
            this.tbAreas.Text = "Areas";
            // 
            // areas1
            // 
            this.areas1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.areas1.Location = new System.Drawing.Point(3, 5);
            this.areas1.Name = "areas1";
            this.areas1.Size = new System.Drawing.Size(550, 389);
            this.areas1.TabIndex = 0;
            // 
            // tbZonePos
            // 
            this.tbZonePos.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbZonePos.Controls.Add(this.panel1);
            this.tbZonePos.Location = new System.Drawing.Point(4, 44);
            this.tbZonePos.Name = "tbZonePos";
            this.tbZonePos.Size = new System.Drawing.Size(556, 441);
            this.tbZonePos.TabIndex = 5;
            this.tbZonePos.Text = "Zone/Pos";
            // 
            // tbComms
            // 
            this.tbComms.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbComms.Controls.Add(this.ucComms1);
            this.tbComms.Location = new System.Drawing.Point(4, 44);
            this.tbComms.Name = "tbComms";
            this.tbComms.Size = new System.Drawing.Size(556, 441);
            this.tbComms.TabIndex = 6;
            this.tbComms.Text = "Comms";
            // 
            // ucComms1
            // 
            this.ucComms1.Location = new System.Drawing.Point(-4, 3);
            this.ucComms1.Name = "ucComms1";
            this.ucComms1.SiteID = 0;
            this.ucComms1.SiteStatus = false;
            this.ucComms1.Size = new System.Drawing.Size(557, 343);
            this.ucComms1.TabIndex = 0;
            // 
            // tbNotes
            // 
            this.tbNotes.Location = new System.Drawing.Point(4, 44);
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(556, 441);
            this.tbNotes.TabIndex = 7;
            this.tbNotes.Text = "Notes";
            this.tbNotes.UseVisualStyleBackColor = true;
            // 
            // tbImages
            // 
            this.tbImages.Location = new System.Drawing.Point(4, 44);
            this.tbImages.Name = "tbImages";
            this.tbImages.Size = new System.Drawing.Size(556, 441);
            this.tbImages.TabIndex = 8;
            this.tbImages.Text = "Images";
            this.tbImages.UseVisualStyleBackColor = true;
            // 
            // tbMaintenance
            // 
            this.tbMaintenance.Location = new System.Drawing.Point(4, 44);
            this.tbMaintenance.Name = "tbMaintenance";
            this.tbMaintenance.Size = new System.Drawing.Size(556, 441);
            this.tbMaintenance.TabIndex = 9;
            this.tbMaintenance.Text = "Maintenance";
            this.tbMaintenance.UseVisualStyleBackColor = true;
            // 
            // tbAFTSettings
            // 
            this.tbAFTSettings.Location = new System.Drawing.Point(4, 44);
            this.tbAFTSettings.Name = "tbAFTSettings";
            this.tbAFTSettings.Size = new System.Drawing.Size(556, 441);
            this.tbAFTSettings.TabIndex = 10;
            this.tbAFTSettings.Text = "AFT Settings";
            this.tbAFTSettings.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(367, 444);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 36);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(467, 444);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(82, 36);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.zonePosition1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 441);
            this.panel1.TabIndex = 0;
            // 
            // zonePosition1
            // 
            this.zonePosition1.AutoScroll = true;
            this.zonePosition1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.zonePosition1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zonePosition1.Location = new System.Drawing.Point(0, 0);
            this.zonePosition1.Name = "zonePosition1";
            this.zonePosition1.Size = new System.Drawing.Size(556, 441);
            this.zonePosition1.TabIndex = 1;
            // 
            // frmAdminSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(562, 486);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.SSTab1);
            this.Name = "frmAdminSite";
            this.Text = "Site Administrator";
            this.Load += new System.EventHandler(this.frmAdminSite_Load);
            this.SSTab1.ResumeLayout(false);
            this.tbDetails1.ResumeLayout(false);
            this.tbDetails2.ResumeLayout(false);
            this.tbAreas.ResumeLayout(false);
            this.tbZonePos.ResumeLayout(false);
            this.tbComms.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl SSTab1;
        private System.Windows.Forms.TabPage tbDetails1;
        private System.Windows.Forms.TabPage tbDetails2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TabPage tbOwner;
        private System.Windows.Forms.TabPage tbDefaults;
        private System.Windows.Forms.TabPage tbAreas;
        private System.Windows.Forms.TabPage tbZonePos;
        private System.Windows.Forms.TabPage tbComms;
        private System.Windows.Forms.TabPage tbNotes;
        private System.Windows.Forms.TabPage tbImages;
        private System.Windows.Forms.TabPage tbMaintenance;
        private System.Windows.Forms.TabPage tbAFTSettings;
        private Details1.Details1UC details1UC1;
        private Details2.Details2UC details2UC1;
        private Areas areas1;
        private ucComms ucComms1;
        private System.Windows.Forms.Panel panel1;
        private ZonePosition zonePosition1;        
    }
}