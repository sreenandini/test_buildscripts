namespace MeterAnalysis
{
    partial class frmMeterAnalysisGraph
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
        private void InitializeComponent(int userID)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeterAnalysis.frmMeterAnalysis));
            this.ucMeterAnalysisGraph1 = new BMCMeterAnalysis.ucMeterAnalysisGraph(userID);
            this.btnExit = new System.Windows.Forms.Button();
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler (this.frmMeterAnalysisGraph_FormClosing);

            this.SuspendLayout();
            // 
            // ucMeterAnalysisGraph1
            // 
            this.ucMeterAnalysisGraph1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ucMeterAnalysisGraph1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top)));
            this.ucMeterAnalysisGraph1.Location = new System.Drawing.Point(0, 0);
            this.ucMeterAnalysisGraph1.Name = "ucMeterAnalysisGraph1";
            this.ucMeterAnalysisGraph1.Size = new System.Drawing.Size(1160, 773);
            this.ucMeterAnalysisGraph1.TabIndex = 0;
            
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(1065, 724);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.TabIndex = 1;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExit.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmMeterAnalysisGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White; //FromArgb(60, 120, 190); // FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));

            this.ClientSize = new System.Drawing.Size(1152, 759);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.ucMeterAnalysisGraph1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMeterAnalysisGraph";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Meter Analysis - Graph";
            this.ResumeLayout(false);

        }

        #endregion

        private BMCMeterAnalysis.ucMeterAnalysisGraph ucMeterAnalysisGraph1;
        private System.Windows.Forms.Button btnExit;
    }
}