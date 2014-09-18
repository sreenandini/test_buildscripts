using BMC.Common.ExceptionManagement;
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

namespace BMC.Presentation.POS
{
    /// <summary>
    /// Interaction logic for PlayerCardValidation.xaml
    /// </summary>
    public partial class PlayerCardValidation : Window
    {
         int  CardRequired=0;
         string PlayerId;
         public int Close = 0;
         public bool valid = false;
         public string dbPlayerCard = string.Empty;
         public string LocalPlayerCardNumber = string.Empty;
        public PlayerCardValidation()
        {
            InitializeComponent();
            this.txtPlayerCardNumber.Focus();
            this.txtPlayerCardNumber.Text = string.Empty;   
        }

        public PlayerCardValidation(int AnyCardRequired)
        {
            InitializeComponent();
            txtPlayerCardNumber.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, ShortcutDisable));
            this.txtPlayerCardNumber.Focus();
            this.txtPlayerCardNumber.Text = string.Empty;
            this.CardRequired = AnyCardRequired;
        }

        public PlayerCardValidation(int AnyCardRequired,string CardNumber)
        {
            InitializeComponent();
            txtPlayerCardNumber.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, ShortcutDisable));
            this.txtPlayerCardNumber.Focus();
            this.txtPlayerCardNumber.Text = string.Empty;
            this.CardRequired = AnyCardRequired;
            this.PlayerId = CardNumber;

            dbPlayerCard = ActualPlayerCard(PlayerId);
           
        }

        private string ActualPlayerCard(string PlayerID)
        {
            try
            {
                if (PlayerID.Trim().Length == 10)
                {
                    PlayerID = PlayerID.Remove(0, 1);
                    PlayerID=PlayerID.TrimStart('0');
                }
                else
                {
                    PlayerID.TrimStart('0');
                }
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
            return PlayerID;
        }


        private void ShortcutDisable(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlayerCardNumber.Text))
            {
                MessageBox.ShowBox("PlayerCardRedeem1", BMC_Icon.Warning);
                txtPlayerCardNumber.Text = string.Empty;
                txtPlayerCardNumber.Focus();
                return;
            }
            
            else
            {
                if (CardRequired == 2)
                {
                    valid = true;
                  
                    this.Close();
                }
                else if (CardRequired == 1)
                {
                    string LocalPlayerCardNumber = ActualPlayerCard(txtPlayerCardNumber.Text.Trim());
                    if (LocalPlayerCardNumber == dbPlayerCard)
                    {
                        valid = true;                    
                        this.Close();
                    }
                    else
                    {
                        valid = false;
                        MessageBox.ShowBox("PlayerCardRedeem5", BMC_Icon.Warning);
                        txtPlayerCardNumber.Text = string.Empty;
                        txtPlayerCardNumber.Focus();
                        return;
                    }
                }
            }
            
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close = 1;
            this.Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            this.txtPlayerCardNumber.Text = string.Empty;
            this.txtPlayerCardNumber.Focus();
        }

        private void txtPlayerCardNumber_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!BMC.Transport.Settings.OnScreenKeyboard)
                return;

            txtPlayerCardNumber.Text = DisplayNumberPad(txtPlayerCardNumber.Text);
            txtPlayerCardNumber.SelectionStart = txtPlayerCardNumber.Text.Length;
            txtPlayerCardNumber.Focus();
        }

        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind();

            try
            {

                ObjNumberpadWind.ValueText = keytext;
                ObjNumberpadWind.Owner = this;
                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    if (ObjNumberpadWind.ValueText == "")
                    {
                        strNumberPadText = "0";
                    }
                    else
                    {
                        strNumberPadText = ObjNumberpadWind.ValueText;
                    }
                }
            }
            catch (Exception ex)
            {
                strNumberPadText = ObjNumberpadWind.ValueText;
                ObjNumberpadWind.Close();
                ExceptionManager.Publish(ex);

            }

            return strNumberPadText;
        }

        private void txtPlayerCardNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;

            }

        }

    }
}
