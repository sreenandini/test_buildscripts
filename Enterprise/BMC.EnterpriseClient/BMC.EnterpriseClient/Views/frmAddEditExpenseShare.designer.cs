namespace BMC.EnterpriseClient.Views
{
    partial class frmAddEditExpenseShare
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
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbShareHolderName = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtSharePercentage = new System.Windows.Forms.NumericUpDown();
            this.lblSharePercentage = new System.Windows.Forms.Label();
            this.lblShareHolderName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSharePercentage)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(144, 87);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(240, 20);
            this.txtDescription.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(228, 130);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 29);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbShareHolderName);
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Controls.Add(this.txtSharePercentage);
            this.panel1.Controls.Add(this.lblSharePercentage);
            this.panel1.Controls.Add(this.lblShareHolderName);
            this.panel1.Location = new System.Drawing.Point(6, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 129);
            this.panel1.TabIndex = 3;
            // 
            // cmbShareHolderName
            // 
            this.cmbShareHolderName.FormattingEnabled = true;
            this.cmbShareHolderName.Location = new System.Drawing.Point(144, 15);
            this.cmbShareHolderName.Name = "cmbShareHolderName";
            this.cmbShareHolderName.Size = new System.Drawing.Size(121, 21);
            this.cmbShareHolderName.TabIndex = 5;
            //this.cmbShareHolderName.SelectedIndexChanged += new System.EventHandler(this.cmbShareHolderName_SelectedIndexChanged);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(6, 87);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(51, 13);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Comment";
            // 
            // txtSharePercentage
            // 
            this.txtSharePercentage.Location = new System.Drawing.Point(144, 51);
            this.txtSharePercentage.Name = "txtSharePercentage";
            this.txtSharePercentage.Size = new System.Drawing.Size(120, 20);
            this.txtSharePercentage.TabIndex = 2;
            // 
            // lblSharePercentage
            // 
            this.lblSharePercentage.AutoSize = true;
            this.lblSharePercentage.Location = new System.Drawing.Point(6, 53);
            this.lblSharePercentage.Name = "lblSharePercentage";
            this.lblSharePercentage.Size = new System.Drawing.Size(117, 13);
            this.lblSharePercentage.TabIndex = 1;
            this.lblSharePercentage.Text = "Profit SharePercentage";
            // 
            // lblShareHolderName
            // 
            this.lblShareHolderName.AutoSize = true;
            this.lblShareHolderName.Location = new System.Drawing.Point(6, 18);
            this.lblShareHolderName.Name = "lblShareHolderName";
            this.lblShareHolderName.Size = new System.Drawing.Size(100, 13);
            this.lblShareHolderName.TabIndex = 0;
            this.lblShareHolderName.Text = "Share Holder Name";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(309, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 29);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmAddEditExpenseShare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 166);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Name = "frmAddEditExpenseShare";
            this.Text = "Add/Edit Profit Share";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSharePercentage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.NumericUpDown txtSharePercentage;
        private System.Windows.Forms.Label lblSharePercentage;
        private System.Windows.Forms.Label lblShareHolderName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbShareHolderName;
    }
}