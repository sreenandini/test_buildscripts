namespace BMC.CoreLib.Win32
{
    partial class AxAsyncProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AxAsyncProgress));
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.tblItems = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progbarValue = new BMC.CoreLib.Win32.ContinuousProgress();
            this.tblItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "ok");
            this.imglstSmallIcons.Images.SetKeyName(1, "cancel");
            // 
            // tblItems
            // 
            this.tblItems.ColumnCount = 2;
            this.tblItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblItems.Controls.Add(this.lblStatus, 0, 0);
            this.tblItems.Controls.Add(this.btnCancel, 1, 0);
            this.tblItems.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblItems.Location = new System.Drawing.Point(0, 50);
            this.tblItems.Name = "tblItems";
            this.tblItems.RowCount = 1;
            this.tblItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblItems.Size = new System.Drawing.Size(690, 33);
            this.tblItems.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(6, 10);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(558, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Loading . . .";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ImageKey = "cancel";
            this.btnCancel.ImageList = this.imglstSmallIcons;
            this.btnCancel.Location = new System.Drawing.Point(576, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // progbarValue
            // 
            this.progbarValue.ActiveColor = System.Drawing.Color.Maroon;
            this.progbarValue.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.progbarValue.CycleSpeed = 7000;
            this.progbarValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progbarValue.ForeColor = System.Drawing.Color.ForestGreen;
            this.progbarValue.Location = new System.Drawing.Point(0, 0);
            this.progbarValue.Name = "progbarValue";
            this.progbarValue.ShapeSize = 10;
            this.progbarValue.ShapeSpacing = 10;
            this.progbarValue.ShapeToDraw = BMC.CoreLib.Win32.ContinuousProgress.ShapeType.Square;
            this.progbarValue.Size = new System.Drawing.Size(690, 50);
            this.progbarValue.TabIndex = 0;
            this.progbarValue.Text = "continuousProgress1";
            // 
            // AxAsyncProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progbarValue);
            this.Controls.Add(this.tblItems);
            this.Name = "AxAsyncProgress";
            this.Size = new System.Drawing.Size(690, 83);
            this.tblItems.ResumeLayout(false);
            this.tblItems.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.TableLayoutPanel tblItems;
        private System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.Button btnCancel;
        private ContinuousProgress progbarValue;
    }
}
