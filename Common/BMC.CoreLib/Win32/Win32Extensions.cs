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
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.ExcelPackage;

namespace BMC.CoreLib.Win32
{
    /// <summary>
    /// Win32 Extensions
    /// </summary>
    public static class Win32Extensions
    {
        /// <summary>
        /// Initializes the <see cref="Extensions"/> class.
        /// </summary>
        static Win32Extensions()
        {
            AppTitle = "Bally Core Library";
        }

        /// <summary>
        /// Gets or sets the app title.
        /// </summary>
        /// <value>The app title.</value>
        public static string AppTitle { get; set; }

        #region Object Methods

        ///// <summary>
        ///// Toes the string safe.
        ///// </summary>
        ///// <param name="source">The source.</param>
        ///// <returns>String value.</returns>
        //public static string ToStringSafe(this object source)
        //{
        //    if (source is string)
        //    {
        //        if (!string.IsNullOrEmpty((string)source))
        //            return string.Empty;
        //    }

        //    if (source == null) return string.Empty;
        //    return source.ToString();
        //}

        public static string ToStringDate(this DateTime source)
        {
            return source.ToString("dd/MM/yyyy");
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

            if (result == DialogResult.OK)
            {
                if (afterAction != null) afterAction(dialogForm);
            }
            return result;
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
            DialogResult result = DialogResult.None;
            if (ownerWindow != null) result = dialog.ShowDialog(ownerWindow);
            else result = dialog.ShowDialog();
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
            ShowMessageBox(owner, message, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows the information message.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        public static void ShowInfoMessageBox(this IWin32Window owner, string message, string title)
        {
            ShowMessageBox(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Shows the error message.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        ///  <param name="title">The Title.</param>
        public static void ShowErrorMessageBox(this IWin32Window owner, string message,string title)
        {
            ShowMessageBox(owner, message,title, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Shows the question message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        /// <returns>Message box result.</returns>
        public static DialogResult ShowQuestionMessageBox(this IWin32Window owner, string message,string title)
        {
            return ShowMessageBox(owner, message,title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
        /// Shows the question message cancel.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        ///  <param name="title">The Title.</param>
        /// <returns>Message box result.</returns>
        public static DialogResult ShowQuestionMessageBoxCancel(this IWin32Window owner, string message,string title)
        {
            return ShowMessageBox(owner, message,title,MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
        /// Shows the warning message.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        /// <param name="title">The Title.</param>
        public static void ShowWarningMessageBox(this IWin32Window owner, string message,string title)
        {
            ShowMessageBox(owner, message,title,MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            return ShowMessageBox(owner, message, AppTitle, button, image);
        }

        /// <summary>
        /// Shows the message box.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        /// <param name="button">The button.</param>
        /// <param name="image">The image.</param>
        /// <returns>Message box result.</returns>
        public static DialogResult ShowMessageBox(this IWin32Window owner, string message, string title, MessageBoxButtons button, MessageBoxIcon image)
        {
            if (owner != null)
                return MessageBox.Show(owner, message, title, button, image);
            else
                return MessageBox.Show(message, title, button, image);
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

        #region Cross Thread Invocation
        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="method">The method.</param>
        /// <param name="args">The args.</param>
        public static void CrossThreadInvoke(this Form ownerForm, Delegate method, params object[] args)
        {
            if (ownerForm.IsDisposed) return;
            if (ownerForm.InvokeRequired)
            {
                ownerForm.Invoke(method, args);
            }
            else
            {
                method.DynamicInvoke(args);
            }
        }
        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="method">The method.</param>
        /// <param name="args">The args.</param>
        public static object CrossThreadInvokeFunc(this Form ownerForm, Delegate method, params object[] args)
        {
            if (ownerForm.IsDisposed) return null;
            if (ownerForm.InvokeRequired)
            {
                return ownerForm.Invoke(method, args);
            }
            else
            {
                return method.DynamicInvoke(args);
            }
        }
        #endregion

        #region Asynchronous Progress Form
        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="callback">The callback.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowAsyncDialog(this Form ownerForm,
            string formCaption, IExecutorService executorService,
            AsyncWaitCallback callback)
        {
            return ShowAsyncDialog(ownerForm, formCaption, executorService, callback, null, null);
        }

        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowAsyncDialog(this Form ownerForm,
            string formCaption, IExecutorService executorService,
            int minimum, int maximum,
            AsyncWaitCallback callback)
        {
            return ShowAsyncDialog(ownerForm, formCaption, executorService, minimum, maximum, callback, null, null);
        }

        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowAsyncDialog(this Form ownerForm,
            string formCaption, IExecutorService executorService,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            return ShowAsyncDialog(ownerForm, formCaption, executorService, -1, -1, callback, finishedCallback, abortCallback);
        }

        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowAsyncDialog(this Form ownerForm,
            string formCaption, IExecutorService executorService,
            int minimum, int maximum,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            return ShowAsyncDialog(ownerForm, formCaption, executorService,
                minimum, maximum, false,
                callback, finishedCallback, abortCallback);
        }

        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowAsyncDialog(this Form ownerForm,
            string formCaption, IExecutorService executorService,
            int minimum, int maximum, bool isConsoleType,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            ModuleProc PROC = new ModuleProc("Win32Extensions", "ShowAsyncDialog");
            try
            {
                if (ownerForm.InvokeRequired)
                {
                    ownerForm.Invoke(new Action(() =>
                    {
                        ShowAsyncDialog(ownerForm, formCaption, executorService,
                            minimum, maximum, isConsoleType,
                            callback, finishedCallback, abortCallback);
                    }));
                }
                else
                {
                    if (!isConsoleType)
                    {
                        AsyncDialogForm asyncForm = new AsyncDialogForm(formCaption, executorService, minimum, maximum,
                        callback, finishedCallback, abortCallback);
                        return asyncForm.ShowDialogExResultAndDestroy(ownerForm,
                            (f) =>
                            {
                                f.OwnerForm = ownerForm;
                            }, null);
                    }
                    else
                    {
                        AsyncConsoleDialogForm asyncForm = new AsyncConsoleDialogForm(formCaption, executorService, minimum, maximum,
                           callback, finishedCallback, abortCallback);
                        return asyncForm.ShowDialogExResultAndDestroy(ownerForm,
                            (f) =>
                            {
                                f.OwnerForm = ownerForm;
                            }, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                ownerForm.ShowErrorMessageBox(ex.Message);
            }
            return DialogResult.None;
        }

        public static DialogResult ShowAsyncDialogControl(this Form ownerForm,
            AxAsyncProgress2 progress, IExecutorService executorService,
            int minimum, int maximum,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            ModuleProc PROC = new ModuleProc("Win32Extensions", "ShowAsyncDialog");
            try
            {
                if (ownerForm.InvokeRequired)
                {
                    ownerForm.Invoke(new Action(() =>
                    {
                        ShowAsyncDialogControl(ownerForm, progress, executorService,
                            minimum, maximum,
                            callback, finishedCallback, abortCallback);
                    }));
                }
                else
                {
                    progress.Visible = true;
                    progress.Initialize(executorService, minimum, maximum,
                        callback, finishedCallback, abortCallback, ownerForm, false);
                    progress.StartAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                ownerForm.ShowErrorMessageBox(ex.Message);
            }
            return DialogResult.None;
        }
        #endregion

        #region Asynchronous Progress Form (Continuous)
        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="callback">The callback.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowAsyncDialogContinuous(this Form ownerForm,
            string formCaption, IExecutorService executorService,
            AsyncWaitCallback callback)
        {
            return ShowAsyncDialogContinuous(ownerForm, formCaption, executorService, callback, null, null);
        }

        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <returns>Dialog result.</returns>
        public static DialogResult ShowAsyncDialogContinuous(this Form ownerForm,
            string formCaption, IExecutorService executorService,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            ModuleProc PROC = new ModuleProc("Win32Extensions", "ShowAsyncDialog");
            try
            {
                if (ownerForm.InvokeRequired)
                {
                    ownerForm.Invoke(new Action(() =>
                    {
                        ShowAsyncDialog(ownerForm, formCaption, executorService,
                            callback, finishedCallback, abortCallback);
                    }));
                }
                else
                {
                    AsyncDialogContinuousForm asyncForm = new AsyncDialogContinuousForm(formCaption, executorService,
                        callback, finishedCallback, abortCallback);
                    return asyncForm.ShowDialogExResultAndDestroy(ownerForm,
                        (f) =>
                        {
                            f.OwnerForm = ownerForm;
                        }, null);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                ownerForm.ShowErrorMessageBox(ex.Message);
            }
            return DialogResult.None;
        }
        #endregion

        #region Remote
        public static Boolean IsRemoteSession
        {
            //This is just a friendly wrapper around the built-in way
            get
            {
                return System.Windows.Forms.SystemInformation.TerminalServerSession;
            }
        }
        #endregion

        #region Export To Exel (DataGridView/ListView)
        public static void HookUpExportControlDataToExcel<T>(this Form owner, Control control, KeyEventArgs args, ExternalWriteExcelHandler externalWrite, 
            bool writeHeaders, bool writeCheckItemsOnly, bool showMessageBoxAfterSave)
        {
            ModuleProc PROC = new ModuleProc("", "HookUpExportControlDataToExcel");

            try
            {
                if (!(control is DataGridView ||
                    control is ListView)) return;
                if (control.Tag != null) return;

                control.Tag = args;
                control.KeyDown += (s, e) =>
                {
                    Control ctl = s as Control;
                    KeyEventArgs te = ctl.Tag as KeyEventArgs;

                    if (e.Control == te.Control &&
                        e.Shift == te.Shift &&
                        e.KeyCode == te.KeyCode)
                    {
                        ExportControlDataToExcel<T>(owner, ctl, externalWrite, writeHeaders, writeCheckItemsOnly, showMessageBoxAfterSave);
                    }
                };
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static void ExportControlDataToExcel<T>(object owner, object ctl, ExternalWriteExcelHandler externalWrite, 
            bool writeHeaders, bool writeCheckItemsOnly, bool showMessageBoxAfterSave)
        {
            ModuleProc PROC = new ModuleProc("", "Method");
            Form ownerWin32 = owner as Form;
            System.Windows.Window ownerWPF = owner as System.Windows.Window;

            try
            {
                FileDialog dlg = ShowSaveFileDialog(ownerWin32, "Export to Excel...", "Excel Files (*.xlsx)|*.xlsx", "*.xlsx");
                if (dlg != null)
                {
                    string fileName = dlg.FileName;
                    using (IExecutorService srv = ExecutorServiceFactory.CreateExecutorService())
                    {
                        if (ownerWin32 != null)
                        {
                            ownerWin32.ShowAsyncDialog("Exporting to excel...", srv, (o) =>
                            {
                                ExportControlDataToExcel<T>(ownerWin32, fileName, o, ctl, externalWrite, writeHeaders, writeCheckItemsOnly, showMessageBoxAfterSave);
                            });
                        }
                        else if (ownerWPF != null)
                        {
                            BMC.CoreLib.WPF.WPFExtensions.ShowAsyncDialog(ownerWPF, "Exporting to excel...", srv, (o) =>
                            {
                                ExportControlDataToExcel<T>(null, fileName, o, ctl, externalWrite, writeHeaders, writeCheckItemsOnly, showMessageBoxAfterSave);
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private static void ExportControlDataToExcel<T>(IWin32Window owner, string fileName, IAsyncProgress o, object ctl,
           ExternalWriteExcelHandler externalWrite, bool writeHeaders, bool writeCheckItemsOnly, bool showMessageBoxAfterSave)
        {
            using (IExternalExcelApplication app = ExternalExcelApplicationFactory.Create())
            {
                app.Open(fileName, true);
                IExternalExcelSheet sheet = app.AddOrGet("Export");

                ExternalExcelWriteArgs<T> args = new ExternalExcelWriteArgs<T>()
                {
                    Progress = o as IAsyncProgress2,
                    Source = ctl,
                    WriteHeaders = writeHeaders,
                    WriteCheckedItems = writeCheckItemsOnly,
                    FormatInfo = (r) => { r.AutoFitColumns = true; },
                    ExternalWrite = externalWrite,
                };
                sheet.Write<T>(args);

                if (!o.ExecutorService.IsShutdown)
                {
                    bool result = app.Save();
                    if (showMessageBoxAfterSave)
                    {
                        o.CrossThreadInvoke(new Action(() =>
                        {
                            if (result)
                                ShowInfoMessageBox(owner, string.Format(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,1,"MSG_DATA_EXPORT_SUCCESS"), fileName));
                            else
                                ShowInfoMessageBox(owner, string.Format(BMC.Common.ResourceExtensions.GetResourceTextByKey(null,1,"MSG_UNABLE_TO_EXPORT_DATA"), fileName));
                        }));
                    }
                }
            }
        }

        #endregion
    }
}
