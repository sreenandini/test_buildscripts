using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseClient.Resources;

namespace BMC.EnterpriseClient.Views.ServiceCalls
{
    public partial class UcSAFaultCodes : UserControlBase
    {
        private IServiceAdmin _admin = null;

        private ServiceCallFaultGroupEntity _newFaultGroup = new ServiceCallFaultGroupEntity()
        {
            Call_Group_ID = 0,
        };
        private ServiceCallRemedyEntity _newFault = new ServiceCallRemedyEntity()
        {
            Call_Remedy_ID = 0,
        };

        public UcSAFaultCodes(IServiceAdmin admin)
        {
            _admin = admin;
            InitializeComponent();
        }

        protected override void LoadControlInternal()
        {
            base.LoadControlInternal();

            this.SelectedFaultGroup = null;
            this.SelectedFault = null;
            this.FillFaultGroups(0);
        }

        private void FillFaultGroups(int id)
        {
            ModuleProc PROC = new ModuleProc("", "FillFaultGroups");

            try
            {
                ServiceCallFaultGroupsEntity entities = _admin.Business.GetFaultGroups();
                lvFaultGroups.AddListViewItem<ServiceCallFaultGroupEntity>(entities,
                    (e, i) =>
                    {
                        i.SubItems.Add(e.Call_Group_Description);
                        i.SubItems.Add(e.Call_Group_Reference);
                        if (e.Call_Group_ID == id) i.Selected = true;
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void FillFaults(int callGroupId, int id)
        {
            ModuleProc PROC = new ModuleProc("", "FillFaults");

            try
            {
                ServiceCallFaultsEntity entities = _admin.Business.GetFaults(callGroupId);
                lvFaults.AddListViewItem<ServiceCallFaultEntity>(entities,
                    (e, i) =>
                    {
                        i.Text = e.Call_Fault_Reference;
                        //i.SubItems.Add(e.Call_Remedy_Attract_Downtime.SafeValue() ? "Yes" : "No");
                        i.SubItems.Add(e.Call_Fault_Description);
                        if (e.Call_Fault_ID == id) i.Selected = true;
                    });
                if (lvFaults.Items.Count == 0)
                {
                    this.SelectedFault = null;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void lvFaultGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "lvFaultGroups_SelectedIndexChanged");

            try
            {
                if (lvFaultGroups.SelectedItems != null &&
                    lvFaultGroups.SelectedItems.Count > 0)
                {
                    ServiceCallFaultGroupEntity entity = lvFaultGroups.SelectedItems[0].Tag as ServiceCallFaultGroupEntity;
                    this.SelectedFaultGroup = entity;
                    this.FillFaults(entity.Call_Group_ID, 0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void lvFaults_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "lvFaultGroups_SelectedIndexChanged");

            try
            {
                if (lvFaults.SelectedItems != null &&
                    lvFaults.SelectedItems.Count > 0)
                {
                    ServiceCallFaultEntity entity = lvFaults.SelectedItems[0].Tag as ServiceCallFaultEntity;
                    this.SelectedFault = entity;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private ServiceCallFaultGroupEntity _selectedFaultGroup = null;

        public ServiceCallFaultGroupEntity SelectedFaultGroup
        {
            get { return _selectedFaultGroup; }
            set
            {
                _selectedFaultGroup = value;
                if (value != null)
                {
                    txtFaultGroupDescription.Text = value.Call_Group_Description;
                    txtFaultGroupReference.Text = value.Call_Group_Reference;
                    ChkDowntime.Checked = value.Call_Group_Downtime.SafeValue();
                    chkGroupLogEngineerChange.Checked = value.Call_Group_Log_Engineer_Change.SafeValue();
                    ChkGroupUse.Checked = !value.Call_Group_End_Date.IsEmpty();
                }
                else
                {
                    txtFaultGroupDescription.Text = string.Empty;
                    txtFaultGroupReference.Text = string.Empty;
                    ChkDowntime.Checked = false;
                    chkGroupLogEngineerChange.Checked = false;
                    ChkGroupUse.Checked = false;
                }
            }
        }

        private ServiceCallFaultEntity _selectedFault = null;

        public ServiceCallFaultEntity SelectedFault
        {
            get { return _selectedFault; }
            set
            {
                _selectedFault = value;
                if (value != null)
                {
                    txtFaultDescription.Text = value.Call_Fault_Description;
                    txtFaultReference.Text = value.Call_Fault_Reference;
                    ChkFaultUse.Checked = !value.Call_Fault_End_Date.IsEmpty();
                }
                else
                {
                    txtFaultDescription.Text = string.Empty;
                    txtFaultReference.Text = string.Empty;
                    ChkFaultUse.Checked = false;
                }
            }
        }

        private void btnApply1_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "btnApply1_Click");

            try
            {
                if (txtFaultGroupReference.Text.IsEmpty())
                {
                    this.ShowInfoMessageBox(MessageResources.MSG_SA_FC_FG_REF);
                    txtFaultGroupReference.Focus();
                    return;
                }
                else if (txtFaultGroupDescription.Text.IsEmpty())
                {
                    this.ShowInfoMessageBox(MessageResources.MSG_SA_FC_FG_DESC);
                    txtFaultGroupDescription.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void btnApply2_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "btnApply1_Click");

            try
            {
                if (txtFaultReference.Text.IsEmpty())
                {
                    this.ShowInfoMessageBox(MessageResources.MSG_SA_FC_FA_REF);
                    txtFaultReference.Focus();
                    return;
                }
                else if (txtFaultDescription.Text.IsEmpty())
                {
                    this.ShowInfoMessageBox(MessageResources.MSG_SA_FC_FA_DESC);
                    txtFaultDescription.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void btnNew1_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "btnNew1_Click");

            try
            {
                ServiceCallFaultGroupEntity entity = _admin.Business.InsertFaultGroup();
                this.FillFaultGroups(entity.Call_Group_ID);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void btnNew2_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "btnNew2_Click");

            try
            {
                if (lvFaultGroups.SelectedItems.Count > 0)
                {
                    ServiceCallFaultGroupEntity parentEntity = lvFaultGroups.SelectedItems[0].Tag as ServiceCallFaultGroupEntity;
                    if (parentEntity != null)
                    {
                        ServiceCallFaultEntity entity = _admin.Business.InsertFaults(parentEntity.Call_Group_ID);
                        this.FillFaults(parentEntity.Call_Group_ID, entity.Call_Fault_ID);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

        }
    }
}
