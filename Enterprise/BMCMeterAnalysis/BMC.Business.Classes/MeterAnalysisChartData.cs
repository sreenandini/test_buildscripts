/// Source File Name	: MeterAnalysisChartData.cs
/// Description		: Defines the MeterAnalysisChartData for the Graph.
/// Revision History
/// Author             Date              Description
/// ---------------------------------------------------
/// Renjish N          19-May-2008       Initial Version

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BMC.Business.Classes
{
    public class MeterAnalysisChartData
    {
        private string strChartDataLabel;
        private float fMachineValue,fGroupValue;
        private Int32 iMachineQuantity,iGroupQuantity;
        public static object objGraphData = null;
        public static string strLegendLabelSelected = "";
        public static string strLegendLabelAll = "";
        public static ArrayList objCollection = new ArrayList();

        public float MachineValue
        {
            get { return fMachineValue; }
            set { fMachineValue = value; }
        }
        public float GroupValue
        {
            get { return fGroupValue; }
            set { fGroupValue = value; }
        }
        public Int32 GroupQuantity
        {
            get { return iGroupQuantity; }
            set { iGroupQuantity = value; }
        }
        public string ChartDataLabel
        {
            get { return strChartDataLabel; }
            set { strChartDataLabel = value; }
        }
        public Int32 MachineQuantity
        {
            get { return iMachineQuantity; }
            set { iMachineQuantity = value; }
        }
    }
    
}
