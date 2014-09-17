namespace BMC.EnterpriseReportsUI
{
    partial class ExceptionReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionReport));
            this.pnlMessages = new System.Windows.Forms.Panel();
            this.lblMessage5 = new System.Windows.Forms.Label();
            this.lblMessage4 = new System.Windows.Forms.Label();
            this.lblMessage3 = new System.Windows.Forms.Label();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.lblMessage1 = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.pnlMessages.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMessages
            // 
            this.pnlMessages.Controls.Add(this.lblMessage5);
            this.pnlMessages.Controls.Add(this.lblMessage4);
            this.pnlMessages.Controls.Add(this.lblMessage3);
            this.pnlMessages.Controls.Add(this.lblMessage2);
            this.pnlMessages.Controls.Add(this.lblMessage1);
            this.pnlMessages.Location = new System.Drawing.Point(9, 12);
            this.pnlMessages.Name = "pnlMessages";
            this.pnlMessages.Size = new System.Drawing.Size(487, 138);
            this.pnlMessages.TabIndex = 0;
            this.pnlMessages.Visible = false;
            // 
            // lblMessage5
            // 
            this.lblMessage5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblMessage5.ForeColor = System.Drawing.Color.Black;
            this.lblMessage5.Location = new System.Drawing.Point(47, 103);
            this.lblMessage5.Name = "lblMessage5";
            this.lblMessage5.Size = new System.Drawing.Size(349, 21);
            this.lblMessage5.TabIndex = 4;
            this.lblMessage5.Text = "Cancel                  -   Cancel report. ";
            // 
            // lblMessage4
            // 
            this.lblMessage4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblMessage4.ForeColor = System.Drawing.Color.Black;
            this.lblMessage4.Location = new System.Drawing.Point(47, 82);
            this.lblMessage4.Name = "lblMessage4";
            this.lblMessage4.Size = new System.Drawing.Size(349, 21);
            this.lblMessage4.TabIndex = 3;
            this.lblMessage4.Text = "View Report          -   Displays the report.";
            // 
            // lblMessage3
            // 
            this.lblMessage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblMessage3.ForeColor = System.Drawing.Color.Black;
            this.lblMessage3.Location = new System.Drawing.Point(47, 61);
            this.lblMessage3.Name = "lblMessage3";
            this.lblMessage3.Size = new System.Drawing.Size(349, 21);
            this.lblMessage3.TabIndex = 2;
            this.lblMessage3.Text = "View Exceptions   -   Displays exception report.";
            // 
            // lblMessage2
            // 
            this.lblMessage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblMessage2.ForeColor = System.Drawing.Color.Black;
            this.lblMessage2.Location = new System.Drawing.Point(31, 35);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(430, 21);
            this.lblMessage2.TabIndex = 1;
            this.lblMessage2.Text = "Please select from the following options:";
            // 
            // lblMessage1
            // 
            this.lblMessage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblMessage1.ForeColor = System.Drawing.Color.Black;
            this.lblMessage1.Location = new System.Drawing.Point(31, 12);
            this.lblMessage1.Name = "lblMessage1";
            this.lblMessage1.Size = new System.Drawing.Size(430, 21);
            this.lblMessage1.TabIndex = 0;
            this.lblMessage1.Text = "There appears to be missing Daily data for the period selected.";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnOpen);
            this.pnlButtons.Controls.Add(this.btnContinue);
            this.pnlButtons.Location = new System.Drawing.Point(9, 162);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(477, 49);
            this.pnlButtons.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::BMC.EnterpriseReportsUI.Properties.Resources.ico00013;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnCancel.Location = new System.Drawing.Point(313, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(143, 38);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "       Cancel ";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.AutoSize = true;
            this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Image = global::BMC.EnterpriseReportsUI.Properties.Resources.ico00012;
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnOpen.Location = new System.Drawing.Point(15, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(143, 38);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "      View Exceptions";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.AutoSize = true;
            this.btnContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.Image = global::BMC.EnterpriseReportsUI.Properties.Resources.retry;
            this.btnContinue.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnContinue.Location = new System.Drawing.Point(164, 3);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(143, 38);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "    View Report";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // ExceptionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 217);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlMessages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ExceptionReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exception Report";
            this.Load += new System.EventHandler(this.frmExceptionReport_Load);
            this.pnlMessages.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMessages;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblMessage1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMessage5;
        private System.Windows.Forms.Label lblMessage4;
        private System.Windows.Forms.Label lblMessage3;
        private System.Windows.Forms.Label lblMessage2;
    }
}