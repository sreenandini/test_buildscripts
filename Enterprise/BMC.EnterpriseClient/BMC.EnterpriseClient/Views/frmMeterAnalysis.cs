using BMC.Common;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;

namespace BMC.EnterpriseClient
{
    public partial class frmMeterAnalysis : Form
    {
        #region Constant
        const string CO = "CO", SC = "SC", RE = "RE", AR = "AR", DI = "DI", SI = "SI";
        #endregion Constant
        #region Objects
        MeterAnalysisBiz oMeterAnalysisBiz = null;

        List<Operators> lstOperator = null;
        List<Manufacturers> lstManufacturer = null;
        List<Machine_Types> lstMachine_Types = null;
        List<GameCategory> lstGameCategory = null;
        List<GameTitle> lstGameTitle = null;
        List<BaseDenom> lstDenom = null;
        List<Payout> lstPayout = null;
        List<OrganisationHierarchy> lstOrganisationHierarchy = null;
        VSDepotsEntity lstDepots = null;

        MeterAnalysisParams oMAParams = null;

        DataTable dtMasterData = new DataTable();
        DataTable dtGraphData = new DataTable();
        Hashtable hstQrderByName = new Hashtable();

        String[] ForCmbRecords = null;
        String[] ForGroupBy = null;
        String[] ForDatatype = null;
        String[] ForCriteria = null;

        private IAsyncProgress2 _syncProgress = new SyncAsyncProgress();

        static ArrayList objCollection = new ArrayList();

        private System.Windows.Forms.DataVisualization.Charting.Chart _activeGraph = null;

        private ListSortDirection direction = ListSortDirection.Descending;

        #endregion Objects

        #region Variables
        int iSiteId = 0, iDistrictId = 0, iAreaId = 0, iRegionId = 0, iSubCompanyId = 0, iCompanyId = 0;
        int iUserID = 0;
        int iSelectedIndexMeterDetails = 1;
        bool _fullScreen = false;
        bool _bGame_TitleColExists;
        string sErrMsg = string.Empty;
        string LastSort = string.Empty;
        string strGraphPeriod = string.Empty;
        string naText, allCapsText, noneText = string.Empty;
        string grpBySUBCOMPANY, grpByREGION, grpByAREA, grpByDISTRICT, grpBySITE, grpByZONE, grpByPOSITION, grpByCATEGORY, grpByTYPE, grpByOPERATOR, grpByDEPOT, grpByGAMETITLE, grpByGAMEASSET, grpByASSET = string.Empty;
        string hdrCompany, hdrSubCompany, hdrRegion, hdrArea, hdrDistrict, hdrSite, hdrSiteCode, hdrZone, hdrType, hdrGameTitle, hdrInstallation, hdrCasinoWin = string.Empty;
        string hdrManufacturer, hdrCategory, hdrDepot, hdrOperator, hdrPosition, hdrAsset = string.Empty;
        string recCntTop20Perc, recCntBottom20Perc = string.Empty;
        string typeAvgDailyWin, typeCasinoWin, typeGamesBet, typeHandle = string.Empty;
        string ordbyHandle, ordbyNetwin, ordbyGamesPlayed, ordbyDOF, ordbyAvgBet, ordbyAvgDailyWin, ordbyQty, ordbyAvgGamesPlyd, ordbyOccupancy, ordbyTheoNetWin, ordbyTheoPerc, ordbyActPerc, ordbyPercVar = string.Empty;
        #endregion Variables

        #region Constructor
        public frmMeterAnalysis(int UserID)
        {
            iUserID = UserID;
            InitializeComponent();
            oMeterAnalysisBiz = MeterAnalysisBiz.CreateInstance();
            allCapsText = this.GetResourceTextByKey("Key_ALLCaps");
            naText = this.GetResourceTextByKey("Key_NA");
            noneText = this.GetResourceTextByKey("Key_None");
            SetTagProperty();

        }
        #endregion Constructor

        #region Property

        public bool FullScreen
        {
            get { return _fullScreen; }
            
            set
            {
                _fullScreen = value;
                tblMainFrame.RowStyles[0].Height = (value ? 100 : 0);
                tblMainFrame.RowStyles[1].Height = (!value ? 100 : 0);

                _activeGraph = (value ? axMSChartMeterGraphFull : axMSChartMeterGraph);

                axMSChartMeterGraphFull.Visible = value;

                tblContent.Visible = !value;

                btnProcess.Visible = !value;
                btnExport.Visible = !value;

                tblButtonLowerPannel.ColumnStyles[0].Width = (!value && tblOrganisationView.Visible ? 300 : 0);
                tblButtonLowerPannel.ColumnStyles[1].Width = (!value ? 120 : 0);
                tblButtonLowerPannel.ColumnStyles[2].Width = (!value ? 120 : 0);

                if (value)
                {
                    using (IExecutorService srv = ExecutorServiceFactory.CreateExecutorService())
                    {
                        Win32Extensions.ShowAsyncDialog(this.ParentForm, "Loading: Populating graph...", srv, (o) =>
                        {
                            BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;

                            this.LoadGraphData(o2);
                        });
                    }
                }
            }
        }

        public string Grid_SP_Name { get; set; }
        public string Graph_SP_Name { get; set; }

        #endregion Property

        #region Events

        private void chkActiveSites_CheckedChanged(object sender, EventArgs e)
        {
            BuildOrganisationHierarchy();
        }

        private void frmMeterAnalysis_Load(object sender, EventArgs e)
        {

            try
            {
                this.ResolveResources();

                ReadResources();

                LoadForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void tvSiteDetails_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            string strNodeKey = string.Empty;
            int iNodeID = -1;

            try
            {
                strNodeKey = e.Node.Name.Substring(0, 2);

                OrganisationHierarchy oOrganisationHierarchy = null;

                int iLastIndex = e.Node.Name.LastIndexOf("#") + 1;

                iCompanyId = 0;
                iSubCompanyId = 0;
                iRegionId = 0;
                iAreaId = 0;
                iDistrictId = 0;
                iSiteId = 0;

                if (iLastIndex > 0)
                {
                    iNodeID = Convert.ToInt32(e.Node.Name.Split('#')[1]);

                    LoadGroupBy(strNodeKey);

                    oOrganisationHierarchy = (OrganisationHierarchy)e.Node.Tag;

                    switch (strNodeKey)
                    {
                        case CO:

                            txtRegion.Text = oOrganisationHierarchy.Sub_Company_Region_ID > 0 ? allCapsText : naText;
                            txtCompany.Text = oOrganisationHierarchy.Company_Name;
                            txtSubCompany.Text = allCapsText;
                            txtSite.Text = allCapsText;

                            iCompanyId = oOrganisationHierarchy.Company_ID;

                            break;

                        case SC:

                            txtRegion.Text = oOrganisationHierarchy.Sub_Company_Region_ID > 0 ? allCapsText : naText;
                            txtCompany.Text = oOrganisationHierarchy.Company_Name;
                            txtSubCompany.Text = oOrganisationHierarchy.Sub_Company_Name;
                            txtSite.Text = allCapsText;

                            iCompanyId = oOrganisationHierarchy.Company_ID;
                            iSubCompanyId = oOrganisationHierarchy.Sub_Company_ID;

                            break;

                        case RE:

                            txtCompany.Text = oOrganisationHierarchy.Company_Name;
                            txtSubCompany.Text = oOrganisationHierarchy.Sub_Company_Name;
                            txtRegion.Text = oOrganisationHierarchy.Sub_Company_Region_ID > 0 ? oOrganisationHierarchy.Sub_Company_Region_Name : naText;
                            txtSite.Text = allCapsText;

                            iCompanyId = oOrganisationHierarchy.Company_ID;
                            iSubCompanyId = oOrganisationHierarchy.Sub_Company_ID;
                            iRegionId = (int)oOrganisationHierarchy.Sub_Company_Region_ID;

                            break;

                        case AR:

                            txtCompany.Text = oOrganisationHierarchy.Company_Name;
                            txtSubCompany.Text = oOrganisationHierarchy.Sub_Company_Name;
                            txtRegion.Text = oOrganisationHierarchy.Sub_Company_Region_ID > 0 ? oOrganisationHierarchy.Sub_Company_Region_Name : naText;
                            txtSite.Text = allCapsText;

                            iCompanyId = oOrganisationHierarchy.Company_ID;
                            iSubCompanyId = oOrganisationHierarchy.Sub_Company_ID;
                            iRegionId = (int)oOrganisationHierarchy.Sub_Company_Region_ID;
                            iAreaId = (int)oOrganisationHierarchy.Sub_Company_Area_ID;

                            break;

                        case DI:

                            txtCompany.Text = oOrganisationHierarchy.Company_Name;
                            txtSubCompany.Text = oOrganisationHierarchy.Sub_Company_Name;
                            txtRegion.Text = oOrganisationHierarchy.Sub_Company_Region_ID > 0 ? oOrganisationHierarchy.Sub_Company_Region_Name : naText;
                            txtSite.Text = allCapsText;

                            iCompanyId = oOrganisationHierarchy.Company_ID;
                            iSubCompanyId = oOrganisationHierarchy.Sub_Company_ID;
                            iRegionId = (int)oOrganisationHierarchy.Sub_Company_Region_ID;
                            iAreaId = (int)oOrganisationHierarchy.Sub_Company_Area_ID;
                            iDistrictId = (int)oOrganisationHierarchy.Sub_Company_District_ID;

                            break;

                        case SI:
  
                            txtCompany.Text = oOrganisationHierarchy.Company_Name;
                            txtSubCompany.Text = oOrganisationHierarchy.Sub_Company_Name;
                            txtRegion.Text = oOrganisationHierarchy.Sub_Company_Region_ID > 0 ? oOrganisationHierarchy.Sub_Company_Region_Name : naText;
                            txtSite.Text = oOrganisationHierarchy.Site_Name;

                            iCompanyId = oOrganisationHierarchy.Company_ID;
                            iSubCompanyId = oOrganisationHierarchy.Sub_Company_ID;
                            iRegionId = (int)oOrganisationHierarchy.Sub_Company_Region_ID;
                            iAreaId = (int)oOrganisationHierarchy.Sub_Company_Area_ID;
                            iDistrictId = (int)oOrganisationHierarchy.Sub_Company_District_ID;
                            iSiteId = oOrganisationHierarchy.Site_ID;

                            break;

                    }
                }
                else
                {
                    txtCompany.Text = allCapsText;
                    txtSubCompany.Text = allCapsText;
                    txtRegion.Text = allCapsText;
                    txtSite.Text = allCapsText;
                }

                tvSiteDetails.SelectedNode = e.Node;
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            finally
            { System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default; }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGameTitle();
        }

        private void cmbOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDepot();
        }

        private void cmbGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDenom();
        }

        private void cmbDenom_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPayout();
        }

