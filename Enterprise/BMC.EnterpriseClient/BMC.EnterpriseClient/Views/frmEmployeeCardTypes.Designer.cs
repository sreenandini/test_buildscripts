namespace BMC.EnterpriseClient.Views
{
    partial class frmEmployeeCardTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployeeCardTypes));
            this.grpCardTypes = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCardType = new System.Windows.Forms.TextBox();
            this.btnCardTypes = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpCardTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCardTypes
            // 
            this.grpCardTypes.Controls.Add(this.label1);
            this.grpCardTypes.Controls.Add(this.txtCardType);
            this.grpCardTypes.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCardTypes.ForeColor = System.Drawing.Color.Black;
            this.grpCardTypes.Location = new System.Drawing.Point(7, 4);
            this.grpCardTypes.Name = "grpCardTypes";
            this.grpCardTypes.Size = new System.Drawing.Size(290, 76);
            this.grpCardTypes.TabIndex = 0;
            this.grpCardTypes.TabStop = false;
            this.grpCardTypes.Text = "Card Types";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Card Type";
            // 
            // txtCardType
            // 
            this.txtCardType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardType.Location = new System.Drawing.Point(108, 34);
            this.txtCardType.Name = "txtCardType";
            this.txtCardType.Size = new System.Drawing.Size(176, 21);
            this.txtCardType.TabIndex = 1;
            // 
            // btnCardTypes
            // 
            this.btnCardTypes.Location = new System.Drawing.Point(157, 86);
            this.btnCardTypes.Name = "btnCardTypes";
            this.btnCardTypes.Size = new System.Drawing.Size(68, 30);
            this.btnCardTypes.TabIndex = 0;
            this.btnCardTypes.Text = "Save";
            this.btnCardTypes.UseVisualStyleBackColor = true;
            this.btnCardTypes.Click += new System.EventHandler(this.btnCardTypes_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(230, 86);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmEmployeeCardTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 119);
            this.Controls.Add(this.btnCardTypes);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpCardTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmployeeCardTypes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmEmployeeCardTypes";
            this.Load += new System.EventHandler(this.frmEmployeeCardTypes_Load);
            this.grpCardTypes.ResumeLayout(false);
            this.grpCardTypes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCardTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCardType;
        private System.Windows.Forms.Button btnCardTypes;
        private System.Windows.Forms.Button btnClose;
    }
}