using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmAddEditExpenseShare : Form
    {
        public frmAddEditExpenseShare()
        {
            InitializeComponent();
            setTagProperty();
        //    cmbShareHolderName.Enabled = true ;
        //    cmbShareHolderName.Text = "Government";
        //    cmbShareHolderName.Items.Add("Operator");
        //    cmbShareHolderName.Items.Add("Government");
        //    cmbShareHolderName.Items.Add("Retailer");
        //    cmbShareHolderName.Items.Add("Location1");
        //    cmbShareHolderName.Items.Add("Location2");
        //    txtSharePercentage.Value = 20;
        //    txtDescription.Text = "Location2 Profit Share Percentage";
        }

        private void setTagProperty()
        {
            this.Tag = "Key_AddEditProfitShare";
            this.btnCancel.Tag = "Key_Cancel";
            this.lblDescription.Tag = "Key_CommentsColon";
            this.lblSharePercentage.Tag = "Key_ProfitSharePercentageColon";
            this.btnSave.Tag = "Key_SaveCaption";
            this.lblShareHolderName.Tag = "Key_ShareHolderNameColon";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string ShareHolderName=null;
            DataSet ds = new DataSet();
            DataRow dr = ds.Tables["guest"].NewRow(); //create new row in dataset
            dr[0] = ShareHolderName;               //store value 
            // dr[1] = comboBox1.Text;
            ds.Tables[0].Rows.Add(dr);              //now add that row to dataset 
            //  .DataSource = ds.Tables[0];   //bind the gridview 
          //  //lvShareGroup.DataBind();
        }

        private void frmAddEditExpenseShare_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }

       
    }
}
