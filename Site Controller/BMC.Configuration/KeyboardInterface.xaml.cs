using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace BMC.ExchangeConfig
{
	public partial class KeyboardInterface
	{

        private string s_KeyboardString = string.Empty;
        private bool b_IsPwd = false;

        public string KeyString 
        { 
            get
            {
                return this.objKeyboard.CurrentValue;
            }
            set
            {
                this.objKeyboard.CurrentValue = value;
            
            }
        }

        public bool IsPwd 
        {
            get { return b_IsPwd; }
            set 
            { 
                b_IsPwd = value;
                this.objKeyboard.IsPasswordMode = b_IsPwd;
            }
        }
        
        public KeyboardInterface()
		{
			this.InitializeComponent();

         
            
			
			// Insert code required on object creation below this point.
		}

        private void CKeyboard_EnterClicked(object sender, EventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void objKeyboard_CancelClicked(object sender, EventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void objKeyboard_MoveKeyboard(object sender, EventArgs e)
        {
            this.DragMove();
        }


	}
}