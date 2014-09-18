namespace BMC.UI.ExchangeConfig
{
    partial class frmSQLRestore
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
            this.errValidate = new System.Windows.Forms.ErrorProvider(this.components);
            this.SsTrip = new System.Windows.Forms.StatusStrip();
            this.ofdSelectDB = new System.Windows.Forms.OpenFileDialog();
            this.grpServer = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDBBrowse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRestore = new System.Windows.Forms.Button();
            this.txtDataBases = new System.Windows.Forms.TextBox();
            this.txtDBFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblConnectDB = new System.Windows.Forms.LinkLabel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.cmbServers = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errValidate)).BeginInit();
            this.grpServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // errValidate
            // 
            this.errValidate.ContainerControl = this;
            // 
            // SsTrip
            // 
            this.SsTrip.Location = new System.Drawing.Point(0, 391);
            this.SsTrip.Name = "SsTrip";
            this.SsTrip.Size = new System.Drawing.Size(411, 22);
            this.SsTrip.TabIndex = 1;
            this.SsTrip.Text = "statusStrip1";
            // 
            // ofdSelectDB
            // 
            this.ofdSelectDB.FileName = "openFileDialog1";
            // 
            // grpServer
            // 
            this.grpServer.BackColor = System.Drawing.Color.White;
            this.grpServer.Controls.Add(this.label5);
            this.grpServer.Controls.Add(this.label1);
            this.grpServer.Controls.Add(this.btnDBBrowse);
            this.grpServer.Controls.Add(this.label4);
            this.grpServer.Controls.Add(this.btnRestore);
            this.grpServer.Controls.Add(this.txtDataBases);
            this.grpServer.Controls.Add(this.txtDBFile);
            this.grpServer.Controls.Add(this.label3);
            this.grpServer.Controls.Add(this.lblUser);
            this.grpServer.Controls.Add(this.lblServer);
            this.grpServer.Controls.Add(this.lblConnectDB);
            this.grpServer.Controls.Add(this.txtPassword);
            this.grpServer.Controls.Add(this.txtUser);
            this.grpServer.Controls.Add(this.cmbServers);
            this.grpServer.Location = new System.Drawing.Point(26, 21);
            this.grpServer.Name = "grpServer";
            this.grpServer.Size = new System.Drawing.Size(357, 343);
            this.grpServer.TabIndex = 38;
            this.grpServer.TabStop = false;
            this.grpServer.Text = "Server Details";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(190, 50);
            this.label5.TabIndex = 41;
            this.label5.Text = "Type in a path on the remote server for a remote database, or choose for local da" +
                "tabase.";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(8, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 22);
            this.label1.TabIndex = 31;
            this.label1.Text = "Restore From";
            this.label1.Visible = false;
            // 
            // btnDBBrowse
            // 
            this.btnDBBrowse.BackColor = System.Drawing.Color.White;
            this.btnDBBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBBrowse.Location = new System.Drawing.Point(309, 250);
            this.btnDBBrowse.Name = "btnDBBrowse";
            this.btnDBBrowse.Size = new System.Drawing.Size(32, 20);
            this.btnDBBrowse.TabIndex = 40;
            this.btnDBBrowse.Text = "...";
            this.btnDBBrowse.UseVisualStyleBackColor = false;
            this.btnDBBrowse.Visible = false;
            this.btnDBBrowse.Click += new System.EventHandler(this.btnDBBrowse_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 44;
            this.label4.Text = "Databases";
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.White;
            this.btnRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestore.Location = new System.Drawing.Point(232, 296);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(109, 24);
            this.btnRestore.TabIndex = 39;
            this.btnRestore.Text = "Restore Database";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // txtDataBases
            // 
            this.txtDataBases.Location = new System.Drawing.Point(11, 203);
            this.txtDataBases.Name = "txtDataBases";
            this.txtDataBases.Size = new System.Drawing.Size(306, 20);
            this.txtDataBases.TabIndex = 43;
            // 
            // txtDBFile
            // 
            this.txtDBFile.BackColor = System.Drawing.SystemColors.Window;
            this.txtDBFile.Location = new System.Drawing.Point(11, 251);
            this.txtDBFile.Name = "txtDBFile";
            this.txtDBFile.Size = new System.Drawing.Size(292, 20);
            this.txtDBFile.TabIndex = 38;
            this.txtDBFile.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.label3.TabIndex = 42;
            this.label3.Text = "Password";
            // 
            // lblUser
            // 
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(11, 63);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(184, 16);
            this.lblUser.TabIndex = 41;
            this.lblUser.Text = "DBUserName";
            // 
            // lblServer
            // 
            this.lblServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(11, 20);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(176, 16);
            this.lblServer.TabIndex = 40;
            this.lblServer.Text = "Server";
            // 
            // lblConnectDB
            // 
            this.lblConnectDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectDB.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblConnectDB.Location = new System.Drawing.Point(237, 159);
            this.lblConnectDB.Name = "lblConnectDB";
            this.lblConnectDB.Size = new System.Drawing.Size(80, 18);
            this.lblConnectDB.TabIndex = 39;
            this.lblConnectDB.TabStop = true;
            this.lblConnectDB.Text = "Connect";
            this.lblConnectDB.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblConnectDB.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblConnectDB_LinkClicked);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(11, 134);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(306, 20);
            this.txtPassword.TabIndex = 38;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(11, 83);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(306, 20);
            this.txtUser.TabIndex = 37;
            this.txtUser.Text = "sa";
            // 
            // cmbServers
            // 
            this.cmbServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServers.Location = new System.Drawing.Point(11, 36);
            this.cmbServers.Name = "cmbServers";
            this.cmbServers.Size = new System.Drawing.Size(306, 21);
            this.cmbServers.TabIndex = 36;
            // 
            // frmSQLRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(411, 413);
            this.Controls.Add(this.grpServer);
            this.Controls.Add(this.SsTrip);
            this.Name = "frmSQLRestore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQLRestore";
            this.Load += new System.EventHandler(this.frmSQLRestore_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errValidate)).EndInit();
            this.grpServer.ResumeLayout(false);
            this.grpServer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      

        #endregion

        private System.Windows.Forms.ErrorProvider errValidate;
        private System.Windows.Forms.StatusStrip SsTrip;
        private System.Windows.Forms.OpenFileDialog ofdSelectDB;
        private System.Windows.Forms.GroupBox grpServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDBBrowse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.TextBox txtDataBases;
        private System.Windows.Forms.TextBox txtDBFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.LinkLabel lblConnectDB;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.ComboBox cmbServers;
    }
}