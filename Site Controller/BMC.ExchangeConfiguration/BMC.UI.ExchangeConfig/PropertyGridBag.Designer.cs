namespace BMC.UI.ExchangeConfig
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.PropertyGridContorl = new System.Windows.Forms.PropertyGrid();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.PropertyGridContorl);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 182);
            this.panel1.TabIndex = 3;
            // 
            // PropertyGridContorl
            // 
            this.PropertyGridContorl.BackColor = System.Drawing.SystemColors.Control;
            this.PropertyGridContorl.CommandsBackColor = System.Drawing.SystemColors.ButtonFace;
            this.PropertyGridContorl.HelpBackColor = System.Drawing.SystemColors.Info;
            this.PropertyGridContorl.LineColor = System.Drawing.Color.DimGray;
            this.PropertyGridContorl.Location = new System.Drawing.Point(-1, -27);
            this.PropertyGridContorl.Name = "PropertyGridContorl";
            this.PropertyGridContorl.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.PropertyGridContorl.Size = new System.Drawing.Size(459, 230);
            this.PropertyGridContorl.TabIndex = 0;
            this.PropertyGridContorl.ViewBackColor = System.Drawing.Color.White;
            // 
            // PropertyGridBag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PropertyGridBag";
            this.Size = new System.Drawing.Size(459, 182);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PropertyGrid PropertyGridContorl;
    }

}
