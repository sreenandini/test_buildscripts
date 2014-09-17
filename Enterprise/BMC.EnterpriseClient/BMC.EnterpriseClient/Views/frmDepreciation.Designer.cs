using BMC.EnterpriseClient.Helpers;
namespace BMC.EnterpriseClient.Views
{
    partial class frmDepreciation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDepreciation));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnApplyPolicy = new System.Windows.Forms.Button();
            this.btnNewPolicy = new System.Windows.Forms.Button();
            this.btnDeletePolicy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtResidualValue = new System.Windows.Forms.TextBox();
            this.cmbDepreciationPolicies = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDropPercent = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.btnApplyDetails = new System.Windows.Forms.Button();
            this.btnNewDetails = new System.Windows.Forms.Button();
            this.lv_DepreciationDetails = new System.Windows.Forms.ListView();
            this.Period = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Drop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteDetails = new System.Windows.Forms.Button();
            this.btnUpdateDefaults = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnApplyPolicy);
            this.groupBox1.Controls.Add(this.btnNewPolicy);
            this.groupBox1.Controls.Add(this.btnDeletePolicy);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtResidualValue);
            this.groupBox1.Controls.Add(this.cmbDepreciationPolicies);
            this.groupBox1.Location = new System.Drawing.Point(6, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 91);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Depreciation Policy";
            // 
            // btnApplyPolicy
            // 
            this.btnApplyPolicy.Location = new System.Drawing.Point(351, 55);
            this.btnApplyPolicy.Name = "btnApplyPolicy";
            this.btnApplyPolicy.Size = new System.Drawing.Size(85, 23);
            this.btnApplyPolicy.TabIndex = 5;
            this.btnApplyPolicy.Text = "Apply";
            this.btnApplyPolicy.UseVisualStyleBackColor = true;
            this.btnApplyPolicy.Click += new System.EventHandler(this.btn_ApplyPolicy_Click);
            // 
            // btnNewPolicy
            // 
            this.btnNewPolicy.Location = new System.Drawing.Point(351, 21);
            this.btnNewPolicy.Name = "btnNewPolicy";
            this.btnNewPolicy.Size = new System.Drawing.Size(85, 23);
            this.btnNewPolicy.TabIndex = 2;
            this.btnNewPolicy.Text = "New";
            this.btnNewPolicy.UseVisualStyleBackColor = true;
            this.btnNewPolicy.Click += new System.EventHandler(this.btn_NewPolicy_Click);
            // 
            // btnDeletePolicy
            // 
            this.btnDeletePolicy.Location = new System.Drawing.Point(252, 21);
            this.btnDeletePolicy.Name = "btnDeletePolicy";
            this.btnDeletePolicy.Size = new System.Drawing.Size(85, 23);
            this.btnDeletePolicy.TabIndex = 1;
            this.btnDeletePolicy.Text = "Delete";
            this.btnDeletePolicy.UseVisualStyleBackColor = true;
            this.btnDeletePolicy.Click += new System.EventHandler(this.btn_DeletePolicy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Residual Value:";
            // 
            // txtResidualValue
            // 
            this.txtResidualValue.Location = new System.Drawing.Point(252, 58);
            this.txtResidualValue.Name = "txtResidualValue";
            this.txtResidualValue.Size = new System.Drawing.Size(84, 21);
            this.txtResidualValue.TabIndex = 4;
            this.txtResidualValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDuration_KeyPress);
            // 
            // cmbDepreciationPolicies
            // 
            this.cmbDepreciationPolicies.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDepreciationPolicies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepreciationPolicies.FormattingEnabled = true;
            this.cmbDepreciationPolicies.Location = new System.Drawing.Point(13, 23);
            this.cmbDepreciationPolicies.Name = "cmbDepreciationPolicies";
            this.cmbDepreciationPolicies.Size = new System.Drawing.Size(215, 22);
            this.cmbDepreciationPolicies.TabIndex = 0;
            this.cmbDepreciationPolicies.SelectedIndexChanged += new System.EventHandler(this.cmbDepreciationPolicies_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDropPercent);
            this.groupBox2.Controls.Add(this.txtDuration);
            this.groupBox2.Controls.Add(this.txtPeriod);
            this.groupBox2.Controls.Add(this.btnApplyDetails);
            this.groupBox2.Controls.Add(this.btnNewDetails);
            this.groupBox2.Controls.Add(this.lv_DepreciationDetails);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnDeleteDetails);
            this.groupBox2.Location = new System.Drawing.Point(6, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 234);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Depreciation Details";
            // 
            // txtDropPercent
            // 
            this.txtDropPercent.Location = new System.Drawing.Point(372, 165);
            this.txtDropPercent.MaxLength = 100;
            this.txtDropPercent.Name = "txtDropPercent";
            this.txtDropPercent.Size = new System.Drawing.Size(63, 21);
            this.txtDropPercent.TabIndex = 6;
            this.txtDropPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDuration_KeyPress);
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(223, 165);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(63, 21);
            this.txtDuration.TabIndex = 4;
            this.txtDuration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDuration_KeyPress);
            // 
            // txtPeriod
            // 
            this.txtPeriod.BackColor = System.Drawing.SystemColors.Window;
            this.txtPeriod.Location = new System.Drawing.Point(63, 165);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.ReadOnly = true;
            this.txtPeriod.Size = new System.Drawing.Size(63, 21);
            this.txtPeriod.TabIndex = 2;
            // 
            // btnApplyDetails
            // 
            this.btnApplyDetails.Location = new System.Drawing.Point(351, 199);
            this.btnApplyDetails.Name = "btnApplyDetails";
            this.btnApplyDetails.Size = new System.Drawing.Size(85, 23);
            this.btnApplyDetails.TabIndex = 9;
            this.btnApplyDetails.Text = "Apply";
            this.btnApplyDetails.UseVisualStyleBackColor = true;
            this.btnApplyDetails.Click += new System.EventHandler(this.btn_ApplyDetails_Click);
            // 
            // btnNewDetails
            // 
            this.btnNewDetails.Location = new System.Drawing.Point(252, 199);
            this.btnNewDetails.Name = "btnNewDetails";
            this.btnNewDetails.Size = new System.Drawing.Size(85, 23);
            this.btnNewDetails.TabIndex = 8;
            this.btnNewDetails.Text = "New";
            this.btnNewDetails.UseVisualStyleBackColor = true;
            this.btnNewDetails.Click += new System.EventHandler(this.btn_NewDetails_Click);
            // 
            // lv_DepreciationDetails
            // 
            this.lv_DepreciationDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Period,
            this.Duration,
            this.Drop});
            this.lv_DepreciationDetails.FullRowSelect = true;
            this.lv_DepreciationDetails.GridLines = true;
            this.lv_DepreciationDetails.Location = new System.Drawing.Point(13, 17);
            this.lv_DepreciationDetails.MultiSelect = false;
            this.lv_DepreciationDetails.Name = "lv_DepreciationDetails";
            this.lv_DepreciationDetails.Size = new System.Drawing.Size(422, 136);
            this.lv_DepreciationDetails.TabIndex = 0;
            this.lv_DepreciationDetails.UseCompatibleStateImageBehavior = false;
            this.lv_DepreciationDetails.View = System.Windows.Forms.View.Details;
            this.lv_DepreciationDetails.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lv_DepreciationDetails_ItemSelectionChanged);
            // 
            // Period
            // 
            this.Period.Text = "Period";
            this.Period.Width = 119;
            // 
            // Duration
            // 
            this.Duration.Text = "Duration(Months)";
            this.Duration.Width = 119;
            // 
            // Drop
            // 
            this.Drop.Text = "%Drop";
            this.Drop.Width = 119;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(317, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "% Drop:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Duration:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Period:";
            // 
            // btnDeleteDetails
            // 
            this.btnDeleteDetails.Location = new System.Drawing.Point(13, 199);
            this.btnDeleteDetails.Name = "btnDeleteDetails";
            this.btnDeleteDetails.Size = new System.Drawing.Size(85, 23);
            this.btnDeleteDetails.TabIndex = 7;
            this.btnDeleteDetails.Text = "Delete";
            this.btnDeleteDetails.UseVisualStyleBackColor = true;
            this.btnDeleteDetails.Click += new System.EventHandler(this.btn_DeleteDetails_Click);
            // 
            // btnUpdateDefaults
            // 
            this.btnUpdateDefaults.Location = new System.Drawing.Point(5, 342);
            this.btnUpdateDefaults.Name = "btnUpdateDefaults";
            this.btnUpdateDefaults.Size = new System.Drawing.Size(133, 31);
            this.btnUpdateDefaults.TabIndex = 2;
            this.btnUpdateDefaults.Text = "Update Defaults";
            this.btnUpdateDefaults.UseVisualStyleBackColor = true;
            this.btnUpdateDefaults.Click += new System.EventHandler(this.btnUpdateDefaults_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(352, 342);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 31);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // frmDepreciation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(458, 379);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdateDefaults);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDepreciation";
            this.Text = "Depreciation";
            this.Load += new System.EventHandler(this.frmDepreciation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtResidualValue;
        private BmcComboBox cmbDepreciationPolicies;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeleteDetails;
        private System.Windows.Forms.Button btnUpdateDefaults;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApplyDetails;
        private System.Windows.Forms.Button btnNewDetails;
        private System.Windows.Forms.ListView lv_DepreciationDetails;
        private System.Windows.Forms.Button btnApplyPolicy;
        private System.Windows.Forms.Button btnNewPolicy;
        private System.Windows.Forms.Button btnDeletePolicy;
        private System.Windows.Forms.TextBox txtDropPercent;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.ColumnHeader Period;
        private System.Windows.Forms.ColumnHeader Duration;
        private System.Windows.Forms.ColumnHeader Drop;
    }
}