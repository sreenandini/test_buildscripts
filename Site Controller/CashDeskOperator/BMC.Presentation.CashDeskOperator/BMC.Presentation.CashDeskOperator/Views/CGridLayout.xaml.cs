using BMC.Common.ExceptionManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml;



namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CGridLayout.xaml
    /// </summary>
    public partial class CGridLayout : Window
    {
        //List<XElement> Items;
        Action<object, SelectionChangedEventArgs> _listSelectionChanged;
        public string  Result = null;
        bool _LoadComplete = false;
        List<XmlElement> _lstitemsSource=null;
        public XmlElement SelectedItem  = null;
        public CGridLayout()
        {
            InitializeComponent();
        }

        public CGridLayout(List<XmlElement> lstitems,  Action<object, SelectionChangedEventArgs> listSelectionChanged):this()
        {
            try
            {
                _listSelectionChanged = listSelectionChanged;
                lstLeftPane.ItemsSource = lstitems;
                _lstitemsSource = lstitems;
            }
            catch (Exception Ex)
            {
                
                ExceptionManager.Publish(Ex);
            }
        }

        private void lstLeftPane_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (_LoadComplete)
                {

                    this.Result = ((XmlElement)((ListBox)sender).SelectedItem).Attributes["ValueName"].Value;
                    this.SelectedItem = ((XmlElement)((ListBox)sender).SelectedItem);
                    this.DialogResult=true;
                   
                }
                _LoadComplete = true;
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception Ex)
            {
                
                ExceptionManager.Publish(Ex);
            }
        }




        

    }
}
