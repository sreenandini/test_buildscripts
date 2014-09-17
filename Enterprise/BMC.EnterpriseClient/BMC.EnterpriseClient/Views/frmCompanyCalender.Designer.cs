namespace BMC.EnterpriseClient.Views
{
    partial class frmCompanyCalender
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.SSTab1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucCalender = new BMC.EnterpriseClient.Views.ucCalenders();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucCompanyCalender1 = new BMC.EnterpriseClient.Views.ucCompanyCalender();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ucOperatorCalendar1 = new BMC.EnterpriseClient.Views.ucOperatorCalendar();
            this.tbAutoCalendar = new System.Windows.Forms.TabPage();
            this.ucAutoCalendar1 = new BMC.EnterpriseClient.Views.UcAutoCalendar();
            this.tableLayoutPanel1.SuspendLayout();
            this.SSTab1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tbAutoCalendar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SSTab1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(626, 434);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(523, 400);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SSTab1
            // 
            this.SSTab1.Controls.Add(this.tabPage1);
            this.SSTab1.Controls.Add(this.tabPage2);
            this.SSTab1.Controls.Add(this.tabPage3);
            this.SSTab1.Controls.Add(this.tbAutoCalendar);
            this.SSTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSTab1.Location = new System.Drawing.Point(3, 3);
            this.SSTab1.Name = "SSTab1";
            this.SSTab1.SelectedIndex = 0;
            this.SSTab1.Size = new System.Drawing.Size(620, 388);
            this.SSTab1.TabIndex = 0;
            this.SSTab1.Click += new System.EventHandler(this.SSTab1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucCalender);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(612, 362);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Calendars";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucCalender
            // 
            this.ucCalender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCalender.Location = new System.Drawing.Point(3, 3);
            this.ucCalender.Name = "ucCalender";
            this.ucCalender.Size = new System.Drawing.Size(606, 356);
            this.ucCalender.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucCompanyCalender1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(612, 362);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "Companies";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucCompanyCalender1
            // 
            this.ucCompanyCalender1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCompanyCalender1.Location = new System.Drawing.Point(3, 3);
            this.ucCompanyCalender1.Name = "ucCompanyCalender1";
            this.ucCompanyCalender1.Size = new System.Drawing.Size(606, 356);
            this.ucCompanyCalender1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucOperatorCalendar1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(612, 362);
            this.tabPage3.TabIndex = 5;
            this.tabPage3.Text = "Operators";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ucOperatorCalendar1
            // 
            this.ucOperatorCalendar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOperatorCalendar1.Location = new System.Drawing.Point(0, 0);
            this.ucOperatorCalendar1.Name = "ucOperatorCalendar1";
            this.ucOperatorCalendar1.Size = new System.Drawing.Size(612, 362);
            this.ucOperatorCalendar1.TabIndex = 0;
            // 
            // tbAutoCalendar
            // 
            this.tbAutoCalendar.Controls.Add(this.ucAutoCalendar1);
            this.tbAutoCalendar.Location = new System.Drawing.Point(4, 22);
            this.tbAutoCalendar.Name = "tbAutoCalendar";
            this.tbAutoCalendar.Size = new System.Drawing.Size(612, 362);
            this.tbAutoCalendar.TabIndex = 6;
            this.tbAutoCalendar.Text = "Auto Calendar";
            this.tbAutoCalendar.UseVisualStyleBackColor = true;
            // 
            // ucAutoCalendar1
            // 
            this.ucAutoCalendar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAutoCalendar1.Location = new System.Drawing.Point(0, 0);
            this.ucAutoCalendar1.Name = "ucAutoCalendar1";
            this.ucAutoCalendar1.Size = new System.Drawing.Size(612, 362);
            this.ucAutoCalendar1.TabIndex = 0;
            // 
            // frmCompanyCalender
            // 
            this.AutoSize = true;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(626, 434);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmCompanyCalender";
            this.Text = "Financial Calender";
            this.Load += new System.EventHandler(this.frmCompanyCalender_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.SSTab1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tbAutoCalendar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Calender;
        private System.Windows.Forms.TabPage CalenderTab;
        private System.Windows.Forms.TabPage CompaniesTab;
        private System.Windows.Forms.TabPage OperatorTab;
        private System.Windows.Forms.GroupBox grpCurrentCalender;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblYearBegins;
        private System.Windows.Forms.Label lblYearEnd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnClose;
        private ucCalenders ucCalenders1;
        private System.Windows.Forms.TabControl SSTab1;
        private System.Windows.Forms.TabPage tabPage1;
        private ucCalenders ucCalender;
        private System.Windows.Forms.TabPage tabPage2;
        private ucCompanyCalender ucCompanyCalender1;
        private System.Windows.Forms.TabPage tabPage3;
        private ucOperatorCalendar ucOperatorCalendar1;
        private System.Windows.Forms.TabPage tbAutoCalendar;
        private UcAutoCalendar ucAutoCalendar1;
    }
}