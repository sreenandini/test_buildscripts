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
using BMC.CoreLib;
using BMC.ExComms.Simulator.ViewModels;

namespace BMC.ExComms.Simulator.Views.Configuration.General
{
    /// <summary>
    /// Interaction logic for AxECashConfig.xaml
    /// </summary>
    public partial class AxECashConfig : UserControl
    {
        private readonly string DYN_MODULE_NAME = "AxECashConfig";

        public AxECashConfig()
        {
            InitializeComponent();
        }

        private void dgvEmployeeCards_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "dgvEmployeeCards_PreparingCellForEdit"))
            {
                try
                {
                    MainConfigurationViewModel vm = this.DataContext as MainConfigurationViewModel;
                    if (vm != null)
                    {
                        ComboBox cmbEditingElement = e.EditingElement as ComboBox;
                        if (cmbEditingElement != null)
                        {
                            BindingOperations.SetBinding(cmbEditingElement, ComboBox.ItemsSourceProperty,
                                new Binding() { Source = vm.GIMInformationsForCard });
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }
}
