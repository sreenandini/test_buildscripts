namespace BMC.EnterpriseClient.Views
{
    partial class frmShare
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
            this.grpExpenseSharePercentage = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvSharePercentage = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtShareGroupName = new System.Windows.Forms.TextBox();
            this.lblExpenseShareGroupName = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Add = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.grpExpenseSharePercentage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSharePercentage)).BeginInit();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpExpenseSharePercentage
            // 
            this.grpExpenseSharePercentage.Controls.Add(this.btnCancel);
            this.grpExpenseSharePercentage.Controls.Add(this.btnSave);
            this.grpExpenseSharePercentage.Controls.Add(this.dgvSharePercentage);
            this.grpExpenseSharePercentage.Controls.Add(this.panel1);
            this.grpExpenseSharePercentage.Location = new System.Drawing.Point(6, 12);
            this.grpExpenseSharePercentage.Name = "grpExpenseSharePercentage";
            this.grpExpenseSharePercentage.Size = new System.Drawing.Size(635, 360);
            this.grpExpenseSharePercentage.TabIndex = 0;
            this.grpExpenseSharePercentage.TabStop = false;
            this.grpExpenseSharePercentage.Text = "ExpenseSharePercentage";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(548, 331);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(464, 331);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // dgvSharePercentage
            // 
            this.dgvSharePercentage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSharePercentage.Location = new System.Drawing.Point(6, 75);
            this.dgvSharePercentage.Name = "dgvSharePercentage";
            this.dgvSharePercentage.Size = new System.Drawing.Size(617, 245);
            this.dgvSharePercentage.TabIndex = 2;
            this.dgvSharePercentage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvSharePercentage_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtShareGroupName);
            this.panel1.Controls.Add(this.lblExpenseShareGroupName);
            this.panel1.Location = new System.Drawing.Point(6, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 44);
            this.panel1.TabIndex = 0;
            // 
            // txtShareGroupName
            // 
            this.txtShareGroupName.Location = new System.Drawing.Point(156, 19);
            this.txtShareGroupName.Name = "txtShareGroupName";
            this.txtShareGroupName.Size = new System.Drawing.Size(263, 20);
            this.txtShareGroupName.TabIndex = 1;
            // 
            // lblExpenseShareGroupName
            // 
            this.lblExpenseShareGroupName.AutoSize = true;
            this.lblExpenseShareGroupName.Location = new System.Drawing.Point(8, 22);
            this.lblExpenseShareGroupName.Name = "lblExpenseShareGroupName";
            this.lblExpenseShareGroupName.Size = new System.Drawing.Size(142, 13);
            this.lblExpenseShareGroupName.TabIndex = 0;
            this.lblExpenseShareGroupName.Text = "Expense Share Group Name";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add,
            this.Edit,
            this.Delete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 92);
            // 
            // Add
            // 
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(152, 22);
            this.Add.Text = "Add";
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Edit
            // 
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(152, 22);
            this.Edit.Text = "Edit";
            // 
            // Delete
            // 
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(152, 22);
            this.Delete.Text = "Delete";
            // 
            // frmShare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 377);
            this.Controls.Add(this.grpExpenseSharePercentage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShare";
            this.Text = "Share";
            this.grpExpenseSharePercentage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSharePercentage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpExpenseSharePercentage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblExpenseShareGroupName;
        private System.Windows.Forms.TextBox txtShareGroupName;
        private System.Windows.Forms.DataGridView dgvSharePercentage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Add;
        private System.Windows.Forms.ToolStripMenuItem Edit;
        private System.Windows.Forms.ToolStripMenuItem Delete;
    }
}