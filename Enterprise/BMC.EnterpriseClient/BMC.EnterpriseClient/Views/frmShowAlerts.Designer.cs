namespace BMC.EnterpriseClient.Views
{
    partial class frmShowAlerts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowAlerts));
            this.ucAlerts1 = new BMC.EnterpriseClient.Views.ucAlerts();
            this.SuspendLayout();
            // 
            // ucAlerts1
            // 
            this.ucAlerts1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAlerts1.Location = new System.Drawing.Point(0, 0);
            this.ucAlerts1.Name = "ucAlerts1";
            this.ucAlerts1.Size = new System.Drawing.Size(964, 755);
            this.ucAlerts1.TabIndex = 0;
            this.ucAlerts1.Tag = "KEY_ALERT";
            // 
            // frmShowAlerts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 755);
            this.Controls.Add(this.ucAlerts1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmShowAlerts";
            this.Text = "Alerts";
            this.ResumeLayout(false);

        }

        #endregion

        private ucAlerts ucAlerts1;

    }
}