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
using System.Text.RegularExpressions;
using BMC.Common.LogManagement;
using BMC.CoreLib.Win32; 
using BMC.EnterpriseClient.Views;


namespace BMC.EnterpriseClient
{
    public partial class Ucnotes : UserControl, IAdminSite
    {


       int  _SiteId=0;
        public Ucnotes()
        {
            InitializeComponent();
            
           // this.btnfontcolor.Image = new Bitmap("fontColorImage.jpg");
        }
        /// <summary>
        /// Font Button click Event for select Font style
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnfont_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnfont click", LogManager.enumLogLevel.Info);
                DialogResult result = fdnotes.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtnotes.SelectionFont = fdnotes.Font;
                }
                LogManager.WriteLog("End btnfont click", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Button Font color click Event for selecting font color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnfontcolor_Click(object sender, EventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnfontcolor click", LogManager.enumLogLevel.Info);
                DialogResult result = clrnotes.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtnotes.SelectionColor = clrnotes.Color;
                }
                LogManager.WriteLog("Inside btnfontcolor click", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        private void LoadNotes()
        {
        }
        /// <summary>
        /// Load details From Entity
        /// </summary>
        /// <param name="entity"></param>
        public void LoadDetails(AdminSiteEntity entity)
        {
            try
            {
                _SiteId = entity.Site_ID;
                if (_SiteId == 0)
                {
                    tblMainFrame.Enabled = false;
                    tblButtons.Enabled = false;
                }
                else
                {
                    tblMainFrame.Enabled = true;
                    tblButtons.Enabled = true;
                }
                LogManager.WriteLog("Inside Load Notes", LogManager.enumLogLevel.Info);
                txtnotes.Rtf = entity.Site_Memo;
                LogManager.WriteLog("End Load Notes", LogManager.enumLogLevel.Info);
                LoadNotes();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (_SiteId!=0 && !AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"))
                {
                    tblMainFrame.Enabled = false;
                }

            }
        }
         /// <summary>
         /// Save Details to Entity
         /// </summary>
         /// <param name="entity"></param>

        public bool SaveDetails(AdminSiteEntity entity)
        {            
            try
            {

               LogManager.WriteLog("Inside Savedetails Notes", LogManager.enumLogLevel.Info);
               entity.Site_Memo=txtnotes.Rtf ;
               LogManager.WriteLog("End Savedetails Notes", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
              ExceptionManager.Publish(ex);
              
            }
            return true;
        }
        
        private void Ucnotes_Load(object sender, EventArgs e)
        {

        }

     
    }
}
