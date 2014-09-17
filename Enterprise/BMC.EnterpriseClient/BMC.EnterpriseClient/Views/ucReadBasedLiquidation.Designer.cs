namespace BMC.EnterpriseClient.Views
{
    partial class ucReadBasedLiquidation
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
            this.lblSite = new System.Windows.Forms.Label();
            this.cboSite = new System.Windows.Forms.ComboBox();
            this.grpReadData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvReadData)).BeginInit();
            this.grpSite.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpReadData
            // 
            this.grpReadData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpReadData.Controls.Add(this.dtgvReadData);
            this.grpReadData.Location = new System.Drawing.Point(3, 38);
            this.grpReadData.Name = "grpReadData";
            this.grpReadData.Size = new System.Drawing.Size(829, 453);
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
            this.dtgvReadData.Size = new System.Drawing.Size(823, 434);
            this.dtgvReadData.TabIndex = 2;
            this.dtgvReadData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvReadData_CellDoubleClick);
            // 
            // grpSite
            // 
            this.grpSite.Controls.Add(this.lblSite);
            this.grpSite.Controls.Add(this.cboSite);
            this.grpSite.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSite.Location = new System.Drawing.Point(0, 0);
            this.grpSite.Name = "grpSite";
            this.grpSite.Size = new System.Drawing.Size(832, 38);
            this.grpSite.TabIndex = 0;
            this.grpSite.TabStop = false;
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(17, 14);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(35, 13);
            this.lblSite.TabIndex = 0;
            this.lblSite.Text = "* Site:";
            // 
            // cboSite
            // 
            this.cboSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSite.FormattingEnabled = true;
            this.cboSite.Location = new System.Drawing.Point(92, 11);
            this.cboSite.Name = "cboSite";
            this.cboSite.Size = new System.Drawing.Size(201, 21);
            this.cboSite.TabIndex = 1;
            this.cboSite.SelectedIndexChanged += new System.EventHandler(this.cboSite_SelectedIndexChanged);
            // 
            // ucReadBasedLiquidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSite);
            this.Controls.Add(this.grpReadData);
            this.Name = "ucReadBasedLiquidation";
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

    }
}
