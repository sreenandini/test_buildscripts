using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseClient.Views
{
    public class ViewFormInfo
    {
        public ViewFormInfo(string uniqueId, Func<Form> createForm)
        {
            this.UniqueId = "BMC_ENTCLIENT_" + uniqueId;
            this.CreateForm = createForm;
        }

        public Func<Form> CreateForm { get; private set; }

        public string UniqueId { get; private set; }

    }

    public class ViewInfoCollection : System.Collections.Generic.SortedDictionary<string, ViewFormInfo>
    {
        public ViewInfoCollection()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
            this.Add("stacker",
              new ViewFormInfo("B6A565EC-4387-4346-870D-B0F2A1EFA57B",
                  () => { return new frmStackerDetails(frmStackerDetails.GridFormTypes.gftSakcer); }));
            this.Add("drop_schedule",
                new ViewFormInfo("H6A565AC-4377-4346-870R-B0F2I1EFA57B",
                    () => { return new frmStackerDetails(frmStackerDetails.GridFormTypes.gftSchedule); }));
            this.Add("depot",
              new ViewFormInfo("B6H565EC-4487-4446-870A-B0F2R1EFI57B",
                  () => { return new frmDepot(); }));
  		//	this.Add("UserSiteAccess",
           //new ViewFormInfo("7654AFC-9702-1205-839F-B0F2A1EFC234",
           //    () => { return new frmUserSiteAccess(); }));

            //this.Add("UserAdminForm",
            // new ViewFormInfo("F453D35-5678-2345-87A2-A0FE3535668C",
            //     () => { return new UserAdminForm("admin"); }));
            //this.Add("UserGroup",
            //new ViewFormInfo("F453D455-5678-7857-87A2-A0FE3534568C",
            //    () => { return new frmUserGroup(); }));

            this.Add("shareholder",
             new ViewFormInfo("1D2ABFB-DDF5-45ff-A62C-23DF7674A5A6",
                 () => { return new frmShareHolderList(); }));

            this.Add("ProfitShareGroups",
             new ViewFormInfo("2D2ABFB-DDF5-45ff-A62C-23DF7674A5A6",
                 () => { return new frmCommonShareGroupList(CommonProfitShareType.ProfitShare); }));

            this.Add("ExpenseShareGroups",
             new ViewFormInfo("3D2ABFB-DDF5-45ff-A62C-23DF7674A5A6",
                 () => { return new frmCommonShareGroupList(CommonProfitShareType .ExpenseShare); }));

			this.Add("operator",
              new ViewFormInfo("B6H565RC-2487-6446-873A-B0A2R1EFI57B",
                  () => { return new frmOperator(); }));
            this.Add("Depreciation",
              new ViewFormInfo("B6G565RC-2587-6446-873A-B0A2R1EFI57B",
                  () => { return new frmDepreciation(); }));

            this.Add("site",
              new ViewFormInfo("C6H565EC-2487-6446-873A-B0A2R1EFI57B",
                  () => { return new frmAdminSite(1); }));
            this.Add("organisation",
              new ViewFormInfo("C6H5F5EC-2497-6426-873F-B0A2R1EFI57B",
                  () => { return new frmOrganisation(); }));

            this.Add("ReadLiquidation",
              new ViewFormInfo("1D2ACFB-DDA5-45ff-A62C-23DF7674A5A6",
                  () => { return new frmReadBasedLiquidation(); }));
            
            this.Add("Liquidation",
              new ViewFormInfo("1D2ACFB-DDA5-45ff-A62C-23DF7674A5B6",
                  () => { return new frmLiquidationBatch(); }));

            this.Add("EnterpriseReport",
              new ViewFormInfo("1D2ACFB-DDA5-45ff-A62C-23DF7674A57B",
                  () => { return new frmCrystalReportViewer(AppGlobals.Current.Params); }));

            this.Add("Settings",
             new ViewFormInfo("1D2ACFB-DDA5-45ff-A62C-23DF7674A57B",
                 () => { return new frmAdminSettings(); }));
            this.Add("VaultAdmin",
                new ViewFormInfo("H6A565AC-4377-4346-870R-B0F2I1EFA57B",
                    () => { return new frmVaultAdmin(); }));
        }      
    }
}
