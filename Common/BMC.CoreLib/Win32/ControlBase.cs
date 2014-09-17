using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace BMC.CoreLib.Win32
{
    /// <summary>
    /// ControlBase
    /// </summary>
    public class ControlBase : Control, IDisposable
    {
        protected Rectangle _rc = Rectangle.Empty;

        protected const int FlagMouseOver = 0x0001;
        protected int _controlState = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlBase"/> class.
        /// </summary>
        protected ControlBase()
        {
            // Set the styles to make things draw properly:
            base.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true);
        }

        protected ControlBase(IContainer contaner)
            : this()
        {
            contaner.Add(this);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.SetFlag(ref _controlState, FlagMouseOver, true);
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Point pt = this.PointToClient(Cursor.Position);            
            //if (!this.ClientRectangle.Contains(pt))
            {
                this.SetFlag(ref _controlState, FlagMouseOver, false);
                this.Invalidate();
            }
            base.OnMouseLeave(e);
        }

        public bool MouseIsOver
        {
            get
            {
                return this.GetFlag(ref _controlState, FlagMouseOver);
            }
        }

        protected void SetFlag(ref int flags, int flag, bool value)
        {
            bool oldValue = ((flags & flag) != 0);

            if (value)
            {
                flags |= flag;
            }
            else
            {
                flags &= ~flag;
            }
        }

        protected bool GetFlag(ref int flags, int flag)
        {
            return ((flags & flag) == flag);
        } 

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //this.RedrawControl();
        }

        /// <summary>
        /// Ensures the objects created.
        /// </summary>
        protected virtual void EnsureObjectsCreated() { }

        /// <summary>
        /// Recalcs the client rectangle.
        /// </summary>
        protected virtual void RecalcClientRectangle()
        {
            _rc = this.ClientRectangle;
            Padding margin = this.Margin;

            _rc.X = _rc.Left + margin.Left;
            _rc.Y = _rc.Top + margin.Top;
            _rc.Width = (_rc.Width - margin.Right - margin.Left);
            _rc.Height = (_rc.Height - margin.Bottom - margin.Top);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="disposableObject">The disposable object.</param>
        protected void DisposeObject<T>(ref T disposableObject)
            where T : IDisposable
        {
            if (disposableObject != null)
            {
                disposableObject.Dispose();
                disposableObject = default(T);
            }
        }

        /// <summary>
        /// Recreates the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="disposableObject">The disposable object.</param>
        /// <param name="createObject">The create object.</param>
        /// <param name="forceToCreate">if set to <c>true</c> [force to create].</param>
        /// <param name="invalidate">if set to <c>true</c> [invalidate].</param>
        protected void RecreateObject<T>(ref T disposableObject, Func<T> createObject,
            bool forceToCreate, bool invalidate)
            where T : IDisposable
        {
            if (forceToCreate)
            {
                this.DisposeObject<T>(ref disposableObject);
            }

            if (disposableObject == null)
            {
                disposableObject = createObject();
            }

            if (invalidate)
            {
                this.RedrawControl();
            }
        }

        /// <summary>
        /// Redraws the control.
        /// </summary>
        protected virtual void RedrawControl()
        {
            this.RefreshPath();
            this.Invalidate(true);
        }

        /// <summary>
        /// Refreshes the path.
        /// </summary>
        protected virtual void RefreshPath() { }

        #region IDisposable Members

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control"/> and its child controls and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ManagedDispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Manageds the dispose.
        /// </summary>
        protected virtual void ManagedDispose() { }

        #endregion

        //public static void PaintBackColor(Graphics g, Rectangle rectangle, SolidBrush backBrush)
        //{
        //    // Common case of just painting the background.  For this, we 
        //    // use GDI because it is faster for simple things than creating
        //    // a graphics object, brush, etc.  Also, we may be able to
        //    // use a system brush, avoiding the brush create altogether.
        //    // 
        //    Color color = backBrush.col;

        //    // we use GDI to paint in most cases using WindowsGraphics 
        //    bool painted = false;
        //    if (color.A == 255)
        //    {
        //        //using (Graphics wg = Graphics.FromGraphics(e.Graphics))
        //        {
        //            color = g.GetNearestColor(color);

        //            //using (WindowsBrush brush = new WindowsSolidBrush(wg.DeviceContext, color))
        //            {
        //                g.FillRectangle(brush, rectangle);
        //            }
        //        }

        //        painted = true;
        //        /*
        //        if (DisplayInformation.BitsPerPixel > 8) {
        //            NativeMethods.RECT r = new NativeMethods.RECT(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom);
        //            SafeNativeMethods.FillRect(new HandleRef(e, e.HDC), ref r, new HandleRef(this, BackColorBrush)); 
        //            painted = true;
        //        }*/
        //    }

        //    if (!painted)
        //    {
        //        // don't paint anything from 100% transparent background
        //        //
        //        if (color.A > 0)
        //        {
        //            // Color has some transparency or we have no HDC, so we must 
        //            // fall back to using GDI+.
        //            // 
        //            using (Brush brush = new SolidBrush(color))
        //            {
        //                e.Graphics.FillRectangle(brush, rectangle);
        //            }
        //        }
        //    }
        //}

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }
    }
}
