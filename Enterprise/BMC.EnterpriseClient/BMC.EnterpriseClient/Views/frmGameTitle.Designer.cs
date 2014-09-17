namespace BMC.EnterpriseClient.Views
{
    partial class frmGameTitle
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
            this.grpGameTitle = new System.Windows.Forms.GroupBox();
            this.chkMultiGame = new System.Windows.Forms.CheckBox();
            this.txtGameTitle = new System.Windows.Forms.TextBox();
            this.cmb_ManufacturerFilter = new System.Windows.Forms.ComboBox();
            this.cmb_GameCategoryFilter = new System.Windows.Forms.ComboBox();
            this.lblGameManufactrer = new System.Windows.Forms.Label();
            this.lblGameTitle = new System.Windows.Forms.Label();
            this.lblGameCategory = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkMultiGame = new System.Windows.Forms.CheckBox();
            this.grpGameTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGameTitle
            // 
            this.grpGameTitle.Controls.Add(this.chkMultiGame);

            this.grpGameTitle.Controls.Add(this.txtGameTitle);
            this.grpGameTitle.Controls.Add(this.cmb_ManufacturerFilter);
            this.grpGameTitle.Controls.Add(this.cmb_GameCategoryFilter);
            this.grpGameTitle.Controls.Add(this.lblGameManufactrer);
            this.grpGameTitle.Controls.Add(this.lblGameTitle);
            this.grpGameTitle.Controls.Add(this.lblGameCategory);
            this.grpGameTitle.Location = new System.Drawing.Point(24, 14);
            this.grpGameTitle.Name = "grpGameTitle";
            this.grpGameTitle.Size = new System.Drawing.Size(341, 166);
            this.grpGameTitle.TabIndex = 0;
            this.grpGameTitle.TabStop = false;
            // chkMultiGame
            // 
            this.chkMultiGame.AutoSize = true;
            this.chkMultiGame.Location = new System.Drawing.Point(14, 139);
            this.chkMultiGame.Name = "chkMultiGame";
            this.chkMultiGame.Size = new System.Drawing.Size(86, 17);
            this.chkMultiGame.TabIndex = 6;
            this.chkMultiGame.Text = "MultiGame";
            this.chkMultiGame.UseVisualStyleBackColor = true;
            // 
            // 
            // txtGameTitle
            // 
            this.txtGameTitle.Location = new System.Drawing.Point(145, 61);
            this.txtGameTitle.MaxLength = 50;
            this.txtGameTitle.Name = "txtGameTitle";
            this.txtGameTitle.Size = new System.Drawing.Size(176, 21);
            this.txtGameTitle.TabIndex = 3;
            // 
            // cmb_ManufacturerFilter
            // 
            this.cmb_ManufacturerFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ManufacturerFilter.FormattingEnabled = true;
            this.cmb_ManufacturerFilter.Location = new System.Drawing.Point(145, 100);
            this.cmb_ManufacturerFilter.Name = "cmb_ManufacturerFilter";
            this.cmb_ManufacturerFilter.Size = new System.Drawing.Size(176, 21);
            this.cmb_ManufacturerFilter.TabIndex = 5;
            // 
            // cmb_GameCategoryFilter
            // 
            this.cmb_GameCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GameCategoryFilter.FormattingEnabled = true;
            this.cmb_GameCategoryFilter.Location = new System.Drawing.Point(145, 25);
            this.cmb_GameCategoryFilter.Name = "cmb_GameCategoryFilter";
            this.cmb_GameCategoryFilter.Size = new System.Drawing.Size(176, 21);
            this.cmb_GameCategoryFilter.TabIndex = 1;
            // 
            // lblGameManufactrer
            // 
            this.lblGameManufactrer.AutoSize = true;
            this.lblGameManufactrer.Location = new System.Drawing.Point(14, 102);
            this.lblGameManufactrer.Name = "lblGameManufactrer";
            this.lblGameManufactrer.Size = new System.Drawing.Size(125, 13);
            this.lblGameManufactrer.TabIndex = 4;
            this.lblGameManufactrer.Text = "Game Manufacturer:";
            // 
            // lblGameTitle
            // 
            this.lblGameTitle.AutoSize = true;
            this.lblGameTitle.Location = new System.Drawing.Point(14, 61);
            this.lblGameTitle.Name = "lblGameTitle";
            this.lblGameTitle.Size = new System.Drawing.Size(74, 13);
            this.lblGameTitle.TabIndex = 2;
            this.lblGameTitle.Text = "Game Title:";
            // 
            // lblGameCategory
            // 
            this.lblGameCategory.AutoSize = true;
            this.lblGameCategory.Location = new System.Drawing.Point(14, 26);
            this.lblGameCategory.Name = "lblGameCategory";
            this.lblGameCategory.Size = new System.Drawing.Size(103, 13);
            this.lblGameCategory.TabIndex = 0;
            this.lblGameCategory.Text = "Game Category:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(102, 198);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(214, 198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkMultiGame
            // 
            this.chkMultiGame.AutoSize = true;
            this.chkMultiGame.Location = new System.Drawing.Point(14, 146);
            this.chkMultiGame.Name = "chkMultiGame";
            this.chkMultiGame.Size = new System.Drawing.Size(86, 17);
            this.chkMultiGame.TabIndex = 6;
            this.chkMultiGame.Text = "MultiGame";
            this.chkMultiGame.UseVisualStyleBackColor = true;
            // 
            // frmGameTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 241);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpGameTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGameTitle";
            this.Text = "Game Title";
            this.Load += new System.EventHandler(this.frmGameTitle_Load);
            this.grpGameTitle.ResumeLayout(false);
            this.grpGameTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpGameTitle;
        private System.Windows.Forms.ComboBox cmb_GameCategoryFilter;
        private System.Windows.Forms.Label lblGameManufactrer;
        private System.Windows.Forms.Label lblGameTitle;
        private System.Windows.Forms.Label lblGameCategory;
        private System.Windows.Forms.ComboBox cmb_ManufacturerFilter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtGameTitle;
        private System.Windows.Forms.CheckBox chkMultiGame;
    }
}