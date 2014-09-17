using BMC.Common.Persistence;
using BMC.Common.Utilities;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace BMC.ConfigurationEditor
{
    public partial class MainForm : Form
    {
        private IDictionary<BMCInstallationType, string> _mappings = new SortedDictionary<BMCInstallationType, string>()
        {
            { BMCInstallationType.ExchangeClient, typeof(IConfig_ExchangeClient).FullName },
            { BMCInstallationType.ExchangeServer, typeof(IConfig_ExchangeServer).FullName },
            { BMCInstallationType.EnterpriseClient, typeof(IConfig_EnterpriseClient).FullName },
            { BMCInstallationType.EnterpriseServer, typeof(IConfig_EnterpriseServer).FullName },
        };

        private IConfigApplication _config = null;
        private RegClassName _classRoot = null;
        private RegClassName _selectedClass = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cboInstallationTypes.Items.Clear();
            cboInstallationTypes.DisplayMember = "Text";
            cboInstallationTypes.ValueMember = "Value";

            var configInterfaces = ConfigApplicationFactory.AvailableInterfaces;
            foreach (var configInterface in configInterfaces)
            {
                ComboBoxItem<string> cbItem = new ComboBoxItem<string>()
                {
                    Text = configInterface.Value,
                    Value = configInterface.Key
                };
                cboInstallationTypes.Items.Add(cbItem);
            }

            BMCInstallationType installationType = BMCRegistryHelper.InstallationType;
            string mapName = _mappings[installationType];
            var item = (from a in cboInstallationTypes.Items.OfType<ComboBoxItem<string>>()
                        where a.Value.IgnoreCaseCompare(mapName)
                        select a).FirstOrDefault();
            if (item != null)
            {
                cboInstallationTypes.SelectedItem = item;
            }
            else
            {
                cboInstallationTypes.SelectedIndex = 0;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboInstallationTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "cboInstallationTypes_SelectedIndexChanged");

            try
            {
                ComboBoxItem<string> cbItem = cboInstallationTypes.SelectedItem as ComboBoxItem<string>;
                _config = ConfigApplicationFactory.Get(cbItem.Value);
                ConfigKeyValuePairTopDictionary keyValues = _config.KeyValues;
                _classRoot = null;
                _selectedClass = null;
                tvwSections.Nodes.Clear();

                // find the class hierarchy
                char[] dirChars = new char[] { '\\' };
                RegClassNames classNames = new RegClassNames();
                RegClassName classParent = null;
                foreach (var pair1 in keyValues)
                {
                    string[] values = pair1.Key.Split(dirChars);
                    foreach (string value in values)
                    {
                        if (!classNames.ContainsKey(value))
                        {
                            RegClassName child = new RegClassName(value);
                            child.RefKeyName = pair1.Key;

                            TreeNode node = new TreeNode(child.ClassName, 2, 2);
                            child.Node = node;
                            node.Tag = child;

                            if (classParent != null)
                            {
                                classParent.Children.Add(child);
                            }
                            classNames.Add(value, child);

                            if (classParent == null)
                            {
                                _classRoot = child;
                                tvwSections.Nodes.Add(node);
                            }
                            else
                            {
                                classParent.Node.Nodes.Add(node);
                                classParent = child;
                            }
                        }
                        else
                        {
                            classParent = classNames[value];
                        }
                    }
                }

                if (tvwSections.Nodes.Count > 0)
                {
                    tvwSections.Nodes[0].ExpandAll();
                    tvwSections.SelectedNode = tvwSections.Nodes[0];
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void tvwSections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "tvwSections_AfterSelect");

            try
            {
                _selectedClass = e.Node.Tag as RegClassName;
                var items = _config.KeyValues[_selectedClass.RefKeyName];

                CustomClass values = new CustomClass();
                foreach (var item in items)
                {
                    object value = _config.GetObjectValue(_selectedClass.RefKeyName, item.Key);
                    CustomProperty kv = new CustomProperty(item.Key, value, false, true);
                    values.Add(kv);
                }

                pgrdKeyValues.SelectedObject = values;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void pgrdKeyValues_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "pgrdKeyValues_PropertyValueChanged");

            try
            {
                GridItem gi = e.ChangedItem;
                if (gi != null)
                {
                    _config.SetObjectValue(_selectedClass.RefKeyName, gi.Label, gi.Value);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }
}
