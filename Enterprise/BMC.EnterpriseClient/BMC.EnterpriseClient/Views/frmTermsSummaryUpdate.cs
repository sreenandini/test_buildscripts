using System;
using System.Windows.Forms;
using Audit.Transport;
using BMC.Common;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseClient.Helpers.ExtensionsMethods;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmTermsSummaryUpdate : Form
    {
        public Tuple<bool, float> Rent { get; set; }
        public Tuple<bool, float> Licence { get; set; }
        public Tuple<bool, float> Supplier { get; set; }
        public Tuple<bool, float> Company { get; set; }
        public Tuple<bool, float> SiteShare { get; set; }
        public string SiteName { get; set; }
        public int Id { get; set; }

        TermsSummaryBusiness objTermsSummaryBusiness = null;
        string barPosId = null;
        public frmTermsSummaryUpdate()
        {
            InitializeComponent();
            SetTagProperty();
            Rent = new Tuple<bool, float>(false, 0);
            Licence = new Tuple<bool, float>(false, 0);
            Supplier = new Tuple<bool, float>(false, 0);
            Company = new Tuple<bool, float>(false, 0);
            SiteShare = new Tuple<bool, float>(false, 0);
            objTermsSummaryBusiness = TermsSummaryBusiness.CreateInstance();
        }

        private void SetTagProperty()
        {
            this.btnUpdate.Tag = "Key_UpdateCaption";
            this.btnClose.Tag = "Key_CloseCaption";
            this.lblRent.Tag = "Key_Rent";
            this.lblLicence.Tag = "Key_Licence";
            this.lblSupplier.Tag = "Key_Supplier";
            this.lblCompany.Tag = "Key_Company";
            this.lblSite.Tag = "Key_Site";
            this.Tag = "Key_UpdateRent";

        }


        private void lblRent_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmTermsSummaryUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("frmTermsSummaryUpdate_Load(), completed", LogManager.enumLogLevel.Info);
                this.ResolveResources();
                this.txtRent.Enabled = Rent.Item1;
                this.txtRent.Text = Rent.Item2.ToString();

                this.txtLicence.Enabled = Licence.Item1;
                this.txtLicence.Text = Licence.Item2.ToString();

                this.txtSupplier.Enabled = Supplier.Item1;
                this.txtSupplier.Text = Supplier.Item2.ToString();

                this.txtCompany.Enabled = Company.Item1;
                this.txtCompany.Text = Company.Item2.ToString();

                this.txtSite.Enabled = SiteShare.Item1;
                this.txtSite.Text = SiteShare.Item2.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                LogManager.WriteLog("frmTermsSummaryUpdate_Load(), completed", LogManager.enumLogLevel.Info);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            float rent, supplier, site, licence, company = 0;

            try
            {
                LogManager.WriteLog("btnUpdate_Click(), update the shares and terms", LogManager.enumLogLevel.Info);

                //audit entity - before changes
                TermsShareRentDetailsEntity termsShareRentDetailsSavedInfo = new TermsShareRentDetailsEntity
                {
                    Bar_Position_Rent = Rent.Item2,
                    Bar_Position_Supplier_Share = Supplier.Item2,
                    Bar_Position_Site_Share = SiteShare.Item2,
                    Bar_Position_Licence_Charge = Licence.Item2,
                    Bar_Position_Owners_Share = Company.Item2
                };

                if (Company.Item1 == true &&
                    float.TryParse(this.txtCompany.Text, out company) &&
                    float.TryParse(this.txtSupplier.Text, out supplier) &&
                    float.TryParse(this.txtSite.Text, out site) &&
                    float.TryParse(this.txtLicence.Text, out licence))
                {
                    Company = new Tuple<bool, float>(true, company);
                    Supplier = new Tuple<bool, float>(true, supplier);
                    SiteShare = new Tuple<bool, float>(true, site);
                    Licence = new Tuple<bool, float>(true, licence);
                }
                else
                {
                    Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_INVALID_SHARES"));
                    return;
                }
                if (Rent.Item1 == true)
                {
                    if (float.TryParse(this.txtRent.Text, out rent))
                    {
                        Rent = new Tuple<bool, float>(true, rent);
                    }
                    else
                    {
                        Win32Extensions.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_INVALID_RENT"));
                        return;
                    }
                }

                //Todo: update the row based on bp,id
                objTermsSummaryBusiness.UpdateTermsShareAndRent(Convert.ToInt32(barPosId.Split(',')[1]), Supplier.Item2, Rent.Item2, SiteShare.Item2, Company.Item2, Licence.Item2);

                //audit changes
                new Audit_History()
                    .AddEntry()
                    .SetModule(ModuleNameEnterprise.SGVIFinancial)
                    .SetScreen("Summary|Update")
                    .SetOperationType(OperationType.MODIFY)
                    .SetDescription("Site '"+SiteName+"' modified. Modified Bar Position ..[{0}]: {2} --> {1}")
                    .InsertAuditEntries(
                        termsShareRentDetailsSavedInfo,
                        new TermsShareRentDetailsEntity
                        {
                            Bar_Position_Rent = Rent.Item2,
                            Bar_Position_Supplier_Share = Supplier.Item2,
                            Bar_Position_Site_Share = SiteShare.Item2,
                            Bar_Position_Owners_Share = Company.Item2,
                            Bar_Position_Licence_Charge = Licence.Item2
                        },
                        false);
                        
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                LogManager.WriteLog("btnUpdate_Click(), completed", LogManager.enumLogLevel.Info);
            }
        }

        private void txtSite_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class TermsShareRentDetailsEntity
    {
        public float Bar_Position_Rent { get; set; }
        public float Bar_Position_Supplier_Share { get; set; }
        public float Bar_Position_Site_Share { get; set; }
        public float Bar_Position_Owners_Share { get; set; }
        public float Bar_Position_Licence_Charge { get; set; }
    }
}
