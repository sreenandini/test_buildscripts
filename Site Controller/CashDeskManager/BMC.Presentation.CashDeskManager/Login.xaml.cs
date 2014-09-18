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
using System.Windows.Shapes;
using BMC.Common.ExceptionManagement;

namespace BMC.Presentation.CashDeskManager
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string s_KeyText = string.Empty;
        public Login()
        {
            InitializeComponent();
        }

        private void CheckConnectionString()
        {
            try
            {
                //BMC.CashDeskOperator objCashdesk = new CashDeskOperator();
                //DataTable dtUser = objCashdesk.GetUserRoles("", "");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Connectionstring Not Found.")
                {
                    /// BMC.Presentation.CConfiguration objConfig = new CConfiguration();
                    // objConfig.ShowDialog();
                }
            }
        }


        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                s_KeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        private void txtUname_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtUname.Text = DisplayKeyboard(txtUname.Text, string.Empty);
            txtUname.SelectionStart = txtUname.Text.Length;
        }

        private void txtPwd_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtPWD.Password = DisplayKeyboard(string.Empty, "Pwd");
            txtPWD.SelectAll();
        }


        private string DisplayKeyboard(string KeyText, string type)
        {
            s_KeyText = "";
            KeyboardInterface objKeyboard = new KeyboardInterface();
            if (type == "Pwd")
            {
                objKeyboard.IsPwd = true;
            }
            objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
            objKeyboard.KeyString = KeyText;
            objKeyboard.Top = this.Top + this.Height - objKeyboard.Height;
            objKeyboard.Left = this.Left + this.Width / 2 - objKeyboard.Width / 2;
            objKeyboard.ShowDialog();
            return s_KeyText;
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool bValid = false;

                //if (txtUname.Text.ToUpper() == "CASH" && txtPWD.Password.ToUpper() == "DESK")
                //{
                //    MainScreen objMainScreen = new MainScreen();
                //    objMainScreen.UserName = txtUname.Text;
                //    objMainScreen.Show();
                //    this.Hide();
                //}
                //else
                //{
                //    // bValid = Checkuser(txtUname.Text, txtPWD.Password);
                //    if (bValid)
                //    {
                //        MainScreen objMainScreen = new MainScreen();
                //        objMainScreen.UserName = txtUname.Text;
                //        objMainScreen.Show();
                //        this.Hide();
                //    }
                //    else
                //    {
                //        MessageBox.showBox("Invalid Login. Please try again.");
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.showBox("Invalid Login. Please try again.");
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
