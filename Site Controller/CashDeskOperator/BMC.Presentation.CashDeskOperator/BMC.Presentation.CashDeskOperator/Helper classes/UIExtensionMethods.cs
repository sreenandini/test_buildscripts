using System;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Controls;
using BMC.Common.Utilities;
using System.ComponentModel;
using System.Windows;
using System.Windows.Resources;
using Form = System.Windows.Forms;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace BMC.Presentation.POS.Helper_classes
{
    public static class UIExtensionMethods
    {
        #region ComboBox
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

        public static int LoadLookUp(this ComboBox objCombo, string sLookupCode) 
        {
            LookupManager objLookupManager = new LookupManager(BMC.CashDeskOperator.BusinessObjects.oCommonUtilities.CreateInstance().GetConnectionString());
            IList<LookupObject> objColLookUp = objLookupManager.GetLookupObject(sLookupCode);
            objCombo.ItemsSource = objColLookUp;
            objCombo.DisplayMemberPath = "DisplayText";
            objCombo.SelectedValuePath = "CodeID";
            objCombo.SelectedIndex = 0;
            return 0;
        }

        public static int LoadLookUp(this ComboBox objCombo, int? Parent_CodeID)
        {
            LookupManager objLookupManager = new LookupManager(BMC.CashDeskOperator.BusinessObjects.oCommonUtilities.CreateInstance().GetConnectionString());
            IList<LookupObject> objColLookUp = objLookupManager.GetLookupObject(Parent_CodeID);
            objCombo.ItemsSource = objColLookUp;
            objCombo.DisplayMemberPath = "DisplayText";
            objCombo.SelectedValuePath = "CodeID";
            objCombo.SelectedIndex = 0;
            return 0;
        }

        #endregion

        #region TextBox

        public static int SetCurrencyText(this TextBox objText, int Value)
        {
            if(Value > -1)   
                objText.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + " " 
                        + Value.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture));
            return 0;
        }

        public static int SetCurrencyText(this TextBox objText, decimal Value)
        {
            if (Value > -1)
                objText.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + " "
                        + Value.ToString(new CultureInfo(ExtensionMethods.CurrentCurrenyCulture));
            return 0;
        }

        public static int SetCurrencyText(this TextBox objText, string Value)
        {
            objText.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + " " + Value;
            return 0;
        }

        public static int GetCurrencyValueAsInt(this TextBox objText)
        {
            int bReturn = -1;
            string []sText = objText.Text.Split(' ');
            if (sText[1] != null && sText[1].Length > 0)
                bReturn = int.Parse(sText[1], NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture));
            return bReturn;
        }

        public static decimal GetCurrencyValueAsDecimal(this TextBox objText)
        {
            if (objText.Text == "0")
                return 0;
            
            decimal bReturn = -1;
            string[] sText = objText.Text.Split(' ');
            if (sText[1] != null && sText[1].Length > 0)
                bReturn = decimal.Parse(sText[1], NumberStyles.Currency, new CultureInfo(ExtensionMethods.CurrentCurrenyCulture));
            return bReturn;
        }

        public static string GetCurrencyValueAsString(this TextBox objText)
        {
            string Return = "";
            if (objText.Text != null && objText.Text.Length > 0 )
            {
                if (objText.Text.IndexOf(ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol()) > -1)
                {
                    string[] sText = objText.Text.Split(' ');
                    if (sText[1] != null && sText[1].Length > 0)
                        Return = sText[1];
                }
                else
                {
                    Return = objText.Text;
                }
            }
            return Return;
        }

        public static string GetCurrencyValueAsString(this string objText)
        {
            string Return = "";
            if (objText != null && objText.Length > 0)
            {
                if (objText.IndexOf(ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol()) > -1)
                {
                    string[] sText = objText.Split(' ');
                    if (sText[1] != null && sText[1].Length > 0)
                        Return = sText[1];
                }
                else
                {
                    Return = objText;
                }
            }
            return Return;
        }

        public static int Add(this TextBox objText, decimal nSecond)
        {
            if (objText.GetCurrencyValueAsString().IsNullOrEmpty())
                objText.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + " " + nSecond.ToString();

             objText.Text = ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() + " " + 
                                            (objText.GetCurrencyValueAsDecimal() + nSecond).ToString();
             return 0;
        }

        #endregion

        #region List View

        public static int Sort(this ListView objListView, GridViewColumnHeader Header)
        {
            string sSortColumn = "";
            ListSortDirection direction;
            direction = ListSortDirection.Ascending;

            DataTemplate o = Header.Column.CellTemplate as DataTemplate;
            
            if (Header.Column.HeaderTemplate == null)
            {
                direction = ListSortDirection.Ascending;
                Header.Column.HeaderTemplate = App.Current.Resources["HeaderArrowUp"] as DataTemplate;
            } 
            else
            {
                if (Header.Column.HeaderTemplate == App.Current.Resources["HeaderArrowUp"] as DataTemplate)
                {
                    direction = ListSortDirection.Descending;
                    Header.Column.HeaderTemplate = App.Current.Resources["HeaderArrowDown"] as DataTemplate;
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                    Header.Column.HeaderTemplate = App.Current.Resources["HeaderArrowUp"] as DataTemplate;
                }
            }

            if(Header.Tag != null)
                sSortColumn = Header.Tag as string;

            var list = objListView.View as GridView;
            foreach (var obj in list.Columns)
            {
                if(obj.Header != Header.Column.Header)
                    obj.HeaderTemplate = null;
            }

            objListView.Items.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sSortColumn, direction);
            objListView.Items.SortDescriptions.Add(sd);
            objListView.Items.Refresh();

            return 0;
        }

        #endregion

        #region Window Methods

        private static Window _defaultDialogOwner = null;

        /// <summary>
        /// Gets or sets the default dialog owner.
        /// </summary>
        /// <value>The default dialog owner.</value>
        public static void SetDefaultDialogOwner(this Window ownerWindow)
        {
            _defaultDialogOwner = ownerWindow;
            MessageBox.parentOwner = _defaultDialogOwner;
        }

        /// <summary>
        /// Shows the extended dialog.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="owner">The owner.</param>
        /// <returns>True if OK clicked; otherwise false or null.</returns>
        [System.Security.SecurityCritical]
        public static bool? ShowDialogEx(this Window source, UIElement owner)
        {
            owner.SetWindowOwner(source);
            source.ShowInTaskbar = false;
            return source.ShowDialog();
        }

        /// <summary>
        /// Sets the owner of a System.Windows.Forms.Form to a System.Windows.Window
        /// </summary>
        /// <param name="childObject"></param>
        /// <param name="owner"></param>
        public static void SetOwner(this Form.Form childObject, Window owner)
        {
            WindowInteropHelper helper = new WindowInteropHelper(owner);
            SetWindowLong(new HandleRef(childObject, childObject.Handle), -8, helper.Handle.ToInt32());
        }

        /// <summary>
        /// Shows the dialog ex.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values</returns>
        public static System.Windows.Forms.DialogResult ShowDialogEx(this System.Windows.Forms.Form form)
        {
            form.ShowInTaskbar = false;
            return form.ShowDialog();
        }

        /// <summary>
        /// Sets the window owner.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="dialogWindow">The dialog window.</param>
        public static void SetWindowOwner(this UIElement source, Window dialogWindow)
        {
            Window ownerWindow = source.GetWindowOwner();
            if (ownerWindow != null)
            {
                if (ownerWindow.IsActive)
                {
                    dialogWindow.Owner = ownerWindow;
                }
            }
        }

        /// <summary>
        /// Gets the window owner.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Owner window.</returns>
        public static Window GetWindowOwner(this UIElement source)
        {
            Window ownerWindow = null;
            if (source != null)
            {
                ownerWindow = Window.GetWindow(source);
            }
            if (ownerWindow == null)
            {
                ownerWindow = _defaultDialogOwner;
            }
            return ownerWindow;
        }

        /// <summary>
        /// Sets the message box owner.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void SetMessageBoxOwner(this UIElement source)
        {
            Window ownerWindow = source.GetWindowOwner();
            if (ownerWindow != null)
            {
                MessageBox.parentOwner = ownerWindow;
            }
        }

        #endregion
    }

     
}
