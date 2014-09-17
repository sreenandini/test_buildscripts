namespace MeterAnalysis
{
    partial class frmMeterAnalysis
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
        private void InitializeComponent(int UserID)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMeterAnalysis));
            this.ucBMCMeterAnalysis1 = new BMCMeterAnalysis.ucBMCMeterAnalysis(UserID);
            this.btnZoomGraph = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ucBMCMeterAnalysis1
            // 
            this.ucBMCMeterAnalysis1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBMCMeterAnalysis1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ucBMCMeterAnalysis1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBMCMeterAnalysis1.BackgroundImage")));
            this.ucBMCMeterAnalysis1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucBMCMeterAnalysis1.Location = new System.Drawing.Point(-9, -2);
            this.ucBMCMeterAnalysis1.Name = "ucBMCMeterAnalysis1";
            this.ucBMCMeterAnalysis1.Size = new System.Drawing.Size(1182, 772);
            this.ucBMCMeterAnalysis1.TabIndex = 0;
            // 
            // btnZoomGraph
            // 
            this.btnZoomGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomGraph.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnZoomGraph.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnZoomGraph.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnZoomGraph.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btnZoomGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomGraph.Location = new System.Drawing.Point(1055, 75);
            this.btnZoomGraph.Name = "btnZoomGraph";
            this.btnZoomGraph.Size = new System.Drawing.Size(100, 23);
            this.btnZoomGraph.TabIndex = 1;
            this.btnZoomGraph.Text = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_ZoomGraph");// "Zoom Graph";
            this.btnZoomGraph.UseVisualStyleBackColor = false;
            this.btnZoomGraph.Click += new System.EventHandler(this.btnZoomGraph_Click);
            // 
            // frmMeterAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1166, 769);
            this.Controls.Add(this.btnZoomGraph);
            this.Controls.Add(this.ucBMCMeterAnalysis1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1174, 803);
            this.Name = "frmMeterAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Meter Analysis";
            this.ResumeLayout(false);

        }  

        #endregion

        private BMCMeterAnalysis.ucBMCMeterAnalysis ucBMCMeterAnalysis1;
        private System.Windows.Forms.Button btnZoomGraph;

      

       
    }
}

