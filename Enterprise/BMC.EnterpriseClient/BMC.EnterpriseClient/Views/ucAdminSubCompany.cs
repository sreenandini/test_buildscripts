using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using Audit.BusinessClasses;
using Audit.Transport;
using System.Text.RegularExpressions;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public delegate void OnOpenSite();
    public delegate void RefreshOnSaveSubCompany(int subCompanyID);
    public partial class ucAdminSubCompany : UserControl
    {
        
        Int32? _subCompanyId = 0;
        AdminSubCompany _AdminSubCompany = null;
        AdminSubCompanyResult objAdminSubCompanyResult = null;
        public RefreshOnSaveSubCompany NotifyChanges;
        public OnOpenExisitingSubCompanyHandler OpensiteForm;
        frmAdminUtilities frmUtobj = new frmAdminUtilities();
        Int32? CompanyID = 0;
        public OnOpenSiteHandler OpenSiteForm;
        public ucAdminSubCompany(int subCompanyId)
        {
            try
            {
                InitializeComponent();
                SetTagProperty();
                this.ResolveResources();
                grpDetails.Text = "";
                _subCompanyId = subCompanyId;
                this.Dock = DockStyle.Fill;
                _AdminSubCompany = new AdminSubCompany();
                LoadInitialData();
                LoadSubCompanyDetails(0);
                EnableControls();
                //SSTab1.TabPages.Remove(tbDefaults);
                SSTab1.TabPages.Remove(tbFinancial);
                txtRegionName.Enabled = false;
                txtRegionDescription.Enabled = false;
                txtAreaName.Enabled = false;
                txtAreaDescription.Enabled = false;
                txtDistrictName.Enabled = false;
                txtDistrictDescription.Enabled = false;
                cmbRegionStaff.Enabled = cmbAreaStaff.Enabled = cmbDistrictStaff.Enabled = false;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.lblPayout.Tag = "Key_PercentPayoutColon";
            this.lblName.Tag = "Key_NameMandatory";
            this.lblAccessKey.Tag = "Key_AccessKeyColon";
            this.lblAccountNumber.Tag = "Key_AccountNumberColon";
            this.lblAccountRef.Tag = "Key_AccountRefColon";
            this.lblAddress.Tag = "Key_AddressColon";
            this.lblANANumber.Tag = "Key_ANANumberColon";
            this.btnPPPApply.Tag = "Key_Apply";
            this.btnJPApply.Tag = "Key_Apply";
            this.btnPercApply.Tag = "Key_Apply";
            this.btnTPApply.Tag = "Key_Apply";
            this.btnAKeyApply.Tag = "Key_Apply";
            this.btnRepApply.Tag = "Key_Apply";
            //this.btnRegionApply.Tag = "Key_Apply";
            this.btnRegionApply.Tag = "Key_Apply_WOShortCut";
            //this.btnAreaApply.Tag = "Key_Apply";
            this.btnAreaApply.Tag = "Key_Apply_WOShortCut";
            //this.btnDistrictApply.Tag = "Key_Apply";
            this.btnDistrictApply.Tag = "Key_Apply_WOShortCut";
            this.btnApply.Tag = "Key_Apply";
            this.tbAreas.Tag = "Key_Areas";
            this.lblAreas.Tag = "Key_AreasColon";
            this.lblBankAccountName.Tag = "Key_BankAccountName";
            this.grpBanking.Tag = "Key_Banking";
            this.btnRegionCancel.Tag = "Key_CancelCaption";
            this.btnAreaCancel.Tag = "Key_CancelCaption";
            this.btnDistrictCancel.Tag = "Key_CancelCaption";
            this.lblCascadeType.Tag = "Key_CascadeTypeColon";
            this.lblCompany.Tag = "Key_CompanyColon";
            this.lblContactEMail.Tag = "Key_ContactEMailColon";
            this.lblContactName.Tag = "Key_ContactNameColon";
            this.lblContactPhoneNumber.Tag = "Key_ContactPhoneNumber";
            this.tbDefaults.Tag = "Key_Defaults";
            this.tbDetails.Tag = "Key_Details";
            this.lblDistricts.Tag = "Key_DistrictsColon";
            this.tbFinancial.Tag = "Key_Details2";
            this.lblIncomeLedgerCode.Tag = "Key_IncomeLedgerCodeColon";
            this.lblInvoiceAddress.Tag = "Key_InvoiceAddressColon";
            this.lblInvoiceName.Tag = "Key_InvoiceNameColon";
            this.lblInvoicePostCode.Tag = "Key_InvoicePostCode";
            this.grpInvoicing.Tag = "Key_Invoicing";
            this.grpDetails.Tag = "Key_Details";
            this.lblItem.Tag = "Key_Item";
            this.lblJackPot.Tag = "Key_JackPotColon";
            this.grpLedgerCodes.Tag = "Key_LedgerCodes";
            this.lblModelSet.Tag = "Key_ModelSetColon";
            this.lblRegionName.Tag = "Key_NameColon";
            this.lblAreaName.Tag = "Key_NameColon";
            this.lblDistrictName.Tag = "Key_NameColon";
            //this.btnRegionNew.Tag = "Key_NewCaption";
            this.btnRegionNew.Tag = "Key_New_WOShortCut";
            //this.btnAreaNew.Tag = "Key_NewCaption";
            this.btnAreaNew.Tag = "Key_New_WOShortCut";
            //this.btnDistrictNew.Tag = "Key_NewCaption";
            this.btnDistrictNew.Tag = "Key_New_WOShortCut";
            this.btnNewSite.Tag = "Key_NewSite";
            this.lblOpeningTimes.Tag = "Key_OpeningTimesColon";
            this.lblPostcode.Tag = "Key_PostcodeColon";
            this.lblPricePerPlay.Tag = "Key_PricePerPlayColon";
            this.lblRegions.Tag = "Key_RegionsColon";
            this.lblRepresentitive.Tag = "Key_RepresentativeColon";
            this.lblSortCode.Tag = "Key_SortCodeColon";
            this.lblStaff.Tag = "Key_StaffColon";
            this.lblAreaStaff.Tag = "Key_StaffColon";
            this.lblDistrictStaff.Tag = "Key_StaffColon";
            this.lblSwitchboardPhoneNumber.Tag = "Key_SwitchboardPhoneNumber";
            this.lblTermsGroup.Tag = "Key_TermsGroupColon";
            this.lblTypeOfTrade.Tag = "Key_TypeofTradeColon";
            this.lblUseSplitRents.Tag = "Key_UseSplitRents";
            this.lblValue.Tag = "Key_Value";
            this.btnClose.Tag = "Key_Close";
        }

        private void EnableControls()
        {
            bool enableControls = AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Sub_Edit") && _subCompanyId != 0;

            if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_New"))
            {
                btnNewSite.Visible = false;
            }

            btnApply.Enabled = enableControls;
            btnPPPApply.Enabled = enableControls;
            btnJPApply.Enabled = enableControls;
            btnPercApply.Enabled = enableControls;
            btnTPApply.Enabled = enableControls;
            btnAKeyApply.Enabled = enableControls;
            btnRepApply.Enabled = enableControls;
            btnRegionApply.Enabled = enableControls;
            btnRegionCancel.Enabled = enableControls;
            btnRegionNew.Enabled = enableControls;
            btnAreaApply.Enabled = enableControls;
            btnAreaCancel.Enabled = enableControls;
            btnAreaNew.Enabled = enableControls;
            btnDistrictApply.Enabled = enableControls;
            btnDistrictCancel.Enabled = enableControls;
            btnDistrictNew.Enabled = enableControls;
            if (!enableControls)
            {
                this.tblMainFram.RowStyles[0].Height = 30;
                lblErrorMessage.Text = this.GetResourceTextByKey(1, "MSG_USER") + AppGlobals.Current.UserName + this.GetResourceTextByKey(1, "MSG_PERMISSION_DENIED_TOEDIT_SUBCOMP");
            }
            else
            {
                this.tblMainFram.RowStyles[0].Height = 0;
                lblErrorMessage.Visible = false;
            }
        }

        public ucAdminSubCompany(OrganisationDetailsEntity _OrganisationDetailsEntity)
        {
            try
            {
                InitializeComponent();
                SetTagProperty();
                this.ResolveResources();
                grpDetails.Text = "";
                this.Dock = DockStyle.Fill;
                _AdminSubCompany = new AdminSubCompany();
                LoadInitialData();
                CompanyID = _OrganisationDetailsEntity.Company_ID;
                LoadSubCompanyDetails(_OrganisationDetailsEntity.Company_ID);
                SSTab1.TabPages.Remove(tbDefaults);
                SSTab1.TabPages.Remove(tbFinancial);
                grpBanking.Enabled = true;
                grpInvoicing.Enabled = true;
                grpLedgerCodes.Enabled = true;
                tblAreasTab.Enabled = false;
                this.tblMainFram.RowStyles[0].Height = 0;
                lblErrorMessage.Visible = false;
                btnNewSite.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadInitialData()
        {
            try
            {
                cmbPPP.Items.Add( this.GetResourceTextByKey("Key_UseCompanyDefault"));

                cmbPPP.Items.Add(0);
                cmbPPP.Items.Add(1);
                cmbPPP.Items.Add(2);
                cmbPPP.Items.Add(5);
                cmbPPP.Items.Add(10);
                cmbPPP.Items.Add(15);
                cmbPPP.Items.Add(20);
                cmbPPP.Items.Add(25);
                cmbPPP.Items.Add(30);
                cmbPPP.Items.Add(35);
                cmbPPP.Items.Add(40);
                cmbPPP.Items.Add(45);
                cmbPPP.Items.Add(50);
                cmbPPP.Items.Add(60);
                cmbPPP.Items.Add(70);
                cmbPPP.Items.Add(80);
                cmbPPP.Items.Add(90);
                cmbPPP.Items.Add(100);
                cmbPPP.Items.Add(150);
                cmbPPP.Items.Add(200);
                cmbPPP.Items.Add(250);
                cmbPPP.Items.Add(300);
                cmbPPP.Items.Add(500);
                cmbPPP.Items.Add(1000);
                cmbPO.Items.Add( this.GetResourceTextByKey("Key_UseCompanyDefault"));
                cmbJP.Items.Clear();
                cmbJP.Items.Add( this.GetResourceTextByKey("Key_UseCompanyDefault"));
                //cmbJP.ItemData(LstJP.NewIndex) = -1;
                cmbJP.Items.Add("1");
                cmbJP.Items.Add("2");
                cmbJP.Items.Add("5");
                cmbJP.Items.Add("10");
                cmbJP.Items.Add("15");
                cmbJP.Items.Add("20");
                cmbJP.Items.Add("25");
                cmbJP.Items.Add("50");
                cmbJP.Items.Add("75");
                cmbJP.Items.Add("100");
                cmbJP.Items.Add("150");
                cmbJP.Items.Add("200");
                cmbJP.Items.Add("250");
                cmbJP.Items.Add("500");
                cmbJP.Items.Add("1000");
                for (int i = 1; i <= 100; i++)
                {
                    cmbPO.Items.Add(i);
                }

                List<ComboBoxItem> lstComboBoxItem = new List<ComboBoxItem>();
                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_NoneText"), Value = "0" });
                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_ItemusingCascade"), Value = "1" });
                lstComboBoxItem.Add(new ComboBoxItem() { Text = this.GetResourceTextByKey("Key_Allitems"), Value = "2" });

                cmbPPPCT.DataSource = lstComboBoxItem;
                cmbJPCT.DataSource = new List<ComboBoxItem>(lstComboBoxItem);
                cmbPOCT.DataSource = new List<ComboBoxItem>(lstComboBoxItem);
                cmbTGCT.DataSource = new List<ComboBoxItem>(lstComboBoxItem);
                cmbAKCT.DataSource = new List<ComboBoxItem>(lstComboBoxItem);
                cmbRepCT.DataSource = new List<ComboBoxItem>(lstComboBoxItem);

                cmbPPPCT.ValueMember = "Value";
                cmbJPCT.ValueMember = "Value";
                cmbPOCT.ValueMember = "Value";
                cmbTGCT.ValueMember = "Value";
                cmbAKCT.ValueMember = "Value";
                cmbRepCT.ValueMember = "Value";

                cmbPPPCT.DisplayMember = "Text";
                cmbJPCT.DisplayMember = "Text";
                cmbPOCT.DisplayMember = "Text";
                cmbTGCT.DisplayMember = "Text";
                cmbAKCT.DisplayMember = "Text";
                cmbRepCT.DisplayMember = "Text";

                List<ComboBoxItem> lstTypeofTradeItems = new List<ComboBoxItem>();

                lstTypeofTradeItems.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_MixedHypen"),
                        Value = "--Mixed--"

                    });

                lstTypeofTradeItems.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_Arcade"),
                        Value = "Arcade"

                    });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_Bar"),
                    Value = "Bar"
                });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_BingoClub"),
                    Value = "Bingo Club"
                });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_Casino"),
                    Value = "Casino"
                });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_Club"),
                    Value = "Club"
                });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_Franchise"),
                    Value = "Franchise"
                });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_IndependantFreeHouse"),
                    Value = "Independant Free House"
                });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_ManagedPublicHouse"),
                    Value = "Managed Public House"
                });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_Other"),
                    Value = "Other"
                });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_TenantedPublicHouse"),
                    Value = "Tenanted Public House"
                });

                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_BarIndependent"),
                    Value = "Bar-Independent"
                });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_BarManaged"),
                    Value = "Bar-Managed"
                });
                lstTypeofTradeItems.Add(new ComboBoxItem()
                {
                    Text = this.GetResourceTextByKey("Key_TenantedBarTenanted"),
                    Value = "Tenanted Bar-Tenanted"
                });



                cmbTypeOfTrade.DataSource=null;
                cmbTypeOfTrade.DataSource = lstTypeofTradeItems;
                cmbTypeOfTrade.DisplayMember = "Text";
                cmbTypeOfTrade.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }

        }

        private void cmbPPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!cmbPPP.Text.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault")))
                    txtPPP.Text = cmbPPP.Text;
                else
                    txtPPP.Text = Convert.ToString(objAdminSubCompanyResult.CompanyDefaultsEntity.Company_Price_Per_Play);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbJP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!cmbJP.Text.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault")))
                    txtJP.Text = cmbJP.Text;
                else
                    txtJP.Text = Convert.ToString(objAdminSubCompanyResult.CompanyDefaultsEntity.Company_Jackpot);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!cmbPO.Text.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault")))
                    txtPO.Text = cmbPO.Text;
                else
                    txtPO.Text = Convert.ToString(objAdminSubCompanyResult.CompanyDefaultsEntity.Company_Percentage_Payout);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbTG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtTG.Text = string.Empty;            
                if (cmbTG.SelectedItem != null && !(((ComboBoxItem)cmbTG.SelectedItem).Text ==  this.GetResourceTextByKey("Key_UseCompanyDefault")))
                    txtTG.Text = ((ComboBoxItem)cmbTG.SelectedItem).Text;
                else
                    foreach (TermsEntity item in objAdminSubCompanyResult.TermsEntities)
                    {
                        if (item.Terms_Group_ID == objAdminSubCompanyResult.CompanyDefaultsEntity.Terms_Group_ID)
                            txtTG.Text = item.Terms_Group_Name;
                    }
                txtTG.Text = (txtTG.Text.Trim() == "0" || txtTG.Text.Trim() == string.Empty) ? this.GetResourceTextByKey("Key_NoneText") : txtTG.Text;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbAK_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtAK.Text = string.Empty;         
                if (cmbAK.SelectedItem != null && !(((ComboBoxItem)cmbAK.SelectedItem).Text ==  this.GetResourceTextByKey("Key_UseCompanyDefault")))
                    txtAK.Text = ((ComboBoxItem)cmbAK.SelectedItem).Text;
                else
                    foreach (SubCompanyAccessEntity item in objAdminSubCompanyResult.AccessEntities)
                    {
                        if (item.Access_Key_ID == objAdminSubCompanyResult.CompanyDefaultsEntity.Access_Key_ID)
                            txtAK.Text = item.Access_Key_Name;
                    }
                txtAK.Text = (txtAK.Text.Trim() == "0" || txtAK.Text.Trim() == string.Empty) ? this.GetResourceTextByKey("Key_NoneText") : txtAK.Text;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtRep.Text = string.Empty;
                if (cmbRep.SelectedItem != null && !(((ComboBoxItem)cmbRep.SelectedItem).Text == this.GetResourceTextByKey("Key_UseCompanyDefault")))
                    txtRep.Text = ((ComboBoxItem)cmbRep.SelectedItem).Text;
                else
                    foreach (SubCompanyStaffEntity item in objAdminSubCompanyResult.StaffEntities)
                    {
                        if (objAdminSubCompanyResult.CompanyDefaultsEntity != null && item.Staff_ID == objAdminSubCompanyResult.CompanyDefaultsEntity.Staff_ID)
                            txtRep.Text = item.StaffName;
                    }
                txtRep.Text = (txtRep.Text.Trim() == "0" || txtRep.Text.Trim() == string.Empty) ? this.GetResourceTextByKey("Key_NoneText") : txtRep.Text;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void LoadSubCompanyDetails(int _CompanyID)
        {
            try
            {
                
                cmbPPPCT.SelectedIndex = -1;
                cmbJPCT.SelectedIndex = -1;
                cmbPOCT.SelectedIndex = -1;
                cmbTGCT.SelectedIndex = -1;
                cmbAKCT.SelectedIndex = -1;
                cmbRepCT.SelectedIndex = -1;
                //SubCOMPANY DETAILS
                objAdminSubCompanyResult = _AdminSubCompany.GetSubCompanyDetails(Convert.ToInt32(_subCompanyId));
                if (_subCompanyId != 0)
                {
                    CompanyID = objAdminSubCompanyResult.SubCompanyEntity.Company_ID;
                    txtName.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Name;
                    txtAddress1.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Address_1;
                    txtAddress2.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Address_2;
                    txtAddress3.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Address_3;
                    txtAddress4.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Address_4;
                    txtAddress5.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Address_5;
                    txtContact.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Contact_Name;
                    txtContactPhoneNumber.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Contact_Phone_No;
                    txtContactEmail.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Contact_Email_Address;
                    txtContactEmail.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Contact_Email_Address;
                    txtPostcode.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Postcode;
                    txtANANumber.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_ANA_Number;
                    TxtPhoneNumber.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Switchboard_Phone_No;
                    chkUseSplitRents.Checked = Convert.ToBoolean(objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Use_Split_Rents);
                    txtInvoiceName.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Invoice_Name;
                    txtBankAccountName.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Account_Name;
                    txtBankAccountSortCode.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Sort_Code;
                    txtBankAccountNumber.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Account_No;
                    txtInvoiceAddress.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Invoice_Address;
                    txtInvoicePostCode.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Invoice_Postcode;
                    txtSageAccountRef.Text = objAdminSubCompanyResult.SubCompanyEntity.Sage_Account_Ref;
                    txtIncomeLedgerCode.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Income_Ledger_Code;
                    cmbTG.DataSource = null;
                    cmbAK.DataSource = null;
                    cmbRep.DataSource = null;
                }
                //LOAD Terms Key
                List<ComboBoxItem> lstTermsKey = new List<ComboBoxItem>();
                lstTermsKey.Add(
                    new ComboBoxItem()
                    {
                        Text =  this.GetResourceTextByKey("Key_UseCompanyDefault"),
                        Value = "-1"
                    }
                       );
                lstTermsKey.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_NoneText"),
                        Value = "0"
                    }
                       );

                foreach (TermsEntity oTermsEntity in objAdminSubCompanyResult.TermsEntities)
                {
                    lstTermsKey.Add(
                        new ComboBoxItem()
                        {
                            Text = oTermsEntity.Terms_Group_Name,
                            Value = oTermsEntity.Terms_Group_ID.ToString()
                        });
                }
                cmbTG.DataSource = lstTermsKey;
                cmbTG.ValueMember = "Value";
                cmbTG.DisplayMember = "Text";
                cmbTG.SelectedIndex = 0;
               

                ////LOAD Access Key
                List<ComboBoxItem> lstAccessKey = new List<ComboBoxItem>();
                lstAccessKey.Add(
                    new ComboBoxItem()
                    {
                        Text =  this.GetResourceTextByKey("Key_UseCompanyDefault"),
                        Value = "-1"
                    }
                       );
                lstAccessKey.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_NoneText"),
                        Value = "0"
                    }
                       );

                foreach (SubCompanyAccessEntity oAccessKeyEntity in objAdminSubCompanyResult.AccessEntities)
                {
                    if (oAccessKeyEntity.Access_Key_Name == null || oAccessKeyEntity.Access_Key_Name.Trim().Equals(string.Empty)) continue;
                    lstAccessKey.Add(
                        new ComboBoxItem()
                        {
                            Text = oAccessKeyEntity.Access_Key_Name,
                            Value = oAccessKeyEntity.Access_Key_ID.ToString()
                        });
                }
                cmbAK.DataSource = lstAccessKey;
                cmbAK.ValueMember = "Value";
                cmbAK.DisplayMember = "Text";
                cmbAK.SelectedIndex = 0;
                ////Load Staffs
                List<ComboBoxItem> lstStaffDetails = new List<ComboBoxItem>();
                lstStaffDetails.Add(
                    new ComboBoxItem()
                    {
                        Text =  this.GetResourceTextByKey("Key_UseCompanyDefault"),
                        Value = "-1"
                    }
                    );
                lstStaffDetails.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_NoneText"),
                        Value = "0"
                    }
                   );
            


                List<DefaultEntity> objRep = Default2business.CreateInstance().GetRepresentativecheck();


                foreach (var v in objRep)
                {
                   
                   
                        lstStaffDetails.Add(
                        new ComboBoxItem()
                        {

                            Text = String.Concat(v.Staff_Last_Name.ToString() + ", " + v.Staff_First_Name.ToString()),
                            Value = v.Staff_ID.ToString()
                        });
                   }



                cmbRep.DataSource = lstStaffDetails;
                
                cmbRep.ValueMember = "Value";
                cmbRep.DisplayMember = "Text";
                cmbRegionStaff.DataSource = GetStaffList(objAdminSubCompanyResult);
                cmbRegionStaff.ValueMember = "Value";
                cmbRegionStaff.DisplayMember = "Text";
                cmbAreaStaff.DataSource = GetStaffList(objAdminSubCompanyResult);
                cmbAreaStaff.ValueMember = "Value";
                cmbAreaStaff.DisplayMember = "Text";
                cmbDistrictStaff.DataSource = GetStaffList(objAdminSubCompanyResult);
                cmbDistrictStaff.ValueMember = "Value";
                cmbDistrictStaff.DisplayMember = "Text";
                ////Load Companiess
                List<ComboBoxItem> lstCompanies = new List<ComboBoxItem>();

                foreach (SubCompanyCompanyEntity oCompanyEntity in objAdminSubCompanyResult.CompanyEntities)
                {
                    lstCompanies.Add(
                    new ComboBoxItem()
                    {
                        Text = oCompanyEntity.Company_Name,
                        Value = oCompanyEntity.Company_ID.ToString()
                    }
                       );
                }
                cmbCompany.DataSource = lstCompanies;
                cmbCompany.ValueMember = "Value";
                cmbCompany.DisplayMember = "Text";
                if (_CompanyID > 0)
                    cmbCompany.SelectedValue = _CompanyID.ToString();
                else
                    cmbCompany.SelectedIndex = 0;
                //Load Models
                List<ComboBoxItem> lstModels = new List<ComboBoxItem>();
                lstModels.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_NoneHyphen"),
                        Value = "0"
                    }
                   );

                foreach (SubCompanyModelEntity oModelEntity in objAdminSubCompanyResult.ModelEntities)
                {
                    lstModels.Add(
                    new ComboBoxItem()
                    {
                        Text = oModelEntity.Company_Model_Set_Description,
                        Value = oModelEntity.Company_Model_Set_ID.ToString()
                    }
                       );
                }
                cmbModelSet.DataSource = lstModels;
                cmbModelSet.ValueMember = "Value";
                cmbModelSet.DisplayMember = "Text";
                cmbModelSet.SelectedIndex = 0;
                //Load Opening Hours
                List<ComboBoxItem> lstHours = new List<ComboBoxItem>();
                lstHours.Add(
                    new ComboBoxItem()
                    {
                        Text = this.GetResourceTextByKey("Key_NoneHyphen"),
                        Value = "0"
                    }
                   );
                foreach (SubCompanyHourEntity oHourEntity in objAdminSubCompanyResult.HoursEntities)
                {
                    lstHours.Add(
                    new ComboBoxItem()
                    {
                        Text = oHourEntity.Standard_Opening_Hours_Description,
                        Value = oHourEntity.Standard_Opening_Hours_ID.ToString()
                    }
                       );
                }
                cmbOpeningTimes.DataSource = lstHours;
                cmbOpeningTimes.ValueMember = "Value";
                cmbOpeningTimes.DisplayMember = "Text";
                cmbOpeningTimes.SelectedIndex = 0;
                if (_subCompanyId > 0)
                {
                    if (Convert.ToBoolean(objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Price_Per_Play_Default))
                        cmbPPP.Text =  this.GetResourceTextByKey("Key_UseCompanyDefault");
                    else
                    {
                        if (objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Price_Per_Play == "")
                        {
                            cmbPPP.Text =  this.GetResourceTextByKey("Key_UseCompanyDefault");
                        }
                        else
                        {
                            cmbPPP.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Price_Per_Play;
                        }
                    }
                    if (Convert.ToBoolean(objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Jackpot_Default))
                        cmbJP.Text =  this.GetResourceTextByKey("Key_UseCompanyDefault");
                    else
                    {
                        if (objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Jackpot == "")
                        {
                            cmbJP.Text =  this.GetResourceTextByKey("Key_UseCompanyDefault");
                        }
                        else
                        {
                            cmbJP.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Jackpot;
                        }
                    }
                    if (Convert.ToBoolean(objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Percentage_Payout_Default))
                        cmbPO.Text =  this.GetResourceTextByKey("Key_UseCompanyDefault");
                    else
                    {
                        if (objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Percentage_Payout == "")
                        {
                            cmbPO.Text =  this.GetResourceTextByKey("Key_UseCompanyDefault");
                        }
                        else
                        {
                            cmbPO.Text = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Percentage_Payout;
                        }
                    }
                    cmbTypeOfTrade.SelectedValue = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Trade_Type;
                    
                    cmbCompany.SelectedValue = objAdminSubCompanyResult.SubCompanyEntity.Company_ID.ToString();
                    
                    cmbModelSet.SelectedValue = Convert.ToString(objAdminSubCompanyResult.SubCompanyEntity.Company_Model_Set_ID);
                    if (cmbModelSet.SelectedValue == null)
                    {
                        cmbModelSet.SelectedIndex = 0;
                    }
                    
                    
                    cmbOpeningTimes.SelectedValue = objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Default_Opening_Hours_ID.ToString();
                    if (Convert.ToBoolean(objAdminSubCompanyResult.SubCompanyEntity.Terms_Group_ID_Default))
                        cmbTG.SelectedIndex = 0;
                    else                        
                        frmUtobj.setListBox(cmbTG, "", Convert.ToInt64(objAdminSubCompanyResult.SubCompanyEntity.Terms_Group_ID));

                    if (Convert.ToBoolean(objAdminSubCompanyResult.SubCompanyEntity.Access_Key_ID_Default))
                        cmbAK.SelectedIndex = 0;
                    else
                        frmUtobj.setListBox(cmbAK, "", Convert.ToInt64(objAdminSubCompanyResult.SubCompanyEntity.Access_Key_ID));                        

                    if (Convert.ToBoolean(objAdminSubCompanyResult.SubCompanyEntity.Staff_ID_Default))
                        cmbRep.SelectedIndex = 0;
                    else
                        frmUtobj.setListBox(cmbRep,"", Convert.ToInt64(objAdminSubCompanyResult.SubCompanyEntity.Staff_ID));                        

                    //txtPPP.Text = cmbPPP.Text;
                    //txtAK.Text = ((ComboBoxItem)cmbAK.SelectedItem).Text;
                    //txtTG.Text = ((ComboBoxItem)cmbTG.SelectedItem).Text;
                    //txtRep.Text = ((ComboBoxItem)cmbRep.SelectedItem).Text;
                    //txtJP.Text = cmbJP.Text;
                    //txtPO.Text = cmbPO.Text;
                    //lvRegions
                    LoadRegionDetails(objAdminSubCompanyResult.RegionEntities);
                    //
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public SubCompanyEntity GetSubCompanyDetails()
        {
            SubCompanyEntity _SubCompanyEntity = null;
            try
            {
                //SubCOMPANY DETAILS
                _SubCompanyEntity = new SubCompanyEntity();
                _SubCompanyEntity.Sub_Company_Name = txtName.Text.Trim();
                _SubCompanyEntity.Sub_Company_Address_1 = txtAddress1.Text.Trim();
                _SubCompanyEntity.Sub_Company_Address_2 = txtAddress2.Text.Trim();
                _SubCompanyEntity.Sub_Company_Address_3 = txtAddress3.Text.Trim();
                _SubCompanyEntity.Sub_Company_Address_4 = txtAddress4.Text.Trim();
                _SubCompanyEntity.Sub_Company_Address_5 = txtAddress5.Text.Trim();
                _SubCompanyEntity.Sub_Company_Contact_Name = txtContact.Text.Trim();
                _SubCompanyEntity.Sub_Company_Contact_Phone_No = txtContactPhoneNumber.Text.Trim();
                _SubCompanyEntity.Sub_Company_Contact_Email_Address = txtContactEmail.Text.Trim();
                _SubCompanyEntity.Sub_Company_Postcode = txtPostcode.Text.Trim();
                _SubCompanyEntity.Sub_Company_ANA_Number = txtANANumber.Text.Trim();
                _SubCompanyEntity.Sub_Company_Switchboard_Phone_No = TxtPhoneNumber.Text.Trim();
                _SubCompanyEntity.Sub_Company_Use_Split_Rents = chkUseSplitRents.Checked;
                _SubCompanyEntity.Sub_Company_Invoice_Name = txtInvoiceName.Text.Trim();
                _SubCompanyEntity.Sub_Company_Account_Name = txtBankAccountName.Text.Trim();
                _SubCompanyEntity.Sub_Company_Sort_Code = txtBankAccountSortCode.Text.Trim();
                _SubCompanyEntity.Sub_Company_Account_No = txtBankAccountNumber.Text.Trim();
                _SubCompanyEntity.Sub_Company_Invoice_Address = txtInvoiceAddress.Text.Trim();
                _SubCompanyEntity.Sub_Company_Invoice_Postcode = txtInvoicePostCode.Text.Trim();
                _SubCompanyEntity.Sage_Account_Ref = txtSageAccountRef.Text.Trim();
                _SubCompanyEntity.Sub_Company_Income_Ledger_Code = txtIncomeLedgerCode.Text.Trim();
                _SubCompanyEntity.Sub_Company_Price_Per_Play = cmbPPP.Text;
                _SubCompanyEntity.Company_ID = Convert.ToInt32(cmbCompany.SelectedValue);
                _SubCompanyEntity.Company_Model_Set_ID = Convert.ToInt32(cmbModelSet.SelectedValue);
                _SubCompanyEntity.Sub_Company_Trade_Type = Convert.ToString(cmbTypeOfTrade.SelectedValue);
                _SubCompanyEntity.Sub_Company_Default_Opening_Hours_ID = Convert.ToInt32(cmbOpeningTimes.SelectedValue);

                _SubCompanyEntity.Terms_Group_ID = Convert.ToInt32(cmbTG.SelectedValue);
                _SubCompanyEntity.Access_Key_ID = Convert.ToInt32(cmbAK.SelectedValue);
                _SubCompanyEntity.Staff_ID = Convert.ToInt32(cmbRep.SelectedValue);
                _SubCompanyEntity.Sub_Company_Percentage_Payout = txtPO.Text;
                _SubCompanyEntity.Sub_Company_Jackpot = txtJP.Text;

                _SubCompanyEntity.Staff_ID_Default = Convert.ToBoolean(cmbRep.SelectedItem != null && (cmbRep.SelectedText.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault"))));
                _SubCompanyEntity.Terms_Group_ID_Default = Convert.ToBoolean(cmbTG.SelectedItem != null && (cmbAK.SelectedText.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault"))));
                _SubCompanyEntity.Sub_Company_Percentage_Payout_Default = Convert.ToBoolean(cmbPO.Text != null && cmbPO.Text.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault")));
                _SubCompanyEntity.Sub_Company_Jackpot_Default = Convert.ToBoolean(cmbJP.Text != null && cmbJP.Text.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault")));
                _SubCompanyEntity.Sub_Company_Price_Per_Play_Default = Convert.ToBoolean(cmbPPP.Text != null && cmbPPP.Text.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault")));
                _SubCompanyEntity.Access_Key_ID_Default = Convert.ToBoolean(cmbAK.SelectedItem != null && (cmbAK.SelectedText.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault"))));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return _SubCompanyEntity;
        }

        private void LoadRegionDetails(List<SubCompanyRegionEntity> RegionEntities)
        {
            try
            {
                lvRegions.Items.Clear();
                foreach (SubCompanyRegionEntity item in RegionEntities)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = item.Sub_Company_Region_Name;
                    lvItem.SubItems.Add((Convert.ToInt32(item.Staff_ID) > 0) ? (item.Staff_Last_Name + ", " + item.Staff_First_Name) : "-");
                    lvItem.SubItems.Add(item.Sub_Company_Region_Description);
                    lvItem.Tag = item;
                    lvRegions.Items.Add(lvItem);
                }
                txtRegionName.Text = string.Empty;
                cmbRegionStaff.SelectedIndex = 0;
                txtRegionDescription.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadAreaDetails(List<SubCompanyAreaEntity> AreaEntities)
        {
            try
            {
                lvAreas.Items.Clear();
                foreach (SubCompanyAreaEntity item in AreaEntities)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = item.Sub_Company_Area_Name;
                    lvItem.SubItems.Add((Convert.ToInt32(item.Staff_ID) > 0) ? (item.Staff_Last_Name + ", " + item.Staff_First_Name) : "-");
                    lvItem.SubItems.Add(item.Sub_Company_Area_Description);
                    lvItem.Tag = item;
                    lvAreas.Items.Add(lvItem);
                }
                txtAreaName.Text = string.Empty;
                cmbAreaStaff.SelectedValue = 0;
                txtAreaDescription.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadDistrictDetails(List<SubCompanyDistrictEntity> DistrictEntities)
        {
            try
            {
                lvDistricts.Items.Clear();
                foreach (SubCompanyDistrictEntity item in DistrictEntities)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = item.Sub_Company_District_Name;
                    lvItem.SubItems.Add((Convert.ToInt32(item.Staff_ID) > 0) ? (item.Staff_Last_Name + ", " + item.Staff_First_Name) : "-");
                    lvItem.SubItems.Add(item.Sub_Company_District_Description);
                    lvItem.Tag = item;
                    lvDistricts.Items.Add(lvItem);
                }
                txtDistrictName.Text = string.Empty;
                cmbDistrictStaff.SelectedValue = 0;
                txtDistrictDescription.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private List<ComboBoxItem> GetStaffList(AdminSubCompanyResult objAdminSubCompanyResult)
        {
            List<ComboBoxItem> lstRep = new List<ComboBoxItem>();
            try
            {
                lstRep.Add(
                        new ComboBoxItem()
                        {
                            Text = this.GetResourceTextByKey("Key_NoneText"),
                            Value = "0"
                        }
                           );

                foreach (SubCompanyStaffEntity oStaffEntity in objAdminSubCompanyResult.StaffEntities)
                {
                    lstRep.Add(
                    new ComboBoxItem()
                    {
                        Text = oStaffEntity.StaffName,
                        Value = oStaffEntity.Staff_ID.ToString()
                    }
                       );
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstRep;
        }

        private void lvRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtRegionName.Enabled = true;
                cmbRegionStaff.Enabled = true;
                txtRegionDescription.Enabled = true;
                if (lvRegions.SelectedItems.Count <= 0) return;
                ListViewItem item = (ListViewItem)lvRegions.SelectedItems[0];
                if (item != null)
                {
                    SubCompanyRegionEntity objSubCompanyRegionEntity = (SubCompanyRegionEntity)item.Tag;
                    txtRegionName.Text = objSubCompanyRegionEntity.Sub_Company_Region_Name;
                    txtRegionDescription.Text = objSubCompanyRegionEntity.Sub_Company_Region_Description;
                    cmbRegionStaff.SelectedValue = Convert.ToString(objSubCompanyRegionEntity.Staff_ID);
                    LoadAreaDetails(_AdminSubCompany.GetSubCompanyAreaDetails(objSubCompanyRegionEntity.Sub_Company_Region_ID));
                    LoadDistrictDetails(_AdminSubCompany.GetSubCompanyDistrictDetails(0));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            //ListViewItem
        }

        private void lvAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtAreaName.Enabled = true;
                cmbAreaStaff.Enabled = true;
                txtAreaDescription.Enabled = true;
                if (lvAreas.SelectedItems.Count <= 0) return;
                ListViewItem item = (ListViewItem)lvAreas.SelectedItems[0];
                if (item != null)
                {
                    SubCompanyAreaEntity objSubCompanyAreaEntity = (SubCompanyAreaEntity)item.Tag;
                    txtAreaName.Text = objSubCompanyAreaEntity.Sub_Company_Area_Name;
                    txtAreaDescription.Text = objSubCompanyAreaEntity.Sub_Company_Area_Description;
                    cmbAreaStaff.SelectedValue = Convert.ToString(objSubCompanyAreaEntity.Staff_ID);
                    LoadDistrictDetails(_AdminSubCompany.GetSubCompanyDistrictDetails(objSubCompanyAreaEntity.Sub_Company_Area_ID));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void lvDistricts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                txtDistrictName.Enabled = true;
                cmbDistrictStaff.Enabled = true;
                txtDistrictDescription.Enabled = true;
                if (lvDistricts.SelectedItems.Count <= 0) return;
                ListViewItem item = (ListViewItem)lvDistricts.SelectedItems[0];
                if (item != null)
                {
                    SubCompanyDistrictEntity objSubCompanyDistrictEntity = (SubCompanyDistrictEntity)item.Tag;
                    txtDistrictName.Text = objSubCompanyDistrictEntity.Sub_Company_District_Name;
                    txtDistrictDescription.Text = objSubCompanyDistrictEntity.Sub_Company_District_Description;
                    cmbDistrictStaff.SelectedValue = Convert.ToString(objSubCompanyDistrictEntity.Staff_ID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRegionNew_Click(object sender, EventArgs e)
        {
            try
            {
                txtRegionName.Enabled = true;
                txtRegionDescription.Enabled = true;
                cmbRegionStaff.Enabled = true;
                btnRegionNew.Visible = false;
                btnRegionCancel.Visible = true;
                txtRegionName.Text = string.Empty;
                txtRegionDescription.Text = string.Empty;
                cmbRegionStaff.SelectedIndex = 0;
                lvRegions.SelectedItems.Clear();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnAreaNew_Click(object sender, EventArgs e)
        {
            try
            {
                txtAreaName.Enabled = true;
                txtAreaDescription.Enabled = true;
                cmbAreaStaff.Enabled = true;
                btnAreaNew.Visible = false;
                btnAreaCancel.Visible = true;
                txtAreaName.Text = string.Empty;
                txtAreaDescription.Text = string.Empty;
                cmbAreaStaff.SelectedIndex = 0;
                lvAreas.SelectedItems.Clear();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnDistrictNew_Click(object sender, EventArgs e)
        {
            try
            {
                txtDistrictName.Enabled = true;
                txtDistrictDescription.Enabled = true;
                cmbDistrictStaff.Enabled = true;
                btnDistrictNew.Visible = false;
                btnDistrictCancel.Visible = true;
                txtDistrictName.Text = string.Empty;
                txtDistrictDescription.Text = string.Empty;
                cmbDistrictStaff.SelectedIndex = 0;
                lvDistricts.SelectedItems.Clear();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRegionCancel_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                tblArea.Enabled = !btnRegionCancel.Visible;
                tblDistrict.Enabled = !btnRegionCancel.Visible;
                lvRegions.Enabled = !btnRegionCancel.Visible;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnAreaCancel_Click(object sender, EventArgs e)
        {
            try
            {                
                btnAreaCancel.Visible = false;
                btnAreaNew.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnAreaCancel_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                tblRegion.Enabled = !btnAreaCancel.Visible;
                tblDistrict.Enabled = !btnAreaCancel.Visible;
                lvAreas.Enabled = !btnAreaCancel.Visible;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnDistrictCancel_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                tblRegion.Enabled = !btnDistrictCancel.Visible;
                tblArea.Enabled = !btnDistrictCancel.Visible;
                lvDistricts.Enabled = !btnDistrictCancel.Visible;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnDistrictCancel_Click(object sender, EventArgs e)
        {
            try
            {
                btnDistrictCancel.Visible = false;
                btnDistrictNew.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRegionCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //txtRegionName.Enabled = txtRegionDescription.Enabled =
                //txtAreaName.Enabled = txtAreaDescription.Enabled =
                //txtDistrictName.Enabled = txtDistrictDescription.Enabled = false;
                btnRegionCancel.Visible = false;
                btnRegionNew.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRegionApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnRegionNew.Visible == true && lvRegions.SelectedItems.Count == 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_REGION"), this.ParentForm.Text);
                    return;
                }
                if (txtRegionName.Text.Trim().Equals(string.Empty))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_REGION_NAME_EMPTY"), this.ParentForm.Text);
                    return;
                }
                SubCompanyRegionEntity entity = null;
                bool newRegion = false;
                if (lvRegions.SelectedItems.Count <= 0)
                {
                    entity = new SubCompanyRegionEntity
                    {
                        Sub_Company_Region_ID = 0,

                    };
                    newRegion = true;
                }
                else
                {
                    entity = (SubCompanyRegionEntity)((ListViewItem)lvRegions.SelectedItems[0]).Tag;
                }
                if (_subCompanyId <= 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SUBCOMPANY_MUST_CREATED"), this.ParentForm.Text);
                    return;
                }
                List<ChangeHistory> lst = new List<ChangeHistory>();
                entity.Sub_Company_ID = Convert.ToInt32(_subCompanyId);

                if (entity.Sub_Company_Region_Name != txtRegionName.Text)
                    lst.Add(new ChangeHistory() { NewValue = txtRegionName.Text, OldValue = entity.Sub_Company_Region_Name, ProperyName = "Region Name" });
                entity.Sub_Company_Region_Name = txtRegionName.Text;
                if (entity.Sub_Company_Region_Description != txtRegionDescription.Text)
                    lst.Add(new ChangeHistory() { NewValue = txtRegionDescription.Text, OldValue = entity.Sub_Company_Region_Description, ProperyName = "Region Description" });
                entity.Sub_Company_Region_Description = txtRegionDescription.Text;
                if (entity.Staff_ID != Convert.ToInt32(cmbRegionStaff.SelectedValue))
                    lst.Add(new ChangeHistory() { NewValue = cmbRegionStaff.Text, OldValue = (Convert.ToInt32(entity.Staff_ID) <= 0) ? "none" : entity.Staff_Last_Name + " ," + entity.Staff_First_Name, ProperyName = "Staff Region" });
                entity.Staff_ID = Convert.ToInt32(cmbRegionStaff.SelectedValue);
                string message = _AdminSubCompany.UpdateSubCompanyRegion(entity);
                if (!message.Trim().Equals(string.Empty))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1,message.Trim()), this.ParentForm.Text);
                    return;
                }
                else
                {
                    string newDesc = string.Format("Record {0} added to Sub Company Administration.Sub_Company_Region", txtRegionName.Text);
                    string newAuditField = "Sub_Company_Region_Name";
                    string newvalue = txtRegionName.Text;
                    string screenName = "Sub Company Administration.Sub_Company_Region";
                    AuditChanges(!newRegion, lst, newDesc, newAuditField, newvalue, screenName);

                    LoadRegionDetails(_AdminSubCompany.GetSubCompanyRegionDetails(Convert.ToInt32(_subCompanyId)));
                    btnRegionCancel_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AuditChanges(bool isNew, List<ChangeHistory> lst, string newDesc, string newAuditField, string newvalue, string screenName)
        {
            bool bOperationType = false;
            try
            {
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                int iCount = 0;
                StringBuilder oStringBuild = new StringBuilder();
                string sOldValue = string.Empty;
                string sNewValue = string.Empty;

                foreach (ChangeHistory ch in lst)
                {
                    oStringBuild.Append(ch.ProperyName).Append(",");
                    iCount = oStringBuild.ToString().Split(',').Count();
                    if (iCount == 2)
                    {
                        sOldValue = ch.OldValue;
                        sNewValue = ch.NewValue;
                    }
                }
                if (!isNew)
                {
                    
                    if (iCount > 2)
                    {
                        bOperationType = true;
                        Audit(oStringBuild.ToString(), "","", "Sub Company [" + oStringBuild.ToString() + "] Modified", bOperationType);
                    }
                    else if(iCount!=0)
                    {
                        bOperationType = true;
                        Audit(oStringBuild.ToString(), sOldValue, sNewValue, string.Format("Sub Company {0} Modified ..[{1}]:{2}-->{3}", objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Name, oStringBuild.ToString(), sOldValue, sNewValue), bOperationType);
                    }
                }
                else
                {
                    Audit(oStringBuild.ToString(), "", txtName.Text, "Sub Company " + txtName.Text + " Added.", bOperationType);
                }
                bOperationType = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bOperationType = false;
            }
        }
        public void Audit(string Audit_Field, string sOldValue, string sNewValue, string Desc, bool AuditOperationType)
        {
            AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
            business.InsertAuditData(new Audit.Transport.Audit_History
            {
                EnterpriseModuleName = ModuleNameEnterprise.AUDIT_SUBCOMPANY,
                Audit_Slot = "",
                Audit_Screen_Name = "SubCompany Administration",
                Audit_Field = Audit_Field,
                Audit_Old_Vl = sOldValue,
                Audit_New_Vl = sNewValue,
                Audit_Desc = Desc,
                AuditOperationType = (AuditOperationType == true) ? OperationType.MODIFY : OperationType.ADD,
                Audit_User_ID = AppEntryPoint.Current.UserId,
                Audit_User_Name = AppEntryPoint.Current.UserName
            }, false);
        }
        private void btnAreaApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAreaNew.Visible == true && lvAreas.SelectedItems.Count == 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_AREA"), this.ParentForm.Text);
                    return;
                }

                if (txtAreaName.Text.Trim().Equals(string.Empty))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_AREA_NAME_EMPTY"), this.ParentForm.Text);
                    return;
                }
                SubCompanyAreaEntity entity = null;
                bool newArea = false;
                if (lvAreas.SelectedItems.Count <= 0)
                {
                    entity = new SubCompanyAreaEntity
                    {
                        Sub_Company_Area_ID = 0,
                    };
                    newArea = true;
                }
                else
                {
                    entity = (SubCompanyAreaEntity)((ListViewItem)lvAreas.SelectedItems[0]).Tag;
                }
                if (lvRegions.SelectedItems.Count <= 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_REGION_MUST_SELECTED"), this.ParentForm.Text);
                    return;
                }
                else
                {
                    entity.Sub_Company_Region_ID = ((SubCompanyRegionEntity)((ListViewItem)lvRegions.SelectedItems[0]).Tag).Sub_Company_Region_ID;
                }
                List<ChangeHistory> lst = new List<ChangeHistory>();

                if (entity.Sub_Company_Area_Name != txtAreaName.Text.ToString())
                    lst.Add(new ChangeHistory() { NewValue = txtAreaName.Text, OldValue = entity.Sub_Company_Area_Name, ProperyName = "Sub Company AreaName" });
                entity.Sub_Company_Area_Name = txtAreaName.Text;
                if (entity.Sub_Company_Area_Description != txtAreaDescription.Text)
                    lst.Add(new ChangeHistory() { NewValue = txtAreaDescription.Text, OldValue = entity.Sub_Company_Area_Description, ProperyName = "Sub Company Area Description" });
                entity.Sub_Company_Area_Description = txtAreaDescription.Text;
                if (Convert.ToInt32(entity.Staff_ID) != Convert.ToInt32(cmbAreaStaff.SelectedValue))
                    lst.Add(new ChangeHistory() { NewValue = cmbAreaStaff.Text, OldValue = (Convert.ToInt32(entity.Staff_ID) <= 0) ? "none" : entity.Staff_Last_Name + " ," + entity.Staff_First_Name, ProperyName = "Staff" });
                entity.Staff_ID = Convert.ToInt32(cmbAreaStaff.SelectedValue);
                string message = _AdminSubCompany.UpdateSubCompanyArea(entity);
                if (!message.Trim().Equals(string.Empty))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1,message.Trim()), this.ParentForm.Text);
                    return;
                }
                else
                {
                    string newDesc = string.Format("Record {0} added to Sub Company Administration.Sub_Company_Area", txtAreaName.Text);
                    string newAuditField = "Sub_Company_Area_Name";
                    string newvalue = txtAreaName.Text;
                    string screenName = "Sub Company Administration.Sub_Company_Area";
                    AuditChanges(!newArea, lst, newDesc, newAuditField, newvalue, screenName);
                    LoadAreaDetails(_AdminSubCompany.GetSubCompanyAreaDetails(entity.Sub_Company_Region_ID));
                    btnAreaCancel_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnDistrictApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnDistrictNew.Visible == true && lvDistricts.SelectedItems.Count == 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_DISTRICT"), this.ParentForm.Text);
                    return;
                }
                if (txtDistrictName.Text.Trim().Equals(string.Empty))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_DISTRICT_NAME_EMPTY"), this.ParentForm.Text);
                    return;
                }
                SubCompanyDistrictEntity entity = null;
                bool newDistrict = false;
                if (lvDistricts.SelectedItems.Count <= 0)
                {
                    entity = new SubCompanyDistrictEntity
                    {
                        Sub_Company_District_ID = 0,
                    };
                    newDistrict = true;
                }
                else
                {
                    entity = (SubCompanyDistrictEntity)((ListViewItem)lvDistricts.SelectedItems[0]).Tag;
                }
                if (lvAreas.SelectedItems.Count <= 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_AREA_MUST_SELECTED"), this.ParentForm.Text);
                    return;
                }
                else
                {
                    entity.Sub_Company_Area_ID = ((SubCompanyAreaEntity)((ListViewItem)lvAreas.SelectedItems[0]).Tag).Sub_Company_Area_ID;
                }
                List<ChangeHistory> lst = new List<ChangeHistory>();

                if (entity.Sub_Company_District_Name != txtDistrictName.Text)
                    lst.Add(new ChangeHistory() { NewValue = txtDistrictName.Text, OldValue = entity.Sub_Company_District_Name, ProperyName = "SubCompany District Name" });
                entity.Sub_Company_District_Name = txtDistrictName.Text;

                if (entity.Sub_Company_District_Description != txtDistrictDescription.Text)
                    lst.Add(new ChangeHistory() { NewValue = txtDistrictDescription.Text, OldValue = entity.Sub_Company_District_Description, ProperyName = "SubCompany District Description" });
                entity.Sub_Company_District_Description = txtDistrictDescription.Text;

                if (entity.Staff_ID != Convert.ToInt32(cmbDistrictStaff.SelectedValue))
                    lst.Add(new ChangeHistory() { NewValue = cmbDistrictStaff.Text, OldValue = (Convert.ToInt32(entity.Staff_ID) <= 0) ? "none" : entity.Staff_Last_Name + " ," + entity.Staff_First_Name, ProperyName = "Staff" });
                entity.Staff_ID = Convert.ToInt32(cmbDistrictStaff.SelectedValue);

                string message = _AdminSubCompany.UpdateSubCompanyDistrict(entity);
                if (!message.Trim().Equals(string.Empty))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1,message.Trim()), this.ParentForm.Text);
                    return;
                }
                else
                {
                    string newDesc = string.Format("Record {0} added to Sub Company Administration.Sub_Company_District", txtAreaName.Text);
                    string newAuditField = "Sub_Company_District_Name";
                    string newvalue = txtAreaName.Text;
                    string screenName = "Sub Company Administration.Sub_Company_District";
                    AuditChanges(!newDistrict, lst, newDesc, newAuditField, newvalue, screenName);
                    LoadDistrictDetails(_AdminSubCompany.GetSubCompanyDistrictDetails(entity.Sub_Company_Area_ID));
                    btnDistrictCancel_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {

                if (!txtName.CheckEmptyAndReturnOnError(this.GetResourceTextByKey(1, "MSG_SUBCOMPANY_EMPTY"), this.GetResourceTextByKey(1, "MSG_SUBCOMPANY_ADMIN"), MessageBoxIcon.Warning))
                {
                    SSTab1.SelectedTab = tbDetails;
                    return;
                }
                if (!ValidateEmailAndReturnOnError(txtContactEmail))
                {
                    SSTab1.SelectedTab = tbDetails;
                    return;
                }
                bool updateAllSites = false;
                if (objAdminSubCompanyResult.SubCompanyEntity != null && Convert.ToString(cmbTypeOfTrade.SelectedValue) != objAdminSubCompanyResult.SubCompanyEntity.Sub_Company_Trade_Type)
                {
                    updateAllSites = this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_UPDATE_ALL_SITES_WITH_TRADE_TYPE"), this.ParentForm.Text) == DialogResult.Yes;
                }
                SubCompanyEntity _SubCompanyEntity = GetSubCompanyDetails();
                if (_SubCompanyEntity == null)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_FAILED_TO_UPDATE_SUBCOMPANY"), this.ParentForm.Text);
                    return;
                }
                //A sub company with that name already exists
                if (_AdminSubCompany.IsSubCompanyExist(txtName.Text.Trim(), _SubCompanyEntity.Company_ID, _subCompanyId))
                {
                    SSTab1.SelectedTab = tbDetails;
                    txtName.Focus();
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SUBCOMPANY_NAME_EXISTS"), this.ParentForm.Text);
                    return;
                }
                _SubCompanyEntity.Sub_Company_Price_Per_Play_Default = _SubCompanyEntity.Sub_Company_Price_Per_Play_Default ?? false;
                _SubCompanyEntity.Sub_Company_Percentage_Payout_Default = _SubCompanyEntity.Sub_Company_Percentage_Payout_Default ?? false;
                _SubCompanyEntity.Sub_Company_Jackpot_Default = _SubCompanyEntity.Sub_Company_Jackpot_Default ?? false;
                List<ChangeHistory> changeHistory = _SubCompanyEntity.GetUpdatedFields(objAdminSubCompanyResult.SubCompanyEntity);
                if (_AdminSubCompany.UpdateSubCompany(ref _subCompanyId, _SubCompanyEntity, updateAllSites))
                {
                    this.ShowInfoMessageBox(String.Format(this.GetResourceTextByKey(1, "MSG_SUBCOMPANY_UPDATE_SUCCESS"), txtName.Text.Trim()), this.ParentForm.Text);
                }
                string newDesc = string.Format("Subcompany '{0}' added to {1}", _SubCompanyEntity.Sub_Company_Name, cmbCompany.Text);
                string newAuditField = "Sub_Company_Name";
                string newvalue = _SubCompanyEntity.Sub_Company_Name;
                string screenName = "SubCompany Administration.SubCompany";
                bool NewSubCompany = (objAdminSubCompanyResult.SubCompanyEntity == null);
                changeHistory = NewSubCompany ? _SubCompanyEntity.GetInitialFields() : changeHistory;
                AuditChanges(NewSubCompany, changeHistory, newDesc, newAuditField, newvalue, screenName);
                LoadSubCompanyDetails(0);
                EnableControls();
                //Notify Change to main screen 
                if (NotifyChanges != null)
                {
                    NotifyChanges(Convert.ToInt32(_subCompanyId));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private void btnDefaults_click(object sender, EventArgs e)
        {

            string strAudit_Field = string.Empty;
            string strNewValue = string.Empty;
            string strAuditCascadeType = string.Empty;
            string strOldValue = string.Empty;
            int iValue = 0;
            try
            {


                switch (((Button)sender).Name)
                {
                    case "btnPPPApply":


                        strNewValue = txtPPP.Text;
                        if (!strNewValue.IsNumeric())
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_NUMBER"), this.ParentForm.Text);
                            cmbPPP.Focus();
                            return;
                        }
                        if (cmbPPPCT.SelectedIndex == -1)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_CASCADE"), this.ParentForm.Text);
                            return;
                        }

                        int.TryParse(strNewValue, out iValue);
                        _AdminSubCompany.UpdateSubCompanyAdminDefaults(Convert.ToInt32(objAdminSubCompanyResult.SubCompanyEntity.Company_ID), false, false, false, true, false, false, Convert.ToInt64(iValue), Convert.ToInt32(cmbPPPCT.SelectedValue), 2, Convert.ToBoolean(cmbPPP.SelectedItem != null && (cmbPPP.SelectedItem).Equals( this.GetResourceTextByKey("Key_UseCompanyDefault"))), AppEntryPoint.Current.UserId, AppEntryPoint.Current.UserName, "MODIFY", "", _subCompanyId);
                        break;
                    case "btnJPApply":

                        strNewValue = txtJP.Text;
                        if (!strNewValue.IsNumeric())
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_NUMBER"), this.ParentForm.Text);
                            cmbJP.Focus();
                            return;
                        }
                        if (cmbJPCT.SelectedIndex == -1)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_CASCADE"), this.ParentForm.Text);
                            return;
                        }

                        _AdminSubCompany.UpdateSubCompanyAdminDefaults(Convert.ToInt32(objAdminSubCompanyResult.SubCompanyEntity.Company_ID), false, true, false, false, false, false, Convert.ToInt64(strNewValue), Convert.ToInt32(cmbJPCT.SelectedValue), 2, Convert.ToBoolean(cmbJP.SelectedItem != null && (cmbJP.SelectedItem).Equals( this.GetResourceTextByKey("Key_UseCompanyDefault"))), AppEntryPoint.Current.UserId, AppEntryPoint.Current.UserName, "MODIFY", "", _subCompanyId);
                        break;
                    case "btnPercApply":


                        strNewValue = txtPO.Text;
                        if (!strNewValue.IsNumeric())
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_NUMBER"), this.ParentForm.Text);
                            cmbPO.Focus();
                            return;
                        }
                        if (cmbPOCT.SelectedIndex == -1)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_CASCADE"), this.ParentForm.Text);
                            return;
                        }
                        int.TryParse(strNewValue, out iValue);
                        _AdminSubCompany.UpdateSubCompanyAdminDefaults(Convert.ToInt32(objAdminSubCompanyResult.SubCompanyEntity.Company_ID), false, false, true, false, false, false, iValue, Convert.ToInt32((cmbPOCT.SelectedValue)), 2, Convert.ToBoolean(cmbPO.SelectedItem != null && (cmbPO.SelectedItem).Equals( this.GetResourceTextByKey("Key_UseCompanyDefault"))), AppEntryPoint.Current.UserId, AppEntryPoint.Current.UserName, "MODIFY", "", _subCompanyId);
                        break;
                    case "btnTPApply":
                        if (cmbTGCT.SelectedIndex == -1)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_CASCADE"), this.ParentForm.Text);
                            return;
                        }
                        _AdminSubCompany.UpdateSubCompanyAdminDefaults(Convert.ToInt32(objAdminSubCompanyResult.SubCompanyEntity.Company_ID), false, false, false, false, false, true, Convert.ToInt64(Convert.ToString(cmbTG.SelectedValue)), Convert.ToInt32(Convert.ToString(cmbTGCT.SelectedValue)), 2, Convert.ToBoolean(cmbTG.SelectedItem != null && ((ComboBoxItem)cmbTG.SelectedItem).Text.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault"))), AppEntryPoint.Current.UserId, AppEntryPoint.Current.UserName, "MODIFY", "", _subCompanyId);
                        break;
                    case "btnAKeyApply":
                        if (cmbAKCT.SelectedIndex == -1)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_CASCADE"), this.ParentForm.Text);
                            return;
                        }
                        _AdminSubCompany.UpdateSubCompanyAdminDefaults(Convert.ToInt32(objAdminSubCompanyResult.SubCompanyEntity.Company_ID), true, false, false, false, false, false, Convert.ToInt64(Convert.ToString(cmbAK.SelectedValue)), Convert.ToInt32(Convert.ToInt32(cmbAKCT.SelectedValue)), 2, Convert.ToBoolean(cmbAK.SelectedItem != null && ((ComboBoxItem)cmbAK.SelectedItem).Text.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault"))), AppEntryPoint.Current.UserId, AppEntryPoint.Current.UserName, "MODIFY", "", _subCompanyId);
                        break;
                    case "btnRepApply":
                        if (cmbRepCT.SelectedIndex == -1)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SELECT_CASCADE"), this.ParentForm.Text);
                            return;
                        }
                        _AdminSubCompany.UpdateSubCompanyAdminDefaults(Convert.ToInt32(objAdminSubCompanyResult.SubCompanyEntity.Company_ID), false, false, false, false, true, false, Convert.ToInt64(Convert.ToString(cmbRep.SelectedValue)), Convert.ToInt32(Convert.ToString(cmbRepCT.SelectedValue)), 2, Convert.ToBoolean(cmbRep.SelectedItem != null && ((ComboBoxItem)cmbRep.SelectedItem).Text.Equals( this.GetResourceTextByKey("Key_UseCompanyDefault"))), AppEntryPoint.Current.UserId, AppEntryPoint.Current.UserName, "MODIFY", "", _subCompanyId);
                        break;
                }
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SAVE_SUCCESS"), this.ParentForm.Text);
            }
            catch (Exception Ex)
            {
                this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ERROR_UPDATING_SUBCOMPANY"), this.ParentForm.Text);
                ExceptionManager.Publish(Ex);
            }
        }

        private void SSTab1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SSTab1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                //if ((SSTab1.SelectedIndex == 1 || SSTab1.SelectedIndex == 3 ||SSTab1.SelectedIndex == 2) && _subCompanyId == 0)
                //{
                //    SSTab1.SelectedIndex = 0;
                //    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SAVE_SUBCOMPANY_FIRST"), this.ParentForm.Text);
                //    //SSTab1.SelectedIndex = 0;

                //    return;
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool ValidateEmailAndReturnOnError(Control textbox)
        {
            try
            {
                if (textbox.Text.Trim() != string.Empty && !textbox.Text.Trim().ValidateEmail())
                {
                    textbox.Focus();
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_INVALID_EMAIL_ID"), this.ParentForm.Text);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;

            }
            return true;
        }
        public OrganisationDetailsEntity Entity { get; set; }
        private void btnNewSite_Click(object sender, EventArgs e)
        {
            try
            {
                if (_subCompanyId!=0)
                 {
                     if (OpenSiteForm != null)
                     {
                         OpenSiteForm(this.Entity);

                     }
                 }
                 else
                 {
                     this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CREATE_SUBCOMPANY"), this.ParentForm.Text);
                 }
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ((Panel)this.Parent).Controls.Remove(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
