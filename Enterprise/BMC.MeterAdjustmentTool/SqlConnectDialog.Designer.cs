namespace BMC.MeterAdjustmentTool
{
    partial class SqlConnectDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlConnectDialog));
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnConnect = new System.Windows.Forms.Button();
            this.tblDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblSite = new System.Windows.Forms.Label();
            this.cboSites = new System.Windows.Forms.ComboBox();
            this.pnlBottom.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.tblDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.tblButtons);
            this.pnlBottom.Location = new System.Drawing.Point(0, 38);
            this.pnlBottom.Size = new System.Drawing.Size(357, 38);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.tblDetails);
            this.pnlContainer.Size = new System.Drawing.Size(357, 38);
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 2;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblButtons.Controls.Add(this.btnCancel, 1, 0);
            this.tblButtons.Controls.Add(this.btnConnect, 0, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.tblButtons.Location = new System.Drawing.Point(97, 0);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(260, 38);
            this.tblButtons.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageList = this.imglstSmallIcons;
            this.btnCancel.Location = new System.Drawing.Point(133, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Database.ico");
            this.imglstSmallIcons.Images.SetKeyName(1, "Cancel.ico");
            this.imglstSmallIcons.Images.SetKeyName(2, "ConnectServer.ico");
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.ImageList = this.imglstSmallIcons;
            this.btnConnect.Location = new System.Drawing.Point(3, 5);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(124, 28);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tblDetails
            // 
            this.tblDetails.ColumnCount = 3;
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblDetails.Controls.Add(this.lblSite, 0, 0);
            this.tblDetails.Controls.Add(this.cboSites, 1, 0);
            this.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDetails.Location = new System.Drawing.Point(0, 0);
            this.tblDetails.Name = "tblDetails";
            this.tblDetails.RowCount = 1;
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.Size = new System.Drawing.Size(357, 38);
            this.tblDetails.TabIndex = 0;
            // 
            // lblSite
            // 
            this.lblSite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(3, 12);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(38, 13);
            this.lblSite.TabIndex = 0;
            this.lblSite.Text = "&Site :";
            // 
            // cboSites
            // 
            this.cboSites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSites.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSites.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSites.FormattingEnabled = true;
            this.cboSites.Location = new System.Drawing.Point(143, 8);
            this.cboSites.Name = "cboSites";
            this.cboSites.Size = new System.Drawing.Size(201, 21);
            this.cboSites.TabIndex = 1;
            this.cboSites.SelectedIndexChanged += new System.EventHandler(this.cboSites_SelectedIndexChanged);
            this.cboSites.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboSites_KeyDown);
            // 
            // SqlConnectDialog
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(357, 76);
            this.Name = "SqlConnectDialog";
            this.Text = "Connect to Site...";
            this.pnlBottom.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.tblDetails.ResumeLayout(false);
            this.tblDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.TableLayoutPanel tblDetails;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.ComboBox cboSites;
    }
}