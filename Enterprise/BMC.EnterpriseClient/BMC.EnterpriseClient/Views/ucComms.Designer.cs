namespace BMC.EnterpriseClient.Views
{
    partial class ucComms
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSiteWebURL = new System.Windows.Forms.Label();
            this.txtWebURL = new System.Windows.Forms.TextBox();
            this.btnExportSiteSetup = new System.Windows.Forms.Button();
            this.btnExportKeys = new System.Windows.Forms.Button();
            this.btnEnableDisableSite = new System.Windows.Forms.Button();
            this.tblMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.grpExportDetails = new System.Windows.Forms.GroupBox();
            this.tblExportChkBox = new System.Windows.Forms.TableLayoutPanel();
            this.chkExportSiteSetup = new System.Windows.Forms.CheckBox();
            this.chkExportSiteCalendar = new System.Windows.Forms.CheckBox();
            this.chkExportModelsToSite = new System.Windows.Forms.CheckBox();
            this.chkExportGames = new System.Windows.Forms.CheckBox();
            this.tblMainFrame.SuspendLayout();
            this.grpExportDetails.SuspendLayout();
            this.tblExportChkBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSiteWebURL
            // 
            this.lblSiteWebURL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSiteWebURL.AutoSize = true;
            this.lblSiteWebURL.Location = new System.Drawing.Point(3, 6);
            this.lblSiteWebURL.Name = "lblSiteWebURL";
            this.lblSiteWebURL.Size = new System.Drawing.Size(89, 13);
            this.lblSiteWebURL.TabIndex = 0;
            this.lblSiteWebURL.Text = "* Site Web URL :";
            // 
            // txtWebURL
            // 
            this.txtWebURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tblMainFrame.SetColumnSpan(this.txtWebURL, 3);
            this.txtWebURL.Location = new System.Drawing.Point(193, 3);
            this.txtWebURL.MaxLength = 2000;
            this.txtWebURL.Name = "txtWebURL";
            this.txtWebURL.Size = new System.Drawing.Size(708, 20);
            this.txtWebURL.TabIndex = 1;
            // 
            // btnExportSiteSetup
            // 
            this.btnExportSiteSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportSiteSetup.Enabled = false;
            this.btnExportSiteSetup.Location = new System.Drawing.Point(732, 89);
            this.btnExportSiteSetup.Name = "btnExportSiteSetup";
            this.btnExportSiteSetup.Size = new System.Drawing.Size(100, 28);
            this.btnExportSiteSetup.TabIndex = 5;
            this.btnExportSiteSetup.Text = "Export Site Details To Site";
            this.btnExportSiteSetup.UseVisualStyleBackColor = true;
            this.btnExportSiteSetup.Click += new System.EventHandler(this.BtnExportSiteSetup_Click);
            // 
            // btnExportKeys
            // 
            this.btnExportKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportKeys.Location = new System.Drawing.Point(193, 34);
            this.btnExportKeys.Name = "btnExportKeys";
            this.btnExportKeys.Size = new System.Drawing.Size(168, 28);
            this.btnExportKeys.TabIndex = 2;
            this.btnExportKeys.Text = "Export Enterprise and Site Keys";
            this.btnExportKeys.UseVisualStyleBackColor = true;
            this.btnExportKeys.Click += new System.EventHandler(this.btnExportKeys_Click);
            // 
            // btnEnableDisableSite
            // 
            this.btnEnableDisableSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnableDisableSite.Location = new System.Drawing.Point(373, 34);
            this.btnEnableDisableSite.Name = "btnEnableDisableSite";
            this.btnEnableDisableSite.Size = new System.Drawing.Size(100, 28);
            this.btnEnableDisableSite.TabIndex = 3;
            this.btnEnableDisableSite.Text = "Enable Site";
            this.btnEnableDisableSite.UseVisualStyleBackColor = true;
            this.btnEnableDisableSite.Click += new System.EventHandler(this.btnEnableDisableSite_Click);
            // 
            // tblMainFrame
            // 
            this.tblMainFrame.ColumnCount = 4;
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 359F));
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tblMainFrame.Controls.Add(this.grpExportDetails, 1, 2);
            this.tblMainFrame.Controls.Add(this.btnExportSiteSetup, 3, 2);
            this.tblMainFrame.Controls.Add(this.lblSiteWebURL, 0, 0);
            this.tblMainFrame.Controls.Add(this.txtWebURL, 1, 0);
            this.tblMainFrame.Controls.Add(this.btnExportKeys, 1, 1);
            this.tblMainFrame.Controls.Add(this.btnEnableDisableSite, 2, 1);
            this.tblMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblMainFrame.Name = "tblMainFrame";
            this.tblMainFrame.RowCount = 3;
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.Size = new System.Drawing.Size(904, 120);
            this.tblMainFrame.TabIndex = 0;
            // 
            // grpExportDetails
            // 
            this.tblMainFrame.SetColumnSpan(this.grpExportDetails, 2);
            this.grpExportDetails.Controls.Add(this.tblExportChkBox);
            this.grpExportDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpExportDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpExportDetails.Location = new System.Drawing.Point(193, 68);
            this.grpExportDetails.Name = "grpExportDetails";
            this.grpExportDetails.Size = new System.Drawing.Size(533, 49);
            this.grpExportDetails.TabIndex = 4;
            this.grpExportDetails.TabStop = false;
            // 
            // tblExportChkBox
            // 
            this.tblExportChkBox.ColumnCount = 4;
            this.tblExportChkBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tblExportChkBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tblExportChkBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tblExportChkBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tblExportChkBox.Controls.Add(this.chkExportSiteSetup, 0, 0);
            this.tblExportChkBox.Controls.Add(this.chkExportSiteCalendar, 1, 0);
            this.tblExportChkBox.Controls.Add(this.chkExportModelsToSite, 2, 0);
            this.tblExportChkBox.Controls.Add(this.chkExportGames, 3, 0);
            this.tblExportChkBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblExportChkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblExportChkBox.Location = new System.Drawing.Point(3, 17);
            this.tblExportChkBox.Name = "tblExportChkBox";
            this.tblExportChkBox.RowCount = 1;
            this.tblExportChkBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblExportChkBox.Size = new System.Drawing.Size(527, 29);
            this.tblExportChkBox.TabIndex = 0;
            // 
            // chkExportSiteSetup
            // 
            this.chkExportSiteSetup.AutoSize = true;
            this.chkExportSiteSetup.Location = new System.Drawing.Point(3, 3);
            this.chkExportSiteSetup.Name = "chkExportSiteSetup";
            this.chkExportSiteSetup.Size = new System.Drawing.Size(120, 17);
            this.chkExportSiteSetup.TabIndex = 0;
            this.chkExportSiteSetup.Text = "chkExportSiteSetup";
            this.chkExportSiteSetup.UseVisualStyleBackColor = true;
            this.chkExportSiteSetup.CheckedChanged += new System.EventHandler(this.chkExportSiteSetup_CheckedChanged);
            // 
            // chkExportSiteCalendar
            // 
            this.chkExportSiteCalendar.AutoSize = true;
            this.chkExportSiteCalendar.Location = new System.Drawing.Point(133, 3);
            this.chkExportSiteCalendar.Name = "chkExportSiteCalendar";
            this.chkExportSiteCalendar.Size = new System.Drawing.Size(124, 17);
            this.chkExportSiteCalendar.TabIndex = 1;
            this.chkExportSiteCalendar.Text = "chkExportSiteCalendar";
            this.chkExportSiteCalendar.UseVisualStyleBackColor = true;
            this.chkExportSiteCalendar.CheckedChanged += new System.EventHandler(this.chkExportSiteSetup_CheckedChanged);
            // 
            // chkExportModelsToSite
            // 
            this.chkExportModelsToSite.AutoSize = true;
            this.chkExportModelsToSite.Location = new System.Drawing.Point(263, 3);
            this.chkExportModelsToSite.Name = "chkExportModelsToSite";
            this.chkExportModelsToSite.Size = new System.Drawing.Size(124, 17);
            this.chkExportModelsToSite.TabIndex = 2;
            this.chkExportModelsToSite.Text = "chkExportModelsToSite";
            this.chkExportModelsToSite.UseVisualStyleBackColor = true;
            this.chkExportModelsToSite.CheckedChanged += new System.EventHandler(this.chkExportSiteSetup_CheckedChanged);
            // 
            // chkExportGames
            // 
            this.chkExportGames.AutoSize = true;
            this.chkExportGames.Location = new System.Drawing.Point(393, 3);
            this.chkExportGames.Name = "chkExportGames";
            this.chkExportGames.Size = new System.Drawing.Size(107, 17);
            this.chkExportGames.TabIndex = 3;
            this.chkExportGames.Text = "chkExportGames";
            this.chkExportGames.UseVisualStyleBackColor = true;
            this.chkExportGames.CheckedChanged += new System.EventHandler(this.chkExportSiteSetup_CheckedChanged);
            // 
            // ucComms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblMainFrame);
            this.Name = "ucComms";
            this.Size = new System.Drawing.Size(904, 120);
            this.Load += new System.EventHandler(this.ucComms_Load);
            this.tblMainFrame.ResumeLayout(false);
            this.tblMainFrame.PerformLayout();
            this.grpExportDetails.ResumeLayout(false);
            this.tblExportChkBox.ResumeLayout(false);
            this.tblExportChkBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSiteWebURL;
        private System.Windows.Forms.TextBox txtWebURL;
        private System.Windows.Forms.Button btnExportSiteSetup;
        private System.Windows.Forms.Button btnExportKeys;
        private System.Windows.Forms.Button btnEnableDisableSite;
        private System.Windows.Forms.TableLayoutPanel tblMainFrame;
        private System.Windows.Forms.CheckBox chkExportSiteSetup;
        private System.Windows.Forms.TableLayoutPanel tblExportChkBox;
        private System.Windows.Forms.CheckBox chkExportGames;
        private System.Windows.Forms.CheckBox chkExportSiteCalendar;
        private System.Windows.Forms.CheckBox chkExportModelsToSite;
        private System.Windows.Forms.GroupBox grpExportDetails;
    }
}
