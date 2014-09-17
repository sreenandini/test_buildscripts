namespace BMC.ComponentVerification.UI
{
    partial class CreateComponentDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateComponentDetails));
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbCType = new System.Windows.Forms.ComboBox();
            this.lblCType = new System.Windows.Forms.Label();
            this.txtSeed = new System.Windows.Forms.TextBox();
            this.lblCName = new System.Windows.Forms.Label();
            this.cbAlgorithmType = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblSerialNo = new System.Windows.Forms.Label();
            this.txtHash = new System.Windows.Forms.TextBox();
            this.lblAlgoType = new System.Windows.Forms.Label();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.lblSeedValue = new System.Windows.Forms.Label();
            this.lblHashValue = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.pnlDetails.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.groupBox2);
            this.pnlDetails.Controls.Add(this.groupBox1);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(332, 358);
            this.pnlDetails.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbCType);
            this.groupBox2.Controls.Add(this.lblCType);
            this.groupBox2.Controls.Add(this.txtSeed);
            this.groupBox2.Controls.Add(this.lblCName);
            this.groupBox2.Controls.Add(this.cbAlgorithmType);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.lblSerialNo);
            this.groupBox2.Controls.Add(this.txtHash);
            this.groupBox2.Controls.Add(this.lblAlgoType);
            this.groupBox2.Controls.Add(this.txtSerial);
            this.groupBox2.Controls.Add(this.lblSeedValue);
            this.groupBox2.Controls.Add(this.lblHashValue);
            this.groupBox2.Location = new System.Drawing.Point(5, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 295);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // cbCType
            // 
            this.cbCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCType.FormattingEnabled = true;
            this.cbCType.Location = new System.Drawing.Point(129, 30);
            this.cbCType.Name = "cbCType";
            this.cbCType.Size = new System.Drawing.Size(167, 21);
            this.cbCType.TabIndex = 1;
            this.cbCType.SelectedIndexChanged += new System.EventHandler(this.cbCType_SelectedIndexChanged);
            // 
            // lblCType
            // 
            this.lblCType.AutoSize = true;
            this.lblCType.Location = new System.Drawing.Point(19, 33);
            this.lblCType.Name = "lblCType";
            this.lblCType.Size = new System.Drawing.Size(88, 13);
            this.lblCType.TabIndex = 12;
            this.lblCType.Text = "Component Type";
            // 
            // txtSeed
            // 
            this.txtSeed.Location = new System.Drawing.Point(129, 208);
            this.txtSeed.MaxLength = 20;
            this.txtSeed.Name = "txtSeed";
            this.txtSeed.Size = new System.Drawing.Size(167, 20);
            this.txtSeed.TabIndex = 5;
            this.txtSeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSeed_KeyPress_1);
            // 
            // lblCName
            // 
            this.lblCName.AutoSize = true;
            this.lblCName.Location = new System.Drawing.Point(19, 80);
            this.lblCName.Name = "lblCName";
            this.lblCName.Size = new System.Drawing.Size(35, 13);
            this.lblCName.TabIndex = 0;
            this.lblCName.Text = "Name";
            // 
            // cbAlgorithmType
            // 
            this.cbAlgorithmType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlgorithmType.FormattingEnabled = true;
            this.cbAlgorithmType.Location = new System.Drawing.Point(129, 163);
            this.cbAlgorithmType.Name = "cbAlgorithmType";
            this.cbAlgorithmType.Size = new System.Drawing.Size(167, 21);
            this.cbAlgorithmType.TabIndex = 4;
            this.cbAlgorithmType.SelectedIndexChanged += new System.EventHandler(this.cbAlgorithmType_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(129, 77);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(167, 20);
            this.txtName.TabIndex = 2;
            // 
            // lblSerialNo
            // 
            this.lblSerialNo.AutoSize = true;
            this.lblSerialNo.Location = new System.Drawing.Point(19, 124);
            this.lblSerialNo.Name = "lblSerialNo";
            this.lblSerialNo.Size = new System.Drawing.Size(67, 13);
            this.lblSerialNo.TabIndex = 2;
            this.lblSerialNo.Text = "Model Name";
            // 
            // txtHash
            // 
            this.txtHash.Location = new System.Drawing.Point(129, 253);
            this.txtHash.MaxLength = 30;
            this.txtHash.Name = "txtHash";
            this.txtHash.Size = new System.Drawing.Size(167, 20);
            this.txtHash.TabIndex = 6;
            // 
            // lblAlgoType
            // 
            this.lblAlgoType.AutoSize = true;
            this.lblAlgoType.Location = new System.Drawing.Point(19, 166);
            this.lblAlgoType.Name = "lblAlgoType";
            this.lblAlgoType.Size = new System.Drawing.Size(77, 13);
            this.lblAlgoType.TabIndex = 4;
            this.lblAlgoType.Text = "Algorithm Type";
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(129, 121);
            this.txtSerial.MaxLength = 50;
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(167, 20);
            this.txtSerial.TabIndex = 3;
            // 
            // lblSeedValue
            // 
            this.lblSeedValue.AutoSize = true;
            this.lblSeedValue.Location = new System.Drawing.Point(19, 211);
            this.lblSeedValue.Name = "lblSeedValue";
            this.lblSeedValue.Size = new System.Drawing.Size(62, 13);
            this.lblSeedValue.TabIndex = 5;
            this.lblSeedValue.Text = "Seed Value";
            // 
            // lblHashValue
            // 
            this.lblHashValue.AutoSize = true;
            this.lblHashValue.Location = new System.Drawing.Point(19, 256);
            this.lblHashValue.Name = "lblHashValue";
            this.lblHashValue.Size = new System.Drawing.Size(62, 13);
            this.lblHashValue.TabIndex = 6;
            this.lblHashValue.Text = "Hash Value";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.Cancel);
            this.groupBox1.Location = new System.Drawing.Point(5, 294);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 58);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(34, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 29);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(186, 19);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(95, 29);
            this.Cancel.TabIndex = 8;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // CreateComponentDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(332, 358);
            this.Controls.Add(this.pnlDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateComponentDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Component Details";
            this.Load += new System.EventHandler(this.CreateComponentDetails_Load);
            this.pnlDetails.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSeedValue;
        private System.Windows.Forms.Label lblAlgoType;
        private System.Windows.Forms.Label lblSerialNo;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCName;
        private System.Windows.Forms.ComboBox cbAlgorithmType;
        private System.Windows.Forms.TextBox txtHash;
        private System.Windows.Forms.TextBox txtSeed;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.Label lblHashValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbCType;
        private System.Windows.Forms.Label lblCType;
    }
}