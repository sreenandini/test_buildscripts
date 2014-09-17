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

namespace BMCCashierTransactions
{
    public partial class DetailsScreen : Form
    {
        DataSet _DtDetails = null;
        DateTime _FromDate;
        DateTime _ToDate;
        string _SiteName;
        int _SiteID;
        string _CODE;
        string _FooterText;
        TreasuryTransactions oBusiness = null;


        public DetailsScreen(DataSet DtDetails, DateTime FromDate, DateTime ToDate, String SiteName, string sFooterText)
        {
            InitializeComponent();

            _DtDetails = DtDetails;
            _FromDate = FromDate;
            _ToDate = ToDate;
            _SiteName = SiteName;
            _CODE = "DETAILS";
            _FooterText = sFooterText;
            LogManager.WriteLog("DetailsScreen: FromDate-" + _FromDate.ToString() + " ToDate-" + _ToDate.ToString() +
                                    " SiteName-" + _SiteName, LogManager.enumLogLevel.Info);
            btnExport.Visible = false;
            oBusiness = new TreasuryTransactions();
            InitializeListView();
        }

        public DetailsScreen(string sCODE, DateTime FromDate, DateTime ToDate, String SiteName, int Site)
        {
            InitializeComponent();
            oBusiness = new TreasuryTransactions();

            _FromDate = FromDate;
            _ToDate = ToDate;
            _SiteName = SiteName;
            _CODE = sCODE;
            _SiteID = Site;
           
            LogManager.WriteLog("DetailsScreen: FromDate-" + _FromDate.ToString() + " ToDate-" + _ToDate.ToString() +
                                   " SiteName-" + _SiteName + " FooterText"+_FooterText, LogManager.enumLogLevel.Info);
            btnExport.Visible = true;
            InitializeListView();
        }


