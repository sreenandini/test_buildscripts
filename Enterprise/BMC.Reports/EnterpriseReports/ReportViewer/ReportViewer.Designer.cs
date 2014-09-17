namespace BMC.ReportViewer
{
    partial class ReportViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false. </param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportViewer));
            this.txtErrorMsg = new System.Windows.Forms.TextBox();
            this.rdlViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // txtErrorMsg
            // 
            this.txtErrorMsg.AcceptsReturn = true;
            this.txtErrorMsg.BackColor = System.Drawing.Color.White;
            this.txtErrorMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtErrorMsg.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.txtErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.txtErrorMsg.Location = new System.Drawing.Point(50, 96);
            this.txtErrorMsg.Multiline = true;
            this.txtErrorMsg.Name = "txtErrorMsg";
            this.txtErrorMsg.ReadOnly = true;
            this.txtErrorMsg.Size = new System.Drawing.Size(337, 141);
            this.txtErrorMsg.TabIndex = 2;
            this.txtErrorMsg.Visible = false;
            // 
            // rdlViewer
            // 
            this.rdlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdlViewer.Location = new System.Drawing.Point(0, 0);
            this.rdlViewer.Name = "rdlViewer";
            this.rdlViewer.Size = new System.Drawing.Size(400, 265);
            this.rdlViewer.TabIndex = 3;
            // 
            // ReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 265);
            this.Controls.Add(this.rdlViewer);
            this.Controls.Add(this.txtErrorMsg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReportViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReportViewer_FormClosing);
            this.Load += new System.EventHandler(this.frmReportViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TextBox txtErrorMsg;
        private Microsoft.Reporting.WinForms.ReportViewer rdlViewer;
    }
}