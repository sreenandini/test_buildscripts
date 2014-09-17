using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMC.Common;

namespace MeterAnalysis
{
    public partial class frmMeterAnalysis : Form
    {
        frmMeterAnalysisGraph frmNew;
        # region Constructor
        int _UserID = 0;
        public frmMeterAnalysis(int UserID)
        {
            _UserID = UserID;
            InitializeComponent(UserID);
            
            // For externalization
            SetTagProperty();
            this.ResolveResources();
        }
        # endregion Constructor
        private static bool BlnOpened = false;

         /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.Tag = "Key_BallyMultiConnectMeterAnalysis";
            this.btnZoomGraph.Tag = "Key_ZoomGraph";

        }

        public static bool FormOpened
        
        {

            get { return BlnOpened; }
            set { BlnOpened = value; }
        }

        #region Events
        /// <summary>
        /// Load new form for Graph.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>12-June-2008</DateCreated>
        /// <Parameters> 
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        /// </Parameters>
        /// <returns>Nothing</returns>
        ///
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          12-June-2008      Intial Version 
        private void btnZoomGraph_Click(object sender, EventArgs e)
        {
            try
            {
                if (BMCMeterAnalysis.ucBMCMeterAnalysis.blnbtnZoomGraphVisible)
                {
                    if (!FormOpened)
                    {
                        FormOpened = true;

                        frmNew = new frmMeterAnalysisGraph(_UserID);
                        frmNew.Show();
                    }
                    else
                    {
                        MessageBox.Show(this.GetResourceTextByKey(1, "MSG_CLOSE_ZOOM"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                    }
                   
                }
                else
                {
                    MessageBox.Show(this.GetResourceTextByKey(1, "MSG_LOAD_GRAPH_WITHMULTIPLE_RECORDS"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
                }
            }
            catch
            {
                MessageBox.Show(this.GetResourceTextByKey(1, "MSG_ERROR_ZOOM_GRAPH"), this.GetResourceTextByKey(1, "MSG_APP_TITLE"));
            }
        }
        #endregion Events
    }
}