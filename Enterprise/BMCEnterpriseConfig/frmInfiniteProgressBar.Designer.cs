namespace BMC
{
    partial class frmInfiniteProgressBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfiniteProgressBar));
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.pbProgressBar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgressBar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.Location = new System.Drawing.Point(12, 9);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(92, 14);
            this.lblPleaseWait.TabIndex = 129;
            this.lblPleaseWait.Text = "Please wait....";
            // 
            // pbProgressBar
            // 
            this.pbProgressBar.BackColor = System.Drawing.Color.White;
            this.pbProgressBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbProgressBar.Image = ((System.Drawing.Image)(resources.GetObject("pbProgressBar.Image")));
            this.pbProgressBar.Location = new System.Drawing.Point(15, 27);
            this.pbProgressBar.Name = "pbProgressBar";
            this.pbProgressBar.Size = new System.Drawing.Size(148, 14);
            this.pbProgressBar.TabIndex = 128;
            this.pbProgressBar.TabStop = false;
            this.pbProgressBar.WaitOnLoad = true;
            // 
            // frmInfiniteProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(178, 53);
            this.ControlBox = false;
            this.Controls.Add(this.lblPleaseWait);
            this.Controls.Add(this.pbProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmInfiniteProgressBar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.Activated += new System.EventHandler(this.frmInfiniteProgressBar_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.pbProgressBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.PictureBox pbProgressBar;
    }
}