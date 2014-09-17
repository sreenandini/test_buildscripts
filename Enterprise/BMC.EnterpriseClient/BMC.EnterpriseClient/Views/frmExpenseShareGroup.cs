using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using System.Data.SqlClient;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmExpenseShareGroup : GenericFormBase
    {
        public frmExpenseShareGroup()
        {
            InitializeComponent();
            setTagProperty();
        }

        private void setTagProperty()
        {
            this.btnClose.Tag = "Key_CloseCaption";
            this.Add.Tag = "Key_AddDot";
            this.clmComments.Tag = "Key_Comments";
            this.Delete.Tag = "Key_DeleteDot";
            this.Edit.Tag = "Key_EditDot";
            this.Tag = "Key_ExpenseShareGroup";
            this.clmPercentage.Tag = "Key_Percentage";
            this.clmSNo.Tag = "Key_SNo";
            this.clmSGName.Tag = "Key_ShareGroupName";

        }

        private void Add_Click(object sender, EventArgs e)
        {
            
            frmShare share = new frmShare();
            share.Show();

            return;
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            frmAddEditExpenseShare f1 = new frmAddEditExpenseShare();
            f1.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvShareGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lvShareGroup.Items.Clear();
            //SqlConnection MyConnection = null;
            //MyConnection = new SqlConnection(ConnectionString);
            //SqlDataAdapter MyDataAdapter = new SqlDataAdapter("rsp_GetProfitShare", MyConnection);
            //MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            //DataTable dt = new DataTable();
            //MyDataAdapter.Fill(dt);
            //dgvSharePercentage.DataSource = dt;
            //foreach (DataRow row in tbl.Rows)
            //{
            //    ListViewItem lv = new ListViewItem(row[0].ToString());
            //    lv.SubItems.Add(row[1].ToString());
            //    lv.SubItems.Add(row[2].ToString());
            //    lv.SubItems.Add(row[3].ToString());
            //    lvShareGroup.Items.Add(lv);
            //}
        }

        private void frmExpenseShareGroup_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }

       

            

        
        }

       
    }
