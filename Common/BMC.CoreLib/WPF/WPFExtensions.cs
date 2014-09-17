// -----------------------------------------------------------------------
// <copyright file="WPFExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BMC.CoreLib.WPF
{
#if NET4
    using BMC.CoreLib.IoC;
#endif
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.ComponentModel;
    using System.Collections;
    using System.Windows.Data;
    using BMC.CoreLib.Diagnostics;
    using BMC.CoreLib.Win32;
    using BMC.CoreLib.Concurrent;
    using System.Windows.Controls;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class WPFExtensions
    {
        public static Uri CreateLocalPackUri(string resourceName)
        {
            return new Uri(string.Format("pack://application:,,,/{0}", resourceName));
        }

        public static Uri CreateAssemblyPackUri(Assembly assembly, string resourceName)
        {
            //return new Uri(string.Format("pack://application:,,,/{0};Component/{1}", assembly.GetName().Name, resourceName), UriKind.Relative);
            return new Uri(string.Format("/{0};component/{1}", assembly.GetAssemblyName(), resourceName), UriKind.Relative);
        }

        public static ICollectionView CreateCollectionView(IList list)
        {
            return CreateCollectionView(list, true);
        }

        public static ICollectionView CreateCollectionView(IListSource list)
        {
            return CreateCollectionView(list.GetList(), true);
        }

        public static ICollectionView CreateCollectionView(IList list, bool moveToFirst)
        {
#if !SILVERLIGHT
            return new ListCollectionView(list);
#else
            return null;
#endif
        }

#if NET4
        public static ICollectionView CreateCollectionViewWithOrder<T>(IEnumerable<Lazy<T, IMEFOrderMetadata>> list)
        {
            return CreateCollectionViewWithOrder(list, true);
        }

        public static ICollectionView CreateCollectionViewWithOrder<T>(IEnumerable<Lazy<T, IMEFOrderMetadata>> list, bool moveToFirst)
        {
            ModuleProc PROC = new ModuleProc("", "CreateCollectionView<T, TMetadata>");
            IList result = null;

            try
            {
                result = (from i in list
                          orderby i.Metadata.Order
                          select i.Value).ToList();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return CreateCollectionView(result, moveToFirst);
        }
#endif

        #region Asynchronous Progress Control
        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="owner">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="callback">The callback.</param>
        /// <returns>Dialog result.</returns>
        public static System.Windows.Forms.DialogResult ShowAsyncDialog(this Control owner,
            string formCaption, IExecutorService executorService,
            AsyncWaitCallback callback)
        {
            return ShowAsyncDialog(owner, formCaption, executorService, callback, null, null);
        }

        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="owner">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>Dialog result.</returns>
        public static System.Windows.Forms.DialogResult ShowAsyncDialog(this Control owner,
            string formCaption, IExecutorService executorService,
            int minimum, int maximum,
            AsyncWaitCallback callback)
        {
            return ShowAsyncDialog(owner, formCaption, executorService, minimum, maximum, callback, null, null);
        }

        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="owner">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <returns>Dialog result.</returns>
        public static System.Windows.Forms.DialogResult ShowAsyncDialog(this Control owner,
            string formCaption, IExecutorService executorService,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            return ShowAsyncDialog(owner, formCaption, executorService, -1, -1, callback, finishedCallback, abortCallback);
        }

        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="owner">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <returns>Dialog result.</returns>
        public static System.Windows.Forms.DialogResult ShowAsyncDialog(this Control owner,
            string formCaption, IExecutorService executorService,
            int minimum, int maximum,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            return ShowAsyncDialog(owner, formCaption, executorService,
                minimum, maximum, false,
                callback, finishedCallback, abortCallback);
        }

        /// <summary>
        /// Shows the async dialog.
        /// </summary>
        /// <param name="owner">The owner form.</param>
        /// <param name="formCaption">The form caption.</param>
        /// <param name="executorService">if set to <c>true</c> [can cancel].</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <returns>Dialog result.</returns>
        public static System.Windows.Forms.DialogResult ShowAsyncDialog(this Control owner,
            string formCaption, IExecutorService executorService,
            int minimum, int maximum, bool isConsoleType,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            ModuleProc PROC = new ModuleProc("Win32Extensions", "ShowAsyncDialog");
            try
            {
                // parent window                
                if (ApplicationManager.IsInvokeRequired)
                {
                    ApplicationManager.SyncSend(new Action(() =>
                    {
                        ShowAsyncDialog(owner, formCaption, executorService,
                            minimum, maximum, isConsoleType,
                            callback, finishedCallback, abortCallback);
                    }));
                }
                else
                {
                    if (!isConsoleType)
                    {
                        WpfAsyncProgress asyncForm = new WpfAsyncProgress(formCaption, executorService, minimum, maximum,
                            callback, finishedCallback, abortCallback);                        
                        asyncForm.OwnerWindow = ApplicationManager.GetWindow(owner);
                        bool? result = asyncForm.ShowDialog();
                        return (result.IsValid() && result.SafeValue() ? 
                            System.Windows.Forms.DialogResult.OK : 
                            System.Windows.Forms.DialogResult.Cancel);
                    }
                    else
                    {
                        //AsyncConsoleDialogForm asyncForm = new AsyncConsoleDialogForm(formCaption, executorService, minimum, maximum,
                        //   callback, finishedCallback, abortCallback);
                        //return asyncForm.ShowDialogExResultAndDestroy(owner,
                        //    (f) =>
                        //    {
                        //        f.OwnerForm = owner;
                        //    }, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                //owner.ShowErrorMessageBox(ex.Message);
            }
            return System.Windows.Forms.DialogResult.None;
        }
        #endregion
    }
}
