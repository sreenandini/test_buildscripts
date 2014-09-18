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
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using System.Globalization;
using System.Reflection;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CAFTSetting.xaml
    /// </summary>
    /// 
    public partial class CAFTSetting : UserControl, IDisposable
    {
        IAFTSettingsDetails objAFTSettings = AFTSettingsBusinessObject.CreateInstance();
        List<Transport.AFTSetting> lstSetting = new List<AFTSetting>();
        ListBoxItem item = null;




        public CAFTSetting()
        {
            InitializeComponent();
            //lvAFTSettings.AddHandler(ListViewItem.SelectedEvent, new RoutedEventHandler(ItemSelected), true);
            //lvAFTSettings.AddHandler(ListBox.MouseDownEvent, new MouseButtonEventHandler(MouseClicked), true);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btnSaveSettings.Visibility = Visibility.Hidden;
            btnCancelSettings.Visibility = Visibility.Hidden;

            //lvAFTSettings.IsEnabled = false;

            LoadAFTSettings();
        }

        private void MouseClicked(object snder, MouseButtonEventArgs e)
        {
            // item = lvAFTSettings.ItemContainerGenerator.ContainerFromItem(lvAFTSettings.SelectedItem) as ListViewItem;

            //DataTemplateSelector datatemplate = item.
            //DataTemplate temp = datatemplate.SelectTemplate(item, null);


            //AFTSetting mysetting = item.Content as AFTSetting;
            //if (lstSetting.Contains(mysetting))
            //{
            //    lstSetting.Remove(mysetting);
            //}
            //lstSetting.Add(mysetting);
        }

        private void ItemSelected(object sender, RoutedEventArgs e)
        {

            ListViewItem lvi = e.OriginalSource as ListViewItem;
            // add your code here

        }

        private void LoadAFTSettings()
        {
            try
            {
                DataTable dtDenoms = objAFTSettings.GetDenoms();
                DataRow oDr = dtDenoms.NewRow();
                //oDr["Denom"] = Application.Current.FindResource("CAFTSetting_xaml_cmbDenom") as String;
                oDr["DenomText"] = Application.Current.FindResource("CAFTSetting_xaml_cmbDenom") as String;
                dtDenoms.Rows.InsertAt( oDr,0);  
                cmbDenom.ItemsSource = ((System.ComponentModel.IListSource)dtDenoms).GetList();
                cmbDenom.DataContext = dtDenoms.DefaultView;
                cmbDenom.DisplayMemberPath = "DenomText";
                cmbDenom.SelectedValuePath = "Denom";
                cmbDenom.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex); 
            }
           
        }



        private void btnCancelSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnCancelSettings.IsEnabled = false;
                System.Windows.Forms.DialogResult dr = MessageBox.ShowBox("MessageID293", BMC_Icon.Question, BMC_Button.YesNo);
                if (dr.ToString() == "Yes")
                {
                    LoadAFTSettings();
                }
            }
            finally
            {
                btnCancelSettings.IsEnabled = true;
            }
        }

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnSaveSettings.IsEnabled = false;
                AFTSetting setting = null;
                lstSetting = new List<AFTSetting>();
                ItemContainerGenerator generator = this.lvAFTSettings.ItemContainerGenerator;
                for (int i = 0; i < lvAFTSettings.Items.Count; i++)
                {
                    ListViewItem selectedItem = (ListViewItem)generator.ContainerFromIndex(i);


                    CheckBox chkNotify = GetDescendantByType(selectedItem, typeof(CheckBox), "chkActive") as CheckBox;
                    if (chkNotify != null
                        )
                    {
                        setting = new AFTSetting();
                        setting.IsActive = (bool)chkNotify.IsChecked;
                        setting.Value = (((bool)chkNotify.IsChecked)) == true ? "1" : "0";
                        setting.IsCheckBox = true;
                    }
                    else
                    {
                        //Try to find others:
                        TextBox tbFind = GetDescendantByType(selectedItem, typeof(TextBox), "txtActive") as TextBox;
                        if (tbFind != null)
                        {
                            setting = new AFTSetting();
                            setting.Value = tbFind.Text;
                            setting.IsCheckBox = false;
                        }
                    }
                    setting.Name = ((AFTSetting)selectedItem.Content).Name;

                    lstSetting.Add(setting);
                }

                System.Windows.Forms.DialogResult dr = MessageBox.ShowBox("MessageID290", BMC_Icon.Question, BMC_Button.YesNo);
                if (dr.ToString() == "Yes")
                {
                    if (objAFTSettings.SaveAFTSettings(lstSetting))
                    {
                        MessageBox.ShowBox("MessageID291", BMC_Icon.Information, BMC_Button.OK);
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID292", BMC_Icon.Information, BMC_Button.OK);
                    }
                }
            }
            finally
            {
                btnSaveSettings.IsEnabled = true;
            }
        }

        public static Visual GetDescendantByType(Visual element, Type type, string name)
        {
            if (element == null) return null;
            if (element.GetType() == type)
            {
                FrameworkElement fe = element as FrameworkElement;
                if (fe != null)
                {
                    if (fe.Name == name)
                    {
                        return fe;
                    }
                }
            }
            Visual foundElement = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();
            for (int i = 0;
                i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type, name);
                if (foundElement != null)
                    break;
            }
            return foundElement;
        }

        private void cmbDenom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbDenom.SelectedIndex > 0)
                {
                    List<Transport.AFTSetting> GetSettings = objAFTSettings.GetAFTSettings(Convert.ToInt32(cmbDenom.SelectedValue));
                    lvAFTSettings.ItemsSource = GetSettings;
                }
                else
                {
                    lvAFTSettings.ItemsSource = null;
                }
            }
            catch(Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }



        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.btnSaveSettings.Click -= (this.btnSaveSettings_Click);
                        this.btnCancelSettings.Click -= (this.btnCancelSettings_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CAFTSetting objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CAFTSetting"/> is reclaimed by garbage collection.
        /// </summary>
        ~CAFTSetting()
        {
            Dispose(false);
        }

        #endregion
    }

    public class StringValidationRule : ValidationRule
    {
        public string ErrorMessage
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value,
            CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            string inputString = (value ?? string.Empty).ToString();
            if (string.IsNullOrEmpty(inputString))
            {
                result = new ValidationResult(false, this.ErrorMessage);
            }
            return result;
        }
    }


    public class settingsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CheckBoxTemplate { get; set; }
        public DataTemplate TextBoxTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            AFTSetting setting = item as AFTSetting;
            if (setting.IsCheckBox)
            {
                return CheckBoxTemplate;
            }
            return TextBoxTemplate;
        }
    }

}