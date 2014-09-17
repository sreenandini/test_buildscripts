using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Win32
{
    public partial class ListViewEx : ListView
    {
        private ListViewClipboardCopyMode _clipboardCopyMode = ListViewClipboardCopyMode.Disable;
        private ListViewClipboardCopyFormat _clipboardCopyFormat = ListViewClipboardCopyFormat.Default;

        public ListViewEx()
        {
        }

        public ListViewGroup AddOrGetGroup(ListViewItem item, string key, string header)
        {
            ModuleProc PROC = new ModuleProc("ListViewEx", "AddOrGetGroup");
            ListViewGroup grp = default(ListViewGroup);

            try
            {
                grp = this.Groups[key];
                if (grp == null)
                {
                    grp = new ListViewGroup(key, header);
                    this.Groups.Add(grp);
                }
                item.Group = grp;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return grp;
        }

        public ListViewClipboardCopyMode ClipboardCopyMode
        {
            get { return _clipboardCopyMode; }
            set { _clipboardCopyMode = value; }
        }

        public ListViewClipboardCopyFormat ClipboardCopyFormat
        {
            get { return _clipboardCopyFormat; }
            set { _clipboardCopyFormat = value; }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "OnKeyDown");

            try
            {
                base.OnKeyDown(e);

                if (_clipboardCopyMode == ListViewClipboardCopyMode.Disable) return;
                if (e.Control && !e.Shift)
                {
                    // copy
                    if (e.KeyCode == Keys.C)
                    {
                        IList items = this.Items;
                        IList indices = null;

                        if (this.FullRowSelect)
                        {
                            if (this.MultiSelect &&
                                this.SelectedItems != null &&
                                this.SelectedItems.Count > 0)
                            {
                                items = this.SelectedItems;
                            }
                        }

                        this.CopyToClipboard(items, indices);
                    }
                    // copy to excel
                    if (e.KeyCode == Keys.E)
                    {
                        var forms = Application.OpenForms;
                        if (forms.Count > 0)
                        {
                            Win32Extensions.ExportControlDataToExcel<object>(forms[0], this, null,
                                (_clipboardCopyMode == ListViewClipboardCopyMode.EnableWithHeaderText),
                                true, true);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void CopyToClipboard(IList items, IList indices)
        {
            ModuleProc PROC = new ModuleProc("", "CopyToClipboard");
            DataTable dt = new DataTable();
            IList<HorizontalAlignment> dataAlign = new List<HorizontalAlignment>();
            IList<Type> dataTypes = new List<Type>();

            try
            {
                // column names
                if (indices == null)
                {
                    foreach (var col in this.Columns.OfType<ColumnHeader>())
                    {
                        dt.Columns.Add(col.Text);
                        dataAlign.Add(col.TextAlign);
                        if (col.Tag != null &&
                            col.Tag is Type)
                        {
                            dataTypes.Add(col.Tag as Type);
                        }
                        else
                        {
                            dataTypes.Add(typeof(string));
                        }
                    }
                }
                else
                {
                    foreach (int idx in indices)
                    {
                        var col = this.Columns[idx];
                        dt.Columns.Add(col.Text);
                        dataAlign.Add(col.TextAlign);
                        if (col.Tag != null &&
                            col.Tag is Type)
                        {
                            dataTypes.Add(col.Tag as Type);
                        }
                        else
                        {
                            dataTypes.Add(typeof(string));
                        }
                    }
                }

                // max lengths (ROW 1)
                DataRow drRow1 = dt.Rows.Add();
                foreach (var item in dt.Columns.OfType<DataColumn>())
                {
                    drRow1[item] = 0;
                    drRow1[item] = item.ColumnName.Length;
                }

                // column alignment (ROW 2)
                DataRow drRow2 = dt.Rows.Add();
                int colIdx = 0;
                foreach (var item in dt.Columns.OfType<DataColumn>())
                {
                    drRow2[item] = (int)dataAlign[colIdx++];
                }

                // column type (ROW 3)
                DataRow drRow3 = dt.Rows.Add();
                colIdx = 0;
                foreach (var item in dt.Columns.OfType<DataColumn>())
                {
                    drRow3[item] = dataTypes[colIdx++].ToString();
                }

                // content (ROW 4)
                foreach (var item in items.OfType<ListViewItem>())
                {
                    colIdx = 0;
                    DataRow dr = dt.Rows.Add();

                    foreach (var subItem in item.SubItems.OfType<ListViewItem.ListViewSubItem>())
                    {
                        string text = subItem.Text;
                        int length = TypeSystem.GetValueInt(drRow1[colIdx]);
                        int lengthNew = text.ToStringSafe().Length;
                        dr[colIdx] = text;
                        if (lengthNew > length)
                        {
                            drRow1[colIdx] = lengthNew;
                        }
                        colIdx++;
                    }
                }

                this.CopyToClipboard(dt);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void CopyToClipboard(DataTable dt)
        {
            ModuleProc PROC = new ModuleProc("", "CopyToClipboard");
            StringBuilder sb = new StringBuilder();
            int gap = 2;
            string stringType = typeof(string).FullName;

            try
            {
                DataRow drRow1 = dt.Rows[0];
                DataRow drRow2 = dt.Rows[1];
                DataRow drRow3 = dt.Rows[2];

                if (_clipboardCopyMode == ListViewClipboardCopyMode.EnableWithHeaderText)
                {
                    int colIdx = 0;
                    foreach (var col in dt.Columns.OfType<DataColumn>())
                    {
                        switch (_clipboardCopyFormat)
                        {
                            case ListViewClipboardCopyFormat.CSV:
                            case ListViewClipboardCopyFormat.Semicolon:
                                {
                                    if (colIdx > 0)
                                    {
                                        if (_clipboardCopyFormat == ListViewClipboardCopyFormat.Semicolon) sb.Append(";");
                                        else sb.Append(",");
                                    }
                                    sb.Append("\"");
                                    sb.Append(col.ColumnName);
                                    sb.Append("\"");
                                }
                                break;

                            default:
                                {
                                    int length = TypeSystem.GetValueInt(drRow1[col]) + gap;
                                    HorizontalAlignment align = (HorizontalAlignment)TypeSystem.GetValueInt(drRow2[col]);
                                    if (align == HorizontalAlignment.Left)
                                        sb.AppendFormattedStringRight(col.ColumnName, 0, length, ' ');
                                    else
                                        sb.AppendFormattedString(col.ColumnName, 0, length, ' ');
                                }
                                break;
                        }

                        colIdx++;
                    }
                    sb.AppendLine();
                }

                if (dt.Rows.Count > 3)
                {
                    for (int i = 3; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        int colIdx = 0;
                        foreach (var col in dt.Columns.OfType<DataColumn>())
                        {
                            string data = dr[col].ToStringSafe();

                            switch (_clipboardCopyFormat)
                            {
                                case ListViewClipboardCopyFormat.CSV:
                                case ListViewClipboardCopyFormat.Semicolon:
                                    {
                                        if (colIdx > 0)
                                        {
                                            if (_clipboardCopyFormat == ListViewClipboardCopyFormat.Semicolon) sb.Append(";");
                                            else sb.Append(",");
                                        }

                                        bool isString = drRow3[col].ToStringSafe().IgnoreCaseCompare(stringType);
                                        if (isString) sb.Append("\"");
                                        sb.Append(data);
                                        if (isString) sb.Append("\"");
                                    }
                                    break;

                                default:
                                    {
                                        int length = TypeSystem.GetValueInt(drRow1[col]) + gap;
                                        HorizontalAlignment align = (HorizontalAlignment)TypeSystem.GetValueInt(drRow2[col]);
                                        if (align == HorizontalAlignment.Left)
                                            sb.AppendFormattedStringRight(data, 0, length, ' ');
                                        else
                                            sb.AppendFormattedString(data, 0, length, ' ');
                                    }
                                    break;
                            }

                            colIdx++;
                        }
                        sb.AppendLine();
                    }
                }

                Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }

    public enum ListViewClipboardCopyMode
    {
        // Summary:
        //     Copying to the Clipboard is disabled.
        Disable = 0,
        //
        // Summary:
        //     The text values of selected cells can be copied to the Clipboard. Row or
        //     column header text is included for rows or columns that contain selected
        //     cells only when the System.Windows.Forms.DataGridView.SelectionMode property
        //     is set to System.Windows.Forms.DataGridViewSelectionMode.RowHeaderSelect
        //     or System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect and
        //     at least one header is selected.
        EnableWithHeaderText = 1,
        //
        // Summary:
        //     The text values of selected cells can be copied to the Clipboard. Header
        //     text is not included.
        EnableWithoutHeaderText = 2,
    }

    public enum ListViewClipboardCopyFormat
    {
        Default = 0,
        CSV,
        Semicolon
    }
}
