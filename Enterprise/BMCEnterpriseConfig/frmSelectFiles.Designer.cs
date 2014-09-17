namespace BMC
{
    partial class frmSelectFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectFiles));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_BrowseLogFilePath = new System.Windows.Forms.Button();
            this.btn_BrowseDataFilePath = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.txtDataFilePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_BrowseLogFilePath
            // 
            this.btn_BrowseLogFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BrowseLogFilePath.Location = new System.Drawing.Point(492, 48);
            this.btn_BrowseLogFilePath.Name = "btn_BrowseLogFilePath";
            this.btn_BrowseLogFilePath.Size = new System.Drawing.Size(73, 23);
            this.btn_BrowseLogFilePath.TabIndex = 3;
            this.btn_BrowseLogFilePath.Text = "Browse";
            this.btn_BrowseLogFilePath.UseVisualStyleBackColor = true;
            this.btn_BrowseLogFilePath.Click += new System.EventHandler(this.btn_BrowseLogFilePath_Click);
            // 
            // btn_BrowseDataFilePath
            // 
            this.btn_BrowseDataFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BrowseDataFilePath.Location = new System.Drawing.Point(492, 14);
            this.btn_BrowseDataFilePath.Name = "btn_BrowseDataFilePath";
            this.btn_BrowseDataFilePath.Size = new System.Drawing.Size(73, 23);
            this.btn_BrowseDataFilePath.TabIndex = 1;
            this.btn_BrowseDataFilePath.Text = "Browse";
            this.btn_BrowseDataFilePath.UseVisualStyleBackColor = true;
            this.btn_BrowseDataFilePath.Click += new System.EventHandler(this.btn_BrowseDataFilePath_Click);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Ok.Location = new System.Drawing.Point(212, 82);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(73, 23);
            this.btn_Ok.TabIndex = 4;
            this.btn_Ok.Text = "OK";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Database Data File Path:";
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.Location = new System.Drawing.Point(158, 50);
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.Size = new System.Drawing.Size(328, 20);
            this.txtLogFilePath.TabIndex = 2;
            // 
            // txtDataFilePath
            // 
            this.txtDataFilePath.Location = new System.Drawing.Point(158, 16);
            this.txtDataFilePath.Name = "txtDataFilePath";
            this.txtDataFilePath.Size = new System.Drawing.Size(328, 20);
            this.txtDataFilePath.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(5, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Database Log File Path:";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(291, 82);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(73, 23);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // frmSelectFiles
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(572, 119);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_BrowseLogFilePath);
            this.Controls.Add(this.btn_BrowseDataFilePath);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLogFilePath);
            this.Controls.Add(this.txtDataFilePath);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectFiles";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database File Path Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btn_BrowseLogFilePath;
        private System.Windows.Forms.Button btn_BrowseDataFilePath;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLogFilePath;
        private System.Windows.Forms.TextBox txtDataFilePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Cancel;
    }
}

