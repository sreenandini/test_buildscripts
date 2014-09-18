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
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CUnlock.xaml
    /// </summary>
    public partial class CUnlock : UserControl
    {
        ApplicationLockObject oApplicationLockObject = null;
        List<LockDetails> olockDetails = null;
        public CUnlock()
        {
            InitializeComponent();
        }
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Unlock:Loading Unlock UI", LogManager.enumLogLevel.Debug);
                oApplicationLockObject = new ApplicationLockObject();
                ApplicationLock oType = oApplicationLockObject.GetLockTypes();
                List<ApplicationTypes> AppTypes = oType.ApplicationType;
                List<LockTypes> LockTypes = oType.LockType;
                AppTypes.Insert(0, new ApplicationTypes() { Lock_Application = "ALL" });
                LockTypes.Insert(0, new LockTypes() { Lock_Type = "ALL" });
                cmbLockApp.ItemsSource = AppTypes;
                cmbLockApp.DisplayMemberPath = "Lock_Application";
                cmbLockType.ItemsSource = LockTypes;
                cmbLockType.DisplayMemberPath = "Lock_Type";
                cmbLockApp.SelectedIndex = 0;
                cmbLockType.SelectedIndex = 0;
                UnlockDetails();
                
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }

        }
        
        private void btn_GetMessages_Click(object sender, RoutedEventArgs e)
        {
            try
            {              
                        RefreshList();
                        UnlockDetails();
                        if (olockDetails.Count<=0)
                        {
                            MessageBox.ShowBox("MessageID546", BMC_Icon.Information);
                            return;
                        }
                            
        }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btn_Unlock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strRequest = string.Empty;
                if (olockDetails != null)
                {
                    if (olockDetails.Count(x => x.IsSelected == true) > 0)
                    {
                        olockDetails.ForEach(x =>
                        {
                            if (x.IsSelected == true)
                                strRequest = strRequest + "," + x.Lock_ID.ToString();
                        });
                    }
                    else
                    {
                       
                        MessageBox.ShowBox("MessageID532", BMC_Icon.Information);
                    }
                    if (strRequest.Length < 1)
                        return;
                    LogManager.WriteLog("Unlock: btn_Unlock_Click UNlocking" + strRequest, LogManager.enumLogLevel.Debug);

                    oApplicationLockObject.UpdateAppLockState(strRequest);

                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.Unlock,
                        Audit_Screen_Name = "Unlock",
                        Audit_Desc = "Lock status for Lock_IDs : " + strRequest + " has be modified",
                        AuditOperationType = OperationType.MODIFY,
                        Audit_Field = "Lock_Active",
                        Audit_Old_Vl = "1",
                        Audit_New_Vl = "0"
                    });

                    RefreshList();
                }
                UnlockDetails();
            }
            catch (Exception Ex)
            {
                
                ExceptionManager.Publish(Ex);
            }
        }

        private void btn_SelectALL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strRequest = string.Empty;
                if (olockDetails != null)
                {
                    olockDetails.ForEach(x =>
                    {
                        x.IsSelected = true;
                    });
                    lvLockData.ItemsSource = olockDetails;
                    foreach (GridViewColumn gvCol in gvUnlock.Columns)
                    {
                        ResizeGridViewColumn(gvCol);
                        //gvUnlock.Columns[0].Width = 0;
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID533", BMC_Icon.Information);
                }
               
            }
            catch (Exception Ex)
            {
                
                ExceptionManager.Publish(Ex);
            }
        }

        private void btn_DeSelectALL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (olockDetails != null)
                {
                    if (olockDetails.Count(x => x.IsSelected == true) > 0)
                    {
                        olockDetails.ForEach(x =>
                        {
                            x.IsSelected = false;
                        });
                        lvLockData.ItemsSource = olockDetails;
                        foreach (GridViewColumn gvCol in gvUnlock.Columns)
                        {
                            ResizeGridViewColumn(gvCol);
                            //gvUnlock.Columns[0].Width = 0;
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID534", BMC_Icon.Information);
                    }
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }
        void UnlockDetails()
        {
            if (lvLockData.Items.Count == 0)
            {
                btn_DeSelectAll.Visibility = btn_SelectAll.Visibility = btn_Unlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                btn_DeSelectAll.Visibility = btn_SelectAll.Visibility = btn_Unlock.Visibility = Visibility.Visible;
            }
        }

        void RefreshList()
        {
            olockDetails = oApplicationLockObject.GetLockDetails(((ApplicationTypes)cmbLockApp.SelectedItem).Lock_Application, ((LockTypes)cmbLockType.SelectedItem).Lock_Type);
            lvLockData.ItemsSource = olockDetails;
            foreach (GridViewColumn gvCol in gvUnlock.Columns)
            {
                ResizeGridViewColumn(gvCol);
                //gvUnlock.Columns[0].Width = 0;
            }
        }

        private void ResizeGridViewColumn(GridViewColumn column)
        {
            if (double.IsNaN(column.Width))
            {
                column.Width = column.ActualWidth;
            }
            column.Width = double.NaN;
        }

    }
  
}
