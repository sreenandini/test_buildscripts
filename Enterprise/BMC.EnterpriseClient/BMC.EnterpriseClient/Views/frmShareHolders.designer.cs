namespace BMC.EnterpriseClient.Views
{
    partial class ShareHolders
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
            this.grpMandatoryShareHolders = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.grdvwDefaultShareHolders = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpMandatoryShareHolders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvwDefaultShareHolders)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMandatoryShareHolders
            // 
            this.grpMandatoryShareHolders.Controls.Add(this.grdvwDefaultShareHolders);
            this.grpMandatoryShareHolders.Controls.Add(this.dataGridView1);
            this.grpMandatoryShareHolders.Location = new System.Drawing.Point(12, 28);
            this.grpMandatoryShareHolders.Name = "grpMandatoryShareHolders";
            this.grpMandatoryShareHolders.Size = new System.Drawing.Size(447, 180);
            this.grpMandatoryShareHolders.TabIndex = 0;
            this.grpMandatoryShareHolders.TabStop = false;
            this.grpMandatoryShareHolders.Text = "Mandatory  Share Holders";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // grdvwDefaultShareHolders
            // 
            this.grdvwDefaultShareHolders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdvwDefaultShareHolders.Location = new System.Drawing.Point(6, 16);
            this.grdvwDefaultShareHolders.Name = "grdvwDefaultShareHolders";
            this.grdvwDefaultShareHolders.Size = new System.Drawing.Size(435, 150);
            this.grdvwDefaultShareHolders.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(280, 235);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(384, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ShareHolders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 288);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpMandatoryShareHolders);
            this.Name = "ShareHolders";
            this.Text = "frmShareHolders";
            this.grpMandatoryShareHolders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvwDefaultShareHolders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMandatoryShareHolders;
        private System.Windows.Forms.DataGridView grdvwDefaultShareHolders;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}