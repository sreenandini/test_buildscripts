using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common;
using BMC.EnterpriseClient.Helpers;


namespace BMC.EnterpriseClient.Views
{
    public partial class frmCreateViewEmployeeCards : Form
    {
        #region User Defined Fields
        private EmployeeCardBiz objEmployeeCardBiz = null;
        List<EmployeeCardEntity> lsEmpCards = null;
        private ListViewColumnSorter _lvwColumnSorter = null;
        #endregion User Defined Fields

        #region Ctor
        public frmCreateViewEmployeeCards()
        {
            InitializeComponent();
            objEmployeeCardBiz = new EmployeeCardBiz();
            _lvwColumnSorter = new ListViewColumnSorter();
            SetTagProperty();
        }
        #endregion Ctor

        #region Events
        /// <summary>
        /// Loads the Card info based on the selected card.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstCardNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EnableDisableControls();
                cmbEmployeeName.SelectedIndex = -1;
                string sCard = lstCardNumber.SelectedItem.ToString();
                LoadEmployeeDetails(sCard);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        /// <summary>
        /// Loads all the data in the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCreateViewEmployeeCards_Load(object sender, EventArgs e)
        {
            try
            {
                lvwEmployeeDetails.ListViewItemSorter = _lvwColumnSorter;
                FillEmpCardInfo(string.Empty); //Loads Employee card 
                LoadEmployeeListView();  //Load card status in ListView
                LoadUserName(); //Load User Name Combobox
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Allows to Create New Employee Card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateCard_Click(object sender, EventArgs e)
        {
            try
            {
                frmAddEmployeeCard oEmpCard = new frmAddEmployeeCard();
                oEmpCard.ShowDialog();
                FillEmpCardInfo(string.Empty);
                LoadEmployeeDetails(txtEmployeeCard.Text);
                LoadEmployeeListView();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        /// <summary>
        /// Acts as Edit and Save 
        /// </summary>
        /// <param name="sender"></param>
        /// <param nabcme="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstCardNumber.SelectedItem != null)
                {
                    if (btnEdit.Text == this.GetResourceTextByKey("Key_EditCaption"))
                    {
                        btnDelete.Visible = true;
                        btnSignOff.Visible = true;
                        rbtnActive.Enabled = true;
                        rbtnInActive.Enabled = true;
                        cmbEmployeeName.Enabled = true;
                        btnCreateCard.Enabled = false;
                        btnEdit.Text = this.GetResourceTextByKey("Key_SaveCaption");
                    }
                    else
                    {
                        EmployeeCardEntity card_ent = null;
                        card_ent = lsEmpCards.Find(o => o.EmployeeCardNumber == lstCardNumber.SelectedItem.ToString());
                        bool bResult = true;
                        EnableDisableControls();
                        if (!string.IsNullOrEmpty(cmbEmployeeName.Text.Trim()) || rbtnActive.Checked != card_ent.IsActive)
                        {

                            var EmpDetails = lsEmpCards.Where(o => o.EmployeeName == cmbEmployeeName.Text.Trim()).Select(o => new { EmpCard = o.EmployeeCardNumber, EmpName = o.EmployeeName }).FirstOrDefault();

                            //if (cmbEmployeeName.Text.Trim() == "" || (EmpDetails != null && (String.IsNullOrEmpty(EmpDetails.EmpName) || EmpDetails.EmpCard == txtEmployeeCard.Text)))
                            if (cmbEmployeeName.Text.Trim() == "" || (EmpDetails == null || EmpDetails.EmpCard == txtEmployeeCard.Text))
                            {
                                if (card_ent.EmployeeName != cmbEmployeeName.Text || card_ent.IsActive != rbtnActive.Checked)
                                {
                                    bResult = objEmployeeCardBiz.InsertEmployeeCardDetails(txtEmployeeCard.Text.Trim(), cmbEmployeeName.Text.Trim(),
                                        string.IsNullOrEmpty(cmbEmployeeName.Text.Trim()) ? 0 : (int)cmbEmployeeName.SelectedValue,
                                                   rbtnActive.Checked ? true : false,
                                                   AppEntryPoint.Current.UserName);
                                }
                            }
                            else
                            {
                                string sEmpNmae = lsEmpCards.Where(o => o.EmployeeCardNumber == txtEmployeeCard.Text.Trim()).Select(o => o.EmployeeName).FirstOrDefault();
                                this.ShowInfoMessageBox("Employee: " + cmbEmployeeName.Text + " already assigned with card: " + EmpDetails.EmpCard);
                                List<UserDetailsResult> lst_user = cmbEmployeeName.DataSource as List<UserDetailsResult>;
                                cmbEmployeeName.SelectedIndex = lst_user.FindIndex(o => o.UserName == sEmpNmae);
                                return;
                            }
                        }
                        if (bResult)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DETAILS_UPDATE_SUCCESS"));
                            FillEmpCardInfo(string.Empty);
                            LoadEmployeeDetails(txtEmployeeCard.Text);
                            LoadEmployeeListView();
                        }
                        else
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_DEPRECIATION_ERROR"));
                    }
                }
                else
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_EMP_CARD_VALID_SELECT"));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        /// To Remove already assigned user and re assign it to new user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignOff_Click(object sender, EventArgs e)
        {
            try
            {
                cmbEmployeeName.Enabled = true;
                if (lstCardNumber.SelectedItem != null)
                {

                    if (string.IsNullOrEmpty(cmbEmployeeName.Text.Trim()))
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSOCIATEEMP_REVOKE"), this.Text);
                        return;
                    }
                    else
                    {
                        //Confirm whether to revoke
                        DialogResult result = this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_WANT_TO_REVOKE") + this.GetResourceTextByKey(1, "MSG_EMPLOYEE_CARD_ASSIGNED"), this.Text);

                        if (result.ToString() == "Yes")
                        {
                            objEmployeeCardBiz.RevokeEmployeeCard((int)cmbEmployeeName.SelectedValue, txtEmployeeCard.Text.Trim(), AppGlobals.Current.UserId, AppGlobals.Current.UserName);
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_EMPLOYEE_CARD_REVOKED"));
                            EnableDisableControls();
                            FillEmpCardInfo(string.Empty);
                            LoadEmployeeDetails(txtEmployeeCard.Text);
                            LoadEmployeeListView();
                        }
                    }
                }
                else
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_EMP_CARD_VALID_SELECT"));


                cmbEmployeeName.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        /// allows to select employee name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sEmpNmae = lsEmpCards.Where(o => o.EmployeeCardNumber == txtEmployeeCard.Text.Trim()).Select(o => o.EmployeeName).FirstOrDefault();
                if (!string.IsNullOrEmpty(sEmpNmae) && cmbEmployeeName.Enabled)
                {
                    this.ShowInfoMessageBox("SignOff exsisting Employee and assign new Employee.");
                    EnableDisableControls();
                    List<UserDetailsResult> lst_user = cmbEmployeeName.DataSource as List<UserDetailsResult>;
                    cmbEmployeeName.SelectedIndex = lst_user.FindIndex(o => o.UserName == sEmpNmae);
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Used to Search card info based on the text entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchCard_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(txtSearchCard))
                {
                    if (txtSearchCard.Text != this.GetResourceTextByKey("Key_SearchEmployeeCard"))
                    {
                        lstCardNumber.Items.Clear();
                        lstCardNumber.Items.AddRange(lsEmpCards.Where(o => o.EmployeeCardNumber.IndexOf(txtSearchCard.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) != -1).Select(o => o.EmployeeCardNumber).ToArray());
                    }
                }
                else
                {
                    if (txtSearchEmployeeDetails.Text != this.GetResourceTextByKey("Key_SearchEmployeeCard"))
                    {
                        lvwEmployeeDetails.Items.Clear();
                        var filter = lsEmpCards.Where(o => o.EmployeeCardNumber.IndexOf(txtSearchEmployeeDetails.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) != -1);
                        if (lsEmpCards != null)
                        {
                            foreach (EmployeeCardEntity result in filter)
                            {
                                ListViewItem lv_item = new ListViewItem(result.EmployeeCardNumber);
                                lv_item.SubItems.Add(Convert.ToBoolean(result.IsActive) ? "ACTIVE" : "INACTIVE");
                                lv_item.SubItems.Add(result.EmployeeName);
                                lvwEmployeeDetails.Items.Add(lv_item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void txtSearchCard_Enter(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(txtSearchCard))
                {
                    txtSearchEmployeeDetails.ForeColor = Color.Black;
                    if (txtSearchCard.Text.Length == 0 || txtSearchCard.Text == this.GetResourceTextByKey("Key_SearchEmployeeCard"))
                    {
                        txtSearchCard.Text = "";
                    }
                    txtSearchCard.ForeColor = Color.Black;
                }
                else
                {
                    if (txtSearchEmployeeDetails.Text.Length == 0 || txtSearchEmployeeDetails.Text == this.GetResourceTextByKey("Key_SearchEmployeeCard"))
                    {
                        txtSearchEmployeeDetails.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void txtSearchCard_Leave(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(txtSearchCard))
                {
                    if (txtSearchCard.Text.Length == 0)
                    {
                        txtSearchCard.Text = this.GetResourceTextByKey("Key_SearchEmployeeCard");
                        txtSearchCard.ForeColor = Color.DimGray;
                    }
                }
                else
                {
                    if (txtSearchEmployeeDetails.Text.Length == 0)
                    {
                        txtSearchEmployeeDetails.Text = this.GetResourceTextByKey("Key_SearchEmployeeCard");
                        txtSearchEmployeeDetails.ForeColor = Color.DimGray;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Column header sorting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwEmployeeDetails_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                if (e.Column == _lvwColumnSorter.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (_lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                    {
                        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    }
                    else
                    {
                        _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    _lvwColumnSorter.SortColumn = e.Column;
                    _lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
                ListView lst_view = sender as ListView;
                lst_view.Sort();
            }
            catch (Exception ex)
            {
               
                ExceptionManager.Publish(ex);
            }

        }
        #endregion Events

        #region User Defined Functions
        private void EnableDisableControls()
        {
            btnDelete.Visible = false;
            btnSignOff.Visible = false;
            rbtnActive.Enabled = false;
            rbtnInActive.Enabled = false;
            cmbEmployeeName.Enabled = false;
            btnCreateCard.Enabled = true;
            btnEdit.Text = this.GetResourceTextByKey("Key_EditCaption");
        }

        private void SetTagProperty()
        {
            btnEdit.Text = this.GetResourceTextByKey("Key_EditCaption");
            txtSearchEmployeeDetails.Text = this.GetResourceTextByKey("Key_SearchEmployeeCard");
            this.lblEmployeeCard.Tag = "Key_EmplCard";
            lblStatus.Tag = "Key_Status";
            lblUserName.Tag = "Key_EmployeeNameMandatory";
            rbtnActive.Tag = "Key_Active";
            rbtnInActive.Tag = "Key_InActive";
            btnCancel.Tag = "Key_CancelCaption";
            btnCreateCard.Tag = "Key_CreateCard";
            btnDelete.Tag = "Key_DeleteCaption";
            btnSignOff.Tag = "Key_Revoke";
        }

        /// <summary>
        /// Loads all distinct cards
        /// </summary>
        /// <param name="EmpCardNo"></param>
        private void FillEmpCardInfo(string EmpCardNo)
        {
            cmbEmployeeName.SelectedIndex = -1;
            lsEmpCards = objEmployeeCardBiz.GetEmployeeCardInfo(string.IsNullOrEmpty(EmpCardNo) ? null : EmpCardNo);
            lstCardNumber.Items.Clear();
            if (lsEmpCards != null)
            {
                lsEmpCards = lsEmpCards.GroupBy(emp => emp.EmpID).Select(emp => emp.First()).ToList();
                lsEmpCards.ForEach(o => lstCardNumber.Items.Add(o.EmployeeCardNumber));
            }
        }

        /// <summary>
        /// Load UserName in Employee Combobox
        /// </summary>
        private void LoadUserName()
        {
            List<UserDetailsResult> oEmpName = objEmployeeCardBiz.LoadEmployeeName();
            cmbEmployeeName.DataSource = oEmpName.OrderBy(o => o.UserName).ToList();
            cmbEmployeeName.DisplayMember = "UserName";
            cmbEmployeeName.ValueMember = "SecurityUserID";
            cmbEmployeeName.Enabled = false;
            cmbEmployeeName.SelectedIndex = -1;
        }


        /// <summary>
        /// Displays all the card info in listview
        /// </summary>
        private void LoadEmployeeListView()
        {
            lvwEmployeeDetails.Columns.Clear();
            lvwEmployeeDetails.Items.Clear();
            lvwEmployeeDetails.Sort();
            lvwEmployeeDetails.Columns.Add(this.GetResourceTextByKey("Key_CardNumber"), 150);
            lvwEmployeeDetails.Columns.Add(this.GetResourceTextByKey("Key_Status"), 100);
            lvwEmployeeDetails.Columns.Add(this.GetResourceTextByKey("Key_EmployeeName"), 750);

            if (lsEmpCards != null)
            {                
                foreach (EmployeeCardEntity result in lsEmpCards)
                {
                    ListViewItem lv_item = new ListViewItem(result.EmployeeCardNumber);
                    lv_item.SubItems.Add(Convert.ToBoolean(result.IsActive) ? "ACTIVE" : "INACTIVE");
                    lv_item.SubItems.Add(result.EmployeeName);
                    lvwEmployeeDetails.Items.Add(lv_item);
                }
            }
        }

        /// <summary>
        /// Loads the details based on the card selected.
        /// </summary>
        /// <param name="sCard">Enter Employee card no</param>
        private void LoadEmployeeDetails(string sCard)
        {
            var lstmpEmpCards = lsEmpCards.Where(obj => obj.EmployeeCardNumber == sCard).ToList<EmployeeCardEntity>();
            if (lstmpEmpCards.Count > 0)
            {
                txtEmployeeCard.Text = lstmpEmpCards[0].EmployeeCardNumber;
                cmbEmployeeName.Text = lstmpEmpCards[0].EmployeeName;
                rbtnActive.Checked = Convert.ToBoolean(lstmpEmpCards[0].IsActive);
                rbtnInActive.Checked = Convert.ToBoolean(!lstmpEmpCards[0].IsActive);
            }
            else
            {
                txtEmployeeCard.Text = sCard;
            }
            LoadEmployeeListView();
            ListViewItem item = lvwEmployeeDetails.FindItemWithText(sCard);
            if (item != null)
                item.Selected = true;

        }
        #endregion User Defined Functions       
    }
}
