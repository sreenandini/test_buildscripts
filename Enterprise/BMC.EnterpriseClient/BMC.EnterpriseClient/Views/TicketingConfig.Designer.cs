namespace BMC.EnterpriseClient.Views
{
    partial class TicketingConfig
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtTicketingURL = new System.Windows.Forms.TextBox();
            this.grdGetSites = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tblMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblLowerButton = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.grdGetSites)).BeginInit();
            this.tblMainFrame.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.tblLowerButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnCancel.Location = new System.Drawing.Point(332, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnOk.Location = new System.Drawing.Point(212, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 28);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtTicketingURL
            // 
            this.txtTicketingURL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTicketingURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtTicketingURL.Location = new System.Drawing.Point(111, 3);
            this.txtTicketingURL.Name = "txtTicketingURL";
            this.txtTicketingURL.Size = new System.Drawing.Size(316, 20);
            this.txtTicketingURL.TabIndex = 9;
            // 
            // grdGetSites
            // 
            this.grdGetSites.AllowUserToAddRows = false;
            this.grdGetSites.AllowUserToDeleteRows = false;
            this.grdGetSites.AllowUserToResizeColumns = false;
            this.grdGetSites.AllowUserToResizeRows = false;
            this.grdGetSites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.tblContainer.SetColumnSpan(this.grdGetSites, 2);
            this.grdGetSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGetSites.Location = new System.Drawing.Point(3, 53);
            this.grdGetSites.Name = "grdGetSites";
            this.grdGetSites.Size = new System.Drawing.Size(429, 286);
            this.grdGetSites.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.Location = new System.Drawing.Point(3, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Site Alliance";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "* Ticketing URL";
            // 
            // tblMainFrame
            // 
            this.tblMainFrame.ColumnCount = 1;
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMainFrame.Controls.Add(this.tblContainer, 0, 0);
            this.tblMainFrame.Controls.Add(this.tblLowerButton, 0, 1);
            this.tblMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblMainFrame.Name = "tblMainFrame";
            this.tblMainFrame.RowCount = 2;
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainFrame.Size = new System.Drawing.Size(441, 388);
            this.tblMainFrame.TabIndex = 12;
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 2;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tblContainer.Controls.Add(this.label1, 0, 0);
            this.tblContainer.Controls.Add(this.txtTicketingURL, 1, 0);
            this.tblContainer.Controls.Add(this.grdGetSites, 0, 2);
            this.tblContainer.Controls.Add(this.label2, 0, 1);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(3, 3);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 3;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Size = new System.Drawing.Size(435, 342);
            this.tblContainer.TabIndex = 13;
            // 
            // tblLowerButton
            // 
            this.tblLowerButton.ColumnCount = 3;
            this.tblLowerButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowerButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowerButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowerButton.Controls.Add(this.btnCancel, 2, 0);
            this.tblLowerButton.Controls.Add(this.btnOk, 1, 0);
            this.tblLowerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLowerButton.Location = new System.Drawing.Point(3, 351);
            this.tblLowerButton.Name = "tblLowerButton";
            this.tblLowerButton.RowCount = 1;
            this.tblLowerButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowerButton.Size = new System.Drawing.Size(435, 34);
            this.tblLowerButton.TabIndex = 13;
            // 
            // TicketingConfig
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(441, 388);
            this.Controls.Add(this.tblMainFrame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TicketingConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TicketingConfig";
            this.Load += new System.EventHandler(this.TicketingConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdGetSites)).EndInit();
            this.tblMainFrame.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            this.tblContainer.PerformLayout();
            this.tblLowerButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtTicketingURL;
        private System.Windows.Forms.DataGridView grdGetSites;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tblMainFrame;
        private System.Windows.Forms.TableLayoutPanel tblLowerButton;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
    }
}