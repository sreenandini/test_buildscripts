namespace BMC.EnterpriseClient.Views
{
    partial class frmCreateViewEmployeeCards
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCreateCard = new System.Windows.Forms.Button();
            this.lstCardNumber = new System.Windows.Forms.ListBox();
            this.txtSearchCard = new System.Windows.Forms.TextBox();
            this.grpViewEditCard = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblEmployeeCard = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.rbtnActive = new System.Windows.Forms.RadioButton();
            this.rbtnInActive = new System.Windows.Forms.RadioButton();
            this.cmbEmployeeName = new System.Windows.Forms.ComboBox();
            this.txtEmployeeCard = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSignOff = new System.Windows.Forms.Button();
            this.txtSearchEmployeeDetails = new System.Windows.Forms.TextBox();
            this.lvwEmployeeDetails = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpViewEditCard.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnCreateCard, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.lstCardNumber, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSearchCard, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpViewEditCard, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnSignOff, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtSearchEmployeeDetails, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lvwEmployeeDetails, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 562);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Location = new System.Drawing.Point(681, 534);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEdit.Location = new System.Drawing.Point(573, 534);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 25);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCreateCard
            // 
            this.btnCreateCard.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCreateCard.Location = new System.Drawing.Point(465, 534);
            this.btnCreateCard.Name = "btnCreateCard";
            this.btnCreateCard.Size = new System.Drawing.Size(100, 25);
            this.btnCreateCard.TabIndex = 2;
            this.btnCreateCard.Text = "Create Card";
            this.btnCreateCard.UseVisualStyleBackColor = true;
            this.btnCreateCard.Click += new System.EventHandler(this.btnCreateCard_Click);
            // 
            // lstCardNumber
            // 
            this.lstCardNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCardNumber.FormattingEnabled = true;
            this.lstCardNumber.Location = new System.Drawing.Point(3, 33);
            this.lstCardNumber.Name = "lstCardNumber";
            this.tableLayoutPanel1.SetRowSpan(this.lstCardNumber, 3);
            this.lstCardNumber.Size = new System.Drawing.Size(194, 495);
            this.lstCardNumber.TabIndex = 3;
            this.lstCardNumber.SelectedIndexChanged += new System.EventHandler(this.lstCardNumber_SelectedIndexChanged);
            // 
            // txtSearchCard
            // 
            this.txtSearchCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchCard.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchCard.ForeColor = System.Drawing.Color.DimGray;
            this.txtSearchCard.Location = new System.Drawing.Point(3, 3);
            this.txtSearchCard.Name = "txtSearchCard";
            this.txtSearchCard.Size = new System.Drawing.Size(194, 23);
            this.txtSearchCard.TabIndex = 4;
            this.txtSearchCard.Text = "Search Employee Card";
            this.txtSearchCard.TextChanged += new System.EventHandler(this.txtSearchCard_TextChanged);
            this.txtSearchCard.Enter += new System.EventHandler(this.txtSearchCard_Enter);
            this.txtSearchCard.Leave += new System.EventHandler(this.txtSearchCard_Leave);
            // 
            // grpViewEditCard
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.grpViewEditCard, 5);
            this.grpViewEditCard.Controls.Add(this.tableLayoutPanel2);
            this.grpViewEditCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpViewEditCard.Location = new System.Drawing.Point(203, 3);
            this.grpViewEditCard.Name = "grpViewEditCard";
            this.tableLayoutPanel1.SetRowSpan(this.grpViewEditCard, 2);
            this.grpViewEditCard.Size = new System.Drawing.Size(578, 104);
            this.grpViewEditCard.TabIndex = 5;
            this.grpViewEditCard.TabStop = false;
            this.grpViewEditCard.Text = "View/Edit Card Details";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 386F));
            this.tableLayoutPanel2.Controls.Add(this.lblEmployeeCard, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblUserName, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.rbtnActive, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.rbtnInActive, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmbEmployeeName, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtEmployeeCard, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblStatus, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(572, 85);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblEmployeeCard
            // 
            this.lblEmployeeCard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEmployeeCard.AutoSize = true;
            this.lblEmployeeCard.Location = new System.Drawing.Point(3, 8);
            this.lblEmployeeCard.Name = "lblEmployeeCard";
            this.lblEmployeeCard.Size = new System.Drawing.Size(81, 13);
            this.lblEmployeeCard.TabIndex = 0;
            this.lblEmployeeCard.Text = "Employee Card:";
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(3, 38);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(63, 13);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "User Name:";
            // 
            // rbtnActive
            // 
            this.rbtnActive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtnActive.AutoSize = true;
            this.rbtnActive.Location = new System.Drawing.Point(110, 66);
            this.rbtnActive.Name = "rbtnActive";
            this.rbtnActive.Size = new System.Drawing.Size(55, 17);
            this.rbtnActive.TabIndex = 4;
            this.rbtnActive.TabStop = true;
            this.rbtnActive.Text = "Active";
            this.rbtnActive.UseVisualStyleBackColor = true;
            // 
            // rbtnInActive
            // 
            this.rbtnInActive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbtnInActive.AutoSize = true;
            this.rbtnInActive.Location = new System.Drawing.Point(189, 66);
            this.rbtnInActive.Name = "rbtnInActive";
            this.rbtnInActive.Size = new System.Drawing.Size(64, 17);
            this.rbtnInActive.TabIndex = 5;
            this.rbtnInActive.TabStop = true;
            this.rbtnInActive.Text = "InActive";
            this.rbtnInActive.UseVisualStyleBackColor = true;
            // 
            // cmbEmployeeName
            // 
            this.cmbEmployeeName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel2.SetColumnSpan(this.cmbEmployeeName, 2);
            this.cmbEmployeeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployeeName.FormattingEnabled = true;
            this.cmbEmployeeName.Location = new System.Drawing.Point(110, 34);
            this.cmbEmployeeName.Name = "cmbEmployeeName";
            this.cmbEmployeeName.Size = new System.Drawing.Size(200, 21);
            this.cmbEmployeeName.TabIndex = 1;
            this.cmbEmployeeName.SelectedIndexChanged += new System.EventHandler(this.cmbEmployeeName_SelectedIndexChanged);
            // 
            // txtEmployeeCard
            // 
            this.txtEmployeeCard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel2.SetColumnSpan(this.txtEmployeeCard, 2);
            this.txtEmployeeCard.Location = new System.Drawing.Point(110, 5);
            this.txtEmployeeCard.Name = "txtEmployeeCard";
            this.txtEmployeeCard.ReadOnly = true;
            this.txtEmployeeCard.Size = new System.Drawing.Size(200, 20);
            this.txtEmployeeCard.TabIndex = 6;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(3, 68);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status:";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDelete.Location = new System.Drawing.Point(359, 534);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 25);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            // 
            // btnSignOff
            // 
            this.btnSignOff.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSignOff.Location = new System.Drawing.Point(251, 534);
            this.btnSignOff.Name = "btnSignOff";
            this.btnSignOff.Size = new System.Drawing.Size(100, 25);
            this.btnSignOff.TabIndex = 7;
            this.btnSignOff.Text = "SignOff";
            this.btnSignOff.UseVisualStyleBackColor = true;
            this.btnSignOff.Visible = false;
            this.btnSignOff.Click += new System.EventHandler(this.btnSignOff_Click);
            // 
            // txtSearchEmployeeDetails
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtSearchEmployeeDetails, 3);
            this.txtSearchEmployeeDetails.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchEmployeeDetails.ForeColor = System.Drawing.Color.DimGray;
            this.txtSearchEmployeeDetails.Location = new System.Drawing.Point(203, 113);
            this.txtSearchEmployeeDetails.Name = "txtSearchEmployeeDetails";
            this.txtSearchEmployeeDetails.Size = new System.Drawing.Size(362, 23);
            this.txtSearchEmployeeDetails.TabIndex = 8;
            this.txtSearchEmployeeDetails.Text = "Search Employee Card ";
            this.txtSearchEmployeeDetails.TextChanged += new System.EventHandler(this.txtSearchCard_TextChanged);
            this.txtSearchEmployeeDetails.Enter += new System.EventHandler(this.txtSearchCard_Enter);
            this.txtSearchEmployeeDetails.Leave += new System.EventHandler(this.txtSearchCard_Leave);
            // 
            // lvwEmployeeDetails
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lvwEmployeeDetails, 5);
            this.lvwEmployeeDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwEmployeeDetails.FullRowSelect = true;
            this.lvwEmployeeDetails.HideSelection = false;
            this.lvwEmployeeDetails.Location = new System.Drawing.Point(203, 140);
            this.lvwEmployeeDetails.Name = "lvwEmployeeDetails";
            this.lvwEmployeeDetails.Size = new System.Drawing.Size(578, 388);
            this.lvwEmployeeDetails.TabIndex = 9;
            this.lvwEmployeeDetails.UseCompatibleStateImageBehavior = false;
            this.lvwEmployeeDetails.View = System.Windows.Forms.View.Details;
            this.lvwEmployeeDetails.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwEmployeeDetails_ColumnClick);
            // 
            // frmCreateViewEmployeeCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmCreateViewEmployeeCards";
            this.Text = "Create/Edit Employee Card";
            this.Load += new System.EventHandler(this.frmCreateViewEmployeeCards_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grpViewEditCard.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCreateCard;
        private System.Windows.Forms.ListBox lstCardNumber;
        private System.Windows.Forms.TextBox txtSearchCard;
        private System.Windows.Forms.GroupBox grpViewEditCard;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSignOff;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblEmployeeCard;
        private System.Windows.Forms.ComboBox cmbEmployeeName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.RadioButton rbtnActive;
        private System.Windows.Forms.RadioButton rbtnInActive;
        private System.Windows.Forms.TextBox txtEmployeeCard;
        private System.Windows.Forms.TextBox txtSearchEmployeeDetails;
        private System.Windows.Forms.ListView lvwEmployeeDetails;
    }
}