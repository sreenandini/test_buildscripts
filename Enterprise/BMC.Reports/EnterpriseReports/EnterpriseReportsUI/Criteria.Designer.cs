namespace BMC.EnterpriseReportsUI
{
    partial class Criteria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Criteria));
            this.lblMain = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMain
            // 
            resources.ApplyResources(this.lblMain, "lblMain");
            this.lblMain.Name = "lblMain";
            // 
            // Criteria
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Criteria";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmCriteria_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Criteria_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMain;

    }
}