/// Source File Name	: frmMeterAnalysisGraph.cs
/// Description		    : The form loads the user control to display graph data.
/// Revision History
/// Author             Date              Description
/// ---------------------------------------------------
/// Renjish N         12/06/08            Created.
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
    public partial class frmMeterAnalysisGraph : Form
    {
        # region Constructor
        public frmMeterAnalysisGraph(int userID)
        {
            InitializeComponent(userID);
            PaintGradient();

            // For externalization
            SetTagProperty();
            this.ResolveResources();
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnExit.Tag = "Key_Exit";
            this.Tag = "Key_MeterAnalysisGraph";
        }

        private void PaintGradient()
        {

            System.Drawing.Drawing2D.LinearGradientBrush gradBrush;

            System.Drawing.Drawing2D.ColorBlend clrbld;
            clrbld = new System.Drawing.Drawing2D.ColorBlend(3);
            gradBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new
                   Point(0, 0), new Point(this.Width, this.Height), Color.FromArgb(0, 100, 220), Color.White);
            //Color.FromArgb(143, 199, 255)
            Bitmap bmp = new Bitmap(this.Width, this.Height);

            Graphics g = Graphics.FromImage(bmp);

            g.FillRectangle(gradBrush, new Rectangle(0, 0, this.Width, this.Height));

            this.BackgroundImage = bmp;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        # endregion Constructor

        # region Events
        /// <summary>
        /// Close the form.
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
        private void btnExit_Click(object sender, EventArgs e)
        {
            frmMeterAnalysis.FormOpened = false;
            this.Close();
        }
        private void frmMeterAnalysisGraph_FormClosing(object sender, EventArgs e)
        {
            frmMeterAnalysis.FormOpened = false;
        
        }
        # endregion Events
    }
}