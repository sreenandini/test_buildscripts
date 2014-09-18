using System;
using System.Drawing;
using BMC.Common.Utilities;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Web;
using System.Drawing.Printing;
using System.Windows.Controls;
using BMC.Transport;
using System.Reflection;
using BMC.Common.ConfigurationManagement;
using System.Globalization;
using BMC.Common.LogManagement;
using System.Data;
using System.Windows.Media;
using System.Windows;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using BMC.Common.ExceptionManagement;

using BMC.DBInterface.CashDeskOperator;
using Microsoft.Win32;// To access Registry
using System.Text.RegularExpressions;



namespace BMC.Business.CashDeskOperator
{
    public class PrintUtility
    {
        #region Declarations

        private string filename = "";
        private DateTime StartDate = DateTime.Now;
        private DateTime EndDate = DateTime.Now;
        private StreamWriter writer = null;
        string filepath;
        FileStream outputfile = null;
        

        #endregion Declarations

        #region Public Functions

        public void PrintFunction(System.Windows.Controls.ListView lvView, DateTime StartDate, DateTime EndDate)
        {
            Font printFont = new Font("Courier New", 12);
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            if (GenerateHTMLCode(lvView))
            {
                if (File.Exists(filepath))
                {
                    IESetupHeader();
                    System.Windows.Forms.WebBrowser webBrowse = new System.Windows.Forms.WebBrowser();
                    webBrowse.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(PrintDoc);
                    //webBrowse.Show();
                    webBrowse.Navigate(filepath);

                }
            }
        }

      

        public void PrintFunction(System.Windows.Controls.ListView lvView,DateTime StartDate,DateTime EndDate, bool isPrintDate, bool isTransactionType, bool isZone,
            bool  isPos,bool  isMachine,bool  isAsset,bool  isAmount,bool  isTicketPrintedDate,bool  isDetails,string screenName)
        {
            Font printFont = new Font("Courier New", 12);
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            if (GenerateHTMLCode_1(lvView,  isPrintDate,  isTransactionType,  isZone,   isPos,  isMachine,  isAsset,  isAmount,  isTicketPrintedDate,  isDetails, screenName))
            {
                if (File.Exists(filepath))
                {
                    IESetupHeader();
                    System.Windows.Forms.WebBrowser webBrowse = new System.Windows.Forms.WebBrowser();
                    webBrowse.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(PrintDoc);
                    //webBrowse.Show();
                    webBrowse.Navigate(filepath);
                   
                }
            }
        }

