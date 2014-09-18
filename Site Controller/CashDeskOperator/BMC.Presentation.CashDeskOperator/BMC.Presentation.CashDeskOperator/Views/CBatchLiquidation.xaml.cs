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
using BMC.Business.CashDeskOperator;
using System.Data;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for CBatchLiquidation.xaml
    /// </summary>
    public partial class CBatchLiquidation : Page
    {
        public CBatchLiquidation()
        {
            InitializeComponent();
            LoadBatchNoCombo();

        }

        private void LoadBatchNoCombo()
        {
            Liquidation objLiquidationBiz = new Liquidation();   
            try
            {
                DataTable dtBatch = new DataTable();
                DataRow drBatch = dtBatch.NewRow();
                
                dtBatch = objLiquidationBiz.GetBatchNoForLiquidation();
                DataRow row = dtBatch.NewRow();


                row["Collection_Batch_Name"] = "Please select Batch";
                row["Collection_Batch_No"] = -1;
                dtBatch.Rows.InsertAt(row, 0);
                        
                if (dtBatch.Rows.Count > 0)
                {
                    cboBatchNo.ItemsSource=((System.ComponentModel.IListSource)dtBatch).GetList();
                }
                
               cboBatchNo.DataContext = dtBatch;
               cboBatchNo.DisplayMemberPath = "Collection_Batch_Name";
               cboBatchNo.SelectedValuePath = "Collection_Batch_No";
               cboBatchNo.SelectedValue = -1;
            
             
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnPerformLiquidation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Batch_Id=0;
                Batch_Id = Convert.ToInt32(cboBatchNo.SelectedValue.ToString());
                if (Batch_Id > 0)
                {
                    if (cboBatchNo.SelectedIndex == 1)
                    {
                        CLiquidationDetailsProfitShare liquidationDetailsPS = new CLiquidationDetailsProfitShare(Batch_Id);
                        liquidationDetailsPS.Owner = MessageBox.parentOwner;
                        liquidationDetailsPS.ShowDialog();
                        LoadBatchNoCombo();
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID498", BMC_Icon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID499", BMC_Icon.Information);
                    return;
                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

       

       
    

       

       
    }
}
