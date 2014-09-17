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
    /// <summary>
    /// ContinuousProgress
    /// </summary>
    public partial class ContinuousProgress : ControlBase
    {
        #region Private Variables
        public enum ShapeType
        {
            Square = 0,
            Circle = 1
        }

        private const int SIZE_INCR = 2;
        private const int MIN_INTERVAL = 100;
        private Brush _activeBrush = null;
        private Brush _normalBrush = null;

        private Timer _cycleTimer = null;
        private int _shapeCount = 0;
        private int _currentActiveItem = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AxContinuousProgress"/> class.
        /// </summary>
        public ContinuousProgress()
        {
            // Default values
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            _cycleTimer = new Timer();
            _cycleTimer.Stop();
            _cycleTimer.Interval = this.CycleSpeed;
            _cycleTimer.Tick += new EventHandler(OnCycleTimer_Tick);
        }
        #endregion

        #region Properties
        #region ShapeToDraw
        private ShapeType _shapeToDraw = ShapeType.Square;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public ShapeType ShapeToDraw
        {
            get { return _shapeToDraw; }
            set
            {
                if (value != _shapeToDraw)
                {
                    _shapeToDraw = value;
                    _currentActiveItem = 0;
                    this.Invalidate();
                }
            }
        }
        #endregion

        #region Shape Size
        private int _shapeSize = 5;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public int ShapeSize
        {
            get { return _shapeSize; }
            set
            {
                if (value != _shapeSize
                    && value > 0)
                {
                    _shapeSize = value;
                    _currentActiveItem = 0;
                    this.RecalcCountAndInterval();
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region Shape Spacing
        private int _shapeSpacing = 5;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public int ShapeSpacing
        {
            get { return _shapeSpacing; }
            set
            {
                if (value != _shapeSpacing
                    && value > 0)
                {
                    _shapeSpacing = value;
                    _currentActiveItem = 0;
                    this.RecalcCountAndInterval();
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region Cycle Speed
        private int _cycleSpeed = 1000;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Behavoir")]
        public int CycleSpeed
        {
            get { return _cycleSpeed; }
            set
            {
                if (value != _cycleSpeed)
                {
                    if (value < 1000) value += 1000;
                    _cycleSpeed = value;
                    _shapeCount = 0;
                    this.RecalcCountAndInterval();
                }
            }
        }

        #endregion

        #region BorderStype
        private Border3DStyle _BorderStyle = Border3DStyle.Flat;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public Border3DStyle BorderStyle
        {
            get { return _BorderStyle; }
            set
            {
                if (value != _BorderStyle)
                {
                    _BorderStyle = value;
                    this.Invalidate();
                }
            }
        }
        #endregion

        #region Active Color
        private Color _activeColor = SystemColors.Highlight;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        public Color ActiveColor
        {
            get { return _activeColor; }
            set
            {
                if (value != _activeColor)
                {
                    _activeColor = value;
                    _currentActiveItem = 0;
                    this.RecalcCountAndInterval();
                    this.RecreateActiveBrush(true, true);
                }
            }
        }

        #endregion

        #region Shape Count
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int ShapeCount
        {
            get { return _shapeCount; }
        }
        #endregion

        #region IsActive
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsActive
        {
            get { return (_cycleTimer.Enabled); }
        }
        #endregion
        #endregion

        #region Private Methods
        /// <summary>
        /// Recalcs the count and interval.
        /// </summary>
        private void RecalcCountAndInterval()
        {
            int width = this.Width;
            int newShapeCount = 1;

            if (_shapeSize > 0 && _shapeSpacing > 0)
            {
                newShapeCount = (int)Math.Floor((double)(width - _shapeSpacing) / (_shapeSize + _shapeSpacing));
            }

            if (newShapeCount != _shapeCount &&
                newShapeCount > 0)
            {
                int interval = (_cycleSpeed / newShapeCount);
                if (interval >= MIN_INTERVAL)
                {
                    _cycleTimer.Interval = interval;
                }
                else
                {
                    _cycleTimer.Interval = MIN_INTERVAL;
                }
                _shapeCount = newShapeCount;
            }
        }

        /// <summary>
        /// Calculates the item position.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private Point CalculateItemPosition(int index)
        {
            return new Point(((_shapeSpacing * index) + (_shapeSize * (index - 1))),
                ((this.Height / 2) - (_shapeSize / 2)));
        }

        /// <summary>
        /// Calcs the item rectangle.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private Rectangle CalcItemRectangle(int index)
        {
            Point pt = this.CalculateItemPosition(index);
            int size = 0;
            this.ReposItemRectangle(ref pt, ref size, true);
            return new Rectangle(pt.X, pt.Y, size, size);
        }

        /// <summary>
        /// Reposes the item rectangle.
        /// </summary>
        /// <param name="pt">The pt.</param>
        /// <param name="size">The size.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        private void ReposItemRectangle(ref Point pt, ref int size, bool isActive)
        {
            if (isActive)
            {
                pt.Offset(-1 * SIZE_INCR, -1 * SIZE_INCR);
                size = (_shapeSize + (2 * SIZE_INCR));
            }
            else
            {
                size = _shapeSize;
            }
        }

        /// <summary>
        /// Called when [cycle timer_ tick].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnCycleTimer_Tick(object sender, EventArgs e)
        {
            int oldActiveItem = _currentActiveItem;

            if (_currentActiveItem >= _shapeCount) _currentActiveItem = 1;
            else _currentActiveItem++;

            this.Invalidate(this.CalcItemRectangle(oldActiveItem));
            this.Invalidate(this.CalcItemRectangle(_currentActiveItem));
        }

        /// <summary>
        /// Recreates the normal brush.
        /// </summary>
        /// <param name="forceToCreate">if set to <c>true</c> [force to create].</param>
        /// <param name="invalidate">if set to <c>true</c> [invalidate].</param>
        private void RecreateNormalBrush(bool forceToCreate, bool invalidate)
        {
            this.RecreateObject(ref _normalBrush, () => { return new SolidBrush(this.ForeColor); },
                forceToCreate, invalidate);
        }

        /// <summary>
        /// Recreates the active brush.
        /// </summary>
        /// <param name="forceToCreate">if set to <c>true</c> [force to create].</param>
        /// <param name="invalidate">if set to <c>true</c> [invalidate].</param>
        private void RecreateActiveBrush(bool forceToCreate, bool invalidate)
        {
            this.RecreateObject(ref _activeBrush, () => { return new SolidBrush(this.ActiveColor); },
                forceToCreate, invalidate);
        }

        /// <summary>
        /// Disposes the normal brush.
        /// </summary>
        private void DisposeNormalBrush()
        {
            this.DisposeObject(ref _normalBrush);
        }

        /// <summary>
        /// Disposes the active brush.
        /// </summary>
        private void DisposeActiveBrush()
        {
            this.DisposeObject(ref _activeBrush);
        }

        /// <summary>
        /// Ensures the objects created.
        /// </summary>
        protected override void EnsureObjectsCreated()
        {
            base.EnsureObjectsCreated();
            this.RecreateNormalBrush(false, false);
            this.RecreateActiveBrush(false, false);
        }

        /// <summary>
        /// Redraws the control.
        /// </summary>
        protected override void RedrawControl() { }

        /// <summary>
        /// Draws the shape.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="shapeToDraw">The shape to draw.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="size">The size.</param>
        private void DrawShape(Graphics g, Brush brush, ShapeType shapeToDraw, int x, int y, int size)
        {
            switch (_shapeToDraw)
            {
                case ShapeType.Square:
                    g.FillRectangle(brush, x, y, size, size);
                    break;
                case ShapeType.Circle:
                    g.FillEllipse(brush, x, y, size, size);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Overridable Methods
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            this.EnsureObjectsCreated();
            Graphics g = e.Graphics;
            ControlPaint.DrawBorder3D(g, this.ClientRectangle, this.BorderStyle);

            if (_shapeCount > 0)
            {
                for (int i = 1; i <= _shapeCount; i++)
                {
                    Point pt = this.CalculateItemPosition(i);
                    int size = 0;
                    bool isActive = (i == _currentActiveItem);
                    this.ReposItemRectangle(ref pt, ref size, isActive);
                    this.DrawShape(g, (isActive ? _activeBrush : _normalBrush), this.ShapeToDraw, pt.X, pt.Y, size);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            this.RecalcCountAndInterval();
            base.OnResize(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            _cycleTimer.Enabled = this.Enabled;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            this.RecreateNormalBrush(true, true);
        }

        /// <summary>
        /// Manageds the dispose.
        /// </summary>
        protected override void ManagedDispose()
        {
            base.ManagedDispose();
            this.DisposeNormalBrush();
            this.DisposeActiveBrush();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            if (!_cycleTimer.Enabled)
                _cycleTimer.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (_cycleTimer.Enabled)
                _cycleTimer.Stop();
        }
        #endregion
    }
}
