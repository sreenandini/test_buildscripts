using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using System.Globalization;
using System.Drawing;

namespace BMC.EnterpriseClient.Helpers
{
    public class frmAdminUtilities
    {

        public void setListBox(ComboBox TheComboBox, string theRequiredText, Int64 theRequiredItemData)
        {

            int i = 0;
            int tempIndex = -1;
            try
            {
                int Count = TheComboBox.Items.Count;
                if (theRequiredItemData == 0 && theRequiredText != "")
                {
                    TheComboBox.SelectedIndex = i;
                    //while ((TheComboBox.SelectedText != theRequiredText) || (i < Count))
                    while (i < Count)
                    {
                        TheComboBox.SelectedIndex = i;
                        if ((TheComboBox.DropDownStyle.ToString() == "DropDown" || TheComboBox.DropDownStyle.ToString() == "Simple") && TheComboBox.Text == theRequiredText)
                        {
                            tempIndex = i;
                        }
                        else if (TheComboBox.DropDownStyle.ToString() == "DropDownList" && TheComboBox.GetItemText(TheComboBox.SelectedItem) == theRequiredText)
                        {
                            tempIndex = i;
                        }
                        i = i + 1;
                    }
                }
                else
                {
                    if (i >= 0 && i < TheComboBox.Items.Count)
                        TheComboBox.SelectedIndex = i;
                    //                    while ((TheComboBox.SelectedValue.ToString() != theRequiredItemData.ToString()) && (i < Count))
                    while (i < Count)
                    {
                        TheComboBox.SelectedIndex = i;
                        if (Convert.ToString(TheComboBox.SelectedValue) == Convert.ToString(theRequiredItemData))
                        {
                            tempIndex = i;
                        }
                        i = i + 1;
                    }
                }
                TheComboBox.SelectedIndex = tempIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                if (TheComboBox.DropDownStyle == ComboBoxStyle.DropDown)
                    TheComboBox.Text = theRequiredText;
            }
        }
        public string GetRegionalDate(string dt)
        {
            try
            {
                DateTime Date = Convert.ToDateTime(dt);
                return Convert.ToString(Date.ToShortDateString());
            }
            catch { return string.Empty; }
        }

        public bool IsDate(string sdate)
        {
            DateTime dt;
            bool isDate = true;

            try
            {
                dt = DateTime.Parse(sdate);
            }
            catch
            {
                isDate = false;
            }

            return isDate;
        }

        public string GetUniversalDate(DateTime strDate)
        {
            return strDate.ToString("dd MMM yyyy");
        }

        public int GetItemValue(ComboBox c)
        {
            if (c.SelectedIndex == -1)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(c.SelectedValue);
            }
        }

        public void AddNumericValue(IFormattable valueProvider, ListViewItem.ListViewSubItem item)
        {
            this.AddNumericValue(valueProvider, item, "#,##0.00");
        }

        public string GetNumericValue(IFormattable valueProvider)
        {
            return this.GetNumericValue(valueProvider, "#,##0.00");
        }      

        public void AddNumericValue(IFormattable valueProvider, ListViewItem.ListViewSubItem item, string format)
        {
            this.AddNumericValue(valueProvider, item, format, false);
        }

        public string GetNumericValue(IFormattable valueProvider, string format)
        {
            return this.GetNumericValue(valueProvider, format, false);
        }

        public void AddNumericValue(IFormattable valueProvider, ListViewItem.ListViewSubItem item, string format, bool noZeroDash)
        {
            this.AddNumericValue(valueProvider, item, format, string.Empty, false, noZeroDash);
        }

        public string GetNumericValue(IFormattable valueProvider, string format, bool noZeroDash)
        {
            return this.GetNumericValue(valueProvider, format, string.Empty, false, noZeroDash);
        }

        //public void AddNumericValue(IFormattable valueProvider, ListViewItem.ListViewSubItem item, string format, string addlText, bool noZeroDash)
        //{
        //    this.AddNumericValue(valueProvider, item, format, addlText, false
        //    ModuleProc PROC = new ModuleProc("", "AddNumericValue");
        //    try
        //    {
        //        string value = valueProvider.ToString(format, CultureInfo.CurrentUICulture);
        //        item.Text = value;
        //        this.FormatListViewSubNumber(item, false);
        //        if (!addlText.IsEmpty())
        //        {
        //            item.Text += addlText;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Exception(PROC, ex);
        //    }
        //}

