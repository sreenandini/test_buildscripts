using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BMC.MeterAdjustmentTool.Helpers
{
    /// <summary>
    /// ControlBase
    /// </summary>
    public class ControlBase : Control, IDisposable
    {
        protected Rectangle _rc = Rectangle.Empty;

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
    }
}
