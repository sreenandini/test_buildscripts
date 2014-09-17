namespace BMC.EnterpriseClient.Views
{
    partial class frmMachineModelAdmin
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
            this.gpFirst = new System.Windows.Forms.GroupBox();
            this.cmbManufacturer = new System.Windows.Forms.ComboBox();
            this.cmbModelName = new System.Windows.Forms.ComboBox();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.lblModelName = new System.Windows.Forms.Label();
            this.gpSASMeter = new System.Windows.Forms.GroupBox();
            this.chkSASRecreateTicketsInsertedfromDrop = new System.Windows.Forms.CheckBox();
            this.chkSASUseCancCredAsPrintedTickets = new System.Windows.Forms.CheckBox();
            this.chkSASAddTrueCoinInToDrop = new System.Windows.Forms.CheckBox();
            this.chkSASJackpotAddedToCC = new System.Windows.Forms.CheckBox();
            this.chkSASRecreateCancelledCredits = new System.Windows.Forms.CheckBox();
            this.gpLast = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gpFirst.SuspendLayout();
            this.gpSASMeter.SuspendLayout();
            this.gpLast.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpFirst
            // 
            this.gpFirst.Controls.Add(this.cmbManufacturer);
            this.gpFirst.Controls.Add(this.cmbModelName);
            this.gpFirst.Controls.Add(this.lblManufacturer);
            this.gpFirst.Controls.Add(this.lblModelName);
            this.gpFirst.Location = new System.Drawing.Point(7, 2);
            this.gpFirst.Name = "gpFirst";
            this.gpFirst.Size = new System.Drawing.Size(351, 88);
            this.gpFirst.TabIndex = 0;
            this.gpFirst.TabStop = false;
            // 
            // cmbManufacturer
            // 
            this.cmbManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbManufacturer.FormattingEnabled = true;
            this.cmbManufacturer.Location = new System.Drawing.Point(99, 52);
            this.cmbManufacturer.Name = "cmbManufacturer";
            this.cmbManufacturer.Size = new System.Drawing.Size(243, 21);
            this.cmbManufacturer.TabIndex = 3;
            this.cmbManufacturer.SelectedIndexChanged += new System.EventHandler(this.cmbManufacturer_SelectedIndexChanged);
            // 
            // cmbModelName
            // 
            this.cmbModelName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModelName.FormattingEnabled = true;
            this.cmbModelName.Location = new System.Drawing.Point(99, 17);
            this.cmbModelName.Name = "cmbModelName";
            this.cmbModelName.Size = new System.Drawing.Size(243, 21);
            this.cmbModelName.TabIndex = 1;
            this.cmbModelName.SelectedIndexChanged += new System.EventHandler(this.cmbModelName_SelectedIndexChanged);
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Location = new System.Drawing.Point(7, 55);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(87, 13);
            this.lblManufacturer.TabIndex = 2;
            this.lblManufacturer.Text = "Manufacturer:";
            // 
            // lblModelName
            // 
            this.lblModelName.AutoSize = true;
            this.lblModelName.Location = new System.Drawing.Point(7, 20);
            this.lblModelName.Name = "lblModelName";
            this.lblModelName.Size = new System.Drawing.Size(86, 13);
            this.lblModelName.TabIndex = 0;
            this.lblModelName.Text = "Model Name :";
            // 
            // gpSASMeter
            // 
            this.gpSASMeter.Controls.Add(this.chkSASRecreateTicketsInsertedfromDrop);
            this.gpSASMeter.Controls.Add(this.chkSASUseCancCredAsPrintedTickets);
            this.gpSASMeter.Controls.Add(this.chkSASAddTrueCoinInToDrop);
            this.gpSASMeter.Controls.Add(this.chkSASJackpotAddedToCC);
            this.gpSASMeter.Controls.Add(this.chkSASRecreateCancelledCredits);
            this.gpSASMeter.Location = new System.Drawing.Point(8, 93);
            this.gpSASMeter.Name = "gpSASMeter";
            this.gpSASMeter.Size = new System.Drawing.Size(350, 181);
            this.gpSASMeter.TabIndex = 1;
            this.gpSASMeter.TabStop = false;
            this.gpSASMeter.Text = "SAS Meter Calculations";
            // 
            // chkSASRecreateTicketsInsertedfromDrop
            // 
            this.chkSASRecreateTicketsInsertedfromDrop.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSASRecreateTicketsInsertedfromDrop.Location = new System.Drawing.Point(29, 153);
            this.chkSASRecreateTicketsInsertedfromDrop.Name = "chkSASRecreateTicketsInsertedfromDrop";
            this.chkSASRecreateTicketsInsertedfromDrop.Size = new System.Drawing.Size(280, 17);
            this.chkSASRecreateTicketsInsertedfromDrop.TabIndex = 4;
            this.chkSASRecreateTicketsInsertedfromDrop.Text = "Recreate Vouchers Inserted from Drop?         ";
            this.chkSASRecreateTicketsInsertedfromDrop.UseVisualStyleBackColor = true;
            // 
            // chkSASUseCancCredAsPrintedTickets
            // 
            this.chkSASUseCancCredAsPrintedTickets.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSASUseCancCredAsPrintedTickets.Location = new System.Drawing.Point(29, 121);
            this.chkSASUseCancCredAsPrintedTickets.Name = "chkSASUseCancCredAsPrintedTickets";
            this.chkSASUseCancCredAsPrintedTickets.Size = new System.Drawing.Size(280, 17);
            this.chkSASUseCancCredAsPrintedTickets.TabIndex = 3;
            this.chkSASUseCancCredAsPrintedTickets.Text = "Use Cancelled Credits As Printed Vouchers?    ";
            this.chkSASUseCancCredAsPrintedTickets.UseVisualStyleBackColor = true;
            // 
            // chkSASAddTrueCoinInToDrop
            // 
            this.chkSASAddTrueCoinInToDrop.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSASAddTrueCoinInToDrop.Location = new System.Drawing.Point(29, 89);
            this.chkSASAddTrueCoinInToDrop.Name = "chkSASAddTrueCoinInToDrop";
            this.chkSASAddTrueCoinInToDrop.Size = new System.Drawing.Size(280, 17);
            this.chkSASAddTrueCoinInToDrop.TabIndex = 2;
            this.chkSASAddTrueCoinInToDrop.Text = "Add True Coin In to Coin Drop?                  ";
            this.chkSASAddTrueCoinInToDrop.UseVisualStyleBackColor = true;
            // 
            // chkSASJackpotAddedToCC
            // 
            this.chkSASJackpotAddedToCC.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSASJackpotAddedToCC.Location = new System.Drawing.Point(29, 57);
            this.chkSASJackpotAddedToCC.Name = "chkSASJackpotAddedToCC";
            this.chkSASJackpotAddedToCC.Size = new System.Drawing.Size(280, 17);
            this.chkSASJackpotAddedToCC.TabIndex = 1;
            this.chkSASJackpotAddedToCC.Text = "Jackpot Added to Cancelled Credits?           ";
            this.chkSASJackpotAddedToCC.UseVisualStyleBackColor = true;
            // 
            // chkSASRecreateCancelledCredits
            // 
            this.chkSASRecreateCancelledCredits.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSASRecreateCancelledCredits.Location = new System.Drawing.Point(29, 25);
            this.chkSASRecreateCancelledCredits.Name = "chkSASRecreateCancelledCredits";
            this.chkSASRecreateCancelledCredits.Size = new System.Drawing.Size(280, 17);
            this.chkSASRecreateCancelledCredits.TabIndex = 0;
            this.chkSASRecreateCancelledCredits.Text = "Recreate Cancelled Credits?                       ";
            this.chkSASRecreateCancelledCredits.UseVisualStyleBackColor = true;
            // 
            // gpLast
            // 
            this.gpLast.Controls.Add(this.btnSave);
            this.gpLast.Controls.Add(this.btnClose);
            this.gpLast.Location = new System.Drawing.Point(8, 274);
            this.gpLast.Name = "gpLast";
            this.gpLast.Size = new System.Drawing.Size(350, 54);
            this.gpLast.TabIndex = 2;
            this.gpLast.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(254, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 34);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(13, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 34);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMachineModelAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(366, 332);
            this.Controls.Add(this.gpLast);
            this.Controls.Add(this.gpSASMeter);
            this.Controls.Add(this.gpFirst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMachineModelAdmin";
            this.Text = "Machine Model Administration";
            this.gpFirst.ResumeLayout(false);
            this.gpFirst.PerformLayout();
            this.gpSASMeter.ResumeLayout(false);
            this.gpLast.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpFirst;
        private System.Windows.Forms.ComboBox cmbManufacturer;
        private System.Windows.Forms.ComboBox cmbModelName;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label lblModelName;
        private System.Windows.Forms.GroupBox gpSASMeter;
        private System.Windows.Forms.CheckBox chkSASRecreateCancelledCredits;
        private System.Windows.Forms.GroupBox gpLast;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkSASRecreateTicketsInsertedfromDrop;
        private System.Windows.Forms.CheckBox chkSASUseCancCredAsPrintedTickets;
        private System.Windows.Forms.CheckBox chkSASAddTrueCoinInToDrop;
        private System.Windows.Forms.CheckBox chkSASJackpotAddedToCC;
    }
}