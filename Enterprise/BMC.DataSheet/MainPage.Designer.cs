namespace CustomReports
{
    partial class MainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.pnlPUPD = new System.Windows.Forms.Panel();
            this.dtpPUPDEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpPUPDStartDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbReportName = new BMC.Common.Utilities.BmcComboBox();
            this.chkLstBoxCustom = new System.Windows.Forms.CheckedListBox();
            this.pnlGeneral = new System.Windows.Forms.Panel();
            this.cmbBasedOn = new BMC.Common.Utilities.BmcComboBox();
            this.lblBasedon = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cmbCompany = new BMC.Common.Utilities.BmcComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbZone = new BMC.Common.Utilities.BmcComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSite = new BMC.Common.Utilities.BmcComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSubCompany = new BMC.Common.Utilities.BmcComboBox();
            this.groupBox1.SuspendLayout();
            this.pnlPUPD.SuspendLayout();
            this.pnlGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSelect);
            this.groupBox1.Controls.Add(this.pnlPUPD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbReportName);
            this.groupBox1.Controls.Add(this.chkLstBoxCustom);
            this.groupBox1.Controls.Add(this.pnlGeneral);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(809, 563);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criteria";
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSize = true;
            this.chkSelect.Checked = true;
            this.chkSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelect.Location = new System.Drawing.Point(532, 21);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(70, 17);
            this.chkSelect.TabIndex = 16;
            this.chkSelect.Text = "Select All";
            this.chkSelect.UseVisualStyleBackColor = true;
            this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
            // 
            // pnlPUPD
            // 
            this.pnlPUPD.Controls.Add(this.dtpPUPDEndDate);
            this.pnlPUPD.Controls.Add(this.dtpPUPDStartDate);
            this.pnlPUPD.Controls.Add(this.label7);
            this.pnlPUPD.Controls.Add(this.label6);
            this.pnlPUPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPUPD.Location = new System.Drawing.Point(39, 260);
            this.pnlPUPD.Name = "pnlPUPD";
            this.pnlPUPD.Size = new System.Drawing.Size(454, 244);
            this.pnlPUPD.TabIndex = 4;
            this.pnlPUPD.Visible = false;
            // 
            // dtpPUPDEndDate
            // 
            this.dtpPUPDEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPUPDEndDate.Location = new System.Drawing.Point(160, 60);
            this.dtpPUPDEndDate.Name = "dtpPUPDEndDate";
            this.dtpPUPDEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpPUPDEndDate.TabIndex = 14;
            this.dtpPUPDEndDate.ValueChanged += new System.EventHandler(this.dtpPUPDEndDate_ValueChanged);
            // 
            // dtpPUPDStartDate
            // 
            this.dtpPUPDStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPUPDStartDate.Location = new System.Drawing.Point(160, 18);
            this.dtpPUPDStartDate.Name = "dtpPUPDStartDate";
            this.dtpPUPDStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpPUPDStartDate.TabIndex = 12;
            this.dtpPUPDStartDate.ValueChanged += new System.EventHandler(this.dtpPUPDStartDate_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(48, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "End Date:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(48, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Start Date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Report Name:";
            // 
            // cmbReportName
            // 
            this.cmbReportName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbReportName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReportName.FormattingEnabled = true;
            this.cmbReportName.Location = new System.Drawing.Point(199, 19);
            this.cmbReportName.Name = "cmbReportName";
            this.cmbReportName.Size = new System.Drawing.Size(294, 21);
            this.cmbReportName.TabIndex = 2;
            this.cmbReportName.SelectedIndexChanged += new System.EventHandler(this.cmbReportName_SelectedIndexChanged);
            // 
            // chkLstBoxCustom
            // 
            this.chkLstBoxCustom.CheckOnClick = true;
            this.chkLstBoxCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLstBoxCustom.FormattingEnabled = true;
            this.chkLstBoxCustom.Location = new System.Drawing.Point(531, 50);
            this.chkLstBoxCustom.Name = "chkLstBoxCustom";
            this.chkLstBoxCustom.Size = new System.Drawing.Size(219, 484);
            this.chkLstBoxCustom.TabIndex = 17;
            // 
            // pnlGeneral
            // 
            this.pnlGeneral.Controls.Add(this.cmbBasedOn);
            this.pnlGeneral.Controls.Add(this.lblBasedon);
            this.pnlGeneral.Controls.Add(this.btnGenerate);
            this.pnlGeneral.Controls.Add(this.cmbCompany);
            this.pnlGeneral.Controls.Add(this.label2);
            this.pnlGeneral.Controls.Add(this.label3);
            this.pnlGeneral.Controls.Add(this.cmbZone);
            this.pnlGeneral.Controls.Add(this.label4);
            this.pnlGeneral.Controls.Add(this.cmbSite);
            this.pnlGeneral.Controls.Add(this.label5);
            this.pnlGeneral.Controls.Add(this.cmbSubCompany);
            this.pnlGeneral.Location = new System.Drawing.Point(39, 46);
            this.pnlGeneral.Name = "pnlGeneral";
            this.pnlGeneral.Size = new System.Drawing.Size(454, 497);
            this.pnlGeneral.TabIndex = 5;
            this.pnlGeneral.Visible = false;
            // 
            // cmbBasedOn
            // 
            this.cmbBasedOn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBasedOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBasedOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBasedOn.FormattingEnabled = true;
            this.cmbBasedOn.Location = new System.Drawing.Point(160, 10);
            this.cmbBasedOn.Name = "cmbBasedOn";
            this.cmbBasedOn.Size = new System.Drawing.Size(200, 21);
            this.cmbBasedOn.TabIndex = 17;
            // 
            // lblBasedon
            // 
            this.lblBasedon.AutoSize = true;
            this.lblBasedon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBasedon.Location = new System.Drawing.Point(48, 13);
            this.lblBasedon.Name = "lblBasedon";
            this.lblBasedon.Size = new System.Drawing.Size(57, 13);
            this.lblBasedon.TabIndex = 16;
            this.lblBasedon.Text = "Based On:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(180, 461);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(85, 28);
            this.btnGenerate.TabIndex = 15;
            this.btnGenerate.Text = "&Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(160, 51);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(200, 21);
            this.cmbCompany.TabIndex = 4;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Company:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sub Company:";
            // 
            // cmbZone
            // 
            this.cmbZone.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbZone.FormattingEnabled = true;
            this.cmbZone.Location = new System.Drawing.Point(160, 180);
            this.cmbZone.Name = "cmbZone";
            this.cmbZone.Size = new System.Drawing.Size(200, 21);
            this.cmbZone.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(48, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Site:";
            // 
            // cmbSite
            // 
            this.cmbSite.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Location = new System.Drawing.Point(160, 137);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(200, 21);
            this.cmbSite.TabIndex = 8;
            this.cmbSite.SelectedIndexChanged += new System.EventHandler(this.cmbSite_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(48, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Zone:";
            // 
            // cmbSubCompany
            // 
            this.cmbSubCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubCompany.FormattingEnabled = true;
            this.cmbSubCompany.Location = new System.Drawing.Point(160, 94);
            this.cmbSubCompany.Name = "cmbSubCompany";
            this.cmbSubCompany.Size = new System.Drawing.Size(200, 21);
            this.cmbSubCompany.TabIndex = 6;
            this.cmbSubCompany.SelectedIndexChanged += new System.EventHandler(this.cmbSubCompany_SelectedIndexChanged);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 563);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BMC Data Sheet";
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlPUPD.ResumeLayout(false);
            this.pnlPUPD.PerformLayout();
            this.pnlGeneral.ResumeLayout(false);
            this.pnlGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chkLstBoxCustom;
        private System.Windows.Forms.Label label1;
        private BMC.Common.Utilities.BmcComboBox cmbReportName;
        private System.Windows.Forms.Panel pnlPUPD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpPUPDEndDate;
        private System.Windows.Forms.DateTimePicker dtpPUPDStartDate;
        private BMC.Common.Utilities.BmcComboBox cmbZone;
        private BMC.Common.Utilities.BmcComboBox cmbSite;
        private BMC.Common.Utilities.BmcComboBox cmbSubCompany;
        private BMC.Common.Utilities.BmcComboBox cmbCompany;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Panel pnlGeneral;
        private System.Windows.Forms.CheckBox chkSelect;
        private BMC.Common.Utilities.BmcComboBox cmbBasedOn;
        private System.Windows.Forms.Label lblBasedon;
    }
}

