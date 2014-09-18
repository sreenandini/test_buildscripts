namespace BMC.UI.ExchangeConfig
{
    partial class frmSetupODBCDsn
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
            this.ofdSelectDB = new System.Windows.Forms.OpenFileDialog();
            this.SsTrip = new System.Windows.Forms.StatusStrip();
            this.gmServerDetails = new System.Windows.Forms.GroupBox();
            this.cmbDefaultLang = new System.Windows.Forms.ComboBox();
            this.lblDefaullang = new System.Windows.Forms.Label();
            this.cmbDefaultDB = new System.Windows.Forms.ComboBox();
            this.lblDefaultDatabase = new System.Windows.Forms.Label();
            this.gpAuthentication = new System.Windows.Forms.GroupBox();
            this.pnlLoginCredentials = new System.Windows.Forms.Panel();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.txtpwd = new System.Windows.Forms.TextBox();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.rbSqlAuthentication = new System.Windows.Forms.RadioButton();
            this.rbWindowsAuthentication = new System.Windows.Forms.RadioButton();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.txtDSReferName = new System.Windows.Forms.TextBox();
            this.lblDBServer = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblDSReferName = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errValidate)).BeginInit();
            this.gmServerDetails.SuspendLayout();
            this.gpAuthentication.SuspendLayout();
            this.pnlLoginCredentials.SuspendLayout();
            this.SuspendLayout();
            // 
            // errValidate
            // 
            this.errValidate.ContainerControl = this;
            // 
            // ofdSelectDB
            // 
            this.ofdSelectDB.FileName = "openFileDialog1";
            // 
            // SsTrip
            // 
            this.SsTrip.Location = new System.Drawing.Point(0, 361);
            this.SsTrip.Name = "SsTrip";
            this.SsTrip.Size = new System.Drawing.Size(378, 22);
            this.SsTrip.TabIndex = 39;
            this.SsTrip.Text = "statusStrip1";
            // 
            // gmServerDetails
            // 
            this.gmServerDetails.BackColor = System.Drawing.Color.White;
            this.gmServerDetails.Controls.Add(this.cmbDefaultLang);
            this.gmServerDetails.Controls.Add(this.lblDefaullang);
            this.gmServerDetails.Controls.Add(this.cmbDefaultDB);
            this.gmServerDetails.Controls.Add(this.lblDefaultDatabase);
            this.gmServerDetails.Controls.Add(this.gpAuthentication);
            this.gmServerDetails.Controls.Add(this.cmbServer);
            this.gmServerDetails.Controls.Add(this.txtDSReferName);
            this.gmServerDetails.Controls.Add(this.lblDBServer);
            this.gmServerDetails.Controls.Add(this.lblDescription);
            this.gmServerDetails.Controls.Add(this.lblDSReferName);
            this.gmServerDetails.Controls.Add(this.txtDescription);
            this.gmServerDetails.Location = new System.Drawing.Point(9, 12);
            this.gmServerDetails.Name = "gmServerDetails";
            this.gmServerDetails.Size = new System.Drawing.Size(357, 312);
            this.gmServerDetails.TabIndex = 41;
            this.gmServerDetails.TabStop = false;
            this.gmServerDetails.Text = "Server Details";
            // 
            // cmbDefaultLang
            // 
            this.cmbDefaultLang.Location = new System.Drawing.Point(125, 276);
            this.cmbDefaultLang.Name = "cmbDefaultLang";
            this.cmbDefaultLang.Size = new System.Drawing.Size(173, 21);
            this.cmbDefaultLang.TabIndex = 51;
            this.cmbDefaultLang.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDefaultLang_KeyPress);
            // 
            // lblDefaullang
            // 
            this.lblDefaullang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaullang.Location = new System.Drawing.Point(11, 274);
            this.lblDefaullang.Name = "lblDefaullang";
            this.lblDefaullang.Size = new System.Drawing.Size(88, 32);
            this.lblDefaullang.TabIndex = 50;
            this.lblDefaullang.Text = "Default Language:";
            // 
            // cmbDefaultDB
            // 
            this.cmbDefaultDB.Location = new System.Drawing.Point(125, 237);
            this.cmbDefaultDB.Name = "cmbDefaultDB";
            this.cmbDefaultDB.Size = new System.Drawing.Size(173, 21);
            this.cmbDefaultDB.TabIndex = 49;
            this.cmbDefaultDB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDefaultDB_KeyPress);
            // 
            // lblDefaultDatabase
            // 
            this.lblDefaultDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultDatabase.Location = new System.Drawing.Point(11, 235);
            this.lblDefaultDatabase.Name = "lblDefaultDatabase";
            this.lblDefaultDatabase.Size = new System.Drawing.Size(88, 32);
            this.lblDefaultDatabase.TabIndex = 48;
            this.lblDefaultDatabase.Text = "Default Database:";
            // 
            // gpAuthentication
            // 
            this.gpAuthentication.Controls.Add(this.pnlLoginCredentials);
            this.gpAuthentication.Controls.Add(this.rbSqlAuthentication);
            this.gpAuthentication.Controls.Add(this.rbWindowsAuthentication);
            this.gpAuthentication.Location = new System.Drawing.Point(14, 121);
            this.gpAuthentication.Name = "gpAuthentication";
            this.gpAuthentication.Size = new System.Drawing.Size(310, 110);
            this.gpAuthentication.TabIndex = 47;
            this.gpAuthentication.TabStop = false;
            this.gpAuthentication.Text = "Authentication";
            // 
            // pnlLoginCredentials
            // 
            this.pnlLoginCredentials.Controls.Add(this.lblPwd);
            this.pnlLoginCredentials.Controls.Add(this.lblLoginName);
            this.pnlLoginCredentials.Controls.Add(this.txtpwd);
            this.pnlLoginCredentials.Controls.Add(this.txtLoginName);
            this.pnlLoginCredentials.Enabled = false;
            this.pnlLoginCredentials.Location = new System.Drawing.Point(6, 37);
            this.pnlLoginCredentials.Name = "pnlLoginCredentials";
            this.pnlLoginCredentials.Size = new System.Drawing.Size(298, 68);
            this.pnlLoginCredentials.TabIndex = 52;
            // 
            // lblPwd
            // 
            this.lblPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPwd.Location = new System.Drawing.Point(2, 38);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(76, 16);
            this.lblPwd.TabIndex = 50;
            this.lblPwd.Text = "Password";
            // 
            // lblLoginName
            // 
            this.lblLoginName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginName.Location = new System.Drawing.Point(2, 10);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(90, 16);
            this.lblLoginName.TabIndex = 49;
            this.lblLoginName.Text = "Login Name";
            // 
            // txtpwd
            // 
            this.txtpwd.Location = new System.Drawing.Point(98, 40);
            this.txtpwd.MaxLength = 50;
            this.txtpwd.Name = "txtpwd";
            this.txtpwd.PasswordChar = '*';
            this.txtpwd.Size = new System.Drawing.Size(173, 20);
            this.txtpwd.TabIndex = 48;
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(98, 7);
            this.txtLoginName.MaxLength = 50;
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(173, 20);
            this.txtLoginName.TabIndex = 47;
            // 
            // rbSqlAuthentication
            // 
            this.rbSqlAuthentication.AutoSize = true;
            this.rbSqlAuthentication.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSqlAuthentication.Location = new System.Drawing.Point(111, 19);
            this.rbSqlAuthentication.Name = "rbSqlAuthentication";
            this.rbSqlAuthentication.Size = new System.Drawing.Size(84, 17);
            this.rbSqlAuthentication.TabIndex = 1;
            this.rbSqlAuthentication.TabStop = true;
            this.rbSqlAuthentication.Text = "Sql Server";
            this.rbSqlAuthentication.UseVisualStyleBackColor = true;
            this.rbSqlAuthentication.CheckedChanged += new System.EventHandler(this.rbSqlAuthentication_CheckedChanged);
            // 
            // rbWindowsAuthentication
            // 
            this.rbWindowsAuthentication.AutoSize = true;
            this.rbWindowsAuthentication.Checked = true;
            this.rbWindowsAuthentication.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbWindowsAuthentication.Location = new System.Drawing.Point(11, 19);
            this.rbWindowsAuthentication.Name = "rbWindowsAuthentication";
            this.rbWindowsAuthentication.Size = new System.Drawing.Size(80, 17);
            this.rbWindowsAuthentication.TabIndex = 0;
            this.rbWindowsAuthentication.TabStop = true;
            this.rbWindowsAuthentication.Text = "Windows ";
            this.rbWindowsAuthentication.UseVisualStyleBackColor = true;
            this.rbWindowsAuthentication.CheckedChanged += new System.EventHandler(this.rbWindowsAuthentication_CheckedChanged);
            // 
            // cmbServer
            // 
            this.cmbServer.Location = new System.Drawing.Point(125, 94);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(173, 21);
            this.cmbServer.TabIndex = 46;
            this.cmbServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbServer_KeyPress);
            // 
            // txtDSReferName
            // 
            this.txtDSReferName.Enabled = false;
            this.txtDSReferName.Location = new System.Drawing.Point(125, 20);
            this.txtDSReferName.MaxLength = 50;
            this.txtDSReferName.Name = "txtDSReferName";
            this.txtDSReferName.Size = new System.Drawing.Size(173, 20);
            this.txtDSReferName.TabIndex = 45;
            this.txtDSReferName.Text = "Leisure SQL";
            // 
            // lblDBServer
            // 
            this.lblDBServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBServer.Location = new System.Drawing.Point(11, 99);
            this.lblDBServer.Name = "lblDBServer";
            this.lblDBServer.Size = new System.Drawing.Size(88, 16);
            this.lblDBServer.TabIndex = 42;
            this.lblDBServer.Text = "DB Server:";
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(11, 63);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(88, 16);
            this.lblDescription.TabIndex = 41;
            this.lblDescription.Text = "Description:";
            // 
            // lblDSReferName
            // 
            this.lblDSReferName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDSReferName.Location = new System.Drawing.Point(11, 20);
            this.lblDSReferName.Name = "lblDSReferName";
            this.lblDSReferName.Size = new System.Drawing.Size(108, 37);
            this.lblDSReferName.TabIndex = 40;
            this.lblDSReferName.Text = "Data Source Reference Name:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(125, 59);
            this.txtDescription.MaxLength = 50;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(173, 20);
            this.txtDescription.TabIndex = 37;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BackColor = System.Drawing.Color.White;
            this.btnTestConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestConnection.Location = new System.Drawing.Point(12, 331);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(119, 24);
            this.btnTestConnection.TabIndex = 42;
            this.btnTestConnection.Text = "Test Data Source";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.White;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(137, 331);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(109, 24);
            this.btnOK.TabIndex = 43;
            this.btnOK.Text = "SAVE";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(257, 331);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 24);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "Clear";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSetupODBCDsn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(378, 383);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.gmServerDetails);
            this.Controls.Add(this.SsTrip);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupODBCDsn";
            this.Text = "ODBC-System DSN Settings";
            this.Load += new System.EventHandler(this.frmSetupODBCDsn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errValidate)).EndInit();
            this.gmServerDetails.ResumeLayout(false);
            this.gmServerDetails.PerformLayout();
            this.gpAuthentication.ResumeLayout(false);
            this.gpAuthentication.PerformLayout();
            this.pnlLoginCredentials.ResumeLayout(false);
            this.pnlLoginCredentials.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errValidate;
        private System.Windows.Forms.GroupBox gmServerDetails;
        private System.Windows.Forms.TextBox txtDSReferName;
        private System.Windows.Forms.Label lblDBServer;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblDSReferName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.StatusStrip SsTrip;
        private System.Windows.Forms.OpenFileDialog ofdSelectDB;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.ComboBox cmbDefaultDB;
        private System.Windows.Forms.Label lblDefaultDatabase;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.ComboBox cmbDefaultLang;
        private System.Windows.Forms.Label lblDefaullang;
        private System.Windows.Forms.GroupBox gpAuthentication;
        private System.Windows.Forms.RadioButton rbSqlAuthentication;
        private System.Windows.Forms.RadioButton rbWindowsAuthentication;
        private System.Windows.Forms.Panel pnlLoginCredentials;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.TextBox txtpwd;
        private System.Windows.Forms.TextBox txtLoginName;

    }
}