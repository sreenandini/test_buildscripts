using System.Windows;
using System.Globalization;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using BMC.CashDeskOperator.BusinessObjects;
using System.Data;
using BMC.Transport;
using BMC.Presentation.POS;
using BMC.Common.ConfigurationManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CFieldService.xaml
    /// </summary>
    public partial class CFieldService : UserControl, IDisposable
    {
        IFieldService fieldService = FieldServiceBusinessObject.CreateInstance();
        DataTable serviceCalls;
        string Pos, Asset, Game = string.Empty, Manufacturer, SerialNo;
        public int Installation;
        private int NoOfRecPerPage = Int32.Parse(ConfigManager.Read("ServiceRecords").ToString());
        Helper_classes.Common common = new BMC.Presentation.Helper_classes.Common();
        
        public CFieldService()
        {
            InitializeComponent();
        }

        public CFieldService(string BarPos, string AssetNo, string Machine, string Manufacturer, string SerialNo)
        {
            InitializeComponent();
            Pos = BarPos;
            Asset = AssetNo;
            Game = Machine;
            this.Manufacturer = Manufacturer;
            this.SerialNo = SerialNo;
            //txtHeader.Text = "Open Service Calls - Position [" + BarPos + "]";
            txtHeader.Text = string.Format("{0} [{1}]", FindResource("CFieldService_xaml_txtHeader") as string, BarPos);
            PopulateOpenCalls(BarPos);
            common.btnFirst = btnFirst;
            common.btnLast = btnLast;
            common.btnNext = btnNext;
            common.btnPrev = btnPrev;
            common.txtPage = txtPage;
            common.CustomPaging(BMC.Presentation.Helper_classes.Common.PagingMode.First, serviceCalls, NoOfRecPerPage, lstFieldService, false);
            common.DisplayPagingInfo(serviceCalls, common, NoOfRecPerPage);

            if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CFieldService.btnClear"))
                btnClear.Visibility = Visibility.Hidden;

            if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CFieldService.btnRequestCall"))
                btnRequest.Visibility = Visibility.Hidden;

            if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CFieldService.btnReview"))
                btnReview.Visibility = Visibility.Hidden;

            //if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CFieldService.btnEscalate"))
            //    btnEscalate.Visibility = Visibility.Hidden; 

            btnEscalate.Visibility = Visibility.Hidden;
        }

        public void PopulateOpenCalls(string BarPos)
        {
            serviceCalls = fieldService.GetOpenServiceCalls(Settings.SiteCode, BarPos);
            Helper_classes.Common.BindListView(serviceCalls.AsEnumerable(), lstFieldService);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ServiceRow row = new ServiceRow();

            if (lstFieldService.Items.Count > 0)
            {
                if (lstFieldService.SelectedItem != null)
                {
                    row.IsClear = true;
                    row.BarPos = Pos;
                    row.JobID = (lstFieldService.SelectedItem as DataRowView).Row["JobID"].ToString();
                    row.LoggedDate = (lstFieldService.SelectedItem as DataRowView).Row["LoggedDate"].ToString();
                    row.Fault = (lstFieldService.SelectedItem as DataRowView).Row["Fault"].ToString();
                    row.DownTime = (lstFieldService.SelectedItem as DataRowView).Row["DownTime"].ToString();
                    row.sAsset = Asset;

                    ViewClearServiceCall clearService = new ViewClearServiceCall(row);
                    clearService.Owner = Window.GetWindow(this);
                    clearService.ShowDialog();
                    PopulateOpenCalls(Pos);
                    common.CustomPaging(BMC.Presentation.Helper_classes.Common.PagingMode.Next, serviceCalls, NoOfRecPerPage, lstFieldService, true);
                    common.DisplayPagingInfo(serviceCalls, common, NoOfRecPerPage);
                }
                else
                    MessageBox.ShowBox("MessageID66", BMC_Icon.Warning);
            }
            else
                MessageBox.ShowBox("MessageID67", BMC_Icon.Warning);
        }

        private void btnReview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnReview.IsEnabled = false;
                ServiceRow row = new ServiceRow();

                if (lstFieldService.Items.Count > 0)
                {
                    if (lstFieldService.SelectedItem != null)
                    {
                        row.IsClear = false;
                        row.BarPos = Pos;
                        row.JobID = (lstFieldService.SelectedItem as DataRowView).Row["JobID"].ToString();
                        row.LoggedDate = (lstFieldService.SelectedItem as DataRowView).Row["LoggedDate"].ToString();
                        row.Fault = (lstFieldService.SelectedItem as DataRowView).Row["Fault"].ToString();
                        row.DownTime = (lstFieldService.SelectedItem as DataRowView).Row["DownTime"].ToString();
                        row.sAsset = Asset;

                        ViewClearServiceCall notes = new ViewClearServiceCall(row);
                        notes.Owner = Window.GetWindow(this);
                        notes.ShowDialog();
                        PopulateOpenCalls(Pos);
                        common.CustomPaging(BMC.Presentation.Helper_classes.Common.PagingMode.Next, serviceCalls, NoOfRecPerPage, lstFieldService, true);
                        common.DisplayPagingInfo(serviceCalls, common, NoOfRecPerPage);
                    }
                    else
                        MessageBox.ShowBox("MessageID68", BMC_Icon.Warning);
                }
                else
                    MessageBox.ShowBox("MessageID69", BMC_Icon.Warning);
            }
            finally
            {
                btnReview.IsEnabled = true;
            }
        }

        private void btnEscalate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnEscalate.IsEnabled = false;
                int returnval;

                if (lstFieldService.Items.Count > 0)
                {
                    if (lstFieldService.SelectedItem != null)
                    {
                        if (MessageBox.ShowBox("MessageID70", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (bool.Parse((lstFieldService.SelectedItem as DataRowView)["IsHighlighted"].ToString()))
                            {
                                MessageBox.ShowBox("MessageID71", BMC_Icon.Warning);
                                return;
                            }

                            returnval = fieldService.EscalateServiceCall((lstFieldService.SelectedItem as DataRowView).Row["JobID"].ToString(), Security.SecurityHelper.CurrentUser.SecurityUserID);
                            string sJOBID = (lstFieldService.SelectedItem as DataRowView).Row["JobID"].ToString();
                            if (returnval == 0)
                            {

                                MessageBox.ShowBox("MessageID72", BMC_Icon.Information, sJOBID);
                                PopulateOpenCalls(Pos);
                                common.CustomPaging(BMC.Presentation.Helper_classes.Common.PagingMode.Next, serviceCalls, NoOfRecPerPage, lstFieldService, true);
                                common.DisplayPagingInfo(serviceCalls, common, NoOfRecPerPage);
                                //Auditing
                                Audit("The Service Call " + sJOBID + " is escalated.", sJOBID);

                            }
                            else if (returnval == -5)
                            {
                                MessageBox.ShowBox("MessageID73", BMC_Icon.Warning);
                                Audit("The call was not escalated, the downtime fell within the response time.", sJOBID);
                                return;
                            }
                            else if (returnval == -10)
                            {
                                MessageBox.ShowBox("MessageID74", BMC_Icon.Warning);
                                Audit("The call was not escalated, Field service manager details were not set up for the depot.", sJOBID);
                                return;
                            }
                            else if (returnval == -15)
                            {
                                MessageBox.ShowBox("MessageID75", BMC_Icon.Warning);
                                Audit("The call was not escalated, user was not configured in enterprise.", sJOBID);
                                return;
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID76", BMC_Icon.Error, (lstFieldService.SelectedItem as DataRowView).Row["JobID"].ToString());
                                Audit("Failed while escalating the service call", sJOBID);
                            }
                        }
                    }
                    else
                        MessageBox.ShowBox("MessageID77", BMC_Icon.Warning);
                }
                else
                    MessageBox.ShowBox("MessageID78", BMC_Icon.Warning);
            }
            finally
            {
                btnEscalate.IsEnabled = true;
            }
        }

        private void Audit(string sDesc,string JOBID)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {
                
                AuditModuleName = ModuleName.FieldServices,
                Audit_Screen_Name = "Position Details|Field Services",
                Audit_Desc = sDesc,
                AuditOperationType = OperationType.ADD,
                Audit_Slot = Asset,
                Audit_Field = "JOB ID",
                Audit_New_Vl = JOBID                
            });
        }

        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnRequest.IsEnabled = false;
                ServiceCalls serviceCall = new ServiceCalls();
                serviceCall.Position = Pos;
                serviceCall.Game = Game;
                serviceCall.Asset = Asset;
                serviceCall.Manufacturer = Manufacturer;
                serviceCall.SerialNo = SerialNo;
                serviceCall.InstallationNo = Installation;
                serviceCall.Owner = Window.GetWindow(this);
                serviceCall.ShowDialog();
                PopulateOpenCalls(Pos);
                common.CustomPaging(BMC.Presentation.Helper_classes.Common.PagingMode.Next, serviceCalls, NoOfRecPerPage, lstFieldService, true);
                common.DisplayPagingInfo(serviceCalls, common, NoOfRecPerPage);
            }
            finally
            {
                btnRequest.IsEnabled = true;
            }
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            common.CustomPaging(BMC.Presentation.Helper_classes.Common.PagingMode.First, serviceCalls, NoOfRecPerPage, lstFieldService, false);
            common.DisplayPagingInfo(serviceCalls, common, NoOfRecPerPage);
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            common.CustomPaging(BMC.Presentation.Helper_classes.Common.PagingMode.Next, serviceCalls, NoOfRecPerPage, lstFieldService, false);
            common.DisplayPagingInfo(serviceCalls, common, NoOfRecPerPage);
        }

        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            common.CustomPaging(BMC.Presentation.Helper_classes.Common.PagingMode.Previous, serviceCalls, NoOfRecPerPage, lstFieldService, false);
            common.DisplayPagingInfo(serviceCalls, common, NoOfRecPerPage);
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            common.CustomPaging(BMC.Presentation.Helper_classes.Common.PagingMode.Last, serviceCalls, NoOfRecPerPage, lstFieldService, false);
            common.DisplayPagingInfo(serviceCalls, common, NoOfRecPerPage);
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
                        this.btnFirst.Click -= (this.btnFirst_Click);
                        this.btnPrev.Click -= (this.btnPrev_Click);
                        this.btnNext.Click -= (this.btnNext_Click);
                        this.btnLast.Click -= (this.btnLast_Click);
                        this.btnRequest.Click -= (this.btnRequest_Click);
                        this.btnClear.Click -= (this.btnClear_Click);
                        this.btnReview.Click -= (this.btnReview_Click);
                        this.btnEscalate.Click -= (this.btnEscalate_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CFieldService objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CFieldService"/> is reclaimed by garbage collection.
        /// </summary>
        ~CFieldService()
        {
            Dispose(false);
        }

        #endregion
    }

    public class ServiceRow
    {
        public string BarPos { get; set; }
        public string JobID { get; set; }
        public string LoggedDate { get; set; }
        public string DownTime { get; set; }
        public string Fault { get; set; }
        public bool IsClear { get; set; }
        public string sAsset { get; set; }
    }
}
