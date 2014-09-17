using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShareHolder.Business;
using BMC.EnterpriseClient.Views;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using System.Windows.Forms.VisualStyles;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmShareHolderList : Form
    {
        #region Events
         public ShareHolderEntity entity = new ShareHolderEntity();
        public frmShareHolderList()
        {
            InitializeComponent();
            SetTagProperty();
            LoadShareHolderGrid();
         
            
        }

        private void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CloseCaption";
            this.dataGridViewButtonColumn2.Tag = "Key_Delete";
            this.dataGridViewButtonColumn1.Tag = "Key_Edit_WOShortCut";
            this.Tag = "Key_ShareHolders";
            this.btnAdd.Tag = "Key_AddCaption";
        }

        private void LoadShareHolderGrid()
        {
            try
            {
               
                ShareHolderBusiness bShareHolders = new ShareHolderBusiness();
                
                grdvwShareHolders.DataSource = bShareHolders.GetShareHolders();
                grdvwShareHolders.Columns[0].Width = 50;
                grdvwShareHolders.Columns[1].Width = grdvwShareHolders.Columns[0].Width;
                grdvwShareHolders.Columns[2].Visible = false;
                grdvwShareHolders.Columns[3].Width = 150;
                grdvwShareHolders.Columns[4].Width = 250;
                FormColor();
                SetTagofgrdItemsColumns();
              
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetTagofgrdItemsColumns()
        {
            grdvwShareHolders.Columns["Name"].Tag = "Key_Name";
            grdvwShareHolders.Columns["Description"].Tag = "Key_Description";
            this.ResolveResources();
        }

        private bool EditGridRow(int rowIndex)
        {
            try
            {
                bool result = false;
                result = EditShareHolderGridRow(rowIndex);
                if (result)
                    LoadShareHolderGrid();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        private bool DeleteGridRow(int rowIndex)
        {
            try
            {
                bool result = false;
                result = DeleteShareHolderGridRow(rowIndex);
                if (result)
                    LoadShareHolderGrid();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        private bool DeleteShareHolderGridRow(int rowIndex)
        {
            
            try
            {

                bool result = false;
                string strShareHolderName = grdvwShareHolders["Name", rowIndex].Value.ToString();
                int ShareHolderId1 = Convert.ToInt32 (grdvwShareHolders["Id", rowIndex].Value.ToString()) ;

                if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_SHL_DEL") + "'" + strShareHolderName + "'" + this.GetResourceTextByKey("Key_Question"), this.Text) == DialogResult.Yes)
                {
                    ShareHolderBusiness objShareHolder = new ShareHolderBusiness();
                    int ShareHolderId = -1;
                    int.TryParse(grdvwShareHolders["Id", rowIndex].Value.ToString(), out ShareHolderId);

                    bool? bExists = false;
                    objShareHolder.CheckShareholder_ExistsIn_PSG_Or_ESG(ShareHolderId, ref bExists);

                    if (bExists.GetValueOrDefault())
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_SHL_NAME") + " '" + strShareHolderName + "'" + this.GetResourceTextByKey(1, "MSG_ENT_SHL_GROUP"), this.Text);
                        return false;
                    }

                    if (objShareHolder.DeleteShareHolder(ShareHolderId))
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_SHL_NAME") + " '" + strShareHolderName + "'" + this.GetResourceTextByKey(1, "MSG_ENT_SHL_SUCCESS"), this.Text);
                        //del-ShareHolderId,ShareHolderName,ShareHolder Describtion
                    }
                    result = true;

                    AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                    {

                        business.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            EnterpriseModuleName = ModuleNameEnterprise.ShareHolder,
                            Audit_Screen_Name = "ShareHolder|ShareHolderAddEdit",
                            Audit_Desc = "ShareHolder Deleted " + "ShareHolder ID: " + ShareHolderId1 + "; ShareHolderName: " + strShareHolderName,
                            AuditOperationType = OperationType.DELETE,
                            Audit_User_ID = AppEntryPoint.Current.UserId,
                            Audit_User_Name = AppEntryPoint.Current.UserName
                        }, false);
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }


        private bool EditShareHolderGridRow(int rowIndex)
        {
            try
            {
                int ShrID = -1;
                int.TryParse(grdvwShareHolders["Id", rowIndex].Value.ToString(), out ShrID);
                frmShareHolderAddEdit shrAdd = new frmShareHolderAddEdit(FormEditTypes.Edit, ShrID);
                shrAdd.IsUpdate = true;

                ShareHolderBusiness objShareHolder = new ShareHolderBusiness();
                shrAdd.ShareHolder.Name = grdvwShareHolders["Name", rowIndex].Value.ToString();
                shrAdd.ShareHolder.Description = grdvwShareHolders["Description", rowIndex].Value.ToString();
                shrAdd.ShowDialog();
                
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (new frmShareHolderAddEdit(FormEditTypes.Add, 0).ShowDialogExAndDestroy(this))
                {
                    LoadShareHolderGrid();
                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private void grdvwShareHolders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                int RowIndex = e.RowIndex;
                if (RowIndex < 0)
                    return;

                bool EditRow = (e.ColumnIndex == 0);
                bool DeleteRow = (e.ColumnIndex == 1);

                if (!((DeleteRow) || (EditRow)))
                    return;

                if ((DeleteRow) && (e.RowIndex <= 2))
                {
                    //this.ShowInfoMessageBox("First 3 Share Holders cannot be Deleted.", this.Text);
                    return;
                }

                if (EditRow)
                    EditGridRow(RowIndex);
                else if (DeleteRow)
                    DeleteGridRow(RowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        private void frmShareHolderList_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            FormColor();
        }
        public void FormColor()
           {
            try
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = new Font(grdvwShareHolders.Font, FontStyle.Bold);
                style.BackColor = Color.AntiqueWhite;
                style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                for (int i = 0; i < grdvwShareHolders.Rows.Count; i++)
                {
                    if (i <= 2)
                    {
                    
                  
                        switch (i)
                        {
                            case 0:
                                grdvwShareHolders.Rows[i].Cells[1] = new DataGridViewTextBoxCell() { Value = "Operator", Style = style };
                                break;
                            case 1:
                                grdvwShareHolders.Rows[i].Cells[1] = new DataGridViewTextBoxCell() { Value = "Government", Style = style };    
                                break;
                            case 2:
                                grdvwShareHolders.Rows[i].Cells[1] = new DataGridViewTextBoxCell() { Value = "Retailer", Style = style };

                                break;
                        }


                    }
                    else
                    {
                        break;
                    }
                }
                grdvwShareHolders.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void grdvwShareHolders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        
    }

}