        public void AddNumericValue(IFormattable valueProvider, ListViewItem.ListViewSubItem item, string format, string addlText, bool AddTextprecede)
        {
            this.AddNumericValue(valueProvider, item, format, addlText, AddTextprecede, false);
        }

        public void AddNumericValue(IFormattable valueProvider, ListViewItem.ListViewSubItem item, string format, string addlText, bool AddTextprecede, bool noZeroDash)
        {
            ModuleProc PROC = new ModuleProc("", "AddNumericValue");
            try
            {
                string value = valueProvider.ToString(format, CultureInfo.CurrentUICulture);
                item.Text = value;
                this.FormatListViewSubNumber(item, format, false, noZeroDash);
                if (!addlText.IsEmpty())
                {
                    if (AddTextprecede)
                        item.Text = addlText + item.Text;
                    else
                        item.Text += addlText;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public string GetNumericValue(IFormattable valueProvider, string format, string addlText, bool AddTextprecede)
        {
            return this.GetNumericValue(valueProvider, format, addlText, AddTextprecede, false);
        }

        public string GetNumericValue(IFormattable valueProvider, string format, string addlText, bool AddTextprecede, bool noZeroDash)
        {
            ModuleProc PROC = new ModuleProc("", "AddNumericValue");
            string result = string.Empty;

            try
            {
                string value = valueProvider.ToString(format, CultureInfo.CurrentUICulture);
                
                result = this.FormatNumericText(value, format, false, noZeroDash);
                if (!addlText.IsEmpty())
                {
                    if (AddTextprecede)
                        result = addlText + result;
                    else
                        result += addlText;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public void ZeroDash(IFormattable valueProvider, ListViewItem.ListViewSubItem item, int decimalPlaces)
        {
            ZeroDash(valueProvider, item, decimalPlaces, string.Empty);
        }

        public void ZeroDashHFDiv(double valueProvider1, double valueProvider2, ListViewItem.ListViewSubItem item, int decimalPlaces)
        {
            ZeroDash(HFDiv(valueProvider1, valueProvider2), item, decimalPlaces, string.Empty);
        }

        public void ZeroDash(IFormattable valueProvider, ListViewItem.ListViewSubItem item, int decimalPlaces, string format)
        {
            if (decimalPlaces > 0)
            {
                format += "." + new String('0', decimalPlaces);
            }
            this.AddNumericValue(valueProvider, item, format, string.Empty, false, false);
        }

        public double HFDiv(double value1, double value2)
        {
            if (Microsoft.VisualBasic.Information.IsNumeric(value1) &&
                Microsoft.VisualBasic.Information.IsNumeric(value2))
            {
                if (value2 == 0)
                    return 0;
                else
                    return (value1 / value2);
            }
            else
            {
                return 0;
            }
        }

        public void FormatListViewSubNumber(ListViewItem.ListViewSubItem theSubItem, string format, bool bHighlight, bool noZeroDash)
        {
            ModuleProc PROC = new ModuleProc("", "FormatListViewSubNumber");
            try
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(theSubItem.Text))
                {
                    double value = TypeSystem.GetValueDouble(theSubItem.Text);
                    if (value == 0)
                    {
                        if (!noZeroDash)
                        {
                            theSubItem.Text = "-";
                        }
                        else
                        {
                            theSubItem.Text = (format.IsEmpty() ? value.ToString() : value.ToString(format, CultureInfo.CurrentUICulture));
                        }
                    }
                    else if (value < 0)
                    {
                        theSubItem.Text = "(" + theSubItem.Text + ")";
                        theSubItem.ForeColor = (bHighlight ? Color.FromArgb(255, 100, 100) : Color.FromArgb(255, 0, 0));
                    }
                    else
                    {
                        if (bHighlight)
                        {
                            theSubItem.ForeColor = Color.FromArgb(100, 100, 100);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public string FormatNumericText(string text, string format, bool bHighlight, bool noZeroDash)
        {
            ModuleProc PROC = new ModuleProc("", "FormatListViewSubNumber");
            string result = text;

            try
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(text))
                {
                    double value = TypeSystem.GetValueDouble(text);
                    if (value == 0)
                    {
                        if (!noZeroDash)
                        {
                            result = "-";
                        }
                        else
                        {
                            result = (format.IsEmpty() ? value.ToString() : value.ToString(format, CultureInfo.CurrentUICulture));
                        }
                    }
                    else if (value < 0)
                    {
                        result = "(" + text + ")";
                    }
                    else
                    {
                        if (bHighlight)
                        {
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public bool CustomerAccessIsDepotVisible(int depotID)
        {
            return true;
        }
    }
}
