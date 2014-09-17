using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BMC.CoreLib.WPF;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Win32;
using System.ComponentModel;

namespace BMC.CoreLib.WPF
{
    /// <summary>
    /// Interaction logic for WpfAsyncProgress.xaml
    /// </summary>
    public partial class WpfAsyncProgress : Window
    {
        public WpfAsyncProgress(string formCaption, IExecutorService executorService,
            int minimum, int maximum,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            InitializeComponent();
            this.Title = formCaption;
            axAsyncProgress.Initialize(formCaption, executorService, minimum, maximum, callback, finishedCallback, abortCallback, this, true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            axAsyncProgress.OwnerWindow = this;
            axAsyncProgress.StartAsync();
        }

        /// <summary>
        /// Gets or sets the owner form.
        /// </summary>
        /// <value>The owner form.</value>
        [Browsable(false)]
        public Window OwnerWindow
        {
            get
            {
                return this.Owner;
            }
            set
            {
                this.Owner = value;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            this.DialogResult = (axAsyncProgress.DialogResult == System.Windows.Forms.DialogResult.OK);
        }
    }
}
