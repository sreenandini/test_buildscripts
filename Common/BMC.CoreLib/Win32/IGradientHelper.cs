using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BMC.CoreLib.Win32
{
    public interface IGradientHelper
    {
        Color StartColor { get;set;}

        Color EndColor { get;set;}

        LinearGradientMode GradientMode { get;set;}

        bool RepeatGradient { get; set; }
    }
}
