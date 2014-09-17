using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BMC.MeterAdjustmentTool.Helpers
{
    public interface IGradientHelper
    {
        Color StartColor { get;set;}

        Color EndColor { get;set;}

        LinearGradientMode GradientMode { get;set;}

        bool RepeatGradient { get; set; }
    }
}
