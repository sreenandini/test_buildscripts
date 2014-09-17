using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.Win32
{
    /// <summary>
    /// Asynchronous Progress Form
    /// </summary>
    public partial class AsyncDialogContinuousForm : FormBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncProgressForm"/> class.
        /// </summary>
        public AsyncDialogContinuousForm(string formCaption, IExecutorService executorService,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            InitializeComponent();
            this.Text = formCaption;
            axAsyncProgress.Initialize(executorService, callback, finishedCallback, abortCallback, this, true);
        }

        /// <summary>
        /// Loads the changes.
        /// </summary>
        protected override void LoadChanges()
        {
            base.LoadChanges();
            axAsyncProgress.DialogOwner = this.OwnerForm;
            axAsyncProgress.IsCancellable = this.IsCancellable;
            axAsyncProgress.StartAsync();
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        protected override bool SaveChanges()
        {
            return base.SaveChanges();
        }

        /// <summary>
        /// Gets or sets the owner form.
        /// </summary>
        /// <value>The owner form.</value>
        [Browsable(false)]
        public Form OwnerForm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancellable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is cancellable; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsCancellable { get; set; }
    }
}
