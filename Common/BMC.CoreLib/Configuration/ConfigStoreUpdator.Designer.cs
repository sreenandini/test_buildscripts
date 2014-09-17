namespace BMC.CoreLib.Configuration
{
    partial class ConfigStoreUpdator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigStoreUpdator));
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.propData = new System.Windows.Forms.PropertyGrid();
            this.lvwProcesses = new System.Windows.Forms.ListView();
            this.chdrSNo = new System.Windows.Forms.ColumnHeader();
            this.chdrName = new System.Windows.Forms.ColumnHeader();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tblOptions = new System.Windows.Forms.TableLayoutPanel();
            this.lblImplementations = new System.Windows.Forms.Label();
            this.cboImplementations = new System.Windows.Forms.ComboBox();
            this.tblContainer.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.tblOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Save.ico");
            this.imglstSmallIcons.Images.SetKeyName(1, "Cancel.ico");
            // 
            // propData
            // 
            this.propData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propData.HelpVisible = false;
            this.propData.Location = new System.Drawing.Point(13, 269);
            this.propData.Name = "propData";
            this.propData.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propData.Size = new System.Drawing.Size(766, 325);
            this.propData.TabIndex = 2;
            this.propData.ToolbarVisible = false;
            // 
            // lvwProcesses
            // 
            this.lvwProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chdrSNo,
            this.chdrName});
            this.lvwProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwProcesses.FullRowSelect = true;
            this.lvwProcesses.HideSelection = false;
            this.lvwProcesses.Location = new System.Drawing.Point(13, 13);
            this.lvwProcesses.Name = "lvwProcesses";
            this.lvwProcesses.Size = new System.Drawing.Size(766, 215);
            this.lvwProcesses.TabIndex = 0;
            this.lvwProcesses.UseCompatibleStateImageBehavior = false;
            this.lvwProcesses.View = System.Windows.Forms.View.Details;
            this.lvwProcesses.SelectedIndexChanged += new System.EventHandler(this.lvwProcesses_SelectedIndexChanged);
            this.lvwProcesses.DoubleClick += new System.EventHandler(this.lvwProcesses_DoubleClick);
            // 
            // chdrSNo
            // 
            this.chdrSNo.Text = "S.No.";
            this.chdrSNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chdrName
            // 
            this.chdrName.Text = "Process";
            this.chdrName.Width = 548;
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 3;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContainer.Controls.Add(this.lvwProcesses, 1, 1);
            this.tblContainer.Controls.Add(this.propData, 1, 3);
            this.tblContainer.Controls.Add(this.tblButtons, 1, 4);
            this.tblContainer.Controls.Add(this.tblOptions, 1, 2);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 6;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContainer.Size = new System.Drawing.Size(792, 653);
            this.tblContainer.TabIndex = 0;
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 3;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.Controls.Add(this.btnClose, 2, 0);
            this.tblButtons.Controls.Add(this.btnSave, 1, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtons.Location = new System.Drawing.Point(13, 600);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(766, 39);
            this.tblButtons.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.ImageKey = "Cancel.ico";
            this.btnClose.ImageList = this.imglstSmallIcons;
            this.btnClose.Location = new System.Drawing.Point(649, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 33);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.ImageKey = "Save.ico";
            this.btnSave.ImageList = this.imglstSmallIcons;
            this.btnSave.Location = new System.Drawing.Point(529, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(114, 33);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tblOptions
            // 
            this.tblOptions.ColumnCount = 2;
            this.tblOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tblOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOptions.Controls.Add(this.lblImplementations, 0, 0);
            this.tblOptions.Controls.Add(this.cboImplementations, 1, 0);
            this.tblOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOptions.Location = new System.Drawing.Point(10, 231);
            this.tblOptions.Margin = new System.Windows.Forms.Padding(0);
            this.tblOptions.Name = "tblOptions";
            this.tblOptions.RowCount = 1;
            this.tblOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOptions.Size = new System.Drawing.Size(772, 35);
            this.tblOptions.TabIndex = 1;
            // 
            // lblImplementations
            // 
            this.lblImplementations.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblImplementations.AutoSize = true;
            this.lblImplementations.Location = new System.Drawing.Point(3, 11);
            this.lblImplementations.Name = "lblImplementations";
            this.lblImplementations.Size = new System.Drawing.Size(112, 13);
            this.lblImplementations.TabIndex = 0;
            this.lblImplementations.Text = "Implementations :";
            // 
            // cboImplementations
            // 
            this.cboImplementations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboImplementations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImplementations.FormattingEnabled = true;
            this.cboImplementations.Location = new System.Drawing.Point(163, 7);
            this.cboImplementations.Name = "cboImplementations";
            this.cboImplementations.Size = new System.Drawing.Size(606, 21);
            this.cboImplementations.TabIndex = 1;
            this.cboImplementations.SelectedIndexChanged += new System.EventHandler(this.cboImplementations_SelectedIndexChanged);
            // 
            // ConfigStoreUpdator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(792, 653);
            this.Controls.Add(this.tblContainer);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "ConfigStoreUpdator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BMC Config Store Updator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.tblOptions.ResumeLayout(false);
            this.tblOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.PropertyGrid propData;
        private System.Windows.Forms.ListView lvwProcesses;
        private System.Windows.Forms.ColumnHeader chdrSNo;
        private System.Windows.Forms.ColumnHeader chdrName;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tblOptions;
        private System.Windows.Forms.Label lblImplementations;
        private System.Windows.Forms.ComboBox cboImplementations;
    }
}

