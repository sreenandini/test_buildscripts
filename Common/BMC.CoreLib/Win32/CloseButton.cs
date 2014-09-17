using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace BMC.CoreLib.Win32
{
    public class CloseButton : ControlBase
    {
        private static Image IMAGE_CLOSE = null;
        private static Image IMAGE_CLOSE_GREY = null;

        private ControlBase _parentControl = null;

        static CloseButton()
        {
            IMAGE_CLOSE = Bitmap.FromStream(typeof(PreviewWidget).Assembly.GetManifestResourceStream("BMC.CoreLib.Win32.Close18.bmp"));
            IMAGE_CLOSE_GREY = Bitmap.FromStream(typeof(PreviewWidget).Assembly.GetManifestResourceStream("BMC.CoreLib.Win32.Close18Grey.bmp"));
        }

        public CloseButton()
        {
            this.Width = 18;
            this.Height = 18;
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public ControlBase ParentControl
        {
            get { return _parentControl; }
            set
            {
                _parentControl = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            bool isMouseOver = this.MouseIsOver;

            if (_parentControl != null)
            {
                isMouseOver = _parentControl.MouseIsOver;
            }
            if (isMouseOver)
            {
                g.DrawImage(IMAGE_CLOSE, this.ClientRectangle);
            }
            else
            {
                g.DrawImage(IMAGE_CLOSE_GREY, this.ClientRectangle);
            }
        }
    }
}
