using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Win32;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System.IO;
using BMC.CoreLib.Concurrent;
using System.Threading;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmEventViewer : Form
    {
        #region Local Declartion

        #region Objects
        private EventViewerBiz _objEventViewerBiz = null;
        List<SiteEvent> _lstSiteEvents;
        List<SitesResult> _lstSiteDetails;
        List<EnterpriseEvent> _lstEnterpriseEvents;
        Dictionary<string, string> dColumnNames;

        #endregion Objects

        #region Local Variables
        bool bIsFilterHidden = false;
        #endregion Local Variables

        #region Resources
        string sLessThanDouble = string.Empty;
        string sGreaterThanDouble = string.Empty;
        string sAny = string.Empty;
        string sNoneHyphen = string.Empty;
        string sHideFilter = string.Empty;
        #endregion Resources

        #endregion Local Declartion

        #region Constructor
        public frmEventViewer()
        {
            InitializeComponent();
            this.SetTagProperty();
            this.ResolveResources();
        }
        #endregion Constructor

        #region Events

        private void frmEventViewer_Load(object sender, EventArgs e)
        {
            
            this.ReadResources();

            // Change Request #205362  fix.
            dtFromDate.CustomFormat = string.Concat(Common.Utilities.Common.GetDateFormatByUserSetting(), " HH:mm:ss");
            dtToDate.CustomFormat = string.Concat(Common.Utilities.Common.GetDateFormatByUserSetting(), " HH:mm:ss");
            _objEventViewerBiz = EventViewerBiz.CreateInstance();
            
            _lstSiteEvents = null;
            _lstEnterpriseEvents = null;
            _lstSiteDetails = null;

            loadFilter();
            refreshFilters();
        }

        private void chkEndDateTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEndDateTime.Checked)
                dtToDate.Enabled = true;
            else
            {
                dtToDate.Enabled = false;
                dtToDate.Value = DateTime.Now;
            }
        }

        private void btnHideFilter_Click(object sender, EventArgs e)
        {
            if (bIsFilterHidden)
            {
                bIsFilterHidden = false;
                btnHideFilter.Text = sLessThanDouble + " " + sHideFilter;
                tblTopControls.ColumnStyles[0].Width = 300;
            }
            else
            {
                bIsFilterHidden = true;
                btnHideFilter.Text = sGreaterThanDouble + " " + sHideFilter;
                tblTopControls.ColumnStyles[0].Width = 0;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgEventViewer.Rows.Count > 0)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "CSV Documents (*.csv)|*.csv";

                if (dialog.ShowDialog() == DialogResult.Cancel) return;

                string fileName = dialog.FileName;

                var sb = new StringBuilder();

                var headers = dgEventViewer.Columns.Cast<DataGridViewColumn>();

                try
                {
                    using (IExecutorService _exec = ExecutorServiceFactory.CreateExecutorService())
                    {
                        Win32Extensions.ShowAsyncDialog(this, "Exporting event details...", _exec,
                            (o) =>
                            {
                                IAsyncProgress2 o2 = o as IAsyncProgress2;

                                o2.InitializeProgress(1, dgEventViewer.Rows.Count);

                                int iCounter = 1;

                                sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));
                                o.CrossThreadInvoke(() =>
                                {
                                    foreach (DataGridViewRow row in dgEventViewer.Rows)
                                    {
                                        o2.UpdateStatusProgress(iCounter++, "Writing data to CSV...");
                                        var cells = row.Cells.Cast<DataGridViewCell>();
                                        sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
                                    }
                                });

                                File.WriteAllText(fileName, sb.ToString());
                            });
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    this.ShowErrorMessageBox("There was an error in exporting data to CSV.");
                }
            }
            else
            {
                this.ShowInfoMessageBox("There are no records to export to CSV.");
            }
        }

        private void rdnSiteLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (rdnSiteLevel.Checked)
                cmbEventType.Enabled = chkShowAutoClosedEvents.Enabled = true;
            else
                cmbEventType.Enabled = chkShowAutoClosedEvents.Enabled = chkShowAutoClosedEvents.Checked = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ValidateControl())
            {
                dgEventViewer.Rows.Clear();
                dgEventViewer.Columns.Clear();
                SearchEvents();
            }
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            refreshFilters();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Events

        #region UserMethods

        private void SetTagProperty()
        {
            this.Tag = "Key_EventViewer";
            
            this.btnExport.Tag = "Key_Export";
            this.btnClose.Tag = "Key_Close";
            this.btnSearch.Tag = "Key_SearchCaption";
            this.btnClearFilters.Tag = "Key_ClearFilters";

            this.lblSite.Tag = "Key_RG_Site";
            this.lblStartDate.Tag = "Key_StartDateColon";
            this.lblEndDate.Tag = "Key_EndDateColon";
            this.lblTypeOfEvent.Tag = "Key_EventType";

            this.rdnSiteLevel.Tag = "Key_SiteLevelEvents";
            this.rdnLocal.Tag = "Key_LocalEvents";

            this.chkEndDateTime.Tag = "Key_SelectEndDateTime";
            this.chkShowAutoClosedEvents.Tag = "Key_ShowAutoCloseEvents";
        }

        private void ReadResources()
        {
            sLessThanDouble = this.GetResourceTextByKey("Key_LessThanDouble");
            sGreaterThanDouble = this.GetResourceTextByKey("Key_GreaterThanDouble");
            sAny = this.GetResourceTextByKey("Key_Any");
            sNoneHyphen = this.GetResourceTextByKey("Key_NoneHyphen");
            sHideFilter = this.GetResourceTextByKey("Key_HideFilter");
        }

        private void loadFilter()
        {
            List<EventTypes> lstEventTypes = new List<EventTypes>();

            dColumnNames = new Dictionary<string, string>();

            dColumnNames.Add("Site_name", this.GetResourceTextByKey("Key_SiteName"));
            dColumnNames.Add("Position", this.GetResourceTextByKey("Key_Position"));
            dColumnNames.Add("Game_Title", this.GetResourceTextByKey("Key_RC_GameTitle"));
            dColumnNames.Add("Date_and_time_of_event", this.GetResourceTextByKey("Key_DateAndTime"));
            dColumnNames.Add("Description_of_event", this.GetResourceTextByKey("Key_RC_EventType"));
            dColumnNames.Add("Details_of_the_event", this.GetResourceTextByKey("Key_EventDetails"));
            dColumnNames.Add("Event_Auto_Closed", this.GetResourceTextByKey("Key_EventAutoClosed"));

            try
            {
                lstEventTypes.Add(new EventTypes() { E_ID = 5, E_Name = sAny });
                lstEventTypes.Add(new EventTypes() { E_ID = 1, E_Name =  this.GetResourceTextByKey("Key_Door")});
                lstEventTypes.Add(new EventTypes() { E_ID = 2, E_Name =  this.GetResourceTextByKey("Key_Communications")});
                lstEventTypes.Add(new EventTypes() { E_ID = 3, E_Name =  this.GetResourceTextByKey("Key_Power")});
                lstEventTypes.Add(new EventTypes() { E_ID = 4, E_Name =  this.GetResourceTextByKey("Key_Fault")});

                _lstSiteDetails = _objEventViewerBiz.GetSiteDetails();
            }
            catch (Exception Ex)
            {
                _lstSiteDetails = new List<SitesResult>();
                ExceptionManager.Publish(Ex);
            }

            cmbEventType.DisplayMember = "E_Name";
            cmbEventType.ValueMember = "E_ID";
            cmbEventType.DataSource = lstEventTypes;

            if (_lstSiteDetails.Count > 0)
                _lstSiteDetails.Insert(0, new SitesResult() { Site_ID = 0, Site_Name = sAny });
            else
                _lstSiteDetails.Add(new SitesResult() { Site_ID = -1, Site_Name = sNoneHyphen });

            cmbSite.DisplayMember = "Site_Name";
            cmbSite.ValueMember = "Site_ID";
            cmbSite.DataSource = _lstSiteDetails;
        }

        private void refreshFilters()
        {
            cmbEventType.SelectedIndex = cmbSite.SelectedIndex = 0;
            chkShowAutoClosedEvents.Checked = chkEndDateTime.Checked = false;
            dtFromDate.Value = dtToDate.Value = DateTime.Now;
            btnHideFilter.Text = sLessThanDouble + " " + sHideFilter;
        }

        private bool ValidateControl()
        {
            bool bResult = true;

            if (dtFromDate.Value > DateTime.Now)
            {
                Win32Extensions.ShowWarningMessageBox(this, "Start date & time cannot be greater than current date & time");
                bResult = false;
            }

            if (bResult)
                if (chkEndDateTime.Checked)
                {
                    if (dtFromDate.Value > dtToDate.Value)
                    {
                        Win32Extensions.ShowWarningMessageBox(this, "Start date & time cannot be greater than end date & time");
                        bResult = false;
                    }

                    if (bResult)
                        if (dtToDate.Value > DateTime.Now)
                        {
                            Win32Extensions.ShowWarningMessageBox(this, "End date & time cannot be greater than current date & time");
                            bResult = false;
                        }
                }

            return bResult;
        }

        private void SearchEvents()
        {
            try
            {
                DateTime startDate, endDate;
                
                int iSiteID = 0;
                int iEventType = 0;

                startDate = dtFromDate.Value;
                endDate = chkEndDateTime.Checked ? dtToDate.Value : DateTime.Now;
                
                iSiteID = ((SitesResult)cmbSite.SelectedItem).Site_ID;
                iEventType = (int)cmbEventType.SelectedValue;

                if (iSiteID > -1)
                {
                    using (IExecutorService _exec = ExecutorServiceFactory.CreateExecutorService())
                    {
                        Win32Extensions.ShowAsyncDialog(this, "Loading event details...", _exec,
                            (o) =>
                            {
                                if (rdnLocal.Checked)
                                {
                                    _lstEnterpriseEvents = _objEventViewerBiz.GetEnterpriseEvents(startDate, endDate, iSiteID);
                                }
                                else
                                {
                                    _lstSiteEvents = _objEventViewerBiz.GetSiteEvents(startDate, endDate, iSiteID, iEventType, chkShowAutoClosedEvents.Checked ? 1 : 0);
                                }
                                
                                BindtoDataGrid(o);
                            });
                    }
                }

                if (dgEventViewer.Rows.Count < 1)
                    this.ShowInfoMessageBox("There are no events available for selected filter criteria.");
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void BindtoDataGrid(IAsyncProgress O)
        {
            DataTable dtEventDetails = null;

            if (rdnLocal.Checked)
            {
                if (_lstEnterpriseEvents.Count > 0)

                    dtEventDetails = DataTableConvertor.ToDataTable(_lstEnterpriseEvents, dColumnNames);
                else
                {
                    dtEventDetails = null;
                }
            }
            else
            {
                if (_lstSiteEvents.Count > 0)
                    dtEventDetails = DataTableConvertor.ToDataTable(_lstSiteEvents, dColumnNames);
                else
                {
                    dtEventDetails = null;
                }
            }

            if (dtEventDetails != null)
            {
                int iCounter = 0;

                //Set the data to the Grid by adding columns and rows manually.
                foreach (DataColumn column in dtEventDetails.Columns)
                {
                    O.CrossThreadInvoke(() =>
                    {
                        dgEventViewer.Columns.Add(column.ColumnName, column.ColumnName);
                    });
                }

                iCounter = 0;
                
                int index = -1;
                
                foreach (DataRow row in dtEventDetails.Rows)
                {
                    DataGridViewCellStyle dgStyle = new DataGridViewCellStyle();

                    O.UpdateStatus("Loading... " + ((iCounter++ * 100.00) / dtEventDetails.Rows.Count).ToString("##0") + "% Completed");
                    
                    O.CrossThreadInvoke(() =>
                    {
                        index = dgEventViewer.Rows.Add(row.ItemArray);
                        if (rdnSiteLevel.Checked)
                        {
                            switch (row["Event Type"].ToString().ToUpper())
                            {
                                case "DOOR":
                                    dgStyle.ForeColor = Color.Blue;
                                    break;
                                case "COMMUNICATIONS":
                                    dgStyle.ForeColor = Color.Red;
                                    dgStyle.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
                                    break;
                                case "POWER":
                                    dgStyle.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
                                    break;
                            }
                        }
                        else
                        {
                            switch (row["Event Details"].ToString().ToUpper())
                            {
                                case "SITE COMMS FAILURE":
                                    dgStyle.ForeColor = Color.Red;
                                    dgStyle.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
                                    break;
                                default:

                                    dgStyle.ForeColor = Color.Green;
                                    dgStyle.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
                                    break;
                            }
                        }
                        
                        dgEventViewer.Rows[index].DefaultCellStyle = dgStyle;
                    });
                    Thread.Sleep(2);
                }
            }
        }

        #endregion UserMethods
    }

    #region DefaultData
    static class DataTableConvertor
    {
        public static DataTable ToDataTable<T>(this IList<T> list, Dictionary<string, string> dColumnNames)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            
            DataTable table = new DataTable();
            string sColumn = string.Empty;

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];

                try
                {
                    sColumn = dColumnNames[prop.Name];
                }
                catch
                { sColumn = prop.Name; }

                table.Columns.Add(sColumn, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            object[] values = new object[props.Count];

            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                }
                
                table.Rows.Add(values);
            }
            return table;
        }
    }
    class EventTypes
    {
        public int E_ID { get; set; }
        public string E_Name { get; set; }
    }
    #endregion DefaultData
}
