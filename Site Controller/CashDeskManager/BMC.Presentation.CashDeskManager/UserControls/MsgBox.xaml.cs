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


namespace BMC.Presentation.CashDeskManager
{
    /// <summary>
    /// Interaction logic for MsgBox.xaml
    /// </summary>
    public partial class MessageBox : Window
    {
        #region private static variables

        static MessageBox newMsgBox;

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
        public static System.Windows.Forms.DialogResult showBox(string content)
        {
            return showBox(content, BMC_Icon.Information, BMC_Button.OK);
        }

        /// <summary>
        /// Creates a custom messagebox
        /// </summary>
        /// <param name="content">Content to be displayed</param>
        /// <param name="RequiredIcon">Icon to be displayed</param>
        /// <returns>Dialogresult of the clicked button</returns>
        public static System.Windows.Forms.DialogResult showBox(string content, BMC_Icon RequiredIcon)
        {
            return showBox(content, RequiredIcon, BMC_Button.OK);
        }

        /// <summary>
        /// Creates a custom messagebox
        /// </summary>
        /// <param name="content">Content to be displayed</param>
        /// <param name="RequiredIcon">Icon to be displayed</param>
        /// <param name="RequiredButtons">Buttons to be displayed</param>
        /// <returns>Dialogresult of the clicked button</returns>
        public static System.Windows.Forms.DialogResult showBox(string content, BMC_Icon RequiredIcon, BMC_Button RequiredButtons)
        {
            newMsgBox = new MessageBox();
            newMsgBox.MsgContent.Text = content;
            //newMsgBox.tbHeader.Text = RequiredIcon.ToString();

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
                        newMsgBox.btnOK.Visibility = Visibility.Visible;
                        break;
                    }
                case BMC_Button.OKCancel:
                    {
                        newMsgBox.btnOK.Visibility = Visibility.Visible;
                        newMsgBox.btnCancel.Visibility = Visibility.Visible;
                        break;
                    }
                case BMC_Button.YesNo:
                    {
                        newMsgBox.btnYes.Visibility = Visibility.Visible;
                        newMsgBox.btnNo.Visibility = Visibility.Visible;
                        break;
                    }
                case BMC_Button.YesNoCancel:
                    {
                        newMsgBox.btnYes.Visibility = Visibility.Visible;
                        newMsgBox.btnNo.Visibility = Visibility.Visible;
                        newMsgBox.btnCancel.Visibility = Visibility.Visible;
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
            object Result = Enum.Parse(typeof(System.Windows.Forms.DialogResult), ((Button)sender).Content.ToString());
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