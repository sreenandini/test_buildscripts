using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Presentation.POS.Helper_classes;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.CashDeskOperator;
using BMC.Security;
using BMC.Common.LogManagement;
using BMC.Common;

namespace BMC.Presentation
{
    public partial class CVoid : IDisposable
	{
		public CVoid()
		{
			InitializeComponent();
            _common.btnFirst = btnFirst;
            _common.btnLast = btnLast;
            _common.btnNext = btnNext;
            _common.btnPrev = btnPrev;
            _common.txtPage = txtPage;
            FillData();
            _common.CustomPaging(Helper_classes.Common.PagingMode.First, _dtTabVoid, _pagingNoOfRecPerPage, lstVoidTransaction, false);
            _common.DisplayPagingInfo(_dtTabVoid, _common, _pagingNoOfRecPerPage);

            if (!SecurityHelper.HasAccess("BMC.Presentation.Void.btnVOID"))
                btnVoid.Visibility = Visibility.Hidden;
            //if (BMC.Transport.Settings.CAGE_ENABLED)
            //{
            //    btnVoid.Visibility = Visibility.Hidden;
            //}
		}

	    readonly DataTable _dtTabVoid = new DataTable("Void");

        private readonly int _pagingNoOfRecPerPage = Int32.Parse(ConfigManager.Read("VoidRecords"));
        GridViewColumnHeader _lastHeaderClicked;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
	    readonly Helper_classes.Common _common = new Helper_classes.Common();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(lstVoidTransaction.Items.Count<=0)
            {
                MessageBox.ShowBox("MessageID115", BMC_Icon.Information);
                return;
            }
            if (lstVoidTransaction.SelectedItems.Count == 0) return;

            int returnValue;
            string strType;
            string amount;

            var objCDOEntity = new VoidTranCreate();
            var objCDO = VoidTransactionBusinessObject.CreateInstance();

