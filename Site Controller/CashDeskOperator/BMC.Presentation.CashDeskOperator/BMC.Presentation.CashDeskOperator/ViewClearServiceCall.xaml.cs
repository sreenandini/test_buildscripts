using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Data;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Helper_classes;
using System.Globalization;
using BMC.Transport;
using Audit.BusinessClasses;
using Audit.Transport;

namespace BMC.Presentation.POS
{
    /// <summary>
    /// Interaction logic for ViewClearServiceCall.xaml
    /// </summary>
    public partial class ViewClearServiceCall : Window, IDisposable
    {
        public DataTable notesRemedy;
        IFieldService fieldService = FieldServiceBusinessObject.CreateInstance();
        string BarPos = string.Empty;
        string Asset = string.Empty;
        private string _sKeyText = string.Empty;

        public ViewClearServiceCall()
        {
            InitializeComponent();
        }

        public ViewClearServiceCall(ServiceRow row)
        {
            InitializeComponent();

            if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CFieldService.btnReview.AddNote"))
                btnAdd.Visibility = Visibility.Hidden;

            if (!row.IsClear)
            {
                ClearCall.Visibility = Visibility.Hidden;                
                AddNote.Visibility = Visibility.Visible;
            }
            BarPos = row.BarPos;
            Asset = row.sAsset;
            PopulateValues(row);
        }

        public void PopulateValues(ServiceRow row)
        {
            txtJobID.Text = row.JobID;
            txtFault.Text = row.Fault;
            txtDate.Text = row.LoggedDate == "" ? "":DateTime.Parse(row.LoggedDate).GetUniversalDateTimeFormat();
            txtDownTime.Text = row.DownTime;

            if (row.IsClear)
            {
                txtClearHeader.Visibility = Visibility.Visible; //= "Clear Service Call";
                txtHeader.Visibility = Visibility.Hidden;
                btnAdd.Visibility = Visibility.Hidden;
                btnClear.Visibility = Visibility.Visible;
                //btnAddClear.Content = "Clear Call";
            }
            else
            {
                txtClearHeader.Visibility = Visibility.Hidden;
                txtHeader.Visibility = Visibility.Visible;
                if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CFieldService.btnReview.AddNote"))
                    btnAdd.Visibility = Visibility.Hidden;
                else
                    btnAdd.Visibility = Visibility.Visible;
                btnClear.Visibility = Visibility.Hidden;
                //txtHeader.Text = "Service Call Notes";
                //btnAddClear.Content = "Add Note";
            }
            BindControl(row.IsClear, row.JobID);            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!btnSave.IsVisible)
                this.Close();
            else
            {
                btnCancel.Content = FindResource("CTicketEntry_xaml_Button_12");
                btnSave.Visibility = Visibility.Hidden;
                if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CFieldService.btnReview.AddNote"))
                    btnAdd.Visibility = Visibility.Hidden;
                else
                    btnAdd.Visibility = Visibility.Visible;
                txtAddNotes.IsReadOnly = true;
                txtAddNotes.Text = "";
                btnCancel.Focus();
            }
        }

        public void BindControl(bool IsClear, string JobID)
        {
            if (IsClear)
            {
                notesRemedy = fieldService.GetRemedies();

                DataRow row = notesRemedy.NewRow();
                row["Call_Remedy_Description"] = "Select Any";
                row["Call_Remedy_ID"] = 0;
                notesRemedy.Rows.InsertAt(row, 0);
                cboRemedy.ItemsSource = notesRemedy.DefaultView;
                cboRemedy.DisplayMemberPath = notesRemedy.Columns["Call_Remedy_Description"].ToString();
                cboRemedy.SelectedValuePath = notesRemedy.Columns["Call_Remedy_ID"].ToString();
                cboRemedy.SelectedIndex = 0;
            }
            else
            {
                notesRemedy = fieldService.GetServiceNotes(JobID);

                Binding bind = new Binding();
                bind.Source = notesRemedy;
                lstNotes.SetBinding(ListView.ItemsSourceProperty, bind);
                
                if (lstNotes.Items.Count > 0)
                    lstNotes.SelectedIndex = 0;
            }
                
        }

