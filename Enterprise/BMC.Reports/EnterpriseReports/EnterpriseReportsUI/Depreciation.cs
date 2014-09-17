
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using BMC.Common.ConfigurationManagement;
using BMC.DataAccess;
using System.Collections;
using BMC.EnterpriseReportsTransport;
using BMC.EnterpriseReportsDataAccess;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseReportsBusiness;
using BMC.Common;
using BMC.Reports;
using BMC.ReportViewer;

namespace BMC.EnterpriseReportsUI
{
    public partial class Depreciation : Form
    {
        public string strArg;
        public ArrayList arrDupCheck = null;

        public Depreciation()
        {
            InitializeComponent();
            setTagProperty();
        }


        public Depreciation(ArrayList arrDupCheck)
        {
            InitializeComponent();
            setTagProperty();
            this.arrDupCheck = arrDupCheck;
        }
        private void setTagProperty()
        {
            this.Tag = "key_Deprecation";
            this.lbl_caution.Tag = "Key_Caution";
            this.btn_CreateReport.Tag = "Key_CreateReport";
            this.label6.Tag = "Key_DepotColon";
            this.lbl_Title.Tag = "Key_DetailedDepreciationReport";
            this.label4.Tag = "Key_MachineNameColon";
            this.label3.Tag = "Key_MachineTypeColon";
            this.label5.Tag = "Key_PeriodFromColon";
            this.label8.Tag = "Key_PurchasedFromColon";
            this.btn_selectAll.Tag = "Key_SelectAll";
            this.btn_selectNone.Tag = "Key_SelectNone";
            this.label10.Tag = "Key_StatusColon";
            this.label1.Tag = "Key_SummaryOnlyColon";
            this.label2.Tag = "Key_SupplierColon";
            this.label9.Tag = "Key_ToColon";
            this.label7.Tag = "Key_ToColon";
            this.chkFilter.Tag = "Key_UsePurchaseDateFilter";

        }

        #region "Declarations"
        public int STOCK_IN_STOCK = 0;
        public int STOCK_IN_USE = 1;
        public int STOCK_IN_STOCK_UNUSABLE = 2;
        public int STOCK_UNDER_REPAIR = 3;
        public int STOCK_ON_ORDER = 4;
        public int STOCK_DUE_OUT = 5;
        public int STOCK_SOLD = 6;
        public int STOCK_CONVERTED = 7;
        DepreciationEnity[] TheData;
        float TheNBV = 0, TheDPW = 0, TheCost = 0;
        StringBuilder qrystatus = null, TheSQL = null;
        string qryFilter = "", TheDepots = "";
        ListItem item = null, supplier = null;
        int progress_count = 0;
        SqlDataReader dr_supplier = null;
        ListBox.SelectedObjectCollection selectedItems;
        DepreciationEnity objDetail;
        #endregion

        #region "Events"
        private void Depreciation_Form_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            lbl_Title.Text = lbl_Title.Text.ToUpper();
            CheckifTableExits();
            //Create the static depreciation Table
            SqlHelper.ExecuteNonQuery(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, "usp_createdepreciation_table");

            //load Machinetypes
            LoadComboItems("GetMachineType", cmb_machinetype, null);
            cmb_machinetype.SelectedIndex = 0;

            //load Machine Names
            LoadComboItems("GetMachineNames", cmb_machinename, null);
            cmb_machinename.SelectedIndex = 0;

            //load Depots
            LoadComboItems("GetListofDepots", cmb_depot, null);
            cmb_depot.SelectedIndex = 0;

            //load Supplier
            LoadComboItems("GetListofSuppliers", cmb_supplier, null);
            cmb_supplier.SelectedIndex = 0;

            //Load the list of machines available
            LoadMachineAvailability();

            dtp_periodfrom.Value = DateTime.Now.AddMonths(-1);
            dtp_periodto.Value = DateTime.Now;
            LoadTabIndex();
        }

        public void LoadTabIndex()
        {
            cmb_machinetype.TabIndex = 0;
            cmb_supplier.TabIndex = 1;
            cmb_machinename.TabIndex = 2;
            cmb_depot.TabIndex = 3;
            dtp_periodfrom.TabIndex = 4;
            dtp_periodto.TabIndex = 5;
            chkFilter.TabIndex = 6;
            dtp_purchasedfrom.TabIndex = 7;
            dtp_purchasedto.TabIndex = 8;
            lst_machineavailability.TabIndex = 9;
            btn_selectNone.TabIndex = 10;
            btn_selectAll.TabIndex = 11;
            chkSum.TabIndex = 12;
            btn_CreateReport.TabIndex = 13;
        }

        private void cmb_machinetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlParameter[] spparams = new SqlParameter[1];
            ListItem item = (ListItem)cmb_machinetype.SelectedItem;

