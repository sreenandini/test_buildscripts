namespace BMC.CoreLib.Win32
{
    partial class PagingDataGridView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PagingDataGridView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnMoveFirst = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnMovePrev = new System.Windows.Forms.Button();
            this.btnMoveNext = new System.Windows.Forms.Button();
            this.btnMoveLast = new System.Windows.Forms.Button();
            this.cboPageNos = new System.Windows.Forms.ComboBox();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.dgvRows = new System.Windows.Forms.DataGridView();
            this.tblButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).BeginInit();
            this.SuspendLayout();
            // 
            // tblButtons
            // 
            this.tblButtons.BackColor = System.Drawing.SystemColors.Control;
            this.tblButtons.ColumnCount = 6;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblButtons.Controls.Add(this.btnMoveFirst, 1, 0);
            this.tblButtons.Controls.Add(this.btnMovePrev, 2, 0);
            this.tblButtons.Controls.Add(this.btnMoveNext, 4, 0);
            this.tblButtons.Controls.Add(this.btnMoveLast, 5, 0);
            this.tblButtons.Controls.Add(this.cboPageNos, 3, 0);
            this.tblButtons.Controls.Add(this.lblPageInfo, 0, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblButtons.Location = new System.Drawing.Point(0, 479);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(602, 32);
            this.tblButtons.TabIndex = 1;
            this.tblButtons.Visible = false;
            // 
            // btnMoveFirst
            // 
            this.btnMoveFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveFirst.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveFirst.ImageKey = "MoveFirst";
            this.btnMoveFirst.ImageList = this.imglstSmallIcons;
            this.btnMoveFirst.Location = new System.Drawing.Point(365, 4);
            this.btnMoveFirst.Name = "btnMoveFirst";
            this.btnMoveFirst.Size = new System.Drawing.Size(34, 24);
            this.btnMoveFirst.TabIndex = 0;
            this.btnMoveFirst.UseVisualStyleBackColor = true;
            this.btnMoveFirst.Click += new System.EventHandler(this.btnMoveFirst_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "MoveFirst");
            this.imglstSmallIcons.Images.SetKeyName(1, "MovePrev");
            this.imglstSmallIcons.Images.SetKeyName(2, "MoveNext");
            this.imglstSmallIcons.Images.SetKeyName(3, "MoveLast");
            this.imglstSmallIcons.Images.SetKeyName(4, "Warning");
            this.imglstSmallIcons.Images.SetKeyName(5, "Error");
            this.imglstSmallIcons.Images.SetKeyName(6, "OK");
            this.imglstSmallIcons.Images.SetKeyName(7, "Timer");
            this.imglstSmallIcons.Images.SetKeyName(8, "Hourglass");
            this.imglstSmallIcons.Images.SetKeyName(9, "Message2");
            this.imglstSmallIcons.Images.SetKeyName(10, "MoveFile");
            this.imglstSmallIcons.Images.SetKeyName(11, "Lock");
            // 
            // btnMovePrev
            // 
            this.btnMovePrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMovePrev.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMovePrev.ImageKey = "MovePrev";
            this.btnMovePrev.ImageList = this.imglstSmallIcons;
            this.btnMovePrev.Location = new System.Drawing.Point(405, 4);
            this.btnMovePrev.Name = "btnMovePrev";
            this.btnMovePrev.Size = new System.Drawing.Size(34, 24);
            this.btnMovePrev.TabIndex = 1;
            this.btnMovePrev.UseVisualStyleBackColor = true;
            this.btnMovePrev.Click += new System.EventHandler(this.btnMovePrev_Click);
            // 
            // btnMoveNext
            // 
            this.btnMoveNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveNext.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveNext.ImageKey = "MoveNext";
            this.btnMoveNext.ImageList = this.imglstSmallIcons;
            this.btnMoveNext.Location = new System.Drawing.Point(525, 4);
            this.btnMoveNext.Name = "btnMoveNext";
            this.btnMoveNext.Size = new System.Drawing.Size(34, 24);
            this.btnMoveNext.TabIndex = 3;
            this.btnMoveNext.UseVisualStyleBackColor = true;
            this.btnMoveNext.Click += new System.EventHandler(this.btnMoveNext_Click);
            // 
            // btnMoveLast
            // 
            this.btnMoveLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveLast.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveLast.ImageKey = "MoveLast";
            this.btnMoveLast.ImageList = this.imglstSmallIcons;
            this.btnMoveLast.Location = new System.Drawing.Point(565, 4);
            this.btnMoveLast.Name = "btnMoveLast";
            this.btnMoveLast.Size = new System.Drawing.Size(34, 24);
            this.btnMoveLast.TabIndex = 4;
            this.btnMoveLast.UseVisualStyleBackColor = true;
            this.btnMoveLast.Click += new System.EventHandler(this.btnMoveLast_Click);
            // 
            // cboPageNos
            // 
            this.cboPageNos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPageNos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPageNos.FormattingEnabled = true;
            this.cboPageNos.Location = new System.Drawing.Point(445, 6);
            this.cboPageNos.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cboPageNos.Name = "cboPageNos";
            this.cboPageNos.Size = new System.Drawing.Size(74, 21);
            this.cboPageNos.TabIndex = 2;
            this.cboPageNos.SelectedIndexChanged += new System.EventHandler(this.cboPageNos_SelectedIndexChanged);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageInfo.Location = new System.Drawing.Point(359, 9);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(0, 13);
            this.lblPageInfo.TabIndex = 5;
            // 
            // dgvRows
            // 
            this.dgvRows.AllowUserToAddRows = false;
            this.dgvRows.AllowUserToDeleteRows = false;
            this.dgvRows.AllowUserToResizeColumns = false;
            this.dgvRows.AllowUserToResizeRows = false;
            this.dgvRows.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRows.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvRows.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRows.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRows.Location = new System.Drawing.Point(0, 0);
            this.dgvRows.MultiSelect = false;
            this.dgvRows.Name = "dgvRows";
            this.dgvRows.RowHeadersVisible = false;
            this.dgvRows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvRows.Size = new System.Drawing.Size(602, 479);
            this.dgvRows.StandardTab = true;
            this.dgvRows.TabIndex = 0;
            this.dgvRows.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRows_CellClick);
            this.dgvRows.SelectionChanged += new System.EventHandler(this.dgvRows_SelectionChanged);
            // 
            // PagingDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvRows);
            this.Controls.Add(this.tblButtons);
            this.Name = "PagingDataGridView";
            this.Size = new System.Drawing.Size(602, 511);
            this.tblButtons.ResumeLayout(false);
            this.tblButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnMoveFirst;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.Button btnMovePrev;
        private System.Windows.Forms.Button btnMoveNext;
        private System.Windows.Forms.Button btnMoveLast;
        private System.Windows.Forms.ComboBox cboPageNos;
        private System.Windows.Forms.DataGridView dgvRows;
        private System.Windows.Forms.Label lblPageInfo;
    }
}
