// Developed by A.Vinod Kumar
// Initial Release on : 22/10/2010

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WIN = System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using System.Drawing.Drawing2D;

namespace BMC.MeterAdjustmentTool.Helpers
{
    /// <summary>
    /// Win32 Extensions
    /// </summary>
    public static class Win32Extensions
    {
        #region Object Methods

        /// <summary>
        /// Toes the string safe.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>String value.</returns>
        public static string ToStringSafe(this object source)
        {
            if (source is string)
            {
                if (!string.IsNullOrEmpty((string)source))
                    return string.Empty;
            }

            if (source == null) return string.Empty;
            return source.ToString();
        }

        #endregion

        #region Folder Dialogs

        /// <summary>
        /// Shows the folder dialog.
        /// </summary>
        /// <param name="selectedObject">The selected object.</param>
        /// <param name="description">The description.</param>
        /// <returns>Selected folder.</returns>
        public static string ShowFolderDialog(string selectedObject, string description)
        {
            if (!Directory.Exists(selectedObject))
                selectedObject = string.Empty;

            FolderBrowserDialog oddCommand = new FolderBrowserDialog();
            oddCommand.Description = description;
            oddCommand.RootFolder = Environment.SpecialFolder.DesktopDirectory;
            oddCommand.SelectedPath = selectedObject;
            if (oddCommand.ShowDialog() == DialogResult.OK)
                return oddCommand.SelectedPath;
            return string.Empty;
        }

        #endregion

        #region Dialog Methods

        /// <summary>
        /// Shows the extended dialog and destroy.
        /// </summary>
        /// <param name="dialogForm">The dialog form.</param>
        /// <param name="ownerForm">The owner form.</param>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        public static bool ShowDialogExAndDestroy(this Form dialogForm, IWin32Window ownerForm)
        {
            return ShowDialogExAndDestroy(dialogForm, ownerForm, null, null);
        }

        /// <summary>
        /// Shows the extended dialog and destroy.
        /// </summary>
        /// <param name="dialogForm">The dialog form.</param>
        /// <param name="ownerForm">The owner form.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowDialogExResultAndDestroy(this Form dialogForm, IWin32Window ownerForm)
        {
            return ShowDialogExResultAndDestroy(dialogForm, ownerForm, null, null);
        }

        /// <summary>
        /// Shows the extended dialog and destroy.
        /// </summary>
        /// <param name="dialogForm">The dialog form.</param>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="action">The action.</param>
        /// <param name="afterAction">The after action.</param>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        public static bool ShowDialogExAndDestroy(this Form dialogForm, IWin32Window ownerForm, Action<Form> action, Action<Form> afterAction)
        {
            if (dialogForm == null) return false;
            using (dialogForm)
            {
                return ShowDialogEx(dialogForm, ownerForm, action, afterAction);
            }
        }

        /// <summary>
        /// Shows the extended dialog and destroy.
        /// </summary>
        /// <param name="dialogForm">The dialog form.</param>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="action">The action.</param>
        /// <param name="afterAction">The after action.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowDialogExResultAndDestroy<T>(this T dialogForm, IWin32Window ownerForm, Action<T> action, Action<T> afterAction)
            where T : Form
        {
            if (dialogForm == null) return DialogResult.None;
            using (dialogForm)
            {
                return ShowDialogExResult(dialogForm, ownerForm, action, afterAction);
            }
        }

        /// <summary>
        /// Shows the extended dialog.
        /// </summary>
        /// <param name="dialogForm">The dialog form.</param>
        /// <param name="ownerForm">The owner form.</param>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        public static bool ShowDialogEx(this Form dialogForm, IWin32Window ownerForm)
        {
            return ShowDialogEx(dialogForm, ownerForm, null, null);
        }

        /// <summary>
        /// Shows the extended dialog.
        /// </summary>
        /// <param name="dialogForm">The dialog form.</param>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="action">The action.</param>
        /// <param name="afterAction">The after action.</param>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        public static bool ShowDialogEx(this Form dialogForm, IWin32Window ownerForm,
            Action<Form> action, Action<Form> afterAction)
        {
            DialogResult result = ShowDialogExResult<Form>(dialogForm, ownerForm, action, afterAction);
            return (result == DialogResult.OK);
        }

