namespace BMC.EnterpriseClient.Views
{
    partial class UCGameCapping
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpSlotSetting = new System.Windows.Forms.GroupBox();
            this.tblSlotSetting = new System.Windows.Forms.TableLayoutPanel();
            this.chkCapRelease = new System.Windows.Forms.CheckBox();
            this.chkEmployeeReserve = new System.Windows.Forms.CheckBox();
            this.chkPlayerReserve = new System.Windows.Forms.CheckBox();
            this.lblMintsExpireGameCapping = new System.Windows.Forms.Label();
            this.nudMintsExpireGameCapping = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCardLevel = new System.Windows.Forms.Label();
            this.lblMaxMachinestoCap = new System.Windows.Forms.Label();
            this.grpCardLevelSetting = new System.Windows.Forms.GroupBox();
            this.tblCardSetting = new System.Windows.Forms.TableLayoutPanel();
            this.gvCardLevelSettings = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tblMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.grpSlotSetting.SuspendLayout();
            this.tblSlotSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMintsExpireGameCapping)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpCardLevelSetting.SuspendLayout();
            this.tblCardSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCardLevelSettings)).BeginInit();
            this.tblMainFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSlotSetting
            // 
            this.grpSlotSetting.Controls.Add(this.tblSlotSetting);
            this.grpSlotSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSlotSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSlotSetting.Location = new System.Drawing.Point(3, 3);
            this.grpSlotSetting.Name = "grpSlotSetting";
            this.grpSlotSetting.Size = new System.Drawing.Size(747, 124);
            this.grpSlotSetting.TabIndex = 0;
            this.grpSlotSetting.TabStop = false;
            this.grpSlotSetting.Text = "Game Capping Settings";
            // 
            // tblSlotSetting
            // 
            this.tblSlotSetting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblSlotSetting.ColumnCount = 2;
            this.tblSlotSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tblSlotSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSlotSetting.Controls.Add(this.chkCapRelease, 0, 0);
            this.tblSlotSetting.Controls.Add(this.chkEmployeeReserve, 0, 1);
            this.tblSlotSetting.Controls.Add(this.chkPlayerReserve, 0, 2);
            this.tblSlotSetting.Controls.Add(this.lblMintsExpireGameCapping, 0, 3);
            this.tblSlotSetting.Controls.Add(this.nudMintsExpireGameCapping, 1, 3);
            this.tblSlotSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSlotSetting.Location = new System.Drawing.Point(3, 17);
            this.tblSlotSetting.Name = "tblSlotSetting";
            this.tblSlotSetting.RowCount = 4;
            this.tblSlotSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblSlotSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblSlotSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblSlotSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblSlotSetting.Size = new System.Drawing.Size(741, 104);
            this.tblSlotSetting.TabIndex = 0;
            // 
            // chkCapRelease
            // 
            this.chkCapRelease.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkCapRelease.AutoSize = true;
            this.chkCapRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCapRelease.Location = new System.Drawing.Point(3, 4);
            this.chkCapRelease.Name = "chkCapRelease";
            this.chkCapRelease.Size = new System.Drawing.Size(217, 17);
            this.chkCapRelease.TabIndex = 0;
            this.chkCapRelease.Text = "Cap Release on Player Upon Card Insert";
            this.chkCapRelease.UseVisualStyleBackColor = true;
            // 
            // chkEmployeeReserve
            // 
            this.chkEmployeeReserve.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkEmployeeReserve.AutoSize = true;
            this.chkEmployeeReserve.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmployeeReserve.Location = new System.Drawing.Point(3, 29);
            this.chkEmployeeReserve.Name = "chkEmployeeReserve";
            this.chkEmployeeReserve.Size = new System.Drawing.Size(190, 17);
            this.chkEmployeeReserve.TabIndex = 1;
            this.chkEmployeeReserve.Text = "Allow Employee To Reserve Game";
            this.chkEmployeeReserve.UseVisualStyleBackColor = true;
            // 
            // chkPlayerReserve
            // 
            this.chkPlayerReserve.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkPlayerReserve.AutoSize = true;
            this.chkPlayerReserve.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPlayerReserve.Location = new System.Drawing.Point(3, 54);
            this.chkPlayerReserve.Name = "chkPlayerReserve";
            this.chkPlayerReserve.Size = new System.Drawing.Size(173, 17);
            this.chkPlayerReserve.TabIndex = 2;
            this.chkPlayerReserve.Text = "Allow Player To Reserve Game";
            this.chkPlayerReserve.UseVisualStyleBackColor = true;
            // 
            // lblMintsExpireGameCapping
            // 
            this.lblMintsExpireGameCapping.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMintsExpireGameCapping.AutoSize = true;
            this.lblMintsExpireGameCapping.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMintsExpireGameCapping.Location = new System.Drawing.Point(3, 83);
            this.lblMintsExpireGameCapping.Name = "lblMintsExpireGameCapping";
            this.lblMintsExpireGameCapping.Size = new System.Drawing.Size(227, 13);
            this.lblMintsExpireGameCapping.TabIndex = 3;
            this.lblMintsExpireGameCapping.Text = "Mints To Expire Game Capping (0 for No Alert):";
            this.lblMintsExpireGameCapping.Visible = false;
            // 
            // nudMintsExpireGameCapping
            // 
            this.nudMintsExpireGameCapping.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudMintsExpireGameCapping.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMintsExpireGameCapping.Location = new System.Drawing.Point(253, 79);
            this.nudMintsExpireGameCapping.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.nudMintsExpireGameCapping.Name = "nudMintsExpireGameCapping";
            this.nudMintsExpireGameCapping.Size = new System.Drawing.Size(74, 20);
            this.nudMintsExpireGameCapping.TabIndex = 4;
            this.nudMintsExpireGameCapping.Visible = false;
            this.nudMintsExpireGameCapping.ValueChanged += new System.EventHandler(this.nudMintsExpireGameCapping_ValueChanged);
            this.nudMintsExpireGameCapping.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudMintsExpireGameCapping_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 208F));
            this.tableLayoutPanel2.Controls.Add(this.lblCardLevel, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblCardLevel
            // 
            this.lblCardLevel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCardLevel.AutoSize = true;
            this.lblCardLevel.Location = new System.Drawing.Point(3, 43);
            this.lblCardLevel.Name = "lblCardLevel";
            this.lblCardLevel.Size = new System.Drawing.Size(1, 13);
            this.lblCardLevel.TabIndex = 0;
            this.lblCardLevel.Text = "Card Level:";
            // 
            // lblMaxMachinestoCap
            // 
            this.lblMaxMachinestoCap.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMaxMachinestoCap.AutoSize = true;
            this.lblMaxMachinestoCap.Location = new System.Drawing.Point(3, 295);
            this.lblMaxMachinestoCap.Name = "lblMaxMachinestoCap";
            this.lblMaxMachinestoCap.Size = new System.Drawing.Size(140, 13);
            this.lblMaxMachinestoCap.TabIndex = 1;
            this.lblMaxMachinestoCap.Text = "Max no of Machines to Cap:";
            // 
            // grpCardLevelSetting
            // 
            this.grpCardLevelSetting.Controls.Add(this.tblCardSetting);
            this.grpCardLevelSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCardLevelSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCardLevelSetting.Location = new System.Drawing.Point(3, 133);
            this.grpCardLevelSetting.Name = "grpCardLevelSetting";
            this.grpCardLevelSetting.Size = new System.Drawing.Size(747, 419);
            this.grpCardLevelSetting.TabIndex = 1;
            this.grpCardLevelSetting.TabStop = false;
            this.grpCardLevelSetting.Text = "Card Level Settings";
            // 
            // tblCardSetting
            // 
            this.tblCardSetting.ColumnCount = 5;
            this.tblCardSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblCardSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblCardSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblCardSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCardSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblCardSetting.Controls.Add(this.gvCardLevelSettings, 0, 1);
            this.tblCardSetting.Controls.Add(this.btnSearch, 2, 0);
            this.tblCardSetting.Controls.Add(this.btnDelete, 1, 2);
            this.tblCardSetting.Controls.Add(this.label1, 0, 0);
            this.tblCardSetting.Controls.Add(this.btnAdd, 0, 2);
            this.tblCardSetting.Controls.Add(this.txtSearch, 1, 0);
            this.tblCardSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCardSetting.Location = new System.Drawing.Point(3, 17);
            this.tblCardSetting.Name = "tblCardSetting";
            this.tblCardSetting.RowCount = 3;
            this.tblCardSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblCardSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCardSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblCardSetting.Size = new System.Drawing.Size(741, 399);
            this.tblCardSetting.TabIndex = 0;
            // 
            // gvCardLevelSettings
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCardLevelSettings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvCardLevelSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.tblCardSetting.SetColumnSpan(this.gvCardLevelSettings, 5);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvCardLevelSettings.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvCardLevelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvCardLevelSettings.Location = new System.Drawing.Point(3, 38);
            this.gvCardLevelSettings.Name = "gvCardLevelSettings";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCardLevelSettings.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvCardLevelSettings.RowHeadersWidth = 20;
            this.gvCardLevelSettings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvCardLevelSettings.Size = new System.Drawing.Size(735, 318);
            this.gvCardLevelSettings.TabIndex = 3;
            this.gvCardLevelSettings.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCardLevelSettings_CellValueChanged);
            this.gvCardLevelSettings.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gvCardLevelSettings_DataError);
            this.gvCardLevelSettings.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvCardLevelSettings_EditingControlShowing);
            this.gvCardLevelSettings.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(243, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "S&earch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(123, 365);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 28);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Card Level:";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(3, 365);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 28);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(123, 7);
            this.txtSearch.MaxLength = 2;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(114, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // tblMainFrame
            // 
            this.tblMainFrame.ColumnCount = 1;
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.Controls.Add(this.grpSlotSetting, 0, 0);
            this.tblMainFrame.Controls.Add(this.grpCardLevelSetting, 0, 1);
            this.tblMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblMainFrame.Name = "tblMainFrame";
            this.tblMainFrame.RowCount = 2;
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.Size = new System.Drawing.Size(753, 555);
            this.tblMainFrame.TabIndex = 0;
            // 
            // UCGameCapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblMainFrame);
            this.Name = "UCGameCapping";
            this.Size = new System.Drawing.Size(753, 555);
            this.Load += new System.EventHandler(this.UCGameCapping_Load);
            this.grpSlotSetting.ResumeLayout(false);
            this.tblSlotSetting.ResumeLayout(false);
            this.tblSlotSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMintsExpireGameCapping)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grpCardLevelSetting.ResumeLayout(false);
            this.tblCardSetting.ResumeLayout(false);
            this.tblCardSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCardLevelSettings)).EndInit();
            this.tblMainFrame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSlotSetting;
        private System.Windows.Forms.TableLayoutPanel tblSlotSetting;
        private System.Windows.Forms.CheckBox chkCapRelease;
        private System.Windows.Forms.CheckBox chkPlayerReserve;
        private System.Windows.Forms.CheckBox chkEmployeeReserve;
        private System.Windows.Forms.Label lblMintsExpireGameCapping;
        private System.Windows.Forms.NumericUpDown nudMintsExpireGameCapping;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblCardLevel;
        private System.Windows.Forms.Label lblMaxMachinestoCap;
        private System.Windows.Forms.GroupBox grpCardLevelSetting;
        private System.Windows.Forms.DataGridView gvCardLevelSettings;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TableLayoutPanel tblMainFrame;
        private System.Windows.Forms.TableLayoutPanel tblCardSetting;
    }
}
