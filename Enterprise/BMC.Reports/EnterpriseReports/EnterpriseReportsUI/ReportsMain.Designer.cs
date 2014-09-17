namespace BMC.EnterpriseReportsUI
{
    partial class ReportsMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsMain));
            this.gpTree = new System.Windows.Forms.GroupBox();
            this.tvRoles = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlChild = new System.Windows.Forms.Panel();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.gpTree.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpTree
            // 
            this.gpTree.BackColor = System.Drawing.Color.Transparent;
            this.gpTree.Controls.Add(this.tvRoles);
            this.gpTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpTree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(102)))), ((int)(((byte)(136)))));
            this.gpTree.Location = new System.Drawing.Point(3, 3);
            this.gpTree.Name = "gpTree";
            this.gpTree.Size = new System.Drawing.Size(344, 627);
            this.gpTree.TabIndex = 0;
            this.gpTree.TabStop = false;
            this.gpTree.Text = "Reports Menu";
            // 
            // tvRoles
            // 
            this.tvRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRoles.Location = new System.Drawing.Point(3, 16);
            this.tvRoles.Name = "tvRoles";
            this.tvRoles.Size = new System.Drawing.Size(338, 608);
            this.tvRoles.TabIndex = 0;
            this.tvRoles.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvRoles_AfterCheck);
            this.tvRoles.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvRoles_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "imagesMenu.jpg");
            this.imageList1.Images.SetKeyName(1, "images.jpg");
            // 
            // pnlChild
            // 
            this.pnlChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChild.Location = new System.Drawing.Point(353, 3);
            this.pnlChild.Name = "pnlChild";
            this.pnlChild.Size = new System.Drawing.Size(481, 627);
            this.pnlChild.TabIndex = 1;
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 2;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.gpTree, 0, 0);
            this.tblContainer.Controls.Add(this.pnlChild, 1, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 1;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Size = new System.Drawing.Size(837, 633);
            this.tblContainer.TabIndex = 0;
            // 
            // ReportsMain
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(837, 633);
            this.Controls.Add(this.tblContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ReportsMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BMC Enterprise Reports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReportRoleAdmin_Load);
            this.gpTree.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpTree;
        private System.Windows.Forms.TreeView tvRoles;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel pnlChild;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
    }
}

