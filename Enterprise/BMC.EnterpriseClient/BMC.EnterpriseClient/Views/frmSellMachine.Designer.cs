namespace BMC.EnterpriseClient.Views
{
    partial class SellMachineForm
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
            this.grp_SaleDetails = new System.Windows.Forms.GroupBox();
            this.dtSaleDate = new System.Windows.Forms.DateTimePicker();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtSaleInvoiceNumber = new System.Windows.Forms.TextBox();
            this.txtSaleType = new System.Windows.Forms.TextBox();
            this.txtSoldTo = new System.Windows.Forms.TextBox();
            this.lbl_Value = new System.Windows.Forms.Label();
            this.lbl_Date = new System.Windows.Forms.Label();
            this.lbl_SoldTo = new System.Windows.Forms.Label();
            this.lbl_InvoiceNo = new System.Windows.Forms.Label();
            this.lbl_SaleType = new System.Windows.Forms.Label();
            this.grp_MachineDetails = new System.Windows.Forms.GroupBox();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.txtAltSerialNo = new System.Windows.Forms.TextBox();
            this.txtAssetNo = new System.Windows.Forms.TextBox();
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_AssetNo = new System.Windows.Forms.Label();
            this.lbl_Model = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.grp_SaleDetails.SuspendLayout();
            this.grp_MachineDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.grp_MachineDetails);
            this.pnlContainer.Controls.Add(this.grp_SaleDetails);
            this.pnlContainer.Size = new System.Drawing.Size(348, 280);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Location = new System.Drawing.Point(0, 280);
            this.pnlButtons.Size = new System.Drawing.Size(348, 41);
            // 
            // grp_SaleDetails
            // 
            this.grp_SaleDetails.Controls.Add(this.dtSaleDate);
            this.grp_SaleDetails.Controls.Add(this.txtValue);
            this.grp_SaleDetails.Controls.Add(this.txtSaleInvoiceNumber);
            this.grp_SaleDetails.Controls.Add(this.txtSaleType);
            this.grp_SaleDetails.Controls.Add(this.txtSoldTo);
            this.grp_SaleDetails.Controls.Add(this.lbl_Value);
            this.grp_SaleDetails.Controls.Add(this.lbl_Date);
            this.grp_SaleDetails.Controls.Add(this.lbl_SoldTo);
            this.grp_SaleDetails.Controls.Add(this.lbl_InvoiceNo);
            this.grp_SaleDetails.Controls.Add(this.lbl_SaleType);
            this.grp_SaleDetails.Location = new System.Drawing.Point(4, 129);
            this.grp_SaleDetails.Name = "grp_SaleDetails";
            this.grp_SaleDetails.Size = new System.Drawing.Size(340, 141);
            this.grp_SaleDetails.TabIndex = 1;
            this.grp_SaleDetails.TabStop = false;
            this.grp_SaleDetails.Text = "SaleDetails";
            // 
            // dtSaleDate
            // 
            this.dtSaleDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtSaleDate.Location = new System.Drawing.Point(60, 15);
            this.dtSaleDate.Name = "dtSaleDate";
            this.dtSaleDate.Size = new System.Drawing.Size(121, 20);
            this.dtSaleDate.TabIndex = 1;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(59, 113);
            this.txtValue.MaxLength = 8;
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(122, 20);
            this.txtValue.TabIndex = 9;
            // 
            // txtSaleInvoiceNumber
            // 
            this.txtSaleInvoiceNumber.Location = new System.Drawing.Point(60, 89);
            this.txtSaleInvoiceNumber.MaxLength = 50;
            this.txtSaleInvoiceNumber.Name = "txtSaleInvoiceNumber";
            this.txtSaleInvoiceNumber.Size = new System.Drawing.Size(274, 20);
            this.txtSaleInvoiceNumber.TabIndex = 7;
            // 
            // txtSaleType
            // 
            this.txtSaleType.Location = new System.Drawing.Point(60, 65);
            this.txtSaleType.MaxLength = 150;
            this.txtSaleType.Name = "txtSaleType";
            this.txtSaleType.Size = new System.Drawing.Size(274, 20);
            this.txtSaleType.TabIndex = 5;
            // 
            // txtSoldTo
            // 
            this.txtSoldTo.Location = new System.Drawing.Point(60, 41);
            this.txtSoldTo.MaxLength = 50;
            this.txtSoldTo.Name = "txtSoldTo";
            this.txtSoldTo.Size = new System.Drawing.Size(274, 20);
            this.txtSoldTo.TabIndex = 3;
            // 
            // lbl_Value
            // 
            this.lbl_Value.AutoSize = true;
            this.lbl_Value.Location = new System.Drawing.Point(3, 116);
            this.lbl_Value.Name = "lbl_Value";
            this.lbl_Value.Size = new System.Drawing.Size(37, 13);
            this.lbl_Value.TabIndex = 8;
            this.lbl_Value.Text = "Value:";
            // 
            // lbl_Date
            // 
            this.lbl_Date.AutoSize = true;
            this.lbl_Date.Location = new System.Drawing.Point(4, 19);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(33, 13);
            this.lbl_Date.TabIndex = 0;
            this.lbl_Date.Text = "Date:";
            // 
            // lbl_SoldTo
            // 
            this.lbl_SoldTo.AutoSize = true;
            this.lbl_SoldTo.Location = new System.Drawing.Point(3, 44);
            this.lbl_SoldTo.Name = "lbl_SoldTo";
            this.lbl_SoldTo.Size = new System.Drawing.Size(44, 13);
            this.lbl_SoldTo.TabIndex = 2;
            this.lbl_SoldTo.Text = "SoldTo:";
            // 
            // lbl_InvoiceNo
            // 
            this.lbl_InvoiceNo.AutoSize = true;
            this.lbl_InvoiceNo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_InvoiceNo.Location = new System.Drawing.Point(3, 91);
            this.lbl_InvoiceNo.Name = "lbl_InvoiceNo";
            this.lbl_InvoiceNo.Size = new System.Drawing.Size(62, 13);
            this.lbl_InvoiceNo.TabIndex = 6;
            this.lbl_InvoiceNo.Text = "Invoice No:";
            // 
            // lbl_SaleType
            // 
            this.lbl_SaleType.AutoSize = true;
            this.lbl_SaleType.BackColor = System.Drawing.Color.Transparent;
            this.lbl_SaleType.Location = new System.Drawing.Point(3, 68);
            this.lbl_SaleType.Name = "lbl_SaleType";
            this.lbl_SaleType.Size = new System.Drawing.Size(58, 13);
            this.lbl_SaleType.TabIndex = 4;
            this.lbl_SaleType.Text = "Sold Type:";
            // 
            // grp_MachineDetails
            // 
            this.grp_MachineDetails.Controls.Add(this.txtSerialNo);
            this.grp_MachineDetails.Controls.Add(this.txtAltSerialNo);
            this.grp_MachineDetails.Controls.Add(this.txtAssetNo);
            this.grp_MachineDetails.Controls.Add(this.txtModelName);
            this.grp_MachineDetails.Controls.Add(this.label4);
            this.grp_MachineDetails.Controls.Add(this.label3);
            this.grp_MachineDetails.Controls.Add(this.lbl_AssetNo);
            this.grp_MachineDetails.Controls.Add(this.lbl_Model);
            this.grp_MachineDetails.Location = new System.Drawing.Point(4, 11);
            this.grp_MachineDetails.Name = "grp_MachineDetails";
            this.grp_MachineDetails.Size = new System.Drawing.Size(340, 118);
            this.grp_MachineDetails.TabIndex = 0;
            this.grp_MachineDetails.TabStop = false;
            this.grp_MachineDetails.Text = "MachineDetails";
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Location = new System.Drawing.Point(60, 67);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.ReadOnly = true;
            this.txtSerialNo.Size = new System.Drawing.Size(274, 20);
            this.txtSerialNo.TabIndex = 5;
            // 
            // txtAltSerialNo
            // 
            this.txtAltSerialNo.Location = new System.Drawing.Point(60, 90);
            this.txtAltSerialNo.Name = "txtAltSerialNo";
            this.txtAltSerialNo.ReadOnly = true;
            this.txtAltSerialNo.Size = new System.Drawing.Size(274, 20);
            this.txtAltSerialNo.TabIndex = 7;
            // 
            // txtAssetNo
            // 
            this.txtAssetNo.Location = new System.Drawing.Point(60, 44);
            this.txtAssetNo.Name = "txtAssetNo";
            this.txtAssetNo.ReadOnly = true;
            this.txtAssetNo.Size = new System.Drawing.Size(274, 20);
            this.txtAssetNo.TabIndex = 3;
            // 
            // txtModelName
            // 
            this.txtModelName.Location = new System.Drawing.Point(60, 21);
            this.txtModelName.Name = "txtModelName";
            this.txtModelName.ReadOnly = true;
            this.txtModelName.Size = new System.Drawing.Size(274, 20);
            this.txtModelName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Alt Serial:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Serial No:";
            // 
            // lbl_AssetNo
            // 
            this.lbl_AssetNo.AutoSize = true;
            this.lbl_AssetNo.Location = new System.Drawing.Point(4, 47);
            this.lbl_AssetNo.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_AssetNo.Name = "lbl_AssetNo";
            this.lbl_AssetNo.Size = new System.Drawing.Size(50, 13);
            this.lbl_AssetNo.TabIndex = 2;
            this.lbl_AssetNo.Text = "AssetNo:";
            // 
            // lbl_Model
            // 
            this.lbl_Model.AutoSize = true;
            this.lbl_Model.Location = new System.Drawing.Point(4, 24);
            this.lbl_Model.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Model.Name = "lbl_Model";
            this.lbl_Model.Size = new System.Drawing.Size(39, 13);
            this.lbl_Model.TabIndex = 0;
            this.lbl_Model.Text = "Model:";
            // 
            // SellMachineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 321);
            this.Name = "SellMachineForm";
            this.Text = "Sell Machine";
            this.Load += new System.EventHandler(this.SellMachineForm_Load);
            this.pnlContainer.ResumeLayout(false);
            this.grp_SaleDetails.ResumeLayout(false);
            this.grp_SaleDetails.PerformLayout();
            this.grp_MachineDetails.ResumeLayout(false);
            this.grp_MachineDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_SaleDetails;
        private System.Windows.Forms.Label lbl_Value;
        private System.Windows.Forms.Label lbl_Date;
        private System.Windows.Forms.Label lbl_SoldTo;
        private System.Windows.Forms.Label lbl_SaleType;
        private System.Windows.Forms.Label lbl_InvoiceNo;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtSaleInvoiceNumber;
        private System.Windows.Forms.TextBox txtSaleType;
        private System.Windows.Forms.TextBox txtSoldTo;
        private System.Windows.Forms.GroupBox grp_MachineDetails;
        private System.Windows.Forms.TextBox txtSerialNo;
        private System.Windows.Forms.TextBox txtAltSerialNo;
        private System.Windows.Forms.TextBox txtAssetNo;
        private System.Windows.Forms.TextBox txtModelName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_AssetNo;
        private System.Windows.Forms.Label lbl_Model;
        private System.Windows.Forms.DateTimePicker dtSaleDate;
    }
}