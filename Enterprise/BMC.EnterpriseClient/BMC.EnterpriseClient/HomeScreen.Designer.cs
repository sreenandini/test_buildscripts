namespace BMC.EnterpriseClient
{
    partial class HomeScreen
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
            this.widgets = new BMC.CoreLib.Win32.HomeScreenWidgets();
            this.SuspendLayout();
            // 
            // widgets
            // 
            this.widgets.BackColor = System.Drawing.Color.Transparent;
            this.widgets.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(160)))), ((int)(((byte)(166)))));
            this.widgets.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(161)))), ((int)(((byte)(167)))));
            this.widgets.Directory = null;
            this.widgets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.widgets.Location = new System.Drawing.Point(0, 0);
            this.widgets.Name = "widgets";
            this.widgets.Size = new System.Drawing.Size(743, 607);
            this.widgets.TabIndex = 0;
            this.widgets.Text = "homeScreenWidgets1";
            this.widgets.WidgetHeight = 155;
            this.widgets.WidgetWidth = 230;
            // 
            // HomeScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::BMC.EnterpriseClient.Properties.Resources.BMC_Enterprise_Transparent2;
            this.ClientSize = new System.Drawing.Size(743, 607);
            this.ControlBox = false;
            this.Controls.Add(this.widgets);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HomeScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.HomeScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BMC.CoreLib.Win32.HomeScreenWidgets widgets;
    }
}