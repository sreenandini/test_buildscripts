namespace BMC.EnterpriseClient.Views
{
    partial class ucReadLiquidationReport
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
            this.grpReadData = new System.Windows.Forms.GroupBox();
            this.dtgvReadData = new System.Windows.Forms.DataGridView();
            this.grpSite = new System.Windows.Forms.GroupBox();
            this.chkOnlyLast20 = new System.Windows.Forms.CheckBox();
            this.lblSite = new System.Windows.Forms.Label();
            this.cboSite = new System.Windows.Forms.ComboBox();
            this.grpReadData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvReadData)).BeginInit();
            this.grpSite.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpReadData
            // 
            this.grpReadData.Controls.Add(this.dtgvReadData);
            this.grpReadData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpReadData.Location = new System.Drawing.Point(0, 0);
            this.grpReadData.Name = "grpReadData";
            this.grpReadData.Size = new System.Drawing.Size(832, 494);
            this.grpReadData.TabIndex = 1;
            this.grpReadData.TabStop = false;
            // 
            // dtgvReadData
            // 
            this.dtgvReadData.AllowUserToAddRows = false;
            this.dtgvReadData.AllowUserToDeleteRows = false;
            this.dtgvReadData.AllowUserToResizeColumns = false;
            this.dtgvReadData.AllowUserToResizeRows = false;
            this.dtgvReadData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgvReadData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtgvReadData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgvReadData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvReadData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgvReadData.Location = new System.Drawing.Point(3, 16);
            this.dtgvReadData.MultiSelect = false;
            this.dtgvReadData.Name = "dtgvReadData";
            this.dtgvReadData.ReadOnly = true;
            this.dtgvReadData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtgvReadData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvReadData.Size = new System.Drawing.Size(826, 475);
            this.dtgvReadData.TabIndex = 2;
            // 
            // grpSite
            // 
            this.grpSite.Controls.Add(this.chkOnlyLast20);
            this.grpSite.Controls.Add(this.lblSite);
            this.grpSite.Controls.Add(this.cboSite);
            this.grpSite.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSite.Location = new System.Drawing.Point(0, 0);
            this.grpSite.Name = "grpSite";
            this.grpSite.Size = new System.Drawing.Size(832, 38);
            this.grpSite.TabIndex = 0;
            this.grpSite.TabStop = false;
            // 
            // chkOnlyLast20
            // 
            this.chkOnlyLast20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOnlyLast20.AutoSize = true;
            this.chkOnlyLast20.Location = new System.Drawing.Point(677, 15);
            this.chkOnlyLast20.Name = "chkOnlyLast20";
            this.chkOnlyLast20.Size = new System.Drawing.Size(152, 17);
            this.chkOnlyLast20.TabIndex = 2;
            this.chkOnlyLast20.Text = "Show only last 20 Records";
            this.chkOnlyLast20.UseVisualStyleBackColor = true;
            this.chkOnlyLast20.CheckedChanged += new System.EventHandler(this.chkOnlyLast20_CheckedChanged);
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(26, 14);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(35, 13);
            this.lblSite.TabIndex = 0;
            this.lblSite.Text = "* Site:";
            // 
            // cboSite
            // 
            this.cboSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSite.FormattingEnabled = true;
            this.cboSite.Location = new System.Drawing.Point(67, 11);
            this.cboSite.Name = "cboSite";
            this.cboSite.Size = new System.Drawing.Size(201, 21);
            this.cboSite.TabIndex = 1;
            this.cboSite.SelectedIndexChanged += new System.EventHandler(this.cboSite_SelectedIndexChanged);
            // 
            // ucReadLiquidationReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.grpReadData);
            this.Controls.Add(this.grpSite);            
            this.Name = "ucReadLiquidationReport";
            this.Size = new System.Drawing.Size(832, 494);
            this.Load += new System.EventHandler(this.ucReadBasedLiquidation_Load);
            this.grpReadData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvReadData)).EndInit();
            this.grpSite.ResumeLayout(false);
            this.grpSite.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpReadData;
        private System.Windows.Forms.DataGridView dtgvReadData;
        private System.Windows.Forms.GroupBox grpSite;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.ComboBox cboSite;
        private System.Windows.Forms.CheckBox chkOnlyLast20;

    }
}
