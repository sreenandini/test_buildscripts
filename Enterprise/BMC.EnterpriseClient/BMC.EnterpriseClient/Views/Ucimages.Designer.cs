namespace BMC.EnterpriseClient.Views
{
    partial class Ucimages
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
            this.tblimages = new System.Windows.Forms.TableLayoutPanel();
            this.txtSitePlan = new System.Windows.Forms.TextBox();
            this.lblSiteImage = new System.Windows.Forms.Label();
            this.txtSiteImage = new System.Windows.Forms.TextBox();
            this.lblSitePlan = new System.Windows.Forms.Label();
            this.btnsiteimage = new System.Windows.Forms.Button();
            this.btnSitePlan = new System.Windows.Forms.Button();
            this.tblimages.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblimages
            // 
            this.tblimages.ColumnCount = 2;
            this.tblimages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tblimages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tblimages.Controls.Add(this.txtSitePlan, 0, 3);
            this.tblimages.Controls.Add(this.lblSiteImage, 0, 0);
            this.tblimages.Controls.Add(this.txtSiteImage, 0, 1);
            this.tblimages.Controls.Add(this.lblSitePlan, 0, 2);
            this.tblimages.Controls.Add(this.btnsiteimage, 1, 1);
            this.tblimages.Controls.Add(this.btnSitePlan, 1, 3);
            this.tblimages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblimages.Location = new System.Drawing.Point(0, 0);
            this.tblimages.Name = "tblimages";
            this.tblimages.RowCount = 4;
            this.tblimages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblimages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblimages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblimages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblimages.Size = new System.Drawing.Size(440, 129);
            this.tblimages.TabIndex = 0;
            // 
            // txtSitePlan
            // 
            this.txtSitePlan.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSitePlan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtSitePlan.Location = new System.Drawing.Point(3, 97);
            this.txtSitePlan.MaxLength = 255;
            this.txtSitePlan.Name = "txtSitePlan";
            this.txtSitePlan.ReadOnly = true;
            this.txtSitePlan.Size = new System.Drawing.Size(291, 20);
            this.txtSitePlan.TabIndex = 4;
            this.txtSitePlan.TabStop = false;
            this.txtSitePlan.TextChanged += new System.EventHandler(this.txtSitePlan_TextChanged);
            this.txtSitePlan.MouseHover += new System.EventHandler(this.textBox_MouseHover);
            // 
            // lblSiteImage
            // 
            this.lblSiteImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSiteImage.AutoSize = true;
            this.lblSiteImage.Location = new System.Drawing.Point(3, 12);
            this.lblSiteImage.Name = "lblSiteImage";
            this.lblSiteImage.Size = new System.Drawing.Size(63, 13);
            this.lblSiteImage.TabIndex = 0;
            this.lblSiteImage.Text = "Site Image :";
            // 
            // txtSiteImage
            // 
            this.txtSiteImage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSiteImage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtSiteImage.Location = new System.Drawing.Point(3, 32);
            this.txtSiteImage.MaxLength = 255;
            this.txtSiteImage.Name = "txtSiteImage";
            this.txtSiteImage.ReadOnly = true;
            this.txtSiteImage.Size = new System.Drawing.Size(291, 20);
            this.txtSiteImage.TabIndex = 1;
            this.txtSiteImage.TabStop = false;
            this.txtSiteImage.TextChanged += new System.EventHandler(this.txtSiteImage_TextChanged);
            this.txtSiteImage.MouseHover += new System.EventHandler(this.textBox_MouseHover);
            // 
            // lblSitePlan
            // 
            this.lblSitePlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSitePlan.AutoSize = true;
            this.lblSitePlan.Location = new System.Drawing.Point(3, 72);
            this.lblSitePlan.Name = "lblSitePlan";
            this.lblSitePlan.Size = new System.Drawing.Size(55, 13);
            this.lblSitePlan.TabIndex = 3;
            this.lblSitePlan.Text = "Site Plan :";
            // 
            // btnsiteimage
            // 
            this.btnsiteimage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnsiteimage.Location = new System.Drawing.Point(303, 28);
            this.btnsiteimage.Name = "btnsiteimage";
            this.btnsiteimage.Size = new System.Drawing.Size(100, 28);
            this.btnsiteimage.TabIndex = 2;
            this.btnsiteimage.Text = "Change";
            this.btnsiteimage.UseVisualStyleBackColor = true;
            this.btnsiteimage.Click += new System.EventHandler(this.btnsiteimage_Click);
            // 
            // btnSitePlan
            // 
            this.btnSitePlan.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSitePlan.Location = new System.Drawing.Point(303, 93);
            this.btnSitePlan.Name = "btnSitePlan";
            this.btnSitePlan.Size = new System.Drawing.Size(100, 28);
            this.btnSitePlan.TabIndex = 5;
            this.btnSitePlan.Text = "Change";
            this.btnSitePlan.UseVisualStyleBackColor = true;
            this.btnSitePlan.Click += new System.EventHandler(this.btnSitePlan_Click);
            // 
            // Ucimages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblimages);
            this.Name = "Ucimages";
            this.Size = new System.Drawing.Size(440, 129);
            this.Load += new System.EventHandler(this.Ucimages_Load);
            this.tblimages.ResumeLayout(false);
            this.tblimages.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblimages;
        private System.Windows.Forms.Button btnSitePlan;
        private System.Windows.Forms.Button btnsiteimage;
        private System.Windows.Forms.Label lblSitePlan;
        private System.Windows.Forms.Label lblSiteImage;
        private System.Windows.Forms.TextBox txtSiteImage;
        private System.Windows.Forms.TextBox txtSitePlan;
    }
}
