namespace BMC.EnterpriseClient.Views
{
    partial class ucRecurrenceRange
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpRecurrenceRange = new System.Windows.Forms.GroupBox();
            this.nudOcc = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.rdEndby = new System.Windows.Forms.RadioButton();
            this.rdEndafter = new System.Windows.Forms.RadioButton();
            this.rdNoenddate = new System.Windows.Forms.RadioButton();
            this.dtStartdate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.grpRecurrenceRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOcc)).BeginInit();
            this.SuspendLayout();
            // 
            // grpRecurrenceRange
            // 
            this.grpRecurrenceRange.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grpRecurrenceRange.Controls.Add(this.nudOcc);
            this.grpRecurrenceRange.Controls.Add(this.label2);
            this.grpRecurrenceRange.Controls.Add(this.dtEndDate);
            this.grpRecurrenceRange.Controls.Add(this.rdEndby);
            this.grpRecurrenceRange.Controls.Add(this.rdEndafter);
            this.grpRecurrenceRange.Controls.Add(this.rdNoenddate);
            this.grpRecurrenceRange.Controls.Add(this.dtStartdate);
            this.grpRecurrenceRange.Controls.Add(this.label1);
            this.grpRecurrenceRange.ForeColor = System.Drawing.Color.Black;
            this.grpRecurrenceRange.Location = new System.Drawing.Point(2, 5);
            this.grpRecurrenceRange.Name = "grpRecurrenceRange";
            this.grpRecurrenceRange.Size = new System.Drawing.Size(537, 110);
            this.grpRecurrenceRange.TabIndex = 0;
            this.grpRecurrenceRange.TabStop = false;
            this.grpRecurrenceRange.Text = "Range of recurrence";
            // 
            // nudOcc
            // 
            this.nudOcc.Location = new System.Drawing.Point(295, 46);
            this.nudOcc.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudOcc.Name = "nudOcc";
            this.nudOcc.Size = new System.Drawing.Size(69, 20);
            this.nudOcc.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "occurrences";
            // 
            // dtEndDate
            // 
            this.dtEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtEndDate.Enabled = false;
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndDate.Location = new System.Drawing.Point(295, 70);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(128, 20);
            this.dtEndDate.TabIndex = 6;
            // 
            // rdEndby
            // 
            this.rdEndby.AutoSize = true;
            this.rdEndby.Location = new System.Drawing.Point(204, 73);
            this.rdEndby.Name = "rdEndby";
            this.rdEndby.Size = new System.Drawing.Size(61, 17);
            this.rdEndby.TabIndex = 4;
            this.rdEndby.Text = "End by:";
            this.rdEndby.UseVisualStyleBackColor = true;
            this.rdEndby.CheckedChanged += new System.EventHandler(this.rdEndby_CheckedChanged);
            // 
            // rdEndafter
            // 
            this.rdEndafter.AutoSize = true;
            this.rdEndafter.Location = new System.Drawing.Point(204, 46);
            this.rdEndafter.Name = "rdEndafter";
            this.rdEndafter.Size = new System.Drawing.Size(71, 17);
            this.rdEndafter.TabIndex = 3;
            this.rdEndafter.Text = "End after:";
            this.rdEndafter.UseVisualStyleBackColor = true;
            this.rdEndafter.CheckedChanged += new System.EventHandler(this.rdEndafter_CheckedChanged);
            // 
            // rdNoenddate
            // 
            this.rdNoenddate.AutoSize = true;
            this.rdNoenddate.Checked = true;
            this.rdNoenddate.Location = new System.Drawing.Point(204, 19);
            this.rdNoenddate.Name = "rdNoenddate";
            this.rdNoenddate.Size = new System.Drawing.Size(84, 17);
            this.rdNoenddate.TabIndex = 2;
            this.rdNoenddate.TabStop = true;
            this.rdNoenddate.Text = "No end date";
            this.rdNoenddate.UseVisualStyleBackColor = true;
            this.rdNoenddate.CheckedChanged += new System.EventHandler(this.rdNoenddate_CheckedChanged);
            // 
            // dtStartdate
            // 
            this.dtStartdate.CustomFormat = "dd/MM/yyyy";
            this.dtStartdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStartdate.Location = new System.Drawing.Point(41, 22);
            this.dtStartdate.Name = "dtStartdate";
            this.dtStartdate.Size = new System.Drawing.Size(128, 20);
            this.dtStartdate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start:";
            // 
            // ucRecurrenceRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpRecurrenceRange);
            this.Name = "ucRecurrenceRange";
            this.Size = new System.Drawing.Size(550, 120);
            this.Load += new System.EventHandler(this.ucRecurrenceRange_Load);
            this.grpRecurrenceRange.ResumeLayout(false);
            this.grpRecurrenceRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOcc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpRecurrenceRange;
        private System.Windows.Forms.DateTimePicker dtStartdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.RadioButton rdEndby;
        private System.Windows.Forms.RadioButton rdEndafter;
        private System.Windows.Forms.RadioButton rdNoenddate;
        private System.Windows.Forms.NumericUpDown nudOcc;
    }
}
