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

namespace BMC.ExchangeConfig
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        private string status = "";
        public LoadingWindow()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                new BitmapImage(new Uri("pack://application:,,,/Resources/back.jpg", UriKind.Absolute));
            this.Background = myBrush;            
            //Brush newBrush = new SolidColorBrush { Color = new Color() {A=255, R = 30, G = 144, B = 255 } };
            //pgBar.Foreground = newBrush;
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                lblStatus.Content = status;
            }
        }
    }
}
