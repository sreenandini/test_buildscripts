using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BMC.Business.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport;
using BMC.DBInterface.CashDeskOperator;
using System.Data;
using System.Windows.Controls;
using System.Windows.Input;
using BMC.CashDeskOperator.BusinessObjects;
using System.Windows.Documents;
using System.ComponentModel;
using BMC.Presentation.POS.Views;
using BMC.Presentation.POS.Helper_classes;
using Audit.BusinessClasses;
using Audit.Transport;


namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CAftSync.xaml
    /// </summary>
    public partial class CMachineEnableDisable : UserControl
    {
        EnableDisableMachineBiz _EnableMachineBiz = null;
        string BarPosNumber;
        List<EnableDisableMachine> _ListActiveMachines;
        bool _IsEnabled;

        public CMachineEnableDisable()
        {
            InitializeComponent();            
        }
       
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {   
                _EnableMachineBiz = EnableDisableMachineBiz.CreateInstance();               
                lstMachineDetails.Items.Clear();
                _ListActiveMachines = _EnableMachineBiz.GetActiveMachine().ToList();
                lstMachineDetails.ItemsSource = _ListActiveMachines;
                BarPosNumber = _ListActiveMachines[0].BarPosNumber;
            }           
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnEnable_Click(object sender, RoutedEventArgs e)
        {
           try
            {                
                IsEnabled(true);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
     
        private void bttnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _ListActiveMachines.ForEach(x => x.IsSelected = true);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void bttnDeSelectAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _ListActiveMachines.ForEach(x => x.IsSelected = false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnDisable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsEnabled(false);
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void IsEnabled(bool _IsEnabled)
        {
            try
            {

                if (_ListActiveMachines != null)
                {
                    if (_ListActiveMachines.Count(x => x.IsSelected == true) > 0)
                    {
                        _ListActiveMachines.ForEach(x =>
                        {
                            if (x.IsSelected == true)
                            {
                                if (_IsEnabled == true)
                                {
                                    _EnableMachineBiz.UpdateBarPositionMachine(x.BarPosNumber, true, true, 0);
                                    x.Status = "Enabled";
                                    x.Message = "Active";
                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        AuditModuleName = ModuleName.EnableDisableMachine,
                                        Audit_Screen_Name = "EnableDisableMachine",
                                        Audit_Desc = "BarPosNumber-" + x.BarPosNumber + "-" + x.Status + "",
                                        AuditOperationType = OperationType.MODIFY,
                                        Audit_Field = "BarPosNumber",
                                        Audit_New_Vl = x.BarPosNumber,
                                        Audit_Slot = string.Empty
                                    });
                                }

                                else
                                {
                                    _EnableMachineBiz.UpdateBarPositionMachine(x.BarPosNumber, true, false, 1);
                                    x.Status = "Disabled";
                                    x.Message = "DeActive";
                                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                    {
                                        AuditModuleName = ModuleName.EnableDisableMachine,
                                        Audit_Screen_Name = "EnableDisableMachine",
                                         Audit_Desc = "BarPosNumber-" + x.BarPosNumber + "-" + x.Status + "",
                                        AuditOperationType = OperationType.MODIFY,
                                        Audit_Field = "BarPosNumber",
                                        Audit_New_Vl = x.BarPosNumber,
                                        Audit_Slot = string.Empty
                                    });


                                }
                            }
                        });
                    }
                    else
                    {
                        MessageBox.ShowBox("Please select atleast a single BarPosition", true, "");
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
    
}
