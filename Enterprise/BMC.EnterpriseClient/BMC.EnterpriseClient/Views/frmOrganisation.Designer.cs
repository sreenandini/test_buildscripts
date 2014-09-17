namespace BMC.EnterpriseClient.Views
{
    partial class frmOrganisation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrganisation));
            this.trvOrganisation = new System.Windows.Forms.TreeView();
            this.cntMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newCompanyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tblMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.cntMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newSubCompanyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cntMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newSiteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cntMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cntMenuStrip1.SuspendLayout();
            this.tblMainFrame.SuspendLayout();
            this.cntMenuStrip2.SuspendLayout();
            this.cntMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvOrganisation
            // 
            this.trvOrganisation.ContextMenuStrip = this.cntMenuStrip1;
            this.trvOrganisation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvOrganisation.FullRowSelect = true;
            this.trvOrganisation.HideSelection = false;
            this.trvOrganisation.Location = new System.Drawing.Point(0, 0);
            this.trvOrganisation.Margin = new System.Windows.Forms.Padding(0);
            this.trvOrganisation.Name = "trvOrganisation";
            this.trvOrganisation.Size = new System.Drawing.Size(300, 690);
            this.trvOrganisation.TabIndex = 0;
            this.trvOrganisation.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trvOrganisation_MouseClick);
            // 
            // cntMenuStrip1
            // 
            this.cntMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCompanyToolStripMenuItem});
            this.cntMenuStrip1.Name = "cntMenuStrip1";
            this.cntMenuStrip1.Size = new System.Drawing.Size(154, 26);
            // 
            // newCompanyToolStripMenuItem
            // 
            this.newCompanyToolStripMenuItem.Name = "newCompanyToolStripMenuItem";
            this.newCompanyToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.newCompanyToolStripMenuItem.Text = "New Company";
            this.newCompanyToolStripMenuItem.Click += new System.EventHandler(this.newCompanyToolStripMenuItem_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(300, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(0);
            this.pnlContent.Name = "pnlContent";
            this.tblMainFrame.SetRowSpan(this.pnlContent, 2);
            this.pnlContent.Size = new System.Drawing.Size(708, 730);
            this.pnlContent.TabIndex = 1;
            // 
            // tblMainFrame
            // 
            this.tblMainFrame.AutoScroll = true;
            this.tblMainFrame.ColumnCount = 2;
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.Controls.Add(this.pnlContent, 1, 0);
            this.tblMainFrame.Controls.Add(this.trvOrganisation, 0, 0);
            this.tblMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblMainFrame.Margin = new System.Windows.Forms.Padding(0);
            this.tblMainFrame.Name = "tblMainFrame";
            this.tblMainFrame.RowCount = 2;
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainFrame.Size = new System.Drawing.Size(1008, 730);
            this.tblMainFrame.TabIndex = 1;
            // 
            // cntMenuStrip2
            // 
            this.cntMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSubCompanyToolStripMenuItem,
            this.newSiteToolStripMenuItem});
            this.cntMenuStrip2.Name = "cntMenuStrip2";
            this.cntMenuStrip2.Size = new System.Drawing.Size(174, 48);
            // 
            // newSubCompanyToolStripMenuItem
            // 
            this.newSubCompanyToolStripMenuItem.Name = "newSubCompanyToolStripMenuItem";
            this.newSubCompanyToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.newSubCompanyToolStripMenuItem.Text = "New Company";
            this.newSubCompanyToolStripMenuItem.Click += new System.EventHandler(this.newCompanyToolStripMenuItem_Click);
            // 
            // newSiteToolStripMenuItem
            // 
            this.newSiteToolStripMenuItem.Name = "newSiteToolStripMenuItem";
            this.newSiteToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.newSiteToolStripMenuItem.Text = "New SubCompany";
            this.newSiteToolStripMenuItem.Click += new System.EventHandler(this.newSiteToolStripMenuItem_Click);
            // 
            // cntMenuStrip3
            // 
            this.cntMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSiteToolStripMenuItem1});
            this.cntMenuStrip3.Name = "contextMenuStrip1";
            this.cntMenuStrip3.Size = new System.Drawing.Size(121, 26);
            // 
            // newSiteToolStripMenuItem1
            // 
            this.newSiteToolStripMenuItem1.Name = "newSiteToolStripMenuItem1";
            this.newSiteToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.newSiteToolStripMenuItem1.Text = "New Site";
            this.newSiteToolStripMenuItem1.Click += new System.EventHandler(this.newSiteToolStripMenuItem1_Click);
            // 
            // cntMenuStrip4
            // 
            this.cntMenuStrip4.Name = "contextMenuStrip1";
            this.cntMenuStrip4.Size = new System.Drawing.Size(61, 4);
            // 
            // frmOrganisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.tblMainFrame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOrganisation";
            this.Text = "s";
            this.Load += new System.EventHandler(this.frmOrganisation_Load);
            this.cntMenuStrip1.ResumeLayout(false);
            this.tblMainFrame.ResumeLayout(false);
            this.cntMenuStrip2.ResumeLayout(false);
            this.cntMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvOrganisation;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TableLayoutPanel tblMainFrame;
        private System.Windows.Forms.ContextMenuStrip cntMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newCompanyToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cntMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem newSubCompanyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSiteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cntMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem newSiteToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip cntMenuStrip4;
    }
}