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
    public class CPeriodCount : ComboBox
    {
        public CPeriodCount() { }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            this.Initialize();
        }

        private void Initialize()
        {
            ModuleProc PROC = new ModuleProc("CPeriodNum", "Initialize");

            try
            {
                this.DisplayMember = "Text";
                this.ValueMember = "Value";
                this.DropDownStyle = ComboBoxStyle.DropDown;
                this.Items.Clear();

                this.Items.Add(new ComboBoxItem<int>(-1, this.GetResourceTextByKey("Key_AllCriteria")));
                this.Items.Add(new ComboBoxItem<int>(1, "1"));
                this.Items.Add(new ComboBoxItem<int>(2, "2"));
                this.Items.Add(new ComboBoxItem<int>(3, "3"));
                this.Items.Add(new ComboBoxItem<int>(4, "4"));
                this.Items.Add(new ComboBoxItem<int>(5, "5"));
                this.Items.Add(new ComboBoxItem<int>(6, "6"));
                this.Items.Add(new ComboBoxItem<int>(12, "12"));
                this.Items.Add(new ComboBoxItem<int>(16, "16"));
                this.Items.Add(new ComboBoxItem<int>(24, "24"));
                this.Items.Add(new ComboBoxItem<int>(36, "36"));
                this.Items.Add(new ComboBoxItem<int>(48, "48"));
                this.Items.Add(new ComboBoxItem<int>(60, "60"));
                this.Items.Add(new ComboBoxItem<int>(90, "90"));
                this.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event CPeriodCountItemSelectedHandler ItemSelected = null;

        private void OnItemSelected()
        {
            if (this.ItemSelected != null &&
                this.SelectedItem != null)
            {
                ComboBoxItem<int> selected = this.SelectedItem as ComboBoxItem<int>;
                using (CPeriodCountItemSelectedEventArgs e = new CPeriodCountItemSelectedEventArgs()
                {
                    Count = selected.Value
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

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Enter)
            {
                this.OnSelectedIndexChanged(EventArgs.Empty);
            }
        }

        public int SelectedCount
        {
            get
            {
                int selectedValue = 0;
                if (this.SelectedItem != null)
                {
                    ComboBoxItem<int> selected = this.SelectedItem as ComboBoxItem<int>;
                    if (selected != null)
                    {
                        selectedValue = selected.Value;
                    }
                }
                else
                {
                    selectedValue = TypeSystem.GetValueInt(this.Text);
                }
                return selectedValue;
            }
            set
            {
                ModuleProc PROC = new ModuleProc("CPeriodCount", "SelectedCount");

                try
                {
                    int index = -1;
                    ComboBoxItem<int> found = (from i in this.Items.OfType<ComboBoxItem<int>>()
                                               let j = ++index
                                               where i.Value == value
                                               select i).FirstOrDefault();
                    if (found != null &&
                        (index >= 0 && index < this.Items.Count))
                    {
                        this.SelectedIndex = index;
                    }
                    else
                    {
                        this.SelectedIndex = -1;
                        this.Text = value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }
    }

    public class CPeriodCountItemSelectedEventArgs : DisposableObject
    {
        public CPeriodCountItemSelectedEventArgs() { }

        public int Count { get; set; }
    }

    public delegate void CPeriodCountItemSelectedHandler(object sender, CPeriodCountItemSelectedEventArgs e);
}