        private void DetailsScreen_Load(object sender, EventArgs e)
        {
            try
            {
                LoadList();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        // Initialize ListView
        private void InitializeListView()
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
                        listView1.Columns.Add("TransactionType", 205, HorizontalAlignment.Left);                                              
                        listView1.Columns.Add("Amount(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")", 75, HorizontalAlignment.Right);
                        listView1.Columns.Add("Printed Asset", 80, HorizontalAlignment.Left);
                        listView1.Columns.Add("Printed SiteCode", 80, HorizontalAlignment.Left);                      
                        listView1.Columns.Add("Printed Position", 80, HorizontalAlignment.Left);
                        listView1.Columns.Add("Printed Date/Time", 130, HorizontalAlignment.Left);
                        listView1.Columns.Add("Paid Asset", 75, HorizontalAlignment.Left);
                        listView1.Columns.Add("Paid Position", 75, HorizontalAlignment.Left);
                        listView1.Columns.Add("Paid/Expired/Void Date/Time", 180, HorizontalAlignment.Left);
						listView1.Columns.Add("Voucher", 120, HorizontalAlignment.Left);
                        LogManager.WriteLog("Inside CheckAccessForViewTicketNumber method with userID " + Program.nUserID.ToString(), LogManager.enumLogLevel.Info);

                        break;
                    }
                case "ACTIVE":
                    {
                        listView1.Columns.Clear();
                        listView1.Columns.Add("Date/Time", 150, HorizontalAlignment.Left);//printDate
                        listView1.Columns.Add("TransactionType", 100, HorizontalAlignment.Left);//TransactionType
                        listView1.Columns.Add("Zone", 75, HorizontalAlignment.Left);//Zone
                        listView1.Columns.Add("Pos-Ter", 75, HorizontalAlignment.Left);//Machine
                        listView1.Columns.Add("Machine", 75, HorizontalAlignment.Left);//SEGM                                          
                         listView1.Columns.Add("Value(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")", 75, HorizontalAlignment.Right);//Value
                        listView1.Columns.Add("Voucher Print Date/Time", 150, HorizontalAlignment.Left);//paydate

                        break;
                    }
                case "VOIDCANCEL":
                case "EXPIRED":
                    {
                        listView1.Columns.Clear();
                        listView1.Columns.Add("Date/Time", 150, HorizontalAlignment.Left);//printDate
                        listView1.Columns.Add("TransactionType", 100, HorizontalAlignment.Left);//TransactionType
                        listView1.Columns.Add("Zone", 75, HorizontalAlignment.Left);//Zone
                        listView1.Columns.Add("Pos-Ter", 75, HorizontalAlignment.Left);//Machine
                        listView1.Columns.Add("Machine", 75, HorizontalAlignment.Left);//SEGM
                         listView1.Columns.Add("Value(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")", 75, HorizontalAlignment.Right);//Amount
                        listView1.Columns.Add("Voucher Print Date/Time", 150, HorizontalAlignment.Left);//paydate
                        if (oBusiness.CheckViewTicketAccess())  
                            listView1.Columns.Add("Details", 150, HorizontalAlignment.Left);//Status

                        LogManager.WriteLog("Inside CheckAccessForViewTicketNumber method with userID " + Program.nUserID.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    }
                case "PROMO":
                    {
                        listView1.Columns.Clear();
                        listView1.Columns.Add("Date/Time", 150, HorizontalAlignment.Left);//printDate
                        listView1.Columns.Add("TransactionType", 100, HorizontalAlignment.Left);//TransactionType
                        listView1.Columns.Add("Pos-Ter", 75, HorizontalAlignment.Left);//Machine
                        listView1.Columns.Add("Game-Title", 75, HorizontalAlignment.Left);//Zone
                        listView1.Columns.Add("Asset-No", 75, HorizontalAlignment.Left);//SEGM
                         listView1.Columns.Add("Value(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")", 75, HorizontalAlignment.Right);//Amount
                        listView1.Columns.Add("Voucher Print Date/Time", 150, HorizontalAlignment.Left);//paydate


                        break;
                    }
                case "EXCEP":
                    {
                        listView1.Columns.Clear();
                        listView1.Columns.Add("Type", 50, HorizontalAlignment.Left);//Type
                        listView1.Columns.Add("Position", 50, HorizontalAlignment.Left);//Position
                        listView1.Columns.Add("Date/Time", 150, HorizontalAlignment.Left);//printDate
                        listView1.Columns.Add("Voucher", 200, HorizontalAlignment.Left);//Ticket
                        listView1.Columns.Add("Value(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")", 75, HorizontalAlignment.Right);//Amount
                        listView1.Columns.Add("Asset-No", 75, HorizontalAlignment.Left);//Asset
                        listView1.Columns.Add("Create Completed", 150, HorizontalAlignment.Left);//CreateCompleted

                        break;
                    }
                case "LIABILITY":
                    {
                        listView1.Columns.Clear();
                        listView1.Columns.Add("Print Date", 150, HorizontalAlignment.Left);//printDate
                        listView1.Columns.Add("Print Device", 100, HorizontalAlignment.Left);//position
                        listView1.Columns.Add("Claim Date", 75, HorizontalAlignment.Left);//payDate
                        listView1.Columns.Add("Claim Device", 75, HorizontalAlignment.Left);//PayDevice
                        listView1.Columns.Add("Amount(" + ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + ")", 75, HorizontalAlignment.Right);//Amount
                        if (oBusiness.CheckViewTicketAccess())
                        {
                        listView1.Columns.Add("Voucher", 200, HorizontalAlignment.Left);//Ticket
                        }
                        else
                        {
                            listView1.Columns.Add("Voucher", 0, HorizontalAlignment.Left);//Ticket
                        }
                        listView1.Columns.Add("Status", 75, HorizontalAlignment.Left);//Status


                        LogManager.WriteLog("Inside CheckAccessForViewTicketNumber method with userID " + Program.nUserID.ToString(), LogManager.enumLogLevel.Info);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Error Occured.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogManager.WriteLog("InitializeListView() Error.", LogManager.enumLogLevel.Debug);
                        this.Close();
                        break;
                    }
            }



            LogManager.WriteLog("Column Definition Done.", LogManager.enumLogLevel.Info);

        }

