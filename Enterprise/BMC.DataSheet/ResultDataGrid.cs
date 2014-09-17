using System;
using System.Data;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using BMC.Common;

namespace CustomReports
{
    public partial class ResultDataGrid : Form
    {
        public ResultDataGrid()
        {
            InitializeComponent();
            setTagProperty();
        }

        private void setTagProperty()
        {
            this.btnExport.Tag = "Key_ExportCaption";
            this.Tag = "Key_BMCDataSheetResultsGrid";
            this.grpResults.Tag = "Key_Results";
            this.ResolveResources();

                      
        }

        public ResultDataGrid(string ReportSPName, DataSet objDataSet, List<string> lstItemsToRemove)
        {
            try
            {
                InitializeComponent();
                setTagProperty();
            
                DataView objDataView = objDataSet.Tables[0].DefaultView;
                DataTable dt = objDataSet.Tables[0];
                foreach (DataColumn col in objDataSet.Tables[0].Columns)
                {
                    if(! lstItemsToRemove.Contains(col.ColumnName))
                    {
                        col.ColumnName = this.GetResourceTextByKey("Key_DS_" + col.ColumnName);
                    }
                
                }
                foreach (string item in lstItemsToRemove)
                {
                    dt.Columns.Remove(item);
                }
                dgResult.DataSource = dt;                       
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string sFileName = string.Empty;

            try
            {
                BMC.CoreLib.Win32.Win32Extensions.ExportControlDataToExcel<object>(this, dgResult, null, true, false, true);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static String getColumnNameFromIndex(int column)
        {
            try
            {
                column--;
                String col = Convert.ToString((char)('A' + (column % 26)));
                while (column >= 26)
                {
                    column = (column / 26) - 1;
                    col = Convert.ToString((char)('A' + (column % 26))) + col;
                }
                return col;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }
    }
}
