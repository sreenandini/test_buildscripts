namespace BMC.EnterpriseClient.Views.ServiceCalls
{
    partial class UcSASource
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
            this.lvFaultGroups = new System.Windows.Forms.ListView();
            this.chdrDescription = new System.Windows.Forms.ColumnHeader();
            this.chdrReference = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFaultGroupReference = new System.Windows.Forms.TextBox();
            this.txtFaultGroupDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.fraFaultDetails = new System.Windows.Forms.GroupBox();
            this.btnApply1 = new System.Windows.Forms.Button();
            this.tblButtons1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew1 = new System.Windows.Forms.Button();
            this.lblFixCodes = new System.Windows.Forms.Label();
            this.tblContent1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3.SuspendLayout();
            this.fraFaultDetails.SuspendLayout();
            this.tblButtons1.SuspendLayout();
            this.tblContent1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvFaultGroups
            // 
            this.lvFaultGroups.CheckBoxes = true;
            this.lvFaultGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chdrDescription,
            this.chdrReference});
            this.lvFaultGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFaultGroups.FullRowSelect = true;
            this.lvFaultGroups.HideSelection = false;
            this.lvFaultGroups.Location = new System.Drawing.Point(3, 28);
            this.lvFaultGroups.Name = "lvFaultGroups";
            this.lvFaultGroups.Size = new System.Drawing.Size(490, 354);
            this.lvFaultGroups.TabIndex = 1;
            this.lvFaultGroups.UseCompatibleStateImageBehavior = false;
            this.lvFaultGroups.View = System.Windows.Forms.View.Details;
            // 
            // chdrDescription
            // 
            this.chdrDescription.DisplayIndex = 1;
            this.chdrDescription.Text = "Description";
            // 
            // chdrReference
            // 
            this.chdrReference.DisplayIndex = 0;
            this.chdrReference.Text = "Reference";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.28852F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.71148F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtFaultGroupReference, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtFaultGroupDescription, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblDescription, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(484, 89);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Reference :";
            // 
            // txtFaultGroupReference
            // 
            this.txtFaultGroupReference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFaultGroupReference.Location = new System.Drawing.Point(106, 5);
            this.txtFaultGroupReference.Name = "txtFaultGroupReference";
            this.txtFaultGroupReference.Size = new System.Drawing.Size(375, 20);
            this.txtFaultGroupReference.TabIndex = 1;
            // 
            // txtFaultGroupDescription
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.txtFaultGroupDescription, 2);
            this.txtFaultGroupDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFaultGroupDescription.Location = new System.Drawing.Point(3, 63);
            this.txtFaultGroupDescription.Name = "txtFaultGroupDescription";
            this.txtFaultGroupDescription.Size = new System.Drawing.Size(478, 20);
            this.txtFaultGroupDescription.TabIndex = 3;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 38);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(66, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description :";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fraFaultDetails
            // 
            this.fraFaultDetails.Controls.Add(this.tableLayoutPanel3);
            this.fraFaultDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraFaultDetails.ForeColor = System.Drawing.Color.Black;
            this.fraFaultDetails.Location = new System.Drawing.Point(3, 388);
            this.fraFaultDetails.Name = "fraFaultDetails";
            this.fraFaultDetails.Size = new System.Drawing.Size(490, 108);
            this.fraFaultDetails.TabIndex = 2;
            this.fraFaultDetails.TabStop = false;
            this.fraFaultDetails.Text = "Fault Details";
            // 
            // btnApply1
            // 
            this.btnApply1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply1.Location = new System.Drawing.Point(387, 5);
            this.btnApply1.Name = "btnApply1";
            this.btnApply1.Size = new System.Drawing.Size(100, 28);
            this.btnApply1.TabIndex = 1;
            this.btnApply1.Text = "Apply";
            this.btnApply1.UseVisualStyleBackColor = true;
            // 
            // tblButtons1
            // 
            this.tblButtons1.ColumnCount = 4;
            this.tblButtons1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblButtons1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblButtons1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblButtons1.Controls.Add(this.btnApply1, 3, 0);
            this.tblButtons1.Controls.Add(this.btnNew1, 2, 0);
            this.tblButtons1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtons1.Location = new System.Drawing.Point(3, 502);
            this.tblButtons1.Name = "tblButtons1";
            this.tblButtons1.RowCount = 1;
            this.tblButtons1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons1.Size = new System.Drawing.Size(490, 39);
            this.tblButtons1.TabIndex = 3;
            // 
            // btnNew1
            // 
            this.btnNew1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew1.Location = new System.Drawing.Point(281, 5);
            this.btnNew1.Name = "btnNew1";
            this.btnNew1.Size = new System.Drawing.Size(100, 28);
            this.btnNew1.TabIndex = 0;
            this.btnNew1.Text = "New";
            this.btnNew1.UseVisualStyleBackColor = true;
            // 
            // lblFixCodes
            // 
            this.lblFixCodes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFixCodes.AutoSize = true;
            this.lblFixCodes.Location = new System.Drawing.Point(3, 6);
            this.lblFixCodes.Name = "lblFixCodes";
            this.lblFixCodes.Size = new System.Drawing.Size(53, 13);
            this.lblFixCodes.TabIndex = 0;
            this.lblFixCodes.Text = "Fix Codes";
            // 
            // tblContent1
            // 
            this.tblContent1.ColumnCount = 1;
            this.tblContent1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent1.Controls.Add(this.lblFixCodes, 0, 0);
            this.tblContent1.Controls.Add(this.lvFaultGroups, 0, 1);
            this.tblContent1.Controls.Add(this.fraFaultDetails, 0, 2);
            this.tblContent1.Controls.Add(this.tblButtons1, 0, 3);
            this.tblContent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent1.Location = new System.Drawing.Point(0, 0);
            this.tblContent1.Name = "tblContent1";
            this.tblContent1.RowCount = 4;
            this.tblContent1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContent1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblContent1.Size = new System.Drawing.Size(496, 544);
            this.tblContent1.TabIndex = 0;
            // 
            // UcSASource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblContent1);
            this.Name = "UcSASource";
            this.Size = new System.Drawing.Size(496, 544);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.fraFaultDetails.ResumeLayout(false);
            this.tblButtons1.ResumeLayout(false);
            this.tblContent1.ResumeLayout(false);
            this.tblContent1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFaultGroups;
        private System.Windows.Forms.ColumnHeader chdrDescription;
        private System.Windows.Forms.ColumnHeader chdrReference;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFaultGroupReference;
        private System.Windows.Forms.TextBox txtFaultGroupDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.GroupBox fraFaultDetails;
        private System.Windows.Forms.Button btnApply1;
        private System.Windows.Forms.TableLayoutPanel tblButtons1;
        private System.Windows.Forms.Button btnNew1;
        private System.Windows.Forms.Label lblFixCodes;
        private System.Windows.Forms.TableLayoutPanel tblContent1;
    }
}
