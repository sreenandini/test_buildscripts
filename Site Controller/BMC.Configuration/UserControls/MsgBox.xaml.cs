#region File Description
//-------------------------------------------------------------------------------------
// File: MsgBox.Xaml.cs
//
// Namespace: BMC.CashDeskOperator
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


#endregion


namespace BMC.ExchangeConfig
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
        /// Creates a custom messagebox
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="RequiredIcon">Icon to be displayed</param>
        /// <param name="arguments"></param>
        /// <returns>Dialogresult of the clicked button</returns>
        public static System.Windows.Forms.DialogResult ShowBox(string messageID, BMC_Icon RequiredIcon, params string[] arguments)
        {
            return ShowBox(Application.Current.FindResource(messageID) as string, RequiredIcon, BMC_Button.OK, true, arguments);
        }

        public static System.Windows.Forms.DialogResult ShowBox(string messageID, BMC_Icon RequiredIcon, BMC_Button RequiredButtons, params string[] arguments)
        {
            return ShowBox(Application.Current.FindResource(messageID) as string, RequiredIcon, RequiredButtons, true, arguments);
        }

        /// <summary>
        /// Creates a custom messagebox
        /// </summary>
        /// <param name="content">Content to be displayed</param>
        /// <param name="RequiredIcon">Icon to be displayed</param>
        /// <param name="RequiredButtons">Buttons to be displayed</param>
        /// <param name="arguments"></param>
        /// <returns>Dialogresult of the clicked button</returns>
        private static System.Windows.Forms.DialogResult ShowBox(string messageID, BMC_Icon RequiredIcon, BMC_Button RequiredButtons, bool isPrivateFunction, params string[] arguments)
        {
            newMsgBox = new MessageBox();
            
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
            if (messageID.Length > 100) { newMsgBox.MsgContent.FontSize = 14.0; }            

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
                        break;
                    }
                case BMC_Button.OKCancel:
                    {
                        newMsgBox.OK.Visibility = Visibility.Visible;
                        newMsgBox.Cancel.Visibility = Visibility.Visible;
                        break;
                    }
                case BMC_Button.YesNo:
                    {
                        newMsgBox.Yes.Visibility = Visibility.Visible;
                        newMsgBox.No.Visibility = Visibility.Visible;
                        break;
                    }
                case BMC_Button.YesNoCancel:
                    {
                        newMsgBox.Yes.Visibility = Visibility.Visible;
                        newMsgBox.No.Visibility = Visibility.Visible;
                        newMsgBox.Cancel.Visibility = Visibility.Visible;
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

        public static System.Windows.Forms.DialogResult ShowText(string message, BMC_Icon RequiredIcon, params string[] arguments)
        {
            return ShowText(message, RequiredIcon, BMC_Button.OK, true, arguments);
        }

        public static System.Windows.Forms.DialogResult ShowText(string message, BMC_Icon RequiredIcon, BMC_Button RequiredButtons, params string[] arguments)
        {
            return ShowBox(message, RequiredIcon, RequiredButtons, true, arguments);
        }

        private static System.Windows.Forms.DialogResult ShowText(string message, BMC_Icon RequiredIcon, BMC_Button RequiredButtons, bool isPrivateFunction, params string[] arguments)
        {
            newMsgBox = new MessageBox();

            newMsgBox.MsgContent.Text = message;
            if (message.Length > 100) { newMsgBox.MsgContent.FontSize = 14.0; }

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
                        break;
                    }
                case BMC_Button.OKCancel:
                    {
                        newMsgBox.OK.Visibility = Visibility.Visible;
                        newMsgBox.Cancel.Visibility = Visibility.Visible;
                        break;
                    }
                case BMC_Button.YesNo:
                    {
                        newMsgBox.Yes.Visibility = Visibility.Visible;
                        newMsgBox.No.Visibility = Visibility.Visible;
                        break;
                    }
                case BMC_Button.YesNoCancel:
                    {
                        newMsgBox.Yes.Visibility = Visibility.Visible;
                        newMsgBox.No.Visibility = Visibility.Visible;
                        newMsgBox.Cancel.Visibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            newMsgBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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