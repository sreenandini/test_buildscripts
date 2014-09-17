using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using BMC.ComponentVerification.BusinessLayer;
using BMC.ComponentVerification.BusinessEntities;
using BMC.Common.ExceptionManagement;
using BMC.ComponentVerification.WebServices;
using System.Data.SqlClient;
using System.Xml.Linq;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using BMC.ComponentVerification.DataAccess;

namespace BMC.ComponentVerification.UI
{
    public partial class ViewComponents : Form
    {
        #region Variables

        ComponentTypeAccessor cta;
        VerificationDemandAccessor vda;
        string strCVCaption = "Component Verification";
        string strCVDCaption = "Component Details";

        #endregion Variables

        #region Constructor

        public ViewComponents()
        {
            InitializeComponent();
            cta = new ComponentTypeAccessor();
            vda = new VerificationDemandAccessor();
        }

        #endregion Constructor

        #region Events

        private void ViewComponents_Load(object sender, EventArgs e)
        {
            try
            {
                FillTree();

                //Set Date.
                dtPickerFrom.Value = DateTime.Now.AddDays(-7);
                dtPickerTo.Value = DateTime.Now;

                //Load Search Criteria Section Data.
                LoadComponentTypesCombo();
                LoadVerificationTypesCombo();

                LoadSiteNameDetails();
                LoadMachineDetails(999999, cmbMachineSerial);
                LoadCompNameDetails("ALL");

                //Load Request Ver Section.
                LoadSiteDetails();
            }
            catch (Exception exViewComponents_Load)
            {
                ExceptionManager.Publish(exViewComponents_Load);
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateComponentDetails ccd = new CreateComponentDetails();

            //Subscribe to the RefreshComponentSEvent.
            ccd.RefreshComponentsEvent += new CreateComponentDetails.RefreshComponentsDelegate(FillTree);

            ccd.ShowDialog();
        }

        private void btnEditComponent_Click_1(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = tvComponetDetails.SelectedNode;
                if (node != null && node.Parent != null)
                {
                    CTreeNode selectednode = (CTreeNode)tvComponetDetails.SelectedNode;
                    CreateComponentDetails ccd = new CreateComponentDetails(selectednode.NodeElement.CCD_ID);
                    ccd.ShowDialog();
                }
                else
                    MessageBox.Show("Please select a Component to modify.", strCVDCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exbtnEditComponent_Click)
            {
                ExceptionManager.Publish(exbtnEditComponent_Click);
            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            FillTree();
        }

        private void cmbSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSite.SelectedIndex > -1 && cmbSite.Focused == true)
                {
                    SiteDetailsData data = (SiteDetailsData)cmbSite.SelectedItem;
                    cmbSerial.Enabled = true;
                    LoadMachineDetails(data.Site_ID, cmbSerial);
                }
            }
            catch (Exception excmbSite_SelectedIndexChanged)
            {
                ExceptionManager.Publish(excmbSite_SelectedIndexChanged);
            }
        }

