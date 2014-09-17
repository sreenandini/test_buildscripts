using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;
using System.Drawing;

namespace BMC.CoreLib.Win32
{
    public class MarqueeLabel : GradientHeader
    {
        private Timer _timer = null;
        private int _scrollAmount = 10;
        private int _position = 0;

        private SolidBrush _foregroundBrush = null;

        public MarqueeLabel()
        {
            _timer = new Timer();
            _timer.Interval = 100;
            _timer.Tick += new EventHandler(OnTimer_Tick);
            _timer.Enabled = true;

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            _foregroundBrush = new SolidBrush(this.ForeColor);
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


        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                if (_foregroundBrush != null &&
                    _foregroundBrush.Color != value)
                {
                    _foregroundBrush.Dispose();
                    _foregroundBrush = new SolidBrush(value);
                    this.Invalidate();
                }
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public int ScrollTimeInterval
        {
            get { return _timer.Interval; }
            set
            {
                _timer.Interval = value;
                this.ResetPosition();
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public int ScrollAmount
        {
            get { return _scrollAmount; }
            set
            {
                _scrollAmount = value;
                this.ResetPosition();
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool CanScroll
        {
            get { return _timer.Enabled; }
            set
            {
                _timer.Enabled = value;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.ResetPosition();
        }

        private void ResetPosition()
        {
            _position = this.Width;
            this.Invalidate();
        }

        void OnTimer_Tick(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("MarqueeLabel", "OnTimer_Tick");

            try
            {
                if (!Win32Extensions.IsInDesignMode())
                {
                    if ((_position - _scrollAmount) > 0)
                    {
                        _position -= _scrollAmount;
                    }
                    else
                    {
                        _position = this.Width;
                    }
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override void OnChangeBounds(ref Rectangle rc)
        {
            rc.X = _position;
        }
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    ModuleProc PROC = new ModuleProc("MarqueeLabel", "OnTimer_Tick");
        //    base.OnPaint(e);

        //    try
        //    {
        //        //if (_position > this.Width)
        //        //{
        //        //    _position = -((int)e.Graphics.MeasureString(this.Text, this.Font).Width);
        //        //}
        //        e.Graphics.DrawString(this.Text, this.Font, _foregroundBrush, _position, 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Exception(PROC, ex);
        //    }
        //}
    }
}
