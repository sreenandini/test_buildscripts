using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Audit.BusinessClasses;
//using Audit.DBBuilder;
using Audit.Transport;
using System.Data.SqlClient;

namespace AuditViewer
{
    public partial class frmAuditViewer : Form
    {
        SqlConnection myConn = new SqlConnection("Server=10.2.100.162;UID=sa;Pwd=sa123;Initial Catalog=Exchange");

        //AuditViewerBusiness AVB = null;
        List<AuditModules> list;
        //AuditDataContext ADC = new AuditDataContext("Server=10.2.100.162;UID=sa;Pwd=sa123;Initial Catalog=Exchange");

        public frmAuditViewer()
        {
            InitializeComponent();

            //AVB = new AuditViewerBusiness(myConn);

            //list = AVB.GetModulesList();

            //var abc = from c in list
            //          select c;

            //cmbModule.DataSource = abc.ToArray();
            //cmbModule.DisplayMember = "Audit_Module_Name";
            //cmbModule.ValueMember = "Audit_Module_ID";

            //cmbModule.DataSource = list;
            //cmbModule.DisplayMember = "Audit_Module_Name";
            //cmbModule.ValueMember = "Audit_Module_ID";

            AuditViewerBusiness.CreateInstance("Server=10.2.100.162;UID=sa;Pwd=sa123;Initial Catalog=Exchange");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Audit_History objAH = new Audit_History();

            objAH.Audit_User_ID = 101;
            objAH.Audit_User_Name = "user1";
            //objAH.AuditModuleID =ModuleID.Enrollment; //not required
            objAH.AuditModuleName = ModuleName.Enrollment;
            objAH.EnterpriseModuleName = ModuleNameEnterprise.Settings;
            objAH.Audit_Screen_Name = "Screen Name";
            
            objAH.Audit_Slot = "LC0003";
            objAH.Audit_Field = "FieldName"; //can be enumerated
            objAH.Audit_Old_Vl = "old";
            objAH.Audit_New_Vl = "new";
            objAH.Audit_Desc = "Desc";
            objAH.AuditOperationType = OperationType.ADD;
            


            AuditViewerBusiness.InsertAuditData(objAH);

            

            //business.InsertAuditData(userID, sUserName, nmoduleId, sModuleName, sSlot, sAudField, sOldValue, sNewValue, sAudDesc);


            //dataGridView1.DataSource = AVB.GetAuditDetails(System.DateTime.Now.AddDays(-3), System.DateTime.Now.AddDays(3), ((int)ModuleID.Enrollment).ToString());
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
