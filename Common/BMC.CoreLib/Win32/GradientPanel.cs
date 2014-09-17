using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace BMC.CoreLib.Win32
{
   public class GradientPanel : Panel, IGradientHelper
   {
       public GradientPanel()
       {
           // Set the styles to make things draw properly:
           base.SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.DoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.SupportsTransparentBackColor,
               true);
           _gradientHelper = new GradientHelper<GradientPanel>(this);
           this.ResizeRedraw = true;
       }

       #region Gradient Related stuff
       private GradientHelper<GradientPanel> _gradientHelper = null;

       private Color _startColor = Color.Empty;

       [Browsable(true)]
       [EditorBrowsable(EditorBrowsableState.Always)]
       [Category("Appearance")]
       public Color StartColor
       {
           get { return _startColor; }
           set
           {
               _startColor = value;
               this.Invalidate();
           }
       }

       private Color _endColor = Color.Empty;

       [Browsable(true)]
       [EditorBrowsable(EditorBrowsableState.Always)]
       [Category("Appearance")]
       public Color EndColor
       {
           get { return _endColor; }
           set
           {
               _endColor = value;
               this.Invalidate();
           }
       }

       private LinearGradientMode _gradientMode = LinearGradientMode.Vertical;

       [Browsable(true)]
       [EditorBrowsable(EditorBrowsableState.Always)]
       [Category("Appearance")]
       public LinearGradientMode GradientMode
       {
           get { return _gradientMode; }
           set
           {
               _gradientMode = value;
               this.Invalidate();
           }
       }

       private bool _repeatGradient = false;

       [Browsable(true)]
       [EditorBrowsable(EditorBrowsableState.Always)]
       [Category("Appearance")]
       public bool RepeatGradient
       {
           get { return _repeatGradient; }
           set
           {
               _repeatGradient = value;
               this.Invalidate();
           }
       }

       /// <summary>
       /// Paints the background of the control.
       /// </summary>
       /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains information about the control to paint.</param>
       protected override void OnPaintBackground(PaintEventArgs pevent)
       {
           if (!_gradientHelper.OnPaintBackgroundInternal(this.ClientRectangle, pevent.Graphics))
               base.OnPaintBackground(pevent);
       }

       protected override void OnResize(EventArgs e)
       {
           base.OnResize(e);
           this.Invalidate(true);
       }
       #endregion
    }
}
