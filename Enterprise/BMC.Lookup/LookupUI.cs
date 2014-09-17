using System;
using System.Windows.Forms;
using BMC.Common.Utilities;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Lookup;
using System.Collections;

namespace MachineMaintenance
{
    public partial class LookupUI : Form
    {
        readonly LookupManager _lookupManager;
        private readonly LookupObject _codeMaster;

        public LookupUI(string lookupCode)
        {
            InitializeComponent();
            lblParent.Text = "Maintenance Category";
            lblParent.Visible = false;
            cboParent.Visible = false;
            
            try
            {
                _lookupManager = new LookupManager(DatabaseHelper.GetConnectionString());
                _codeMaster = _lookupManager.GetCodeDescription(lookupCode);

                if (_codeMaster.parentID != null)
                {
                    lblParent.Visible = true;
                    cboParent.Visible = true;
                }

                lblHeader.Text = "Manage " + _codeMaster.DisplayText;
                lblItem.Text = _codeMaster.DisplayText;
                Text = _codeMaster.DisplayText;
            }
            catch 
            {
                
            }
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void LookupUI_Load(object sender, EventArgs e)
        {
            if (_codeMaster.parentID != null)
            {

                cboParent.DataSource = _lookupManager.GetLookupObject(_lookupManager.GetCodeDescription((int)_codeMaster.parentID).CodeValue);
                cboParent.DisplayMember = "DisplayText";
                cboParent.ValueMember = "CodeValue";
                return;
            }
            SetDataSource(null);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            int recordCount;
            if (string.IsNullOrEmpty(txtValue.Text))
            {
                MessageBox.Show("Item Value Cannot be empty. Please enter a valid Item name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            recordCount = dgMain.RowCount;
            if (cboParent.DataSource == null)
            {
                _lookupManager.InsertLookupObject(_codeMaster.CodeValue, txtValue.Text, null);
                SetDataSource(null);
                if (dgMain.RowCount > recordCount) { AuditRecord(OperationType.ADD, txtValue.Text); }
            }
            else
            {
                _lookupManager.InsertLookupObject(_codeMaster.CodeValue, txtValue.Text, ((LookupObject)cboParent.SelectedItem).CodeID);
                SetDataSource(((LookupObject)cboParent.SelectedItem).CodeID);
                if (dgMain.RowCount > recordCount) { AuditRecord(OperationType.ADD, txtValue.Text); }
            }


            txtValue.Text = "";
        }

        private void SetDataSource(int? parentID)
        {
            dgMain.DataSource = parentID != null ? _lookupManager.GetLookupObject(parentID) : _lookupManager.GetLookupObject(_codeMaster.CodeValue);

            dgMain.Columns[0].Visible = false;
            dgMain.Columns[1].Visible = false;
            dgMain.Columns[3].Visible = false;
            dgMain.Columns[2].HeaderText = "Items";
            dgMain.Columns[2].Width = dgMain.Width - 50;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Close")
                Close();


            cboParent.Enabled = true;
            button3.Text = "Close";
            btnAddNew.Visible = cboParent.Enabled;
            dgMain.Enabled = btnAddNew.Visible;
            cboParent.Enabled = btnAddNew.Visible;
            btnEdit.Text = "Edit";
            txtValue.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _lookupManager.RemoveLookupObject(int.Parse(dgMain[0, dgMain.SelectedCells[0].RowIndex].Value.ToString()));
                AuditRecord(OperationType.DELETE, dgMain[0, dgMain.SelectedCells[0].RowIndex].Value.ToString());
                if (cboParent.DataSource == null) SetDataSource(null);
                else SetDataSource(((LookupObject)cboParent.SelectedItem).CodeID);
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a Item to Delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                    );
            }


        }

        private void cboParent_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboParent.DataSource == null) return;
            if (cboParent.Visible && cboParent.SelectedItem == null )
            {
                MessageBox.Show("No Associated Category available. Please add a Category before Continuing", "Reason Settings",  MessageBoxButtons.OK, MessageBoxIcon.Error );
                this.Close();
                return;
            }
            SetDataSource(((LookupObject)cboParent.SelectedItem).CodeID);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgMain.SelectedCells.Count > 0 && btnEdit.Text == "Edit")
            {
                txtValue.Text = dgMain[2, dgMain.SelectedCells[0].RowIndex].Value.ToString();
            }
            else
            {
                if (btnEdit.Text != "Update")
                {
                    MessageBox.Show("Please select a Item to Edit", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (btnEdit.Text == "Update")
            {
                _lookupManager.UpdateLookupObject(int.Parse(dgMain.Rows[dgMain.SelectedCells[0].RowIndex].Cells[0].Value.ToString()), txtValue.Text);
                if (dgMain.Rows[dgMain.SelectedCells[0].RowIndex].Cells[2].Value.ToString() != txtValue.Text)
                AuditRecord(OperationType.MODIFY, dgMain.Rows[dgMain.SelectedCells[0].RowIndex].Cells[2].Value.ToString());
                dgMain.Rows[dgMain.SelectedCells[0].RowIndex].Cells[2].Value = txtValue.Text;
            }


            btnEdit.Text = btnEdit.Text == "Update" ? "Edit" : "Update";
            btnAddNew.Visible = btnEdit.Text != "Update";
            dgMain.Enabled = btnAddNew.Visible;
            cboParent.Enabled = btnAddNew.Visible;
            button3.Text = dgMain.Enabled ? "Close" : "Cancel";

        }

        private void AuditRecord(OperationType mode , string value)
        {
            string sDescription = "Maintainence " + (Program.CatergoryOrReason == "MAINCA" ? "Category " : "Reason ");
           
            AuditViewerBusiness.CreateInstance(DatabaseHelper.GetConnectionString());
            Audit_History AH = new Audit_History();
            AH.EnterpriseModuleName = ModuleNameEnterprise.UserGroupAdmin;
            AH.Audit_Screen_Name = sDescription;
            AH.Audit_Desc = sDescription + value + (mode == OperationType.ADD ? " created" : mode == OperationType.MODIFY ? " modified" : " deleted");
            AH.AuditOperationType = mode;
            AH.Audit_User_ID = Program.LoginUserID;
            AH.Audit_User_Name = Program.LoginUserName;
            AuditViewerBusiness.InsertAuditData(AH);
        }
    }
}
