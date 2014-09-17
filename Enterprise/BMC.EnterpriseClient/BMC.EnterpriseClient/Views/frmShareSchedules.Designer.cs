namespace BMC.EnterpriseClient.Views
{
    partial class frmShareSchedules
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lvShareSchedules = new BMC.CoreLib.Win32.ListViewEx();
            this.clmHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHeaderNoofBands = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmHeaderStartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctMenuItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.IDM_EDIT_SCHEDULE = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_NEW_SCHEDULE = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.ctMenuItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lvShareSchedules, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(988, 490);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.Controls.Add(this.btnClose, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnEdit, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 450);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(988, 40);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(871, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 28);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(751, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(114, 28);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(631, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(114, 28);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lvShareSchedules
            // 
            this.lvShareSchedules.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvShareSchedules.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvShareSchedules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmHeaderName,
            this.clmHeaderDescription,
            this.clmHeaderNoofBands,
            this.clmHeaderStartDate});
            this.lvShareSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvShareSchedules.FullRowSelect = true;
            this.lvShareSchedules.GridLines = true;
            this.lvShareSchedules.HideSelection = false;
            this.lvShareSchedules.Location = new System.Drawing.Point(3, 3);
            this.lvShareSchedules.Name = "lvShareSchedules";
            this.lvShareSchedules.Size = new System.Drawing.Size(982, 444);
            this.lvShareSchedules.TabIndex = 1;
            this.lvShareSchedules.UseCompatibleStateImageBehavior = false;
            this.lvShareSchedules.View = System.Windows.Forms.View.Details;
            this.lvShareSchedules.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvShareSchedules_MouseDoubleClick);
            this.lvShareSchedules.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvShareSchedules_MouseUp);
            // 
            // clmHeaderName
            // 
            this.clmHeaderName.Text = "Name";
            this.clmHeaderName.Width = 100;
            // 
            // clmHeaderDescription
            // 
            this.clmHeaderDescription.Text = "Description";
            this.clmHeaderDescription.Width = 100;
            // 
            // clmHeaderNoofBands
            // 
            this.clmHeaderNoofBands.Text = "No of Bands";
            this.clmHeaderNoofBands.Width = 100;
            // 
            // clmHeaderStartDate
            // 
            this.clmHeaderStartDate.Text = "Start Date";
            this.clmHeaderStartDate.Width = 100;
            // 
            // ctMenuItems
            // 
            this.ctMenuItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_EDIT_SCHEDULE,
            this.IDM_NEW_SCHEDULE});
            this.ctMenuItems.Name = "ctMenuItems";
            this.ctMenuItems.Size = new System.Drawing.Size(102, 48);
            // 
            // IDM_EDIT_SCHEDULE
            // 
            this.IDM_EDIT_SCHEDULE.Name = "IDM_EDIT_SCHEDULE";
            this.IDM_EDIT_SCHEDULE.Size = new System.Drawing.Size(101, 22);
            this.IDM_EDIT_SCHEDULE.Text = "Edit";
            this.IDM_EDIT_SCHEDULE.Click += new System.EventHandler(this.IDM_EDIT_SCHEDULE_Click);
            // 
            // IDM_NEW_SCHEDULE
            // 
            this.IDM_NEW_SCHEDULE.Name = "IDM_NEW_SCHEDULE";
            this.IDM_NEW_SCHEDULE.Size = new System.Drawing.Size(101, 22);
            this.IDM_NEW_SCHEDULE.Text = "New ";
            this.IDM_NEW_SCHEDULE.Click += new System.EventHandler(this.IDM_NEW_SCHEDULE_Click);
            // 
            // frmShareSchedules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 490);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShareSchedules";
            this.Text = "Share Schedules";
            this.Load += new System.EventHandler(this.frmShareSchedules_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ctMenuItems.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip ctMenuItems;
        private System.Windows.Forms.ToolStripMenuItem IDM_EDIT_SCHEDULE;
        private System.Windows.Forms.ToolStripMenuItem IDM_NEW_SCHEDULE;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private CoreLib.Win32.ListViewEx lvShareSchedules;
        private System.Windows.Forms.ColumnHeader clmHeaderName;
        private System.Windows.Forms.ColumnHeader clmHeaderDescription;
        private System.Windows.Forms.ColumnHeader clmHeaderNoofBands;
        private System.Windows.Forms.ColumnHeader clmHeaderStartDate;
    }
}