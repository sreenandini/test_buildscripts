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
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator;
using BMC.Transport;
using Audit.BusinessClasses;
using Audit.Transport;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CNGAEnroll.xaml
    /// </summary>
    public partial class CNGAEnroll : UserControl
    {

        private GetNGANameResult Current_NGAName = null;


        public CNGAEnroll()
        {
            InitializeComponent();
            cVaultEnroll.btnEnroll.Click += new RoutedEventHandler(Enroll_Click);
        }

        private void cmbNGAType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("CNGAEnroll->cmbNGAType_SelectionChanged", LogManager.enumLogLevel.Debug);
                if (e.AddedItems.Count > 0)
                {
                    GetNGATypesResult NGA_type = e.AddedItems[0] as GetNGATypesResult;
                    LoadNGANames(NGA_type.Name);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void btngetdetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("CNGAEnroll->btngetdetails_Click()", LogManager.enumLogLevel.Debug);
                if (cmbNGAName.Items.Count > 0)
                {
                    LoadVaultCassetteDetails(Current_NGAName.NGAID);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNGAType();
        }

        private void LoadNGAType()
        {
            try
            {
                LogManager.WriteLog("CNGAEnroll->LoadNGAType()", LogManager.enumLogLevel.Debug);
                List<GetNGATypesResult> lst_NGAType = Vault.CreateInstance().GetNGATypes();
                if (lst_NGAType != null)
                {
                    cmbNGAType.DataContext = lst_NGAType;
                    cmbNGAType.ItemsSource = lst_NGAType;
                    cmbNGAType.DisplayMemberPath = "Name";
                    cmbNGAType.SelectedValuePath = "Type_ID";
                    cmbNGAType.SelectedIndex = 0;
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void LoadNGANames(string NGAType)
        {
            try
            {
                LogManager.WriteLog("CNGAEnroll->LoadNGANames()", LogManager.enumLogLevel.Debug);
                List<GetNGANameResult> lst_NGAName = Vault.CreateInstance().GetNGAName(NGAType);

                if (lst_NGAName != null && lst_NGAName.Count > 0)
                {
                    Current_NGAName = lst_NGAName[0];
                    cmbNGAName.DataContext = lst_NGAName;
                    cmbNGAName.ItemsSource = lst_NGAName;
                    cmbNGAName.DisplayMemberPath = "NGAName";
                    cmbNGAName.SelectedValuePath = "NGAID";
                    cmbNGAName.SelectedIndex = 0;
                }
                else
                {
                    cmbNGAName.DataContext = null;
                    cmbNGAName.ItemsSource = null;
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        private void LoadVaultCassetteDetails(int VaultID)
        {
            try
            {
                LogManager.WriteLog("CNGAEnroll->LoadVaultCassetteDetails()", LogManager.enumLogLevel.Debug);
                List<NGA_GetCassetteDetailsResult> lst_NGADetails = Vault.CreateInstance().GetNGADetails(VaultID);

                if (lst_NGADetails != null)
                {

                    cVaultEnroll.DataContext = lst_NGADetails;

                    if (lst_NGADetails.Count > 0 && lst_NGADetails[0].Cassette_ID > 0)
                    {
                        cVaultEnroll.lst_CassetteDetails.ItemsSource = lst_NGADetails;
                    }

                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        void Enroll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("CNGAEnroll->Enroll_Click()", LogManager.enumLogLevel.Debug);
                if (cmbNGAType.SelectedItem != null)
                {

                    string Name = ((GetNGATypesResult)cmbNGAType.SelectedItem).Name;
                    if (Vault.CreateInstance().EnrollNGA(Current_NGAName.Installation_No, Security.SecurityHelper.CurrentUser.SecurityUserID))
                    {

                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            AuditModuleName = ModuleName.EnrollmentNGA,
                            Audit_Screen_Name = "NGA Enrollment",
                            Audit_Desc = "NGA Enrollment Completed For Type: " + cmbNGAType.Text + "; Name: " + cmbNGAName.Text + "; ID:" + Current_NGAName.NGAID + "; User ID :" + Security.SecurityHelper.CurrentUser.SecurityUserID,
                            AuditOperationType = OperationType.MODIFY,
                        });
                        LogManager.WriteLog("CNGAEnroll->Enrollment Completed Type: " + cmbNGAType.Text + " Name: " + cmbNGAName.Text + " ID:" + Current_NGAName.NGAID, LogManager.enumLogLevel.Debug);
                        if (Name.ToLower().Equals("vault"))
                        {
                            string strMsg = Application.Current.FindResource("Vault_MessageID12").ToString().Replace("@@@@@", Current_NGAName.NGAName);
                            MessageBox.ShowBox(strMsg, BMC_Icon.Information, true);
                            LoadNGANames(Name);
                            cVaultEnroll.DataContext = null;
                            cVaultEnroll.lst_CassetteDetails.ItemsSource = null;
                        }
                    }
                    else
                    {
                        if (Name.ToLower().Equals("vault"))
                        {
                            string strMsg = Application.Current.FindResource("Vault_MessageID13").ToString().Replace("@@@@@", Current_NGAName.NGAName);
                            MessageBox.ShowBox(strMsg, BMC_Icon.Error, true);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void cmbNGAName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox cmb_NGA = sender as ComboBox;
                GetNGANameResult NGA_Name = cmb_NGA.SelectedItem as GetNGANameResult;
                if (NGA_Name != null)
                {
                    cVaultEnroll.btnEnroll.Visibility = NGA_Name.IsEnrolled ? Visibility.Hidden : Visibility.Visible;
                    cVaultEnroll.txt_Enroll.Visibility = NGA_Name.IsEnrolled ? Visibility.Visible : Visibility.Hidden;
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

    }
}
