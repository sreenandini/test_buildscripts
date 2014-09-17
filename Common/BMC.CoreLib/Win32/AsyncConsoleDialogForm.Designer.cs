namespace BMC.CoreLib.Win32
{
    partial class AsyncConsoleDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsyncConsoleDialogForm));
            this.imglstIcons = new System.Windows.Forms.ImageList(this.components);
            this.pbarStatus = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.tblContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // imglstIcons
            // 
            this.imglstIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstIcons.ImageStream")));
            this.imglstIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstIcons.Images.SetKeyName(0, "OK.ico");
            this.imglstIcons.Images.SetKeyName(1, "Cancel.ico");
            // 
            // pbarStatus
            // 
            this.pbarStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbarStatus.Location = new System.Drawing.Point(6, 502);
            this.pbarStatus.Margin = new System.Windows.Forms.Padding(6);
            this.pbarStatus.MarqueeAnimationSpeed = 50;
            this.pbarStatus.Name = "pbarStatus";
            this.pbarStatus.Size = new System.Drawing.Size(780, 18);
            this.pbarStatus.Step = 1;
            this.pbarStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbarStatus.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //this.btnCancel.ImageKey = "Cancel.ico";
            this.btnCancel.ImageList = this.imglstIcons;
            this.btnCancel.Location = new System.Drawing.Point(326, 529);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tblContainer
            // 
            this.tblContainer.BackColor = System.Drawing.SystemColors.Control;
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.btnCancel, 0, 2);
            this.tblContainer.Controls.Add(this.pbarStatus, 0, 1);
            this.tblContainer.Controls.Add(this.txtMessage, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 4;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblContainer.Size = new System.Drawing.Size(792, 566);
            this.tblContainer.TabIndex = 0;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.Black;
            this.txtMessage.CausesValidation = false;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(6, 6);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.txtMessage.MaxLength = 32767;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtMessage.ShortcutsEnabled = false;
            this.txtMessage.Size = new System.Drawing.Size(780, 490);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.Text = "";
            // 
            // AsyncConsoleDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tblContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "AsyncConsoleDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AsyncConsoleDialogForm";
            this.Load += new System.EventHandler(this.AsyncConsoleDialogForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AsyncConsoleDialogForm_FormClosing_1);
            this.tblContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imglstIcons;
        private System.Windows.Forms.ProgressBar pbarStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.RichTextBox txtMessage;
    }
}