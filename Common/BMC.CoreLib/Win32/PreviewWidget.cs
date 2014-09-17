using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BMC.CoreLib.Win32
{
    internal partial class PreviewWidget : ControlBase
    {
        private Image _imageThumbnail = null;
        private Size _szText = Size.Empty;

        private static SolidBrush _textBrush = new SolidBrush(Color.White);
        private static SolidBrush _shadowBrush = new SolidBrush(Color.DarkGray);

        private const int GAP_OFFSET_SHADOW = 2;
        private const int GAP_SIZE_SHADOW = (2 * GAP_OFFSET_SHADOW);
        private const int GAP_OFFSET = 2;
        private const int GAP_SIZE = (2 * GAP_OFFSET);
        private const int GAP_SIZE_CLOSE = (8);
        private const int GAP_SIZE_CLOSE_TOP = (4);

        private CloseButton _btnClose = null;
        private Rectangle _rcClient = Rectangle.Empty;

        public event EventHandler CloseClick = null;

        [CLSCompliant(false)]
        protected TextFormatFlags _formatFlags = TextFormatFlags.Left |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.GlyphOverhangPadding;

        internal PreviewWidget(int width, int height, Image image, bool titleOnBottom)
        {
            this.Width = width;
            this.Height = height;
            this.TitleOnBottom = titleOnBottom;
            this.LoadImage(image);
            this.LoadCloseButton();
        }

        private void LoadImage(Image image)
        {
            this.CalculateTextSize();
            _szText.Height += 4;
            this.Height += _szText.Height + GAP_SIZE_SHADOW;
            this.Width += GAP_SIZE_SHADOW;

            Rectangle rc = this.ClientRectangle;
            _rcClient = new Rectangle(rc.Left + GAP_OFFSET_SHADOW, rc.Top + GAP_OFFSET_SHADOW,
                                      rc.Width - GAP_SIZE_SHADOW, rc.Height - GAP_SIZE_SHADOW);

            using (image)
            {
                _imageThumbnail = image.GetThumbnailImage(_rcClient.Width - GAP_SIZE,
                                                        _rcClient.Height - _szText.Height - GAP_SIZE,
                                                        null, IntPtr.Zero);
            }
        }

        private void LoadCloseButton()
        {
            _btnClose = new CloseButton();
            _btnClose.ParentControl = this;
            int gap = (this.TitleOnBottom ? GAP_SIZE_CLOSE : GAP_SIZE_CLOSE_TOP);
            _btnClose.Location = new Point((this.Width - gap - _btnClose.Width), (this.Top + gap));
            _btnClose.Click += new EventHandler(OnBtnClose_Click);
            this.Controls.Add(_btnClose);
        }

        void OnBtnClose_Click(object sender, EventArgs e)
        {
            if (this.CloseClick != null)
            {
                this.CloseClick(this, EventArgs.Empty);
            }
        }

        private Color _borderColor = Color.FromArgb(255, 155, 160, 166);

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }

        private Color _borderColorHover = Color.FromArgb(255, 156, 161, 167);

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public Color BorderColorHover
        {
            get { return _borderColorHover; }
            set
            {
                _borderColorHover = value;
                this.Invalidate();
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                this.CalculateTextSize();
            }
        }

        public bool TitleOnBottom { get; set; }

        private void CalculateTextSize()
        {
            _szText = TextRenderer.MeasureText("SAMPLE", this.Font);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);            
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;            
            if (this.MouseIsOver)
            {
                g.FillRectangle(_shadowBrush, this.ClientRectangle);
            }
            Rectangle rcImage = Rectangle.Empty;
            Rectangle rcText = Rectangle.Empty;

            if (this.TitleOnBottom)
            {
                rcImage = new Rectangle(_rcClient.X + GAP_OFFSET,
                                        _rcClient.Y + GAP_OFFSET,
                                        _rcClient.Width - GAP_SIZE,
                                        _rcClient.Height - _szText.Height - GAP_SIZE);
                rcText = new Rectangle(rcImage.X, rcImage.Y + rcImage.Height,
                                        rcImage.Width, _szText.Height);
            }
            else
            {
                rcText = new Rectangle(_rcClient.X + GAP_OFFSET,
                                        _rcClient.Y + GAP_OFFSET,
                                        _rcClient.Width - GAP_SIZE, _szText.Height + 2);
                rcImage = new Rectangle(rcText.X,
                                        rcText.Y + rcText.Height,
                                        rcText.Width,
                                        _rcClient.Height - rcText.Height - GAP_SIZE);
            }

            ControlPaint.DrawBorder(e.Graphics, _rcClient, (this.MouseIsOver ? this.BorderColorHover : this.BorderColor), ButtonBorderStyle.Solid);
            if (!this.MouseIsOver)
            {
                //ControlPaint.DrawBorder3D(g, this.ClientRectangle, Border3DStyle.Bump);
                ControlPaint.DrawImageDisabled(g, _imageThumbnail, rcImage.X, rcImage.Y, this.BackColor);
            }
            else
            {
                g.DrawImage(_imageThumbnail, rcImage);
            }
            g.FillRectangle(_textBrush, rcText);
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, rcText, this.ForeColor, _formatFlags);
        }
    }
}
