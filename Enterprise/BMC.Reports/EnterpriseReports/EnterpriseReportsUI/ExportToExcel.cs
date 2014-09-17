using BMC.EnterpriseReportsBusiness;
using BMC.EnterpriseReportsTransport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using BMC.CoreLib.Win32;
using BMC.Common.ExceptionManagement;
using BMC.Common;
using BMC.Reports;
namespace BMC.EnterpriseReportsUI
{
    public partial class ExportToExcel : Form
    {
        #region ExportReportDatatoExcel
        /// <summary>
        /// Exports Report data to excel.
        /// </summary>
        /// <param name="ListParams"></param>
        /// <param name="ProcedureName"></param>
        /// <param name="XMLName"></param>
        /// <param name="lstparams"></param>
        public void ExportDataToExcel(string ProcedureName, string XMLName, clsSPParams lstparams,DataSet dsReportData)
        {
            try
            {
                ReportsBusiness oReportBusiness = new ReportsBusiness();
                decimal CalcTotalNotes = 0, CalcTotalCoins = 0, CalcTotalCash = 0, CashDesk = 0;
               //DataSet dsReportData = null;
                object odsToExcel;


                //Reterives Win Comparison data from Database and filters data When sortorder=1
                if (ProcedureName.ToUpper() == "RSP_REPORT_WINCOMPARISON")
                {
                    bool isSuccess = true;
                    if(dsReportData==null)
                        dsReportData = oReportBusiness.GetWinComparisonReport(lstparams.Company, lstparams.SubCompany, lstparams.Zone, lstparams.Site, Convert.ToDateTime(lstparams.GamingDate), lstparams.IncludeNonCashable, lstparams.UsePhysicalWin, lstparams.Slot, lstparams.Period, lstparams.SiteIDList, ref isSuccess);
                    else
                    {
                        DataView dv_view = dsReportData.Tables[0].DefaultView;
                        dv_view.RowFilter = "SortOrder=1";
                        dsReportData.Tables.Clear();
                        dsReportData.Tables.Add(dv_view.ToTable());
                    }
                }

                //Reterives Accounintg Machine Win Loss data from Database
                if (ProcedureName.ToUpper() == "RSP_REPORT_BANKINGREPORT" && dsReportData==null)
                    dsReportData = oReportBusiness.ExecuteDataSet(ProcedureName, lstparams);



                //Load ColumnName from XML to DataTable.
                if (dsReportData.Tables[0].Rows.Count != 0)
                {
                    DataTable dtCalculateddata = new DataTable();
                    XmlDocument xdoc = new XmlDocument();
                    DataColumn dtColumn = null;
                    xdoc.Load(XMLName);
                    XmlNodeList XLNodelist = xdoc.SelectNodes("//add");
                    foreach (XmlNode xNode in XLNodelist)
                    {
                        dtColumn = new DataColumn();
                        dtColumn.Caption = xNode.Attributes["DisplayName"].Value;
                        dtColumn.DataType = Type.GetType(xNode.Attributes["type"].Value);
                        dtColumn.ColumnName = xNode.Attributes["key"].Value;
                        dtCalculateddata.Columns.Add(dtColumn);
                    }


                    DataTable dtTable = dsReportData.Tables[0];


                    //Map New Datatable column name with database Column values.
                    foreach (DataRow dtRowOriginal in dtTable.Rows)
                    {
                        DataRow drCalculated = dtCalculateddata.NewRow();

                        foreach (DataColumn dtColumnName in dtCalculateddata.Columns)
                        {
                            string sColumnName = dtColumnName.ToString();
                            if (dtRowOriginal.Table.Columns.Contains(sColumnName))
                            {
                                if (dtColumnName.DataType.ToString() == "System.Decimal")
                                    drCalculated[dtColumnName] = Math.Round(Convert.ToDecimal(dtRowOriginal[sColumnName]), 2);//.ToString("0.00");
                                else
                                    drCalculated[dtColumnName] = dtRowOriginal[sColumnName];
                            }
                        }

                        //Applying report calculation over raw data.

                        if (ProcedureName.ToUpper() == "RSP_REPORT_BANKINGREPORT")
                        {
                            CalcTotalNotes = Convert.ToDecimal(dtRowOriginal["Cash_Collected_10000p"]) + Convert.ToDecimal(dtRowOriginal["Cash_Collected_5000P"]) + Convert.ToDecimal(dtRowOriginal["Cash_Collected_2000P"]) + Convert.ToDecimal(dtRowOriginal["Cash_Collected_1000P"]) + Convert.ToDecimal(dtRowOriginal["Cash_Collected_500P"]) + Convert.ToDecimal(dtRowOriginal["Cash_collected_100p"]);
                            CalcTotalCoins = (Convert.ToDecimal(dtRowOriginal["CashCollected"]) - (CalcTotalNotes + Convert.ToDecimal(dtRowOriginal["Declared_Total_Tickets"])));
                            CalcTotalCash = CalcTotalNotes + CalcTotalCoins;
                            CashDesk = Convert.ToDecimal(dtRowOriginal["Refunds"]) + Convert.ToDecimal(dtRowOriginal["ManualRefills"]);
                            drCalculated["CalcTotalNotes"] = CalcTotalNotes;
                            drCalculated["CalcTotalCoins"] = CalcTotalCoins;
                            drCalculated["CalcTotalCash"] = CalcTotalCash;
                            drCalculated["CashDesk"] = CashDesk;
                        }
                        dtCalculateddata.Rows.Add(drCalculated);
                    }

                    odsToExcel = dtCalculateddata;


                    //Grouping and summing data based on Site_Name,Zone_Name,Machine_Type_Code,Position_Name
                    if (ProcedureName.ToUpper() == "RSP_REPORT_BANKINGREPORT")
                    {
                        odsToExcel = (from grp in dtCalculateddata.AsEnumerable()
                                      group grp by new
                                      {

                                          Site_Name = grp["Site_Name"],
                                          Zone_Name = grp["Zone_Name"],
                                          Machine_Type_Code = grp["Machine_Type_Code"],
                                          PosName = grp["PosName"]
                                      } into GroupedData
                                      select new
                                      {
                                          PosName = GroupedData.Key.PosName,
                                          MachineName = GroupedData.Select(o => o.Field<string>("MachineName")).First(),
                                          Asset = GroupedData.Select(o => o.Field<string>("machine_stock_no")).First(),
                                         CoinsIn =Math.Round( GroupedData.Sum(o => o.Field<decimal>("CalcTotalCoins")),2),                                         
                                          Bills = Math.Round(GroupedData.Sum(o => o.Field<decimal>("CalcTotalNotes")),2),
                                          VouchersIn = Math.Round(GroupedData.Sum(o => o.Field<decimal>("Declared_Tickets")),2),
                                          EftIn = Math.Round(GroupedData.Sum(o => o.Field<decimal>("EftIn")),2),
                                          PromoCashableIn = Math.Round(GroupedData.Sum(o => o.Field<decimal>("PromoCashAmount")),2),
                                          PromoNonCashableIn = Math.Round(GroupedData.Sum(o => o.Field<decimal>("PromoNonCashAmount")),2),
                                          CoinsOut = Math.Round(GroupedData.Sum(o => o.Field<decimal>("CoinsOut")),2),
                                          VouchersOut = Math.Round(GroupedData.Sum(o => o.Field<decimal>("Tickets_Printed")),2),
                                          AttendantPay =Math.Round( GroupedData.Sum(o => o.Field<decimal>("dechandpay")),2),
                                          Shortpay =Math.Round( GroupedData.Sum(o => o.Field<decimal>("Shortpay")),2),
                                          ManualAttendantPay = Math.Round(GroupedData.Sum(o => o.Field<decimal>("ManAttPay")),2),
                                          MachinePaidAttendantPay =Math.Round( GroupedData.Sum(o => o.Field<decimal>("MacAttPay")),2),
                                          ManualJackpot =Math.Round( GroupedData.Sum(o => o.Field<decimal>("ManJackpot")),2),
                                          MachinePaidJackpot =Math.Round( GroupedData.Sum(o => o.Field<decimal>("MacJackpot")),2),
                                          ManualProgressive =Math.Round (GroupedData.Sum(o => o.Field<decimal>("ManProgressive")),2),
                                          MachinePaidProgressive =Math.Round( GroupedData.Sum(o => o.Field<decimal>("MacProgressive")),2),
                                          EFTOut = Math.Round(GroupedData.Sum(o => o.Field<decimal>("EFTOut")),2),
                                          TotalCash = Math.Round (GroupedData.Sum(o => o.Field<decimal>("CalcTotalCash")),2),
                                          CashDesh = Math.Round (GroupedData.Sum(o => o.Field<decimal>("CashDesk")),2),
                                          WinLoss =Math.Round (GroupedData.Sum(o => o.Field<decimal>("Cash_Take")),2),
                                          Variance = Math.Round(GroupedData.Sum(o => o.Field<decimal>("Take_Var")), 2)
                                      }).ToList();
                    }
                    Win32Extensions.ExportControlDataToExcel<object>(this, odsToExcel, null, true, false, true);
                }
                else
                {
                    this.ShowMessageBox(this.GetResourceTextByKey(1, "MSG_REP_NO_DATA"), this.GetResourceTextByKey(1, "MSG_REP_TITLE"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion ExportReportDatatoExcel
    }
}
