namespace BMC.EnterpriseClient.Views
{
    partial class frmReadLiquidationDetails
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
            this.grpReadData = new System.Windows.Forms.GroupBox();
            this.dtgvReadData = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpReadData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvReadData)).BeginInit();
            this.SuspendLayout();
            // 
            // grpReadData
            // 
            this.grpReadData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpReadData.AutoSize = true;
            this.grpReadData.Controls.Add(this.dtgvReadData);
            this.grpReadData.Location = new System.Drawing.Point(7, 4);
            this.grpReadData.Name = "grpReadData";
            this.grpReadData.Size = new System.Drawing.Size(1157, 536);
            this.grpReadData.TabIndex = 0;
            this.grpReadData.TabStop = false;
            this.grpReadData.Text = "Read Details";
            // 
            // dtgvReadData
            // 
            this.dtgvReadData.AllowUserToAddRows = false;
            this.dtgvReadData.AllowUserToDeleteRows = false;
            this.dtgvReadData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvReadData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvReadData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgvReadData.Location = new System.Drawing.Point(3, 17);
            this.dtgvReadData.Name = "dtgvReadData";
            this.dtgvReadData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtgvReadData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvReadData.Size = new System.Drawing.Size(1151, 516);
            this.dtgvReadData.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1085, 546);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 28);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmReadLiquidationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 580);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpReadData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "frmReadLiquidationDetails";
            this.Text = "Read Liquidation Details";
            this.grpReadData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvReadData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpReadData;
        private System.Windows.Forms.DataGridView dtgvReadData;
        private System.Windows.Forms.Button btnClose;
    }
}