namespace BMC.EnterpriseClient.Views
{
    partial class frmAdminSiteClass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdminSiteClass));
            this.LvSiteClass = new System.Windows.Forms.ListBox();
            this.grpName = new System.Windows.Forms.GroupBox();
            this.tblClassName = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.BtnAddNewOp = new System.Windows.Forms.Button();
            this.BtnUpdateOp = new System.Windows.Forms.Button();
            this.BtnEditOp = new System.Windows.Forms.Button();
            this.BtnDeleteOp = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.tblMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.tblLowwerButtons = new System.Windows.Forms.TableLayoutPanel();
            this.tblUpperControls = new System.Windows.Forms.TableLayoutPanel();
            this.grpName.SuspendLayout();
            this.tblClassName.SuspendLayout();
            this.tblMainFrame.SuspendLayout();
            this.tblLowwerButtons.SuspendLayout();
            this.tblUpperControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // LvSiteClass
            // 
            this.LvSiteClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LvSiteClass.FormattingEnabled = true;
            this.LvSiteClass.Location = new System.Drawing.Point(3, 3);
            this.LvSiteClass.Name = "LvSiteClass";
            this.LvSiteClass.Size = new System.Drawing.Size(359, 220);
            this.LvSiteClass.TabIndex = 0;
            this.LvSiteClass.SelectedIndexChanged += new System.EventHandler(this.LvSiteClass_SelectedIndexChanged);
            // 
            // grpName
            // 
            this.grpName.Controls.Add(this.tblClassName);
            this.grpName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpName.Location = new System.Drawing.Point(3, 229);
            this.grpName.Name = "grpName";
            this.grpName.Size = new System.Drawing.Size(359, 62);
            this.grpName.TabIndex = 1;
            this.grpName.TabStop = false;
            // 
            // tblClassName
            // 
            this.tblClassName.ColumnCount = 1;
            this.tblClassName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblClassName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblClassName.Controls.Add(this.label1, 0, 0);
            this.tblClassName.Controls.Add(this.txtName, 0, 1);
            this.tblClassName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblClassName.Location = new System.Drawing.Point(3, 16);
            this.tblClassName.Name = "tblClassName";
            this.tblClassName.RowCount = 2;
            this.tblClassName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tblClassName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblClassName.Size = new System.Drawing.Size(353, 43);
            this.tblClassName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(3, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(347, 20);
            this.txtName.TabIndex = 1;
            // 
            // BtnAddNewOp
            // 
            this.BtnAddNewOp.Location = new System.Drawing.Point(3, 3);
            this.BtnAddNewOp.Name = "BtnAddNewOp";
            this.BtnAddNewOp.Size = new System.Drawing.Size(87, 28);
            this.BtnAddNewOp.TabIndex = 0;
            this.BtnAddNewOp.Text = "AddNew";
            this.BtnAddNewOp.UseVisualStyleBackColor = true;
            this.BtnAddNewOp.Click += new System.EventHandler(this.BtnAddNewOp_Click);
            // 
            // BtnUpdateOp
            // 
            this.BtnUpdateOp.Location = new System.Drawing.Point(123, 3);
            this.BtnUpdateOp.Name = "BtnUpdateOp";
            this.BtnUpdateOp.Size = new System.Drawing.Size(87, 28);
            this.BtnUpdateOp.TabIndex = 1;
            this.BtnUpdateOp.Text = "Update";
            this.BtnUpdateOp.UseVisualStyleBackColor = true;
            this.BtnUpdateOp.Click += new System.EventHandler(this.BtnUpdateOp_Click);
            // 
            // BtnEditOp
            // 
            this.BtnEditOp.Location = new System.Drawing.Point(243, 3);
            this.BtnEditOp.Name = "BtnEditOp";
            this.BtnEditOp.Size = new System.Drawing.Size(87, 28);
            this.BtnEditOp.TabIndex = 2;
            this.BtnEditOp.Text = "Edit";
            this.BtnEditOp.UseVisualStyleBackColor = true;
            this.BtnEditOp.Click += new System.EventHandler(this.BtnEditOp_Click);
            // 
            // BtnDeleteOp
            // 
            this.BtnDeleteOp.Location = new System.Drawing.Point(363, 3);
            this.BtnDeleteOp.Name = "BtnDeleteOp";
            this.BtnDeleteOp.Size = new System.Drawing.Size(87, 28);
            this.BtnDeleteOp.TabIndex = 3;
            this.BtnDeleteOp.Text = "Delete";
            this.BtnDeleteOp.UseVisualStyleBackColor = true;
            this.BtnDeleteOp.Click += new System.EventHandler(this.BtnDeleteOp_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(483, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(87, 28);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // tblMainFrame
            // 
            this.tblMainFrame.ColumnCount = 1;
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMainFrame.Controls.Add(this.tblLowwerButtons, 0, 1);
            this.tblMainFrame.Controls.Add(this.tblUpperControls, 0, 0);
            this.tblMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblMainFrame.Name = "tblMainFrame";
            this.tblMainFrame.RowCount = 2;
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainFrame.Size = new System.Drawing.Size(371, 340);
            this.tblMainFrame.TabIndex = 0;
            // 
            // tblLowwerButtons
            // 
            this.tblLowwerButtons.ColumnCount = 5;
            this.tblLowwerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowwerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowwerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowwerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowwerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowwerButtons.Controls.Add(this.BtnAddNewOp, 0, 0);
            this.tblLowwerButtons.Controls.Add(this.BtnUpdateOp, 1, 0);
            this.tblLowwerButtons.Controls.Add(this.BtnDeleteOp, 3, 0);
            this.tblLowwerButtons.Controls.Add(this.BtnClose, 4, 0);
            this.tblLowwerButtons.Controls.Add(this.BtnEditOp, 2, 0);
            this.tblLowwerButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLowwerButtons.Location = new System.Drawing.Point(3, 303);
            this.tblLowwerButtons.Name = "tblLowwerButtons";
            this.tblLowwerButtons.RowCount = 1;
            this.tblLowwerButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLowwerButtons.Size = new System.Drawing.Size(365, 34);
            this.tblLowwerButtons.TabIndex = 1;
            // 
            // tblUpperControls
            // 
            this.tblUpperControls.ColumnCount = 1;
            this.tblUpperControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblUpperControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblUpperControls.Controls.Add(this.LvSiteClass, 0, 0);
            this.tblUpperControls.Controls.Add(this.grpName, 0, 1);
            this.tblUpperControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblUpperControls.Location = new System.Drawing.Point(3, 3);
            this.tblUpperControls.Name = "tblUpperControls";
            this.tblUpperControls.RowCount = 2;
            this.tblUpperControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblUpperControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tblUpperControls.Size = new System.Drawing.Size(365, 294);
            this.tblUpperControls.TabIndex = 0;
            // 
            // frmAdminSiteClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 340);
            this.Controls.Add(this.tblMainFrame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdminSiteClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Site Classification";
            this.Load += new System.EventHandler(this.frmAdminSiteClass_Load);
            this.grpName.ResumeLayout(false);
            this.tblClassName.ResumeLayout(false);
            this.tblClassName.PerformLayout();
            this.tblMainFrame.ResumeLayout(false);
            this.tblLowwerButtons.ResumeLayout(false);
            this.tblUpperControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LvSiteClass;
        private System.Windows.Forms.GroupBox grpName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button BtnAddNewOp;
        private System.Windows.Forms.Button BtnUpdateOp;
        private System.Windows.Forms.Button BtnEditOp;
        private System.Windows.Forms.Button BtnDeleteOp;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.TableLayoutPanel tblMainFrame;
        private System.Windows.Forms.TableLayoutPanel tblLowwerButtons;
        private System.Windows.Forms.TableLayoutPanel tblUpperControls;
        private System.Windows.Forms.TableLayoutPanel tblClassName;
    }
}