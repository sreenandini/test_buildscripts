using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace BMC.MeterAdjustmentTool.Helpers
{
    public class UxHeaderContentDesigner : ParentControlDesigner
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            if (this.Control is UxHeaderContent)
            {
                this.EnableDesignMode(((UxHeaderContent)this.Control).ChildContainer, "Child");
            }
        }
    }

    //public class UxDockPanelDesigner : ParentControlDesigner
    //{
    //    public override void Initialize(System.ComponentModel.IComponent component)
    //    {
    //        base.Initialize(component);
    //        if (this.Control is UxDockPanel)
    //        {
    //            this.EnableDesignMode(((UxDockPanel)this.Control).ChildContainer, "ChildContainer");
    //        }
    //    }
    //}
}