        private void cmbSerial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSerial.SelectedIndex > -1 && cmbSerial.Focused == true)
                {
                    MachineDetailsData data = (MachineDetailsData)cmbSerial.SelectedItem;
                    cmbComp.Enabled = true;
                    LoadMachineCompDetails();
                }
            }
            catch (Exception excmbSerial_SelectedIndexChanged)
            {
                ExceptionManager.Publish(excmbSerial_SelectedIndexChanged);
            }
        }

        private void cmbSiteName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSiteName.SelectedIndex > -1 && cmbSiteName.Focused == true)
                {
                    SiteDetailsData data = (SiteDetailsData)cmbSiteName.SelectedItem;
                    LoadMachineDetails((cmbSiteName.SelectedIndex == 0 ? 999999 : data.Site_ID), cmbMachineSerial);
                }
            }
            catch (Exception excmbSiteName_SelectedIndexChanged)
            {
                ExceptionManager.Publish(excmbSiteName_SelectedIndexChanged);
            }
        }

        /// <summary>
        /// Machine Serial Selected Change event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMachineSerial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //Validations.
                if (dtPickerFrom.Value > dtPickerTo.Value)
                {
                    MessageBox.Show("'From Date' cannot be greater than 'To Date'.", strCVCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtPickerFrom.Focus();
                    return;
                }

                if (dtPickerTo.Value > DateTime.Now)
                {
                    MessageBox.Show("'To Date' cannot be greater than the current date.", strCVCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtPickerTo.Focus();
                    return;
                }

                //Fill the Grid.
                FillGrid();
            }
            catch (Exception exbtnSearch_Click)
            {
                ExceptionManager.Publish(exbtnSearch_Click);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Reset Controls.
            ResetAllSearchControls();

           
        }

        private void dgCompDetails_Paint_1(object sender, PaintEventArgs e)
        {
            try
            {
                foreach (DataGridViewColumn dgc in dgCompDetails.Columns)
                    dgc.HeaderText = dgc.HeaderText.Replace("_", " ");
            }
            catch (Exception exdgCompDetails_Paint)
            {
                ExceptionManager.Publish(exdgCompDetails_Paint);
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            /*New Logic try
            {
                VerificationOnDemand vod = new VerificationOnDemand();
                vod.Show();
            }
            catch (Exception exbtnVerify_Click)
            {
                ExceptionManager.Publish(exbtnVerify_Click);
                LogManager.WriteLog("CV - Exception occured in On Demand Verification" + exbtnVerify_Click.Message, LogManager.enumLogLevel.Info);
            }
            */

            #region OldLogic
                       
            var iCompId = 0;
            var strSerialNo = string.Empty;
            var strMessage = string.Empty;
            var IsSuccess = false;
            try
            {
                if (cmbSite.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a Site.", strCVCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbSite.Focus();
                    return;
                }

                if (!(cmbSite.SelectedIndex > -1 && cmbSerial.SelectedIndex > -1))
                {
                    MessageBox.Show("Please select a Machine.", strCVCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbSerial.Focus();
                    return;
                }

                strSerialNo = ((MachineDetailsData)cmbSerial.SelectedItem).Machine_Manufacturers_Serial_No;

                if (!(cmbSite.SelectedIndex > -1 && cmbSerial.SelectedIndex > -1 && cmbComp.SelectedIndex > -1))
                {
                    MessageBox.Show("Please select a Component.", strCVCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbComp.Focus();
                    return;
                }

                iCompId = ((ComponentTypesData)cmbComp.SelectedItem).CCT_Code;

                try
                {
                    var strSiteId = ((SiteDetailsData)cmbSite.SelectedItem).Site_ID.ToString();
                    var webUrl = SqlHelper.ExecuteScalar(DbUtilities.GetConnectionString(), CommandType.Text,
                                                       "Select WebURL From Site Where Site_Id = " + strSiteId.Trim()).ToString();
                    var Site_Code = SqlHelper.ExecuteScalar(DbUtilities.GetConnectionString(), CommandType.Text,
                                                       "Select Site_code From Site Where Site_Id = " + strSiteId.Trim()).ToString();


                    LogManager.WriteLog("CV - On Demand Verification Calling webservice for Site  - " + strSiteId + "Site Code : " + Site_Code + " WebURL :" + webUrl, LogManager.enumLogLevel.Info);
                    Proxy proxy = new Proxy(Site_Code);
                    proxy.WebURL = webUrl;  
                    
                    var result  = proxy.GetMachineComponentStatus(strSerialNo, iCompId);
                    LogManager.WriteLog("CV On Demand Verification  GetMachineComponentStatus returned " + result.ToString(), LogManager.enumLogLevel.Info);
                    switch (result)
                    {
                        case 0:
                            LogManager.WriteLog(string.Format("CV - On Demand Verification Calling ImportOnDemandVerificationDetails for Serial no {0} , CompId {1}", strSerialNo, iCompId), LogManager.enumLogLevel.Info);
                            IsSuccess = proxy.ImportOnDemandVerificationDetails("2:" + strSerialNo, iCompId);
                            if (IsSuccess)                            
                                strMessage = "Component Verification requested for the selected Component.";                               
                            else
                                strMessage = "On demand Verification cannot be requested .";
                            break;
                        case 1:
                            strMessage = "Invalid Machine Serial Number. Please select another Machine.";                            
                            break;
                        case 2:
                            strMessage = "Invalid Component Type. Please select another Component.";                            
                            break;
                        case 3:
                            strMessage = "Machine and Component is not linked. Please select another Component.";                            
                            break;
                        case 4:
                            strMessage = "Component Verification is already in progress for the selected Component.";                            
                            break;
                        case 5:
                        default:
                            strMessage = "On demand Verification cannot be requested .";                            
                            break;
                    }                    
                }
                catch (Exception exProxy)
                {
                    LogManager.WriteLog(string.Format("CV - On Demand Verification Calling webservice Failed - " + exProxy.Message), LogManager.enumLogLevel.Error);
                    vda.InsertComponentVerificationData(iCompId, strSerialNo);
                    LogManager.WriteLog(string.Format("CV - On Demand Verification Calling InsertComponentVerificationData for Serial no {0} , CompId {1}", strSerialNo, iCompId), LogManager.enumLogLevel.Info);
                    strMessage = "Component Verification requested for the selected Component.";                    
                }                
                MessageBox.Show(strMessage, strCVCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            catch (Exception exbtnVerify_Click)
            {
                LogManager.WriteLog("CV - Exception occured in On Demand Verification" + exbtnVerify_Click.Message, LogManager.enumLogLevel.Error);
                //ExceptionManager.Publish(exbtnVerify_Click);
                
            }
            #endregion
        }

        /// <summary>
        /// Clear functionality for Request Verification.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRVClear_Click(object sender, EventArgs e)
        {
            try
            {
                Reset();
            }
            catch (Exception exbtnRVClear_Click)
            {
                ExceptionManager.Publish(exbtnRVClear_Click);
            }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// Fill the Grid.
        /// </summary>
        private void FillGrid()
        {
            string strCompType = string.Empty;
            string strVerType = string.Empty;
            string strSiteName = string.Empty;
            string strSerial = string.Empty;
            string strCompName = string.Empty;

            try
            {
                if (cmbCompType.SelectedIndex > 0)
                {
                    strCompType = cmbCompType.SelectedValue.ToString();
                }

                if (cmbVerType.SelectedIndex > 0)
                {
                    strVerType = cmbVerType.SelectedValue.ToString();
                }

                if (cmbSiteName.SelectedIndex > 0)
                {
                    strSiteName = cmbSiteName.SelectedValue.ToString();
                }

                if (cmbMachineSerial.SelectedIndex > 0)
                {
                    strSerial = cmbMachineSerial.SelectedValue.ToString();
                }

                if (cmbCompName.SelectedIndex > 0)
                {
                    strCompName = cmbCompName.SelectedValue.ToString();
                }

                DateTime dtStartDate;
                DateTime dtEndDate;

                try
                {
                    dtStartDate = Convert.ToDateTime(dtPickerFrom.Value.ToShortDateString() + " 00:00:00");
                }
                catch (Exception exStartDate)
                {
                    dtStartDate = Convert.ToDateTime(dtPickerFrom.Value.ToShortDateString() + " 00:00:00");
                    ExceptionManager.Publish(exStartDate);
                }

                try
                {
                    dtEndDate = Convert.ToDateTime(dtPickerTo.Value.ToShortDateString() + " 23:59:59");
                }
                catch (Exception exEndDate)
                {
                    dtEndDate = Convert.ToDateTime(dtPickerTo.Value.ToShortDateString() + " 23:59:59");
                    ExceptionManager.Publish(exEndDate);
                }

                List<VerificationCompDetailsData> compDetails = cta.GetVerificationComponentDetails(dtStartDate,
                    dtEndDate, strCompType, strVerType, strSiteName, strSerial, strCompName);

                if (compDetails.Count > 0)
                {
                    dgCompDetails.DataSource = compDetails;
                }
                else
                {
                    MessageBox.Show("No Data exists for the selected search criteria.", strCVCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ResetAllSearchControls();
                }
            }
            catch (Exception exFillGrid)
            {
                ExceptionManager.Publish(exFillGrid);
            }
        }

        /// <summary>
        /// Load the Component Details.
        /// </summary>
        private void LoadComponentDetails()
        {
            try
            {
                var types = cta.GetComponentTypes();

                foreach (var node in types)
                {
                    tvComponetDetails.Nodes.Add(new CTreeNode(node.CCT_Name));
                }
            }
            catch (Exception exLoadComponentDetails)
            {
                ExceptionManager.Publish(exLoadComponentDetails);
            }
        }

        /// <summary>
        /// Load the Component Types.
        /// </summary>
        private void LoadComponentTypes()
        {
            try
            {
                List<ComponentTypesData> types = cta.GetComponentTypes();

                foreach (ComponentTypesData node in types)
                    tvComponetDetails.Nodes.Add(new TreeNode(node.CCT_Name));

            }
            catch (Exception exLoadComponentTypes)
            {
                ExceptionManager.Publish(exLoadComponentTypes);
            }
        }

        /// <summary>
        /// Fill the Tree View.
        /// </summary>
        private void FillTree()
        {
            try
            {
                tvComponetDetails.Nodes.Clear();
                var types = cta.GetComponentTypes();

                foreach (ComponentTypesData node in types)
                {
                    var cDetails = cta.GetAllComponentDetails();
                    var nodes = GetComponentDetails(cDetails, node.CCT_Name);
                    var tnode = new CTreeNode(node.CCT_Name, nodes);
                    tnode.ExpandAll();
                    tvComponetDetails.Nodes.Add(tnode);
                }
            }
            catch (Exception exFillTree)
            {
                ExceptionManager.Publish(exFillTree);
            }
        }

        /// <summary>
        /// Retrieve the Component Details.
        /// </summary>
        /// <param name="cDetails"></param>
        /// <param name="strcname"></param>
        /// <returns></returns>
        private static CTreeNode[] GetComponentDetails(IEnumerable<AllComponentDetailsData> cDetails, string strcname)
        {
            try
            {
                var treeNodes = new CTreeNode[cDetails.Where(x => x.CCT_Name == strcname).Where(x => x.CCD_ID != 0).Select(x => x.CCD_ID).Distinct().Count()];
                var nodeIndex = 0;

                foreach (var cdetail in cDetails.Where(x => x.CCT_Name == strcname).Where(x => x.CCD_ID != 0).Select(x => x))
                {
                    treeNodes[nodeIndex] = new CTreeNode(cdetail,
                                                         cdetail.CCD_Name, true
                                               );
                    nodeIndex++;
                }

                return treeNodes;
            }
            catch (Exception exGetComponentDetails)
            {
                ExceptionManager.Publish(exGetComponentDetails);
                return new CTreeNode[0];
            }
        }

        /// <summary>
        /// Load Comp Types Combo.
        /// </summary>
        private void LoadComponentTypesCombo()
        {
            List<ComponentTypesData> lstCompTypes = new List<ComponentTypesData>();

            try
            {
                //Clear the combo.
                cmbCompType.Items.Clear();

                //Insert a dummy record.
                ComponentTypesData objCompTypes = new ComponentTypesData();
                objCompTypes.CCT_Code = 0;
                objCompTypes.CCT_Name = "N/A";
                lstCompTypes.Add(objCompTypes);

                foreach (ComponentTypesData objComponentTypesData in cta.GetComponentTypes())
                {
                    lstCompTypes.Add(objComponentTypesData);
                }

                cmbCompType.DisplayMember = "CCT_Name";
                cmbCompType.ValueMember = "CCT_Code";
                cmbCompType.DataSource = lstCompTypes;
                cmbCompType.SelectedIndex = 0;

            }
            catch (Exception exLoadComponentTypesCombo)
            {
                ExceptionManager.Publish(exLoadComponentTypesCombo);
            }
        }

        /// <summary>
        /// Load Verification Types Combo.
        /// </summary>
        private void LoadVerificationTypesCombo()
        {
            List<VerificationTypesData> lstVerTypes = new List<VerificationTypesData>();

            try
            {
                //Clear the combo.
                cmbVerType.Items.Clear();

                //Insert a dummy record.
                VerificationTypesData objVerTypes = new VerificationTypesData();
                objVerTypes.CVT_Code = 0;
                objVerTypes.CVT_Name = "N/A";
                lstVerTypes.Add(objVerTypes);

                foreach (VerificationTypesData objVerData in cta.GetVerificationTypes())
                {
                    lstVerTypes.Add(objVerData);
                }

                cmbVerType.DisplayMember = "CVT_Name";
                cmbVerType.ValueMember = "CVT_Code";
                cmbVerType.DataSource = lstVerTypes;
                cmbVerType.SelectedIndex = 0;

            }
            catch (Exception exLoadVerificationTypesCombo)
            {
                ExceptionManager.Publish(exLoadVerificationTypesCombo);
            }
        }

        /// <summary>
        /// Load Site Name Details.
        /// </summary>
        private void LoadSiteNameDetails()
        {
            List<SiteDetailsData> lstSiteData = new List<SiteDetailsData>();

            try
            {
                //Clear the combo.
                cmbSiteName.Items.Clear();

                //Insert a dummy record.
                SiteDetailsData objSiteTypes = new SiteDetailsData();
                objSiteTypes.Site_ID = 0;
                objSiteTypes.Site_Name = "N/A";
                lstSiteData.Add(objSiteTypes);

                foreach (SiteDetailsData objSiteDetailsData in vda.GetAllSites())
                {
                    lstSiteData.Add(objSiteDetailsData);
                }
                cmbSiteName.DataSource = lstSiteData;
                cmbSiteName.DisplayMember = "Site_Name";
                cmbSiteName.ValueMember = "Site_ID";
                cmbSiteName.SelectedIndex = 0;

            }
            catch (Exception exLoadSiteNameDetails)
            {
                ExceptionManager.Publish(exLoadSiteNameDetails);
            }
        }

        /// <summary>
        /// Load Site Details.
        /// </summary>
        private void LoadSiteDetails()
        {
            try
            {
                List<SiteDetailsData> ds = vda.GetAllSites();
                cmbSite.DataSource = ds;
                cmbSite.DisplayMember = "Site_Name";
                cmbSite.ValueMember = "Site_ID";
                cmbSite.SelectedIndex = -1;

            }
            catch (Exception exLoadSiteDetails)
            {
                ExceptionManager.Publish(exLoadSiteDetails);
            }
        }

        /// <summary>
        /// Load Machine Details.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="cmbMachineSerialNo"></param>
        private void LoadMachineDetails(int siteId, ComboBox cmbMachineSerialNo)
        {
            List<MachineDetailsData> lstMachines = new List<MachineDetailsData>();

            try
            {
                //Clear the combo.
                cmbMachineSerialNo.DataSource = null;

                if (siteId == 999999)
                {
                    //Insert a dummy record.
                    MachineDetailsData objMachineData = new MachineDetailsData();
                    objMachineData.Machine_Manufacturers_Serial_No = "N/A";
                    lstMachines.Add(objMachineData);

                    foreach (MachineDetailsData objMachine in vda.GetAllMachinesForSite(siteId))
                    {
                        lstMachines.Add(objMachine);
                    }
                }
                else
                {
                    //Insert a dummy record.
                    if (cmbMachineSerialNo.Name == "cmbMachineSerial")
                    {
                        MachineDetailsData objMachineData = new MachineDetailsData();
                        objMachineData.Machine_Manufacturers_Serial_No = "N/A";
                        lstMachines.Add(objMachineData);

                        foreach (MachineDetailsData objMachine in vda.GetAllMachinesForSite(siteId))
                        {
                            lstMachines.Add(objMachine);
                        }
                    }
                    else
                    {
                        lstMachines = vda.GetAllMachinesForSite(siteId);
                    }
                }

                cmbMachineSerialNo.DisplayMember = "Machine_Manufacturers_Serial_No";
                cmbMachineSerialNo.ValueMember = "Machine_Manufacturers_Serial_No";
                cmbMachineSerialNo.DataSource = lstMachines;

                if (siteId == 999999 || cmbMachineSerialNo.Name == "cmbMachineSerial")
                {
                    cmbMachineSerialNo.SelectedIndex = 0;
                }
                else
                {
                    cmbMachineSerialNo.SelectedIndex = -1;
                }

            }
            catch (Exception exLoadMachineDetails)
            {
                ExceptionManager.Publish(exLoadMachineDetails);
            }
        }

        /// <summary>
        /// Load Machine Comp Details.
        /// </summary>
        /// <param name="p"></param>
        private void LoadMachineCompDetails()
        {
            try
            {
                List<ComponentTypesData> ds = cta.GetComponentTypes();

                cmbComp.DisplayMember = "CCT_Name";
                cmbComp.ValueMember = "CCT_Code";
                cmbComp.DataSource = ds;
                cmbComp.SelectedIndex = -1;
            }
            catch (Exception exLoadMachineCompDetails)
            {
                ExceptionManager.Publish(exLoadMachineCompDetails);
            }
        }

        /// <summary>
        /// Load Comp Name Details.
        /// </summary>
        /// <param name="strSerialNo"></param>
        private void LoadCompNameDetails(string strSerialNo)
        {
            List<MachineCompDetails> lstMachineCompDetails = new List<MachineCompDetails>();

            try
            {
                //Clear the combo.
                cmbCompName.DataSource = null;

                //Insert a dummy record.
                MachineCompDetails objMachineCompDetail = new MachineCompDetails();
                objMachineCompDetail.Component_ID = 0;
                objMachineCompDetail.Component_Name = "N/A";
                lstMachineCompDetails.Add(objMachineCompDetail);

                foreach (MachineCompDetails objMachineComp in vda.GetAllComponentsForMachine(strSerialNo))
                {
                    lstMachineCompDetails.Add(objMachineComp);
                }

                cmbCompName.DisplayMember = "Component_Name";
                cmbCompName.ValueMember = "Component_ID";
                cmbCompName.DataSource = lstMachineCompDetails;
                cmbCompName.SelectedIndex = 0;

            }
            catch (Exception exLoadCompNameDetails)
            {
                ExceptionManager.Publish(exLoadCompNameDetails);
            }
        }

        /// <summary>
        /// Reset the Combos.
        /// </summary>
        private void Reset()
        {
            cmbSite.SelectedIndex = -1;
            cmbSerial.SelectedIndex = -1;
            cmbComp.SelectedIndex = -1;

            cmbSerial.Enabled = false;
            cmbComp.Enabled = false;
        }

        /// <summary>
        /// Check the Component Status.
        /// </summary>
        /// <param name="iCompId"></param>
        /// <param name="strSerialNo"></param>
        /// <returns></returns>
        private bool CheckComponentStatus(int iCompId, string strSerialNo)
        {
            var flag = false;

            try
            {
                var result = vda.CheckComponentStatus(iCompId, strSerialNo);

                if (result > 0)
                    flag = true;
                return flag;

            }
            catch (Exception exCheckComponentStatus)
            {
                ExceptionManager.Publish(exCheckComponentStatus);
                return false;
            }
        }

        /// <summary>
        /// Reset the Search Controls.
        /// </summary>
        private void ResetAllSearchControls()
        {
            try
            {
                //Set Date.
                dtPickerFrom.Value = DateTime.Now.AddDays(-7);
                dtPickerTo.Value = DateTime.Now;

                //Reset to N/A.
                cmbCompType.SelectedIndex = 0;
                cmbVerType.SelectedIndex = 0;
                cmbSiteName.SelectedIndex = 0;

                //Reload again.
                LoadMachineDetails(999999, cmbMachineSerial);
                LoadCompNameDetails("ALL");

                List<VerificationCompDetailsData> compDetails = new List<VerificationCompDetailsData>();
                dgCompDetails.DataSource = compDetails;
            }
            catch (Exception exResetAllSearchControls)
            {
                ExceptionManager.Publish(exResetAllSearchControls);
            }
        }

        /// <summary>
        /// Export Component Verification Details.
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="ehID"></param>
        /// <param name="SiteCode"></param>
        public string GetComponentVerificationDetails(string strSerialNo, int iCompId, int iVerificationType)
        {
            System.Xml.Linq.XElement xml = null;
            try
            {
                
                var result = vda.GetCompVerificationRecordForExport(strSerialNo, iCompId, iVerificationType);
                foreach (ComponentVerificationData data in result)
                {
                    xml = data.XMLData;  
                }
                                                  
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                
            }
            return xml.Value;
        }
                
        #endregion Methods

        #region Class

        public class CTreeNode : TreeNode
        {
            public AllComponentDetailsData NodeElement { get; set; }

            public bool IsChildNode { get; set; }

            public CTreeNode(string nodeName)
                : base(nodeName)
            {

            }
            public CTreeNode(AllComponentDetailsData element, string nodeName, bool isChildNode)
                : base(nodeName)
            {
                IsChildNode = isChildNode;
                NodeElement = element;
            }          
            public CTreeNode(string nodeName, TreeNode[] treeNodes)
                : base(nodeName, treeNodes)
            {
                IsChildNode = false;
            }
        }

        #endregion Class


    }
}
