namespace BMC.EnterpriseClient.Views
{
    partial class frmAdminBarPos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdminBarPos));
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tblMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.tblLowerButton = new System.Windows.Forms.TableLayoutPanel();
            this.ucBarPosition1 = new BMC.EnterpriseClient.Views.ucBarPosition();
            this.tblMainFrame.SuspendLayout();
            this.tblLowerButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(417, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 28);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(297, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tblMainFrame
            // 
            this.tblMainFrame.ColumnCount = 1;
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.Controls.Add(this.tblLowerButton, 0, 1);
            this.tblMainFrame.Controls.Add(this.ucBarPosition1, 0, 0);
            this.tblMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblMainFrame.Name = "tblMainFrame";
            this.tblMainFrame.RowCount = 2;
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainFrame.Size = new System.Drawing.Size(526, 435);
            this.tblMainFrame.TabIndex = 0;
            // 
            // tblLowerButton
            // 
            this.tblLowerButton.ColumnCount = 3;
            this.tblLowerButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowerButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowerButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowerButton.Controls.Add(this.btnCancel, 1, 0);
            this.tblLowerButton.Controls.Add(this.btnApply, 2, 0);
            this.tblLowerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLowerButton.Location = new System.Drawing.Point(3, 398);
            this.tblLowerButton.Name = "tblLowerButton";
            this.tblLowerButton.RowCount = 1;
            this.tblLowerButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowerButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblLowerButton.Size = new System.Drawing.Size(520, 34);
            this.tblLowerButton.TabIndex = 1;
            // 
            // ucBarPosition1
            // 
            this.ucBarPosition1.BarPositionID = 0;
            this.ucBarPosition1.BarPositionName = "";
            this.ucBarPosition1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBarPosition1.Location = new System.Drawing.Point(3, 3);
            this.ucBarPosition1.Name = "ucBarPosition1";
            this.ucBarPosition1.SiteId = 0;
            this.ucBarPosition1.Size = new System.Drawing.Size(520, 389);
            this.ucBarPosition1.TabIndex = 0;
            this.ucBarPosition1.Load += new System.EventHandler(this.ucBarPosition1_Load);
            // 
            // frmAdminBarPos
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(526, 435);
            this.Controls.Add(this.tblMainFrame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdminBarPos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BarPosition";
            this.tblMainFrame.ResumeLayout(false);
            this.tblLowerButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ucBarPosition ucBarPosition1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tblMainFrame;
        private System.Windows.Forms.TableLayoutPanel tblLowerButton;

    }
}