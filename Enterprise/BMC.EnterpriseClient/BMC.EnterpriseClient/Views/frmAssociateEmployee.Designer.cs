namespace BMC.EnterpriseClient.Views
{
    partial class frmAssociateEmployee
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbCardLevel = new System.Windows.Forms.ComboBox();
            this.lblCardLevel = new System.Windows.Forms.Label();
            this.txtUserGroup = new System.Windows.Forms.TextBox();
            this.lblUserGroup = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpAssociateModes = new System.Windows.Forms.TabPage();
            this.ucAssociateEmployeeGMUModes1 = new BMC.EnterpriseClient.Views.ucAssociateEmployeeGMUModes();
            this.tbpAssociateEvents = new System.Windows.Forms.TabPage();
            this.ucAssoicateEmployeeEvents1 = new BMC.EnterpriseClient.Views.ucAssoicateEmployeeEvents();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpAssociateModes.SuspendLayout();
            this.tbpAssociateEvents.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.872385F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.87385F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.253764F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 534);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnClose, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(678, 500);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 34);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(103, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 28);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbCardLevel);
            this.panel1.Controls.Add(this.lblCardLevel);
            this.panel1.Controls.Add(this.txtUserGroup);
            this.panel1.Controls.Add(this.lblUserGroup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(875, 42);
            this.panel1.TabIndex = 0;
            // 
            // cmbCardLevel
            // 
            this.cmbCardLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardLevel.FormattingEnabled = true;
            this.cmbCardLevel.Location = new System.Drawing.Point(412, 8);
            this.cmbCardLevel.Name = "cmbCardLevel";
            this.cmbCardLevel.Size = new System.Drawing.Size(194, 21);
            this.cmbCardLevel.TabIndex = 3;
            this.cmbCardLevel.SelectedIndexChanged += new System.EventHandler(this.cmbCardLevel_SelectedIndexChanged);
            // 
            // lblCardLevel
            // 
            this.lblCardLevel.AutoSize = true;
            this.lblCardLevel.Location = new System.Drawing.Point(277, 11);
            this.lblCardLevel.Name = "lblCardLevel";
            this.lblCardLevel.Size = new System.Drawing.Size(61, 13);
            this.lblCardLevel.TabIndex = 2;
            this.lblCardLevel.Text = "Card Level:";
            // 
            // txtUserGroup
            // 
            this.txtUserGroup.Location = new System.Drawing.Point(93, 8);
            this.txtUserGroup.Name = "txtUserGroup";
            this.txtUserGroup.ReadOnly = true;
            this.txtUserGroup.Size = new System.Drawing.Size(155, 20);
            this.txtUserGroup.TabIndex = 1;
            // 
            // lblUserGroup
            // 
            this.lblUserGroup.AutoSize = true;
            this.lblUserGroup.Location = new System.Drawing.Point(9, 11);
            this.lblUserGroup.Name = "lblUserGroup";
            this.lblUserGroup.Size = new System.Drawing.Size(64, 13);
            this.lblUserGroup.TabIndex = 0;
            this.lblUserGroup.Text = "User Group:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpAssociateModes);
            this.tabControl1.Controls.Add(this.tbpAssociateEvents);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 42);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(875, 458);
            this.tabControl1.TabIndex = 6;
            // 
            // tbpAssociateModes
            // 
            this.tbpAssociateModes.BackColor = System.Drawing.SystemColors.Control;
            this.tbpAssociateModes.Controls.Add(this.ucAssociateEmployeeGMUModes1);
            this.tbpAssociateModes.Location = new System.Drawing.Point(4, 22);
            this.tbpAssociateModes.Name = "tbpAssociateModes";
            this.tbpAssociateModes.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAssociateModes.Size = new System.Drawing.Size(867, 432);
            this.tbpAssociateModes.TabIndex = 0;
            this.tbpAssociateModes.Text = "Associate Mode(s)";
            // 
            // ucAssociateEmployeeGMUModes1
            // 
            this.ucAssociateEmployeeGMUModes1.BackColor = System.Drawing.SystemColors.Control;
            this.ucAssociateEmployeeGMUModes1.CardLevel = null;
            this.ucAssociateEmployeeGMUModes1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAssociateEmployeeGMUModes1.Location = new System.Drawing.Point(3, 3);
            this.ucAssociateEmployeeGMUModes1.Name = "ucAssociateEmployeeGMUModes1";
            this.ucAssociateEmployeeGMUModes1.RoleID = 0;
            this.ucAssociateEmployeeGMUModes1.RoleName = null;
            this.ucAssociateEmployeeGMUModes1.Size = new System.Drawing.Size(186, 68);
            this.ucAssociateEmployeeGMUModes1.TabIndex = 0;
            // 
            // tbpAssociateEvents
            // 
            this.tbpAssociateEvents.BackColor = System.Drawing.SystemColors.Control;
            this.tbpAssociateEvents.Controls.Add(this.ucAssoicateEmployeeEvents1);
            this.tbpAssociateEvents.Location = new System.Drawing.Point(4, 22);
            this.tbpAssociateEvents.Name = "tbpAssociateEvents";
            this.tbpAssociateEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAssociateEvents.Size = new System.Drawing.Size(867, 432);
            this.tbpAssociateEvents.TabIndex = 1;
            this.tbpAssociateEvents.Text = "Associate Event(s)";
            // 
            // ucAssoicateEmployeeEvents1
            // 
            this.ucAssoicateEmployeeEvents1.BackColor = System.Drawing.SystemColors.Control;
            this.ucAssoicateEmployeeEvents1.CardLevel = null;
            this.ucAssoicateEmployeeEvents1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAssoicateEmployeeEvents1.Location = new System.Drawing.Point(3, 3);
            this.ucAssoicateEmployeeEvents1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ucAssoicateEmployeeEvents1.Name = "ucAssoicateEmployeeEvents1";
            this.ucAssoicateEmployeeEvents1.RoleID = 0;
            this.ucAssoicateEmployeeEvents1.RoleName = null;
            this.ucAssoicateEmployeeEvents1.Size = new System.Drawing.Size(861, 434);
            this.ucAssoicateEmployeeEvents1.TabIndex = 0;
            // 
            // frmAssociateEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 534);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmAssociateEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GMU Mode/Event Access";
            this.Load += new System.EventHandler(this.frmAssociateEmployee_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbpAssociateModes.ResumeLayout(false);
            this.tbpAssociateEvents.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtUserGroup;
        private System.Windows.Forms.Label lblUserGroup;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpAssociateModes;
        private System.Windows.Forms.TabPage tbpAssociateEvents;
        private ucAssociateEmployeeGMUModes ucAssociateEmployeeGMUModes1;
        private ucAssoicateEmployeeEvents ucAssoicateEmployeeEvents1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbCardLevel;
        private System.Windows.Forms.Label lblCardLevel;




    }
}