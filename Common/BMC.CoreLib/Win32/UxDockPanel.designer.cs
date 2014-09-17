namespace BMC.CoreLib.Win32
{
    partial class UxDockPanel
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
            this.pnlHide = new System.Windows.Forms.Panel();
            this.tabHide = new System.Windows.Forms.TabControl();
            this.tbpHide = new System.Windows.Forms.TabPage();
            this.axContent = new BMC.CoreLib.Win32.UxHeaderContent();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlHide.SuspendLayout();
            this.tabHide.SuspendLayout();
            this.axContent.ChildContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHide
            // 
            this.pnlHide.Controls.Add(this.tabHide);
            this.pnlHide.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHide.Location = new System.Drawing.Point(0, 0);
            this.pnlHide.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHide.Name = "pnlHide";
            this.pnlHide.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlHide.Size = new System.Drawing.Size(29, 507);
            this.pnlHide.TabIndex = 0;
            // 
            // tabHide
            // 
            this.tabHide.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabHide.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabHide.Controls.Add(this.tbpHide);
            this.tabHide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabHide.Location = new System.Drawing.Point(3, 3);
            this.tabHide.Multiline = true;
            this.tabHide.Name = "tabHide";
            this.tabHide.SelectedIndex = 0;
            this.tabHide.Size = new System.Drawing.Size(26, 501);
            this.tabHide.TabIndex = 0;
            this.tabHide.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabHide_MouseClick);
            // 
            // tbpHide
            // 
            this.tbpHide.Location = new System.Drawing.Point(83, 4);
            this.tbpHide.Name = "tbpHide";
            this.tbpHide.Padding = new System.Windows.Forms.Padding(3);
            this.tbpHide.Size = new System.Drawing.Size(0, 493);
            this.tbpHide.TabIndex = 1;
            this.tbpHide.Text = "AxDockPanel";
            this.tbpHide.UseVisualStyleBackColor = true;
            // 
            // axContent
            // 
            // 
            // axContent.ChildContainer
            // 
            this.axContent.ChildContainer.Controls.Add(this.pnlContent);
            this.axContent.ChildContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axContent.ChildContainer.Location = new System.Drawing.Point(0, 23);
            this.axContent.ChildContainer.Name = "ChildContainer";
            this.axContent.ChildContainer.Padding = new System.Windows.Forms.Padding(3);
            this.axContent.ChildContainer.Size = new System.Drawing.Size(537, 484);
            this.axContent.ChildContainer.TabIndex = 2;
            this.axContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axContent.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.axContent.HeaderText = "AxDockPanel";
            this.axContent.Location = new System.Drawing.Point(29, 0);
            this.axContent.Name = "axContent";
            this.axContent.PinVisible = true;
            this.axContent.Size = new System.Drawing.Size(537, 507);
            this.axContent.TabIndex = 1;
            this.axContent.PinClick += new System.EventHandler(this.axContent_PinClick);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(3, 3);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(3);
            this.pnlContent.Size = new System.Drawing.Size(531, 478);
            this.pnlContent.TabIndex = 0;
            // 
            // AxDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axContent);
            this.Controls.Add(this.pnlHide);
            this.Name = "AxDockPanel";
            this.Size = new System.Drawing.Size(566, 507);
            this.pnlHide.ResumeLayout(false);
            this.tabHide.ResumeLayout(false);
            this.axContent.ChildContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHide;
        private System.Windows.Forms.TabControl tabHide;
        private System.Windows.Forms.TabPage tbpHide;
        private UxHeaderContent axContent;
        public System.Windows.Forms.Panel pnlContent;
    }
}
