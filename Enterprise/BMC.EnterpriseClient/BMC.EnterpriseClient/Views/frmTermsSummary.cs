namespace BMC.EnterpriseClient.Views
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using BMC.Common.ExceptionManagement;
    using BMC.Common.LogManagement;
    using BMC.EnterpriseBusiness.Business;
    using BMC.EnterpriseDataAccess;
    using BMC.EnterpriseClient.Helpers;
    using BMC.EnterpriseBusiness.Entities;
    using System.Text;
    using BMC.Common;
    using BMC.CoreLib.Concurrent;
    using BMC.CoreLib.Win32;
    #endregion Namespaces

    #region Class
    public partial class frmTermsSummary : Form
    {
        #region Constants
        private const int TERMS_RENT_SCHEDULE = 28;
        private const int TERMS_SHARE_SCHEDULE = 29;
        #endregion Constants

        #region Private Members
        private TermsSummaryBusiness objTermsSummaryBusiness;
        private readonly IExecutorService _exec = ExecutorServiceFactory.CreateExecutorService();
        private ListViewCustomSorter _lvCustomSorter = null;
        #endregion Private Members

        #region Constructor
        public frmTermsSummary()
        {
            InitializeComponent();
            this.lvTermsSummary.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
            this.lvTermsSummary.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;
            _lvCustomSorter = new ListViewCustomSorter(this.lvTermsSummary, this);
            SetTagProperty();
            objTermsSummaryBusiness = TermsSummaryBusiness.CreateInstance();
        }
        #endregion Constructor

        #region Events
        public void frmTermsSummary_Load(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside frmTermsSummary_Load...", LogManager.enumLogLevel.Info);
                this.ResolveResources();
                this.FillSubCompanyNames();
                this.FillOperatorNames();
                this.FillMachineTypes();
                this.FillDepotNames();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            TermsConfigurationInfo _termsConfigInfo = null;
            TermsInfo _termsInfo = null;
            Tuple<TermsConfigurationInfo, TermsInfo> _result = null;
            int installationID = 0;
            int totalItems;
            try
            {
                LogManager.WriteLog("Inside btnView_Click...", LogManager.enumLogLevel.Info);

                //clear when invoked
                this.lvTermsSummary.Items.Clear();

                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, "Fetching...", _exec,
                (x) =>
                {
                    BMC.CoreLib.Win32.IAsyncProgress2 o2 = (BMC.CoreLib.Win32.IAsyncProgress2)x;

                    List<ListViewItem> listViewItems = new List<ListViewItem>();
                    ListViewItem listViewItem = new ListViewItem();

                    List<TermsSummaryList> termsSummaryList = new List<TermsSummaryList>();

                    o2.CrossThreadInvoke(() =>
                    {
                        termsSummaryList = this.objTermsSummaryBusiness.GetTermsSummaryList(
                                Convert.ToInt32(this.cmbOperators.SelectedValue),
                                Convert.ToInt32(this.cmbDepot.SelectedValue),
                                Convert.ToInt32(this.cmbMachineTypes.SelectedValue),
                                Convert.ToInt32(this.cmbSubCompanies.SelectedValue));
                        // skip null records if vacant is not checked
                        if (!this.chkDisplayVacantPositions.Checked)
                        {
                            termsSummaryList = termsSummaryList.Where(y => (Convert.ToInt32(y.InstallationID) > 0 && string.IsNullOrEmpty(y.InstallationEndDate))).ToList();
                        }

                    });
                    totalItems = termsSummaryList.Count;
                    o2.InitializeProgress(1, totalItems);
                    if (totalItems == 0)
                    {
                        BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_NO_RECORDS_AVAILABLE"));
                    }
                    else
                    {
                        for (int i = 0; i < totalItems; i++)
                        {
                            TermsSummaryList termsSummary = termsSummaryList[i];
                            if (string.IsNullOrEmpty(listViewItem.Name) || !("BP," + termsSummary.BarPositionID).Equals(listViewItem.Name))
                            {
                                installationID = Convert.ToInt32(termsSummary.InstallationID);

                                ////skip null records
                                //if (!this.chkDisplayVacantPositions.Checked &&
                                //    (installationID <= 0 || !string.IsNullOrEmpty(termsSummary.InstallationEndDate)))
                                //{
                                //    //this.pbListTermsSummary.PerformStep();
                                //    continue;
                                //}

                                if (installationID > 0)
                                {
                                    _result = new TermsCalcBusiness().GetTermsInfoForCollectionID(installationID, System.DateTime.Now);
                                    if (_result != null)
                                    {
                                        _termsConfigInfo = _result.Item1;
                                        _termsInfo = _result.Item2;
                                    }
                                }
                                if (_termsConfigInfo == null)
                                {
                                    _termsConfigInfo = new TermsConfigurationInfo();
                                }
                                if (_termsInfo == null)
                                {
                                    _termsInfo = new TermsInfo();
                                }

                                List<string> listViewColumns = new List<string>();
                                listViewColumns.Add(termsSummary.SiteName);
                                listViewColumns.Add(termsSummary.BarPositionName.ToString());
                                listViewColumns.Add(installationID > 0 && string.IsNullOrEmpty(termsSummary.InstallationEndDate) ? termsSummary.MachineName : this.GetResourceTextByKey("Key_Vacant"));
                                listViewColumns.Add(termsSummary.BarPositionSupplierPositionCode);
                                listViewColumns.Add(termsSummary.SiteCode.ToString());
                                listViewColumns.Add(termsSummary.BarPositionSupplierSiteCode);
                                listViewColumns.Add((installationID > 0 && string.IsNullOrEmpty(termsSummary.InstallationEndDate)) ? _termsConfigInfo.TermsSet : termsSummary.TermsGroupName);
                                listViewColumns.Add(GetRentOrShares(_termsConfigInfo, _termsInfo));
                                listViewColumns.Add(GetShortfall(_termsConfigInfo, _termsInfo));
                                listViewColumns.Add(this.ZeroDash(this.ReturnValueFromChangeDates(_termsInfo.SupplierValueBefore.ToString(), _termsInfo.SupplierValueBeforeChangeDate ?? string.Empty, _termsInfo.SupplierValue.ToString(), _termsInfo.SupplierValueAfterChangeDate ?? string.Empty, _termsInfo.SupplierValueAfter.ToString())));
                                listViewColumns.Add(_termsInfo.OtherLicenceUse > 0 ? this.ZeroDash(_termsInfo.OtherLicenceCharge.ToString()) : "-");
                                listViewColumns.Add(this.ZeroDash(_termsInfo.SupplierShare.ToString()));
                                listViewColumns.Add(this.ZeroDash(_termsInfo.SiteShare.ToString()));
                                listViewColumns.Add(this.ZeroDash(_termsInfo.GroupShare.ToString()));
                                listViewColumns.Add(_termsInfo.BarPosSetForNoTerms.GetValueOrDefault() == true ? "-TV" : "");
                                listViewColumns.Add(this.ZeroDash(termsSummary.MachineTypeCode));
                                listViewColumns.Add(this.ZeroDash(termsSummary.InstallationPricePerPlay));
                                listViewColumns.Add(this.ZeroDash(termsSummary.InstallationJackpotValue));
                                listViewItem = new ListViewItem(listViewColumns.ToArray());
                                listViewItem.Name = string.Format("BP, {0}", termsSummary.BarPositionID);
                                listViewItem.Text = termsSummary.SiteName;
                                o2.CrossThreadInvoke(() =>
                                     {
                                         if (i <= 100)
                                         {
                                             this.lvTermsSummary.Items.Add(listViewItem);
                                         }
                                         else
                                         {
                                             listViewItems.Add(listViewItem);
                                         }
                                     });
                            }
                            o2.UpdateStatusProgress(i + 1, string.Format("Fetching records : {0}/{1}", i + 1, totalItems));
                        }
                        o2.CrossThreadInvoke(() =>
                            {
                                this.lvTermsSummary.Items.AddRange(listViewItems.ToArray());
                            });
                    }
                });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            this.lvTermsSummary.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnExport_Click...", LogManager.enumLogLevel.Info);

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Choose file to save to",
                    FileName = "example.csv",
                    Filter = "CSV (*.csv)|*.csv",
                    FilterIndex = 0,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };

                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] headerValues = this.lvTermsSummary.Columns
                                       .OfType<ColumnHeader>()
                                       .Select(header => header.Text.Trim())
                                       .ToArray();

                    string[][] columnValues = this.lvTermsSummary.Items
                                .OfType<ListViewItem>()
                                .Select(lvi => lvi.SubItems
                                    .OfType<ListViewItem.ListViewSubItem>()
                                    .Select(si => si.Text).ToArray()).ToArray();

                    StringBuilder table = new StringBuilder(string.Join(",", new string[] { "Terms Summary For: ", this.cmbSubCompanies.Text, this.cmbOperators.Text, this.cmbMachineTypes.Text }) + Environment.NewLine);
                    table.Append(string.Join(",", headerValues) + Environment.NewLine);
                    foreach (string[] columnValue in columnValues)
                    {
                        table.Append(string.Join(",", columnValue.Select<string, string>(x => x.Contains(",") ? "\"" + x + "\"" : x).ToArray()) + Environment.NewLine);
                    }
                    System.IO.File.WriteAllText(saveFileDialog.FileName, table.ToString().TrimEnd('\r', '\n'));
                    BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DATA_SAVED") + " " + saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbOperators_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside cmbOperators_SelectedIndexChanged...", LogManager.enumLogLevel.Info);
                this.FillDepotNames(this.cmbOperators.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvTermsSummary_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lvTermsSummary_DoubleClick...", LogManager.enumLogLevel.Info);

                frmTermsSummaryUpdate updateTermsSummary = new frmTermsSummaryUpdate();
                ListViewItem selectedRow = this.lvTermsSummary.SelectedItems[0];

                if (selectedRow != null)
                {
                    string termsRental = selectedRow.SubItems[6].Text;
                    if (termsRental.IndexOf("Bar Pos") == -1)
                    {
                        BMC.CoreLib.Win32.Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SET_FROM_TERMS"));
                        return;
                    }

                    updateTermsSummary.Id = Convert.ToInt32(selectedRow.Name.Split(',')[1]);

                    if (termsRental.IndexOf("Bar Pos/") > 0)
                    {
                        updateTermsSummary.Rent = new Tuple<bool, float>(true, Convert.ToSingle(selectedRow.SubItems[8].Text));
                    }

                    updateTermsSummary.Licence = new Tuple<bool, float>(true, Convert.ToSingle(selectedRow.SubItems[9].Text));

                    if (termsRental.IndexOf("/Bar Pos") > 0)
                    {
                        updateTermsSummary.Supplier = new Tuple<bool, float>(true, Convert.ToSingle(selectedRow.SubItems[10].Text));
                        updateTermsSummary.Company = new Tuple<bool, float>(true, Convert.ToSingle(selectedRow.SubItems[11].Text));
                        updateTermsSummary.SiteShare = new Tuple<bool, float>(true, Convert.ToSingle(selectedRow.SubItems[12].Text));
                    }

                    updateTermsSummary.SiteName = selectedRow.SubItems[0].Text;

                    if (updateTermsSummary.ShowDialog() == DialogResult.OK)
                    {
                        selectedRow.SubItems[8].Text = updateTermsSummary.Rent.Item2.ToString();
                        selectedRow.SubItems[9].Text = updateTermsSummary.Licence.Item2.ToString();
                        selectedRow.SubItems[10].Text = updateTermsSummary.Supplier.Item2.ToString();
                        selectedRow.SubItems[11].Text = updateTermsSummary.Company.Item2.ToString();
                        selectedRow.SubItems[12].Text = updateTermsSummary.SiteShare.Item2.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Events

        #region Private Methods
        private void SetTagProperty()
        {
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnExport.Tag = "Key_Export";
            this.btnView.Tag = "Key_View";
            this.grpOptions.Tag = "Key_Options";
            this.chkDisplayVacantPositions.Tag = "Key_DisplayVacantPositions";
            this.lblSubCompany.Tag = "Key_SubCompany";
            this.lblOperator.Tag = "Key_Operator";
            this.lblMachineType.Tag = "Key_MachineType";
            this.lblDepot.Tag = "Key_Depot";
            this.clmSiteName.Tag = "Key_Site";
            this.clmPosition.Tag = "Key_Pos";
            this.clmMachine.Tag = "Key_Machine";
            this.clmSupPos.Tag = "Key_SupPos";
            this.clmSiteCode.Tag = "Key_SiteCode";
            this.clmSupSite.Tag = "Key_SupSite";
            this.clmTerms.Tag = "Key_Terms";
            this.clmRentShares.Tag = "Key_RentShares";
            this.clmShortfall.Tag = "Key_Shortfall";
            this.clmRent.Tag = "Key_Rent";
            this.clmLicence.Tag = "Key_Licence";
            this.clmSupplierPercent.Tag = "Key_SupplierPercent";
            this.clmLocationPercent.Tag = "Key_LocationPercent";
            this.clmCompanyPercent.Tag = "Key_CompanyPercent";
            this.clmImport.Tag = "Key_Import";
            this.clmPosType.Tag = "Key_PosType";
            this.clmPop.Tag = "Key_PoP";
            this.clmJackpot.Tag = "Key_Jackpot";
            this.Tag = "Key_TermsSummary";

        }
        private void FillSubCompanyNames()
        {
            try
            {
                LogManager.WriteLog("Inside FillSubCompanyNames...", LogManager.enumLogLevel.Info);

                this.cmbSubCompanies.DataSource = this.objTermsSummaryBusiness.GetSubCompanyNames();
                this.cmbSubCompanies.DisplayMember = "SubCompanyName";
                this.cmbSubCompanies.ValueMember = "SubCompanyId";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillOperatorNames()
        {
            try
            {
                LogManager.WriteLog("Inside FillOperatorNames...", LogManager.enumLogLevel.Info);

                this.cmbOperators.DataSource = this.objTermsSummaryBusiness.GetOperatorNames();
                this.cmbOperators.DisplayMember = "OperatorName";
                this.cmbOperators.ValueMember = "OperatorId";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillMachineTypes()
        {
            try
            {
                LogManager.WriteLog("Inside FillMachineTypes...", LogManager.enumLogLevel.Info);

                this.cmbMachineTypes.DataSource = this.objTermsSummaryBusiness.GetMachineTypes();
                this.cmbMachineTypes.DisplayMember = "MachineTypeCode";
                this.cmbMachineTypes.ValueMember = "MachineTypeId";
                this.cmbMachineTypes.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FillDepotNames(string supplierId = "0")
        {
            int parsedSupplierId = 0;
            try
            {
                LogManager.WriteLog("Inside FillDepotNames...", LogManager.enumLogLevel.Info);

                if (int.TryParse(supplierId, out parsedSupplierId))
                {
                    this.cmbDepot.DataSource = this.objTermsSummaryBusiness.GetDepotNames(Convert.ToInt32(parsedSupplierId));
                    this.cmbDepot.DisplayMember = "DepotName";
                    this.cmbDepot.ValueMember = "DepotId";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private string GetRentOrShares(TermsConfigurationInfo _termsConfigInfo, TermsInfo _termsInfo)
        {
            string result = string.Empty;

            try
            {
                LogManager.WriteLog("Inside GetRentOrShares...", LogManager.enumLogLevel.Info);

                if (_termsConfigInfo.BarPosOverrideRent.GetValueOrDefault())
                    result = "Bar Pos";
                else if (_termsInfo.SupplierType.GetValueOrDefault() == TERMS_RENT_SCHEDULE)
                    result = "Rent Sch";
                else
                    result = "Terms";

                if (_termsConfigInfo.BarPosOverrideShares.GetValueOrDefault())
                    result = string.Format("{0}\\{1}", result, "Bar Pos");
                else if (_termsInfo.SupplierType.GetValueOrDefault() == TERMS_SHARE_SCHEDULE)
                    result = string.Format("{0}\\{1}", result, "Share Sch");
                else
                    result = string.Format("{0}\\{1}", result, "Terms");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        private string GetShortfall(TermsConfigurationInfo _termsConfigInfo, TermsInfo _termsInfo)
        {
            string data = string.Empty;
            try
            {
                LogManager.WriteLog("Inside GetShortfall...", LogManager.enumLogLevel.Info);

                if (_termsInfo.SupplierGuarantor.GetValueOrDefault() > 0)
                    data = string.Format("{0}\\{1}", data, "Supplier");
                if (_termsInfo.SiteGuarantor.GetValueOrDefault() > 0)
                    data = string.Format("{0}\\{1}", data, "Site");
                if (_termsInfo.GroupGuarantor.GetValueOrDefault() > 0)
                    data = string.Format("{0}\\{1}", data, "Group");
                if (_termsInfo.SecGroupGuarantor.GetValueOrDefault() > 0)
                    data = string.Format("{0}\\{1}", data, "Sec Group");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return data;
        }

        private string ZeroDash(string input, int decPlaces = 2, string mask = "###,###,0")
        {
            float iResult;
            try
            {
                LogManager.WriteLog("Inside ZeroDash...", LogManager.enumLogLevel.Info);
                return float.TryParse(input, out iResult) && iResult != 0.0 ? iResult.ToString(mask + (decPlaces > 0 ? "." + new string('0', decPlaces) : "")) : "-";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return string.Empty;
        }

        private string ReturnValueFromChangeDates(string previousValue, string previousDate, string currentValue, string futureDate, string futureValue)
        {
            try
            {
                LogManager.WriteLog("Inside ReturnValueFromChangeDates...", LogManager.enumLogLevel.Info);
                if ((previousDate != futureDate) || (previousDate == string.Empty && futureDate == string.Empty))
                    return currentValue;
                else
                    return previousValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return string.Empty;
        }
        #endregion Private Methods
    }
    #endregion Class
}
