namespace BMC.MeterAdjustmentTool
{
    partial class DeltaDetailsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.lblCalcMeters = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.btnViewRamReset = new System.Windows.Forms.Button();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvInfo = new System.Windows.Forms.DataGridView();
            this.dgvRamResets = new System.Windows.Forms.DataGridView();
            this.pnlBottom.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.tblContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRamResets)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.dgvInfo);
            this.pnlBottom.Controls.Add(this.dgvRamResets);
            this.pnlBottom.Location = new System.Drawing.Point(0, 535);
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(3);
            this.pnlBottom.Size = new System.Drawing.Size(693, 133);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlGrid);
            this.pnlContainer.Controls.Add(this.pnlButtons);
            this.pnlContainer.Size = new System.Drawing.Size(693, 535);
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.tableLayoutPanel1);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(693, 500);
            this.pnlGrid.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvDetails, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCalcMeters, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(693, 500);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvDetails
            // 
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AllowUserToDeleteRows = false;
            this.dgvDetails.AllowUserToResizeRows = false;
            this.dgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetails.Location = new System.Drawing.Point(0, 33);
            this.dgvDetails.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetails.Size = new System.Drawing.Size(693, 464);
            this.dgvDetails.TabIndex = 1;
            this.dgvDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetails_CellDoubleClick);
            // 
            // lblCalcMeters
            // 
            this.lblCalcMeters.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCalcMeters.AutoSize = true;
            this.lblCalcMeters.Location = new System.Drawing.Point(552, 8);
            this.lblCalcMeters.Name = "lblCalcMeters";
            this.lblCalcMeters.Size = new System.Drawing.Size(138, 13);
            this.lblCalcMeters.TabIndex = 0;
            this.lblCalcMeters.Text = "***  Calculated Meters";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.tblContent);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 500);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(693, 35);
            this.pnlButtons.TabIndex = 3;
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 6;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblContent.Controls.Add(this.btnViewRamReset, 1, 0);
            this.tblContent.Controls.Add(this.btnViewDetails, 2, 0);
            this.tblContent.Controls.Add(this.btnUpdate, 3, 0);
            this.tblContent.Controls.Add(this.btnCancel, 4, 0);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(0, 0);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 1;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContent.Size = new System.Drawing.Size(693, 35);
            this.tblContent.TabIndex = 0;
            // 
            // btnViewRamReset
            // 
            this.btnViewRamReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewRamReset.ImageKey = "Component.ico";
            this.btnViewRamReset.Location = new System.Drawing.Point(99, 3);
            this.btnViewRamReset.Name = "btnViewRamReset";
            this.btnViewRamReset.Size = new System.Drawing.Size(119, 28);
            this.btnViewRamReset.TabIndex = 0;
            this.btnViewRamReset.Text = "View &RAM Reset";
            this.btnViewRamReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnViewRamReset.UseVisualStyleBackColor = true;
            this.btnViewRamReset.Click += new System.EventHandler(this.btnViewRamReset_Click);
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewDetails.ImageKey = "Component.ico";
            this.btnViewDetails.Location = new System.Drawing.Point(224, 3);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(119, 28);
            this.btnViewDetails.TabIndex = 1;
            this.btnViewDetails.Text = "&View Info";
            this.btnViewDetails.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnViewDetails.UseVisualStyleBackColor = true;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.ImageKey = "Component.ico";
            this.btnUpdate.Location = new System.Drawing.Point(349, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(119, 28);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageKey = "Component.ico";
            this.btnCancel.Location = new System.Drawing.Point(474, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dgvInfo
            // 
            this.dgvInfo.AllowUserToAddRows = false;
            this.dgvInfo.AllowUserToDeleteRows = false;
            this.dgvInfo.AllowUserToOrderColumns = true;
            this.dgvInfo.AllowUserToResizeRows = false;
            this.dgvInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvInfo.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvInfo.Location = new System.Drawing.Point(3, 3);
            this.dgvInfo.MultiSelect = false;
            this.dgvInfo.Name = "dgvInfo";
            this.dgvInfo.ReadOnly = true;
            this.dgvInfo.RowHeadersVisible = false;
            this.dgvInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInfo.Size = new System.Drawing.Size(687, 127);
            this.dgvInfo.TabIndex = 1;
            this.dgvInfo.Visible = false;
            // 
            // dgvRamResets
            // 
            this.dgvRamResets.AllowUserToAddRows = false;
            this.dgvRamResets.AllowUserToDeleteRows = false;
            this.dgvRamResets.AllowUserToOrderColumns = true;
            this.dgvRamResets.AllowUserToResizeRows = false;
            this.dgvRamResets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvRamResets.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRamResets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRamResets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRamResets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRamResets.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvRamResets.Location = new System.Drawing.Point(3, 3);
            this.dgvRamResets.MultiSelect = false;
            this.dgvRamResets.Name = "dgvRamResets";
            this.dgvRamResets.ReadOnly = true;
            this.dgvRamResets.RowHeadersVisible = false;
            this.dgvRamResets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRamResets.Size = new System.Drawing.Size(687, 127);
            this.dgvRamResets.TabIndex = 9;
            // 
            // DeltaDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(693, 668);
            this.MaximizeBox = true;
            this.Name = "DeltaDetailsForm";
            this.Text = "Delta Adjustment";
            this.pnlBottom.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.tblContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRamResets)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel pnlGrid;
        protected System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.Button btnViewRamReset;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvInfo;
        private System.Windows.Forms.DataGridView dgvRamResets;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.Label lblCalcMeters;
    }
}