        public void PrintFunction(System.Windows.Controls.ListView lvView, string ReportName,DateTime ReportStartdate)
        {
            try
            {
                Font printFont = new Font("Courier New", 12);
                if (GenerateHTMLCode(lvView,ReportName,ReportStartdate))
                {
                    if (File.Exists(filepath))
                    {
                       
                        System.Windows.Forms.WebBrowser webBrowse = new System.Windows.Forms.WebBrowser();
                        webBrowse.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(PrintDoc);
                        webBrowse.Navigate(filepath);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception printfunction = " + ex.Message + ":" + ex.Source + ":" + ex.StackTrace, LogManager.enumLogLevel.Error);
            }

        }

         public void PrintMeterFunction(System.Windows.Controls.ListView lvView,string strSiteName,string strAsset,string strPostion,string strSerialNo)
        {
            Font printFont = new Font("Courier New", 12);          
            
            if (GenerateHTMLCode_2(lvView,strSiteName,strAsset,strPostion,strSerialNo))
            {
                if (File.Exists(filepath))
                {
                    IESetupHeader();
                    System.Windows.Forms.WebBrowser webBrowse = new System.Windows.Forms.WebBrowser();
                    webBrowse.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(PrintDoc);
                    //webBrowse.Show();
                    webBrowse.Navigate(filepath);
                }
            }
        }

        #endregion Public Functions

        #region Private Functions

        private void PrintDoc(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                ((System.Windows.Forms.WebBrowser)sender).ShowPrintDialog();                
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                ((System.Windows.Forms.WebBrowser)sender).Dispose();
            }
            catch (Exception ex)
            {
                
            }
        }

        public void IESetupHeader()
        {
            string strKey = "Software\\Microsoft\\Internet Explorer\\PageSetup";
            bool bolWritable = true;
            string strName = "header";
            object oValue = "&b&p of &P";
            RegistryKey oKey = Registry.CurrentUser.OpenSubKey(strKey, bolWritable);
            oKey.SetValue(strName, oValue);
            oKey.Close();
        }



        private bool GenerateHTMLCode(System.Windows.Controls.ListView lvView)
        {
            try
            {

                if (lvView.Items.Count > 0)
                {
                    GridView gridview = lvView.View as GridView;
                    string sColCount = gridview.Columns.Count.ToString();

                    filename = "Print.HTML";
                    filepath = System.Windows.Forms.Application.StartupPath + "\\" + filename;
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }                    
                    outputfile = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(outputfile);
                    writer.BaseStream.Seek(0, SeekOrigin.End);
                    DoWrite("<!DOCTYPE HTML PUBLIC " + "-" + "//w3c//DTD HTML 3.2//EN" + " >" + "");
                    DoWrite("<HTML>");
                    DoWrite("<HEAD>");
                    DoWrite("<style type='text/css'>@media print {thead {display: table-header-group;} tfoot { display: table-footer-group; } }</style>");
                    DoWrite("</HEAD>");
                    DoWrite("<BODY >");
                    DoWrite("<font face='Arial' size=9>");
                    string dynTableHeader = "";

                    dynTableHeader="<table border='0' cellspacing='15' width=100% height=100%>";
                    dynTableHeader += "<thead>";
                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='3'><b>Treasury Details Report</b></font></th>";
                    dynTableHeader += "</tr>";
                    
                    dynTableHeader += "<tr>";
                    string sVersion = CommonDataAccess.GetVersion();
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='3'><b> BMC Version:" + sVersion + "</b></font></th>";
                    dynTableHeader += "</tr>";

                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'><b>Printed on: " 
                                            + DateTime.Now.GetUniversalDateTimeFormat() + "</b></font></th>";
                    dynTableHeader += "</tr>";

                                    
                    //dynTableHeader += "<tr>";
                    //dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'>";
                    //dynTableHeader += "<b>Start Date : </b>" + StartDate.GetUniversalDateFormat() + "</font></th>";
                    ////dynTableHeader += "</tr>";
                    
                    ////dynTableHeader += "<tr>";
                    //dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'>";
                    //dynTableHeader += "<b>&nbsp;End Date: </b>" + EndDate.GetUniversalDateFormat() + "</font></th>";
                    //dynTableHeader += "</tr>";

                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'>";
                    dynTableHeader += "<b>Start Date : </b>" + StartDate.GetUniversalDateFormat() + "<b>&nbsp;End Date: </b>" 
                                        + EndDate.GetUniversalDateFormat() +"</font></th>";
                    dynTableHeader += "</tr>";

                    

                    dynTableHeader += "<tr><th colspan="+sColCount+"><hr/></th></tr>";
                                                
                    //dynTableHeader += "<table cellspacing='0' height='40%' cellpadding='0' border='1' style='border-collapse:collapse;border-style:solid;border-width:2px;border-color:gray;'>";
                    
                    dynTableHeader += "<tr>";
              

                    
                    foreach (GridViewColumn col in gridview.Columns)
                    {
                        dynTableHeader += "<th align='left'><font face='Arial' size='2'> <b>" + col.Header.ToString();
                        dynTableHeader += "</b></font></th>";
                    }
                    dynTableHeader += "</tr>";
                    dynTableHeader += "<tr><th colspan=" + sColCount + "><hr/></th></tr>";

                    dynTableHeader += "</thead>";
                    dynTableHeader += "</font>";
                    dynTableHeader += "<tbody>";

                    CashDeskmanagerCommon objcommon = new CashDeskmanagerCommon();
                    int colcount = gridview.Columns.Count;

                    int nCount = 0;
                    string sTr="";
                    string sFirstRow = "";
                    var ItemSource = (List<TicketExceptions>)lvView.ItemsSource;
                    foreach (var item in lvView.Items)
                    {
                                              
                        //dynTableHeader += "<tr>";
                        sTr += "<tr>";

                        System.Windows.Controls.ListViewItem item1 = lvView.ItemContainerGenerator.ContainerFromIndex(lvView.Items.IndexOf(item))
                            as System.Windows.Controls.ListViewItem;
                       

                        //System.Windows.Controls.ListViewItem item1 = lvView.ItemContainerGenerator.ContainerFromItem(lvView.Items[nLoop])
                        //    as System.Windows.Controls.ListViewItem;


                        GridViewRowPresenter rowPresenter =
                           objcommon.GetFrameworkElementByName<GridViewRowPresenter>(item1);

                        for (int col = 0; col < colcount; col++)
                        {
                            if (rowPresenter != null)
                            {
                                ContentPresenter templatedParent = System.Windows.Media.VisualTreeHelper.GetChild(rowPresenter, col) as ContentPresenter;
                                foreach (PropertyInfo info in item.GetType().GetProperties())
                                {
                                    TextBlock block = (TextBlock)gridview.Columns[col].CellTemplate.FindName(info.Name, templatedParent);                                    

                                    if (block != null)
                                    {

                                        //if (gridview.Columns[col].Header.ToString().ToUpper() == block.Tag.ToString().ToUpper())
                                        //{

                                            if (IsNumber(block.Text))
                                            {
                                                // dynTableHeader += "<td align='right'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";

                                                if (nCount != 0)
                                                    sTr += "<td align='right'><font face='Arial' size='2'> " + block.Text + "</font></td>";
                                                else
                                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" + block.Text + "</b>" + "</font></td>";
                                            }
                                            else
                                            {
                                                if (nCount != 0)
                                                    sTr += "<td align='left'><font face='Arial' size='2'> " + block.Text + "</font></td>";
                                                else
                                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + block.Text + "</b>" + "</font></td>";
                                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                                            }
                                            break;
                                        //}
                                    }


                                }
                            }
                        }
                        //dynTableHeader += "</tr>";
                        sTr += "</tr>";
                        dynTableHeader += sTr;

                        if (nCount == 0)
                        {
                            sFirstRow = sTr;
                            nCount++;
                        }
                        sTr = "";


                    }
                    dynTableHeader += sFirstRow;
                    dynTableHeader += "</tbody>";

                    dynTableHeader += "<tfoot><tr><td colspan=" + sColCount + "><hr/></td></tr>";
                    dynTableHeader += "<tr><td colspan=" + sColCount + " align='center' style='padding: 0in;'><span style='font-size: 7.5pt; color: rgb(102, 102, 102); font-family:verdana'> This content is confidential, subject to contract and copyright, and may be privileged. </span></td>";   
                     dynTableHeader +=  "</tfoot>";
                    dynTableHeader += "</table>";
                    DoWrite(dynTableHeader);
                    DoWrite("</BODY>");
                    DoWrite("</HTML>");
                    writer.Close();
                }
            }
            catch (Exception ex)
            {                
                LogManager.WriteLog("Exception GenerateCode = " + ex.Message + ":" + ex.Source + ":" + ex.StackTrace, LogManager.enumLogLevel.Error);
                outputfile = null;
                writer = null;
                return false;
            }
            return true;
        }


