namespace BMC.EnterpriseClient.Views
{
    partial class ucAdminSite
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
            this.btnApply = new System.Windows.Forms.Button();
            this.tblMainFram = new System.Windows.Forms.TableLayoutPanel();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.SSTab1 = new System.Windows.Forms.TabControl();
            this.tbDetails1 = new System.Windows.Forms.TabPage();
            this.details1UC1 = new Details1.Details1UC();
            this.tbDetails2 = new System.Windows.Forms.TabPage();
            this.tblDetailsAndOwnerMainFram = new System.Windows.Forms.TableLayoutPanel();
            this.grpOwner = new System.Windows.Forms.GroupBox();
            this.ucowner1 = new BMC.EnterpriseClient.Views.ucowner();
            this.grpDetails2 = new System.Windows.Forms.GroupBox();
            this.details2UC1 = new Details2.Details2UC();
            this.tbOwner = new System.Windows.Forms.TabPage();
            this.tblOwnerMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.grpNotes = new System.Windows.Forms.GroupBox();
            this.ucnotes1 = new BMC.EnterpriseClient.Ucnotes();
            this.grpImage = new System.Windows.Forms.GroupBox();
            this.ucimages1 = new BMC.EnterpriseClient.Views.Ucimages();
            this.tbDefaults = new System.Windows.Forms.TabPage();
            this.ucDefaults21 = new BMC.EnterpriseClient.Views.UcDefaults2();
            this.tbAreas = new System.Windows.Forms.TabPage();
            this.areas1 = new BMC.EnterpriseClient.Views.Areas();
            this.tbZonePos = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.zonePosition1 = new BMC.EnterpriseClient.Views.ZonePosition();
            this.tbComms = new System.Windows.Forms.TabPage();
            this.tblCommsMaintenance = new System.Windows.Forms.TableLayoutPanel();
            this.grpComms = new System.Windows.Forms.GroupBox();
            this.ucComms1 = new BMC.EnterpriseClient.Views.ucComms();
            this.grpMaintenanace = new System.Windows.Forms.GroupBox();
            this.ucSiteMaintenance1 = new BMC.EnterpriseClient.Views.ucSiteMaintenance();
            this.tbNotes = new System.Windows.Forms.TabPage();
            this.tbImages = new System.Windows.Forms.TabPage();
            this.tbMaintenance = new System.Windows.Forms.TabPage();
            this.tbAFTSettings = new System.Windows.Forms.TabPage();
            this.tblAftAndGameCapping = new System.Windows.Forms.TableLayoutPanel();
            this.tblAftAndGameCappingSetting = new System.Windows.Forms.TableLayoutPanel();
            this.grpAftSetting = new System.Windows.Forms.GroupBox();
            this.tblAftContainer = new System.Windows.Forms.TableLayoutPanel();
            this.ucAftsettings1 = new BMC.EnterpriseClient.Views.UcAftsettings();
            this.grpGameCapping = new System.Windows.Forms.GroupBox();
            this.ucGameCapping1 = new BMC.EnterpriseClient.Views.UCGameCapping();
            this.tabGameCapping = new System.Windows.Forms.TabPage();
            this.tblLowerButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblMainFram.SuspendLayout();
            this.SSTab1.SuspendLayout();
            this.tbDetails1.SuspendLayout();
            this.tbDetails2.SuspendLayout();
            this.tblDetailsAndOwnerMainFram.SuspendLayout();
            this.grpOwner.SuspendLayout();
            this.grpDetails2.SuspendLayout();
            this.tbOwner.SuspendLayout();
            this.tblOwnerMainFrame.SuspendLayout();
            this.grpNotes.SuspendLayout();
            this.grpImage.SuspendLayout();
            this.tbDefaults.SuspendLayout();
            this.tbAreas.SuspendLayout();
            this.tbZonePos.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tbComms.SuspendLayout();
            this.tblCommsMaintenance.SuspendLayout();
            this.grpComms.SuspendLayout();
            this.grpMaintenanace.SuspendLayout();
            this.tbAFTSettings.SuspendLayout();
            this.tblAftAndGameCapping.SuspendLayout();
            this.tblAftAndGameCappingSetting.SuspendLayout();
            this.grpAftSetting.SuspendLayout();
            this.tblAftContainer.SuspendLayout();
            this.grpGameCapping.SuspendLayout();
            this.tblLowerButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(777, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 28);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tblMainFram
            // 
            this.tblMainFram.ColumnCount = 1;
            this.tblMainFram.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFram.Controls.Add(this.lblErrorMessage, 0, 0);
            this.tblMainFram.Controls.Add(this.SSTab1, 0, 1);
            this.tblMainFram.Controls.Add(this.tblLowerButtons, 0, 2);
            this.tblMainFram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFram.Location = new System.Drawing.Point(0, 0);
            this.tblMainFram.Name = "tblMainFram";
            this.tblMainFram.RowCount = 3;
            this.tblMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMainFram.Size = new System.Drawing.Size(1006, 781);
            this.tblMainFram.TabIndex = 0;
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblErrorMessage.AutoSize = true;
            this.lblErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Maroon;
            this.lblErrorMessage.Location = new System.Drawing.Point(3, 2);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(1000, 15);
            this.lblErrorMessage.TabIndex = 0;
            this.lblErrorMessage.Text = "lblErrorMessage";
            this.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.SSTab1.Controls.Add(this.tabGameCapping);
            this.SSTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSTab1.ItemSize = new System.Drawing.Size(53, 20);
            this.SSTab1.Location = new System.Drawing.Point(3, 23);
            this.SSTab1.Multiline = true;
            this.SSTab1.Name = "SSTab1";
            this.SSTab1.Padding = new System.Drawing.Point(26, 3);
            this.SSTab1.SelectedIndex = 0;
            this.SSTab1.Size = new System.Drawing.Size(1000, 715);
            this.SSTab1.TabIndex = 0;
            // 
            // tbDetails1
            // 
            this.tbDetails1.AutoScroll = true;
            this.tbDetails1.AutoScrollMinSize = new System.Drawing.Size(800, 600);
            this.tbDetails1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbDetails1.Controls.Add(this.details1UC1);
            this.tbDetails1.Location = new System.Drawing.Point(4, 44);
            this.tbDetails1.Name = "tbDetails1";
            this.tbDetails1.Padding = new System.Windows.Forms.Padding(3);
            this.tbDetails1.Size = new System.Drawing.Size(992, 667);
            this.tbDetails1.TabIndex = 0;
            this.tbDetails1.Text = "Details 1";
            // 
            // details1UC1
            // 
            this.details1UC1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.details1UC1.DlgDetail1UC = null;
            this.details1UC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.details1UC1.Location = new System.Drawing.Point(3, 3);
            this.details1UC1.Name = "details1UC1";
            this.details1UC1.Size = new System.Drawing.Size(986, 661);
            this.details1UC1.TabIndex = 0;
            // 
            // tbDetails2
            // 
            this.tbDetails2.AutoScroll = true;
            this.tbDetails2.AutoScrollMinSize = new System.Drawing.Size(800, 600);
            this.tbDetails2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbDetails2.Controls.Add(this.tblDetailsAndOwnerMainFram);
            this.tbDetails2.Location = new System.Drawing.Point(4, 44);
            this.tbDetails2.Name = "tbDetails2";
            this.tbDetails2.Padding = new System.Windows.Forms.Padding(3);
            this.tbDetails2.Size = new System.Drawing.Size(992, 667);
            this.tbDetails2.TabIndex = 1;
            this.tbDetails2.Text = "Details 2";
            // 
            // tblDetailsAndOwnerMainFram
            // 
            this.tblDetailsAndOwnerMainFram.ColumnCount = 1;
            this.tblDetailsAndOwnerMainFram.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetailsAndOwnerMainFram.Controls.Add(this.grpOwner, 0, 1);
            this.tblDetailsAndOwnerMainFram.Controls.Add(this.grpDetails2, 0, 0);
            this.tblDetailsAndOwnerMainFram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDetailsAndOwnerMainFram.Location = new System.Drawing.Point(3, 3);
            this.tblDetailsAndOwnerMainFram.Name = "tblDetailsAndOwnerMainFram";
            this.tblDetailsAndOwnerMainFram.RowCount = 2;
            this.tblDetailsAndOwnerMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 425F));
            this.tblDetailsAndOwnerMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.tblDetailsAndOwnerMainFram.Size = new System.Drawing.Size(986, 661);
            this.tblDetailsAndOwnerMainFram.TabIndex = 0;
            // 
            // grpOwner
            // 
            this.grpOwner.Controls.Add(this.ucowner1);
            this.grpOwner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOwner.Location = new System.Drawing.Point(3, 428);
            this.grpOwner.Name = "grpOwner";
            this.grpOwner.Size = new System.Drawing.Size(980, 230);
            this.grpOwner.TabIndex = 1;
            this.grpOwner.TabStop = false;
            this.grpOwner.Text = "grpOwner";
            // 
            // ucowner1
            // 
            this.ucowner1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ucowner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucowner1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucowner1.Location = new System.Drawing.Point(3, 17);
            this.ucowner1.Name = "ucowner1";
            this.ucowner1.Size = new System.Drawing.Size(974, 210);
            this.ucowner1.TabIndex = 0;
            // 
            // grpDetails2
            // 
            this.grpDetails2.Controls.Add(this.details2UC1);
            this.grpDetails2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetails2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDetails2.Location = new System.Drawing.Point(3, 3);
            this.grpDetails2.Name = "grpDetails2";
            this.grpDetails2.Size = new System.Drawing.Size(980, 419);
            this.grpDetails2.TabIndex = 0;
            this.grpDetails2.TabStop = false;
            this.grpDetails2.Text = "grpDetails2";
            // 
            // details2UC1
            // 
            this.details2UC1.AutoScroll = true;
            this.details2UC1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.details2UC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.details2UC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.details2UC1.Location = new System.Drawing.Point(3, 16);
            this.details2UC1.Name = "details2UC1";
            this.details2UC1.Size = new System.Drawing.Size(974, 400);
            this.details2UC1.TabIndex = 0;
            // 
            // tbOwner
            // 
            this.tbOwner.AutoScroll = true;
            this.tbOwner.AutoScrollMinSize = new System.Drawing.Size(450, 600);
            this.tbOwner.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbOwner.Controls.Add(this.tblOwnerMainFrame);
            this.tbOwner.Location = new System.Drawing.Point(4, 44);
            this.tbOwner.Name = "tbOwner";
            this.tbOwner.Size = new System.Drawing.Size(992, 667);
            this.tbOwner.TabIndex = 2;
            this.tbOwner.Text = "Owner";
            this.tbOwner.UseVisualStyleBackColor = true;
            // 
            // tblOwnerMainFrame
            // 
            this.tblOwnerMainFrame.AutoSize = true;
            this.tblOwnerMainFrame.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblOwnerMainFrame.ColumnCount = 2;
            this.tblOwnerMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOwnerMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOwnerMainFrame.Controls.Add(this.grpNotes, 0, 2);
            this.tblOwnerMainFrame.Controls.Add(this.grpImage, 0, 0);
            this.tblOwnerMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOwnerMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblOwnerMainFrame.Name = "tblOwnerMainFrame";
            this.tblOwnerMainFrame.RowCount = 5;
            this.tblOwnerMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tblOwnerMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblOwnerMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblOwnerMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblOwnerMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOwnerMainFrame.Size = new System.Drawing.Size(992, 667);
            this.tblOwnerMainFrame.TabIndex = 0;
            // 
            // grpNotes
            // 
            this.tblOwnerMainFrame.SetColumnSpan(this.grpNotes, 2);
            this.grpNotes.Controls.Add(this.ucnotes1);
            this.grpNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpNotes.Location = new System.Drawing.Point(3, 203);
            this.grpNotes.Name = "grpNotes";
            this.tblOwnerMainFrame.SetRowSpan(this.grpNotes, 3);
            this.grpNotes.Size = new System.Drawing.Size(986, 461);
            this.grpNotes.TabIndex = 1;
            this.grpNotes.TabStop = false;
            this.grpNotes.Text = "grpNotes";
            // 
            // ucnotes1
            // 
            this.ucnotes1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ucnotes1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucnotes1.Location = new System.Drawing.Point(3, 17);
            this.ucnotes1.Name = "ucnotes1";
            this.ucnotes1.Size = new System.Drawing.Size(980, 441);
            this.ucnotes1.TabIndex = 0;
            // 
            // grpImage
            // 
            this.tblOwnerMainFrame.SetColumnSpan(this.grpImage, 2);
            this.grpImage.Controls.Add(this.ucimages1);
            this.grpImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpImage.Location = new System.Drawing.Point(3, 3);
            this.grpImage.Name = "grpImage";
            this.grpImage.Size = new System.Drawing.Size(986, 184);
            this.grpImage.TabIndex = 0;
            this.grpImage.TabStop = false;
            this.grpImage.Text = "grpImage";
            // 
            // ucimages1
            // 
            this.ucimages1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ucimages1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucimages1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucimages1.Location = new System.Drawing.Point(3, 17);
            this.ucimages1.Name = "ucimages1";
            this.ucimages1.Size = new System.Drawing.Size(980, 164);
            this.ucimages1.TabIndex = 0;
            // 
            // tbDefaults
            // 
            this.tbDefaults.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbDefaults.Controls.Add(this.ucDefaults21);
            this.tbDefaults.Location = new System.Drawing.Point(4, 44);
            this.tbDefaults.Name = "tbDefaults";
            this.tbDefaults.Size = new System.Drawing.Size(992, 667);
            this.tbDefaults.TabIndex = 3;
            this.tbDefaults.Text = "Defaults";
            this.tbDefaults.UseVisualStyleBackColor = true;
            // 
            // ucDefaults21
            // 
            this.ucDefaults21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDefaults21.Location = new System.Drawing.Point(0, 0);
            this.ucDefaults21.Name = "ucDefaults21";
            this.ucDefaults21.Size = new System.Drawing.Size(992, 667);
            this.ucDefaults21.TabIndex = 0;
            // 
            // tbAreas
            // 
            this.tbAreas.AutoScroll = true;
            this.tbAreas.AutoScrollMinSize = new System.Drawing.Size(800, 600);
            this.tbAreas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbAreas.Controls.Add(this.areas1);
            this.tbAreas.Location = new System.Drawing.Point(4, 44);
            this.tbAreas.Name = "tbAreas";
            this.tbAreas.Size = new System.Drawing.Size(992, 667);
            this.tbAreas.TabIndex = 4;
            this.tbAreas.Text = "Areas";
            this.tbAreas.UseVisualStyleBackColor = true;
            // 
            // areas1
            // 
            this.areas1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.areas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areas1.Location = new System.Drawing.Point(0, 0);
            this.areas1.Name = "areas1";
            this.areas1.Size = new System.Drawing.Size(992, 667);
            this.areas1.TabIndex = 0;
            // 
            // tbZonePos
            // 
            this.tbZonePos.AutoScroll = true;
            this.tbZonePos.AutoScrollMinSize = new System.Drawing.Size(800, 600);
            this.tbZonePos.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbZonePos.Controls.Add(this.panel2);
            this.tbZonePos.Location = new System.Drawing.Point(4, 44);
            this.tbZonePos.Name = "tbZonePos";
            this.tbZonePos.Size = new System.Drawing.Size(992, 667);
            this.tbZonePos.TabIndex = 5;
            this.tbZonePos.Text = "Zone/Pos";
            this.tbZonePos.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.zonePosition1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(992, 667);
            this.panel2.TabIndex = 1;
            // 
            // zonePosition1
            // 
            this.zonePosition1.AutoScroll = true;
            this.zonePosition1.AutoSize = true;
            this.zonePosition1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.zonePosition1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zonePosition1.Location = new System.Drawing.Point(0, 0);
            this.zonePosition1.Name = "zonePosition1";
            this.zonePosition1.Size = new System.Drawing.Size(992, 667);
            this.zonePosition1.TabIndex = 0;
            // 
            // tbComms
            // 
            this.tbComms.AutoScroll = true;
            this.tbComms.AutoScrollMinSize = new System.Drawing.Size(800, 600);
            this.tbComms.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbComms.Controls.Add(this.tblCommsMaintenance);
            this.tbComms.Location = new System.Drawing.Point(4, 44);
            this.tbComms.Name = "tbComms";
            this.tbComms.Size = new System.Drawing.Size(992, 667);
            this.tbComms.TabIndex = 6;
            this.tbComms.Text = "Comms";
            this.tbComms.UseVisualStyleBackColor = true;
            // 
            // tblCommsMaintenance
            // 
            this.tblCommsMaintenance.ColumnCount = 1;
            this.tblCommsMaintenance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCommsMaintenance.Controls.Add(this.grpComms, 0, 0);
            this.tblCommsMaintenance.Controls.Add(this.grpMaintenanace, 0, 2);
            this.tblCommsMaintenance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCommsMaintenance.Location = new System.Drawing.Point(0, 0);
            this.tblCommsMaintenance.Name = "tblCommsMaintenance";
            this.tblCommsMaintenance.RowCount = 3;
            this.tblCommsMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tblCommsMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblCommsMaintenance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCommsMaintenance.Size = new System.Drawing.Size(992, 667);
            this.tblCommsMaintenance.TabIndex = 0;
            // 
            // grpComms
            // 
            this.grpComms.Controls.Add(this.ucComms1);
            this.grpComms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpComms.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpComms.Location = new System.Drawing.Point(3, 3);
            this.grpComms.Name = "grpComms";
            this.grpComms.Size = new System.Drawing.Size(986, 134);
            this.grpComms.TabIndex = 0;
            this.grpComms.TabStop = false;
            this.grpComms.Text = "grpComms";
            // 
            // ucComms1
            // 
            this.ucComms1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucComms1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucComms1.Location = new System.Drawing.Point(3, 17);
            this.ucComms1.Name = "ucComms1";
            this.ucComms1.SiteID = 0;
            this.ucComms1.SiteStatus = false;
            this.ucComms1.Size = new System.Drawing.Size(980, 114);
            this.ucComms1.TabIndex = 0;
            // 
            // grpMaintenanace
            // 
            this.grpMaintenanace.Controls.Add(this.ucSiteMaintenance1);
            this.grpMaintenanace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMaintenanace.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMaintenanace.Location = new System.Drawing.Point(3, 153);
            this.grpMaintenanace.Name = "grpMaintenanace";
            this.grpMaintenanace.Size = new System.Drawing.Size(986, 511);
            this.grpMaintenanace.TabIndex = 1;
            this.grpMaintenanace.TabStop = false;
            this.grpMaintenanace.Text = "grpMaintenanace";
            // 
            // ucSiteMaintenance1
            // 
            this.ucSiteMaintenance1.AutoScroll = true;
            this.ucSiteMaintenance1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSiteMaintenance1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucSiteMaintenance1.Location = new System.Drawing.Point(3, 17);
            this.ucSiteMaintenance1.Name = "ucSiteMaintenance1";
            this.ucSiteMaintenance1.Size = new System.Drawing.Size(980, 491);
            this.ucSiteMaintenance1.TabIndex = 0;
            // 
            // tbNotes
            // 
            this.tbNotes.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbNotes.Location = new System.Drawing.Point(4, 44);
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.Size = new System.Drawing.Size(992, 667);
            this.tbNotes.TabIndex = 7;
            this.tbNotes.Text = "Notes";
            this.tbNotes.UseVisualStyleBackColor = true;
            // 
            // tbImages
            // 
            this.tbImages.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbImages.Location = new System.Drawing.Point(4, 44);
            this.tbImages.Name = "tbImages";
            this.tbImages.Size = new System.Drawing.Size(992, 667);
            this.tbImages.TabIndex = 8;
            this.tbImages.Text = "Images";
            this.tbImages.UseVisualStyleBackColor = true;
            // 
            // tbMaintenance
            // 
            this.tbMaintenance.Location = new System.Drawing.Point(4, 44);
            this.tbMaintenance.Name = "tbMaintenance";
            this.tbMaintenance.Size = new System.Drawing.Size(992, 667);
            this.tbMaintenance.TabIndex = 9;
            this.tbMaintenance.Text = "Maintenance";
            this.tbMaintenance.UseVisualStyleBackColor = true;
            // 
            // tbAFTSettings
            // 
            this.tbAFTSettings.AutoScroll = true;
            this.tbAFTSettings.AutoScrollMinSize = new System.Drawing.Size(800, 650);
            this.tbAFTSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbAFTSettings.Controls.Add(this.tblAftAndGameCapping);
            this.tbAFTSettings.Location = new System.Drawing.Point(4, 44);
            this.tbAFTSettings.Name = "tbAFTSettings";
            this.tbAFTSettings.Size = new System.Drawing.Size(992, 667);
            this.tbAFTSettings.TabIndex = 10;
            this.tbAFTSettings.Text = "AFT Settings";
            // 
            // tblAftAndGameCapping
            // 
            this.tblAftAndGameCapping.ColumnCount = 1;
            this.tblAftAndGameCapping.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAftAndGameCapping.Controls.Add(this.tblAftAndGameCappingSetting, 0, 0);
            this.tblAftAndGameCapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAftAndGameCapping.Location = new System.Drawing.Point(0, 0);
            this.tblAftAndGameCapping.Name = "tblAftAndGameCapping";
            this.tblAftAndGameCapping.RowCount = 1;
            this.tblAftAndGameCapping.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAftAndGameCapping.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 650F));
            this.tblAftAndGameCapping.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 650F));
            this.tblAftAndGameCapping.Size = new System.Drawing.Size(992, 667);
            this.tblAftAndGameCapping.TabIndex = 0;
            // 
            // tblAftAndGameCappingSetting
            // 
            this.tblAftAndGameCappingSetting.ColumnCount = 2;
            this.tblAftAndGameCappingSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblAftAndGameCappingSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblAftAndGameCappingSetting.Controls.Add(this.grpAftSetting, 0, 0);
            this.tblAftAndGameCappingSetting.Controls.Add(this.grpGameCapping, 1, 0);
            this.tblAftAndGameCappingSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAftAndGameCappingSetting.Location = new System.Drawing.Point(3, 3);
            this.tblAftAndGameCappingSetting.Name = "tblAftAndGameCappingSetting";
            this.tblAftAndGameCappingSetting.RowCount = 1;
            this.tblAftAndGameCappingSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAftAndGameCappingSetting.Size = new System.Drawing.Size(986, 661);
            this.tblAftAndGameCappingSetting.TabIndex = 0;
            // 
            // grpAftSetting
            // 
            this.grpAftSetting.Controls.Add(this.tblAftContainer);
            this.grpAftSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAftSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAftSetting.Location = new System.Drawing.Point(3, 3);
            this.grpAftSetting.Name = "grpAftSetting";
            this.grpAftSetting.Size = new System.Drawing.Size(388, 655);
            this.grpAftSetting.TabIndex = 1;
            this.grpAftSetting.TabStop = false;
            this.grpAftSetting.Text = "grpAftSetting";
            // 
            // tblAftContainer
            // 
            this.tblAftContainer.ColumnCount = 2;
            this.tblAftContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 99.0099F));
            this.tblAftContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.990099F));
            this.tblAftContainer.Controls.Add(this.ucAftsettings1, 0, 0);
            this.tblAftContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAftContainer.Location = new System.Drawing.Point(3, 17);
            this.tblAftContainer.Name = "tblAftContainer";
            this.tblAftContainer.RowCount = 1;
            this.tblAftContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAftContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 618F));
            this.tblAftContainer.Size = new System.Drawing.Size(382, 635);
            this.tblAftContainer.TabIndex = 1;
            // 
            // ucAftsettings1
            // 
            this.ucAftsettings1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAftsettings1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucAftsettings1.Location = new System.Drawing.Point(4, 3);
            this.ucAftsettings1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ucAftsettings1.Name = "ucAftsettings1";
            this.ucAftsettings1.Size = new System.Drawing.Size(370, 629);
            this.ucAftsettings1.TabIndex = 0;
            // 
            // grpGameCapping
            // 
            this.grpGameCapping.Controls.Add(this.ucGameCapping1);
            this.grpGameCapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGameCapping.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGameCapping.Location = new System.Drawing.Point(397, 3);
            this.grpGameCapping.Name = "grpGameCapping";
            this.grpGameCapping.Size = new System.Drawing.Size(586, 655);
            this.grpGameCapping.TabIndex = 1;
            this.grpGameCapping.TabStop = false;
            this.grpGameCapping.Text = "grpGameCapping";
            // 
            // ucGameCapping1
            // 
            this.ucGameCapping1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ucGameCapping1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGameCapping1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucGameCapping1.Location = new System.Drawing.Point(3, 17);
            this.ucGameCapping1.Name = "ucGameCapping1";
            this.ucGameCapping1.Size = new System.Drawing.Size(580, 635);
            this.ucGameCapping1.TabIndex = 0;
            // 
            // tabGameCapping
            // 
            this.tabGameCapping.Location = new System.Drawing.Point(4, 44);
            this.tabGameCapping.Name = "tabGameCapping";
            this.tabGameCapping.Padding = new System.Windows.Forms.Padding(3);
            this.tabGameCapping.Size = new System.Drawing.Size(992, 667);
            this.tabGameCapping.TabIndex = 11;
            this.tabGameCapping.Text = "Game Capping Settings";
            this.tabGameCapping.UseVisualStyleBackColor = true;
            // 
            // tblLowerButtons
            // 
            this.tblLowerButtons.ColumnCount = 3;
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowerButtons.Controls.Add(this.btnApply, 1, 0);
            this.tblLowerButtons.Controls.Add(this.btnClose, 2, 0);
            this.tblLowerButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLowerButtons.Location = new System.Drawing.Point(3, 744);
            this.tblLowerButtons.Name = "tblLowerButtons";
            this.tblLowerButtons.RowCount = 1;
            this.tblLowerButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowerButtons.Size = new System.Drawing.Size(1000, 34);
            this.tblLowerButtons.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(897, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucAdminSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblMainFram);
            this.Name = "ucAdminSite";
            this.Size = new System.Drawing.Size(1006, 781);
            this.Load += new System.EventHandler(this.ucAdminSite_Load);
            this.tblMainFram.ResumeLayout(false);
            this.tblMainFram.PerformLayout();
            this.SSTab1.ResumeLayout(false);
            this.tbDetails1.ResumeLayout(false);
            this.tbDetails2.ResumeLayout(false);
            this.tblDetailsAndOwnerMainFram.ResumeLayout(false);
            this.grpOwner.ResumeLayout(false);
            this.grpDetails2.ResumeLayout(false);
            this.tbOwner.ResumeLayout(false);
            this.tbOwner.PerformLayout();
            this.tblOwnerMainFrame.ResumeLayout(false);
            this.grpNotes.ResumeLayout(false);
            this.grpImage.ResumeLayout(false);
            this.tbDefaults.ResumeLayout(false);
            this.tbAreas.ResumeLayout(false);
            this.tbZonePos.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tbComms.ResumeLayout(false);
            this.tblCommsMaintenance.ResumeLayout(false);
            this.grpComms.ResumeLayout(false);
            this.grpMaintenanace.ResumeLayout(false);
            this.tbAFTSettings.ResumeLayout(false);
            this.tblAftAndGameCapping.ResumeLayout(false);
            this.tblAftAndGameCappingSetting.ResumeLayout(false);
            this.grpAftSetting.ResumeLayout(false);
            this.tblAftContainer.ResumeLayout(false);
            this.grpGameCapping.ResumeLayout(false);
            this.tblLowerButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TableLayoutPanel tblMainFram;
        private System.Windows.Forms.TableLayoutPanel tblLowerButtons;
        private System.Windows.Forms.TabControl SSTab1;
        private System.Windows.Forms.TabPage tbDetails1;
        private Details1.Details1UC details1UC1;
        private System.Windows.Forms.TabPage tbDetails2;
        private Details2.Details2UC details2UC1;
        private System.Windows.Forms.TabPage tbOwner;
        private System.Windows.Forms.TableLayoutPanel tblOwnerMainFrame;
        private System.Windows.Forms.GroupBox grpImage;
        private Ucimages ucimages1;
        private System.Windows.Forms.GroupBox grpNotes;
        private Ucnotes ucnotes1;
        private System.Windows.Forms.TabPage tbDefaults;
        private UcDefaults2 ucDefaults21;
        private System.Windows.Forms.TabPage tbAreas;
        private Areas areas1;
        private System.Windows.Forms.TabPage tbZonePos;
        private System.Windows.Forms.Panel panel2;
        private ZonePosition zonePosition1;
        private System.Windows.Forms.TabPage tbComms;
        private System.Windows.Forms.TableLayoutPanel tblCommsMaintenance;
        private System.Windows.Forms.GroupBox grpComms;
        private ucComms ucComms1;
        private System.Windows.Forms.GroupBox grpMaintenanace;
        private ucSiteMaintenance ucSiteMaintenance1;
        private System.Windows.Forms.TabPage tbNotes;
        private System.Windows.Forms.TabPage tbImages;
        private System.Windows.Forms.TabPage tbMaintenance;
        private System.Windows.Forms.TabPage tbAFTSettings;
        private System.Windows.Forms.TableLayoutPanel tblAftAndGameCapping;
        private System.Windows.Forms.TableLayoutPanel tblAftAndGameCappingSetting;
        private System.Windows.Forms.GroupBox grpAftSetting;
        private UcAftsettings ucAftsettings1;
        private System.Windows.Forms.GroupBox grpGameCapping;
        private UCGameCapping ucGameCapping1;
        private System.Windows.Forms.TabPage tabGameCapping;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tblDetailsAndOwnerMainFram;
        private System.Windows.Forms.GroupBox grpDetails2;
        private System.Windows.Forms.GroupBox grpOwner;
        private ucowner ucowner1;
        private System.Windows.Forms.TableLayoutPanel tblAftContainer;
    }
}
