using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System.Windows.Forms;
using BMC.EnterpriseClient.Views;
using System.IO;
using System.Drawing;
using BMC.Common.Utilities;

namespace BMC.EnterpriseClient.Helpers
{
    public enum ComboAdditionalItemType
    {
        NoFilter = 0,
        Any = 1,
        None = 2
    }

    public static class GlobalHelper
    {
        public static void FillCombo(this ComboBox combo,
            object dataSource, string valueMember, string displayMember,
            ComboAdditionalItemType additionItemType,
            Action<object, int, string> displayAdditionalItem)
        {
            ModuleProc PROC = new ModuleProc("GlobalHelper", "FillCombo");

            try
            {
                switch (additionItemType)
                {
                    case ComboAdditionalItemType.Any:
                        if (displayAdditionalItem != null)
                        {
                            displayAdditionalItem(dataSource, 0,BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_Any"));
                        }
                        break;

                    case ComboAdditionalItemType.None:
                        if (displayAdditionalItem != null)
                        {
                            displayAdditionalItem(dataSource, 0, BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_NoneHyphen"));
                        }
                        break;

                    default:
                        break;
                }

                combo.DataSource = dataSource;
                combo.DisplayMember = displayMember;
                combo.ValueMember = valueMember;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static void ActivateControl(object source, object input)
        {
             IControlActivator activator = source as IControlActivator;
            if (activator != null)
            {
                activator.ActivateControl(input);
            }
        }
        /// <summary>
        /// List View Export To Csv File R.Rajkumar
        /// </summary>
        /// <param name="Lvitems"></param>
        public static void ExportTocsv(ListView Lvitems)
        {
            try
            {
                string fileName = string.Empty;
                System.Text.StringBuilder theBuilder = new System.Text.StringBuilder();
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Word Documents (*.csv)|*.csv";
                if (dialog.ShowDialog() == DialogResult.Cancel) return;

                fileName = dialog.FileName;
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default))
                {
                    foreach (System.Windows.Forms.ColumnHeader item in Lvitems.Columns)
                    {
                        theBuilder.AppendFormat("\"{0}\",", item.Text);
                    }
                    theBuilder.AppendLine();

                    foreach (ListViewItem item in Lvitems.Items)
                    {
                        foreach (System.Windows.Forms.ListViewItem.ListViewSubItem subItem in item.SubItems)
                        {
                            theBuilder.AppendFormat("\"{0}\",", subItem.Text.Replace(ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol(), string.Empty).Trim());
                        }

                        theBuilder.AppendLine();
                    }
                    sw.Write(theBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        /// <summary>
        /// DataGridView Export To Csv File
        /// </summary>
        /// <param name="dGV"></param>
        public static void ExportTocsv(DataGridView dGV)
        {
            try
            {
                string fileName = string.Empty;
                System.Text.StringBuilder theBuilder = new System.Text.StringBuilder();
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Word Documents (*.csv)|*.csv";
                if (dialog.ShowDialog() == DialogResult.Cancel) return;

                fileName = dialog.FileName;

                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default))
                {
                    for (int j = 0; j < dGV.Columns.Count; j++)
                    {
                        if (dGV.Columns[j].Visible)
                        {
                            theBuilder.AppendFormat("\"{0}\",", dGV.Columns[j].HeaderText);
                        }
                    }
                    theBuilder.AppendLine();

                    // Export data.
                    for (int i = 0; i < dGV.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                        {
                            if (dGV.Rows[i].Cells[j].Visible)
                            {
                                theBuilder.AppendFormat("\"{0}\",", dGV.Rows[i].Cells[j].Value);
                            }
                        }
                        theBuilder.AppendLine();
                    }
                    sw.Write(theBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }
    }
}
