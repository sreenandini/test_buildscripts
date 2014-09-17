/// Source File Name	: ucMeterAnalysisGraph.cs
/// Description		    : Meter analysis user control provides user to view graph data.
/// Revision History
/// Author             Date              Description
/// ---------------------------------------------------
/// Renjish N         12/06/08            Created.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using BMC.Business.Classes;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Windows.Forms.DataVisualization.Charting;
using BMC.Common;

namespace BMCMeterAnalysis
{
    public partial class ucMeterAnalysisGraph : UserControl
    {
        private const string strCompany = "Company";
        private const string strAvg = "Average";
        int _userID = 0;
        # region Constructor
        public ucMeterAnalysisGraph(int userID)
        {
            InitializeComponent();
            PaintGradient();
            _userID = userID;

        }
        private void PaintGradient()
        {

            System.Drawing.Drawing2D.LinearGradientBrush gradBrush;

            System.Drawing.Drawing2D.ColorBlend clrbld;
            clrbld = new System.Drawing.Drawing2D.ColorBlend(3);
            gradBrush = new System.Drawing.Drawing2D.LinearGradientBrush(new
                   Point(0, 0), new Point(this.Width, this.Height), Color.FromArgb(170, 200, 255), Color.White);
            //Color.FromArgb(143, 199, 255)
            Bitmap bmp = new Bitmap(this.Width, this.Height);

            Graphics g = Graphics.FromImage(bmp);

            g.FillRectangle(gradBrush, new Rectangle(0, 0, this.Width, this.Height));

            this.BackgroundImage = bmp;
            this.BackgroundImageLayout = ImageLayout.Stretch;




        }
        # endregion Constructor

        #region User Control Load
        /// <summary>
        /// User control load event.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>12-06-08</DateCreated>
        /// <Parameters></Parameters>
        private void ucMeterAnalysisGraph_Load(object sender, EventArgs e)
        {
            try
            {
                //declare Arraylist.
                ArrayList objCollection = new ArrayList();
                objCollection = MeterAnalysisChartData.objCollection;

                //Object to store graph data./kalai hdcode
                object[,] objGraphData = new object[2, objCollection.Count + 2];

                int iCount = 0;
                //Chart Graph.
                //Make the Graph visible.
                axMSChartMeterGraph.Visible = true;
                //Set the required properties for the chart.



                AddSeries();
                foreach (MeterAnalysisChartData objMACData in objCollection)
                {
                    objGraphData[0, iCount] = objMACData.MachineValue;
                    axMSChartMeterGraph.Series[strCompany].Points.AddY(objGraphData[0, iCount]);
                    if (objMACData.GroupQuantity > 0)
                    {
                        objGraphData[1, iCount] = Math.Round(Convert.ToDecimal((objMACData.GroupValue / objMACData.GroupQuantity)), 2);
                        axMSChartMeterGraph.Series[strAvg].Points.AddY(objGraphData[1, iCount]);
                        //if (cmbDatatype.Text == "AVG DAILY WIN")
                        //    objGraphData[1, iCount] = objMACData.GroupValue / objMACData.GroupQuantity;
                        //else
                        //    objGraphData[1, iCount] = objMACData.GroupValue;
                    }
                    else
                    {
                        objGraphData[1, iCount] = 0;
                        axMSChartMeterGraph.Series[strAvg].Points.AddY(0);
                    }
                    iCount++;
                }
               
                if (objGraphData.Length > 0)
                {
                    axMSChartMeterGraph.Visible = true;
                    if (!(objGraphData.Length > 2))
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            axMSChartMeterGraph.Series[i].MarkerStyle = (i == 1) ? MarkerStyle.Diamond : MarkerStyle.Cross;
                            axMSChartMeterGraph.Series[i].MarkerSize = 15;
                            axMSChartMeterGraph.Series[i].MarkerColor = (i == 1) ? Color.Yellow : Color.DarkRed;
                            axMSChartMeterGraph.Series[i].MarkerBorderColor = Color.Green;
                            axMSChartMeterGraph.Series[i].MarkerBorderWidth = 1;
                        }

                    }
                    axMSChartMeterGraph.Series[strCompany].ChartType = SeriesChartType.Line;
                    axMSChartMeterGraph.Series[strAvg].ChartType = SeriesChartType.Line;
                    axMSChartMeterGraph.Series[strCompany].IsValueShownAsLabel = true;
                    axMSChartMeterGraph.Series[strAvg].IsValueShownAsLabel = true;
                    axMSChartMeterGraph.Series[strCompany].LegendText = this.GetResourceTextByKey("Key_Selected") + " " + MeterAnalysisChartData.strLegendLabelSelected;
                    axMSChartMeterGraph.Series[strAvg].LegendText = this.GetResourceTextByKey("Key_AvgOfAll") + " " + MeterAnalysisChartData.strLegendLabelAll;
                    //Set the legend details.
                    axMSChartMeterGraph.ChartAreas["Default"].AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
                    axMSChartMeterGraph.ChartAreas["Default"].AxisX.CustomLabels.Clear();
                    double iColumnCount = 0.5;
                    //Assign the Column label for the Graph.
                    foreach (MeterAnalysisChartData objMACData in objCollection)
                    {
                        axMSChartMeterGraph.ChartAreas["Default"].AxisX.CustomLabels.Add(iColumnCount, 1 + iColumnCount, Convert.ToDateTime(objMACData.ChartDataLabel).ToString("dd MMM yy"));
                        iColumnCount += 1;
                    }

                }

            }
            catch (Exception exLoadGraphData)
            {
                LogManager.WriteLog("Error in loading Graph." + "-Error Message-" + exLoadGraphData.Message, LogManager.enumLogLevel.Error);
                throw exLoadGraphData;
            }
        }


        #endregion User Control Load

        private void AddSeries()
        {
            try
            {
                axMSChartMeterGraph.Series.Clear();
                Series sc_company = new Series();
                Series sc_Avg = new Series();
                sc_company.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
                sc_company.BorderWidth = 3;
                sc_company.ChartArea = "Default";
                sc_company.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                sc_company.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(65)))), ((int)(((byte)(140)))), ((int)(((byte)(240)))));
                sc_company.Legend = "Default";
                sc_company.MarkerSize = 8;
                sc_company.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                sc_company.Name = strCompany;
                sc_company.ShadowColor = System.Drawing.Color.Black;
                sc_company.ShadowOffset = 2;
                sc_company.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                sc_company.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                sc_Avg.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
                sc_Avg.BorderWidth = 3;
                sc_Avg.ChartArea = "Default";
                sc_Avg.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                sc_Avg.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(10)))));
                sc_Avg.Legend = "Default";
                sc_Avg.MarkerSize = 9;
                sc_Avg.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Diamond;
                sc_Avg.Name = strAvg;
                sc_Avg.ShadowColor = System.Drawing.Color.Black;
                sc_Avg.ShadowOffset = 2;
                sc_Avg.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                sc_Avg.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                axMSChartMeterGraph.Series.Add(sc_company);
                axMSChartMeterGraph.Series.Add(sc_Avg);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
