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
using BMC.EnterpriseClient.Helpers;
using BMC.Common;


namespace BMC.EnterpriseClient.Views
{
    public partial class ucAssoicateEmployeeEvents : UserControl
    {
        #region Declarations
        private EmployeeCardBiz objEmployeeCardBiz = null;
        List<EmployeeEvents> oModes = null;
        List<EmployeeCardEntity> lstEmpCards = null;
        List<EmployeeEventsGroup> oEmpGroup = null;
        private Dictionary<int, List<MarkGMUEventsorModes>> dicEventItems;
        public string RoleName { get; set; }
        public int? CardLevel { get; set; }
        public int RoleID { get; set; }
        public SaveChanges objDelSave;
        bool bFirst = true;
        bool bParentNodeTrigger = true;
        #endregion

        #region Ctor
        public ucAssoicateEmployeeEvents()
        {
            InitializeComponent();
            this.chkAllEvents.Tag = "Key_SelectAll";
        }
        #endregion Ctor

        #region Events
        private void ucAssoicateEmployeeEvents_Load(object sender, EventArgs e)
        {
            objEmployeeCardBiz = new EmployeeCardBiz();
            lstEmpCards = objEmployeeCardBiz.GetEmployeeCardInfo(null);
            dicEventItems = new Dictionary<int, List<MarkGMUEventsorModes>>();
            this.ResolveResources();
            CreateNodesinTreeView();
            LoadEmpEventDetails();
        }
        private void tvEvents_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name.Split(',')[0] == "EG")
            {
                CheckTreeViewNode(e.Node, e.Node.Checked);
            }
            if (this.RoleID != 0 && e.Node.Name.Split(',')[0] != "EG")
            {
                List<MarkGMUEventsorModes> lstGMUEvents = null;

                if (e.Node.Checked == Convert.ToBoolean(CheckState.Checked))
                {
                    if (dicEventItems.ContainsKey(this.RoleID))
                    {
                        lstGMUEvents = dicEventItems[this.RoleID];
                        if (lstGMUEvents == null)
                            lstGMUEvents = new List<MarkGMUEventsorModes>();
                        MarkGMUEventsorModes oFind = lstGMUEvents.Find(o => o.GMUEventorModeID == Convert.ToInt32(e.Node.Tag));
                        if (oFind == null)
                            lstGMUEvents.Add(new MarkGMUEventsorModes { GMUEventorModeID = Convert.ToInt32(e.Node.Tag), isNew = true, isDelete = e.Node.Checked == Convert.ToBoolean(CheckState.Unchecked) }); //Adds GMUModeId to the Value list in dictionary
                        else
                            oFind.isDelete = e.Node.Checked == Convert.ToBoolean(CheckState.Unchecked);
                    }
                    else
                        dicEventItems.Add(this.RoleID, new List<MarkGMUEventsorModes> { new MarkGMUEventsorModes { GMUEventorModeID = Convert.ToInt32(e.Node.Tag), isNew = true, isDelete = e.Node.Checked == Convert.ToBoolean(CheckState.Unchecked) } }); //Add the key with value for first time                    
                }
                else
                {
                    if (dicEventItems.ContainsKey(this.RoleID))
                    {
                        lstGMUEvents = dicEventItems[this.RoleID];
                        if (lstGMUEvents != null)
                        {
                            int GMUEventID = Convert.ToInt32(e.Node.Tag);
                            MarkGMUEventsorModes oMarkEvent = lstGMUEvents.Find(o => o.GMUEventorModeID == GMUEventID);
                            if (oMarkEvent != null)
                            {
                                oMarkEvent.isDelete = e.Node.Checked == Convert.ToBoolean(CheckState.Unchecked);
                            }
                        }
                    }
                }
                if (bFirst)
                {
                    objDelSave += SaveDetails;
                    bFirst = false;
                }
            }
        }

        private void chkAllModes_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < tvEvents.Nodes.Count; i++)
            {
                if (tvEvents.Nodes[i].Checked && chkAllEvents.Checked == true)
                    continue;
                else
                    tvEvents.Nodes[i].Checked = chkAllEvents.Checked;
            }
        }
        #endregion Events

        #region User Defined Functions
        /// <summary>
        /// Creates Mode Group in Listview and Calls CardMode() to Load Mode details.
        /// </summary>
        public void CreateNodesinTreeView()
        {
            int iOldEventID = 0, iOldEventGroupID = 0;
            try
            {
                TreeNode tnEvents = new TreeNode();
                tvEvents.Nodes.Clear();
                List<EmployeeEventGroupTypes> lstEmpCardTypes = objEmployeeCardBiz.GetEventGroupTypes();
                foreach (EmployeeEventGroupTypes oEventGroup in lstEmpCardTypes)
                {
                    if (iOldEventGroupID != oEventGroup.EmpEventGroupID)
                    {
                        TreeNode tempnode = tvEvents.Nodes.Add("EG,#" + oEventGroup.EmpEventGroupID.ToString(), oEventGroup.EmpEventGroupType.ToString());
                        tvEvents.Nodes["EG,#" + oEventGroup.EmpEventGroupID.ToString()].ExpandAll();
                        iOldEventGroupID = oEventGroup.EmpEventGroupID;
                    }
                }
                oModes = objEmployeeCardBiz.GetGMUEvents();//Gets all the modes from DB.
                foreach (EmployeeEvents oEvent in oModes)
                {
                    if (oModes != null && iOldEventID != oEvent.GMUEventID)
                    {
                        TreeNode tnNode = tvEvents.Nodes.Find("EG,#" + oEvent.GMUEventGroupID.ToString(), true)[0];
                        if (tnNode != null)
                        {
                            TreeNode tempnode = tnNode.Nodes.Add("EI,#" + oEvent.GMUEventID.ToString(), oEvent.GMUEventName.ToString());
                            tempnode.Tag = oEvent.GMUEventID.ToString();
                            iOldEventID = oEvent.GMUEventID;
                        }
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
        private void LoadEmpEventDetails()
        {
            try
            {
                oEmpGroup = objEmployeeCardBiz.GetEmpGMUEvents(this.RoleID);
                List<MarkGMUEventsorModes> lstGMUEvents = new List<MarkGMUEventsorModes>();
                dicEventItems.Clear();
                foreach (EmployeeEventsGroup EmpEvent in oEmpGroup)
                {
                    if (dicEventItems.ContainsKey(this.RoleID))
                    {
                        lstGMUEvents = dicEventItems[this.RoleID];
                        lstGMUEvents.Add(new MarkGMUEventsorModes { GMUEventorModeID = EmpEvent.GMUEventID });
                    }
                    else
                    {
                        dicEventItems.Add(this.RoleID, new List<MarkGMUEventsorModes> { new MarkGMUEventsorModes { GMUEventorModeID = EmpEvent.GMUEventID } });
                    }
                }
                tvEvents.AfterCheck -= tvEvents_AfterSelect;

                for (int i = 0; i < tvEvents.Nodes.Count; i++)
                    tvEvents.Nodes[i].Checked = false;

                if (dicEventItems.ContainsKey(this.RoleID))
                {
                    List<MarkGMUEventsorModes> lstGMUModes = dicEventItems[this.RoleID];

                    foreach (MarkGMUEventsorModes GMUModeID in lstGMUModes)
                    {
                        var index = oModes.Where(o => o.GMUEventID == GMUModeID.GMUEventorModeID).Select(o => new { GMUEventGroupID = o.GMUEventGroupID, GMUEventID = o.GMUEventID }).SingleOrDefault();
                        TreeNode[] tnNode = tvEvents.Nodes.Find("EG,#" + index.GMUEventGroupID.ToString(), false);
                        if (tnNode.Length > 0)
                        {
                            TreeNode[] tnSubNode = tnNode[0].Nodes.Find("EI,#" + index.GMUEventID, true);
                            foreach (TreeNode tn in tnSubNode)
                            {
                                tn.Checked = true;
                                if (bParentNodeTrigger && tn.Checked)
                                {
                                    CheckMyParent(tn, tn.Checked);
                                }
                            }
                        }
                    }
                }

                bool isAllNodeChecked = true;
                chkAllEvents.CheckedChanged -= chkAllModes_CheckedChanged;
                for (int i = 0; i < tvEvents.Nodes.Count; i++)
                {
                    if (!tvEvents.Nodes[i].Checked)
                    {
                        chkAllEvents.Checked = false;
                        isAllNodeChecked = false;
                        break;
                    }
                }
                if (isAllNodeChecked)
                {
                    chkAllEvents.Checked = true;
                }
                chkAllEvents.CheckedChanged += chkAllModes_CheckedChanged;
                tvEvents.AfterCheck += tvEvents_AfterSelect;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        public bool SaveDetails()
        {
            bool bRetVal = true;
            try
            {
                StringBuilder sDescription = new StringBuilder();
                if (dicEventItems.Count != 0)
                {
                    XElement oEntXML = objEmployeeCardBiz.GetEmployeeXML(false, true, null, oModes, lstEmpCards, dicEventItems);
                    XElement oExportXML = objEmployeeCardBiz.GetEmployeeXML(true, true, null, oModes, lstEmpCards, dicEventItems);
                    if (oEntXML != null)
                    {
                        objEmployeeCardBiz.InsertEmpGMUEvents(oEntXML.ToString(), oExportXML.ToString()
                            , AppGlobals.Current.UserId, AppGlobals.Current.UserName, (int)ModuleNameEnterprise.EmployeeGMUEvents
                            , ModuleNameEnterprise.EmployeeGMUEvents.ToString(), RoleID, CardLevel);

                    }

                    bRetVal = true;
                }
                else
                    bRetVal = false;

                LoadEmpEventDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bRetVal = false;
            }
            return bRetVal;
        }       
        private void CheckTreeViewNode(TreeNode node, bool isChecked)
        {
            try
            {
                foreach (TreeNode item in node.Nodes)
                {
                    item.Checked = isChecked;

                    if (item.Nodes.Count > 0)
                    {
                        this.CheckTreeViewNode(item, isChecked);
                    }
                }
                bParentNodeTrigger = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void CheckMyParent(TreeNode treeNode, Boolean bCheck)
        {
            try
            {
                if (treeNode == null) return;
                if (treeNode.Parent == null) return;
                bParentNodeTrigger = false;
                treeNode.Parent.Checked = bCheck;
                //CheckMyParent(treeNode.Parent, bCheck);
                bParentNodeTrigger = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion User Defined Functions
    }
}


