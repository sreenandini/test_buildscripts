using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using System.Xml.Linq;
using Audit.Transport;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public delegate bool SaveChanges();

    public partial class ucAssociateEmployeeGMUModes : UserControl
    {
        #region Declarations
        private EmployeeCardBiz objEmployeeCardBiz = null;
        List<EmployeeModeGroup> oModes = null;
        List<EmployeeCardEntity> lstEmpCards = null;
        List<EmployeeGMUModeGroup> oEmpGroup = null;
      
        private Dictionary<int, List<MarkGMUEventsorModes>> dicModeItems;
        public SaveChanges objDelSave;
        public bool bFirst = true;
        public string RoleName { get; set; }
        public int RoleID { get; set; }
        public int? CardLevel { get; set; }
        #endregion

        #region Ctor
        public ucAssociateEmployeeGMUModes()
        {
            InitializeComponent();
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.chkAllModes.Tag = "Key_SelectAll";
            this.lblModeType.Tag = "Key_ModeGroup";
        }
        #endregion Ctor

        #region Events
        private void ucAssociateEmployeeGMUModes_Load(object sender, EventArgs e)
        {
            objEmployeeCardBiz = new EmployeeCardBiz();
            lstEmpCards = objEmployeeCardBiz.GetEmployeeCardInfo(null);
            dicModeItems = new Dictionary<int, List<MarkGMUEventsorModes>>();
            FillCardModes();
            LoadEmpModeDetails();
            FillEmpModeGroupTypes();
            this.ResolveResources();
        }

        private void chkAllModes_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lvGMUModes.Items.Count; i++)
            {
                if (lvGMUModes.Items[i].Checked && chkAllModes.Checked == true)
                    continue;
                else
                    lvGMUModes.Items[i].Checked = chkAllModes.Checked;
            }

        }

        /// <summary>
        /// Mode item checked
        /// Adds the selected mode into dictionary with key as card number and value as GMUMode
        /// Removes from dictonary when unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvGMUModes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (this.RoleID != 0)
                {
                    List<MarkGMUEventsorModes> lstGMUModes = null;
                    if (e.NewValue == CheckState.Checked)
                    {
                        if (dicModeItems.ContainsKey(this.RoleID))
                        {
                            lstGMUModes = dicModeItems[this.RoleID];
                            if (lstGMUModes == null)
                                lstGMUModes = new List<MarkGMUEventsorModes>();
                            MarkGMUEventsorModes oFind = lstGMUModes.Find(o => o.GMUEventorModeID == Convert.ToInt32(lvGMUModes.Items[e.Index].SubItems[2].Text));
                            if (oFind == null)
                                lstGMUModes.Add(new MarkGMUEventsorModes { GMUEventorModeID = Convert.ToInt32(lvGMUModes.Items[e.Index].SubItems[2].Text), isNew = true, isDelete = (e.NewValue == CheckState.Unchecked) });  //Adds GMUModeId to the Value list in dictionary
                            else
                            {
                                oFind.isDelete = (e.NewValue == CheckState.Unchecked);
                            }
                        }
                        else
                        {
                            dicModeItems.Add(this.RoleID, new List<MarkGMUEventsorModes> { new MarkGMUEventsorModes { GMUEventorModeID = Convert.ToInt32(lvGMUModes.Items[e.Index].SubItems[2].Text), isNew = true, isDelete = (e.NewValue == CheckState.Unchecked) } }); //Add the key with value for first time
                        }
                    }
                    else
                    {
                        if (dicModeItems.ContainsKey(this.RoleID))
                        {
                            lstGMUModes = dicModeItems[this.RoleID];
                            if (lstGMUModes != null)
                            {
                                int GMUEventID = Convert.ToInt32(lvGMUModes.Items[e.Index].SubItems[2].Text);
                                MarkGMUEventsorModes oMarkGMU = lstGMUModes.Find(o => o.GMUEventorModeID == GMUEventID);
                                if (oMarkGMU != null)
                                {
                                    oMarkGMU.isDelete = (e.NewValue == CheckState.Unchecked);
                                }
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
        #endregion Events

        #region User Defined Functions
        /// <summary>
        /// Creates Mode Group in Listview and Calls CardMode() to Load Mode details.
        /// </summary>
        private void FillCardModes()
        {
            try
            {
                lvGMUModes.Groups.Clear();
                lvGMUModes.Columns.Clear();
                lvGMUModes.Items.Clear();

                lvGMUModes.Columns.Add(this.GetResourceTextByKey("Key_GMUMode"), 100);
                lvGMUModes.Columns.Add(this.GetResourceTextByKey("Key_ModeDesc"), 850);

                List<EmployeeCardTypes> lstEmpCardTypes = objEmployeeCardBiz.GetCardTypes();
                foreach (EmployeeCardTypes EmpCardType in lstEmpCardTypes)
                {
                    lvGMUModes.Groups.Add(new ListViewGroup(EmpCardType.EmpCardType, HorizontalAlignment.Left));
                }
                LoadModesByGroup();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Get the List of Employee card types.
        /// <paramref name=""/>
        /// <returns></returns>
        /// </summary>
        private void FillEmpModeGroupTypes()
        {
            try
            {
                cmbModeGroup.SelectedIndexChanged -= cmbModeGroup_SelectedIndexChanged;
                cmbModeGroup.DisplayMember = "EmpCardType";
                cmbModeGroup.ValueMember = "EmpCardTypeID";
                List<EmployeeCardTypes> lstEmpCardTypes = objEmployeeCardBiz.GetCardTypes();
                lstEmpCardTypes.Insert(0, new EmployeeCardTypes { EmpCardTypeID = -1, EmpCardType = BMC.Common.ResourceExtensions.GetResourceTextByKey(null, "Key_Any") });
                cmbModeGroup.DataSource = lstEmpCardTypes == null ? new List<EmployeeCardTypes>() : lstEmpCardTypes;
                cmbModeGroup.SelectedIndex = (oEmpGroup.Select(obj => obj.EmpGMUModeGroup).FirstOrDefault() == null) ? 0 : Convert.ToInt32(oEmpGroup.Select(obj => obj.EmpGMUModeGroup).FirstOrDefault());
                lvGMUModes.Enabled = cmbModeGroup.SelectedIndex == 0;
                cmbModeGroup.SelectedIndexChanged += cmbModeGroup_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Add Mode Group from DB.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="iModeType"></param>
        private void LoadModesByGroup()
        {
            try
            {
                oModes = objEmployeeCardBiz.GetGMUModes();//Gets all the modes from DB.
                if (oModes != null)
                {
                    foreach (EmployeeModeGroup result in oModes)
                    {
                        ListViewItem lv_item = new ListViewItem(result.GMUMode);
                        lv_item.SubItems.Add(result.ModeDescription);
                        lv_item.SubItems.Add(result.GMUModeID.ToString());
                        lvGMUModes.Items.Add(lv_item);
                        lvGMUModes.Groups[result.GMUModeGroupID - 1].Items.Add(lv_item);
                    }
                }
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Load Employee Modes.
        /// </summary>
        private void LoadEmpModeDetails()
        {
            try
            {
                oEmpGroup = objEmployeeCardBiz.GetEmpGMUModes(this.RoleID);

                List<MarkGMUEventsorModes> lstGMUModes = new List<MarkGMUEventsorModes>();
                lstGMUModes.Clear();
                dicModeItems.Clear();

                foreach (EmployeeGMUModeGroup EmpMode in oEmpGroup)
                {
                    if (dicModeItems.ContainsKey(this.RoleID))
                    {
                        lstGMUModes = dicModeItems[this.RoleID];
                        lstGMUModes.Add(new MarkGMUEventsorModes { GMUEventorModeID = EmpMode.GMUModeId });
                    }
                    else
                    {
                        dicModeItems.Add(this.RoleID, new List<MarkGMUEventsorModes> { new MarkGMUEventsorModes { GMUEventorModeID = EmpMode.GMUModeId } });
                    }
                }
                if (oEmpGroup.Count != 0)
                {
                    int index = 0;
                    lvGMUModes.ItemCheck -= lvGMUModes_ItemCheck;

                    for (int i = 0; i < lvGMUModes.Items.Count; i++)
                        lvGMUModes.Items[i].Checked = false;


                    if (dicModeItems.ContainsKey(this.RoleID))
                    {
                        List<MarkGMUEventsorModes> lstChkModes = dicModeItems[this.RoleID];
                        foreach (MarkGMUEventsorModes GMUModeID in lstChkModes)
                        {
                            index = oModes.FindIndex(o => o.GMUModeID == GMUModeID.GMUEventorModeID);
                            if (index != -1)
                            {
                                ListViewItem lvSelectedItem = lvGMUModes.Items[index];
                                lvSelectedItem.Checked = true; //checks the modes associated to card.
                            }
                        }
                    }
                    lvGMUModes.ItemCheck += lvGMUModes_ItemCheck;

                    //Removing CheckedChanged event trigger.
                    chkAllModes.CheckedChanged -= chkAllModes_CheckedChanged;

                    for (int i = 0; i < lvGMUModes.Items.Count; i++)
                    {
                        if (lvGMUModes.Items[i].Checked == true)
                            chkAllModes.Checked = true;
                        else
                            chkAllModes.Checked = false;
                    }
                    //Adding CheckedChanged event trigger.
                    chkAllModes.CheckedChanged += chkAllModes_CheckedChanged;
                }
                //Hooking Save Method to Delegate(only for first time)
                if (bFirst)
                {
                    objDelSave += SaveDetails;
                    bFirst = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Save Employee GMU Mode Details.
        /// </summary>
        public bool SaveDetails()
        {
            bool bRetVal = true;
            try
            {
                if (this.RoleID != 0)
                {
                    string sCard = string.Empty;
                    XElement oEntXML = objEmployeeCardBiz.GetEmployeeXML(false, false, oModes, null, lstEmpCards, dicModeItems);
                    XElement oExportXML = objEmployeeCardBiz.GetEmployeeXML(true, false, oModes, null, lstEmpCards, dicModeItems);
                    if (oEntXML != null)
                    {
                        objEmployeeCardBiz.InsertEmpGMUMode(oEntXML.ToString(), oExportXML.ToString(),
                            AppGlobals.Current.UserId, AppGlobals.Current.UserName, (int)ModuleNameEnterprise.EmployeeGMUModes,
                            ModuleNameEnterprise.EmployeeGMUModes.ToString(), RoleName, (int?)cmbModeGroup.SelectedValue == -1 ? 0 : (int?)cmbModeGroup.SelectedValue,CardLevel);
                    }
                    LoadEmpModeDetails();
                    bRetVal = true;
                }
                else
                    bRetVal = false;

            }
            catch (Exception ex)
            {
                bRetVal = false;
                ExceptionManager.Publish(ex);
            }
            return bRetVal;
        }
        #endregion User Defined Functions

        private void cmbModeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ModeType = (int)cmbModeGroup.SelectedValue;

            if (ModeType != -1)
            {
                for (int i = 0; i < lvGMUModes.Items.Count; i++)
                {
                    ListViewItem lvSelectedItem = lvGMUModes.Items[i];
                    lvSelectedItem.Checked = false;
                }

                lvGMUModes.Enabled = chkAllModes.Enabled = ModeType == 0;

                if (ModeType != 0)
                {
                    List<int> chkGroupItems = new List<int>();
                    foreach (EmployeeModeGroup obj in oModes.FindAll(o => o.GMUModeGroupID == ModeType))
                    {
                        int lvIndex = oModes.FindIndex(o => o.GMUModeID == obj.GMUModeID);
                        if (lvIndex != -1)
                        {
                            ListViewItem lvSelectedItem = lvGMUModes.Items[lvIndex];
                            lvSelectedItem.Checked = true; //checks the modes associated to card.
                        }
                    }
                    return;
                }
            }
            else
                lvGMUModes.Enabled = chkAllModes.Enabled = true;

        }
    }
}

