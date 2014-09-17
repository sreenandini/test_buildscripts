using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace BMC.MeterAdjustmentTool.Helpers
{
    [Designer(typeof(UxHeaderContentDesigner))]
    public partial class UxHeaderContent : UserControl
    {        
        public UxHeaderContent()
        {
            InitializeComponent();
            this.HeaderText = this.Name;
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string HeaderText
        {
            get
            {
                return uxHeader.Text;
            }
            set
            {
                uxHeader.Text = value;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Color StartColor
        {
            get { return uxHeader.StartColor; }
            set
            {
                uxHeader.StartColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Color EndColor
        {
            get { return uxHeader.EndColor; }
            set
            {
                uxHeader.EndColor = value;
                btnPin.BackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public LinearGradientMode GradientMode
        {
            get { return uxHeader.GradientMode; }
            set
            {
                uxHeader.GradientMode = value;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool PinVisible
        {
            get { return btnPin.Visible; }
            set
            {
                btnPin.Visible = value;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Padding ContentPadding
        {
            get { return pnlContent.Padding; }
            set
            {
                pnlContent.Padding = value;
            }
        }

        [Category("Content")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel ChildContainer
        {
            get { return this.pnlContent; }
        }

        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The text associated with this control.
        /// </returns>
        public override string Text
        {
            get
            {
                return this.HeaderText;
            }
            set
            {
                this.HeaderText = value;
            }
        }

        #region Pin Click Event
        /// <summary>
        /// Occurs when [pin click].
        /// </summary>
        public event EventHandler PinClick = null;

        /// <summary>
        /// Called when [pin click].
        /// </summary>
        protected void OnPinClick()
        {
            if (this.PinClick != null)
                this.PinClick(this, EventArgs.Empty);
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the btnPin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnPin_Click(object sender, EventArgs e)
        {
            this.OnPinClick();
        }
    }
}
