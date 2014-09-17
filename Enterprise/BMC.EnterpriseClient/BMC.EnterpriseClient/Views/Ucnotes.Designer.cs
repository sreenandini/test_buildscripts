namespace BMC.EnterpriseClient
{
    partial class Ucnotes
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
            this.tblMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.txtnotes = new System.Windows.Forms.RichTextBox();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnfont = new System.Windows.Forms.Button();
            this.btnfontcolor = new System.Windows.Forms.Button();
            this.fdnotes = new System.Windows.Forms.FontDialog();
            this.clrnotes = new System.Windows.Forms.ColorDialog();
            this.tblMainFrame.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMainFrame
            // 
            this.tblMainFrame.ColumnCount = 1;
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.Controls.Add(this.tblButtons, 0, 1);
            this.tblMainFrame.Controls.Add(this.txtnotes, 0, 0);
            this.tblMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblMainFrame.Name = "tblMainFrame";
            this.tblMainFrame.RowCount = 2;
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainFrame.Size = new System.Drawing.Size(502, 446);
            this.tblMainFrame.TabIndex = 0;
            // 
            // txtnotes
            // 
            this.txtnotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtnotes.Location = new System.Drawing.Point(3, 3);
            this.txtnotes.Name = "txtnotes";
            this.txtnotes.Size = new System.Drawing.Size(496, 400);
            this.txtnotes.TabIndex = 0;
            this.txtnotes.Text = "";
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 3;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Controls.Add(this.btnfont, 0, 0);
            this.tblButtons.Controls.Add(this.btnfontcolor, 1, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtons.Location = new System.Drawing.Point(3, 409);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(496, 34);
            this.tblButtons.TabIndex = 1;
            // 
            // btnfont
            // 
            this.btnfont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnfont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfont.Location = new System.Drawing.Point(3, 3);
            this.btnfont.Name = "btnfont";
            this.btnfont.Size = new System.Drawing.Size(34, 28);
            this.btnfont.TabIndex = 0;
            this.btnfont.Text = "A";
            this.btnfont.UseVisualStyleBackColor = true;
            this.btnfont.Click += new System.EventHandler(this.btnfont_Click);
            // 
            // btnfontcolor
            // 
            this.btnfontcolor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnfontcolor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfontcolor.ForeColor = System.Drawing.Color.Red;
            this.btnfontcolor.Image = global::BMC.EnterpriseClient.Properties.Resources.fontColorImage;
            this.btnfontcolor.Location = new System.Drawing.Point(43, 3);
            this.btnfontcolor.Name = "btnfontcolor";
            this.btnfontcolor.Size = new System.Drawing.Size(34, 28);
            this.btnfontcolor.TabIndex = 1;
            this.btnfontcolor.UseVisualStyleBackColor = true;
            this.btnfontcolor.Click += new System.EventHandler(this.btnfontcolor_Click);
            // 
            // fdnotes
            // 
            this.fdnotes.Color = System.Drawing.SystemColors.ControlText;
            // 
            // Ucnotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblMainFrame);
            this.Name = "Ucnotes";
            this.Size = new System.Drawing.Size(502, 446);
            this.Load += new System.EventHandler(this.Ucnotes_Load);
            this.tblMainFrame.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMainFrame;
        private System.Windows.Forms.RichTextBox txtnotes;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnfont;
        private System.Windows.Forms.Button btnfontcolor;
        private System.Windows.Forms.FontDialog fdnotes;
        private System.Windows.Forms.ColorDialog clrnotes;
    }
}
