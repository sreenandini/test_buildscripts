namespace AuditViewer
{
    partial class frmAuditViewer
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblModule = new System.Windows.Forms.Label();
            this.dtFromPicker = new System.Windows.Forms.DateTimePicker();
            this.dtToPicker = new System.Windows.Forms.DateTimePicker();
            this.cmbModule = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtFromTime = new System.Windows.Forms.DateTimePicker();
            this.dtToTime = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(25, 158);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1128, 500);
            this.dataGridView1.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(483, 19);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(213, 23);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Audit Viewer";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblFromDate
            // 
            this.lblFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Location = new System.Drawing.Point(32, 112);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(45, 23);
            this.lblFromDate.TabIndex = 2;
            this.lblFromDate.Text = "From:";
            // 
            // lblToDate
            // 
            this.lblToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(240, 112);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(45, 23);
            this.lblToDate.TabIndex = 3;
            this.lblToDate.Text = "To:";
            // 
            // lblModule
            // 
            this.lblModule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModule.Location = new System.Drawing.Point(484, 110);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(63, 23);
            this.lblModule.TabIndex = 4;
            this.lblModule.Text = "Module:";
            // 
            // dtFromPicker
            // 
            this.dtFromPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFromPicker.Location = new System.Drawing.Point(74, 110);
            this.dtFromPicker.Name = "dtFromPicker";
            this.dtFromPicker.ShowUpDown = true;
            this.dtFromPicker.Size = new System.Drawing.Size(87, 20);
            this.dtFromPicker.TabIndex = 5;
            // 
            // dtToPicker
            // 
            this.dtToPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtToPicker.Location = new System.Drawing.Point(265, 110);
            this.dtToPicker.Name = "dtToPicker";
            this.dtToPicker.ShowUpDown = true;
            this.dtToPicker.Size = new System.Drawing.Size(86, 20);
            this.dtToPicker.TabIndex = 6;
            // 
            // cmbModule
            // 
            this.cmbModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModule.FormattingEnabled = true;
            this.cmbModule.Location = new System.Drawing.Point(542, 109);
            this.cmbModule.Name = "cmbModule";
            this.cmbModule.Size = new System.Drawing.Size(121, 21);
            this.cmbModule.TabIndex = 7;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(691, 109);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(124, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtFromTime
            // 
            this.dtFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtFromTime.Location = new System.Drawing.Point(167, 110);
            this.dtFromTime.Name = "dtFromTime";
            this.dtFromTime.ShowUpDown = true;
            this.dtFromTime.Size = new System.Drawing.Size(67, 20);
            this.dtFromTime.TabIndex = 9;
            // 
            // dtToTime
            // 
            this.dtToTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtToTime.Location = new System.Drawing.Point(357, 110);
            this.dtToTime.Name = "dtToTime";
            this.dtToTime.ShowUpDown = true;
            this.dtToTime.Size = new System.Drawing.Size(67, 20);
            this.dtToTime.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(62, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmAuditViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 691);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtToTime);
            this.Controls.Add(this.dtFromTime);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cmbModule);
            this.Controls.Add(this.dtToPicker);
            this.Controls.Add(this.dtFromPicker);
            this.Controls.Add(this.lblModule);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmAuditViewer";
            this.Text = "Audit Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblModule;
        private System.Windows.Forms.DateTimePicker dtFromPicker;
        private System.Windows.Forms.DateTimePicker dtToPicker;
        private System.Windows.Forms.ComboBox cmbModule;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtFromTime;
        private System.Windows.Forms.DateTimePicker dtToTime;
        private System.Windows.Forms.Button button1;
    }
}

