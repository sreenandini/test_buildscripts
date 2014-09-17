namespace BMC.EnterpriseClient.Views.ServiceCalls
{
    partial class ServiceAdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceAdminForm));
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rbtnSLAContracts = new System.Windows.Forms.RadioButton();
            this.imglstLargeIcons = new System.Windows.Forms.ImageList(this.components);
            this.rbtnSource = new System.Windows.Forms.RadioButton();
            this.rbtnFixCodes = new System.Windows.Forms.RadioButton();
            this.rbtnFaultCodes = new System.Windows.Forms.RadioButton();
            this.grpContent = new System.Windows.Forms.GroupBox();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.vldCustom = new BMC.CoreLib.Win32.Validation.CustomValidator(this.components);
            this.tblContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpContent.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 2;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tblContainer.Controls.Add(this.grpContent, 1, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 1;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Size = new System.Drawing.Size(623, 543);
            this.tblContainer.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.rbtnSLAContracts, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.rbtnSource, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.rbtnFixCodes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rbtnFaultCodes, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(135, 537);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // rbtnSLAContracts
            // 
            this.rbtnSLAContracts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbtnSLAContracts.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnSLAContracts.ImageKey = "FaultAdmin4.ico";
            this.rbtnSLAContracts.ImageList = this.imglstLargeIcons;
            this.rbtnSLAContracts.Location = new System.Drawing.Point(19, 372);
            this.rbtnSLAContracts.Margin = new System.Windows.Forms.Padding(12);
            this.rbtnSLAContracts.Name = "rbtnSLAContracts";
            this.rbtnSLAContracts.Size = new System.Drawing.Size(96, 96);
            this.rbtnSLAContracts.TabIndex = 3;
            this.rbtnSLAContracts.Tag = "3";
            this.rbtnSLAContracts.Text = "SLA Contracts";
            this.rbtnSLAContracts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnSLAContracts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.rbtnSLAContracts.UseVisualStyleBackColor = true;
            this.rbtnSLAContracts.CheckedChanged += new System.EventHandler(this.OnSource_CheckedChanged);
            // 
            // imglstLargeIcons
            // 
            this.imglstLargeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstLargeIcons.ImageStream")));
            this.imglstLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstLargeIcons.Images.SetKeyName(0, "FaultAdmin1.ico");
            this.imglstLargeIcons.Images.SetKeyName(1, "FaultAdmin2.ico");
            this.imglstLargeIcons.Images.SetKeyName(2, "FaultAdmin3.ico");
            this.imglstLargeIcons.Images.SetKeyName(3, "FaultAdmin4.ico");
            // 
            // rbtnSource
            // 
            this.rbtnSource.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbtnSource.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnSource.ImageKey = "FaultAdmin3.ico";
            this.rbtnSource.ImageList = this.imglstLargeIcons;
            this.rbtnSource.Location = new System.Drawing.Point(19, 252);
            this.rbtnSource.Margin = new System.Windows.Forms.Padding(12);
            this.rbtnSource.Name = "rbtnSource";
            this.rbtnSource.Size = new System.Drawing.Size(96, 96);
            this.rbtnSource.TabIndex = 2;
            this.rbtnSource.Tag = "2";
            this.rbtnSource.Text = "Source";
            this.rbtnSource.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnSource.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.rbtnSource.UseVisualStyleBackColor = true;
            this.rbtnSource.CheckedChanged += new System.EventHandler(this.OnSource_CheckedChanged);
            // 
            // rbtnFixCodes
            // 
            this.rbtnFixCodes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbtnFixCodes.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnFixCodes.ImageKey = "FaultAdmin2.ico";
            this.rbtnFixCodes.ImageList = this.imglstLargeIcons;
            this.rbtnFixCodes.Location = new System.Drawing.Point(19, 132);
            this.rbtnFixCodes.Margin = new System.Windows.Forms.Padding(12);
            this.rbtnFixCodes.Name = "rbtnFixCodes";
            this.rbtnFixCodes.Size = new System.Drawing.Size(96, 96);
            this.rbtnFixCodes.TabIndex = 1;
            this.rbtnFixCodes.Tag = "1";
            this.rbtnFixCodes.Text = "Fix Codes";
            this.rbtnFixCodes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnFixCodes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.rbtnFixCodes.UseVisualStyleBackColor = true;
            this.rbtnFixCodes.CheckedChanged += new System.EventHandler(this.OnSource_CheckedChanged);
            // 
            // rbtnFaultCodes
            // 
            this.rbtnFaultCodes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbtnFaultCodes.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnFaultCodes.ImageKey = "FaultAdmin1.ico";
            this.rbtnFaultCodes.ImageList = this.imglstLargeIcons;
            this.rbtnFaultCodes.Location = new System.Drawing.Point(19, 12);
            this.rbtnFaultCodes.Margin = new System.Windows.Forms.Padding(12);
            this.rbtnFaultCodes.Name = "rbtnFaultCodes";
            this.rbtnFaultCodes.Size = new System.Drawing.Size(96, 96);
            this.rbtnFaultCodes.TabIndex = 0;
            this.rbtnFaultCodes.Tag = "0";
            this.rbtnFaultCodes.Text = "Fault Codes";
            this.rbtnFaultCodes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnFaultCodes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.rbtnFaultCodes.UseVisualStyleBackColor = true;
            this.rbtnFaultCodes.CheckedChanged += new System.EventHandler(this.OnSource_CheckedChanged);
            // 
            // grpContent
            // 
            this.grpContent.Controls.Add(this.tblContent);
            this.grpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpContent.Location = new System.Drawing.Point(144, 3);
            this.grpContent.Name = "grpContent";
            this.grpContent.Size = new System.Drawing.Size(476, 537);
            this.grpContent.TabIndex = 1;
            this.grpContent.TabStop = false;
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 1;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Controls.Add(this.pnlContent, 0, 0);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(3, 16);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 1;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Size = new System.Drawing.Size(470, 518);
            this.tblContent.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(3, 3);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(464, 512);
            this.pnlContent.TabIndex = 1;
            // 
            // vldCustom
            // 
            this.vldCustom.CancelFocusChangeWhenInvalid = false;
            this.vldCustom.ControlToValidate = null;
            this.vldCustom.ErrorMessage = "";
            this.vldCustom.HasErrors = false;
            this.vldCustom.Icon = ((System.Drawing.Icon)(resources.GetObject("vldCustom.Icon")));
            // 
            // ServiceAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 543);
            this.Controls.Add(this.tblContainer);
            this.Name = "ServiceAdminForm";
            this.Text = "ServiceAdminForm";
            this.tblContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpContent.ResumeLayout(false);
            this.tblContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton rbtnFaultCodes;
        private System.Windows.Forms.ImageList imglstLargeIcons;
        private System.Windows.Forms.RadioButton rbtnSLAContracts;
        private System.Windows.Forms.RadioButton rbtnSource;
        private System.Windows.Forms.RadioButton rbtnFixCodes;
        private System.Windows.Forms.GroupBox grpContent;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private BMC.CoreLib.Win32.Validation.CustomValidator vldCustom;
        private System.Windows.Forms.Panel pnlContent;
    }
}