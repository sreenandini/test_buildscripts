namespace BMC.EnterpriseClient.Views
{
    partial class frmNotifications
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotifications));
            this.tlp_MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lv_Notifications = new System.Windows.Forms.ListView();
            this.clmCheckBox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNotificationItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNotifications = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNotificationTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.cb_SelectAll = new System.Windows.Forms.CheckBox();
            this.flp_Buttons = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.tlp_MainPanel.SuspendLayout();
            this.flp_Buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_MainPanel
            // 
            this.tlp_MainPanel.ColumnCount = 2;
            this.tlp_MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.72005F));
            this.tlp_MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.27995F));
            this.tlp_MainPanel.Controls.Add(this.lv_Notifications, 0, 1);
            this.tlp_MainPanel.Controls.Add(this.cb_SelectAll, 0, 0);
            this.tlp_MainPanel.Controls.Add(this.flp_Buttons, 1, 2);
            this.tlp_MainPanel.Controls.Add(this.lbl_Status, 0, 2);
            this.tlp_MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_MainPanel.Location = new System.Drawing.Point(0, 0);
            this.tlp_MainPanel.Name = "tlp_MainPanel";
            this.tlp_MainPanel.RowCount = 3;
            this.tlp_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.832298F));
            this.tlp_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.1677F));
            this.tlp_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_MainPanel.Size = new System.Drawing.Size(743, 358);
            this.tlp_MainPanel.TabIndex = 0;
            // 
            // lv_Notifications
            // 
            this.lv_Notifications.CheckBoxes = true;
            this.lv_Notifications.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmCheckBox,
            this.clmNotificationItem,
            this.clmNotifications,
            this.clmNotificationTime});
            this.tlp_MainPanel.SetColumnSpan(this.lv_Notifications, 2);
            this.lv_Notifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_Notifications.GridLines = true;
            this.lv_Notifications.Location = new System.Drawing.Point(3, 24);
            this.lv_Notifications.Name = "lv_Notifications";
            this.lv_Notifications.Size = new System.Drawing.Size(737, 290);
            this.lv_Notifications.TabIndex = 0;
            this.lv_Notifications.UseCompatibleStateImageBehavior = false;
            this.lv_Notifications.View = System.Windows.Forms.View.Details;
            // 
            // clmCheckBox
            // 
            this.clmCheckBox.Text = "";
            this.clmCheckBox.Width = 30;
            // 
            // clmNotificationItem
            // 
            this.clmNotificationItem.Text = "Items";
            this.clmNotificationItem.Width = 125;
            // 
            // clmNotifications
            // 
            this.clmNotifications.Text = "Notifications";
            this.clmNotifications.Width = 438;
            // 
            // clmNotificationTime
            // 
            this.clmNotificationTime.Text = "Notified Time";
            this.clmNotificationTime.Width = 134;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Clear.Location = new System.Drawing.Point(114, 3);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(100, 28);
            this.btn_Clear.TabIndex = 2;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Refresh.Location = new System.Drawing.Point(8, 3);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(100, 28);
            this.btn_Refresh.TabIndex = 3;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // cb_SelectAll
            // 
            this.cb_SelectAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_SelectAll.AutoSize = true;
            this.cb_SelectAll.Location = new System.Drawing.Point(3, 3);
            this.cb_SelectAll.Name = "cb_SelectAll";
            this.cb_SelectAll.Size = new System.Drawing.Size(70, 15);
            this.cb_SelectAll.TabIndex = 4;
            this.cb_SelectAll.Text = "Select All";
            this.cb_SelectAll.UseVisualStyleBackColor = true;
            this.cb_SelectAll.CheckedChanged += new System.EventHandler(this.cb_SelectAll_CheckedChanged);
            // 
            // flp_Buttons
            // 
            this.flp_Buttons.Controls.Add(this.btn_Close);
            this.flp_Buttons.Controls.Add(this.btn_Clear);
            this.flp_Buttons.Controls.Add(this.btn_Refresh);
            this.flp_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_Buttons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flp_Buttons.Location = new System.Drawing.Point(417, 320);
            this.flp_Buttons.Name = "flp_Buttons";
            this.flp_Buttons.Size = new System.Drawing.Size(323, 35);
            this.flp_Buttons.TabIndex = 5;
            // 
            // lbl_Status
            // 
            this.lbl_Status.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.ForeColor = System.Drawing.Color.Red;
            this.lbl_Status.Location = new System.Drawing.Point(3, 331);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(0, 13);
            this.lbl_Status.TabIndex = 6;
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Close.Location = new System.Drawing.Point(220, 3);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(100, 28);
            this.btn_Close.TabIndex = 4;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // frmNotifications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 358);
            this.Controls.Add(this.tlp_MainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNotifications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Notifications";
            this.Load += new System.EventHandler(this.frmNotifications_Load);
            this.tlp_MainPanel.ResumeLayout(false);
            this.tlp_MainPanel.PerformLayout();
            this.flp_Buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_MainPanel;
        private System.Windows.Forms.ListView lv_Notifications;
        private System.Windows.Forms.ColumnHeader clmNotificationItem;
        private System.Windows.Forms.ColumnHeader clmNotifications;
        private System.Windows.Forms.ColumnHeader clmCheckBox;
        private System.Windows.Forms.ColumnHeader clmNotificationTime;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.CheckBox cb_SelectAll;
        private System.Windows.Forms.FlowLayoutPanel flp_Buttons;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.Button btn_Close;
    }
}