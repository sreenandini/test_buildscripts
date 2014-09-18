namespace BMC.CashDispenser.UI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnGet = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.chdrSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chdrCassetteAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chdrDenomination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chdrTotalValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblContainer.SuspendLayout();
            this.tblButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tblButtons, 0, 1);
            this.tblContainer.Controls.Add(this.dgvItems, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.Size = new System.Drawing.Size(594, 268);
            this.tblContainer.TabIndex = 0;
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 4;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.Controls.Add(this.btnGet, 0, 0);
            this.tblButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblButtons.Controls.Add(this.btnClose, 3, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtons.Location = new System.Drawing.Point(0, 233);
            this.tblButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(594, 35);
            this.tblButtons.TabIndex = 0;
            // 
            // btnGet
            // 
            this.btnGet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGet.ImageKey = "Download1.ico";
            this.btnGet.ImageList = this.imglstSmallIcons;
            this.btnGet.Location = new System.Drawing.Point(3, 3);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(114, 29);
            this.btnGet.TabIndex = 0;
            this.btnGet.Text = "&Get...";
            this.btnGet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Save.ico");
            this.imglstSmallIcons.Images.SetKeyName(1, "Clear.ico");
            this.imglstSmallIcons.Images.SetKeyName(2, "Download1.ico");
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.ImageKey = "Save.ico";
            this.btnSave.ImageList = this.imglstSmallIcons;
            this.btnSave.Location = new System.Drawing.Point(357, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(114, 29);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.ImageKey = "Clear.ico";
            this.btnClose.ImageList = this.imglstSmallIcons;
            this.btnClose.Location = new System.Drawing.Point(477, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 29);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeColumns = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chdrSNo,
            this.chdrCassetteAlias,
            this.chdrDenomination,
            this.chdrTotalValue});
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(3, 3);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvItems.Size = new System.Drawing.Size(588, 227);
            this.dgvItems.TabIndex = 1;
            // 
            // chdrSNo
            // 
            this.chdrSNo.Frozen = true;
            this.chdrSNo.HeaderText = "S.No";
            this.chdrSNo.Name = "chdrSNo";
            this.chdrSNo.ReadOnly = true;
            // 
            // chdrCassetteAlias
            // 
            this.chdrCassetteAlias.HeaderText = "Cassette Alias";
            this.chdrCassetteAlias.Name = "chdrCassetteAlias";
            // 
            // chdrDenomination
            // 
            this.chdrDenomination.HeaderText = "Denomination";
            this.chdrDenomination.Name = "chdrDenomination";
            // 
            // chdrTotalValue
            // 
            this.chdrTotalValue.HeaderText = "Total Value";
            this.chdrTotalValue.Name = "chdrTotalValue";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 268);
            this.Controls.Add(this.tblContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cash Dispenser Configuration";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn chdrSNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn chdrCassetteAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn chdrDenomination;
        private System.Windows.Forms.DataGridViewTextBoxColumn chdrTotalValue;
    }
}