        private void cmbRecordCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCriteria();
            if (cmbRecordCount.Text == allCapsText)
            {
                cmbCriteria.Enabled = false;
            }
            else
            {
                cmbCriteria.Enabled = true;
            }
        }

        private void rdnGame_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                Grid_SP_Name = rdnSlot.Checked ? "rsp_GetSlotLevelMeterAnalysisData" : "rsp_GetGameLevelMeterAnalysisData";
                Graph_SP_Name = rdnSlot.Checked ? "rsp_GetSlotLevelMeterAnalysisGraph" : "rsp_GetGameLevelMeterAnalysisGraph";
                LoadGroupBy(tvSiteDetails.SelectedNode.Name.Substring(0, 2));
                ResetFiltersDetails();
                ShowGameOptions();
            }
        }

        private void cmbDatatype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                if (dgMeterDetails.Rows.Count > 0)
                {
                    _activeGraph.Legends[0].Title = cmbDatatype.Text;
                    using (IExecutorService srv = ExecutorServiceFactory.CreateExecutorService())
                    {
                        Win32Extensions.ShowAsyncDialog(this.ParentForm, "Loading: Populating graph...", srv, (o) =>
                        {
                            BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;

                            LoadGraphData(o2);
                        });
                    }

                    if (objCollection.Count == 0)
                    {
                        this.ShowErrorMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_NO_GRAPH_DATA"), strGraphPeriod), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }

        }

        private void cmbColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string sColor = ((ComboBox)sender).Items[e.Index].ToString();
                Color c = Color.FromName(sColor);
                Brush b = new SolidBrush(c);
                g.FillRectangle(b, rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        private void cmbAvgColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sColor = this.cmbAvgColor.SelectedItem.ToString();
            _activeGraph.Series["Average"].Color = Color.FromName(sColor);
        }

        private void cmbSelectColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sColor = this.cmbSelectColor.SelectedItem.ToString();
            _activeGraph.Series["Selected"].Color = Color.FromName(sColor);
        }

        private void dgMeterDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0 || e.RowIndex == iSelectedIndexMeterDetails)
            {
                dgMeterDetails.Rows[iSelectedIndexMeterDetails].Selected = true;
            }
        }

        private void dgMeterDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                if (e.RowIndex == 0 || e.RowIndex == iSelectedIndexMeterDetails)
                {
                    dgMeterDetails.Rows[iSelectedIndexMeterDetails].Selected = true;
                }
                else
                {
                    //Set the selected row value.
                    iSelectedIndexMeterDetails = e.RowIndex;

                    using (IExecutorService srv = ExecutorServiceFactory.CreateExecutorService())
                    {
                        Win32Extensions.ShowAsyncDialog(this.ParentForm, "Loading: Populating graph...", srv, (o) =>
                        {
                            BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;

                            //Reload Graph with the selected data.
                            LoadGraphData(o2);
                        });
                    }

                    if (objCollection.Count == 0)
                    {
                        this.ShowErrorMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_NO_GRAPH_DATA"), strGraphPeriod), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void dgMeterDetails_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && dgMeterDetails.Rows.Count > 2)
            {
                object[] values = new object[dgMeterDetails.Rows[0].Cells.Count];

                try
                {
                    for (int i = 0; i < dgMeterDetails.Rows[0].Cells.Count; i++)
                    {
                        if (dgMeterDetails.Rows[0].Cells[i] != null)
                            values[i] = dgMeterDetails.Rows[0].Cells[i].Value;
                    }

                    dgMeterDetails.Rows.RemoveAt(0);

                    if (LastSort == dgMeterDetails.Columns[e.ColumnIndex].HeaderText)
                    {
                        if (direction == ListSortDirection.Descending)
                        {
                            dgMeterDetails.Sort(dgMeterDetails.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                            direction = ListSortDirection.Ascending;
                        }
                        else
                        {
                            dgMeterDetails.Sort(dgMeterDetails.Columns[e.ColumnIndex], ListSortDirection.Descending);
                            direction = ListSortDirection.Descending;
                        }
                    }
                    else
                    {
                        LastSort = dgMeterDetails.Columns[e.ColumnIndex].HeaderText;
                        dgMeterDetails.Sort(dgMeterDetails.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                        direction = ListSortDirection.Ascending;
                    }

                    dgMeterDetails.Rows[dgMeterDetails.SelectedRows[0].Index].Selected = true;
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                }

                DataGridViewCellStyle dgStyle = new DataGridViewCellStyle();

                dgStyle.ForeColor = Color.DarkBlue;
                dgStyle.SelectionBackColor = Color.Silver;
                dgStyle.SelectionForeColor = Color.DarkBlue;

                dgMeterDetails.Rows.Insert(0, values);
                dgMeterDetails.Rows[0].DefaultCellStyle = dgStyle;
                dgMeterDetails.Rows[0].Frozen = true;
            }
        }

        private void rdnGraphPeriod_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton trdnButton = (RadioButton)sender;
                
                if (!trdnButton.Checked)
                    return;

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                
                if (dgMeterDetails.Rows.Count > 0)
                {
                    if (SetParamValues())
                    {
                        using (IExecutorService srv = ExecutorServiceFactory.CreateExecutorService())
                        {
                            Win32Extensions.ShowAsyncDialog(this.ParentForm, "Loading: Populating graph...", srv, (o) =>
                            {
                                BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;

                                o2.CrossThreadInvoke(() =>
                                {
                                    _activeGraph.Visible = false;
                                });

                                dtGraphData = oMeterAnalysisBiz.GetMeterAnalysisData(oMAParams, Graph_SP_Name, rdnGame.Checked);

                                //Reload Graph.
                                LoadGraphData(o2);
                            });
                        }

                        if (objCollection.Count == 0)
                        {
                            this.ShowErrorMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_NO_GRAPH_DATA"), strGraphPeriod), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void rdnChartType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!((RadioButton)sender).Checked)
                    return;

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                if (dgMeterDetails.Rows.Count > 0)
                {
                    using (IExecutorService srv = ExecutorServiceFactory.CreateExecutorService())
                    {
                        Win32Extensions.ShowAsyncDialog(this.ParentForm, "Loading: Populating graph...", srv, (o) =>
                        {
                            BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;

                            LoadGraphData(o2);
                        });
                    }

                    if (objCollection.Count == 0)
                    {
                        this.ShowErrorMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_NO_GRAPH_DATA"), strGraphPeriod), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            bool bResult = true;

            if (dtpTo.Value < dtpFrom.Value)
            {
                Win32Extensions.ShowErrorMessageBox(this.GetResourceTextByKey("Key_MeterAnalysis_DatePickerTo_Error"));
                dtpTo.Focus();
                SendKeys.Send("%{DOWN}");
            }
            else
            {
                if (SetParamValues())
                {
                    using (IExecutorService srv = ExecutorServiceFactory.CreateExecutorService())
                    {
                        Win32Extensions.ShowAsyncDialog(this.ParentForm, "Loading: Fetching meter analysis details...", srv, (o) =>
                        {
                            BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;

                            o2.CrossThreadInvoke(() =>
                                {
                                    dgMeterDetails.Rows.Clear();
                                    dgMeterDetails.Columns.Clear();
                                    _activeGraph.Visible = false;
                                });

                            bResult = LoadMeterAnalysisData(o2);

                            if (bResult)
                            {
                                o2.UpdateStatus("Loading: Populating data grid...");
                                if (LoadDataGrid(dgMeterDetails, o2))
                                {
                                    LoadDataGrid(dgExportMADetails, o2);
                                    o2.UpdateStatus("Loading: Populating graph...");
                                    LoadGraphData(o2);
                                }
                            }
                        });

                        if (!bResult)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                        }

                        if (bResult && dgMeterDetails.RowCount > 0 && objCollection.Count == 0)
                        {
                            this.ShowErrorMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_NO_GRAPH_DATA"), strGraphPeriod), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                        }
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

            try
            {
                bool bResult = false;

                dgExportMADetails.Rows.Clear();

                if (SetParamValues())
                {
                    bResult = true;

                    using (IExecutorService srv = ExecutorServiceFactory.CreateExecutorService())
                    {
                        Win32Extensions.ShowAsyncDialog(this.ParentForm, "Loading: Fetching meter analysis details...", srv, (o) =>
                        {
                            BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;
                            bResult = LoadMeterAnalysisData(o2);
                            if (bResult)
                            {
                                LoadDataGrid(dgExportMADetails, o2);
                            }
                        });
                    }

                    if (!bResult)
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                    }
                }

                if (dgExportMADetails.Rows.Count > 1)
                {
                    Win32Extensions.ExportControlDataToExcel<object>(this, dgExportMADetails, null, true, false, true);
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            finally
            { System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default; }
        }

        private void btnZoomGraph_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgMeterDetails.Rows.Count > 0)
                {
                    if (objCollection.Count == 0)
                    {
                        this.ShowErrorMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_NO_GRAPH_DATA"), strGraphPeriod), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                    }
                    else
                        this.FullScreen = !this.FullScreen ? true : false;
                }
                else
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_LOAD_GRAPH_WITHMULTIPLE_RECORDS"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                    this.FullScreen = false;
                }
            }
            catch
            {
                this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_ERROR_ZOOM_GRAPH"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                this.FullScreen = false;
            }
        }

        private void chkShowDataValue_CheckedChanged(object sender, EventArgs e)
        {
            if (!_activeGraph.Visible)
                return;

            using (IExecutorService srv = ExecutorServiceFactory.CreateExecutorService())
            {
                Win32Extensions.ShowAsyncDialog(this.ParentForm, "Loading: Populating graph...", srv, (o) =>
                {
                    BMC.CoreLib.Win32.IAsyncProgress2 o2 = o as BMC.CoreLib.Win32.IAsyncProgress2;
                    
                    LoadGraphData(o2);
                });
            }
        }

        private void btnHideTree_Click(object sender, EventArgs e)
        {
            tblOrganisationView.Visible = !tblOrganisationView.Visible;
            tblContent.ColumnStyles[0].Width = (tblOrganisationView.Visible ? 300 : 0);
            tblButtonLowerPannel.ColumnStyles[0].Width = (tblOrganisationView.Visible ? 300 : 0);            
            btnHideTree.ImageKey = (tblOrganisationView.Visible ? "MovePrev" : "MoveNext");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (axMSChartMeterGraphFull.Visible)
            {
                this.FullScreen = false;
            }
            else
            {
                this.Close();
            }
        }

        private void frmMeterAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (axMSChartMeterGraphFull.Visible)
            {
                e.Cancel = true;
                this.FullScreen = false;
            }
        }

        #endregion Events

        #region User Defind Methods

        private void ReadResources()
        {
            //Data Type Combo
            typeAvgDailyWin = this.GetResourceTextByKey("Key_AvgDailyWin").ToUpper();
            typeCasinoWin = this.GetResourceTextByKey("Key_CasinoWin").ToUpper();
            typeGamesBet = this.GetResourceTextByKey("Key_GamesBet").ToUpper();
            typeHandle = this.GetResourceTextByKey("Key_Handle").ToUpper();

            //Records Combo
            recCntTop20Perc = this.GetResourceTextByKey("Key_Top20Perc");
            recCntBottom20Perc = this.GetResourceTextByKey("Key_Bottom20Perc");

            //Group By Combo
            grpBySUBCOMPANY = this.GetResourceTextByKey("Key_SubCompanyCaps");
            grpByREGION = this.GetResourceTextByKey("Key_RegionCaps");
            grpByAREA = this.GetResourceTextByKey("Key_AreaCaps");
            grpByDISTRICT = this.GetResourceTextByKey("Key_DistrictCaps");
            grpBySITE = this.GetResourceTextByKey("Key_SiteCaps");
            grpByZONE = this.GetResourceTextByKey("Key_ZoneCaps");
            grpByPOSITION = this.GetResourceTextByKey("Key_PositionCaps");
            grpByCATEGORY = this.GetResourceTextByKey("Key_CategoryCaps");
            grpByTYPE = this.GetResourceTextByKey("Key_TypeCaps");
            grpByOPERATOR = this.GetResourceTextByKey("Key_OperatorCaps");
            grpByDEPOT = this.GetResourceTextByKey("Key_DepotCaps");
            grpByGAMETITLE = this.GetResourceTextByKey("Key_GameTitleCaps");
            grpByGAMEASSET = this.GetResourceTextByKey("Key_GameAssetCaps");
            grpByASSET = this.GetResourceTextByKey("Key_AssetCaps");

            //Order By Combo
            ordbyHandle = this.GetResourceTextByKey("Key_Handle");
            ordbyNetwin = this.GetResourceTextByKey("Key_CasinoWinText");
            ordbyGamesPlayed = this.GetResourceTextByKey("Key_GamesPlayed");
            ordbyDOF = this.GetResourceTextByKey("Key_DOF");
            ordbyAvgBet = this.GetResourceTextByKey("Key_AvgBet");
            ordbyAvgDailyWin = this.GetResourceTextByKey("Key_AvgDailyWin");
            ordbyQty = this.GetResourceTextByKey("Key_Qty");
            ordbyAvgGamesPlyd = this.GetResourceTextByKey("Key_AvgGamesPlayed");
            ordbyOccupancy = this.GetResourceTextByKey("Key_Occupancy");
            ordbyTheoNetWin = this.GetResourceTextByKey("Key_TheoNetWin");
            ordbyTheoPerc = this.GetResourceTextByKey("Key_TheoPerc");
            ordbyActPerc = this.GetResourceTextByKey("Key_ActPerc");
            ordbyPercVar = this.GetResourceTextByKey("Key_VarPerc");

            //Grid Headers
            hdrCompany = this.GetResourceTextByKey("Key_Company");
            hdrSubCompany = this.GetResourceTextByKey("Key_SubCompany");
            hdrRegion = this.GetResourceTextByKey("Key_Region");
            hdrArea = this.GetResourceTextByKey("Key_Area");
            hdrDistrict = this.GetResourceTextByKey("Key_District");
            hdrSite = this.GetResourceTextByKey("Key_Site");
            hdrSiteCode = this.GetResourceTextByKey("Key_SiteCode");
            hdrZone = this.GetResourceTextByKey("Key_Zone");
            hdrType = this.GetResourceTextByKey("Key_Type");
            hdrGameTitle = this.GetResourceTextByKey("Key_GameTitle");
            hdrInstallation = this.GetResourceTextByKey("Key_Installation");
            hdrManufacturer = this.GetResourceTextByKey("Key_Manufacturer");
            hdrCategory = this.GetResourceTextByKey("Key_Category");
            hdrDepot = this.GetResourceTextByKey("Key_Depot");
            hdrOperator = this.GetResourceTextByKey("Key_Operator");
            hdrPosition = this.GetResourceTextByKey("Key_Position");
            hdrAsset = this.GetResourceTextByKey("Key_Asset");
            hdrCasinoWin = this.GetResourceTextByKey("Key_CasinoWinText");

        }

        private void LoadForm()
        {
            try
            {
                if (Thread.CurrentThread.CurrentCulture.Name == "en-GB")
                {
                    dtpTo.Format = dtpFrom.Format = DateTimePickerFormat.Custom;
                    dtpTo.CustomFormat = dtpFrom.CustomFormat = "dd/MM/yyyy";
                }
                else
                {
                    dtpTo.Format = dtpFrom.Format = DateTimePickerFormat.Custom;
                    dtpTo.CustomFormat = dtpFrom.CustomFormat = "MM/dd/yyyy";
                }

                dtpTo.MaxDate = DateTime.Today;
                dtpFrom.MaxDate = dtpTo.MaxDate;

                IAsyncProgress2 o = _syncProgress;
                o.UpdateStatus("Loading Organisation Details");

                o.CrossThreadInvoke(() =>
                {
                    BuildOrganisationHierarchy();
                });
                hstQrderByName.Add(ordbyHandle, "Handle");
                hstQrderByName.Add(ordbyNetwin, "Casino Win");
                hstQrderByName.Add(ordbyGamesPlayed, "Games Played");
                hstQrderByName.Add(ordbyDOF, "DOF");
                hstQrderByName.Add(ordbyAvgBet, "Avg Bet");
                hstQrderByName.Add(ordbyAvgDailyWin, "Avg Daily Win");
                hstQrderByName.Add(ordbyQty, "Qty");
                hstQrderByName.Add(ordbyAvgGamesPlyd, "Avg Games");
                hstQrderByName.Add(ordbyOccupancy, "Occupancy");
                hstQrderByName.Add(ordbyTheoNetWin, "Theo_Net_Win");
                hstQrderByName.Add(ordbyTheoPerc, "Theo %");
                hstQrderByName.Add(ordbyActPerc, "Act %");
                hstQrderByName.Add(ordbyPercVar, "% Var");

                o.UpdateStatus("Loading: Common Data...");

                o.CrossThreadInvoke(() =>
                {
                    LoadCommonData();
                });

                o.UpdateStatus("Loading: Combo Boxes...");

                o.CrossThreadInvoke(() =>
                {
                    GetTextForComboBoxes();
                });

                o.UpdateStatus("Loading: Group By...");

                o.CrossThreadInvoke(() =>
                {
                    LoadGroupBy(string.Empty);
                });

                o.CrossThreadInvoke(() =>
                {
                    Grid_SP_Name = rdnSlot.Checked ? "rsp_GetSlotLevelMeterAnalysisData" : "rsp_GetGameLevelMeterAnalysisData";
                    Graph_SP_Name = rdnSlot.Checked ? "rsp_GetSlotLevelMeterAnalysisGraph" : "rsp_GetGameLevelMeterAnalysisGraph";

                    ShowGameOptions();

                    this.FullScreen = false;

                    _activeGraph.Visible = false;
                });

                Type colorType = typeof(System.Drawing.Color);
                PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
                foreach (PropertyInfo c in propInfoList)
                {
                    this.cmbAvgColor.Items.Add(c.Name);
                    this.cmbSelectColor.Items.Add(c.Name);
                }

                cmbAvgColor.SelectedItem = Color.DarkRed.Name;
                cmbSelectColor.SelectedItem = Color.Gold.Name;
                oMAParams = new MeterAnalysisParams();
            }
            catch
            {
                throw;
            }
        }

        private void BuildOrganisationHierarchy()
        {
            int iOldSubComp = 0, iOldCompany = 0, iOldRegion = 0, iOldArea = 0, iOldSite = 0, iOldDistrict = 0;
            TreeNode tnAllCompNode = new TreeNode();
            tvSiteDetails.Nodes.Clear();
            try
            {

                tnAllCompNode = tvSiteDetails.Nodes.Add("ALL", this.GetResourceTextByKey("Key_AllCompanies"), 10, 10);
                tvSiteDetails.SelectedNode = tnAllCompNode;


                lstOrganisationHierarchy = oMeterAnalysisBiz.GetOrganisationHierarchy(!chkActiveSites.Checked ? 1 : 0, iUserID);

                if (lstOrganisationHierarchy.Count > 0)
                {
                    foreach (OrganisationHierarchy tOH in lstOrganisationHierarchy)
                    {
                        //Add company Node
                        if (iOldCompany != tOH.Company_ID)
                        {
                            TreeNode nodeCompany = tvSiteDetails.Nodes["ALL"].Nodes.Add(CO + ",#" + tOH.Company_ID.ToString(), tOH.Company_Name.ToString(), 0, 0);
                            nodeCompany.Tag = tOH;
                            iOldCompany = tOH.Company_ID;
                        }

                        //Add subcompany if available in record
                        if (iOldSubComp != tOH.Sub_Company_ID)
                        {
                            //find the parent where this node needs to be added
                            TreeNode tnNode = tvSiteDetails.Nodes.Find(CO + ",#" + tOH.Company_ID.ToString(), true)[0];
                            //if found add the node to parent
                            if (tnNode != null)
                            {
                                TreeNode nodeSubCompany = tnNode.Nodes.Add(SC + ",#" + tOH.Sub_Company_ID.ToString(), tOH.Sub_Company_Name.ToString(), 1, 1);
                                nodeSubCompany.Tag = tOH;
                                iOldSubComp = tOH.Sub_Company_ID;
                            }
                        }

                        //Add region node if available
                        if (iOldRegion != tOH.Sub_Company_Region_ID)
                        {
                            TreeNode[] tnNodeArray;
                            TreeNode tnNode = new TreeNode();
                            tnNodeArray = tvSiteDetails.Nodes.Find(SC + ",#" + tOH.Sub_Company_ID.ToString(), true);
                            if (tnNodeArray != null && tnNodeArray.Length > 0) { tnNode = tnNodeArray[0]; }
                            if (!string.IsNullOrEmpty(tnNode.Text) && !string.IsNullOrEmpty(tOH.Sub_Company_Region_Name.ToString()))
                            {
                                TreeNode nodeRegion = tnNode.Nodes.Add(RE + "," + tOH.Sub_Company_ID.ToString() + "#" + tOH.Sub_Company_Region_ID.ToString(), tOH.Sub_Company_Region_Name.ToString(), 2, 2);
                                nodeRegion.Tag = tOH;
                                iOldRegion = Convert.ToInt32(tOH.Sub_Company_Region_ID);
                            }
                        }

                        //Add area node if available
                        if (iOldArea != tOH.Sub_Company_Area_ID)
                        {
                            TreeNode[] tnNodeArray;
                            TreeNode tnNode = new TreeNode();
                            tnNodeArray = tvSiteDetails.Nodes.Find(RE + "," + tOH.Sub_Company_ID.ToString() + "#" + tOH.Sub_Company_Region_ID.ToString(), true);
                            if (tnNodeArray != null && tnNodeArray.Length > 0) { tnNode = tnNodeArray[0]; }
                            if (!string.IsNullOrEmpty(tnNode.Text) && !string.IsNullOrEmpty(tOH.Sub_Company_Area_Name.ToString()))
                            {
                                TreeNode nodeArea = tnNode.Nodes.Add(AR + "," + tOH.Sub_Company_ID.ToString() + "#" + tOH.Sub_Company_Region_ID.ToString() + "#" + tOH.Sub_Company_Area_ID.ToString(), tOH.Sub_Company_Area_Name.ToString(), 3, 3);
                                nodeArea.Tag = tOH;
                                iOldArea = Convert.ToInt32(tOH.Sub_Company_Area_ID);
                            }
                        }

                        //Add district node if available
                        if (iOldDistrict != tOH.Sub_Company_District_ID)
                        {
                            TreeNode[] tnNodeArray;
                            TreeNode tnNode = new TreeNode();
                            tnNodeArray = tvSiteDetails.Nodes.Find(AR + "," + tOH.Sub_Company_ID.ToString() + "#" + tOH.Sub_Company_Region_ID.ToString() + "#" + tOH.Sub_Company_Area_ID.ToString(), true);
                            if (tnNodeArray != null && tnNodeArray.Length > 0) { tnNode = tnNodeArray[0]; }
                            if (!string.IsNullOrEmpty(tnNode.Text) && !string.IsNullOrEmpty(tOH.Sub_Company_District_Name.ToString()))
                            {
                                TreeNode nodeDistrict = tnNode.Nodes.Add(DI + "," + tOH.Sub_Company_ID.ToString() + "#" + tOH.Sub_Company_Region_ID.ToString() + "#" + tOH.Sub_Company_Area_ID.ToString() + "#" + tOH.Sub_Company_District_ID.ToString(), tOH.Sub_Company_District_Name.ToString(), 4, 4);
                                nodeDistrict.Tag = tOH;
                                iOldDistrict = Convert.ToInt32(tOH.Sub_Company_District_ID);
                            }
                        }

                        //Add site to the respective nodes
                        if (iOldSite != tOH.Site_ID)
                        {
                            TreeNode tnNode;
                            if (tOH.Sub_Company_District_ID != 0)
                            {
                                tnNode = tvSiteDetails.Nodes.Find(DI + "," + tOH.Sub_Company_ID.ToString() + "#" + tOH.Sub_Company_Region_ID.ToString() + "#" + tOH.Sub_Company_Area_ID.ToString() + "#" + tOH.Sub_Company_District_ID.ToString(), true)[0];
                            }
                            else if (tOH.Sub_Company_Area_ID != 0)
                            {
                                tnNode = tvSiteDetails.Nodes.Find(AR + "," + tOH.Sub_Company_ID.ToString() + "#" + tOH.Sub_Company_Region_ID.ToString() + "#" + tOH.Sub_Company_Area_ID.ToString(), true)[0];
                            }
                            else if (tOH.Sub_Company_Region_ID != 0)
                            {
                                tnNode = tvSiteDetails.Nodes.Find(RE + "," + tOH.Sub_Company_ID.ToString() + "#" + tOH.Sub_Company_Region_ID.ToString(), true)[0];
                            }
                            else if (tOH.Sub_Company_ID != 0)
                            {
                                tnNode = tvSiteDetails.Nodes.Find(SC + ",#" + tOH.Sub_Company_ID.ToString(), true)[0];
                            }
                            else
                            {
                                tnNode = tvSiteDetails.Nodes.Find(CO + ",#" + tOH.Company_ID.ToString(), true)[0];
                            }

                            if (tnNode != null)
                            {
                                TreeNode nodeSite = tnNode.Nodes.Add(SI + ",#" + tOH.Site_ID.ToString(), tOH.Site_Name.ToString() + "[" + tOH.Site_Code.ToString() + "]", 5, 5);
                                nodeSite.Tag = tOH;
                            }
                        }
                    }

                    tvSiteDetails.Nodes["ALL"].Expand();
                }

                tvSiteDetails_NodeMouseClick(tvSiteDetails, new TreeNodeMouseClickEventArgs(tvSiteDetails.Nodes["ALL"], MouseButtons.Left, 1, 0, 0));
            }
            catch (Exception exFillSite)
            {
                LogManager.WriteLog("Error occured in loading the Org structure Tree." + "-Error Message-" + exFillSite.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void LoadCommonData()
        {
            oMeterAnalysisBiz.GetCommonData(out lstOperator, out lstMachine_Types, out lstManufacturer, out lstGameCategory);

            if (lstOperator.Count > 0)
                lstOperator.Insert(0, new Operators() { Operator_ID = 0, Operator_Name = allCapsText });
            else
                lstOperator.Insert(0, new Operators() { Operator_ID = 0, Operator_Name = noneText });

            if (lstMachine_Types.Count > 0)
                lstMachine_Types.Insert(0, new Machine_Types() { Machine_Type_ID = 0, Machine_Type_Code = allCapsText });
            else
                lstMachine_Types.Insert(0, new Machine_Types() { Machine_Type_ID = -1, Machine_Type_Code = noneText });

            if (lstManufacturer.Count > 0)
                lstManufacturer.Insert(0, new Manufacturers() { Manufacturer_ID = 0, Manufacturer_Name = allCapsText });
            else
                lstManufacturer.Insert(0, new Manufacturers() { Manufacturer_ID = -1, Manufacturer_Name = noneText });

            if (lstGameCategory.Count > 0)
                lstGameCategory.Insert(0, new GameCategory() { Game_Category_ID = 0, Game_Category_Name = allCapsText });
            else
                lstGameCategory.Insert(0, new GameCategory() { Game_Category_ID = -1, Game_Category_Name = noneText });

            cmbOperator.DisplayMember = "Operator_Name";
            cmbOperator.ValueMember = "Operator_ID";
            cmbOperator.DataSource = lstOperator;

            cmbType.DisplayMember = "Machine_Type_Code";
            cmbType.ValueMember = "Machine_Type_ID";
            cmbType.DataSource = lstMachine_Types;

            cmbManufacturer.DisplayMember = "Manufacturer_Name";
            cmbManufacturer.ValueMember = "Manufacturer_ID";
            cmbManufacturer.DataSource = lstManufacturer;

            cmbCategory.DisplayMember = "Game_Category_Name";
            cmbCategory.ValueMember = "Game_Category_ID";
            cmbCategory.DataSource = lstGameCategory;

        }

        private void LoadGameTitle()
        {
            if (rdnGame.Checked)
            {
                int iCategoryID = ((GameCategory)cmbCategory.SelectedItem).Game_Category_ID;
                lstGameTitle = oMeterAnalysisBiz.GetGameTitleForCategory(iCategoryID,true);
            }
            else
            {
                int iTypeID = ((Machine_Types)cmbType.SelectedItem).Machine_Type_ID;
                lstGameTitle = oMeterAnalysisBiz.GetGameTitleForCategory(iTypeID,false);
            }

            if (lstGameTitle.Count > 0)
                lstGameTitle.Insert(0, new GameTitle() { Game_Title_ID = 0, Game_Title = allCapsText});
            else
                lstGameTitle.Insert(0, new GameTitle() { Game_Title_ID = -1, Game_Title = noneText });

            cmbGame.DataSource = lstGameTitle;
            cmbGame.DisplayMember = "Game_Title";
            cmbGame.ValueMember = "Game_Title_ID";
        }

        private void LoadDepot()
        {
            int iOperatorID = ((Operators)cmbOperator.SelectedItem).Operator_ID;
            lstDepots = oMeterAnalysisBiz.GetDepots(iOperatorID);

            if (lstDepots.Count > 0)
            {
                lstDepots.Insert(0, new VSDepotEntity()
                {
                    Depot_ID = 0,
                    Depot_Name = allCapsText,
                });
            }
            else
            {
                lstDepots.Add(new VSDepotEntity()
                {
                    Depot_ID = 0,
                    Depot_Name = noneText
                });
            }

            cmbDepot.DataSource = lstDepots;
            cmbDepot.DisplayMember = "Depot_Name";
            cmbDepot.ValueMember = "Depot_ID";
        }

        private void LoadDenom()
        {
            try
            {
                int iGameTitleID = ((GameTitle)cmbGame.SelectedItem).Game_Title_ID;
                lstDenom = oMeterAnalysisBiz.GetBaseDenoms(iGameTitleID);

                cmbDenom.DisplayMember = "MGMD_Denom_Value";
                cmbDenom.ValueMember = "MGMD_Denom";

                if (lstDenom.Count > 0)
                {
                    lstDenom.Insert(0, new BaseDenom()
                                    {
                                        MGMD_Denom = 0,
                                        MGMD_Denom_Value = allCapsText
                                    });
                }
                else
                {
                    lstDenom.Insert(0, new BaseDenom()
                                    {
                                        MGMD_Denom = 0,
                                        MGMD_Denom_Value = noneText
                                    });
                }

                cmbDenom.DataSource = lstDenom;                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void LoadPayout()
        {
            try
            {
                int iMGMD_Denom = (int)((BaseDenom)cmbDenom.SelectedItem).MGMD_Denom;
                int iGameTitleID = (int)((GameTitle)cmbGame.SelectedItem).Game_Title_ID;
                
                lstPayout = oMeterAnalysisBiz.GetPayoutPercent(iMGMD_Denom, iGameTitleID);

                if (lstPayout.Count > 0)
                {
                    lstPayout.Insert(0, new Payout()
                                    {
                                        TheoPayout = 0,
                                        TheoreticalPayout = allCapsText
                                    });
                }
                else
                {
                    lstPayout.Insert(0, new Payout()
                                    {
                                        TheoPayout = 0,
                                        TheoreticalPayout = noneText
                                    });
                }

                cmbPayout.DataSource = lstPayout;
                cmbPayout.DisplayMember = "TheoreticalPayout";
                cmbPayout.ValueMember = "TheoPayout";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void GetTextForComboBoxes()
        {

            ForCmbRecords = new String[] {
                  allCapsText,
                  recCntBottom20Perc,
                  recCntTop20Perc};

            ForDatatype = new String[] {
                  typeAvgDailyWin.ToUpper(),
                  typeCasinoWin,
                  typeGamesBet,
                  typeHandle.ToUpper()};

            cmbRecordCount.DataSource = ForCmbRecords;
            cmbRecordCount.SelectedIndex = 0;

            cmbDatatype.DataSource = ForDatatype;
            cmbDatatype.SelectedIndex = 0;
        }

        private void LoadGroupBy(string sNodeSelected)
        {
            string SelectedText = string.Empty;

            cmbGroupBy.Items.Clear();

            switch (sNodeSelected)
            {
                case SC:

                    ForGroupBy = new String[]
                    { 
                        grpBySUBCOMPANY
                        , grpByREGION
                        , grpByAREA
                        , grpByDISTRICT
                        , grpBySITE
                        , grpByZONE
                        , grpByPOSITION
                        , grpByCATEGORY
                        , grpByTYPE
                        , grpByOPERATOR
                        , grpByDEPOT
                        , grpByGAMETITLE
                        , grpByGAMEASSET
                        , grpByASSET
                    };

                    SelectedText = grpBySUBCOMPANY;
                    break;

                case RE:
                    ForGroupBy = new String[]
                    {
                        grpByREGION
                        , grpByAREA
                        , grpByDISTRICT
                        , grpBySITE
                        , grpByZONE
                        , grpByPOSITION
                        , grpByCATEGORY
                        , grpByTYPE
                        , grpByOPERATOR
                        , grpByDEPOT
                        , grpByGAMETITLE
                        , grpByGAMEASSET
                        , grpByASSET
                    };

                    SelectedText = grpByREGION;
                    break;

                case AR:
                    ForGroupBy = new String[]
                    { 
                        grpByAREA
                        , grpByDISTRICT
                        , grpBySITE
                        , grpByZONE
                        , grpByPOSITION
                        , grpByCATEGORY
                        , grpByTYPE
                        , grpByOPERATOR
                        , grpByDEPOT
                        , grpByGAMETITLE
                        , grpByGAMEASSET
                        , grpByASSET
                    };

                    SelectedText = grpByAREA;
                    break;

                case DI:

                    ForGroupBy = new String[] 
                    {
                        grpByDISTRICT
                        , grpBySITE
                        , grpByZONE
                        , grpByPOSITION
                        , grpByCATEGORY
                        , grpByTYPE
                        , grpByOPERATOR
                        , grpByDEPOT
                        , grpByGAMETITLE
                        , grpByGAMEASSET
                        , grpByASSET
                    };

                    SelectedText = grpByDISTRICT;
                    break;

                case SI:

                    ForGroupBy = new String[]
                    { 
                        grpBySITE
                        , grpByZONE
                        , grpByPOSITION
                        , grpByCATEGORY
                        , grpByTYPE
                        , grpByOPERATOR
                        , grpByDEPOT
                        , grpByGAMETITLE
                        , grpByGAMEASSET
                        , grpByASSET
                    };

                    SelectedText = grpBySITE;
                    break;
                
                default:
                    ForGroupBy = new String[]
                    { 
                        grpBySUBCOMPANY
                        , grpByREGION
                        , grpByAREA
                        , grpByDISTRICT
                        , grpBySITE
                        , grpByZONE
                        , grpByPOSITION
                        , grpByCATEGORY
                        , grpByTYPE
                        , grpByOPERATOR
                        , grpByDEPOT
                        , grpByGAMETITLE
                        , grpByGAMEASSET
                        , grpByASSET
                    };

                    SelectedText = grpBySUBCOMPANY;
                    break;
            }

            cmbGroupBy.Items.AddRange(ForGroupBy);

            if (rdnSlot.Checked)
            {
                cmbGroupBy.Items.Remove(grpByCATEGORY);
                cmbGroupBy.Items.Remove(grpByGAMETITLE);
                cmbGroupBy.Items.Remove(grpByGAMEASSET);
            }

            cmbGroupBy.SelectedIndex = cmbGroupBy.FindStringExact(SelectedText);
        }

        private void LoadCriteria()
        {
            ForCriteria = new String[] { 
                ordbyPercVar,
                ordbyActPerc,
                ordbyAvgBet,
                ordbyAvgDailyWin,
                ordbyAvgGamesPlyd,
                ordbyDOF,
                ordbyGamesPlayed,
                ordbyHandle,
                ordbyNetwin,
                ordbyOccupancy,
                ordbyQty,
                ordbyTheoPerc,
                ordbyTheoNetWin};

            cmbCriteria.DataSource = ForCriteria;
            cmbCriteria.SelectedIndex = 0;
        }

        private void ShowGameOptions()
        {
            tblComboBoxPanel.RowStyles[10].Height = rdnGame.Checked? 30:0;
            lblPayout.Visible = lblDenom.Visible = lblCategory.Visible = rdnGame.Checked;
            cmbPayout.Visible = cmbDenom.Visible = cmbCategory.Visible = rdnGame.Checked;
        }

        private bool LoadMeterAnalysisData(BMC.CoreLib.Win32.IAsyncProgress2 o2)
        {
            bool bReturn = false;
            try
            {
                //Get grid data
                dtMasterData = oMeterAnalysisBiz.GetMeterAnalysisData(oMAParams, Grid_SP_Name, rdnGame.Checked);

                if (dtMasterData.Rows.Count > 0)
                {
                    //Get graph data
                    dtGraphData = oMeterAnalysisBiz.GetMeterAnalysisData(oMAParams, Graph_SP_Name, rdnGame.Checked);
                    bReturn = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in LoadMeterAnalysisData" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
            }

            return bReturn;
        }

        private bool SetParamValues()
        {
            bool bReturn = false;

            int iMaxMonths = 0;

            try
            {
                iMaxMonths = Convert.ToInt32(ConfigurationManager.AppSettings["MaxMonths"]);
            }
            catch
            {
                iMaxMonths = 12;
            }

            //Check if To-date is grater than From-Date
            if (dtpFrom.Value > dtpTo.Value)
            {
                this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_STARTDT_LESS_ENDDT"), this.GetResourceTextByKey(1, "MSG_APP_TITLE")); 
            }
            else
            {
                if ((12 * (dtpTo.Value.Year - dtpFrom.Value.Year)) + (dtpTo.Value.Month - dtpFrom.Value.Month) > iMaxMonths)
                {
                    this.ShowErrorMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_SEARCHRANGE_EXCEED"), iMaxMonths), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                    try
                    {
                        cmbCriteria.Enabled = cmbRecordCount.SelectedItem.ToString() != allCapsText;

                        string strFromDate = dtpFrom.Value.ToShortDateString();
                        string strToDate = dtpTo.Value.ToShortDateString();

                        oMAParams.StartDate = Convert.ToDateTime(strFromDate + " 00:00:00 AM");
                        oMAParams.EndDate = Convert.ToDateTime(strToDate + " 11:59:59 PM");

                        oMAParams.CompanyID = iCompanyId;
                        oMAParams.SubCompanyID = iSubCompanyId;
                        oMAParams.SiteID = iSiteId;
                        oMAParams.OperatorID = (int)cmbOperator.SelectedValue;
                        oMAParams.DepotID = (int)cmbDepot.SelectedValue;
                        oMAParams.Machine_TypeID = (int)cmbType.SelectedValue;
                        oMAParams.Game_CategoryID = (int)cmbCategory.SelectedValue;
                        oMAParams.Game_Title_ID = (int)cmbGame.SelectedValue;
                        oMAParams.Game_Title = cmbGame.SelectedIndex == 0 ? "ALL" : ((GameTitle)cmbGame.SelectedItem).Game_Title;
                        oMAParams.RegionID = iRegionId;
                        oMAParams.AreaID = iAreaId;
                        oMAParams.DistrictID = iDistrictId;
                        oMAParams.ManufacturerID = (int)cmbManufacturer.SelectedValue;
                        oMAParams.PeriodID = 0;
                        oMAParams.ActiveAsset = chkActive.Checked;
                        oMAParams.ActiveSites = chkActiveSites.Checked;

                        //Get Group by based on selected value
                        if (cmbGroupBy.SelectedItem.ToString() == grpBySUBCOMPANY)
                            oMAParams.GroupByClause = "Sub_Company_Name";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByREGION)
                            oMAParams.GroupByClause = "Region_ID";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByAREA)
                            oMAParams.GroupByClause = "Area_ID";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByDISTRICT)
                            oMAParams.GroupByClause = "District_ID";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpBySITE)
                            oMAParams.GroupByClause = "Site_Name";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByZONE)
                            oMAParams.GroupByClause = "Zone_Name";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByPOSITION)
                            oMAParams.GroupByClause = "Bar_Position_Name";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByCATEGORY)
                            oMAParams.GroupByClause = "Category_Code";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByTYPE)
                            oMAParams.GroupByClause = "Machine_Type_Code";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByOPERATOR)
                            oMAParams.GroupByClause = "Operator_Name";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByDEPOT)
                            oMAParams.GroupByClause = "Depot_Name";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByGAMETITLE)
                            oMAParams.GroupByClause = "Machine_Name";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByGAMEASSET)
                            oMAParams.GroupByClause = "Installation_ID";
                        else if (cmbGroupBy.SelectedItem.ToString() == grpByASSET)
                            oMAParams.GroupByClause = "Machine_Stock_No";
                        else
                            oMAParams.GroupByClause = string.Empty;

                        //Get Record Count and Order By
                        if (cmbRecordCount.SelectedItem.ToString() == recCntTop20Perc)
                        {
                            oMAParams.NoOfRecords = "Top20";
                            oMAParams.Order_By = hstQrderByName[cmbCriteria.Text].ToString();
                        }
                        else if (cmbRecordCount.SelectedItem.ToString() == recCntBottom20Perc)
                        {
                            oMAParams.NoOfRecords = "Bottom20";
                            oMAParams.Order_By = hstQrderByName[cmbCriteria.Text].ToString();
                        }
                        else
                            oMAParams.NoOfRecords = string.Empty;

                        //Format to SQL column
                        oMAParams.Order_By = (oMAParams.Order_By == ordbyTheoPerc) && rdnHold.Checked ? "Hold_Perc" : "Payout_Perc";
                        oMAParams.Order_By = (oMAParams.Order_By == ordbyActPerc) && rdnHold.Checked ? "Hold_Act_Perc" : "Payout_Act_Perc";
                        oMAParams.Order_By = (oMAParams.Order_By == ordbyPercVar) && rdnHold.Checked ? "Hold_Perc_Var" : "Payout_Perc_Var";

                        oMAParams.UserID = iUserID;

                        if (rdnDay.Checked)
                        { oMAParams.PeriodID = 1; strGraphPeriod = "Day";}
                        else if (rdnWeek.Checked)
                        { oMAParams.PeriodID = 2; strGraphPeriod = "Week";}
                        else
                        { oMAParams.PeriodID = 3; strGraphPeriod = "Period";}

                        if (rdnGame.Checked)
                        {
                            oMAParams.Denom = (int)cmbDenom.SelectedValue;
                            oMAParams.Payout = (float)cmbPayout.SelectedValue;
                        }

                        bReturn = true;
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error in setting parameter for calling SP " + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
                    }
                }
            }

            return bReturn;
        }

        private bool LoadDataGrid(DataGridView dgGridView, BMC.CoreLib.Win32.IAsyncProgress2 o2)
        {
            bool bReturn = false;

            DataTable dtMeterAnalysisGrid = null;

            dtMeterAnalysisGrid = SummarizeData(o2);

            if (dtMeterAnalysisGrid.Rows.Count > 0)
            {
                o2.CrossThreadInvoke(() =>
                {
                    DataGridViewCellStyle dgStyle = new DataGridViewCellStyle();
                    dgStyle.BackColor = Color.Silver;
                    dgStyle.ForeColor = Color.DarkBlue;
                    dgStyle.SelectionBackColor = Color.Silver;
                    dgStyle.SelectionForeColor = Color.DarkBlue;

                    dgGridView.Rows.Clear();
                    dgGridView.Columns.Clear();

                    int counter = 0;

                    //Set the data to the Grid by adding columns and rows manually.
                    foreach (DataColumn column in dtMeterAnalysisGrid.Columns)
                    {
                        o2.UpdateStatusProgress(counter++, "Creating Columns...");
                        dgGridView.Columns.Add(column.ColumnName, column.ColumnName);
                    }

                    counter = 0;
                    
                    foreach (DataRow row in dtMeterAnalysisGrid.Rows)
                    {
                        o2.UpdateStatusProgress(counter++, "Adding rows...");
                        dgGridView.Rows.Add(row.ItemArray);
                    }

                    dgGridView.Rows[0].DefaultCellStyle = dgStyle;

                    for (int iColIndex = 0; iColIndex < dgGridView.Columns.Count; iColIndex++)
                    {
                        dgGridView.Columns[iColIndex].Visible = false;
                    }

                    //set visibilty and header text for columns based on selected group by
                    if (cmbGroupBy.SelectedItem.ToString() == grpBySUBCOMPANY)
                    {
                        SetColumnHeaderText(dgGridView, "SubCompany", hdrSubCompany);
                        SetColumnHeaderText(dgGridView, "Company", hdrCompany);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByREGION)
                    {
                        SetColumnHeaderText(dgGridView, "Region", hdrRegion);
                        SetColumnHeaderText(dgGridView, "SubCompany", hdrSubCompany);
                        SetColumnHeaderText(dgGridView, "Company", hdrCompany);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByAREA)
                    {
                        SetColumnHeaderText(dgGridView, "Area", hdrArea);
                        SetColumnHeaderText(dgGridView, "SubCompany", hdrSubCompany);
                        SetColumnHeaderText(dgGridView, "Company", hdrCompany);
                        SetColumnHeaderText(dgGridView, "Region", hdrRegion);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByDISTRICT)
                    {
                        SetColumnHeaderText(dgGridView, "District", hdrDistrict);
                        SetColumnHeaderText(dgGridView, "SubCompany", hdrSubCompany);
                        SetColumnHeaderText(dgGridView, "Company", hdrCompany);
                        SetColumnHeaderText(dgGridView, "Region", hdrRegion);
                        SetColumnHeaderText(dgGridView, "Area", hdrArea);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpBySITE)
                    {
                        SetColumnHeaderText(dgGridView, "Site", hdrSite);
                        SetColumnHeaderText(dgGridView, "SiteCode", hdrSiteCode);
                        SetColumnHeaderText(dgGridView, "SubCompany", hdrSubCompany);
                        SetColumnHeaderText(dgGridView, "Company", hdrCompany);
                        SetColumnHeaderText(dgGridView, "Region", hdrRegion);
                        SetColumnHeaderText(dgGridView, "Area", hdrArea);
                        SetColumnHeaderText(dgGridView, "District", hdrDistrict);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByZONE)
                    {
                        SetColumnHeaderText(dgGridView, "Zone", hdrZone);
                        SetColumnHeaderText(dgGridView, "Company", hdrCompany);
                        SetColumnHeaderText(dgGridView, "SubCompany", hdrSubCompany);
                        SetColumnHeaderText(dgGridView, "Region", hdrRegion);
                        SetColumnHeaderText(dgGridView, "Area", hdrArea);
                        SetColumnHeaderText(dgGridView, "District", hdrDistrict);
                        SetColumnHeaderText(dgGridView, "Site", hdrSite);
                        SetColumnHeaderText(dgGridView, "SiteCode", hdrSiteCode);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByPOSITION)
                    {
                        SetColumnHeaderText(dgGridView, "Position", hdrPosition);
                        SetColumnHeaderText(dgGridView, "Company", hdrCompany);
                        SetColumnHeaderText(dgGridView, "SubCompany", hdrSubCompany);
                        SetColumnHeaderText(dgGridView, "Region", hdrRegion);
                        SetColumnHeaderText(dgGridView, "Area", hdrArea);
                        SetColumnHeaderText(dgGridView, "District", hdrDistrict);
                        SetColumnHeaderText(dgGridView, "Site", hdrSite);
                        SetColumnHeaderText(dgGridView, "SiteCode", hdrSiteCode);
                        SetColumnHeaderText(dgGridView, "Zone", hdrZone);
                        SetColumnHeaderText(dgGridView, "Type", hdrType);
                        SetColumnHeaderText(dgGridView, "GameTitle", hdrGameTitle);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByCATEGORY)
                    {
                        SetColumnHeaderText(dgGridView, "Category", hdrCategory);
                        SetColumnHeaderText(dgGridView, "Type", hdrType);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByTYPE)
                    {
                        SetColumnHeaderText(dgGridView, "Type", hdrType);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByOPERATOR)
                    {
                        SetColumnHeaderText(dgGridView, "Operator", hdrOperator);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByDEPOT)
                    {
                        SetColumnHeaderText(dgGridView, "Depot", hdrDepot);
                        SetColumnHeaderText(dgGridView, "Operator", hdrOperator);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByGAMETITLE)
                    {
                        SetColumnHeaderText(dgGridView, "GameTitle", hdrGameTitle);
                        SetColumnHeaderText(dgGridView, "Position", hdrPosition);
                        SetColumnHeaderText(dgGridView, "Site", hdrSite);
                        SetColumnHeaderText(dgGridView, "SiteCode", hdrSiteCode);
                        SetColumnHeaderText(dgGridView, "Company", hdrCompany);
                        SetColumnHeaderText(dgGridView, "SubCompany", hdrSubCompany);
                        SetColumnHeaderText(dgGridView, "Manufacturer", hdrManufacturer);
                        SetColumnHeaderText(dgGridView, "Category", hdrCategory);
                        //SetColumnHeaderText(dgGridView, "Type", hdrType);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByGAMEASSET)
                    {
                        SetColumnHeaderText(dgGridView, "Asset", hdrAsset);
                        SetColumnHeaderText(dgGridView, "Game Title", hdrGameTitle);
                        SetColumnHeaderText(dgGridView, "Position", hdrPosition);
                        SetColumnHeaderText(dgGridView, "Site", hdrSite);
                        SetColumnHeaderText(dgGridView, "SiteCode", hdrSiteCode);
                        SetColumnHeaderText(dgGridView, "SubCompany", hdrSubCompany);
                        SetColumnHeaderText(dgGridView, "Company", hdrCompany);
                        //  SetColumnHeaderText(dgGridView, "Installation", hdrInstallation);
                        SetColumnHeaderText(dgGridView, "Manufacturer_Name", hdrManufacturer);
                        SetColumnHeaderText(dgGridView, "Category_Code", hdrCategory);
                        SetColumnHeaderText(dgGridView, "Type", hdrType);
                    }
                    else if (cmbGroupBy.SelectedItem.ToString() == grpByASSET)
                    {
                        SetColumnHeaderText(dgGridView, "Asset", hdrAsset);
                        SetColumnHeaderText(dgGridView, "GameTitle", hdrGameTitle);
                        SetColumnHeaderText(dgGridView, "Position", hdrPosition);
                        SetColumnHeaderText(dgGridView, "Site", hdrSite);
                        SetColumnHeaderText(dgGridView, "SiteCode", hdrSiteCode);
                        SetColumnHeaderText(dgGridView, "SubCompany", hdrSubCompany);
                        SetColumnHeaderText(dgGridView, "Company", hdrCompany);
                        SetColumnHeaderText(dgGridView, "Manufacturer", hdrManufacturer);
                        if (rdnGame.Checked) SetColumnHeaderText(dgGridView, "Category", hdrCategory);
                        SetColumnHeaderText(dgGridView, "Type", hdrType);
                    }

                    SetColumnHeaderText(dgGridView, "Qty", ordbyQty, DataGridViewContentAlignment.MiddleRight, "");
                    SetColumnHeaderText(dgGridView, "Handle", ordbyHandle, DataGridViewContentAlignment.MiddleRight, "##,##0.00");

                    SetColumnHeaderText(dgGridView, "CasinoWin", hdrCasinoWin, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                    SetColumnHeaderText(dgGridView, "AvgDailyWin", ordbyAvgDailyWin, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                    SetColumnHeaderText(dgGridView, "AvgBet", ordbyAvgBet, DataGridViewContentAlignment.MiddleRight, "N");
                    SetColumnHeaderText(dgGridView, "DOF", ordbyDOF, DataGridViewContentAlignment.MiddleRight, "##,##0");
                    SetColumnHeaderText(dgGridView, "AvgGames", ordbyAvgGamesPlyd, DataGridViewContentAlignment.MiddleRight, "##,##0.00");

                    if (ConfigManager.Read("HideOccupancy").ToUpper() != "YES" && rdnSlot.Checked)
                    {
                        SetColumnHeaderText(dgGridView, "Occupancy", ordbyOccupancy, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                    }


                    SetColumnHeaderText(dgGridView, "GamesPlayed", ordbyGamesPlayed, DataGridViewContentAlignment.MiddleRight, "##,##0");
                    SetColumnHeaderText(dgGridView, "Theo_Net_Win", ordbyTheoNetWin, DataGridViewContentAlignment.MiddleRight, "##,##0");

                    if (rdnHold.Checked)
                    {
                        SetColumnHeaderText(dgGridView, "Hold_Perc", ordbyTheoPerc, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                        SetColumnHeaderText(dgGridView, "Hold_Act_Perc", ordbyActPerc, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                        SetColumnHeaderText(dgGridView, "Hold_Perc_Var", ordbyPercVar, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                    }
                    else
                    {
                        SetColumnHeaderText(dgGridView, "Payout_Perc", ordbyTheoPerc, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                        SetColumnHeaderText(dgGridView, "Payout_Act_Perc", ordbyActPerc, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                        SetColumnHeaderText(dgGridView, "Payout_Perc_Var", ordbyPercVar, DataGridViewContentAlignment.MiddleRight, "##,##0.00");
                    }

                    //Set to selected index as 2 of data grid
                    iSelectedIndexMeterDetails = 1;
                    dgGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    if (dgMeterDetails.Rows.Count >= 2)
                    {
                        dgMeterDetails.Rows[0].Frozen = true;
                        dgMeterDetails.Rows[iSelectedIndexMeterDetails].Selected = true;
                    }
                    bReturn = true;

                    Thread.Sleep(1);
                });
            }

            return bReturn;
        }

        private DataTable SummarizeData(IAsyncProgress2 o2)
        {
            DataTable dtNewGrid = new DataTable();
            try
            {
                dtNewGrid = dtMasterData;

                if (dtNewGrid.Rows.Count > 0)
                {
                    DataRow TotalRow = dtNewGrid.NewRow();
                    double iQty = 0, iHandle = 0, iDOF = 0, iGamesPlayed = 0, iCasinoWin = 0, iDailyWin = 0, iBet = 0;
                    double iTheoNetwin = 0, iHoldTheopercent = 0, iHoldActPercent = 0, iHoldPercentVar = 0, iAvgGames = 0, iOccupancy = 0;
                    double iPayoutTheopercent = 0, iPayoutActPercent = 0, iwin = 0, iPayoutPercentVar = 0;
                    Dictionary<string, string> dMachineNo = new Dictionary<string, string>();
                    string sSelectdGrpBy = string.Empty;
                    
                    o2.CrossThreadInvoke(() =>
                    {
                        sSelectdGrpBy = cmbGroupBy.SelectedItem.ToString();
                    });

                    int iRowCount = 0;

                    //calculate the total for records
                    foreach (DataRow dr in dtNewGrid.Rows)
                    {
                        //if (sSelectdGrpBy == grpByPOSITION)
                        //{
                        //    if (!dMachineNo.ContainsKey(dr["Position"].ToString()))
                        //    {
                        //        dMachineNo.Add(dr["Position"].ToString(), dr["Position"].ToString());
                        //        iDOF += Convert.ToDouble(dr["DOF"]);
                        //        iRowCount++;
                        //    }
                        //}
                        //else if (sSelectdGrpBy == grpByCATEGORY)
                        //{
                        //    //SetColumnHeaderText(dgGridView, "Category", hdrCategory);
                        //    //SetColumnHeaderText(dgGridView, "Type", hdrType);
                        //}
                        //else if (sSelectdGrpBy == grpByGAMETITLE)
                        //{
                        //    if (!dMachineNo.ContainsKey(dr["GameTitle"].ToString()))
                        //    {
                        //        dMachineNo.Add(dr["GameTitle"].ToString(), dr["GameTitle"].ToString());
                        //        iDOF += Convert.ToDouble(dr["DOF"]);
                        //        iRowCount++;
                        //    }
                        //}

                        //else if (sSelectdGrpBy == grpByGAMEASSET)
                        //{
                        //    if (!dMachineNo.ContainsKey(dr["Asset"].ToString()))
                        //    {
                        //        iDOF += Convert.ToDouble(dr["DOF"]);
                        //        dMachineNo.Add(dr["Asset"].ToString(), dr["Asset"].ToString());
                        //        iRowCount++;
                        //    }
                        //}
                        //else if (sSelectdGrpBy == grpByASSET)
                        //{
                        //    if (!dMachineNo.ContainsKey(dr["Asset"].ToString()))
                        //    {
                        //        iDOF += Convert.ToDouble(dr["DOF"]);
                        //        dMachineNo.Add(dr["Asset"].ToString(), dr["Asset"].ToString());
                        //        iRowCount++;
                        //    }
                        //}
                        //else
                        //{
                        //    iDOF += Convert.ToDouble(dr["DOF"]);
                        //    iRowCount++;
                        //}

                        iQty += Convert.ToDouble((dr["QTY"]));
                        iHandle += Convert.ToDouble(dr["Handle"]);
                        iDOF += Convert.ToDouble(dr["DOF"]);
                        iGamesPlayed += Convert.ToDouble(dr["GamesPlayed"]);
                        iCasinoWin += Convert.ToDouble(dr["CasinoWin"]);
                        iDailyWin += Convert.ToDouble(dr["AvgDailyWin"]);
                        iBet += Convert.ToDouble(dr["AvgBet"]);
                        iTheoNetwin += Convert.ToDouble(dr["Theo_Net_Win"]);
                        iAvgGames += Convert.ToDouble(dr["AvgGames"]);
                        iOccupancy += Convert.ToDouble(dr["Occupancy"]);

                        iHoldTheopercent += (Convert.ToDouble(dr["Handle"]) * Convert.ToDouble(dr["Hold_Perc"]));

                        iHoldActPercent += Convert.ToDouble(dr["Hold_Act_Perc"]);
                        iHoldPercentVar += Convert.ToDouble(dr["Hold_Perc_Var"]);

                        iPayoutTheopercent += Convert.ToDouble(dr["Payout_Perc"]);
                        iPayoutActPercent += Convert.ToDouble(dr["Payout_Act_Perc"]);
                        iPayoutPercentVar += Convert.ToDouble(dr["Payout_Perc_Var"]);
                        iwin += Convert.ToDouble(dr["win"]);
                    }

                    iRowCount = dtNewGrid.Rows.Count;
                    iDailyWin = Math.Round(iDailyWin / iRowCount);
                    iBet = Math.Round(iBet / iRowCount);
                    iHoldTheopercent = iHoldTheopercent / (iHandle > 0 ? iHandle : 1);
                    iHoldActPercent = Math.Round(((iHandle - iwin) / (iHandle > 0 ? iHandle : 1)) * 100, 2);
                    iHoldPercentVar = Math.Round(iHoldPercentVar / iRowCount, 2);
                    iPayoutTheopercent = Math.Round(iPayoutTheopercent / iQty, 2);

                    iPayoutActPercent = Math.Round(iPayoutActPercent / iRowCount, 2);
                    iPayoutPercentVar = Math.Round(iPayoutPercentVar / iRowCount, 2);
                    iAvgGames = Math.Round(iGamesPlayed / iDOF,2);
                    iOccupancy = Math.Round((iOccupancy / iDOF) * 100, 2);

                    //assign total values to the row
                    if(sSelectdGrpBy == grpByGAMEASSET)
                        TotalRow[2] = "Total";
                    else
                        TotalRow[0] = "Total";

                    TotalRow["Qty"] = iQty;
                    TotalRow["Handle"] = iHandle;
                    TotalRow["DOF"] = iDOF;
                    TotalRow["GamesPlayed"] = iGamesPlayed;
                    TotalRow["CasinoWin"] = iCasinoWin;

                    if (iDOF == 0 || iQty == 0)
                    {
                        TotalRow["AvgDailyWin"] = Math.Round(iCasinoWin, 2);
                    }
                    else
                    {
                        TotalRow["AvgDailyWin"] = Math.Round(iCasinoWin / (iDOF), 2);
                    }

                    if (iGamesPlayed == 0)
                    {
                        iGamesPlayed = 1;
                    }

                    TotalRow["AvgBet"] = Math.Round(iHandle / iGamesPlayed, 2);
                    TotalRow["Theo_Net_Win"] = iTheoNetwin;
                    TotalRow["AvgGames"] = iAvgGames;

                    if (ConfigManager.Read("HideOccupancy").ToUpper() != "YES" && rdnSlot.Checked)
                    {
                        TotalRow["Occupancy"] = iOccupancy;
                    }

                    TotalRow["Hold_Perc"] = iHoldTheopercent;
                    TotalRow["Hold_Act_Perc"] = iHoldActPercent;
                    TotalRow["Hold_Perc_Var"] = Math.Round(iHoldTheopercent - iHoldActPercent, 2);

                    TotalRow["Payout_Perc"] = iPayoutTheopercent;
                    TotalRow["Payout_Act_Perc"] = iPayoutActPercent;
                    TotalRow["Payout_Perc_Var"] = iPayoutPercentVar;

                    //insert the total row to the main table
                    dtNewGrid.Rows.InsertAt(TotalRow, 0);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return dtNewGrid;
        }

        private void LoadGraphData(BMC.CoreLib.Win32.IAsyncProgress2 o2)
        {
            try
            {
                //Load Graph only if some record is selected in Grid.
                if (iSelectedIndexMeterDetails > 0)
                {
                    //Declare variables.
                    DataTable objGraphDataTable = null;
                    MeterAnalysisChartData objMeterAnalysisChartData = new MeterAnalysisChartData();

                    decimal fGraphValue = 0;

                    //Graph data count.
                    int iCount = 0;

                    //Clear the arraylist data.
                    objCollection.Clear();

                    //Retrieve data from DB.
                    dtGraphData.DefaultView.Sort = "Read_Date";
                    objGraphDataTable = dtGraphData;

                    //Load data to the chart.
                    string strGroupText = string.Empty;
                    if (objGraphDataTable.Rows.Count > 0)
                    {
                        string strDataType = string.Empty;
                        o2.CrossThreadInvoke(() =>
                        {
                            strDataType = cmbDatatype.Text;
                        });

                        //Check for selected Data Type and assign the value.
                        o2.CrossThreadInvoke(() =>
                        {
                            _activeGraph.Legends[0].Title = strDataType;
                            _activeGraph.Series["Average"].Color = Color.FromName(this.cmbAvgColor.SelectedItem.ToString());
                            _activeGraph.Series["Selected"].Color = Color.FromName(this.cmbSelectColor.SelectedItem.ToString());
                            
                            _activeGraph.Series["Selected"].ToolTip = this.GetResourceTextByKey("Key_Selected") + ": #VALY";
                            _activeGraph.Series["Average"].ToolTip = this.GetResourceTextByKey("Key_AvgOfAll") + ": #VALY";
                        });

                        //Iterate through each row and assign data to the arraylist.
                        foreach (DataRow objGraphDataRow in objGraphDataTable.Rows)
                        {
                            //Add data to arraylist and return the same.
                            objMeterAnalysisChartData = AddItemToList(objGraphDataRow["Read_Date"].ToString());

                            fGraphValue = Convert.ToDecimal(objGraphDataRow["AVGDAILYWIN"]);

                            if (strDataType == typeHandle)
                            {
                                fGraphValue = Convert.ToDecimal(objGraphDataRow["Bet"]);
                            }
                            else if (strDataType == typeCasinoWin)
                            {
                                fGraphValue = Convert.ToDecimal(objGraphDataRow["CasinoWin"]);
                            }
                            else if (strDataType == typeGamesBet)
                            {
                                fGraphValue = Convert.ToDecimal(objGraphDataRow["GamesBet"]);
                            }

                            o2.CrossThreadInvoke(() =>
                            {
                                strGroupText = cmbGroupBy.Text;
                            });

                            //Check for the selected row in grid and add machine data for matching records.

                            if (strGroupText == grpByZONE)
                            {
                                if (dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["Zone"].Value.ToString() == objGraphDataRow["Zone"].ToString())
                                    if (dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["Site"].Value.ToString() == objGraphDataRow["Site"].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                            }
                            else if (strGroupText == grpByPOSITION)
                            {
                                if (dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["Position"].Value.ToString() == objGraphDataRow["Position"].ToString())
                                    if (dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["Site"].Value.ToString() == objGraphDataRow["Site"].ToString())
                                        if (dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["GameTitle"].Value.ToString() == objGraphDataRow["GameTitle"].ToString())
                                                objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                            }
                            else if (strGroupText == grpByGAMETITLE)
                            {
                                if (dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["GameTitle"].Value.ToString() == objGraphDataRow["GameTitle"].ToString()
                                    && dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["Manufacturer"].Value.ToString() == objGraphDataRow["Manufacturer"].ToString()
                                    && dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["Category"].Value.ToString() == objGraphDataRow["Category"].ToString())
                                    objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                            }
                            else if(strGroupText == grpByASSET)
                            {
                                if ((dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["Asset"].Value.ToString() == objGraphDataRow["Asset"].ToString()
                                        && dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["Position"].Value.ToString() == objGraphDataRow["Position"].ToString()))
                                        if (dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["GameTitle"].Value.ToString() == objGraphDataRow["GameTitle"].ToString())
                                            objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                            }
                            else
                            {
                                string sFieldname = string.Empty;
                                sFieldname = getFieldName(strGroupText);
                                if (_bGame_TitleColExists)
                                {
                                    if (dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells[sFieldname].Value.ToString() == objGraphDataRow[sFieldname].ToString())
                                        if (dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells["GameTitle"].Value.ToString() == objGraphDataRow["GameTitle"].ToString())
                                            objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                }
                                else
                                {
                                    if (dgMeterDetails.Rows[iSelectedIndexMeterDetails].Cells[sFieldname].Value.ToString() == objGraphDataRow[sFieldname].ToString())
                                        objMeterAnalysisChartData.MachineValue = objMeterAnalysisChartData.MachineValue + fGraphValue;
                                }
                            }

                            //Add Group data for all records.
                            objMeterAnalysisChartData.GroupValue = objMeterAnalysisChartData.GroupValue + fGraphValue;
                            objMeterAnalysisChartData.GroupQuantity = objMeterAnalysisChartData.GroupQuantity + 1;
                        }

                        //Clear the null values from the collection.
                        objCollection.TrimToSize();

                        if (objCollection.Count == 0)
                        {
                            return;
                        }

                        //Object to store graph data.
                        object[,] objGraphData = new object[2, objCollection.Count];

                        //Iterate through the arraylist and calculate the average for Machine & Group data.

                        o2.CrossThreadInvoke(() =>
                        {
                            _activeGraph.Series["Selected"].ChartType = rdnColumnChart.Checked? System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column : System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                            _activeGraph.Series["Selected"].MarkerStyle = rdnColumnChart.Checked ? System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None : System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;

                            _activeGraph.Series["Selected"].IsValueShownAsLabel = chkShowDataValue.Checked;
                            _activeGraph.Series["Average"].IsValueShownAsLabel = chkShowDataValue.Checked;

                            _activeGraph.Series["Selected"].Points.Clear();
                            _activeGraph.Series["Average"].Points.Clear();

                            _activeGraph.ResetAutoValues();
                        });

                        
                        foreach (MeterAnalysisChartData objMACData in objCollection)
                        {
                            objGraphData[0, iCount] = objMACData.MachineValue;
                            o2.CrossThreadInvoke(() =>
                            {
                                _activeGraph.Series["Selected"].Points.AddY(objGraphData[0, iCount]);
                                if (objMACData.GroupQuantity > 0)
                                {
                                    objGraphData[1, iCount] = Math.Round(Convert.ToDecimal((objMACData.GroupValue / objMACData.GroupQuantity)), 2);
                                    _activeGraph.Series["Average"].Points.AddY(objGraphData[1, iCount]);
                                }
                                else
                                {
                                    objGraphData[1, iCount] = 0;
                                    _activeGraph.Series["Average"].Points.AddY(0);
                                }
                            });

                            Thread.Sleep(10);
                            iCount++;
                        }

                        //Set the label text of the legend for the Graph.
                        string strLegendLabelSelected = "";
                        string strLegendLabelAll = "";

                        if (strGroupText == grpByGAMETITLE)
                        {
                            strLegendLabelSelected = hdrGameTitle;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_GameTitles1");
                        }
                        else if (strGroupText == grpByGAMEASSET)
                        {
                            strLegendLabelSelected = this.GetResourceTextByKey("Key_GameAsset");
                            strLegendLabelAll = this.GetResourceTextByKey("Key_GameAssets");
                        }
                        else if (strGroupText == grpByASSET)
                        {
                            strLegendLabelSelected = hdrAsset;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Assets");
                        }
                        else if (strGroupText == grpBySUBCOMPANY)
                        {
                            strLegendLabelSelected = hdrSubCompany;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_SubCompanies");
                        }
                        else if (strGroupText == grpByREGION)
                        {
                            strLegendLabelSelected = hdrRegion;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Regions");
                        }
                        else if (strGroupText == grpByAREA)
                        {
                            strLegendLabelSelected = hdrArea;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Areas");
                        }
                        else if (strGroupText == grpByDISTRICT)
                        {
                            strLegendLabelSelected = hdrDistrict;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Districts");
                        }
                        else if (strGroupText == grpByZONE)
                        {
                            strLegendLabelSelected = hdrZone;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Zones");
                        }
                        else if (strGroupText == grpByPOSITION)
                        {
                            strLegendLabelSelected = hdrPosition;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Positions");
                        }
                        else if (strGroupText == grpByOPERATOR)
                        {
                            strLegendLabelSelected = hdrOperator;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Operators");
                        }
                        else if (strGroupText == grpByDEPOT)
                        {
                            strLegendLabelSelected = hdrDepot;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Depots");
                        }
                        else if (strGroupText == grpBySITE)
                        {
                            strLegendLabelSelected = hdrSite;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Sites");
                        }
                        else if (strGroupText == grpByCATEGORY)
                        {
                            strLegendLabelSelected = hdrCategory;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Categories");
                        }
                        else if (strGroupText == grpByTYPE)
                        {
                            strLegendLabelSelected = hdrType;
                            strLegendLabelAll = this.GetResourceTextByKey("Key_Types");
                        }

                        //Chart Graph.
                        //Set the required properties for the chart.
                        
                        if (objGraphData.Length > 0)
                        {
                            o2.CrossThreadInvoke(() =>
                            {
                                _activeGraph.Visible = true;

                                //Set the legend details.
                                _activeGraph.Series["Selected"].LegendText = this.GetResourceTextByKey("Key_Selected") + " " + strLegendLabelSelected;
                                _activeGraph.Series["Average"].LegendText = this.GetResourceTextByKey("Key_AvgOfAll") + " " + strLegendLabelAll;

                                //Set X-Axis lable
                                double iColumnCount = 0.5;
                                _activeGraph.ChartAreas["caMeterAnalysis"].AxisX.CustomLabels.Clear();

                                //Assign the Column label for the Graph.
                                foreach (MeterAnalysisChartData objMACData in objCollection)
                                {
                                    _activeGraph.ChartAreas["caMeterAnalysis"].AxisX.CustomLabels.Add(iColumnCount, 1 + iColumnCount, objMACData.ChartDataLabel.ToString("dd MMM yyyy"));
                                    iColumnCount += 1;
                                }
                            });
                        }

                        //Persist with graph data.
                        MeterAnalysisChartData.objCollection = objCollection;
                        MeterAnalysisChartData.objGraphData = objGraphData;
                        MeterAnalysisChartData.strLegendLabelAll = strLegendLabelAll;
                        MeterAnalysisChartData.strLegendLabelSelected = strLegendLabelSelected;
                    }

                }
            }
            catch (Exception exLoadGraphData)
            {
                LogManager.WriteLog("Error in loading Graph." + "-Error Message-" + exLoadGraphData.Message, LogManager.enumLogLevel.Error);
            }
        }

        private string getFieldName(string strGroupText)
        {
            _bGame_TitleColExists = false;

            string sReturn = string.Empty;

            if (strGroupText == grpByGAMEASSET)
            {
                _bGame_TitleColExists = true;
                sReturn = "Installation";
            }
            else if (strGroupText == grpByASSET)
            {
                _bGame_TitleColExists = true;
                sReturn = "Asset";
            }
            else if (strGroupText == grpBySUBCOMPANY)
            {
                sReturn = "SubCompany";
            }
            else if (strGroupText == grpByREGION)
            {
                sReturn = "Region";
            }
            else if (strGroupText == grpByAREA)
            {
                sReturn = "Area";
            }
            else if (strGroupText == grpByDISTRICT)
            {
                sReturn = "District";
            }
            else if (strGroupText == grpByOPERATOR)
            {
                sReturn = "Operator";
            }
            else if (strGroupText == grpByDEPOT)
            {
                sReturn = "Depot";
            }
            else if (strGroupText == grpBySITE)
            {
                sReturn = "Site";
            }
            else if (strGroupText == grpByTYPE)
            {
                sReturn = "Type";
            }
            else if (strGroupText == grpByCATEGORY)
            {
                sReturn = "Category";
            }

            return sReturn;
        }
    
        private void SetTagProperty()
        {
            this.Tag = "Key_BallyMultiConnectMeterAnalysis";
            this.uxOrganisationDetails.Tag = "Key_OrgHierarchicalView";
            this.rdnSlot.Tag = "Key_RC_SLOTCaps";
            this.rdnGame.Tag = "Key_RC_GAMECaps";
            this.btnHideTree.Tag = "Key_Tree";
            this.lblCompany.Tag = "Key_RF_Company";
            this.lblSubCompany.Tag = "Key_RG_SubCompany";
            this.lblRegion.Tag = "Key_RC_Region";
            this.lblSite.Tag = "Key_RF_Site";
            this.lblDataType.Tag = "Key_DataTypeColon";
            this.rdnDay.Tag = "Key_ByDay";
            this.rdnWeek.Tag = "Key_ByWeek";
            this.rdnPeriod.Tag = "Key_ByPeriod";
            this.btnZoom.Tag = "Key_ZoomIn";
            this.lblOperator.Tag = "Key_Operator";
            this.lblDepot.Tag = "Key_RC_Depot";
            this.lblType.Tag = "Key_RC_Type";
            this.lblManufacturer.Tag = "Key_Manufacturer";
            this.lblFrom.Tag = "Key_FromColon";
            this.lblTo.Tag = "Key_ToColon";
            this.lblRecords.Tag = "Key_RecordsColon";
            this.lblCriteria.Tag = "Key_CriteriaColon";
            this.lblGroupBy.Tag = "Key_GroupByColon";
            this.rdnHold.Tag = "Key_HoldPercent";
            this.rdnPayout.Tag = "Key_PayouPercent";
            this.chkActive.Tag = "Key_ActiveAssets";
            this.lblCategory.Tag = "Key_GameCategory";
            this.lblGameTitle.Tag = "Key_GameTitle";
            this.chkActiveSites.Tag = "Key_ActiveSites";
            this.btnProcess.Tag = "Key_Process";
            this.btnExport.Tag = "Key_Export";
            this.btnClose.Tag = "Key_Close";
            this.chkShowDataValue.Tag = "Key_ShowDataValues";
            this.lblSelectedColor.Tag = "Key_SelectedColor";
            this.lblAvgColor.Tag = "Key_AvgColor";
            this.uxOrganisationDetails.Tag = "Key_OrgHierarchicalView";
            this.rdnColumnChart.Tag = "Key_BarChart";
            this.rdnLineChart.Tag = "Key_LineChart";
            this.lblDenom.Tag = "Key_Denom";
            this.lblPayout.Tag = "Key_Payout";
        }

        private MeterAnalysisChartData AddItemToList(string sText)
        {
            int iDataIndex = 0;
            try
            {
                //Declare variables.
                MeterAnalysisChartData objMeterAnalysisChartData = new MeterAnalysisChartData();
                int iCollectionIndex = 0;
                bool bDataExists = false;
                if (!string.IsNullOrEmpty(sText))
                {
                    //Remove time part of the datetime string.
                    sText = sText.Substring(0, sText.IndexOf(" "));

                    if (objCollection.Count > 0)
                    {
                        //Iterate through collection if data already exists.
                        foreach (MeterAnalysisChartData objMACData in objCollection)
                        {
                            if (objMACData.ChartDataLabel == Convert.ToDateTime(sText))
                            {
                                bDataExists = true;
                                break;
                            }
                            else
                                bDataExists = false;
                            iCollectionIndex++;
                        }
                    }
                }
                //If data does not exist add new.
                if (!bDataExists && !string.IsNullOrEmpty(sText))
                {
                    objMeterAnalysisChartData.ChartDataLabel = Convert.ToDateTime(sText);
                    objCollection.Add(objMeterAnalysisChartData);
                    iDataIndex = objCollection.Count - 1;
                }
                else
                    iDataIndex = iCollectionIndex;
            }
            catch (Exception exAddItemToList)
            {
                LogManager.WriteLog("Error in Adding Item to Arraylist." + "-Error Message-" + exAddItemToList.Message, LogManager.enumLogLevel.Error);
                throw exAddItemToList;
            }

            if (objCollection.Count > 0)
                return (MeterAnalysisChartData)objCollection[iDataIndex];
            else
                return new MeterAnalysisChartData();

        }

        private void ResetFiltersDetails()
        {

            try
            {
                _activeGraph.Visible = false;

                LoadGameTitle();

                dgMeterDetails.Columns.Clear();
                dgMeterDetails.Rows.Clear();

                dgExportMADetails.Columns.Clear();

                chkShowDataValue.Checked = true;
                chkActive.Checked = chkActiveSites.Checked = false;

                rdnHold.Checked = rdnLineChart.Checked = rdnWeek.Checked = true;

                dtpTo.Value = dtpFrom.Value = DateTime.Now;

                cmbOperator.SelectedIndex = cmbDepot.SelectedIndex = cmbType.SelectedIndex = 0;
                cmbManufacturer.SelectedIndex = cmbRecordCount.SelectedIndex = cmbCriteria.SelectedIndex = 0;
                cmbCategory.SelectedIndex = cmbGame.SelectedIndex = 0;
                cmbDenom.SelectedIndex = cmbPayout.SelectedIndex = cmbDatatype.SelectedIndex = 0;                
            }
            catch
            {
            }
        }

        #region Grid Formatter
        private void SetColumnHeaderText(DataGridView dgGridView, string strColumnName, string strHeaderText)
        {
            bool blnHasText = false;
            DataGridViewColumn dgcColumn;
            try
            {
                dgcColumn = dgGridView.Columns[strColumnName];
                if (dgcColumn != null)
                {
                    //chk each row whether the text is available
                    foreach (DataGridViewRow dgrRow in dgGridView.Rows)
                    {
                        if (dgrRow.Cells[strColumnName].Value != null)
                        {
                            //if text found break the loop
                            if (!string.IsNullOrEmpty(dgrRow.Cells[strColumnName].Value.ToString().Trim()))
                            {
                                blnHasText = true;
                                break;
                            }
                        }
                    }
                    //if text not found set visibilty as false
                    if (!blnHasText)
                    {
                        dgcColumn.Visible = false;
                    }
                    //else set visibilty and header text
                    else
                    {
                        dgcColumn.Visible = true;
                        dgcColumn.HeaderText = strHeaderText;
                        dgcColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Setting Column Header Text for " + strColumnName + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);

            }
        }

        //Overloaded method for setting column properties
        private void SetColumnHeaderText(DataGridView dgGridView, string strColumnName, string strHeaderText, DataGridViewContentAlignment Alignment, string strFormat)
        {
            DataGridViewColumn dgcColumn;
            try
            {
                dgcColumn = dgGridView.Columns[strColumnName];
                dgcColumn.Visible = true;
                dgcColumn.HeaderText = strHeaderText;
                dgcColumn.DefaultCellStyle.Alignment = Alignment;
                if (!string.IsNullOrEmpty(strFormat))
                {
                    dgcColumn.DefaultCellStyle.Format = strFormat;
                }
                dgcColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Setting Column Header Text" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);

            }
        }
        #endregion Grid Formater

        #endregion User Defind Methods

    }
}
