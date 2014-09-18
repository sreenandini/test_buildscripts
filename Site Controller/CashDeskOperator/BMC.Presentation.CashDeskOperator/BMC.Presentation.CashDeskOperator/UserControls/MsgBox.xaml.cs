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
    public partial class MessageBox : Window
    {
        #region private static variables

        static MessageBox newMsgBox;
        public static Window parentOwner = null;
        public static Window childOwner = null;
        /// <summary>
        /// private variable containing the DialogResult info
        /// </summary>
        static System.Windows.Forms.DialogResult _DialogResult;

        #endregion

        #region Constructor
        public MessageBox()
        {
            this.InitializeComponent();
            // Insert code required on object creation below this point.
        }
        #endregion

        #region static methods

        /// <summary>
        /// Creates a custom messagebox
        /// </summary>
        /// <param name="content">Content to be displayed</param>
        /// <returns>Dialogresult of the clicked button</returns>
        public static System.Windows.Forms.DialogResult ShowBox(string messageID, params string[] arguments)
        {
            return ShowBox( Application.Current.FindResource(messageID) as string, BMC_Icon.Information, BMC_Button.OK, true, arguments);
        }

        /// <summary>
        /// Creates a custom messagebox whose arguments can be literals
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="RequiredIcon"></param>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult ShowBoxWithLiteralSubstitute(string messageID, BMC_Icon RequiredIcon, string argument)
        {
            string localizedValue = (string)Application.Current.FindResource(argument);
            return ShowBox(Application.Current.FindResource(messageID) as string, RequiredIcon, BMC_Button.OK, true, localizedValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isHardCodedMessage">Should be set to True in case the String is sent instead of the MessageID</param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult ShowBox(string message,bool isHardCodedMessage,params string[] arguments)
        {
            return ShowBox(message, BMC_Icon.Information, BMC_Button.OK, true, arguments);
        }

        /// <summary>
        /// Shows the box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="isHardCodedMessage">if set to <c>true</c> [is hard coded message].</param>
        /// <param name="height">The height.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public static System.Windows.Forms.DialogResult ShowBox(string message, bool isHardCodedMessage, int height, params string[] arguments)
        {
            return ShowBox(message, BMC_Icon.Information, BMC_Button.OK, true, height, arguments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sMessage"></param>
        /// <param name="RequiredIcon">Icon to be displayed</param>
        /// <param name="isHardCodedMessage">Should be set to True in case the String is sent instead of the MessageID</param>
        /// <param name="arguments"></param>
        /// <returns>Dialogresult of the clicked button</returns>
        public static System.Windows.Forms.DialogResult ShowBox(string sMessage, BMC_Icon RequiredIcon, bool isHardCodedMessage, params string[] arguments)
        {
            return ShowBox(sMessage, RequiredIcon, BMC_Button.OK, true, arguments);
        }



        /// <summary>
        /// Creates a custom messagebox
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="RequiredIcon">Icon to be displayed</param>
        /// <param name="arguments"></param>
        /// <returns>Dialogresult of the clicked button</returns>
        public static System.Windows.Forms.DialogResult ShowBox(string messageID, BMC_Icon RequiredIcon,params string[] arguments)
        {
            return ShowBox(Application.Current.FindResource(messageID) as string, RequiredIcon, BMC_Button.OK, true, arguments);
        }

        public static System.Windows.Forms.DialogResult ShowBox(string messageID, BMC_Icon RequiredIcon, BMC_Button RequiredButtons, params string[] arguments)
        {
            return ShowBox(Application.Current.FindResource(messageID) as string, RequiredIcon, RequiredButtons, true, arguments);
        }

        private static System.Windows.Forms.DialogResult ShowBox(string messageID, BMC_Icon RequiredIcon, BMC_Button RequiredButtons, bool isPrivateFunction, params string[] arguments)
        {
            return ShowBox(messageID, RequiredIcon, RequiredButtons, isPrivateFunction, 0, arguments);
        }

        /// <summary>
        /// Creates a custom messagebox
        /// </summary>
        /// <param name="content">Content to be displayed</param>
        /// <param name="RequiredIcon">Icon to be displayed</param>
        /// <param name="RequiredButtons">Buttons to be displayed</param>
        /// <param name="arguments"></param>
        /// <returns>Dialogresult of the clicked button</returns>
        private static System.Windows.Forms.DialogResult ShowBox(string messageID, BMC_Icon RequiredIcon, BMC_Button RequiredButtons, bool isPrivateFunction, int height, params string[] arguments)
        {
            newMsgBox = new MessageBox();

            if (height != 0 &&
                height > 250)
            {
                newMsgBox.LayoutRoot.Height = height;
            }

            var sval = messageID.Split(new string[] { "@@@@@@" }, StringSplitOptions.None);
            if (sval.Count() > 0)
            {
                string newmessage = string.Empty;
                for (int i = 0; i < sval.Count(); i++)
                {
                    newmessage += sval[i];
                    if (arguments.Count() - 1 >= i)
                        newmessage += arguments[i];
                }
                newmessage.Replace("@@@@@@", "");
                messageID = newmessage;
            }

            newMsgBox.MsgContent.Text = messageID;
            if (messageID.Length > 100) { newMsgBox.MsgContent.FontSize = 14.0; newMsgBox.MsgContent.Margin = new Thickness(0, 0, 0, 15); }
       

            switch (RequiredIcon)
            {
                case (BMC_Icon.Information):
                    {
                        newMsgBox.icnInformation.Opacity = 1.0;
                        break;
                    }
                case (BMC_Icon.Warning):
                    {
                        newMsgBox.icnWarning.Opacity = 1.0;
                        break;
                    }
                case BMC_Icon.Question:
                    {
                        newMsgBox.icnQuestion.Opacity = 1.0;
                        break;
                    }
                case BMC_Icon.Error:
                    {
                        newMsgBox.icnError.Opacity = 1.0;
                        break;
                    }
                default:
                    {
                        newMsgBox.icnError.Opacity = 1.0;
                        break;
                    }
            }

            switch (RequiredButtons)
            {
                case BMC_Button.OK:
                    {
                        newMsgBox.OK.Visibility = Visibility.Visible;
                        newMsgBox.OK.IsDefault = true;
                        break;
                    }
                case BMC_Button.OKCancel:
                    {
                        newMsgBox.OK.Visibility = Visibility.Visible;
                        newMsgBox.Cancel.Visibility = Visibility.Visible;
                        newMsgBox.OK.IsDefault = true;
                        break;
                    }
                case BMC_Button.YesNo:
                    {
                        newMsgBox.Yes.Visibility = Visibility.Visible;
                        newMsgBox.No.Visibility = Visibility.Visible;
                        newMsgBox.Yes.IsDefault = true;
                        break;
                    }
                case BMC_Button.YesNoCancel:
                    {
                        newMsgBox.Yes.Visibility = Visibility.Visible;
                        newMsgBox.No.Visibility = Visibility.Visible;
                        newMsgBox.Cancel.Visibility = Visibility.Visible;
                        newMsgBox.Yes.IsDefault = true;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            newMsgBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //
            if ((parentOwner != null) && (parentOwner.IsActive))
            {
                if ((childOwner != null) && (childOwner.IsActive))
                {
                    newMsgBox.Owner = childOwner;
                }
                else
                    newMsgBox.Owner = parentOwner;
            }
            else
            {
                WindowCollection WinScreens = Application.Current.Windows;
                foreach (Window screen in WinScreens)
                {
                    if ((screen.IsActive) && (screen != newMsgBox))
                        newMsgBox.Owner = screen;
                }
                
            }
            //
            if (newMsgBox.Owner == null)
                newMsgBox.Topmost = true;
            //
            newMsgBox.ShowDialog();
            return _DialogResult;
        }

        #endregion

        #region event handlers

        private void DragM(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object Result = Enum.Parse(typeof(System.Windows.Forms.DialogResult), ((Button)sender).Name.ToString());
            _DialogResult = (System.Windows.Forms.DialogResult)Result;
            this.Close();
        }
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
                        this.OK.Click -= (this.Button_Click);
                        this.Cancel.Click -= (this.Button_Click);
                        this.Yes.Click -= (this.Button_Click);
                        this.No.Click -= (this.Button_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("MessageBox objects are released successfully.");
                }
                disposed = true;
            }
        }

        ~MessageBox()
        {
            Dispose(false);
        }

        #endregion

    }

    #region Custom Enumerations
    public enum BMC_Icon
    {
        Error,
        Information,
        Question,
        Warning
    }

    public enum BMC_Button
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }
    #endregion
}