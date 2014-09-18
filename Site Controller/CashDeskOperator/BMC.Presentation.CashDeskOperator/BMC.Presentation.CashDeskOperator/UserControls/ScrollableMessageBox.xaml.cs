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

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for ScrollableMessageBox.xaml
    /// </summary>
    public partial class ScrollableMessageBox : Window
    {

        public bool _isOk = false;
        /// <summary>
        /// Intialize a new instance of ScrollableMessageBox
        /// </summary>
        /// <param name="Caption">Header text of Message</param>
        /// <param name="Body">Body of Message</param>
        public ScrollableMessageBox(string Caption, string Body)
        {
            InitializeComponent();
            txt_MsgHeader.Text = Caption;
            txt_MsgBody.Text = Body;
          

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            _isOk = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
