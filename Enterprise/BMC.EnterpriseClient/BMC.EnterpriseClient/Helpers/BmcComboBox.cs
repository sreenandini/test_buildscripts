using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using BMC.Common.ExceptionManagement;


namespace BMC.EnterpriseClient.Helpers
{
    public class BmcComboBox : ComboBox
    {

      /// <summary>
     /// R.RAJKUMAR FOR ComboBox ToolTip 
    /// </summary>
        private ToolTip ToolTipobject = new ToolTip();

        public BmcComboBox()
        {
            try
            {
                this.DrawMode = DrawMode.OwnerDrawFixed;
                ToolTipobject.AutoPopDelay = 5000;
                ToolTipobject.InitialDelay = 1000;
                ToolTipobject.ReshowDelay = 500;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);

            try
            {

                int width = this.DropDownWidth;
                Graphics g = this.CreateGraphics();
                Font font = this.Font;
                int vertScrollBarWidth =
                    (this.Items.Count > this.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;


                for (int iIndex = 0; iIndex < this.Items.Count; iIndex++)
                {
                    newWidth = (int)g.MeasureString(this.GetItemText(this.Items[iIndex]), font).Width
                       + vertScrollBarWidth;
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }
                this.DropDownWidth = width + 10;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            ComboBox cbo = this;

            bool hideTooltip;
            if (e.Index < 0) return;
            string text = cbo.GetItemText(cbo.Items[e.Index]);

            e.DrawBackground();

            using (SolidBrush br = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, e.Font, br, e.Bounds);

            }
            hideTooltip = true;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                Size sz = TextRenderer.MeasureText(text, e.Font, e.Bounds.Size);
                if (sz.Width > e.Bounds.Width)
                {
                    ToolTipobject.Show(text, cbo, e.Bounds.Right, e.Bounds.Bottom);

                    hideTooltip = false;
                }

            }
            if (hideTooltip)
            {
                ToolTipobject.Hide(cbo);
            }
            e.DrawFocusRectangle();
        }
    }
}