            //Gets the list of machines names based on the machine type
            spparams[0] = new SqlParameter("@mtype", item.GetItemID);
            LoadComboItems("GetMachineNamesOnType", cmb_machinename, spparams);
        }

        private void cmb_supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlParameter[] spparams = new SqlParameter[1];
                ListItem item = (ListItem)cmb_supplier.SelectedItem;
                //Gets the list of depots names based on the supplier
                spparams[0] = new SqlParameter("@supplierID", item.GetItemID);
                LoadComboItems("GetDepotDetails", cmb_depot, spparams);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_selectNone_Click(object sender, EventArgs e)
        {
            lst_machineavailability.Visible = false;

            for (int i = 0; i < lst_machineavailability.Items.Count; i++)
            {
                lst_machineavailability.SetSelected(i, false);
            }
            lst_machineavailability.SelectedIndex = -1;
            lst_machineavailability.Visible = true;
            lbl_caution.Visible = false;
        }

        private void btn_selectAll_Click(object sender, EventArgs e)
        {
            lst_machineavailability.Visible = false;

            for (int i = 0; i < lst_machineavailability.Items.Count; i++)
            {
                lst_machineavailability.SetSelected(i, true);
            }

            lst_machineavailability.Visible = true;

            lbl_caution.Visible = true;
        }

        void Depreciation_Form_Disposed(object sender, System.EventArgs e)
        {
            //  CheckifTableExits();
        }



        private void btn_CreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                TheData = null;
                progress_count = 0;
                TheSQL = null;
                //Create a new table everytime a new report is generated.
                CheckifTableExits();
                //Create the static depreciation Table
                SqlHelper.ExecuteNonQuery(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, "usp_createdepreciation_table");

                int result = LoadInfo();
                //if number of records is greater than zero, view the report.
                if (result > 0)
                {
                    ReportsBusiness oReportBusiness = new ReportsBusiness();
                    item = (ListItem)cmb_machinetype.SelectedItem;
                    clsSPParams lstParams = new clsSPParams();
                    lstParams.MTypeID = item.GetItemID.ToString();
                    lstParams.Status = (chkSum.Checked) ? "1" : "0";
                    lstParams.CurrentDate = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                    lstParams.ProductVersion = oReportBusiness.GetBMCVersion();
                    lstParams.StartDateString = dtp_periodfrom.Value.ToString("dd-MMM-yyyy");
                    lstParams.EndDateString = dtp_periodto.Value.ToString("dd-MMM-yyyy");                                       
                    lstParams.ReportFilterDateFormat = oReportBusiness.GetSetting("ReportDateFormat", "dd-MMM-yyyy"); 
                    lstParams.ReportPrintDateTimeFormat = oReportBusiness.GetSetting("ReportPrintDateTimeFormat", "dd-MMM-yyyy HH:mm:ss");
                    lstParams.ReportDataDateAloneFormat = oReportBusiness.GetSetting("ReportDataDateAloneFormat", "dd-MMM-yyyy");
                    lstParams.ReportDataDateNTimeFormat = oReportBusiness.GetSetting("ReportDataDateNTimeFormat", "dd-MMM-yyyy HH:mm:ss");
                    selectedItems = lst_machineavailability.SelectedItems;
                    StringBuilder strListStatus = new StringBuilder();
                    for (int index = 0; index < selectedItems.Count; index++)
                    {
                        item = (ListItem)selectedItems[index];
                        if (index < (selectedItems.Count - 1))
                            strListStatus.AppendFormat("{0},", item.ToString());
                        else
                            strListStatus.AppendFormat("{0}", item.ToString());
                    }

                    lstParams.ListStatus = strListStatus.ToString();
                    strArg = "DEPRECIATIONREPORT";
                    RDLReportViewer.Instance.LoadReport("usp_createdepreciation_table", "Depreciation Report", strArg, lstParams, false);

                    //ReportViewer viewer = new ReportViewer(strArg, lstParams);
                    //viewer.Text = "Depreciation Report";
                    //viewer.Show();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFilter.Checked == true)
            {
                dtp_purchasedfrom.Enabled = true;
                dtp_purchasedfrom.Value = DateTime.Now.AddMonths(-12);
                dtp_purchasedto.Enabled = true;
                dtp_purchasedto.Value = DateTime.Now;
            }
            else
            {
                dtp_purchasedfrom.Enabled = false;
                dtp_purchasedto.Enabled = false;
            }
        }

        private void chkSum_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSum.Checked == true)
            {
                lbl_Title.Text = "Depreciation Summary Report";
            }
            else
            {
                lbl_Title.Text = "Depreciation Detail Report";
            }
            lbl_Title.Text = lbl_Title.Text.ToUpper();
        }
        #endregion

        #region "Functions"
        private void LoadComboItems(string spname, ComboBox comboname, SqlParameter[] parameters)
        {
            //Fill the combo with Data
            SqlDataReader reader = null;
            try
            {
                comboname.Items.Clear();
                comboname.Items.Add(new ListItem(0, "{ALL}"));
                if (parameters == null)
                {
                    reader = SqlHelper.ExecuteReader(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, spname);
                }
                else
                {
                    reader = SqlHelper.ExecuteReader(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, spname, parameters);
                }
                while (reader.Read())
                {
                    comboname.Items.Add(new ListItem(Convert.ToInt32(reader["ItemID"]), reader["Item"].ToString()));
                }

                reader.Dispose();

                comboname.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void LoadMachineAvailability()
        {
            //Fill the list with machines status
            lst_machineavailability.Items.Clear();

            lst_machineavailability.Items.Add(new ListItem(0, this.GetResourceTextByKey("Key_RX_Available"))); //lst_machineavailability.Items.Add(new ListItem(0, "Available"));
            lst_machineavailability.SetSelected(0, true);
            lst_machineavailability.Items.Add(new ListItem(1, this.GetResourceTextByKey("Key_RX_InUse"))); //lst_machineavailability.Items.Add(new ListItem(1, "In Use"));
            lst_machineavailability.SetSelected(1, true);
            lst_machineavailability.Items.Add(new ListItem(2, this.GetResourceTextByKey("Key_RX_UnderRepair"))); //lst_machineavailability.Items.Add(new ListItem(2, "Under Repair"));
            lst_machineavailability.SetSelected(2, true);
            lst_machineavailability.Items.Add(new ListItem(3, this.GetResourceTextByKey("Key_RX_Unusable"))); //lst_machineavailability.Items.Add(new ListItem(3, "Unusable"));
            lst_machineavailability.SetSelected(3, true);
            lst_machineavailability.Items.Add(new ListItem(4, this.GetResourceTextByKey("Key_RX_OnOrder"))); //lst_machineavailability.Items.Add(new ListItem(4, "On Order"));
            lst_machineavailability.SetSelected(4, true);
            lst_machineavailability.Items.Add(new ListItem(5, this.GetResourceTextByKey("Key_RX_Reserved"))); //lst_machineavailability.Items.Add(new ListItem(5, "Reserved"));
            lst_machineavailability.SetSelected(5, true);
            lst_machineavailability.Items.Add(new ListItem(6, this.GetResourceTextByKey("Key_RX_Sold"))); //lst_machineavailability.Items.Add(new ListItem(6, "Sold"));
            lst_machineavailability.SetSelected(6, false);
            lst_machineavailability.Items.Add(new ListItem(7, this.GetResourceTextByKey("Key_RX_Converted"))); //lst_machineavailability.Items.Add(new ListItem(7, "Converted"));
            lst_machineavailability.SetSelected(7, false);
        }

        private void CheckifTableExits()
        {
            //if table exists drop it
            string str_temp = "";
            str_temp = "if exists (select * from dbo.sysobjects where id = object_id(N'ddp_ttk') " +
                "and OBJECTPROPERTY(id, N'IsTable') = 1) drop table ddp_ttk";
            SqlHelper.ExecuteNonQuery(DbConnection.EnterpriseConnectionString, CommandType.Text, str_temp);
        }
        private int LoadInfo()
        {
            try
            {

                if (dtp_periodfrom.Value > dtp_periodto.Value)
                {
                    MessageBox.Show(this.GetResourceTextByKey(1, "MSG_REP_DATE_FORMAT"), this.GetResourceTextByKey(1, "MSG_DEP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }


                if (chkFilter.Checked == true)
                {
                    if (dtp_purchasedfrom.Value > dtp_purchasedto.Value)
                    {
                        MessageBox.Show(this.GetResourceTextByKey(1, "MSG_REP_DATE_FORMAT1"), this.GetResourceTextByKey(1, "MSG_DEP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;
                    }
                }
                //Get the records based on the parameters.
                qrystatus = new StringBuilder();
                qrystatus.Remove(0, qrystatus.Length);
                qrystatus.Append(" AND (1=2 ");

                selectedItems = lst_machineavailability.SelectedItems;
                for (int i = 0; i < selectedItems.Count; i++)
                {
                    item = (ListItem)selectedItems[i];
                    qrystatus.Append(" OR Machine.Machine_Status_Flag = " + item.GetItemID);
                }

                qrystatus.Append(" OR Machine.Machine_Status_Flag IS NULL)");
                //-->Added by Vineetha
                //string dteFrom = dtp_purchasedfrom.Value.ToShortDateString();
                //string dteTo = dtp_purchasedto.Value.ToShortDateString();
                string dteFrom = dtp_purchasedfrom.Value.ToString("dd MMM yyyy");
                string dteTo = dtp_purchasedto.Value.ToString("dd MMM yyyy");
                //<--Added by Vineetha

                //If type of filter is purchased date then
                if (chkFilter.Checked == true)
                {
                    //-->Querry changed by Vineetha
                    // qryFilter = "AND DateDiff('d',cdate(Machine.Machine_Start_Date),'" + dteFrom + "')<=0" + " AND DateDiff('d',cdate(Machine.Machine_Start_Date), '" + dteTo + "')>=0 ";
                    qryFilter = "AND DateDiff(d,CONVERT(DATETIME,Machine.Machine_Start_Date,103),CONVERT(datetime,'" + dteFrom + "',103))<=0" + " AND DateDiff(d,CONVERT(datetime,Machine.Machine_Start_Date,103), CONVERT(datetime,'" + dteTo + "',103))>=0 ";
                    //<--Querry changed by Vineetha
                }
                else
                {
                    qryFilter = "";
                }
                item = null;
                TheDepots = "";
                item = (ListItem)cmb_depot.SelectedItem;

                if (item.GetItemValue == "{ALL}")
                {

                    supplier = (ListItem)cmb_supplier.SelectedItem;
                    SqlParameter[] parames = new SqlParameter[1];

                    if (supplier.GetItemValue == "{ALL}")
                    {
                        parames[0] = new SqlParameter("@supplierID", "0");
                    }
                    else
                    {
                        parames[0] = new SqlParameter("@supplierID", supplier.GetItemID);
                    }
                    dr_supplier = SqlHelper.ExecuteReader(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, "GetDepotId", parames);

                    parames = null;

                    while (dr_supplier.Read())
                    {
                        TheDepots += dr_supplier["Depot_ID"] + ", ";
                    }
                    //Get the number of depots.
                    TheDepots = TheDepots.Trim();
                    if (TheDepots.Length > 0)
                    {
                        TheDepots = "(" + TheDepots.Trim().Substring(0, TheDepots.Trim().Length - 1) + ")";
                    }

                    dr_supplier.Dispose();
                }
                else
                {
                    //changed newly by anu
                    TheDepots = "(" + item.GetItemID + ")";
                }

                TheSQL = new StringBuilder();
                TheSQL.Append("SELECT DISTINCT Machine_Type_Code, Machine_Name, Machine_Stock_No, Machine.Machine_ID, Machine_End_date, Machine_Start_date ");
                TheSQL.Append(" FROM ((Machine ");
                TheSQL.Append(" INNER JOIN Machine_Class ON Machine.Machine_class_ID = Machine_class.Machine_class_ID)");
                TheSQL.Append(" INNER JOIN Machine_Type ON Machine_Class.Machine_Type_ID = Machine_Type.Machine_Type_ID)");
                TheSQL.Append(" WHERE Machine_Type.IsNonGamingAssetType = 0 AND Machine.Depot_ID IN " + TheDepots);


                qrystatus.Append(WhereClause());
                qrystatus.Append(qryFilter);
                TheSQL.Append(qrystatus);

                TheSQL.Append(" ORDER BY Machine_Type_Code, Machine_Name, Machine_Stock_No");
                string temp_str = TheSQL.ToString();
                TheSQL.Remove(0, TheSQL.Length);
                TheSQL.Append(DepDetModule.VerifySQL(temp_str));

                DataTable dt_machine = SqlHelper.ExecuteDataset(DbConnection.EnterpriseConnectionString, CommandType.Text, TheSQL.ToString()).Tables[0];
                if (dt_machine.Rows.Count > 0)
                {
                    //set the progressbar maximum to number of records
                    depreciation_progress.Maximum = dt_machine.Rows.Count;
                }
                else
                {
                    ReportsBusiness oReportBusiness = new ReportsBusiness();
                    string strMessage = oReportBusiness.GetReportMessageException();
                    MessageBox.Show(strMessage, Constants.MessageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }


                SqlParameter[] prms = new SqlParameter[1];
                TheData = new DepreciationEnity[dt_machine.Rows.Count];
                float TheReturn = 0;
                foreach (DataRow row in dt_machine.Rows)
                {
                    //  progress_count += 1;
                    TheData[progress_count] = new DepreciationEnity();
                    depreciation_progress.Value = progress_count;

                    Application.DoEvents();
                    TheSQL.Remove(0, TheSQL.Length);

                    TheSQL.Append("Select * From Planned_Movement ");
                    TheSQL.Append(" WHERE Planned_Movement_From_Machine_ID =" + row["Machine_ID"].ToString());
                    TheSQL.Append(" AND Planned_Movement_From_Type = 2");
                    TheSQL.Append(" AND Planned_Movement_to_Type = 2");
                    TheSQL.Append(" AND (Planned_Movement.Planned_Movement_To_Depot_ID IN " + TheDepots);
                    TheSQL.Append(" OR Planned_Movement.Planned_Movement_From_Depot_ID IN " + TheDepots + ")");

                    string str_temp = DepDetModule.VerifySQL(TheSQL.ToString());

                    DataTable dt_movement = SqlHelper.ExecuteDataset(DbConnection.EnterpriseConnectionString, CommandType.Text, str_temp).Tables[0];

                    //If there are any records in the planned_movement table
                    if (dt_movement.Rows.Count > 0)
                    {

                        string MachineArrivedAtDepot = row["Machine_Start_Date"].ToString();
                        foreach (DataRow move_row in dt_movement.Rows)
                        {
                            // For each planned movement
                            int pos = TheDepots.IndexOf(" " + move_row["Planned_Movement_From_Depot_ID"].ToString() + ",");
                            if (pos > 0)
                            {
                                //If Planned_Movement_From_Depot_ID = SELECTED DEPOT then Machine was at selected depot between MachineArrivedAtDepot and Planned_Movement_Date_Scheduled
                                //Machine was at selected depot between MachineArrivedAtDepot and Planned_Movement_Date_Scheduled
                                if (chkFilter.Checked == false)
                                {
                                    TheReturn = DepDetModule.ReturnMachineDepreciation(Convert.ToInt32(row["Machine_ID"]),
                                       MachineArrivedAtDepot, move_row["Planned_Movement_Date_Scheduled"].ToString(),
                                       dtp_periodfrom.Value.ToString(),
                                       dtp_periodto.Value.ToString());
                                    TheData[progress_count].GetNBV = TheData[progress_count].GetNBV + TheReturn;
                                }
                                else if (chkFilter.Checked == true)
                                {
                                    TheReturn = DepDetModule.ReturnMachineDepreciation(Convert.ToInt32(row["Machine_ID"]),
                                       MachineArrivedAtDepot, move_row["Planned_Movement_Date_Scheduled"].ToString(),
                                       dtp_purchasedfrom.Value.ToString(),
                                       dtp_purchasedto.Value.ToString());
                                }


                                MachineArrivedAtDepot = "";
                            }
                            int position = TheDepots.IndexOf(" " + move_row["Planned_Movement_To_Depot_ID"].ToString() + ",");
                            if (position > 0)
                            {
                                // If Planned_Movement_From_Depot_ID = SELECTED DEPOT then Machine was at selected depot between MachineArrivedAtDepot and Planned_Movement_Date_Scheduled 'If Planned_Movement_To_Depot_ID = SELECTED DEPOT then
                                MachineArrivedAtDepot = move_row["Planned_Movement_Date_Scheduled"].ToString();
                            }
                        }
                        if (MachineArrivedAtDepot != "") //if the MachineArrivedAtDepot variable <> ""
                        //    'The last movement of the machine was to the selected depot, therefore
                        //    'Machine has been at selected depot between MachineArrivedAtDepot and Now
                        {
                            if (chkFilter.Checked == false)
                            {
                                TheReturn = DepDetModule.ReturnMachineDepreciation(Convert.ToInt32(row["Machine_ID"]),
                                    MachineArrivedAtDepot, DateTime.Now.ToString(), dtp_periodfrom.Value.ToString(),
                                    dtp_periodto.Value.ToString());
                            }
                            else if (chkFilter.Checked == true)
                            {
                                TheReturn = DepDetModule.ReturnMachineDepreciation(Convert.ToInt32(row["Machine_ID"]),
                                    MachineArrivedAtDepot, DateTime.Now.ToString(), dtp_purchasedfrom.Value.ToString(),
                                    dtp_purchasedto.Value.ToString());
                            }
                            TheData[progress_count].GetDCP = TheData[progress_count].GetDCP + TheReturn;
                            MachineArrivedAtDepot = "";
                        }
                    }
                    else
                    {
                        string TheDate = "";
                        //The machine has always been in the same depot, 
                        //depreciation is worked out from Machine_Start_Date to Machine_End_Date (Or now if null)
                        if (row["Machine_End_Date"] == DBNull.Value)
                        {
                            TheDate = DateTime.Now.ToShortDateString();
                        }
                        else
                        {
                            TheDate = row["Machine_End_Date"].ToString();
                        }
                        if (row["Machine_Start_Date"] == DBNull.Value)
                        {

                            prms[0] = new SqlParameter("@machineID", row["Machine_ID"].ToString());
                            SqlDataReader dr_Details = SqlHelper.ExecuteReader(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, "GetInstallationDet", prms);

                            //TheDetailRS.Open VerifySQL("Select Top 1 * From Installation WHERE Machine_ID=" & TheRS!Machine_ID & " ORDER BY Installation_Start_Date ASC")
                            while (dr_Details.Read())
                            {
                                if (chkFilter.Checked == false)
                                {
                                    TheReturn = DepDetModule.ReturnMachineDepreciation(Convert.ToInt32(dr_Details["Machine_ID"]),
                                        dr_Details["Installation_Start_date"].ToString(), TheDate, dtp_periodfrom.Value.ToString(),
                                       dtp_periodto.Value.ToString());
                                }
                                else if (chkFilter.Checked == true)
                                {
                                    TheReturn = DepDetModule.ReturnMachineDepreciation(Convert.ToInt32(dr_Details["Machine_ID"]),
                                       dr_Details["Installation_Start_date"].ToString(), TheDate, dtp_purchasedfrom.Value.ToString(),
                                      dtp_purchasedto.Value.ToString());
                                }
                            }
                            dr_Details.Dispose();
                            prms = null;
                        }
                        else
                        {
                            if (chkFilter.Checked == false)
                            {
                                TheReturn = DepDetModule.ReturnMachineDepreciation(Convert.ToInt32(row["Machine_ID"]),
                                        row["Machine_Start_Date"].ToString(), TheDate, dtp_periodfrom.Value.ToString(),
                                       dtp_periodto.Value.ToString());
                            }
                            else if (chkFilter.Checked == true)
                            {
                                TheReturn = DepDetModule.ReturnMachineDepreciation(Convert.ToInt32(row["Machine_ID"]),
                                        row["Machine_Start_Date"].ToString(), TheDate, dtp_periodfrom.Value.ToString(),
                                       dtp_periodto.Value.ToString());
                            }

                        }
                        TheData[progress_count].GetDCP = TheData[progress_count].GetDCP + TheReturn;
                        // MachineArrivedAtDepot = "";
                    }

                    SqlParameter[] parmets = new SqlParameter[1];
                    parmets[0] = new SqlParameter("@machineID", row["Machine_ID"].ToString());
                    DataTable dt_details = SqlHelper.ExecuteDataset(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, "GetStatusDet", parmets).Tables[0];
                    parmets = null;

                    //Fill the array with values obtained 
                    foreach (DataRow drRow in dt_details.Rows)
                    {
                        bool TheCheck = DepDetModule.GetDepreciationDetailsFromMachineID(Convert.ToInt32(row["Machine_ID"]),
                               ref TheNBV, ref TheDPW, ref TheCost, DateTime.Now.ToString());
                        TheData[progress_count].GetAlt_serial_No = drRow["Machine_Alternative_Serial_Numbers"].ToString() + "";
                        TheData[progress_count].GetAssetNo = drRow["Machine_Stock_No"] + "";
                        TheData[progress_count].GetCost = TheCost;
                        TheData[progress_count].GetCurrent_Depot_ID = Convert.ToInt32(drRow["Depot_ID"]);
                        TheData[progress_count].GetCurrent_Depot_Name = drRow["Depot_Name"] + "";
                        TheData[progress_count].GetNBV = TheNBV;
                        TheData[progress_count].GetDPW = TheDPW;
                        TheData[progress_count].GetMachine_Class_ID = Convert.ToInt32(drRow["Machine_Class_ID"]);
                        TheData[progress_count].GetMachineClassName = drRow["Machine_Name"].ToString().Trim() + "";
                        TheData[progress_count].GetMachine_Type_ID = Convert.ToInt32(drRow["Machine_Type_ID"]);
                        TheData[progress_count].GetMachineTypeName = drRow["Machine_Type_Code"].ToString().Trim() + "";
                        //-->if condition added by Vineetha
                        if (chkFilter.Checked == true)
                        {

                            TheData[progress_count].GetPassPurchaseDateFrom = dtp_purchasedfrom.Value.ToString("MM/dd/yyyy") +"";
                            TheData[progress_count].GetPassPurchaseDateTo = dtp_purchasedto.Value.ToString("MM/dd/yyyy") +"";
                        }
                        else
                        {
                            TheData[progress_count].GetPassPurchaseDateFrom = "";
                            TheData[progress_count].GetPassPurchaseDateTo = "";
                        }
                        TheData[progress_count].GetPassDateFrom = dtp_periodfrom.Value.ToString("MM/dd/yyyy") +"";
                        TheData[progress_count].GetPassDateToo = dtp_periodto.Value.ToString("MM/dd/yyyy") +"";

                        //<--if condition added by Vineetha
                        //TheData[progress_count].GetPassDateFrom = dtp_periodfrom.Value.ToShortDateString() + "";
                        //TheData[progress_count].GetPassDateToo = dtp_periodto.Value.ToShortDateString() + "";
                        item = (ListItem)cmb_depot.SelectedItem;
                        TheData[progress_count].GetPassDepot = item.GetItemValue + "";
                        item = (ListItem)cmb_machinename.SelectedItem;
                        TheData[progress_count].GetPassMachineName = item.GetItemValue + "";
                        item = (ListItem)cmb_machinetype.SelectedItem;
                        TheData[progress_count].GetPassMachineType = item.GetItemValue + "";
                        item = (ListItem)cmb_supplier.SelectedItem;
                        TheData[progress_count].GetPassSupplier = item.GetItemValue + "";
                        TheData[progress_count].GetPurchase_Date = Convert.ToDateTime(drRow["Machine_Start_Date"]).ToString("MM/dd/yyyy");
                        TheData[progress_count].GetSerialNo = drRow["Machine_Manufacturers_Serial_No"].ToString() + "";
                        TheData[progress_count].GetSpare1 = "1";
                        TheData[progress_count].GetSpare2 = "2";
                        TheData[progress_count].GetSpare3 = "3";
                        TheData[progress_count].GetSpare4 = "4";
                        TheData[progress_count].GetSpare5 = "5";
                        TheData[progress_count].GetSpare6 = "6";
                        TheData[progress_count].GetSpare7 = "7";
                        TheData[progress_count].GetSpare8 = "8";
                        TheData[progress_count].GetSpare9 = "9";
                        TheData[progress_count].GetSpare10 = "10";
                        TheData[progress_count].GetSpare11 = "11";
                        TheData[progress_count].GetSpare12 = "12";
                        TheData[progress_count].GetSpare13 = "13";
                        TheData[progress_count].GetSpare14 = "14";
                        TheData[progress_count].GetSpare15 = "15";
                        TheData[progress_count].GetSpare16 = "16";
                        TheData[progress_count].GetSpare17 = "17";
                        TheData[progress_count].GetSpare18 = "18";
                        TheData[progress_count].GetSpare19 = "19";
                        TheData[progress_count].GetSpare20 = "20";
                        if (drRow["Machine_Status_Flag"] != DBNull.Value)
                        {
                            switch (Convert.ToInt32(drRow["Machine_Status_Flag"]))
                            {
                                case 0:
                                    {
                                        TheData[progress_count].GetStock_Status = this.GetResourceTextByKey("Key_RX_InStock");  //TheData[progress_count].GetStock_Status = "In Stock";
                                        break;
                                    }
                                case 1:
                                    {
                                        TheData[progress_count].GetStock_Status = this.GetResourceTextByKey("Key_RX_InUse");  //TheData[progress_count].GetStock_Status = "In Use";
                                        break;
                                    }
                                case 2:
                                    {
                                        TheData[progress_count].GetStock_Status = this.GetResourceTextByKey("Key_RX_InStockUnusable"); //TheData[progress_count].GetStock_Status = "In Stock Unusable";
                                        break;
                                    }
                                case 3:
                                    {
                                        TheData[progress_count].GetStock_Status = this.GetResourceTextByKey("Key_RX_UnderRepair"); //TheData[progress_count].GetStock_Status = "Under Repair";
                                        break;
                                    }
                                case 4:
                                    {
                                        TheData[progress_count].GetStock_Status = this.GetResourceTextByKey("Key_RX_OnOrder"); //TheData[progress_count].GetStock_Status = "On Order";
                                        break;
                                    }
                                case 5:
                                    {
                                        TheData[progress_count].GetStock_Status = this.GetResourceTextByKey("Key_RX_DueOut"); //TheData[progress_count].GetStock_Status = "Due Out";
                                        break;
                                    }
                                case 6:
                                    {
                                        TheData[progress_count].GetStock_Status = this.GetResourceTextByKey("Key_RX_Sold"); ; //TheData[progress_count].GetStock_Status = "Sold";
                                        break;
                                    }
                                case 7:
                                    {
                                        TheData[progress_count].GetStock_Status = this.GetResourceTextByKey("Key_RX_Converted"); ; //TheData[progress_count].GetStock_Status = "Converted";
                                        break;
                                    }
                                default:
                                    {
                                        TheData[progress_count].GetStock_Status = drRow["Machine_Status"].ToString() + "";
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            // TheData[progress_count].GetStock_Status = string.Empty;
                        }


                        TheData[progress_count].GetSupp_ID = 0;
                        TheData[progress_count].GetTD = TheCost - TheNBV;
                        TheDPW = 0.0F;

                        dt_details.Dispose();
                        dt_movement.Dispose();

                        if (chkSum.Checked == true)
                        {
                            TheData[progress_count].GetSpare1 = "1";
                        }
                        else
                        {
                            TheData[progress_count].GetSpare1 = "0";
                        }
                        progress_count++;
                    }
                }

                dt_machine.Dispose();


                objDetail = null; ;
                SqlParameter[] repparameters = new SqlParameter[46];
                repparameters[0] = new SqlParameter("@MTID", SqlDbType.Int);
                repparameters[1] = new SqlParameter("@MCID", SqlDbType.Int);
                repparameters[2] = new SqlParameter("@depotID", SqlDbType.Int);
                repparameters[3] = new SqlParameter("@suppID", SqlDbType.Int);
                repparameters[4] = new SqlParameter("@mtypename", SqlDbType.VarChar, 100);
                repparameters[5] = new SqlParameter("@mclassname", SqlDbType.VarChar, 100);
                repparameters[6] = new SqlParameter("@mAsset_No", SqlDbType.VarChar, 100);
                repparameters[7] = new SqlParameter("@Serial_No", SqlDbType.VarChar, 100);
                repparameters[8] = new SqlParameter("@Alt_serial_No", SqlDbType.VarChar, 100);
                repparameters[9] = new SqlParameter("@purchase_date", SqlDbType.VarChar, 100);
                repparameters[10] = new SqlParameter("@cost", SqlDbType.Money);
                repparameters[11] = new SqlParameter("@NBV", SqlDbType.Money);
                repparameters[12] = new SqlParameter("@DCP", SqlDbType.Money);
                repparameters[13] = new SqlParameter("@TD", SqlDbType.Money);
                repparameters[14] = new SqlParameter("@DPW", SqlDbType.Money);
                repparameters[15] = new SqlParameter("@Current_Depot_ID", SqlDbType.Int);
                repparameters[16] = new SqlParameter("@Current_Depot_Name", SqlDbType.VarChar, 100);
                repparameters[17] = new SqlParameter("@Stock_Status", SqlDbType.VarChar, 20);
                repparameters[18] = new SqlParameter("@PassDateFrom", SqlDbType.VarChar, 50);
                repparameters[19] = new SqlParameter("@PassDateToo", SqlDbType.VarChar, 50);
                repparameters[20] = new SqlParameter("@PassMachineType", SqlDbType.VarChar, 50);
                repparameters[21] = new SqlParameter("@PassMachineName", SqlDbType.VarChar, 50);
                repparameters[22] = new SqlParameter("@PassSupplier", SqlDbType.VarChar, 100);
                repparameters[23] = new SqlParameter("@PassDepot", SqlDbType.VarChar, 50);
                repparameters[24] = new SqlParameter("@Spare1", SqlDbType.VarChar, 10);
                repparameters[25] = new SqlParameter("@Spare2", SqlDbType.VarChar, 10);
                repparameters[26] = new SqlParameter("@Spare3", SqlDbType.VarChar, 10);
                repparameters[27] = new SqlParameter("@Spare4", SqlDbType.VarChar, 10);
                repparameters[28] = new SqlParameter("@Spare5", SqlDbType.VarChar, 10);
                repparameters[29] = new SqlParameter("@Spare6", SqlDbType.VarChar, 10);
                repparameters[30] = new SqlParameter("@Spare7", SqlDbType.VarChar, 10);
                repparameters[31] = new SqlParameter("@Spare8", SqlDbType.VarChar, 10);
                repparameters[32] = new SqlParameter("@Spare9", SqlDbType.VarChar, 10);
                repparameters[33] = new SqlParameter("@Spare10", SqlDbType.VarChar, 10);
                repparameters[34] = new SqlParameter("@Spare11", SqlDbType.VarChar, 10);
                repparameters[35] = new SqlParameter("@Spare12", SqlDbType.VarChar, 10);
                repparameters[36] = new SqlParameter("@Spare13", SqlDbType.VarChar, 10);
                repparameters[37] = new SqlParameter("@Spare14", SqlDbType.VarChar, 10);
                repparameters[38] = new SqlParameter("@Spare15", SqlDbType.VarChar, 10);
                repparameters[39] = new SqlParameter("@Spare16", SqlDbType.VarChar, 10);
                repparameters[40] = new SqlParameter("@Spare17", SqlDbType.VarChar, 10);
                repparameters[41] = new SqlParameter("@Spare18", SqlDbType.VarChar, 10);
                repparameters[42] = new SqlParameter("@Spare19", SqlDbType.VarChar, 10);
                repparameters[43] = new SqlParameter("@Spare20", SqlDbType.VarChar, 10);
                repparameters[44] = new SqlParameter("@PassPurchaseDateFrom", SqlDbType.VarChar, 50);
                repparameters[45] = new SqlParameter("@PassPurchaseDateTo", SqlDbType.VarChar, 50);

                int j;
                //Create an array of SQL parameters to insert into the table 
                for (j = 0; j < TheData.Length; j++)
                {
                    objDetail = (DepreciationEnity)TheData[j];
                    repparameters[0].Value = objDetail.GetMachine_Type_ID;
                    repparameters[1].Value = objDetail.GetMachine_Class_ID;
                    repparameters[2].Value = objDetail.GetDepot_ID;
                    repparameters[3].Value = objDetail.GetSupp_ID;
                    repparameters[4].Value = objDetail.GetMachineTypeName + "";
                    repparameters[5].Value = objDetail.GetMachineClassName + "";
                    repparameters[6].Value = objDetail.GetAssetNo + "";
                    repparameters[7].Value = objDetail.GetSerialNo + "";
                    repparameters[8].Value = objDetail.GetAlt_serial_No + "";
                    repparameters[9].Value = objDetail.GetPurchase_Date + "";
                    repparameters[10].Value = objDetail.GetCost;
                    repparameters[11].Value = objDetail.GetNBV;
                    repparameters[12].Value = objDetail.GetDCP;
                    repparameters[13].Value = objDetail.GetTD;
                    repparameters[14].Value = objDetail.GetDPW;
                    repparameters[15].Value = objDetail.GetCurrent_Depot_ID;
                    repparameters[16].Value = objDetail.GetCurrent_Depot_Name + "";
                    repparameters[17].Value = objDetail.GetStock_Status + "";
                    repparameters[18].Value = objDetail.GetPassDateFrom + "";
                    repparameters[19].Value = objDetail.GetPassDateToo + "";
                    repparameters[20].Value = objDetail.GetPassMachineName + "";
                    repparameters[21].Value = objDetail.GetPassMachineType + "";
                    repparameters[22].Value = objDetail.GetPassSupplier + "";
                    repparameters[23].Value = objDetail.GetPassDepot + "";
                    repparameters[24].Value = objDetail.GetSpare1;
                    repparameters[25].Value = objDetail.GetSpare2;
                    repparameters[26].Value = objDetail.GetSpare3;
                    repparameters[27].Value = objDetail.GetSpare4;
                    repparameters[28].Value = objDetail.GetSpare5;
                    repparameters[29].Value = objDetail.GetSpare6;
                    repparameters[30].Value = objDetail.GetSpare7;
                    repparameters[31].Value = objDetail.GetSpare8;
                    repparameters[32].Value = objDetail.GetSpare9;
                    repparameters[33].Value = objDetail.GetSpare10;
                    repparameters[34].Value = objDetail.GetSpare11;
                    repparameters[35].Value = objDetail.GetSpare12;
                    repparameters[36].Value = objDetail.GetSpare13;
                    repparameters[37].Value = objDetail.GetSpare14;
                    repparameters[38].Value = objDetail.GetSpare15;
                    repparameters[39].Value = objDetail.GetSpare16;
                    repparameters[40].Value = objDetail.GetSpare17;
                    repparameters[41].Value = objDetail.GetSpare18;
                    repparameters[42].Value = objDetail.GetSpare19;
                    repparameters[43].Value = objDetail.GetSpare20;
                    repparameters[44].Value = objDetail.GetPassPurchaseDateFrom + "";
                    repparameters[45].Value = objDetail.GetPassPurchaseDateTo + "";


                    //Execute the sp to insert the values in the table DDP_ttk
                    SqlHelper.ExecuteNonQuery(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, "esp_InsertDepreciationDetails", repparameters);
                }
                repparameters = null;
                return j;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }

        }
        private string WhereClause()
        {
            string WhereClause = "";
            item = (ListItem)cmb_machinename.SelectedItem;
            if (item.GetItemID > 0)
            {
                WhereClause += " And Machine_Class.Machine_Class_ID = " + item.GetItemID;
            }
            item = (ListItem)cmb_machinetype.SelectedItem;
            if (item.GetItemID > 0)
            {
                WhereClause += " AND Machine_Type.Machine_Type_ID= " + item.GetItemID;
            }
            return WhereClause;
        }

        #endregion

        private void Depreciation_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.arrDupCheck != null)
            {
                if (this.arrDupCheck.Count > 0)
                {
                    this.arrDupCheck.Remove(strArg);
                }
            }

        }





    }
}