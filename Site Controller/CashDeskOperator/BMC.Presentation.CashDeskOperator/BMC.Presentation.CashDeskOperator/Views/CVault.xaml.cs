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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BMC.Common.ExceptionManagement;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CFillVault.xaml
    /// </summary>
    public partial class CVault : IDisposable
    {
        public CVault()
        {
            InitializeComponent();
        }

        #region IDisposable Members

        public void Dispose()
        {
          
        }

        #endregion

        

        private void chk_FillVault_Checked(object sender, RoutedEventArgs e)
        {

            try
            {
                pnlDropContent.Children.Clear();
                CFillVault declaration = new CFillVault();
                pnlDropContent.Children.Add(declaration);
                declaration.Margin = new Thickness(0);
            }
            catch(Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        private void chk_FillHistory_Checked(object sender, RoutedEventArgs e)
        {

            try
            {
                pnlDropContent.Children.Clear();
                CVaultFillHistory declaration = new CVaultFillHistory();
                pnlDropContent.Children.Add(declaration);
                declaration.Margin = new Thickness(0);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        private void chk_Declaration_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                pnlDropContent.Children.Clear();
                CVaultDeclaration v_declare = new CVaultDeclaration();
                pnlDropContent.Children.Add(v_declare);
                v_declare.Margin = new Thickness(0);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }


        private void chk_Events_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                pnlDropContent.Children.Clear();
                CVaultEvents v_declare = new CVaultEvents();
                pnlDropContent.Children.Add(v_declare);
                v_declare.Margin = new Thickness(0);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
    }
}
