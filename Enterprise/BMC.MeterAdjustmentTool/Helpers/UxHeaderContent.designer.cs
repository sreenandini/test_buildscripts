using System.Drawing;
namespace BMC.MeterAdjustmentTool.Helpers
{
    partial class UxHeaderContent
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UxHeaderContent));
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.btnPin = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.pnlContent = new System.Windows.Forms.Panel();
            this.uxHeader = new BMC.MeterAdjustmentTool.Helpers.GradientHeader();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnPin);
            this.pnlContainer.Controls.Add(this.pnlContent);
            this.pnlContainer.Controls.Add(this.uxHeader);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(705, 392);
            this.pnlContainer.TabIndex = 0;
            // 
            // btnPin
            // 
            this.btnPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            this.btnPin.CausesValidation = false;
            this.btnPin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(185)))), ((int)(((byte)(225)))));
            this.btnPin.FlatAppearance.BorderSize = 0;
            this.btnPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPin.ImageKey = "PinWhite.ico";
            this.btnPin.ImageList = this.imglstSmallIcons;
            this.btnPin.Location = new System.Drawing.Point(678, 0);
            this.btnPin.Name = "btnPin";
            this.btnPin.Size = new System.Drawing.Size(27, 23);
            this.btnPin.TabIndex = 1;
            this.btnPin.UseVisualStyleBackColor = false;
            this.btnPin.Visible = false;
            this.btnPin.Click += new System.EventHandler(this.btnPin_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "PinWhite.ico");
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 26);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(3);
            this.pnlContent.Size = new System.Drawing.Size(705, 366);
            this.pnlContent.TabIndex = 2;
            // 
            // uxHeader
            // 
            this.uxHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxHeader.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            this.uxHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxHeader.ForeColor = System.Drawing.Color.White;
            this.uxHeader.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uxHeader.Location = new System.Drawing.Point(0, 0);
            this.uxHeader.Name = "uxHeader";
            this.uxHeader.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.uxHeader.Size = new System.Drawing.Size(705, 26);
            this.uxHeader.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.uxHeader.BackColor = SystemColors.Control;
            this.uxHeader.TabIndex = 0;
            this.uxHeader.Text = "[HeaderText]";
            // 
            // UxHeaderContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContainer);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UxHeaderContent";
            this.Size = new System.Drawing.Size(705, 392);
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private GradientHeader uxHeader;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnPin;
        private System.Windows.Forms.ImageList imglstSmallIcons;
    }
}