        private bool GenerateHTMLCode_2(System.Windows.Controls.ListView lvView, string strSiteName, string strAsset, string strPostion, string strSerialNo)
        {


            DataView dtSource;
            DataTable dtTable = new DataTable();
            try
            {

                if (lvView.Items.Count > 0)
                {
                    
                    filename = "Print.HTML";
                    filepath = System.Windows.Forms.Application.StartupPath + "\\" + filename;
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                    outputfile = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(outputfile);
                    writer.BaseStream.Seek(0, SeekOrigin.End);
                    DoWrite("<!DOCTYPE HTML PUBLIC " + "-" + "//w3c//DTD HTML 3.2//EN" + " >" + "");
                    DoWrite("<HTML>");
                    DoWrite("<HEAD>");
                    DoWrite("<style type='text/css'>@media print {thead {display: table-header-group;} tfoot { display: table-footer-group; } }</style>");
                    DoWrite("</HEAD>");
                    DoWrite("<BODY >");
                    DoWrite("<font face='Arial' size=9>");
                    string dynTableHeader = "";

                    dynTableHeader = "<table border='0' cellspacing='12' width=100% height=100%>";
                    dynTableHeader += "<thead>";
                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='4'><b>Machine Current Meters</b></font></th>";
                    dynTableHeader += "</tr>";

                    dynTableHeader += "<tr>";
                    string sVersion = CommonDataAccess.GetVersion();
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='3'><b> BMC Version:" + sVersion + "</b></font></th>";
                    dynTableHeader += "</tr>";

                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'><b>Printed on: "
                                            + DateTime.Now.GetUniversalDateTimeFormat() + "</b></font></th>";
                    dynTableHeader += "</tr>";


                    //dynTableHeader += "<tr>";
                    //dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'>";
                    //dynTableHeader += "<b>Start Date : </b>" + StartDate.GetUniversalDateFormat() + "</font></th>";
                    ////dynTableHeader += "</tr>";

                    ////dynTableHeader += "<tr>";
                    //dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'>";
                    //dynTableHeader += "<b>&nbsp;End Date: </b>" + EndDate.GetUniversalDateFormat() + "</font></th>";
                    //dynTableHeader += "</tr>";


                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='left' colspan=10><font face='Arial' size='2'>";
                    dynTableHeader += "<b>Site Name : </b>" + strSiteName + "</font></th>";
                    dynTableHeader += "</tr>";


                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='left' colspan=10><font face='Arial' size='2'>";
                    dynTableHeader += "<b>Position  : </b>" + strPostion + "</font></th>";
                    dynTableHeader += "</tr>";


                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='left' colspan=10><font face='Arial' size='2'>";
                    dynTableHeader += "<b>Asset No : </b>" + strAsset + "</font></th>";
                    dynTableHeader += "</tr>";


                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='left' colspan=10><font face='Arial' size='2'>";
                    dynTableHeader += "<b>Serial No : </b>" + strSerialNo + "</font></th>";
                    dynTableHeader += "</tr>";

                    GridView gridview = lvView.View as GridView;
                    string sColCount = gridview.Columns.Count.ToString();
                    foreach (GridViewColumn col in gridview.Columns)
                    {
                        dynTableHeader += "<th align='left'><font face='Arial' size='2'> <b>" + col.Header.ToString();
                        dynTableHeader += "</b></font></th>";
                    }
                    dynTableHeader += "</tr>";
                    dynTableHeader += "<tr><th colspan=" + sColCount + "><hr/></th></tr>";

                    dynTableHeader += "</thead>";
                    dynTableHeader += "</font>";
                    dynTableHeader += "<tbody>";

                    int colcount = gridview.Columns.Count;

                    if (FindType(lvView.ItemsSource) != null)
                    {
                        int iType = Convert.ToInt32(FindType(lvView.ItemsSource));
                        if (iType == 1)
                        {
                            dtSource = (DataView)lvView.ItemsSource;
                            dtTable = (DataTable)dtSource.Table;
                        }
                        else if (iType == 2)
                        {
                            dtTable = (DataTable)lvView.ItemsSource;
                        }
                        else
                        {
                            LogManager.WriteLog("List View Source is different", LogManager.enumLogLevel.Debug);
                            return false;
                        }

                    }

                    foreach (DataRowView row in lvView.Items)
                    {
                        dynTableHeader += "<tr>";
                        System.Windows.Controls.ListViewItem item1 = lvView.ItemContainerGenerator.ContainerFromIndex(lvView.Items.IndexOf(row))
                            as System.Windows.Controls.ListViewItem;

                        if (item1 != null)
                        {

                            GridViewRowPresenter rowPresenter = GetFrameworkElementByName<GridViewRowPresenter>(item1);

                            for (int col = 0; col < colcount; col++)
                            {
                                if (rowPresenter != null)
                                {
                                    ContentPresenter templatedParent = System.Windows.Media.VisualTreeHelper.GetChild(rowPresenter, col) as ContentPresenter;

                                    foreach (GridViewColumn cols in gridview.Columns)
                                    {
                                        TextBlock block = (TextBlock)gridview.Columns[col].CellTemplate.FindName(cols.Header.ToString(), templatedParent);
                                        if (block != null)
                                        {
                                            if (gridview.Columns[col].Header.ToString().ToUpper() == block.Name.ToString().ToUpper())
                                            {
                                               
                                                dynTableHeader += "<td align='left'><font face='Arial' size='1'> " + block.Text + "</font></td>";
                                                break;
                                            }
                                        }
                                        //dynTableHeader += "</td>";
                                    }

                                }
                            }
                            dynTableHeader += "</tr>";
                        }

                    }
                    
                    dynTableHeader += "</tbody>";
                    dynTableHeader += "<tfoot><tr><td colspan=" + colcount.ToString() + "><hr/></td></tr>";
                    dynTableHeader += "<tr><td colspan=" + colcount.ToString() + " align='center' style='padding: 0in;'><span style='font-size: 7.5pt; color: rgb(102, 102, 102); font-family:verdana'> This content is confidential, subject to contract and copyright, and may be privileged. </span></td>";
                    dynTableHeader += "</tfoot>";
                    dynTableHeader += "</table>";
                    DoWrite(dynTableHeader);                     
                    DoWrite("<BODY>");
                    DoWrite("</HTML>");
                    writer.Close();
                    
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception GenerateHTMLCode = " + ex.Message + ":" + ex.Source + ":" + ex.StackTrace, LogManager.enumLogLevel.Error);
                outputfile = null;
                writer = null;
                return false;
            }
            return true;































            #region Old Code
            //try
            //{
            //    if (lvView.Items.Count > 0)
            //    {
            //        GridView gridview = lvView.View as GridView;
            //        string sColCount = gridview.Columns.Count.ToString();

            //        filename = "Print.HTML";
            //        filepath = System.Windows.Forms.Application.StartupPath + "\\" + filename;
            //        if (File.Exists(filepath))
            //        {
            //            File.Delete(filepath);
            //        }
            //        outputfile = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            //        writer = new StreamWriter(outputfile);
            //        writer.BaseStream.Seek(0, SeekOrigin.End);
            //        DoWrite("<!DOCTYPE HTML PUBLIC " + "-" + "//w3c//DTD HTML 3.2//EN" + " >" + "");
            //        DoWrite("<HTML>");
            //        DoWrite("<HEAD>");
            //        DoWrite("<style type='text/css'>@media print {thead {display: table-header-group;} tfoot { display: table-footer-group; } }</style>");
            //        DoWrite("</HEAD>");
            //        DoWrite("<BODY >");
            //        DoWrite("<font face='Arial' size=9>");
            //        string dynTableHeader = "";

            //        dynTableHeader = "<table border='0' cellspacing='15' width=100% height=100%>";
            //        dynTableHeader += "<thead>";
            //        dynTableHeader += "<tr>";
            //        dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='3'><b>Machine Current Meters</b></font></th>";
            //        dynTableHeader += "</tr>";

            //        dynTableHeader += "<tr>";
            //        string sVersion = CommonDataAccess.GetVersion();
            //        dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='3'><b> BMC Version:" + sVersion + "</b></font></th>";
            //        dynTableHeader += "</tr>";

            //        dynTableHeader += "<tr>";
            //        dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'><b>Printed on: "
            //                                + DateTime.Now.GetUniversalDateTimeFormat() + "</b></font></th>";
            //        dynTableHeader += "</tr>";


            //        //dynTableHeader += "<tr>";
            //        //dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'>";
            //        //dynTableHeader += "<b>Start Date : </b>" + StartDate.GetUniversalDateFormat() + "</font></th>";
            //        ////dynTableHeader += "</tr>";

            //        ////dynTableHeader += "<tr>";
            //        //dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'>";
            //        //dynTableHeader += "<b>&nbsp;End Date: </b>" + EndDate.GetUniversalDateFormat() + "</font></th>";
            //        //dynTableHeader += "</tr>";


            //        dynTableHeader += "<tr>";
            //        dynTableHeader += "<th align='left' colspan=10><font face='Arial' size='2'>";
            //        dynTableHeader += "<b>Site Name : </b>" + strSiteName + "</font></th>";
            //        dynTableHeader += "</tr>";


            //        dynTableHeader += "<tr>";
            //        dynTableHeader += "<th align='left' colspan=10><font face='Arial' size='2'>";
            //        dynTableHeader += "<b>Position : </b>" + strPostion + "</font></th>";
            //        dynTableHeader += "</tr>";


            //        dynTableHeader += "<tr>";
            //        dynTableHeader += "<th align='left' colspan=10><font face='Arial' size='2'>";
            //        dynTableHeader += "<b>Asset No : </b>" + strAsset + "</font></th>";
            //        dynTableHeader += "</tr>";


            //        dynTableHeader += "<tr>";
            //        dynTableHeader += "<th align='left' colspan=10><font face='Arial' size='2'>";
            //        dynTableHeader += "<b>Serial No : </b>" + strSerialNo  + "</font></th>";
            //        dynTableHeader += "</tr>";

            //        dynTableHeader += "<tr><th colspan=" + sColCount + "><hr/></th></tr>";

            //        //dynTableHeader += "<table cellspacing='0' height='40%' cellpadding='0' border='1' style='border-collapse:collapse;border-style:solid;border-width:2px;border-color:gray;'>";

            //        dynTableHeader += "<tr>";



            //        foreach (GridViewColumn col in gridview.Columns)
            //        {
            //            dynTableHeader += "<th align='left'><font face='Arial' size='2'> <b>" + col.Header.ToString();
            //            dynTableHeader += "</b></font></th>";
            //        }
            //        dynTableHeader += "</tr>";
            //        dynTableHeader += "<tr><th colspan=" + sColCount + "><hr/></th></tr>";

            //        dynTableHeader += "</thead>";
            //        dynTableHeader += "</font>";
            //        dynTableHeader += "<tbody>";

            //        CashDeskmanagerCommon objcommon = new CashDeskmanagerCommon();
            //        int colcount = gridview.Columns.Count;

            //        int nCount = 0;
            //        string sTr = "";
            //        string sFirstRow = "";
            //        var ItemSource = lvView.ItemsSource;
            //        foreach (DataRowView row in lvView.Items)
            //        {
            //            dynTableHeader += "<tr>";
            //            System.Windows.Controls.ListViewItem item1 = lvView.ItemContainerGenerator.ContainerFromIndex(lvView.Items.IndexOf(row))
            //                as System.Windows.Controls.ListViewItem;


            //            GridViewRowPresenter rowPresenter = GetFrameworkElementByName<GridViewRowPresenter>(item1);

            //            for (int col = 0; col < colcount; col++)
            //            {
            //                if (rowPresenter != null)
            //                {
            //                    ContentPresenter templatedParent = System.Windows.Media.VisualTreeHelper.GetChild(rowPresenter, col) as ContentPresenter;

            //                    foreach (GridViewColumn cols in gridview.Columns)
            //                    {                                    
            //                        TextBlock block = (TextBlock)gridview.Columns[col].CellTemplate.FindName(cols.Header.ToString(), templatedParent);
            //                        if (block != null)
            //                        {
            //                            if (gridview.Columns[col].Header.ToString().ToUpper() == block.Name.ToString().ToUpper())
            //                            {
            //                                dynTableHeader += "<td> " + block.Text;
            //                                break;
            //                            }
            //                        }
            //                        dynTableHeader += "</td>";
            //                    }
                               

            //                }
            //            }
            //            dynTableHeader += "</tr>";
            //        }


            //        //    //dynTableHeader += "</tr>";
            //        //    sTr += "</tr>";
            //        //    dynTableHeader += sTr;

            //        //    if (nCount == 0)
            //        //    {
            //        //        sFirstRow = sTr;
            //        //        nCount++;
            //        //    }
            //        //    sTr = "";


            //        //}
            //        dynTableHeader += sFirstRow;
            //        dynTableHeader += "</tbody>";

            //        dynTableHeader += "<tfoot><tr><td colspan=" + sColCount + "><hr/></td></tr>";
            //        dynTableHeader += "<tr><td colspan=" + sColCount + " align='center' style='padding: 0in;'><span style='font-size: 7.5pt; color: rgb(102, 102, 102); font-family:verdana'> This content is confidential, subject to contract and copyright, and may be privileged. </span></td>";
            //        dynTableHeader += "</tfoot>";
            //        dynTableHeader += "</table>";
            //        DoWrite(dynTableHeader);
            //        DoWrite("</BODY>");
            //        DoWrite("</HTML>");
            //        writer.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("Exception GenerateCode = " + ex.Message + ":" + ex.Source + ":" + ex.StackTrace, LogManager.enumLogLevel.Error);
            //    outputfile = null;
            //    writer = null;
            //    return false;
            //}
            //return true;
            #endregion
        }



        public object Price(string value)
        {
            decimal price = 0;
            if (value != null &&
                value != String.Empty &&
                decimal.TryParse(value, out price))
            {
                price = System.Convert.ToDecimal(value);
            }
            return price.GetUniversalCurrencyFormatWithSymbol();
        }


        private bool GenerateHTMLCode_1(System.Windows.Controls.ListView lvView, bool isPrintDate, bool isTransactionType, bool isZone,
            bool  isPos,bool  isMachine,bool  isAsset,bool  isAmount,bool  isTicketPrintedDate,bool  isDetails,string screenName)

        {
            try
            {
                System.Globalization.DateTimeFormatInfo cultureInfo = new System.Globalization.CultureInfo(ExtensionMethods.CurrentDateCulture, false).DateTimeFormat;
               // line = "Date/Time".PadRight(number1 + 5, ctab) + System.DateTime.Now.ToString(cultureInfo.ShortDatePattern) + " " + System.DateTime.Now.ToString(cultureInfo.ShortTimePattern);

                if (lvView.Items.Count > 0)
                {
                    GridView gridview = lvView.View as GridView;
                    string sColCount = gridview.Columns.Count.ToString();

                    filename = "Print.HTML";
                    filepath = System.Windows.Forms.Application.StartupPath + "\\" + filename;
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                    outputfile = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(outputfile);
                    writer.BaseStream.Seek(0, SeekOrigin.End);
                    DoWrite("<!DOCTYPE HTML PUBLIC " + "-" + "//w3c//DTD HTML 3.2//EN" + " >" + "");
                    DoWrite("<HTML>");
                    DoWrite("<HEAD>");
                    DoWrite("<style type='text/css'>@media print {thead {display: table-header-group;} tfoot { display: table-footer-group; } }</style>");
                    DoWrite("</HEAD>");
                    DoWrite("<BODY >");
                    DoWrite("<font face='Arial' size=9>");
                    string dynTableHeader = "";

                    dynTableHeader = "<table border='0' cellspacing='15' width=100% height=100%>";
                    dynTableHeader += "<thead>";
                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='3'><b>Voucher Report</b></font></th>";
                    dynTableHeader += "</tr>";

                    dynTableHeader += "<tr>";
                    string sVersion = CommonDataAccess.GetVersion();
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='3'><b> BMC Version:" + sVersion + "</b></font></th>";
                    dynTableHeader += "</tr>";

                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'><b>Printed on: "
                                            + DateTime.Now.ToString(cultureInfo.ShortDatePattern) + "</b></font></th>";
                    dynTableHeader += "</tr>";

                    dynTableHeader += "<tr>";
                    dynTableHeader += "<th align='center' colspan=10><font face='Arial' size='2'>";
                    dynTableHeader += "<b>Start Date : </b>" + StartDate.ToString(cultureInfo.ShortDatePattern) + "<b>&nbsp;End Date: </b>"
                                        + EndDate.ToString(cultureInfo.ShortDatePattern) + "</font></th>";
                    dynTableHeader += "</tr>";



                    dynTableHeader += "<tr><th colspan=" + sColCount + "><hr/></th></tr>";

                    dynTableHeader += "<tr>";



                    foreach (GridViewColumn col in gridview.Columns)
                    {
                        dynTableHeader += "<th align='left'><font face='Arial' size='2'> <b>" + col.Header.ToString();
                        dynTableHeader += "</b></font></th>";
                    }
                    dynTableHeader += "</tr>";
                    dynTableHeader += "<tr><th colspan=" + sColCount + "><hr/></th></tr>";

                    dynTableHeader += "</thead>";
                    dynTableHeader += "</font>";
                    dynTableHeader += "<tbody>";

                    CashDeskmanagerCommon objcommon = new CashDeskmanagerCommon();
                    int colcount = gridview.Columns.Count;

                    int nCount = 0;
                    string sTr = "";
                    string sFirstRow = "";
                    string sTemp;

                    var ItemSource = (List<TicketExceptions>)lvView.ItemsSource;

                    for (int nLoop = 0; nLoop < ItemSource.Count;nLoop++ )
                    {

                        
                        sTr += "<tr>";



                        if (isPrintDate)
                        {
                            sTemp = !string.IsNullOrEmpty(ItemSource[nLoop].PrintDate) ? ItemSource[nLoop].PrintDate : string.Empty;
                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" +sTemp + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + sTemp+ "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }

                        }


                        if (isTransactionType)
                        {
                            sTemp = !string.IsNullOrEmpty(ItemSource[nLoop].TransactionType) ? ItemSource[nLoop].TransactionType : string.Empty;
                                
                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }

                        }


                        if (isZone)
                        {
                            sTemp = !string.IsNullOrEmpty(ItemSource[nLoop].Zone) ? ItemSource[nLoop].Zone : string.Empty;
                                

                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }

                        }


                        if (isPos)
                        {
                            sTemp = !string.IsNullOrEmpty(ItemSource[nLoop].Machine) ? ItemSource[nLoop].Machine : string.Empty;
                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }

                        }


                        if (isMachine && screenName == "PROMO")
                        {
                            sTemp = !string.IsNullOrEmpty(ItemSource[nLoop].Zone) ? ItemSource[nLoop].Zone : string.Empty;
                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }

                        }
                        else
                        {
                            sTemp = !string.IsNullOrEmpty(ItemSource[nLoop].SEGM) ? ItemSource[nLoop].SEGM : string.Empty;
                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }
                        }



