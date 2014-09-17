namespace BMC.EnterpriseClient.Views
{
    partial class frmPayTableMaster
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
            this.grpPayTable = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtTheoPayout = new System.Windows.Forms.TextBox();
            this.txtMaxbet = new System.Windows.Forms.TextBox();
            this.txtPaytablePayout = new System.Windows.Forms.TextBox();
            this.txtPayTableID = new System.Windows.Forms.TextBox();
            this.lblTheoPercentagePayout = new System.Windows.Forms.Label();
            this.lblPTBet = new System.Windows.Forms.Label();
            this.lblPTPercentagePayout = new System.Windows.Forms.Label();
            this.lblPTId = new System.Windows.Forms.Label();
            this.grpPayTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPayTable
            // 
            this.grpPayTable.Controls.Add(this.btnCancel);
            this.grpPayTable.Controls.Add(this.btnUpdate);
            this.grpPayTable.Controls.Add(this.txtTheoPayout);
            this.grpPayTable.Controls.Add(this.txtMaxbet);
            this.grpPayTable.Controls.Add(this.txtPaytablePayout);
            this.grpPayTable.Controls.Add(this.txtPayTableID);
            this.grpPayTable.Controls.Add(this.lblTheoPercentagePayout);
            this.grpPayTable.Controls.Add(this.lblPTBet);
            this.grpPayTable.Controls.Add(this.lblPTPercentagePayout);
            this.grpPayTable.Controls.Add(this.lblPTId);
            this.grpPayTable.Location = new System.Drawing.Point(7, 0);
            this.grpPayTable.Name = "grpPayTable";
            this.grpPayTable.Size = new System.Drawing.Size(413, 240);
            this.grpPayTable.TabIndex = 0;
            this.grpPayTable.TabStop = false;
            this.grpPayTable.Text = "Pay Table";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(266, 197);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 31);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(46, 197);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(103, 31);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Save && Close";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtTheoPayout
            // 
            this.txtTheoPayout.Location = new System.Drawing.Point(170, 151);
            this.txtTheoPayout.Name = "txtTheoPayout";
            this.txtTheoPayout.Size = new System.Drawing.Size(230, 21);
            this.txtTheoPayout.TabIndex = 7;
            this.txtTheoPayout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTheoPayout_KeyPress);
            // 
            // txtMaxbet
            // 
            this.txtMaxbet.Enabled = false;
            this.txtMaxbet.Location = new System.Drawing.Point(170, 111);
            this.txtMaxbet.Name = "txtMaxbet";
            this.txtMaxbet.Size = new System.Drawing.Size(230, 21);
            this.txtMaxbet.TabIndex = 5;
            // 
            // txtPaytablePayout
            // 
            this.txtPaytablePayout.Enabled = false;
            this.txtPaytablePayout.Location = new System.Drawing.Point(170, 70);
            this.txtPaytablePayout.Name = "txtPaytablePayout";
            this.txtPaytablePayout.Size = new System.Drawing.Size(230, 21);
            this.txtPaytablePayout.TabIndex = 3;
            // 
            // txtPayTableID
            // 
            this.txtPayTableID.Enabled = false;
            this.txtPayTableID.Location = new System.Drawing.Point(170, 29);
            this.txtPayTableID.Name = "txtPayTableID";
            this.txtPayTableID.Size = new System.Drawing.Size(230, 21);
            this.txtPayTableID.TabIndex = 1;
            // 
            // lblTheoPercentagePayout
            // 
            this.lblTheoPercentagePayout.AutoSize = true;
            this.lblTheoPercentagePayout.Location = new System.Drawing.Point(6, 151);
            this.lblTheoPercentagePayout.Name = "lblTheoPercentagePayout";
            this.lblTheoPercentagePayout.Size = new System.Drawing.Size(94, 13);
            this.lblTheoPercentagePayout.TabIndex = 6;
            this.lblTheoPercentagePayout.Text = "Theo % Payout";
            // 
            // lblPTBet
            // 
            this.lblPTBet.AutoSize = true;
            this.lblPTBet.Location = new System.Drawing.Point(6, 111);
            this.lblPTBet.Name = "lblPTBet";
            this.lblPTBet.Size = new System.Drawing.Size(141, 13);
            this.lblPTBet.TabIndex = 4;
            this.lblPTBet.Text = "Pay Table Bet (Credits)";
            // 
            // lblPTPercentagePayout
            // 
            this.lblPTPercentagePayout.AutoSize = true;
            this.lblPTPercentagePayout.Location = new System.Drawing.Point(6, 70);
            this.lblPTPercentagePayout.Name = "lblPTPercentagePayout";
            this.lblPTPercentagePayout.Size = new System.Drawing.Size(122, 13);
            this.lblPTPercentagePayout.TabIndex = 2;
            this.lblPTPercentagePayout.Text = "Pay Table % Payout";
            // 
            // lblPTId
            // 
            this.lblPTId.AutoSize = true;
            this.lblPTId.Location = new System.Drawing.Point(6, 32);
            this.lblPTId.Name = "lblPTId";
            this.lblPTId.Size = new System.Drawing.Size(77, 13);
            this.lblPTId.TabIndex = 0;
            this.lblPTId.Text = "PayTable ID";
            // 
            // frmPayTableMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 247);
            this.Controls.Add(this.grpPayTable);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPayTableMaster";
            this.Text = "Pay Table Administration";
            this.grpPayTable.ResumeLayout(false);
            this.grpPayTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPayTable;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtTheoPayout;
        private System.Windows.Forms.TextBox txtMaxbet;
        private System.Windows.Forms.TextBox txtPaytablePayout;
        private System.Windows.Forms.TextBox txtPayTableID;
        private System.Windows.Forms.Label lblTheoPercentagePayout;
        private System.Windows.Forms.Label lblPTBet;
        private System.Windows.Forms.Label lblPTPercentagePayout;
        private System.Windows.Forms.Label lblPTId;
        private System.Windows.Forms.Button btnCancel;
    }
}