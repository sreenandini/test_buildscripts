namespace BMC.CentralisedSiteSettings.Presentation
{
    partial class PropertyGridBag
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
            this.PropertyGridContorl = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // PropertyGridContorl
            // 
            this.PropertyGridContorl.BackColor = System.Drawing.SystemColors.Control;
            this.PropertyGridContorl.CommandsBackColor = System.Drawing.SystemColors.ButtonFace;
            this.PropertyGridContorl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyGridContorl.HelpBackColor = System.Drawing.SystemColors.Info;
            this.PropertyGridContorl.LineColor = System.Drawing.Color.DimGray;
            this.PropertyGridContorl.Location = new System.Drawing.Point(0, 0);
            this.PropertyGridContorl.Name = "PropertyGridContorl";
            this.PropertyGridContorl.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.PropertyGridContorl.Size = new System.Drawing.Size(535, 473);
            this.PropertyGridContorl.TabIndex = 4;
            this.PropertyGridContorl.ToolbarVisible = false;
            this.PropertyGridContorl.ViewBackColor = System.Drawing.Color.White;
            // 
            // PropertyGridBag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PropertyGridContorl);
            this.Name = "PropertyGridBag";
            this.Size = new System.Drawing.Size(535, 473);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PropertyGrid PropertyGridContorl;

    }

}
