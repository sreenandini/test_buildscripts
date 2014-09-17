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

namespace BMC.ExchangeConfig
{
	/// <summary>
	/// Interaction logic for NumberPadWind.xaml
	/// </summary>
	public partial class NumberPadWind : Window
	{

        private bool _isPlayerClub;
        public bool isPlayerClub
        {
            get { 
                return _isPlayerClub; 
            } 
            set
            { 
                _isPlayerClub = value;
                ucTicketEntry.isPlayerClub = value;
            } 
        }

        
        public NumberPadWind()
		{
			this.InitializeComponent();
            isPlayerClub = false;
		// Insert code required on object creation below this point.
		}

        private void Drag_Event(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CTicketEntry_EnterClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void CTicketEntry_CancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        public string ValueText 
        { 
            get
            {
                return ucTicketEntry.ValueText;
            }
            set
            {
                ucTicketEntry.ValueText = value;
             }
        }


	}
}