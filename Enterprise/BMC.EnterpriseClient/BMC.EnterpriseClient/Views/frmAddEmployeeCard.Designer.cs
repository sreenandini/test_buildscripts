namespace BMC.EnterpriseClient.Views
{
    partial class frmAddEmployeeCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEmployeeCard));
            this.cmbEmpCardNumber = new System.Windows.Forms.ComboBox();
            this.optActiveCard = new System.Windows.Forms.RadioButton();
            this.optInactiveCard = new System.Windows.Forms.RadioButton();
            this.lblEmpCardnumber = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbEmpCardNumber
            // 
            this.cmbEmpCardNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbEmpCardNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tableLayoutPanel1.SetColumnSpan(this.cmbEmpCardNumber, 2);
            this.cmbEmpCardNumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmpCardNumber.FormattingEnabled = true;
            this.cmbEmpCardNumber.Location = new System.Drawing.Point(134, 4);
            this.cmbEmpCardNumber.MaxLength = 10;
            this.cmbEmpCardNumber.Name = "cmbEmpCardNumber";
            this.cmbEmpCardNumber.Size = new System.Drawing.Size(200, 21);
            this.cmbEmpCardNumber.TabIndex = 1;
            this.cmbEmpCardNumber.SelectedIndexChanged += new System.EventHandler(this.cmbEmpCardNumber_SelectedIndexChanged);
            this.cmbEmpCardNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbEmpCardNumber_KeyPress);
            // 
            // optActiveCard
            // 
            this.optActiveCard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.optActiveCard.AutoSize = true;
            this.optActiveCard.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optActiveCard.Location = new System.Drawing.Point(134, 36);
            this.optActiveCard.Name = "optActiveCard";
            this.optActiveCard.Size = new System.Drawing.Size(92, 17);
            this.optActiveCard.TabIndex = 6;
            this.optActiveCard.TabStop = true;
            this.optActiveCard.Text = "Active Card";
            this.optActiveCard.UseVisualStyleBackColor = true;
            // 
            // optInactiveCard
            // 
            this.optInactiveCard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.optInactiveCard.AutoSize = true;
            this.optInactiveCard.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optInactiveCard.Location = new System.Drawing.Point(233, 36);
            this.optInactiveCard.Name = "optInactiveCard";
            this.optInactiveCard.Size = new System.Drawing.Size(104, 17);
            this.optInactiveCard.TabIndex = 7;
            this.optInactiveCard.TabStop = true;
            this.optInactiveCard.Text = "InActive Card";
            this.optInactiveCard.UseVisualStyleBackColor = true;
            // 
            // lblEmpCardnumber
            // 
            this.lblEmpCardnumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEmpCardnumber.AutoSize = true;
            this.lblEmpCardnumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpCardnumber.Location = new System.Drawing.Point(3, 8);
            this.lblEmpCardnumber.Name = "lblEmpCardnumber";
            this.lblEmpCardnumber.Size = new System.Drawing.Size(96, 13);
            this.lblEmpCardnumber.TabIndex = 0;
            this.lblEmpCardnumber.Text = "*Card Number:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.26166F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.12186F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.61648F));
            this.tableLayoutPanel1.Controls.Add(this.lblEmpCardnumber, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbEmpCardNumber, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.optActiveCard, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.optInactiveCard, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(343, 101);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.Location = new System.Drawing.Point(136, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(240, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.35593F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.64407F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel2.Controls.Add(this.btnClose, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 63);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(343, 35);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // frmAddEmployeeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 101);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddEmployeeCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create\\View Employee Card";
            this.Load += new System.EventHandler(this.frmAddEmployeeCard_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEmpCardNumber;
        private System.Windows.Forms.RadioButton optActiveCard;
        private System.Windows.Forms.RadioButton optInactiveCard;
        private System.Windows.Forms.Label lblEmpCardnumber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
    }
}