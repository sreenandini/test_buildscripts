namespace BMC.EnterpriseClient.Views
{
    partial class ucAssociateEmployeeGMUModes
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkAllModes = new System.Windows.Forms.CheckBox();
            this.lvGMUModes = new System.Windows.Forms.ListView();
            this.lblModeType = new System.Windows.Forms.Label();
            this.cmbModeGroup = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.chkAllModes, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lvGMUModes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblModeType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbModeGroup, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // chkAllModes
            // 
            this.chkAllModes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAllModes.AutoSize = true;
            this.chkAllModes.Location = new System.Drawing.Point(3, 6);
            this.chkAllModes.Name = "chkAllModes";
            this.chkAllModes.Size = new System.Drawing.Size(70, 17);
            this.chkAllModes.TabIndex = 5;
            this.chkAllModes.Text = "Select All";
            this.chkAllModes.UseVisualStyleBackColor = true;
            this.chkAllModes.CheckedChanged += new System.EventHandler(this.chkAllModes_CheckedChanged);
            // 
            // lvGMUModes
            // 
            this.lvGMUModes.CheckBoxes = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lvGMUModes, 3);
            this.lvGMUModes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGMUModes.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvGMUModes.Location = new System.Drawing.Point(3, 30);
            this.lvGMUModes.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lvGMUModes.Name = "lvGMUModes";
            this.lvGMUModes.Size = new System.Drawing.Size(797, 570);
            this.lvGMUModes.TabIndex = 4;
            this.lvGMUModes.UseCompatibleStateImageBehavior = false;
            this.lvGMUModes.View = System.Windows.Forms.View.Details;
            this.lvGMUModes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvGMUModes_ItemCheck);
            // 
            // lblModeType
            // 
            this.lblModeType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblModeType.AutoSize = true;
            this.lblModeType.Location = new System.Drawing.Point(553, 8);
            this.lblModeType.Name = "lblModeType";
            this.lblModeType.Size = new System.Drawing.Size(69, 13);
            this.lblModeType.TabIndex = 6;
            this.lblModeType.Text = "Mode Group:";
            // 
            // cmbModeGroup
            // 
            this.cmbModeGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbModeGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModeGroup.FormattingEnabled = true;
            this.cmbModeGroup.Location = new System.Drawing.Point(633, 4);
            this.cmbModeGroup.Name = "cmbModeGroup";
            this.cmbModeGroup.Size = new System.Drawing.Size(164, 21);
            this.cmbModeGroup.TabIndex = 7;
            this.cmbModeGroup.SelectedIndexChanged += new System.EventHandler(this.cmbModeGroup_SelectedIndexChanged);
            // 
            // ucAssociateEmployeeGMUModes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ucAssociateEmployeeGMUModes";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.ucAssociateEmployeeGMUModes_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView lvGMUModes;
        private System.Windows.Forms.CheckBox chkAllModes;
        private System.Windows.Forms.Label lblModeType;
        private System.Windows.Forms.ComboBox cmbModeGroup;
    }
}
