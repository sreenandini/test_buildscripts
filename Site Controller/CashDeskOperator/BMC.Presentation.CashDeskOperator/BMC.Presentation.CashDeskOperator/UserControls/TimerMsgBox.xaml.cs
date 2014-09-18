#region File Description
//-------------------------------------------------------------------------------------
// File: MsgBox.Xaml.cs
//
// Namespace: BMC.Presentation
//
// Description: MessageBox control logic
//
// Copyright: Bally Technologies Inc. 2008
//
// Revision History:
//----------------------------------------------------------------------------------------
// Date				    Engineer			        Description
// ------------------------------------------------------------------------------------------
// 22/10/2008          Balasubramanyam.G        First version. Limited functionality
// 12/11/2008          Balasubramanyam.G        Added dialogresult and additional icon types
//-------------------------------------------------------------------------------------------
#endregion

#region Namespace declaration

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;

#endregion


namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for MsgBox.xaml
    /// </summary>
    public partial class TimerMessageBox : Window
    {
        #region private static variables

        static TimerMessageBox newMsgBox;

        /// <summary>
        /// private variable containing the DialogResult info
        /// </summary>
        static string _DialogResult="NO";

        #endregion

        #region Constructor
        public TimerMessageBox()
        {
            this.InitializeComponent();
            // Insert code required on object creation below this point.
            int nSeconds=10;
            string sMessage=Application.Current.FindResource("MessageID325")  as string;
            MsgContent.Text = string.Format(sMessage, nSeconds.ToString());
            var timerLogoff = new System.Windows.Forms.Timer { Interval = 1000, Enabled = true };
            timerLogoff.Tick += (object sender, EventArgs e) =>
            {
                MsgContent.Text = string.Format(sMessage, nSeconds.ToString());
                if (nSeconds == 0)
                {
                    _DialogResult = "NO";
                    timerLogoff.Stop();
                    Hide();
                }
                nSeconds--;
            };
        }
        #endregion

       
        public static string ShowBox()
        {
            newMsgBox = new TimerMessageBox();
            newMsgBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newMsgBox.ShowDialog();
            return _DialogResult;
        }
        

        #region event handlers

        private void DragM(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            _DialogResult = "YES";
            Hide();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            _DialogResult = "NO";
            Hide();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    object Result = Enum.Parse(typeof(System.Windows.Forms.DialogResult), ((Button)sender).Name.ToString());
        //    _DialogResult = (System.Windows.Forms.DialogResult)Result;
        //    this.Close();
        //}
        #endregion

        #region IDisposable Members

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.btnYes.Click -= (this.btnYes_Click);
                        this.btnNo.Click -= (this.btnNo_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("TimerMsgBox objects are released successfully.");
                }
                disposed = true;
            }
        }

        ~TimerMessageBox()
        {
            Dispose(false);
        }

        #endregion

    }

 
}