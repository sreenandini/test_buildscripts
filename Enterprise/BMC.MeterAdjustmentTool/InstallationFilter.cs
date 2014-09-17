using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.MeterAdjustmentTool.Helpers;
using BMC.Common.ExceptionManagement;
using BMC.Common;

namespace BMC.MeterAdjustmentTool
{
    public partial class InstallationFilter : DialogFormBase
    {
        private rsp_GetAllInstallationsWithStatusResult _selItem = null;

        public InstallationFilter(string connectionString,
            rsp_GetAllInstallationsWithStatusResult selItem)
        {
            _selItem = selItem;
            this.ConnectionString = connectionString;
            this.SelectedInstallationNo = -1;
            InitializeComponent();

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            this.Tag = "Key_Installations";
            this.btnOK.Tag = "Key_OKCaption";
            this.btnClear.Tag = "Key_CloseCaption";
        }

        public string ConnectionString { get; private set; }

        public int SelectedInstallationNo { get; set; }

        protected override void LoadChanges()
        {
            base.LoadChanges();

            this.LoadGrid();

            // For externalization
            this.ResolveResources();
        }

        private void LoadGrid()
        {
            try
            {
                using (ExchangeDataContext db = new ExchangeDataContext(this.ConnectionString))
                {
                    dgvData.DataSource = db.FuncGetAllInstallationsWithStatus();
                }

                foreach (DataGridViewColumn dc in dgvData.Columns)
                {
                    // Get Header Texts from Resource
                    switch (dc.Name.ToUpper())
                    {
                        case "SERIAL_NO":           dc.Tag = "Key_SerialNoHeader"; break;
                        case "INSTALLATION_NO":     dc.Tag = "Key_MGMD_Installation_No"; break;
                        case "INSTALLATIONSTATUS":  dc.Tag = "Key_InstallationStatus"; break;
                        case "STOCK_NO":            dc.Tag = "Key_CollSch_Stock_no"; break;
                        case "BAR_POS_NAME":        dc.Tag = "Key_HrlyRdSch_Bar_Pos_Name"; break;
                        case "NAME":                dc.Tag = "Key_Name"; break;
                        case "MODEL_CODE":          dc.Tag = "Key_Model_Code"; break;
                        case "MACHINE_SERIAL_NO":   dc.Tag = "Key_Machine_Serial_No"; break;
                        case "MACHINE_ALTERNATIVE_SERIAL_NUMBERS":  dc.Tag = "Key_Machine_Alternative_Serial_Numbers"; break;
                        case "INSTALLATION_PRICE_OF_PLAY":          dc.Tag = "Key_Installation_Price_Of_Play"; break;
                        case "INSTALLATION_TOKEN_VALUE":            dc.Tag = "Key_Installation_Token_Value"; break;
                        case "INSTALLATION_JACKPOT":                dc.Tag = "Key_Installation_Jackpot"; break;
                        case "ANTICIPATED_PERCENTAGE_PAYOUT":       dc.Tag = "Key_Anticipated_Percentage_Payout"; break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dgvData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    string installationStatus = dgvData.Rows[i].Cells["InstallationStatus"].Value.ToString();
                    if (string.Compare(installationStatus, "CLOSED", true) == 0)
                    {
                        dgvData.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    int installationNo = Convert.ToInt32(dgvData.Rows[i].Cells["Installation_No"].Value.ToString());
                    if (_selItem != null)
                    {
                        if (installationNo == _selItem.Installation_No)
                        {
                            dgvData.FirstDisplayedScrollingRowIndex = i;
                            dgvData.Rows[i].Selected = true;
                            //break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnOK.Enabled = (dgvData.Rows.Count > 0);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.OnAcceptButtonClicked();
        }

        public override bool ValidateUI()
        {
            if (dgvData.SelectedCells.Count < 0)
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_SELECTINSTALLATION"));        //"Please select an installation.");
                dgvData.Focus();
                return false;
            }

            return true;
        }

        protected override bool SaveChanges()
        {
            try
            {
                this.SelectedInstallationNo = Convert.ToInt32(dgvData.Rows[dgvData.SelectedCells[0].RowIndex].Cells["Installation_No"].Value.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return base.SaveChanges();
        }

        private void dgvData_DoubleClick(object sender, EventArgs e)
        {
            if (dgvData.SelectedCells.Count > 0)
            {
                this.OnAcceptButtonClicked();
            }
        }
    }
}
