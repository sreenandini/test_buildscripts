namespace BMC_GoLiveApplication_Check
{
    partial class frmGoLive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGoLive));
            this.lvGoLive = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGMUStatus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lvGoLive
            // 
            this.lvGoLive.BackColor = System.Drawing.Color.White;
            this.lvGoLive.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvGoLive.ForeColor = System.Drawing.Color.Black;
            this.lvGoLive.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvGoLive.Location = new System.Drawing.Point(12, 107);
            this.lvGoLive.Name = "lvGoLive";
            this.lvGoLive.Size = new System.Drawing.Size(876, 346);
            this.lvGoLive.TabIndex = 0;
            this.lvGoLive.UseCompatibleStateImageBehavior = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BMC_GoLiveApplication_Check.Properties.Resources.MultiConnect_Logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(251, 96);
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Bisque;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Crimson;
            this.btnRefresh.Location = new System.Drawing.Point(739, 459);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(149, 33);
            this.btnRefresh.TabIndex = 31;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(400, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(488, 52);
            this.label1.TabIndex = 32;
            this.label1.Text = "BMC GoLive Verification";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGMUStatus
            // 
            this.btnGMUStatus.BackColor = System.Drawing.Color.Bisque;
            this.btnGMUStatus.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnGMUStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnGMUStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnGMUStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGMUStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGMUStatus.ForeColor = System.Drawing.Color.Crimson;
            this.btnGMUStatus.Location = new System.Drawing.Point(538, 459);
            this.btnGMUStatus.Name = "btnGMUStatus";
            this.btnGMUStatus.Size = new System.Drawing.Size(149, 33);
            this.btnGMUStatus.TabIndex = 33;
            this.btnGMUStatus.Text = "GMU Status";
            this.btnGMUStatus.UseVisualStyleBackColor = false;
            this.btnGMUStatus.Click += new System.EventHandler(this.btnGMUStatus_Click);
            // 
            // frmGoLive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.btnGMUStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lvGoLive);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(908, 534);
            this.MinimumSize = new System.Drawing.Size(908, 534);
            this.Name = "frmGoLive";
            this.Text = " BMC Go Live Verification";
            this.Load += new System.EventHandler(this.frmGoLive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvGoLive;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGMUStatus;



    }
}