namespace BMC.CoreLib.Win32
{
    partial class AsyncDialogContinuousForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsyncDialogContinuousForm));
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.axAsyncProgress = new BMC.CoreLib.Win32.AxAsyncProgress();
            this.SuspendLayout();
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "ok");
            this.imglstSmallIcons.Images.SetKeyName(1, "cancel");
            // 
            // axAsyncProgress
            // 
            this.axAsyncProgress.BackColor = System.Drawing.Color.White;
            this.axAsyncProgress.CloseOnComplete = false;
            this.axAsyncProgress.ConsoleColor = null;
            this.axAsyncProgress.DialogOwner = null;
            this.axAsyncProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAsyncProgress.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.axAsyncProgress.IsActive = false;
            this.axAsyncProgress.IsCancellable = true;
            this.axAsyncProgress.IsStatusUpdatable = true;
            this.axAsyncProgress.Location = new System.Drawing.Point(2, 2);
            this.axAsyncProgress.Name = "axAsyncProgress";
            this.axAsyncProgress.Size = new System.Drawing.Size(596, 66);
            this.axAsyncProgress.TabIndex = 0;
            // 
            // AsyncDialogContinuousForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(600, 70);
            this.ControlBox = false;
            this.Controls.Add(this.axAsyncProgress);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsyncDialogContinuousForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AsyncProgressForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imglstSmallIcons;
        private AxAsyncProgress axAsyncProgress;


    }
}