namespace BMC.ExOneService.Hosting
{
    partial class ServiceMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceMainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.notifySysTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabContent = new System.Windows.Forms.TabControl();
            this.tbpLog = new System.Windows.Forms.TabPage();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.colhdrSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colhdrDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colhdrDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabContent.SuspendLayout();
            this.tbpLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.SuspendLayout();
            // 
            // notifySysTray
            // 
            this.notifySysTray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifySysTray.BalloonTipText = "BMC Exchange One Service";
            this.notifySysTray.BalloonTipTitle = "BMC Exchange One Service";
            this.notifySysTray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifySysTray.Icon")));
            this.notifySysTray.Text = "BMC Exchange One Service";
            this.notifySysTray.Visible = true;
            // 
            // tabContent
            // 
            this.tabContent.Controls.Add(this.tbpLog);
            this.tabContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContent.Location = new System.Drawing.Point(6, 6);
            this.tabContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabContent.Name = "tabContent";
            this.tabContent.SelectedIndex = 0;
            this.tabContent.Size = new System.Drawing.Size(667, 513);
            this.tabContent.TabIndex = 0;
            // 
            // tbpLog
            // 
            this.tbpLog.Controls.Add(this.dgvLog);
            this.tbpLog.Location = new System.Drawing.Point(4, 22);
            this.tbpLog.Margin = new System.Windows.Forms.Padding(2);
            this.tbpLog.Name = "tbpLog";
            this.tbpLog.Padding = new System.Windows.Forms.Padding(2);
            this.tbpLog.Size = new System.Drawing.Size(659, 487);
            this.tbpLog.TabIndex = 0;
            this.tbpLog.Text = "Log";
            this.tbpLog.UseVisualStyleBackColor = true;
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.AllowUserToOrderColumns = true;
            this.dgvLog.AllowUserToResizeRows = false;
            this.dgvLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLog.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colhdrSNo,
            this.colhdrDate,
            this.colhdrDescription});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLog.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.Location = new System.Drawing.Point(2, 2);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.Size = new System.Drawing.Size(655, 483);
            this.dgvLog.TabIndex = 0;
            // 
            // colhdrSNo
            // 
            this.colhdrSNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colhdrSNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colhdrSNo.Frozen = true;
            this.colhdrSNo.HeaderText = "S.No";
            this.colhdrSNo.Name = "colhdrSNo";
            this.colhdrSNo.ReadOnly = true;
            this.colhdrSNo.Width = 60;
            // 
            // colhdrDate
            // 
            this.colhdrDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colhdrDate.Frozen = true;
            this.colhdrDate.HeaderText = "Date/Time";
            this.colhdrDate.Name = "colhdrDate";
            this.colhdrDate.ReadOnly = true;
            this.colhdrDate.Width = 200;
            // 
            // colhdrDescription
            // 
            this.colhdrDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colhdrDescription.HeaderText = "Description";
            this.colhdrDescription.Name = "colhdrDescription";
            this.colhdrDescription.ReadOnly = true;
            this.colhdrDescription.Width = 121;
            // 
            // ServiceMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 525);
            this.Controls.Add(this.tabContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(689, 560);
            this.Name = "ServiceMainForm";
            this.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabContent.ResumeLayout(false);
            this.tbpLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifySysTray;
        private System.Windows.Forms.TabControl tabContent;
        private System.Windows.Forms.TabPage tbpLog;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn colhdrSNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colhdrDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colhdrDescription;
    }
}