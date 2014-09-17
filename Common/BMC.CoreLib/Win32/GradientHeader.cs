using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BMC.CoreLib.Win32
{
    public partial class GradientHeader : ControlBase, IGradientHelper
    {
        protected TextFormatFlags _formatFlags = TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.GlyphOverhangPadding;

        public GradientHeader()
        {
            _startColor = this.BackColor;
            _endColor = this.ForeColor;
            this.ResizeRedraw = true;
            _gradientHelper = new GradientHelper<GradientHeader>(this);
        }        

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.Invalidate();
            }
        }

        #region Gradient Related stuff
        private GradientHelper<GradientHeader> _gradientHelper = null;

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
            this.Invalidate();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            // Text
            if (!string.IsNullOrEmpty(this.Text))
            {
                Rectangle rc = this.ClientRectangle;
                Padding pad = this.Padding;
                Rectangle rc2 = new Rectangle(rc.Left + pad.Left, rc.Top + pad.Top,
                    (rc.Right - pad.Right - pad.Left), (rc.Bottom - pad.Bottom - pad.Top));

                this.OnChangeBounds(ref rc2);
                TextRenderer.DrawText(e.Graphics, this.Text, this.Font,
                    rc2, this.ForeColor, _formatFlags);
            }
        }

        protected virtual void OnChangeBounds(ref Rectangle rc) { }
    }
}