        private void lstNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstNotes.Items.Count > 0)
                if (lstNotes.SelectedItem != null)
                    txtAddNotes.Text = (lstNotes.SelectedItem as DataRowView).Row["Service_Notes_Notes"].ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnAdd.IsEnabled = false;
                btnAdd.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
                //btnAdd.Content = "Save";
                txtAddNotes.Text = "";
                txtAddNotes.IsReadOnly = false;
                txtAddNotes.Focus();
                btnCancel.Content = FindResource("ViewClearServiceCall_xaml_btnCancel");
                if (Settings.OnScreenKeyboard)
                {
                    txtAddNotes.Text = DisplayKeyboard(txtAddNotes.Text, string.Empty);
                    txtAddNotes.SelectionStart = txtAddNotes.Text.Length;
                }
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
                btnSave.IsEnabled = false;
                int retValue;

                if (txtAddNotes.Text == "")
                {
                    MessageBox.ShowBox("MessageID149", BMC_Icon.Information);
                    return;
                }

                if (MessageBox.ShowBox("MessageID150", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    retValue = fieldService.InsertServiceNotes(txtJobID.Text, txtAddNotes.Text, Security.SecurityHelper.CurrentUser.UserName);

                    if (retValue > 0)
                    {
                        MessageBox.ShowBox("MessageID151", BMC_Icon.Information);

                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.FieldServices,
                            Audit_Screen_Name = "Position Details|Field Services",
                            Audit_Desc = "Review Notes for " + txtJobID.Text + " has been updated",
                            AuditOperationType = OperationType.ADD,
                            Audit_Slot = Asset,
                            Audit_Field = "JOB ID",
                            Audit_New_Vl = txtJobID.Text
                        });
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID152", BMC_Icon.Error);
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.FieldServices,
                            Audit_Screen_Name = "Position Details|Field Services",
                            Audit_Desc = "Failed while adding a note",
                            AuditOperationType = OperationType.ADD,
                            Audit_Slot = Asset

                        });
                    }

                    BindControl(false, txtJobID.Text);
                }
                btnSave.Visibility = Visibility.Hidden;
                if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CFieldService.btnReview.AddNote"))
                    btnAdd.Visibility = Visibility.Hidden;
                else
                    btnAdd.Visibility = Visibility.Visible;
                btnCancel.Content = FindResource("CTicketEntry_xaml_Button_12");
                txtAddNotes.IsReadOnly = true;
                btnAdd.Focus();
            }
            finally
            {
                btnSave.IsEnabled = true;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnClear.IsEnabled = false;
                int retValue;

                if (int.Parse(cboRemedy.SelectedValue.ToString()) < 1)
                {
                    MessageBox.ShowBox("MessageID143", BMC_Icon.Information);
                    return;
                }

                if (txtNotes.Text == "")
                {
                    MessageBox.ShowBox("MessageID144", BMC_Icon.Information);
                    return;
                }

                if (MessageBox.ShowBox("MessageID145", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    retValue = fieldService.CloseServiceCall(9999999, txtJobID.Text, int.Parse(cboRemedy.SelectedValue.ToString()), Security.SecurityHelper.CurrentUser.SecurityUserID, txtNotes.Text);

                    if (retValue == 10)
                    {
                        MessageBox.ShowBox("MessageID146", BMC_Icon.Information);

                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.FieldServices,
                            Audit_Screen_Name = "Position Details|Field Services",
                            Audit_Desc = "Cleared Service for " + txtJobID.Text,
                            AuditOperationType = OperationType.MODIFY,
                            Audit_Slot = Asset,
                            Audit_Field = "JOB ID",
                            Audit_New_Vl = txtJobID.Text
                        });


                        this.Close();
                    }
                    else if (retValue == 99)
                    {
                        MessageBox.ShowBox("MessageID147", BMC_Icon.Error);
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.FieldServices,
                            Audit_Screen_Name = "Position Details|Field Services",
                            Audit_Desc = "Service Call was not cleared.",
                            AuditOperationType = OperationType.ADD,
                            Audit_Slot = Asset
                        });
                        return;
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID148", BMC_Icon.Error);
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.FieldServices,
                            Audit_Screen_Name = "Position Details|Field Services",
                            Audit_Desc = "Failed while clearing a Service call.",
                            AuditOperationType = OperationType.ADD,
                            Audit_Slot = Asset
                        });
                    }
                }
            }
            finally
            {
                btnClear.IsEnabled = true;
            }
        }

        private void txtAddNotes_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (btnSave.IsVisible)
            {
                if (Settings.OnScreenKeyboard)
                {   
                    txtAddNotes.Text = DisplayKeyboard(txtAddNotes.Text, string.Empty);
                    txtAddNotes.SelectionStart = txtAddNotes.Text.Length;
                }
            }
        }

        

        private void txtNotes_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Settings.OnScreenKeyboard)
            {   
                txtNotes.Text = DisplayKeyboard(txtNotes.Text, string.Empty);
                txtNotes.SelectionStart = txtNotes.Text.Length;    
            }
        }

        public string DisplayKeyboard(string keyText, string type)
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
            objKeyboard.ShowDialogEx(this);
            return _sKeyText;
        }

        private void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                _sKeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

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
                        this.txtNotes.PreviewMouseUp -= (this.txtNotes_PreviewMouseUp);
                        this.lstNotes.SelectionChanged -= (this.lstNotes_SelectionChanged);
                        this.txtAddNotes.PreviewMouseUp -= (this.txtAddNotes_PreviewMouseUp);
                        this.btnAdd.Click -= (this.btnAdd_Click);
                        this.btnSave.Click -= (this.btnSave_Click);
                        this.btnClear.Click -= (this.btnClear_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("ViewClearServiceCall objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ViewClearServiceCall"/> is reclaimed by garbage collection.
        /// </summary>
        ~ViewClearServiceCall()
        {
            Dispose(false);
        }

        #endregion
    }
}