                        if (isAsset)
                        {
                            sTemp = !string.IsNullOrEmpty(ItemSource[nLoop].SEGM) ? ItemSource[nLoop].SEGM : string.Empty;
                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }

                        }

                        if (isAmount & (screenName == "ACTIVE" || screenName == "PROMO"))
                        {
                            sTemp = !string.IsNullOrEmpty( Convert.ToString(ItemSource[nLoop].Value)) ? ItemSource[nLoop].Value.ToString() : string.Empty;
                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + Price(sTemp) + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" +Price(sTemp) + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + Price(sTemp) + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + Price(sTemp)+ "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }

                        }

                        else
                        {
                            sTemp = !string.IsNullOrEmpty(ItemSource[nLoop].Amount) ? ItemSource[nLoop].Amount: string.Empty;
                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + Price(sTemp) + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" + Price(sTemp) + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + Price(sTemp) + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + Price(sTemp) + "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }

                        }





                        if (isTicketPrintedDate)
                        {
                            sTemp = !string.IsNullOrEmpty(ItemSource[nLoop].PayDate) ? ItemSource[nLoop].PayDate : string.Empty;
                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }

                        }

                        if (isDetails)
                        {
                            sTemp = !string.IsNullOrEmpty(ItemSource[nLoop].Status) ? ItemSource[nLoop].Status : string.Empty;
                            if (IsNumber(sTemp))
                            {
                                if (nCount != 0)
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='right'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";

                            }
                            else
                            {
                                if (nCount != 0)
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + sTemp + "</font></td>";
                                else
                                    sTr += "<td align='left'><font face='Arial' size='2'> " + "<b>" + sTemp + "</b>" + "</font></td>";
                                //dynTableHeader += "<td align='left'><font face='Arial' size='2'> " + block.Text; dynTableHeader += "</font></td>";
                            }

                        }   



                        //dynTableHeader += "</tr>";
                        sTr += "</tr>";
                        dynTableHeader += sTr;

                        if (nCount == 0)
                        {
                            sFirstRow = sTr;
                            nCount++;
                        }
                        sTr = "";


                    }
                    dynTableHeader += sFirstRow;
                    dynTableHeader += "</tbody>";

                    dynTableHeader += "<tfoot><tr><td colspan=" + sColCount + "><hr/></td></tr>";
                    dynTableHeader += "<tr><td colspan=" + sColCount + " align='center' style='padding: 0in;'><span style='font-size: 7.5pt; color: rgb(102, 102, 102); font-family:verdana'> This content is confidential, subject to contract and copyright, and may be privileged. </span></td>";
                    dynTableHeader += "</tfoot>";
                    dynTableHeader += "</table>";
                    DoWrite(dynTableHeader);
                    DoWrite("</BODY>");
                    DoWrite("</HTML>");
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception GenerateCode = " + ex.Message + ":" + ex.Source + ":" + ex.StackTrace, LogManager.enumLogLevel.Error);
                outputfile = null;
                writer = null;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Return true if this is a number.
        /// </summary>
        /// <param name="value">string to be parsed</param>
        /// <returns></returns>
        private bool IsNumber(string value)
        {
            bool match;
            string pattern = @"(^[0-9\.]*$)";
            Regex regEx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            match = regEx.Match(value).Success ? true : false;
            return match;

        }


        private bool GenerateHTMLCode(System.Windows.Controls.ListView lvView, string ReportName, DateTime ReportStartdate)
        {
            DataView dtSource;
            DataTable dtTable = new DataTable();
            try
            {

                if (lvView.Items.Count > 0)
                {

                    filename = "Print.HTML";
                    filepath = System.Windows.Forms.Application.StartupPath + "\\" + filename;
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                    outputfile = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(outputfile);
                    writer.BaseStream.Seek(0, SeekOrigin.End);
                    DoWrite("<!DOCTYPE HTML PUBLIC " + "-" + "//w3c//DTD HTML 3.2//EN" + " >" + "");
                    DoWrite("<HTML>");
                    DoWrite("<HEAD>");
                    DoWrite("<TITLE>");
                    DoWrite("</TITLE>");
                    DoWrite("</HEAD>");
                    DoWrite("<BODY >");
                    string dynTableHeader = "";

                    dynTableHeader = "<table border='0'width='75%'>";
                    dynTableHeader += "<tr>";
                    dynTableHeader += "<td><b>" + ReportName + "</b></td>";
                    dynTableHeader += "<td align='right'> <b>" + DateTime.Now.GetUniversalDateTimeFormat() + "</b></td>";
                    dynTableHeader += "</tr>";
                    dynTableHeader += "<tr><td></td><tr>";
                    dynTableHeader += "</table>";


                    dynTableHeader += "<table cellspacing='0'width='40%' cellpadding='0' border='0'>";
                    dynTableHeader += "<tr>";

                    dynTableHeader += "<td>";
                    dynTableHeader += "<b>Start Date : </b></td><td>" + ReportStartdate.GetUniversalDateTimeFormat() + "</td>";
                    dynTableHeader += "</tr>";
                    dynTableHeader += "<tr>";

                    dynTableHeader += "<td>";
                    dynTableHeader += "<b>End Date: </b></td><td>" + DateTime.Now.GetUniversalDateTimeFormat()
                        + "</td>";

                    dynTableHeader += "</tr>";
                    dynTableHeader += "</table>";
                    dynTableHeader += "<br/>";

                    dynTableHeader += "<table cellspacing='0' height='40%' cellpadding='0' border='1' style='border-collapse:collapse;border-style:solid;border-width:2px;border-color:gray;'>";

                    dynTableHeader += "<tr>";

                    GridView gridview = lvView.View as GridView;
                    foreach (GridViewColumn col in gridview.Columns)
                    {
                        dynTableHeader += "<td> " + col.Header.ToString();
                        dynTableHeader += "</td>";
                    }
                    dynTableHeader += "</tr>";

                    int colcount = gridview.Columns.Count;

                    if (FindType(lvView.ItemsSource) != null)
                    {
                        int iType = Convert.ToInt32(FindType(lvView.ItemsSource));
                        if (iType == 1)
                        {
                            dtSource = (DataView)lvView.ItemsSource;
                            dtTable = (DataTable)dtSource.Table;
                        }
                        else if (iType == 2)
                        {
                            dtTable = (DataTable)lvView.ItemsSource;
                        }
                        else
                        {
                            LogManager.WriteLog("List View Source is different", LogManager.enumLogLevel.Debug);
                            return false;
                        }

                    }

                    foreach (DataRowView row in lvView.Items)
                    {
                        dynTableHeader += "<tr>";
                        System.Windows.Controls.ListViewItem item1 = lvView.ItemContainerGenerator.ContainerFromIndex(lvView.Items.IndexOf(row))
                            as System.Windows.Controls.ListViewItem;


                        GridViewRowPresenter rowPresenter = GetFrameworkElementByName<GridViewRowPresenter>(item1);

                        for (int col = 0; col < colcount; col++)
                        {
                            if (rowPresenter != null)
                            {
                                ContentPresenter templatedParent = System.Windows.Media.VisualTreeHelper.GetChild(rowPresenter, col) as ContentPresenter;

                                foreach (GridViewColumn cols in gridview.Columns)
                                {                                    
                                    TextBlock block = (TextBlock)gridview.Columns[col].CellTemplate.FindName(cols.Header.ToString(), templatedParent);
                                    if (block != null)
                                    {
                                        if (gridview.Columns[col].Header.ToString().ToUpper() == block.Name.ToString().ToUpper())
                                        {
                                            dynTableHeader += "<td> " + block.Text;
                                            break;
                                        }
                                    }
                                    dynTableHeader += "</td>";
                                }
                               

                            }
                        }
                        dynTableHeader += "</tr>";
                    }


                    dynTableHeader += "</table>";
                    DoWrite(dynTableHeader);
                    //DoWrite("</CENTER>");
                    DoWrite("<BODY>");
                    DoWrite("</HTML>");
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception GenerateHTMLCode = " + ex.Message + ":" + ex.Source + ":" + ex.StackTrace, LogManager.enumLogLevel.Error);
                outputfile = null;
                writer = null;
                return false;
            }
            return true;
        }


        private void DoWrite(String line)
        {

            writer.WriteLine(line);
            writer.Flush();
        }

        private object FindType(object objSource)
        {
            switch (objSource.GetType().Name)
            {
                case "DataView":
                    return 1;
                case "DataTable":
                    return 2;

                default:
                    return null;
            }
        }

        private T GetFrameworkElementByName<T>(FrameworkElement referenceElement) where T : FrameworkElement
        {
            FrameworkElement child = null;

            for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(referenceElement); i++)
            {
                child = VisualTreeHelper.GetChild(referenceElement, i) as FrameworkElement;
                if (child != null && child.GetType() == typeof(T))
                {
                    break;
                }
                else if (child != null)
                {
                    child = GetFrameworkElementByName<T>(child);
                    if (child != null && child.GetType() == typeof(T))
                    {
                        break;
                    }
                }
            }

            return child as T;
        }

#endregion Private Functions
    }
}
