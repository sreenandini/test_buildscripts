using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Windows.Media;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Helper_classes;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Transport;
using System.Collections.Generic;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CPositionDetailsEvents.xaml
    /// </summary>
    public partial class CPositionDetailsEvents : UserControl, IDisposable
    {
        #region Private Variables

        private string strBarPosName = string.Empty;
        private DateTime dtStartDate;
        private DateTime dtEndDate;
        private bool bRefresh = false;
        private int installationNo;
        //private bool IsSelectedFirst = true;
        private int iPageNum = 1;
        private int iPageSize = 13;
        private int iLastPage = 1;
        DataTable dtEventDetails;
        DataTable dtEventDetailByPage;
        #endregion Private Variables

        #region Constructor
        public CPositionDetailsEvents()
        {
            InitializeComponent();

            if (!Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.Events.ClearEvents"))
            {
                btnClearEvents.Visibility = Visibility.Hidden;
                //CR#85378 - MANOJ 15th Sep 2010.
                //If Clear Events right is not given to user
                //Then user should not be allowed to Clear Machine & Clear Site.
                btnClearMachine.Visibility = Visibility.Hidden;
                btnClearSite.Visibility = Visibility.Hidden;
            }

        }

        public CPositionDetailsEvents(string strBarPos, int installationNumber)
        {
            InitializeComponent();
            BarPosName = strBarPos;
            installationNo = installationNumber;
            if (!Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.Events.ClearEvents"))
            {
                btnClearEvents.Visibility = Visibility.Hidden;
                //CR#85378 - MANOJ 15th Sep 2010.
                //If Clear Events right is not given to user
                //Then user should not be allowed to Clear Machine & Clear Site.
                btnClearMachine.Visibility = Visibility.Hidden;
                btnClearSite.Visibility = Visibility.Hidden;
            }

            #region Hide Column
            //Hide "Legal" column in events gridview if Employee card is disabled
            if (!Settings.IsEmployeeCardTrackingEnabled)
            {
                GridView_1.Columns[GridView_1.Columns.Count-1].Width = 0;
                lblLegalEvents.Visibility = System.Windows.Visibility.Hidden;
                cmbLegalEvent.Visibility = System.Windows.Visibility.Hidden;
            }
            #endregion
        }

        #endregion Constructor

        #region Properties

        public string BarPosName
        {
            get
            {
                return strBarPosName;
            }
            set
            {
                strBarPosName = value;
            }

        }

        #endregion Properties

        #region Events
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dtStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 1, 0);
            dtStartDate = dtStartDate.AddDays(-7);
            dtEndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            dtEventDetails = new DataTable();

            dtpStartDate.Text = dtStartDate.ToShortDateString();
            dtpEndDate.Text = dtEndDate.ToShortDateString();

            FillEventTypeComboList();
            FillEventsDataForPosition();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            iPageNum = 1;

            dtStartDate = new DateTime(dtpStartDate.Text.ReadDate().Year, dtpStartDate.Text.ReadDate().Month, dtpStartDate.Text.ReadDate().Day, 0, 1, 0);
            dtEndDate = new DateTime(dtpEndDate.Text.ReadDate().Year, dtpEndDate.Text.ReadDate().Month, dtpEndDate.Text.ReadDate().Day, 23, 59, 59);

            if (dtStartDate >= dtEndDate)
            {
                MessageBox.ShowBox("MessageID99", BMC_Icon.Warning);
                lstPositionDetailsEvents.DataContext = null;
            }
            else
            {
                bRefresh = true;
                FillEventsDataForPosition();
            }
        }

        private void btnClearEvents_Click(object sender, RoutedEventArgs e)
        {
            int PosCount = 0;
            try
            {
                btnClearEvents.IsEnabled = false;
                string sEvents = string.Empty;

                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);


                if (lstPositionDetailsEvents.Items.Count == 0)
                {
                    MessageBox.ShowBox("MessageID896", BMC_Icon.Information);
                    return;
                }
                if (lstPositionDetailsEvents.SelectedItems.Count == 0)
                {
                    MessageBox.ShowBox("MessageID229", BMC_Icon.Information);
                    return;
                }

                foreach (DataRowView dataRowEvent in lstPositionDetailsEvents.SelectedItems)
                {
                    if (string.Format(dataRowEvent["ClearedFlag"].ToString()).ToUpper() == "NO")
                    {
                        PosCount = PosCount + 1;
                    }
                }

                if (PosCount < 1)
                {
                    MessageBox.ShowBox("MessageID226", BMC_Icon.Information);
                    return;
                }

                if (MessageBox.ShowBox("MessageID230", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No) return;

                foreach (DataRowView dataRowEvent in lstPositionDetailsEvents.SelectedItems)
                {
                    if (string.Format(dataRowEvent["ClearedFlag"].ToString()).ToUpper() == "NO")
                    {
                        if (ClearEvents("EVENTS", dataRowEvent["Event_Type"].ToString(), Convert.ToInt32(dataRowEvent["Event_No"])))
                        {
                            sEvents += dataRowEvent["Event_No"].ToString() + ",";
                        }
                    }

                }

                MessageBox.ShowBox("MessageID227", BMC_Icon.Information);

                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {

                    AuditModuleName = ModuleName.Events,
                    Audit_Screen_Name = "Events|ClearEvents",
                    Audit_Desc = "Events Cleared: " + sEvents.Substring(0, sEvents.Length - 1),
                    AuditOperationType = OperationType.MODIFY,
                    Audit_Field = "Event Number(s)",
                    Audit_Old_Vl = "Open Events",
                    Audit_New_Vl = "Closed"
                });
                                
                FillEventsDataForPosition();
            }
            finally
            {
                btnClearEvents.IsEnabled = true;
            }
        }

        private void btnClearMachine_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                btnClearMachine.IsEnabled = false;
                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);

                if (lstPositionDetailsEvents.Items.Count == 0)
                {
                    MessageBox.ShowBox("MessageID896", BMC_Icon.Information);
                    return;
                }

                if (MessageBox.ShowBox("MessageID231", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No) return;

                if (ClearEvents("MACHINE", string.Empty, 0))
                {
                    MessageBox.ShowBox("MessageID227", BMC_Icon.Information);

                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.Events,
                        Audit_Screen_Name = "Events|ClearMachine",
                        Audit_Desc = "Clear Machine",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Event Number(s)",
                        Audit_Old_Vl = "Open Events",
                        Audit_New_Vl = "Closed"
                    });

                }
                else
                {
                    MessageBox.ShowBox("MessageID228", BMC_Icon.Error);

                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.Events,
                        Audit_Screen_Name = "Events|ClearMachine",
                        Audit_Desc = "Unable to Clear Machine",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Event Number(s)",
                        Audit_Old_Vl = "Open Events",
                        Audit_New_Vl = ""
                    });
                }

                iPageNum = iLastPage = 1;

                FillEventsDataForPosition();
            }
            finally
            {
                btnClearMachine.IsEnabled = true;
            }
        }

        private void btnClearSite_Click(object sender, RoutedEventArgs e)
        {
            IEventDetails objCDO = EventsBusinessObject.CreateInstance();
            try
            {
                btnClearSite.IsEnabled = false;
                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);


                bool unclearedEvents = objCDO.CheckForUnclearedEvents();
                if (unclearedEvents)
                {
                    MessageBox.ShowBox("MessageID896", BMC_Icon.Information);
                    return;
                }

                if (MessageBox.ShowBox("MessageID232", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No) return;

                if (ClearEvents("SITE", string.Empty, 0))
                {
                    MessageBox.ShowBox("MessageID227", BMC_Icon.Information);

                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.Events,
                        Audit_Screen_Name = "Events|ClearSite",
                        Audit_Desc = "Clear Site",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Event Number(s)",
                        Audit_Old_Vl = "Open Events",
                        Audit_New_Vl = "Closed"
                    });
                }
                else
                {
                    MessageBox.ShowBox("MessageID228", BMC_Icon.Error);

                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.Events,
                        Audit_Screen_Name = "Events|ClearSite",
                        Audit_Desc = "Unable to Clear Site",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Event Number(s)",
                        Audit_Old_Vl = "Open Events",
                        Audit_New_Vl = ""
                    });
                }

                iPageNum = iLastPage = 1;
                FillEventsDataForPosition();
            }
            finally
            {
                btnClearSite.IsEnabled = true;
            }
        }

        #endregion Events

        #region Private Methods

        void FillEventsDataForPosition()
        {
            IEventDetails objCDO = EventsBusinessObject.CreateInstance();

            lstPositionDetailsEvents.DataContext = null;

            txtPageNo.Text = "";
            
            dtEventDetails = objCDO.GetEventDetails(dtStartDate, dtEndDate, BarPosName, Convert.ToInt32(chkShowClearedEvents.IsChecked == true ? 1 : 0), cmbEventType.SelectedItem.ToString(), iPageSize,cmbLegalEvent.SelectedIndex);

            if (dtEventDetails.Rows.Count > 0)
            {
                iLastPage = Convert.ToInt32(dtEventDetails.Rows[dtEventDetails.Rows.Count - 1]["Page"]);
                if (iPageNum > iLastPage)
                    iPageNum = iLastPage;
                PopulateEventsListView();                
            }
            else
            {
                if (bRefresh)
                {
                    MessageBox.ShowBox("MessageID37", BMC_Icon.Information);
                    bRefresh = false;
                }
            }
        }

        void PopulateEventsListView()
        {

            if (dtEventDetailByPage == null)
                dtEventDetailByPage = new DataTable();
            
            DataView dview = dtEventDetails.DefaultView;
            dview.RowFilter = "Page = " + iPageNum;
            dtEventDetailByPage = dview.ToTable();

            if (dtEventDetailByPage.Rows.Count > 0)
            {
                lstPositionDetailsEvents.DataContext = dtEventDetailByPage.DefaultView;
                this.DataContext = lstPositionDetailsEvents.DataContext = dtEventDetailByPage.DefaultView;
                lstPositionDetailsEvents.ScrollIntoView(lstPositionDetailsEvents.SelectedItem);
                txtPageNo.Text = iPageNum.ToString() + "/" + iLastPage.ToString();
            }
            else
            {
                if (bRefresh)
                {
                    MessageBox.ShowBox("MessageID37", BMC_Icon.Information);
                    bRefresh = false;
                }
            }
        }

        bool ClearEvents(string clearType, string eventType, int eventNo)
        {
            try
            {
                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);

                IEventDetails objCDO = EventsBusinessObject.CreateInstance();

                if (objCDO.UpdateEventDetails(clearType, eventType, eventNo, installationNo))
                {
                    LogManager.WriteLog("Event(s) cleared successfully", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to clear event(s)", LogManager.enumLogLevel.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        void FillEventTypeComboList()
        {
            IEventDetails objCDO = EventsBusinessObject.CreateInstance();
            string strEventTypes = string.Empty;
            string[] strList;

            strEventTypes = objCDO.FillEventType();
            strEventTypes = "All," + strEventTypes;
            strList = strEventTypes.Split(',');
            Array.Sort(strList);
            cmbEventType.DataContext = strList;
            cmbEventType.ItemsSource = strList;
            cmbEventType.DataContext = strList;
            cmbEventType.SelectedIndex = 0;

            string[] _lstLegalTypes = { "All", "Legal", "Illegal" };

            cmbLegalEvent.ItemsSource = _lstLegalTypes;
            cmbLegalEvent.DataContext = _lstLegalTypes;

            cmbLegalEvent.SelectedIndex = 0;
        }
        
        #endregion Private Methods

        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.btnRefresh.Click -= (this.btnRefresh_Click);
                        this.btnClearEvents.Click -= (this.btnClearEvents_Click);
                        this.btnClearMachine.Click -= (this.btnClearMachine_Click);
                        this.btnClearSite.Click -= (this.btnClearSite_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CPos objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CPositionDetailsEvents"/> is reclaimed by garbage collection.
        /// </summary>
        ~CPositionDetailsEvents()
        {
            Dispose(false);
        }

        #endregion

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            if (iPageNum == 1)
                return;
            else
            {
                iPageNum = 1;
                PopulateEventsListView();
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (iPageNum == 1)
                return;

            iPageNum = iPageNum - 1;
            PopulateEventsListView();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPageNo.Text))
                return;

            string[] sPageSplit = txtPageNo.Text.Split(new char[] { '/' });
            int nLastPage = Convert.ToInt32(sPageSplit[1]);

            if (iPageNum == nLastPage)
                return;
            else
            {
                iPageNum++;                
                PopulateEventsListView();
            }

        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPageNo.Text))
                return;

            string[] sPageSplit = txtPageNo.Text.Split(new char[] { '/' });
            int nLastPage = Convert.ToInt32(sPageSplit[1]);

            if (iPageNum == nLastPage)
                return;
            else
            {
                iPageNum = nLastPage;
                PopulateEventsListView();
            }
        }
    }
}
