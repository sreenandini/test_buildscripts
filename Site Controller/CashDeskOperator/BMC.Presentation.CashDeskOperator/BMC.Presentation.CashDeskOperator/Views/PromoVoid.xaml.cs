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
using BMC.Business.CashDeskOperator;
using System.Data;
using BMC.Transport;
using System.Data.Linq;
using BMC.Common.LogManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using Audit.BusinessClasses;
using Audit.Transport;
namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for PromoVoid.xaml
    /// </summary>
    public partial class PromoVoid : UserControl
    {

        Promotional objprom = new Promotional();
       public ISingleResult<PromotionalClassVoidDetails> lstPromotionalVoid;
       int NoOfRecordsInPage;
        public PromoVoid()
        {
            InitializeComponent();
            NoOfRecordsInPage = Convert.ToInt32(Common.ConfigurationManagement.ConfigManager.Read("DisplayTISRecords"));
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                int voidtype = 1;
                lstPromotionalVoid = objprom.BGetPromoVoidDetails(voidtype, NoOfRecordsInPage);
                dgVoidGridView.DataContext = lstPromotionalVoid as ISingleResult<PromotionalClassVoidDetails>;
                if (lstPromotionalVoid == null) return;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnVoid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgVoidGridView.Items.Count > 0)
                {
                    int TicketId = (dgVoidGridView.SelectedItem as PromotionalClassVoidDetails).PromotionalID;
                    if (MessageBox.ShowBox("MessageID436", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                        return;
                    try
                    {
                        int result = objprom.BVoidStatusUpdate(TicketId, System.Environment.MachineName, Security.SecurityHelper.CurrentUser.User_No);
                        MessageBox.ShowBox("MessageID414", BMC_Icon.Information);
                        /* audit*/
                        string VoidEXdate = ((PromotionalClassVoidDetails)dgVoidGridView.SelectedItem).dtExpire.ToString();
                        string VoidPromationNumber = ((PromotionalClassVoidDetails)dgVoidGridView.SelectedItem).PromotionalID.ToString();
                        string voidNumberofTickets = ((PromotionalClassVoidDetails)dgVoidGridView.SelectedItem).TotalTickets.ToString();
                        string voidAmount = ((PromotionalClassVoidDetails)dgVoidGridView.SelectedItem).TotalTicketAmount.ToString();
                        string voiddate = DateTime.Now.ToString();
                        string promationalName = ((PromotionalClassVoidDetails)dgVoidGridView.SelectedItem).PromotionalName.ToString();
                        //Refresh the grid
                        int voidtype = 1;
                        dgVoidGridView.DataContext = null;
                        lstPromotionalVoid = objprom.BGetPromoVoidDetails(voidtype, NoOfRecordsInPage);
                        dgVoidGridView.DataContext = lstPromotionalVoid as ISingleResult<PromotionalClassVoidDetails>;
                        PromoVoidAudit(VoidPromationNumber, voidNumberofTickets, voidAmount, VoidEXdate, promationalName, voiddate);
                        if (lstPromotionalVoid == null) return;
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Promotional Void : " + ex.Message, LogManager.enumLogLevel.Error);
                    }

                }
            }


            catch (Exception ex)
            {
                LogManager.WriteLog("Promotional Void : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }
        public void PromoVoidAudit(string VoidPromationNumber, string voidNumberofTickets, string voidAmount, string VoidEXdate, string promationalName, string voiddate)
        {
            try
            {
                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {
                    AuditModuleName = ModuleName.Promotion,
                    Audit_Screen_Name = "Promo Void",
                    Audit_Desc = " Promotional Name: " + promationalName + ";No.Of Tickets Voided: " + voidNumberofTickets + ";Amount: " + voidAmount + "; VoidDate: " + voiddate + "; Expiry Date: " + VoidEXdate,
                    AuditOperationType = OperationType.ADD,
                    Audit_New_Vl = promationalName
                    
                });
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Promotional Void : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

   

    }
}
