using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BMC.MeterAdjustmentTool.Helpers
{
    internal class GradientHelper<T> : DisposableObject
        where T : Control, IGradientHelper
    {
        private T _source = null;

        internal GradientHelper(T source)
        {
            _source = source;
        }

        /// <summary>
        /// Gets the gradientbrush.
        /// </summary>
        /// <value>The gradientbrush.</value>
        private Brush GetGradientbrushFull(Rectangle rc, out Rectangle rc2)
        {
            rc2 = rc;
            return new LinearGradientBrush(rc2, _source.StartColor,
                   _source.EndColor, _source.GradientMode);
        }

        /// <summary>
        /// Gets the gradientbrush.
        /// </summary>
        /// <value>The gradientbrush.</value>
        private Brush GetGradientbrush1(Rectangle rc, out Rectangle rc2)
        {
            rc2 = rc;
            switch (_source.GradientMode)
            {
                case LinearGradientMode.BackwardDiagonal:
                    break;
                case LinearGradientMode.ForwardDiagonal:
                    break;

                case LinearGradientMode.Vertical:
                    rc2 = new Rectangle(rc.Left,
                        rc.Top,
                        rc.Width,
                        (rc.Height / 2));
                    break;

                default:
                    rc2 = new Rectangle(rc.Left,
                        rc.Top,
                        (rc.Width / 2),
                        rc.Height);
                    break;
            }
            return new LinearGradientBrush(rc2, _source.StartColor,
                _source.EndColor, _source.GradientMode);
        }

        /// <summary>
        /// Gets the gradientbrush.
        /// </summary>
        /// <value>The gradientbrush.</value>
        private Brush GetGradientbrush2(Rectangle rc, out Rectangle rc2)
        {
            rc2 = rc;
            switch (_source.GradientMode)
            {
                case LinearGradientMode.BackwardDiagonal:
                    break;
                case LinearGradientMode.ForwardDiagonal:
                    break;

                case LinearGradientMode.Vertical:
                    rc2 = new Rectangle(rc.Left,
                        rc.Top + (rc.Height / 2),
                        rc.Width,
                        (rc.Height / 2));
                    break;

                default:
                    rc2 = new Rectangle(rc.Left + (rc.Width / 2),
                        rc.Top,
                        (rc.Width / 2),
                        rc.Height);
                    break;
            }
            return new LinearGradientBrush(rc2, _source.EndColor,
                _source.StartColor, _source.GradientMode);
        }



        /// <summary>
        /// Called when [paint background internal].
        /// </summary>
        /// <param name="rc">The rc.</param>
        /// <param name="g">The g.</param>
        internal virtual bool OnPaintBackgroundInternal(Rectangle rc, Graphics g)
        {
            if (!rc.IsEmpty &&
                (_source.BackColor != Color.Transparent) &&
                (_source.StartColor != Color.Empty &&
                _source.EndColor != Color.Empty))
            {
                try
                {
                    Rectangle rc1 = Rectangle.Empty;
                    Rectangle rc2 = Rectangle.Empty;

                    if (_source.RepeatGradient)
                    {

                        using (Brush linearBrush = this.GetGradientbrush1(rc, out rc1))
                        {
                            // Gradient Background (first half)
                            g.FillRectangle(linearBrush, rc1);
                        }
                        using (Brush linearBrush = this.GetGradientbrush2(rc, out rc2))
                        {
                            // Gradient Background (second half)
                            g.FillRectangle(linearBrush, rc2);
                        }
                    }
                    else
                    {
                        using (Brush linearBrush = this.GetGradientbrushFull(rc, out rc1))
                        {
                            // Gradient Background (full)
                            g.FillRectangle(linearBrush, rc1);
                        }
                    }
                }
                finally { }

                return true;
            }

            return false;
        }
    }
}
