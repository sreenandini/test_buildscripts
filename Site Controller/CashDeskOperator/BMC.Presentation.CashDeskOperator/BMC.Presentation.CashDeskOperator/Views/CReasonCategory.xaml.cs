using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BMC.Common.LogManagement;

using Audit.Transport;
using BMC.Transport;
using Audit.BusinessClasses;
using BMC.CashDeskOperator;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CReasonCategory.xaml
    /// </summary>
    public partial class CReasonCategory : Window, IDisposable
    {
        #region Private Variables
        private CMaintenanceReasonCategory []_objMaintenanceReasonCategory;
        private ObservableCollection<CMaintenanceReasonCategory> objRC;
        private string _sKeyText = string.Empty;
        #endregion Private Variables

        #region Properties
        public CMaintenanceReasonCategory []MaintenanceReasonCategory
        {
            get { return _objMaintenanceReasonCategory; }
            set { _objMaintenanceReasonCategory = value; }
        }
        #endregion Properties

        #region Constructor

        public CReasonCategory()
        {
            InitializeComponent();
            MessageBox.childOwner = this;
            objRC = new ObservableCollection<CMaintenanceReasonCategory>();
        }

        #endregion Constructor     

        #region Events

        private void ReasonandCategory_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbCategory.LoadLookUp("MAINCA");
                cmbCategory.Focus();
                lstRC.ItemsSource = objRC;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(cmbCategory.SelectedValue != null && cmbCategory.SelectedValue.ToString().Length > 0)
                    cmbReason.LoadLookUp(int.Parse(cmbCategory.SelectedValue.ToString()));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnAdd.IsEnabled = false;
                CMaintenanceReasonCategory obj = new CMaintenanceReasonCategory();
                if (cmbCategory.SelectedValue != null && cmbCategory.SelectedValue.ToString().Length > 0)
                {
                    obj.CategoryID = int.Parse(cmbCategory.SelectedValue.ToString());
                    obj.CategoryText = cmbCategory.Text;
                }
                else
                {
                    MessageBox.ShowBox("MessageID302", BMC_Icon.Information, BMC_Button.OK);
                    cmbCategory.Focus();
                    return;
                }

                if (cmbReason.SelectedValue != null && cmbReason.SelectedValue.ToString().Length > 0)
                {
                    obj.ReasonID = int.Parse(cmbReason.SelectedValue.ToString());
                    obj.ReasonText = cmbReason.Text;

                }
                else
                {
                    MessageBox.ShowBox("MessageID303", BMC_Icon.Information, BMC_Button.OK);
                    cmbReason.Focus();
                    return;
                }

                if (txtComments.Text.Trim().Length > 0)
                {
                    obj.Comments = txtComments.Text;
                }
                else
                {
                    MessageBox.ShowBox("MessageID307", BMC_Icon.Information, BMC_Button.OK);
                    txtComments.Focus();
                    return;
                }

                if (IsExist(obj))
                {
                    MessageBox.ShowBox("MessageID311", BMC_Icon.Information, BMC_Button.OK);
                    cmbCategory.Focus();
                    return;
                }

                objRC.Add(obj);
                cmbCategory.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnAdd.IsEnabled = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (objRC != null && objRC.ToArray().Length > 0)
                {
                    _objMaintenanceReasonCategory = objRC.ToArray();
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.ShowBox("MessageID308", BMC_Icon.Information, BMC_Button.OK);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _objMaintenanceReasonCategory = null;
                DialogResult = false;
                Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ReasonandCategory_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _objMaintenanceReasonCategory = null;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void DeleteEntry(object sender, RoutedEventArgs e)
        {
            try
            {
                CMaintenanceReasonCategory obj = ((CMaintenanceReasonCategory)((Button)sender).DataContext);
                if (obj != null)
                {
                    objRC.Remove(obj);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtComments_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtComments.Text = DisplayKeyboard(txtComments.Text, string.Empty);
            txtComments.SelectionStart = txtComments.Text.Length;
        }

       #endregion Events

        #region Private Methods

        void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                _sKeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        string DisplayKeyboard(string keyText, string type)
        {
            _sKeyText = "";

            var objKeyboard = new KeyboardInterface();
            if (type == "Pwd")
            {
                objKeyboard.IsPwd = true;
            }
            objKeyboard.Closing += ObjKeyboardClosing;
            objKeyboard.KeyString = keyText;
            objKeyboard.Top = Top + Height - objKeyboard.Height;
            objKeyboard.Left = Left + Width / 2 - objKeyboard.Width / 2;
            objKeyboard.ShowDialog();
            return _sKeyText;
        }

        bool IsExist(CMaintenanceReasonCategory objMaintenanceReasonCategory)
        {
            bool bReturn = false;
            foreach (var obj in objRC)
            {
                if (obj.CategoryID == objMaintenanceReasonCategory.CategoryID &&
                    obj.ReasonID == objMaintenanceReasonCategory.ReasonID)
                {
                    bReturn = true;
                    break;
                }
            }
            return bReturn;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.UserControl.Loaded -= (this.ReasonandCategory_Loaded);
                        this.UserControl.Unloaded -= (this.ReasonandCategory_Unloaded);
                        this.cmbCategory.SelectionChanged -= (this.cmbCategory_SelectionChanged);
                        this.txtComments.PreviewMouseUp -= (this.txtComments_PreviewMouseUp);
                        this.btnAdd.Click -= (this.btnAdd_Click);
                        this.btnSave.Click -= (this.btnSave_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CReasonCategory objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CReasonCategory"/> is reclaimed by garbage collection.
        /// </summary>
        ~CReasonCategory()
        {
            Dispose(false);
        }

        #endregion

    }
}