        // Load Data from the DataSet into the ListView
        private void LoadList()
        {

            switch (_CODE)
            {
                case "DETAILS":
                    {

                        // Get the table from the data set
                        DataTable dtable = _DtDetails.Tables["DetailsView"];

                        // Clear the ListView control
                        listView1.Items.Clear();

                        // Display items in the ListView control
                        for (int i = 0; i < dtable.Rows.Count; i++)
                        {
                            DataRow drow = dtable.Rows[i];

                            // Only row that have not been deleted
                            if (drow.RowState != DataRowState.Deleted)
                            {
                                if (!oBusiness.CheckViewTicketAccess())
                                {
                                    if (drow["Ticket"] != null)
                                    {
                                        string strTicket = drow["Ticket"].ToString().Trim() ?? "";
                                        if (strTicket.Length > 8)
                                        {
                                            drow["Ticket"] = strTicket.Remove(strTicket.Length - 4, 4) + "****";
                                        }
                                    }
                                }
                                
                                // Define the list items .Trim().Remove(x.Ticket.Trim().Length - 4, 4) + "****"
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
                            MessageBox.Show("There are no records available for the selected criteria.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("There are no records available for the selected criteria.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("There are no records available for the selected criteria.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("There are no records available for the selected criteria.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                    
                                    if (!oBusiness.CheckViewTicketAccess())
                                        drow["Ticket"] = string.Empty;
                                    
                                    listView1.Items.Add(lvi);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("There are no records available for the selected criteria.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                                    if (!oBusiness.CheckViewTicketAccess())
                                    {
                                        drow["Ticket"] = string.Empty;
                                    }

                                    listView1.Items.Add(lvi);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("There are no records available for the selected criteria.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Error Occured.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogManager.WriteLog("Loadlist() Error.", LogManager.enumLogLevel.Debug);
                        this.Close();
                        break;
                    }

            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                switch (_CODE)
                {
                    case "DETAILS":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                
                                cReportViewer.showDetailedReport(_DtDetails, _FromDate, _ToDate, _SiteName,_FooterText);
                                cReportViewer.ShowDialog();
                            }
                            break;
                        }
                    case "ACTIVE":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                cReportViewer.showActiveReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Print, "");
                                //cReportViewer.ShowDialog();
                            }
                            break;
                        }
                    case "VOIDCANCEL":
                    case "EXPIRED":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                cReportViewer.showVOIDReport(_CODE, _DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Print, "");
                                //cReportViewer.ShowDialog();
                            }

                            break;
                        }
                    case "PROMO":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                cReportViewer.showPROMOReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Print, "");
                                //cReportViewer.ShowDialog();
                            }

                            break;
                        }
                    case "EXCEP":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                cReportViewer.showEXCEPReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Print, "");
                                //cReportViewer.ShowDialog();
                            }

                            break;
                        }
                    case "LIABILITY":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                cReportViewer.showLIABILITYReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Print, "");
                                //cReportViewer.ShowDialog();
                            }

                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Error Occured.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LogManager.WriteLog("btnPrint_Click() Error.", LogManager.enumLogLevel.Debug);
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


        internal DataSet LoadActiveTickets()
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

        internal DataSet LoadVoidCancelledTickets()
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

        internal DataSet LoadVoidExpiredTickets()
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

        internal DataSet LoadPromoTickets()
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

        internal DataSet LoadExceptions()
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

        internal DataSet LoadLiabilities()
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

        private void btnExport_Click(object sender, EventArgs e)
        {

            SaveFileDialog fileDialog = null;
            string filepath = string.Empty;

            fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Excel Files (*.xls)|*.xls|All Files (*.*) |*.*";
            fileDialog.ShowDialog();
            filepath = fileDialog.FileName;
            if (filepath != "")
            {
                switch (_CODE)
                {
                    case "ACTIVE":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                cReportViewer.showActiveReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Export, filepath);
                                cReportViewer.ShowDialog();
                            }
                            break;
                        }
                    case "VOIDCANCEL":
                    case "EXPIRED":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                cReportViewer.showVOIDReport(_CODE, _DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Export, filepath);
                                //cReportViewer.ShowDialog();
                            }

                            break;
                        }
                    case "PROMO":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                cReportViewer.showPROMOReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Export, filepath);
                                //cReportViewer.ShowDialog();
                            }

                            break;
                        }
                    case "EXCEP":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                cReportViewer.showEXCEPReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Export, filepath);
                                //cReportViewer.ShowDialog();
                            }

                            break;
                        }
                    case "LIABILITY":
                        {
                            using (ReportViewer cReportViewer = new ReportViewer())
                            {
                                cReportViewer.showLIABILITYReport(_DtDetails, _FromDate, _ToDate, _SiteName, ReportOptions.Export, filepath);
                                //cReportViewer.ShowDialog();
                            }

                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Error Occured.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LogManager.WriteLog("btnExport_Click() Error.", LogManager.enumLogLevel.Debug);
                            this.Close();
                            break;
                        }
                }

            }





        }


    }
}
