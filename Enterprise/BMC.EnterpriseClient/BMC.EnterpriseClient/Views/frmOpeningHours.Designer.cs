namespace BMC.EnterpriseClient.Views
{
    partial class frmOpeningHours
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
            this.ucOpeningHour = new BMC.EnterpriseClient.Views.ucOpeningHours();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.ucOpeningHour);
            this.pnlContainer.Size = new System.Drawing.Size(979, 579);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 579);
            this.pnlButtons.Size = new System.Drawing.Size(979, 41);
            // 
            // ucOpeningHour
            // 
            this.ucOpeningHour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOpeningHour.Location = new System.Drawing.Point(0, 0);
            this.ucOpeningHour.Name = "ucOpeningHour";
            this.ucOpeningHour.Size = new System.Drawing.Size(979, 579);
            this.ucOpeningHour.TabIndex = 0;
            this.ucOpeningHour.Tag = "Key_OpeningHours";
            // 
            // frmOpeningHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 620);
            this.Name = "frmOpeningHours";
            this.Text = "Opening Hours";
            this.Load += new System.EventHandler(this.frmOpeningHours_Load);
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ucOpeningHours ucOpeningHour;

    }
}