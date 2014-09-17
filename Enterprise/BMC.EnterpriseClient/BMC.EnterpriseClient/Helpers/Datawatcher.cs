using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMC.EnterpriseClient.Helpers
{

    /// <summary>
    /// RAJKUMAR.R FOR DATA MODIFIED ON FORM CLOSE EVENT
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public class Datawatcher
    {
        private Form _ownerForm = null;
        private bool _FormDataChanged = false;
        private Action<Datawatcher, Form> _afterLoad = null;

        public Datawatcher(Form ownerForm)
            : this(ownerForm, null) { }

        public Datawatcher(Form ownerForm,
            Action<Datawatcher, Form> afterLoad)
            : this(ownerForm, afterLoad, false) { }

        public Datawatcher(Form ownerForm,
            Action<Datawatcher, Form> afterLoad, bool isUserClosing)
        {
            try
            {
                _afterLoad = afterLoad;
                _ownerForm = ownerForm;
                _ownerForm.Load += OwnerForm_Load;
                if (!isUserClosing) _ownerForm.FormClosing += OwnerForm_FormClosing;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

       public void OwnerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogManager.WriteLog(_ownerForm.Name + "Form Closed", LogManager.enumLogLevel.Info);

            try
            {
                if (this.DataModify)
                {
                    if (_ownerForm.ShowQuestionMessageBox(BMC.Common.ResourceExtensions.GetResourceTextByKey(null, 1, "MSG_CONFIRM_CLOSE")) != DialogResult.Yes)
                    {
                        e.Cancel = true;
                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void OwnerForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitializeControls();
                this.AddControlToWatcher(_ownerForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (_afterLoad != null)
                {
                    try
                    {
                        _afterLoad(this, _ownerForm);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
        }

        /// <summary>
        /// RAJKUMAR.R FOR DATA MODIFIED ON FORM CLOSE EVENT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ControlDataChanged(object sender, EventArgs e)
        {
            _FormDataChanged = true;
        }

        private void CheckListBoxDataChanged(object sender, ItemCheckEventArgs e)
        {
            _FormDataChanged = true;
        }

        private void ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _FormDataChanged = true;
        }

        private void AfterCheck(object sender, TreeViewEventArgs e)
        {
            _FormDataChanged = true;
        }

        public bool DataModify
        {
            get
            {
                return _FormDataChanged;
            }
            set
            {
                _FormDataChanged = value;
            }
        }


        private void checkControls(Control.ControlCollection collection)
        {

            try
            {
                if (collection == null) return;

                foreach (Control c in collection)
                {
                    checkControls(c.Controls);
                    AddControlToWatcher(c);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        public void InitializeControls()
        {
            checkControls(_ownerForm.Controls);
        }

        public void AddControlToWatcher(Control c)
        {

            try
            {

                if (c is Button)
                {
                    return; //Dont assign button event by default.    
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).CheckedChanged += ControlDataChanged;
                }
                else if (c is RadioButton)
                {
                    ((RadioButton)c).CheckedChanged += ControlDataChanged;
                }
                else if (c is CheckedListBox)
                {
                    ((CheckedListBox)c).ItemCheck += CheckListBoxDataChanged;
                }
                else if (c is ListBox)
                {
                    ((ListBox)c).SelectedIndexChanged += ControlDataChanged;
                }
                else if (c is ListView)
                {
                    ((ListView)c).ItemCheck += CheckListBoxDataChanged;
                    ((ListView)c).SelectedIndexChanged += ControlDataChanged;
                    ((ListView)c).ItemSelectionChanged += ItemSelectionChanged;
                }
                else if (c is TreeView)
                {
                    ((TreeView)c).AfterCheck += AfterCheck;
                }
                else if (c is Button)
                {
                    c.Click += new EventHandler(ControlDataChanged);
                }
                else if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedIndexChanged += ControlDataChanged;
                }

                else
                {
                    c.TextChanged += new EventHandler(ControlDataChanged);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public void RemoveControlFromWatcher(Control c)
        {
            try
            {
                if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedIndexChanged -= ControlDataChanged;
                }
                else if (c is ListBox)
                {
                    ((ListBox)c).SelectedIndexChanged -= ControlDataChanged;
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).CheckedChanged -= ControlDataChanged;
                }
                else if (c is ListBox)
                {
                    ((ListBox)c).SelectedIndexChanged -= ControlDataChanged;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}




