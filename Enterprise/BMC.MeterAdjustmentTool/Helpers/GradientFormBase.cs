using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace BMC.MeterAdjustmentTool.Helpers
{
    public partial class GradientFormBase : Form, IUserInterface, IGradientHelper
    {
        protected bool _isNotSaved = true;        

        public GradientFormBase()
        {
            InitializeComponent();            
            _startColor = SystemColors.Control;
            _endColor = SystemColors.Control;
            _gradientHelper = new GradientHelper<GradientFormBase>(this);
        }

        #region Gradient Related stuff
        private GradientHelper<GradientFormBase> _gradientHelper = null;

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

        protected IntPtr RefWindowHandle { get; set; }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains information about the control to paint.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (!_gradientHelper.OnPaintBackgroundInternal(this.ClientRectangle, pevent.Graphics))
                base.OnPaintBackground(pevent);
        }

        protected void OnPaintBackgroundInternal(ref Message m)
        {
            RECT rc = new RECT();
            UnManagedMethods.GetClientRect(m.HWnd, ref rc);
            Rectangle rc2 = new Rectangle(rc.left, rc.top, (rc.right - rc.left), (rc.bottom - rc.top));

            Graphics g = Graphics.FromHdc(m.WParam);
            _gradientHelper.OnPaintBackgroundInternal(rc2, g);
            g.Dispose();
            m.Result = (IntPtr)1;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }
        #endregion

        #region Form Events
        private void GradientFormBase_Load(object sender, EventArgs e)
        {            
            this.LoadUI();
        }
        #endregion

        #region IUserInterface Members

        /// <summary>
        /// Loads the form.
        /// </summary>
        protected void LoadForm()
        {
            this.LoadUI();
        }

        /// <summary>
        /// Loads the UI.
        /// </summary>
        public void LoadUI()
        {
            this.LoadResourceValues();
            this.LoadChanges();
        }

        /// <summary>
        /// Loads the changes.
        /// </summary>
        protected virtual void LoadChanges()
        {            
        }

        /// <summary>
        /// Loads the resource values.
        /// </summary>
        protected virtual void LoadResourceValues() { }

        /// <summary>
        /// Saves the UI.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        public bool SaveUI()
        {
            if (this.ValidateUI())
            {
                return this.SaveChanges();
            }
            return false;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>        
        protected virtual bool SaveChanges() { return true; }

        /// <summary>
        /// Validates the UI.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool ValidateUI()
        {
            return true;
        }

        /// <summary>
        /// Called when [accept button clicked].
        /// </summary>
        protected void OnAcceptButtonClicked()
        {
            if (this.SaveUI())
            {
                _isNotSaved = false;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Called when [cancel button clicked].
        /// </summary>
        protected void OnCancelButtonClicked()
        {
            this.Close();
        }

        #endregion

        private void GradientFormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnBeforeFormClosing(e.CloseReason);
        }

        protected virtual void OnBeforeFormClosing(CloseReason reason) { }
    }
}
