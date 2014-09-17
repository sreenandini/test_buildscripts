namespace BMC.EnterpriseClient.Views
{
    partial class frmAdminOpeningTimes
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
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lstOpeningTimes = new System.Windows.Forms.ListBox();
            this.tbl_MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.fpnl_Buttons = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.ucOpeningHour = new BMC.EnterpriseClient.Views.ucOpeningHours();
            this.tbl_MainPanel.SuspendLayout();
            this.fpnl_Buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNew.Location = new System.Drawing.Point(54, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 28);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEdit.Location = new System.Drawing.Point(160, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 28);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(478, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lstOpeningTimes
            // 
            this.lstOpeningTimes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOpeningTimes.FormattingEnabled = true;
            this.lstOpeningTimes.HorizontalScrollbar = true;
            this.lstOpeningTimes.Location = new System.Drawing.Point(3, 3);
            this.lstOpeningTimes.Name = "lstOpeningTimes";
            this.lstOpeningTimes.Size = new System.Drawing.Size(140, 610);
            this.lstOpeningTimes.Sorted = true;
            this.lstOpeningTimes.TabIndex = 1;
            this.lstOpeningTimes.SelectedIndexChanged += new System.EventHandler(this.lstOpeningTimes_SelectedIndexChanged);
            // 
            // tbl_MainPanel
            // 
            this.tbl_MainPanel.ColumnCount = 2;
            this.tbl_MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tbl_MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tbl_MainPanel.Controls.Add(this.lstOpeningTimes, 0, 0);
            this.tbl_MainPanel.Controls.Add(this.ucOpeningHour, 1, 0);
            this.tbl_MainPanel.Controls.Add(this.fpnl_Buttons, 1, 1);
            this.tbl_MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_MainPanel.Location = new System.Drawing.Point(0, 0);
            this.tbl_MainPanel.Name = "tbl_MainPanel";
            this.tbl_MainPanel.RowCount = 2;
            this.tbl_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tbl_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_MainPanel.Size = new System.Drawing.Size(733, 656);
            this.tbl_MainPanel.TabIndex = 1;
            // 
            // fpnl_Buttons
            // 
            this.fpnl_Buttons.Controls.Add(this.btnClose);
            this.fpnl_Buttons.Controls.Add(this.btn_Cancel);
            this.fpnl_Buttons.Controls.Add(this.btn_Update);
            this.fpnl_Buttons.Controls.Add(this.btnEdit);
            this.fpnl_Buttons.Controls.Add(this.btnNew);
            this.fpnl_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpnl_Buttons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.fpnl_Buttons.Location = new System.Drawing.Point(149, 619);
            this.fpnl_Buttons.Name = "fpnl_Buttons";
            this.fpnl_Buttons.Size = new System.Drawing.Size(581, 34);
            this.fpnl_Buttons.TabIndex = 3;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Cancel.Location = new System.Drawing.Point(372, 3);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(100, 28);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Visible = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Update.Location = new System.Drawing.Point(266, 3);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(100, 28);
            this.btn_Update.TabIndex = 4;
            this.btn_Update.Text = "&Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Visible = false;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // ucOpeningHour
            // 
            this.ucOpeningHour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOpeningHour.Location = new System.Drawing.Point(149, 3);
            this.ucOpeningHour.Name = "ucOpeningHour";
            this.ucOpeningHour.Size = new System.Drawing.Size(581, 610);
            this.ucOpeningHour.TabIndex = 2;
            this.ucOpeningHour.Tag = "Key_OpeningHours";
            // 
            // frmAdminOpeningTimes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(733, 656);
            this.Controls.Add(this.tbl_MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdminOpeningTimes";
            this.Text = "Standard Opening Times";
            this.Load += new System.EventHandler(this.frmAdminOpeningTimes_Load);
            this.tbl_MainPanel.ResumeLayout(false);
            this.fpnl_Buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lstOpeningTimes;
        private System.Windows.Forms.TableLayoutPanel tbl_MainPanel;
        private ucOpeningHours ucOpeningHour;
        private System.Windows.Forms.FlowLayoutPanel fpnl_Buttons;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Update;
    }
}