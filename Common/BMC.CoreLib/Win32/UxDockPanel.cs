using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMC.CoreLib.Win32
{
    [Designer(typeof(UxDockPanelDesigner))]
    public partial class UxDockPanel : UserControl
    {
        private ExpandOrCollapsedEventArgs _eArgs = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AxDockPanel"/> class.
        /// </summary>
        public UxDockPanel()
        {
            InitializeComponent();
            _eArgs = new ExpandOrCollapsedEventArgs();
            this.HeaderText = this.Name;
        }

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        /// <value>The header text.</value>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string HeaderText
        {
            get
            {
                return axContent.HeaderText;
            }
            set
            {
                axContent.HeaderText = value;
                tbpHide.Text = value;
            }
        }

        [Category("Content")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel ChildContainer
        {
            get { return this.pnlContent; }
        }

        /// <summary>
        /// Gets the form that the container control is assigned to.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.Form"/> that the container control is assigned to. This property will return null if the control is hosted inside of Internet Explorer or in another hosting context where there is no parent form.
        /// </returns>
        public Form OwnerForm { get; set; }

        #region ActualWidth
        private int _actualWidth = 0;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int ActualWidth
        {
            get { return _actualWidth; }
            set
            {
                _actualWidth = value;
                this.Width = value;
            }
        }
        #endregion

        #region IsHidden
        private bool _isHidden = false;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsHidden
        {
            get { return _isHidden; }
            set
            {
                _isHidden = value;
                pnlHide.Visible = _isHidden;
                axContent.Visible = !_isHidden;

                int newWidth = 0;
                if (_isHidden)
                {
                    newWidth = pnlHide.Width + 2;
                }
                else
                {
                    newWidth = this.ActualWidth;
                }
                _eArgs.NewWidth = newWidth;
                this.OnExpandOrCollapsed();
                this.Width = newWidth;
            }
        }
        #endregion

        #region Pin Click Event
        /// <summary>
        /// Occurs when [expand or collapsed].
        /// </summary>
        public event ExpandOrCollapsedEventHandler ExpandOrCollapsed = null;

        /// <summary>
        /// Called when [expand or collapsed].
        /// </summary>
        protected void OnExpandOrCollapsed()
        {
            if (this.ExpandOrCollapsed != null)
                this.ExpandOrCollapsed(this, _eArgs);
        }


        #endregion

        /// <summary>
        /// Handles the PinClick event of the axContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void axContent_PinClick(object sender, EventArgs e)
        {
            this.IsHidden = true;
        }

        /// <summary>
        /// Handles the MouseClick event of the tabHide control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void tabHide_MouseClick(object sender, MouseEventArgs e)
        {
            this.IsHidden = false;
        }
    }
}
