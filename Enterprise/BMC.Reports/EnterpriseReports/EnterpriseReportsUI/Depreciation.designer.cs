namespace BMC.EnterpriseReportsUI
{
    partial class Depreciation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Depreciation));
            this.Grpbox_reportparams = new System.Windows.Forms.GroupBox();
            this.lbl_caution = new System.Windows.Forms.Label();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.dtp_purchasedto = new System.Windows.Forms.DateTimePicker();
            this.dtp_purchasedfrom = new System.Windows.Forms.DateTimePicker();
            this.dtp_periodto = new System.Windows.Forms.DateTimePicker();
            this.dtp_periodfrom = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_CreateReport = new System.Windows.Forms.Button();
            this.chkSum = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_selectAll = new System.Windows.Forms.Button();
            this.btn_selectNone = new System.Windows.Forms.Button();
            this.lst_machineavailability = new System.Windows.Forms.ListBox();
            this.cmb_depot = new System.Windows.Forms.ComboBox();
            this.cmb_supplier = new System.Windows.Forms.ComboBox();
            this.cmb_machinename = new System.Windows.Forms.ComboBox();
            this.cmb_machinetype = new System.Windows.Forms.ComboBox();
            this.depreciation_progress = new System.Windows.Forms.ProgressBar();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.Grpbox_reportparams.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grpbox_reportparams
            // 
            this.Grpbox_reportparams.Controls.Add(this.lbl_caution);
            this.Grpbox_reportparams.Controls.Add(this.chkFilter);
            this.Grpbox_reportparams.Controls.Add(this.dtp_purchasedto);
            this.Grpbox_reportparams.Controls.Add(this.dtp_purchasedfrom);
            this.Grpbox_reportparams.Controls.Add(this.dtp_periodto);
            this.Grpbox_reportparams.Controls.Add(this.dtp_periodfrom);
            this.Grpbox_reportparams.Controls.Add(this.label10);
            this.Grpbox_reportparams.Controls.Add(this.label9);
            this.Grpbox_reportparams.Controls.Add(this.label8);
            this.Grpbox_reportparams.Controls.Add(this.label7);
            this.Grpbox_reportparams.Controls.Add(this.label6);
            this.Grpbox_reportparams.Controls.Add(this.label5);
            this.Grpbox_reportparams.Controls.Add(this.label4);
            this.Grpbox_reportparams.Controls.Add(this.label3);
            this.Grpbox_reportparams.Controls.Add(this.label2);
            this.Grpbox_reportparams.Controls.Add(this.btn_CreateReport);
            this.Grpbox_reportparams.Controls.Add(this.chkSum);
            this.Grpbox_reportparams.Controls.Add(this.label1);
            this.Grpbox_reportparams.Controls.Add(this.btn_selectAll);
            this.Grpbox_reportparams.Controls.Add(this.btn_selectNone);
            this.Grpbox_reportparams.Controls.Add(this.lst_machineavailability);
            this.Grpbox_reportparams.Controls.Add(this.cmb_depot);
            this.Grpbox_reportparams.Controls.Add(this.cmb_supplier);
            this.Grpbox_reportparams.Controls.Add(this.cmb_machinename);
            this.Grpbox_reportparams.Controls.Add(this.cmb_machinetype);
            this.Grpbox_reportparams.Location = new System.Drawing.Point(48, 40);
            this.Grpbox_reportparams.Name = "Grpbox_reportparams";
            this.Grpbox_reportparams.Size = new System.Drawing.Size(665, 502);
            this.Grpbox_reportparams.TabIndex = 0;
            this.Grpbox_reportparams.TabStop = false;
            // 
            // lbl_caution
            // 
            this.lbl_caution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbl_caution.Location = new System.Drawing.Point(413, 281);
            this.lbl_caution.Name = "lbl_caution";
            this.lbl_caution.Size = new System.Drawing.Size(239, 62);
            this.lbl_caution.TabIndex = 51;
            this.lbl_caution.Text = "Caution - combining certain selections will give conflicting results";
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.Location = new System.Drawing.Point(135, 170);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(144, 17);
            this.chkFilter.TabIndex = 50;
            this.chkFilter.Text = "Use Purchase Date Filter";
            this.chkFilter.UseVisualStyleBackColor = true;
            this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // dtp_purchasedto
            // 
            this.dtp_purchasedto.Enabled = false;
            this.dtp_purchasedto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtp_purchasedto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_purchasedto.Location = new System.Drawing.Point(452, 209);
            this.dtp_purchasedto.Name = "dtp_purchasedto";
            this.dtp_purchasedto.Size = new System.Drawing.Size(200, 20);
            this.dtp_purchasedto.TabIndex = 49;
            this.dtp_purchasedto.Value = new System.DateTime(2003, 4, 30, 15, 22, 0, 0);
            // 
            // dtp_purchasedfrom
            // 
            this.dtp_purchasedfrom.Enabled = false;
            this.dtp_purchasedfrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtp_purchasedfrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_purchasedfrom.Location = new System.Drawing.Point(135, 213);
            this.dtp_purchasedfrom.Name = "dtp_purchasedfrom";
            this.dtp_purchasedfrom.Size = new System.Drawing.Size(200, 20);
            this.dtp_purchasedfrom.TabIndex = 48;
            this.dtp_purchasedfrom.Value = new System.DateTime(2003, 4, 30, 15, 22, 0, 0);
            // 
            // dtp_periodto
            // 
            this.dtp_periodto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtp_periodto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_periodto.Location = new System.Drawing.Point(452, 137);
            this.dtp_periodto.Name = "dtp_periodto";
            this.dtp_periodto.Size = new System.Drawing.Size(200, 20);
            this.dtp_periodto.TabIndex = 47;
            this.dtp_periodto.Value = new System.DateTime(2003, 4, 30, 15, 22, 0, 0);
            // 
            // dtp_periodfrom
            // 
            this.dtp_periodfrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dtp_periodfrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_periodfrom.Location = new System.Drawing.Point(135, 134);
            this.dtp_periodfrom.Name = "dtp_periodfrom";
            this.dtp_periodfrom.Size = new System.Drawing.Size(200, 20);
            this.dtp_periodfrom.TabIndex = 46;
            this.dtp_periodfrom.Value = new System.DateTime(2003, 4, 30, 15, 22, 0, 0);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label10.Location = new System.Drawing.Point(12, 299);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 45;
            this.label10.Text = "Status";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label9.Location = new System.Drawing.Point(376, 212);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "To";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label8.Location = new System.Drawing.Point(12, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Purchased From";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label7.Location = new System.Drawing.Point(373, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "To";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(373, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Depot";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.Location = new System.Drawing.Point(12, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Period From";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(12, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Machine Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Machine Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.Location = new System.Drawing.Point(373, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Supplier";
            // 
            // btn_CreateReport
            // 
            this.btn_CreateReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_CreateReport.Location = new System.Drawing.Point(497, 465);
            this.btn_CreateReport.Name = "btn_CreateReport";
            this.btn_CreateReport.Size = new System.Drawing.Size(121, 23);
            this.btn_CreateReport.TabIndex = 36;
            this.btn_CreateReport.Text = "Create Report";
            this.btn_CreateReport.UseVisualStyleBackColor = true;
            this.btn_CreateReport.Click += new System.EventHandler(this.btn_CreateReport_Click);
            // 
            // chkSum
            // 
            this.chkSum.AutoSize = true;
            this.chkSum.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSum.Location = new System.Drawing.Point(260, 449);
            this.chkSum.Name = "chkSum";
            this.chkSum.Size = new System.Drawing.Size(15, 14);
            this.chkSum.TabIndex = 35;
            this.chkSum.UseVisualStyleBackColor = true;
            this.chkSum.CheckedChanged += new System.EventHandler(this.chkSum_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(132, 449);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Summary Only";
            // 
            // btn_selectAll
            // 
            this.btn_selectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_selectAll.Location = new System.Drawing.Point(260, 397);
            this.btn_selectAll.Name = "btn_selectAll";
            this.btn_selectAll.Size = new System.Drawing.Size(75, 23);
            this.btn_selectAll.TabIndex = 33;
            this.btn_selectAll.Text = "Select All";
            this.btn_selectAll.UseVisualStyleBackColor = true;
            this.btn_selectAll.Click += new System.EventHandler(this.btn_selectAll_Click);
            // 
            // btn_selectNone
            // 
            this.btn_selectNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_selectNone.Location = new System.Drawing.Point(135, 397);
            this.btn_selectNone.Name = "btn_selectNone";
            this.btn_selectNone.Size = new System.Drawing.Size(75, 23);
            this.btn_selectNone.TabIndex = 32;
            this.btn_selectNone.Text = "Select None";
            this.btn_selectNone.UseVisualStyleBackColor = true;
            this.btn_selectNone.Click += new System.EventHandler(this.btn_selectNone_Click);
            // 
            // lst_machineavailability
            // 
            this.lst_machineavailability.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lst_machineavailability.FormattingEnabled = true;
            this.lst_machineavailability.Location = new System.Drawing.Point(135, 281);
            this.lst_machineavailability.Name = "lst_machineavailability";
            this.lst_machineavailability.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_machineavailability.Size = new System.Drawing.Size(200, 82);
            this.lst_machineavailability.TabIndex = 31;
            // 
            // cmb_depot
            // 
            this.cmb_depot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_depot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmb_depot.FormattingEnabled = true;
            this.cmb_depot.Location = new System.Drawing.Point(452, 85);
            this.cmb_depot.Name = "cmb_depot";
            this.cmb_depot.Size = new System.Drawing.Size(199, 21);
            this.cmb_depot.TabIndex = 30;
            // 
            // cmb_supplier
            // 
            this.cmb_supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_supplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmb_supplier.FormattingEnabled = true;
            this.cmb_supplier.Location = new System.Drawing.Point(452, 45);
            this.cmb_supplier.Name = "cmb_supplier";
            this.cmb_supplier.Size = new System.Drawing.Size(199, 21);
            this.cmb_supplier.TabIndex = 29;
            this.cmb_supplier.SelectedIndexChanged += new System.EventHandler(this.cmb_supplier_SelectedIndexChanged);
            // 
            // cmb_machinename
            // 
            this.cmb_machinename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_machinename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmb_machinename.FormattingEnabled = true;
            this.cmb_machinename.Location = new System.Drawing.Point(135, 85);
            this.cmb_machinename.Name = "cmb_machinename";
            this.cmb_machinename.Size = new System.Drawing.Size(200, 21);
            this.cmb_machinename.TabIndex = 28;
            // 
            // cmb_machinetype
            // 
            this.cmb_machinetype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_machinetype.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmb_machinetype.FormattingEnabled = true;
            this.cmb_machinetype.Location = new System.Drawing.Point(135, 45);
            this.cmb_machinetype.Name = "cmb_machinetype";
            this.cmb_machinetype.Size = new System.Drawing.Size(200, 21);
            this.cmb_machinetype.TabIndex = 27;
            this.cmb_machinetype.SelectedIndexChanged += new System.EventHandler(this.cmb_machinetype_SelectedIndexChanged);
            // 
            // depreciation_progress
            // 
            this.depreciation_progress.Location = new System.Drawing.Point(48, 554);
            this.depreciation_progress.Name = "depreciation_progress";
            this.depreciation_progress.Size = new System.Drawing.Size(665, 23);
            this.depreciation_progress.TabIndex = 1;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Title.Location = new System.Drawing.Point(211, 8);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(342, 29);
            this.lbl_Title.TabIndex = 2;
            this.lbl_Title.Text = "Detailed Depreciation Report";
            // 
            // Depreciation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 584);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.depreciation_progress);
            this.Controls.Add(this.Grpbox_reportparams);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Depreciation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Depreciation Report";
            this.Load += new System.EventHandler(this.Depreciation_Form_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Depreciation_Form_FormClosed);
            this.Disposed += new System.EventHandler(this.Depreciation_Form_Disposed);
            this.Grpbox_reportparams.ResumeLayout(false);
            this.Grpbox_reportparams.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private System.Windows.Forms.GroupBox Grpbox_reportparams;
        private System.Windows.Forms.DateTimePicker dtp_purchasedto;
        private System.Windows.Forms.DateTimePicker dtp_purchasedfrom;
        private System.Windows.Forms.DateTimePicker dtp_periodto;
        private System.Windows.Forms.DateTimePicker dtp_periodfrom;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_CreateReport;
        private System.Windows.Forms.CheckBox chkSum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_selectAll;
        private System.Windows.Forms.Button btn_selectNone;
        private System.Windows.Forms.ListBox lst_machineavailability;
        private System.Windows.Forms.ComboBox cmb_depot;
        private System.Windows.Forms.ComboBox cmb_supplier;
        private System.Windows.Forms.ComboBox cmb_machinename;
        private System.Windows.Forms.ComboBox cmb_machinetype;
        private System.Windows.Forms.CheckBox chkFilter;
        private System.Windows.Forms.ProgressBar depreciation_progress;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label lbl_caution;

    }
}