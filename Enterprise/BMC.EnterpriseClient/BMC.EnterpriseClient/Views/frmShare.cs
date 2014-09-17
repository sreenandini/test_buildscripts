using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common;
using BMC.CoreLib.Win32;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmShare : Form
    {
        public static string ConnectionString
        {
            get
            {
                return DatabaseHelper.GetConnectionString();
            }
        }
       // BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        public frmShare()
        {
            InitializeComponent();
            SetTagProperty();
            this.ResolveResources();
            try
            {
                SqlConnection MyConnection = null;
                MyConnection = new SqlConnection(ConnectionString);
                SqlDataAdapter MyDataAdapter = new SqlDataAdapter("rsp_GetProfitShare", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);
                dgvSharePercentage.DataSource = dt;
            }
            catch (Exception e)
            {
               // MessageBox.Show(this.GetResourceTextByKey(1, "MSG_EXCEPTION") + e);
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_EXCEPTION") + e,this.Text);
                
            }
          //  objDatawatcher = new Helpers.Datawatcher(this);

        }
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_Cancel";
            this.lblExpenseShareGroupName.Tag = "Key_ExpenseShareGroupNameColon";
            this.grpExpenseSharePercentage.Tag = "Key_ExpenseSharePercentage";
            this.btnSave.Tag = "Key_SaveCaption";
            this.Tag = "Key_Share";

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvSharePercentage_MouseDown(object sender, MouseEventArgs e)
        {
            {
                if (e.Button == MouseButtons.Right)
                {
                    var hti = dgvSharePercentage.HitTest(e.X, e.Y);
                    dgvSharePercentage.ClearSelection();
                    dgvSharePercentage.Rows[hti.RowIndex].Selected = true;


                //DataGridView.HitTestInfo hitTestInfo;
                //if (e.Button == MouseButtons.Right)
                //{
                    //hitTestInfo = dgvSharePercentage.HitTest(e.X, e.Y);
                    if (hti.Type == DataGridViewHitTestType.Cell && hti.RowIndex == 0)
                        contextMenuStrip1.Show(dgvSharePercentage, new Point(e.X, e.Y));
                    if (hti.Type == DataGridViewHitTestType.Cell && hti.RowIndex == 1)
                        contextMenuStrip1.Show(dgvSharePercentage, new Point(e.X, e.Y));
                }

            }

        }

        private void Add_Click(object sender, EventArgs e)
        {
            frmAddEditExpenseShare Ae = new frmAddEditExpenseShare();
            Ae.Show();
        }
    }
}

    

       