namespace BMC.EnterpriseClient.Views.ServiceCalls
{
    partial class frmConfiguareGMUFaults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfiguareGMUFaults));
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblLowerButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvEventTypeDisplay = new System.Windows.Forms.DataGridView();
            this.tblGMUConfiguration = new System.Windows.Forms.TableLayoutPanel();
            this.lblFaultGroup = new System.Windows.Forms.Label();
            this.lblFault = new System.Windows.Forms.Label();
            this.lblEventType = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmbFaultGroup = new System.Windows.Forms.ComboBox();
            this.cmbFault = new System.Windows.Forms.ComboBox();
            this.cmbEventType = new System.Windows.Forms.ComboBox();
            this.cbAutoCreateServiceCall = new System.Windows.Forms.CheckBox();
            this.cbAutoCloseService = new System.Windows.Forms.CheckBox();
            this.tblAutoCreateMail = new System.Windows.Forms.TableLayoutPanel();
            this.cbAutoCreateMail = new System.Windows.Forms.CheckBox();
            this.btnConfiguareMail = new System.Windows.Forms.Button();
            this.tblContainer.SuspendLayout();
            this.tblLowerButtons.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventTypeDisplay)).BeginInit();
            this.tblGMUConfiguration.SuspendLayout();
            this.tblAutoCreateMail.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tblLowerButtons, 0, 2);
            this.tblContainer.Controls.Add(this.dgvEventTypeDisplay, 0, 1);
            this.tblContainer.Controls.Add(this.tblGMUConfiguration, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Margin = new System.Windows.Forms.Padding(0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 3;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.Size = new System.Drawing.Size(1010, 592);
            this.tblContainer.TabIndex = 0;
            // 
            // tblLowerButtons
            // 
            this.tblLowerButtons.ColumnCount = 5;
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLowerButtons.Controls.Add(this.btnUpdate, 3, 0);
            this.tblLowerButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblLowerButtons.Controls.Add(this.flowLayoutPanel1, 2, 0);
            this.tblLowerButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblLowerButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLowerButtons.Location = new System.Drawing.Point(0, 552);
            this.tblLowerButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblLowerButtons.Name = "tblLowerButtons";
            this.tblLowerButtons.RowCount = 1;
            this.tblLowerButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowerButtons.Size = new System.Drawing.Size(1010, 40);
            this.tblLowerButtons.TabIndex = 2;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(801, 6);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 28);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(589, 6);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 28);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(692, 3);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(106, 37);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(3, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 28);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(3, 37);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Canc&el";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(907, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvEventTypeDisplay
            // 
            this.dgvEventTypeDisplay.AllowUserToAddRows = false;
            this.dgvEventTypeDisplay.AllowUserToDeleteRows = false;
            this.dgvEventTypeDisplay.AllowUserToResizeRows = false;
            this.dgvEventTypeDisplay.BackgroundColor = System.Drawing.Color.White;
            this.dgvEventTypeDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventTypeDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEventTypeDisplay.Location = new System.Drawing.Point(3, 103);
            this.dgvEventTypeDisplay.MultiSelect = false;
            this.dgvEventTypeDisplay.Name = "dgvEventTypeDisplay";
            this.dgvEventTypeDisplay.ReadOnly = true;
            this.dgvEventTypeDisplay.RowHeadersVisible = false;
            this.dgvEventTypeDisplay.RowHeadersWidth = 150;
            this.dgvEventTypeDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEventTypeDisplay.Size = new System.Drawing.Size(1004, 446);
            this.dgvEventTypeDisplay.TabIndex = 1;
            this.dgvEventTypeDisplay.SelectionChanged += new System.EventHandler(this.dgvEventTypeDisplay_RowStateChanged);
            // 
            // tblGMUConfiguration
            // 
            this.tblGMUConfiguration.ColumnCount = 5;
            this.tblGMUConfiguration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblGMUConfiguration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGMUConfiguration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblGMUConfiguration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblGMUConfiguration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGMUConfiguration.Controls.Add(this.lblFaultGroup, 3, 0);
            this.tblGMUConfiguration.Controls.Add(this.lblFault, 3, 1);
            this.tblGMUConfiguration.Controls.Add(this.lblEventType, 3, 2);
            this.tblGMUConfiguration.Controls.Add(this.lblDescription, 0, 0);
            this.tblGMUConfiguration.Controls.Add(this.txtDescription, 1, 0);
            this.tblGMUConfiguration.Controls.Add(this.cmbFaultGroup, 4, 0);
            this.tblGMUConfiguration.Controls.Add(this.cmbFault, 4, 1);
            this.tblGMUConfiguration.Controls.Add(this.cmbEventType, 4, 2);
            this.tblGMUConfiguration.Controls.Add(this.cbAutoCreateServiceCall, 0, 1);
            this.tblGMUConfiguration.Controls.Add(this.cbAutoCloseService, 0, 2);
            this.tblGMUConfiguration.Controls.Add(this.tblAutoCreateMail, 1, 1);
            this.tblGMUConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblGMUConfiguration.Location = new System.Drawing.Point(3, 3);
            this.tblGMUConfiguration.Name = "tblGMUConfiguration";
            this.tblGMUConfiguration.RowCount = 4;
            this.tblGMUConfiguration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblGMUConfiguration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblGMUConfiguration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblGMUConfiguration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblGMUConfiguration.Size = new System.Drawing.Size(1004, 94);
            this.tblGMUConfiguration.TabIndex = 0;
            // 
            // lblFaultGroup
            // 
            this.lblFaultGroup.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFaultGroup.AutoSize = true;
            this.lblFaultGroup.Location = new System.Drawing.Point(515, 8);
            this.lblFaultGroup.Name = "lblFaultGroup";
            this.lblFaultGroup.Size = new System.Drawing.Size(65, 13);
            this.lblFaultGroup.TabIndex = 5;
            this.lblFaultGroup.Text = "Fault Group:";
            this.lblFaultGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFault
            // 
            this.lblFault.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFault.AutoSize = true;
            this.lblFault.Location = new System.Drawing.Point(515, 38);
            this.lblFault.Name = "lblFault";
            this.lblFault.Size = new System.Drawing.Size(33, 13);
            this.lblFault.TabIndex = 7;
            this.lblFault.Text = "Fault:";
            this.lblFault.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEventType
            // 
            this.lblEventType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEventType.AutoSize = true;
            this.lblEventType.Location = new System.Drawing.Point(515, 68);
            this.lblEventType.Name = "lblEventType";
            this.lblEventType.Size = new System.Drawing.Size(62, 13);
            this.lblEventType.TabIndex = 9;
            this.lblEventType.Text = "EventType:";
            this.lblEventType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 8);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Description:";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(203, 3);
            this.txtDescription.MaxLength = 60;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(286, 20);
            this.txtDescription.TabIndex = 1;
            // 
            // cmbFaultGroup
            // 
            this.cmbFaultGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFaultGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFaultGroup.FormattingEnabled = true;
            this.cmbFaultGroup.Location = new System.Drawing.Point(715, 3);
            this.cmbFaultGroup.Name = "cmbFaultGroup";
            this.cmbFaultGroup.Size = new System.Drawing.Size(286, 21);
            this.cmbFaultGroup.TabIndex = 6;
            this.cmbFaultGroup.SelectedIndexChanged += new System.EventHandler(this.cmbFaultGroup_SelectedIndexChanged);
            // 
            // cmbFault
            // 
            this.cmbFault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFault.FormattingEnabled = true;
            this.cmbFault.Location = new System.Drawing.Point(715, 33);
            this.cmbFault.Name = "cmbFault";
            this.cmbFault.Size = new System.Drawing.Size(286, 21);
            this.cmbFault.TabIndex = 8;
            this.cmbFault.SelectedIndexChanged += new System.EventHandler(this.cmbFault_SelectedIndexChanged);
            // 
            // cmbEventType
            // 
            this.cmbEventType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventType.FormattingEnabled = true;
            this.cmbEventType.Location = new System.Drawing.Point(715, 63);
            this.cmbEventType.Name = "cmbEventType";
            this.cmbEventType.Size = new System.Drawing.Size(286, 21);
            this.cmbEventType.TabIndex = 10;
            // 
            // cbAutoCreateServiceCall
            // 
            this.cbAutoCreateServiceCall.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbAutoCreateServiceCall.AutoSize = true;
            this.cbAutoCreateServiceCall.Location = new System.Drawing.Point(3, 36);
            this.cbAutoCreateServiceCall.Name = "cbAutoCreateServiceCall";
            this.cbAutoCreateServiceCall.Size = new System.Drawing.Size(150, 17);
            this.cbAutoCreateServiceCall.TabIndex = 2;
            this.cbAutoCreateServiceCall.Text = "Auto Create a Service Call";
            this.cbAutoCreateServiceCall.UseVisualStyleBackColor = true;
            // 
            // cbAutoCloseService
            // 
            this.cbAutoCloseService.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbAutoCloseService.AutoSize = true;
            this.cbAutoCloseService.Location = new System.Drawing.Point(3, 66);
            this.cbAutoCloseService.Name = "cbAutoCloseService";
            this.cbAutoCloseService.Size = new System.Drawing.Size(148, 17);
            this.cbAutoCloseService.TabIndex = 3;
            this.cbAutoCloseService.Text = "Auto  Close a Service Call";
            this.cbAutoCloseService.UseVisualStyleBackColor = true;
            // 
            // tblAutoCreateMail
            // 
            this.tblAutoCreateMail.ColumnCount = 2;
            this.tblAutoCreateMail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAutoCreateMail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAutoCreateMail.Controls.Add(this.cbAutoCreateMail, 0, 0);
            this.tblAutoCreateMail.Controls.Add(this.btnConfiguareMail, 1, 0);
            this.tblAutoCreateMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAutoCreateMail.Location = new System.Drawing.Point(203, 33);
            this.tblAutoCreateMail.Name = "tblAutoCreateMail";
            this.tblAutoCreateMail.RowCount = 1;
            this.tblGMUConfiguration.SetRowSpan(this.tblAutoCreateMail, 2);
            this.tblAutoCreateMail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAutoCreateMail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tblAutoCreateMail.Size = new System.Drawing.Size(286, 54);
            this.tblAutoCreateMail.TabIndex = 4;
            // 
            // cbAutoCreateMail
            // 
            this.cbAutoCreateMail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbAutoCreateMail.AutoSize = true;
            this.cbAutoCreateMail.Location = new System.Drawing.Point(3, 18);
            this.cbAutoCreateMail.Name = "cbAutoCreateMail";
            this.cbAutoCreateMail.Size = new System.Drawing.Size(104, 17);
            this.cbAutoCreateMail.TabIndex = 0;
            this.cbAutoCreateMail.Text = "Auto Create Mail";
            this.cbAutoCreateMail.UseVisualStyleBackColor = true;
            this.cbAutoCreateMail.CheckedChanged += new System.EventHandler(this.cbAutoCreateMail_CheckedChanged);
            // 
            // btnConfiguareMail
            // 
            this.btnConfiguareMail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnConfiguareMail.Enabled = false;
            this.btnConfiguareMail.Location = new System.Drawing.Point(146, 13);
            this.btnConfiguareMail.Name = "btnConfiguareMail";
            this.btnConfiguareMail.Size = new System.Drawing.Size(120, 28);
            this.btnConfiguareMail.TabIndex = 1;
            this.btnConfiguareMail.Text = "Configure  Mail";
            this.btnConfiguareMail.UseVisualStyleBackColor = true;
            this.btnConfiguareMail.Click += new System.EventHandler(this.btnConfiguareMail_Click);
            // 
            // frmConfiguareGMUFaults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1010, 592);
            this.Controls.Add(this.tblContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfiguareGMUFaults";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configure GMU Faults";
            this.Load += new System.EventHandler(this.frmConfiguareGMUFaults_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblLowerButtons.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventTypeDisplay)).EndInit();
            this.tblGMUConfiguration.ResumeLayout(false);
            this.tblGMUConfiguration.PerformLayout();
            this.tblAutoCreateMail.ResumeLayout(false);
            this.tblAutoCreateMail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblLowerButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.DataGridView dgvEventTypeDisplay;
        private System.Windows.Forms.TableLayoutPanel tblGMUConfiguration;
        private System.Windows.Forms.Label lblFaultGroup;
        private System.Windows.Forms.Label lblFault;
        private System.Windows.Forms.Label lblEventType;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox cmbFaultGroup;
        private System.Windows.Forms.ComboBox cmbFault;
        private System.Windows.Forms.ComboBox cmbEventType;
        private System.Windows.Forms.CheckBox cbAutoCreateServiceCall;
        private System.Windows.Forms.CheckBox cbAutoCloseService;
        private System.Windows.Forms.CheckBox cbAutoCreateMail;
        private System.Windows.Forms.TableLayoutPanel tblAutoCreateMail;
        private System.Windows.Forms.Button btnConfiguareMail;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnClose;
    }
}