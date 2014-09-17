using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class Ucimages : UserControl, IAdminSite
    {
        private ToolTip ToolTip1 = null;
        private const int MaxLengthPath = 255;//Referred from DB
        public Ucimages()
        {
            InitializeComponent();
            ToolTip1 = new ToolTip();
            // Set Tags for controls
            SetTagProperty();
        }
        private void SetTagProperty()
        {
            try
            {
                this.btnsiteimage.Tag = "Key_Change";
                this.btnSitePlan.Tag = "Key_Change";
                this.lblSiteImage.Tag = "Key_SiteImageColon";
                this.lblSitePlan.Tag = "Key_SitePlanColon";

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void Ucimages_Load(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Load details..
        /// </summary>
        /// <param name="entity"></param>
        public void LoadDetails(AdminSiteEntity entity)
        {
            try
            {
                LogManager.WriteLog("Inside LoadDetails", LogManager.enumLogLevel.Info);
                txtSiteImage.Text = entity.Site_Image_Reference;
                txtSitePlan.Text = entity.Site_Image_Reference_2;
                if (entity.Site_ID == 0)
                    tblimages.Enabled = false;
                else
				{
                    tblimages.Enabled = true;
					if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"))
            		{
                		tblimages.Enabled = false;
		            }
				}
                LogManager.WriteLog("End LoadDetails", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Button site image click and get the site image path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsiteimage_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnsiteimage_Click", LogManager.enumLogLevel.Info);
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Title = this.GetResourceTextByKey("Key_SiteImage");
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;
                openFileDialog1.DefaultExt = "All files";
                openFileDialog1.Filter = "Images(*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF||";//" + "All files (*.*)|*.*
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.ReadOnlyChecked = true;
                openFileDialog1.ShowReadOnly = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtSiteImage.Text = openFileDialog1.FileName;
                    if (txtSiteImage.TextLength > MaxLengthPath)
                    {
                        //this.ShowInfoMessageBox("Please select a small Path.");
                        Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_IMAGES_SELECT"), this.ParentForm.Text);
                        txtSiteImage.Text = "";                      
                    }                  
                }
                LogManager.WriteLog("End btnsiteimage_Click", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        /// <summary>
        /// Site plan button click open dialog box  and get image path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSitePlan_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnSitePlan_Click", LogManager.enumLogLevel.Info);
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = @"E:\";
                openFileDialog1.Title = this.GetResourceTextByKey("Key_SitePlan");
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;
                openFileDialog1.DefaultExt = "All files";
                openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF||";//" + "All files (*.*)|*.*
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.ReadOnlyChecked = true;
                openFileDialog1.ShowReadOnly = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtSitePlan.Text = openFileDialog1.FileName;
                    if (txtSitePlan.TextLength > MaxLengthPath)
                    {
                       // this.ShowInfoMessageBox("Please select a small Path.");
                        Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_IMAGES_SELECT"), this.ParentForm.Text);
                        txtSitePlan.Text = "";
                    }                    
                }
                LogManager.WriteLog("End btnSitePlan_Click", LogManager.enumLogLevel.Info);
        }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Save details to entity on apply click
        /// </summary>
        /// <param name="entity"></param>
        public bool SaveDetails(AdminSiteEntity entity)
        {
            try
            {
                LogManager.WriteLog("Inside SaveDetails", LogManager.enumLogLevel.Info);
                entity.Site_Image_Reference = txtSiteImage.Text;
                entity.Site_Image_Reference_2 = txtSitePlan.Text;
                LogManager.WriteLog("End SaveDetails", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        private void txtSitePlan_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSiteImage_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void textBox_MouseHover(object sender, EventArgs e)
        {
            SetToolTip((Control)sender, 60);
        }

        /// <summary>
        /// Set tooltip to the ComboBoxes 
        /// </summary>
        /// <param name="comboBox1"></param>
        /// <param name="length"></param>
        private void SetToolTip(Control windowsControl, int length)
        {
            String caption = "" + windowsControl.Text;
            ToolTip1.SetToolTip(windowsControl, (caption.Trim().Length > length) ? caption : "");
            ToolTip1.AutoPopDelay = 2000;
            ToolTip1.InitialDelay = 0;
            ToolTip1.ReshowDelay = 100;
            ToolTip1.ShowAlways = true;
        }       
    }
}
