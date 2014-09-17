namespace BMC.MeterAdjustmentTool
{
    partial class CalculatedMeterInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculatedMeterInfoForm));
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.dgvMeterInfo = new System.Windows.Forms.DataGridView();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.uxHeader = new BMC.MeterAdjustmentTool.Helpers.GradientHeader();
            this.pnlContainer.SuspendLayout();
            this.tblContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeterInfo)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(0, 283);
            this.pnlBottom.Size = new System.Drawing.Size(594, 35);
            this.pnlBottom.Visible = false;
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlContent);
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(6);
            this.pnlContainer.Size = new System.Drawing.Size(594, 283);
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.btnClose, 0, 1);
            this.tblContainer.Controls.Add(this.dgvMeterInfo, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 23);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblContainer.Size = new System.Drawing.Size(582, 248);
            this.tblContainer.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageList = this.imglstSmallIcons;
            this.btnClose.Location = new System.Drawing.Point(462, 216);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 28);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Cancel.ico");
            // 
            // dgvMeterInfo
            // 
            this.dgvMeterInfo.AllowUserToAddRows = false;
            this.dgvMeterInfo.AllowUserToDeleteRows = false;
            this.dgvMeterInfo.AllowUserToOrderColumns = true;
            this.dgvMeterInfo.AllowUserToResizeColumns = false;
            this.dgvMeterInfo.AllowUserToResizeRows = false;
            this.dgvMeterInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMeterInfo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMeterInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMeterInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMeterInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMeterInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMeterInfo.Location = new System.Drawing.Point(0, 3);
            this.dgvMeterInfo.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.dgvMeterInfo.Name = "dgvMeterInfo";
            this.dgvMeterInfo.RowHeadersVisible = false;
            this.dgvMeterInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMeterInfo.ShowCellErrors = false;
            this.dgvMeterInfo.ShowCellToolTips = false;
            this.dgvMeterInfo.ShowEditingIcon = false;
            this.dgvMeterInfo.ShowRowErrors = false;
            this.dgvMeterInfo.Size = new System.Drawing.Size(582, 207);
            this.dgvMeterInfo.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.tblContainer);
            this.pnlContent.Controls.Add(this.uxHeader);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(6, 6);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(582, 271);
            this.pnlContent.TabIndex = 2;
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
            this.uxHeader.Size = new System.Drawing.Size(582, 23);
            this.uxHeader.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.uxHeader.TabIndex = 0;
            this.uxHeader.Text = "Related Meters";
            // 
            // CalculatedMeterInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(594, 318);
            this.Name = "CalculatedMeterInfoForm";
            this.Text = "Related Meters";
            this.pnlContainer.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeterInfo)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.DataGridView dgvMeterInfo;
        private BMC.MeterAdjustmentTool.Helpers.GradientHeader uxHeader;
    }
}