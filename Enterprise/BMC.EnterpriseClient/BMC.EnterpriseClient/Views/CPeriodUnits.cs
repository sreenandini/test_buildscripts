using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public class CPeriodUnits : ComboBox
    {
        private IDictionary<CPeriodUnitsType, int> _indexes = null;

        public CPeriodUnits()
        {
            _indexes = new SortedDictionary<CPeriodUnitsType, int>();
            this.DisplayMember = "Text";
            this.ValueMember = "Value";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
        }

        public void Fill()
        {
            this.Fill(CPeriodUnitsType.Periods | CPeriodUnitsType.Records | CPeriodUnitsType.Weeks);
        }

        public void Fill(CPeriodUnitsType type)
        {
            ModuleProc PROC = new ModuleProc("CPeriodUnits", "Fill");

            try
            {
                CPeriodUnitsType lastSelected = CPeriodUnitsType.None;
                int selectedIndex = 0;
                if (this.SelectedItem != null)
                {
                    try
                    {
                        lastSelected = ((ComboBoxItem<CPeriodUnitsType>)this.SelectedItem).Value;
                    }
                    catch { }
                }
                this.Items.Clear();
                _indexes.Clear();

                if ((type & CPeriodUnitsType.Records) == CPeriodUnitsType.Records)
                {
                    _indexes.Add(CPeriodUnitsType.Records, this.Items.Count);
                    this.Items.Add(new ComboBoxItem<CPeriodUnitsType>(CPeriodUnitsType.Records, this.GetResourceTextByKey("Key_CPeriodUnitsType_Records")));// TextResources.CPeriodUnitsType_Records));
                }
                if ((type & CPeriodUnitsType.Weeks) == CPeriodUnitsType.Weeks)
                {
                    _indexes.Add(CPeriodUnitsType.Weeks, this.Items.Count);
                    this.Items.Add(new ComboBoxItem<CPeriodUnitsType>(CPeriodUnitsType.Weeks, this.GetResourceTextByKey("Key_CPeriodUnitsType_Weeks")));// TextResources.CPeriodUnitsType_Weeks));
                }
                if ((type & CPeriodUnitsType.Periods) == CPeriodUnitsType.Periods)
                {
                    _indexes.Add(CPeriodUnitsType.Periods, this.Items.Count);
                    this.Items.Add(new ComboBoxItem<CPeriodUnitsType>(CPeriodUnitsType.Periods, this.GetResourceTextByKey("Key_CPeriodUnitsType_Periods")));// TextResources.CPeriodUnitsType_Periods));
                }

                if (_indexes.ContainsKey(lastSelected))
                {
                    selectedIndex = _indexes[lastSelected];
                }

                if (selectedIndex >= 0 && selectedIndex < this.Items.Count)
                    this.SelectedIndex = selectedIndex;
                else if (this.Items.Count > 0)
                    this.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event CPeriodUnitsItemSelectedHandler ItemSelected = null;

        private void OnItemSelected()
        {
            if (this.ItemSelected != null &&
                this.SelectedItem != null)
            {
                ComboBoxItem<CPeriodUnitsType> selected = this.SelectedItem as ComboBoxItem<CPeriodUnitsType>;
                using (CPeriodUnitsItemSelectedEventArgs e = new CPeriodUnitsItemSelectedEventArgs()
                {
                    Unit = selected.Value
                })
                {
                    this.ItemSelected(this, e);
                }
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            this.OnItemSelected();
        }

        public CPeriodUnitsType SelectedUnit
        {
            get
            {
                if (this.SelectedItem != null)
                {
                    ComboBoxItem<CPeriodUnitsType> selected = this.SelectedItem as ComboBoxItem<CPeriodUnitsType>;
                    if (selected != null)
                    {
                        return selected.Value;
                    }
                }
                return CPeriodUnitsType.Records;
            }
            set
            {
                ModuleProc PROC = new ModuleProc("CPeriodUnits", "SelectedUnit");

                try
                {
                    if (_indexes.ContainsKey(value))
                        this.SelectedIndex = _indexes[value];
                    else
                        this.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }
    }

    [Flags]
    public enum CPeriodUnitsType
    {
        None = 0,
        Records = 2,
        Weeks = 4,
        Periods = 8
    }

    public class CPeriodUnitsItemSelectedEventArgs : DisposableObject
    {
        public CPeriodUnitsItemSelectedEventArgs() { }

        public CPeriodUnitsType Unit { get; set; }
    }

    public delegate void CPeriodUnitsItemSelectedHandler(object sender, CPeriodUnitsItemSelectedEventArgs e);
}
