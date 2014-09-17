namespace BMC.EnterpriseClient.Views
{
    partial class frmAssociateUsertoEmployeeCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssociateUsertoEmployeeCard));
            this.grpUsertoEmpCard = new System.Windows.Forms.GroupBox();
            this.txtSrcOtherCards = new System.Windows.Forms.TextBox();
            this.lblOtherCards = new System.Windows.Forms.Label();
            this.lblAssignedCards = new System.Windows.Forms.Label();
            this.chklstEmpcards = new System.Windows.Forms.CheckedListBox();
            this.chklstAsignEmpCards = new System.Windows.Forms.CheckedListBox();
            this.btnRevoke = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRevokeAll = new System.Windows.Forms.Button();
            this.grpUsertoEmpCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpUsertoEmpCard
            // 
            this.grpUsertoEmpCard.Controls.Add(this.txtSrcOtherCards);
            this.grpUsertoEmpCard.Controls.Add(this.lblOtherCards);
            this.grpUsertoEmpCard.Controls.Add(this.lblAssignedCards);
            this.grpUsertoEmpCard.Controls.Add(this.chklstEmpcards);
            this.grpUsertoEmpCard.Controls.Add(this.chklstAsignEmpCards);
            this.grpUsertoEmpCard.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpUsertoEmpCard.ForeColor = System.Drawing.Color.Black;
            this.grpUsertoEmpCard.Location = new System.Drawing.Point(0, 12);
            this.grpUsertoEmpCard.Name = "grpUsertoEmpCard";
            this.grpUsertoEmpCard.Size = new System.Drawing.Size(431, 284);
            this.grpUsertoEmpCard.TabIndex = 0;
            this.grpUsertoEmpCard.TabStop = false;
            this.grpUsertoEmpCard.Text = "Employee Card Tracking";
            // 
            // txtSrcOtherCards
            // 
            this.txtSrcOtherCards.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrcOtherCards.ForeColor = System.Drawing.Color.DimGray;
            this.txtSrcOtherCards.Location = new System.Drawing.Point(298, 14);
            this.txtSrcOtherCards.Name = "txtSrcOtherCards";
            this.txtSrcOtherCards.Size = new System.Drawing.Size(123, 21);
            this.txtSrcOtherCards.TabIndex = 2;
            this.txtSrcOtherCards.Text = "Search Other Cards";
            this.txtSrcOtherCards.TextChanged += new System.EventHandler(this.txtSrcOtherCards_TextChanged);
            this.txtSrcOtherCards.Enter += new System.EventHandler(this.txtSrcOtherCards_Enter);
            this.txtSrcOtherCards.Leave += new System.EventHandler(this.txtSrcOtherCards_Leave);
            // 
            // lblOtherCards
            // 
            this.lblOtherCards.AutoSize = true;
            this.lblOtherCards.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherCards.Location = new System.Drawing.Point(215, 17);
            this.lblOtherCards.Name = "lblOtherCards";
            this.lblOtherCards.Size = new System.Drawing.Size(77, 13);
            this.lblOtherCards.TabIndex = 1;
            this.lblOtherCards.Text = "Other Cards";
            // 
            // lblAssignedCards
            // 
            this.lblAssignedCards.AutoSize = true;
            this.lblAssignedCards.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssignedCards.Location = new System.Drawing.Point(6, 17);
            this.lblAssignedCards.Name = "lblAssignedCards";
            this.lblAssignedCards.Size = new System.Drawing.Size(96, 13);
            this.lblAssignedCards.TabIndex = 0;
            this.lblAssignedCards.Text = "Assigned Cards";
            // 
            // chklstEmpcards
            // 
            this.chklstEmpcards.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklstEmpcards.FormattingEnabled = true;
            this.chklstEmpcards.HorizontalScrollbar = true;
            this.chklstEmpcards.Location = new System.Drawing.Point(218, 38);
            this.chklstEmpcards.Name = "chklstEmpcards";
            this.chklstEmpcards.ScrollAlwaysVisible = true;
            this.chklstEmpcards.Size = new System.Drawing.Size(203, 244);
            this.chklstEmpcards.TabIndex = 4;
            this.chklstEmpcards.ThreeDCheckBoxes = true;
            this.chklstEmpcards.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chklstEmpcards_ItemCheck);
            // 
            // chklstAsignEmpCards
            // 
            this.chklstAsignEmpCards.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklstAsignEmpCards.FormattingEnabled = true;
            this.chklstAsignEmpCards.HorizontalScrollbar = true;
            this.chklstAsignEmpCards.Location = new System.Drawing.Point(9, 38);
            this.chklstAsignEmpCards.Name = "chklstAsignEmpCards";
            this.chklstAsignEmpCards.ScrollAlwaysVisible = true;
            this.chklstAsignEmpCards.Size = new System.Drawing.Size(203, 244);
            this.chklstAsignEmpCards.TabIndex = 3;
            this.chklstAsignEmpCards.ThreeDCheckBoxes = true;
            // 
            // btnRevoke
            // 
            this.btnRevoke.Location = new System.Drawing.Point(90, 302);
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.Size = new System.Drawing.Size(75, 40);
            this.btnRevoke.TabIndex = 2;
            this.btnRevoke.Text = "&Revoke ";
            this.btnRevoke.UseVisualStyleBackColor = true;
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(9, 302);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "A&ssign";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(346, 302);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 40);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRevokeAll
            // 
            this.btnRevokeAll.Location = new System.Drawing.Point(171, 302);
            this.btnRevokeAll.Name = "btnRevokeAll";
            this.btnRevokeAll.Size = new System.Drawing.Size(75, 40);
            this.btnRevokeAll.TabIndex = 3;
            this.btnRevokeAll.Text = "Revoke &All";
            this.btnRevokeAll.UseVisualStyleBackColor = true;
            this.btnRevokeAll.Click += new System.EventHandler(this.btnRevokeAll_Click);
            // 
            // frmAssociateUsertoEmployeeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 347);
            this.Controls.Add(this.btnRevokeAll);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRevoke);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpUsertoEmpCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAssociateUsertoEmployeeCard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Associate User to Employee Card";
            this.Load += new System.EventHandler(this.frmAssociateUsertoEmployeeCard_Load);
            this.grpUsertoEmpCard.ResumeLayout(false);
            this.grpUsertoEmpCard.PerformLayout();
            this.ResumeLayout(false);

        }

      
        #endregion

        private System.Windows.Forms.GroupBox grpUsertoEmpCard;
        private System.Windows.Forms.Button btnRevoke;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckedListBox chklstEmpcards;
        private System.Windows.Forms.Label lblOtherCards;
        private System.Windows.Forms.Label lblAssignedCards;
        private System.Windows.Forms.CheckedListBox chklstAsignEmpCards;
        private System.Windows.Forms.Button btnRevokeAll;
        private System.Windows.Forms.TextBox txtSrcOtherCards;
    }
}