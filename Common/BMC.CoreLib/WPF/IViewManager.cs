// -----------------------------------------------------------------------
// <copyright file="IApplicationManager.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IViewManager : IDisposable
    {
        bool? Show(object child, ViewParameters parameters);
        object CreateView();

        void CloseApplication();
    }

    public class ViewParameters : DisposableObject
    {
        public ViewParameters() { }

        public bool IsDialog { get; set; }

        public ContentControl Parent { get; set; }

        public string Title { get; set; }

        public bool IsMaximized { get; set; }
    }

    public enum ViewMessageBoxButtons
    {
        // Summary:
        //     The message box contains an OK button.
        OK = 0,
        //
        // Summary:
        //     The message box contains OK and Cancel buttons.
        OKCancel = 1,
        //
        // Summary:
        //     The message box contains Abort, Retry, and Ignore buttons.
        AbortRetryIgnore = 2,
        //
        // Summary:
        //     The message box contains Yes, No, and Cancel buttons.
        YesNoCancel = 3,
        //
        // Summary:
        //     The message box contains Yes and No buttons.
        YesNo = 4,
        //
        // Summary:
        //     The message box contains Retry and Cancel buttons.
        RetryCancel = 5,
    }

    public enum ViewDialogResult
    {
        // Summary:
        //     Nothing is returned from the dialog box. This means that the modal dialog
        //     continues running.
        None = 0,
        //
        // Summary:
        //     The dialog box return value is OK (usually sent from a button labeled OK).
        OK = 1,
        //
        // Summary:
        //     The dialog box return value is Cancel (usually sent from a button labeled
        //     Cancel).
        Cancel = 2,
        //
        // Summary:
        //     The dialog box return value is Abort (usually sent from a button labeled
        //     Abort).
        Abort = 3,
        //
        // Summary:
        //     The dialog box return value is Retry (usually sent from a button labeled
        //     Retry).
        Retry = 4,
        //
        // Summary:
        //     The dialog box return value is Ignore (usually sent from a button labeled
        //     Ignore).
        Ignore = 5,
        //
        // Summary:
        //     The dialog box return value is Yes (usually sent from a button labeled Yes).
        Yes = 6,
        //
        // Summary:
        //     The dialog box return value is No (usually sent from a button labeled No).
        No = 7,
    }

    public enum ViewMessageBoxIcon
    {
        // Summary:
        //     The message box contain no symbols.
        None = 0,
        //
        // Summary:
        //     The message box contains a symbol consisting of white X in a circle with
        //     a red background.
        Error = 16,
        //
        // Summary:
        //     The message box contains a symbol consisting of a white X in a circle with
        //     a red background.
        Hand = 16,
        //
        // Summary:
        //     The message box contains a symbol consisting of white X in a circle with
        //     a red background.
        Stop = 16,
        //
        // Summary:
        //     The message box contains a symbol consisting of a question mark in a circle.
        //     The question-mark message icon is no longer recommended because it does not
        //     clearly represent a specific type of message and because the phrasing of
        //     a message as a question could apply to any message type. In addition, users
        //     can confuse the message symbol question mark with Help information. Therefore,
        //     do not use this question mark message symbol in your message boxes. The system
        //     continues to support its inclusion only for backward compatibility.
        Question = 32,
        //
        // Summary:
        //     The message box contains a symbol consisting of an exclamation point in a
        //     triangle with a yellow background.
        Exclamation = 48,
        //
        // Summary:
        //     The message box contains a symbol consisting of an exclamation point in a
        //     triangle with a yellow background.
        Warning = 48,
        //
        // Summary:
        //     The message box contains a symbol consisting of a lowercase letter i in a
        //     circle.
        Information = 64,
        //
        // Summary:
        //     The message box contains a symbol consisting of a lowercase letter i in a
        //     circle.
        Asterisk = 64,
    }

    public interface IMessageBoxViewManager : IDisposable
    {
        string Title { get; set; }

        ViewDialogResult ShowInfo(string message);
        ViewDialogResult ShowWarning(string message);
        ViewDialogResult ShowQuestion(string message);
        ViewDialogResult ShowError(string message);

        ViewDialogResult ShowInfo(string message, ViewMessageBoxButtons button);
        ViewDialogResult ShowWarning(string message, ViewMessageBoxButtons button);
        ViewDialogResult ShowQuestion(string message, ViewMessageBoxButtons button);
        ViewDialogResult ShowError(string message, ViewMessageBoxButtons button);

        ViewDialogResult ShowInfo(string message, string title);
        ViewDialogResult ShowWarning(string message, string title);
        ViewDialogResult ShowQuestion(string message, string title);
        ViewDialogResult ShowError(string message, string title);

        ViewDialogResult ShowInfo(string message, string title, ViewMessageBoxButtons button);
        ViewDialogResult ShowWarning(string message, string title, ViewMessageBoxButtons button);
        ViewDialogResult ShowQuestion(string message, string title, ViewMessageBoxButtons button);
        ViewDialogResult ShowError(string message, string title, ViewMessageBoxButtons button);

        ViewDialogResult Show(string message, string title, ViewMessageBoxButtons button, ViewMessageBoxIcon icon);
    }
}