        /// <summary>
        /// Shows the extended dialog with DialogResult.
        /// </summary>
        /// <param name="dialogForm">The dialog form.</param>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="action">The action.</param>
        /// <param name="afterAction">The after action.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowDialogExResult<T>(this T dialogForm, IWin32Window ownerForm,
            Action<T> action, Action<T> afterAction)
            where T : Form
        {
            if (dialogForm == null) return DialogResult.None;
            if (action != null) action(dialogForm);
            DialogResult result = DialogResult.Cancel;

            if (ownerForm == null) result = dialogForm.ShowDialog();
            else result = dialogForm.ShowDialog(ownerForm);

            if (afterAction != null) afterAction(dialogForm);
            return result;
        }

        public static bool IsFormClosedOK<T>(this T dialogForm)
            where T : Form
        {
            return (dialogForm.DialogResult == DialogResult.OK);
        }

        #endregion

        #region File Dialogs

        #region Generic File Dialog
        /// <summary>
        /// Shows the file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="givenDialog">The given dialog.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowFileDialog(this IWin32Window ownerWindow,
                                                    WIN.FileDialog givenDialog,
                                                    string title,
                                                    string filter,
                                                    string defaultExt)
        {
            return ShowFileDialog(ownerWindow, givenDialog, title, filter, defaultExt, string.Empty, null);
        }

        /// <summary>
        /// Shows the file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="givenDialog">The given dialog.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <param name="action">The action.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowFileDialog(this IWin32Window ownerWindow,
                                                    WIN.FileDialog givenDialog,
                                                    string title,
                                                    string filter,
                                                    string defaultExt,
                                                    Action<WIN.FileDialog> action)
        {
            return ShowFileDialog(ownerWindow, givenDialog, title, filter, defaultExt, string.Empty, action);
        }

        /// <summary>
        /// Shows the file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="givenDialog">The given dialog.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <param name="defaultFileName">Default name of the file.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowFileDialog(this IWin32Window ownerWindow,
                                                    WIN.FileDialog givenDialog,
                                                    string title,
                                                    string filter,
                                                    string defaultExt,
                                                    string defaultFileName)
        {
            return ShowFileDialog(ownerWindow, givenDialog, title, filter, defaultExt, defaultFileName, null);
        }

        /// <summary>
        /// Shows the file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="givenDialog">The given dialog.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <param name="defaultFileName">Default name of the file.</param>
        /// <param name="func">The func.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowFileDialog(this IWin32Window ownerWindow,
                                                    WIN.FileDialog givenDialog,
                                                    string title,
                                                    string filter,
                                                    string defaultExt,
                                                    string defaultFileName,
                                                    Action<WIN.FileDialog> action)
        {
            WIN.FileDialog dialog = givenDialog;
            dialog.Title = title;
            dialog.CheckFileExists = (givenDialog is WIN.SaveFileDialog ? false : true);
            dialog.CheckPathExists = true;
            dialog.DefaultExt = defaultExt;
            dialog.Filter = filter;
            dialog.FilterIndex = 0;
            dialog.FileName = defaultFileName;
            dialog.AddExtension = true;

            if (action != null) action(dialog);
            DialogResult result = dialog.ShowDialog(ownerWindow);
            if (result == DialogResult.OK)
            {
                return dialog;
            }
            return null;
        }
        #endregion

        #region Open Dialog
        /// <summary>
        /// Shows the open file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowOpenFileDialog(this IWin32Window ownerWindow,
                                                    string title,
                                                    string filter,
                                                    string defaultExt)
        {
            return ShowOpenFileDialog(ownerWindow, title, filter, defaultExt, string.Empty, null);
        }

        /// <summary>
        /// Shows the open file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <param name="action">The action.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowOpenFileDialog(this IWin32Window ownerWindow,
                                                    string title,
                                                    string filter,
                                                    string defaultExt,
                                                    Action<WIN.FileDialog> action)
        {
            return ShowOpenFileDialog(ownerWindow, title, filter, defaultExt, string.Empty, action);
        }

        /// <summary>
        /// Shows the open file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="givenDialog">The given dialog.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <param name="defaultFileName">Default name of the file.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowOpenFileDialog(this IWin32Window ownerWindow,
                                                    string title,
                                                    string filter,
                                                    string defaultExt,
                                                    string defaultFileName)
        {
            return ShowOpenFileDialog(ownerWindow, title, filter, defaultExt, defaultFileName, null);
        }

        /// <summary>
        /// Shows the open file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="givenDialog">The given dialog.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <param name="defaultFileName">Default name of the file.</param>
        /// <param name="func">The func.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowOpenFileDialog(this IWin32Window ownerWindow,
                                                    string title,
                                                    string filter,
                                                    string defaultExt,
                                                    string defaultFileName,
                                                    Action<WIN.FileDialog> action)
        {
            return ShowFileDialog(ownerWindow, new WIN.OpenFileDialog(), title, filter, defaultExt, defaultFileName, action);
        }
        #endregion

        #region Save Dialog
        /// <summary>
        /// Shows the save file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowSaveFileDialog(this IWin32Window ownerWindow,
                                                    string title,
                                                    string filter,
                                                    string defaultExt)
        {
            return ShowSaveFileDialog(ownerWindow, title, filter, defaultExt, string.Empty, null);
        }

        /// <summary>
        /// Shows the save file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <param name="action">The action.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowSaveFileDialog(this IWin32Window ownerWindow,
                                                    string title,
                                                    string filter,
                                                    string defaultExt,
                                                    Action<WIN.FileDialog> action)
        {
            return ShowSaveFileDialog(ownerWindow, title, filter, defaultExt, string.Empty, action);
        }

        /// <summary>
        /// Shows the save file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="givenDialog">The given dialog.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <param name="defaultFileName">Default name of the file.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowSaveFileDialog(this IWin32Window ownerWindow,
                                                    string title,
                                                    string filter,
                                                    string defaultExt,
                                                    string defaultFileName)
        {
            return ShowSaveFileDialog(ownerWindow, title, filter, defaultExt, defaultFileName, null);
        }

        /// <summary>
        /// Shows the save file dialog.
        /// </summary>
        /// <param name="ownerWindow">The owner window.</param>
        /// <param name="givenDialog">The given dialog.</param>
        /// <param name="title">The title.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="defaultExt">The default ext.</param>
        /// <param name="defaultFileName">Default name of the file.</param>
        /// <param name="func">The func.</param>
        /// <returns>File dialog</returns>
        public static WIN.FileDialog ShowSaveFileDialog(this IWin32Window ownerWindow,
                                                    string title,
                                                    string filter,
                                                    string defaultExt,
                                                    string defaultFileName,
                                                    Action<WIN.FileDialog> action)
        {
            return ShowFileDialog(ownerWindow, new WIN.SaveFileDialog(), title, filter, defaultExt, defaultFileName, action);
        }
        #endregion

        #endregion

        #region MessageBox Members

        /// <summary>
        /// Shows the information message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void ShowInfoMessageBox(string message)
        {
            ShowInfoMessageBox(null, message);
        }

        /// <summary>
        /// Shows the information message.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        public static void ShowInfoMessageBox(this IWin32Window owner, string message)
        {
            ShowMessageBox(null, message, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void ShowErrorMessageBox(string message)
        {
            ShowErrorMessageBox(null, message);
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        public static void ShowErrorMessageBox(this IWin32Window owner, string message)
        {
            ShowMessageBox(owner, message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows the question message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message box result.</returns>
        public static DialogResult ShowQuestionMessageBox(string message)
        {
            return ShowQuestionMessageBox(null, message);
        }

        /// <summary>
        /// Shows the question message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message box result.</returns>
        public static DialogResult ShowQuestionMessageBox(this IWin32Window owner, string message)
        {
            return ShowMessageBox(owner, message, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Shows the question message cancel.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message box result.</returns>
        public static DialogResult ShowQuestionMessageBoxCancel(string message)
        {
            return ShowQuestionMessageBoxCancel(null, message);
        }

        /// <summary>
        /// Shows the question message cancel.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        /// <returns>Message box result.</returns>
        public static DialogResult ShowQuestionMessageBoxCancel(this IWin32Window owner, string message)
        {
            return ShowMessageBox(owner, message, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Shows the warning message.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        public static void ShowWarningMessageBox(this IWin32Window owner, string message)
        {
            ShowMessageBox(owner, message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows the message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        /// <param name="button">The button.</param>
        /// <param name="image">The image.</param>
        /// <returns>Message box result.</returns>
        public static DialogResult ShowMessageBox(this IWin32Window owner, string message, MessageBoxButtons button, MessageBoxIcon image)
        {
            if (owner != null)
                return MessageBox.Show(owner, message, Extensions.AppTitle, button, image);
            else
                return MessageBox.Show(message, Extensions.AppTitle, button, image);
        }

        #endregion

        #region Data Grid View Methods

        /// <summary>
        /// Gets the check box value.
        /// </summary>
        /// <param name="grdView">The GRD view.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>True if succeeded; otherwise false</returns>
        public static bool GetCheckBoxValue(this DataGridView grdView, int rowIndex, int columnIndex)
        {
            DataGridViewCheckBoxCell cell = grdView.Rows[rowIndex].Cells[columnIndex] as DataGridViewCheckBoxCell;
            if (cell != null)
            {
                return (bool)cell.Value;
            }
            return false;
        }

        /// <summary>
        /// Sets the check box value.
        /// </summary>
        /// <param name="grdView">The GRD view.</param>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetCheckBoxValue(this DataGridView grdView, int rowIndex, int columnIndex, bool value)
        {
            DataGridViewCheckBoxCell cell = grdView.Rows[rowIndex].Cells[columnIndex] as DataGridViewCheckBoxCell;
            if (cell != null)
            {
                cell.Value = value;
            }
        }

        /// <summary>
        /// Checks the column header check box.
        /// </summary>
        /// <param name="grdView">The GRD view.</param>
        /// <param name="columnIndex">Index of the column.</param>
        public static void CheckColumnHeaderCheckBox(this DataGridView grdView, int columnIndex)
        {
            bool value = false;
            object tagValue = grdView.Columns[columnIndex].Tag;
            if (tagValue != null)
                bool.TryParse(tagValue.ToString(), out value);

            foreach (DataGridViewRow row in grdView.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[columnIndex] as DataGridViewCheckBoxCell;
                if (cell != null)
                {
                    cell.Value = !value;
                }
            }

            grdView.Columns[columnIndex].Tag = !value;
        }

        #endregion

        #region Control Methods
        /// <summary>
        /// Enables the disable.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        public static void EnableDisable(this Control control, bool enabled)
        {
            control.Enabled = enabled;
            control.BackColor = (enabled ? SystemColors.Window : SystemColors.ButtonFace);
        }
        #endregion

        #region Text Box Methods
        /// <summary>
        /// Shows the folder dialog box.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        public static void ShowFolderDialogBox(this TextBox textBox)
        {
            ShowFolderDialogBox(textBox, null);
        }

        /// <summary>
        /// Shows the folder dialog box.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        public static void ShowFolderDialogBox(this TextBox textBox, Func<string, string> formatResult)
        {
            string selectedPath = ShowFolderDialog(textBox.Text, "Browse...");
            if (!string.IsNullOrEmpty(selectedPath))
            {
                if (formatResult != null)
                    textBox.Text = formatResult(selectedPath);
                else
                    textBox.Text = selectedPath;
            }
        }

        /// <summary>
        /// Updates the text.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="text">The text.</param>
        public static void UpdateText(this TextBox textBox, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                textBox.Text = "";
            }
            else
            {
                if ((textBox.Text.Length + text.Length) < short.MaxValue)
                {
                    textBox.Text += text;
                }
                else
                {
                    textBox.Text = "";
                }
            }
            textBox.SelectionStart = textBox.Text.Length;
            textBox.ScrollToCaret();
            //textBox.Focus();
        }

        /// <summary>
        /// Selects the text and focus.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        public static void SelectTextAndFocus(this TextBox textBox)
        {
            textBox.SelectionStart = textBox.Text.Length;
            textBox.Focus();
        }

        #endregion

        #region Customized Message Box



        #endregion

        #region TreeView Members

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="text">The text.</param>
        /// <param name="imageIndex">Index of the image.</param>
        /// <returns>Created node.</returns>
        public static TreeNode AddNode(this TreeView owner, TreeNode parentNode, string text, int imageIndex)
        {
            TreeNode childNode = new TreeNode(text, imageIndex, imageIndex);
            if (parentNode != null) parentNode.Nodes.Add(childNode);
            else owner.Nodes.Add(childNode);
            childNode.ExpandAll();
            return childNode;
        }

        #endregion

        #region ImageList Members

        /// <summary>
        /// Adds the icon.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="imageKey">The image key.</param>
        /// <param name="icon">The icon.</param>
        /// <returns>Icon index.</returns>
        public static int AddIcon(this ImageList owner, string imageKey, Icon icon)
        {
            if (!owner.Images.ContainsKey(imageKey))
            {
                owner.Images.Add(imageKey, icon);
            }
            return owner.Images.IndexOfKey(imageKey);
        }

        #endregion

        #region Combo Box Methods
        public static void InitializeComboBox(this ComboBox source)
        {
            source.DisplayMember = "Text";
            source.ValueMember = "Value";
            source.Items.Clear();
        }

        public static ComboBoxItem<T> AddComboBoxItem<T>(this ComboBox source, string text, T value)
        {
            ComboBoxItem<T> item = new ComboBoxItem<T>(value, text);
            source.Items.Add(item);
            return item;
        }

        public static ComboBoxItem<T> SelectComboBoxItem<T>(this ComboBox source, T value)
        {
            ComboBoxItem<T> item = (from i in source.Items.OfType<ComboBoxItem<T>>()
                                    where i.Value.Equals(value) == true
                                    select i).FirstOrDefault();
            if (item != null)
            {
                source.SelectedItem = item;
            }
            return item;
        }

        public static ComboBoxItem<T> SelectComboBoxItem<T>(this ComboBox source, Func<T, bool> predicate)
        {
            ComboBoxItem<T> item = (from i in source.Items.OfType<ComboBoxItem<T>>()
                                    where predicate(i.Value) == true
                                    select i).FirstOrDefault();
            if (item != null)
            {
                source.SelectedItem = item;
            }
            return item;
        }
        #endregion

        #region Visual Studio Specific Members
        /// <summary>
        /// Determines whether [is in design mode].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is in design mode]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInDesignMode()
        {
            bool returnFlag = false;

#if DEBUG
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                returnFlag = true;
            }
            else if (Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV"))
            {
                returnFlag = true;
            }
#endif
            return returnFlag;

        }
        #endregion

        #region Asynchronous Progress Form Methods
#if ASYNC_FORM
        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="canCancel">if set to <c>true</c> [can cancel].</param>
        /// <param name="callback">The callback.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowAsyncDialog(this Form ownerForm,
            string formCaption, bool canCancel,
            AsyncWaitCallback callback)
        {
            return ShowAsyncDialog(ownerForm, formCaption, canCancel, callback, null, null);
        }

        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="canCancel">if set to <c>true</c> [can cancel].</param>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowAsyncDialog(this Form ownerForm,
            string formCaption, bool canCancel,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            AsyncProgressForm asyncForm = new AsyncProgressForm(formCaption, callback, finishedCallback, abortCallback);
            return asyncForm.ShowDialogExResultAndDestroy(ownerForm,
                (f) =>
                {
                    f.OwnerForm = ownerForm;
                    f.IsCancellable = canCancel;
                }, null);
        }

        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="method">The method.</param>
        /// <param name="args">The args.</param>
        public static void CrossThreadInvoke(this Form ownerForm, Delegate method, params object[] args)
        {
            if (ownerForm.InvokeRequired)
            {
                ownerForm.Invoke(method, args);
            }
            else
            {
                method.DynamicInvoke(args);
            }
        }
#endif
        #endregion        

        #region Graphics Methods

        /// <summary>
        /// Paints the flat border.
        /// </summary>
        /// <param name="g">The g.</param>
        public static void PaintFlatBorder(this Control owner, Graphics g, Color borderColor, int borderWidth)
        {
            Rectangle rc = owner.ClientRectangle;
            ControlPaint.DrawBorder(g, rc,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid,
                    borderColor, borderWidth, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Draws a rounded rectangle on a bitmap
        /// </summary>
        /// <param name="Image">Image to draw on</param>
        /// <param name="BoxColor">The color that the box should be</param>
        /// <param name="XPosition">The upper right corner's x position</param>
        /// <param name="YPosition">The upper right corner's y position</param>
        /// <param name="Height">Height of the box</param>
        /// <param name="Width">Width of the box</param>
        /// <param name="CornerRadius">Radius of the corners</param>
        /// <returns>The bitmap with the rounded box on it</returns>
        public static void DrawRoundedRectangle(this Graphics g, Color BoxColor, int XPosition, int YPosition,
             int Height, int Width, int CornerRadius)
        {
            using (Pen BoxPen = new Pen(BoxColor))
            {
                using (GraphicsPath Path = new GraphicsPath())
                {
                    Path.AddLine(XPosition + CornerRadius, YPosition, XPosition + Width - (CornerRadius * 2), YPosition);
                    Path.AddArc(XPosition + Width - (CornerRadius * 2), YPosition, CornerRadius * 2, CornerRadius * 2, 270, 90);
                    Path.AddLine(XPosition + Width, YPosition + CornerRadius, XPosition + Width, YPosition + Height - (CornerRadius * 2));
                    Path.AddArc(XPosition + Width - (CornerRadius * 2), YPosition + Height - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 0, 90);
                    Path.AddLine(XPosition + Width - (CornerRadius * 2), YPosition + Height, XPosition + CornerRadius, YPosition + Height);
                    Path.AddArc(XPosition, YPosition + Height - (CornerRadius * 2), CornerRadius * 2, CornerRadius * 2, 90, 90);
                    Path.AddLine(XPosition, YPosition + Height - (CornerRadius * 2), XPosition, YPosition + CornerRadius);
                    Path.AddArc(XPosition, YPosition, CornerRadius * 2, CornerRadius * 2, 180, 90);
                    Path.CloseFigure();
                    g.DrawPath(BoxPen, Path);
                }
            }
        }

        #endregion        
    }
}
