namespace BMC.CoreLib.Win32
{
    partial class BMCDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BMCDialogForm));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlInnerHeader = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.pnlInnerContainer = new System.Windows.Forms.Panel();
            this.pnlContainer.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlInnerHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.pnlInnerContainer);
            this.pnlContainer.Controls.Add(this.pnlHeader);
            this.pnlContainer.Size = new System.Drawing.Size(594, 327);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 327);
            this.pnlButtons.Size = new System.Drawing.Size(594, 41);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.pnlInnerHeader);
            this.pnlHeader.Controls.Add(this.picLogo);
            this.pnlHeader.Controls.Add(this.pnlLine);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(594, 55);
            this.pnlHeader.TabIndex = 0;
            // 
            // pnlInnerHeader
            // 
            this.pnlInnerHeader.Controls.Add(this.lblDescription);
            this.pnlInnerHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInnerHeader.Location = new System.Drawing.Point(152, 0);
            this.pnlInnerHeader.Name = "pnlInnerHeader";
            this.pnlInnerHeader.Size = new System.Drawing.Size(442, 54);
            this.pnlInnerHeader.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(0, 0);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(3);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Padding = new System.Windows.Forms.Padding(3);
            this.lblDescription.Size = new System.Drawing.Size(442, 54);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picLogo
            // 
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.picLogo.Image = global::BMC.CoreLib.Properties.Resources.MultiConnect_Logo;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(152, 54);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 3;
            this.picLogo.TabStop = false;
            // 
            // pnlLine
            // 
            this.pnlLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLine.Location = new System.Drawing.Point(0, 54);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(594, 1);
            this.pnlLine.TabIndex = 0;
            // 
            // pnlInnerContainer
            // 
            this.pnlInnerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInnerContainer.Location = new System.Drawing.Point(0, 55);
            this.pnlInnerContainer.Name = "pnlInnerContainer";
            this.pnlInnerContainer.Size = new System.Drawing.Size(594, 272);
            this.pnlInnerContainer.TabIndex = 1;
            // 
            // BMCDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(594, 368);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BMCDialogForm";
            this.pnlContainer.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlInnerHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        public System.Windows.Forms.Panel pnlInnerContainer;
        private System.Windows.Forms.Panel pnlInnerHeader;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Label lblDescription;
    }
}
