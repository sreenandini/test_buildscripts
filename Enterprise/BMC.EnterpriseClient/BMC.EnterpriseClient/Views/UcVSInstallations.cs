using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.CoreLib.Win32;
using System.Threading;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class UcVSInstallations : UserControl, IUserControl2
    {
        private enum InstallationFilter
        {
            All = 0,
            Position = 1,
            Terms = 2
        };

        private ViewSitesBusiness _business = new ViewSitesBusiness();
        private InstallationFilter _filter = InstallationFilter.All;
        private IDictionary<InstallationFilter, List<ColumnHeader>> _invisibleColumns = new SortedDictionary<InstallationFilter, List<ColumnHeader>>();
        private BMC.EnterpriseClient.Helpers.frmAdminUtilities _adminUtilities = new BMC.EnterpriseClient.Helpers.frmAdminUtilities();

        public UcVSInstallations(IViewSiteInfo viewSite)
        {
            this.ViewSite = viewSite;
            InitializeComponent();
            this.Initialize();
            SetTagProperty();
        }

        public void SetTagProperty()
        {
            this.chdrPayoutPer.Tag = "Key_PercentPayout";
            this.chdrAcDenom.Tag = "Key_AcDenom";
            this.rbtnAll.Tag = "Key_All";
            this.chdrAltSerialNo.Tag = "Key_AltSerialNoSortForm";
            this.chdrAssetNo.Tag = "Key_AssetNo";
            this.chdrBillValStatus.Tag = "Key_BillValStatus";
            this.chdrCoinType.Tag = "Key_CoinType";
            this.chdrCompanyPer.Tag = "Key_CompanyPercent";
            this.chdrDepot.Tag = "Key_Depot";
            this.chdrDueInStock.Tag = "Key_DueInStock";
            this.chdrGame.Tag = "Key_Game";
            this.chdrGameCategory.Tag = "Key_GameCategory";
            this.chdrGameCode.Tag = "Key_GameCode";
            this.chdrGameTitle.Tag = "Key_GameTitle";
            this.chdrGMUNo.Tag = "Key_GMUNo";
            this.chdrInstallationStatus.Tag = "Key_InstallationStatus";
            this.chdrInstalled.Tag = "Key_Installed";
            this.chdrJackpot.Tag = "Key_Jackpot";
            this.chdrLicence.Tag = "Key_Licence";
            this.chdrLocationPer.Tag = "Key_LocationPercent";
            this.chdrMcStatus.Tag = "Key_McStatus";
            this.chdrManufacturer.Tag = "Key_Manufacturer";
            this.cboManufacturer.Tag = "Key_Manufacturer";
            this.cboMaxBet.Tag = "Key_MaxBet";
            this.chdrNBV.Tag = "Key_NBV";
            this.chdrOpeningHours.Tag = "Key_OpeningHours";
            this.chdrOperator.Tag = "Key_Operator";
            this.chdrOperatorPer.Tag = "Key_OperatorPercent";
            this.chdrPercPayout.Tag = "Key_PayouPercent";
            this.chdrPaytableID.Tag = "Key_PaytableID";
            this.chdrPos.Tag = "Key_Pos";
            this.ctxItemPosAdmin.Tag = "Key_POS_ADMIN";
            this.rbtnPosition.Tag = "Key_Position";
            this.chdrRentShares.Tag = "Key_RentShares";
            this.chdrSecSharePer.Tag = "Key_SecSharePerc";
            this.chdrSerialNo.Tag = "Key_SerialNoHeader";
            this.chdrSFGroup.Tag = "Key_SFGroup";
            this.chdrSFSecGroup.Tag = "Key_SFSecGroup";
            this.chdrSFSite.Tag = "Key_SFSite";
            this.chdrSFSupplier.Tag = "Key_SFSupplier";
            this.chdrShortfall.Tag = "Key_Shortfall";
            this.rbtnTerms.Tag = "Key_Terms";
            this.chdrTerms.Tag = "Key_Terms";
            this.chdrTest.Tag = "Key_Test";
            this.chdrTheoPercPayout.Tag = "Key_TheoPayoutPerc";
            this.chdrType.Tag = "Key_Type";
            this.chdrWeeklyDep.Tag = "Key_WeeklyDep";
            this.chdrWeeklyTarget.Tag = "Key_WeeklyTarget";
            this.chdrZone.Tag = "Key_Zone";

        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
        }

        private void Initialize()
        {
            ModuleProc PROC = new ModuleProc("", "");
            this.IsMultiGame = false;

            try
            {
                lvwDetails.ClipboardCopyMode = ListViewClipboardCopyMode.EnableWithHeaderText;
                lvwDetails.ClipboardCopyFormat = ListViewClipboardCopyFormat.Semicolon;
                lvwDetails.MultiSelect = false;
                ctxItemPosAdmin.Text = ResourceExtensions.GetResourceTextByKey(null,"Key_PositionAdministration");

                _getSelectedInstallation = new Func<VSInstallationEntity>(this.GetSelectedInstallationPrivate);
                int optIndex = TypeSystem.GetValueInt(RdcHQ.GetSetting("Opt", "frmSiteoptBarPosDisplay", "0"));

                this.PrepareInvisibleColumns();
                rbtnAll.Tag = InstallationFilter.All;
                rbtnPosition.Tag = InstallationFilter.Position;
                rbtnTerms.Tag = InstallationFilter.Terms;

                switch (optIndex)
                {
                    case 1:
                        rbtnPosition.Checked = true;
                        break;
                    case 2:
                        rbtnTerms.Checked = true;
                        break;
                    default:
                        rbtnAll.Checked = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void PrepareInvisibleColumns()
        {
            ModuleProc PROC = new ModuleProc("", "PrepareInvisibleColumns");

            try
            {
                _invisibleColumns.Add(InstallationFilter.All, new List<ColumnHeader>());
                _invisibleColumns.Add(InstallationFilter.Position, new List<ColumnHeader>());
                _invisibleColumns.Add(InstallationFilter.Terms, new List<ColumnHeader>());

                // common
                ColumnHeader[] commonHeaders = new ColumnHeader[] 
                {
                    chdrShortfall,
                    chdrRentShares,
                    chdrSecSharePer,
                    chdrAltSerialNo,
                    chdrTest,
                    chdrDueInStock,
                    chdrSFSupplier,
                    chdrSFSite,
                    chdrSFGroup
                };

                foreach (KeyValuePair<InstallationFilter, List<ColumnHeader>> pair in _invisibleColumns)
                {
                    List<ColumnHeader> list = pair.Value;

                    foreach (var columnHeader in commonHeaders)
                    {
                        list.Add(columnHeader);
                    }

                    switch (pair.Key)
                    {
                        case InstallationFilter.All:
                            break;

                        case InstallationFilter.Position:
                            {
                                list.AddRange(new ColumnHeader[] 
                                {
                                    chdrLicence,
                                    chdrOperatorPer,
                                    chdrCompanyPer,
                                    chdrLocationPer,
                                    chdrSFSecGroup,
                                });
                            }
                            break;

                        case InstallationFilter.Terms:
                            {
                                list.AddRange(new ColumnHeader[] 
                                {
                                    chdrManufacturer,
                                    chdrInstalled,
                                    chdrOperator,
                                    chdrDepot,
                                    chdrTerms,
                                    chdrWeeklyTarget,
                                    chdrAssetNo,
                                    chdrSerialNo,                                    
                                    chdrGMUNo,
                                });
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public IViewSiteInfo ViewSite { get; set; }

        private Func<VSInstallationEntity> _getSelectedInstallation = null;

        private VSInstallationEntity GetSelectedInstallationPrivate()
        {
            ModuleProc PROC = new ModuleProc("", "");

            try
            {
                if (lvwDetails.SelectedItems != null &&
                    lvwDetails.SelectedItems.Count > 0)
                {
                    VSInstallationEntity entity = lvwDetails.SelectedItems[0].Tag as VSInstallationEntity;
                    if (entity != null)
                    {
                        return entity;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return null;
        }

        public VSInstallationEntity SelectedInstallation
        {
            get
            {
                return (VSInstallationEntity)BMC.CoreLib.Win32.Win32Extensions.CrossThreadInvokeFunc(this.ParentForm, _getSelectedInstallation);
            }
        }

        #region IUserControl Members

        public bool IsControlInitialized { get; set; }

        public void LoadControl()
        {
            ModuleProc PROC = new ModuleProc("", "LoadControl");

            try
            {
                //Clear existing details and load the current installations for the selected site.
                lvwDetails.Items.Clear();
                lvwMultiGame.Items.Clear();
                lvwPaytable.Items.Clear();

                VSInstallationsEntity entity = null;
                if (this.ViewSite.SelectedSite == null || this.ViewSite.SelectedSite.Site_ID <= 0) return;

                // fetching details
                BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this.ParentForm, this.GetResourceTextByKey(1,"MSG_INST_FETCH"), null,
                    (o) =>
                    {
                        o.CloseOnComplete = true;
                        entity = _business.GetInstallations(this.ViewSite.SelectedSite.Site_ID, null);
                    });

                // loading details
                if (entity != null && entity.Count > 0)
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this.ParentForm, this.GetResourceTextByKey(1,"MSG_INST_LOAD"), null, 1, entity.Count,
                        //BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialogContinuous(this.ParentForm, "Installations - [Loading details...]", null, 
                        (o) =>
                        {
                            IAsyncProgress2 o2 = o as IAsyncProgress2;
                            o.CloseOnComplete = true;

                            int count = entity.Count;
                            for (int i = 0; i < entity.Count; i++)
                            {
                                VSInstallationEntity det = entity[i];
                                int proIdx = (i + 1);
                                int proPerc = (int)(((float)proIdx / (float)count) * 100.0);
                                o2.UpdateStatusProgress(i, string.Format(this.GetResourceTextByKey(1,"MSG_INST_LOAD_DETAILS"), proIdx, count.ToString(), proPerc));
                                //o2.UpdateStatusProgress(i, "Loading Installation Details . . . : " +
                                //    proIdx + " of " + count.ToString() +
                                //    " (" + proPerc + "%)");

                                ListViewItem item = new ListViewItem();
                                item.ImageIndex = (det.IsMultiGame ? 1 : 0);
                                item.Text = det.Bar_Position_Name;
                                item.Tag = det;

                                for (int j = 1; j < lvwDetails.Columns.Count; j++)
                                {
                                    item.SubItems.Add("");
                                }

                                string game = string.Empty;
                                string manufacturer = det.Manufacturer_Name;
                                string type = det.Machine_Type_Code;
                                DateTime installedDate = det.Installation_Start_Date.GetDateTime("dd MMM yyyy");
                                string installed = string.Empty;
                                if (installedDate != DateTime.MinValue)
                                {
                                    installed = installedDate.ToStringDate();
                                }

                                string operatorName = "-";
                                string depotName = "-";
                                if (det.IsVacant)
                                {
                                    game = "         [VACANT]";
                                    manufacturer = string.Empty;
                                    type = det.Bar_Pos_Type_Code;
                                }
                                else
                                {
                                    game = det.Machine_Name;
                                    operatorName = det.Operator_Name;
                                    depotName = det.Depot_Name;
                                }

                                item.SubItems[chdrGame.Index].Text = game;
                                item.SubItems[chdrManufacturer.Index].Text = manufacturer;
                                item.SubItems[chdrType.Index].Text = type;
                                item.SubItems[chdrInstalled.Index].Text = installed;
                                item.SubItems[chdrOperator.Index].Text = operatorName;
                                item.SubItems[chdrDepot.Index].Text = depotName;

                                // terms calculator will be implemented later
                                bool terms = det.Bar_Position_Use_Terms.SafeValue();
                                if (terms)
                                {
                                    if (det.IsVacant)
                                    {
                                        item.SubItems[chdrTerms.Index].Text = "";
                                        item.SubItems[chdrRentShares.Index].Text = "";
                                        item.SubItems[chdrShortfall.Index].Text = "";
                                        item.SubItems[chdrLicence.Index].Text = "";
                                        item.SubItems[chdrOperatorPer.Index].Text = "";
                                        item.SubItems[chdrCompanyPer.Index].Text = "";
                                        item.SubItems[chdrLocationPer.Index].Text = "";
                                        item.SubItems[chdrSecSharePer.Index].Text = "";
                                    }
                                    else
                                    {
                                        // PENDING
                                    }
                                }
                                else
                                {
                                    item.SubItems[chdrTerms.Index].Text = "-";
                                    item.SubItems[chdrRentShares.Index].Text = "-";
                                    item.SubItems[chdrShortfall.Index].Text = "-";
                                    item.SubItems[chdrLicence.Index].Text = "-";
                                    item.SubItems[chdrOperatorPer.Index].Text = "-";
                                    item.SubItems[chdrCompanyPer.Index].Text = "-";
                                    item.SubItems[chdrLocationPer.Index].Text = "-";
                                    item.SubItems[chdrSecSharePer.Index].Text = "-";
                                }

                                // weekly target
                                _adminUtilities.ZeroDash(det.Bar_Position_Net_Target, item.SubItems[chdrWeeklyTarget.Index], 0, "###,##0.00");

                                // AGS
                                item.SubItems[chdrAssetNo.Index].Text = det.Machine_Stock_No;
                                item.SubItems[chdrSerialNo.Index].Text = det.Machine_Manufacturers_Serial_No;
                                item.SubItems[chdrAltSerialNo.Index].Text = det.Machine_Alternative_Serial_Numbers;
                                item.SubItems[chdrGMUNo.Index].Text = det.GMUNo;
                                item.SubItems[chdrTest.Index].Text = det.Machine_Test.SafeValue() ? "Test" : "";
                                item.SubItems[chdrDueInStock.Index].Text = det.Machine_Due_In_Stock.SafeValue() ? _adminUtilities.GetRegionalDate(det.Machine_Due_In_Stock_Date) : "";

                                // terms
                                if (terms)
                                {
                                    // PENDING
                                }
                                else
                                {
                                    item.SubItems[chdrSFSupplier.Index].Text = "-";
                                    item.SubItems[chdrSFSite.Index].Text = "-";
                                    item.SubItems[chdrSFGroup.Index].Text = "-";
                                    item.SubItems[chdrSFSecGroup.Index].Text = "-";
                                }

                                // machine history
                                if (det.IsVacant)
                                {
                                    item.SubItems[chdrNBV.Index].Text = "-";
                                    item.SubItems[chdrWeeklyDep.Index].Text = "-";
                                }
                                else
                                {
                                    // depreciation pending
                                    if (AppGlobals.Current.HasUserAccess("HQ_Stock_Machine_History"))
                                    {
                                        if (det.Depreciation_Policy_ID > 0)
                                        {
                                            double NBV = 0, DepPerWeek = 0, PurchasePrice = 0;
                                            DateTime? DateTo = null;
                                            _business.GetDepreciationDetailsFromMachineID(det.Machine_ID.SafeValue(), ref NBV, ref DepPerWeek,
                                                ref PurchasePrice, DateTo);
                                            _adminUtilities.ZeroDash(NBV, item.SubItems[chdrNBV.Index], 0, "###,##0.00");
                                            _adminUtilities.ZeroDash(DepPerWeek, item.SubItems[chdrWeeklyDep.Index], 0, "###,##0.00");
                                        }
                                        else
                                        {
                                            item.SubItems[chdrNBV.Index].Text = "-";
                                            item.SubItems[chdrWeeklyDep.Index].Text = "-";
                                        }
                                    }
                                    else
                                    {
                                        item.SubItems[chdrNBV.Index].Text = "-";
                                        item.SubItems[chdrWeeklyDep.Index].Text = "-";
                                    }
                                }

                                // zone
                                if (det.IsVacant)
                                {
                                    item.SubItems[chdrZone.Index].Text = "-";
                                    item.SubItems[chdrAcDenom.Index].Text = "-";
                                    item.SubItems[chdrCoinType.Index].Text = "-";
                                }
                                else
                                {
                                    item.SubItems[chdrZone.Index].Text = det.Zone_Name;
                                    _adminUtilities.ZeroDash(det.Installation_Price_Per_Play, item.SubItems[chdrAcDenom.Index], 0);
                                    _adminUtilities.ZeroDash(det.installation_token_value, item.SubItems[chdrCoinType.Index], 0);
                                }

                                // Machine_Class_Model_Code
                                if (det.IsVacant)
                                {
                                    item.SubItems[chdrGameCode.Index].Text = "-";
                                }
                                else
                                {
                                    item.SubItems[chdrGameCode.Index].Text = det.Machine_Class_Model_Code;
                                }

                                // Jackpot
                                if (det.IsVacant)
                                {
                                    _adminUtilities.AddNumericValue(TypeSystem.GetValueDouble(det.Bar_Position_Jackpot), item.SubItems[chdrJackpot.Index], "###,###");
                                    _adminUtilities.ZeroDash(TypeSystem.GetValueDouble(det.Bar_Position_Percentage_Payout), item.SubItems[chdrPayoutPer.Index], 0);
                                }
                                else
                                {
                                    _adminUtilities.AddNumericValue(TypeSystem.GetValueDouble(det.Installation_Jackpot_Value), item.SubItems[chdrJackpot.Index], "###,###");
                                    _adminUtilities.ZeroDash(TypeSystem.GetValueDouble(det.Installation_Percentage_Payout), item.SubItems[chdrPayoutPer.Index], 0);
                                }

                                // Opening hours
                                if (det.Standard_Opening_Hours_ID.SafeValue() > 0)
                                {
                                    item.SubItems[chdrOpeningHours.Index].Text = det.Standard_Opening_Hours_Description;
                                }
                                else
                                {
                                    item.SubItems[chdrOpeningHours.Index].Text = "Custom";
                                }

                                // Bar_Position_Machine_Enabled
                                int mcEnabled = 0;
                                string mcEnabledStatus = "-";
                                if (!det.IsVacant)
                                {
                                    if (det.Bar_Position_Machine_Enabled.IsValid())
                                        mcEnabled = det.Bar_Position_Machine_Enabled.SafeValue();
                                    else
                                        mcEnabled = 1;
                                }
                                else
                                {
                                    mcEnabled = 99;
                                }

                                switch (mcEnabled)
                                {
                                    case 0:
                                        mcEnabledStatus = "DISABLED";
                                        break;
                                    case 1:
                                        mcEnabledStatus = "ENABLED";
                                        break;
                                    case 100:
                                        mcEnabledStatus = "PENDING(DISABLED)";
                                        break;
                                    case 101:
                                        mcEnabledStatus = "PENDING(ENABLED)";
                                        break;
                                    default:
                                        break;
                                }
                                item.SubItems[chdrMcStatus.Index].Text = mcEnabledStatus;

                                // Bar_Position_Note_Acceptor_Enabled
                                int billValEnabled = 0;
                                string billValEnabledStatus = "-";
                                if (!det.IsVacant)
                                {
                                    if (det.Bar_Position_Note_Acceptor_Enabled.IsValid())
                                        billValEnabled = det.Bar_Position_Note_Acceptor_Enabled.SafeValue();
                                    else
                                        billValEnabled = 1;
                                }
                                else
                                {
                                    billValEnabled = 99;
                                }

                                switch (billValEnabled)
                                {
                                    case 0:
                                        billValEnabledStatus = "DISABLED";
                                        break;
                                    case 1:
                                        billValEnabledStatus = "ENABLED";
                                        break;
                                    case 100:
                                        billValEnabledStatus = "PENDING(DISABLED)";
                                        break;
                                    case 101:
                                        billValEnabledStatus = "PENDING(ENABLED)";
                                        break;
                                    default:
                                        break;
                                }
                                item.SubItems[chdrBillValStatus.Index].Text = billValEnabledStatus;

                                // Installation_Status
                                item.SubItems[chdrInstallationStatus.Index].Text = det.Installation_Status;

                                o.CrossThreadInvoke(() =>
                                {
                                    ListViewItem itemAdded = lvwDetails.Items.Add(item);
                                    lvwDetails.EnsureVisible(itemAdded.Index);
                                });
                                Thread.Sleep(1);
                            }

                            o.CrossThreadInvoke(() =>
                            {
                                //lvwHourly.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                                if (lvwDetails.Items.Count > 0) lvwDetails.EnsureVisible(0);
                            });
                        });
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.ResizeColumns();
            }
        }

        public void SelectItem()
        {
            if (lvwDetails.Items.Count > 0)
            {
                lvwDetails.Items[0].Selected = true;
            }
            else
            {
                this.ViewSite.Reload();
            }

        }

        public bool SaveControl()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void ResizeColumns()
        {
            ModuleProc PROC = new ModuleProc("", "ResizeColumns");

            try
            {
                lvwDetails.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                if (_invisibleColumns.ContainsKey(_filter))
                {
                    List<ColumnHeader> list = _invisibleColumns[_filter];
                    foreach (var columnHeader in list)
                    {
                        columnHeader.Width = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void rbtnFilter_CheckedChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "Method");

            try
            {
                _filter = (InstallationFilter)((RadioButton)sender).Tag;
                this.ResizeColumns();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void lvwDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwDetails.SelectedItems != null &&
                lvwDetails.SelectedItems.Count > 0)
            {
                this.LoadInstallationDetails();
                //this.ViewSite.Reload();
            }
        }

        public bool IsMultiGame
        {
            get { return lvwMultiGame.Visible; }
            set
            {
                lvwMultiGame.Visible = value;
                lvwPaytable.Visible = value;
            }
        }

        private void LoadInstallationDetails()
        {
            ModuleProc PROC = new ModuleProc("", "LoadInstallationDetails");

            try
            {
                VSInstallationEntity entity = this.SelectedInstallation;
                this.IsMultiGame = entity.IsMultiGame;

                if (entity.IsMultiGame)
                {
                    this.FillMultiGames();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #region IUserControl2 Members

        public void ClearControl()
        {
            ModuleProc PROC = new ModuleProc("", "ClearControl");

            try
            {
                lvwDetails.Items.Clear();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        private void FillMultiGames()
        {
            ModuleProc PROC = new ModuleProc("", "FillMultiGames");

            try
            {
                lvwMultiGame.Items.Clear();

                if (this.SelectedInstallation != null)
                {
                    VSMultiGameLibrariesEntity entities = _business.GetMultiGameLibraries(this.SelectedInstallation.Installation_ID.SafeValue());
                    foreach (var entity in entities)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Tag = entity;

                        item.Text = entity.MG_Alias_Game_Name;
                        item.SubItems.Add(entity.Game_Category_Name);
                        item.SubItems.Add(entity.Manufacturer);
                        lvwMultiGame.Items.Add(item);
                    }

                    if (lvwMultiGame.Items.Count > 0)
                    {
                        lvwMultiGame.Items[0].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                lvwMultiGame.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void lvwMultiGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "lvwMultiGame_SelectedIndexChanged");

            try
            {
                if (lvwMultiGame.SelectedItems != null &&
                    lvwMultiGame.SelectedItems.Count > 0)
                {
                    VSMultiGameLibraryEntity entity = lvwMultiGame.SelectedItems[0].Tag as VSMultiGameLibraryEntity;
                    if (entity != null)
                    {
                        this.FillPaytable(entity.MG_Group_ID);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void FillPaytable(int groupId)
        {
            ModuleProc PROC = new ModuleProc("", "FillMultiGames");

            try
            {
                lvwPaytable.Items.Clear();

                if (this.SelectedInstallation != null)
                {
                    VSPaytableForGamesEntity entities = _business.GetPaytableForGames(this.SelectedInstallation.Installation_ID.SafeValue(), groupId);
                    foreach (var entity in entities)
                    {
                        int index = 1;
                        ListViewItem item = new ListViewItem();
                        item.Tag = entity;

                        item.Text = entity.PaytableID;
                        if (lvwPaytable.Columns.Count > 1)
                        {
                            for (int i = 1; i < lvwPaytable.Columns.Count; i++)
                            {
                                item.SubItems.Add("x");
                            }
                        }
                        _adminUtilities.ZeroDash(entity.Payout.SafeValue(), item.SubItems[index++], 2);
                        _adminUtilities.ZeroDash(entity.TheoreticalPayout, item.SubItems[index++], 2);
                        _adminUtilities.ZeroDash(entity.MaxBet, item.SubItems[index++], 0);
                        lvwPaytable.Items.Add(item);
                    }

                    if (lvwPaytable.Items.Count > 0)
                    {
                        lvwPaytable.Items[0].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                lvwPaytable.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void ctxItemPosAdmin_Click(object sender, EventArgs e)
        {
            this.ViewSite.InvokeOrganisationForm(OrganisationInputType.Position, 0);
        }

        private void UcVSInstallations_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }
    }
}
