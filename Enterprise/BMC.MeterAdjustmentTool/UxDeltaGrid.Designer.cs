namespace BMC.MeterAdjustmentTool
{
    partial class UxDeltaGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UxDeltaGrid));
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.uxHeader = new BMC.MeterAdjustmentTool.Helpers.GradientHeader();
            this.pnlContainer.SuspendLayout();
            this.tblContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.tblContainer);
            this.pnlContainer.Controls.Add(this.uxHeader);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(668, 290);
            this.pnlContainer.TabIndex = 1;
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.btnEdit, 0, 1);
            this.tblContainer.Controls.Add(this.dgvData, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 23);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblContainer.Size = new System.Drawing.Size(668, 267);
            this.tblContainer.TabIndex = 2;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEdit.ImageKey = "Component.ico";
            this.btnEdit.ImageList = this.imglstSmallIcons;
            this.btnEdit.Location = new System.Drawing.Point(548, 235);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 28);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Component.ico");
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvData.Location = new System.Drawing.Point(0, 3);
            this.dgvData.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.ShowCellErrors = false;
            this.dgvData.ShowCellToolTips = false;
            this.dgvData.ShowEditingIcon = false;
            this.dgvData.ShowRowErrors = false;
            this.dgvData.Size = new System.Drawing.Size(668, 226);
            this.dgvData.TabIndex = 5;
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
            this.dgvData.DoubleClick += new System.EventHandler(this.dgvData_DoubleClick);
            // 
            // uxHeader
            // 
            this.uxHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxHeader.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            this.uxHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxHeader.ForeColor = System.Drawing.Color.White;
            this.uxHeader.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uxHeader.Location = new System.Drawing.Point(0, 0);
            this.uxHeader.Name = "uxHeader";
            this.uxHeader.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.uxHeader.RepeatGradient = false;
            this.uxHeader.Size = new System.Drawing.Size(668, 23);
            this.uxHeader.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.uxHeader.TabIndex = 1;
            this.uxHeader.Text = "[HEADER]";
            // 
            // UxDeltaGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContainer);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UxDeltaGrid";
            this.Size = new System.Drawing.Size(668, 290);
            this.pnlContainer.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private BMC.MeterAdjustmentTool.Helpers.GradientHeader uxHeader;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.DataGridView dgvData;
    }
}
