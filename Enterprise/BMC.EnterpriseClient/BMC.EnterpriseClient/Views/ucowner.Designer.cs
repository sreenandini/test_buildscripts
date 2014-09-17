namespace BMC.EnterpriseClient.Views
{
    partial class ucowner
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
            this.tblowner = new System.Windows.Forms.TableLayoutPanel();
            this.dtpickpsucod = new System.Windows.Forms.DateTimePicker();
            this.dtpicknsucod = new System.Windows.Forms.DateTimePicker();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.cmbSubcompany = new System.Windows.Forms.ComboBox();
            this.cmbpreviousSubcompany = new System.Windows.Forms.ComboBox();
            this.cmbNextsubcompany = new System.Windows.Forms.ComboBox();
            this.lblcompany = new System.Windows.Forms.Label();
            this.lblsubcompany = new System.Windows.Forms.Label();
            this.lblpsubcompany = new System.Windows.Forms.Label();
            this.lblnextsubcompany = new System.Windows.Forms.Label();
            this.lblpcod = new System.Windows.Forms.Label();
            this.lblnextcod = new System.Windows.Forms.Label();
            this.tblowner.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblowner
            // 
            this.tblowner.ColumnCount = 3;
            this.tblowner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblowner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblowner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblowner.Controls.Add(this.dtpickpsucod, 0, 5);
            this.tblowner.Controls.Add(this.dtpicknsucod, 1, 5);
            this.tblowner.Controls.Add(this.cmbCompany, 0, 1);
            this.tblowner.Controls.Add(this.cmbSubcompany, 1, 1);
            this.tblowner.Controls.Add(this.cmbpreviousSubcompany, 0, 3);
            this.tblowner.Controls.Add(this.cmbNextsubcompany, 1, 3);
            this.tblowner.Controls.Add(this.lblcompany, 0, 0);
            this.tblowner.Controls.Add(this.lblsubcompany, 1, 0);
            this.tblowner.Controls.Add(this.lblpsubcompany, 0, 2);
            this.tblowner.Controls.Add(this.lblnextsubcompany, 1, 2);
            this.tblowner.Controls.Add(this.lblpcod, 0, 4);
            this.tblowner.Controls.Add(this.lblnextcod, 1, 4);
            this.tblowner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblowner.Location = new System.Drawing.Point(0, 0);
            this.tblowner.Name = "tblowner";
            this.tblowner.RowCount = 6;
            this.tblowner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblowner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblowner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblowner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblowner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblowner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblowner.Size = new System.Drawing.Size(528, 164);
            this.tblowner.TabIndex = 0;
            this.tblowner.Paint += new System.Windows.Forms.PaintEventHandler(this.tblowner_Paint);
            // 
            // dtpickpsucod
            // 
            this.dtpickpsucod.CustomFormat = "";
            this.dtpickpsucod.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpickpsucod.Location = new System.Drawing.Point(3, 128);
            this.dtpickpsucod.Name = "dtpickpsucod";
            this.dtpickpsucod.Size = new System.Drawing.Size(205, 20);
            this.dtpickpsucod.TabIndex = 0;
            // 
            // dtpicknsucod
            // 
            this.dtpicknsucod.CustomFormat = "";
            this.dtpicknsucod.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpicknsucod.Location = new System.Drawing.Point(214, 128);
            this.dtpicknsucod.Name = "dtpicknsucod";
            this.dtpicknsucod.Size = new System.Drawing.Size(200, 20);
            this.dtpicknsucod.TabIndex = 1;
            this.dtpicknsucod.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // cmbCompany
            // 
            this.cmbCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(3, 28);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(205, 21);
            this.cmbCompany.TabIndex = 2;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // cmbSubcompany
            // 
            this.cmbSubcompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSubcompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubcompany.FormattingEnabled = true;
            this.cmbSubcompany.Location = new System.Drawing.Point(214, 28);
            this.cmbSubcompany.Name = "cmbSubcompany";
            this.cmbSubcompany.Size = new System.Drawing.Size(205, 21);
            this.cmbSubcompany.TabIndex = 3;
            // 
            // cmbpreviousSubcompany
            // 
            this.cmbpreviousSubcompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbpreviousSubcompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpreviousSubcompany.FormattingEnabled = true;
            this.cmbpreviousSubcompany.Location = new System.Drawing.Point(3, 78);
            this.cmbpreviousSubcompany.Name = "cmbpreviousSubcompany";
            this.cmbpreviousSubcompany.Size = new System.Drawing.Size(205, 21);
            this.cmbpreviousSubcompany.TabIndex = 4;
            this.cmbpreviousSubcompany.SelectedIndexChanged += new System.EventHandler(this.cmbpreviousSubcompany_SelectedIndexChanged);
            // 
            // cmbNextsubcompany
            // 
            this.cmbNextsubcompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbNextsubcompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNextsubcompany.FormattingEnabled = true;
            this.cmbNextsubcompany.Location = new System.Drawing.Point(214, 78);
            this.cmbNextsubcompany.Name = "cmbNextsubcompany";
            this.cmbNextsubcompany.Size = new System.Drawing.Size(205, 21);
            this.cmbNextsubcompany.TabIndex = 5;
            this.cmbNextsubcompany.SelectedIndexChanged += new System.EventHandler(this.cmbNextsubcompany_SelectedIndexChanged);
            // 
            // lblcompany
            // 
            this.lblcompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblcompany.AutoSize = true;
            this.lblcompany.Location = new System.Drawing.Point(3, 12);
            this.lblcompany.Name = "lblcompany";
            this.lblcompany.Size = new System.Drawing.Size(57, 13);
            this.lblcompany.TabIndex = 6;
            this.lblcompany.Text = "Company :";
            // 
            // lblsubcompany
            // 
            this.lblsubcompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblsubcompany.AutoSize = true;
            this.lblsubcompany.Location = new System.Drawing.Point(214, 12);
            this.lblsubcompany.Name = "lblsubcompany";
            this.lblsubcompany.Size = new System.Drawing.Size(79, 13);
            this.lblsubcompany.TabIndex = 7;
            this.lblsubcompany.Text = "Sub Company :";
            // 
            // lblpsubcompany
            // 
            this.lblpsubcompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblpsubcompany.AutoSize = true;
            this.lblpsubcompany.Location = new System.Drawing.Point(3, 62);
            this.lblpsubcompany.Name = "lblpsubcompany";
            this.lblpsubcompany.Size = new System.Drawing.Size(123, 13);
            this.lblpsubcompany.TabIndex = 8;
            this.lblpsubcompany.Text = "Previous Sub Company :";
            // 
            // lblnextsubcompany
            // 
            this.lblnextsubcompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblnextsubcompany.AutoSize = true;
            this.lblnextsubcompany.Location = new System.Drawing.Point(214, 62);
            this.lblnextsubcompany.Name = "lblnextsubcompany";
            this.lblnextsubcompany.Size = new System.Drawing.Size(104, 13);
            this.lblnextsubcompany.TabIndex = 9;
            this.lblnextsubcompany.Text = "Next Sub Company :";
            // 
            // lblpcod
            // 
            this.lblpcod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblpcod.AutoSize = true;
            this.lblpcod.Location = new System.Drawing.Point(3, 100);
            this.lblpcod.Name = "lblpcod";
            this.lblpcod.Size = new System.Drawing.Size(204, 25);
            this.lblpcod.TabIndex = 10;
            this.lblpcod.Text = "Previous Sub Company Changeover Date :";
            // 
            // lblnextcod
            // 
            this.lblnextcod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblnextcod.AutoSize = true;
            this.lblnextcod.Location = new System.Drawing.Point(214, 112);
            this.lblnextcod.Name = "lblnextcod";
            this.lblnextcod.Size = new System.Drawing.Size(191, 13);
            this.lblnextcod.TabIndex = 11;
            this.lblnextcod.Text = "Next Sub Company Changeover Date :";
            // 
            // ucowner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblowner);
            this.Name = "ucowner";
            this.Size = new System.Drawing.Size(528, 164);
            this.Load += new System.EventHandler(this.ucowner_Load);
            this.tblowner.ResumeLayout(false);
            this.tblowner.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblowner;
        private System.Windows.Forms.DateTimePicker dtpickpsucod;
        private System.Windows.Forms.DateTimePicker dtpicknsucod;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.ComboBox cmbSubcompany;
        private System.Windows.Forms.ComboBox cmbpreviousSubcompany;
        private System.Windows.Forms.ComboBox cmbNextsubcompany;
        private System.Windows.Forms.Label lblcompany;
        private System.Windows.Forms.Label lblsubcompany;
        private System.Windows.Forms.Label lblpsubcompany;
        private System.Windows.Forms.Label lblnextsubcompany;
        private System.Windows.Forms.Label lblpcod;
        private System.Windows.Forms.Label lblnextcod;
    }
}
