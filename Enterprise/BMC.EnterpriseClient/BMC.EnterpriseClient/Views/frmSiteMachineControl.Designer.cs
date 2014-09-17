namespace BMC.EnterpriseClient.Views
{
    partial class frmSiteMachineControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSiteMachineControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.optDisableAll = new System.Windows.Forms.RadioButton();
            this.optEnableAll = new System.Windows.Forms.RadioButton();
            this.btnUpdateSite = new System.Windows.Forms.Button();
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.grv_PosDetails = new System.Windows.Forms.DataGridView();
            this.im_PosDetails = new System.Windows.Forms.ImageList(this.components);
            this.TimerMachineControl = new System.Windows.Forms.Timer(this.components);
            this.tmrCheckFocus = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv_PosDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.84689F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.15311F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 219F));
            this.tableLayoutPanel1.Controls.Add(this.optDisableAll, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.optEnableAll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnUpdateSite, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkAutoRefresh, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.grv_PosDetails, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.79692F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.20309F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(549, 458);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // optDisableAll
            // 
            this.optDisableAll.AutoSize = true;
            this.optDisableAll.Location = new System.Drawing.Point(160, 3);
            this.optDisableAll.Name = "optDisableAll";
            this.optDisableAll.Padding = new System.Windows.Forms.Padding(15, 10, 0, 0);
            this.optDisableAll.Size = new System.Drawing.Size(89, 27);
            this.optDisableAll.TabIndex = 1;
            this.optDisableAll.Text = "Disable All";
            this.optDisableAll.UseVisualStyleBackColor = true;
            this.optDisableAll.Click += new System.EventHandler(this.optDisableAll_Click);
            // 
            // optEnableAll
            // 
            this.optEnableAll.AutoSize = true;
            this.optEnableAll.Location = new System.Drawing.Point(3, 3);
            this.optEnableAll.Name = "optEnableAll";
            this.optEnableAll.Padding = new System.Windows.Forms.Padding(15, 10, 0, 0);
            this.optEnableAll.Size = new System.Drawing.Size(87, 27);
            this.optEnableAll.TabIndex = 0;
            this.optEnableAll.Text = "Enable All";
            this.optEnableAll.UseVisualStyleBackColor = true;
            this.optEnableAll.Click += new System.EventHandler(this.optEnableAll_Click);
            // 
            // btnUpdateSite
            // 
            this.btnUpdateSite.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnUpdateSite.Location = new System.Drawing.Point(411, 10);
            this.btnUpdateSite.Name = "btnUpdateSite";
            this.btnUpdateSite.Size = new System.Drawing.Size(135, 23);
            this.btnUpdateSite.TabIndex = 2;
            this.btnUpdateSite.Text = "Update Site >>";
            this.btnUpdateSite.UseVisualStyleBackColor = true;
            this.btnUpdateSite.Click += new System.EventHandler(this.btnUpdateSite_Click);
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.Location = new System.Drawing.Point(438, 423);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.chkAutoRefresh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAutoRefresh.Size = new System.Drawing.Size(108, 17);
            this.chkAutoRefresh.TabIndex = 4;
            this.chkAutoRefresh.Text = "Auto Refresh";
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            this.chkAutoRefresh.CheckedChanged += new System.EventHandler(this.chkAutoRefresh_CheckedChanged);
            // 
            // grv_PosDetails
            // 
            this.grv_PosDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.grv_PosDetails, 3);
            this.grv_PosDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grv_PosDetails.Location = new System.Drawing.Point(3, 46);
            this.grv_PosDetails.Name = "grv_PosDetails";
            this.grv_PosDetails.ReadOnly = true;
            this.grv_PosDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grv_PosDetails.Size = new System.Drawing.Size(543, 357);
            this.grv_PosDetails.TabIndex = 3;
            this.grv_PosDetails.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grv_PosDetails_CellMouseDoubleClick);
            // 
            // im_PosDetails
            // 
            this.im_PosDetails.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("im_PosDetails.ImageStream")));
            this.im_PosDetails.TransparentColor = System.Drawing.Color.Transparent;
            this.im_PosDetails.Images.SetKeyName(0, "1367254394_exclamation.png");
            this.im_PosDetails.Images.SetKeyName(1, "tick.PNG");
            this.im_PosDetails.Images.SetKeyName(2, "Error.png");
            // 
            // TimerMachineControl
            // 
            this.TimerMachineControl.Tick += new System.EventHandler(this.TimerMachineControl_Tick);
            // 
            // tmrCheckFocus
            // 
            this.tmrCheckFocus.Tick += new System.EventHandler(this.tmrCheckFocus_Tick);
            // 
            // frmSiteMachineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 458);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSiteMachineControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Site Machine Control";
            this.Load += new System.EventHandler(this.frmSiteMachineControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv_PosDetails)).EndInit();
            this.ResumeLayout(false);

        }

      

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton optDisableAll;
        private System.Windows.Forms.RadioButton optEnableAll;
        private System.Windows.Forms.Button btnUpdateSite;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
        private System.Windows.Forms.Timer TimerMachineControl;
        private System.Windows.Forms.Timer tmrCheckFocus;
        private System.Windows.Forms.ImageList im_PosDetails;
        private System.Windows.Forms.DataGridView grv_PosDetails;
    }
}