namespace BMC.EnterpriseClient.Views
{
    partial class frmStackerAddEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStackerAddEdit));
            this.grpStackerType = new System.Windows.Forms.GroupBox();
            this.txtStackerSize = new System.Windows.Forms.NumericUpDown();
            this.txtStackerName = new System.Windows.Forms.TextBox();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.rdbInActive = new System.Windows.Forms.RadioButton();
            this.rdbActive = new System.Windows.Forms.RadioButton();
            this.txtStackerDescription = new System.Windows.Forms.RichTextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpStackerType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStackerSize)).BeginInit();
            this.grpStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpStackerType
            // 
            this.grpStackerType.Controls.Add(this.txtStackerSize);
            this.grpStackerType.Controls.Add(this.txtStackerName);
            this.grpStackerType.Controls.Add(this.grpStatus);
            this.grpStackerType.Controls.Add(this.txtStackerDescription);
            this.grpStackerType.Controls.Add(this.lblDescription);
            this.grpStackerType.Controls.Add(this.lblSize);
            this.grpStackerType.Controls.Add(this.lblName);
            this.grpStackerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStackerType.Location = new System.Drawing.Point(12, 12);
            this.grpStackerType.Name = "grpStackerType";
            this.grpStackerType.Size = new System.Drawing.Size(444, 218);
            this.grpStackerType.TabIndex = 0;
            this.grpStackerType.TabStop = false;
            this.grpStackerType.Text = "Stacker Type";
            // 
            // txtStackerSize
            // 
            this.txtStackerSize.Location = new System.Drawing.Point(79, 65);
            this.txtStackerSize.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtStackerSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtStackerSize.Name = "txtStackerSize";
            this.txtStackerSize.Size = new System.Drawing.Size(109, 20);
            this.txtStackerSize.TabIndex = 3;
            this.txtStackerSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtStackerSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStackerSize_KeyPress);
            // 
            // txtStackerName
            // 
            this.txtStackerName.Location = new System.Drawing.Point(79, 28);
            this.txtStackerName.MaxLength = 50;
            this.txtStackerName.Name = "txtStackerName";
            this.txtStackerName.Size = new System.Drawing.Size(228, 20);
            this.txtStackerName.TabIndex = 1;
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.rdbInActive);
            this.grpStatus.Controls.Add(this.rdbActive);
            this.grpStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStatus.Location = new System.Drawing.Point(313, 19);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(118, 87);
            this.grpStatus.TabIndex = 4;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status";
            // 
            // rdbInActive
            // 
            this.rdbInActive.AutoSize = true;
            this.rdbInActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbInActive.Location = new System.Drawing.Point(20, 53);
            this.rdbInActive.Name = "rdbInActive";
            this.rdbInActive.Size = new System.Drawing.Size(67, 17);
            this.rdbInActive.TabIndex = 1;
            this.rdbInActive.TabStop = true;
            this.rdbInActive.Text = "In Active";
            this.rdbInActive.UseVisualStyleBackColor = true;
            // 
            // rdbActive
            // 
            this.rdbActive.AutoSize = true;
            this.rdbActive.Checked = true;
            this.rdbActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbActive.Location = new System.Drawing.Point(20, 20);
            this.rdbActive.Name = "rdbActive";
            this.rdbActive.Size = new System.Drawing.Size(55, 17);
            this.rdbActive.TabIndex = 0;
            this.rdbActive.TabStop = true;
            this.rdbActive.Text = "Active";
            this.rdbActive.UseVisualStyleBackColor = true;
            // 
            // txtStackerDescription
            // 
            this.txtStackerDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStackerDescription.Location = new System.Drawing.Point(20, 127);
            this.txtStackerDescription.MaxLength = 255;
            this.txtStackerDescription.Name = "txtStackerDescription";
            this.txtStackerDescription.Size = new System.Drawing.Size(411, 74);
            this.txtStackerDescription.TabIndex = 6;
            this.txtStackerDescription.Text = "";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(17, 101);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(66, 13);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Description :";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSize.Location = new System.Drawing.Point(17, 67);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(40, 13);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = "* Size :";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(17, 31);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(48, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "* Name :";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 241);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 49);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(270, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 49);
            this.panel2.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(19, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(113, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmStackerAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 290);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpStackerType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStackerAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stacker - Add & Edit ";
            this.Load += new System.EventHandler(this.StackerAddEdit_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStackerAddEdit_KeyDown);
            this.grpStackerType.ResumeLayout(false);
            this.grpStackerType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStackerSize)).EndInit();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStackerType;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.RadioButton rdbInActive;
        private System.Windows.Forms.RadioButton rdbActive;
        private System.Windows.Forms.RichTextBox txtStackerDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.NumericUpDown txtStackerSize;
        private System.Windows.Forms.TextBox txtStackerName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
    }
}