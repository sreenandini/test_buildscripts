namespace BMC.EnterpriseClient.Views
{
    partial class frmVaultCopy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVaultCopy));
            this.tbl_Main = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_VaultName = new System.Windows.Forms.Label();
            this.lbl_OldVault = new System.Windows.Forms.Label();
            this.txt_NewVaultName = new System.Windows.Forms.TextBox();
            this.lbl_NewVaultSerial = new System.Windows.Forms.Label();
            this.lbl_NewVaultName = new System.Windows.Forms.Label();
            this.txt_newVaultSerial = new System.Windows.Forms.TextBox();
            this.lbl_newVault = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.chk_ClearOnSave = new System.Windows.Forms.CheckBox();
            this.tbl_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbl_Main
            // 
            this.tbl_Main.ColumnCount = 3;
            this.tbl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tbl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tbl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tbl_Main.Controls.Add(this.lbl_VaultName, 0, 0);
            this.tbl_Main.Controls.Add(this.lbl_OldVault, 1, 0);
            this.tbl_Main.Controls.Add(this.txt_NewVaultName, 1, 2);
            this.tbl_Main.Controls.Add(this.lbl_NewVaultSerial, 0, 3);
            this.tbl_Main.Controls.Add(this.lbl_NewVaultName, 0, 2);
            this.tbl_Main.Controls.Add(this.txt_newVaultSerial, 1, 3);
            this.tbl_Main.Controls.Add(this.lbl_newVault, 0, 1);
            this.tbl_Main.Controls.Add(this.btn_Close, 2, 4);
            this.tbl_Main.Controls.Add(this.btn_Save, 1, 4);
            this.tbl_Main.Controls.Add(this.chk_ClearOnSave, 0, 4);
            this.tbl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_Main.Location = new System.Drawing.Point(0, 0);
            this.tbl_Main.Name = "tbl_Main";
            this.tbl_Main.RowCount = 5;
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_Main.Size = new System.Drawing.Size(406, 182);
            this.tbl_Main.TabIndex = 0;
            // 
            // lbl_VaultName
            // 
            this.lbl_VaultName.AutoSize = true;
            this.lbl_VaultName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_VaultName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_VaultName.Location = new System.Drawing.Point(3, 0);
            this.lbl_VaultName.Name = "lbl_VaultName";
            this.lbl_VaultName.Size = new System.Drawing.Size(115, 36);
            this.lbl_VaultName.TabIndex = 0;
            this.lbl_VaultName.Text = "Source Vault";
            this.lbl_VaultName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_OldVault
            // 
            this.lbl_OldVault.AutoSize = true;
            this.tbl_Main.SetColumnSpan(this.lbl_OldVault, 2);
            this.lbl_OldVault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_OldVault.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_OldVault.Location = new System.Drawing.Point(124, 0);
            this.lbl_OldVault.Name = "lbl_OldVault";
            this.lbl_OldVault.Size = new System.Drawing.Size(279, 36);
            this.lbl_OldVault.TabIndex = 1;
            this.lbl_OldVault.Text = "Src vault Name";
            this.lbl_OldVault.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_NewVaultName
            // 
            this.tbl_Main.SetColumnSpan(this.txt_NewVaultName, 2);
            this.txt_NewVaultName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_NewVaultName.Location = new System.Drawing.Point(124, 75);
            this.txt_NewVaultName.MaxLength = 20;
            this.txt_NewVaultName.Name = "txt_NewVaultName";
            this.txt_NewVaultName.Size = new System.Drawing.Size(279, 20);
            this.txt_NewVaultName.TabIndex = 4;
            // 
            // lbl_NewVaultSerial
            // 
            this.lbl_NewVaultSerial.AutoSize = true;
            this.lbl_NewVaultSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_NewVaultSerial.Location = new System.Drawing.Point(3, 108);
            this.lbl_NewVaultSerial.Name = "lbl_NewVaultSerial";
            this.lbl_NewVaultSerial.Size = new System.Drawing.Size(115, 36);
            this.lbl_NewVaultSerial.TabIndex = 5;
            this.lbl_NewVaultSerial.Text = "*Serial No:";
            this.lbl_NewVaultSerial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_NewVaultName
            // 
            this.lbl_NewVaultName.AutoSize = true;
            this.lbl_NewVaultName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_NewVaultName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_NewVaultName.Location = new System.Drawing.Point(3, 72);
            this.lbl_NewVaultName.Name = "lbl_NewVaultName";
            this.lbl_NewVaultName.Size = new System.Drawing.Size(115, 36);
            this.lbl_NewVaultName.TabIndex = 3;
            this.lbl_NewVaultName.Text = "*Name:";
            this.lbl_NewVaultName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_newVaultSerial
            // 
            this.tbl_Main.SetColumnSpan(this.txt_newVaultSerial, 2);
            this.txt_newVaultSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_newVaultSerial.Location = new System.Drawing.Point(124, 111);
            this.txt_newVaultSerial.MaxLength = 20;
            this.txt_newVaultSerial.Name = "txt_newVaultSerial";
            this.txt_newVaultSerial.Size = new System.Drawing.Size(279, 20);
            this.txt_newVaultSerial.TabIndex = 6;
            // 
            // lbl_newVault
            // 
            this.lbl_newVault.AutoSize = true;
            this.tbl_Main.SetColumnSpan(this.lbl_newVault, 2);
            this.lbl_newVault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_newVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_newVault.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_newVault.Location = new System.Drawing.Point(3, 36);
            this.lbl_newVault.Name = "lbl_newVault";
            this.lbl_newVault.Size = new System.Drawing.Size(257, 36);
            this.lbl_newVault.TabIndex = 2;
            this.lbl_newVault.Text = "New Vault Details:";
            this.lbl_newVault.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Close.Location = new System.Drawing.Point(328, 147);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 9;
            this.btn_Close.Text = "C&lose";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Location = new System.Drawing.Point(185, 147);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 8;
            this.btn_Save.Text = "&Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // chk_ClearOnSave
            // 
            this.chk_ClearOnSave.AutoSize = true;
            this.chk_ClearOnSave.Checked = true;
            this.chk_ClearOnSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ClearOnSave.Location = new System.Drawing.Point(3, 147);
            this.chk_ClearOnSave.Name = "chk_ClearOnSave";
            this.chk_ClearOnSave.Size = new System.Drawing.Size(91, 17);
            this.chk_ClearOnSave.TabIndex = 7;
            this.chk_ClearOnSave.Text = "Clear on save";
            this.chk_ClearOnSave.UseVisualStyleBackColor = true;
            // 
            // frmVaultCopy
            // 
            this.AcceptButton = this.btn_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Close;
            this.ClientSize = new System.Drawing.Size(406, 182);
            this.Controls.Add(this.tbl_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVaultCopy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy Vault Device";
            this.tbl_Main.ResumeLayout(false);
            this.tbl_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbl_Main;
        private System.Windows.Forms.Label lbl_VaultName;
        private System.Windows.Forms.Label lbl_OldVault;
        private System.Windows.Forms.TextBox txt_NewVaultName;
        private System.Windows.Forms.Label lbl_NewVaultSerial;
        private System.Windows.Forms.Label lbl_NewVaultName;
        private System.Windows.Forms.TextBox txt_newVaultSerial;
        private System.Windows.Forms.Label lbl_newVault;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.CheckBox chk_ClearOnSave;
    }
}