using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.EnterpriseBusiness;
using BMC.EnterpriseBusiness.Business.CashierTransations;
using BMC.EnterpriseClient.Views;
using BMC.EnterpriseDataAccess.CashierTransations;
using BMC.Common;
using BMC.CoreLib.Win32;
using BMC.ReportViewer;
using BMC.Reports;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseClient.CashierTransactions
{
    public partial class frmDetailsScreen : Form
    {
        DataSet _DtDetails = null;
        DateTime _FromDate;
        DateTime _ToDate;
        string _SiteName;
        int _SiteID;
        string _CODE;
        int _UserID = 0;
        string _FooterText;
        BMCCashierTreasuryTransactions oBusiness = null;
       
        public frmDetailsScreen(DataSet DtDetails, DateTime FromDate, DateTime ToDate, String SiteName, string sFooterText, int UserID)
        {
            InitializeComponent();
            SetTagProperty();
            _UserID = UserID;
            _DtDetails = DtDetails;
            _FromDate = FromDate;
            _ToDate = ToDate;
            _SiteName = SiteName;
            _CODE = "DETAILS";
           
            _FooterText = sFooterText;
            LogManager.WriteLog("DetailsScreen: FromDate-" + _FromDate.ToString() + " ToDate-" + _ToDate.ToString() +
                                    " SiteName-" + _SiteName, LogManager.enumLogLevel.Info);
            btnExport.Visible = false;
            oBusiness = new BMCCashierTreasuryTransactions();
            InitializeListView();
        }

        //public DetailsScreen(string sCODE, DateTime FromDate, DateTime ToDate, String SiteName, int Site)
        //{

        //    InitializeComponent();
        //    SetTagProperty();
        //    oBusiness = new BMCCashierTreasuryTransactions();

        //    _FromDate = FromDate;
        //    _ToDate = ToDate;
        //    _SiteName = SiteName;
        //    _CODE = sCODE;
        //    _SiteID = Site;

        //    LogManager.WriteLog("DetailsScreen: FromDate-" + _FromDate.ToString() + " ToDate-" + _ToDate.ToString() +
        //                           " SiteName-" + _SiteName + " FooterText" + _FooterText, LogManager.enumLogLevel.Info);
        //    btnExport.Visible = true;
        //    InitializeListView();
        //}


        private void DetailsScreen_Load(object sender, EventArgs e)
        {
            try
            {
                this.ResolveResources();
                LoadList();
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
            this.btnExport.Tag = "Key_ExportCaption";
            this.btnPrint.Tag = "Key_ReportCaption";
            this.Tag = "Key_CashierHistory_Details";
        }

        // Initialize ListView
        private void InitializeListView()
        {
            try
            {
                // Set the view to show details.
                listView1.View = View.Details;

                // Allow the user to edit item text.
                listView1.LabelEdit = false;

                // Allow the user to rearrange columns.
                listView1.AllowColumnReorder = false;

                // Select the item and subitems when selection is made.
                listView1.FullRowSelect = true;

                // Display grid lines.
                listView1.GridLines = true;

                // Sort the items in the list in ascending order.
                //listView1.Sorting = SortOrder.Descending;

                switch (_CODE)
                {
                    case "DETAILS":
                        {
                            // Attach Subitems to the ListView
                            //listView1.Columns.Clear();
                            //listView1.Columns.Add("Date/Time", 150, HorizontalAlignment.Left);
                            //listView1.Columns.Add("TransactionType", 100, HorizontalAlignment.Left);                                              
                            //listView1.Columns.Add("Value(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")", 75, HorizontalAlignment.Right);                        
                            //listView1.Columns.Add("Pos-Ter", 75, HorizontalAlignment.Left);
                            //listView1.Columns.Add("Game", 75, HorizontalAlignment.Left);
                            //listView1.Columns.Add("Voucher Print Date/Time", 150, HorizontalAlignment.Left);
                            //if (oBusiness.CheckViewTicketAccess())  
                            //    listView1.Columns.Add("Details", 150, HorizontalAlignment.Left);
                            ////listView1.Columns.Add("Zone", 75, HorizontalAlignment.Left);

                            listView1.Columns.Clear();

                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_TransactionType",Width = 205, TextAlign =HorizontalAlignment.Left});
                            listView1.Columns.Add(new ColumnHeader() { Text =this.GetResourceTextByKey("Key_Amount")+"(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")",Width = 75,TextAlign = HorizontalAlignment.Right});
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_PrintedAsset",Width = 80,TextAlign = HorizontalAlignment.Left});
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_PrintedSiteCode",Width = 80,TextAlign = HorizontalAlignment.Left});
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_PrintedPosition",Width = 80,TextAlign = HorizontalAlignment.Left});
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_PrintedDateTime", Width =130,TextAlign = HorizontalAlignment.Left});
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_PaidAsset",Width = 75,TextAlign = HorizontalAlignment.Left});
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_PaidPosition",Width = 75, TextAlign =HorizontalAlignment.Left});
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_PaidExpiredVoidDateTime", Width =180,TextAlign = HorizontalAlignment.Left});
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_Voucher",Width = 120,TextAlign = HorizontalAlignment.Left});
                            LogManager.WriteLog("Inside CheckAccessForViewTicketNumber method with userID " + _UserID.ToString(), LogManager.enumLogLevel.Info);

                            break;
                        }
                    case "ACTIVE":
                        {
                            listView1.Columns.Clear();
                            listView1.Columns.Add(new ColumnHeader() { Tag = "Key_DateByTime", Width = 150, TextAlign = HorizontalAlignment.Left });
                            listView1.Columns.Add(new ColumnHeader() { Tag = "Key_TransactionType", Width = 100, TextAlign = HorizontalAlignment.Left });//TransactionType
                            listView1.Columns.Add(new ColumnHeader() { Tag = "Key_Zone", Width = 75, TextAlign =HorizontalAlignment.Left});//Zone
                            listView1.Columns.Add(new ColumnHeader() { Tag = "Key_PosTer", Width = 75, TextAlign =HorizontalAlignment.Left});//Machine
                            listView1.Columns.Add(new ColumnHeader() { Tag = "Key_Machine", Width = 75,TextAlign = HorizontalAlignment.Left});//SEGM                                          
                            listView1.Columns.Add(new ColumnHeader() { Text = this.GetResourceTextByKey("Key_Value") + "(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")", Width = 75, TextAlign=HorizontalAlignment.Right});//Value
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_VoucherPrintDateTime", Width = 150, TextAlign=HorizontalAlignment.Left});//paydate

                            break;
                        }
                    case "VOIDCANCEL":
                    case "EXPIRED":
                        {
                            listView1.Columns.Clear();
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_DateByTime", Width =150, TextAlign =HorizontalAlignment.Left});//printDate,Key_Details
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_TransactionType",Width = 100,TextAlign = HorizontalAlignment.Left});//TransactionType
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_Zone",Width = 75,TextAlign = HorizontalAlignment.Left});//Zone
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_PosTer",Width = 75,TextAlign = HorizontalAlignment.Left});//Machine
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_Machine",Width = 75, TextAlign =HorizontalAlignment.Left});//SEGM
                            listView1.Columns.Add(new ColumnHeader() { Text =this.GetResourceTextByKey("Key_Value")+"(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")", Width =75,TextAlign = HorizontalAlignment.Right});//Amount
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_VoucherPrintDateTime",Width = 150, TextAlign =HorizontalAlignment.Left});//paydate
                            if (oBusiness.CheckViewTicketAccess(_UserID))
                                listView1.Columns.Add(new ColumnHeader() { Tag ="Key_Details", Width =150,TextAlign = HorizontalAlignment.Left});//Status

                            LogManager.WriteLog("Inside CheckAccessForViewTicketNumber method with userID " + _UserID.ToString(), LogManager.enumLogLevel.Info);
                            break;
                        }
                    case "PROMO":
                        {
                            listView1.Columns.Clear();
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_DateByTime", Width =150, TextAlign =HorizontalAlignment.Left});//printDate,Key_AssetsNo
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_TransactionType",Width = 100,TextAlign = HorizontalAlignment.Left});//TransactionType
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_PosTer",Width = 75,TextAlign = HorizontalAlignment.Left});//Machine
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_GameTitles", Width =75, TextAlign =HorizontalAlignment.Left});//Zone
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_AssetsNo", Width =75,TextAlign = HorizontalAlignment.Left});//SEGM
                            listView1.Columns.Add(new ColumnHeader() { Text =this.GetResourceTextByKey("Key_Value") + "(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")",Width = 75,TextAlign = HorizontalAlignment.Right});//Amount
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_VoucherPrintDateTime",Width = 150,TextAlign = HorizontalAlignment.Left});//paydate


                            break;
                        }
                    case "EXCEP":
                        {
                            listView1.Columns.Clear();
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_Type",Width = 50,TextAlign = HorizontalAlignment.Left});//Type
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_Position",Width = 50, TextAlign =HorizontalAlignment.Left});//Position
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_DateByTime",Width = 150, TextAlign =HorizontalAlignment.Left});//printDate
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_Voucher",Width = 200, TextAlign =HorizontalAlignment.Left});//Ticket
                            listView1.Columns.Add(new ColumnHeader() { Text =this.GetResourceTextByKey("Key_Value") + "(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")",Width = 75,TextAlign = HorizontalAlignment.Right});//Amount
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_AssetsNo",Width = 75, TextAlign =HorizontalAlignment.Left});//Asset
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_CreateCompleted",Width = 150, TextAlign =HorizontalAlignment.Left});//CreateCompleted

                            break;
                        }
                    case "LIABILITY":
                        {
                            listView1.Columns.Clear();//Key_PrintDate
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_PrintDate",Width = 150, TextAlign =HorizontalAlignment.Left});//printDate,key_PrintDevice,Key_ClaimDevice
                            listView1.Columns.Add(new ColumnHeader() { Tag ="key_PrintDevice",Width = 100,TextAlign = HorizontalAlignment.Left});//position
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_ClaimDate",Width = 75, TextAlign =HorizontalAlignment.Left});//payDate
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_ClaimDevice",Width = 75,TextAlign = HorizontalAlignment.Left});//PayDevice
                            listView1.Columns.Add(new ColumnHeader() { Text =this.GetResourceTextByKey("Key_Amount")+"(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")",Width = 75,TextAlign = HorizontalAlignment.Right});//Amount
                            if (oBusiness.CheckViewTicketAccess(_UserID))
                            {
                                listView1.Columns.Add(new ColumnHeader() { Tag ="Key_Voucher",Width = 200,TextAlign = HorizontalAlignment.Left});//Ticket,Key_Status
                            }
                            else
                            {
                                listView1.Columns.Add(new ColumnHeader() { Tag ="Key_Voucher",Width = 0, TextAlign =HorizontalAlignment.Left});//Ticket
                            }
                            listView1.Columns.Add(new ColumnHeader() { Tag ="Key_Status",Width = 75,TextAlign = HorizontalAlignment.Left});//Status
                            
                            
                            LogManager.WriteLog("Inside CheckAccessForViewTicketNumber method with userID " + _UserID.ToString(), LogManager.enumLogLevel.Info);
                            break;
                        }
                    default:
                        {
                            this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_ERROR_OCCURED"), this.Text);
                            LogManager.WriteLog("InitializeListView() Error.", LogManager.enumLogLevel.Debug);
                            this.Close();
                            break;
                        }
                }

                LogManager.WriteLog("Column Definition Done.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        // Load Data from the DataSet into the ListView
        private void LoadList()
        {
            try
            {

                switch (_CODE)
                {
                    case "DETAILS":
                        {

                            // Get the table from the data set
                            DataTable dtable = _DtDetails.Tables["DetailsView"];

                            // Clear the ListView control
                            listView1.Items.Clear();
                            bool ViewTicketAccess = oBusiness.CheckViewTicketAccess(_UserID);
                            // Display items in the ListView control
                            for (int i = 0; i < dtable.Rows.Count; i++)
                            {
                                DataRow drow = dtable.Rows[i];

                                // Only row that have not been deleted
                                if (drow.RowState != DataRowState.Deleted)
                                {
                                    try
                                    {
                                        if (!ViewTicketAccess)
                                        {
                                            string str = drow["Ticket"].ToString().Trim();
                                            drow["Ticket"] = (str.Length > 8) ? str.Remove(str.Length - 4, 4) + "****" : str;
                                        }
                                    }
                                    catch (Exception E)
                                    {
                                        ExceptionManager.Publish(E);

                                    }
                                    // Define the list items
                                    ListViewItem lvi = new ListViewItem(drow["Trans_Type"].ToString());
                                    lvi.SubItems.Add(drow["Amount"].ToString().GetDecimal().GetUniversalCurrencyFormat());
                                    lvi.SubItems.Add(drow["PrintAsset"].ToString());
                                    lvi.SubItems.Add(drow["PrintSiteCode"].ToString());
                                    lvi.SubItems.Add(drow["PrintPosition"].ToString());
                                    lvi.SubItems.Add(drow["PrintedDate"].ToDateTimeString());
                                    lvi.SubItems.Add(drow["PaidAsset"].ToString());
                                    lvi.SubItems.Add(drow["PaidPosition"].ToString());
                                    lvi.SubItems.Add(drow["PaidDate"].ToDateTimeString());
                                    lvi.SubItems.Add(drow["Ticket"].ToString().TrimEnd());


                                    // Add the list items to the ListView
                                    listView1.Items.Add(lvi);
                                }
                            }
                            break;
                        }
                    case "ACTIVE":
                        {
                            this.Text = "Cashier Transactions(Active)";
                            _DtDetails = LoadActiveTickets();

                            if (_DtDetails.Tables[0].Rows.Count > 1)
                            {
                                DataTable dtACTIVE = _DtDetails.Tables["Tickets"];
                                listView1.Items.Clear();

                                for (int i = 0; i < dtACTIVE.Rows.Count; i++)
                                {
                                    DataRow drow = dtACTIVE.Rows[i];

                                    if (drow.RowState != DataRowState.Deleted)
                                    {
                                        ListViewItem lvi = new ListViewItem(drow["printDate"].ToDateTimeString());
                                        lvi.SubItems.Add(drow["TransactionType"].ToString());
                                        lvi.SubItems.Add(drow["Zone"].ToString());
                                        lvi.SubItems.Add(drow["Machine"].ToString());
                                        lvi.SubItems.Add(drow["SEGM"].ToString());
                                        lvi.SubItems.Add(drow["Amount"].ToString().GetDecimal().GetUniversalCurrencyFormat());
                                        lvi.SubItems.Add(drow["paydate"].ToDateTimeString());

                                        listView1.Items.Add(lvi);
                                    }
                                }
                            }
                            else
                            {
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
                                this.Close();
                            }
                            break;
                        }
                    case "VOIDCANCEL":
                        {
                            this.Text = "Cashier Transactions(Void/Cancel)";
                            _DtDetails = LoadVoidCancelledTickets();
                            if (_DtDetails.Tables[0].Rows.Count > 1)
                            {

                                DataTable dtVOID = _DtDetails.Tables["Tickets"];
                                listView1.Items.Clear();



                                for (int i = 0; i < dtVOID.Rows.Count; i++)
                                {
                                    DataRow drow = dtVOID.Rows[i];

                                    if (drow.RowState != DataRowState.Deleted)
                                    {
                                        ListViewItem lvi = new ListViewItem(drow["printDate"].ToDateTimeString());
                                        lvi.SubItems.Add(drow["TransactionType"].ToString());
                                        lvi.SubItems.Add(drow["Zone"].ToString());
                                        lvi.SubItems.Add(drow["Machine"].ToString());
                                        lvi.SubItems.Add(drow["SEGM"].ToString());
                                        lvi.SubItems.Add(drow["Amount"].ToString().GetDecimal().GetUniversalCurrencyFormat());
                                        lvi.SubItems.Add(drow["paydate"].ToDateTimeString());
                                        lvi.SubItems.Add(drow["Status"].ToString());

                                        listView1.Items.Add(lvi);
                                    }
                                }

                            }
                            else
                            {
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
                                this.Close();
                            }
                            break;
                        }
                    case "EXPIRED":
                        {
                            this.Text = "Cashier Transactions(Expired)";
                            _DtDetails = LoadVoidExpiredTickets();

                            if (_DtDetails.Tables[0].Rows.Count > 1)
                            {
                                DataTable dtExpired = _DtDetails.Tables["Tickets"];
                                listView1.Items.Clear();


                                for (int i = 0; i < dtExpired.Rows.Count; i++)
                                {
                                    DataRow drow = dtExpired.Rows[i];

                                    if (drow.RowState != DataRowState.Deleted)
                                    {
                                        ListViewItem lvi = new ListViewItem(drow["printDate"].ToDateTimeString());
                                        lvi.SubItems.Add(drow["TransactionType"].ToString());
                                        lvi.SubItems.Add(drow["Zone"].ToString());
                                        lvi.SubItems.Add(drow["Machine"].ToString());
                                        lvi.SubItems.Add(drow["SEGM"].ToString());
                                        lvi.SubItems.Add(drow["Amount"].ToString().GetDecimal().GetUniversalCurrencyFormat());
                                        lvi.SubItems.Add(drow["paydate"].ToDateTimeString());
                                        lvi.SubItems.Add(drow["Status"].ToString());

                                        listView1.Items.Add(lvi);
                                    }
                                }
                            }
                            else
                            {
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
                                this.Close();
                            }
                            break;
                        }

                    case "PROMO":
                        {
                            this.Text = "Cashier Transactions(Promo)";
                            _DtDetails = LoadPromoTickets();
                            if (_DtDetails.Tables[0].Rows.Count > 1)
                            {
                                DataTable dtPromo = _DtDetails.Tables["Tickets"];
                                listView1.Items.Clear();


                                for (int i = 0; i < dtPromo.Rows.Count; i++)
                                {
                                    DataRow drow = dtPromo.Rows[i];

                                    if (drow.RowState != DataRowState.Deleted)
                                    {
                                        ListViewItem lvi = new ListViewItem(drow["printDate"].ToDateTimeString());
                                        lvi.SubItems.Add(drow["TransactionType"].ToString());
                                        lvi.SubItems.Add(drow["Machine"].ToString());
                                        lvi.SubItems.Add(drow["Zone"].ToString());
                                        lvi.SubItems.Add(drow["SEGM"].ToString());
                                        lvi.SubItems.Add(drow["Value"].ToString().GetDecimal().GetUniversalCurrencyFormat());
                                        lvi.SubItems.Add(drow["paydate"].ToDateTimeString());

                                        listView1.Items.Add(lvi);
                                    }
                                }
                            }
                            else
                            {
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
                                this.Close();
                            }
                            break;
                        }
                    case "EXCEP":
                        {
                            this.Text = "Cashier Transactions(Exception)";
                            _DtDetails = LoadExceptions();
                            if (_DtDetails.Tables[0].Rows.Count > 1)
                            {
                                DataTable dtExcep = _DtDetails.Tables["Tickets"];
                                listView1.Items.Clear();


                                for (int i = 0; i < dtExcep.Rows.Count; i++)
                                {
                                    DataRow drow = dtExcep.Rows[i];

                                    if (drow.RowState != DataRowState.Deleted)
                                    {
                                        ListViewItem lvi = new ListViewItem(drow["Type"].ToString());
                                        lvi.SubItems.Add(drow["Position"].ToString());
                                        lvi.SubItems.Add(drow["printDate"].ToDateTimeString());
                                        lvi.SubItems.Add(drow["Ticket"].ToString().TrimEnd());
                                        lvi.SubItems.Add(drow["Amount"].ToString().GetDecimal().GetUniversalCurrencyFormat());
                                        lvi.SubItems.Add(drow["Asset"].ToString());
                                        lvi.SubItems.Add(drow["CreateCompleted"].ToString());

                                        if (!oBusiness.CheckViewTicketAccess(_UserID))
                                            drow["Ticket"] = string.Empty;

                                        listView1.Items.Add(lvi);
                                    }
                                }
                            }
                            else
                            {
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
                                this.Close();
                            }

                            break;
                        }
                    case "LIABILITY":
                        {
                            this.Text = "Cashier Transactions(Liability)";
                            _DtDetails = LoadLiabilities();
                            if (_DtDetails.Tables[0].Rows.Count > 1)
                            {
                                DataTable dtExcep = _DtDetails.Tables["Tickets"];
                                listView1.Items.Clear();


                                for (int i = 0; i < dtExcep.Rows.Count; i++)
                                {
                                    DataRow drow = dtExcep.Rows[i];

                                    if (drow.RowState != DataRowState.Deleted)
                                    {
                                        ListViewItem lvi = new ListViewItem(drow["printDate"].ToDateTimeString());
                                        lvi.SubItems.Add(drow["position"].ToString());
                                        lvi.SubItems.Add(drow["payDate"].ToDateTimeString());
                                        lvi.SubItems.Add(drow["PayDevice"].ToString());
                                        lvi.SubItems.Add(drow["Amount"].ToString().GetDecimal().GetUniversalCurrencyFormat());
                                        lvi.SubItems.Add(drow["Ticket"].ToString().TrimEnd());
                                        lvi.SubItems.Add(drow["Status"].ToString());

                                        if (!oBusiness.CheckViewTicketAccess(_UserID))
                                        {
                                            drow["Ticket"] = string.Empty;
                                        }

                                        listView1.Items.Add(lvi);
                                    }
                                }
                            }
                            else
                            {
                                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CDM_NO_RECORDS_FOR_SELECT"), this.Text);
                                this.Close();
                            }
                            break;
                        }
                    default:
                        {
                            this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_ERROR_OCCURED"), this.Text);
                            LogManager.WriteLog("Loadlist() Error.", LogManager.enumLogLevel.Debug);
                            this.Close();
                            break;
                        }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.StartDate = _FromDate.ToString();
                spParams.EndDate = _ToDate.ToString();
                spParams.SiteName = _SiteName;
                spParams.User_No = _UserID;
                
                
                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadLocalReport(Application.StartupPath + "\\BMC.ClientReports\\ENT_CDMDetailedView.rdl", "DtDetails", _DtDetails, this.GetResourceTextByKey("Key_RT_CashierTransactionsDetailedView"), "ENT_CDMDetailedView", spParams);
               // BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_CDM_GetCashierTransactionsDetails", "Cashier Transactions Detailed View", "ENT_CDMDetailedView", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            //try
            //{
                
                //using (ReportViewer cReportViewer = new ReportViewer())
                //{

                //    cReportViewer.showDetailedReport(_DtDetails, _FromDate, _ToDate, _SiteName, _FooterText);
                //    cReportViewer.ShowDialog();
                //}

                //switch (_CODE)
                //{
                //    case "DETAILS":
                //        {
                //            using (ReportViewer cReportViewer = new ReportViewer())
                //            {

                //                cReportViewer.showDetailedReport(_DtDetails, _FromDate, _ToDate, _SiteName, _FooterText);
                //                cReportViewer.ShowDialog();
                //            }
                //            break;
                //        }
                    //case "ACTIVE":
                    //    {
                    //        using (ReportViewer cReportViewer = new ReportViewer())
                    //        {
                    //            cReportViewer.showActiveReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Print, "");
                    //            //cReportViewer.ShowDialog();
                    //        }
                    //        break;
                    //    }
                    //case "VOIDCANCEL":
                    //case "EXPIRED":
                    //    {
                    //        using (ReportViewer cReportViewer = new ReportViewer())
                    //        {
                    //            cReportViewer.showVOIDReport(_CODE, _DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Print, "");
                    //            //cReportViewer.ShowDialog();
                    //        }

                    //        break;
                    //    }
                    //case "PROMO":
                    //    {
                    //        using (ReportViewer cReportViewer = new ReportViewer())
                    //        {
                    //            cReportViewer.showPROMOReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Print, "");
                    //            //cReportViewer.ShowDialog();
                    //        }

                    //        break;
                    //    }
                    //case "EXCEP":
                    //    {
                    //        using (ReportViewer cReportViewer = new ReportViewer())
                    //        {
                    //            cReportViewer.showEXCEPReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Print, "");
                    //            //cReportViewer.ShowDialog();
                    //        }

                    //        break;
                    //    }
                    //case "LIABILITY":
                    //    {
                    //        using (ReportViewer cReportViewer = new ReportViewer())
                    //        {
                    //            cReportViewer.showLIABILITYReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Print, "");
                    //            //cReportViewer.ShowDialog();
                    //        }

                    //        break;
                    //    }
                //    default:
                //        {
                //            this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_ERROR_OCCURED"), this.Text);
                //            LogManager.WriteLog("btnPrint_Click() Error.", LogManager.enumLogLevel.Debug);
                //            this.Close();
                //            break;
                //        }
                //}
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.Publish(ex);
            //}
        }

        public DataSet LoadActiveTickets()
        {
            try
            {
                TicketsClaimed oTicketsClaimed = new TicketsClaimed();
                oTicketsClaimed.TicketsClaimedFrom = _FromDate;
                oTicketsClaimed.TicketsClaimedTo = _ToDate;
                oTicketsClaimed.SITE = _SiteID;

                Tickets oTickets = new Tickets();
                oTickets.EndDate = _ToDate;
                oTickets.StartDate = _FromDate;
                oTickets.IsLiability = false;
                oTickets.BarCode = "%";
                oTickets.Type = "U";
                oTickets.SITE = _SiteID;

                List<TicketExceptions> lstTitoTicketsUnclaimed = oBusiness.TitoTicketsUnclaimed(oTickets);
                if (lstTitoTicketsUnclaimed == null)
                {
                    lstTitoTicketsUnclaimed = new List<TicketExceptions>();
                }

                List<TicketExceptions> lstTicketsUnClaimed = oBusiness.TicketsUnclaimed(oTicketsClaimed);

                if (lstTicketsUnClaimed != null)
                {
                    foreach (TicketExceptions item in lstTicketsUnClaimed)
                    {
                        lstTitoTicketsUnclaimed.Add(item);
                    }

                }
                decimal ExceptionTotal = 0;
                TicketExceptions Total = new TicketExceptions();
                Total.PrintDate = string.Empty;
                Total.PrintDate = "Total";
                Total.PayDate = string.Empty;
                foreach (TicketExceptions exep in lstTitoTicketsUnclaimed)
                {
                    ExceptionTotal += (decimal)exep.Value;
                }
                Total.Value = (double)Decimal.Round(ExceptionTotal, 2);
                Total.Amount = Total.Value.ToString("###0.#0");
                lstTitoTicketsUnclaimed.Insert(0, Total);

                return lstTitoTicketsUnclaimed.ConvertGenericList("Tickets");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return new DataSet();
        }

        public DataSet LoadVoidCancelledTickets()
        {
            try
            {
                Tickets oTickets = new Tickets();
                oTickets.EndDate = _ToDate;
                oTickets.StartDate = _FromDate;
                oTickets.IsLiability = false;
                oTickets.BarCode = "%";
                oTickets.Type = "B";
                oTickets.SITE = _SiteID;

                List<TicketExceptions> lstVoidCancelled = oBusiness.GetTicket_VoidnExpired(oTickets);

                if (lstVoidCancelled != null)
                {
                    decimal ExceptionTotal = 0;
                    TicketExceptions Total = new TicketExceptions();
                    Total.PrintDate = "Total";
                    foreach (TicketExceptions exep in lstVoidCancelled)
                    {
                        ExceptionTotal += (decimal)exep.Value;
                    }
                    Total.Value = (double)Decimal.Round(ExceptionTotal, 2);
                    Total.Amount = Total.Value.ToString("###0.#0");
                    lstVoidCancelled.Insert(0, Total);
                }

                return lstVoidCancelled.ConvertGenericList("Tickets");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return new DataSet();
        }

        public DataSet LoadVoidExpiredTickets()
        {
            try
            {
                Tickets oTickets = new Tickets();
                oTickets.EndDate = _ToDate;
                oTickets.StartDate = _FromDate;
                oTickets.IsLiability = false;
                oTickets.BarCode = "%";
                oTickets.Type = "D";
                oTickets.SITE = _SiteID;

                List<TicketExceptions> lstVoidExpired = oBusiness.GetTicket_VoidnExpired(oTickets);

                decimal ExceptionTotal = 0;
                TicketExceptions Total = new TicketExceptions();
                Total.PrintDate = "Total";

                if (lstVoidExpired != null)
                {
                    foreach (TicketExceptions exep in lstVoidExpired)
                    {
                        ExceptionTotal += (decimal)exep.Value;
                    }

                    Total.Value = (double)Decimal.Round(ExceptionTotal, 2);
                    Total.Amount = Total.Value.ToString("###0.0#");
                    lstVoidExpired.Insert(0, Total);
                }

                return lstVoidExpired.ConvertGenericList("Tickets");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new DataSet();
        }

        public DataSet LoadPromoTickets()
        {
            try
            {
                TicketsClaimed oTicketsClaimed = new TicketsClaimed();
                oTicketsClaimed.TicketsClaimedFrom = _FromDate;
                oTicketsClaimed.TicketsClaimedTo = _ToDate;
                oTicketsClaimed.SITE = _SiteID;

                List<TicketExceptions> lstPromoTickets = oBusiness.GetPromoCashableTickets(oTicketsClaimed);

                decimal ExceptionTotal = 0;
                TicketExceptions Total = new TicketExceptions();
                Total.PrintDate = "Total";
                if (lstPromoTickets != null)
                {
                    foreach (TicketExceptions exep in lstPromoTickets)
                    {
                        ExceptionTotal += (decimal)exep.Value;
                    }
                    Total.Value = (double)Decimal.Round(ExceptionTotal, 2);
                    Total.Amount = Total.Value.ToString("###0.0#");
                    lstPromoTickets.Insert(0, Total);
                }

                return lstPromoTickets.ConvertGenericList("Tickets");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new DataSet();
        }

        public DataSet LoadExceptions()
        {
            try
            {
                float ExceptionTotal = 0F;
                Tickets oTickets = new Tickets();
                oTickets.EndDate = _ToDate;
                oTickets.StartDate = _FromDate;
                oTickets.IsLiability = false;
                oTickets.BarCode = "%";
                oTickets.Type = "E";
                oTickets.SITE = _SiteID;


                List<TicketExceptions> lstExceptions = oBusiness.TITOTicketInExceptions(oTickets);
                if (lstExceptions == null)
                {
                    lstExceptions = new List<TicketExceptions>();

                }

                List<TicketExceptions> lstExceptionsOut = oBusiness.TitoTicketOutExceptions(oTickets);


                if (lstExceptionsOut != null)
                {
                    foreach (TicketExceptions exep in lstExceptionsOut)
                    {
                        lstExceptions.Add(exep);

                    }
                }

                //CreateTotals cExceptionsTotal
                foreach (TicketExceptions item in lstExceptions)
                {
                    ExceptionTotal += item.currValue;
                }

                TicketExceptions Total = new TicketExceptions();
                Total.Type = "Total";
                Total.Amount = ExceptionTotal.ToString("###0.#0");

                lstExceptions.Insert(0, Total);


                return lstExceptions.ConvertGenericList("Tickets");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new DataSet();
        }

        public DataSet LoadLiabilities()
        {

            try
            {
                TicketsClaimed oTicketsClaimed = new TicketsClaimed();
                oTicketsClaimed.TicketsClaimedFrom = _FromDate;
                oTicketsClaimed.TicketsClaimedTo = _ToDate;
                oTicketsClaimed.SITE = _SiteID;

                Tickets oTickets = new Tickets();
                oTickets.EndDate = _ToDate;
                oTickets.StartDate = _FromDate;
                oTickets.IsLiability = true;
                oTickets.BarCode = "%";
                oTickets.Type = "C";
                oTickets.SITE = _SiteID;

                List<TicketExceptions> lstTitoTicketsClaimed = oBusiness.TitoTicketsClaimedLiability(oTickets);
                if (lstTitoTicketsClaimed == null)
                {
                    lstTitoTicketsClaimed = new List<TicketExceptions>();
                }

                oTickets.Type = "P";

                List<TicketExceptions> lstTitoTicketsPrinted = oBusiness.TitoTicketsPrintedLiability(oTickets);


                if (lstTitoTicketsPrinted != null)
                {
                    foreach (TicketExceptions item in lstTitoTicketsPrinted)
                    {
                        if (item.VoucherStatus.ToUpper().Trim() != "PP")
                        {
                            lstTitoTicketsClaimed.Add(item);
                        }
                    }
                }

                List<TicketExceptions> lstTicketsClaimed = oBusiness.TicketsClaimed(oTicketsClaimed);
                if (lstTicketsClaimed != null)
                {
                    foreach (TicketExceptions item in lstTicketsClaimed)
                    {
                        if (item.VoucherStatus.ToUpper().Trim() != "PP")
                        {
                            lstTitoTicketsClaimed.Add(item);
                        }
                    }

                }

                List<TicketExceptions> lstTicketsPrinted = oBusiness.TicketsPrinted(oTicketsClaimed);
                if (lstTicketsPrinted != null)
                {
                    foreach (TicketExceptions item in lstTicketsPrinted)
                    {
                        lstTitoTicketsClaimed.Add(item);
                    }

                }


                float ExceptionTotal = 0F;
                TicketExceptions Total = new TicketExceptions();
                Total.PrintDate = "Total";
                foreach (TicketExceptions exep in lstTitoTicketsClaimed)
                {
                    if (exep.VoucherStatus.ToUpper().Trim() != "PP")
                    {
                        ExceptionTotal += (float)exep.Value;
                    }
                }
                Total.Value = ExceptionTotal;
                Total.Amount = ExceptionTotal.ToString();
                // Total.Amount = Total.Value.ToString("###0.0#");
                lstTitoTicketsClaimed.Insert(0, Total);
                return lstTitoTicketsClaimed.ConvertGenericList("Tickets");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return new DataSet();

        }
       
        private void btnExport_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    SaveFileDialog fileDialog = null;
            //    string filepath = string.Empty;

            //    fileDialog = new SaveFileDialog();
            //    fileDialog.Filter = "Excel Files (*.xls)|*.xls|All Files (*.*) |*.*";
            //    fileDialog.ShowDialog();
            //    filepath = fileDialog.FileName;
            //    if (filepath != "")
            //    {
            //        switch (_CODE)
            //        {
            //            case "ACTIVE":
            //                {
            //                    using (ReportViewer cReportViewer = new ReportViewer())
            //                    {
            //                        cReportViewer.showActiveReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Export, filepath);
            //                        cReportViewer.ShowDialog();
            //                    }
            //                    break;
            //                }
            //            case "VOIDCANCEL":
            //            case "EXPIRED":
            //                {
            //                    //using (ReportViewer cReportViewer = new ReportViewer())
            //                    //{
            //                    //    cReportViewer.showVOIDReport(_CODE, _DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Export, filepath);
            //                    //    //cReportViewer.ShowDialog();
            //                    //}

            //                    break;
            //                }
            //            case "PROMO":
            //                {
            //                    //using (ReportViewer cReportViewer = new ReportViewer())
            //                    //{
            //                    //    cReportViewer.showPROMOReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Export, filepath);
            //                    //    //cReportViewer.ShowDialog();
            //                    //}

            //                    break;
            //                }
            //            case "EXCEP":
            //                {
            //                    //using (ReportViewer cReportViewer = new ReportViewer())
            //                    //{
            //                    //    cReportViewer.showEXCEPReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Export, filepath);
            //                    //    //cReportViewer.ShowDialog();
            //                    //}

            //                    break;
            //                }
            //            case "LIABILITY":
            //                {
            //                    //using (ReportViewer cReportViewer = new ReportViewer())
            //                    //{
            //                    //    cReportViewer.showLIABILITYReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Export, filepath);
            //                    //    //cReportViewer.ShowDialog();
            //                    //}

            //                    break;
            //                }
            //            default:
            //                {
            //                    this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_ERROR_OCCURED"), this.Text);
            //                    LogManager.WriteLog("btnExport_Click() Error.", LogManager.enumLogLevel.Debug);
            //                    this.Close();
            //                    break;
            //                }
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.Publish(ex);
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