            try
            {


                if ((((GetVoidTransactionListResult)lstVoidTransaction.SelectedItem)).Treasury_Reason != "NEGATIVE TREASURY ENTRY")
                {
                    if (MessageBox.ShowBox("MessageID109", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        objCDOEntity.TreasuryID = ((GetVoidTransactionListResult)lstVoidTransaction.SelectedItem).Treasury_No.ToString();
                        objCDOEntity.UserNo = SecurityHelper.CurrentUser.User_No.ToString();
                        objCDOEntity.Date = DateTime.Now;
                            //clsSecurity.UserNo.ToString();
                            //SecurityHelper.CurrentUser.User_No.ToString();
                           // ((GetVoidTransactionListResult)lstVoidTransaction.SelectedItem).User_No.ToString();
                        strType = ((GetVoidTransactionListResult)lstVoidTransaction.SelectedItem).Type;
                        amount = BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(((GetVoidTransactionListResult)lstVoidTransaction.SelectedItem).Amount.Value);
                        returnValue = objCDO.VoidTransaction(objCDOEntity);

                        if (returnValue > 0)
                        {
                            MessageBox.ShowBox("MessageID125", BMC_Icon.Information);

                            AuditViewerBusiness.InsertAuditData(new Audit_History
                            {

                                AuditModuleName = ModuleName.Void,
                                Audit_Screen_Name = "Void|VoidTransaction",
                                Audit_Desc = strType + " Date: " + ((GetVoidTransactionListResult)lstVoidTransaction.SelectedItem).FormattedDate
                                                            + " Amount: " + amount,
                                AuditOperationType = OperationType.ADD,
                                Audit_Field = "Treasury Number",
                                Audit_New_Vl = ((GetVoidTransactionListResult)lstVoidTransaction.SelectedItem).Treasury_No.ToString()
                            });


                            (oCommonUtilities.CreateInstance()).PrintCommonReceipt(true, strType, returnValue.ToString());
                            FillData();
                            _common.CustomPaging(Helper_classes.Common.PagingMode.Next, _dtTabVoid, _pagingNoOfRecPerPage, lstVoidTransaction, true);
                            _common.DisplayPagingInfo(_dtTabVoid, _common, _pagingNoOfRecPerPage);
                        }
                        else
                        {
                            switch (returnValue)
                            {
                                case -2://LockExists
                                case -3://LockError
                                    {
                                        MessageBox.ShowBox("MessageID375", BMC_Icon.Error);

                                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                        {

                                            AuditModuleName = ModuleName.Void,
                                            Audit_Screen_Name = "Void|VoidTransaction",
                                            Audit_Desc = "Locked by another user for processing.",
                                            AuditOperationType = OperationType.ADD
                                        });

                                        break;
                                    }
                                case -4://DatabaseError
                                    {
                                        MessageBox.ShowBox("MessageID374", BMC_Icon.Error);

                                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                        {

                                            AuditModuleName = ModuleName.Void,
                                            Audit_Screen_Name = "Void|VoidTransaction",
                                            Audit_Desc =  "Unable to Access the database.",
                                            AuditOperationType = OperationType.ADD
                                        });
                                        break;
                                    }
                                case -5://Cashdesk transaction included in drop
                                    {
                                        MessageBox.ShowBox("MessageID546", BMC_Icon.Information);
                                        AuditViewerBusiness.InsertAuditData(new Audit_History
                                        {

                                            AuditModuleName = ModuleName.Void,
                                            Audit_Screen_Name = "Void|VoidTransaction",
                                            Audit_Desc = "Transaction already included in drop.",
                                            AuditOperationType = OperationType.ADD
                                        });

										FillData();

                                        break;
                                    }
                                default:
                                    {
                                        MessageBox.ShowBox("MessageID126", BMC_Icon.Information);
                                        AuditViewerBusiness.InsertAuditData(new Audit_History
                                        {

                                            AuditModuleName = ModuleName.Void,
                                            Audit_Screen_Name = "Void|VoidTransaction",
                                            Audit_Desc = "Error occured while voiding this transaction.",
                                            AuditOperationType = OperationType.ADD
                                        });
                                        break;
                                    }

                            }
                        }
                        //else if (returnValue == -1)
                        //{
                        //    MessageBox.ShowBox("MessageID126", BMC_Icon.Information);
                        //    AuditViewerBusiness.InsertAuditData(new Audit_History
                        //    {

                        //        AuditModuleName = ModuleName.Void,
                        //        Audit_Screen_Name = "Void|VoidTransaction",
                        //        Audit_Desc = "Error occured while voiding this transaction.",
                        //        AuditOperationType = OperationType.ADD
                        //    });

                        //}
                        //else
                        //    MessageBox.ShowBox("MessageID127", BMC_Icon.Error);
                    }
                    else
                        return;
                }
                else
                    MessageBox.ShowBox("MessageID128", BMC_Icon.Information);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public void FillData()
        {

            try
            {
				var voidTransactionHelper = new VoidTransactionHelper();
                lstVoidTransaction.DataContext = voidTransactionHelper.GetVoidTransactionList();
           
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #region Sort Code

        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            try
            {
                var headerClicked = e.OriginalSource as GridViewColumnHeader;
                ListSortDirection direction;

                if (headerClicked != null)
                {
                    if (_lastHeaderClicked != null)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var header = headerClicked.Column.Header as string;
                    if (header == "Date")
                    {
                        header = "FormattedDate";
                    }
                    Sort(header, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Header Click : " + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);

            }
        }


        private void Sort(string sortBy, ListSortDirection direction)
        {
            if (sortBy == "Game Title")
            {
                sortBy = "Game_Title";
            }
            lstVoidTransaction.Items.SortDescriptions.Clear();
            var sd = new SortDescription(sortBy, direction);
            lstVoidTransaction.Items.SortDescriptions.Add(sd);

            lstVoidTransaction.Items.Refresh();
        }

        #endregion


        private void btnNext_Click(object sender, EventArgs e)
        {
            _common.CustomPaging(Helper_classes.Common.PagingMode.Next, _dtTabVoid, _pagingNoOfRecPerPage, lstVoidTransaction, false);
            _common.DisplayPagingInfo(_dtTabVoid, _common, _pagingNoOfRecPerPage);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            _common.CustomPaging(Helper_classes.Common.PagingMode.Previous, _dtTabVoid, _pagingNoOfRecPerPage, lstVoidTransaction, false);
            _common.DisplayPagingInfo(_dtTabVoid, _common, _pagingNoOfRecPerPage);
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            _common.CustomPaging(Helper_classes.Common.PagingMode.First, _dtTabVoid, _pagingNoOfRecPerPage, lstVoidTransaction, false);
            _common.DisplayPagingInfo(_dtTabVoid, _common, _pagingNoOfRecPerPage);
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            _common.CustomPaging(Helper_classes.Common.PagingMode.Last, _dtTabVoid, _pagingNoOfRecPerPage, lstVoidTransaction, false);
            _common.DisplayPagingInfo(_dtTabVoid, _common, _pagingNoOfRecPerPage);
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
                        this.lstVoidTransaction.RemoveHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.GridViewColumnHeaderClickedHandler));
                        this.btnVoid.Click -= (this.Button_Click);
                        this.btnPrev.Click -= (this.btnPrev_Click);
                        this.btnFirst.Click -= (this.btnFirst_Click);
                        this.btnNext.Click -= (this.btnNext_Click);
                        this.btnLast.Click -= (this.btnLast_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CVoid objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CVoid"/> is reclaimed by garbage collection.
        /// </summary>
        ~CVoid()
        {
            Dispose(false);
        }

        #endregion
    }
}