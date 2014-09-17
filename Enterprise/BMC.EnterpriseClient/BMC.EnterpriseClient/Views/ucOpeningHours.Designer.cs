namespace BMC.EnterpriseClient.Views
{
    partial class ucOpeningHours
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lblHours = new System.Windows.Forms.Label();
            this.dgOpeningHours = new System.Windows.Forms.DataGridView();
            this.tlp_SelectionDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_CurrentSelectionValue = new System.Windows.Forms.Label();
            this.lbl_CurrentSelection = new System.Windows.Forms.Label();
            this.btnUseSiteTimes = new System.Windows.Forms.Button();
            this.lblToDayValue = new System.Windows.Forms.Label();
            this.lblFromDayValue = new System.Windows.Forms.Label();
            this.lbl_FromDay = new System.Windows.Forms.Label();
            this.lbl_To_Day = new System.Windows.Forms.Label();
            this.tlp_OpenCloseButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClosed = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbl_Period = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Midnight2 = new System.Windows.Forms.Label();
            this.lbl_Noon = new System.Windows.Forms.Label();
            this.lbl_Midnight = new System.Windows.Forms.Label();
            this.tbl_OpenHoursDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.txt_OpeningHours = new System.Windows.Forms.TextBox();
            this.tblContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOpeningHours)).BeginInit();
            this.tlp_SelectionDetails.SuspendLayout();
            this.tlp_OpenCloseButtons.SuspendLayout();
            this.tbl_Period.SuspendLayout();
            this.tbl_OpenHoursDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.lblHours, 0, 2);
            this.tblContainer.Controls.Add(this.dgOpeningHours, 0, 4);
            this.tblContainer.Controls.Add(this.tlp_SelectionDetails, 0, 5);
            this.tblContainer.Controls.Add(this.tbl_Period, 0, 3);
            this.tblContainer.Controls.Add(this.tbl_OpenHoursDetails, 0, 1);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 6;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 318F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblContainer.Size = new System.Drawing.Size(813, 586);
            this.tblContainer.TabIndex = 1;
            // 
            // lblHours
            // 
            this.lblHours.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHours.AutoSize = true;
            this.lblHours.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblHours.Location = new System.Drawing.Point(3, 61);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(87, 13);
            this.lblHours.TabIndex = 2;
            this.lblHours.Text = "Standard &Hours :";
            // 
            // dgOpeningHours
            // 
            this.dgOpeningHours.AllowUserToAddRows = false;
            this.dgOpeningHours.AllowUserToDeleteRows = false;
            this.dgOpeningHours.AllowUserToResizeColumns = false;
            this.dgOpeningHours.AllowUserToResizeRows = false;
            this.dgOpeningHours.BackgroundColor = System.Drawing.Color.White;
            this.dgOpeningHours.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgOpeningHours.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgOpeningHours.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgOpeningHours.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgOpeningHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOpeningHours.Location = new System.Drawing.Point(3, 108);
            this.dgOpeningHours.Name = "dgOpeningHours";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgOpeningHours.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgOpeningHours.RowHeadersWidth = 100;
            this.dgOpeningHours.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgOpeningHours.Size = new System.Drawing.Size(807, 312);
            this.dgOpeningHours.TabIndex = 5;
            this.dgOpeningHours.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgOpeningHours_CellEnter);
            this.dgOpeningHours.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgOpeningHours_CellMouseEnter);
            this.dgOpeningHours.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgOpeningHours_CellMouseMove);
            this.dgOpeningHours.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgOpeningHours_CellPainting);
            this.dgOpeningHours.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgOpeningHours_RowHeaderMouseClick);
            this.dgOpeningHours.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgOpeningHours_RowHeaderMouseDoubleClick);
            this.dgOpeningHours.SelectionChanged += new System.EventHandler(this.dgOpeningHours_SelectionChanged);
            this.dgOpeningHours.Paint += new System.Windows.Forms.PaintEventHandler(this.dgOpeningHours_Paint);
            // 
            // tlp_SelectionDetails
            // 
            this.tlp_SelectionDetails.ColumnCount = 5;
            this.tlp_SelectionDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tlp_SelectionDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_SelectionDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_SelectionDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tlp_SelectionDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlp_SelectionDetails.Controls.Add(this.lbl_CurrentSelectionValue, 1, 0);
            this.tlp_SelectionDetails.Controls.Add(this.lbl_CurrentSelection, 0, 0);
            this.tlp_SelectionDetails.Controls.Add(this.btnUseSiteTimes, 0, 3);
            this.tlp_SelectionDetails.Controls.Add(this.lblToDayValue, 1, 2);
            this.tlp_SelectionDetails.Controls.Add(this.lblFromDayValue, 1, 1);
            this.tlp_SelectionDetails.Controls.Add(this.lbl_FromDay, 0, 1);
            this.tlp_SelectionDetails.Controls.Add(this.lbl_To_Day, 0, 2);
            this.tlp_SelectionDetails.Controls.Add(this.tlp_OpenCloseButtons, 3, 0);
            this.tlp_SelectionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_SelectionDetails.Location = new System.Drawing.Point(3, 426);
            this.tlp_SelectionDetails.Name = "tlp_SelectionDetails";
            this.tlp_SelectionDetails.RowCount = 4;
            this.tlp_SelectionDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_SelectionDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_SelectionDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_SelectionDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_SelectionDetails.Size = new System.Drawing.Size(807, 157);
            this.tlp_SelectionDetails.TabIndex = 6;
            // 
            // lbl_CurrentSelectionValue
            // 
            this.lbl_CurrentSelectionValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_CurrentSelectionValue.AutoSize = true;
            this.lbl_CurrentSelectionValue.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_CurrentSelectionValue.Location = new System.Drawing.Point(120, 13);
            this.lbl_CurrentSelectionValue.Name = "lbl_CurrentSelectionValue";
            this.lbl_CurrentSelectionValue.Size = new System.Drawing.Size(0, 13);
            this.lbl_CurrentSelectionValue.TabIndex = 0;
            // 
            // lbl_CurrentSelection
            // 
            this.lbl_CurrentSelection.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_CurrentSelection.AutoSize = true;
            this.lbl_CurrentSelection.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_CurrentSelection.Location = new System.Drawing.Point(3, 13);
            this.lbl_CurrentSelection.Name = "lbl_CurrentSelection";
            this.lbl_CurrentSelection.Size = new System.Drawing.Size(94, 13);
            this.lbl_CurrentSelection.TabIndex = 6;
            this.lbl_CurrentSelection.Text = "Current Selection :";
            // 
            // btnUseSiteTimes
            // 
            this.btnUseSiteTimes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnUseSiteTimes.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUseSiteTimes.Location = new System.Drawing.Point(3, 128);
            this.btnUseSiteTimes.Name = "btnUseSiteTimes";
            this.btnUseSiteTimes.Size = new System.Drawing.Size(102, 23);
            this.btnUseSiteTimes.TabIndex = 3;
            this.btnUseSiteTimes.Text = "&Use Site Times";
            this.btnUseSiteTimes.UseVisualStyleBackColor = false;
            this.btnUseSiteTimes.Visible = false;
            this.btnUseSiteTimes.Click += new System.EventHandler(this.btnUseSiteTimes_Click);
            // 
            // lblToDayValue
            // 
            this.lblToDayValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblToDayValue.AutoSize = true;
            this.lblToDayValue.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblToDayValue.Location = new System.Drawing.Point(120, 93);
            this.lblToDayValue.Name = "lblToDayValue";
            this.lblToDayValue.Size = new System.Drawing.Size(0, 13);
            this.lblToDayValue.TabIndex = 2;
            // 
            // lblFromDayValue
            // 
            this.lblFromDayValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFromDayValue.AutoSize = true;
            this.lblFromDayValue.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblFromDayValue.Location = new System.Drawing.Point(120, 53);
            this.lblFromDayValue.Name = "lblFromDayValue";
            this.lblFromDayValue.Size = new System.Drawing.Size(0, 13);
            this.lblFromDayValue.TabIndex = 1;
            // 
            // lbl_FromDay
            // 
            this.lbl_FromDay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_FromDay.AutoSize = true;
            this.lbl_FromDay.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_FromDay.Location = new System.Drawing.Point(3, 53);
            this.lbl_FromDay.Name = "lbl_FromDay";
            this.lbl_FromDay.Size = new System.Drawing.Size(58, 13);
            this.lbl_FromDay.TabIndex = 7;
            this.lbl_FromDay.Text = "From Day :";
            // 
            // lbl_To_Day
            // 
            this.lbl_To_Day.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_To_Day.AutoSize = true;
            this.lbl_To_Day.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_To_Day.Location = new System.Drawing.Point(3, 93);
            this.lbl_To_Day.Name = "lbl_To_Day";
            this.lbl_To_Day.Size = new System.Drawing.Size(48, 13);
            this.lbl_To_Day.TabIndex = 8;
            this.lbl_To_Day.Text = "To Day :";
            // 
            // tlp_OpenCloseButtons
            // 
            this.tlp_OpenCloseButtons.ColumnCount = 3;
            this.tlp_SelectionDetails.SetColumnSpan(this.tlp_OpenCloseButtons, 2);
            this.tlp_OpenCloseButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_OpenCloseButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tlp_OpenCloseButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tlp_OpenCloseButtons.Controls.Add(this.btnClosed, 2, 0);
            this.tlp_OpenCloseButtons.Controls.Add(this.btnOpen, 1, 0);
            this.tlp_OpenCloseButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_OpenCloseButtons.Location = new System.Drawing.Point(572, 3);
            this.tlp_OpenCloseButtons.Name = "tlp_OpenCloseButtons";
            this.tlp_OpenCloseButtons.RowCount = 1;
            this.tlp_OpenCloseButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlp_OpenCloseButtons.Size = new System.Drawing.Size(232, 34);
            this.tlp_OpenCloseButtons.TabIndex = 11;
            // 
            // btnClosed
            // 
            this.btnClosed.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClosed.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClosed.Location = new System.Drawing.Point(132, 3);
            this.btnClosed.Name = "btnClosed";
            this.btnClosed.Size = new System.Drawing.Size(97, 28);
            this.btnClosed.TabIndex = 1;
            this.btnClosed.Text = "C&losed";
            this.btnClosed.UseVisualStyleBackColor = false;
            this.btnClosed.Click += new System.EventHandler(this.btnClosed_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOpen.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpen.Location = new System.Drawing.Point(29, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(97, 28);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "O&pen";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbl_Period
            // 
            this.tbl_Period.ColumnCount = 4;
            this.tbl_Period.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.02806F));
            this.tbl_Period.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.65732F));
            this.tbl_Period.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.65732F));
            this.tbl_Period.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.65732F));
            this.tbl_Period.Controls.Add(this.lbl_Midnight2, 1, 0);
            this.tbl_Period.Controls.Add(this.lbl_Noon, 2, 0);
            this.tbl_Period.Controls.Add(this.lbl_Midnight, 3, 0);
            this.tbl_Period.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_Period.Location = new System.Drawing.Point(3, 83);
            this.tbl_Period.Name = "tbl_Period";
            this.tbl_Period.RowCount = 2;
            this.tbl_Period.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_Period.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_Period.Size = new System.Drawing.Size(807, 19);
            this.tbl_Period.TabIndex = 3;
            // 
            // lbl_Midnight2
            // 
            this.lbl_Midnight2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Midnight2.AutoSize = true;
            this.lbl_Midnight2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_Midnight2.Location = new System.Drawing.Point(116, 0);
            this.lbl_Midnight2.Name = "lbl_Midnight2";
            this.lbl_Midnight2.Size = new System.Drawing.Size(47, 1);
            this.lbl_Midnight2.TabIndex = 0;
            this.lbl_Midnight2.Text = "Midnight";
            // 
            // lbl_Noon
            // 
            this.lbl_Noon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_Noon.AutoSize = true;
            this.lbl_Noon.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_Noon.Location = new System.Drawing.Point(443, 0);
            this.lbl_Noon.Name = "lbl_Noon";
            this.lbl_Noon.Size = new System.Drawing.Size(33, 1);
            this.lbl_Noon.TabIndex = 1;
            this.lbl_Noon.Text = "Noon";
            // 
            // lbl_Midnight
            // 
            this.lbl_Midnight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Midnight.AutoSize = true;
            this.lbl_Midnight.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_Midnight.Location = new System.Drawing.Point(757, 0);
            this.lbl_Midnight.Name = "lbl_Midnight";
            this.lbl_Midnight.Size = new System.Drawing.Size(47, 1);
            this.lbl_Midnight.TabIndex = 2;
            this.lbl_Midnight.Text = "Midnight";
            // 
            // tbl_OpenHoursDetails
            // 
            this.tbl_OpenHoursDetails.ColumnCount = 2;
            this.tbl_OpenHoursDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tbl_OpenHoursDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tbl_OpenHoursDetails.Controls.Add(this.lblCaption, 0, 0);
            this.tbl_OpenHoursDetails.Controls.Add(this.txt_OpeningHours, 1, 0);
            this.tbl_OpenHoursDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_OpenHoursDetails.Location = new System.Drawing.Point(3, 28);
            this.tbl_OpenHoursDetails.Name = "tbl_OpenHoursDetails";
            this.tbl_OpenHoursDetails.RowCount = 1;
            this.tbl_OpenHoursDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_OpenHoursDetails.Size = new System.Drawing.Size(807, 24);
            this.tbl_OpenHoursDetails.TabIndex = 6;
            // 
            // lblCaption
            // 
            this.lblCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCaption.AutoSize = true;
            this.lblCaption.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblCaption.Location = new System.Drawing.Point(3, 5);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(31, 13);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "&Site :";
            // 
            // txt_OpeningHours
            // 
            this.txt_OpeningHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OpeningHours.Location = new System.Drawing.Point(164, 3);
            this.txt_OpeningHours.MaxLength = 50;
            this.txt_OpeningHours.Name = "txt_OpeningHours";
            this.txt_OpeningHours.ReadOnly = true;
            this.txt_OpeningHours.Size = new System.Drawing.Size(640, 20);
            this.txt_OpeningHours.TabIndex = 1;
            // 
            // ucOpeningHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblContainer);
            this.Name = "ucOpeningHours";
            this.Size = new System.Drawing.Size(813, 586);
            this.Load += new System.EventHandler(this.ucOpeningHours_Load);
            this.Resize += new System.EventHandler(this.ucOpeningHours_Resize);
            this.tblContainer.ResumeLayout(false);
            this.tblContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOpeningHours)).EndInit();
            this.tlp_SelectionDetails.ResumeLayout(false);
            this.tlp_SelectionDetails.PerformLayout();
            this.tlp_OpenCloseButtons.ResumeLayout(false);
            this.tbl_Period.ResumeLayout(false);
            this.tbl_Period.PerformLayout();
            this.tbl_OpenHoursDetails.ResumeLayout(false);
            this.tbl_OpenHoursDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.TableLayoutPanel tbl_Period;
        private System.Windows.Forms.Label lbl_Midnight2;
        private System.Windows.Forms.Label lbl_Noon;
        private System.Windows.Forms.Label lbl_Midnight;
        private System.Windows.Forms.TableLayoutPanel tlp_SelectionDetails;
        private System.Windows.Forms.Label lbl_CurrentSelectionValue;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClosed;
        private System.Windows.Forms.Label lblFromDayValue;
        private System.Windows.Forms.Label lblToDayValue;
        private System.Windows.Forms.Button btnUseSiteTimes;
        private System.Windows.Forms.TableLayoutPanel tbl_OpenHoursDetails;
        private System.Windows.Forms.Label lbl_CurrentSelection;
        private System.Windows.Forms.Label lbl_FromDay;
        private System.Windows.Forms.Label lbl_To_Day;
        private System.Windows.Forms.DataGridView dgOpeningHours;
        private System.Windows.Forms.TextBox txt_OpeningHours;
        private System.Windows.Forms.TableLayoutPanel tlp_OpenCloseButtons;
    }
}
