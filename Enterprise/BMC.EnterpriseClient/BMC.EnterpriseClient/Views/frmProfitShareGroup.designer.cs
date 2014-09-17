﻿namespace BMC.EnterpriseClient.Views
{
    partial class frmProfitShareGroup
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "1",
            "Share Group1",
            "100%",
            "This is Share Group1"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "2",
            "Share Group2",
            "100%",
            "This is Share Group2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "3",
            "Share Group3",
            "100%",
            "This is Share Group3"}, -1);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvShareGroup = new System.Windows.Forms.ListView();
            this.clmSNo = new System.Windows.Forms.ColumnHeader();
            this.clmSGName = new System.Windows.Forms.ColumnHeader();
            this.clmPercentage = new System.Windows.Forms.ColumnHeader();
            this.clmComments = new System.Windows.Forms.ColumnHeader();
            this.btnClose = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Add = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvShareGroup);
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 203);
            this.panel1.TabIndex = 0;
            // 
            // lvShareGroup
            // 
            this.lvShareGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmSNo,
            this.clmSGName,
            this.clmPercentage,
            this.clmComments});
            this.lvShareGroup.FullRowSelect = true;
            this.lvShareGroup.GridLines = true;
            this.lvShareGroup.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lvShareGroup.Location = new System.Drawing.Point(6, 5);
            this.lvShareGroup.Name = "lvShareGroup";
            this.lvShareGroup.Size = new System.Drawing.Size(514, 192);
            this.lvShareGroup.TabIndex = 0;
            this.lvShareGroup.UseCompatibleStateImageBehavior = false;
            this.lvShareGroup.View = System.Windows.Forms.View.Details;
            // 
            // clmSNo
            // 
            this.clmSNo.Text = "SNo";
            this.clmSNo.Width = 45;
            // 
            // clmSGName
            // 
            this.clmSGName.Text = "Share Group Name";
            this.clmSGName.Width = 200;
            // 
            // clmPercentage
            // 
            this.clmPercentage.Text = "Percentage";
            this.clmPercentage.Width = 100;
            // 
            // clmComments
            // 
            this.clmComments.Text = "Comments";
            this.clmComments.Width = 200;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(458, 219);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add,
            this.Edit,
            this.Delete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 70);
            // 
            // Add
            // 
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(116, 22);
            this.Add.Text = "Add";
            // 
            // Edit
            // 
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(116, 22);
            this.Edit.Text = "Edit";
            // 
            // Delete
            // 
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(116, 22);
            this.Delete.Text = "Delete";
            // 
            // frmProfitShareGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 251);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProfitShareGroup";
            this.Text = "Profit Share Group";
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListView lvShareGroup;
        private System.Windows.Forms.ColumnHeader clmSNo;
        private System.Windows.Forms.ColumnHeader clmSGName;
        private System.Windows.Forms.ColumnHeader clmPercentage;
        private System.Windows.Forms.ColumnHeader clmComments;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Add;
        private System.Windows.Forms.ToolStripMenuItem Edit;
        private System.Windows.Forms.ToolStripMenuItem Delete;
    }
}