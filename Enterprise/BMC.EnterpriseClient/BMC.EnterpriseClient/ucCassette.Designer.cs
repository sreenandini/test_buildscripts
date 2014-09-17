
namespace BMC.EnterpriseClient.Views
{
    partial class ucCassette
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
            this.pnlBase = new System.Windows.Forms.Panel();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.txtDropAmount = new System.Windows.Forms.Label();
            this.lblCassetteName = new System.Windows.Forms.Label();
            this.lblDenom = new System.Windows.Forms.Label();
            this.chkIsSelected = new System.Windows.Forms.CheckBox();
            this.txtQuantity = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.txtAmount = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.pnlBase.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBase
            // 
            this.pnlBase.Controls.Add(this.tblMain);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBase.Location = new System.Drawing.Point(0, 0);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(578, 30);
            this.pnlBase.TabIndex = 0;
            // 
            // tblMain
            // 
            this.tblMain.AutoSize = true;
            this.tblMain.ColumnCount = 6;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.41176F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.76471F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.60784F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.60784F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.60784F));
            this.tblMain.Controls.Add(this.txtDropAmount, 5, 0);
            this.tblMain.Controls.Add(this.txtQuantity, 3, 0);
            this.tblMain.Controls.Add(this.lblCassetteName, 1, 0);
            this.tblMain.Controls.Add(this.lblDenom, 2, 0);
            this.tblMain.Controls.Add(this.chkIsSelected, 0, 0);
            this.tblMain.Controls.Add(this.txtAmount, 4, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 1;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Size = new System.Drawing.Size(578, 30);
            this.tblMain.TabIndex = 0;
            // 
            // txtDropAmount
            // 
            this.txtDropAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.txtDropAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDropAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDropAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtDropAmount.Location = new System.Drawing.Point(470, 0);
            this.txtDropAmount.Name = "txtDropAmount";
            this.txtDropAmount.Size = new System.Drawing.Size(105, 30);
            this.txtDropAmount.TabIndex = 5;
            this.txtDropAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCassetteName
            // 
            this.lblCassetteName.AutoSize = true;
            this.lblCassetteName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCassetteName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCassetteName.Location = new System.Drawing.Point(23, 0);
            this.lblCassetteName.Name = "lblCassetteName";
            this.lblCassetteName.Size = new System.Drawing.Size(158, 30);
            this.lblCassetteName.TabIndex = 1;
            this.lblCassetteName.Text = "Cassette Name";
            this.lblCassetteName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDenom
            // 
            this.lblDenom.AutoSize = true;
            this.lblDenom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDenom.Location = new System.Drawing.Point(187, 0);
            this.lblDenom.Name = "lblDenom";
            this.lblDenom.Size = new System.Drawing.Size(59, 30);
            this.lblDenom.TabIndex = 2;
            this.lblDenom.Text = "Denom";
            this.lblDenom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkIsSelected
            // 
            this.chkIsSelected.AutoSize = true;
            this.chkIsSelected.Checked = true;
            this.chkIsSelected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsSelected.Location = new System.Drawing.Point(3, 3);
            this.chkIsSelected.Name = "chkIsSelected";
            this.chkIsSelected.Size = new System.Drawing.Size(14, 14);
            this.chkIsSelected.TabIndex = 0;
            this.chkIsSelected.UseVisualStyleBackColor = true;
            // 
            // txtQuantity
            // 
            this.txtQuantity.AllowDecimal = false;
            this.txtQuantity.AllowNegative = false;
            this.txtQuantity.BackColor = System.Drawing.Color.White;
            this.txtQuantity.DecimalLength = 2;
            this.txtQuantity.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtQuantity.Location = new System.Drawing.Point(252, 3);
            this.txtQuantity.MaxLength = 8;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.ShortcutsEnabled = false;
            this.txtQuantity.Size = new System.Drawing.Size(103, 20);
            this.txtQuantity.TabIndex = 3;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            this.txtQuantity.Enter += new System.EventHandler(this.txtAmount_Enter);
            // 
            // txtAmount
            // 
            this.txtAmount.AllowDecimal = false;
            this.txtAmount.AllowNegative = false;
            this.txtAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.DecimalLength = 0;
            this.txtAmount.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtAmount.Location = new System.Drawing.Point(361, 3);
            this.txtAmount.MaxLength = 8;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.ShortcutsEnabled = false;
            this.txtAmount.Size = new System.Drawing.Size(103, 20);
            this.txtAmount.TabIndex = 4;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.Enter += new System.EventHandler(this.txtAmount_Enter);
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // ucCassette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBase);
            this.Name = "ucCassette";
            this.Size = new System.Drawing.Size(578, 30);
            this.pnlBase.ResumeLayout(false);
            this.pnlBase.PerformLayout();
            this.tblMain.ResumeLayout(false);
            this.tblMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Label lblCassetteName;
        private System.Windows.Forms.Label lblDenom;
        private NumberTextBox txtQuantity;
        private System.Windows.Forms.CheckBox chkIsSelected;
        private System.Windows.Forms.Label txtDropAmount;
        private NumberTextBox txtAmount;
    }
}
