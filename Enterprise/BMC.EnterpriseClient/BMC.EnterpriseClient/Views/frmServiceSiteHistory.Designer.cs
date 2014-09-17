namespace BMC.EnterpriseClient.Views
{
    partial class frmServiceSiteHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServiceSiteHistory));
            this.tblInner = new System.Windows.Forms.TableLayoutPanel();
            this.gBOpen = new System.Windows.Forms.GroupBox();
            this.dgvSiteOpenCalls = new System.Windows.Forms.DataGridView();
            this.gBSite = new System.Windows.Forms.GroupBox();
            this.gBClosed = new System.Windows.Forms.GroupBox();
            this.dgvSiteClosedCalls = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.lblSite = new System.Windows.Forms.Label();
            this.tblInner.SuspendLayout();
            this.gBOpen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiteOpenCalls)).BeginInit();
            this.gBSite.SuspendLayout();
            this.gBClosed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiteClosedCalls)).BeginInit();
            this.tblMain.SuspendLayout();
            this.tblFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblInner
            // 
            this.tblInner.ColumnCount = 1;
            this.tblInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInner.Controls.Add(this.gBOpen, 0, 1);
            this.tblInner.Controls.Add(this.gBSite, 0, 0);
            this.tblInner.Controls.Add(this.gBClosed, 0, 2);
            this.tblInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInner.Location = new System.Drawing.Point(0, 0);
            this.tblInner.Margin = new System.Windows.Forms.Padding(0);
            this.tblInner.Name = "tblInner";
            this.tblInner.RowCount = 3;
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInner.Size = new System.Drawing.Size(804, 610);
            this.tblInner.TabIndex = 0;
            // 
            // gBOpen
            // 
            this.gBOpen.Controls.Add(this.dgvSiteOpenCalls);
            this.gBOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBOpen.ForeColor = System.Drawing.Color.Black;
            this.gBOpen.Location = new System.Drawing.Point(3, 78);
            this.gBOpen.Name = "gBOpen";
            this.gBOpen.Size = new System.Drawing.Size(798, 144);
            this.gBOpen.TabIndex = 1;
            this.gBOpen.TabStop = false;
            this.gBOpen.Text = "Open Calls";
            // 
            // dgvSiteOpenCalls
            // 
            this.dgvSiteOpenCalls.AllowUserToAddRows = false;
            this.dgvSiteOpenCalls.AllowUserToDeleteRows = false;
            this.dgvSiteOpenCalls.AllowUserToResizeRows = false;
            this.dgvSiteOpenCalls.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvSiteOpenCalls.BackgroundColor = System.Drawing.Color.White;
            this.dgvSiteOpenCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSiteOpenCalls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSiteOpenCalls.Location = new System.Drawing.Point(3, 16);
            this.dgvSiteOpenCalls.Name = "dgvSiteOpenCalls";
            this.dgvSiteOpenCalls.ReadOnly = true;
            this.dgvSiteOpenCalls.RowHeadersVisible = false;
            this.dgvSiteOpenCalls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSiteOpenCalls.Size = new System.Drawing.Size(792, 125);
            this.dgvSiteOpenCalls.TabIndex = 1;
            // 
            // gBSite
            // 
            this.gBSite.Controls.Add(this.lblSite);
            this.gBSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBSite.ForeColor = System.Drawing.Color.Black;
            this.gBSite.Location = new System.Drawing.Point(3, 3);
            this.gBSite.Name = "gBSite";
            this.gBSite.Size = new System.Drawing.Size(798, 69);
            this.gBSite.TabIndex = 0;
            this.gBSite.TabStop = false;
            this.gBSite.Text = "Site";
            // 
            // gBClosed
            // 
            this.gBClosed.Controls.Add(this.dgvSiteClosedCalls);
            this.gBClosed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBClosed.ForeColor = System.Drawing.Color.Black;
            this.gBClosed.Location = new System.Drawing.Point(3, 228);
            this.gBClosed.Name = "gBClosed";
            this.gBClosed.Size = new System.Drawing.Size(798, 379);
            this.gBClosed.TabIndex = 2;
            this.gBClosed.TabStop = false;
            this.gBClosed.Text = "Closed Calls";
            // 
            // dgvSiteClosedCalls
            // 
            this.dgvSiteClosedCalls.AllowUserToAddRows = false;
            this.dgvSiteClosedCalls.AllowUserToDeleteRows = false;
            this.dgvSiteClosedCalls.AllowUserToResizeRows = false;
            this.dgvSiteClosedCalls.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvSiteClosedCalls.BackgroundColor = System.Drawing.Color.White;
            this.dgvSiteClosedCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSiteClosedCalls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSiteClosedCalls.Location = new System.Drawing.Point(3, 16);
            this.dgvSiteClosedCalls.Name = "dgvSiteClosedCalls";
            this.dgvSiteClosedCalls.ReadOnly = true;
            this.dgvSiteClosedCalls.RowHeadersVisible = false;
            this.dgvSiteClosedCalls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSiteClosedCalls.Size = new System.Drawing.Size(792, 360);
            this.dgvSiteClosedCalls.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(701, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.tblInner, 0, 0);
            this.tblMain.Controls.Add(this.tblFooter, 0, 1);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 2;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMain.Size = new System.Drawing.Size(804, 650);
            this.tblMain.TabIndex = 1;
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 2;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.Controls.Add(this.btnClose, 1, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFooter.Location = new System.Drawing.Point(0, 610);
            this.tblFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 1;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.Size = new System.Drawing.Size(804, 40);
            this.tblFooter.TabIndex = 1;
            // 
            // lblSite
            // 
            this.lblSite.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSite.Location = new System.Drawing.Point(3, 16);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(792, 50);
            this.lblSite.TabIndex = 1;
            this.lblSite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmServiceSiteHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(804, 650);
            this.Controls.Add(this.tblMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmServiceSiteHistory";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Site History";
            this.tblInner.ResumeLayout(false);
            this.gBOpen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiteOpenCalls)).EndInit();
            this.gBSite.ResumeLayout(false);
            this.gBClosed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiteClosedCalls)).EndInit();
            this.tblMain.ResumeLayout(false);
            this.tblFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblInner;
        private System.Windows.Forms.GroupBox gBSite;
        private System.Windows.Forms.GroupBox gBOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox gBClosed;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.TableLayoutPanel tblFooter;
        private System.Windows.Forms.DataGridView dgvSiteOpenCalls;
        private System.Windows.Forms.DataGridView dgvSiteClosedCalls;
        private System.Windows.Forms.Label lblSite;
    }
}